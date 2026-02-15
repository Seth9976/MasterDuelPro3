using System;
using Unity.Burst;
using UnityEngine;

// Token: 0x020000BB RID: 187
internal static class $BurstDirectCallInitializer
{
	// Token: 0x06000404 RID: 1028 RVA: 0x0001DF54 File Offset: 0x0001C154
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	private static void Initialize()
	{
		BurstCompilerOptions options = BurstCompiler.Options;
	}
}
