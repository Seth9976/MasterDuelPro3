using System;
using System.Windows.Forms;

namespace SFB
{
	// Token: 0x02000072 RID: 114
	public class WindowWrapper : IWin32Window
	{
		// Token: 0x0600021E RID: 542 RVA: 0x00006C53 File Offset: 0x00004E53
		public WindowWrapper(IntPtr handle)
		{
			this._hwnd = handle;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00006C62 File Offset: 0x00004E62
		public IntPtr Handle
		{
			get
			{
				return this._hwnd;
			}
		}

		// Token: 0x040002BF RID: 703
		private IntPtr _hwnd;
	}
}
