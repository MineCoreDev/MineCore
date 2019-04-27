using System;

namespace MineCore.Events
{
    public static class EventArgsManager
    {
        public static bool CancelableInvoke<T>(this EventHandler<T> hander, object sender, T args)
            where T : CancelableEventArgs
        {
            if (!args.IsCancel || !args.IsCancelable)
            {
                hander?.Invoke(sender, args);
                return true;
            }

            return false;
        }
    }
}