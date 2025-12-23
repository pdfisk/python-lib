using Python.src.python.engine;

namespace PythonLib
{
    public class PythonApi
    {

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

        public void SetGlobal(string name, object value)
        {
            var engine = Engine.GetInstance();
            engine.GetGlobalAccessor().SetGlobal(name, value);
        }

    }
}
