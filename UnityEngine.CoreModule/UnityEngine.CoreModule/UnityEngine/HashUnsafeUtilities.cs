using System;

namespace UnityEngine
{
	// Token: 0x02000137 RID: 311
	public static class HashUnsafeUtilities
	{
		// Token: 0x06000D26 RID: 3366 RVA: 0x00018C95 File Offset: 0x00016E95
		public unsafe static void ComputeHash128(void* data, ulong dataSize, ulong* hash1, ulong* hash2)
		{
			SpookyHash.Hash(data, dataSize, hash1, hash2);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00018CA4 File Offset: 0x00016EA4
		public unsafe static void ComputeHash128(void* data, ulong dataSize, Hash128* hash)
		{
			ulong u61_0 = hash->u64_0;
			ulong u61_ = hash->u64_1;
			HashUnsafeUtilities.ComputeHash128(data, dataSize, &u61_0, &u61_);
			*hash = new Hash128(u61_0, u61_);
		}
	}
}
