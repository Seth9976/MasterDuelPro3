using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x02000232 RID: 562
	[Serializable]
	public class UnityEvent<T0> : UnityEventBase
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x0002B32C File Offset: 0x0002952C
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0002B33D File Offset: 0x0002953D
		public void AddListener(UnityAction<T0> call)
		{
			base.AddCall(UnityEvent<T0>.GetDelegate(call));
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0002B21A File Offset: 0x0002941A
		public void RemoveListener(UnityAction<T0> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0002B350 File Offset: 0x00029550
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[] { typeof(T0) });
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0002B37C File Offset: 0x0002957C
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0>(target, theFunction);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0002B398 File Offset: 0x00029598
		private static BaseInvokableCall GetDelegate(UnityAction<T0> action)
		{
			return new InvokableCall<T0>(action);
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0002B3B0 File Offset: 0x000295B0
		public void Invoke(T0 arg0)
		{
			List<BaseInvokableCall> calls = base.PrepareInvoke();
			for (int i = 0; i < calls.Count; i++)
			{
				InvokableCall<T0> curCall = calls[i] as InvokableCall<T0>;
				bool flag = curCall != null;
				if (flag)
				{
					curCall.Invoke(arg0);
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
							this.m_InvokeArray = new object[1];
						}
						this.m_InvokeArray[0] = arg0;
						cachedCurCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x04000792 RID: 1938
		private object[] m_InvokeArray = null;
	}
}
