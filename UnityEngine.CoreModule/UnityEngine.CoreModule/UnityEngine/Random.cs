using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000169 RID: 361
	[NativeHeader("Runtime/Export/Random/Random.bindings.h")]
	public static class Random
	{
		// Token: 0x06000F42 RID: 3906
		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		[NativeMethod("SetSeed")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InitState(int seed);

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x000203E0 File Offset: 0x0001E5E0
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x000203F8 File Offset: 0x0001E5F8
		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		public static Random.State state
		{
			get
			{
				Random.State state;
				Random.get_state_Injected(out state);
				return state;
			}
			set
			{
				Random.set_state_Injected(ref value);
			}
		}

		// Token: 0x06000F45 RID: 3909
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Range(float minInclusive, float maxInclusive);

		// Token: 0x06000F46 RID: 3910 RVA: 0x0002040C File Offset: 0x0001E60C
		public static int Range(int minInclusive, int maxExclusive)
		{
			return Random.RandomRangeInt(minInclusive, maxExclusive);
		}

		// Token: 0x06000F47 RID: 3911
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RandomRangeInt(int minInclusive, int maxExclusive);

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000F48 RID: 3912
		public static extern float value
		{
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000F49 RID: 3913
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_state_Injected(out Random.State ret);

		// Token: 0x06000F4A RID: 3914
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_state_Injected([In] ref Random.State value);

		// Token: 0x0200016A RID: 362
		[Serializable]
		public struct State
		{
			// Token: 0x04000607 RID: 1543
			[SerializeField]
			private int s0;

			// Token: 0x04000608 RID: 1544
			[SerializeField]
			private int s1;

			// Token: 0x04000609 RID: 1545
			[SerializeField]
			private int s2;

			// Token: 0x0400060A RID: 1546
			[SerializeField]
			private int s3;
		}
	}
}
