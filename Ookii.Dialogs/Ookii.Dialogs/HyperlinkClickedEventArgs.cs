using System;

namespace Ookii.Dialogs
{
	// Token: 0x0200000F RID: 15
	public class HyperlinkClickedEventArgs : EventArgs
	{
		// Token: 0x0600006A RID: 106 RVA: 0x000040A4 File Offset: 0x000022A4
		public HyperlinkClickedEventArgs(string href)
		{
			this._href = href;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000040B8 File Offset: 0x000022B8
		public string Href
		{
			get
			{
				return this._href;
			}
		}

		// Token: 0x0400002F RID: 47
		private string _href;
	}
}
