using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x02000234 RID: 564
	[Serializable]
	public class UnityEvent<T0, T1> : UnityEventBase
	{
		// Token: 0x06001481 RID: 5249 RVA: 0x0002B46A File Offset: 0x0002966A
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0002B47C File Offset: 0x0002967C
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[]
			{
				typeof(T0),
				typeof(T1)
			});
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0002B4B8 File Offset: 0x000296B8
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1>(target, theFunction);
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0002B4D4 File Offset: 0x000296D4
		public void Invoke(T0 arg0, T1 arg1)
		{
			List<BaseInvokableCall> calls = base.PrepareInvoke();
			for (int i = 0; i < calls.Count; i++)
			{
				InvokableCall<T0, T1> curCall = calls[i] as InvokableCall<T0, T1>;
				bool flag = curCall != null;
				if (flag)
				{
					curCall.Invoke(arg0, arg1);
				}
				else
				{
					InvokableCall staticCurCall = calls[i] as InvokableCall;
					bool flag2 = staticCurCall != null;
					if (flag2)
					{
						staticCurCall.Invoke();
					}
					else
					{
						BaseInvokableCall cachedCurCall = calls[i];
						bool flag3 = this.m_InvokeArray == null;
						if (flag3)
						{
							this.m_InvokeArray = new object[2];
						}
						this.m_InvokeArray[0] = arg0;
						this.m_InvokeArray[1] = arg1;
						cachedCurCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x04000793 RID: 1939
		private object[] m_InvokeArray = null;
	}
}
