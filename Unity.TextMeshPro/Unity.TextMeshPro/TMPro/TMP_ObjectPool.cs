using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro
{
	// Token: 0x02000067 RID: 103
	internal class TMP_ObjectPool<T> where T : new()
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00011EA5 File Offset: 0x000100A5
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00011EAD File Offset: 0x000100AD
		public int countAll { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00011EB6 File Offset: 0x000100B6
		public int countActive
		{
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00011EC5 File Offset: 0x000100C5
		public int countInactive
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00011ED2 File Offset: 0x000100D2
		public TMP_ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00011EF4 File Offset: 0x000100F4
		public T Get()
		{
			T element;
			if (this.m_Stack.Count == 0)
			{
				element = new T();
				int countAll = this.countAll;
				this.countAll = countAll + 1;
			}
			else
			{
				element = this.m_Stack.Pop();
			}
			if (this.m_ActionOnGet != null)
			{
				this.m_ActionOnGet(element);
			}
			return element;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00011F48 File Offset: 0x00010148
		public void Release(T element)
		{
			if (this.m_Stack.Count > 0 && this.m_Stack.Peek() == element)
			{
				Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
			}
			if (this.m_ActionOnRelease != null)
			{
				this.m_ActionOnRelease(element);
			}
			this.m_Stack.Push(element);
		}

		// Token: 0x0400024F RID: 591
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x04000250 RID: 592
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x04000251 RID: 593
		private readonly UnityAction<T> m_ActionOnRelease;
	}
}
