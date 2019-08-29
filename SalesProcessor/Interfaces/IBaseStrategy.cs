namespace SalesProcessor.Interfaces
{
    public interface IBaseStrategy
    {
        object Execute(string[] fileLine);
    }
}
