using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004FB RID: 1275
	internal sealed class WriteObjectInfo
	{
		// Token: 0x06002806 RID: 10246 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal WriteObjectInfo()
		{
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000A1593 File Offset: 0x0009F793
		internal void ObjectEnd()
		{
			WriteObjectInfo.PutObjectInfo(this.serObjectInfoInit, this);
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x000A15A4 File Offset: 0x0009F7A4
		private void InternalInit()
		{
			this.obj = null;
			this.objectType = null;
			this.isSi = false;
			this.isNamed = false;
			this.isTyped = false;
			this.isArray = false;
			this.si = null;
			this.cache = null;
			this.memberData = null;
			this.objectId = 0L;
			this.assemId = 0L;
			this.binderTypeName = null;
			this.binderAssemblyString = null;
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x000A1610 File Offset: 0x0009F810
		internal static WriteObjectInfo Serialize(object obj, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, ObjectWriter objectWriter, SerializationBinder binder)
		{
			WriteObjectInfo objectInfo = WriteObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.InitSerialize(obj, surrogateSelector, context, serObjectInfoInit, converter, objectWriter, binder);
			return objectInfo;
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000A1634 File Offset: 0x0009F834
		internal void InitSerialize(object obj, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, ObjectWriter objectWriter, SerializationBinder binder)
		{
			this.context = context;
			this.obj = obj;
			this.serObjectInfoInit = serObjectInfoInit;
			if (RemotingServices.IsTransparentProxy(obj))
			{
				this.objectType = Converter.typeofMarshalByRefObject;
			}
			else
			{
				this.objectType = obj.GetType();
			}
			if (this.objectType.IsArray)
			{
				this.isArray = true;
				this.InitNoMembers();
				return;
			}
			this.InvokeSerializationBinder(binder);
			objectWriter.ObjectManager.RegisterObject(obj);
			ISurrogateSelector surrogateSelector2;
			if (surrogateSelector != null && (this.serializationSurrogate = surrogateSelector.GetSurrogate(this.objectType, context, out surrogateSelector2)) != null)
			{
				this.si = new SerializationInfo(this.objectType, converter);
				if (!this.objectType.IsPrimitive)
				{
					this.serializationSurrogate.GetObjectData(obj, this.si, context);
				}
				this.InitSiWrite();
				return;
			}
			if (!(obj is ISerializable))
			{
				this.InitMemberInfo();
				WriteObjectInfo.CheckTypeForwardedFrom(this.cache, this.objectType, this.binderAssemblyString);
				return;
			}
			if (!this.objectType.IsSerializable)
			{
				throw new SerializationException(Environment.GetResourceString("Type '{0}' in Assembly '{1}' is not marked as serializable.", new object[]
				{
					this.objectType.FullName,
					this.objectType.Assembly.FullName
				}));
			}
			this.si = new SerializationInfo(this.objectType, converter, !FormatterServices.UnsafeTypeForwardersIsEnabled());
			((ISerializable)obj).GetObjectData(this.si, context);
			this.InitSiWrite();
			WriteObjectInfo.CheckTypeForwardedFrom(this.cache, this.objectType, this.binderAssemblyString);
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000A17B6 File Offset: 0x0009F9B6
		internal static WriteObjectInfo Serialize(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, SerializationBinder binder)
		{
			WriteObjectInfo objectInfo = WriteObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.InitSerialize(objectType, surrogateSelector, context, serObjectInfoInit, converter, binder);
			return objectInfo;
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000A17CC File Offset: 0x0009F9CC
		internal void InitSerialize(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, SerializationBinder binder)
		{
			this.objectType = objectType;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			if (objectType.IsArray)
			{
				this.InitNoMembers();
				return;
			}
			this.InvokeSerializationBinder(binder);
			ISurrogateSelector surrogateSelector2 = null;
			if (surrogateSelector != null)
			{
				this.serializationSurrogate = surrogateSelector.GetSurrogate(objectType, context, out surrogateSelector2);
			}
			if (this.serializationSurrogate != null)
			{
				this.si = new SerializationInfo(objectType, converter);
				this.cache = new SerObjectInfoCache(objectType);
				this.isSi = true;
			}
			else if (objectType != Converter.typeofObject && Converter.typeofISerializable.IsAssignableFrom(objectType))
			{
				this.si = new SerializationInfo(objectType, converter, !FormatterServices.UnsafeTypeForwardersIsEnabled());
				this.cache = new SerObjectInfoCache(objectType);
				WriteObjectInfo.CheckTypeForwardedFrom(this.cache, objectType, this.binderAssemblyString);
				this.isSi = true;
			}
			if (!this.isSi)
			{
				this.InitMemberInfo();
				WriteObjectInfo.CheckTypeForwardedFrom(this.cache, objectType, this.binderAssemblyString);
			}
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000A18B8 File Offset: 0x0009FAB8
		private void InitSiWrite()
		{
			this.isSi = true;
			SerializationInfoEnumerator serializationInfoEnumerator = this.si.GetEnumerator();
			int memberCount = this.si.MemberCount;
			TypeInformation typeInformation = null;
			string text = this.si.FullTypeName;
			string text2 = this.si.AssemblyName;
			bool flag = false;
			if (!this.si.IsFullTypeNameSetExplicit)
			{
				typeInformation = BinaryFormatter.GetTypeInformation(this.si.ObjectType);
				text = typeInformation.FullTypeName;
				flag = typeInformation.HasTypeForwardedFrom;
			}
			if (!this.si.IsAssemblyNameSetExplicit)
			{
				if (typeInformation == null)
				{
					typeInformation = BinaryFormatter.GetTypeInformation(this.si.ObjectType);
				}
				text2 = typeInformation.AssemblyString;
				flag = typeInformation.HasTypeForwardedFrom;
			}
			this.cache = new SerObjectInfoCache(text, text2, flag);
			this.cache.memberNames = new string[memberCount];
			this.cache.memberTypes = new Type[memberCount];
			this.memberData = new object[memberCount];
			serializationInfoEnumerator = this.si.GetEnumerator();
			int num = 0;
			while (serializationInfoEnumerator.MoveNext())
			{
				this.cache.memberNames[num] = serializationInfoEnumerator.Name;
				this.cache.memberTypes[num] = serializationInfoEnumerator.ObjectType;
				this.memberData[num] = serializationInfoEnumerator.Value;
				num++;
			}
			this.isNamed = true;
			this.isTyped = false;
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000A1A04 File Offset: 0x0009FC04
		private static void CheckTypeForwardedFrom(SerObjectInfoCache cache, Type objectType, string binderAssemblyString)
		{
			if (cache.hasTypeForwardedFrom && binderAssemblyString == null && !FormatterServices.UnsafeTypeForwardersIsEnabled())
			{
				Assembly assembly = objectType.Assembly;
				if (!SerializationInfo.IsAssemblyNameAssignmentSafe(assembly.FullName, cache.assemblyString) && !assembly.IsFullyTrusted)
				{
					throw new SecurityException(Environment.GetResourceString("A type '{0}' that is defined in a partially trusted assembly cannot be type forwarded from an assembly with a different Public Key Token or without a public key token. To fix this, please either turn on unsafeTypeForwarding flag in the configuration file or remove the TypeForwardedFrom attribute.", new object[] { objectType }));
				}
			}
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000A1A60 File Offset: 0x0009FC60
		private void InitNoMembers()
		{
			this.cache = (SerObjectInfoCache)this.serObjectInfoInit.seenBeforeTable[this.objectType];
			if (this.cache == null)
			{
				this.cache = new SerObjectInfoCache(this.objectType);
				this.serObjectInfoInit.seenBeforeTable.Add(this.objectType, this.cache);
			}
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000A1AC4 File Offset: 0x0009FCC4
		private void InitMemberInfo()
		{
			this.cache = (SerObjectInfoCache)this.serObjectInfoInit.seenBeforeTable[this.objectType];
			if (this.cache == null)
			{
				this.cache = new SerObjectInfoCache(this.objectType);
				this.cache.memberInfos = FormatterServices.GetSerializableMembers(this.objectType, this.context);
				int num = this.cache.memberInfos.Length;
				this.cache.memberNames = new string[num];
				this.cache.memberTypes = new Type[num];
				for (int i = 0; i < num; i++)
				{
					this.cache.memberNames[i] = this.cache.memberInfos[i].Name;
					this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
				}
				this.serObjectInfoInit.seenBeforeTable.Add(this.objectType, this.cache);
			}
			if (this.obj != null)
			{
				this.memberData = FormatterServices.GetObjectData(this.obj, this.cache.memberInfos);
			}
			this.isTyped = true;
			this.isNamed = true;
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000A1BF3 File Offset: 0x0009FDF3
		internal string GetTypeFullName()
		{
			return this.binderTypeName ?? this.cache.fullTypeName;
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000A1C0A File Offset: 0x0009FE0A
		internal string GetAssemblyString()
		{
			return this.binderAssemblyString ?? this.cache.assemblyString;
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x000A1C21 File Offset: 0x0009FE21
		private void InvokeSerializationBinder(SerializationBinder binder)
		{
			if (binder != null)
			{
				binder.BindToName(this.objectType, out this.binderAssemblyString, out this.binderTypeName);
			}
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x000A1C40 File Offset: 0x0009FE40
		internal Type GetMemberType(MemberInfo objMember)
		{
			Type type;
			if (objMember is FieldInfo)
			{
				type = ((FieldInfo)objMember).FieldType;
			}
			else
			{
				if (!(objMember is PropertyInfo))
				{
					throw new SerializationException(Environment.GetResourceString("MemberInfo type {0} cannot be serialized.", new object[] { objMember.GetType() }));
				}
				type = ((PropertyInfo)objMember).PropertyType;
			}
			return type;
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x000A1C9C File Offset: 0x0009FE9C
		internal void GetMemberInfo(out string[] outMemberNames, out Type[] outMemberTypes, out object[] outMemberData)
		{
			outMemberNames = this.cache.memberNames;
			outMemberTypes = this.cache.memberTypes;
			outMemberData = this.memberData;
			if (this.isSi && !this.isNamed)
			{
				throw new SerializationException(Environment.GetResourceString("MemberInfo requested for ISerializable type."));
			}
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x000A1CEC File Offset: 0x0009FEEC
		private static WriteObjectInfo GetObjectInfo(SerObjectInfoInit serObjectInfoInit)
		{
			WriteObjectInfo writeObjectInfo;
			if (!serObjectInfoInit.oiPool.IsEmpty())
			{
				writeObjectInfo = (WriteObjectInfo)serObjectInfoInit.oiPool.Pop();
				writeObjectInfo.InternalInit();
			}
			else
			{
				writeObjectInfo = new WriteObjectInfo();
				WriteObjectInfo writeObjectInfo2 = writeObjectInfo;
				int objectInfoIdCount = serObjectInfoInit.objectInfoIdCount;
				serObjectInfoInit.objectInfoIdCount = objectInfoIdCount + 1;
				writeObjectInfo2.objectInfoId = objectInfoIdCount;
			}
			return writeObjectInfo;
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x000A1D3F File Offset: 0x0009FF3F
		private static void PutObjectInfo(SerObjectInfoInit serObjectInfoInit, WriteObjectInfo objectInfo)
		{
			serObjectInfoInit.oiPool.Push(objectInfo);
		}

		// Token: 0x040013EE RID: 5102
		internal int objectInfoId;

		// Token: 0x040013EF RID: 5103
		internal object obj;

		// Token: 0x040013F0 RID: 5104
		internal Type objectType;

		// Token: 0x040013F1 RID: 5105
		internal bool isSi;

		// Token: 0x040013F2 RID: 5106
		internal bool isNamed;

		// Token: 0x040013F3 RID: 5107
		internal bool isTyped;

		// Token: 0x040013F4 RID: 5108
		internal bool isArray;

		// Token: 0x040013F5 RID: 5109
		internal SerializationInfo si;

		// Token: 0x040013F6 RID: 5110
		internal SerObjectInfoCache cache;

		// Token: 0x040013F7 RID: 5111
		internal object[] memberData;

		// Token: 0x040013F8 RID: 5112
		internal ISerializationSurrogate serializationSurrogate;

		// Token: 0x040013F9 RID: 5113
		internal StreamingContext context;

		// Token: 0x040013FA RID: 5114
		internal SerObjectInfoInit serObjectInfoInit;

		// Token: 0x040013FB RID: 5115
		internal long objectId;

		// Token: 0x040013FC RID: 5116
		internal long assemId;

		// Token: 0x040013FD RID: 5117
		private string binderTypeName;

		// Token: 0x040013FE RID: 5118
		private string binderAssemblyString;
	}
}
