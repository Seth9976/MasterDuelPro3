using System;
using Unity.Burst;
using UnityEngine;

// Token: 0x02000158 RID: 344
internal static class $BurstDirectCallInitializer
{
	// Token: 0x06000DCF RID: 3535 RVA: 0x0002AB30 File Offset: 0x00028D30
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	private static void Initialize()
	{
		BurstCompilerOptions options = BurstCompiler.Options;
	}
}
