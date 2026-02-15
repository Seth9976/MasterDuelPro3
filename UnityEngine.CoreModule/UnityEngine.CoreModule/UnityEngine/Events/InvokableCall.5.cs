using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnityEngine.Events
{
	// Token: 0x02000228 RID: 552
	internal class InvokableCall<T1, T2, T3, T4> : BaseInvokableCall
	{
		// Token: 0x06001440 RID: 5184 RVA: 0x0002A778 File Offset: 0x00028978
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3, T4>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2, T3, T4>), target, theFunction);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0002A7A0 File Offset: 0x000289A0
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 4;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			BaseInvokableCall.ThrowOnInvalidArg<T4>(args[3]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]), (T4)((object)args[3]));
			}
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0002A828 File Offset: 0x00028A28
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}

		// Token: 0x0400077D RID: 1917
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private UnityAction<T1, T2, T3, T4> Delegate;
	}
}
