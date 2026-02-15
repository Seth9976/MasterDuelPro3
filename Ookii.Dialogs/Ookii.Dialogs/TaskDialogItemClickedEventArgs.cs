using System;
using System.ComponentModel;

namespace Ookii.Dialogs
{
	// Token: 0x02000048 RID: 72
	public class TaskDialogItemClickedEventArgs : CancelEventArgs
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x000080F2 File Offset: 0x000062F2
		public TaskDialogItemClickedEventArgs(TaskDialogItem item)
		{
			this._item = item;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00008104 File Offset: 0x00006304
		public TaskDialogItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x040001F2 RID: 498
		private readonly TaskDialogItem _item;
	}
}
