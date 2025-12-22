using Python.src.python.engine;

namespace PythonLib
{
    public class PythonApi
    {
        public string GetStudioVersion()
        {
            return "Studio API Version 1.0.0";
        }

        public async Task<string> RunScript(string code)
        {
            var task = Task.Run(async () =>
            {
                var engine = Engine.GetInstance();
                await engine.RunScript(code);
                var result = engine.GetStdOut();
                return result;
            });
            task.Wait();
            var result = task.Result;
            return result;
        }

    }
}
