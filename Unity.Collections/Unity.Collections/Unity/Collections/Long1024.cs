using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x02000043 RID: 67
	internal struct Long1024 : IIndexable<long>
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005237 File Offset: 0x00003437
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00002C47 File Offset: 0x00000E47
		public int Length
		{
			get
			{
				return 1024;
			}
			set
			{
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005240 File Offset: 0x00003440
		public unsafe ref long ElementAt(int index)
		{
			fixed (Long512* ptr = &this.f0)
			{
				return UnsafeUtility.AsRef<long>((void*)((byte*)ptr + (IntPtr)index * 8));
			}
		}

		// Token: 0x040000A1 RID: 161
		internal Long512 f0;

		// Token: 0x040000A2 RID: 162
		internal Long512 f1;
	}
}
