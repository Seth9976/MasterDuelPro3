using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000019 RID: 25
	public interface ICameraHistoryReadAccess
	{
		// Token: 0x060000A1 RID: 161
		Type GetHistoryForRead<Type>() where Type : ContextItem;

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000A2 RID: 162
		// (remove) Token: 0x060000A3 RID: 163
		event ICameraHistoryReadAccess.HistoryRequestDelegate OnGatherHistoryRequests;

		// Token: 0x0200001A RID: 26
		// (Invoke) Token: 0x060000A5 RID: 165
		public delegate void HistoryRequestDelegate(IPerFrameHistoryAccessTracker historyAccess);
	}
}
