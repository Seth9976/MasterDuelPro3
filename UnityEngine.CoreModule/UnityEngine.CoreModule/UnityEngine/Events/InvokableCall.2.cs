using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000225 RID: 549
	internal class InvokableCall<T1> : BaseInvokableCall
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600142E RID: 5166 RVA: 0x0002A3B4 File Offset: 0x000285B4
		// (remove) Token: 0x0600142F RID: 5167 RVA: 0x0002A3EC File Offset: 0x000285EC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected event UnityAction<T1> Delegate;

		// Token: 0x06001430 RID: 5168 RVA: 0x0002A421 File Offset: 0x00028621
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate += (UnityAction<T1>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1>), target, theFunction);
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0002A44A File Offset: 0x0002864A
		public InvokableCall(UnityAction<T1> action)
		{
			this.Delegate += action;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0002A45C File Offset: 0x0002865C
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 1;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]));
			}
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0002A4B0 File Offset: 0x000286B0
		public virtual void Invoke(T1 args0)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0);
			}
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0002A4DC File Offset: 0x000286DC
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
