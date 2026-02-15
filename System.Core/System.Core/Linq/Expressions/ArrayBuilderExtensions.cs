using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200008A RID: 138
	internal static class ArrayBuilderExtensions
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x00013058 File Offset: 0x00011258
		public static ReadOnlyCollection<T> ToReadOnly<T>(this global::System.Collections.Generic.ArrayBuilder<T> builder)
		{
			return new TrueReadOnlyCollection<T>(builder.ToArray());
		}
	}
}
