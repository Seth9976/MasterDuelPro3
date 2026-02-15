using System;

namespace UnityEngine
{
	// Token: 0x02000136 RID: 310
	public static class HashUtilities
	{
		// Token: 0x06000D25 RID: 3365 RVA: 0x00018C64 File Offset: 0x00016E64
		public unsafe static void AppendHash(ref Hash128 inHash, ref Hash128 outHash)
		{
			fixed (Hash128* ptr = &outHash)
			{
				Hash128* outH = ptr;
				fixed (Hash128* ptr2 = &inHash)
				{
					Hash128* message = ptr2;
					HashUnsafeUtilities.ComputeHash128((void*)message, (ulong)((long)sizeof(Hash128)), outH);
				}
			}
		}
	}
}
