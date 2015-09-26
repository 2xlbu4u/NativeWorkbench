using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


public class SaveLoadData
{
    public List<string> PropData = new List<string>();
    public StringBuilder OnTickCode = new StringBuilder();
    public StringBuilder ImmediateCode = new StringBuilder();
    public StringBuilder CompileTemplate = new StringBuilder();
    public string Error;
}
public class SaveLoad
{
    private static string SAVE_LOAD_FILE = @"scripts\NativeWorkbench.ini";
    public static void Log(string textOut)
    {
        File.AppendAllText(@"scripts\NativeWorkbench.log", textOut);
    }
    public static SaveLoadData Load()
    {

        string[] saveFileLines;
        var retVal  = new SaveLoadData();
        try
        {
            saveFileLines = File.ReadAllLines(SAVE_LOAD_FILE);
        }
        catch(Exception ex)
        {
            retVal.Error = "Could not load " + SAVE_LOAD_FILE + " Did you copy it to the scripts folder?";
            SaveLoad.Log(ex.ToString());
            return retVal;
        }

        StringBuilder currentsb = null;
        for (var i = 0; i < saveFileLines.Length; i++)
        {
            var nextLine = saveFileLines[i];
            if (nextLine == "")
                continue;
            switch (nextLine)
            {
                case "[PropData]":
                {
                    currentsb = null;
                    break;
                }
                case "[OnTickCode]":
                {
                    currentsb = retVal.OnTickCode;
                    break;
                }
                case "[ImmediateCode]":
                {
                    currentsb = retVal.ImmediateCode;
                    break;
                }
                //case "[CompileTemplate]":
                //{
                //    currentsb = retVal.CompileTemplate;
                //    break;
                //}
                default:
                {
                    if (currentsb != null)
                        currentsb.AppendLine(nextLine);
                    else 
                        retVal.PropData.Add(nextLine);
                    break;
                }
            }
        }
        return retVal;
    }

    public static void Save(string onTickText, string immediateSourceText, List<string> dataList)
    {
        StringBuilder sb = new StringBuilder();
        if (dataList.Count > 0)
        {
            dataList.Insert(0, "[PropData]");
            foreach (var line in dataList)
            {
                sb.AppendLine(line);
            }
        }

        if (onTickText != "")
        {
            sb.AppendLine("\n[OnTickCode]");
            sb.Append(onTickText);
        }
        if (immediateSourceText != "")
        {
            sb.AppendLine("\n[ImmediateCode]");
            sb.Append(immediateSourceText);
        }
        
        File.WriteAllText(SAVE_LOAD_FILE, sb.ToString());

    }
}

