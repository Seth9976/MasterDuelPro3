namespace Ookii.Dialogs
{
	// Token: 0x02000011 RID: 17
	internal partial class InputDialogForm : global::Ookii.Dialogs.ExtendedForm
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00004918 File Offset: 0x00002B18
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004950 File Offset: 0x00002B50
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Ookii.Dialogs.InputDialogForm));
			this._primaryPanel = new global::System.Windows.Forms.Panel();
			this._inputTextBox = new global::System.Windows.Forms.TextBox();
			this._secondaryPanel = new global::System.Windows.Forms.Panel();
			this._cancelButton = new global::System.Windows.Forms.Button();
			this._okButton = new global::System.Windows.Forms.Button();
			this._primaryPanel.SuspendLayout();
			this._secondaryPanel.SuspendLayout();
			base.SuspendLayout();
			this._primaryPanel.Controls.Add(this._inputTextBox);
			componentResourceManager.ApplyResources(this._primaryPanel, "_primaryPanel");
			this._primaryPanel.Name = "_primaryPanel";
			this._primaryPanel.Paint += new global::System.Windows.Forms.PaintEventHandler(this._primaryPanel_Paint);
			componentResourceManager.ApplyResources(this._inputTextBox, "_inputTextBox");
			this._inputTextBox.Name = "_inputTextBox";
			this._secondaryPanel.Controls.Add(this._cancelButton);
			this._secondaryPanel.Controls.Add(this._okButton);
			componentResourceManager.ApplyResources(this._secondaryPanel, "_secondaryPanel");
			this._secondaryPanel.Name = "_secondaryPanel";
			this._secondaryPanel.Paint += new global::System.Windows.Forms.PaintEventHandler(this._secondaryPanel_Paint);
			componentResourceManager.ApplyResources(this._cancelButton, "_cancelButton");
			this._cancelButton.DialogResult = 2;
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this._okButton, "_okButton");
			this._okButton.Name = "_okButton";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new global::System.EventHandler(this._okButton_Click);
			base.AcceptButton = this._okButton;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = 1;
			base.CancelButton = this._cancelButton;
			base.Controls.Add(this._primaryPanel);
			base.Controls.Add(this._secondaryPanel);
			base.FormBorderStyle = 3;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "InputDialogForm";
			base.ShowInTaskbar = false;
			base.UseSystemFont = true;
			base.Load += new global::System.EventHandler(this.NewInputBoxForm_Load);
			this._primaryPanel.ResumeLayout(false);
			this._primaryPanel.PerformLayout();
			this._secondaryPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x0400003F RID: 63
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000040 RID: 64
		private global::System.Windows.Forms.Panel _primaryPanel;

		// Token: 0x04000041 RID: 65
		private global::System.Windows.Forms.Panel _secondaryPanel;

		// Token: 0x04000042 RID: 66
		private global::System.Windows.Forms.Button _cancelButton;

		// Token: 0x04000043 RID: 67
		private global::System.Windows.Forms.Button _okButton;

		// Token: 0x04000044 RID: 68
		private global::System.Windows.Forms.TextBox _inputTextBox;
	}
}
