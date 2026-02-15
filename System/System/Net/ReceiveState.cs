using System;

namespace System.Net
{
	// Token: 0x0200038D RID: 909
	internal class ReceiveState
	{
		// Token: 0x060016B1 RID: 5809 RVA: 0x000604E8 File Offset: 0x0005E6E8
		internal ReceiveState(CommandStream connection)
		{
			this.Connection = connection;
			this.Resp = new ResponseDescription();
			this.Buffer = new byte[1024];
			this.ValidThrough = 0;
		}

		// Token: 0x04000DCE RID: 3534
		internal ResponseDescription Resp;

		// Token: 0x04000DCF RID: 3535
		internal int ValidThrough;

		// Token: 0x04000DD0 RID: 3536
		internal byte[] Buffer;

		// Token: 0x04000DD1 RID: 3537
		internal CommandStream Connection;
	}
}
