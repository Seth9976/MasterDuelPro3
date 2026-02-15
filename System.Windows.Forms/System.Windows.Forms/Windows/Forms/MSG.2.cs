using System;

namespace System.Windows.Forms
{
	// Token: 0x020002A1 RID: 673
	internal struct MSG
	{
		// Token: 0x06001913 RID: 6419 RVA: 0x00079060 File Offset: 0x00077260
		public override string ToString()
		{
			return string.Format("msg=0x{0:x} ({1}) hwnd=0x{2:x} wparam=0x{3:x} lparam=0x{4:x} pt={5}", new object[]
			{
				(int)this.message,
				this.message.ToString(),
				this.hwnd.ToInt32(),
				this.wParam.ToInt32(),
				this.lParam.ToInt32(),
				this.pt
			});
		}

		// Token: 0x0400135F RID: 4959
		internal IntPtr hwnd;

		// Token: 0x04001360 RID: 4960
		internal Msg message;

		// Token: 0x04001361 RID: 4961
		internal IntPtr wParam;

		// Token: 0x04001362 RID: 4962
		internal IntPtr lParam;

		// Token: 0x04001363 RID: 4963
		internal uint time;

		// Token: 0x04001364 RID: 4964
		internal POINT pt;

		// Token: 0x04001365 RID: 4965
		internal object refobject;
	}
}
