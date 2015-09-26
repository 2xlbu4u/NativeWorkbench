using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;



public class NativeManager
{
    private static Assembly _assembly = Assembly.GetExecutingAssembly();
    private static Dictionary<ulong, NativeDescriptor> _nativeDescriptorMap = new Dictionary<ulong, NativeDescriptor>();

    private static Dictionary<ItemGroup, List<NativeDescriptor>> _indexByGroup = new Dictionary<ItemGroup, List<NativeDescriptor>>(); 

    private static List<string> _filterList = new List<string>();

    public static NativeDescriptor _currentNativeDescriptor;
    private static Stopwatch _stopwatch = new Stopwatch();
    private const int MAX_PARAMS = 50;

    public static Dictionary<ulong, NativeDescriptor> Init()
    {
        buildNativeMap();
        indexByGroup();
        return _nativeDescriptorMap;
    }

    private static string getNativeResource()
    {
        var names = _assembly.GetManifestResourceNames();
        var resourceName = "NativeWorkbench.natives.h";
        string retVal;
        using (Stream stream = _assembly.GetManifestResourceStream(resourceName))
        using (StreamReader reader = new StreamReader(stream))
        {
            retVal = reader.ReadToEnd();
        }
        return retVal;
    }

    private static void buildNativeMap()
    {
        try
        {
            var fileLines = getNativesList();

            ItemGroup currentItemGroup = ItemGroup.Select;
            foreach (var _line in fileLines)
            {
                var line = _line.Trim();

                if (!line.StartsWith("static") && !line.StartsWith("namespace"))
                    continue;

                // var nativeDescriptor = new NativeDescriptor();
                NativeDescriptor nativeDescriptor = new NativeDescriptor();

                if (line.StartsWith("namespace"))
                {
                    var category = line.Split(" ".ToCharArray())[1];
                    currentItemGroup = (ItemGroup) Enum.Parse(typeof (ItemGroup), category);
                    nativeDescriptor.ItemGroup = currentItemGroup;
                }
                else if (line.StartsWith("static"))
                {
                    nativeDescriptor.ParamList = new List<NativeParam>();

                    // var nativeBoundItem = new NativeBoundItem();

                    var methodDeclare = line.Split("{".ToCharArray())[0].Trim();
                    var invokeDeclare = line.Split("{".ToCharArray())[1].Trim();
                    var methodParts = methodDeclare.Split(" ".ToCharArray());

                    var hashDeclare =
                        invokeDeclare.Split("(".ToCharArray())[1].Split(",".ToCharArray())[0].Split(")".ToCharArray())[0
                            ];

                    nativeDescriptor.NativeHash = UInt64.Parse(hashDeclare.Replace("0x", ""), NumberStyles.HexNumber);

                    nativeDescriptor.ReturnTypeName = methodParts[1];

                    var methodNameFirstParamDeclare = methodParts[2].Split("(".ToCharArray());
                    nativeDescriptor.NativeName = methodNameFirstParamDeclare[0];

                    if (methodNameFirstParamDeclare[1] != ")")
                    {
                        // Params
                        var methodParamList = methodDeclare.Split("(".ToCharArray());
                        var paramListString = methodParamList[1].TrimEnd(")".ToCharArray());
                        var typeAndNameParts = paramListString.Split(",".ToCharArray());
                        foreach (var _typeAndNamePair in typeAndNameParts)
                        {
                            var typeAndNamePair = _typeAndNamePair.Trim();
                            var typeAndName = typeAndNamePair.Split(" ".ToCharArray());
                            if (typeAndName.Length == 1)
                                typeAndName = typeAndNameParts;

                            var paramTypeName = typeAndName[0];
                            var paramName = typeAndName[1];

                            nativeDescriptor.ParamList.Add(new NativeParam(paramTypeName, paramName));
                        }
                    }

                    nativeDescriptor.ItemGroup = currentItemGroup;
                    _nativeDescriptorMap.Add(nativeDescriptor.NativeHash, nativeDescriptor);
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception("Init Failed ", e);
        }
    }

    private static List<string> getNativesList()
    {

        string result = getNativeResource();//File.ReadAllText("natives.h");

        if (string.IsNullOrEmpty(result))
        {
            MessageBox.Show("Could not read natives.h");
            return null;
        }
        result = result.Replace("\r", "").Replace("\t", "");
        List<string> retList = new List<string>(result.Split("\n".ToCharArray()));

        return retList;
    }


    public static void BuildFilterList()
    {
        _filterList.Clear();

        var lines = File.ReadAllLines("filter.txt");
        foreach (var line in lines)
        {
            if (line == "" || line.StartsWith("#") || _filterList.Contains(line))
                continue;

            _filterList.Add(line);
        }
    }

    public static void indexByGroup()
    {
        foreach (var dict in _nativeDescriptorMap)
        {
            if (!_indexByGroup.ContainsKey(dict.Value.ItemGroup))
            {
                _indexByGroup.Add(dict.Value.ItemGroup, new List<NativeDescriptor>());
            }
            _indexByGroup[dict.Value.ItemGroup].Add(dict.Value);
        }
    }

    public static List<NativeDescriptor> GetDescriptorsByItemGroup(ItemGroup itemGroup)
    {
        return _indexByGroup[itemGroup];
    }

    public enum OutputType
    {
        UnknownsOnly,
        AllGettable,
    }

    public enum OutputFormat
    {
        ToCS,
        ToIni
    }
    public static void DebugOutAllUnknownGets(OutputType outputType, OutputFormat outputFormat)
    {
        Init();
        int rowIndex = 0;
        string currentGroup = "";

        var linesToWrite = new List<string>();
        for (var i = 0; i < _nativeDescriptorMap.Values.Count; i++)
        {
            var nativeDescriptor = _nativeDescriptorMap.Values.ToList()[i];
            StringBuilder outStr = new StringBuilder();
            if (currentGroup != nativeDescriptor.ItemGroup.ToString() )
            {
                outStr.AppendLine("// " + nativeDescriptor.ItemGroup);
                currentGroup = nativeDescriptor.ItemGroup.ToString();
            }
            
            if (nativeDescriptor.ReturnTypeName != "void")
            {
                if (!nativeDescriptor.NativeName.StartsWith("_0x") && outputType == OutputType.UnknownsOnly)
                    continue;

                StringBuilder paramStrs = new StringBuilder();

                foreach (var param in nativeDescriptor.ParamList)
                {
                    if (paramStrs.Length > 0)
                        paramStrs.Append(", ");
                    var paramTypeName = param.ParamTypeName;
                    if ((paramTypeName == "Any" && nativeDescriptor.ItemGroup == ItemGroup.PLAYER) || paramTypeName == "Player")
                        paramTypeName = "player";
                    paramStrs.Append(paramTypeName);
                }

                var returnTypeStr = nativeDescriptor.ReturnTypeName;
                if (returnTypeStr == "Any")
                    returnTypeStr = "int";
                else if (returnTypeStr == "BOOL")
                    returnTypeStr = "bool";
                else if (returnTypeStr == "char*")
                    returnTypeStr = "string";


                if (outputFormat == OutputFormat.ToIni)
                {
                    var outCode = string.Format("return Function.Call<{0}>(Hash.{1}", returnTypeStr,
                        nativeDescriptor.NativeName);

                    outStr.Append(string.Format("{0}#[{1}] {2}#{3}", rowIndex, nativeDescriptor.ItemGroup,
                        nativeDescriptor.NativeName, outCode));
                    if (paramStrs.Length > 0)
                        outStr.Append(", " + paramStrs);
                    outStr.AppendLine(");");
                    outStr.AppendLine(string.Format("{0}#end", rowIndex));
                }
                else if (outputFormat == OutputFormat.ToCS)
                {

                    outStr.Append(string.Format("_evaluatorMap.Add(Hash.{1}, Function.Call<{0}>(Hash.{1}", returnTypeStr, nativeDescriptor.NativeName));
                    if (paramStrs.Length > 0)
                        outStr.Append(", " + paramStrs);
                    outStr.Append("));");
                }


                linesToWrite.Add(outStr.ToString());

                rowIndex++;
            }
        }
        File.WriteAllLines("scripts\\Getters.cs", linesToWrite);
    }

    public static NativeDescriptor GetCurrentNativeDescriptor()
    {
        return _currentNativeDescriptor;
    }
    public static NativeDescriptor InitNativeDescriptor(UInt64 hash)
    {
        if (hash == 0)
        {
            // For testing param marshalling
            _currentNativeDescriptor = NativeDescriptor.TestNativeDescriptor;
        }
        else
        {
            _currentNativeDescriptor = _nativeDescriptorMap.ContainsKey(hash) ? _nativeDescriptorMap[hash] : null;
            if (_currentNativeDescriptor == null)
            {
                Debug.WriteLine("Init Hash lookup not found: " + hash);
                return _currentNativeDescriptor;
            }
            _currentNativeDescriptor.Reset();
            _currentNativeDescriptor.NativeProcessState = NativeProcessState.ReadyInit;
            _currentNativeDescriptor.IsNativeFiltered = (_filterList.Contains(_currentNativeDescriptor.NativeName));
        }
        return _currentNativeDescriptor;
    }



    public static void OutAllNativeTypeDefs()
    {
        var nativeTypedefs = new List<string>();

        foreach (var nativeDescriptor in _nativeDescriptorMap.Values)
        {
            foreach (var nativeParam in nativeDescriptor.ParamList)
            {
                if (!nativeTypedefs.Contains(nativeParam.ParamTypeName))
                {
                    nativeTypedefs.Add(nativeParam.ParamTypeName);
                }
            }
        }

        foreach (var typeDefName in nativeTypedefs)
        {
            Debug.WriteLine(typeDefName);
        }
    }

    public static void DebugOutNativeMap()
    {

        Debug.Write("ItemGroup\tReturnType\tNativeHash\tNativeName");
        for (var i = 0; i < MAX_PARAMS; i++)
        {
            Debug.Write("\t");
            Debug.Write(String.Format("P{0}Type\t", i));
            Debug.Write(String.Format("P{0}Name", i));
        }
        Debug.WriteLine("");
        Debug.WriteLine("");

        foreach (var nativeDescriptor in _nativeDescriptorMap.Values)
        {
            var sb = new StringBuilder();
            var itemGroupName = nativeDescriptor.ItemGroup.ToString();
            sb.Append(itemGroupName[0] + itemGroupName.Substring(1).ToLower() + "\t");
            sb.Append(nativeDescriptor.ReturnTypeName + "\t");
            sb.Append("0x" + nativeDescriptor.NativeHash.ToString("X") + "\t");
            sb.Append(nativeDescriptor.NativeName + "\t");


            var paramPairTemplate = "{0} {1}";
            var paramTemplate = "({0})";
            StringBuilder sbParams = new StringBuilder();
            foreach (var nativeParam in nativeDescriptor.ParamList)
            {
                if (sbParams.Length > 0)
                    sbParams.Append(", ");
                sbParams.Append(string.Format(paramPairTemplate, nativeParam.ParamTypeName, nativeParam.ParamName));
            }
            sb.Append(string.Format(paramTemplate, sbParams));

            Debug.WriteLine(sb);
        }

    }
}
