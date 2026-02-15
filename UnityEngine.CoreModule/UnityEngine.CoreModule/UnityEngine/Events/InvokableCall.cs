using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000224 RID: 548
	internal class InvokableCall : BaseInvokableCall
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06001427 RID: 5159 RVA: 0x0002A280 File Offset: 0x00028480
		// (remove) Token: 0x06001428 RID: 5160 RVA: 0x0002A2B8 File Offset: 0x000284B8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event UnityAction Delegate;

		// Token: 0x06001429 RID: 5161 RVA: 0x0002A2ED File Offset: 0x000284ED
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate += (UnityAction)global::System.Delegate.CreateDelegate(typeof(UnityAction), target, theFunction);
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0002A316 File Offset: 0x00028516
		public InvokableCall(UnityAction action)
		{
			this.Delegate += action;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0002A328 File Offset: 0x00028528
		public override void Invoke(object[] args)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate();
			}
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0002A354 File Offset: 0x00028554
		public void Invoke()
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate();
			}
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0002A380 File Offset: 0x00028580
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
