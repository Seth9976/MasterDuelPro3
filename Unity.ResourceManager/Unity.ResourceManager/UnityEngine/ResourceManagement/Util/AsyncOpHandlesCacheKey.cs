using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x02000030 RID: 48
	internal sealed class AsyncOpHandlesCacheKey : IOperationCacheKey, IEquatable<IOperationCacheKey>
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00006336 File Offset: 0x00004536
		public AsyncOpHandlesCacheKey(IList<AsyncOperationHandle> handles)
		{
			this.m_Handles = new HashSet<AsyncOperationHandle>(handles);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000634A File Offset: 0x0000454A
		public override int GetHashCode()
		{
			return this.m_Handles.GetHashCode();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006357 File Offset: 0x00004557
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AsyncOpHandlesCacheKey);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006357 File Offset: 0x00004557
		public bool Equals(IOperationCacheKey other)
		{
			return this.Equals(other as AsyncOpHandlesCacheKey);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006365 File Offset: 0x00004565
		private bool Equals(AsyncOpHandlesCacheKey other)
		{
			return this == other || (other != null && this.m_Handles.SetEquals(other.m_Handles));
		}

		// Token: 0x04000084 RID: 132
		private readonly HashSet<AsyncOperationHandle> m_Handles;
	}
}
