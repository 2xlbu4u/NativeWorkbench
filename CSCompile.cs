using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using GTA;


public class CSCompile
{
    private static CSharpCodeProvider _cSharpCodeProvider = new CSharpCodeProvider();
    private static CompilerParameters _compilerParameter = new CompilerParameters();

    static CSCompile()
    {
        foreach (var aTypeName in new[] { typeof(Debug), typeof(NativeWorkbenchForm), 
            typeof(Script), typeof(Form), typeof(Point), typeof(List<>), typeof(Enumerable)})
        {
            var extraAssembly = aTypeName.Assembly.Location;
            _compilerParameter.ReferencedAssemblies.Add(extraAssembly);
        }
        _compilerParameter.GenerateExecutable = false;
        _compilerParameter.GenerateInMemory = true;
    }

    public static CompilerResults DoCompile(string sourceCode, out string errorText)
    {
        errorText = null;

        var codeToRun = SourceTemplate.Replace("{0}", sourceCode);

        CompilerResults compilerResult = _cSharpCodeProvider.CompileAssemblyFromSource(_compilerParameter, codeToRun);
        if (compilerResult.Errors.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            foreach (CompilerError CompErr in compilerResult.Errors)
            {
                sb.Append("Line number " + CompErr.Line +", Error Number: " + CompErr.ErrorNumber +", '" + CompErr.ErrorText + ";" +
                    Environment.NewLine + Environment.NewLine);
            }
            errorText = sb.ToString();
        }
        return compilerResult;
    }

    public static string DoRun(object compilehandle)
    {
        var compilerResults = (CompilerResults) compilehandle;
        var obj = compilerResults.CompiledAssembly.CreateInstance("InlineCompile");
        Type type = obj.GetType();
        var retVal = type.GetMethod("EvalCode").Invoke(obj, null);
        return Convert.ToString(retVal);
    }

    public static string DoImmediateCompileRun(string sourceCode)
    {
        try
        {
            if (sourceCode == null)
                return null;

            string errorText;
            var compilerResults = DoCompile(sourceCode, out errorText);
            if (errorText != null)
                return errorText;

            var retString = DoRun(compilerResults);
            return retString;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        return null;
    }

    public static string SourceTemplate = @"
using System;
using System.Diagnostics;
using System.IO;
using GTA;
using GTA.Native;
using GTA.Math;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

public class InlineCompile

{   public Player Player = Game.Player;
    public Player player = Game.Player;
    public Ped ped = Game.Player.Character;
    public Ped Ped = Game.Player.Character;

    public object EvalCode()
    {
        try
        {
            {0};
        }
        catch (Exception ex)
        {   
            return (ex);
        }
        return null;
    }
}";

}

