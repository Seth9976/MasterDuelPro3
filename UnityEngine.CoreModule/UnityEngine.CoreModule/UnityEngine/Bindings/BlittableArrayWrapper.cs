using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000240 RID: 576
	[VisibleToOtherModules]
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	internal ref struct BlittableArrayWrapper
	{
		// Token: 0x060014A0 RID: 5280 RVA: 0x0002B8D0 File Offset: 0x00029AD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe BlittableArrayWrapper(void* data, int size)
		{
			this.data = data;
			this.size = size;
			this.updateFlags = BlittableArrayWrapper.UpdateFlags.NoUpdateNeeded;
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0002B8E8 File Offset: 0x00029AE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void Unmarshal<[IsUnmanaged] T>(ref T[] array) where T : struct, ValueType
		{
			switch (this.updateFlags)
			{
			case BlittableArrayWrapper.UpdateFlags.SizeChanged:
			case BlittableArrayWrapper.UpdateFlags.DataIsNativePointer:
				array = new Span<T>(this.data, this.size).ToArray();
				break;
			case BlittableArrayWrapper.UpdateFlags.DataIsNativeOwnedMemory:
				array = new Span<T>(BindingsAllocator.GetNativeOwnedDataPointer(this.data), this.size).ToArray();
				BindingsAllocator.FreeNativeOwnedMemory(this.data);
				break;
			case BlittableArrayWrapper.UpdateFlags.DataIsEmpty:
				array = Array.Empty<T>();
				break;
			case BlittableArrayWrapper.UpdateFlags.DataIsNull:
				array = null;
				break;
			}
		}

		// Token: 0x0400079C RID: 1948
		internal unsafe void* data;

		// Token: 0x0400079D RID: 1949
		internal int size;

		// Token: 0x0400079E RID: 1950
		internal BlittableArrayWrapper.UpdateFlags updateFlags;

		// Token: 0x02000241 RID: 577
		internal enum UpdateFlags
		{
			// Token: 0x040007A0 RID: 1952
			NoUpdateNeeded,
			// Token: 0x040007A1 RID: 1953
			SizeChanged,
			// Token: 0x040007A2 RID: 1954
			DataIsNativePointer,
			// Token: 0x040007A3 RID: 1955
			DataIsNativeOwnedMemory,
			// Token: 0x040007A4 RID: 1956
			DataIsEmpty,
			// Token: 0x040007A5 RID: 1957
			DataIsNull
		}
	}
}
