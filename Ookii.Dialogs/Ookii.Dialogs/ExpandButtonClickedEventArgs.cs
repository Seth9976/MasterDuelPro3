using System;

namespace Ookii.Dialogs
{
	// Token: 0x0200000C RID: 12
	public class ExpandButtonClickedEventArgs : EventArgs
	{
		// Token: 0x0600004F RID: 79 RVA: 0x000034FC File Offset: 0x000016FC
		public ExpandButtonClickedEventArgs(bool expanded)
		{
			this._expanded = expanded;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003510 File Offset: 0x00001710
		public bool Expanded
		{
			get
			{
				return this._expanded;
			}
		}

		// Token: 0x0400002A RID: 42
		private bool _expanded;
	}
}
