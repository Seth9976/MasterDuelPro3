using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006C RID: 108
	internal struct SmallIntegerArray : IDisposable
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000D114 File Offset: 0x0000B314
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000D11C File Offset: 0x0000B31C
		public bool Valid { readonly get; private set; }

		// Token: 0x0600021B RID: 539 RVA: 0x0000D128 File Offset: 0x0000B328
		public SmallIntegerArray(int length, Allocator allocator)
		{
			this.m_FixedArray = default(FixedList32Bytes<int>);
			this.m_List = default(UnsafeList<int>);
			this.Length = length;
			this.Valid = true;
			if (this.Length <= this.m_FixedArray.Capacity)
			{
				this.m_FixedArray = default(FixedList32Bytes<int>);
				this.m_FixedArray.Length = this.Length;
				this.m_IsEmbedded = true;
				return;
			}
			this.m_List = new UnsafeList<int>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			this.m_List.Resize(this.Length, NativeArrayOptions.UninitializedMemory);
			this.m_IsEmbedded = false;
		}

		// Token: 0x17000043 RID: 67
		public int this[int index]
		{
			get
			{
				if (this.m_IsEmbedded)
				{
					return this.m_FixedArray[index];
				}
				return this.m_List[index];
			}
			set
			{
				if (this.m_IsEmbedded)
				{
					this.m_FixedArray[index] = value;
					return;
				}
				this.m_List[index] = value;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000D20C File Offset: 0x0000B40C
		public void Dispose()
		{
			if (!this.Valid)
			{
				return;
			}
			this.m_List.Dispose();
			this.Valid = false;
		}

		// Token: 0x0400021D RID: 541
		private FixedList32Bytes<int> m_FixedArray;

		// Token: 0x0400021E RID: 542
		private UnsafeList<int> m_List;

		// Token: 0x0400021F RID: 543
		private readonly bool m_IsEmbedded;

		// Token: 0x04000221 RID: 545
		public readonly int Length;
	}
}
