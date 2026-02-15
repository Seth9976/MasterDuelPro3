using System;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x02000238 RID: 568
	[Serializable]
	public class UnityEvent<T0, T1, T2, T3> : UnityEventBase
	{
		// Token: 0x06001490 RID: 5264 RVA: 0x0002B717 File Offset: 0x00029917
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0002B728 File Offset: 0x00029928
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[]
			{
				typeof(T0),
				typeof(T1),
				typeof(T2),
				typeof(T3)
			});
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0002B77C File Offset: 0x0002997C
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1, T2, T3>(target, theFunction);
		}

		// Token: 0x04000795 RID: 1941
		private object[] m_InvokeArray = null;
	}
}
