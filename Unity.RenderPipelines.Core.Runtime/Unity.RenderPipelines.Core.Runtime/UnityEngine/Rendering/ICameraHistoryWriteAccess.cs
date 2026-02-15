using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000018 RID: 24
	public interface ICameraHistoryWriteAccess
	{
		// Token: 0x0600009E RID: 158
		bool IsAccessRequested<Type>() where Type : ContextItem;

		// Token: 0x0600009F RID: 159
		Type GetHistoryForWrite<Type>() where Type : ContextItem, new();

		// Token: 0x060000A0 RID: 160
		bool IsWritten<Type>() where Type : ContextItem;
	}
}
