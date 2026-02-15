using System;
using System.Collections.Generic;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200033C RID: 828
	internal sealed class ClonableStack<T> : List<T>
	{
		// Token: 0x06002580 RID: 9600 RVA: 0x000D486C File Offset: 0x000D2A6C
		public ClonableStack()
		{
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000D4874 File Offset: 0x000D2A74
		private ClonableStack(IEnumerable<T> collection)
			: base(collection)
		{
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000D487D File Offset: 0x000D2A7D
		public void Push(T value)
		{
			base.Add(value);
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000D4888 File Offset: 0x000D2A88
		public T Pop()
		{
			int num = base.Count - 1;
			T t = base[num];
			base.RemoveAt(num);
			return t;
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000D48AC File Offset: 0x000D2AAC
		public T Peek()
		{
			return base[base.Count - 1];
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000D48BC File Offset: 0x000D2ABC
		public ClonableStack<T> Clone()
		{
			return new ClonableStack<T>(this);
		}
	}
}
