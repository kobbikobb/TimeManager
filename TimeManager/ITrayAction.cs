namespace TimeManager
{
    public interface ITrayAction
    {
        string Name { get; }
        void Execute();
    }
}