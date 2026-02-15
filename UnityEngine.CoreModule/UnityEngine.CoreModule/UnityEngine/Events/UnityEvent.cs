using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x02000230 RID: 560
	[Serializable]
	public class UnityEvent : UnityEventBase
	{
		// Token: 0x0600146F RID: 5231 RVA: 0x0002B1F9 File Offset: 0x000293F9
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0002B20A File Offset: 0x0002940A
		public void AddListener(UnityAction call)
		{
			base.AddCall(UnityEvent.GetDelegate(call));
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0002B21A File Offset: 0x0002941A
		public void RemoveListener(UnityAction call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0002B230 File Offset: 0x00029430
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[0]);
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0002B250 File Offset: 0x00029450
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall(target, theFunction);
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0002B26C File Offset: 0x0002946C
		private static BaseInvokableCall GetDelegate(UnityAction action)
		{
			return new InvokableCall(action);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0002B284 File Offset: 0x00029484
		public void Invoke()
		{
			List<BaseInvokableCall> calls = base.PrepareInvoke();
			for (int i = 0; i < calls.Count; i++)
			{
				InvokableCall curCall = calls[i] as InvokableCall;
				bool flag = curCall != null;
				if (flag)
				{
					curCall.Invoke();
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
							this.m_InvokeArray = new object[0];
						}
						cachedCurCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x04000791 RID: 1937
		private object[] m_InvokeArray = null;
	}
}
