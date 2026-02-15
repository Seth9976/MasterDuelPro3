using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x0200022E RID: 558
	[UsedByNativeCode]
	[Serializable]
	public abstract class UnityEventBase : ISerializationCallbackReceiver
	{
		// Token: 0x0600145C RID: 5212 RVA: 0x0002AE64 File Offset: 0x00029064
		protected UnityEventBase()
		{
			this.m_Calls = new InvokableCallList();
			this.m_PersistentCalls = new PersistentCallGroup();
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0002AE8B File Offset: 0x0002908B
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.DirtyPersistentCalls();
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0002AE8B File Offset: 0x0002908B
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.DirtyPersistentCalls();
		}

		// Token: 0x0600145F RID: 5215
		protected abstract MethodInfo FindMethod_Impl(string name, Type targetObjType);

		// Token: 0x06001460 RID: 5216
		internal abstract BaseInvokableCall GetDelegate(object target, MethodInfo theFunction);

		// Token: 0x06001461 RID: 5217 RVA: 0x0002AE98 File Offset: 0x00029098
		internal MethodInfo FindMethod(PersistentCall call)
		{
			Type type = typeof(Object);
			bool flag = !string.IsNullOrEmpty(call.arguments.unityObjectArgumentAssemblyTypeName);
			if (flag)
			{
				type = Type.GetType(call.arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(Object);
			}
			Type targetType = ((call.target != null) ? call.target.GetType() : Type.GetType(call.targetAssemblyTypeName, false));
			return this.FindMethod(call.methodName, targetType, call.mode, type);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0002AF28 File Offset: 0x00029128
		internal MethodInfo FindMethod(string name, Type listenerType, PersistentListenerMode mode, Type argumentType)
		{
			MethodInfo methodInfo;
			switch (mode)
			{
			case PersistentListenerMode.EventDefined:
				methodInfo = this.FindMethod_Impl(name, listenerType);
				break;
			case PersistentListenerMode.Void:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[0]);
				break;
			case PersistentListenerMode.Object:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { argumentType ?? typeof(Object) });
				break;
			case PersistentListenerMode.Int:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { typeof(int) });
				break;
			case PersistentListenerMode.Float:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { typeof(float) });
				break;
			case PersistentListenerMode.String:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { typeof(string) });
				break;
			case PersistentListenerMode.Bool:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { typeof(bool) });
				break;
			default:
				methodInfo = null;
				break;
			}
			return methodInfo;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0002B020 File Offset: 0x00029220
		internal int GetCallsCount()
		{
			return this.m_Calls.Count;
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0002B040 File Offset: 0x00029240
		public int GetPersistentEventCount()
		{
			return this.m_PersistentCalls.Count;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0002B05D File Offset: 0x0002925D
		private void DirtyPersistentCalls()
		{
			this.m_Calls.ClearPersistent();
			this.m_CallsDirty = true;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0002B074 File Offset: 0x00029274
		private void RebuildPersistentCallsIfNeeded()
		{
			bool callsDirty = this.m_CallsDirty;
			if (callsDirty)
			{
				this.m_PersistentCalls.Initialize(this.m_Calls, this);
				this.m_CallsDirty = false;
			}
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0002B0A8 File Offset: 0x000292A8
		internal void AddCall(BaseInvokableCall call)
		{
			this.m_Calls.AddListener(call);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0002B0B8 File Offset: 0x000292B8
		protected void RemoveListener(object targetObj, MethodInfo method)
		{
			this.m_Calls.RemoveListener(targetObj, method);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0002B0C9 File Offset: 0x000292C9
		public void RemoveAllListeners()
		{
			this.m_Calls.Clear();
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0002B0D8 File Offset: 0x000292D8
		internal List<BaseInvokableCall> PrepareInvoke()
		{
			this.RebuildPersistentCallsIfNeeded();
			return this.m_Calls.PrepareInvoke();
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0002B0FC File Offset: 0x000292FC
		public override string ToString()
		{
			return base.ToString() + " " + base.GetType().FullName;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0002B12C File Offset: 0x0002932C
		public static MethodInfo GetValidMethodInfo(Type objectType, string functionName, Type[] argumentTypes)
		{
			while (objectType != typeof(object) && objectType != null)
			{
				MethodInfo method = objectType.GetMethod(functionName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, argumentTypes, null);
				bool flag = method != null;
				if (flag)
				{
					ParameterInfo[] parameterInfos = method.GetParameters();
					bool methodValid = true;
					int i = 0;
					foreach (ParameterInfo pi in parameterInfos)
					{
						Type requestedType = argumentTypes[i];
						Type receivedType = pi.ParameterType;
						methodValid = requestedType.IsPrimitive == receivedType.IsPrimitive;
						bool flag2 = !methodValid;
						if (flag2)
						{
							break;
						}
						i++;
					}
					bool flag3 = methodValid;
					if (flag3)
					{
						return method;
					}
				}
				objectType = objectType.BaseType;
			}
			return null;
		}

		// Token: 0x0400078E RID: 1934
		private InvokableCallList m_Calls;

		// Token: 0x0400078F RID: 1935
		[SerializeField]
		[FormerlySerializedAs("m_PersistentListeners")]
		private PersistentCallGroup m_PersistentCalls;

		// Token: 0x04000790 RID: 1936
		private bool m_CallsDirty = true;
	}
}
