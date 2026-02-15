using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x02000236 RID: 566
	[Serializable]
	public class UnityEvent<T0, T1, T2> : UnityEventBase
	{
		// Token: 0x06001487 RID: 5255 RVA: 0x0002B59D File Offset: 0x0002979D
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0002B5AE File Offset: 0x000297AE
		public void AddListener(UnityAction<T0, T1, T2> call)
		{
			base.AddCall(UnityEvent<T0, T1, T2>.GetDelegate(call));
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0002B21A File Offset: 0x0002941A
		public void RemoveListener(UnityAction<T0, T1, T2> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0002B5C0 File Offset: 0x000297C0
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[]
			{
				typeof(T0),
				typeof(T1),
				typeof(T2)
			});
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0002B608 File Offset: 0x00029808
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1, T2>(target, theFunction);
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0002B624 File Offset: 0x00029824
		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1, T2> action)
		{
			return new InvokableCall<T0, T1, T2>(action);
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0002B63C File Offset: 0x0002983C
		public void Invoke(T0 arg0, T1 arg1, T2 arg2)
		{
			List<BaseInvokableCall> calls = base.PrepareInvoke();
			for (int i = 0; i < calls.Count; i++)
			{
				InvokableCall<T0, T1, T2> curCall = calls[i] as InvokableCall<T0, T1, T2>;
				bool flag = curCall != null;
				if (flag)
				{
					curCall.Invoke(arg0, arg1, arg2);
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
							this.m_InvokeArray = new object[3];
						}
						this.m_InvokeArray[0] = arg0;
						this.m_InvokeArray[1] = arg1;
						this.m_InvokeArray[2] = arg2;
						cachedCurCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x04000794 RID: 1940
		private object[] m_InvokeArray = null;
	}
}
