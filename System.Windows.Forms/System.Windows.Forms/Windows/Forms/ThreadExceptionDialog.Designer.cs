namespace System.Windows.Forms
{
	/// <summary>Implements a dialog box that is displayed when an unhandled exception occurs in a thread.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001AC RID: 428
	[global::System.Runtime.InteropServices.ComVisible(true)]
	[global::System.Runtime.InteropServices.ClassInterface(global::System.Runtime.InteropServices.ClassInterfaceType.AutoDispatch)]
	public partial class ThreadExceptionDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x060011EA RID: 4586 RVA: 0x0005CC40 File Offset: 0x0005AE40
		private void InitializeComponent()
		{
			this.helpText = new global::System.Windows.Forms.Label();
			this.buttonAbort = new global::System.Windows.Forms.Button();
			this.buttonIgnore = new global::System.Windows.Forms.Button();
			this.buttonDetails = new global::System.Windows.Forms.Button();
			this.labelException = new global::System.Windows.Forms.Label();
			this.textBoxDetails = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.helpText.Location = new global::System.Drawing.Point(60, 8);
			this.helpText.Name = "helpText";
			this.helpText.Size = new global::System.Drawing.Size(356, 40);
			this.helpText.TabIndex = 0;
			this.helpText.Text = "An unhandled exception has occurred in you application. If you click Ignore the application will ignore this error and attempt to continue. If you click Abort, the application will quit immediately.";
			this.buttonAbort.DialogResult = global::System.Windows.Forms.DialogResult.Abort;
			this.buttonAbort.Location = new global::System.Drawing.Point(332, 112);
			this.buttonAbort.Name = "buttonAbort";
			this.buttonAbort.Size = new global::System.Drawing.Size(85, 23);
			this.buttonAbort.TabIndex = 4;
			this.buttonAbort.Text = "&Abort";
			this.buttonAbort.Click += new global::System.EventHandler(this.buttonAbort_Click);
			this.buttonIgnore.DialogResult = global::System.Windows.Forms.DialogResult.Ignore;
			this.buttonIgnore.Location = new global::System.Drawing.Point(236, 112);
			this.buttonIgnore.Name = "buttonIgnore";
			this.buttonIgnore.Size = new global::System.Drawing.Size(85, 23);
			this.buttonIgnore.TabIndex = 3;
			this.buttonIgnore.Text = "&Ignore";
			this.buttonDetails.Location = new global::System.Drawing.Point(140, 112);
			this.buttonDetails.Name = "buttonDetails";
			this.buttonDetails.Size = new global::System.Drawing.Size(85, 23);
			this.buttonDetails.TabIndex = 2;
			this.buttonDetails.Text = "Show &Details";
			this.buttonDetails.Click += new global::System.EventHandler(this.buttonDetails_Click);
			this.labelException.Location = new global::System.Drawing.Point(60, 64);
			this.labelException.Name = "labelException";
			this.labelException.Size = new global::System.Drawing.Size(356, 32);
			this.labelException.TabIndex = 1;
			this.textBoxDetails.Location = new global::System.Drawing.Point(8, 168);
			this.textBoxDetails.Multiline = true;
			this.textBoxDetails.Name = "textBoxDetails";
			this.textBoxDetails.ReadOnly = true;
			this.textBoxDetails.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.textBoxDetails.Size = new global::System.Drawing.Size(408, 196);
			this.textBoxDetails.TabIndex = 5;
			this.textBoxDetails.TabStop = false;
			this.textBoxDetails.Text = "";
			this.textBoxDetails.WordWrap = false;
			this.label1.Location = new global::System.Drawing.Point(8, 148);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(100, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Exception details";
			base.AcceptButton = this.buttonIgnore;
			base.CancelButton = this.buttonAbort;
			base.ClientSize = new global::System.Drawing.Size(428, 374);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.textBoxDetails);
			base.Controls.Add(this.labelException);
			base.Controls.Add(this.buttonDetails);
			base.Controls.Add(this.buttonIgnore);
			base.Controls.Add(this.buttonAbort);
			base.Controls.Add(this.helpText);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ThreadExceptionDialog";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			base.TopMost = true;
			base.Paint += new global::System.Windows.Forms.PaintEventHandler(this.PaintHandler);
			base.ResumeLayout(false);
		}

		// Token: 0x04000B3D RID: 2877
		private global::System.Windows.Forms.Button buttonIgnore;

		// Token: 0x04000B3E RID: 2878
		private global::System.Windows.Forms.Button buttonAbort;

		// Token: 0x04000B3F RID: 2879
		private global::System.Windows.Forms.Button buttonDetails;

		// Token: 0x04000B40 RID: 2880
		private global::System.Windows.Forms.Label labelException;

		// Token: 0x04000B41 RID: 2881
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000B42 RID: 2882
		private global::System.Windows.Forms.TextBox textBoxDetails;

		// Token: 0x04000B43 RID: 2883
		private global::System.Windows.Forms.Label helpText;
	}
}
