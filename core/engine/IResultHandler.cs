namespace Python.src.python.engine
{
    public interface IResultHandler
    {
        void HandleResult(ResultObject resultObject);
        void PrintLn(string text);
    }
}
