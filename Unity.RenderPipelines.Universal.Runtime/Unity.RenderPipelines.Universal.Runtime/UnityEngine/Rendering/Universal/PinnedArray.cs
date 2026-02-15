using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000DF RID: 223
	internal struct PinnedArray<T> : IDisposable where T : struct
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00015521 File Offset: 0x00013721
		public int length
		{
			get
			{
				if (this.managedArray == null)
				{
					return 0;
				}
				return this.managedArray.Length;
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00015535 File Offset: 0x00013735
		public unsafe PinnedArray(int length)
		{
			this.managedArray = new T[length];
			this.handle = GCHandle.Alloc(this.managedArray, GCHandleType.Pinned);
			this.nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.handle.AddrOfPinnedObject(), length, Allocator.None);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00015572 File Offset: 0x00013772
		public void Dispose()
		{
			if (this.managedArray == null)
			{
				return;
			}
			this.handle.Free();
			this = default(PinnedArray<T>);
		}

		// Token: 0x040004E3 RID: 1251
		public T[] managedArray;

		// Token: 0x040004E4 RID: 1252
		public GCHandle handle;

		// Token: 0x040004E5 RID: 1253
		public NativeArray<T> nativeArray;
	}
}
