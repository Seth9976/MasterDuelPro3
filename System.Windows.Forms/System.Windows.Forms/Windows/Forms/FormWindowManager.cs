using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020000BA RID: 186
	internal class FormWindowManager : InternalWindowManager
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x00020702 File Offset: 0x0001E902
		public FormWindowManager(Form form)
			: base(form)
		{
			form.MouseCaptureChanged += this.HandleCaptureChanged;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0002071D File Offset: 0x0001E91D
		private void HandleCaptureChanged(object sender, EventArgs e)
		{
			if (this.pending_activation && !this.form.Capture)
			{
				this.form.BringToFront();
				this.pending_activation = false;
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00020746 File Offset: 0x0001E946
		public override void PointToClient(ref int x, ref int y)
		{
			XplatUI.ScreenToClient(base.Form.Parent.Handle, ref x, ref y);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0002075F File Offset: 0x0001E95F
		protected override bool HandleNCLButtonDown(ref Message m)
		{
			this.pending_activation = true;
			return base.HandleNCLButtonDown(ref m);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00020770 File Offset: 0x0001E970
		protected override void HandleTitleBarDoubleClick(int x, int y)
		{
			if (base.IconRectangleContains(x, y))
			{
				this.form.Close();
			}
			else if (this.form.WindowState == FormWindowState.Maximized)
			{
				this.form.WindowState = FormWindowState.Normal;
			}
			else
			{
				this.form.WindowState = FormWindowState.Maximized;
			}
			base.HandleTitleBarDoubleClick(x, y);
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x000207C4 File Offset: 0x0001E9C4
		internal override Rectangle MaximizedBounds
		{
			get
			{
				Rectangle maximizedBounds = base.MaximizedBounds;
				int num = ThemeEngine.Current.ManagedWindowBorderWidth(this);
				maximizedBounds.Inflate(num, num);
				return maximizedBounds;
			}
		}

		// Token: 0x040004B9 RID: 1209
		private bool pending_activation;
	}
}
