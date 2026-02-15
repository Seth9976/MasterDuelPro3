using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.Rendering
{
	// Token: 0x02000052 RID: 82
	public class ObjectPool<T> where T : new()
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00008C43 File Offset: 0x00006E43
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x00008C4B File Offset: 0x00006E4B
		public int countAll { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00008C54 File Offset: 0x00006E54
		public int countActive
		{
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00008C63 File Offset: 0x00006E63
		public int countInactive
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00008C70 File Offset: 0x00006E70
		public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease, bool collectionCheck = true)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
			this.m_CollectionCheck = collectionCheck;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00008CA0 File Offset: 0x00006EA0
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

		// Token: 0x06000499 RID: 1177 RVA: 0x00008CF4 File Offset: 0x00006EF4
		public ObjectPool<T>.PooledObject Get(out T v)
		{
			return new ObjectPool<T>.PooledObject(v = this.Get(), this);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00008D16 File Offset: 0x00006F16
		public void Release(T element)
		{
			if (this.m_ActionOnRelease != null)
			{
				this.m_ActionOnRelease(element);
			}
			this.m_Stack.Push(element);
		}

		// Token: 0x04000126 RID: 294
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x04000127 RID: 295
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x04000128 RID: 296
		private readonly UnityAction<T> m_ActionOnRelease;

		// Token: 0x04000129 RID: 297
		private readonly bool m_CollectionCheck = true;

		// Token: 0x02000053 RID: 83
		public struct PooledObject : IDisposable
		{
			// Token: 0x0600049B RID: 1179 RVA: 0x00008D38 File Offset: 0x00006F38
			internal PooledObject(T value, ObjectPool<T> pool)
			{
				this.m_ToReturn = value;
				this.m_Pool = pool;
			}

			// Token: 0x0600049C RID: 1180 RVA: 0x00008D48 File Offset: 0x00006F48
			void IDisposable.Dispose()
			{
				this.m_Pool.Release(this.m_ToReturn);
			}

			// Token: 0x0400012B RID: 299
			private readonly T m_ToReturn;

			// Token: 0x0400012C RID: 300
			private readonly ObjectPool<T> m_Pool;
		}
	}
}
