#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Math;
//using GTAWorld = GTA.World;
//using GTA.Native;
//using Menu = GTA.Menu;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using System.IO;
using System.Runtime.Remoting.Channels;
using GTA.Native;
using MessageBox = System.Windows.Forms.MessageBox;

#endregion

public class NativeWorkbench : Script
{
    private NativeWorkbenchForm _nativeWorkbenchForm = new NativeWorkbenchForm();

    public static DataGridView Properties;
    public static TextBox Output;
    public static Stopwatch Stopwatch = new Stopwatch();
    public static bool[] Bool = new bool[10];
    public static int[] Int = new int[10];
    public static string[] Str = new string[10];
    public static Model[] Model = new Model[10];
    public static Entity[] Entity = new Entity[10];
    public static Ped[] Ped = new Ped[10];
    public static Vehicle[] Vehicle = new Vehicle[10];
    public static TimeSpan[] TimeSpan = new TimeSpan[10];
    public static Camera[] Camera = new Camera[10];
    public static float[] Float = new float[10];
    public static long[] Long = new long[10];
    public static List<int> IntList = new List<int>();
    public static float FlySpeed = 2f;
    public static Prop[] Prop = new Prop[10];
    public static Vector3[] XYZ = new Vector3[10];
    public static Func<object, object>[] Func = new Func<object, object>[10];

    public NativeWorkbench()
    {
        try
        {
          //  KeyDown += OnKeyDown;
            Tick += OnTick;
        }
        catch (Exception e)
        {
            SaveLoad.Log(e.ToString());
           // Debug.WriteLine(e);
        }


        BackgroundWorker myWorker = new BackgroundWorker();
        myWorker.DoWork += (sender, e) =>
        {
            try
            {
                Application.EnableVisualStyles();
                Application.Run(_nativeWorkbenchForm);
            }
            catch (Exception ex)
            {
                SaveLoad.Log(ex.ToString());
            }
        };
        myWorker.RunWorkerAsync();
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            _nativeWorkbenchForm.OnTick();
            processOnTick();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    public void processOnTick()
    {
        
    }
    public void NullTheMap()
    {
        var pos = Game.Player.Character.Position;
        Function.Call(Hash.SET_GARBAGE_TRUCKS, 0);
        Function.Call(Hash.CLEAR_AREA, pos.X, pos.Y, pos.Z, 9000f, 0, 0, 0, 0, 0);
        Function.Call(Hash.DELETE_ALL_TRAINS);

        var IPLs = File.ReadAllLines("MapList.txt");
        foreach (var ipl in IPLs)
        {
            Function.Call(Hash.REMOVE_IPL, ipl);
        }

    }

}



