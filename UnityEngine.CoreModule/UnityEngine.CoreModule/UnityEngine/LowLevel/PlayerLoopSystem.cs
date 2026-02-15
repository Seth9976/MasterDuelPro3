using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.LowLevel
{
	// Token: 0x02000256 RID: 598
	[MovedFrom("UnityEngine.Experimental.LowLevel")]
	public struct PlayerLoopSystem
	{
		// Token: 0x060014D6 RID: 5334 RVA: 0x0002BF9A File Offset: 0x0002A19A
		public override string ToString()
		{
			return this.type.Name;
		}

		// Token: 0x040007CB RID: 1995
		public Type type;

		// Token: 0x040007CC RID: 1996
		public PlayerLoopSystem[] subSystemList;

		// Token: 0x040007CD RID: 1997
		public PlayerLoopSystem.UpdateFunction updateDelegate;

		// Token: 0x040007CE RID: 1998
		public IntPtr updateFunction;

		// Token: 0x040007CF RID: 1999
		public IntPtr loopConditionFunction;

		// Token: 0x02000257 RID: 599
		// (Invoke) Token: 0x060014D8 RID: 5336
		public delegate void UpdateFunction();
	}
}
