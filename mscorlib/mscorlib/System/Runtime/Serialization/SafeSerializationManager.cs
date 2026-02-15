using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x020004C6 RID: 1222
	[Serializable]
	internal sealed class SafeSerializationManager : IObjectReference, ISerializable
	{
		// Token: 0x060026EE RID: 9966 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal SafeSerializationManager()
		{
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x0009D224 File Offset: 0x0009B424
		private SafeSerializationManager(SerializationInfo info, StreamingContext context)
		{
			RuntimeType runtimeType = info.GetValueNoThrow("CLR_SafeSerializationManager_RealType", typeof(RuntimeType)) as RuntimeType;
			if (runtimeType == null)
			{
				this.m_serializedStates = info.GetValue("m_serializedStates", typeof(List<object>)) as List<object>;
				return;
			}
			this.m_realType = runtimeType;
			this.m_savedSerializationInfo = info;
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060026F0 RID: 9968 RVA: 0x0009D28A File Offset: 0x0009B48A
		internal bool IsActive
		{
			get
			{
				return this.SerializeObjectState != null;
			}
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x0009D298 File Offset: 0x0009B498
		internal void CompleteSerialization(object serializedObject, SerializationInfo info, StreamingContext context)
		{
			this.m_serializedStates = null;
			EventHandler<SafeSerializationEventArgs> serializeObjectState = this.SerializeObjectState;
			if (serializeObjectState != null)
			{
				SafeSerializationEventArgs safeSerializationEventArgs = new SafeSerializationEventArgs(context);
				serializeObjectState(serializedObject, safeSerializationEventArgs);
				this.m_serializedStates = safeSerializationEventArgs.SerializedStates;
				info.AddValue("CLR_SafeSerializationManager_RealType", serializedObject.GetType(), typeof(RuntimeType));
				info.SetType(typeof(SafeSerializationManager));
			}
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x0009D2FC File Offset: 0x0009B4FC
		internal void CompleteDeserialization(object deserializedObject)
		{
			if (this.m_serializedStates != null)
			{
				foreach (object obj in this.m_serializedStates)
				{
					((ISafeSerializationData)obj).CompleteDeserialization(deserializedObject);
				}
			}
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x0009D354 File Offset: 0x0009B554
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_serializedStates", this.m_serializedStates, typeof(List<IDeserializationCallback>));
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x0009D374 File Offset: 0x0009B574
		object IObjectReference.GetRealObject(StreamingContext context)
		{
			if (this.m_realObject != null)
			{
				return this.m_realObject;
			}
			if (this.m_realType == null)
			{
				return this;
			}
			Stack stack = new Stack();
			RuntimeType runtimeType = this.m_realType;
			do
			{
				stack.Push(runtimeType);
				runtimeType = runtimeType.BaseType as RuntimeType;
			}
			while (runtimeType != typeof(object));
			RuntimeType runtimeType2;
			RuntimeConstructorInfo runtimeConstructorInfo;
			do
			{
				runtimeType2 = runtimeType;
				runtimeType = stack.Pop() as RuntimeType;
				runtimeConstructorInfo = runtimeType.GetSerializationCtor();
			}
			while (runtimeConstructorInfo != null && runtimeConstructorInfo.IsSecurityCritical);
			runtimeConstructorInfo = ObjectManager.GetConstructor(runtimeType2);
			object uninitializedObject = FormatterServices.GetUninitializedObject(this.m_realType);
			runtimeConstructorInfo.SerializationInvoke(uninitializedObject, this.m_savedSerializationInfo, context);
			this.m_savedSerializationInfo = null;
			this.m_realType = null;
			this.m_realObject = uninitializedObject;
			return uninitializedObject;
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x0009D437 File Offset: 0x0009B637
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.m_realObject != null)
			{
				SerializationEventsCache.GetSerializationEventsForType(this.m_realObject.GetType()).InvokeOnDeserialized(this.m_realObject, context);
				this.m_realObject = null;
			}
		}

		// Token: 0x04001290 RID: 4752
		private IList<object> m_serializedStates;

		// Token: 0x04001291 RID: 4753
		private SerializationInfo m_savedSerializationInfo;

		// Token: 0x04001292 RID: 4754
		private object m_realObject;

		// Token: 0x04001293 RID: 4755
		private RuntimeType m_realType;

		// Token: 0x04001294 RID: 4756
		[CompilerGenerated]
		private EventHandler<SafeSerializationEventArgs> SerializeObjectState;
	}
}
