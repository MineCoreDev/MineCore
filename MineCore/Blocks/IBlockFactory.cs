using System;
using System.Collections.Concurrent;

namespace MineCore.Blocks
{
    public interface IBlockFactory
    {
        void RegisterBlock(int id, Type type, bool registerRuntime = false);

        void Compile(int id);
        void CompileAll();

        IBlock GetBlockFromId(int id);
        IBlock GetBlockFromRuntime(int runtimeId);
    }
}