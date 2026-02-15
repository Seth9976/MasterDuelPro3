using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000242 RID: 578
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	[VisibleToOtherModules]
	internal ref struct BlittableListWrapper
	{
		// Token: 0x060014A2 RID: 5282 RVA: 0x0002B97A File Offset: 0x00029B7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public BlittableListWrapper(BlittableArrayWrapper arrayWrapper, int listSize)
		{
			this.arrayWrapper = arrayWrapper;
			this.listSize = listSize;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0002B98C File Offset: 0x00029B8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void Unmarshal<[IsUnmanaged] T>(List<T> list) where T : struct, ValueType
		{
			bool flag = list == null;
			if (!flag)
			{
				switch (this.arrayWrapper.updateFlags)
				{
				case BlittableArrayWrapper.UpdateFlags.SizeChanged:
				case BlittableArrayWrapper.UpdateFlags.DataIsEmpty:
				case BlittableArrayWrapper.UpdateFlags.DataIsNull:
					NoAllocHelpers.ResetListSize<T>(list, this.listSize);
					break;
				case BlittableArrayWrapper.UpdateFlags.DataIsNativePointer:
					NoAllocHelpers.ResetListContents<T>(list, new ReadOnlySpan<T>(this.arrayWrapper.data, this.arrayWrapper.size));
					break;
				case BlittableArrayWrapper.UpdateFlags.DataIsNativeOwnedMemory:
					NoAllocHelpers.ResetListContents<T>(list, new ReadOnlySpan<T>(BindingsAllocator.GetNativeOwnedDataPointer(this.arrayWrapper.data), this.arrayWrapper.size));
					BindingsAllocator.FreeNativeOwnedMemory(this.arrayWrapper.data);
					break;
				}
			}
		}

		// Token: 0x040007A6 RID: 1958
		private BlittableArrayWrapper arrayWrapper;

		// Token: 0x040007A7 RID: 1959
		private int listSize;
	}
}
