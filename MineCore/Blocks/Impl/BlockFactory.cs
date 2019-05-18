using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using BinaryIO;
using Optional;

namespace MineCore.Blocks.Impl
{
    public class BlockFactory : IBlockFactory
    {
        private ConcurrentDictionary<int, Type> _blockFactory = new ConcurrentDictionary<int, Type>();

        private ConcurrentDictionary<int, Func<int, IBlock>>
            _blockCreateFunc = new ConcurrentDictionary<int, Func<int, IBlock>>();

        private ConcurrentDictionary<int, int> _runtimeToIndex = new ConcurrentDictionary<int, int>();
        private ConcurrentDictionary<int, int> _indexToRuntime = new ConcurrentDictionary<int, int>();

        public BlockFactory()
        {
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
            int index = _indexToRuntime[runtimeId];
            int id = index >> 4;
            int data = index & 0xf;

            return _blockCreateFunc[id].Invoke(data).SomeNotNull();
        }

        public BinaryStream GetRuntimeTable()
        {
            throw new NotImplementedException();
        }
    }
}