using System;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200051C RID: 1308
	internal class EntryPool
	{
		// Token: 0x0600244D RID: 9293 RVA: 0x0008925C File Offset: 0x0008745C
		public EntryPool(int maxCapacity = 1024)
		{
			this.m_ThreadEntries = new Stack<Entry>[JobsUtility.ThreadIndexCount];
			int i = 0;
			int count = JobsUtility.ThreadIndexCount;
			while (i < count)
			{
				this.m_ThreadEntries[i] = new Stack<Entry>(128);
				i++;
			}
			this.m_SharedPool = new ImplicitPool<Entry>(EntryPool.k_CreateAction, EntryPool.k_ResetAction, 128, maxCapacity);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000892C4 File Offset: 0x000874C4
		public Entry Get()
		{
			Stack<Entry> entries = this.m_ThreadEntries[UIRUtility.GetThreadIndex()];
			bool flag = entries.Count == 0;
			if (flag)
			{
				ImplicitPool<Entry> sharedPool = this.m_SharedPool;
				lock (sharedPool)
				{
					for (int i = 0; i < 128; i++)
					{
						entries.Push(this.m_SharedPool.Get());
					}
				}
			}
			return entries.Pop();
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x00089358 File Offset: 0x00087558
		public void ReturnAll()
		{
			int i = 0;
			int count = this.m_ThreadEntries.Length;
			while (i < count)
			{
				this.m_ThreadEntries[i].Clear();
				i++;
			}
			this.m_SharedPool.ReturnAll();
		}

		// Token: 0x0400110F RID: 4367
		private const int k_StackSize = 128;

		// Token: 0x04001110 RID: 4368
		private Stack<Entry>[] m_ThreadEntries;

		// Token: 0x04001111 RID: 4369
		private ImplicitPool<Entry> m_SharedPool;

		// Token: 0x04001112 RID: 4370
		private static readonly Func<Entry> k_CreateAction = () => new Entry();

		// Token: 0x04001113 RID: 4371
		private static readonly Action<Entry> k_ResetAction = delegate(Entry e)
		{
			e.nextSibling = null;
			e.firstChild = null;
			e.lastChild = null;
			e.texture = null;
			e.material = null;
			e.gradientsOwner = null;
			e.flags = (EntryFlags)0;
		};
	}
}
