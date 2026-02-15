using System;
using Unity.IL2CPP.CompilerServices;

namespace UnityEngineInternal
{
	// Token: 0x02000005 RID: 5
	[Il2CppEagerStaticClassConstruction]
	public struct MathfInternal
	{
		// Token: 0x04000001 RID: 1
		public static volatile float FloatMinNormal = 1.1754944E-38f;

		// Token: 0x04000002 RID: 2
		public static volatile float FloatMinDenormal = float.Epsilon;

		// Token: 0x04000003 RID: 3
		public static bool IsFlushToZeroEnabled = MathfInternal.FloatMinDenormal == 0f;
	}
}
