using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GTA;

public class PropertiesControl
{
    private DataGridView _propertyControlGrid;

    private int MAX_PROP_ROWS = 25;

    private ComboBox _nativeGroupDDL;
    private ComboBox _nativeNameDDL;
    private Button _addToPropBtn;
    private Button _addOnTickBtn;
    private Button _addImmediateButton;

    private TextBox _ontickText;
    private TextBox _immediateText;
    private TextBox _outputText;
    private CheckBox _enablePropUpdate;
    private CheckBox _enableBoolColors;

    private Dictionary<int, PropertyBoundItem> _propMapData = new Dictionary<int, PropertyBoundItem>();
    private NativeWorkbenchForm _parent;

    public PropertiesControl(NativeWorkbenchForm parent,
    DataGridView propertyControlGrid, ComboBox nativeGroupDDL, ComboBox nativeNameDDL, 
        Button addToPropBtn, Button addOnTickBtn, Button addImmediateButton, TextBox ontickText, TextBox immediateText, TextBox ouputText,
    CheckBox enablePropUpdate, CheckBox enableBoolColors)
    {
        _parent = parent;
        _propertyControlGrid = propertyControlGrid;
        _propertyControlGrid.Click += _propertyControlGrid_Click;
        _propertyControlGrid.LostFocus += _propertyControlGrid_LostFocus;
        
        NativeManager.Init();

        _nativeNameDDL = nativeNameDDL;
        _nativeGroupDDL = nativeGroupDDL;
        ItemGroup[] values = (ItemGroup[])Enum.GetValues(typeof(ItemGroup));
        var nativeValues = values.OrderBy(v => v.ToString()).ToList();
        nativeValues.Insert(0, ItemGroup.Select);
        _nativeGroupDDL.DataSource = nativeValues;
        _nativeNameDDL.DisplayMember = "NativeNameReturn";
        _nativeNameDDL.ValueMember = "NativeHash";
        _nativeNameDDL.SelectedValueChanged += _nativeNameDDL_SelectedValueChanged;
        _nativeGroupDDL.SelectedValueChanged += nativeGroupDDL_SelectedValueChanged;
        _nativeGroupDDL.GotFocus += _nativeGroupDDL_GotFocus;
        _nativeNameDDL.GotFocus += _nativeNameDDL_GotFocus;
        _ontickText = ontickText;
        _outputText = ouputText;
        _immediateText = immediateText;

        _enablePropUpdate = enablePropUpdate;
        _enablePropUpdate.CheckStateChanged += _enablePropUpdate_CheckStateChanged;

        _enableBoolColors = enableBoolColors;
        _enableBoolColors.CheckStateChanged += _enableBoolColors_CheckStateChanged;

        _addToPropBtn = addToPropBtn;
        _addOnTickBtn = addOnTickBtn;
        _addImmediateButton = addImmediateButton;

        _addToPropBtn.Click += _addBtnClick;
        _addOnTickBtn.Click += _addBtnClick;
        _addImmediateButton.Click += _addBtnClick;
        _propertyControlGrid.MouseDown += _propertyControlGrid_MouseDown;
        
      //  _propertyControlGrid

        initGrid();
    }

    void _nativeNameDDL_GotFocus(object sender, EventArgs e)
    {
        _propertyControlGrid.ClearSelection();
    }

    void _nativeGroupDDL_GotFocus(object sender, EventArgs e)
    {
        _propertyControlGrid.ClearSelection();
    }

    void _propertyControlGrid_LostFocus(object sender, EventArgs e)
    {

    }

    void _propertyControlGrid_Click(object sender, EventArgs e)
    {
        _addOnTickBtn.Enabled = true;
        _addImmediateButton.Enabled = true;
    }

    void _enablePropUpdate_CheckStateChanged(object sender, EventArgs e)
    {
        if (!_enablePropUpdate.Checked)
        {
            // Unchecked means must be recompiled
            foreach (var key in _propMapData.Keys)
            {
                PropertyBoundItem propertyBoundItem = _propMapData[key];

                // Submit for compile/run
                propertyBoundItem.CompileHandle = null;
            }
        }
    }

    void _enableBoolColors_CheckStateChanged(object sender, EventArgs e)
    {
        if (!_enableBoolColors.Checked)
        {
            for (var i = 0; i < MAX_PROP_ROWS; i++)
                _propertyControlGrid[1, i].Style.BackColor = Color.White;
        }
    }

    private void initGrid()
    {
        _propertyControlGrid.RowTemplate.Height = 20;
        _propertyControlGrid.Columns[0].Width = 250;

        for (var i = 0; i < MAX_PROP_ROWS; i++)
        {
            _propertyControlGrid.Rows.Add("", "");
        }

        _propertyControlGrid.ClearSelection();
    }

    public List<string> GetPropsForSave()
    {
        var propData = new List<string>();
        foreach (var dict in _propMapData)
        {
            var codeBehind = dict.Value.CodeBehind.TrimEnd(new []{'\n','\r'});
            propData.Add(string.Format("{0}#{1}#{2}", dict.Key, dict.Value.Label, codeBehind));
            propData.Add(dict.Key + "#end");
        }
        return propData;

    }

    public void OnLoadWork(List<string> propDatalines)
    {
        StringBuilder sb = new StringBuilder();
        int rowId = -1;
        string label = "";
        for (var i = 0; i < propDatalines.Count; i++)
        {
            var line = propDatalines[i];
            if (line == "")
                continue;

            var parts = line.Split("#".ToCharArray());
 
            if (parts.Length < 3)
            {
                // not normal parse 
                if (parts.Length == 1)
                {
                    // additional code 
                    sb.AppendLine(parts[0]);
                }               
                else if (parts[1] == "end")
                {
                    // End of section
                    OnAddProp(label, sb.ToString(), rowId);
                    sb.Clear();
                }

            }
            
            else
            {
                // normal parse
                rowId = int.Parse(parts[0]);
                label = parts[1];
                sb.AppendLine(parts[2]);
            }
        }
    }

    #region Native
    private void nativeGroupDDL_SelectedValueChanged(object sender, EventArgs e)
    {
        ItemGroup itemGroup = (ItemGroup)_nativeGroupDDL.SelectedItem;
        var descriptors = NativeManager.GetDescriptorsByItemGroup(itemGroup);
        var namedDescriptors = new List<NativeDescriptor>();
        var unamedDescriptors = new List<NativeDescriptor>();

        foreach (var descriptor in descriptors)
        {
            if (descriptor.NativeName.StartsWith("_0x"))
                unamedDescriptors.Add(descriptor);
            else
                namedDescriptors.Add(descriptor);
        }
        var namedDescriptorsSorted = namedDescriptors.OrderBy(v => v.NativeName).ToList();
        var unamedDescriptorsSorted = unamedDescriptors.OrderBy(v => v.NativeName).ToList();
        namedDescriptorsSorted.AddRange(unamedDescriptorsSorted);
        _nativeNameDDL.DataSource = namedDescriptorsSorted;
    }

    // Native Name selected
    void _nativeNameDDL_SelectedValueChanged(object sender, EventArgs e)
    {
        NativeDescriptor nd = (NativeDescriptor)_nativeNameDDL.SelectedItem;
        _addToPropBtn.Enabled = true;
        _addOnTickBtn.Enabled = true;
        _addImmediateButton.Enabled = true;
        Debug.WriteLine(nd.ToLongName());

    }

    private void _addBtnClick(object sender, EventArgs e)
    {
        var theButton = (Button)sender;
        NativeDescriptor nd = (NativeDescriptor)_nativeNameDDL.SelectedItem;

        var textToAppend = "";
        if (_propertyControlGrid.SelectedRows.Count > 0)
            textToAppend = _propertyControlGrid.SelectedRows[0].Cells[1].Value.ToString();
        else
        {
            textToAppend = nd.ToCodeFormat();
        }

        if (theButton.Name == "_addOnTickBtn")
        {
            _ontickText.AppendText(textToAppend);
            _ontickText.AppendText("");
            _ontickText.Select();
        }
        else if (theButton.Name == "_addImmediateBtn")
        {
            _immediateText.AppendText(textToAppend);
            _immediateText.AppendText("");
            _immediateText.Select();
        }
        else if (theButton.Name == "_addToPropBtn")
        {
            OnAddProp(nd.NativeName, nd.ToCodeFormat());
        }
    }
    #endregion

    #region Property Add
    void _propertyControlGrid_MouseDown(object sender, MouseEventArgs e)
    {
        if(e.Button == MouseButtons.Right)
        {
            var hti = _propertyControlGrid.HitTest(e.X, e.Y);
            _propertyControlGrid.ClearSelection();
            _propertyControlGrid.Rows[hti.RowIndex].Selected = true;
            _enablePropUpdate.Checked = false;

            EditProperty editPropertyPopup = new EditProperty();
            if (_propMapData.ContainsKey(hti.RowIndex) && _propMapData[hti.RowIndex] != null)
            {
                 var propBoundItem = _propMapData[hti.RowIndex];
                editPropertyPopup.DisplayLabel = propBoundItem.Label;

                editPropertyPopup.CodeBehind = propBoundItem.CodeBehind;
                editPropertyPopup.Refresh();
            }
            _parent.TopMost = false;
            editPropertyPopup.StartPosition = FormStartPosition.CenterParent;
            DialogResult dialogresult = editPropertyPopup.ShowDialog(_parent);
            if (dialogresult == DialogResult.OK)
            {
                OnAddProp(editPropertyPopup.DisplayLabel, editPropertyPopup.CodeBehind);
            }

            _parent.TopMost = true;
            editPropertyPopup.Dispose();
        }
    }

    public void OnAddProp(string label, string codeBehind, int rowIndex = -1)
    {
        if (rowIndex == -1 && _propertyControlGrid.SelectedCells.Count == 0)
            return;
        var rowIndexVal = rowIndex >= 0
            ? rowIndex
            : _propertyControlGrid.SelectedCells[0].RowIndex;

        var theRow = _propertyControlGrid.Rows[rowIndexVal];

        theRow.Cells[0].Value = label;
        theRow.Cells[1].Value = "";

        var newBoundItem = new PropertyBoundItem(label, codeBehind);

        if (!_propMapData.ContainsKey(theRow.Index))
            _propMapData.Add(theRow.Index, newBoundItem);
        else
        {
            _propMapData[theRow.Index] = newBoundItem;
        }

        if (label == "" && codeBehind == "")
            _propMapData.Remove(theRow.Index);
    }

    private void SetProp(int row, object value, string name = null)
    {
        if (name != null)
            _propertyControlGrid[0, row].Value = name;
        if (value != null)
            _propertyControlGrid[1, row].Value = value.ToString();

        if (_enableBoolColors.Checked)
        {
            var backColor = Color.White;
            if (value.ToString() == "True")
                backColor = Color.LightGreen;
            else if (value.ToString() == "False")
                backColor = Color.LightCoral;

            _propertyControlGrid[1, row].Style.BackColor = backColor;
        }
    }
    #endregion Region

    public void OnYieldTick()
    {
        updatePropertiesGrid();
    }

    private void updatePropertiesGrid()
    {
        if (_propMapData.Count == 0)
            return;

     //   StringBuilder sb = new StringBuilder();
        foreach (var key in _propMapData.Keys)
        {
            PropertyBoundItem propertyBoundItem = _propMapData[key];

            // Submit for compile/run
            if (propertyBoundItem.CompileHandle == null)
            {
                string errorText;
                propertyBoundItem.CompileHandle = CSCompile.DoCompile(propertyBoundItem.CodeBehind, out errorText);

                if (errorText != null)
                {
                    SetProp(key, "Compile Fail - See Output for Details");
                    _outputText.Text =  errorText;
                    propertyBoundItem.CompileHandle = null;
                    continue;
                }
                else
                {
                    _outputText.Text = "Compiled Successfully";
                }
            }

            try
            {
                propertyBoundItem.RunResults = CSCompile.DoRun(propertyBoundItem.CompileHandle);
                SetProp(key, propertyBoundItem.RunResults);
            }
            catch (Exception ex)
            {
                SetProp(key, "Code Execution Fail on Row " + key + " - See Output for Details");
                _outputText.Text = ex.ToString();
            }
        }
        _propertyControlGrid.ClearSelection();
        _propertyControlGrid.Refresh();

    }


    //private void loadCustomHash()
    //{
    //    string[] lines;

    //    if (!File.Exists(CUSTOM_HASH_FILE))
    //    {
    //        File.WriteAllText(CUSTOM_HASH_FILE, "");

    //    }
    //    lines = File.ReadAllLines(CUSTOM_HASH_FILE);
    //    foreach (var line in lines)
    //    {
    //        var keyValSplit = line.Split("=".ToCharArray());
    //        string strVal = keyValSplit[0];
    //        int intKey = Convert.ToInt32(keyValSplit[1]);

    //        if (!_customHashLookup.ContainsKey(intKey))
    //        {
    //            _customHashLookup.Add(intKey, strVal);
    //        }
    //    }
    //}

    //private Entity getLastTargettedEntity()
    //{
    //    var thisEntity = Game.Player.GetTargetedEntity();
    //    if (thisEntity != null)
    //    {
    //        _lastTargettedEntity = thisEntity;
    //    }
    //    return _lastTargettedEntity;
    //}

    //private string getCustomHashName(int hashID)
    //{
    //    if (_customHashLookup.ContainsKey(hashID))
    //        return _customHashLookup[hashID];
    //    return hashID.ToString();
    //}

    //private void saveCustomHashName(int hashID, string hashName)
    //{
    //    if (!_customHashLookup.ContainsKey(hashID))
    //    {
    //        _customHashLookup.Add(hashID, hashName);
    //        var newEntry = string.Format("{0}={1}", hashName, hashID);
    //        File.AppendAllLines(CUSTOM_HASH_FILE, new[] { newEntry });
    //    }
    //}


 }
