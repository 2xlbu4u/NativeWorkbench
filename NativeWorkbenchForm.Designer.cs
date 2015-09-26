

public partial class NativeWorkbenchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._immediateSourceTxt = new System.Windows.Forms.TextBox();
            this._propertiesGrid = new System.Windows.Forms.DataGridView();
            this.PropName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.immediate = new System.Windows.Forms.GroupBox();
            this.executeImmediateBtn = new System.Windows.Forms.Button();
            this._propertysGrp = new System.Windows.Forms.GroupBox();
            this._flyMode = new System.Windows.Forms.CheckBox();
            this._enablePropUpdate = new System.Windows.Forms.CheckBox();
            this._enableBoolColors = new System.Windows.Forms.CheckBox();
            this.ontickUpdateBtn = new System.Windows.Forms.CheckBox();
            this.OutputGroup = new System.Windows.Forms.GroupBox();
            this._outputTxt = new System.Windows.Forms.TextBox();
            this._nativeGroupDDL = new System.Windows.Forms.ComboBox();
            this.nariveTypeGroup = new System.Windows.Forms.GroupBox();
            this.nativeNameGroup = new System.Windows.Forms.GroupBox();
            this._nativeNameDDL = new System.Windows.Forms.ComboBox();
            this._addOnTickBtn = new System.Windows.Forms.Button();
            this.onTickGroup = new System.Windows.Forms.GroupBox();
            this._onTickTxt = new System.Windows.Forms.TextBox();
            this._addImmediateBtn = new System.Windows.Forms.Button();
            this._addToPropBtn = new System.Windows.Forms.Button();
            this._useEmptyMapCb = new System.Windows.Forms.CheckBox();
            this._clerAreacb = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this._propertiesGrid)).BeginInit();
            this.immediate.SuspendLayout();
            this._propertysGrp.SuspendLayout();
            this.OutputGroup.SuspendLayout();
            this.nariveTypeGroup.SuspendLayout();
            this.nativeNameGroup.SuspendLayout();
            this.onTickGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // _immediateSourceTxt
            // 
            this._immediateSourceTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._immediateSourceTxt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._immediateSourceTxt.Location = new System.Drawing.Point(5, 22);
            this._immediateSourceTxt.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this._immediateSourceTxt.Multiline = true;
            this._immediateSourceTxt.Name = "_immediateSourceTxt";
            this._immediateSourceTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._immediateSourceTxt.Size = new System.Drawing.Size(679, 205);
            this._immediateSourceTxt.TabIndex = 2;
            // 
            // _propertiesGrid
            // 
            this._propertiesGrid.AllowUserToAddRows = false;
            this._propertiesGrid.AllowUserToDeleteRows = false;
            this._propertiesGrid.AllowUserToResizeRows = false;
            this._propertiesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._propertiesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._propertiesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PropName,
            this.Value});
            this._propertiesGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this._propertiesGrid.Location = new System.Drawing.Point(5, 23);
            this._propertiesGrid.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this._propertiesGrid.MultiSelect = false;
            this._propertiesGrid.Name = "_propertiesGrid";
            this._propertiesGrid.ReadOnly = true;
            this._propertiesGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this._propertiesGrid.RowHeadersVisible = false;
            this._propertiesGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._propertiesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._propertiesGrid.Size = new System.Drawing.Size(683, 85);
            this._propertiesGrid.TabIndex = 0;
            // 
            // PropName
            // 
            this.PropName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PropName.HeaderText = "Name";
            this.PropName.Name = "PropName";
            this.PropName.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // immediate
            // 
            this.immediate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.immediate.Controls.Add(this.executeImmediateBtn);
            this.immediate.Controls.Add(this._immediateSourceTxt);
            this.immediate.Location = new System.Drawing.Point(23, 494);
            this.immediate.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.immediate.Name = "immediate";
            this.immediate.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.immediate.Size = new System.Drawing.Size(695, 269);
            this.immediate.TabIndex = 3;
            this.immediate.TabStop = false;
            this.immediate.Text = "Immediate";
            // 
            // executeImmediateBtn
            // 
            this.executeImmediateBtn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.executeImmediateBtn.Location = new System.Drawing.Point(8, 233);
            this.executeImmediateBtn.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.executeImmediateBtn.Name = "executeImmediateBtn";
            this.executeImmediateBtn.Size = new System.Drawing.Size(101, 26);
            this.executeImmediateBtn.TabIndex = 3;
            this.executeImmediateBtn.Text = "Run";
            this.executeImmediateBtn.UseVisualStyleBackColor = true;
            this.executeImmediateBtn.Click += new System.EventHandler(this.OnCompileRunClick);
            // 
            // _propertysGrp
            // 
            this._propertysGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._propertysGrp.Controls.Add(this._enablePropUpdate);
            this._propertysGrp.Controls.Add(this._enableBoolColors);
            this._propertysGrp.Controls.Add(this._propertiesGrid);
            this._propertysGrp.Location = new System.Drawing.Point(21, 113);
            this._propertysGrp.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this._propertysGrp.Name = "_propertysGrp";
            this._propertysGrp.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this._propertysGrp.Size = new System.Drawing.Size(695, 144);
            this._propertysGrp.TabIndex = 2;
            this._propertysGrp.TabStop = false;
            this._propertysGrp.Text = "Properties";
            // 
            // _flyMode
            // 
            this._flyMode.AutoSize = true;
            this._flyMode.Location = new System.Drawing.Point(130, 68);
            this._flyMode.Name = "_flyMode";
            this._flyMode.Size = new System.Drawing.Size(86, 20);
            this._flyMode.TabIndex = 3;
            this._flyMode.Text = "Fly Mode";
            this._flyMode.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this._flyMode.UseVisualStyleBackColor = true;

            // 
            // _enablePropUpdate
            // 
            this._enablePropUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._enablePropUpdate.AutoSize = true;
            this._enablePropUpdate.Location = new System.Drawing.Point(11, 116);
            this._enablePropUpdate.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this._enablePropUpdate.Name = "_enablePropUpdate";
            this._enablePropUpdate.Size = new System.Drawing.Size(187, 20);
            this._enablePropUpdate.TabIndex = 1;
            this._enablePropUpdate.Text = "Enable Properies Update";
            this._enablePropUpdate.UseVisualStyleBackColor = true;
            // 
            // _enableBoolColors
            // 
            this._enableBoolColors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._enableBoolColors.AutoSize = true;
            this._enableBoolColors.Location = new System.Drawing.Point(206, 116);
            this._enableBoolColors.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this._enableBoolColors.Name = "_enableBoolColors";
            this._enableBoolColors.Size = new System.Drawing.Size(147, 20);
            this._enableBoolColors.TabIndex = 2;
            this._enableBoolColors.Text = "Enable Bool Colors";
            this._enableBoolColors.UseVisualStyleBackColor = true;
            // 
            // ontickUpdateBtn
            // 
            this.ontickUpdateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ontickUpdateBtn.AutoSize = true;
            this.ontickUpdateBtn.CausesValidation = false;
            this.ontickUpdateBtn.Location = new System.Drawing.Point(4, 199);
            this.ontickUpdateBtn.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.ontickUpdateBtn.Name = "ontickUpdateBtn";
            this.ontickUpdateBtn.Size = new System.Drawing.Size(177, 20);
            this.ontickUpdateBtn.TabIndex = 1;
            this.ontickUpdateBtn.Text = "Enable On Tick Update";
            this.ontickUpdateBtn.UseVisualStyleBackColor = true;
            this.ontickUpdateBtn.CheckedChanged += new System.EventHandler(this.ontickUpdateBtn_CheckedChanged);
            // 
            // OutputGroup
            // 
            this.OutputGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputGroup.Controls.Add(this._outputTxt);
            this.OutputGroup.Location = new System.Drawing.Point(25, 760);
            this.OutputGroup.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.OutputGroup.Name = "OutputGroup";
            this.OutputGroup.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.OutputGroup.Size = new System.Drawing.Size(691, 99);
            this.OutputGroup.TabIndex = 4;
            this.OutputGroup.TabStop = false;
            this.OutputGroup.Text = "Output";
            // 
            // _outputTxt
            // 
            this._outputTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._outputTxt.Location = new System.Drawing.Point(8, 19);
            this._outputTxt.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this._outputTxt.Multiline = true;
            this._outputTxt.Name = "_outputTxt";
            this._outputTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._outputTxt.Size = new System.Drawing.Size(669, 71);
            this._outputTxt.TabIndex = 0;
            // 
            // _nativeGroupDDL
            // 
            this._nativeGroupDDL.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._nativeGroupDDL.FormattingEnabled = true;
            this._nativeGroupDDL.Location = new System.Drawing.Point(8, 19);
            this._nativeGroupDDL.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this._nativeGroupDDL.Name = "_nativeGroupDDL";
            this._nativeGroupDDL.Size = new System.Drawing.Size(153, 24);
            this._nativeGroupDDL.TabIndex = 3;
            // 
            // nariveTypeGroup
            // 
            this.nariveTypeGroup.Controls.Add(this._nativeGroupDDL);
            this.nariveTypeGroup.Location = new System.Drawing.Point(21, 9);
            this.nariveTypeGroup.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.nariveTypeGroup.Name = "nariveTypeGroup";
            this.nariveTypeGroup.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.nariveTypeGroup.Size = new System.Drawing.Size(173, 58);
            this.nariveTypeGroup.TabIndex = 4;
            this.nariveTypeGroup.TabStop = false;
            this.nariveTypeGroup.Text = "Native Type";
            // 
            // nativeNameGroup
            // 
            this.nativeNameGroup.Controls.Add(this._nativeNameDDL);
            this.nativeNameGroup.Location = new System.Drawing.Point(203, 9);
            this.nativeNameGroup.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.nativeNameGroup.Name = "nativeNameGroup";
            this.nativeNameGroup.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.nativeNameGroup.Size = new System.Drawing.Size(496, 58);
            this.nativeNameGroup.TabIndex = 5;
            this.nativeNameGroup.TabStop = false;
            this.nativeNameGroup.Text = "Native Name";
            // 
            // _nativeNameDDL
            // 
            this._nativeNameDDL.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._nativeNameDDL.FormattingEnabled = true;
            this._nativeNameDDL.Location = new System.Drawing.Point(10, 19);
            this._nativeNameDDL.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this._nativeNameDDL.Name = "_nativeNameDDL";
            this._nativeNameDDL.Size = new System.Drawing.Size(475, 24);
            this._nativeNameDDL.TabIndex = 0;
            // 
            // _addOnTickBtn
            // 
            this._addOnTickBtn.Enabled = false;
            this._addOnTickBtn.Location = new System.Drawing.Point(418, 79);
            this._addOnTickBtn.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this._addOnTickBtn.Name = "_addOnTickBtn";
            this._addOnTickBtn.Size = new System.Drawing.Size(127, 29);
            this._addOnTickBtn.TabIndex = 6;
            this._addOnTickBtn.Text = "Add To On Tick";
            this._addOnTickBtn.UseVisualStyleBackColor = true;
            // 
            // onTickGroup
            // 
            this.onTickGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.onTickGroup.Controls.Add(this.ontickUpdateBtn);
            this.onTickGroup.Controls.Add(this._onTickTxt);
            this.onTickGroup.Location = new System.Drawing.Point(21, 263);
            this.onTickGroup.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.onTickGroup.Name = "onTickGroup";
            this.onTickGroup.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.onTickGroup.Size = new System.Drawing.Size(695, 225);
            this.onTickGroup.TabIndex = 7;
            this.onTickGroup.TabStop = false;
            this.onTickGroup.Text = "On Tick";
            // 
            // _onTickTxt
            // 
            this._onTickTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._onTickTxt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._onTickTxt.Location = new System.Drawing.Point(2, 22);
            this._onTickTxt.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this._onTickTxt.Multiline = true;
            this._onTickTxt.Name = "_onTickTxt";
            this._onTickTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._onTickTxt.Size = new System.Drawing.Size(684, 171);
            this._onTickTxt.TabIndex = 0;
            // 
            // _addImmediateBtn
            // 
            this._addImmediateBtn.Enabled = false;
            this._addImmediateBtn.Location = new System.Drawing.Point(555, 79);
            this._addImmediateBtn.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this._addImmediateBtn.Name = "_addImmediateBtn";
            this._addImmediateBtn.Size = new System.Drawing.Size(144, 29);
            this._addImmediateBtn.TabIndex = 8;
            this._addImmediateBtn.Text = "Add To Immediate";
            this._addImmediateBtn.UseVisualStyleBackColor = true;
            // 
            // _addToPropBtn
            // 
            this._addToPropBtn.Enabled = false;
            this._addToPropBtn.Location = new System.Drawing.Point(275, 79);
            this._addToPropBtn.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this._addToPropBtn.Name = "_addToPropBtn";
            this._addToPropBtn.Size = new System.Drawing.Size(133, 29);
            this._addToPropBtn.TabIndex = 9;
            this._addToPropBtn.Text = "Add To Properties";
            this._addToPropBtn.UseVisualStyleBackColor = true;
            // 
            // _useEmptyMapCb
            // 
            this._useEmptyMapCb.AutoSize = true;
            this._useEmptyMapCb.Location = new System.Drawing.Point(32, 68);
            this._useEmptyMapCb.Name = "_useEmptyMapCb";
            this._useEmptyMapCb.Size = new System.Drawing.Size(92, 20);
            this._useEmptyMapCb.TabIndex = 10;
            this._useEmptyMapCb.Text = "Clear Map";
            this._useEmptyMapCb.UseVisualStyleBackColor = true;
            // 
            // _clerAreacb
            // 
            this._clerAreacb.AutoSize = true;
            this._clerAreacb.Cursor = System.Windows.Forms.Cursors.Default;
            this._clerAreacb.Location = new System.Drawing.Point(32, 88);
            this._clerAreacb.Name = "_clerAreacb";
            this._clerAreacb.Size = new System.Drawing.Size(95, 20);
            this._clerAreacb.TabIndex = 11;
            this._clerAreacb.Text = "Clear Area";
            this._clerAreacb.UseVisualStyleBackColor = true;

            // 
            // NativeWorkbenchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 869);
            this.Controls.Add(this._flyMode);
            this.Controls.Add(this._clerAreacb);
            this.Controls.Add(this._useEmptyMapCb);
            this.Controls.Add(this._addToPropBtn);
            this.Controls.Add(this._addImmediateBtn);
            this.Controls.Add(this.onTickGroup);
            this.Controls.Add(this.OutputGroup);
            this.Controls.Add(this.immediate);
            this.Controls.Add(this._propertysGrp);
            this.Controls.Add(this._addOnTickBtn);
            this.Controls.Add(this.nativeNameGroup);
            this.Controls.Add(this.nariveTypeGroup);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Location = new System.Drawing.Point(50, 50);
            this.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Name = "NativeWorkbenchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Native Workbench";
            this.Load += new System.EventHandler(this.ModWorkbenchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._propertiesGrid)).EndInit();
            this.immediate.ResumeLayout(false);
            this.immediate.PerformLayout();
            this._propertysGrp.ResumeLayout(false);
            this._propertysGrp.PerformLayout();
            this.OutputGroup.ResumeLayout(false);
            this.OutputGroup.PerformLayout();
            this.nariveTypeGroup.ResumeLayout(false);
            this.nativeNameGroup.ResumeLayout(false);
            this.onTickGroup.ResumeLayout(false);
            this.onTickGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _immediateSourceTxt;
        private System.Windows.Forms.DataGridView _propertiesGrid;
        private System.Windows.Forms.GroupBox immediate;
        private System.Windows.Forms.Button executeImmediateBtn;
        private System.Windows.Forms.GroupBox OutputGroup;
        private System.Windows.Forms.TextBox _outputTxt;
        private System.Windows.Forms.CheckBox ontickUpdateBtn;
        private System.Windows.Forms.GroupBox _propertysGrp;
        private System.Windows.Forms.GroupBox nativeNameGroup;
        private System.Windows.Forms.ComboBox _nativeNameDDL;
        private System.Windows.Forms.GroupBox nariveTypeGroup;
        private System.Windows.Forms.ComboBox _nativeGroupDDL;
        private System.Windows.Forms.Button _addOnTickBtn;
        private System.Windows.Forms.GroupBox onTickGroup;
        private System.Windows.Forms.Button _addImmediateBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.CheckBox _enablePropUpdate;
        private System.Windows.Forms.Button _addToPropBtn;
        private System.Windows.Forms.CheckBox _enableBoolColors;
        private System.Windows.Forms.TextBox _onTickTxt;
        private System.Windows.Forms.CheckBox _useEmptyMapCb;
        private System.Windows.Forms.CheckBox _clerAreacb;
        private System.Windows.Forms.CheckBox _flyMode;

    }
