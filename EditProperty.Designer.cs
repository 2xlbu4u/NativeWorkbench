
partial class EditProperty
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
            this._propDisplayNameTxt = new System.Windows.Forms.TextBox();
            this.editPropGrp = new System.Windows.Forms.GroupBox();
            this.codeBehindgrp = new System.Windows.Forms.GroupBox();
            this._propCodeBehindTxt = new System.Windows.Forms.TextBox();
            this.propEditOKBtn = new System.Windows.Forms.Button();
            this.propEditCancelBtn = new System.Windows.Forms.Button();
            this._clearPropData = new System.Windows.Forms.Button();
            this.editPropGrp.SuspendLayout();
            this.codeBehindgrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // _propDisplayNameTxt
            // 
            this._propDisplayNameTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._propDisplayNameTxt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._propDisplayNameTxt.Location = new System.Drawing.Point(6, 17);
            this._propDisplayNameTxt.MaximumSize = new System.Drawing.Size(1000, 1000);
            this._propDisplayNameTxt.Name = "_propDisplayNameTxt";
            this._propDisplayNameTxt.Size = new System.Drawing.Size(470, 23);
            this._propDisplayNameTxt.TabIndex = 0;
            // 
            // editPropGrp
            // 
            this.editPropGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editPropGrp.Controls.Add(this._propDisplayNameTxt);
            this.editPropGrp.Location = new System.Drawing.Point(12, 39);
            this.editPropGrp.Name = "editPropGrp";
            this.editPropGrp.Size = new System.Drawing.Size(482, 48);
            this.editPropGrp.TabIndex = 1;
            this.editPropGrp.TabStop = false;
            this.editPropGrp.Text = "Displayed Name";
            // 
            // codeBehindgrp
            // 
            this.codeBehindgrp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codeBehindgrp.Controls.Add(this._propCodeBehindTxt);
            this.codeBehindgrp.Location = new System.Drawing.Point(12, 97);
            this.codeBehindgrp.Name = "codeBehindgrp";
            this.codeBehindgrp.Size = new System.Drawing.Size(482, 172);
            this.codeBehindgrp.TabIndex = 2;
            this.codeBehindgrp.TabStop = false;
            this.codeBehindgrp.Text = "Code Behind";
            // 
            // _propCodeBehindTxt
            // 
            this._propCodeBehindTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._propCodeBehindTxt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._propCodeBehindTxt.Location = new System.Drawing.Point(6, 19);
            this._propCodeBehindTxt.Multiline = true;
            this._propCodeBehindTxt.Name = "_propCodeBehindTxt";
            this._propCodeBehindTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._propCodeBehindTxt.Size = new System.Drawing.Size(470, 133);
            this._propCodeBehindTxt.TabIndex = 0;
            // 
            // propEditOKBtn
            // 
            this.propEditOKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.propEditOKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.propEditOKBtn.Location = new System.Drawing.Point(274, 280);
            this.propEditOKBtn.Name = "propEditOKBtn";
            this.propEditOKBtn.Size = new System.Drawing.Size(75, 23);
            this.propEditOKBtn.TabIndex = 3;
            this.propEditOKBtn.Text = "OK";
            this.propEditOKBtn.UseVisualStyleBackColor = true;
            // 
            // propEditCancelBtn
            // 
            this.propEditCancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.propEditCancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.propEditCancelBtn.Location = new System.Drawing.Point(366, 280);
            this.propEditCancelBtn.Name = "propEditCancelBtn";
            this.propEditCancelBtn.Size = new System.Drawing.Size(75, 23);
            this.propEditCancelBtn.TabIndex = 4;
            this.propEditCancelBtn.Text = "Cancel";
            this.propEditCancelBtn.UseVisualStyleBackColor = true;
            // 
            // _clearPropData
            // 
            this._clearPropData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._clearPropData.Location = new System.Drawing.Point(366, 12);
            this._clearPropData.Name = "_clearPropData";
            this._clearPropData.Size = new System.Drawing.Size(72, 23);
            this._clearPropData.TabIndex = 5;
            this._clearPropData.Text = "Clear";
            this._clearPropData.UseVisualStyleBackColor = true;
            this._clearPropData.Click += new System.EventHandler(this._clearPropData_Click);
            // 
            // EditProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 318);
            this.Controls.Add(this.editPropGrp);
            this.Controls.Add(this._clearPropData);
            this.Controls.Add(this.propEditCancelBtn);
            this.Controls.Add(this.propEditOKBtn);
            this.Controls.Add(this.codeBehindgrp);
            this.Name = "EditProperty";
            this.Text = "                 Edit Property";
            this.editPropGrp.ResumeLayout(false);
            this.editPropGrp.PerformLayout();
            this.codeBehindgrp.ResumeLayout(false);
            this.codeBehindgrp.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox _propDisplayNameTxt;
    private System.Windows.Forms.GroupBox editPropGrp;
    private System.Windows.Forms.GroupBox codeBehindgrp;
    private System.Windows.Forms.TextBox _propCodeBehindTxt;
    private System.Windows.Forms.Button propEditOKBtn;
    private System.Windows.Forms.Button propEditCancelBtn;
    private System.Windows.Forms.Button _clearPropData;
}
