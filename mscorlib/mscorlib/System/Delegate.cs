using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Represents a delegate, which is a data structure that refers to a static method or to a class instance and an instance method of that class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CA RID: 458
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class Delegate : ICloneable, ISerializable
	{
		/// <summary>Gets the method represented by the delegate.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> describing the method represented by the delegate.</returns>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private). </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00048BE8 File Offset: 0x00046DE8
		public MethodInfo Method
		{
			get
			{
				return this.GetMethodImpl();
			}
		}

		// Token: 0x060011F5 RID: 4597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo GetVirtualMethod_internal();

		/// <summary>Gets the class instance on which the current delegate invokes the instance method.</summary>
		/// <returns>The object on which the current delegate invokes the instance method, if the delegate represents an instance method; null if the delegate represents a static method.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x00048BF0 File Offset: 0x00046DF0
		public object Target
		{
			get
			{
				return this.m_target;
			}
		}

		// Token: 0x060011F7 RID: 4599
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Delegate CreateDelegate_internal(Type type, object target, MethodInfo info, bool throwOnBindFailure);

		// Token: 0x060011F8 RID: 4600 RVA: 0x00048BF8 File Offset: 0x00046DF8
		private static bool arg_type_match(Type delArgType, Type argType)
		{
			bool flag = delArgType == argType;
			if (!flag && !argType.IsValueType && argType.IsAssignableFrom(delArgType))
			{
				flag = true;
			}
			if (!flag)
			{
				if (delArgType.IsEnum && Enum.GetUnderlyingType(delArgType) == argType)
				{
					flag = true;
				}
				else if (argType.IsEnum && Enum.GetUnderlyingType(argType) == delArgType)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00048C5C File Offset: 0x00046E5C
		private static bool arg_type_match_this(Type delArgType, Type argType, bool boxedThis)
		{
			bool flag;
			if (argType.IsValueType)
			{
				flag = (delArgType.IsByRef && delArgType.GetElementType() == argType) || (boxedThis && delArgType == argType);
			}
			else
			{
				flag = delArgType == argType || argType.IsAssignableFrom(delArgType);
			}
			return flag;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00048CB0 File Offset: 0x00046EB0
		private static bool return_type_match(Type delReturnType, Type returnType)
		{
			bool flag = returnType == delReturnType;
			if (!flag)
			{
				if (!returnType.IsValueType && delReturnType.IsAssignableFrom(returnType))
				{
					flag = true;
				}
				else
				{
					bool isEnum = delReturnType.IsEnum;
					bool isEnum2 = returnType.IsEnum;
					if (isEnum2 && isEnum)
					{
						flag = Enum.GetUnderlyingType(delReturnType) == Enum.GetUnderlyingType(returnType);
					}
					else if (isEnum && Enum.GetUnderlyingType(delReturnType) == returnType)
					{
						flag = true;
					}
					else if (isEnum2 && Enum.GetUnderlyingType(returnType) == delReturnType)
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		/// <summary>Creates a delegate of the specified type that represents the specified static or instance method, with the specified first argument and the specified behavior on failure to bind.</summary>
		/// <returns>A delegate of the specified type that represents the specified static or instance method, or null if <paramref name="throwOnBindFailure" /> is false and the delegate cannot be bound to <paramref name="method" />. </returns>
		/// <param name="type">A <see cref="T:System.Type" /> representing the type of delegate to create. </param>
		/// <param name="firstArgument">An <see cref="T:System.Object" /> that is the first argument of the method the delegate represents. For instance methods, it must be compatible with the instance type. </param>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> describing the static or instance method the delegate is to represent.</param>
		/// <param name="throwOnBindFailure">true to throw an exception if <paramref name="method" /> cannot be bound; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.-or- <paramref name="method" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or-<paramref name="type" /> is not a RuntimeType. See Runtime Types in Reflection. -or-<paramref name="method" /> cannot be bound, and <paramref name="throwOnBindFailure" /> is true.-or-<paramref name="method" /> is not a RuntimeMethodInfo. See Runtime Types in Reflection.</exception>
		/// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060011FB RID: 4603 RVA: 0x00048D2D File Offset: 0x00046F2D
		public static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method, bool throwOnBindFailure)
		{
			return Delegate.CreateDelegate(type, firstArgument, method, throwOnBindFailure, true);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00048D3C File Offset: 0x00046F3C
		private static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method, bool throwOnBindFailure, bool allowClosed)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (!type.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw new ArgumentException("type is not a subclass of Multicastdelegate");
			}
			MethodInfo methodInfo = type.GetMethod("Invoke");
			if (!Delegate.return_type_match(methodInfo.ReturnType, method.ReturnType))
			{
				if (throwOnBindFailure)
				{
					throw new ArgumentException("method return type is incompatible");
				}
				return null;
			}
			else
			{
				ParameterInfo[] parametersInternal = methodInfo.GetParametersInternal();
				ParameterInfo[] parametersInternal2 = method.GetParametersInternal();
				bool flag;
				if (firstArgument != null)
				{
					if (!method.IsStatic)
					{
						flag = parametersInternal2.Length == parametersInternal.Length;
					}
					else
					{
						flag = parametersInternal2.Length == parametersInternal.Length + 1;
					}
				}
				else if (!method.IsStatic)
				{
					flag = parametersInternal2.Length + 1 == parametersInternal.Length;
					if (!flag)
					{
						flag = parametersInternal2.Length == parametersInternal.Length;
					}
				}
				else
				{
					flag = parametersInternal2.Length == parametersInternal.Length;
					if (!flag)
					{
						flag = parametersInternal2.Length == parametersInternal.Length + 1;
					}
				}
				if (!flag)
				{
					if (throwOnBindFailure)
					{
						throw new TargetParameterCountException("Parameter count mismatch.");
					}
					return null;
				}
				else
				{
					DelegateData delegateData = new DelegateData();
					bool flag2;
					if (firstArgument != null)
					{
						if (!method.IsStatic)
						{
							flag2 = Delegate.arg_type_match_this(firstArgument.GetType(), method.DeclaringType, true);
							for (int i = 0; i < parametersInternal2.Length; i++)
							{
								flag2 &= Delegate.arg_type_match(parametersInternal[i].ParameterType, parametersInternal2[i].ParameterType);
							}
						}
						else
						{
							flag2 = Delegate.arg_type_match(firstArgument.GetType(), parametersInternal2[0].ParameterType);
							for (int j = 1; j < parametersInternal2.Length; j++)
							{
								flag2 &= Delegate.arg_type_match(parametersInternal[j - 1].ParameterType, parametersInternal2[j].ParameterType);
							}
							delegateData.curried_first_arg = true;
						}
					}
					else if (!method.IsStatic)
					{
						if (parametersInternal2.Length + 1 == parametersInternal.Length)
						{
							flag2 = Delegate.arg_type_match_this(parametersInternal[0].ParameterType, method.DeclaringType, false);
							for (int k = 0; k < parametersInternal2.Length; k++)
							{
								flag2 &= Delegate.arg_type_match(parametersInternal[k + 1].ParameterType, parametersInternal2[k].ParameterType);
							}
						}
						else
						{
							flag2 = allowClosed;
							for (int l = 0; l < parametersInternal2.Length; l++)
							{
								flag2 &= Delegate.arg_type_match(parametersInternal[l].ParameterType, parametersInternal2[l].ParameterType);
							}
						}
					}
					else if (parametersInternal.Length + 1 == parametersInternal2.Length)
					{
						flag2 = !parametersInternal2[0].ParameterType.IsValueType && !parametersInternal2[0].ParameterType.IsByRef && allowClosed;
						for (int m = 0; m < parametersInternal.Length; m++)
						{
							flag2 &= Delegate.arg_type_match(parametersInternal[m].ParameterType, parametersInternal2[m + 1].ParameterType);
						}
						delegateData.curried_first_arg = true;
					}
					else
					{
						flag2 = true;
						for (int n = 0; n < parametersInternal2.Length; n++)
						{
							flag2 &= Delegate.arg_type_match(parametersInternal[n].ParameterType, parametersInternal2[n].ParameterType);
						}
					}
					if (flag2)
					{
						Delegate @delegate = Delegate.CreateDelegate_internal(type, firstArgument, method, throwOnBindFailure);
						if (@delegate != null)
						{
							@delegate.original_method_info = method;
						}
						if (delegateData != null)
						{
							@delegate.data = delegateData;
						}
						return @delegate;
					}
					if (throwOnBindFailure)
					{
						throw new ArgumentException("method arguments are incompatible");
					}
					return null;
				}
			}
		}

		/// <summary>Creates a delegate of the specified type that represents the specified static or instance method, with the specified first argument.</summary>
		/// <returns>A delegate of the specified type that represents the specified static or instance method. </returns>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create. </param>
		/// <param name="firstArgument">The object to which the delegate is bound, or null to treat <paramref name="method" /> as static (Shared in Visual Basic). </param>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> describing the static or instance method the delegate is to represent.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.-or- <paramref name="method" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or-<paramref name="type" /> is not a RuntimeType. See Runtime Types in Reflection. -or-<paramref name="method" /> cannot be bound.-or-<paramref name="method" /> is not a RuntimeMethodInfo. See Runtime Types in Reflection.</exception>
		/// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060011FD RID: 4605 RVA: 0x0004905F File Offset: 0x0004725F
		public static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method)
		{
			return Delegate.CreateDelegate(type, firstArgument, method, true, true);
		}

		/// <summary>Creates a delegate of the specified type to represent the specified static method, with the specified behavior on failure to bind.</summary>
		/// <returns>A delegate of the specified type to represent the specified static method.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create. </param>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> describing the static or instance method the delegate is to represent.</param>
		/// <param name="throwOnBindFailure">true to throw an exception if <paramref name="method" /> cannot be bound; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.-or- <paramref name="method" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or-<paramref name="type" /> is not a RuntimeType. See Runtime Types in Reflection. -or-<paramref name="method" /> cannot be bound, and <paramref name="throwOnBindFailure" /> is true.-or-<paramref name="method" /> is not a RuntimeMethodInfo. See Runtime Types in Reflection.</exception>
		/// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060011FE RID: 4606 RVA: 0x0004906B File Offset: 0x0004726B
		public static Delegate CreateDelegate(Type type, MethodInfo method, bool throwOnBindFailure)
		{
			return Delegate.CreateDelegate(type, null, method, throwOnBindFailure, false);
		}

		/// <summary>Creates a delegate of the specified type to represent the specified static method.</summary>
		/// <returns>A delegate of the specified type to represent the specified static method.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create. </param>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> describing the static or instance method the delegate is to represent. Only static methods are supported in the .NET Framework version 1.0 and 1.1.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.-or- <paramref name="method" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or-<paramref name="type" /> is not a RuntimeType. See Runtime Types in Reflection. -or- <paramref name="method" /> is not a static method, and the .NET Framework version is 1.0 or 1.1. -or-<paramref name="method" /> cannot be bound.-or-<paramref name="method" /> is not a RuntimeMethodInfo. See Runtime Types in Reflection.</exception>
		/// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060011FF RID: 4607 RVA: 0x00049077 File Offset: 0x00047277
		public static Delegate CreateDelegate(Type type, MethodInfo method)
		{
			return Delegate.CreateDelegate(type, method, true);
		}

		/// <summary>Creates a delegate of the specified type that represents the specified instance method to invoke on the specified class instance.</summary>
		/// <returns>A delegate of the specified type that represents the specified instance method to invoke on the specified class instance.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create. </param>
		/// <param name="target">The class instance on which <paramref name="method" /> is invoked. </param>
		/// <param name="method">The name of the instance method that the delegate is to represent. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.-or- <paramref name="target" /> is null.-or- <paramref name="method" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />. -or-<paramref name="type" /> is not a RuntimeType. See Runtime Types in Reflection.-or- <paramref name="method" /> is not an instance method. -or-<paramref name="method" /> cannot be bound, for example because it cannot be found.</exception>
		/// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001200 RID: 4608 RVA: 0x00049081 File Offset: 0x00047281
		public static Delegate CreateDelegate(Type type, object target, string method)
		{
			return Delegate.CreateDelegate(type, target, method, false);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0004908C File Offset: 0x0004728C
		private static MethodInfo GetCandidateMethod(Type type, Type target, string method, BindingFlags bflags, bool ignoreCase, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (!type.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw new ArgumentException("type is not subclass of MulticastDelegate.");
			}
			MethodInfo methodInfo = type.GetMethod("Invoke");
			ParameterInfo[] parametersInternal = methodInfo.GetParametersInternal();
			Type[] array = new Type[parametersInternal.Length];
			for (int i = 0; i < parametersInternal.Length; i++)
			{
				array[i] = parametersInternal[i].ParameterType;
			}
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.ExactBinding | bflags;
			if (ignoreCase)
			{
				bindingFlags |= BindingFlags.IgnoreCase;
			}
			MethodInfo methodInfo2 = null;
			Type type2 = target;
			while (type2 != null)
			{
				MethodInfo methodInfo3 = type2.GetMethod(method, bindingFlags, null, array, Array.Empty<ParameterModifier>());
				if (methodInfo3 != null && Delegate.return_type_match(methodInfo.ReturnType, methodInfo3.ReturnType))
				{
					methodInfo2 = methodInfo3;
					break;
				}
				type2 = type2.BaseType;
			}
			if (!(methodInfo2 == null))
			{
				return methodInfo2;
			}
			if (throwOnBindFailure)
			{
				throw new ArgumentException("Couldn't bind to method '" + method + "'.");
			}
			return null;
		}

		/// <summary>Creates a delegate of the specified type that represents the specified static method of the specified class, with the specified case-sensitivity and the specified behavior on failure to bind.</summary>
		/// <returns>A delegate of the specified type that represents the specified static method of the specified class.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create. </param>
		/// <param name="target">The <see cref="T:System.Type" /> representing the class that implements <paramref name="method" />. </param>
		/// <param name="method">The name of the static method that the delegate is to represent. </param>
		/// <param name="ignoreCase">A Boolean indicating whether to ignore the case when comparing the name of the method.</param>
		/// <param name="throwOnBindFailure">true to throw an exception if <paramref name="method" /> cannot be bound; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.-or- <paramref name="target" /> is null.-or- <paramref name="method" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or- <paramref name="type" /> is not a RuntimeType. See Runtime Types in Reflection. -or-<paramref name="target" /> is not a RuntimeType.-or-<paramref name="target" /> is an open generic type. That is, its <see cref="P:System.Type.ContainsGenericParameters" /> property is true.-or-<paramref name="method" /> is not a static method (Shared method in Visual Basic). -or-<paramref name="method" /> cannot be bound, for example because it cannot be found, and <paramref name="throwOnBindFailure" /> is true. </exception>
		/// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001202 RID: 4610 RVA: 0x0004919C File Offset: 0x0004739C
		public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase, bool throwOnBindFailure)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			MethodInfo candidateMethod = Delegate.GetCandidateMethod(type, target, method, BindingFlags.Static, ignoreCase, throwOnBindFailure);
			if (candidateMethod == null)
			{
				return null;
			}
			return Delegate.CreateDelegate_internal(type, null, candidateMethod, throwOnBindFailure);
		}

		/// <summary>Creates a delegate of the specified type that represents the specified static method of the specified class.</summary>
		/// <returns>A delegate of the specified type that represents the specified static method of the specified class.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create. </param>
		/// <param name="target">The <see cref="T:System.Type" /> representing the class that implements <paramref name="method" />. </param>
		/// <param name="method">The name of the static method that the delegate is to represent. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.-or- <paramref name="target" /> is null.-or- <paramref name="method" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or- <paramref name="type" /> is not a RuntimeType. See Runtime Types in Reflection. -or-<paramref name="target" /> is not a RuntimeType.-or-<paramref name="target" /> is an open generic type. That is, its <see cref="P:System.Type.ContainsGenericParameters" /> property is true.-or-<paramref name="method" /> is not a static method (Shared method in Visual Basic). -or-<paramref name="method" /> cannot be bound, for example because it cannot be found, and <paramref name="throwOnBindFailure" /> is true.</exception>
		/// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001203 RID: 4611 RVA: 0x000491DF File Offset: 0x000473DF
		public static Delegate CreateDelegate(Type type, Type target, string method)
		{
			return Delegate.CreateDelegate(type, target, method, false, true);
		}

		/// <summary>Creates a delegate of the specified type that represents the specified static method of the specified class, with the specified case-sensitivity.</summary>
		/// <returns>A delegate of the specified type that represents the specified static method of the specified class.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create. </param>
		/// <param name="target">The <see cref="T:System.Type" /> representing the class that implements <paramref name="method" />. </param>
		/// <param name="method">The name of the static method that the delegate is to represent. </param>
		/// <param name="ignoreCase">A Boolean indicating whether to ignore the case when comparing the name of the method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.-or- <paramref name="target" /> is null.-or- <paramref name="method" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or- <paramref name="type" /> is not a RuntimeType. See Runtime Types in Reflection. -or-<paramref name="target" /> is not a RuntimeType.-or-<paramref name="target" /> is an open generic type. That is, its <see cref="P:System.Type.ContainsGenericParameters" /> property is true.-or-<paramref name="method" /> is not a static method (Shared method in Visual Basic). -or-<paramref name="method" /> cannot be bound, for example because it cannot be found.</exception>
		/// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001204 RID: 4612 RVA: 0x000491EB File Offset: 0x000473EB
		public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase)
		{
			return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
		}

		/// <summary>Creates a delegate of the specified type that represents the specified instance method to invoke on the specified class instance, with the specified case-sensitivity and the specified behavior on failure to bind.</summary>
		/// <returns>A delegate of the specified type that represents the specified instance method to invoke on the specified class instance.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create. </param>
		/// <param name="target">The class instance on which <paramref name="method" /> is invoked. </param>
		/// <param name="method">The name of the instance method that the delegate is to represent. </param>
		/// <param name="ignoreCase">A Boolean indicating whether to ignore the case when comparing the name of the method. </param>
		/// <param name="throwOnBindFailure">true to throw an exception if <paramref name="method" /> cannot be bound; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.-or- <paramref name="target" /> is null.-or- <paramref name="method" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or-<paramref name="type" /> is not a RuntimeType. See Runtime Types in Reflection. -or-  <paramref name="method" /> is not an instance method. -or-<paramref name="method" /> cannot be bound, for example because it cannot be found, and <paramref name="throwOnBindFailure" /> is true.</exception>
		/// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001205 RID: 4613 RVA: 0x000491F8 File Offset: 0x000473F8
		public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase, bool throwOnBindFailure)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			MethodInfo candidateMethod = Delegate.GetCandidateMethod(type, target.GetType(), method, BindingFlags.Instance, ignoreCase, throwOnBindFailure);
			if (candidateMethod == null)
			{
				return null;
			}
			return Delegate.CreateDelegate_internal(type, target, candidateMethod, throwOnBindFailure);
		}

		/// <summary>Creates a delegate of the specified type that represents the specified instance method to invoke on the specified class instance with the specified case-sensitivity.</summary>
		/// <returns>A delegate of the specified type that represents the specified instance method to invoke on the specified class instance.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of delegate to create. </param>
		/// <param name="target">The class instance on which <paramref name="method" /> is invoked. </param>
		/// <param name="method">The name of the instance method that the delegate is to represent. </param>
		/// <param name="ignoreCase">A Boolean indicating whether to ignore the case when comparing the name of the method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.-or- <paramref name="target" /> is null.-or- <paramref name="method" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or-<paramref name="type" /> is not a RuntimeType. See Runtime Types in Reflection.-or- <paramref name="method" /> is not an instance method. -or-<paramref name="method" /> cannot be bound, for example because it cannot be found.</exception>
		/// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001206 RID: 4614 RVA: 0x0004923A File Offset: 0x0004743A
		public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase)
		{
			return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
		}

		/// <summary>Dynamically invokes (late-bound) the method represented by the current delegate.</summary>
		/// <returns>The object returned by the method represented by the delegate.</returns>
		/// <param name="args">An array of objects that are the arguments to pass to the method represented by the current delegate.-or- null, if the method represented by the current delegate does not require arguments. </param>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).-or- The number, order, or type of parameters listed in <paramref name="args" /> is invalid. </exception>
		/// <exception cref="T:System.ArgumentException">The method represented by the delegate is invoked on an object or a class that does not support it. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The method represented by the delegate is an instance method and the target object is null.-or- One of the encapsulated methods throws an exception. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001207 RID: 4615 RVA: 0x00049246 File Offset: 0x00047446
		public object DynamicInvoke(params object[] args)
		{
			return this.DynamicInvokeImpl(args);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00049250 File Offset: 0x00047450
		private void InitializeDelegateData()
		{
			DelegateData delegateData = new DelegateData();
			if (this.method_info.IsStatic)
			{
				if (this.m_target != null)
				{
					delegateData.curried_first_arg = true;
				}
				else if (base.GetType().GetMethod("Invoke").GetParametersCount() + 1 == this.method_info.GetParametersCount())
				{
					delegateData.curried_first_arg = true;
				}
			}
			this.data = delegateData;
		}

		/// <summary>Dynamically invokes (late-bound) the method represented by the current delegate.</summary>
		/// <returns>The object returned by the method represented by the delegate.</returns>
		/// <param name="args">An array of objects that are the arguments to pass to the method represented by the current delegate.-or- null, if the method represented by the current delegate does not require arguments. </param>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).-or- The number, order, or type of parameters listed in <paramref name="args" /> is invalid. </exception>
		/// <exception cref="T:System.ArgumentException">The method represented by the delegate is invoked on an object or a class that does not support it. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The method represented by the delegate is an instance method and the target object is null.-or- One of the encapsulated methods throws an exception. </exception>
		// Token: 0x06001209 RID: 4617 RVA: 0x000492B4 File Offset: 0x000474B4
		protected virtual object DynamicInvokeImpl(object[] args)
		{
			if (this.Method == null)
			{
				Type[] array = new Type[args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					array[i] = args[i].GetType();
				}
				this.method_info = this.m_target.GetType().GetMethod(this.data.method_name, array);
			}
			object obj = this.m_target;
			if (this.data == null)
			{
				this.InitializeDelegateData();
			}
			if (this.Method.IsStatic)
			{
				if (this.data.curried_first_arg)
				{
					if (args == null)
					{
						args = new object[] { obj };
					}
					else
					{
						Array.Resize<object>(ref args, args.Length + 1);
						Array.Copy(args, 0, args, 1, args.Length - 1);
						args[0] = obj;
					}
					obj = null;
				}
			}
			else if (this.m_target == null && args != null && args.Length != 0)
			{
				obj = args[0];
				Array.Copy(args, 1, args, 0, args.Length - 1);
				Array.Resize<object>(ref args, args.Length - 1);
			}
			return this.Method.Invoke(obj, args);
		}

		/// <summary>Creates a shallow copy of the delegate.</summary>
		/// <returns>A shallow copy of the delegate.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600120A RID: 4618 RVA: 0x00019FBE File Offset: 0x000181BE
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified object and the current delegate are of the same type and share the same targets, methods, and invocation list.</summary>
		/// <returns>true if <paramref name="obj" /> and the current delegate have the same targets, methods, and invocation list; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current delegate. </param>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private). </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600120B RID: 4619 RVA: 0x000493B0 File Offset: 0x000475B0
		public override bool Equals(object obj)
		{
			Delegate @delegate = obj as Delegate;
			if (@delegate == null)
			{
				return false;
			}
			if (@delegate.m_target != this.m_target || !(@delegate.Method == this.Method))
			{
				return false;
			}
			if (@delegate.data == null && this.data == null)
			{
				return true;
			}
			if (@delegate.data != null && this.data != null)
			{
				return @delegate.data.target_type == this.data.target_type && @delegate.data.method_name == this.data.method_name;
			}
			if (@delegate.data != null)
			{
				return @delegate.data.target_type == null;
			}
			return this.data != null && this.data.target_type == null;
		}

		/// <summary>Returns a hash code for the delegate.</summary>
		/// <returns>A hash code for the delegate.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600120C RID: 4620 RVA: 0x00049488 File Offset: 0x00047688
		public override int GetHashCode()
		{
			MethodInfo methodInfo = this.Method;
			return ((methodInfo != null) ? methodInfo.GetHashCode() : base.GetType().GetHashCode()) ^ RuntimeHelpers.GetHashCode(this.m_target);
		}

		/// <summary>Gets the static method represented by the current delegate.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> describing the static method represented by the current delegate.</returns>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private). </exception>
		// Token: 0x0600120D RID: 4621 RVA: 0x000494C4 File Offset: 0x000476C4
		protected virtual MethodInfo GetMethodImpl()
		{
			if (this.method_info != null)
			{
				return this.method_info;
			}
			if (this.method != IntPtr.Zero)
			{
				if (!this.method_is_virtual)
				{
					this.method_info = (MethodInfo)RuntimeMethodInfo.GetMethodFromHandleNoGenericCheck(new RuntimeMethodHandle(this.method));
				}
				else
				{
					this.method_info = this.GetVirtualMethod_internal();
				}
			}
			return this.method_info;
		}

		/// <summary>Not supported.</summary>
		/// <param name="info">Not supported. </param>
		/// <param name="context">Not supported. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600120E RID: 4622 RVA: 0x0004952F File Offset: 0x0004772F
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			DelegateSerializationHolder.GetDelegateData(this, info, context);
		}

		/// <summary>Returns the invocation list of the delegate.</summary>
		/// <returns>An array of delegates representing the invocation list of the current delegate.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600120F RID: 4623 RVA: 0x00049539 File Offset: 0x00047739
		public virtual Delegate[] GetInvocationList()
		{
			return new Delegate[] { this };
		}

		/// <summary>Concatenates the invocation lists of two delegates.</summary>
		/// <returns>A new delegate with an invocation list that concatenates the invocation lists of <paramref name="a" /> and <paramref name="b" /> in that order. Returns <paramref name="a" /> if <paramref name="b" /> is null, returns <paramref name="b" /> if <paramref name="a" /> is a null reference, and returns a null reference if both <paramref name="a" /> and <paramref name="b" /> are null references.</returns>
		/// <param name="a">The delegate whose invocation list comes first. </param>
		/// <param name="b">The delegate whose invocation list comes last. </param>
		/// <exception cref="T:System.ArgumentException">Both <paramref name="a" /> and <paramref name="b" /> are not null, and <paramref name="a" /> and <paramref name="b" /> are not instances of the same delegate type. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001210 RID: 4624 RVA: 0x00049548 File Offset: 0x00047748
		public static Delegate Combine(Delegate a, Delegate b)
		{
			if (a == null)
			{
				return b;
			}
			if (b == null)
			{
				return a;
			}
			if (a.GetType() != b.GetType())
			{
				throw new ArgumentException(string.Format("Incompatible Delegate Types. First is {0} second is {1}.", a.GetType().FullName, b.GetType().FullName));
			}
			return a.CombineImpl(b);
		}

		/// <summary>Concatenates the invocation lists of an array of delegates.</summary>
		/// <returns>A new delegate with an invocation list that concatenates the invocation lists of the delegates in the <paramref name="delegates" /> array. Returns null if <paramref name="delegates" /> is null, if <paramref name="delegates" /> contains zero elements, or if every entry in <paramref name="delegates" /> is null.</returns>
		/// <param name="delegates">The array of delegates to combine. </param>
		/// <exception cref="T:System.ArgumentException">Not all the non-null entries in <paramref name="delegates" /> are instances of the same delegate type. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001211 RID: 4625 RVA: 0x000495A0 File Offset: 0x000477A0
		[ComVisible(true)]
		public static Delegate Combine(params Delegate[] delegates)
		{
			if (delegates == null)
			{
				return null;
			}
			Delegate @delegate = null;
			foreach (Delegate delegate2 in delegates)
			{
				@delegate = Delegate.Combine(@delegate, delegate2);
			}
			return @delegate;
		}

		/// <summary>Concatenates the invocation lists of the specified multicast (combinable) delegate and the current multicast (combinable) delegate.</summary>
		/// <returns>A new multicast (combinable) delegate with an invocation list that concatenates the invocation list of the current multicast (combinable) delegate and the invocation list of <paramref name="d" />, or the current multicast (combinable) delegate if <paramref name="d" /> is null.</returns>
		/// <param name="d">The multicast (combinable) delegate whose invocation list to append to the end of the invocation list of the current multicast (combinable) delegate. </param>
		/// <exception cref="T:System.MulticastNotSupportedException">Always thrown. </exception>
		// Token: 0x06001212 RID: 4626 RVA: 0x000495D1 File Offset: 0x000477D1
		protected virtual Delegate CombineImpl(Delegate d)
		{
			throw new MulticastNotSupportedException(string.Empty);
		}

		/// <summary>Removes the last occurrence of the invocation list of a delegate from the invocation list of another delegate.</summary>
		/// <returns>A new delegate with an invocation list formed by taking the invocation list of <paramref name="source" /> and removing the last occurrence of the invocation list of <paramref name="value" />, if the invocation list of <paramref name="value" /> is found within the invocation list of <paramref name="source" />. Returns <paramref name="source" /> if <paramref name="value" /> is null or if the invocation list of <paramref name="value" /> is not found within the invocation list of <paramref name="source" />. Returns a null reference if the invocation list of <paramref name="value" /> is equal to the invocation list of <paramref name="source" /> or if <paramref name="source" /> is a null reference.</returns>
		/// <param name="source">The delegate from which to remove the invocation list of <paramref name="value" />. </param>
		/// <param name="value">The delegate that supplies the invocation list to remove from the invocation list of <paramref name="source" />. </param>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private). </exception>
		/// <exception cref="T:System.ArgumentException">The delegate types do not match.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001213 RID: 4627 RVA: 0x000495E0 File Offset: 0x000477E0
		public static Delegate Remove(Delegate source, Delegate value)
		{
			if (source == null)
			{
				return null;
			}
			if (value == null)
			{
				return source;
			}
			if (source.GetType() != value.GetType())
			{
				throw new ArgumentException(string.Format("Incompatible Delegate Types. First is {0} second is {1}.", source.GetType().FullName, value.GetType().FullName));
			}
			return source.RemoveImpl(value);
		}

		/// <summary>Removes the invocation list of a delegate from the invocation list of another delegate.</summary>
		/// <returns>A new delegate with an invocation list formed by taking the invocation list of the current delegate and removing the invocation list of <paramref name="value" />, if the invocation list of <paramref name="value" /> is found within the current delegate's invocation list. Returns the current delegate if <paramref name="value" /> is null or if the invocation list of <paramref name="value" /> is not found within the current delegate's invocation list. Returns null if the invocation list of <paramref name="value" /> is equal to the current delegate's invocation list.</returns>
		/// <param name="d">The delegate that supplies the invocation list to remove from the invocation list of the current delegate. </param>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private). </exception>
		// Token: 0x06001214 RID: 4628 RVA: 0x00049637 File Offset: 0x00047837
		protected virtual Delegate RemoveImpl(Delegate d)
		{
			if (this.Equals(d))
			{
				return null;
			}
			return this;
		}

		/// <summary>Determines whether the specified delegates are equal.</summary>
		/// <returns>true if <paramref name="d1" /> is equal to <paramref name="d2" />; otherwise, false.</returns>
		/// <param name="d1">The first delegate to compare. </param>
		/// <param name="d2">The second delegate to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001215 RID: 4629 RVA: 0x00049645 File Offset: 0x00047845
		public static bool operator ==(Delegate d1, Delegate d2)
		{
			if (d1 == null)
			{
				return d2 == null;
			}
			return d2 != null && d1.Equals(d2);
		}

		/// <summary>Determines whether the specified delegates are not equal.</summary>
		/// <returns>true if <paramref name="d1" /> is not equal to <paramref name="d2" />; otherwise, false.</returns>
		/// <param name="d1">The first delegate to compare. </param>
		/// <param name="d2">The second delegate to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001216 RID: 4630 RVA: 0x0004965D File Offset: 0x0004785D
		public static bool operator !=(Delegate d1, Delegate d2)
		{
			return !(d1 == d2);
		}

		// Token: 0x06001217 RID: 4631
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MulticastDelegate AllocDelegateLike_internal(Delegate d);

		// Token: 0x04000743 RID: 1859
		private IntPtr method_ptr;

		// Token: 0x04000744 RID: 1860
		private IntPtr invoke_impl;

		// Token: 0x04000745 RID: 1861
		private object m_target;

		// Token: 0x04000746 RID: 1862
		private IntPtr method;

		// Token: 0x04000747 RID: 1863
		private IntPtr delegate_trampoline;

		// Token: 0x04000748 RID: 1864
		private IntPtr extra_arg;

		// Token: 0x04000749 RID: 1865
		private IntPtr method_code;

		// Token: 0x0400074A RID: 1866
		private IntPtr interp_method;

		// Token: 0x0400074B RID: 1867
		private IntPtr interp_invoke_impl;

		// Token: 0x0400074C RID: 1868
		private MethodInfo method_info;

		// Token: 0x0400074D RID: 1869
		private MethodInfo original_method_info;

		// Token: 0x0400074E RID: 1870
		private DelegateData data;

		// Token: 0x0400074F RID: 1871
		private bool method_is_virtual;
	}
}
