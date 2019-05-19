using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using BinaryIO;
using MineCore.Data;
using MineCore.Data.Impl;
using MineCore.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Optional;

namespace MineCore.Blocks.Impl
{
    public class BlockFactory : IBlockFactory
    {
        private int _runtimeId;
        private ConcurrentDictionary<int, Type> _blockFactory = new ConcurrentDictionary<int, Type>();

        private ConcurrentDictionary<int, Func<int, IBlock>>
            _blockCreateFunc = new ConcurrentDictionary<int, Func<int, IBlock>>();

        private ConcurrentDictionary<int, IRuntimeBlockData> _runtimeBlockDatas =
            new ConcurrentDictionary<int, IRuntimeBlockData>();

        private ConcurrentDictionary<int, int> _runtimeToIndex = new ConcurrentDictionary<int, int>();
        private ConcurrentDictionary<int, int> _indexToRuntime = new ConcurrentDictionary<int, int>();

        private byte[] _compiledTable;

        public BlockFactory()
        {
            JArray datas = JArray.Parse(Encoding.UTF8.GetString(FileResources.RuntimeIdTable));
            NetworkStream stream = new NetworkStream();
            stream.WriteUVarInt((uint) datas.Count);
            foreach (JToken obj in datas)
            {
                string name = obj.Value<string>("name");
                int id = obj.Value<int>("id");
                int data = obj.Value<int>("data");

                int index = (id << 4) + data;
                _runtimeBlockDatas.TryAdd(_runtimeId, new RuntimeBlockData(name, _runtimeId, id, data, index));
                _runtimeToIndex.TryAdd(_runtimeId, index);
                _indexToRuntime.TryAdd(index, _runtimeId);

                stream.WriteString(name);
                stream.WriteShort((short) data, ByteOrder.Little);

                _runtimeId++;
            }

            _compiledTable = stream.GetBuffer();
        }

        public void RegisterBlock(int id, Type type)
        {
            if (!_blockFactory.ContainsKey(id))
            {
                _blockFactory.TryAdd(id, type);
            }
        }

        public void Compile(int id)
        {
            if (_blockFactory.ContainsKey(id))
            {
                ConstructorInfo ctor = _blockFactory[id].GetConstructor(
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder,
                    new Type[] {typeof(int)}, null);
                ParameterExpression p1 = Expression.Parameter(typeof(int));
                _blockCreateFunc.TryAdd(id, Expression.Lambda<Func<int, IBlock>>(Expression.New(ctor, p1), p1)
                    .Compile());
            }
        }

        public void CompileAll()
        {
            foreach (int id in _blockFactory.Keys)
            {
                Compile(id);
            }
        }

        public Option<IBlock> GetBlockFromId(int id, int data = 0)
        {
            return _blockCreateFunc[id].Invoke(data).SomeNotNull();
        }

        public Option<IBlock> GetBlockFromRuntime(int runtimeId)
        {
            int index = _runtimeToIndex[runtimeId];
            int id = index >> 4;
            int data = index & 0xf;

            return _blockCreateFunc[id].Invoke(data).SomeNotNull();
        }

        public IRuntimeBlockData GetRuntimeBlockData(int id, int data)
        {
            int index = (id << 4) + data;
            int runtime = _indexToRuntime[index];

            return _runtimeBlockDatas[runtime];
        }

        public IRuntimeBlockData GetRuntimeBlockData(int runtimeId)
        {
            return _runtimeBlockDatas[runtimeId];
        }

        public void RegisterRuntimeId(IRuntimeBlockData runtimeBlockData)
        {
            _runtimeBlockDatas.TryAdd(_runtimeId, runtimeBlockData);
            _runtimeToIndex.TryAdd(_runtimeId, runtimeBlockData.Index);
            _indexToRuntime.TryAdd(runtimeBlockData.Index, _runtimeId);

            _runtimeId++;
        }

        public byte[] GetRuntimeTable()
        {
            return _compiledTable;
        }

        public void CompileRuntimeTable()
        {
            NetworkStream stream = new NetworkStream();
            stream.WriteUVarInt((uint) _runtimeBlockDatas.Count);
            foreach (IRuntimeBlockData runtimeBlockData in _runtimeBlockDatas.Values)
            {
                stream.WriteString(runtimeBlockData.Name);
                stream.WriteShort((short) runtimeBlockData.Data, ByteOrder.Little);
            }

            _compiledTable = stream.GetBuffer();
        }
    }
}