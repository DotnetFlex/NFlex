using System;
using System.Linq;
using System.Reflection;

namespace NFlex
{
    public sealed class JScript
    {
        private Type _controlType;
        private object _control;

        public JScript()
        {
            _controlType = Type.GetTypeFromProgID("ScriptControl");
            if (_controlType == null) return;
            _control = Activator.CreateInstance(_controlType);
            _controlType.InvokeMember("Language", BindingFlags.SetProperty, null, _control, new object[] { "JavaScript" });
        }
        public JScript(string scriptCode):this()
        {
            AddCode(scriptCode);
        }

        public void AddCode(string code)
        {
            _controlType.InvokeMember("AddCode", BindingFlags.InvokeMethod, null, _control, new object[] { code });
        }

        public T Invoke<T>(string functionName, params object[] parameters)
        {
            var pList = string.Join(",", parameters.Select(t => (t is string) ? "\"" + t + "\"" : t));
            var code = string.Format("{0}({1})", functionName, pList);
            return Eval<T>(code);
        }

        public T Eval<T>(string code)
        {
            var result= _controlType.InvokeMember("Eval", BindingFlags.InvokeMethod, null, _control, new object[] { code });
            return result.To<T>();
        }
    }
}
