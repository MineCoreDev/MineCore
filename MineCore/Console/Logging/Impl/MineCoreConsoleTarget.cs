using System;
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
            int top = System.Console.CursorTop;
            int left = System.Console.CursorLeft;
            int inputTop = Console.InputStartTop;
            int width = System.Console.BufferWidth;
            string[] lines = msg.Split(Environment.NewLine);

            if (inputTop != 0)
            {
                int lineOverflow = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    lineOverflow += lines[i].Length / width;
                }

                System.Console.MoveBufferArea(0, inputTop, width, top - inputTop + 1, 0,
                    inputTop + lines.Length + lineOverflow);
                System.Console.SetCursorPosition(0, inputTop);
                System.Console.WriteLine(msg);
                System.Console.SetCursorPosition(left, inputTop + top - inputTop + lines.Length + lineOverflow);

                Console.InputStartTop = inputTop + lines.Length + lineOverflow;
            }
            else
            {
                System.Console.WriteLine(msg);
            }
        }
    }
}