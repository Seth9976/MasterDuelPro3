using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200028E RID: 654
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class XIMCallback
	{
		// Token: 0x06001744 RID: 5956 RVA: 0x00074A0C File Offset: 0x00072C0C
		public XIMCallback(IntPtr clientData, XIMProc proc)
		{
			this.client_data = clientData;
			this.gch = GCHandle.Alloc(proc);
			this.callback = proc;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00074A30 File Offset: 0x00072C30
		~XIMCallback()
		{
			this.gch.Free();
		}

		// Token: 0x0400120C RID: 4620
		public IntPtr client_data;

		// Token: 0x0400120D RID: 4621
		public XIMProc callback;

		// Token: 0x0400120E RID: 4622
		[NonSerialized]
		private GCHandle gch;
	}
}
