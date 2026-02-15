using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine.Bindings;

namespace Unity.Collections
{
	// Token: 0x0200004A RID: 74
	[VisibleToOtherModules]
	internal static class CollectionExtensions
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x0000365C File Offset: 0x0000185C
		internal static string SerializedView<T>([DisallowNull] this IEnumerable<T> collection, [DisallowNull] Func<T, string> serializeElement)
		{
			bool flag = collection == null;
			if (flag)
			{
				throw new ArgumentNullException("collection must not be null.");
			}
			bool flag2 = serializeElement == null;
			if (flag2)
			{
				throw new ArgumentNullException("Argument serializeElement must not be null.");
			}
			return "[" + string.Join(",", collection.Select((T t) => (t == null) ? "null" : serializeElement(t))) + "]";
		}
	}
}
