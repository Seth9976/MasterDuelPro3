using System;
using System.Collections.Generic;

namespace TMPro
{
	// Token: 0x02000008 RID: 8
	public class FastAction<A, B, C>
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000022D4 File Offset: 0x000004D4
		public void Add(Action<A, B, C> rhs)
		{
			if (this.lookup.ContainsKey(rhs))
			{
				return;
			}
			this.lookup[rhs] = this.delegates.AddLast(rhs);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002300 File Offset: 0x00000500
		public void Remove(Action<A, B, C> rhs)
		{
			LinkedListNode<Action<A, B, C>> node;
			if (this.lookup.TryGetValue(rhs, out node))
			{
				this.lookup.Remove(rhs);
				this.delegates.Remove(node);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002338 File Offset: 0x00000538
		public void Call(A a, B b, C c)
		{
			for (LinkedListNode<Action<A, B, C>> node = this.delegates.First; node != null; node = node.Next)
			{
				node.Value(a, b, c);
			}
		}

		// Token: 0x04000011 RID: 17
		private LinkedList<Action<A, B, C>> delegates = new LinkedList<Action<A, B, C>>();

		// Token: 0x04000012 RID: 18
		private Dictionary<Action<A, B, C>, LinkedListNode<Action<A, B, C>>> lookup = new Dictionary<Action<A, B, C>, LinkedListNode<Action<A, B, C>>>();
	}
}
