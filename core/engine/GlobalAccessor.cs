using Microsoft.Scripting.Hosting;
using Newtonsoft.Json;

namespace Python.src.python.engine
{
    public class GlobalAccessor
    {
        private readonly ScriptEngine _engine;
        private readonly ScriptScope _scope;

        public GlobalAccessor(ScriptEngine engine, ScriptScope scope)
        {
            _engine = engine;
            _scope = scope;
        }

        public void ExecuteScript(string pythonCode)
        {
            _engine.Execute(pythonCode, _scope);
        }

        // -----------------------------
        // Get a Python global variable
        // -----------------------------
        public T GetGlobal<T>(string name)
        {
            if (!_scope.ContainsVariable(name))
                throw new KeyNotFoundException($"Global variable '{name}' not found.");

            var value = _scope.GetVariable(name);

            // Convert using JSON serialization if types differ
            string json = JsonConvert.SerializeObject(value);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public List<string> GetGlobalNames()
        {
            var names = _scope.GetVariableNames();
            return new List<string>(names);
         }

        // -----------------------------
        // Set/update a Python global variable
        // -----------------------------
        public void SetGlobal(string name, object value)
        {
            _scope.SetVariable(name, value);
        }

        // -----------------------------
        // Call a Python global function with JSON arguments
        // -----------------------------
        public T CallFunction<T>(string functionName, params object[] arg)
        {
            if (!_scope.ContainsVariable(functionName))
                throw new MissingMethodException($"Function '{functionName}' not found.");

            dynamic func = _scope.GetVariable(functionName);
            var result = func.__call__(arg);

            string json = JsonConvert.SerializeObject(result);
            return JsonConvert.DeserializeObject<T>(json);
        }

        // -----------------------------
        // Call a Python function with JSON input payload
        // -----------------------------
        public T CallFunctionFromJson<T>(string functionName, string jsonArgs)
        {
            var arg = JsonConvert.DeserializeObject<object[]>(jsonArgs);
            return CallFunction<T>(functionName, arg);
        }
    }
}
