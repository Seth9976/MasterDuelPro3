using System;
using System.Runtime.ExceptionServices;

namespace Mono.Net.Security
{
	// Token: 0x02000061 RID: 97
	internal class AsyncProtocolResult
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00004E9C File Offset: 0x0000309C
		public int UserResult { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004EA4 File Offset: 0x000030A4
		public ExceptionDispatchInfo Error { get; }

		// Token: 0x0600011F RID: 287 RVA: 0x00004EAC File Offset: 0x000030AC
		public AsyncProtocolResult(int result)
		{
			this.UserResult = result;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004EBB File Offset: 0x000030BB
		public AsyncProtocolResult(ExceptionDispatchInfo error)
		{
			this.Error = error;
		}
	}
}
