
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;


public partial class NativeWorkbenchForm : Form
{
    private PropertiesControl _propertiesControl;
    public string ImmediateCodeToSubmit;
    private object immediateLockObj = new object();
    public string OnTickCodeToSubmit;
    private object onTickLockObj = new object();
    private bool _mapRemoved;
    private bool _mapCleared;
    private int _propRefreshTimer;
    private int PROP_TICK_COUNT_RESET = 0;
 
    private object _compiledHandle;

    public NativeWorkbenchForm()
    {
        try 
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {
            SaveLoad.Log(ex.ToString());
        }
    }
    
    private void ModWorkbenchForm_Load(object sender, EventArgs e)
    {
        TopMost = true;


        _propertiesGrid.ClearSelection();

        NativeWorkbench.Properties = _propertiesGrid;
        NativeWorkbench.Output = _outputTxt;

        _propertiesControl = new PropertiesControl(this, _propertiesGrid, _nativeGroupDDL, _nativeNameDDL,
             _addToPropBtn, _addOnTickBtn, _addImmediateBtn, _onTickTxt, _immediateSourceTxt, _outputTxt, _enablePropUpdate,
             _enableBoolColors);

        _propertiesGrid.Click += _propertiesGrid_Click;

        _onTickTxt.LostFocus += _onLostFocus;
        _immediateSourceTxt.LostFocus += _onLostFocus;
        _propertiesGrid.LostFocus += _onLostFocus;

        for (var i = 0; i < _propertiesGrid.Columns.Count; i++)
        {
            _propertiesGrid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        onLoadWork();
       
    }


    void _onLostFocus(object sender, EventArgs e)
    {
        doSaveWork();
    }

    void _propertiesGrid_Click(object sender, EventArgs e)
    {
        _enablePropUpdate.Checked = false;
    }

    public string DequeueImmediateSourceCode()
    {
        lock (immediateLockObj)
        {
            var retVal = ImmediateCodeToSubmit;
            ImmediateCodeToSubmit = null;
            return retVal;
        }
    }

    public string DequeueOnTickSourceCode()
    {
        lock (onTickLockObj)
        {
            var retVal = OnTickCodeToSubmit;
            OnTickCodeToSubmit = null;
            return retVal;
        }
    }

    public void OnTick()
    {
        if (InvokeRequired)
        {
            Invoke((Action)OnTick);
        }
        else
        {
            // Make the form always show
     
            doOnTickCompile();
            doOnTickRun();
            doImmediateCompileRun();

            //OnSaveTick();

            if (_propertiesControl != null && _enablePropUpdate.Checked)
            {
                OnPropTick();
            }

            flyModeOnTick();

        }
    }

    public void OnPropTick()
    {
        if (_propRefreshTimer == 0)
        {
            TopMost = true;
            _propertiesControl.OnYieldTick();
        }

        _propRefreshTimer = (_propRefreshTimer == 0) ? PROP_TICK_COUNT_RESET : (_propRefreshTimer - 1);

    }

    // Enable on tick clicked
    private void ontickUpdateBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (!ontickUpdateBtn.Checked)
            _compiledHandle = null;

        if (ontickUpdateBtn.Checked && _onTickTxt.Text != "")
        {
            lock (onTickLockObj)
            {
                OnTickCodeToSubmit = _onTickTxt.Text;
            }
        }

    }
    #region Compile and Run

    // Run button click
    private void OnCompileRunClick(object sender, EventArgs e)
    {
        if (_immediateSourceTxt.Text != "")
        {
            var textToSubmit = _immediateSourceTxt.SelectedText != ""
                ? _immediateSourceTxt.SelectedText 
                : _immediateSourceTxt.Text;

            lock (immediateLockObj)
            {
                ImmediateCodeToSubmit = textToSubmit;
            }
        }
    }
    private void doOnTickCompile()
    {
        string sourceCode;
        if ((sourceCode = DequeueOnTickSourceCode()) == null)
            return;

        string errorText;
        _compiledHandle = CSCompile.DoCompile(sourceCode, out errorText);

        if (errorText != null)
        {
            setCompileRunResult(errorText);
            _compiledHandle = null;
        }
    }

    private void doOnTickRun()
    {
        if (ontickUpdateBtn.Checked)
        {
            if (_compiledHandle != null)
            {
                setCompileRunResult(CSCompile.DoRun(_compiledHandle));
            }
        }

        if (_clerAreacb.Checked)
        {
            Function.Call(Hash.SET_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_PED_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_PARKED_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            var pos = Game.Player.Character.Position;
            Function.Call(Hash.SET_GARBAGE_TRUCKS, 0);
            Function.Call(Hash.CLEAR_AREA, pos.X, pos.Y, pos.Z, 9000f, 0, 0, 0, 0, 0);
            Function.Call(Hash.DELETE_ALL_TRAINS);
            Game.Player.WantedLevel = 0;

            Game.Player.Character.FreezePosition = true;
            Game.Player.Character.IsVisible = false;
            Function.Call(Hash.DESTROY_MOBILE_PHONE);

            _mapCleared = true;
        }
        
        if (!_clerAreacb.Checked && _mapCleared)
        {
            _mapCleared = false;
            Game.Player.Character.FreezePosition = false;
            Game.Player.Character.IsVisible = true;
        }

        if (_useEmptyMapCb.Checked)
        {

            if (!_mapRemoved)
            {
                var IPLs = File.ReadAllLines("MapList.txt");
                foreach (var ipl in IPLs)
                    Function.Call(Hash.REMOVE_IPL, ipl);
                _mapRemoved = true;
            }
        }
        else
        {
            if (_mapRemoved)
            {
                var IPLs = File.ReadAllLines("MapList.txt");
                foreach (var ipl in IPLs)
                    Function.Call(Hash.REQUEST_IPL, ipl);
                _mapRemoved = false;
            }
        }
    }

    private void doImmediateCompileRun()
    {
        try
        {
            string sourceCode;
            if ((sourceCode = DequeueImmediateSourceCode()) == null)
                return;

            string outVal = CSCompile.DoImmediateCompileRun(sourceCode);
            setCompileRunResult(outVal);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    private void setCompileRunResult(string result)
    {
        if (result != null)
            _outputTxt.Text = result;
    }
    #endregion

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        const int WM_KEYDOWN = 0x100;
        var keyCode = (Keys)(msg.WParam.ToInt32() & Convert.ToInt32(Keys.KeyCode));
        if (msg.Msg == WM_KEYDOWN)
        {
                
            if (ModifierKeys == Keys.Control)
            {
                if ( keyCode == Keys.A)
                {
                    if (_onTickTxt.Focused) _onTickTxt.SelectAll();
                    else if (_immediateSourceTxt.Focused) _immediateSourceTxt.SelectAll();
                    else if (_propertiesGrid.Focused)_propertiesGrid.SelectAll();
                    else if (_outputTxt.Focused) _outputTxt.SelectAll();
                    return true;
                }
            }
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void onLoadWork()
    {
        var saveLoadData = SaveLoad.Load();

        if (saveLoadData.Error != null)
        {
            _outputTxt.Text = saveLoadData.Error;
            return;
        }
        _onTickTxt.Text = saveLoadData.OnTickCode.ToString();
        _immediateSourceTxt.Text = saveLoadData.ImmediateCode.ToString();
        _propertiesControl.OnLoadWork(saveLoadData.PropData);
               
    }

    private void doSaveWork()
    {
        try
        {
            SaveLoad.Save(_onTickTxt.Text, _immediateSourceTxt.Text, _propertiesControl.GetPropsForSave());
        }
        catch (Exception ex)
        {
            _outputTxt.Text = "Cound not save NativeWorkbench.ini file\n" + ex;
        }
    }

    private void flyModeOnTick()
    {
        try
        {
            var ped = Game.Player.Character;
            if (!_flyMode.Checked)
            {
                ped.FreezePosition = false;
                ped.IsVisible = true;
                return;
            }

            ped.FreezePosition = true;
            ped.IsVisible = false;
            int characterHandle = Game.Player.Character.Handle;
            Vector3 camRotate = Function.Call<Vector3>(Hash.GET_GAMEPLAY_CAM_ROT);
            Function.Call(Hash.SET_ENTITY_ROTATION, characterHandle, camRotate.X, camRotate.Y, camRotate.Z);


            // determine the new position now that the player matches the camera rotation
            if (NativeWorkbench.FlySpeed == 0f)
                NativeWorkbench.FlySpeed = 2;
            float unit = NativeWorkbench.FlySpeed;

            if (Game.IsKeyPressed(Keys.W))
            {
                var newCoord = ped.GetOffsetInWorldCoords(new Vector3(0f, unit, 0f));
                Function.Call(Hash.SET_ENTITY_COORDS, characterHandle, newCoord.X, newCoord.Y, newCoord.Z, 0, 1, 0);
            }
        }
        catch (Exception ex)
        {
            _outputTxt.Text = ex.ToString();
        }
    }

}
