using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x020002C4 RID: 708
	internal sealed class ReflectTypeDescriptionProvider : TypeDescriptionProvider
	{
		// Token: 0x060010DD RID: 4317 RVA: 0x00047247 File Offset: 0x00045447
		internal ReflectTypeDescriptionProvider()
		{
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x00047250 File Offset: 0x00045450
		private static Hashtable IntrinsicTypeConverters
		{
			get
			{
				if (ReflectTypeDescriptionProvider._intrinsicTypeConverters == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable[typeof(bool)] = typeof(BooleanConverter);
					hashtable[typeof(byte)] = typeof(ByteConverter);
					hashtable[typeof(sbyte)] = typeof(SByteConverter);
					hashtable[typeof(char)] = typeof(CharConverter);
					hashtable[typeof(double)] = typeof(DoubleConverter);
					hashtable[typeof(string)] = typeof(StringConverter);
					hashtable[typeof(int)] = typeof(Int32Converter);
					hashtable[typeof(short)] = typeof(Int16Converter);
					hashtable[typeof(long)] = typeof(Int64Converter);
					hashtable[typeof(float)] = typeof(SingleConverter);
					hashtable[typeof(ushort)] = typeof(UInt16Converter);
					hashtable[typeof(uint)] = typeof(UInt32Converter);
					hashtable[typeof(ulong)] = typeof(UInt64Converter);
					hashtable[typeof(object)] = typeof(TypeConverter);
					hashtable[typeof(void)] = typeof(TypeConverter);
					hashtable[typeof(CultureInfo)] = typeof(CultureInfoConverter);
					hashtable[typeof(DateTime)] = typeof(DateTimeConverter);
					hashtable[typeof(DateTimeOffset)] = typeof(DateTimeOffsetConverter);
					hashtable[typeof(decimal)] = typeof(DecimalConverter);
					hashtable[typeof(TimeSpan)] = typeof(TimeSpanConverter);
					hashtable[typeof(Guid)] = typeof(GuidConverter);
					hashtable[typeof(Array)] = typeof(ArrayConverter);
					hashtable[typeof(ICollection)] = typeof(CollectionConverter);
					hashtable[typeof(Enum)] = typeof(EnumConverter);
					hashtable[ReflectTypeDescriptionProvider._intrinsicReferenceKey] = typeof(ReferenceConverter);
					hashtable[ReflectTypeDescriptionProvider._intrinsicNullableKey] = typeof(NullableConverter);
					ReflectTypeDescriptionProvider._intrinsicTypeConverters = hashtable;
				}
				return ReflectTypeDescriptionProvider._intrinsicTypeConverters;
			}
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00047518 File Offset: 0x00045718
		internal static void AddEditorTable(Type editorBaseType, Hashtable table)
		{
			if (editorBaseType == null)
			{
				throw new ArgumentNullException("editorBaseType");
			}
			object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (internalSyncObject)
			{
				if (ReflectTypeDescriptionProvider._editorTables == null)
				{
					ReflectTypeDescriptionProvider._editorTables = new Hashtable(4);
				}
				if (!ReflectTypeDescriptionProvider._editorTables.ContainsKey(editorBaseType))
				{
					ReflectTypeDescriptionProvider._editorTables[editorBaseType] = table;
				}
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0004759C File Offset: 0x0004579C
		public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			object obj;
			if (argTypes != null)
			{
				obj = SecurityUtils.SecureConstructorInvoke(objectType, argTypes, args, true, BindingFlags.ExactBinding);
			}
			else
			{
				if (args != null)
				{
					argTypes = new Type[args.Length];
					for (int i = 0; i < args.Length; i++)
					{
						if (args[i] != null)
						{
							argTypes[i] = args[i].GetType();
						}
						else
						{
							argTypes[i] = typeof(object);
						}
					}
				}
				else
				{
					argTypes = new Type[0];
				}
				obj = SecurityUtils.SecureConstructorInvoke(objectType, argTypes, args, true);
			}
			if (obj == null)
			{
				obj = SecurityUtils.SecureCreateInstance(objectType, args);
			}
			return obj;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00047624 File Offset: 0x00045824
		private static object CreateInstance(Type objectType, Type callingType)
		{
			object obj = SecurityUtils.SecureConstructorInvoke(objectType, ReflectTypeDescriptionProvider._typeConstructor, new object[] { callingType }, false);
			if (obj == null)
			{
				obj = SecurityUtils.SecureCreateInstance(objectType);
			}
			return obj;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00047653 File Offset: 0x00045853
		internal AttributeCollection GetAttributes(Type type)
		{
			return this.GetTypeData(type, true).GetAttributes();
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00047664 File Offset: 0x00045864
		public override IDictionary GetCache(object instance)
		{
			IComponent component = instance as IComponent;
			if (component != null && component.Site != null)
			{
				IDictionaryService dictionaryService = component.Site.GetService(typeof(IDictionaryService)) as IDictionaryService;
				if (dictionaryService != null)
				{
					IDictionary dictionary = dictionaryService.GetValue(ReflectTypeDescriptionProvider._dictionaryKey) as IDictionary;
					if (dictionary == null)
					{
						dictionary = new Hashtable();
						dictionaryService.SetValue(ReflectTypeDescriptionProvider._dictionaryKey, dictionary);
					}
					return dictionary;
				}
			}
			return null;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000476CA File Offset: 0x000458CA
		internal string GetClassName(Type type)
		{
			return this.GetTypeData(type, true).GetClassName(null);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x000476DA File Offset: 0x000458DA
		internal string GetComponentName(Type type, object instance)
		{
			return this.GetTypeData(type, true).GetComponentName(instance);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x000476EA File Offset: 0x000458EA
		internal TypeConverter GetConverter(Type type, object instance)
		{
			return this.GetTypeData(type, true).GetConverter(instance);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x000476FA File Offset: 0x000458FA
		internal EventDescriptor GetDefaultEvent(Type type, object instance)
		{
			return this.GetTypeData(type, true).GetDefaultEvent(instance);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0004770A File Offset: 0x0004590A
		internal PropertyDescriptor GetDefaultProperty(Type type, object instance)
		{
			return this.GetTypeData(type, true).GetDefaultProperty(instance);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0004771A File Offset: 0x0004591A
		internal object GetEditor(Type type, object instance, Type editorBaseType)
		{
			return this.GetTypeData(type, true).GetEditor(instance, editorBaseType);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0004772C File Offset: 0x0004592C
		private static Hashtable GetEditorTable(Type editorBaseType)
		{
			if (ReflectTypeDescriptionProvider._editorTables == null)
			{
				object obj = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (obj)
				{
					if (ReflectTypeDescriptionProvider._editorTables == null)
					{
						ReflectTypeDescriptionProvider._editorTables = new Hashtable(4);
					}
				}
			}
			object obj2 = ReflectTypeDescriptionProvider._editorTables[editorBaseType];
			if (obj2 == null)
			{
				RuntimeHelpers.RunClassConstructor(editorBaseType.TypeHandle);
				obj2 = ReflectTypeDescriptionProvider._editorTables[editorBaseType];
				if (obj2 == null)
				{
					object obj = ReflectTypeDescriptionProvider._internalSyncObject;
					lock (obj)
					{
						obj2 = ReflectTypeDescriptionProvider._editorTables[editorBaseType];
						if (obj2 == null)
						{
							ReflectTypeDescriptionProvider._editorTables[editorBaseType] = ReflectTypeDescriptionProvider._editorTables;
						}
					}
				}
			}
			if (obj2 == ReflectTypeDescriptionProvider._editorTables)
			{
				obj2 = null;
			}
			return (Hashtable)obj2;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00047810 File Offset: 0x00045A10
		internal EventDescriptorCollection GetEvents(Type type)
		{
			return this.GetTypeData(type, true).GetEvents();
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0004781F File Offset: 0x00045A1F
		internal AttributeCollection GetExtendedAttributes(object instance)
		{
			return AttributeCollection.Empty;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00047826 File Offset: 0x00045A26
		internal string GetExtendedClassName(object instance)
		{
			return this.GetClassName(instance.GetType());
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00047834 File Offset: 0x00045A34
		internal string GetExtendedComponentName(object instance)
		{
			return this.GetComponentName(instance.GetType(), instance);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00047843 File Offset: 0x00045A43
		internal TypeConverter GetExtendedConverter(object instance)
		{
			return this.GetConverter(instance.GetType(), instance);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x000027B6 File Offset: 0x000009B6
		internal EventDescriptor GetExtendedDefaultEvent(object instance)
		{
			return null;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x000027B6 File Offset: 0x000009B6
		internal PropertyDescriptor GetExtendedDefaultProperty(object instance)
		{
			return null;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00047852 File Offset: 0x00045A52
		internal object GetExtendedEditor(object instance, Type editorBaseType)
		{
			return this.GetEditor(instance.GetType(), instance, editorBaseType);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00047862 File Offset: 0x00045A62
		internal EventDescriptorCollection GetExtendedEvents(object instance)
		{
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0004786C File Offset: 0x00045A6C
		internal PropertyDescriptorCollection GetExtendedProperties(object instance)
		{
			Type type = instance.GetType();
			IExtenderProvider[] extenderProviders = this.GetExtenderProviders(instance);
			IDictionary cache = TypeDescriptor.GetCache(instance);
			if (extenderProviders.Length == 0)
			{
				return PropertyDescriptorCollection.Empty;
			}
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			if (cache != null)
			{
				propertyDescriptorCollection = cache[ReflectTypeDescriptionProvider._extenderPropertiesKey] as PropertyDescriptorCollection;
			}
			if (propertyDescriptorCollection != null)
			{
				return propertyDescriptorCollection;
			}
			ArrayList arrayList = null;
			for (int i = 0; i < extenderProviders.Length; i++)
			{
				PropertyDescriptor[] array = ReflectTypeDescriptionProvider.ReflectGetExtendedProperties(extenderProviders[i]);
				if (arrayList == null)
				{
					arrayList = new ArrayList(array.Length * extenderProviders.Length);
				}
				foreach (PropertyDescriptor propertyDescriptor in array)
				{
					ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = propertyDescriptor.Attributes[typeof(ExtenderProvidedPropertyAttribute)] as ExtenderProvidedPropertyAttribute;
					if (extenderProvidedPropertyAttribute != null)
					{
						Type receiverType = extenderProvidedPropertyAttribute.ReceiverType;
						if (receiverType != null && receiverType.IsAssignableFrom(type))
						{
							arrayList.Add(propertyDescriptor);
						}
					}
				}
			}
			if (arrayList != null)
			{
				PropertyDescriptor[] array2 = new PropertyDescriptor[arrayList.Count];
				arrayList.CopyTo(array2, 0);
				propertyDescriptorCollection = new PropertyDescriptorCollection(array2, true);
			}
			else
			{
				propertyDescriptorCollection = PropertyDescriptorCollection.Empty;
			}
			if (cache != null)
			{
				cache[ReflectTypeDescriptionProvider._extenderPropertiesKey] = propertyDescriptorCollection;
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00047998 File Offset: 0x00045B98
		protected internal override IExtenderProvider[] GetExtenderProviders(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			IComponent component = instance as IComponent;
			if (component != null && component.Site != null)
			{
				IExtenderListService extenderListService = component.Site.GetService(typeof(IExtenderListService)) as IExtenderListService;
				IDictionary cache = TypeDescriptor.GetCache(instance);
				if (extenderListService != null)
				{
					return ReflectTypeDescriptionProvider.GetExtenders(extenderListService.GetExtenderProviders(), instance, cache);
				}
				IContainer container = component.Site.Container;
				if (container != null)
				{
					return ReflectTypeDescriptionProvider.GetExtenders(container.Components, instance, cache);
				}
			}
			return new IExtenderProvider[0];
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00047A1C File Offset: 0x00045C1C
		private static IExtenderProvider[] GetExtenders(ICollection components, object instance, IDictionary cache)
		{
			bool flag = false;
			int num = 0;
			IExtenderProvider[] array = null;
			ulong num2 = 0UL;
			int num3 = 64;
			IExtenderProvider[] array2 = components as IExtenderProvider[];
			if (cache != null)
			{
				array = cache[ReflectTypeDescriptionProvider._extenderProviderKey] as IExtenderProvider[];
			}
			if (array == null)
			{
				flag = true;
			}
			int i = 0;
			int num4 = 0;
			if (array2 != null)
			{
				for (i = 0; i < array2.Length; i++)
				{
					if (array2[i].CanExtend(instance))
					{
						num++;
						if (i < num3)
						{
							num2 |= 1UL << i;
						}
						if (!flag && (num4 >= array.Length || array2[i] != array[num4++]))
						{
							flag = true;
						}
					}
				}
			}
			else if (components != null)
			{
				foreach (object obj in components)
				{
					IExtenderProvider extenderProvider = obj as IExtenderProvider;
					if (extenderProvider != null && extenderProvider.CanExtend(instance))
					{
						num++;
						if (i < num3)
						{
							num2 |= 1UL << i;
						}
						if (!flag && (num4 >= array.Length || extenderProvider != array[num4++]))
						{
							flag = true;
						}
					}
					i++;
				}
			}
			if (array != null && num != array.Length)
			{
				flag = true;
			}
			if (flag)
			{
				if (array2 == null || num != array2.Length)
				{
					IExtenderProvider[] array3 = new IExtenderProvider[num];
					i = 0;
					num4 = 0;
					if (array2 != null && num > 0)
					{
						while (i < array2.Length)
						{
							if ((i < num3 && (num2 & (1UL << i)) != 0UL) || (i >= num3 && array2[i].CanExtend(instance)))
							{
								array3[num4++] = array2[i];
							}
							i++;
						}
					}
					else if (num > 0)
					{
						foreach (object obj2 in components)
						{
							IExtenderProvider extenderProvider2 = obj2 as IExtenderProvider;
							if (extenderProvider2 != null && ((i < num3 && (num2 & (1UL << i)) != 0UL) || (i >= num3 && extenderProvider2.CanExtend(instance))))
							{
								array3[num4++] = extenderProvider2;
							}
							i++;
						}
					}
					array2 = array3;
				}
				if (cache != null)
				{
					cache[ReflectTypeDescriptionProvider._extenderProviderKey] = array2;
					cache.Remove(ReflectTypeDescriptionProvider._extenderPropertiesKey);
				}
			}
			else
			{
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00047C50 File Offset: 0x00045E50
		internal object GetExtendedPropertyOwner(object instance, PropertyDescriptor pd)
		{
			return this.GetPropertyOwner(instance.GetType(), instance, pd);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000027B6 File Offset: 0x000009B6
		public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			return null;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00047C60 File Offset: 0x00045E60
		internal PropertyDescriptorCollection GetProperties(Type type)
		{
			return this.GetTypeData(type, true).GetProperties();
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00047C6F File Offset: 0x00045E6F
		internal object GetPropertyOwner(Type type, object instance, PropertyDescriptor pd)
		{
			return TypeDescriptor.GetAssociation(type, instance);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00030668 File Offset: 0x0002E868
		public override Type GetReflectionType(Type objectType, object instance)
		{
			return objectType;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00047C78 File Offset: 0x00045E78
		private ReflectTypeDescriptionProvider.ReflectedTypeData GetTypeData(Type type, bool createIfNeeded)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData reflectedTypeData = null;
			if (this._typeData != null)
			{
				reflectedTypeData = (ReflectTypeDescriptionProvider.ReflectedTypeData)this._typeData[type];
				if (reflectedTypeData != null)
				{
					return reflectedTypeData;
				}
			}
			object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (internalSyncObject)
			{
				if (this._typeData != null)
				{
					reflectedTypeData = (ReflectTypeDescriptionProvider.ReflectedTypeData)this._typeData[type];
				}
				if (reflectedTypeData == null && createIfNeeded)
				{
					reflectedTypeData = new ReflectTypeDescriptionProvider.ReflectedTypeData(type);
					if (this._typeData == null)
					{
						this._typeData = new Hashtable();
					}
					this._typeData[type] = reflectedTypeData;
				}
			}
			return reflectedTypeData;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x000027B6 File Offset: 0x000009B6
		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			return null;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00047D1C File Offset: 0x00045F1C
		private static Type GetTypeFromName(string typeName)
		{
			Type type = Type.GetType(typeName);
			if (type == null)
			{
				int num = typeName.IndexOf(',');
				if (num != -1)
				{
					type = Type.GetType(typeName.Substring(0, num));
				}
			}
			return type;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00047D58 File Offset: 0x00045F58
		internal bool IsPopulated(Type type)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, false);
			return typeData != null && typeData.IsPopulated;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00047D7C File Offset: 0x00045F7C
		private static Attribute[] ReflectGetAttributes(Type type)
		{
			object obj;
			if (ReflectTypeDescriptionProvider._attributeCache == null)
			{
				obj = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (obj)
				{
					if (ReflectTypeDescriptionProvider._attributeCache == null)
					{
						ReflectTypeDescriptionProvider._attributeCache = new Hashtable();
					}
				}
			}
			Attribute[] array = (Attribute[])ReflectTypeDescriptionProvider._attributeCache[type];
			if (array != null)
			{
				return array;
			}
			obj = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (obj)
			{
				array = (Attribute[])ReflectTypeDescriptionProvider._attributeCache[type];
				if (array == null)
				{
					object[] customAttributes = type.GetCustomAttributes(typeof(Attribute), false);
					array = new Attribute[customAttributes.Length];
					customAttributes.CopyTo(array, 0);
					ReflectTypeDescriptionProvider._attributeCache[type] = array;
				}
			}
			return array;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00047E5C File Offset: 0x0004605C
		internal static Attribute[] ReflectGetAttributes(MemberInfo member)
		{
			object obj;
			if (ReflectTypeDescriptionProvider._attributeCache == null)
			{
				obj = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (obj)
				{
					if (ReflectTypeDescriptionProvider._attributeCache == null)
					{
						ReflectTypeDescriptionProvider._attributeCache = new Hashtable();
					}
				}
			}
			Attribute[] array = (Attribute[])ReflectTypeDescriptionProvider._attributeCache[member];
			if (array != null)
			{
				return array;
			}
			obj = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (obj)
			{
				array = (Attribute[])ReflectTypeDescriptionProvider._attributeCache[member];
				if (array == null)
				{
					object[] customAttributes = member.GetCustomAttributes(typeof(Attribute), false);
					array = new Attribute[customAttributes.Length];
					customAttributes.CopyTo(array, 0);
					ReflectTypeDescriptionProvider._attributeCache[member] = array;
				}
			}
			return array;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00047F3C File Offset: 0x0004613C
		private static EventDescriptor[] ReflectGetEvents(Type type)
		{
			object obj;
			if (ReflectTypeDescriptionProvider._eventCache == null)
			{
				obj = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (obj)
				{
					if (ReflectTypeDescriptionProvider._eventCache == null)
					{
						ReflectTypeDescriptionProvider._eventCache = new Hashtable();
					}
				}
			}
			EventDescriptor[] array = (EventDescriptor[])ReflectTypeDescriptionProvider._eventCache[type];
			if (array != null)
			{
				return array;
			}
			obj = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (obj)
			{
				array = (EventDescriptor[])ReflectTypeDescriptionProvider._eventCache[type];
				if (array == null)
				{
					BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
					EventInfo[] events = type.GetEvents(bindingFlags);
					array = new EventDescriptor[events.Length];
					int num = 0;
					foreach (EventInfo eventInfo in events)
					{
						if (eventInfo.DeclaringType.IsPublic || eventInfo.DeclaringType.IsNestedPublic || !(eventInfo.DeclaringType.Assembly == typeof(ReflectTypeDescriptionProvider).Assembly))
						{
							MethodInfo addMethod = eventInfo.GetAddMethod();
							MethodInfo removeMethod = eventInfo.GetRemoveMethod();
							if (addMethod != null && removeMethod != null)
							{
								array[num++] = new ReflectEventDescriptor(type, eventInfo);
							}
						}
					}
					if (num != array.Length)
					{
						EventDescriptor[] array2 = new EventDescriptor[num];
						Array.Copy(array, 0, array2, 0, num);
						array = array2;
					}
					ReflectTypeDescriptionProvider._eventCache[type] = array;
				}
			}
			return array;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x000480C4 File Offset: 0x000462C4
		private static PropertyDescriptor[] ReflectGetExtendedProperties(IExtenderProvider provider)
		{
			IDictionary cache = TypeDescriptor.GetCache(provider);
			PropertyDescriptor[] array;
			if (cache != null)
			{
				array = cache[ReflectTypeDescriptionProvider._extenderProviderPropertiesKey] as PropertyDescriptor[];
				if (array != null)
				{
					return array;
				}
			}
			if (ReflectTypeDescriptionProvider._extendedPropertyCache == null)
			{
				object obj = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (obj)
				{
					if (ReflectTypeDescriptionProvider._extendedPropertyCache == null)
					{
						ReflectTypeDescriptionProvider._extendedPropertyCache = new Hashtable();
					}
				}
			}
			Type type = provider.GetType();
			ReflectPropertyDescriptor[] array2 = (ReflectPropertyDescriptor[])ReflectTypeDescriptionProvider._extendedPropertyCache[type];
			if (array2 == null)
			{
				object obj = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (obj)
				{
					array2 = (ReflectPropertyDescriptor[])ReflectTypeDescriptionProvider._extendedPropertyCache[type];
					if (array2 == null)
					{
						AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
						ArrayList arrayList = new ArrayList(attributes.Count);
						foreach (object obj2 in attributes)
						{
							ProvidePropertyAttribute providePropertyAttribute = ((Attribute)obj2) as ProvidePropertyAttribute;
							if (providePropertyAttribute != null)
							{
								Type typeFromName = ReflectTypeDescriptionProvider.GetTypeFromName(providePropertyAttribute.ReceiverTypeName);
								if (typeFromName != null)
								{
									MethodInfo method = type.GetMethod("Get" + providePropertyAttribute.PropertyName, new Type[] { typeFromName });
									if (method != null && !method.IsStatic && method.IsPublic)
									{
										MethodInfo methodInfo = type.GetMethod("Set" + providePropertyAttribute.PropertyName, new Type[] { typeFromName, method.ReturnType });
										if (methodInfo != null && (methodInfo.IsStatic || !methodInfo.IsPublic))
										{
											methodInfo = null;
										}
										arrayList.Add(new ReflectPropertyDescriptor(type, providePropertyAttribute.PropertyName, method.ReturnType, typeFromName, method, methodInfo, null));
									}
								}
							}
						}
						array2 = new ReflectPropertyDescriptor[arrayList.Count];
						arrayList.CopyTo(array2, 0);
						ReflectTypeDescriptionProvider._extendedPropertyCache[type] = array2;
					}
				}
			}
			array = new PropertyDescriptor[array2.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				Attribute[] array3 = null;
				IComponent component = provider as IComponent;
				if (component == null || component.Site == null)
				{
					array3 = new Attribute[] { DesignOnlyAttribute.Yes };
				}
				ReflectPropertyDescriptor reflectPropertyDescriptor = array2[i];
				ExtendedPropertyDescriptor extendedPropertyDescriptor = new ExtendedPropertyDescriptor(reflectPropertyDescriptor, reflectPropertyDescriptor.ExtenderGetReceiverType(), provider, array3);
				array[i] = extendedPropertyDescriptor;
			}
			if (cache != null)
			{
				cache[ReflectTypeDescriptionProvider._extenderProviderPropertiesKey] = array;
			}
			return array;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0004839C File Offset: 0x0004659C
		private static PropertyDescriptor[] ReflectGetProperties(Type type)
		{
			object obj;
			if (ReflectTypeDescriptionProvider._propertyCache == null)
			{
				obj = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (obj)
				{
					if (ReflectTypeDescriptionProvider._propertyCache == null)
					{
						ReflectTypeDescriptionProvider._propertyCache = new Hashtable();
					}
				}
			}
			PropertyDescriptor[] array = (PropertyDescriptor[])ReflectTypeDescriptionProvider._propertyCache[type];
			if (array != null)
			{
				return array;
			}
			obj = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (obj)
			{
				array = (PropertyDescriptor[])ReflectTypeDescriptionProvider._propertyCache[type];
				if (array == null)
				{
					BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
					PropertyInfo[] properties = type.GetProperties(bindingFlags);
					array = new PropertyDescriptor[properties.Length];
					int num = 0;
					foreach (PropertyInfo propertyInfo in properties)
					{
						if (propertyInfo.GetIndexParameters().Length == 0)
						{
							MethodInfo getMethod = propertyInfo.GetGetMethod();
							MethodInfo setMethod = propertyInfo.GetSetMethod();
							string name = propertyInfo.Name;
							if (getMethod != null)
							{
								array[num++] = new ReflectPropertyDescriptor(type, name, propertyInfo.PropertyType, propertyInfo, getMethod, setMethod, null);
							}
						}
					}
					if (num != array.Length)
					{
						PropertyDescriptor[] array2 = new PropertyDescriptor[num];
						Array.Copy(array, 0, array2, 0, num);
						array = array2;
					}
					ReflectTypeDescriptionProvider._propertyCache[type] = array;
				}
			}
			return array;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x000484FC File Offset: 0x000466FC
		internal void Refresh(Type type)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, false);
			if (typeData != null)
			{
				typeData.Refresh();
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0004851C File Offset: 0x0004671C
		private static object SearchIntrinsicTable(Hashtable table, Type callingType)
		{
			object obj = null;
			lock (table)
			{
				Type type = callingType;
				while (type != null && type != typeof(object))
				{
					obj = table[type];
					string text = obj as string;
					if (text != null)
					{
						obj = Type.GetType(text);
						if (obj != null)
						{
							table[type] = obj;
						}
					}
					if (obj != null)
					{
						break;
					}
					type = type.BaseType;
				}
				if (obj == null)
				{
					foreach (object obj2 in table)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
						Type type2 = dictionaryEntry.Key as Type;
						if (type2 != null && type2.IsInterface && type2.IsAssignableFrom(callingType))
						{
							obj = dictionaryEntry.Value;
							string text2 = obj as string;
							if (text2 != null)
							{
								obj = Type.GetType(text2);
								if (obj != null)
								{
									table[callingType] = obj;
								}
							}
							if (obj != null)
							{
								break;
							}
						}
					}
				}
				if (obj == null)
				{
					if (callingType.IsGenericType && callingType.GetGenericTypeDefinition() == typeof(Nullable<>))
					{
						obj = table[ReflectTypeDescriptionProvider._intrinsicNullableKey];
					}
					else if (callingType.IsInterface)
					{
						obj = table[ReflectTypeDescriptionProvider._intrinsicReferenceKey];
					}
				}
				if (obj == null)
				{
					obj = table[typeof(object)];
				}
				Type type3 = obj as Type;
				if (type3 != null)
				{
					obj = ReflectTypeDescriptionProvider.CreateInstance(type3, callingType);
					if (type3.GetConstructor(ReflectTypeDescriptionProvider._typeConstructor) == null)
					{
						table[callingType] = obj;
					}
				}
			}
			return obj;
		}

		// Token: 0x04000A9B RID: 2715
		private Hashtable _typeData;

		// Token: 0x04000A9C RID: 2716
		private static Type[] _typeConstructor = new Type[] { typeof(Type) };

		// Token: 0x04000A9D RID: 2717
		private static volatile Hashtable _editorTables;

		// Token: 0x04000A9E RID: 2718
		private static volatile Hashtable _intrinsicTypeConverters;

		// Token: 0x04000A9F RID: 2719
		private static object _intrinsicReferenceKey = new object();

		// Token: 0x04000AA0 RID: 2720
		private static object _intrinsicNullableKey = new object();

		// Token: 0x04000AA1 RID: 2721
		private static object _dictionaryKey = new object();

		// Token: 0x04000AA2 RID: 2722
		private static volatile Hashtable _propertyCache;

		// Token: 0x04000AA3 RID: 2723
		private static volatile Hashtable _eventCache;

		// Token: 0x04000AA4 RID: 2724
		private static volatile Hashtable _attributeCache;

		// Token: 0x04000AA5 RID: 2725
		private static volatile Hashtable _extendedPropertyCache;

		// Token: 0x04000AA6 RID: 2726
		private static readonly Guid _extenderProviderKey = Guid.NewGuid();

		// Token: 0x04000AA7 RID: 2727
		private static readonly Guid _extenderPropertiesKey = Guid.NewGuid();

		// Token: 0x04000AA8 RID: 2728
		private static readonly Guid _extenderProviderPropertiesKey = Guid.NewGuid();

		// Token: 0x04000AA9 RID: 2729
		private static readonly Type[] _skipInterfaceAttributeList = new Type[]
		{
			typeof(GuidAttribute),
			typeof(ComVisibleAttribute),
			typeof(InterfaceTypeAttribute)
		};

		// Token: 0x04000AAA RID: 2730
		private static object _internalSyncObject = new object();

		// Token: 0x020002C5 RID: 709
		private class ReflectedTypeData
		{
			// Token: 0x06001108 RID: 4360 RVA: 0x00048789 File Offset: 0x00046989
			internal ReflectedTypeData(Type type)
			{
				this._type = type;
			}

			// Token: 0x1700039C RID: 924
			// (get) Token: 0x06001109 RID: 4361 RVA: 0x00048798 File Offset: 0x00046998
			internal bool IsPopulated
			{
				get
				{
					return (this._attributes != null) | (this._events != null) | (this._properties != null);
				}
			}

			// Token: 0x0600110A RID: 4362 RVA: 0x000487B8 File Offset: 0x000469B8
			internal AttributeCollection GetAttributes()
			{
				if (this._attributes == null)
				{
					Attribute[] array = ReflectTypeDescriptionProvider.ReflectGetAttributes(this._type);
					Type type = this._type.BaseType;
					while (type != null && type != typeof(object))
					{
						Attribute[] array2 = ReflectTypeDescriptionProvider.ReflectGetAttributes(type);
						Attribute[] array3 = new Attribute[array.Length + array2.Length];
						Array.Copy(array, 0, array3, 0, array.Length);
						Array.Copy(array2, 0, array3, array.Length, array2.Length);
						array = array3;
						type = type.BaseType;
					}
					int num = array.Length;
					foreach (Type type2 in this._type.GetInterfaces())
					{
						if ((type2.Attributes & TypeAttributes.NestedPrivate) != TypeAttributes.NotPublic)
						{
							AttributeCollection attributes = TypeDescriptor.GetAttributes(type2);
							if (attributes.Count > 0)
							{
								Attribute[] array4 = new Attribute[array.Length + attributes.Count];
								Array.Copy(array, 0, array4, 0, array.Length);
								attributes.CopyTo(array4, array.Length);
								array = array4;
							}
						}
					}
					OrderedDictionary orderedDictionary = new OrderedDictionary(array.Length);
					for (int j = 0; j < array.Length; j++)
					{
						bool flag = true;
						if (j >= num)
						{
							for (int k = 0; k < ReflectTypeDescriptionProvider._skipInterfaceAttributeList.Length; k++)
							{
								if (ReflectTypeDescriptionProvider._skipInterfaceAttributeList[k].IsInstanceOfType(array[j]))
								{
									flag = false;
									break;
								}
							}
						}
						if (flag && !orderedDictionary.Contains(array[j].TypeId))
						{
							orderedDictionary[array[j].TypeId] = array[j];
						}
					}
					array = new Attribute[orderedDictionary.Count];
					orderedDictionary.Values.CopyTo(array, 0);
					this._attributes = new AttributeCollection(array);
				}
				return this._attributes;
			}

			// Token: 0x0600110B RID: 4363 RVA: 0x00048961 File Offset: 0x00046B61
			internal string GetClassName(object instance)
			{
				return this._type.FullName;
			}

			// Token: 0x0600110C RID: 4364 RVA: 0x00048970 File Offset: 0x00046B70
			internal string GetComponentName(object instance)
			{
				IComponent component = instance as IComponent;
				if (component != null)
				{
					ISite site = component.Site;
					if (site != null)
					{
						INestedSite nestedSite = site as INestedSite;
						if (nestedSite != null)
						{
							return nestedSite.FullName;
						}
						return site.Name;
					}
				}
				return null;
			}

			// Token: 0x0600110D RID: 4365 RVA: 0x000489AC File Offset: 0x00046BAC
			internal TypeConverter GetConverter(object instance)
			{
				TypeConverterAttribute typeConverterAttribute = null;
				if (instance != null)
				{
					typeConverterAttribute = (TypeConverterAttribute)TypeDescriptor.GetAttributes(this._type)[typeof(TypeConverterAttribute)];
					TypeConverterAttribute typeConverterAttribute2 = (TypeConverterAttribute)TypeDescriptor.GetAttributes(instance)[typeof(TypeConverterAttribute)];
					if (typeConverterAttribute != typeConverterAttribute2)
					{
						Type typeFromName = this.GetTypeFromName(typeConverterAttribute2.ConverterTypeName);
						if (typeFromName != null && typeof(TypeConverter).IsAssignableFrom(typeFromName))
						{
							return (TypeConverter)ReflectTypeDescriptionProvider.CreateInstance(typeFromName, this._type);
						}
					}
				}
				if (this._converter == null)
				{
					if (typeConverterAttribute == null)
					{
						typeConverterAttribute = (TypeConverterAttribute)TypeDescriptor.GetAttributes(this._type)[typeof(TypeConverterAttribute)];
					}
					if (typeConverterAttribute != null)
					{
						Type typeFromName2 = this.GetTypeFromName(typeConverterAttribute.ConverterTypeName);
						if (typeFromName2 != null && typeof(TypeConverter).IsAssignableFrom(typeFromName2))
						{
							this._converter = (TypeConverter)ReflectTypeDescriptionProvider.CreateInstance(typeFromName2, this._type);
						}
					}
					if (this._converter == null)
					{
						this._converter = (TypeConverter)ReflectTypeDescriptionProvider.SearchIntrinsicTable(ReflectTypeDescriptionProvider.IntrinsicTypeConverters, this._type);
					}
				}
				return this._converter;
			}

			// Token: 0x0600110E RID: 4366 RVA: 0x00048AD0 File Offset: 0x00046CD0
			internal EventDescriptor GetDefaultEvent(object instance)
			{
				AttributeCollection attributeCollection;
				if (instance != null)
				{
					attributeCollection = TypeDescriptor.GetAttributes(instance);
				}
				else
				{
					attributeCollection = TypeDescriptor.GetAttributes(this._type);
				}
				DefaultEventAttribute defaultEventAttribute = (DefaultEventAttribute)attributeCollection[typeof(DefaultEventAttribute)];
				if (defaultEventAttribute == null || defaultEventAttribute.Name == null)
				{
					return null;
				}
				if (instance != null)
				{
					return TypeDescriptor.GetEvents(instance)[defaultEventAttribute.Name];
				}
				return TypeDescriptor.GetEvents(this._type)[defaultEventAttribute.Name];
			}

			// Token: 0x0600110F RID: 4367 RVA: 0x00048B44 File Offset: 0x00046D44
			internal PropertyDescriptor GetDefaultProperty(object instance)
			{
				AttributeCollection attributeCollection;
				if (instance != null)
				{
					attributeCollection = TypeDescriptor.GetAttributes(instance);
				}
				else
				{
					attributeCollection = TypeDescriptor.GetAttributes(this._type);
				}
				DefaultPropertyAttribute defaultPropertyAttribute = (DefaultPropertyAttribute)attributeCollection[typeof(DefaultPropertyAttribute)];
				if (defaultPropertyAttribute == null || defaultPropertyAttribute.Name == null)
				{
					return null;
				}
				if (instance != null)
				{
					return TypeDescriptor.GetProperties(instance)[defaultPropertyAttribute.Name];
				}
				return TypeDescriptor.GetProperties(this._type)[defaultPropertyAttribute.Name];
			}

			// Token: 0x06001110 RID: 4368 RVA: 0x00048BB8 File Offset: 0x00046DB8
			internal object GetEditor(object instance, Type editorBaseType)
			{
				EditorAttribute editorAttribute;
				if (instance != null)
				{
					editorAttribute = ReflectTypeDescriptionProvider.ReflectedTypeData.GetEditorAttribute(TypeDescriptor.GetAttributes(this._type), editorBaseType);
					EditorAttribute editorAttribute2 = ReflectTypeDescriptionProvider.ReflectedTypeData.GetEditorAttribute(TypeDescriptor.GetAttributes(instance), editorBaseType);
					if (editorAttribute != editorAttribute2)
					{
						Type typeFromName = this.GetTypeFromName(editorAttribute2.EditorTypeName);
						if (typeFromName != null && editorBaseType.IsAssignableFrom(typeFromName))
						{
							return ReflectTypeDescriptionProvider.CreateInstance(typeFromName, this._type);
						}
					}
				}
				ReflectTypeDescriptionProvider.ReflectedTypeData reflectedTypeData = this;
				lock (reflectedTypeData)
				{
					for (int i = 0; i < this._editorCount; i++)
					{
						if (this._editorTypes[i] == editorBaseType)
						{
							return this._editors[i];
						}
					}
				}
				object obj = null;
				editorAttribute = ReflectTypeDescriptionProvider.ReflectedTypeData.GetEditorAttribute(TypeDescriptor.GetAttributes(this._type), editorBaseType);
				if (editorAttribute != null)
				{
					Type typeFromName2 = this.GetTypeFromName(editorAttribute.EditorTypeName);
					if (typeFromName2 != null && editorBaseType.IsAssignableFrom(typeFromName2))
					{
						obj = ReflectTypeDescriptionProvider.CreateInstance(typeFromName2, this._type);
					}
				}
				if (obj == null)
				{
					Hashtable editorTable = ReflectTypeDescriptionProvider.GetEditorTable(editorBaseType);
					if (editorTable != null)
					{
						obj = ReflectTypeDescriptionProvider.SearchIntrinsicTable(editorTable, this._type);
					}
					if (obj != null && !editorBaseType.IsInstanceOfType(obj))
					{
						obj = null;
					}
				}
				if (obj != null)
				{
					reflectedTypeData = this;
					lock (reflectedTypeData)
					{
						if (this._editorTypes == null || this._editorTypes.Length == this._editorCount)
						{
							int num = ((this._editorTypes == null) ? 4 : (this._editorTypes.Length * 2));
							Type[] array = new Type[num];
							object[] array2 = new object[num];
							if (this._editorTypes != null)
							{
								this._editorTypes.CopyTo(array, 0);
								this._editors.CopyTo(array2, 0);
							}
							this._editorTypes = array;
							this._editors = array2;
							this._editorTypes[this._editorCount] = editorBaseType;
							object[] editors = this._editors;
							int editorCount = this._editorCount;
							this._editorCount = editorCount + 1;
							editors[editorCount] = obj;
						}
					}
				}
				return obj;
			}

			// Token: 0x06001111 RID: 4369 RVA: 0x00048DBC File Offset: 0x00046FBC
			private static EditorAttribute GetEditorAttribute(AttributeCollection attributes, Type editorBaseType)
			{
				foreach (object obj in attributes)
				{
					EditorAttribute editorAttribute = ((Attribute)obj) as EditorAttribute;
					if (editorAttribute != null)
					{
						Type type = Type.GetType(editorAttribute.EditorBaseTypeName);
						if (type != null && type == editorBaseType)
						{
							return editorAttribute;
						}
					}
				}
				return null;
			}

			// Token: 0x06001112 RID: 4370 RVA: 0x00048E3C File Offset: 0x0004703C
			internal EventDescriptorCollection GetEvents()
			{
				if (this._events == null)
				{
					Dictionary<string, EventDescriptor> dictionary = new Dictionary<string, EventDescriptor>(16);
					Type type = this._type;
					Type typeFromHandle = typeof(object);
					EventDescriptor[] array;
					do
					{
						array = ReflectTypeDescriptionProvider.ReflectGetEvents(type);
						foreach (EventDescriptor eventDescriptor in array)
						{
							if (!dictionary.ContainsKey(eventDescriptor.Name))
							{
								dictionary.Add(eventDescriptor.Name, eventDescriptor);
							}
						}
						type = type.BaseType;
					}
					while (type != null && type != typeFromHandle);
					array = new EventDescriptor[dictionary.Count];
					dictionary.Values.CopyTo(array, 0);
					this._events = new EventDescriptorCollection(array, true);
				}
				return this._events;
			}

			// Token: 0x06001113 RID: 4371 RVA: 0x00048EF8 File Offset: 0x000470F8
			internal PropertyDescriptorCollection GetProperties()
			{
				if (this._properties == null)
				{
					Dictionary<string, PropertyDescriptor> dictionary = new Dictionary<string, PropertyDescriptor>(10);
					Type type = this._type;
					Type typeFromHandle = typeof(object);
					PropertyDescriptor[] array;
					do
					{
						array = ReflectTypeDescriptionProvider.ReflectGetProperties(type);
						foreach (PropertyDescriptor propertyDescriptor in array)
						{
							if (!dictionary.ContainsKey(propertyDescriptor.Name))
							{
								dictionary.Add(propertyDescriptor.Name, propertyDescriptor);
							}
						}
						type = type.BaseType;
					}
					while (type != null && type != typeFromHandle);
					array = new PropertyDescriptor[dictionary.Count];
					dictionary.Values.CopyTo(array, 0);
					this._properties = new PropertyDescriptorCollection(array, true);
				}
				return this._properties;
			}

			// Token: 0x06001114 RID: 4372 RVA: 0x00048FB4 File Offset: 0x000471B4
			private Type GetTypeFromName(string typeName)
			{
				if (typeName == null || typeName.Length == 0)
				{
					return null;
				}
				int num = typeName.IndexOf(',');
				Type type = null;
				if (num == -1)
				{
					type = this._type.Assembly.GetType(typeName);
				}
				if (type == null)
				{
					type = Type.GetType(typeName);
				}
				if (type == null && num != -1)
				{
					type = Type.GetType(typeName.Substring(0, num));
				}
				return type;
			}

			// Token: 0x06001115 RID: 4373 RVA: 0x0004901B File Offset: 0x0004721B
			internal void Refresh()
			{
				this._attributes = null;
				this._events = null;
				this._properties = null;
				this._converter = null;
				this._editors = null;
				this._editorTypes = null;
				this._editorCount = 0;
			}

			// Token: 0x04000AAB RID: 2731
			private Type _type;

			// Token: 0x04000AAC RID: 2732
			private AttributeCollection _attributes;

			// Token: 0x04000AAD RID: 2733
			private EventDescriptorCollection _events;

			// Token: 0x04000AAE RID: 2734
			private PropertyDescriptorCollection _properties;

			// Token: 0x04000AAF RID: 2735
			private TypeConverter _converter;

			// Token: 0x04000AB0 RID: 2736
			private object[] _editors;

			// Token: 0x04000AB1 RID: 2737
			private Type[] _editorTypes;

			// Token: 0x04000AB2 RID: 2738
			private int _editorCount;
		}
	}
}
