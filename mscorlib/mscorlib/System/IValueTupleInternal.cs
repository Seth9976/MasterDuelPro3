using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000162 RID: 354
	internal interface IValueTupleInternal : ITuple
	{
		// Token: 0x06000CB8 RID: 3256
		int GetHashCode(IEqualityComparer comparer);

		// Token: 0x06000CB9 RID: 3257
		string ToStringEnd();
	}
}
