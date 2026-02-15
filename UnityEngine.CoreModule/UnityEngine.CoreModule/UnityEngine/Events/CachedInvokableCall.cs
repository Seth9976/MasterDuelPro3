using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000229 RID: 553
	internal class CachedInvokableCall<T> : InvokableCall<T>
	{
		// Token: 0x06001443 RID: 5187 RVA: 0x0002A85C File Offset: 0x00028A5C
		public CachedInvokableCall(Object target, MethodInfo theFunction, T argument)
			: base(target, theFunction)
		{
			this.m_Arg1 = argument;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0002A86F File Offset: 0x00028A6F
		public override void Invoke(object[] args)
		{
			base.Invoke(this.m_Arg1);
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0002A86F File Offset: 0x00028A6F
		public override void Invoke(T arg0)
		{
			base.Invoke(this.m_Arg1);
		}

		// Token: 0x0400077E RID: 1918
		private readonly T m_Arg1;
	}
}
