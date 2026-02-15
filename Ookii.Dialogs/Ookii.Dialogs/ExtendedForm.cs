using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Ookii.Dialogs
{
	// Token: 0x0200000D RID: 13
	public partial class ExtendedForm : Form
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000051 RID: 81 RVA: 0x00003528 File Offset: 0x00001728
		// (remove) Token: 0x06000052 RID: 82 RVA: 0x00003560 File Offset: 0x00001760
		[field: DebuggerBrowsable(0)]
		public event EventHandler DwmCompositionChanged;

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000035A8 File Offset: 0x000017A8
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000035C0 File Offset: 0x000017C0
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Indicates whether or not the form automatically uses the system default font.")]
		public bool UseSystemFont
		{
			get
			{
				return this._useSystemFont;
			}
			set
			{
				this._useSystemFont = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000035CC File Offset: 0x000017CC
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000035E4 File Offset: 0x000017E4
		[Category("Appearance")]
		[Description("The glass margins of the form.")]
		public Padding GlassMargin
		{
			get
			{
				return this._glassMargin;
			}
			set
			{
				bool flag = this._glassMargin != value;
				if (flag)
				{
					this._glassMargin = value;
					this.EnableGlass();
				}
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003614 File Offset: 0x00001814
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000362C File Offset: 0x0000182C
		[Category("Behavior")]
		[Description("Indicates whether the form can be dragged by the glass areas inside the client area.")]
		[DefaultValue(true)]
		public bool AllowGlassDragging
		{
			get
			{
				return this._allowGlassDragging;
			}
			set
			{
				this._allowGlassDragging = value;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003638 File Offset: 0x00001838
		protected virtual void OnDwmCompositionChanged(EventArgs e)
		{
			EventHandler dwmCompositionChanged = this.DwmCompositionChanged;
			bool flag = dwmCompositionChanged != null;
			if (flag)
			{
				dwmCompositionChanged.Invoke(this, e);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003660 File Offset: 0x00001860
		protected override void OnLoad(EventArgs e)
		{
			bool flag = !base.DesignMode && this._useSystemFont;
			if (flag)
			{
				this.Font = SystemFonts.IconTitleFont;
				SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(this.SystemEvents_UserPreferenceChanged);
			}
			base.OnLoad(e);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000036AB File Offset: 0x000018AB
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(this.SystemEvents_UserPreferenceChanged);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000036C8 File Offset: 0x000018C8
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			bool flag = base.DesignMode || Glass.IsDwmCompositionEnabled;
			if (flag)
			{
				bool designMode = base.DesignMode;
				if (designMode)
				{
					using (HatchBrush hatchBrush = new HatchBrush(51, Color.SkyBlue, this.BackColor))
					{
						this.PaintGlassArea(pevent, hatchBrush);
					}
				}
				else
				{
					this.PaintGlassArea(pevent, Brushes.Black);
				}
			}
			else
			{
				base.OnPaintBackground(pevent);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000374C File Offset: 0x0000194C
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			bool flag = this._glassMargin.All != 0;
			if (flag)
			{
				base.Invalidate();
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000377B File Offset: 0x0000197B
		protected override void OnHandleCreated(EventArgs e)
		{
			this.EnableGlass();
			base.OnHandleCreated(e);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003790 File Offset: 0x00001990
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			int msg = m.Msg;
			if (msg != 132)
			{
				if (msg == 798)
				{
					bool flag = this._glassMargin.All != 0;
					if (flag)
					{
						this.EnableGlass();
					}
					this.OnDwmCompositionChanged(EventArgs.Empty);
					m.Result = IntPtr.Zero;
				}
			}
			else
			{
				bool flag2 = this._allowGlassDragging && m.Result == new IntPtr(1) && Glass.IsDwmCompositionEnabled;
				if (flag2)
				{
					bool flag3 = this._glassMargin.Left == -1 && this._glassMargin.Top == -1 && this._glassMargin.Right == -1 && this._glassMargin.Bottom == -1;
					if (flag3)
					{
						m.Result = new IntPtr(2);
					}
					else
					{
						Point point;
						point..ctor((int)m.LParam & 65535, (int)m.LParam >> 16);
						point = base.PointToClient(point);
						bool flag4 = point.X < this._glassMargin.Left || point.X > base.ClientSize.Width - this._glassMargin.Right || point.Y < this._glassMargin.Top || point.Y > base.ClientSize.Height - this._glassMargin.Bottom;
						if (flag4)
						{
							m.Result = new IntPtr(2);
						}
					}
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003938 File Offset: 0x00001B38
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			float width = factor.Width;
			Padding glassMargin = this.GlassMargin;
			bool flag = width != 1f;
			if (flag)
			{
				bool flag2 = glassMargin.Left > 0;
				if (flag2)
				{
					glassMargin.Left = (int)Math.Round((double)((float)glassMargin.Left * width));
				}
				bool flag3 = glassMargin.Right > 0;
				if (flag3)
				{
					glassMargin.Right = (int)Math.Round((double)((float)glassMargin.Right * width));
				}
			}
			float height = factor.Height;
			bool flag4 = height != 1f;
			if (flag4)
			{
				bool flag5 = glassMargin.Top > 0;
				if (flag5)
				{
					glassMargin.Top = (int)Math.Round((double)((float)glassMargin.Top * height));
				}
				bool flag6 = glassMargin.Bottom > 0;
				if (flag6)
				{
					glassMargin.Bottom = (int)Math.Round((double)((float)glassMargin.Bottom * height));
				}
			}
			this.GlassMargin = glassMargin;
			base.ScaleControl(factor, specified);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003A38 File Offset: 0x00001C38
		private void EnableGlass()
		{
			bool flag = !base.DesignMode && Glass.IsDwmCompositionEnabled;
			if (flag)
			{
				this.ExtendFrameIntoClientArea(this.GlassMargin);
				base.Invalidate();
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003A70 File Offset: 0x00001C70
		private void PaintGlassArea(PaintEventArgs pevent, Brush brush)
		{
			bool flag = this._glassMargin.Left == -1 && this._glassMargin.Top == -1 && this._glassMargin.Right == -1 && this._glassMargin.Bottom == -1;
			if (flag)
			{
				pevent.Graphics.FillRectangle(brush, pevent.ClipRectangle);
			}
			else
			{
				Rectangle rectangle;
				rectangle..ctor(this._glassMargin.Left, this._glassMargin.Top, base.ClientSize.Width - this._glassMargin.Right, base.ClientSize.Height - this._glassMargin.Bottom);
				pevent.Graphics.FillRectangle(new SolidBrush(this.BackColor), rectangle);
				bool flag2 = this._glassMargin.Left != 0;
				if (flag2)
				{
					pevent.Graphics.FillRectangle(brush, new Rectangle(0, 0, this._glassMargin.Left, base.ClientSize.Height));
				}
				bool flag3 = this._glassMargin.Right != 0;
				if (flag3)
				{
					pevent.Graphics.FillRectangle(brush, new Rectangle(base.ClientSize.Width - this._glassMargin.Right, 0, base.ClientSize.Width, base.ClientSize.Height));
				}
				bool flag4 = this._glassMargin.Top != 0;
				if (flag4)
				{
					pevent.Graphics.FillRectangle(brush, new Rectangle(0, 0, base.ClientSize.Width, this._glassMargin.Top));
				}
				bool flag5 = this._glassMargin.Bottom != 0;
				if (flag5)
				{
					pevent.Graphics.FillRectangle(brush, new Rectangle(0, base.ClientSize.Height - this._glassMargin.Bottom, base.ClientSize.Width, base.ClientSize.Height));
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003C7C File Offset: 0x00001E7C
		private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			bool flag = e.Category == 12 && this._useSystemFont;
			if (flag)
			{
				this.Font = SystemFonts.IconTitleFont;
			}
		}

		// Token: 0x0400002B RID: 43
		private bool _useSystemFont;

		// Token: 0x0400002C RID: 44
		private Padding _glassMargin;

		// Token: 0x0400002D RID: 45
		private bool _allowGlassDragging = true;
	}
}
