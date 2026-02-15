using System;
using System.Windows.Forms;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000070 RID: 112
	internal class WindowHandleWrapper : IWin32Window
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0000A34F File Offset: 0x0000854F
		public WindowHandleWrapper(IntPtr handle)
		{
			this._handle = handle;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000A360 File Offset: 0x00008560
		public IntPtr Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x04000244 RID: 580
		private IntPtr _handle;
	}
}
