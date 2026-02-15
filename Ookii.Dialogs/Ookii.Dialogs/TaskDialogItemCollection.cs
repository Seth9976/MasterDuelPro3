using System;
using System.Collections.ObjectModel;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x02000049 RID: 73
	public class TaskDialogItemCollection<T> : Collection<T> where T : TaskDialogItem
	{
		// Token: 0x060001BA RID: 442 RVA: 0x0000811C File Offset: 0x0000631C
		internal TaskDialogItemCollection(TaskDialog owner)
		{
			this._owner = owner;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008130 File Offset: 0x00006330
		protected override void ClearItems()
		{
			foreach (T t in this)
			{
				t.Owner = null;
			}
			base.ClearItems();
			this._owner.UpdateDialog();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008198 File Offset: 0x00006398
		protected override void InsertItem(int index, T item)
		{
			bool flag = item == null;
			if (flag)
			{
				throw new ArgumentNullException("item");
			}
			bool flag2 = item.Owner != null;
			if (flag2)
			{
				throw new ArgumentException(Resources.TaskDialogItemHasOwnerError);
			}
			item.Owner = this._owner;
			try
			{
				item.CheckDuplicate(null);
			}
			catch (InvalidOperationException)
			{
				item.Owner = null;
				throw;
			}
			base.InsertItem(index, item);
			this._owner.UpdateDialog();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008234 File Offset: 0x00006434
		protected override void RemoveItem(int index)
		{
			base[index].Owner = null;
			base.RemoveItem(index);
			this._owner.UpdateDialog();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008260 File Offset: 0x00006460
		protected override void SetItem(int index, T item)
		{
			bool flag = item == null;
			if (flag)
			{
				throw new ArgumentNullException("item");
			}
			bool flag2 = base[index] != item;
			if (flag2)
			{
				bool flag3 = item.Owner != null;
				if (flag3)
				{
					throw new ArgumentException(Resources.TaskDialogItemHasOwnerError);
				}
				item.Owner = this._owner;
				try
				{
					item.CheckDuplicate(base[index]);
				}
				catch (InvalidOperationException)
				{
					item.Owner = null;
					throw;
				}
				base[index].Owner = null;
				base.SetItem(index, item);
				this._owner.UpdateDialog();
			}
		}

		// Token: 0x040001F3 RID: 499
		private TaskDialog _owner;
	}
}
