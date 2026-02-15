using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000017 RID: 23
	public interface IPerFrameHistoryAccessTracker
	{
		// Token: 0x0600009D RID: 157
		void RequestAccess<Type>() where Type : ContextItem;
	}
}
