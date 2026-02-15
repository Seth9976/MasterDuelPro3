using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x020004B2 RID: 1202
	internal sealed class SerializationEvents
	{
		// Token: 0x06002652 RID: 9810 RVA: 0x0009A9C4 File Offset: 0x00098BC4
		internal SerializationEvents(Type t)
		{
			this._onSerializingMethods = this.GetMethodsWithAttribute(typeof(OnSerializingAttribute), t);
			this._onSerializedMethods = this.GetMethodsWithAttribute(typeof(OnSerializedAttribute), t);
			this._onDeserializingMethods = this.GetMethodsWithAttribute(typeof(OnDeserializingAttribute), t);
			this._onDeserializedMethods = this.GetMethodsWithAttribute(typeof(OnDeserializedAttribute), t);
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x0009AA34 File Offset: 0x00098C34
		private List<MethodInfo> GetMethodsWithAttribute(Type attribute, Type t)
		{
			List<MethodInfo> list = null;
			Type type = t;
			while (type != null && type != typeof(object))
			{
				foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (methodInfo.IsDefined(attribute, false))
					{
						if (list == null)
						{
							list = new List<MethodInfo>();
						}
						list.Add(methodInfo);
					}
				}
				type = type.BaseType;
			}
			if (list != null)
			{
				list.Reverse();
			}
			return list;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x0009AAAB File Offset: 0x00098CAB
		internal bool HasOnSerializingEvents
		{
			get
			{
				return this._onSerializingMethods != null || this._onSerializedMethods != null;
			}
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x0009AAC0 File Offset: 0x00098CC0
		internal void InvokeOnSerializing(object obj, StreamingContext context)
		{
			SerializationEvents.InvokeOnDelegate(obj, context, this._onSerializingMethods);
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x0009AACF File Offset: 0x00098CCF
		internal void InvokeOnDeserializing(object obj, StreamingContext context)
		{
			SerializationEvents.InvokeOnDelegate(obj, context, this._onDeserializingMethods);
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x0009AADE File Offset: 0x00098CDE
		internal void InvokeOnDeserialized(object obj, StreamingContext context)
		{
			SerializationEvents.InvokeOnDelegate(obj, context, this._onDeserializedMethods);
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x0009AAED File Offset: 0x00098CED
		internal SerializationEventHandler AddOnSerialized(object obj, SerializationEventHandler handler)
		{
			return SerializationEvents.AddOnDelegate(obj, handler, this._onSerializedMethods);
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x0009AAFC File Offset: 0x00098CFC
		internal SerializationEventHandler AddOnDeserialized(object obj, SerializationEventHandler handler)
		{
			return SerializationEvents.AddOnDelegate(obj, handler, this._onDeserializedMethods);
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x0009AB0B File Offset: 0x00098D0B
		private static void InvokeOnDelegate(object obj, StreamingContext context, List<MethodInfo> methods)
		{
			SerializationEventHandler serializationEventHandler = SerializationEvents.AddOnDelegate(obj, null, methods);
			if (serializationEventHandler == null)
			{
				return;
			}
			serializationEventHandler(context);
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x0009AB20 File Offset: 0x00098D20
		private static SerializationEventHandler AddOnDelegate(object obj, SerializationEventHandler handler, List<MethodInfo> methods)
		{
			if (methods != null)
			{
				foreach (MethodInfo methodInfo in methods)
				{
					SerializationEventHandler serializationEventHandler = (SerializationEventHandler)methodInfo.CreateDelegate(typeof(SerializationEventHandler), obj);
					handler = (SerializationEventHandler)Delegate.Combine(handler, serializationEventHandler);
				}
			}
			return handler;
		}

		// Token: 0x0400124F RID: 4687
		private readonly List<MethodInfo> _onSerializingMethods;

		// Token: 0x04001250 RID: 4688
		private readonly List<MethodInfo> _onSerializedMethods;

		// Token: 0x04001251 RID: 4689
		private readonly List<MethodInfo> _onDeserializingMethods;

		// Token: 0x04001252 RID: 4690
		private readonly List<MethodInfo> _onDeserializedMethods;
	}
}
