using System;
using System.Collections.Generic;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006D2 RID: 1746
	public abstract class AbstractReferenceCache<T> where T : class
	{
		// Token: 0x06003673 RID: 13939
		protected abstract T LoadRequest(string key, params object[] param);

		// Token: 0x06003674 RID: 13940 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void RemoveCacheAction(T value)
		{
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Clear()
		{
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void ClearKey(string key)
		{
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Exist(string key)
		{
			return false;
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddReference(string key, T value)
		{
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x000F35BC File Offset: 0x000F17BC
		public T GetReference(string key, params object[] param)
		{
			return default(T);
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveReference(string key)
		{
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddRefCount(string key)
		{
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x000029CC File Offset: 0x00000BCC
		private int DecRefCount(string key)
		{
			return 0;
		}

		// Token: 0x04003131 RID: 12593
		protected Dictionary<string, T> m_refCache;

		// Token: 0x04003132 RID: 12594
		private Dictionary<string, int> m_refCount;
	}
}
