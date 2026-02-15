using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x02000191 RID: 401
	internal interface IEventMerger
	{
		// Token: 0x06000FBA RID: 4026
		bool MergeForward(InputEventPtr currentEventPtr, InputEventPtr nextEventPtr);
	}
}
