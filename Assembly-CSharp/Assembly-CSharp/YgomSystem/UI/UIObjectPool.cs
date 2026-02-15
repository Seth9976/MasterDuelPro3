using System;
using System.Collections.Generic;

namespace YgomSystem.UI
{
	// Token: 0x02000649 RID: 1609
	public abstract class UIObjectPool<T> : IObjectPool<T>
	{
		// Token: 0x06003247 RID: 12871 RVA: 0x0000216D File Offset: 0x0000036D
		public void CreateReserve(int cnt)
		{
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000F2990 File Offset: 0x000F0B90
		public T Rent()
		{
			return default(T);
		}

		// Token: 0x06003249 RID: 12873
		protected abstract T Create();

		// Token: 0x0600324A RID: 12874 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnAfterCreate(T obj)
		{
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnBeforeRent(T obj)
		{
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x0000216D File Offset: 0x0000036D
		public void Return(T obj)
		{
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnBeforeReturn(T obj)
		{
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReturnAll()
		{
		}

		// Token: 0x04002EEA RID: 12010
		private readonly List<T> m_ActiveObjects;

		// Token: 0x04002EEB RID: 12011
		private readonly Stack<T> m_UsableStack;
	}
}
