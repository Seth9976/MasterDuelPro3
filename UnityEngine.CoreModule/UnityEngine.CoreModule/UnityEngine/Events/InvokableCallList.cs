using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x0200022D RID: 557
	internal class InvokableCallList
	{
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0002AC5C File Offset: 0x00028E5C
		public int Count
		{
			get
			{
				return this.m_PersistentCalls.Count + this.m_RuntimeCalls.Count;
			}
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0002AC85 File Offset: 0x00028E85
		public void AddPersistentInvokableCall(BaseInvokableCall call)
		{
			this.m_PersistentCalls.Add(call);
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0002AC9C File Offset: 0x00028E9C
		public void AddListener(BaseInvokableCall call)
		{
			this.m_RuntimeCalls.Add(call);
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0002ACB4 File Offset: 0x00028EB4
		public void RemoveListener(object targetObj, MethodInfo method)
		{
			List<BaseInvokableCall> toRemove = new List<BaseInvokableCall>();
			for (int index = 0; index < this.m_RuntimeCalls.Count; index++)
			{
				bool flag = this.m_RuntimeCalls[index].Find(targetObj, method);
				if (flag)
				{
					toRemove.Add(this.m_RuntimeCalls[index]);
				}
			}
			this.m_RuntimeCalls.RemoveAll(new Predicate<BaseInvokableCall>(toRemove.Contains));
			List<BaseInvokableCall> newExecutingCalls = new List<BaseInvokableCall>(this.m_PersistentCalls.Count + this.m_RuntimeCalls.Count);
			newExecutingCalls.AddRange(this.m_PersistentCalls);
			newExecutingCalls.AddRange(this.m_RuntimeCalls);
			this.m_ExecutingCalls = newExecutingCalls;
			this.m_NeedsUpdate = false;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0002AD70 File Offset: 0x00028F70
		public void Clear()
		{
			this.m_RuntimeCalls.Clear();
			List<BaseInvokableCall> newExecutingCalls = new List<BaseInvokableCall>(this.m_PersistentCalls);
			this.m_ExecutingCalls = newExecutingCalls;
			this.m_NeedsUpdate = false;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0002ADA4 File Offset: 0x00028FA4
		public void ClearPersistent()
		{
			this.m_PersistentCalls.Clear();
			List<BaseInvokableCall> newExecutingCalls = new List<BaseInvokableCall>(this.m_RuntimeCalls);
			this.m_ExecutingCalls = newExecutingCalls;
			this.m_NeedsUpdate = false;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0002ADD8 File Offset: 0x00028FD8
		public List<BaseInvokableCall> PrepareInvoke()
		{
			bool needsUpdate = this.m_NeedsUpdate;
			if (needsUpdate)
			{
				this.m_ExecutingCalls.Clear();
				this.m_ExecutingCalls.AddRange(this.m_PersistentCalls);
				this.m_ExecutingCalls.AddRange(this.m_RuntimeCalls);
				this.m_NeedsUpdate = false;
			}
			return this.m_ExecutingCalls;
		}

		// Token: 0x0400078A RID: 1930
		private readonly List<BaseInvokableCall> m_PersistentCalls = new List<BaseInvokableCall>();

		// Token: 0x0400078B RID: 1931
		private readonly List<BaseInvokableCall> m_RuntimeCalls = new List<BaseInvokableCall>();

		// Token: 0x0400078C RID: 1932
		private List<BaseInvokableCall> m_ExecutingCalls = new List<BaseInvokableCall>();

		// Token: 0x0400078D RID: 1933
		private bool m_NeedsUpdate = true;
	}
}
