using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Ookii.Dialogs
{
	// Token: 0x02000011 RID: 17
	internal partial class InputDialogForm : ExtendedForm
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000088 RID: 136 RVA: 0x0000453C File Offset: 0x0000273C
		// (remove) Token: 0x06000089 RID: 137 RVA: 0x00004574 File Offset: 0x00002774
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OkButtonClickedEventArgs> OkButtonClicked;

		// Token: 0x0600008A RID: 138 RVA: 0x000045A9 File Offset: 0x000027A9
		public InputDialogForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000045D8 File Offset: 0x000027D8
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000045F0 File Offset: 0x000027F0
		public string MainInstruction
		{
			get
			{
				return this._mainInstruction;
			}
			set
			{
				this._mainInstruction = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000045FC File Offset: 0x000027FC
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00004614 File Offset: 0x00002814
		public string Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004620 File Offset: 0x00002820
		// (set) Token: 0x06000090 RID: 144 RVA: 0x0000463D File Offset: 0x0000283D
		public string Input
		{
			get
			{
				return this._inputTextBox.Text;
			}
			set
			{
				this._inputTextBox.Text = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004650 File Offset: 0x00002850
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000466D File Offset: 0x0000286D
		public int MaxLength
		{
			get
			{
				return this._inputTextBox.MaxLength;
			}
			set
			{
				this._inputTextBox.MaxLength = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004680 File Offset: 0x00002880
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000469D File Offset: 0x0000289D
		public bool UsePasswordMasking
		{
			get
			{
				return this._inputTextBox.UseSystemPasswordChar;
			}
			set
			{
				this._inputTextBox.UseSystemPasswordChar = value;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000046B0 File Offset: 0x000028B0
		protected virtual void OnOkButtonClicked(OkButtonClickedEventArgs e)
		{
			bool flag = this.OkButtonClicked != null;
			if (flag)
			{
				this.OkButtonClicked.Invoke(this, e);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000046D9 File Offset: 0x000028D9
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			this._textMargin = new SizeF(this._textMargin.Width * factor.Width, this._textMargin.Height * factor.Height);
			base.ScaleControl(factor, specified);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004718 File Offset: 0x00002918
		private void SizeDialog()
		{
			int num = (int)this._textMargin.Width * 2;
			int num2 = base.ClientSize.Height - this._inputTextBox.Top + (int)this._textMargin.Height * 3;
			using (Graphics graphics = this._primaryPanel.CreateGraphics())
			{
				base.ClientSize = DialogHelper.SizeDialog(graphics, this.MainInstruction, this.Content, Screen.FromControl(this), new Font(this.Font, 1), this.Font, num, num2, base.ClientSize.Width, 0);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000047CC File Offset: 0x000029CC
		private static void DrawThemeBackground(IDeviceContext dc, VisualStyleElement element, Rectangle bounds, Rectangle clipRectangle)
		{
			bool isTaskDialogThemeSupported = DialogHelper.IsTaskDialogThemeSupported;
			if (isTaskDialogThemeSupported)
			{
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(element);
				visualStyleRenderer.DrawBackground(dc, bounds, clipRectangle);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000047F6 File Offset: 0x000029F6
		private void DrawText(IDeviceContext dc, ref Point location, bool measureOnly, int width)
		{
			DialogHelper.DrawText(dc, this.MainInstruction, this.Content, ref location, new Font(this.Font, 1), this.Font, measureOnly, width);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004824 File Offset: 0x00002A24
		private void _primaryPanel_Paint(object sender, PaintEventArgs e)
		{
			InputDialogForm.DrawThemeBackground(e.Graphics, AdditionalVisualStyleElements.TaskDialog.PrimaryPanel, this._primaryPanel.ClientRectangle, e.ClipRectangle);
			Point point;
			point..ctor((int)this._textMargin.Width, (int)this._textMargin.Height);
			this.DrawText(e.Graphics, ref point, false, base.ClientSize.Width - (int)this._textMargin.Width * 2);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000048A0 File Offset: 0x00002AA0
		private void _secondaryPanel_Paint(object sender, PaintEventArgs e)
		{
			InputDialogForm.DrawThemeBackground(e.Graphics, AdditionalVisualStyleElements.TaskDialog.SecondaryPanel, this._secondaryPanel.ClientRectangle, e.ClipRectangle);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000048C5 File Offset: 0x00002AC5
		private void NewInputBoxForm_Load(object sender, EventArgs e)
		{
			this.SizeDialog();
			base.CenterToScreen();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000048D8 File Offset: 0x00002AD8
		private void _okButton_Click(object sender, EventArgs e)
		{
			OkButtonClickedEventArgs okButtonClickedEventArgs = new OkButtonClickedEventArgs(this._inputTextBox.Text, this);
			this.OnOkButtonClicked(okButtonClickedEventArgs);
			bool flag = !okButtonClickedEventArgs.Cancel;
			if (flag)
			{
				base.DialogResult = 1;
			}
		}

		// Token: 0x0400003B RID: 59
		private SizeF _textMargin = new SizeF(12f, 9f);

		// Token: 0x0400003C RID: 60
		private string _mainInstruction;

		// Token: 0x0400003D RID: 61
		private string _content;
	}
}
