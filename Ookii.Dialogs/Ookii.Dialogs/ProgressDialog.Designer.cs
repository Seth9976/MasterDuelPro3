using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x0200003B RID: 59
	[DefaultEvent("DoWork")]
	[DefaultProperty("Text")]
	[Description("Represents a dialog that can be used to report progress to the user.")]
	public class ProgressDialog : Component
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060000D0 RID: 208 RVA: 0x00004E84 File Offset: 0x00003084
		// (remove) Token: 0x060000D1 RID: 209 RVA: 0x00004EBC File Offset: 0x000030BC
		[field: DebuggerBrowsable(0)]
		public event DoWorkEventHandler DoWork;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060000D2 RID: 210 RVA: 0x00004EF4 File Offset: 0x000030F4
		// (remove) Token: 0x060000D3 RID: 211 RVA: 0x00004F2C File Offset: 0x0000312C
		[field: DebuggerBrowsable(0)]
		public event RunWorkerCompletedEventHandler RunWorkerCompleted;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060000D4 RID: 212 RVA: 0x00004F64 File Offset: 0x00003164
		// (remove) Token: 0x060000D5 RID: 213 RVA: 0x00004F9C File Offset: 0x0000319C
		[field: DebuggerBrowsable(0)]
		public event ProgressChangedEventHandler ProgressChanged;

		// Token: 0x060000D6 RID: 214 RVA: 0x00004FD1 File Offset: 0x000031D1
		public ProgressDialog()
			: this(null)
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004FDC File Offset: 0x000031DC
		public ProgressDialog(IContainer container)
		{
			bool flag = container != null;
			if (flag)
			{
				container.Add(this);
			}
			this.InitializeComponent();
			this.ProgressBarStyle = ProgressBarStyle.ProgressBar;
			this.ShowCancelButton = true;
			this.MinimizeBox = true;
			bool flag2 = !NativeMethods.IsWindowsVistaOrLater;
			if (flag2)
			{
				this.Animation = AnimationResource.GetShellAnimation(ShellAnimation.FlyingPapers);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005044 File Offset: 0x00003244
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00005065 File Offset: 0x00003265
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text in the progress dialog's title bar.")]
		[DefaultValue("")]
		public string WindowTitle
		{
			get
			{
				return this._windowTitle ?? string.Empty;
			}
			set
			{
				this._windowTitle = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005070 File Offset: 0x00003270
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00005094 File Offset: 0x00003294
		[Localizable(true)]
		[Category("Appearance")]
		[Description("A short description of the operation being carried out.")]
		public string Text
		{
			get
			{
				return this._text ?? string.Empty;
			}
			set
			{
				this._text = value;
				bool flag = this._dialog != null;
				if (flag)
				{
					this._dialog.SetLine(1U, this.Text, this.UseCompactPathsForText, IntPtr.Zero);
				}
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000050D4 File Offset: 0x000032D4
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000050EC File Offset: 0x000032EC
		[Category("Behavior")]
		[Description("Indicates whether path strings in the Text property should be compacted if they are too large to fit on one line.")]
		[DefaultValue(false)]
		public bool UseCompactPathsForText
		{
			get
			{
				return this._useCompactPathsForText;
			}
			set
			{
				this._useCompactPathsForText = value;
				bool flag = this._dialog != null;
				if (flag)
				{
					this._dialog.SetLine(1U, this.Text, this.UseCompactPathsForText, IntPtr.Zero);
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000512C File Offset: 0x0000332C
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00005150 File Offset: 0x00003350
		[Localizable(true)]
		[Category("Appearance")]
		[Description("Additional details about the operation being carried out.")]
		[DefaultValue("")]
		public string Description
		{
			get
			{
				return this._description ?? string.Empty;
			}
			set
			{
				this._description = value;
				bool flag = this._dialog != null;
				if (flag)
				{
					this._dialog.SetLine(2U, this.Description, this.UseCompactPathsForDescription, IntPtr.Zero);
				}
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005190 File Offset: 0x00003390
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000051A8 File Offset: 0x000033A8
		[Category("Behavior")]
		[Description("Indicates whether path strings in the Description property should be compacted if they are too large to fit on one line.")]
		[DefaultValue(false)]
		public bool UseCompactPathsForDescription
		{
			get
			{
				return this._useCompactPathsForDescription;
			}
			set
			{
				this._useCompactPathsForDescription = value;
				bool flag = this._dialog != null;
				if (flag)
				{
					this._dialog.SetLine(2U, this.Description, this.UseCompactPathsForDescription, IntPtr.Zero);
				}
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000051E8 File Offset: 0x000033E8
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00005209 File Offset: 0x00003409
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text that will be shown after the Cancel button is pressed.")]
		[DefaultValue("")]
		public string CancellationText
		{
			get
			{
				return this._cancellationText ?? string.Empty;
			}
			set
			{
				this._cancellationText = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005213 File Offset: 0x00003413
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000521B File Offset: 0x0000341B
		[Category("Appearance")]
		[Description("Indicates whether an estimate of the remaining time will be shown.")]
		[DefaultValue(false)]
		public bool ShowTimeRemaining { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005224 File Offset: 0x00003424
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x0000522C File Offset: 0x0000342C
		[Category("Appearance")]
		[Description("Indicates whether the dialog has a cancel button. Do not set to false unless absolutely necessary.")]
		[DefaultValue(true)]
		public bool ShowCancelButton { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005235 File Offset: 0x00003435
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x0000523D File Offset: 0x0000343D
		[Category("Window Style")]
		[Description("Indicates whether the progress dialog has a minimize button.")]
		[DefaultValue(true)]
		public bool MinimizeBox { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005248 File Offset: 0x00003448
		[Browsable(false)]
		public bool CancellationPending
		{
			get
			{
				this._backgroundWorker.ReportProgress(-1);
				return this._cancellationPending;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000526D File Offset: 0x0000346D
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00005275 File Offset: 0x00003475
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public AnimationResource Animation { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000527E File Offset: 0x0000347E
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00005286 File Offset: 0x00003486
		[Category("Appearance")]
		[Description("Indicates the style of the progress bar.")]
		[DefaultValue(ProgressBarStyle.ProgressBar)]
		public ProgressBarStyle ProgressBarStyle { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005290 File Offset: 0x00003490
		[Browsable(false)]
		public bool IsBusy
		{
			get
			{
				return this._backgroundWorker.IsBusy;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000052AD File Offset: 0x000034AD
		public void Show()
		{
			this.Show(null);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000052B8 File Offset: 0x000034B8
		public void Show(object argument)
		{
			this.RunProgressDialog(IntPtr.Zero, argument);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000052C8 File Offset: 0x000034C8
		public void ShowDialog()
		{
			this.ShowDialog(null, null);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000052D4 File Offset: 0x000034D4
		public void ShowDialog(IWin32Window owner)
		{
			this.ShowDialog(owner, null);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000052E0 File Offset: 0x000034E0
		public void ShowDialog(IWin32Window owner, object argument)
		{
			this.RunProgressDialog((owner == null) ? NativeMethods.GetActiveWindow() : owner.Handle, argument);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000052FB File Offset: 0x000034FB
		public void ReportProgress(int percentProgress)
		{
			this.ReportProgress(percentProgress, null, null, null);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005309 File Offset: 0x00003509
		public void ReportProgress(int percentProgress, string text, string description)
		{
			this.ReportProgress(percentProgress, text, description, null);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005318 File Offset: 0x00003518
		public void ReportProgress(int percentProgress, string text, string description, object userState)
		{
			bool flag = percentProgress < 0 || percentProgress > 100;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("percentProgress");
			}
			bool flag2 = this._dialog == null;
			if (flag2)
			{
				throw new InvalidOperationException(Resources.ProgressDialogNotRunningError);
			}
			this._backgroundWorker.ReportProgress(percentProgress, new ProgressDialog.ProgressChangedData
			{
				Text = text,
				Description = description,
				UserState = userState
			});
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005384 File Offset: 0x00003584
		protected virtual void OnDoWork(DoWorkEventArgs e)
		{
			DoWorkEventHandler doWork = this.DoWork;
			bool flag = doWork != null;
			if (flag)
			{
				doWork.Invoke(this, e);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000053AC File Offset: 0x000035AC
		protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
		{
			RunWorkerCompletedEventHandler runWorkerCompleted = this.RunWorkerCompleted;
			bool flag = runWorkerCompleted != null;
			if (flag)
			{
				runWorkerCompleted.Invoke(this, e);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000053D4 File Offset: 0x000035D4
		protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
		{
			ProgressChangedEventHandler progressChanged = this.ProgressChanged;
			bool flag = progressChanged != null;
			if (flag)
			{
				progressChanged.Invoke(this, e);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000053FC File Offset: 0x000035FC
		private void RunProgressDialog(IntPtr owner, object argument)
		{
			bool isBusy = this._backgroundWorker.IsBusy;
			if (isBusy)
			{
				throw new InvalidOperationException(Resources.ProgressDialogRunning);
			}
			bool flag = this.Animation != null;
			if (flag)
			{
				try
				{
					this._currentAnimationModuleHandle = this.Animation.LoadLibrary();
				}
				catch (Win32Exception ex)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.AnimationLoadErrorFormat, new object[] { ex.Message }), ex);
				}
				catch (FileNotFoundException ex2)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.AnimationLoadErrorFormat, new object[] { ex2.Message }), ex2);
				}
			}
			this._cancellationPending = false;
			this._dialog = (ProgressDialog)new ProgressDialogRCW();
			this._dialog.SetTitle(this.WindowTitle);
			bool flag2 = this.Animation != null;
			if (flag2)
			{
				this._dialog.SetAnimation(this._currentAnimationModuleHandle, (ushort)this.Animation.ResourceId);
			}
			bool flag3 = this.CancellationText.Length > 0;
			if (flag3)
			{
				this._dialog.SetCancelMsg(this.CancellationText, null);
			}
			this._dialog.SetLine(1U, this.Text, this.UseCompactPathsForText, IntPtr.Zero);
			this._dialog.SetLine(2U, this.Description, this.UseCompactPathsForDescription, IntPtr.Zero);
			ProgressDialogFlags progressDialogFlags = ProgressDialogFlags.Normal;
			bool flag4 = owner != IntPtr.Zero;
			if (flag4)
			{
				progressDialogFlags |= ProgressDialogFlags.Modal;
			}
			ProgressBarStyle progressBarStyle = this.ProgressBarStyle;
			if (progressBarStyle != ProgressBarStyle.None)
			{
				if (progressBarStyle == ProgressBarStyle.MarqueeProgressBar)
				{
					bool isWindowsVistaOrLater = NativeMethods.IsWindowsVistaOrLater;
					if (isWindowsVistaOrLater)
					{
						progressDialogFlags |= ProgressDialogFlags.MarqueeProgress;
					}
					else
					{
						progressDialogFlags |= ProgressDialogFlags.NoProgressBar;
					}
				}
			}
			else
			{
				progressDialogFlags |= ProgressDialogFlags.NoProgressBar;
			}
			bool showTimeRemaining = this.ShowTimeRemaining;
			if (showTimeRemaining)
			{
				progressDialogFlags |= ProgressDialogFlags.AutoTime;
			}
			bool flag5 = !this.ShowCancelButton;
			if (flag5)
			{
				progressDialogFlags |= ProgressDialogFlags.NoCancel;
			}
			bool flag6 = !this.MinimizeBox;
			if (flag6)
			{
				progressDialogFlags |= ProgressDialogFlags.NoMinimize;
			}
			this._dialog.StartProgressDialog(owner, null, progressDialogFlags, IntPtr.Zero);
			this._backgroundWorker.RunWorkerAsync(argument);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005614 File Offset: 0x00003814
		private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			this.OnDoWork(e);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005620 File Offset: 0x00003820
		private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this._dialog.StopProgressDialog();
			Marshal.ReleaseComObject(this._dialog);
			this._dialog = null;
			bool flag = this._currentAnimationModuleHandle != null;
			if (flag)
			{
				this._currentAnimationModuleHandle.Dispose();
				this._currentAnimationModuleHandle = null;
			}
			this.OnRunWorkerCompleted(new RunWorkerCompletedEventArgs((!e.Cancelled && e.Error == null) ? e.Result : null, e.Error, e.Cancelled));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000056A0 File Offset: 0x000038A0
		private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this._cancellationPending = this._dialog.HasUserCancelled();
			bool flag = e.ProgressPercentage >= 0 && e.ProgressPercentage <= 100;
			if (flag)
			{
				this._dialog.SetProgress((uint)e.ProgressPercentage, 100U);
				ProgressDialog.ProgressChangedData progressChangedData = e.UserState as ProgressDialog.ProgressChangedData;
				bool flag2 = progressChangedData != null;
				if (flag2)
				{
					bool flag3 = progressChangedData.Text != null;
					if (flag3)
					{
						this.Text = progressChangedData.Text;
					}
					bool flag4 = progressChangedData.Description != null;
					if (flag4)
					{
						this.Description = progressChangedData.Description;
					}
					this.OnProgressChanged(new ProgressChangedEventArgs(e.ProgressPercentage, progressChangedData.UserState));
				}
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005758 File Offset: 0x00003958
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					bool flag = this.components != null;
					if (flag)
					{
						this.components.Dispose();
					}
					bool flag2 = this._currentAnimationModuleHandle != null;
					if (flag2)
					{
						this._currentAnimationModuleHandle.Dispose();
						this._currentAnimationModuleHandle = null;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000057C8 File Offset: 0x000039C8
		private void InitializeComponent()
		{
			this._backgroundWorker = new BackgroundWorker();
			this._backgroundWorker.WorkerReportsProgress = true;
			this._backgroundWorker.WorkerSupportsCancellation = true;
			this._backgroundWorker.DoWork += new DoWorkEventHandler(this._backgroundWorker_DoWork);
			this._backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this._backgroundWorker_RunWorkerCompleted);
			this._backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this._backgroundWorker_ProgressChanged);
		}

		// Token: 0x040001A1 RID: 417
		private string _windowTitle;

		// Token: 0x040001A2 RID: 418
		private string _text;

		// Token: 0x040001A3 RID: 419
		private string _description;

		// Token: 0x040001A4 RID: 420
		private IProgressDialog _dialog;

		// Token: 0x040001A5 RID: 421
		private string _cancellationText;

		// Token: 0x040001A6 RID: 422
		private bool _useCompactPathsForText;

		// Token: 0x040001A7 RID: 423
		private bool _useCompactPathsForDescription;

		// Token: 0x040001A8 RID: 424
		private SafeModuleHandle _currentAnimationModuleHandle;

		// Token: 0x040001A9 RID: 425
		private bool _cancellationPending;

		// Token: 0x040001B2 RID: 434
		private IContainer components = null;

		// Token: 0x040001B3 RID: 435
		private BackgroundWorker _backgroundWorker;

		// Token: 0x0200003C RID: 60
		private class ProgressChangedData
		{
			// Token: 0x1700003A RID: 58
			// (get) Token: 0x06000101 RID: 257 RVA: 0x00005843 File Offset: 0x00003A43
			// (set) Token: 0x06000102 RID: 258 RVA: 0x0000584B File Offset: 0x00003A4B
			public string Text { get; set; }

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x06000103 RID: 259 RVA: 0x00005854 File Offset: 0x00003A54
			// (set) Token: 0x06000104 RID: 260 RVA: 0x0000585C File Offset: 0x00003A5C
			public string Description { get; set; }

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000105 RID: 261 RVA: 0x00005865 File Offset: 0x00003A65
			// (set) Token: 0x06000106 RID: 262 RVA: 0x0000586D File Offset: 0x00003A6D
			public object UserState { get; set; }
		}
	}
}
