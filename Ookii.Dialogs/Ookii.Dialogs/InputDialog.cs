using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Ookii.Dialogs
{
	// Token: 0x02000010 RID: 16
	[DefaultProperty("MainInstruction")]
	[DefaultEvent("ButtonClicked")]
	[Description("A dialog that allows the user to input a single text value.")]
	public class InputDialog : Component, IBindableComponent, IComponent, IDisposable
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600006C RID: 108 RVA: 0x000040D0 File Offset: 0x000022D0
		// (remove) Token: 0x0600006D RID: 109 RVA: 0x00004108 File Offset: 0x00002308
		[Category("Property Changed")]
		[Description("Event raised when the value of the Input property changes.")]
		[field: DebuggerBrowsable(0)]
		public event EventHandler InputChanged;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600006E RID: 110 RVA: 0x00004140 File Offset: 0x00002340
		// (remove) Token: 0x0600006F RID: 111 RVA: 0x00004178 File Offset: 0x00002378
		[Category("Action")]
		[Description("Event raised when the user clicks the OK button on the dialog.")]
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OkButtonClickedEventArgs> OkButtonClicked;

		// Token: 0x06000070 RID: 112 RVA: 0x000041AD File Offset: 0x000023AD
		public InputDialog()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000041D0 File Offset: 0x000023D0
		public InputDialog(IContainer container)
		{
			bool flag = container != null;
			if (flag)
			{
				container.Add(this);
			}
			this.InitializeComponent();
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004210 File Offset: 0x00002410
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00004231 File Offset: 0x00002431
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The dialog's main instruction.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string MainInstruction
		{
			get
			{
				return this._mainInstruction ?? string.Empty;
			}
			set
			{
				this._mainInstruction = (string.IsNullOrEmpty(value) ? null : value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004248 File Offset: 0x00002448
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00004269 File Offset: 0x00002469
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The dialog's primary content.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string Content
		{
			get
			{
				return this._content ?? string.Empty;
			}
			set
			{
				this._content = (string.IsNullOrEmpty(value) ? null : value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004280 File Offset: 0x00002480
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000042A1 File Offset: 0x000024A1
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The window title of the task dialog.")]
		[DefaultValue("")]
		public string WindowTitle
		{
			get
			{
				return this._windowTitle ?? string.Empty;
			}
			set
			{
				this._windowTitle = (string.IsNullOrEmpty(value) ? null : value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000042B8 File Offset: 0x000024B8
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000042D9 File Offset: 0x000024D9
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text specified by the user.")]
		[DefaultValue("")]
		public string Input
		{
			get
			{
				return this._input ?? string.Empty;
			}
			set
			{
				value = (this._input = (string.IsNullOrEmpty(value) ? null : value));
				this.OnInputChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004300 File Offset: 0x00002500
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00004318 File Offset: 0x00002518
		[Localizable(true)]
		[Category("Behavior")]
		[Description("The maximum number of characters that can be entered into the input field of the dialog.")]
		[DefaultValue(32767)]
		public int MaxLength
		{
			get
			{
				return this._maxLength;
			}
			set
			{
				this._maxLength = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004324 File Offset: 0x00002524
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000433C File Offset: 0x0000253C
		[Category("Behavior")]
		[Description("Indicates whether the input will be masked using the system password character.")]
		[DefaultValue(false)]
		public bool UsePasswordMasking
		{
			get
			{
				return this._usePasswordMasking;
			}
			set
			{
				this._usePasswordMasking = value;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004348 File Offset: 0x00002548
		protected virtual void OnInputChanged(EventArgs e)
		{
			bool flag = this.InputChanged != null;
			if (flag)
			{
				this.InputChanged.Invoke(this, e);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004374 File Offset: 0x00002574
		protected virtual void OnOkButtonClicked(OkButtonClickedEventArgs e)
		{
			bool flag = this.OkButtonClicked != null;
			if (flag)
			{
				this.OkButtonClicked.Invoke(this, e);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000043A0 File Offset: 0x000025A0
		public DialogResult ShowDialog()
		{
			return this.ShowDialog(null);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000043BC File Offset: 0x000025BC
		public DialogResult ShowDialog(IWin32Window owner)
		{
			DialogResult dialogResult2;
			using (InputDialogForm inputDialogForm = new InputDialogForm())
			{
				inputDialogForm.MainInstruction = this.MainInstruction;
				inputDialogForm.Content = this.Content;
				inputDialogForm.Text = this.WindowTitle;
				inputDialogForm.Input = this.Input;
				inputDialogForm.UsePasswordMasking = this.UsePasswordMasking;
				inputDialogForm.MaxLength = this.MaxLength;
				inputDialogForm.OkButtonClicked += new EventHandler<OkButtonClickedEventArgs>(this.InputBoxForm_OkButtonClicked);
				DialogResult dialogResult = inputDialogForm.ShowDialog(owner);
				bool flag = dialogResult == 1;
				if (flag)
				{
					this.Input = inputDialogForm.Input;
				}
				dialogResult2 = dialogResult;
			}
			return dialogResult2;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004470 File Offset: 0x00002670
		private void InputBoxForm_OkButtonClicked(object sender, OkButtonClickedEventArgs e)
		{
			this.OnOkButtonClicked(e);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000447C File Offset: 0x0000267C
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000044A6 File Offset: 0x000026A6
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		public BindingContext BindingContext
		{
			get
			{
				BindingContext bindingContext;
				if ((bindingContext = this._context) == null)
				{
					bindingContext = (this._context = new BindingContext());
				}
				return bindingContext;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000044B0 File Offset: 0x000026B0
		[DesignerSerializationVisibility(2)]
		[Category("Data")]
		[RefreshProperties(1)]
		[ParenthesizePropertyName(true)]
		public ControlBindingsCollection DataBindings
		{
			get
			{
				ControlBindingsCollection controlBindingsCollection;
				if ((controlBindingsCollection = this._bindings) == null)
				{
					controlBindingsCollection = (this._bindings = new ControlBindingsCollection(this));
				}
				return controlBindingsCollection;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000044DC File Offset: 0x000026DC
		protected override void Dispose(bool disposing)
		{
			try
			{
				bool flag = disposing && this.components != null;
				if (flag)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000452C File Offset: 0x0000272C
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x04000030 RID: 48
		private string _mainInstruction;

		// Token: 0x04000031 RID: 49
		private string _content;

		// Token: 0x04000032 RID: 50
		private string _windowTitle;

		// Token: 0x04000033 RID: 51
		private string _input;

		// Token: 0x04000034 RID: 52
		private int _maxLength = 32767;

		// Token: 0x04000035 RID: 53
		private bool _usePasswordMasking;

		// Token: 0x04000038 RID: 56
		private BindingContext _context;

		// Token: 0x04000039 RID: 57
		private ControlBindingsCollection _bindings;

		// Token: 0x0400003A RID: 58
		private IContainer components = null;
	}
}
