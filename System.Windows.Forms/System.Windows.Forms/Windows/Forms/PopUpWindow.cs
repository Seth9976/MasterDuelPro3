using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000135 RID: 309
	internal class PopUpWindow : Control
	{
		// Token: 0x06000C43 RID: 3139 RVA: 0x00035B66 File Offset: 0x00033D66
		public PopUpWindow(Control form, Menu menu)
		{
			this.menu = menu;
			this.form = form;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw, true);
			this.is_visible = false;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00035B98 File Offset: 0x00033D98
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Caption = "Menu PopUp";
				createParams.Style = int.MinValue;
				createParams.ExStyle |= 136;
				return createParams;
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00035BC8 File Offset: 0x00033DC8
		public void ShowWindow()
		{
			XplatUI.SetCursor(this.form.Handle, Cursors.Default.handle);
			this.RefreshItems();
			base.Show();
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00035BF0 File Offset: 0x00033DF0
		internal override void OnPaintInternal(PaintEventArgs args)
		{
			ThemeEngine.Current.DrawPopupMenu(args.Graphics, this.menu, args.ClipRectangle, base.ClientRectangle);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00035C14 File Offset: 0x00033E14
		public void HideWindow()
		{
			XplatUI.SetCursor(this.form.Handle, this.form.Cursor.handle);
			MenuTracker.HideSubPopups(this.menu, null);
			base.Hide();
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00035C48 File Offset: 0x00033E48
		protected override void CreateHandle()
		{
			base.CreateHandle();
			this.RefreshItems();
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00035C58 File Offset: 0x00033E58
		internal void RefreshItems()
		{
			Point point = new Point(base.Location.X, base.Location.Y);
			ThemeEngine.Current.CalcPopupMenuSize(base.DeviceContext, this.menu);
			if (point.X + this.menu.Rect.Width > SystemInformation.VirtualScreen.Width)
			{
				if (point.X - this.menu.Rect.Width > 0 && !(this.menu.parent_menu is MainMenu))
				{
					point.X -= this.menu.Rect.Width;
				}
				else
				{
					point.X = SystemInformation.VirtualScreen.Width - this.menu.Rect.Width;
				}
				if (point.X < 0)
				{
					point.X = 0;
				}
			}
			if (point.Y + this.menu.Rect.Height > SystemInformation.VirtualScreen.Height)
			{
				if (point.Y - this.menu.Rect.Height > 0)
				{
					point.Y -= this.menu.Rect.Height;
				}
				else
				{
					point.Y = SystemInformation.VirtualScreen.Height - this.menu.Rect.Height;
				}
				if (point.Y < 0)
				{
					point.Y = 0;
				}
			}
			base.Location = point;
			base.Width = this.menu.Rect.Width;
			base.Height = this.menu.Rect.Height;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x00002D70 File Offset: 0x00000F70
		internal override bool ActivateOnShow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040007C3 RID: 1987
		private Menu menu;

		// Token: 0x040007C4 RID: 1988
		private Control form;
	}
}
