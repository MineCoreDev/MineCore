using System;
using MineCore.Utils;

namespace MineCore.Events
{
    public static class EventArgsManager
    {
        public static bool CancelableInvoke<T>(this EventHandler<T> hander, object sender, T args)
            where T : CancelableEventArgs
        {
            sender.ThrownOnArgNull(nameof(hander));
            args.ThrownOnArgNull(nameof(args));

            if (!args.IsCancel || !args.IsCancelable)
            {
                hander?.Invoke(sender, args);
                return true;
            }

            return false;
        }
    }
}