using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnityEngine.Events
{
	// Token: 0x02000226 RID: 550
	internal class InvokableCall<T1, T2> : BaseInvokableCall
	{
		// Token: 0x06001435 RID: 5173 RVA: 0x0002A510 File Offset: 0x00028710
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2>), target, theFunction);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0002A538 File Offset: 0x00028738
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 2;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]));
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0002A59C File Offset: 0x0002879C
		public void Invoke(T1 args0, T2 args1)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1);
			}
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0002A5C8 File Offset: 0x000287C8
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}

		// Token: 0x0400077B RID: 1915
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private UnityAction<T1, T2> Delegate;
	}
}
