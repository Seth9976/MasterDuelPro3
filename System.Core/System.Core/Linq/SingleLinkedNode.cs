using System;

namespace System.Linq
{
	// Token: 0x02000054 RID: 84
	internal sealed class SingleLinkedNode<TSource>
	{
		// Token: 0x0600028F RID: 655 RVA: 0x0000B80A File Offset: 0x00009A0A
		public SingleLinkedNode(TSource item)
		{
			this.Item = item;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000B819 File Offset: 0x00009A19
		private SingleLinkedNode(SingleLinkedNode<TSource> linked, TSource item)
		{
			this.Linked = linked;
			this.Item = item;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000B82F File Offset: 0x00009A2F
		public TSource Item { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000B837 File Offset: 0x00009A37
		public SingleLinkedNode<TSource> Linked { get; }

		// Token: 0x06000293 RID: 659 RVA: 0x0000B83F File Offset: 0x00009A3F
		public SingleLinkedNode<TSource> Add(TSource item)
		{
			return new SingleLinkedNode<TSource>(this, item);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000B848 File Offset: 0x00009A48
		public SingleLinkedNode<TSource> GetNode(int index)
		{
			SingleLinkedNode<TSource> singleLinkedNode = this;
			while (index > 0)
			{
				singleLinkedNode = singleLinkedNode.Linked;
				index--;
			}
			return singleLinkedNode;
		}
	}
}
