using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;


class Program
{
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private static NativeWorkbenchForm _nativeWorkbenchForm;
    private static IntPtr _hookID = IntPtr.Zero;
    private static System.Windows.Forms.Timer _timer;
   
    // private static SimpleCompileForm _simpleCompileForm;

    private static void Main(string[] args)
    {
        var worker = new BackgroundWorker();
        worker.DoWork += (sender, e) =>
        {
            try
            {
                _nativeWorkbenchForm = new NativeWorkbenchForm();
                _timer = new System.Windows.Forms.Timer();
                _timer.Stop();
                _timer.Interval = 100;
                _timer.Tick += _timer_Tick;
                _timer.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            Application.EnableVisualStyles();
            Application.Run(_nativeWorkbenchForm);
        };
        worker.RunWorkerAsync();


        Thread.Sleep(-1);
    }

    static void _timer_Tick(object sender, EventArgs e)
    {
        _timer.Stop();
        if (_nativeWorkbenchForm != null)
            _nativeWorkbenchForm.OnTick();
        _timer.Start();
    }

}

