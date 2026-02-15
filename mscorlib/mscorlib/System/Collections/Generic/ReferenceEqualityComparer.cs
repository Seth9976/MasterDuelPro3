using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x0200075E RID: 1886
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x06003C13 RID: 15379 RVA: 0x00003CE1 File Offset: 0x00001EE1
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x000E8094 File Offset: 0x000E6294
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x000E80A4 File Offset: 0x000E62A4
		public int GetHashCode(T obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x04001F36 RID: 7990
		internal static readonly ReferenceEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();
	}
}
