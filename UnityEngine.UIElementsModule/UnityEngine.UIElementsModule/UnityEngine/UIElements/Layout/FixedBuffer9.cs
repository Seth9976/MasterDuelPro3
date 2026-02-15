using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200058E RID: 1422
	internal struct FixedBuffer9<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x17000A00 RID: 2560
		public unsafe ref T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				bool flag = index < 0 || index >= 9;
				if (flag)
				{
					throw new IndexOutOfRangeException("index");
				}
				fixed (FixedBuffer9<T>* ptr2 = (FixedBuffer9<T>*)(&this))
				{
					void* ptr = (void*)ptr2;
					T* p = (T*)ptr;
					return ref p[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
			}
		}

		// Token: 0x040013CA RID: 5066
		private T __0;

		// Token: 0x040013CB RID: 5067
		private T __1;

		// Token: 0x040013CC RID: 5068
		private T __2;

		// Token: 0x040013CD RID: 5069
		private T __3;

		// Token: 0x040013CE RID: 5070
		private T __4;

		// Token: 0x040013CF RID: 5071
		private T __5;

		// Token: 0x040013D0 RID: 5072
		private T __6;

		// Token: 0x040013D1 RID: 5073
		private T __7;

		// Token: 0x040013D2 RID: 5074
		private T __8;
	}
}
