using System;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000328 RID: 808
	[Serializable]
	internal sealed class TreeSet<T> : SortedSet<T>
	{
		// Token: 0x060013F0 RID: 5104 RVA: 0x00056E42 File Offset: 0x00055042
		public TreeSet()
		{
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00056E4A File Offset: 0x0005504A
		public TreeSet(IComparer<T> comparer)
			: base(comparer)
		{
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00056E53 File Offset: 0x00055053
		public TreeSet(SerializationInfo siInfo, StreamingContext context)
			: base(siInfo, context)
		{
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00056E5D File Offset: 0x0005505D
		internal override bool AddIfNotPresent(T item)
		{
			bool flag = base.AddIfNotPresent(item);
			if (!flag)
			{
				throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", item));
			}
			return flag;
		}
	}
}
