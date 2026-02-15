using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200012F RID: 303
	[NullableContext(1)]
	[Nullable(0)]
	internal static class JsonTypeReflector
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x0002E255 File Offset: 0x0002C455
		[return: Nullable(2)]
		public static T GetCachedAttribute<[Nullable(0)] T>(object attributeProvider) where T : Attribute
		{
			return CachedAttributeGetter<T>.GetAttribute(attributeProvider);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0002E260 File Offset: 0x0002C460
		public static bool CanTypeDescriptorConvertString(Type type, out TypeConverter typeConverter)
		{
			typeConverter = TypeDescriptor.GetConverter(type);
			if (typeConverter != null)
			{
				Type type2 = typeConverter.GetType();
				if (!string.Equals(type2.FullName, "System.ComponentModel.ComponentConverter", StringComparison.Ordinal) && !string.Equals(type2.FullName, "System.ComponentModel.ReferenceConverter", StringComparison.Ordinal) && !string.Equals(type2.FullName, "System.Windows.Forms.Design.DataSourceConverter", StringComparison.Ordinal) && type2 != typeof(TypeConverter))
				{
					return typeConverter.CanConvertTo(typeof(string));
				}
			}
			return false;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0002E2E0 File Offset: 0x0002C4E0
		[return: Nullable(2)]
		public static DataContractAttribute GetDataContractAttribute(Type type)
		{
			Type type2 = type;
			while (type2 != null)
			{
				DataContractAttribute attribute = CachedAttributeGetter<DataContractAttribute>.GetAttribute(type2);
				if (attribute != null)
				{
					return attribute;
				}
				type2 = type2.BaseType();
			}
			return null;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002E310 File Offset: 0x0002C510
		[return: Nullable(2)]
		public static DataMemberAttribute GetDataMemberAttribute(MemberInfo memberInfo)
		{
			if (memberInfo.MemberType() == MemberTypes.Field)
			{
				return CachedAttributeGetter<DataMemberAttribute>.GetAttribute(memberInfo);
			}
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			DataMemberAttribute dataMemberAttribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo);
			if (dataMemberAttribute == null && propertyInfo.IsVirtual())
			{
				Type type = propertyInfo.DeclaringType;
				while (dataMemberAttribute == null && type != null)
				{
					PropertyInfo propertyInfo2 = (PropertyInfo)ReflectionUtils.GetMemberInfoFromType(type, propertyInfo);
					if (propertyInfo2 != null && propertyInfo2.IsVirtual())
					{
						dataMemberAttribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo2);
					}
					type = type.BaseType();
				}
			}
			return dataMemberAttribute;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0002E388 File Offset: 0x0002C588
		public static MemberSerialization GetObjectMemberSerialization(Type objectType, bool ignoreSerializableAttribute)
		{
			JsonObjectAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonObjectAttribute>(objectType);
			if (cachedAttribute != null)
			{
				return cachedAttribute.MemberSerialization;
			}
			if (JsonTypeReflector.GetDataContractAttribute(objectType) != null)
			{
				return MemberSerialization.OptIn;
			}
			if (!ignoreSerializableAttribute && JsonTypeReflector.IsSerializable(objectType))
			{
				return MemberSerialization.Fields;
			}
			return MemberSerialization.OptOut;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0002E3C0 File Offset: 0x0002C5C0
		[return: Nullable(2)]
		public static JsonConverter GetJsonConverter(object attributeProvider)
		{
			JsonConverterAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonConverterAttribute>(attributeProvider);
			if (cachedAttribute != null)
			{
				Func<object[], object> func = JsonTypeReflector.CreatorCache.Get(cachedAttribute.ConverterType);
				if (func != null)
				{
					return (JsonConverter)func(cachedAttribute.ConverterParameters);
				}
			}
			return null;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0002E3FE File Offset: 0x0002C5FE
		public static JsonConverter CreateJsonConverterInstance(Type converterType, [Nullable(new byte[] { 2, 1 })] object[] args)
		{
			return (JsonConverter)JsonTypeReflector.CreatorCache.Get(converterType)(args);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0002E416 File Offset: 0x0002C616
		public static NamingStrategy CreateNamingStrategyInstance(Type namingStrategyType, [Nullable(new byte[] { 2, 1 })] object[] args)
		{
			return (NamingStrategy)JsonTypeReflector.CreatorCache.Get(namingStrategyType)(args);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0002E42E File Offset: 0x0002C62E
		[return: Nullable(2)]
		public static NamingStrategy GetContainerNamingStrategy(JsonContainerAttribute containerAttribute)
		{
			if (containerAttribute.NamingStrategyInstance == null)
			{
				if (containerAttribute.NamingStrategyType == null)
				{
					return null;
				}
				containerAttribute.NamingStrategyInstance = JsonTypeReflector.CreateNamingStrategyInstance(containerAttribute.NamingStrategyType, containerAttribute.NamingStrategyParameters);
			}
			return containerAttribute.NamingStrategyInstance;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0002E468 File Offset: 0x0002C668
		[return: Nullable(new byte[] { 1, 2, 1, 1 })]
		private static Func<object[], object> GetCreator(Type type)
		{
			Func<object> defaultConstructor = (ReflectionUtils.HasDefaultConstructor(type, false) ? JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type) : null);
			return delegate([Nullable(new byte[] { 2, 1 })] object[] parameters)
			{
				object obj;
				try
				{
					if (parameters != null)
					{
						Type[] array = parameters.Select(delegate(object param)
						{
							if (param == null)
							{
								throw new InvalidOperationException("Cannot pass a null parameter to the constructor.");
							}
							return param.GetType();
						}).ToArray<Type>();
						ConstructorInfo constructor = type.GetConstructor(array);
						if (!(constructor != null))
						{
							throw new JsonException("No matching parameterized constructor found for '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
						}
						obj = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor)(parameters);
					}
					else
					{
						if (defaultConstructor == null)
						{
							throw new JsonException("No parameterless constructor defined for '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
						}
						obj = defaultConstructor();
					}
				}
				catch (Exception ex)
				{
					throw new JsonException("Error creating '{0}'.".FormatWith(CultureInfo.InvariantCulture, type), ex);
				}
				return obj;
			};
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0002E4B5 File Offset: 0x0002C6B5
		[return: Nullable(2)]
		private static Type GetAssociatedMetadataType(Type type)
		{
			return JsonTypeReflector.AssociatedMetadataTypesCache.Get(type);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002E4C4 File Offset: 0x0002C6C4
		[return: Nullable(2)]
		private static Type GetAssociateMetadataTypeFromAttribute(Type type)
		{
			foreach (Attribute attribute in ReflectionUtils.GetAttributes(type, null, true))
			{
				Type type2 = attribute.GetType();
				if (string.Equals(type2.FullName, "System.ComponentModel.DataAnnotations.MetadataTypeAttribute", StringComparison.Ordinal))
				{
					if (JsonTypeReflector._metadataTypeAttributeReflectionObject == null)
					{
						JsonTypeReflector._metadataTypeAttributeReflectionObject = ReflectionObject.Create(type2, new string[] { "MetadataClassType" });
					}
					return (Type)JsonTypeReflector._metadataTypeAttributeReflectionObject.GetValue(attribute, "MetadataClassType");
				}
			}
			return null;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0002E540 File Offset: 0x0002C740
		[return: Nullable(2)]
		private static T GetAttribute<[Nullable(0)] T>(Type type) where T : Attribute
		{
			Type associatedMetadataType = JsonTypeReflector.GetAssociatedMetadataType(type);
			T t;
			if (associatedMetadataType != null)
			{
				t = ReflectionUtils.GetAttribute<T>(associatedMetadataType, true);
				if (t != null)
				{
					return t;
				}
			}
			t = ReflectionUtils.GetAttribute<T>(type, true);
			if (t != null)
			{
				return t;
			}
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				t = ReflectionUtils.GetAttribute<T>(interfaces[i], true);
				if (t != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0002E5B4 File Offset: 0x0002C7B4
		[return: Nullable(2)]
		private static T GetAttribute<[Nullable(0)] T>(MemberInfo memberInfo) where T : Attribute
		{
			Type associatedMetadataType = JsonTypeReflector.GetAssociatedMetadataType(memberInfo.DeclaringType);
			T t;
			if (associatedMetadataType != null)
			{
				MemberInfo memberInfoFromType = ReflectionUtils.GetMemberInfoFromType(associatedMetadataType, memberInfo);
				if (memberInfoFromType != null)
				{
					t = ReflectionUtils.GetAttribute<T>(memberInfoFromType, true);
					if (t != null)
					{
						return t;
					}
				}
			}
			t = ReflectionUtils.GetAttribute<T>(memberInfo, true);
			if (t != null)
			{
				return t;
			}
			if (memberInfo.DeclaringType != null)
			{
				Type[] interfaces = memberInfo.DeclaringType.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					MemberInfo memberInfoFromType2 = ReflectionUtils.GetMemberInfoFromType(interfaces[i], memberInfo);
					if (memberInfoFromType2 != null)
					{
						t = ReflectionUtils.GetAttribute<T>(memberInfoFromType2, true);
						if (t != null)
						{
							return t;
						}
					}
				}
			}
			return default(T);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0002E66A File Offset: 0x0002C86A
		public static bool IsNonSerializable(object provider)
		{
			return ReflectionUtils.GetAttribute<NonSerializedAttribute>(provider, false) != null;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0002E676 File Offset: 0x0002C876
		public static bool IsSerializable(object provider)
		{
			return ReflectionUtils.GetAttribute<SerializableAttribute>(provider, false) != null;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0002E684 File Offset: 0x0002C884
		[return: Nullable(2)]
		public static T GetAttribute<[Nullable(0)] T>(object provider) where T : Attribute
		{
			Type type = provider as Type;
			if (type != null)
			{
				return JsonTypeReflector.GetAttribute<T>(type);
			}
			MemberInfo memberInfo = provider as MemberInfo;
			if (memberInfo != null)
			{
				return JsonTypeReflector.GetAttribute<T>(memberInfo);
			}
			return ReflectionUtils.GetAttribute<T>(provider, true);
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0002E6BA File Offset: 0x0002C8BA
		public static bool DynamicCodeGeneration
		{
			get
			{
				if (JsonTypeReflector._dynamicCodeGeneration == null)
				{
					JsonTypeReflector._dynamicCodeGeneration = new bool?(false);
				}
				return JsonTypeReflector._dynamicCodeGeneration.GetValueOrDefault();
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0002E6E0 File Offset: 0x0002C8E0
		public static bool FullyTrusted
		{
			get
			{
				if (JsonTypeReflector._fullyTrusted == null)
				{
					AppDomain currentDomain = AppDomain.CurrentDomain;
					JsonTypeReflector._fullyTrusted = new bool?(currentDomain.IsHomogenous && currentDomain.IsFullyTrusted);
				}
				return JsonTypeReflector._fullyTrusted.GetValueOrDefault();
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0002E724 File Offset: 0x0002C924
		public static ReflectionDelegateFactory ReflectionDelegateFactory
		{
			get
			{
				return LateBoundReflectionDelegateFactory.Instance;
			}
		}

		// Token: 0x040005A5 RID: 1445
		private static bool? _dynamicCodeGeneration;

		// Token: 0x040005A6 RID: 1446
		private static bool? _fullyTrusted;

		// Token: 0x040005A7 RID: 1447
		public const string IdPropertyName = "$id";

		// Token: 0x040005A8 RID: 1448
		public const string RefPropertyName = "$ref";

		// Token: 0x040005A9 RID: 1449
		public const string TypePropertyName = "$type";

		// Token: 0x040005AA RID: 1450
		public const string ValuePropertyName = "$value";

		// Token: 0x040005AB RID: 1451
		public const string ArrayValuesPropertyName = "$values";

		// Token: 0x040005AC RID: 1452
		public const string ShouldSerializePrefix = "ShouldSerialize";

		// Token: 0x040005AD RID: 1453
		public const string SpecifiedPostfix = "Specified";

		// Token: 0x040005AE RID: 1454
		public const string ConcurrentDictionaryTypeName = "System.Collections.Concurrent.ConcurrentDictionary`2";

		// Token: 0x040005AF RID: 1455
		[Nullable(new byte[] { 1, 1, 1, 2, 1, 1 })]
		private static readonly ThreadSafeStore<Type, Func<object[], object>> CreatorCache = new ThreadSafeStore<Type, Func<object[], object>>(new Func<Type, Func<object[], object>>(JsonTypeReflector.GetCreator));

		// Token: 0x040005B0 RID: 1456
		[Nullable(new byte[] { 1, 1, 2 })]
		private static readonly ThreadSafeStore<Type, Type> AssociatedMetadataTypesCache = new ThreadSafeStore<Type, Type>(new Func<Type, Type>(JsonTypeReflector.GetAssociateMetadataTypeFromAttribute));

		// Token: 0x040005B1 RID: 1457
		[Nullable(2)]
		private static ReflectionObject _metadataTypeAttributeReflectionObject;
	}
}
