using System;
using System.Reflection;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x0200022B RID: 555
	[Serializable]
	internal class PersistentCall : ISerializationCallbackReceiver
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0002A880 File Offset: 0x00028A80
		public Object target
		{
			get
			{
				return this.m_Target;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0002A898 File Offset: 0x00028A98
		public string targetAssemblyTypeName
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.m_TargetAssemblyTypeName) && this.m_Target != null;
				if (flag)
				{
					this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_Target.GetType().AssemblyQualifiedName);
				}
				return this.m_TargetAssemblyTypeName;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0002A8F0 File Offset: 0x00028AF0
		public string methodName
		{
			get
			{
				return this.m_MethodName;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0002A908 File Offset: 0x00028B08
		public PersistentListenerMode mode
		{
			get
			{
				return this.m_Mode;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0002A920 File Offset: 0x00028B20
		public ArgumentCache arguments
		{
			get
			{
				return this.m_Arguments;
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0002A938 File Offset: 0x00028B38
		public bool IsValid()
		{
			return !string.IsNullOrEmpty(this.targetAssemblyTypeName) && !string.IsNullOrEmpty(this.methodName);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0002A968 File Offset: 0x00028B68
		public BaseInvokableCall GetRuntimeCall(UnityEventBase theEvent)
		{
			bool flag = this.m_CallState == UnityEventCallState.Off || theEvent == null;
			BaseInvokableCall baseInvokableCall;
			if (flag)
			{
				baseInvokableCall = null;
			}
			else
			{
				MethodInfo method = theEvent.FindMethod(this);
				bool flag2 = method == null;
				if (flag2)
				{
					baseInvokableCall = null;
				}
				else
				{
					bool flag3 = !method.IsStatic && this.target == null;
					if (flag3)
					{
						baseInvokableCall = null;
					}
					else
					{
						Object targetObject = (method.IsStatic ? null : this.target);
						switch (this.m_Mode)
						{
						case PersistentListenerMode.EventDefined:
							baseInvokableCall = theEvent.GetDelegate(targetObject, method);
							break;
						case PersistentListenerMode.Void:
							baseInvokableCall = new InvokableCall(targetObject, method);
							break;
						case PersistentListenerMode.Object:
							baseInvokableCall = PersistentCall.GetObjectCall(targetObject, method, this.m_Arguments);
							break;
						case PersistentListenerMode.Int:
							baseInvokableCall = new CachedInvokableCall<int>(targetObject, method, this.m_Arguments.intArgument);
							break;
						case PersistentListenerMode.Float:
							baseInvokableCall = new CachedInvokableCall<float>(targetObject, method, this.m_Arguments.floatArgument);
							break;
						case PersistentListenerMode.String:
							baseInvokableCall = new CachedInvokableCall<string>(targetObject, method, this.m_Arguments.stringArgument);
							break;
						case PersistentListenerMode.Bool:
							baseInvokableCall = new CachedInvokableCall<bool>(targetObject, method, this.m_Arguments.boolArgument);
							break;
						default:
							baseInvokableCall = null;
							break;
						}
					}
				}
			}
			return baseInvokableCall;
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0002AA94 File Offset: 0x00028C94
		private static BaseInvokableCall GetObjectCall(Object target, MethodInfo method, ArgumentCache arguments)
		{
			Type type = typeof(Object);
			bool flag = !string.IsNullOrEmpty(arguments.unityObjectArgumentAssemblyTypeName);
			if (flag)
			{
				type = Type.GetType(arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(Object);
			}
			Type generic = typeof(CachedInvokableCall<>);
			Type specific = generic.MakeGenericType(new Type[] { type });
			ConstructorInfo ci = specific.GetConstructor(new Type[]
			{
				typeof(Object),
				typeof(MethodInfo),
				type
			});
			Object castedObject = arguments.unityObjectArgument;
			bool flag2 = castedObject != null && !type.IsAssignableFrom(castedObject.GetType());
			if (flag2)
			{
				castedObject = null;
			}
			return ci.Invoke(new object[] { target, method, castedObject }) as BaseInvokableCall;
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0002AB75 File Offset: 0x00028D75
		public void OnBeforeSerialize()
		{
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_TargetAssemblyTypeName);
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0002AB75 File Offset: 0x00028D75
		public void OnAfterDeserialize()
		{
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_TargetAssemblyTypeName);
		}

		// Token: 0x04000783 RID: 1923
		[FormerlySerializedAs("instance")]
		[SerializeField]
		private Object m_Target;

		// Token: 0x04000784 RID: 1924
		[SerializeField]
		private string m_TargetAssemblyTypeName;

		// Token: 0x04000785 RID: 1925
		[SerializeField]
		[FormerlySerializedAs("methodName")]
		private string m_MethodName;

		// Token: 0x04000786 RID: 1926
		[FormerlySerializedAs("mode")]
		[SerializeField]
		private PersistentListenerMode m_Mode = PersistentListenerMode.EventDefined;

		// Token: 0x04000787 RID: 1927
		[SerializeField]
		[FormerlySerializedAs("arguments")]
		private ArgumentCache m_Arguments = new ArgumentCache();

		// Token: 0x04000788 RID: 1928
		[SerializeField]
		[FormerlySerializedAs("m_Enabled")]
		[FormerlySerializedAs("enabled")]
		private UnityEventCallState m_CallState = UnityEventCallState.RuntimeOnly;
	}
}
