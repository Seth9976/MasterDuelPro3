using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Unity.Properties.Internal
{
	// Token: 0x0200009E RID: 158
	internal static class PropertyBagStore
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000A9D5 File Offset: 0x00008BD5
		private static ReflectedPropertyBagProvider ReflectedPropertyBagProvider
		{
			get
			{
				ReflectedPropertyBagProvider reflectedPropertyBagProvider;
				if ((reflectedPropertyBagProvider = PropertyBagStore.s_PropertyBagProvider) == null)
				{
					reflectedPropertyBagProvider = (PropertyBagStore.s_PropertyBagProvider = new ReflectedPropertyBagProvider());
				}
				return reflectedPropertyBagProvider;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000A9EB File Offset: 0x00008BEB
		internal static void CreatePropertyBagProvider()
		{
			PropertyBagStore.s_PropertyBagProvider = new ReflectedPropertyBagProvider();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		internal static void AddPropertyBag<TContainer>(IPropertyBag<TContainer> propertyBag)
		{
			bool flag = !TypeTraits<TContainer>.IsContainer;
			if (flag)
			{
				throw new Exception(string.Format("PropertyBagStore Type=[{0}] is not a valid container type. Type can not be primitive, enum or string.", typeof(TContainer)));
			}
			bool isAbstractOrInterface = TypeTraits<TContainer>.IsAbstractOrInterface;
			if (isAbstractOrInterface)
			{
				throw new Exception(string.Format("PropertyBagStore Type=[{0}] is not a valid container type. Type can not be abstract or interface.", typeof(TContainer)));
			}
			bool flag2 = PropertyBagStore.TypedStore<TContainer>.PropertyBag != null;
			if (flag2)
			{
				IPropertyBag<TContainer> currentPropertyBag = PropertyBagStore.TypedStore<TContainer>.PropertyBag;
				bool flag3 = currentPropertyBag.GetType().Assembly == typeof(TContainer).Assembly;
				if (flag3)
				{
					return;
				}
				bool flag4 = propertyBag.GetType().GetCustomAttributes<CompilerGeneratedAttribute>().Any<CompilerGeneratedAttribute>();
				if (flag4)
				{
					bool flag5 = propertyBag.GetType().Assembly != typeof(TContainer).Assembly;
					if (flag5)
					{
						return;
					}
				}
			}
			PropertyBagStore.TypedStore<TContainer>.PropertyBag = propertyBag;
			bool flag6 = !PropertyBagStore.s_PropertyBags.ContainsKey(typeof(TContainer));
			if (flag6)
			{
				PropertyBagStore.s_RegisteredTypes.Add(typeof(TContainer));
			}
			PropertyBagStore.s_PropertyBags[typeof(TContainer)] = propertyBag;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000AB20 File Offset: 0x00008D20
		internal static IPropertyBag<TContainer> GetPropertyBag<TContainer>()
		{
			bool flag = PropertyBagStore.TypedStore<TContainer>.PropertyBag != null;
			IPropertyBag<TContainer> propertyBag;
			if (flag)
			{
				propertyBag = PropertyBagStore.TypedStore<TContainer>.PropertyBag;
			}
			else
			{
				IPropertyBag untyped = PropertyBagStore.GetPropertyBag(typeof(TContainer));
				bool flag2 = untyped == null;
				if (flag2)
				{
					propertyBag = null;
				}
				else
				{
					IPropertyBag<TContainer> typed = untyped as IPropertyBag<TContainer>;
					bool flag3 = typed == null;
					if (flag3)
					{
						throw new InvalidOperationException("PropertyBag type container type mismatch.");
					}
					propertyBag = typed;
				}
			}
			return propertyBag;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000AB88 File Offset: 0x00008D88
		internal static IPropertyBag GetPropertyBag(Type type)
		{
			IPropertyBag propertyBag;
			bool flag = PropertyBagStore.s_PropertyBags.TryGetValue(type, out propertyBag);
			IPropertyBag propertyBag2;
			if (flag)
			{
				propertyBag2 = propertyBag;
			}
			else
			{
				bool flag2 = !TypeTraits.IsContainer(type);
				if (flag2)
				{
					propertyBag2 = null;
				}
				else
				{
					bool flag3 = type.IsArray && type.GetArrayRank() != 1;
					if (flag3)
					{
						propertyBag2 = null;
					}
					else
					{
						bool flag4 = type.IsInterface || type.IsAbstract;
						if (flag4)
						{
							propertyBag2 = null;
						}
						else
						{
							bool flag5 = type == typeof(object);
							if (flag5)
							{
								propertyBag2 = null;
							}
							else
							{
								propertyBag = PropertyBagStore.ReflectedPropertyBagProvider.CreatePropertyBag(type);
								bool flag6 = propertyBag == null;
								if (flag6)
								{
									PropertyBagStore.s_PropertyBags.TryAdd(type, null);
									propertyBag2 = null;
								}
								else
								{
									IPropertyBagRegister propertyBagRegister = propertyBag as IPropertyBagRegister;
									if (propertyBagRegister != null)
									{
										propertyBagRegister.Register();
									}
									propertyBag2 = propertyBag;
								}
							}
						}
					}
				}
			}
			return propertyBag2;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000AC60 File Offset: 0x00008E60
		internal static bool TryGetPropertyBagForValue<TValue>(ref TValue value, out IPropertyBag propertyBag)
		{
			bool flag = !TypeTraits<TValue>.IsContainer;
			bool flag2;
			if (flag)
			{
				propertyBag = null;
				flag2 = false;
			}
			else
			{
				bool canBeNull = TypeTraits<TValue>.CanBeNull;
				if (canBeNull)
				{
					bool flag3 = EqualityComparer<TValue>.Default.Equals(value, default(TValue));
					if (flag3)
					{
						propertyBag = PropertyBagStore.GetPropertyBag<TValue>();
						return propertyBag != null;
					}
				}
				bool isValueType = TypeTraits<TValue>.IsValueType;
				if (isValueType)
				{
					propertyBag = PropertyBagStore.GetPropertyBag<TValue>();
					flag2 = propertyBag != null;
				}
				else
				{
					propertyBag = PropertyBagStore.GetPropertyBag(value.GetType());
					flag2 = propertyBag != null;
				}
			}
			return flag2;
		}

		// Token: 0x0400015D RID: 349
		private static readonly ConcurrentDictionary<Type, IPropertyBag> s_PropertyBags = new ConcurrentDictionary<Type, IPropertyBag>();

		// Token: 0x0400015E RID: 350
		private static readonly List<Type> s_RegisteredTypes = new List<Type>();

		// Token: 0x0400015F RID: 351
		private static ReflectedPropertyBagProvider s_PropertyBagProvider = null;

		// Token: 0x0200009F RID: 159
		internal struct TypedStore<TContainer>
		{
			// Token: 0x04000160 RID: 352
			public static IPropertyBag<TContainer> PropertyBag;
		}
	}
}
