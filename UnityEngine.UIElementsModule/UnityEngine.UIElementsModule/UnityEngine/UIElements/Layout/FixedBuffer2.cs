using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200058D RID: 1421
	internal struct FixedBuffer2<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x170009FF RID: 2559
		public unsafe ref T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				bool flag = index < 0 || index >= 2;
				if (flag)
				{
					throw new IndexOutOfRangeException("index");
				}
				fixed (FixedBuffer2<T>* ptr2 = (FixedBuffer2<T>*)(&this))
				{
					void* ptr = (void*)ptr2;
					T* p = (T*)ptr;
					return ref p[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
			}
		}

		// Token: 0x040013C8 RID: 5064
		private T __0;

		// Token: 0x040013C9 RID: 5065
		private T __1;
	}
}
