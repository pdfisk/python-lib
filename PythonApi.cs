using Python.src.python.engine;
using PythonLib.constants;
using UtilLib;

namespace PythonLib
{
    public class PythonApi
    {
        public static PythonApi Instance { get; } = new PythonApi();

        public ICollection<string> GetSearchPaths()
        {
            return Engine.GetInstance().GetSearchPaths();
        }

        public string LoadFile(string fileName){ 
            var code = FileUtil.GetScript(fileName);
            RunScript(code).Wait();
            return $"Loaded file: {fileName}";
        }

        public void LoadStarterScripts()
        {
            foreach (var script in ScriptConstants.STARTUP_SCRIPTS)
            {
                LoadFile(script);
            }
        }

        public void SetSearchPaths(ICollection<string> paths)
        {
            Engine.GetInstance().SetSearchPaths(paths);
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

        public void SetGlobal(string name, object value)
        {
            var engine = Engine.GetInstance();
            engine.GetGlobalAccessor().SetGlobal(name, value);
        }

    }
}
