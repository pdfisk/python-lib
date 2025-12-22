using Python.src.python.engine;

namespace Python.src.python
{
    internal class ResultHandler : IResultHandler
    {
         StringWriter stringWriter = new StringWriter();

        public string GetText()
        {
            return stringWriter.ToString();
        }

        public void HandleResult(ResultObject resultObject)
        {
            if (resultObject._stdout != null)
                stringWriter.Write(resultObject._stdout);
            if (resultObject._stderr != null)
                stringWriter.Write(resultObject._stderr);
        }

        public void PrintLn(string text)
        {
            stringWriter.WriteLine(text);
        }
    }
}
