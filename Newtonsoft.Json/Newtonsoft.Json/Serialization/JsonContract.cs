using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000119 RID: 281
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonContract
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x000272BB File Offset: 0x000254BB
		public Type UnderlyingType { get; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x000272C3 File Offset: 0x000254C3
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x000272CC File Offset: 0x000254CC
		public Type CreatedType
		{
			get
			{
				return this._createdType;
			}
			set
			{
				ValidationUtils.ArgumentNotNull(value, "value");
				this._createdType = value;
				this.IsSealed = this._createdType.IsSealed();
				this.IsInstantiable = !this._createdType.IsInterface() && !this._createdType.IsAbstract();
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00027320 File Offset: 0x00025520
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x00027328 File Offset: 0x00025528
		public bool? IsReference { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x00027331 File Offset: 0x00025531
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x00027339 File Offset: 0x00025539
		[Nullable(2)]
		public JsonConverter Converter
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00027342 File Offset: 0x00025542
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x0002734A File Offset: 0x0002554A
		[Nullable(2)]
		public JsonConverter InternalConverter
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			internal set;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x00027353 File Offset: 0x00025553
		public IList<SerializationCallback> OnDeserializedCallbacks
		{
			get
			{
				if (this._onDeserializedCallbacks == null)
				{
					this._onDeserializedCallbacks = new List<SerializationCallback>();
				}
				return this._onDeserializedCallbacks;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0002736E File Offset: 0x0002556E
		public IList<SerializationCallback> OnDeserializingCallbacks
		{
			get
			{
				if (this._onDeserializingCallbacks == null)
				{
					this._onDeserializingCallbacks = new List<SerializationCallback>();
				}
				return this._onDeserializingCallbacks;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00027389 File Offset: 0x00025589
		public IList<SerializationCallback> OnSerializedCallbacks
		{
			get
			{
				if (this._onSerializedCallbacks == null)
				{
					this._onSerializedCallbacks = new List<SerializationCallback>();
				}
				return this._onSerializedCallbacks;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x000273A4 File Offset: 0x000255A4
		public IList<SerializationCallback> OnSerializingCallbacks
		{
			get
			{
				if (this._onSerializingCallbacks == null)
				{
					this._onSerializingCallbacks = new List<SerializationCallback>();
				}
				return this._onSerializingCallbacks;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x000273BF File Offset: 0x000255BF
		public IList<SerializationErrorCallback> OnErrorCallbacks
		{
			get
			{
				if (this._onErrorCallbacks == null)
				{
					this._onErrorCallbacks = new List<SerializationErrorCallback>();
				}
				return this._onErrorCallbacks;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x000273DA File Offset: 0x000255DA
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x000273E2 File Offset: 0x000255E2
		[Nullable(new byte[] { 2, 1 })]
		public Func<object> DefaultCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x000273EB File Offset: 0x000255EB
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x000273F3 File Offset: 0x000255F3
		public bool DefaultCreatorNonPublic { get; set; }

		// Token: 0x0600080A RID: 2058 RVA: 0x000273FC File Offset: 0x000255FC
		internal JsonContract(Type underlyingType)
		{
			ValidationUtils.ArgumentNotNull(underlyingType, "underlyingType");
			this.UnderlyingType = underlyingType;
			underlyingType = ReflectionUtils.EnsureNotByRefType(underlyingType);
			this.IsNullable = ReflectionUtils.IsNullable(underlyingType);
			this.NonNullableUnderlyingType = ((this.IsNullable && ReflectionUtils.IsNullableType(underlyingType)) ? Nullable.GetUnderlyingType(underlyingType) : underlyingType);
			this._createdType = (this.CreatedType = this.NonNullableUnderlyingType);
			this.IsConvertable = ConvertUtils.IsConvertible(this.NonNullableUnderlyingType);
			this.IsEnum = this.NonNullableUnderlyingType.IsEnum();
			this.InternalReadType = ReadType.Read;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00027494 File Offset: 0x00025694
		internal void InvokeOnSerializing(object o, StreamingContext context)
		{
			if (this._onSerializingCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onSerializingCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000274F0 File Offset: 0x000256F0
		internal void InvokeOnSerialized(object o, StreamingContext context)
		{
			if (this._onSerializedCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onSerializedCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0002754C File Offset: 0x0002574C
		internal void InvokeOnDeserializing(object o, StreamingContext context)
		{
			if (this._onDeserializingCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onDeserializingCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000275A8 File Offset: 0x000257A8
		internal void InvokeOnDeserialized(object o, StreamingContext context)
		{
			if (this._onDeserializedCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onDeserializedCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00027604 File Offset: 0x00025804
		internal void InvokeOnError(object o, StreamingContext context, ErrorContext errorContext)
		{
			if (this._onErrorCallbacks != null)
			{
				foreach (SerializationErrorCallback serializationErrorCallback in this._onErrorCallbacks)
				{
					serializationErrorCallback(o, context, errorContext);
				}
			}
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00027660 File Offset: 0x00025860
		internal static SerializationCallback CreateSerializationCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context)
			{
				callbackMethodInfo.Invoke(o, new object[] { context });
			};
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00027679 File Offset: 0x00025879
		internal static SerializationErrorCallback CreateSerializationErrorCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context, ErrorContext econtext)
			{
				callbackMethodInfo.Invoke(o, new object[] { context, econtext });
			};
		}

		// Token: 0x0400052C RID: 1324
		internal bool IsNullable;

		// Token: 0x0400052D RID: 1325
		internal bool IsConvertable;

		// Token: 0x0400052E RID: 1326
		internal bool IsEnum;

		// Token: 0x0400052F RID: 1327
		internal Type NonNullableUnderlyingType;

		// Token: 0x04000530 RID: 1328
		internal ReadType InternalReadType;

		// Token: 0x04000531 RID: 1329
		internal JsonContractType ContractType;

		// Token: 0x04000532 RID: 1330
		internal bool IsReadOnlyOrFixedSize;

		// Token: 0x04000533 RID: 1331
		internal bool IsSealed;

		// Token: 0x04000534 RID: 1332
		internal bool IsInstantiable;

		// Token: 0x04000535 RID: 1333
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationCallback> _onDeserializedCallbacks;

		// Token: 0x04000536 RID: 1334
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationCallback> _onDeserializingCallbacks;

		// Token: 0x04000537 RID: 1335
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationCallback> _onSerializedCallbacks;

		// Token: 0x04000538 RID: 1336
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationCallback> _onSerializingCallbacks;

		// Token: 0x04000539 RID: 1337
		[Nullable(new byte[] { 2, 1 })]
		private List<SerializationErrorCallback> _onErrorCallbacks;

		// Token: 0x0400053A RID: 1338
		private Type _createdType;
	}
}
