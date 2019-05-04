using MineCore.Console.Impl;
using NLog;
using NLog.Layouts;
using NLog.Targets;

namespace MineCore.Console.Logging.Impl
{
    [Layout("MineCoreConsole")]
    public class MineCoreConsoleTarget : TargetWithLayout
    {
        internal MineCoreConsole Console { private get; set; }

        protected override void Write(LogEventInfo logEvent)
        {
            string msg = Layout.Render(logEvent);
            int left = System.Console.CursorLeft;
            int topline = System.Console.CursorTop;
            int startTop = Console?.InputStartTop ?? 0;
            if (startTop != 0)
            {
                int diff = (topline - startTop) + 1;
                System.Console.MoveBufferArea(0, startTop, System.Console.BufferWidth,
                    diff, 0,
                    topline + 1 + msg.Length / System.Console.BufferWidth);
                System.Console.SetCursorPosition(0, topline);
                System.Console.WriteLine(msg);
                if (left == System.Console.BufferWidth - 1)
                {
                    System.Console.SetCursorPosition(0, System.Console.CursorTop + diff);
                    System.Console.SetCursorPosition(0, System.Console.CursorTop + diff);
                }
                else
                {
                    System.Console.SetCursorPosition(left, System.Console.CursorTop + diff - 1);
                    System.Console.SetCursorPosition(left, System.Console.CursorTop + diff - 1);
                }

                Console.InputStartTop++;
            }
            else
            {
                System.Console.WriteLine(msg);
            }
        }
    }
}