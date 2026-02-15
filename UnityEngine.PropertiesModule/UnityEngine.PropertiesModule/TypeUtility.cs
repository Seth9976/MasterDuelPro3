using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Unity.Properties.Internal;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Scripting;

namespace Unity.Properties
{
	// Token: 0x02000066 RID: 102
	public static class TypeUtility
	{
		// Token: 0x06000239 RID: 569 RVA: 0x000096EC File Offset: 0x000078EC
		static TypeUtility()
		{
			TypeUtility.SetExplicitInstantiationMethod<string>(() => string.Empty);
			foreach (MethodInfo method in typeof(TypeUtility).GetMethods(BindingFlags.Static | BindingFlags.NonPublic))
			{
				bool flag = method.Name != "CreateTypeConstructor" || !method.IsGenericMethod;
				if (!flag)
				{
					TypeUtility.s_CreateTypeConstructor = method;
					break;
				}
			}
			bool flag2 = null == TypeUtility.s_CreateTypeConstructor;
			if (flag2)
			{
				throw new InvalidProgramException();
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000097CC File Offset: 0x000079CC
		public static string GetTypeDisplayName(Type type)
		{
			string name;
			bool flag = TypeUtility.s_CachedResolvedName.TryGetValue(type, out name);
			string text;
			if (flag)
			{
				text = name;
			}
			else
			{
				int index = 0;
				name = TypeUtility.GetTypeDisplayName(type, type.GetGenericArguments(), ref index);
				TypeUtility.s_CachedResolvedName[type] = name;
				text = name;
			}
			return text;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009814 File Offset: 0x00007A14
		private static string GetTypeDisplayName(Type type, IReadOnlyList<Type> args, ref int argIndex)
		{
			bool flag = type == typeof(int);
			string text;
			if (flag)
			{
				text = "int";
			}
			else
			{
				bool flag2 = type == typeof(uint);
				if (flag2)
				{
					text = "uint";
				}
				else
				{
					bool flag3 = type == typeof(short);
					if (flag3)
					{
						text = "short";
					}
					else
					{
						bool flag4 = type == typeof(ushort);
						if (flag4)
						{
							text = "ushort";
						}
						else
						{
							bool flag5 = type == typeof(byte);
							if (flag5)
							{
								text = "byte";
							}
							else
							{
								bool flag6 = type == typeof(char);
								if (flag6)
								{
									text = "char";
								}
								else
								{
									bool flag7 = type == typeof(bool);
									if (flag7)
									{
										text = "bool";
									}
									else
									{
										bool flag8 = type == typeof(long);
										if (flag8)
										{
											text = "long";
										}
										else
										{
											bool flag9 = type == typeof(ulong);
											if (flag9)
											{
												text = "ulong";
											}
											else
											{
												bool flag10 = type == typeof(float);
												if (flag10)
												{
													text = "float";
												}
												else
												{
													bool flag11 = type == typeof(double);
													if (flag11)
													{
														text = "double";
													}
													else
													{
														bool flag12 = type == typeof(string);
														if (flag12)
														{
															text = "string";
														}
														else
														{
															string name = type.Name;
															bool isGenericParameter = type.IsGenericParameter;
															if (isGenericParameter)
															{
																text = name;
															}
															else
															{
																bool isNested = type.IsNested;
																if (isNested)
																{
																	name = TypeUtility.GetTypeDisplayName(type.DeclaringType, args, ref argIndex) + "." + name;
																}
																bool flag13 = !type.IsGenericType;
																if (flag13)
																{
																	text = name;
																}
																else
																{
																	int tickIndex = name.IndexOf('`');
																	int count = type.GetGenericArguments().Length;
																	bool flag14 = tickIndex > -1;
																	if (flag14)
																	{
																		count = int.Parse(name.Substring(tickIndex + 1));
																		name = name.Remove(tickIndex);
																	}
																	StringBuilder genericTypeNames = null;
																	object obj = TypeUtility.syncedPoolObject;
																	lock (obj)
																	{
																		genericTypeNames = TypeUtility.s_Builders.Get();
																	}
																	try
																	{
																		int i = 0;
																		while (i < count && argIndex < args.Count)
																		{
																			bool flag16 = i != 0;
																			if (flag16)
																			{
																				genericTypeNames.Append(", ");
																			}
																			genericTypeNames.Append(TypeUtility.GetTypeDisplayName(args[argIndex]));
																			i++;
																			argIndex++;
																		}
																		bool flag17 = genericTypeNames.Length > 0;
																		if (flag17)
																		{
																			name = string.Format("{0}<{1}>", name, genericTypeNames);
																		}
																	}
																	finally
																	{
																		object obj2 = TypeUtility.syncedPoolObject;
																		lock (obj2)
																		{
																			TypeUtility.s_Builders.Release(genericTypeNames);
																		}
																	}
																	text = name;
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009B58 File Offset: 0x00007D58
		public static Type GetRootType(this Type type)
		{
			bool isInterface = type.IsInterface;
			Type type2;
			if (isInterface)
			{
				type2 = null;
			}
			else
			{
				Type baseType = (type.IsValueType ? typeof(ValueType) : typeof(object));
				while (baseType != type.BaseType)
				{
					type = type.BaseType;
				}
				type2 = type;
			}
			return type2;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00009BB4 File Offset: 0x00007DB4
		[Preserve]
		private static TypeUtility.ITypeConstructor CreateTypeConstructor(Type type)
		{
			IPropertyBag properties = PropertyBagStore.GetPropertyBag(type);
			bool flag = properties != null;
			TypeUtility.ITypeConstructor typeConstructor;
			if (flag)
			{
				TypeUtility.TypeConstructorVisitor visitor = new TypeUtility.TypeConstructorVisitor();
				properties.Accept(visitor);
				typeConstructor = visitor.TypeConstructor;
			}
			else
			{
				bool containsGenericParameters = type.ContainsGenericParameters;
				if (containsGenericParameters)
				{
					TypeUtility.NonConstructable constructor = new TypeUtility.NonConstructable();
					TypeUtility.s_TypeConstructors[type] = constructor;
					typeConstructor = constructor;
				}
				else
				{
					typeConstructor = TypeUtility.s_CreateTypeConstructor.MakeGenericMethod(new Type[] { type }).Invoke(null, null) as TypeUtility.ITypeConstructor;
				}
			}
			return typeConstructor;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009C34 File Offset: 0x00007E34
		private static TypeUtility.ITypeConstructor<T> CreateTypeConstructor<T>()
		{
			TypeUtility.TypeConstructor<T> constructor = new TypeUtility.TypeConstructor<T>();
			TypeUtility.Cache<T>.TypeConstructor = constructor;
			TypeUtility.s_TypeConstructors[typeof(T)] = constructor;
			return constructor;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009C6C File Offset: 0x00007E6C
		private static TypeUtility.ITypeConstructor GetTypeConstructor(Type type)
		{
			TypeUtility.ITypeConstructor constructor;
			return TypeUtility.s_TypeConstructors.TryGetValue(type, out constructor) ? constructor : TypeUtility.CreateTypeConstructor(type);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009C98 File Offset: 0x00007E98
		private static TypeUtility.ITypeConstructor<T> GetTypeConstructor<T>()
		{
			return (TypeUtility.Cache<T>.TypeConstructor != null) ? TypeUtility.Cache<T>.TypeConstructor : TypeUtility.CreateTypeConstructor<T>();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009CBD File Offset: 0x00007EBD
		public static bool CanBeInstantiated(Type type)
		{
			return TypeUtility.GetTypeConstructor(type).CanBeInstantiated;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009CCA File Offset: 0x00007ECA
		public static bool CanBeInstantiated<T>()
		{
			return TypeUtility.GetTypeConstructor<T>().CanBeInstantiated;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009CD6 File Offset: 0x00007ED6
		public static void SetExplicitInstantiationMethod<T>(Func<T> constructor)
		{
			TypeUtility.GetTypeConstructor<T>().SetExplicitConstructor(constructor);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009CE4 File Offset: 0x00007EE4
		public static T Instantiate<T>()
		{
			TypeUtility.ITypeConstructor<T> constructor = TypeUtility.GetTypeConstructor<T>();
			TypeUtility.CheckCanBeInstantiated<T>(constructor);
			return constructor.Instantiate();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009D0C File Offset: 0x00007F0C
		public static bool TryInstantiate<T>(out T instance)
		{
			TypeUtility.ITypeConstructor<T> constructor = TypeUtility.GetTypeConstructor<T>();
			bool canBeInstantiated = constructor.CanBeInstantiated;
			bool flag;
			if (canBeInstantiated)
			{
				instance = constructor.Instantiate();
				flag = true;
			}
			else
			{
				instance = default(T);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00009D48 File Offset: 0x00007F48
		public static T Instantiate<T>(Type derivedType)
		{
			TypeUtility.ITypeConstructor constructor = TypeUtility.GetTypeConstructor(derivedType);
			TypeUtility.CheckIsAssignableFrom(typeof(T), derivedType);
			TypeUtility.CheckCanBeInstantiated(constructor, derivedType);
			return (T)((object)constructor.Instantiate());
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009D88 File Offset: 0x00007F88
		public static bool TryInstantiate<T>(Type derivedType, out T value)
		{
			bool flag = !typeof(T).IsAssignableFrom(derivedType);
			bool flag2;
			if (flag)
			{
				value = default(T);
				value = default(T);
				flag2 = false;
			}
			else
			{
				TypeUtility.ITypeConstructor constructor = TypeUtility.GetTypeConstructor(derivedType);
				bool flag3 = !constructor.CanBeInstantiated;
				if (flag3)
				{
					value = default(T);
					flag2 = false;
				}
				else
				{
					value = (T)((object)constructor.Instantiate());
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00009DF8 File Offset: 0x00007FF8
		public static TArray InstantiateArray<TArray>(int count = 0)
		{
			bool flag = count < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("{0}: Cannot construct an array with {1}={2}", "TypeUtility", "count", count));
			}
			IPropertyBag<TArray> properties = PropertyBagStore.GetPropertyBag<TArray>();
			IConstructorWithCount<TArray> constructor = properties as IConstructorWithCount<TArray>;
			bool flag2 = constructor != null;
			TArray tarray;
			if (flag2)
			{
				tarray = constructor.InstantiateWithCount(count);
			}
			else
			{
				Type type = typeof(TArray);
				bool flag3 = !type.IsArray;
				if (flag3)
				{
					throw new ArgumentException("TypeUtility: Cannot construct an array, since " + typeof(TArray).Name + " is not an array type.");
				}
				Type elementType = type.GetElementType();
				bool flag4 = null == elementType;
				if (flag4)
				{
					throw new ArgumentException("TypeUtility: Cannot construct an array, since " + typeof(TArray).Name + ".GetElementType() returned null.");
				}
				tarray = (TArray)((object)Array.CreateInstance(elementType, count));
			}
			return tarray;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009EE4 File Offset: 0x000080E4
		public static bool TryInstantiateArray<TArray>(int count, out TArray instance)
		{
			bool flag = count < 0;
			bool flag2;
			if (flag)
			{
				instance = default(TArray);
				flag2 = false;
			}
			else
			{
				IPropertyBag<TArray> properties = PropertyBagStore.GetPropertyBag<TArray>();
				IConstructorWithCount<TArray> constructor = properties as IConstructorWithCount<TArray>;
				bool flag3 = constructor != null;
				if (flag3)
				{
					try
					{
						instance = constructor.InstantiateWithCount(count);
						return true;
					}
					catch
					{
					}
				}
				Type type = typeof(TArray);
				bool flag4 = !type.IsArray;
				if (flag4)
				{
					instance = default(TArray);
					flag2 = false;
				}
				else
				{
					Type elementType = type.GetElementType();
					bool flag5 = null == elementType;
					if (flag5)
					{
						instance = default(TArray);
						flag2 = false;
					}
					else
					{
						instance = (TArray)((object)Array.CreateInstance(elementType, count));
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009FB0 File Offset: 0x000081B0
		public static TArray InstantiateArray<TArray>(Type derivedType, int count = 0)
		{
			bool flag = count < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("{0}: Cannot instantiate an array with {1}={2}", "TypeUtility", "count", count));
			}
			IPropertyBag properties = PropertyBagStore.GetPropertyBag(derivedType);
			IConstructorWithCount<TArray> constructor = properties as IConstructorWithCount<TArray>;
			bool flag2 = constructor != null;
			TArray tarray;
			if (flag2)
			{
				tarray = constructor.InstantiateWithCount(count);
			}
			else
			{
				Type type = typeof(TArray);
				bool flag3 = !type.IsArray;
				if (flag3)
				{
					throw new ArgumentException("TypeUtility: Cannot instantiate an array, since " + typeof(TArray).Name + " is not an array type.");
				}
				Type elementType = type.GetElementType();
				bool flag4 = null == elementType;
				if (flag4)
				{
					throw new ArgumentException("TypeUtility: Cannot instantiate an array, since " + typeof(TArray).Name + ".GetElementType() returned null.");
				}
				tarray = (TArray)((object)Array.CreateInstance(elementType, count));
			}
			return tarray;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000A09C File Offset: 0x0000829C
		private static void CheckIsAssignableFrom(Type type, Type derivedType)
		{
			bool flag = !type.IsAssignableFrom(derivedType);
			if (flag)
			{
				throw new ArgumentException(string.Concat(new string[] { "Could not create instance of type `", derivedType.Name, "` and convert to `", type.Name, "`: The given type is not assignable to target type." }));
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000A0F4 File Offset: 0x000082F4
		private static void CheckCanBeInstantiated<T>(TypeUtility.ITypeConstructor<T> constructor)
		{
			bool flag = !constructor.CanBeInstantiated;
			if (flag)
			{
				throw new InvalidOperationException("Type `" + typeof(T).Name + "` could not be instantiated. A parameter-less constructor or an explicit construction method is required.");
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000A134 File Offset: 0x00008334
		private static void CheckCanBeInstantiated(TypeUtility.ITypeConstructor constructor, Type type)
		{
			bool flag = !constructor.CanBeInstantiated;
			if (flag)
			{
				throw new InvalidOperationException("Type `" + type.Name + "` could not be instantiated. A parameter-less constructor or an explicit construction method is required.");
			}
		}

		// Token: 0x04000152 RID: 338
		private static readonly ConcurrentDictionary<Type, TypeUtility.ITypeConstructor> s_TypeConstructors = new ConcurrentDictionary<Type, TypeUtility.ITypeConstructor>();

		// Token: 0x04000153 RID: 339
		private static readonly MethodInfo s_CreateTypeConstructor;

		// Token: 0x04000154 RID: 340
		private static readonly ConcurrentDictionary<Type, string> s_CachedResolvedName = new ConcurrentDictionary<Type, string>();

		// Token: 0x04000155 RID: 341
		private static readonly ObjectPool<StringBuilder> s_Builders = new ObjectPool<StringBuilder>(() => new StringBuilder(), null, delegate(StringBuilder sb)
		{
			sb.Clear();
		}, null, true, 10, 10000);

		// Token: 0x04000156 RID: 342
		private static readonly object syncedPoolObject = new object();

		// Token: 0x02000067 RID: 103
		private interface ITypeConstructor
		{
			// Token: 0x1700004D RID: 77
			// (get) Token: 0x0600024E RID: 590
			bool CanBeInstantiated { get; }

			// Token: 0x0600024F RID: 591
			object Instantiate();
		}

		// Token: 0x02000068 RID: 104
		private interface ITypeConstructor<T> : TypeUtility.ITypeConstructor
		{
			// Token: 0x06000250 RID: 592
			T Instantiate();

			// Token: 0x06000251 RID: 593
			void SetExplicitConstructor(Func<T> constructor);
		}

		// Token: 0x02000069 RID: 105
		private class TypeConstructor<T> : TypeUtility.ITypeConstructor<T>, TypeUtility.ITypeConstructor
		{
			// Token: 0x1700004E RID: 78
			// (get) Token: 0x06000252 RID: 594 RVA: 0x0000A16C File Offset: 0x0000836C
			bool TypeUtility.ITypeConstructor.CanBeInstantiated
			{
				get
				{
					bool flag = this.m_ExplicitConstructor != null;
					bool flag2;
					if (flag)
					{
						flag2 = true;
					}
					else
					{
						bool flag3 = this.m_OverrideConstructor != null;
						if (flag3)
						{
							bool flag4 = this.m_OverrideConstructor.InstantiationKind == InstantiationKind.NotInstantiatable;
							if (flag4)
							{
								return false;
							}
							bool flag5 = this.m_OverrideConstructor.InstantiationKind == InstantiationKind.PropertyBagOverride;
							if (flag5)
							{
								return true;
							}
						}
						flag2 = this.m_ImplicitConstructor != null;
					}
					return flag2;
				}
			}

			// Token: 0x06000253 RID: 595 RVA: 0x0000A1D5 File Offset: 0x000083D5
			public TypeConstructor()
			{
				this.m_OverrideConstructor = PropertyBagStore.GetPropertyBag<T>() as IConstructor<T>;
				this.SetImplicitConstructor();
			}

			// Token: 0x06000254 RID: 596 RVA: 0x0000A1F8 File Offset: 0x000083F8
			private void SetImplicitConstructor()
			{
				Type type = typeof(T);
				bool isValueType = type.IsValueType;
				if (isValueType)
				{
					this.m_ImplicitConstructor = new Func<T>(TypeUtility.TypeConstructor<T>.CreateValueTypeInstance);
				}
				else
				{
					bool isAbstract = type.IsAbstract;
					if (!isAbstract)
					{
						bool flag = typeof(ScriptableObject).IsAssignableFrom(type);
						if (flag)
						{
							this.m_ImplicitConstructor = new Func<T>(TypeUtility.TypeConstructor<T>.CreateScriptableObjectInstance);
						}
						else
						{
							bool flag2 = null != type.GetConstructor(Array.Empty<Type>());
							if (flag2)
							{
								this.m_ImplicitConstructor = new Func<T>(TypeUtility.TypeConstructor<T>.CreateClassInstance);
							}
						}
					}
				}
			}

			// Token: 0x06000255 RID: 597 RVA: 0x0000A294 File Offset: 0x00008494
			private static T CreateValueTypeInstance()
			{
				return default(T);
			}

			// Token: 0x06000256 RID: 598 RVA: 0x0000A2B0 File Offset: 0x000084B0
			private static T CreateScriptableObjectInstance()
			{
				return (T)((object)ScriptableObject.CreateInstance(typeof(T)));
			}

			// Token: 0x06000257 RID: 599 RVA: 0x0000A2D8 File Offset: 0x000084D8
			private static T CreateClassInstance()
			{
				return Activator.CreateInstance<T>();
			}

			// Token: 0x06000258 RID: 600 RVA: 0x0000A2EF File Offset: 0x000084EF
			public void SetExplicitConstructor(Func<T> constructor)
			{
				this.m_ExplicitConstructor = constructor;
			}

			// Token: 0x06000259 RID: 601 RVA: 0x0000A2FC File Offset: 0x000084FC
			T TypeUtility.ITypeConstructor<T>.Instantiate()
			{
				bool flag = this.m_ExplicitConstructor != null;
				T t;
				if (flag)
				{
					t = this.m_ExplicitConstructor();
				}
				else
				{
					bool flag2 = this.m_OverrideConstructor != null;
					if (flag2)
					{
						bool flag3 = this.m_OverrideConstructor.InstantiationKind == InstantiationKind.NotInstantiatable;
						if (flag3)
						{
							throw new InvalidOperationException("The type '" + typeof(T).Name + "' is not constructable.");
						}
						bool flag4 = this.m_OverrideConstructor.InstantiationKind == InstantiationKind.PropertyBagOverride;
						if (flag4)
						{
							return this.m_OverrideConstructor.Instantiate();
						}
					}
					bool flag5 = this.m_ImplicitConstructor != null;
					if (!flag5)
					{
						throw new InvalidOperationException("The type '" + typeof(T).Name + "' is not constructable.");
					}
					t = this.m_ImplicitConstructor();
				}
				return t;
			}

			// Token: 0x0600025A RID: 602 RVA: 0x0000A3D2 File Offset: 0x000085D2
			object TypeUtility.ITypeConstructor.Instantiate()
			{
				return ((TypeUtility.ITypeConstructor<T>)this).Instantiate();
			}

			// Token: 0x04000157 RID: 343
			private Func<T> m_ExplicitConstructor;

			// Token: 0x04000158 RID: 344
			private Func<T> m_ImplicitConstructor;

			// Token: 0x04000159 RID: 345
			private IConstructor<T> m_OverrideConstructor;
		}

		// Token: 0x0200006A RID: 106
		private class NonConstructable : TypeUtility.ITypeConstructor
		{
			// Token: 0x1700004F RID: 79
			// (get) Token: 0x0600025B RID: 603 RVA: 0x0000498D File Offset: 0x00002B8D
			bool TypeUtility.ITypeConstructor.CanBeInstantiated
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600025C RID: 604 RVA: 0x0000A3DF File Offset: 0x000085DF
			public object Instantiate()
			{
				throw new InvalidOperationException("The type is not instantiatable.");
			}
		}

		// Token: 0x0200006B RID: 107
		private struct Cache<T>
		{
			// Token: 0x0400015A RID: 346
			public static TypeUtility.ITypeConstructor<T> TypeConstructor;
		}

		// Token: 0x0200006C RID: 108
		private class TypeConstructorVisitor : ITypeVisitor
		{
			// Token: 0x0600025E RID: 606 RVA: 0x0000A3EB File Offset: 0x000085EB
			public void Visit<TContainer>()
			{
				this.TypeConstructor = TypeUtility.CreateTypeConstructor<TContainer>();
			}

			// Token: 0x0400015B RID: 347
			public TypeUtility.ITypeConstructor TypeConstructor;
		}
	}
}
