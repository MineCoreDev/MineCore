namespace MineCore.Events
{
    public class CancelableEventArgs : ICancelable
    {
        public bool IsCancelable { get; protected set; } = true;
        public bool IsCancel { get; set; } = false;
    }
}