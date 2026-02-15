using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000530 RID: 1328
	internal class BasicNode<T> : LinkedPoolItem<BasicNode<T>>
	{
		// Token: 0x060024BA RID: 9402 RVA: 0x0008BF40 File Offset: 0x0008A140
		public void InsertFirst(ref BasicNode<T> first)
		{
			bool flag = first == null;
			if (flag)
			{
				first = this;
			}
			else
			{
				this.next = first.next;
				first.next = this;
			}
		}

		// Token: 0x040011B8 RID: 4536
		public BasicNode<T> next;

		// Token: 0x040011B9 RID: 4537
		public T data;
	}
}
