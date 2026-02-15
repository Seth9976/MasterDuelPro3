using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents the container for multiple-document interface (MDI) child forms. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000128 RID: 296
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	public sealed class MdiClient : Control
	{
		// Token: 0x06000B90 RID: 2960 RVA: 0x000313B8 File Offset: 0x0002F5B8
		internal void SendFocusToActiveChild()
		{
			Form activeMdiChild = this.ActiveMdiChild;
			if (activeMdiChild == null)
			{
				this.ParentForm.SendControlFocus(this);
				return;
			}
			activeMdiChild.SendControlFocus(activeMdiChild);
			this.ParentForm.ActiveControl = activeMdiChild;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x000313EF File Offset: 0x0002F5EF
		internal bool HorizontalScrollbarVisible
		{
			get
			{
				return this.hbar != null && this.hbar.Visible;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00031406 File Offset: 0x0002F606
		internal bool VerticalScrollbarVisible
		{
			get
			{
				return this.vbar != null && this.vbar.Visible;
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00031420 File Offset: 0x0002F620
		internal void SetParentText(bool text_changed)
		{
			if (this.setting_form_text)
			{
				return;
			}
			this.setting_form_text = true;
			if (text_changed)
			{
				this.form_text = this.ParentForm.Text;
			}
			if (this.ParentForm.ActiveMaximizedMdiChild == null)
			{
				this.ParentForm.Text = this.form_text;
			}
			else if (this.ParentForm.ActiveMaximizedMdiChild.form.Text.Length > 0)
			{
				this.ParentForm.Text = this.form_text + " - [" + this.ParentForm.ActiveMaximizedMdiChild.form.Text + "]";
			}
			else
			{
				this.ParentForm.Text = this.form_text;
			}
			this.setting_form_text = false;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x000314DD File Offset: 0x0002F6DD
		internal Form ParentForm
		{
			get
			{
				return (Form)base.Parent;
			}
		}

		/// <summary>Gets the child multiple-document interface (MDI) forms of the <see cref="T:System.Windows.Forms.MdiClient" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> array that contains the child MDI forms of the <see cref="T:System.Windows.Forms.MdiClient" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x000314EA File Offset: 0x0002F6EA
		public Form[] MdiChildren
		{
			get
			{
				if (this.mdi_child_list == null)
				{
					return new Form[0];
				}
				return (Form[])this.mdi_child_list.ToArray(typeof(Form));
			}
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00031518 File Offset: 0x0002F718
		internal void SizeScrollBars()
		{
			if (this.lock_sizing)
			{
				return;
			}
			if (!base.IsHandleCreated)
			{
				return;
			}
			if (base.Controls.Count == 0 || ((Form)base.Controls[0]).WindowState == FormWindowState.Maximized)
			{
				if (this.hbar != null)
				{
					this.hbar.Visible = false;
				}
				if (this.vbar != null)
				{
					this.vbar.Visible = false;
				}
				if (this.sizegrip != null)
				{
					this.sizegrip.Visible = false;
				}
				return;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			foreach (object obj in base.Controls)
			{
				Form form = (Form)obj;
				if (form.Visible)
				{
					if (form.Right > num)
					{
						num = form.Right;
					}
					if (form.Left < num2)
					{
						num2 = form.Left;
					}
					if (form.Bottom > num4)
					{
						num4 = form.Bottom;
					}
					if (form.Top < 0)
					{
						num3 = form.Top;
					}
				}
			}
			int num5 = base.ClientSize.Width;
			int num6 = base.ClientSize.Height;
			bool flag = false;
			bool flag2 = false;
			if (num - num2 > num5 || num2 < 0)
			{
				flag = true;
				num6 -= SystemInformation.HorizontalScrollBarHeight;
			}
			if (num4 - num3 > num6 || num3 < 0)
			{
				flag2 = true;
				num5 -= SystemInformation.VerticalScrollBarWidth;
				if (!flag && (num - num2 > num5 || num2 < 0))
				{
					flag = true;
					num6 -= SystemInformation.HorizontalScrollBarHeight;
				}
			}
			if (flag)
			{
				if (this.hbar == null)
				{
					this.hbar = new ImplicitHScrollBar();
					base.Controls.AddImplicit(this.hbar);
				}
				this.hbar.Visible = true;
				this.CalcHBar(num2, num, flag2);
			}
			else if (this.hbar != null)
			{
				this.hbar.Visible = false;
			}
			if (flag2)
			{
				if (this.vbar == null)
				{
					this.vbar = new ImplicitVScrollBar();
					base.Controls.AddImplicit(this.vbar);
				}
				this.vbar.Visible = true;
				this.CalcVBar(num3, num4, flag);
			}
			else if (this.vbar != null)
			{
				this.vbar.Visible = false;
			}
			if (flag && flag2)
			{
				if (this.sizegrip == null)
				{
					this.sizegrip = new SizeGrip(this.ParentForm);
					base.Controls.AddImplicit(this.sizegrip);
				}
				this.sizegrip.Location = new Point(this.hbar.Right, this.vbar.Bottom);
				this.sizegrip.Visible = true;
				XplatUI.SetZOrder(this.sizegrip.Handle, this.vbar.Handle, false, false);
			}
			else if (this.sizegrip != null)
			{
				this.sizegrip.Visible = false;
			}
			XplatUI.InvalidateNC(base.Handle);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00031800 File Offset: 0x0002FA00
		private void CalcHBar(int left, int right, bool vert_vis)
		{
			this.initializing_scrollbars = true;
			this.hbar.Left = 0;
			this.hbar.Top = base.ClientRectangle.Bottom - this.hbar.Height;
			this.hbar.Width = base.ClientRectangle.Width - (vert_vis ? SystemInformation.VerticalScrollBarWidth : 0);
			this.hbar.LargeChange = 50;
			this.hbar.Minimum = Math.Min(left, 0);
			this.hbar.Maximum = Math.Max(right - base.ClientSize.Width + 51 + (vert_vis ? SystemInformation.VerticalScrollBarWidth : 0), 0);
			this.hbar.Value = 0;
			this.hbar_value = 0;
			this.hbar.ValueChanged += this.HBarValueChanged;
			XplatUI.SetZOrder(this.hbar.Handle, IntPtr.Zero, true, false);
			this.initializing_scrollbars = false;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00031904 File Offset: 0x0002FB04
		private void CalcVBar(int top, int bottom, bool horz_vis)
		{
			this.initializing_scrollbars = true;
			this.vbar.Top = 0;
			this.vbar.Left = base.ClientRectangle.Right - this.vbar.Width;
			this.vbar.Height = base.ClientRectangle.Height - (horz_vis ? SystemInformation.HorizontalScrollBarHeight : 0);
			this.vbar.LargeChange = 50;
			this.vbar.Minimum = Math.Min(top, 0);
			this.vbar.Maximum = Math.Max(bottom - base.ClientSize.Height + 51 + (horz_vis ? SystemInformation.HorizontalScrollBarHeight : 0), 0);
			this.vbar.Value = 0;
			this.vbar_value = 0;
			this.vbar.ValueChanged += this.VBarValueChanged;
			XplatUI.SetZOrder(this.vbar.Handle, IntPtr.Zero, true, false);
			this.initializing_scrollbars = false;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00031A08 File Offset: 0x0002FC08
		private void HBarValueChanged(object sender, EventArgs e)
		{
			if (this.initializing_scrollbars)
			{
				return;
			}
			if (this.hbar.Value == this.hbar_value)
			{
				return;
			}
			this.lock_sizing = true;
			try
			{
				int num = this.hbar_value - this.hbar.Value;
				foreach (object obj in base.Controls)
				{
					((Form)obj).Left += num;
				}
			}
			finally
			{
				this.lock_sizing = false;
			}
			this.hbar_value = this.hbar.Value;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00031AC4 File Offset: 0x0002FCC4
		private void VBarValueChanged(object sender, EventArgs e)
		{
			if (this.initializing_scrollbars)
			{
				return;
			}
			if (this.vbar.Value == this.vbar_value)
			{
				return;
			}
			this.lock_sizing = true;
			try
			{
				int num = this.vbar_value - this.vbar.Value;
				foreach (object obj in base.Controls)
				{
					((Form)obj).Top += num;
				}
			}
			finally
			{
				this.lock_sizing = false;
			}
			this.vbar_value = this.vbar.Value;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00031B80 File Offset: 0x0002FD80
		internal void ArrangeIconicWindows(bool rearrange_all)
		{
			Rectangle empty = Rectangle.Empty;
			this.lock_sizing = true;
			foreach (object obj in base.Controls)
			{
				Form form = (Form)obj;
				if (form.WindowState == FormWindowState.Minimized)
				{
					MdiWindowManager mdiWindowManager = (MdiWindowManager)form.WindowManager;
					if (mdiWindowManager.IconicBounds != Rectangle.Empty && !rearrange_all)
					{
						if (form.Bounds != mdiWindowManager.IconicBounds)
						{
							form.Bounds = mdiWindowManager.IconicBounds;
						}
					}
					else
					{
						bool flag = true;
						empty.Size = mdiWindowManager.IconicSize;
						int num = 0;
						int num2 = base.ClientSize.Height - empty.Height;
						int num3 = num;
						int num4 = num2;
						do
						{
							empty.X = num3;
							empty.Y = num4;
							flag = true;
							foreach (object obj2 in base.Controls)
							{
								Form form2 = (Form)obj2;
								if (form2 != form && form2.window_state == FormWindowState.Minimized && form2.Bounds.IntersectsWith(empty))
								{
									flag = false;
									break;
								}
							}
							if (!flag)
							{
								num3 += empty.Width;
								if (num3 + empty.Width > base.Right)
								{
									num3 = num;
									num4 -= empty.Height;
								}
							}
						}
						while (!flag);
						mdiWindowManager.IconicBounds = empty;
						form.Bounds = mdiWindowManager.IconicBounds;
					}
				}
			}
			this.lock_sizing = false;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00031D5C File Offset: 0x0002FF5C
		internal void ChildFormClosed(Form form)
		{
			FormWindowState windowState = form.WindowState;
			form.Visible = false;
			base.Controls.Remove(form);
			if (base.Controls.Count == 0)
			{
				((MdiWindowManager)form.window_manager).RaiseDeactivate();
			}
			else if (windowState == FormWindowState.Maximized)
			{
				Form form2 = (Form)base.Controls[0];
				form2.WindowState = FormWindowState.Maximized;
				this.ActivateChild(form2);
			}
			if (base.Controls.Count == 0)
			{
				XplatUI.RequestNCRecalc(base.Parent.Handle);
				this.ParentForm.PerformLayout();
				MenuStrip mainMenuStrip = form.MdiParent.MainMenuStrip;
				if (mainMenuStrip != null && mainMenuStrip.IsCurrentlyMerged)
				{
					ToolStripManager.RevertMerge(mainMenuStrip);
				}
			}
			this.SizeScrollBars();
			this.SetParentText(false);
			form.Dispose();
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00031E20 File Offset: 0x00030020
		internal void ActivateNextChild()
		{
			if (base.Controls.Count < 1)
			{
				return;
			}
			if (base.Controls.Count == 1 && base.Controls[0] == this.ActiveMdiChild)
			{
				return;
			}
			Control control = (Form)base.Controls[0];
			Form form = (Form)base.Controls[1];
			this.ActivateChild(form);
			control.SendToBack();
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00031E90 File Offset: 0x00030090
		internal void ActivatePreviousChild()
		{
			if (base.Controls.Count <= 1)
			{
				return;
			}
			Form form = (Form)base.Controls[base.Controls.Count - 1];
			this.ActivateChild(form);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00031ED4 File Offset: 0x000300D4
		internal void ActivateChild(Form form)
		{
			if (base.Controls.Count < 1)
			{
				return;
			}
			if (this.ParentForm.is_changing_visible_state > 0)
			{
				return;
			}
			Form form2 = (Form)base.Controls[0];
			bool flag = this.ParentForm.ActiveControl == form2;
			MdiWindowManager mdiWindowManager = (MdiWindowManager)form.WindowManager;
			if (form2.WindowState == FormWindowState.Maximized && form.WindowState != FormWindowState.Maximized && form.Visible)
			{
				FormWindowState window_state = form.window_state;
				this.SetWindowState(form, window_state, FormWindowState.Maximized, true);
				mdiWindowManager.was_minimized = form.window_state == FormWindowState.Minimized;
				form.window_state = FormWindowState.Maximized;
				this.SetParentText(false);
			}
			form.BringToFront();
			form.SendControlFocus(form);
			this.SetWindowStates(mdiWindowManager);
			if (form2 != form)
			{
				form.has_focus = false;
				if (form2.IsHandleCreated)
				{
					XplatUI.InvalidateNC(form2.Handle);
				}
				if (form.IsHandleCreated)
				{
					XplatUI.InvalidateNC(form.Handle);
				}
				if (flag)
				{
					((MdiWindowManager)form2.window_manager).RaiseDeactivate();
				}
			}
			this.active_child = (Form)base.Controls[0];
			if (this.active_child.Visible)
			{
				bool flag2 = this.ParentForm.ActiveControl != this.active_child;
				this.ParentForm.ActiveControl = this.active_child;
				if (flag2)
				{
					((MdiWindowManager)this.active_child.window_manager).RaiseActivated();
				}
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00032030 File Offset: 0x00030230
		internal bool SetWindowStates(MdiWindowManager wm)
		{
			Form form = wm.form;
			if (this.setting_windowstates)
			{
				return false;
			}
			if (!form.Visible)
			{
				return false;
			}
			bool isActive = wm.IsActive;
			bool flag = false;
			if (!isActive)
			{
				return false;
			}
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			this.setting_windowstates = true;
			foreach (object obj in this.mdi_child_list)
			{
				Form form2 = (Form)obj;
				if (form2 != form && form2.Visible && (form2.WindowState == FormWindowState.Maximized && isActive))
				{
					flag = true;
					if (((MdiWindowManager)form2.window_manager).was_minimized)
					{
						arrayList.Add(form2);
					}
					else
					{
						arrayList2.Add(form2);
					}
				}
			}
			if (flag && form.WindowState != FormWindowState.Maximized)
			{
				wm.was_minimized = form.window_state == FormWindowState.Minimized;
				form.WindowState = FormWindowState.Maximized;
			}
			foreach (object obj2 in arrayList)
			{
				((Form)obj2).WindowState = FormWindowState.Minimized;
			}
			foreach (object obj3 in arrayList2)
			{
				((Form)obj3).WindowState = FormWindowState.Normal;
			}
			this.SetParentText(false);
			XplatUI.RequestNCRecalc(this.ParentForm.Handle);
			XplatUI.RequestNCRecalc(base.Handle);
			this.SizeScrollBars();
			this.setting_windowstates = false;
			if (form.MdiParent.MainMenuStrip != null)
			{
				form.MdiParent.MainMenuStrip.RefreshMdiItems();
			}
			MenuStrip mainMenuStrip = form.MdiParent.MainMenuStrip;
			if (mainMenuStrip != null)
			{
				if (mainMenuStrip.IsCurrentlyMerged)
				{
					ToolStripManager.RevertMerge(mainMenuStrip);
				}
				MenuStrip menuStrip = this.LookForChildMenu(form);
				if (form.WindowState != FormWindowState.Maximized)
				{
					this.RemoveControlMenuItems(wm);
				}
				if (form.WindowState == FormWindowState.Maximized)
				{
					bool flag2 = false;
					foreach (object obj4 in mainMenuStrip.Items)
					{
						ToolStripItem toolStripItem = (ToolStripItem)obj4;
						if (toolStripItem is MdiControlStrip.SystemMenuItem)
						{
							(toolStripItem as MdiControlStrip.SystemMenuItem).MdiForm = form;
							flag2 = true;
						}
						else if (toolStripItem is MdiControlStrip.ControlBoxMenuItem)
						{
							(toolStripItem as MdiControlStrip.ControlBoxMenuItem).MdiForm = form;
							flag2 = true;
						}
					}
					if (!flag2)
					{
						mainMenuStrip.SuspendLayout();
						mainMenuStrip.Items.Insert(0, new MdiControlStrip.SystemMenuItem(form));
						mainMenuStrip.Items.Add(new MdiControlStrip.ControlBoxMenuItem(form, MdiControlStrip.ControlBoxType.Close));
						mainMenuStrip.Items.Add(new MdiControlStrip.ControlBoxMenuItem(form, MdiControlStrip.ControlBoxType.Max));
						mainMenuStrip.Items.Add(new MdiControlStrip.ControlBoxMenuItem(form, MdiControlStrip.ControlBoxType.Min));
						mainMenuStrip.ResumeLayout();
					}
				}
				if (menuStrip != null)
				{
					ToolStripManager.Merge(menuStrip, mainMenuStrip);
				}
			}
			return flag;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00032348 File Offset: 0x00030548
		private MenuStrip LookForChildMenu(Control parent)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control is MenuStrip)
				{
					return (MenuStrip)control;
				}
				if (control is ToolStripContainer || control is ToolStripPanel)
				{
					MenuStrip menuStrip = this.LookForChildMenu(control);
					if (menuStrip != null)
					{
						return menuStrip;
					}
				}
			}
			return null;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x000323D0 File Offset: 0x000305D0
		internal void RemoveControlMenuItems(MdiWindowManager wm)
		{
			Form form = wm.form;
			MenuStrip mainMenuStrip = form.MdiParent.MainMenuStrip;
			if (mainMenuStrip != null)
			{
				mainMenuStrip.SuspendLayout();
				for (int i = mainMenuStrip.Items.Count - 1; i >= 0; i--)
				{
					if (mainMenuStrip.Items[i] is MdiControlStrip.SystemMenuItem)
					{
						if ((mainMenuStrip.Items[i] as MdiControlStrip.SystemMenuItem).MdiForm == form)
						{
							mainMenuStrip.Items.RemoveAt(i);
						}
					}
					else if (mainMenuStrip.Items[i] is MdiControlStrip.ControlBoxMenuItem && (mainMenuStrip.Items[i] as MdiControlStrip.ControlBoxMenuItem).MdiForm == form)
					{
						mainMenuStrip.Items.RemoveAt(i);
					}
				}
				mainMenuStrip.ResumeLayout();
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0003248C File Offset: 0x0003068C
		internal void SetWindowState(Form form, FormWindowState old_window_state, FormWindowState new_window_state, bool is_activating_child)
		{
			MdiWindowManager mdiWindowManager = (MdiWindowManager)form.window_manager;
			if (!is_activating_child && new_window_state == FormWindowState.Maximized && !mdiWindowManager.IsActive)
			{
				this.ActivateChild(form);
				return;
			}
			if (old_window_state == FormWindowState.Normal)
			{
				mdiWindowManager.NormalBounds = form.Bounds;
			}
			if (this.SetWindowStates(mdiWindowManager))
			{
				return;
			}
			if (old_window_state == new_window_state)
			{
				return;
			}
			bool flag = old_window_state == FormWindowState.Maximized || new_window_state == FormWindowState.Maximized;
			switch (new_window_state)
			{
			case FormWindowState.Normal:
				form.Bounds = mdiWindowManager.NormalBounds;
				break;
			case FormWindowState.Minimized:
				this.ArrangeIconicWindows(false);
				break;
			case FormWindowState.Maximized:
				form.Bounds = mdiWindowManager.MaximizedBounds;
				break;
			}
			mdiWindowManager.UpdateWindowDecorations(new_window_state);
			form.ResetCursor();
			if (flag)
			{
				base.Parent.PerformLayout();
			}
			XplatUI.RequestNCRecalc(base.Parent.Handle);
			XplatUI.RequestNCRecalc(form.Handle);
			if (!this.setting_windowstates)
			{
				this.SizeScrollBars();
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00032564 File Offset: 0x00030764
		internal Form ActiveMdiChild
		{
			get
			{
				if (this.ParentForm != null && !this.ParentForm.Visible)
				{
					return null;
				}
				if (base.Controls.Count < 1)
				{
					return null;
				}
				if (!this.ParentForm.IsHandleCreated)
				{
					return null;
				}
				if (!this.ParentForm.has_been_visible)
				{
					return null;
				}
				if (!this.ParentForm.Visible)
				{
					return this.active_child;
				}
				this.active_child = null;
				for (int i = 0; i < base.Controls.Count; i++)
				{
					if (base.Controls[i].Visible)
					{
						this.active_child = (Form)base.Controls[i];
						break;
					}
				}
				return this.active_child;
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0003261C File Offset: 0x0003081C
		internal void ActivateActiveMdiChild()
		{
			if (this.ParentForm.is_changing_visible_state > 0)
			{
				return;
			}
			for (int i = 0; i < base.Controls.Count; i++)
			{
				if (base.Controls[i].Visible)
				{
					this.ActivateChild((Form)base.Controls[i]);
					return;
				}
			}
		}

		// Token: 0x0400077D RID: 1917
		private ImplicitHScrollBar hbar;

		// Token: 0x0400077E RID: 1918
		private ImplicitVScrollBar vbar;

		// Token: 0x0400077F RID: 1919
		private SizeGrip sizegrip;

		// Token: 0x04000780 RID: 1920
		private int hbar_value;

		// Token: 0x04000781 RID: 1921
		private int vbar_value;

		// Token: 0x04000782 RID: 1922
		private bool lock_sizing;

		// Token: 0x04000783 RID: 1923
		private bool initializing_scrollbars;

		// Token: 0x04000784 RID: 1924
		private bool setting_windowstates;

		// Token: 0x04000785 RID: 1925
		internal ArrayList mdi_child_list;

		// Token: 0x04000786 RID: 1926
		private string form_text;

		// Token: 0x04000787 RID: 1927
		private bool setting_form_text;

		// Token: 0x04000788 RID: 1928
		private Form active_child;
	}
}
