using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.LowLevel
{
	// Token: 0x02000255 RID: 597
	[MovedFrom("UnityEngine.Experimental.LowLevel")]
	[NativeType(Header = "Runtime/Misc/PlayerLoop.h")]
	[RequiredByNativeCode]
	internal struct PlayerLoopSystemInternal
	{
		// Token: 0x040007C6 RID: 1990
		public Type type;

		// Token: 0x040007C7 RID: 1991
		public PlayerLoopSystem.UpdateFunction updateDelegate;

		// Token: 0x040007C8 RID: 1992
		public IntPtr updateFunction;

		// Token: 0x040007C9 RID: 1993
		public IntPtr loopConditionFunction;

		// Token: 0x040007CA RID: 1994
		public int numSubSystems;
	}
}
