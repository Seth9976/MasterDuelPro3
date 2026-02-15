using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000156 RID: 342
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x06000B4C RID: 2892 RVA: 0x00009F1D File Offset: 0x0000811D
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002C5FF File Offset: 0x0002A7FF
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002C60F File Offset: 0x0002A80F
		public int GetHashCode(T obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x04000366 RID: 870
		internal static readonly ReferenceEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();
	}
}
