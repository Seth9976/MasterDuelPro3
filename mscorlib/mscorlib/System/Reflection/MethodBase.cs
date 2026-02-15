using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection
{
	/// <summary>Provides information about methods and constructors. </summary>
	// Token: 0x0200060B RID: 1547
	[Serializable]
	public abstract class MethodBase : MemberInfo
	{
		/// <summary>When overridden in a derived class, gets the parameters of the specified method or constructor.</summary>
		/// <returns>An array of type ParameterInfo containing information that matches the signature of the method (or constructor) reflected by this MethodBase instance.</returns>
		// Token: 0x06002CFD RID: 11517
		public abstract ParameterInfo[] GetParameters();

		/// <summary>Gets the attributes associated with this method.</summary>
		/// <returns>One of the <see cref="T:System.Reflection.MethodAttributes" /> values.</returns>
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06002CFE RID: 11518
		public abstract MethodAttributes Attributes { get; }

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Reflection.MethodImplAttributes" /> flags.</summary>
		/// <returns>The MethodImplAttributes flags.</returns>
		// Token: 0x06002CFF RID: 11519
		public abstract MethodImplAttributes GetMethodImplementationFlags();

		/// <summary>Gets a value indicating the calling conventions for this method.</summary>
		/// <returns>The <see cref="T:System.Reflection.CallingConventions" /> for this method.</returns>
		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x0000C091 File Offset: 0x0000A291
		public virtual CallingConventions CallingConvention
		{
			get
			{
				return CallingConventions.Standard;
			}
		}

		/// <summary>Gets a value indicating whether the method is abstract.</summary>
		/// <returns>true if the method is abstract; otherwise, false.</returns>
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06002D01 RID: 11521 RVA: 0x000B1EDC File Offset: 0x000B00DC
		public bool IsAbstract
		{
			get
			{
				return (this.Attributes & MethodAttributes.Abstract) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the method is a constructor.</summary>
		/// <returns>true if this method is a constructor represented by a <see cref="T:System.Reflection.ConstructorInfo" /> object (see note in Remarks about <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> objects); otherwise, false.</returns>
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06002D02 RID: 11522 RVA: 0x000B1EED File Offset: 0x000B00ED
		public bool IsConstructor
		{
			get
			{
				return this is ConstructorInfo && !this.IsStatic && (this.Attributes & MethodAttributes.RTSpecialName) == MethodAttributes.RTSpecialName;
			}
		}

		/// <summary>Gets a value indicating whether this method has a special name.</summary>
		/// <returns>true if this method has a special name; otherwise, false.</returns>
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x000B1F14 File Offset: 0x000B0114
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & MethodAttributes.SpecialName) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the method is static.</summary>
		/// <returns>true if this method is static; otherwise, false.</returns>
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002D04 RID: 11524 RVA: 0x000B1F25 File Offset: 0x000B0125
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & MethodAttributes.Static) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the method is virtual.</summary>
		/// <returns>true if this method is virtual; otherwise, false.</returns>
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06002D05 RID: 11525 RVA: 0x000B1F33 File Offset: 0x000B0133
		public bool IsVirtual
		{
			get
			{
				return (this.Attributes & MethodAttributes.Virtual) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether this is a public method.</summary>
		/// <returns>true if this method is public; otherwise, false.</returns>
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06002D06 RID: 11526 RVA: 0x000B1F41 File Offset: 0x000B0141
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
			}
		}

		/// <summary>Gets a value indicating whether the method is generic.</summary>
		/// <returns>true if the current <see cref="T:System.Reflection.MethodBase" /> represents a generic method; otherwise, false.</returns>
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06002D07 RID: 11527 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the method is a generic method definition.</summary>
		/// <returns>true if the current <see cref="T:System.Reflection.MethodBase" /> object represents the definition of a generic method; otherwise, false.</returns>
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06002D08 RID: 11528 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic method or the type parameters of a generic method definition.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic method or the type parameters of a generic method definition. Returns an empty array if the current method is not a generic method.</returns>
		/// <exception cref="T:System.NotSupportedException">The current object is a <see cref="T:System.Reflection.ConstructorInfo" />. Generic constructors are not supported in the .NET Framework version 2.0. This exception is the default behavior if this method is not overridden in a derived class.</exception>
		// Token: 0x06002D09 RID: 11529 RVA: 0x000339C9 File Offset: 0x00031BC9
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Gets a value indicating whether the generic method contains unassigned generic type parameters.</summary>
		/// <returns>true if the current <see cref="T:System.Reflection.MethodBase" /> object represents a generic method that contains unassigned generic type parameters; otherwise, false.</returns>
		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		/// <summary>Invokes the method or constructor represented by the current instance, using the specified parameters.</summary>
		/// <returns>An object containing the return value of the invoked method, or null in the case of a constructor.CautionElements of the <paramref name="parameters" /> array that represent parameters declared with the ref or out keyword may also be modified.</returns>
		/// <param name="obj">The object on which to invoke the method or constructor. If a method is static, this argument is ignored. If a constructor is static, this argument must be null or an instance of the class that defines the constructor. </param>
		/// <param name="parameters">An argument list for the invoked method or constructor. This is an array of objects with the same number, order, and type as the parameters of the method or constructor to be invoked. If there are no parameters, <paramref name="parameters" /> should be null.If the method or constructor represented by this instance takes a ref parameter (ByRef in Visual Basic), no special attribute is required for that parameter in order to invoke the method or constructor using this function. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or false, depending on the specific element type. </param>
		/// <exception cref="T:System.Reflection.TargetException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.The <paramref name="obj" /> parameter is null and the method is not static.-or- The method is not declared or inherited by the class of <paramref name="obj" />. -or-A static constructor is invoked, and <paramref name="obj" /> is neither null nor an instance of the class that declared the constructor.</exception>
		/// <exception cref="T:System.ArgumentException">The elements of the <paramref name="parameters" />array do not match the signature of the method or constructor reflected by this instance. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked method or constructor throws an exception. -or-The current instance is a <see cref="T:System.Reflection.Emit.DynamicMethod" /> that contains unverifiable code. See the "Verification" section in Remarks for <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The <paramref name="parameters" /> array does not have the correct number of arguments. </exception>
		/// <exception cref="T:System.MethodAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to execute the method or constructor that is represented by the current instance. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type that declares the method is an open generic type. That is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns true for the declaring type.</exception>
		/// <exception cref="T:System.NotSupportedException">The current instance is a <see cref="T:System.Reflection.Emit.MethodBuilder" />.</exception>
		// Token: 0x06002D0B RID: 11531 RVA: 0x000B1F4E File Offset: 0x000B014E
		[DebuggerStepThrough]
		[DebuggerHidden]
		public object Invoke(object obj, object[] parameters)
		{
			return this.Invoke(obj, BindingFlags.Default, null, parameters, null);
		}

		/// <summary>When overridden in a derived class, invokes the reflected method or constructor with the given parameters.</summary>
		/// <returns>An Object containing the return value of the invoked method, or null in the case of a constructor, or null if the method's return type is void. Before calling the method or constructor, Invoke checks to see if the user has access permission and verifies that the parameters are valid.CautionElements of the <paramref name="parameters" /> array that represent parameters declared with the ref or out keyword may also be modified.</returns>
		/// <param name="obj">The object on which to invoke the method or constructor. If a method is static, this argument is ignored. If a constructor is static, this argument must be null or an instance of the class that defines the constructor.</param>
		/// <param name="invokeAttr">A bitmask that is a combination of 0 or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. If <paramref name="binder" /> is null, this parameter is assigned the value <see cref="F:System.Reflection.BindingFlags.Default" />; thus, whatever you pass in is ignored. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects via reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="parameters">An argument list for the invoked method or constructor. This is an array of objects with the same number, order, and type as the parameters of the method or constructor to be invoked. If there are no parameters, this should be null.If the method or constructor represented by this instance takes a ByRef parameter, there is no special attribute required for that parameter in order to invoke the method or constructor using this function. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or false, depending on the specific element type. </param>
		/// <param name="culture">An instance of CultureInfo used to govern the coercion of types. If this is null, the CultureInfo for the current thread is used. (This is necessary to convert a String that represents 1000 to a Double value, for example, since 1000 is represented differently by different cultures.) </param>
		/// <exception cref="T:System.Reflection.TargetException">The <paramref name="obj" /> parameter is null and the method is not static.-or- The method is not declared or inherited by the class of <paramref name="obj" />. -or-A static constructor is invoked, and <paramref name="obj" /> is neither null nor an instance of the class that declared the constructor.</exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="parameters" /> parameter does not match the signature of the method or constructor reflected by this instance. </exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The <paramref name="parameters" /> array does not have the correct number of arguments. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked method or constructor throws an exception. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to execute the method or constructor that is represented by the current instance. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type that declares the method is an open generic type. That is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns true for the declaring type.</exception>
		// Token: 0x06002D0C RID: 11532
		public abstract object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		/// <summary>Gets a handle to the internal metadata representation of a method.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> object.</returns>
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06002D0D RID: 11533
		public abstract RuntimeMethodHandle MethodHandle { get; }

		/// <summary>Gets a value that indicates whether the current method or constructor is security-critical or security-safe-critical at the current trust level, and therefore can perform critical operations. </summary>
		/// <returns>true if the current method or constructor is security-critical or security-safe-critical at the current trust level; false if it is transparent. </returns>
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual bool IsSecurityCritical
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06002D0F RID: 11535 RVA: 0x000B1944 File Offset: 0x000AFB44
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002D10 RID: 11536 RVA: 0x000B194D File Offset: 0x000AFB4D
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.MethodBase" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002D11 RID: 11537 RVA: 0x000B1F5C File Offset: 0x000B015C
		public static bool operator ==(MethodBase left, MethodBase right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			MethodInfo methodInfo;
			MethodInfo methodInfo2;
			if ((methodInfo = left as MethodInfo) != null && (methodInfo2 = right as MethodInfo) != null)
			{
				return methodInfo == methodInfo2;
			}
			ConstructorInfo constructorInfo;
			ConstructorInfo constructorInfo2;
			return (constructorInfo = left as ConstructorInfo) != null && (constructorInfo2 = right as ConstructorInfo) != null && constructorInfo == constructorInfo2;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.MethodBase" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002D12 RID: 11538 RVA: 0x000B1FC8 File Offset: 0x000B01C8
		public static bool operator !=(MethodBase left, MethodBase right)
		{
			return !(left == right);
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000B1FD4 File Offset: 0x000B01D4
		internal virtual ParameterInfo[] GetParametersInternal()
		{
			return this.GetParameters();
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000B1FDC File Offset: 0x000B01DC
		internal virtual int GetParametersCount()
		{
			return this.GetParametersInternal().Length;
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000B1FE6 File Offset: 0x000B01E6
		internal virtual int get_next_table_index(object obj, int table, int count)
		{
			if (this is MethodBuilder)
			{
				return ((MethodBuilder)this).get_next_table_index(obj, table, count);
			}
			if (this is ConstructorBuilder)
			{
				return ((ConstructorBuilder)this).get_next_table_index(obj, table, count);
			}
			throw new Exception("Method is not a builder method");
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000B2020 File Offset: 0x000B0220
		internal virtual string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Name);
			stringBuilder.Append("(");
			stringBuilder.Append(MethodBase.ConstructParameters(this.GetParameterTypes(), this.CallingConvention, serialization));
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000B2070 File Offset: 0x000B0270
		internal virtual Type[] GetParameterTypes()
		{
			ParameterInfo[] parametersNoCopy = this.GetParametersNoCopy();
			Type[] array = new Type[parametersNoCopy.Length];
			for (int i = 0; i < parametersNoCopy.Length; i++)
			{
				array[i] = parametersNoCopy[i].ParameterType;
			}
			return array;
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000B1FD4 File Offset: 0x000B01D4
		internal virtual ParameterInfo[] GetParametersNoCopy()
		{
			return this.GetParameters();
		}

		/// <summary>Gets method information by using the method's internal metadata representation (handle).</summary>
		/// <returns>A MethodBase containing information about the method.</returns>
		/// <param name="handle">The method's handle. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.</exception>
		// Token: 0x06002D19 RID: 11545 RVA: 0x000B20A8 File Offset: 0x000B02A8
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("The handle is invalid."));
			}
			MethodBase methodFromHandleInternalType = RuntimeMethodInfo.GetMethodFromHandleInternalType(handle.Value, IntPtr.Zero);
			if (methodFromHandleInternalType == null)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			Type declaringType = methodFromHandleInternalType.DeclaringType;
			if (declaringType != null && declaringType.IsGenericType)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cannot resolve method {0} because the declaring type of the method handle {1} is generic. Explicitly provide the declaring type to GetMethodFromHandle."), methodFromHandleInternalType, declaringType.GetGenericTypeDefinition()));
			}
			return methodFromHandleInternalType;
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodBase" /> object for the constructor or method represented by the specified handle, for the specified generic type.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> object representing the method or constructor specified by <paramref name="handle" />, in the generic type specified by <paramref name="declaringType" />.</returns>
		/// <param name="handle">A handle to the internal metadata representation of a constructor or method.</param>
		/// <param name="declaringType">A handle to the generic type that defines the constructor or method.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.</exception>
		// Token: 0x06002D1A RID: 11546 RVA: 0x000B2130 File Offset: 0x000B0330
		[ComVisible(false)]
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("The handle is invalid."));
			}
			MethodBase methodFromHandleInternalType = RuntimeMethodInfo.GetMethodFromHandleInternalType(handle.Value, declaringType.Value);
			if (methodFromHandleInternalType == null)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			return methodFromHandleInternalType;
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000B2180 File Offset: 0x000B0380
		internal static string ConstructParameters(Type[] parameterTypes, CallingConventions callingConvention, bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (Type type in parameterTypes)
			{
				stringBuilder.Append(text);
				string text2 = type.FormatTypeName(serialization);
				if (type.IsByRef && !serialization)
				{
					stringBuilder.Append(text2.TrimEnd(new char[] { '&' }));
					stringBuilder.Append(" ByRef");
				}
				else
				{
					stringBuilder.Append(text2);
				}
				text = ", ";
			}
			if ((callingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				stringBuilder.Append(text);
				stringBuilder.Append("...");
			}
			return stringBuilder.ToString();
		}
	}
}
