namespace Python.src.python.engine
{
    public class ResultObject
    {
        public object _result;
        public string _stdout;
        public string _stderr;

        public ResultObject(object result, string stdout, string stderr)
        {
            _result = result;
            _stdout = stdout;
            _stderr = stderr;
        }
    }
}
