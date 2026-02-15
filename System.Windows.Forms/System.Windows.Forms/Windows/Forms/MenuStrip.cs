using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides a menu system for a form.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000139 RID: 313
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public class MenuStrip : ToolStrip
	{
		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuStrip.MenuActivate" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000CA7 RID: 3239 RVA: 0x0003729C File Offset: 0x0003549C
		protected virtual void OnMenuActivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuStrip.MenuActivateEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x000372CA File Offset: 0x000354CA
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x000372D2 File Offset: 0x000354D2
		internal bool MenuDroppedDown
		{
			get
			{
				return this.menu_selected;
			}
			set
			{
				this.menu_selected = value;
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x000372DB File Offset: 0x000354DB
		internal void FireMenuActivate()
		{
			ToolStripManager.AppClicked += this.ToolStripMenuTracker_AppClicked;
			ToolStripManager.AppFocusChange += this.ToolStripMenuTracker_AppFocusChange;
			this.OnMenuActivate(EventArgs.Empty);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0003730A File Offset: 0x0003550A
		private void ToolStripMenuTracker_AppFocusChange(object sender, EventArgs e)
		{
			this.GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.AppFocusChange);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00037318 File Offset: 0x00035518
		private void ToolStripMenuTracker_AppClicked(object sender, EventArgs e)
		{
			this.GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.AppClicked);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00037328 File Offset: 0x00035528
		internal void RefreshMdiItems()
		{
			if (this.mdi_window_list_item == null)
			{
				return;
			}
			Form form = base.FindForm();
			if (form == null || form.MainMenuStrip != this)
			{
				return;
			}
			MdiClient mdiContainer = form.MdiContainer;
			if (mdiContainer == null)
			{
				return;
			}
			ToolStripItem[] array = new ToolStripItem[this.mdi_window_list_item.DropDownItems.Count];
			this.mdi_window_list_item.DropDownItems.CopyTo(array, 0);
			foreach (ToolStripItem toolStripItem in array)
			{
				if (toolStripItem is ToolStripMenuItem && (toolStripItem as ToolStripMenuItem).IsMdiWindowListEntry && (!mdiContainer.mdi_child_list.Contains((toolStripItem as ToolStripMenuItem).MdiClientForm) || !(toolStripItem as ToolStripMenuItem).MdiClientForm.Visible))
				{
					this.mdi_window_list_item.DropDownItems.Remove(toolStripItem);
				}
			}
			for (int j = 0; j < mdiContainer.mdi_child_list.Count; j++)
			{
				Form form2 = (Form)mdiContainer.mdi_child_list[j];
				if (form2.Visible)
				{
					ToolStripMenuItem toolStripMenuItem;
					if ((toolStripMenuItem = this.FindMdiMenuItemOfForm(form2)) == null)
					{
						if (this.CountMdiMenuItems() == 0 && this.mdi_window_list_item.DropDownItems.Count > 0 && !(this.mdi_window_list_item.DropDownItems[this.mdi_window_list_item.DropDownItems.Count - 1] is ToolStripSeparator))
						{
							this.mdi_window_list_item.DropDownItems.Add(new ToolStripSeparator());
						}
						toolStripMenuItem = new ToolStripMenuItem();
						toolStripMenuItem.MdiClientForm = form2;
						this.mdi_window_list_item.DropDownItems.Add(toolStripMenuItem);
					}
					toolStripMenuItem.Text = string.Format("&{0} {1}", j + 1, form2.Text);
					toolStripMenuItem.Checked = form.ActiveMdiChild == form2;
				}
			}
			if (this.NeedToReorderMdi())
			{
				this.ReorderMdiMenu();
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00037500 File Offset: 0x00035700
		private ToolStripMenuItem FindMdiMenuItemOfForm(Form f)
		{
			foreach (object obj in this.mdi_window_list_item.DropDownItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem is ToolStripMenuItem && (toolStripItem as ToolStripMenuItem).MdiClientForm == f)
				{
					return (ToolStripMenuItem)toolStripItem;
				}
			}
			return null;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0003757C File Offset: 0x0003577C
		private int CountMdiMenuItems()
		{
			int num = 0;
			foreach (object obj in this.mdi_window_list_item.DropDownItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem is ToolStripMenuItem && (toolStripItem as ToolStripMenuItem).IsMdiWindowListEntry)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x000375F0 File Offset: 0x000357F0
		private bool NeedToReorderMdi()
		{
			bool flag = false;
			foreach (object obj in this.mdi_window_list_item.DropDownItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem is ToolStripMenuItem)
				{
					if (!(toolStripItem as ToolStripMenuItem).IsMdiWindowListEntry)
					{
						if (flag)
						{
							return true;
						}
					}
					else
					{
						flag = true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00037670 File Offset: 0x00035870
		private void ReorderMdiMenu()
		{
			ToolStripItem[] array = new ToolStripItem[this.mdi_window_list_item.DropDownItems.Count];
			this.mdi_window_list_item.DropDownItems.CopyTo(array, 0);
			this.mdi_window_list_item.DropDownItems.Clear();
			foreach (ToolStripItem toolStripItem in array)
			{
				if (toolStripItem is ToolStripSeparator || !(toolStripItem as ToolStripMenuItem).IsMdiWindowListEntry)
				{
					this.mdi_window_list_item.DropDownItems.Add(toolStripItem);
				}
			}
			int count = this.mdi_window_list_item.DropDownItems.Count;
			if (count > 0 && !(this.mdi_window_list_item.DropDownItems[count - 1] is ToolStripSeparator))
			{
				this.mdi_window_list_item.DropDownItems.Add(new ToolStripSeparator());
			}
			foreach (ToolStripItem toolStripItem2 in array)
			{
				if (toolStripItem2 is ToolStripMenuItem && (toolStripItem2 as ToolStripMenuItem).IsMdiWindowListEntry)
				{
					this.mdi_window_list_item.DropDownItems.Add(toolStripItem2);
				}
			}
		}

		// Token: 0x040007F4 RID: 2036
		private ToolStripMenuItem mdi_window_list_item;

		// Token: 0x040007F5 RID: 2037
		private static object MenuActivateEvent = new object();

		// Token: 0x040007F6 RID: 2038
		private static object MenuDeactivateEvent = new object();
	}
}
