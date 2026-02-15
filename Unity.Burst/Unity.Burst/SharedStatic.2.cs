using System;
using System.Diagnostics;
using Unity.Burst.LowLevel;
using UnityEngine;

namespace Unity.Burst
{
	// Token: 0x0200002D RID: 45
	internal static class SharedStatic
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00005A4A File Offset: 0x00003C4A
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckSizeOf(uint sizeOf)
		{
			if (sizeOf == 0U)
			{
				throw new ArgumentException("sizeOf must be > 0", "sizeOf");
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005A5F File Offset: 0x00003C5F
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private unsafe static void CheckResult(void* result)
		{
			if (result == null)
			{
				throw new InvalidOperationException("Unable to create a SharedStatic for this key. This is most likely due to the size of the struct inside of the SharedStatic having changed or the same key being reused for differently sized values. To fix this the editor needs to be restarted.");
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005A74 File Offset: 0x00003C74
		[SharedStatic.PreserveAttribute]
		public unsafe static void* GetOrCreateSharedStaticInternal(long getHashCode64, long getSubHashCode64, uint sizeOf, uint alignment)
		{
			Hash128 hash128 = new Hash128((ulong)getHashCode64, (ulong)getSubHashCode64);
			return BurstCompilerService.GetOrCreateSharedMemory(ref hash128, sizeOf, alignment);
		}

		// Token: 0x0200002E RID: 46
		internal class PreserveAttribute : Attribute
		{
		}
	}
}
