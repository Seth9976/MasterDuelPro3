using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A7 RID: 423
	public class DropdownMenu
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0003B4D8 File Offset: 0x000396D8
		internal int Count
		{
			get
			{
				return this.m_MenuItems.Count;
			}
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0003B4E8 File Offset: 0x000396E8
		public List<DropdownMenuItem> MenuItems()
		{
			return this.m_MenuItems;
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0003B500 File Offset: 0x00039700
		public void AppendAction(string actionName, Action<DropdownMenuAction> action, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			DropdownMenuAction menuAction = new DropdownMenuAction(actionName, action, actionStatusCallback, userData);
			this.m_MenuItems.Add(menuAction);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0003B528 File Offset: 0x00039728
		public void AppendAction(string actionName, Action<DropdownMenuAction> action, DropdownMenuAction.Status status = DropdownMenuAction.Status.Normal)
		{
			bool flag = status == DropdownMenuAction.Status.Normal;
			if (flag)
			{
				this.AppendAction(actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysEnabled), null);
			}
			else
			{
				bool flag2 = status == DropdownMenuAction.Status.Disabled;
				if (flag2)
				{
					this.AppendAction(actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysDisabled), null);
				}
				else
				{
					this.AppendAction(actionName, action, (DropdownMenuAction e) => status, null);
				}
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0003B5AC File Offset: 0x000397AC
		public void AppendSeparator(string subMenuPath = null)
		{
			if (subMenuPath == null)
			{
				subMenuPath = string.Empty;
			}
			bool isFirstItemOfMenu = this.m_MenuItems.FindIndex(delegate(DropdownMenuItem item)
			{
				DropdownMenuAction action = item as DropdownMenuAction;
				return action != null && action.name.StartsWith(subMenuPath);
			}) == -1;
			bool flag;
			if (this.m_MenuItems.Count > 0)
			{
				List<DropdownMenuItem> menuItems = this.m_MenuItems;
				if (menuItems[menuItems.Count - 1] is DropdownMenuSeparator)
				{
					List<DropdownMenuItem> menuItems2 = this.m_MenuItems;
					if (((DropdownMenuSeparator)menuItems2[menuItems2.Count - 1]).subMenuPath == subMenuPath)
					{
						goto IL_0094;
					}
				}
				flag = !isFirstItemOfMenu;
				goto IL_0095;
			}
			IL_0094:
			flag = false;
			IL_0095:
			bool flag2 = flag;
			if (flag2)
			{
				DropdownMenuSeparator separator = new DropdownMenuSeparator(subMenuPath);
				this.m_MenuItems.Add(separator);
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0003B670 File Offset: 0x00039870
		public void InsertSeparator(string subMenuPath, int atIndex)
		{
			bool flag = atIndex > 0 && atIndex <= this.m_MenuItems.Count && !(this.m_MenuItems[atIndex - 1] is DropdownMenuSeparator);
			if (flag)
			{
				DropdownMenuSeparator separator = new DropdownMenuSeparator(subMenuPath ?? string.Empty);
				this.m_MenuItems.Insert(atIndex, separator);
			}
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0003B6D0 File Offset: 0x000398D0
		public void PrepareForDisplay(EventBase e)
		{
			this.m_DropdownMenuEventInfo = ((e != null) ? new DropdownMenuEventInfo(e) : null);
			bool flag = this.m_MenuItems.Count == 0;
			if (!flag)
			{
				foreach (DropdownMenuItem item in this.m_MenuItems)
				{
					DropdownMenuAction action = item as DropdownMenuAction;
					bool flag2 = action != null;
					if (flag2)
					{
						action.UpdateActionStatus(this.m_DropdownMenuEventInfo);
					}
				}
				bool flag3 = this.m_MenuItems[this.m_MenuItems.Count - 1] is DropdownMenuSeparator;
				if (flag3)
				{
					this.m_MenuItems.RemoveAt(this.m_MenuItems.Count - 1);
				}
			}
		}

		// Token: 0x040007C8 RID: 1992
		private List<DropdownMenuItem> m_MenuItems = new List<DropdownMenuItem>();

		// Token: 0x040007C9 RID: 1993
		private DropdownMenuEventInfo m_DropdownMenuEventInfo;
	}
}
