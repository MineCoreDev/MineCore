using System;
using System.Collections.Concurrent;
using BinaryIO;
using MineCore.Data;
using Optional;

namespace MineCore.Blocks
{
    public interface IBlockFactory
    {
        void RegisterBlock(int id, Type type);

        void Compile(int id);
        void CompileAll();

        Option<IBlock> GetBlockFromId(int id, int data);
        Option<IBlock> GetBlockFromRuntime(int runtimeId);

        IRuntimeBlockData GetRuntimeBlockData(int id, int data);
        IRuntimeBlockData GetRuntimeBlockData(int runtimeId);

        void RegisterRuntimeId(IRuntimeBlockData runtimeBlockData);

        byte[] GetRuntimeTable();
        void CompileRuntimeTable();
    }
}