using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004FC RID: 1276
	internal sealed class ReadObjectInfo
	{
		// Token: 0x06002818 RID: 10264 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal ReadObjectInfo()
		{
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x00002C89 File Offset: 0x00000E89
		internal void ObjectEnd()
		{
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000A1D4D File Offset: 0x0009FF4D
		internal void PrepareForReuse()
		{
			this.lastPosition = 0;
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000A1D58 File Offset: 0x0009FF58
		internal static ReadObjectInfo Create(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			ReadObjectInfo objectInfo = ReadObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.Init(objectType, surrogateSelector, context, objectManager, serObjectInfoInit, converter, bSimpleAssembly);
			return objectInfo;
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000A1D7C File Offset: 0x0009FF7C
		internal void Init(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			this.objectType = objectType;
			this.objectManager = objectManager;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			this.formatterConverter = converter;
			this.bSimpleAssembly = bSimpleAssembly;
			this.InitReadConstructor(objectType, surrogateSelector, context);
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000A1DB8 File Offset: 0x0009FFB8
		internal static ReadObjectInfo Create(Type objectType, string[] memberNames, Type[] memberTypes, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			ReadObjectInfo objectInfo = ReadObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.Init(objectType, memberNames, memberTypes, surrogateSelector, context, objectManager, serObjectInfoInit, converter, bSimpleAssembly);
			return objectInfo;
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000A1DE0 File Offset: 0x0009FFE0
		internal void Init(Type objectType, string[] memberNames, Type[] memberTypes, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			this.objectType = objectType;
			this.objectManager = objectManager;
			this.wireMemberNames = memberNames;
			this.wireMemberTypes = memberTypes;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			this.formatterConverter = converter;
			this.bSimpleAssembly = bSimpleAssembly;
			if (memberNames != null)
			{
				this.isNamed = true;
			}
			if (memberTypes != null)
			{
				this.isTyped = true;
			}
			if (objectType != null)
			{
				this.InitReadConstructor(objectType, surrogateSelector, context);
			}
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x000A1E4C File Offset: 0x000A004C
		private void InitReadConstructor(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context)
		{
			if (objectType.IsArray)
			{
				this.InitNoMembers();
				return;
			}
			ISurrogateSelector surrogateSelector2 = null;
			if (surrogateSelector != null)
			{
				this.serializationSurrogate = surrogateSelector.GetSurrogate(objectType, context, out surrogateSelector2);
			}
			if (this.serializationSurrogate != null)
			{
				this.isSi = true;
			}
			else if (objectType != Converter.typeofObject && Converter.typeofISerializable.IsAssignableFrom(objectType))
			{
				this.isSi = true;
			}
			if (this.isSi)
			{
				this.InitSiRead();
				return;
			}
			this.InitMemberInfo();
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000A1EBF File Offset: 0x000A00BF
		private void InitSiRead()
		{
			if (this.memberTypesList != null)
			{
				this.memberTypesList = new List<Type>(20);
			}
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000A1ED6 File Offset: 0x000A00D6
		private void InitNoMembers()
		{
			this.cache = new SerObjectInfoCache(this.objectType);
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000A1EEC File Offset: 0x000A00EC
		private void InitMemberInfo()
		{
			this.cache = new SerObjectInfoCache(this.objectType);
			this.cache.memberInfos = FormatterServices.GetSerializableMembers(this.objectType, this.context);
			this.count = this.cache.memberInfos.Length;
			this.cache.memberNames = new string[this.count];
			this.cache.memberTypes = new Type[this.count];
			for (int i = 0; i < this.count; i++)
			{
				this.cache.memberNames[i] = this.cache.memberInfos[i].Name;
				this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
			}
			this.isTyped = true;
			this.isNamed = true;
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000A1FC4 File Offset: 0x000A01C4
		internal MemberInfo GetMemberInfo(string name)
		{
			if (this.cache == null)
			{
				return null;
			}
			if (this.isSi)
			{
				string text = "MemberInfo cannot be obtained for ISerialized Object '{0}'.";
				object[] array = new object[1];
				int num = 0;
				Type type = this.objectType;
				array[num] = ((type != null) ? type.ToString() : null) + " " + name;
				throw new SerializationException(Environment.GetResourceString(text, array));
			}
			if (this.cache.memberInfos == null)
			{
				string text2 = "No MemberInfo for Object {0}.";
				object[] array2 = new object[1];
				int num2 = 0;
				Type type2 = this.objectType;
				array2[num2] = ((type2 != null) ? type2.ToString() : null) + " " + name;
				throw new SerializationException(Environment.GetResourceString(text2, array2));
			}
			if (this.Position(name) != -1)
			{
				return this.cache.memberInfos[this.Position(name)];
			}
			return null;
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000A207C File Offset: 0x000A027C
		internal Type GetType(string name)
		{
			int num = this.Position(name);
			if (num == -1)
			{
				return null;
			}
			Type type;
			if (this.isTyped)
			{
				type = this.cache.memberTypes[num];
			}
			else
			{
				type = this.memberTypesList[num];
			}
			if (type == null)
			{
				string text = "Types not available for ISerializable object '{0}'.";
				object[] array = new object[1];
				int num2 = 0;
				Type type2 = this.objectType;
				array[num2] = ((type2 != null) ? type2.ToString() : null) + " " + name;
				throw new SerializationException(Environment.GetResourceString(text, array));
			}
			return type;
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x000A20F8 File Offset: 0x000A02F8
		internal void AddValue(string name, object value, ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isSi)
			{
				si.AddValue(name, value);
				return;
			}
			int num = this.Position(name);
			if (num != -1)
			{
				memberData[num] = value;
			}
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x000A212C File Offset: 0x000A032C
		internal void InitDataStore(ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isSi)
			{
				if (si == null)
				{
					si = new SerializationInfo(this.objectType, this.formatterConverter);
					return;
				}
			}
			else if (memberData == null && this.cache != null)
			{
				memberData = new object[this.cache.memberNames.Length];
			}
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x000A217C File Offset: 0x000A037C
		internal void RecordFixup(long objectId, string name, long idRef)
		{
			if (this.isSi)
			{
				this.objectManager.RecordDelayedFixup(objectId, name, idRef);
				return;
			}
			int num = this.Position(name);
			if (num != -1)
			{
				this.objectManager.RecordFixup(objectId, this.cache.memberInfos[num], idRef);
			}
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x000A21C6 File Offset: 0x000A03C6
		internal void PopulateObjectMembers(object obj, object[] memberData)
		{
			if (!this.isSi && memberData != null)
			{
				FormatterServices.PopulateObjectMembers(obj, this.cache.memberInfos, memberData);
			}
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x000A21E8 File Offset: 0x000A03E8
		private int Position(string name)
		{
			if (this.cache == null)
			{
				return -1;
			}
			if (this.cache.memberNames.Length != 0 && this.cache.memberNames[this.lastPosition].Equals(name))
			{
				return this.lastPosition;
			}
			int num = this.lastPosition + 1;
			this.lastPosition = num;
			if (num < this.cache.memberNames.Length && this.cache.memberNames[this.lastPosition].Equals(name))
			{
				return this.lastPosition;
			}
			for (int i = 0; i < this.cache.memberNames.Length; i++)
			{
				if (this.cache.memberNames[i].Equals(name))
				{
					this.lastPosition = i;
					return this.lastPosition;
				}
			}
			this.lastPosition = 0;
			return -1;
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000A22B4 File Offset: 0x000A04B4
		internal Type[] GetMemberTypes(string[] inMemberNames, Type objectType)
		{
			if (this.isSi)
			{
				throw new SerializationException(Environment.GetResourceString("Types not available for ISerializable object '{0}'.", new object[] { objectType }));
			}
			if (this.cache == null)
			{
				return null;
			}
			if (this.cache.memberTypes == null)
			{
				this.cache.memberTypes = new Type[this.count];
				for (int i = 0; i < this.count; i++)
				{
					this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
				}
			}
			bool flag = false;
			if (inMemberNames.Length < this.cache.memberInfos.Length)
			{
				flag = true;
			}
			Type[] array = new Type[this.cache.memberInfos.Length];
			for (int j = 0; j < this.cache.memberInfos.Length; j++)
			{
				if (!flag && inMemberNames[j].Equals(this.cache.memberInfos[j].Name))
				{
					array[j] = this.cache.memberTypes[j];
				}
				else
				{
					bool flag2 = false;
					for (int k = 0; k < inMemberNames.Length; k++)
					{
						if (this.cache.memberInfos[j].Name.Equals(inMemberNames[k]))
						{
							array[j] = this.cache.memberTypes[j];
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						object[] customAttributes = this.cache.memberInfos[j].GetCustomAttributes(typeof(OptionalFieldAttribute), false);
						if ((customAttributes == null || customAttributes.Length == 0) && !this.bSimpleAssembly)
						{
							throw new SerializationException(Environment.GetResourceString("Member '{0}' in class '{1}' is not present in the serialized stream and is not marked with {2}.", new object[]
							{
								this.cache.memberNames[j],
								objectType,
								typeof(OptionalFieldAttribute).FullName
							}));
						}
					}
				}
			}
			return array;
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000A2480 File Offset: 0x000A0680
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

		// Token: 0x0600282C RID: 10284 RVA: 0x000A24DB File Offset: 0x000A06DB
		private static ReadObjectInfo GetObjectInfo(SerObjectInfoInit serObjectInfoInit)
		{
			return new ReadObjectInfo
			{
				objectInfoId = Interlocked.Increment(ref ReadObjectInfo.readObjectInfoCounter)
			};
		}

		// Token: 0x040013FF RID: 5119
		internal int objectInfoId;

		// Token: 0x04001400 RID: 5120
		internal static int readObjectInfoCounter;

		// Token: 0x04001401 RID: 5121
		internal Type objectType;

		// Token: 0x04001402 RID: 5122
		internal ObjectManager objectManager;

		// Token: 0x04001403 RID: 5123
		internal int count;

		// Token: 0x04001404 RID: 5124
		internal bool isSi;

		// Token: 0x04001405 RID: 5125
		internal bool isNamed;

		// Token: 0x04001406 RID: 5126
		internal bool isTyped;

		// Token: 0x04001407 RID: 5127
		internal bool bSimpleAssembly;

		// Token: 0x04001408 RID: 5128
		internal SerObjectInfoCache cache;

		// Token: 0x04001409 RID: 5129
		internal string[] wireMemberNames;

		// Token: 0x0400140A RID: 5130
		internal Type[] wireMemberTypes;

		// Token: 0x0400140B RID: 5131
		private int lastPosition;

		// Token: 0x0400140C RID: 5132
		internal ISerializationSurrogate serializationSurrogate;

		// Token: 0x0400140D RID: 5133
		internal StreamingContext context;

		// Token: 0x0400140E RID: 5134
		internal List<Type> memberTypesList;

		// Token: 0x0400140F RID: 5135
		internal SerObjectInfoInit serObjectInfoInit;

		// Token: 0x04001410 RID: 5136
		internal IFormatterConverter formatterConverter;
	}
}
