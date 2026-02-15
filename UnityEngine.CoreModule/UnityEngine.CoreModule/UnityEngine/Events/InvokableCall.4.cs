using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000227 RID: 551
	internal class InvokableCall<T1, T2, T3> : BaseInvokableCall
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06001439 RID: 5177 RVA: 0x0002A5FC File Offset: 0x000287FC
		// (remove) Token: 0x0600143A RID: 5178 RVA: 0x0002A634 File Offset: 0x00028834
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected event UnityAction<T1, T2, T3> Delegate;

		// Token: 0x0600143B RID: 5179 RVA: 0x0002A669 File Offset: 0x00028869
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2, T3>), target, theFunction);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0002A691 File Offset: 0x00028891
		public InvokableCall(UnityAction<T1, T2, T3> action)
		{
			this.Delegate += action;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0002A6A4 File Offset: 0x000288A4
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 3;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]));
			}
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0002A718 File Offset: 0x00028918
		public void Invoke(T1 args0, T2 args1, T3 args2)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1, args2);
			}
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0002A744 File Offset: 0x00028944
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
