using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200027D RID: 637
	internal class ObjectPool<T> where T : new()
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x00048F44 File Offset: 0x00047144
		// (set) Token: 0x06001107 RID: 4359 RVA: 0x00048F5C File Offset: 0x0004715C
		public int maxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = Math.Max(0, value);
				while (this.Size() > this.m_MaxSize)
				{
					this.Get();
				}
			}
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00048F94 File Offset: 0x00047194
		public ObjectPool(Func<T> CreateFunc, int maxSize = 100)
		{
			this.maxSize = maxSize;
			bool flag = CreateFunc == null;
			if (flag)
			{
				this.CreateFunc = () => new T();
			}
			else
			{
				this.CreateFunc = CreateFunc;
			}
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00048FF8 File Offset: 0x000471F8
		public int Size()
		{
			return this.m_Stack.Count;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00049018 File Offset: 0x00047218
		public T Get()
		{
			return (this.m_Stack.Count == 0) ? this.CreateFunc() : this.m_Stack.Pop();
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00049054 File Offset: 0x00047254
		public void Release(T element)
		{
			bool flag = this.m_Stack.Count > 0 && this.m_Stack.Peek() == element;
			if (flag)
			{
				Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
			}
			bool flag2 = this.m_Stack.Count < this.maxSize;
			if (flag2)
			{
				this.m_Stack.Push(element);
			}
		}

		// Token: 0x040009CC RID: 2508
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x040009CD RID: 2509
		private int m_MaxSize;

		// Token: 0x040009CE RID: 2510
		internal Func<T> CreateFunc;
	}
}
