using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Scripting.Hosting;
using IronPython.Hosting;

namespace IronPythonDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //无参调用文本 ScriptEngine 和 ScriptScope 调用Python的两个基础库
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            var strExpression = "1+2";
            var sourceCode = engine.CreateScriptSourceFromString(strExpression);
            //可以指定返回的类型
            var actual = sourceCode.Execute<int>();
            Console.WriteLine(actual);

            //传参调用 execute 可设置返回类型 
            // \" 转义字符 双引号 使用转义字符最外层需要有一组引号
            //可以将变量隐藏在字符串中
            var strExpression1 = "\"Hello:\"+str";
            var sourceCode1 = engine.CreateScriptSourceFromString(strExpression1);
            scope.SetVariable("str", "world");
            var actual1 = sourceCode1.Execute<string>(scope);
            Console.WriteLine(actual1);

            //调用函数
            var strExpression2 = "CMethod('Python')";
            var souceCode2 = engine.CreateScriptSourceFromString(strExpression2);
            scope.SetVariable("CMethod", (Func<string, string>)TMethod);
            var actua2 = souceCode2.Execute<string>(scope);
            scope.RemoveVariable("CMethod");
            Console.WriteLine(actua2);
            //Console.ReadKey();
        }

        public static string TMethod(string info)
        {
            return "Hello:" + info;
        }
    }
}
