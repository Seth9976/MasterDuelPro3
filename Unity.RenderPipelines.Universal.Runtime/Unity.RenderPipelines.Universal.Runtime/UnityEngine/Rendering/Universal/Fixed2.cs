using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000DE RID: 222
	internal struct Fixed2<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x060005AF RID: 1455 RVA: 0x000154B5 File Offset: 0x000136B5
		public Fixed2(T item1)
		{
			this = new Fixed2<T>(item1, item1);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000154BF File Offset: 0x000136BF
		public Fixed2(T item1, T item2)
		{
			this.item1 = item1;
			this.item2 = item2;
		}

		// Token: 0x17000162 RID: 354
		public unsafe T this[int index]
		{
			get
			{
				fixed (T* ptr = &this.item1)
				{
					return ptr[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
			}
			set
			{
				fixed (T* ptr = &this.item1)
				{
					ptr[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
				}
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000217F File Offset: 0x0000037F
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckRange(int index)
		{
		}

		// Token: 0x040004E1 RID: 1249
		public T item1;

		// Token: 0x040004E2 RID: 1250
		public T item2;
	}
}
