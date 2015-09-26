using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Text;
using GTA;
using GTA.Native;


public class PropertyBoundItem
{
    public string CodeBehind;
    public object CompileHandle;
    public string RunResults;
    public string Label;

    public PropertyBoundItem(string label, string codeBehind)
    {
        CodeBehind = codeBehind;
        Label = label;
    }
}

public class ImmediateBoundItem
{
    public ImmediateProperty ImmediateProperty;
    public bool CheckBoxState;
    public Entity LastFocusedEntity;
    public Hash LastFocusedHash;
    public UInt64 NativeHashID;
    public string CodeBehind;

    public string Name { get; set; }
    public string Value { get; set; }


    public ImmediateBoundItem(NativeDescriptor nativeDescriptor)
    {
        if (nativeDescriptor == null)
            return;
        Name = nativeDescriptor.NativeName;
    }

    public object GetCheckboxState()
    {
        return CheckBoxState;
    }

    public void SetCheckBoxState(object state)
    {
        CheckBoxState = (bool)state;
    }
}


public class NativeDescriptor
{
    public static NativeDescriptor TestNativeDescriptor = new NativeDescriptor()
    {
            IsNativeFiltered = true,
            NativeName = "TEST",
            ParamList = new List<NativeParam>(new []
            {
                new NativeParam("float", "pfloat"),
                new NativeParam("int", "pInt"),
                new NativeParam("char*", "pCharP"),
                new NativeParam("BOOL", "pBool"), 
            })
    };

    //public int NativeID { get; set; }
    public NativeProcessState NativeProcessState { get; set; }
    public ItemGroup ItemGroup { get; set; }
    public string ReturnTypeName { get; set; }
    public object ReturnValue { get; set; }
    public string NativeName { get; set; }

    public string NativeNameReturn
    {
        get { return NativeName + " [Returns: " + ReturnTypeName +"]"; }
    
    }
    public UInt64 NativeHash { get; set; }
    public List<NativeParam> ParamList { get; set; }
    public bool IsNativeFiltered { get; set; }

    private int _paramIndex;

    public NativeDescriptor()
    {
        NativeProcessState = NativeProcessState.ReadyInit;
    }


    public NativeParam GetNextParam()
    {
        // These are reused so make sure vals are cleared
        var retParam = ParamList[_paramIndex];
        retParam.ParamValue = null;
        return ParamList[_paramIndex];
    }

    public void SetParamValue(NativeParam param, object paramVal)
    {
        try
        {
            var displayVal = paramVal;
            switch (param.ParamTypeName)
            {
                case "Hash":
                {
                    displayVal = String.Format("0x{0:X}", paramVal);
                    break;
                }
                case "char*":
                {
                    displayVal = "\"" + paramVal + "\"";
                    break;
                }
            }
            param.ParamValue = displayVal.ToString();
            _paramIndex++;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    public void SetReturnValue(object returnVal)
    {
        try
        {
            var displayVal = returnVal;

            ReturnValue = displayVal.ToString();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    public void Reset()
    {
        foreach (var param in ParamList)
        {
            param.ParamValue = null;
        }
        _paramIndex = 0;
        ReturnValue = 0;
    }

    public string ToLongName()
    {
        StringBuilder sbParams = new StringBuilder();
        foreach (var nativeParam in ParamList)
        {
            if (nativeParam == null)
                continue;

            if (sbParams.Length > 0)
                sbParams.Append(", ");

            sbParams.Append(String.Format("{0}", nativeParam.ParamTypeName));
        }
        var retval = String.Format("{0} {1} ({2})", ReturnTypeName, NativeName, sbParams);

        return retval;        
    }

    public string ToCodeFormat()
    {
        StringBuilder sbParams = new StringBuilder();
        foreach (var nativeParam in ParamList)
        {
            if (nativeParam == null)
                continue;

            if (sbParams.Length > 0)
                sbParams.Append(", ");

            sbParams.Append(String.Format("{0}", nativeParam.ParamTypeName));
        }

        var retString = String.Format("Function.Call(Hash.{0}", NativeName);
        if (sbParams.Length != 0)
            retString = retString + ", " + sbParams + ");\n";
        else
            retString = retString + ");\n";

        // If it returns a value    
        if (ReturnTypeName != "void")
        {
            if (ReturnTypeName == "char*")
                ReturnTypeName = "string";
            else if (ReturnTypeName == "Any")
                ReturnTypeName = "int";
            else if (ReturnTypeName.ToLower() == "hash")
                ReturnTypeName = "int";
            else if (ReturnTypeName == "BOOL")
                ReturnTypeName = "bool";
            retString = retString.Replace(".Call(", string.Format(".Call<{0}>(", ReturnTypeName));
        }
        if (ReturnTypeName != "void")
            retString =  "return " + retString;;
        return retString;

    }
}
public enum ItemGroup
{
    Select, UI, GRAPHICS, PLAYER, VEHICLE, ENTITY, PED, OBJECT, AI, GAMEPLAY, AUDIO, CUTSCENE, INTERIOR, CAM, WEAPON, ITEMSET, STREAMING,
    SCRIPT, STATS, BRAIN, MOBILE, APP, TIME, PATHFIND, CONTROLS, DATAFILE, FIRE, DECISIONEVENT, ZONE,
    ROPE, WATER, WORLDPROBE, NETWORK, NETWORKCASH, DLC1, DLC2, SYSTEM, DECORATOR, SOCIALCLUB, UNK, UNK1, UNK2, UNK3
}


public enum GetSetType
{
    NoGetSetType,
    GetOnly,
    SetOnly,
    Both
}

public enum ImmediateProperty
{
    NoProperty, PlayerHandle, CurrentFPS, Paused, PlayerColor, GameTimer, LastFrameTime, MissionFlag, NightVision, RadarZoom, RadioStation,
    ScreenResolution, TimeScale, PlaySound, PlayMusic, SoundID, Armor, Name,
    Accuracy, AlwaysDiesOnLowHealth, AlwaysKeepTaskArmor, CanBeDraggedOutOfVehicleCanBeKnockedOffBike, CanBeTargetted,
    CanPlayGestures, CanRagdoll, CanSufferCriticalHits, CanSwitchWeapons, CurrentVehicle, DiesInstantlyInWater,
    OffsetInWorldCoords, AlwaysKeepTask, DrivingSpeed, DrivingStyle, IsSittingInVehicle, IsInCover, Velocity, DrownsInSinkingVehicle, FiringPattern, Gender,
    IsAimingFromCover, IsBeingJacked, IsBeingStealthKilled, LastTargetName, IsBeingStunned, IsDoingDriveBy, IsDucking, IsGettingUp, IsHuman, IsIdle, IsInBoat,
    IsInCombat, IsInFlyingVehicle, IsInHeli, IsInjured, EntityName, IsInMeleeCombat, IsInPlane, IsInPoliceVehicle, IsInSub, IsInTrain, IsJacking, IsOnBike,
    IsOnFoot, IsPlayer, IsProne, IsRagdoll, IsReloading, IsRunning, IsShooting, IsSprinting, IsStopped, IsSwimming, IsSwimmingUnderWater,
    IsTryingToEnterALockedVehicle, IsWalking, Invincible, Checkbox, IsGettingIntoAVehicle, IsClimbing, Money, TargetedEntity, Vector3, Vector3X, Vector3Y, Vector3Z,
}
//public delegate TResult MyFunc<out TResult>();

public class NativeParam
{
    public string ParamTypeName { get; set; }
    public string ParamName { get; set; }
    public object ParamValue { get; set; }

    public NativeParam(string type, string name, object value = null)
    {
        ParamTypeName = type;
        ParamName = name;
        ParamValue = value;
    }
}
public class NativeBoundItem
{
    public int NativeCallCount { get; set; }
    public string DisplayedReturns { get; set; }
    public string DisplayedNativeMethod { get; set; }

}
public enum NativeProcessState
{
    ReadyInit,
    ReadyParam,
    ReadyCall,
    ReceivedCall,
    HasBeenFiltered,

}

public enum Vector3NextState
{
    None,
    X,
    Y,
    Z
}
