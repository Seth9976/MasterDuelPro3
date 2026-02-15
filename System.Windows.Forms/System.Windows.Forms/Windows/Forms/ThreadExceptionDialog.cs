using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Implements a dialog box that is displayed when an unhandled exception occurs in a thread.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001AC RID: 428
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public partial class ThreadExceptionDialog : Form
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ThreadExceptionDialog" /> class.</summary>
		/// <param name="t">The <see cref="T:System.Exception" /> that represents the exception that occurred. </param>
		// Token: 0x060011EB RID: 4587 RVA: 0x0005D068 File Offset: 0x0005B268
		public ThreadExceptionDialog(Exception t)
		{
			this.e = t;
			this.InitializeComponent();
			this.labelException.Text = t.Message;
			if (Form.ActiveForm != null)
			{
				this.Text = Form.ActiveForm.Text;
			}
			else
			{
				this.Text = "Mono";
			}
			this.buttonAbort.Enabled = Application.AllowQuit;
			this.RefreshDetails();
			this.FillExceptionDetails();
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0005D0D9 File Offset: 0x0005B2D9
		private void buttonDetails_Click(object sender, EventArgs e)
		{
			this.details = !this.details;
			this.RefreshDetails();
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0005D0F0 File Offset: 0x0005B2F0
		private void FillExceptionDetails()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.e.ToString());
			stringBuilder.Append(Environment.NewLine + Environment.NewLine);
			stringBuilder.Append("Loaded assemblies:" + Environment.NewLine + Environment.NewLine);
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				AssemblyName name = assemblies[i].GetName();
				stringBuilder.AppendFormat("Name:\t{0}" + Environment.NewLine, name.Name);
				stringBuilder.AppendFormat("Version:\t{0}" + Environment.NewLine, name.Version);
				stringBuilder.AppendFormat("Location:\t{0}" + Environment.NewLine, name.CodeBase);
				stringBuilder.Append(Environment.NewLine);
			}
			this.textBoxDetails.Text = stringBuilder.ToString();
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0005D1DC File Offset: 0x0005B3DC
		private void RefreshDetails()
		{
			if (this.details)
			{
				this.buttonDetails.Text = "Hide &Details";
				base.Height = 410;
				this.label1.Visible = true;
				this.textBoxDetails.Visible = true;
				return;
			}
			this.buttonDetails.Text = "Show &Details";
			this.label1.Visible = false;
			this.textBoxDetails.Visible = false;
			base.Height = 180;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0005D258 File Offset: 0x0005B458
		private void buttonAbort_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0005D25F File Offset: 0x0005B45F
		private void PaintHandler(object o, PaintEventArgs args)
		{
			args.Graphics.DrawIcon(SystemIcons.Error, 15, 10);
		}

		/// <summary>Gets or sets a value indicating whether the dialog box automatically sizes to its content.</summary>
		/// <returns>true if the dialog box automatically sizes; otherwise, false. </returns>
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0005D275 File Offset: 0x0005B475
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x0005D27D File Offset: 0x0005B47D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		// Token: 0x04000B3B RID: 2875
		private Exception e;

		// Token: 0x04000B3C RID: 2876
		private bool details;
	}
}
