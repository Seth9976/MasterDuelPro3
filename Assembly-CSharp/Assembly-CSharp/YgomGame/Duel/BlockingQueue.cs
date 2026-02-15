using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000CAC RID: 3244
	public class BlockingQueue<T>
	{
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06005C6E RID: 23662 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06005C6F RID: 23663 RVA: 0x0000216D File Offset: 0x0000036D
		public void Clear()
		{
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x000F4F4C File Offset: 0x000F314C
		public T Peek()
		{
			return default(T);
		}

		// Token: 0x06005C71 RID: 23665 RVA: 0x0000216D File Offset: 0x0000036D
		public void Enqueue(T obj)
		{
		}

		// Token: 0x06005C72 RID: 23666 RVA: 0x000F4F64 File Offset: 0x000F3164
		public T Dequeue()
		{
			return default(T);
		}

		// Token: 0x06005C73 RID: 23667 RVA: 0x0000216A File Offset: 0x0000036A
		public T[] ToArray()
		{
			return null;
		}

		// Token: 0x06005C74 RID: 23668 RVA: 0x000F4F7C File Offset: 0x000F317C
		public U DeepCopy<U>(U target)
		{
			return default(U);
		}

		// Token: 0x040097FD RID: 38909
		private Queue<T> queue;
	}
}
