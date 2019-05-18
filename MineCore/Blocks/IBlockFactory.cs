using System;
using System.Collections.Concurrent;
using BinaryIO;
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

        BinaryStream GetRuntimeTable();
    }
}