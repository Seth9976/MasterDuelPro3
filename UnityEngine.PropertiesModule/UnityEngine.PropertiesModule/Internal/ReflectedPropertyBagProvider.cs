using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Scripting;

namespace Unity.Properties.Internal
{
	// Token: 0x020000A2 RID: 162
	internal class ReflectedPropertyBagProvider
	{
		// Token: 0x0600032B RID: 811 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		public ReflectedPropertyBagProvider()
		{
			this.m_CreatePropertyMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateProperty", BindingFlags.Instance | BindingFlags.NonPublic);
			this.m_CreatePropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethods(BindingFlags.Instance | BindingFlags.Public).First((MethodInfo x) => x.Name == "CreatePropertyBag" && x.IsGenericMethod);
			this.m_CreateIndexedCollectionPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateIndexedCollectionPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
			this.m_CreateSetPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateSetPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
			this.m_CreateKeyValueCollectionPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateKeyValueCollectionPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
			this.m_CreateKeyValuePairPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateKeyValuePairPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
			this.m_CreateArrayPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateArrayPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
			this.m_CreateListPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateListPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
			this.m_CreateHashSetPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateHashSetPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
			this.m_CreateDictionaryPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateDictionaryPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000AF2C File Offset: 0x0000912C
		public IPropertyBag CreatePropertyBag(Type type)
		{
			bool isGenericTypeDefinition = type.IsGenericTypeDefinition;
			IPropertyBag propertyBag;
			if (isGenericTypeDefinition)
			{
				propertyBag = null;
			}
			else
			{
				propertyBag = (IPropertyBag)this.m_CreatePropertyBagMethod.MakeGenericMethod(new Type[] { type }).Invoke(this, null);
			}
			return propertyBag;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000AF70 File Offset: 0x00009170
		public IPropertyBag<TContainer> CreatePropertyBag<TContainer>()
		{
			bool flag = !TypeTraits<TContainer>.IsContainer || TypeTraits<TContainer>.IsObject;
			if (flag)
			{
				throw new InvalidOperationException("Invalid container type.");
			}
			bool isArray = typeof(TContainer).IsArray;
			IPropertyBag<TContainer> propertyBag2;
			if (isArray)
			{
				bool flag2 = typeof(TContainer).GetArrayRank() != 1;
				if (flag2)
				{
					throw new InvalidOperationException("Properties does not support multidimensional arrays.");
				}
				propertyBag2 = (IPropertyBag<TContainer>)this.m_CreateArrayPropertyBagMethod.MakeGenericMethod(new Type[] { typeof(TContainer).GetElementType() }).Invoke(this, new object[0]);
			}
			else
			{
				bool flag3 = typeof(TContainer).IsGenericType && typeof(TContainer).GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
				if (flag3)
				{
					propertyBag2 = (IPropertyBag<TContainer>)this.m_CreateListPropertyBagMethod.MakeGenericMethod(new Type[] { typeof(TContainer).GetGenericArguments().First<Type>() }).Invoke(this, new object[0]);
				}
				else
				{
					bool flag4 = typeof(TContainer).IsGenericType && typeof(TContainer).GetGenericTypeDefinition().IsAssignableFrom(typeof(HashSet<>));
					if (flag4)
					{
						propertyBag2 = (IPropertyBag<TContainer>)this.m_CreateHashSetPropertyBagMethod.MakeGenericMethod(new Type[] { typeof(TContainer).GetGenericArguments().First<Type>() }).Invoke(this, new object[0]);
					}
					else
					{
						bool flag5 = typeof(TContainer).IsGenericType && typeof(TContainer).GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<, >));
						if (flag5)
						{
							propertyBag2 = (IPropertyBag<TContainer>)this.m_CreateDictionaryPropertyBagMethod.MakeGenericMethod(new Type[]
							{
								typeof(TContainer).GetGenericArguments().First<Type>(),
								typeof(TContainer).GetGenericArguments().ElementAt(1)
							}).Invoke(this, new object[0]);
						}
						else
						{
							bool flag6 = typeof(TContainer).IsGenericType && typeof(TContainer).GetGenericTypeDefinition().IsAssignableFrom(typeof(IList<>));
							if (flag6)
							{
								propertyBag2 = (IPropertyBag<TContainer>)this.m_CreateIndexedCollectionPropertyBagMethod.MakeGenericMethod(new Type[]
								{
									typeof(TContainer),
									typeof(TContainer).GetGenericArguments().First<Type>()
								}).Invoke(this, new object[0]);
							}
							else
							{
								bool flag7 = typeof(TContainer).IsGenericType && typeof(TContainer).GetGenericTypeDefinition().IsAssignableFrom(typeof(ISet<>));
								if (flag7)
								{
									propertyBag2 = (IPropertyBag<TContainer>)this.m_CreateSetPropertyBagMethod.MakeGenericMethod(new Type[]
									{
										typeof(TContainer),
										typeof(TContainer).GetGenericArguments().First<Type>()
									}).Invoke(this, new object[0]);
								}
								else
								{
									bool flag8 = typeof(TContainer).IsGenericType && typeof(TContainer).GetGenericTypeDefinition().IsAssignableFrom(typeof(IDictionary<, >));
									if (flag8)
									{
										propertyBag2 = (IPropertyBag<TContainer>)this.m_CreateKeyValueCollectionPropertyBagMethod.MakeGenericMethod(new Type[]
										{
											typeof(TContainer),
											typeof(TContainer).GetGenericArguments().First<Type>(),
											typeof(TContainer).GetGenericArguments().ElementAt(1)
										}).Invoke(this, new object[0]);
									}
									else
									{
										bool flag9 = typeof(TContainer).IsGenericType && typeof(TContainer).GetGenericTypeDefinition().IsAssignableFrom(typeof(KeyValuePair<, >));
										if (flag9)
										{
											Type[] types = typeof(TContainer).GetGenericArguments().ToArray<Type>();
											propertyBag2 = (IPropertyBag<TContainer>)this.m_CreateKeyValuePairPropertyBagMethod.MakeGenericMethod(new Type[]
											{
												types[0],
												types[1]
											}).Invoke(this, new object[0]);
										}
										else
										{
											ReflectedPropertyBag<TContainer> propertyBag = new ReflectedPropertyBag<TContainer>();
											foreach (MemberInfo member in ReflectedPropertyBagProvider.GetPropertyMembers(typeof(TContainer)))
											{
												MemberInfo memberInfo = member;
												MemberInfo memberInfo2 = memberInfo;
												FieldInfo field = memberInfo2 as FieldInfo;
												IMemberInfo info;
												if (field == null)
												{
													PropertyInfo property = memberInfo2 as PropertyInfo;
													if (property == null)
													{
														throw new InvalidOperationException();
													}
													info = new PropertyMember(property);
												}
												else
												{
													info = new FieldMember(field);
												}
												this.m_CreatePropertyMethod.MakeGenericMethod(new Type[]
												{
													typeof(TContainer),
													info.ValueType
												}).Invoke(this, new object[] { info, propertyBag });
											}
											propertyBag2 = propertyBag;
										}
									}
								}
							}
						}
					}
				}
			}
			return propertyBag2;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000B4AC File Offset: 0x000096AC
		[Preserve]
		private void CreateProperty<TContainer, TValue>(IMemberInfo member, ReflectedPropertyBag<TContainer> propertyBag)
		{
			bool isPointer = typeof(TValue).IsPointer;
			if (!isPointer)
			{
				propertyBag.AddProperty<TValue>(new ReflectedMemberProperty<TContainer, TValue>(member, member.Name));
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000B4E3 File Offset: 0x000096E3
		[Preserve]
		private IPropertyBag<TList> CreateIndexedCollectionPropertyBag<TList, TElement>() where TList : IList<TElement>
		{
			return new IndexedCollectionPropertyBag<TList, TElement>();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B4EA File Offset: 0x000096EA
		[Preserve]
		private IPropertyBag<TSet> CreateSetPropertyBag<TSet, TValue>() where TSet : ISet<TValue>
		{
			return new SetPropertyBagBase<TSet, TValue>();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000B4F1 File Offset: 0x000096F1
		[Preserve]
		private IPropertyBag<TDictionary> CreateKeyValueCollectionPropertyBag<TDictionary, TKey, TValue>() where TDictionary : IDictionary<TKey, TValue>
		{
			return new KeyValueCollectionPropertyBag<TDictionary, TKey, TValue>();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000B4F8 File Offset: 0x000096F8
		[Preserve]
		private IPropertyBag<KeyValuePair<TKey, TValue>> CreateKeyValuePairPropertyBag<TKey, TValue>()
		{
			return new KeyValuePairPropertyBag<TKey, TValue>();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000B4FF File Offset: 0x000096FF
		[Preserve]
		private IPropertyBag<TElement[]> CreateArrayPropertyBag<TElement>()
		{
			return new ArrayPropertyBag<TElement>();
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000B506 File Offset: 0x00009706
		[Preserve]
		private IPropertyBag<List<TElement>> CreateListPropertyBag<TElement>()
		{
			return new ListPropertyBag<TElement>();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000B50D File Offset: 0x0000970D
		[Preserve]
		private IPropertyBag<HashSet<TElement>> CreateHashSetPropertyBag<TElement>()
		{
			return new HashSetPropertyBag<TElement>();
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000B514 File Offset: 0x00009714
		[Preserve]
		private IPropertyBag<Dictionary<TKey, TValue>> CreateDictionaryPropertyBag<TKey, TValue>()
		{
			return new DictionaryPropertyBag<TKey, TValue>();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000B51B File Offset: 0x0000971B
		private static IEnumerable<MemberInfo> GetPropertyMembers(Type type)
		{
			do
			{
				IOrderedEnumerable<MemberInfo> members = from x in type.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
					orderby x.MetadataToken
					select x;
				foreach (MemberInfo member in members)
				{
					bool flag = member.MemberType != MemberTypes.Field && member.MemberType != MemberTypes.Property;
					if (!flag)
					{
						bool flag2 = member.DeclaringType != type;
						if (!flag2)
						{
							bool flag3 = !ReflectedPropertyBagProvider.IsValidMember(member);
							if (!flag3)
							{
								bool hasDontCreatePropertyAttribute = member.GetCustomAttribute<DontCreatePropertyAttribute>() != null;
								bool hasCreatePropertyAttribute = member.GetCustomAttribute<CreatePropertyAttribute>() != null;
								bool hasNonSerializedAttribute = member.GetCustomAttribute<NonSerializedAttribute>() != null;
								bool hasSerializedFieldAttribute = member.GetCustomAttribute<SerializeField>() != null;
								bool hasSerializeReferenceAttribute = member.GetCustomAttribute<SerializeReference>() != null;
								bool flag4 = hasDontCreatePropertyAttribute;
								if (!flag4)
								{
									bool flag5 = hasCreatePropertyAttribute;
									if (flag5)
									{
										yield return member;
									}
									else
									{
										bool flag6 = hasNonSerializedAttribute;
										if (!flag6)
										{
											bool flag7 = hasSerializedFieldAttribute;
											if (flag7)
											{
												yield return member;
											}
											else
											{
												bool flag8 = hasSerializeReferenceAttribute;
												if (flag8)
												{
													yield return member;
												}
												else
												{
													FieldInfo field = member as FieldInfo;
													bool flag9 = field != null && field.IsPublic;
													if (flag9)
													{
														yield return member;
													}
													field = null;
													member = null;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				IEnumerator<MemberInfo> enumerator = null;
				type = type.BaseType;
				members = null;
			}
			while (type != null && type != typeof(object));
			yield break;
			yield break;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000B52C File Offset: 0x0000972C
		private static bool IsValidMember(MemberInfo memberInfo)
		{
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			bool flag;
			if (fieldInfo == null)
			{
				PropertyInfo propertyInfo = memberInfo as PropertyInfo;
				flag = propertyInfo != null && (null != propertyInfo.GetMethod && !propertyInfo.GetMethod.IsStatic) && ReflectedPropertyBagProvider.IsValidPropertyType(propertyInfo.PropertyType);
			}
			else
			{
				flag = !fieldInfo.IsStatic && ReflectedPropertyBagProvider.IsValidPropertyType(fieldInfo.FieldType);
			}
			return flag;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000B5A8 File Offset: 0x000097A8
		private static bool IsValidPropertyType(Type type)
		{
			bool isPointer = type.IsPointer;
			return !isPointer && (!type.IsGenericType || type.GetGenericArguments().All(new Func<Type, bool>(ReflectedPropertyBagProvider.IsValidPropertyType)));
		}

		// Token: 0x04000161 RID: 353
		private readonly MethodInfo m_CreatePropertyMethod;

		// Token: 0x04000162 RID: 354
		private readonly MethodInfo m_CreatePropertyBagMethod;

		// Token: 0x04000163 RID: 355
		private readonly MethodInfo m_CreateIndexedCollectionPropertyBagMethod;

		// Token: 0x04000164 RID: 356
		private readonly MethodInfo m_CreateSetPropertyBagMethod;

		// Token: 0x04000165 RID: 357
		private readonly MethodInfo m_CreateKeyValueCollectionPropertyBagMethod;

		// Token: 0x04000166 RID: 358
		private readonly MethodInfo m_CreateKeyValuePairPropertyBagMethod;

		// Token: 0x04000167 RID: 359
		private readonly MethodInfo m_CreateArrayPropertyBagMethod;

		// Token: 0x04000168 RID: 360
		private readonly MethodInfo m_CreateListPropertyBagMethod;

		// Token: 0x04000169 RID: 361
		private readonly MethodInfo m_CreateHashSetPropertyBagMethod;

		// Token: 0x0400016A RID: 362
		private readonly MethodInfo m_CreateDictionaryPropertyBagMethod;
	}
}
