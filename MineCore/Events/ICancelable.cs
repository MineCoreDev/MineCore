namespace MineCore.Events
{
    public interface ICancelable
    {
        bool IsCancel { get; set; }
    }
}