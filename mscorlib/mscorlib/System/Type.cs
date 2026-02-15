using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	/// <summary>Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000158 RID: 344
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_Type))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public abstract class Type : MemberInfo, _Type
	{
		/// <summary>Returns a value that indicates whether the specified value exists in the current enumeration type.</summary>
		/// <returns>true if the specified value is a member of the current enumeration type; otherwise, false.</returns>
		/// <param name="value">The value to be tested.</param>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> is of a type that cannot be the underlying type of an enumeration.</exception>
		// Token: 0x06000BA2 RID: 2978 RVA: 0x00033098 File Offset: 0x00031298
		public virtual bool IsEnumDefined(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			Type type = value.GetType();
			if (type.IsEnum)
			{
				if (!type.IsEquivalentTo(this))
				{
					throw new ArgumentException(SR.Format("Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.", type.ToString(), this.ToString()));
				}
				type = type.GetEnumUnderlyingType();
			}
			if (type == typeof(string))
			{
				object[] enumNames = this.GetEnumNames();
				return Array.IndexOf<object>(enumNames, value) >= 0;
			}
			if (!Type.IsIntegerType(type))
			{
				throw new InvalidOperationException("Unknown enum type.");
			}
			Type enumUnderlyingType = this.GetEnumUnderlyingType();
			if (enumUnderlyingType.GetTypeCodeImpl() != type.GetTypeCodeImpl())
			{
				throw new ArgumentException(SR.Format("Enum underlying type and the object must be same type or object must be a String. Type passed in was '{0}'; the enum underlying type was '{1}'.", type.ToString(), enumUnderlyingType.ToString()));
			}
			return Type.BinarySearch(this.GetEnumRawConstantValues(), value) >= 0;
		}

		/// <summary>Returns the name of the constant that has the specified value, for the current enumeration type.</summary>
		/// <returns>The name of the member of the current enumeration type that has the specified value, or null if no such constant is found.</returns>
		/// <param name="value">The value whose name is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.-or-<paramref name="value" /> is neither of the current type nor does it have the same underlying type as the current type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000BA3 RID: 2979 RVA: 0x00033184 File Offset: 0x00031384
		public virtual string GetEnumName(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			Type type = value.GetType();
			if (!type.IsEnum && !Type.IsIntegerType(type))
			{
				throw new ArgumentException("The value passed in must be an enum base or an underlying type for an enum, such as an Int32.", "value");
			}
			int num = Type.BinarySearch(this.GetEnumRawConstantValues(), value);
			if (num >= 0)
			{
				return this.GetEnumNames()[num];
			}
			return null;
		}

		/// <summary>Returns the names of the members of the current enumeration type.</summary>
		/// <returns>An array that contains the names of the members of the enumeration.</returns>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.</exception>
		// Token: 0x06000BA4 RID: 2980 RVA: 0x000331FC File Offset: 0x000313FC
		public virtual string[] GetEnumNames()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			string[] array;
			Array array2;
			this.GetEnumData(out array, out array2);
			return array;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0003322C File Offset: 0x0003142C
		private Array GetEnumRawConstantValues()
		{
			string[] array;
			Array array2;
			this.GetEnumData(out array, out array2);
			return array2;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00033244 File Offset: 0x00031444
		private void GetEnumData(out string[] enumNames, out Array enumValues)
		{
			FieldInfo[] fields = this.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			object[] array = new object[fields.Length];
			string[] array2 = new string[fields.Length];
			for (int i = 0; i < fields.Length; i++)
			{
				array2[i] = fields[i].Name;
				array[i] = fields[i].GetRawConstantValue();
			}
			IComparer @default = Comparer<object>.Default;
			for (int j = 1; j < array.Length; j++)
			{
				int num = j;
				string text = array2[j];
				object obj = array[j];
				bool flag = false;
				while (@default.Compare(array[num - 1], obj) > 0)
				{
					array2[num] = array2[num - 1];
					array[num] = array[num - 1];
					num--;
					flag = true;
					if (num == 0)
					{
						break;
					}
				}
				if (flag)
				{
					array2[num] = text;
					array[num] = obj;
				}
			}
			enumNames = array2;
			enumValues = array;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00033310 File Offset: 0x00031510
		private static int BinarySearch(Array array, object value)
		{
			ulong[] array2 = new ulong[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = Enum.ToUInt64(array.GetValue(i));
			}
			ulong num = Enum.ToUInt64(value);
			return Array.BinarySearch<ulong>(array2, num);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00033358 File Offset: 0x00031558
		internal static bool IsIntegerType(Type t)
		{
			return t == typeof(int) || t == typeof(short) || t == typeof(ushort) || t == typeof(byte) || t == typeof(sbyte) || t == typeof(uint) || t == typeof(long) || t == typeof(ulong) || t == typeof(char) || t == typeof(bool);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is serializable.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is serializable; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00033420 File Offset: 0x00031620
		public virtual bool IsSerializable
		{
			get
			{
				if ((this.GetAttributeFlagsImpl() & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
				{
					return true;
				}
				Type type = this.UnderlyingSystemType;
				if (type.IsRuntimeImplemented())
				{
					while (!(type == typeof(Delegate)) && !(type == typeof(Enum)))
					{
						type = type.BaseType;
						if (!(type != null))
						{
							return false;
						}
					}
					return true;
				}
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> object has type parameters that have not been replaced by specific types.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> object is itself a generic type parameter or has type parameters for which specific types have not been supplied; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00033484 File Offset: 0x00031684
		public virtual bool ContainsGenericParameters
		{
			get
			{
				if (this.HasElementType)
				{
					return this.GetRootElementType().ContainsGenericParameters;
				}
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (!this.IsGenericType)
				{
					return false;
				}
				Type[] genericArguments = this.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (genericArguments[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x000334DC File Offset: 0x000316DC
		internal Type GetRootElementType()
		{
			Type type = this;
			while (type.HasElementType)
			{
				type = type.GetElementType();
			}
			return type;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> can be accessed by code outside the assembly.</summary>
		/// <returns>true if the current <see cref="T:System.Type" /> is a public type or a public nested type such that all the enclosing types are public; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00033500 File Offset: 0x00031700
		public bool IsVisible
		{
			get
			{
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (this.HasElementType)
				{
					return this.GetElementType().IsVisible;
				}
				Type type = this;
				while (type.IsNested)
				{
					if (!type.IsNestedPublic)
					{
						return false;
					}
					type = type.DeclaringType;
				}
				if (!type.IsPublic)
				{
					return false;
				}
				if (this.IsGenericType && !this.IsGenericTypeDefinition)
				{
					Type[] genericArguments = this.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						if (!genericArguments[i].IsVisible)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		/// <summary>Determines whether the class represented by the current <see cref="T:System.Type" /> derives from the class represented by the specified <see cref="T:System.Type" />.</summary>
		/// <returns>true if the Type represented by the <paramref name="c" /> parameter and the current Type represent classes, and the class represented by the current Type derives from the class represented by <paramref name="c" />; otherwise, false. This method also returns false if <paramref name="c" /> and the current Type represent the same class.</returns>
		/// <param name="c">The type to compare with the current type. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="c" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BAD RID: 2989 RVA: 0x00033584 File Offset: 0x00031784
		[ComVisible(true)]
		public virtual bool IsSubclassOf(Type c)
		{
			Type type = this;
			if (type == c)
			{
				return false;
			}
			while (type != null)
			{
				if (type == c)
				{
					return true;
				}
				type = type.BaseType;
			}
			return false;
		}

		/// <summary>Determines whether an instance of the current <see cref="T:System.Type" /> can be assigned from an instance of the specified Type.</summary>
		/// <returns>true if <paramref name="c" /> and the current Type represent the same type, or if the current Type is in the inheritance hierarchy of <paramref name="c" />, or if the current Type is an interface that <paramref name="c" /> implements, or if <paramref name="c" /> is a generic type parameter and the current Type represents one of the constraints of <paramref name="c" />, or if <paramref name="c" /> represents a value type and the current Type represents Nullable&lt;c&gt; (Nullable(Of c) in Visual Basic). false if none of these conditions are true, or if <paramref name="c" /> is null.</returns>
		/// <param name="c">The type to compare with the current type. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BAE RID: 2990 RVA: 0x000335BC File Offset: 0x000317BC
		public virtual bool IsAssignableFrom(Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (this == c)
			{
				return true;
			}
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType.IsRuntimeImplemented())
			{
				return underlyingSystemType.IsAssignableFrom(c);
			}
			if (c.IsSubclassOf(this))
			{
				return true;
			}
			if (this.IsInterface)
			{
				return c.ImplementInterface(this);
			}
			if (this.IsGenericParameter)
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				for (int i = 0; i < genericParameterConstraints.Length; i++)
				{
					if (!genericParameterConstraints[i].IsAssignableFrom(c))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00033640 File Offset: 0x00031840
		internal bool ImplementInterface(Type ifaceType)
		{
			Type type = this;
			while (type != null)
			{
				Type[] interfaces = type.GetInterfaces();
				if (interfaces != null)
				{
					for (int i = 0; i < interfaces.Length; i++)
					{
						if (interfaces[i] == ifaceType || (interfaces[i] != null && interfaces[i].ImplementInterface(ifaceType)))
						{
							return true;
						}
					}
				}
				type = type.BaseType;
			}
			return false;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000336A0 File Offset: 0x000318A0
		private static bool FilterAttributeImpl(MemberInfo m, object filterCriteria)
		{
			if (filterCriteria == null)
			{
				throw new InvalidFilterCriteriaException("An Int32 must be provided for the filter criteria.");
			}
			MemberTypes memberType = m.MemberType;
			if (memberType != MemberTypes.Constructor)
			{
				if (memberType == MemberTypes.Field)
				{
					FieldAttributes fieldAttributes = FieldAttributes.PrivateScope;
					try
					{
						fieldAttributes = (FieldAttributes)((int)filterCriteria);
					}
					catch
					{
						throw new InvalidFilterCriteriaException("An Int32 must be provided for the filter criteria.");
					}
					FieldAttributes attributes = ((FieldInfo)m).Attributes;
					return ((fieldAttributes & FieldAttributes.FieldAccessMask) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.FieldAccessMask) == (fieldAttributes & FieldAttributes.FieldAccessMask)) && ((fieldAttributes & FieldAttributes.Static) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.InitOnly) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.InitOnly) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.Literal) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.Literal) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.NotSerialized) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.PinvokeImpl) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.PinvokeImpl) != FieldAttributes.PrivateScope);
				}
				if (memberType != MemberTypes.Method)
				{
					return false;
				}
			}
			MethodAttributes methodAttributes = MethodAttributes.PrivateScope;
			try
			{
				methodAttributes = (MethodAttributes)((int)filterCriteria);
			}
			catch
			{
				throw new InvalidFilterCriteriaException("An Int32 must be provided for the filter criteria.");
			}
			MethodAttributes methodAttributes2;
			if (m.MemberType == MemberTypes.Method)
			{
				methodAttributes2 = ((MethodInfo)m).Attributes;
			}
			else
			{
				methodAttributes2 = ((ConstructorInfo)m).Attributes;
			}
			return ((methodAttributes & MethodAttributes.MemberAccessMask) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.MemberAccessMask) == (methodAttributes & MethodAttributes.MemberAccessMask)) && ((methodAttributes & MethodAttributes.Static) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Static) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Final) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Final) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Virtual) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Virtual) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Abstract) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Abstract) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.SpecialName) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.SpecialName) != MethodAttributes.PrivateScope);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0003381C File Offset: 0x00031A1C
		private static bool FilterNameImpl(MemberInfo m, object filterCriteria)
		{
			if (filterCriteria == null || !(filterCriteria is string))
			{
				throw new InvalidFilterCriteriaException("A String must be provided for the filter criteria.");
			}
			string text = (string)filterCriteria;
			text = text.Trim();
			string text2 = m.Name;
			if (m.MemberType == MemberTypes.NestedType)
			{
				text2 = text2.Substring(text2.LastIndexOf('+') + 1);
			}
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				text = text.Substring(0, text.Length - 1);
				return text2.StartsWith(text, StringComparison.Ordinal);
			}
			return text2.Equals(text);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000338B0 File Offset: 0x00031AB0
		private static bool FilterNameIgnoreCaseImpl(MemberInfo m, object filterCriteria)
		{
			if (filterCriteria == null || !(filterCriteria is string))
			{
				throw new InvalidFilterCriteriaException("A String must be provided for the filter criteria.");
			}
			string text = (string)filterCriteria;
			text = text.Trim();
			string text2 = m.Name;
			if (m.MemberType == MemberTypes.NestedType)
			{
				text2 = text2.Substring(text2.LastIndexOf('+') + 1);
			}
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				text = text.Substring(0, text.Length - 1);
				return string.Compare(text2, 0, text, 0, text.Length, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return string.Compare(text, text2, StringComparison.OrdinalIgnoreCase) == 0;
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a type or a nested type.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a type or a nested type.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x00033958 File Offset: 0x00031B58
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.TypeInfo;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Type" />.</summary>
		/// <returns>The current <see cref="T:System.Type" />.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BB5 RID: 2997 RVA: 0x0003395C File Offset: 0x00031B5C
		public new Type GetType()
		{
			return base.GetType();
		}

		/// <summary>Gets the namespace of the <see cref="T:System.Type" />.</summary>
		/// <returns>The namespace of the <see cref="T:System.Type" />; null if the current instance has no namespace or represents a generic parameter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000BB6 RID: 2998
		public abstract string Namespace { get; }

		/// <summary>Gets the assembly-qualified name of the <see cref="T:System.Type" />, which includes the name of the assembly from which the <see cref="T:System.Type" /> was loaded.</summary>
		/// <returns>The assembly-qualified name of the <see cref="T:System.Type" />, which includes the name of the assembly from which the <see cref="T:System.Type" /> was loaded, or null if the current instance represents a generic type parameter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000BB7 RID: 2999
		public abstract string AssemblyQualifiedName { get; }

		/// <summary>Gets the fully qualified name of the <see cref="T:System.Type" />, including the namespace of the <see cref="T:System.Type" /> but not the assembly.</summary>
		/// <returns>The fully qualified name of the <see cref="T:System.Type" />, including the namespace of the <see cref="T:System.Type" /> but not the assembly; or null if the current instance represents a generic type parameter, an array type, pointer type, or byref type based on a type parameter, or a generic type that is not a generic type definition but contains unresolved type parameters.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000BB8 RID: 3000
		public abstract string FullName { get; }

		/// <summary>Gets the <see cref="T:System.Reflection.Assembly" /> in which the type is declared. For generic types, gets the <see cref="T:System.Reflection.Assembly" /> in which the generic type is defined.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> instance that describes the assembly containing the current type. For generic types, the instance describes the assembly that contains the generic type definition, not the assembly that creates and uses a particular constructed type.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000BB9 RID: 3001
		public abstract Assembly Assembly { get; }

		/// <summary>Gets the module (the DLL) in which the current <see cref="T:System.Type" /> is defined.</summary>
		/// <returns>The module in which the current <see cref="T:System.Type" /> is defined.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000BBA RID: 3002
		public new abstract Module Module { get; }

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> object represents a type whose definition is nested inside the definition of another type.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested inside another type; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00033964 File Offset: 0x00031B64
		public bool IsNested
		{
			get
			{
				return this.DeclaringType != null;
			}
		}

		/// <summary>Gets the type that declares the current nested type or generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the enclosing type, if the current type is a nested type; or the generic type definition, if the current type is a type parameter of a generic type; or the type that declares the generic method, if the current type is a type parameter of a generic method; otherwise, null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x000082D2 File Offset: 0x000064D2
		public override Type DeclaringType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodBase" /> that represents the declaring method, if the current <see cref="T:System.Type" /> represents a type parameter of a generic method.</summary>
		/// <returns>If the current <see cref="T:System.Type" /> represents a type parameter of a generic method, a <see cref="T:System.Reflection.MethodBase" /> that represents declaring method; otherwise, null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x000082D2 File Offset: 0x000064D2
		public virtual MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the class object that was used to obtain this member. </summary>
		/// <returns>The Type object through which this <see cref="T:System.Type" /> object was obtained. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x000082D2 File Offset: 0x000064D2
		public override Type ReflectedType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Indicates the type provided by the common language runtime that represents this type.</summary>
		/// <returns>The underlying system type for the <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000BBF RID: 3007
		public abstract Type UnderlyingSystemType { get; }

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is an array.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an array; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00033972 File Offset: 0x00031B72
		public bool IsArray
		{
			get
			{
				return this.IsArrayImpl();
			}
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsArray" /> property and determines whether the <see cref="T:System.Type" /> is an array.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an array; otherwise, false.</returns>
		// Token: 0x06000BC1 RID: 3009
		protected abstract bool IsArrayImpl();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is passed by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is passed by reference; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0003397A File Offset: 0x00031B7A
		public bool IsByRef
		{
			get
			{
				return this.IsByRefImpl();
			}
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsByRef" /> property and determines whether the <see cref="T:System.Type" /> is passed by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is passed by reference; otherwise, false.</returns>
		// Token: 0x06000BC3 RID: 3011
		protected abstract bool IsByRefImpl();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a pointer.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a pointer; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00033982 File Offset: 0x00031B82
		public bool IsPointer
		{
			get
			{
				return this.IsPointerImpl();
			}
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsPointer" /> property and determines whether the <see cref="T:System.Type" /> is a pointer.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a pointer; otherwise, false.</returns>
		// Token: 0x06000BC5 RID: 3013
		protected abstract bool IsPointerImpl();

		/// <summary>Gets a value that indicates whether this object represents a constructed generic type. You can create instances of a constructed generic type. </summary>
		/// <returns>true if this object represents a constructed generic type; otherwise, false.</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual bool IsConstructedGenericType
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> represents a type parameter in the definition of a generic type or method.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> object represents a type parameter of a generic type definition or generic method definition; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00033994 File Offset: 0x00031B94
		public virtual bool IsGenericMethodParameter
		{
			get
			{
				return this.IsGenericParameter && this.DeclaringMethod != null;
			}
		}

		/// <summary>Gets a value indicating whether the current type is a generic type.</summary>
		/// <returns>true if the current type is a generic type; otherwise, false.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> represents a generic type definition, from which other generic types can be constructed.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> object represents a generic type definition; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual bool IsSZArray
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x000339AC File Offset: 0x00031BAC
		public virtual bool IsVariableBoundArray
		{
			get
			{
				return this.IsArray && !this.IsSZArray;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> encompasses or refers to another type; that is, whether the current <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x000339C1 File Offset: 0x00031BC1
		public bool HasElementType
		{
			get
			{
				return this.HasElementTypeImpl();
			}
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.HasElementType" /> property and determines whether the current <see cref="T:System.Type" /> encompasses or refers to another type; that is, whether the current <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference; otherwise, false.</returns>
		// Token: 0x06000BCE RID: 3022
		protected abstract bool HasElementTypeImpl();

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer or reference type.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer, or reference type, or null if the current <see cref="T:System.Type" /> is not an array or a pointer, or is not passed by reference, or represents a generic type or a type parameter in the definition of a generic type or generic method.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BCF RID: 3023
		public abstract Type GetElementType();

		/// <summary>Gets the number of dimensions in an <see cref="T:System.Array" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of dimensions in the current Type.</returns>
		/// <exception cref="T:System.NotSupportedException">The functionality of this method is unsupported in the base class and must be implemented in a derived class instead. </exception>
		/// <exception cref="T:System.ArgumentException">The current Type is not an array. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BD0 RID: 3024 RVA: 0x000339C9 File Offset: 0x00031BC9
		public virtual int GetArrayRank()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a generic type definition from which the current generic type can be constructed.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a generic type from which the current type can be constructed.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type is not a generic type.  That is, <see cref="P:System.Type.IsGenericType" /> returns false. </exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BD1 RID: 3025 RVA: 0x000339C9 File Offset: 0x00031BC9
		public virtual Type GetGenericTypeDefinition()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Gets an array of the generic type arguments for this type.</summary>
		/// <returns>An array of the generic type arguments for this type.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x000339D5 File Offset: 0x00031BD5
		public virtual Type[] GenericTypeArguments
		{
			get
			{
				if (!this.IsGenericType || this.IsGenericTypeDefinition)
				{
					return Array.Empty<Type>();
				}
				return this.GetGenericArguments();
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic type or the type parameters of a generic type definition.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic type. Returns an empty array if the current type is not a generic type.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BD3 RID: 3027 RVA: 0x000339C9 File Offset: 0x00031BC9
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Gets the position of the type parameter in the type parameter list of the generic type or method that declared the parameter, when the <see cref="T:System.Type" /> object represents a type parameter of a generic type or a generic method.</summary>
		/// <returns>The position of a type parameter in the type parameter list of the generic type or method that defines the parameter. Position numbers begin at 0.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type does not represent a type parameter. That is, <see cref="P:System.Type.IsGenericParameter" /> returns false. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x000339F3 File Offset: 0x00031BF3
		public virtual int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException("Method may only be called on a Type for which Type.IsGenericParameter is true.");
			}
		}

		/// <summary>Gets a combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> flags that describe the covariance and special constraints of the current generic type parameter. </summary>
		/// <returns>A bitwise combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> values that describes the covariance and special constraints of the current generic type parameter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Type" /> object is not a generic type parameter. That is, the <see cref="P:System.Type.IsGenericParameter" /> property returns false.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x000339FF File Offset: 0x00031BFF
		public virtual GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects that represent the constraints on the current generic type parameter. </summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the constraints on the current generic type parameter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Type" /> object is not a generic type parameter. That is, the <see cref="P:System.Type.IsGenericParameter" /> property returns false.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BD6 RID: 3030 RVA: 0x00033A06 File Offset: 0x00031C06
		public virtual Type[] GetGenericParameterConstraints()
		{
			if (!this.IsGenericParameter)
			{
				throw new InvalidOperationException("Method may only be called on a Type for which Type.IsGenericParameter is true.");
			}
			throw new InvalidOperationException();
		}

		/// <summary>Gets the attributes associated with the <see cref="T:System.Type" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object representing the attribute set of the <see cref="T:System.Type" />, unless the <see cref="T:System.Type" /> represents a generic type parameter, in which case the value is unspecified. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x00033A20 File Offset: 0x00031C20
		public TypeAttributes Attributes
		{
			get
			{
				return this.GetAttributeFlagsImpl();
			}
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.Attributes" /> property and gets a bitmask indicating the attributes associated with the <see cref="T:System.Type" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object representing the attribute set of the <see cref="T:System.Type" />.</returns>
		// Token: 0x06000BD8 RID: 3032
		protected abstract TypeAttributes GetAttributeFlagsImpl();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is abstract and must be overridden.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is abstract; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x00033A28 File Offset: 0x00031C28
		public bool IsAbstract
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Abstract) > TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> has a <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> attribute applied, indicating that it was imported from a COM type library.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> has a <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x00033A39 File Offset: 0x00031C39
		public bool IsImport
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Import) > TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is declared sealed.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is declared sealed; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00033A4A File Offset: 0x00031C4A
		public bool IsSealed
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Sealed) > TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a class; that is, not a value type or interface.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a class; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x00033A5B File Offset: 0x00031C5B
		public bool IsClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !this.IsValueType;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and visible only within its own assembly.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested and visible only within its own assembly; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00033A73 File Offset: 0x00031C73
		public bool IsNestedAssembly
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedAssembly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and declared private.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested and declared private; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x00033A80 File Offset: 0x00031C80
		public bool IsNestedPrivate
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPrivate;
			}
		}

		/// <summary>Gets a value indicating whether a class is nested and declared public.</summary>
		/// <returns>true if the class is nested and declared public; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00033A8D File Offset: 0x00031C8D
		public bool IsNestedPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is not declared public.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is not declared public and is not a nested type; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00033A9A File Offset: 0x00031C9A
		public bool IsNotPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is declared public.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is declared public and is not a nested type; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00033AA7 File Offset: 0x00031CA7
		public bool IsPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.Public;
			}
		}

		/// <summary>Gets a value indicating whether the fields of the current type are laid out at explicitly specified offsets.</summary>
		/// <returns>true if the <see cref="P:System.Type.Attributes" /> property of the current type includes <see cref="F:System.Reflection.TypeAttributes.ExplicitLayout" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x00033AB4 File Offset: 0x00031CB4
		public bool IsExplicitLayout
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.ExplicitLayout;
			}
		}

		/// <summary>Gets a value indicating whether the fields of the current type are laid out sequentially, in the order that they were defined or emitted to the metadata.</summary>
		/// <returns>true if the <see cref="P:System.Type.Attributes" /> property of the current type includes <see cref="F:System.Reflection.TypeAttributes.SequentialLayout" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00033AC3 File Offset: 0x00031CC3
		public bool IsLayoutSequential
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.SequentialLayout;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a COM object.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a COM object; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x00033AD1 File Offset: 0x00031CD1
		public bool IsCOMObject
		{
			get
			{
				return this.IsCOMObjectImpl();
			}
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsCOMObject" /> property and determines whether the <see cref="T:System.Type" /> is a COM object.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a COM object; otherwise, false.</returns>
		// Token: 0x06000BE5 RID: 3045
		protected abstract bool IsCOMObjectImpl();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> can be hosted in a context.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> can be hosted in a context; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x00033AD9 File Offset: 0x00031CD9
		public bool IsContextful
		{
			get
			{
				return this.IsContextfulImpl();
			}
		}

		/// <summary>Implements the <see cref="P:System.Type.IsContextful" /> property and determines whether the <see cref="T:System.Type" /> can be hosted in a context.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> can be hosted in a context; otherwise, false.</returns>
		// Token: 0x06000BE7 RID: 3047 RVA: 0x00033AE1 File Offset: 0x00031CE1
		protected virtual bool IsContextfulImpl()
		{
			return typeof(ContextBoundObject).IsAssignableFrom(this);
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0000C091 File Offset: 0x0000A291
		public virtual bool IsCollectible
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> represents an enumeration.</summary>
		/// <returns>true if the current <see cref="T:System.Type" /> represents an enumeration; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x00033AF3 File Offset: 0x00031CF3
		public virtual bool IsEnum
		{
			get
			{
				return this.IsSubclassOf(typeof(Enum));
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is marshaled by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is marshaled by reference; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00033B05 File Offset: 0x00031D05
		public bool IsMarshalByRef
		{
			get
			{
				return this.IsMarshalByRefImpl();
			}
		}

		/// <summary>Implements the <see cref="P:System.Type.IsMarshalByRef" /> property and determines whether the <see cref="T:System.Type" /> is marshaled by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is marshaled by reference; otherwise, false.</returns>
		// Token: 0x06000BEB RID: 3051 RVA: 0x00033B0D File Offset: 0x00031D0D
		protected virtual bool IsMarshalByRefImpl()
		{
			return typeof(MarshalByRefObject).IsAssignableFrom(this);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is one of the primitive types.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is one of the primitive types; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x00033B1F File Offset: 0x00031D1F
		public bool IsPrimitive
		{
			get
			{
				return this.IsPrimitiveImpl();
			}
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsPrimitive" /> property and determines whether the <see cref="T:System.Type" /> is one of the primitive types.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is one of the primitive types; otherwise, false.</returns>
		// Token: 0x06000BED RID: 3053
		protected abstract bool IsPrimitiveImpl();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a value type.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a value type; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00033B27 File Offset: 0x00031D27
		public bool IsValueType
		{
			get
			{
				return this.IsValueTypeImpl();
			}
		}

		/// <summary>Implements the <see cref="P:System.Type.IsValueType" /> property and determines whether the <see cref="T:System.Type" /> is a value type; that is, not a class or an interface.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a value type; otherwise, false.</returns>
		// Token: 0x06000BEF RID: 3055 RVA: 0x00033B2F File Offset: 0x00031D2F
		protected virtual bool IsValueTypeImpl()
		{
			return this.IsSubclassOf(typeof(ValueType));
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsSignatureType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Searches for a public instance constructor whose parameters match the types in the specified array.</summary>
		/// <returns>An object representing the public instance constructor whose parameters match the types in the parameter type array, if found; otherwise, null.</returns>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the desired constructor.-or- An empty array of <see cref="T:System.Type" /> objects, to get a constructor that takes no parameters. Such an empty array is provided by the static field <see cref="F:System.Type.EmptyTypes" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BF1 RID: 3057 RVA: 0x00033B41 File Offset: 0x00031D41
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(Type[] types)
		{
			return this.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, types, null);
		}

		/// <summary>Searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object representing the constructor that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the constructor to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a constructor that takes no parameters.-or- <see cref="F:System.Type.EmptyTypes" />. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the parameter type array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BF2 RID: 3058 RVA: 0x00033B4E File Offset: 0x00031D4E
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetConstructor(bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		/// <summary>Searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <returns>An object representing the constructor that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and the stack is cleaned up. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the constructor to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a constructor that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BF3 RID: 3059 RVA: 0x00033B5C File Offset: 0x00031D5C
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetConstructorImpl(bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>When overridden in a derived class, searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object representing the constructor that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and the stack is cleaned up. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the constructor to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a constructor that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <exception cref="T:System.NotSupportedException">The current type is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> or <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</exception>
		// Token: 0x06000BF4 RID: 3060
		protected abstract ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		/// <summary>Returns all the public constructors defined for the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing all the public instance constructors defined for the current <see cref="T:System.Type" />, but not including the type initializer (static constructor). If no public instance constructors are defined for the current <see cref="T:System.Type" />, or if the current <see cref="T:System.Type" /> represents a type parameter in the definition of a generic type or generic method, an empty array of type <see cref="T:System.Reflection.ConstructorInfo" /> is returned.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BF5 RID: 3061 RVA: 0x00033BAB File Offset: 0x00031DAB
		[ComVisible(true)]
		public ConstructorInfo[] GetConstructors()
		{
			return this.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the constructors defined for the current <see cref="T:System.Type" />, using the specified BindingFlags.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing all constructors defined for the current <see cref="T:System.Type" /> that match the specified binding constraints, including the type initializer if it is defined. Returns an empty array of type <see cref="T:System.Reflection.ConstructorInfo" /> if no constructors are defined for the current <see cref="T:System.Type" />, if none of the defined constructors match the binding constraints, or if the current <see cref="T:System.Type" /> represents a type parameter in the definition of a generic type or generic method.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BF6 RID: 3062
		[ComVisible(true)]
		public abstract ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

		/// <summary>Returns the <see cref="T:System.Reflection.EventInfo" /> object representing the specified public event.</summary>
		/// <returns>The object representing the specified public event that is declared or inherited by the current <see cref="T:System.Type" />, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of an event that is declared or inherited by the current <see cref="T:System.Type" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BF7 RID: 3063 RVA: 0x00033BB5 File Offset: 0x00031DB5
		public EventInfo GetEvent(string name)
		{
			return this.GetEvent(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Reflection.EventInfo" /> object representing the specified event, using the specified binding constraints.</summary>
		/// <returns>The object representing the specified event that is declared or inherited by the current <see cref="T:System.Type" />, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of an event which is declared or inherited by the current <see cref="T:System.Type" />. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BF8 RID: 3064
		public abstract EventInfo GetEvent(string name, BindingFlags bindingAttr);

		/// <summary>When overridden in a derived class, searches for events that are declared or inherited by the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing all events that are declared or inherited by the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.EventInfo" />, if the current <see cref="T:System.Type" /> does not have events, or if none of the events match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BF9 RID: 3065
		public abstract EventInfo[] GetEvents(BindingFlags bindingAttr);

		/// <summary>Searches for the public field with the specified name.</summary>
		/// <returns>An object representing the public field with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the data field to get. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">This <see cref="T:System.Type" /> object is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> whose <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method has not yet been called. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BFA RID: 3066 RVA: 0x00033BC0 File Offset: 0x00031DC0
		public FieldInfo GetField(string name)
		{
			return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Searches for the specified field, using the specified binding constraints.</summary>
		/// <returns>An object representing the field that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the data field to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BFB RID: 3067
		public abstract FieldInfo GetField(string name, BindingFlags bindingAttr);

		/// <summary>Returns all the public fields of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects representing all the public fields defined for the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.FieldInfo" />, if no public fields are defined for the current <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BFC RID: 3068 RVA: 0x00033BCB File Offset: 0x00031DCB
		public FieldInfo[] GetFields()
		{
			return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the fields defined for the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects representing all fields defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.FieldInfo" />, if no fields are defined for the current <see cref="T:System.Type" />, or if none of the defined fields match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BFD RID: 3069
		public abstract FieldInfo[] GetFields(BindingFlags bindingAttr);

		/// <summary>Searches for the public members with the specified name.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		/// <param name="name">The string containing the name of the public members to get. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BFE RID: 3070 RVA: 0x00033BD5 File Offset: 0x00031DD5
		public MemberInfo[] GetMember(string name)
		{
			return this.GetMember(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Searches for the specified members, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		/// <param name="name">The string containing the name of the members to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return an empty array. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BFF RID: 3071 RVA: 0x00033BE0 File Offset: 0x00031DE0
		public virtual MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
		{
			return this.GetMember(name, MemberTypes.All, bindingAttr);
		}

		/// <summary>Searches for the specified members of the specified member type, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		/// <param name="name">The string containing the name of the members to get. </param>
		/// <param name="type">The value to search for. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return an empty array. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">A derived class must provide an implementation. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C00 RID: 3072 RVA: 0x000339C9 File Offset: 0x00031BC9
		public virtual MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>When overridden in a derived class, searches for the members defined for the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing all members defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if no members are defined for the current <see cref="T:System.Type" />, or if none of the defined members match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero (<see cref="F:System.Reflection.BindingFlags.Default" />), to return an empty array. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C01 RID: 3073
		public abstract MemberInfo[] GetMembers(BindingFlags bindingAttr);

		/// <summary>Searches for the public method with the specified name.</summary>
		/// <returns>An object that represents the public method with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public method to get. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C02 RID: 3074 RVA: 0x00033BEF File Offset: 0x00031DEF
		public MethodInfo GetMethod(string name)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Searches for the specified method, using the specified binding constraints.</summary>
		/// <returns>An object representing the method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the method to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C03 RID: 3075 RVA: 0x00033BFA File Offset: 0x00031DFA
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetMethodImpl(name, bindingAttr, null, CallingConventions.Any, null, null);
		}

		/// <summary>Searches for the specified public method whose parameters match the specified argument types.</summary>
		/// <returns>An object representing the public method whose parameters match the specified argument types, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public method to get. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and specified parameters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C04 RID: 3076 RVA: 0x00033C16 File Offset: 0x00031E16
		public MethodInfo GetMethod(string name, Type[] types)
		{
			return this.GetMethod(name, types, null);
		}

		/// <summary>Searches for the specified public method whose parameters match the specified argument types and modifiers.</summary>
		/// <returns>An object representing the public method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public method to get. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.  </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and specified parameters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C05 RID: 3077 RVA: 0x00033C21 File Offset: 0x00031E21
		public MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, types, modifiers);
		}

		/// <summary>Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <returns>An object representing the method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the method to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C06 RID: 3078 RVA: 0x00033C2F File Offset: 0x00031E2F
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		/// <summary>Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <returns>An object representing the method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the method to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and how the stack is cleaned up. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C07 RID: 3079 RVA: 0x00033C40 File Offset: 0x00031E40
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>When overridden in a derived class, searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <returns>An object representing the method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the method to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and what process cleans up the stack. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a method that takes no parameters.-or- null. If <paramref name="types" /> is null, arguments are not matched. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <exception cref="T:System.NotSupportedException">The current type is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> or <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</exception>
		// Token: 0x06000C08 RID: 3080
		protected abstract MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		/// <summary>Returns all the public methods of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects representing all the public methods defined for the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.MethodInfo" />, if no public methods are defined for the current <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C09 RID: 3081 RVA: 0x00033C9F File Offset: 0x00031E9F
		public MethodInfo[] GetMethods()
		{
			return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the methods defined for the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects representing all methods defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.MethodInfo" />, if no methods are defined for the current <see cref="T:System.Type" />, or if none of the defined methods match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C0A RID: 3082
		public abstract MethodInfo[] GetMethods(BindingFlags bindingAttr);

		/// <summary>When overridden in a derived class, searches for the specified nested type, using the specified binding constraints.</summary>
		/// <returns>An object representing the nested type that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the nested type to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C0B RID: 3083
		public abstract Type GetNestedType(string name, BindingFlags bindingAttr);

		/// <summary>Searches for the public property with the specified name.</summary>
		/// <returns>An object representing the public property with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public property to get. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name. See Remarks.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C0C RID: 3084 RVA: 0x00033CA9 File Offset: 0x00031EA9
		public PropertyInfo GetProperty(string name)
		{
			return this.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Searches for the specified property, using the specified binding constraints.</summary>
		/// <returns>An object representing the property that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the property to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints. See Remarks.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C0D RID: 3085 RVA: 0x00033CB4 File Offset: 0x00031EB4
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetPropertyImpl(name, bindingAttr, null, null, null, null);
		}

		/// <summary>Searches for the public property with the specified name and return type.</summary>
		/// <returns>An object representing the public property with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public property to get. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null, or <paramref name="returnType" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C0E RID: 3086 RVA: 0x00033CD0 File Offset: 0x00031ED0
		public PropertyInfo GetProperty(string name, Type returnType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, null, null);
		}

		/// <summary>Searches for the specified public property whose parameters match the specified argument types.</summary>
		/// <returns>An object representing the public property whose parameters match the specified argument types, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public property to get. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified argument types. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional. </exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C0F RID: 3087 RVA: 0x00033D01 File Offset: 0x00031F01
		public PropertyInfo GetProperty(string name, Type returnType, Type[] types)
		{
			return this.GetProperty(name, returnType, types, null);
		}

		/// <summary>Searches for the specified public property whose parameters match the specified argument types and modifiers.</summary>
		/// <returns>An object representing the public property that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public property to get. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified argument types and modifiers. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C10 RID: 3088 RVA: 0x00033D0D File Offset: 0x00031F0D
		public PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, types, modifiers);
		}

		/// <summary>Searches for the specified property whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <returns>An object representing the property that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the property to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C11 RID: 3089 RVA: 0x00033D1D File Offset: 0x00031F1D
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			return this.GetPropertyImpl(name, bindingAttr, binder, returnType, types, modifiers);
		}

		/// <summary>When overridden in a derived class, searches for the specified property whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <returns>An object representing the property that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the property to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded member, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <exception cref="T:System.NotSupportedException">The current type is a <see cref="T:System.Reflection.Emit.TypeBuilder" />, <see cref="T:System.Reflection.Emit.EnumBuilder" />, or <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</exception>
		// Token: 0x06000C12 RID: 3090
		protected abstract PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		/// <summary>Returns all the public properties of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing all public properties of the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.PropertyInfo" />, if the current <see cref="T:System.Type" /> does not have public properties.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C13 RID: 3091 RVA: 0x00033D4B File Offset: 0x00031F4B
		public PropertyInfo[] GetProperties()
		{
			return this.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the properties of the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing all properties of the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.PropertyInfo" />, if the current <see cref="T:System.Type" /> does not have properties, or if none of the properties match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C14 RID: 3092
		public abstract PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		/// <summary>Searches for the members defined for the current <see cref="T:System.Type" /> whose <see cref="T:System.Reflection.DefaultMemberAttribute" /> is set.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing all default members of the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if the current <see cref="T:System.Type" /> does not have default members.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C15 RID: 3093 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual MemberInfo[] GetDefaultMembers()
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Gets the handle for the current <see cref="T:System.Type" />.</summary>
		/// <returns>The handle for the current <see cref="T:System.Type" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The .NET Compact Framework does not currently support this property.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x000339FF File Offset: 0x00031BFF
		public virtual RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the handle for the <see cref="T:System.Type" /> of a specified object.</summary>
		/// <returns>The handle for the <see cref="T:System.Type" /> of the specified <see cref="T:System.Object" />.</returns>
		/// <param name="o">The object for which to get the type handle. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="o" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C17 RID: 3095 RVA: 0x00033D55 File Offset: 0x00031F55
		public static RuntimeTypeHandle GetTypeHandle(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException(null, "Invalid handle.");
			}
			return o.GetType().TypeHandle;
		}

		/// <summary>Gets the underlying type code of the specified <see cref="T:System.Type" />.</summary>
		/// <returns>The code of the underlying type, or <see cref="F:System.TypeCode.Empty" /> if <paramref name="type" /> is null.</returns>
		/// <param name="type">The type whose underlying type code to get. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C18 RID: 3096 RVA: 0x00033D71 File Offset: 0x00031F71
		public static TypeCode GetTypeCode(Type type)
		{
			if (type == null)
			{
				return TypeCode.Empty;
			}
			return type.GetTypeCodeImpl();
		}

		/// <summary>Returns the underlying type code of the specified <see cref="T:System.Type" />.</summary>
		/// <returns>The code of the underlying type.</returns>
		// Token: 0x06000C19 RID: 3097 RVA: 0x00033D84 File Offset: 0x00031F84
		protected virtual TypeCode GetTypeCodeImpl()
		{
			if (this != this.UnderlyingSystemType && this.UnderlyingSystemType != null)
			{
				return Type.GetTypeCode(this.UnderlyingSystemType);
			}
			return TypeCode.Object;
		}

		/// <summary>Gets the GUID associated with the <see cref="T:System.Type" />.</summary>
		/// <returns>The GUID associated with the <see cref="T:System.Type" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000C1A RID: 3098
		public abstract Guid GUID { get; }

		/// <summary>Gets the type associated with the specified class identifier (CLSID).</summary>
		/// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
		/// <param name="clsid">The CLSID of the type to get. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C1B RID: 3099 RVA: 0x00033DAF File Offset: 0x00031FAF
		public static Type GetTypeFromCLSID(Guid clsid)
		{
			return Type.GetTypeFromCLSID(clsid, null, false);
		}

		/// <summary>Gets the type from which the current <see cref="T:System.Type" /> directly inherits.</summary>
		/// <returns>The <see cref="T:System.Type" /> from which the current <see cref="T:System.Type" /> directly inherits, or null if the current Type represents the <see cref="T:System.Object" /> class or an interface.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000C1C RID: 3100
		public abstract Type BaseType { get; }

		/// <summary>When overridden in a derived class, invokes the specified member, using the specified binding constraints and matching the specified argument list, modifiers and culture.</summary>
		/// <returns>An object representing the return value of the invoked member.</returns>
		/// <param name="name">The string containing the name of the constructor, method, property, or field member to invoke.-or- An empty string ("") to invoke the default member. -or-For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
		/// <param name="invokeAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted. The access can be one of the BindingFlags such as Public, NonPublic, Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified. If the type of lookup is omitted, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static are used. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. Note that explicitly defining a <see cref="T:System.Reflection.Binder" /> object may be required for successfully invoking method overloads with variable arguments.</param>
		/// <param name="target">The object on which to invoke the specified member. </param>
		/// <param name="args">An array containing the arguments to pass to the member to invoke. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="args" /> array. A parameter's associated attributes are stored in the member's signature. The default binder processes this parameter only when calling a COM component. </param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> object representing the globalization locale to use, which may be necessary for locale-specific conversions, such as converting a numeric String to a Double.-or- A null reference (Nothing in Visual Basic) to use the current thread's <see cref="T:System.Globalization.CultureInfo" />. </param>
		/// <param name="namedParameters">An array containing the names of the parameters to which the values in the <paramref name="args" /> array are passed. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="invokeAttr" /> does not contain CreateInstance and <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="args" /> and <paramref name="modifiers" /> do not have the same length.-or- <paramref name="invokeAttr" /> is not a valid <see cref="T:System.Reflection.BindingFlags" /> attribute.-or- <paramref name="invokeAttr" /> does not contain one of the following binding flags: InvokeMethod, CreateInstance, GetField, SetField, GetProperty, or SetProperty.-or- <paramref name="invokeAttr" /> contains CreateInstance combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty.-or- <paramref name="invokeAttr" /> contains both GetField and SetField.-or- <paramref name="invokeAttr" /> contains both GetProperty and SetProperty.-or- <paramref name="invokeAttr" /> contains InvokeMethod combined with SetField or SetProperty.-or- <paramref name="invokeAttr" /> contains SetField and <paramref name="args" /> has more than one element.-or- The named parameter array is larger than the argument array.-or- This method is called on a COM object and one of the following binding flags was not passed in: BindingFlags.InvokeMethod, BindingFlags.GetProperty, BindingFlags.SetProperty, BindingFlags.PutDispProperty, or BindingFlags.PutRefDispProperty.-or- One of the named parameter arrays contains a string that is null. </exception>
		/// <exception cref="T:System.MethodAccessException">The specified member is a class initializer. </exception>
		/// <exception cref="T:System.MissingFieldException">The field or property cannot be found. </exception>
		/// <exception cref="T:System.MissingMethodException">No method can be found that matches the arguments in <paramref name="args" />.-or- No member can be found that has the argument names supplied in <paramref name="namedParameters" />.-or- The current <see cref="T:System.Type" /> object represents a type that contains open type parameters, that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns true. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The specified member cannot be invoked on <paramref name="target" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method matches the binding criteria. </exception>
		/// <exception cref="T:System.InvalidOperationException">The method represented by <paramref name="name" /> has one or more unspecified generic type parameters. That is, the method's <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> property returns true.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C1D RID: 3101
		public abstract object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		/// <summary>Searches for the interface with the specified name.</summary>
		/// <returns>An object representing the interface with the specified name, implemented or inherited by the current <see cref="T:System.Type" />, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the interface to get. For generic interfaces, this is the mangled name.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The current <see cref="T:System.Type" /> represents a type that implements the same generic interface with different type arguments. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C1E RID: 3102 RVA: 0x00033DB9 File Offset: 0x00031FB9
		public Type GetInterface(string name)
		{
			return this.GetInterface(name, false);
		}

		/// <summary>When overridden in a derived class, searches for the specified interface, specifying whether to do a case-insensitive search for the interface name.</summary>
		/// <returns>An object representing the interface with the specified name, implemented or inherited by the current <see cref="T:System.Type" />, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the interface to get. For generic interfaces, this is the mangled name.</param>
		/// <param name="ignoreCase">true to ignore the case of that part of <paramref name="name" /> that specifies the simple interface name (the part that specifies the namespace must be correctly cased).-or- false to perform a case-sensitive search for all parts of <paramref name="name" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The current <see cref="T:System.Type" /> represents a type that implements the same generic interface with different type arguments. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C1F RID: 3103
		public abstract Type GetInterface(string name, bool ignoreCase);

		/// <summary>When overridden in a derived class, gets all the interfaces implemented or inherited by the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the interfaces implemented or inherited by the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Type" />, if no interfaces are implemented or inherited by the current <see cref="T:System.Type" />.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A static initializer is invoked and throws an exception. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C20 RID: 3104
		public abstract Type[] GetInterfaces();

		/// <summary>Returns an interface mapping for the specified interface type.</summary>
		/// <returns>An object that represents the interface mapping for <paramref name="interfaceType" />.</returns>
		/// <param name="interfaceType">The interface type to retrieve a mapping for. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="interfaceType" /> is not implemented by the current type. -or-The <paramref name="interfaceType" /> parameter does not refer to an interface. -or-<paramref name="interfaceType" /> is a generic interface, and the current type is an array type. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="interfaceType" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Type" /> represents a generic type parameter; that is, <see cref="P:System.Type.IsGenericParameter" /> is true. </exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C21 RID: 3105 RVA: 0x000339C9 File Offset: 0x00031BC9
		[ComVisible(true)]
		public virtual InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Determines whether the specified object is an instance of the current <see cref="T:System.Type" />.</summary>
		/// <returns>true if the current Type is in the inheritance hierarchy of the object represented by <paramref name="o" />, or if the current Type is an interface that <paramref name="o" /> supports. false if neither of these conditions is the case, or if <paramref name="o" /> is null, or if the current Type is an open generic type (that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns true).</returns>
		/// <param name="o">The object to compare with the current type. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C22 RID: 3106 RVA: 0x00033DC3 File Offset: 0x00031FC3
		public virtual bool IsInstanceOfType(object o)
		{
			return o != null && this.IsAssignableFrom(o.GetType());
		}

		/// <summary>Determines whether two COM types have the same identity and are eligible for type equivalence.</summary>
		/// <returns>true if the COM types are equivalent; otherwise, false. This method also returns false if one type is in an assembly that is loaded for execution, and the other is in an assembly that is loaded into the reflection-only context.</returns>
		/// <param name="other">The COM type that is tested for equivalence with the current type.</param>
		// Token: 0x06000C23 RID: 3107 RVA: 0x00033DD6 File Offset: 0x00031FD6
		public virtual bool IsEquivalentTo(Type other)
		{
			return this == other;
		}

		/// <summary>Returns the underlying type of the current enumeration type. </summary>
		/// <returns>The underlying type of the current enumeration.</returns>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.-or-The enumeration type is not valid, because it contains more than one instance field.</exception>
		// Token: 0x06000C24 RID: 3108 RVA: 0x00033DE0 File Offset: 0x00031FE0
		public virtual Type GetEnumUnderlyingType()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			FieldInfo[] fields = this.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (fields == null || fields.Length != 1)
			{
				throw new ArgumentException("The Enum type should contain one and only one instance field.", "enumType");
			}
			return fields[0].FieldType;
		}

		/// <summary>Returns an array of the values of the constants in the current enumeration type.</summary>
		/// <returns>An array that contains the values. The elements of the array are sorted by the binary values (that is, the unsigned values) of the enumeration constants.</returns>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.</exception>
		// Token: 0x06000C25 RID: 3109 RVA: 0x00033E2F File Offset: 0x0003202F
		public virtual Array GetEnumValues()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object representing a one-dimensional array of the current type, with a lower bound of zero.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a one-dimensional array of the current type, with a lower bound of zero.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.-or-The current type is a ByRef type. That is, <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C26 RID: 3110 RVA: 0x000339FF File Offset: 0x00031BFF
		public virtual Type MakeArrayType()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object representing an array of the current type, with the specified number of dimensions.</summary>
		/// <returns>An object representing an array of the current type, with the specified number of dimensions.</returns>
		/// <param name="rank">The number of dimensions for the array. This number must be less than or equal to 32.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is invalid. For example, 0 or negative.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.-or-The current type is a ByRef type. That is, <see cref="P:System.Type.IsByRef" /> returns true. -or-<paramref name="rank" /> is greater than 32.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C27 RID: 3111 RVA: 0x000339FF File Offset: 0x00031BFF
		public virtual Type MakeArrayType(int rank)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the current type when passed as a ref parameter (ByRef parameter in Visual Basic).</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the current type when passed as a ref parameter (ByRef parameter in Visual Basic).</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.-or-The current type is a ByRef type. That is, <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C28 RID: 3112 RVA: 0x000339FF File Offset: 0x00031BFF
		public virtual Type MakeByRefType()
		{
			throw new NotSupportedException();
		}

		/// <summary>Substitutes the elements of an array of types for the type parameters of the current generic type definition and returns a <see cref="T:System.Type" /> object representing the resulting constructed type.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the constructed type formed by substituting the elements of <paramref name="typeArguments" /> for the type parameters of the current generic type.</returns>
		/// <param name="typeArguments">An array of types to be substituted for the type parameters of the current generic type.</param>
		/// <exception cref="T:System.InvalidOperationException">The current type does not represent a generic type definition. That is, <see cref="P:System.Type.IsGenericTypeDefinition" /> returns false. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeArguments" /> is null.-or- Any element of <paramref name="typeArguments" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in <paramref name="typeArguments" /> is not the same as the number of type parameters in the current generic type definition.-or- Any element of <paramref name="typeArguments" /> does not satisfy the constraints specified for the corresponding type parameter of the current generic type. -or- <paramref name="typeArguments" /> contains an element that is a pointer type (<see cref="P:System.Type.IsPointer" /> returns true), a by-ref type (<see cref="P:System.Type.IsByRef" /> returns true), or <see cref="T:System.Void" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		// Token: 0x06000C29 RID: 3113 RVA: 0x000339C9 File Offset: 0x00031BC9
		public virtual Type MakeGenericType(params Type[] typeArguments)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a pointer to the current type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents a pointer to the current type.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.-or-The current type is a ByRef type. That is, <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C2A RID: 3114 RVA: 0x000339FF File Offset: 0x00031BFF
		public virtual Type MakePointerType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00033E4E File Offset: 0x0003204E
		public static Type MakeGenericSignatureType(Type genericTypeDefinition, params Type[] typeArguments)
		{
			return new SignatureConstructedGenericType(genericTypeDefinition, typeArguments);
		}

		/// <summary>Returns a String representing the name of the current Type.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of the current <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C2C RID: 3116 RVA: 0x00033E57 File Offset: 0x00032057
		public override string ToString()
		{
			return "Type: " + this.Name;
		}

		/// <summary>Determines if the underlying system type of the current <see cref="T:System.Type" /> is the same as the underlying system type of the specified <see cref="T:System.Object" />.</summary>
		/// <returns>true if the underlying system type of <paramref name="o" /> is the same as the underlying system type of the current <see cref="T:System.Type" />; otherwise, false. This method also returns false if the object specified by the <paramref name="o" /> parameter is not a Type.</returns>
		/// <param name="o">The object whose underlying system type is to be compared with the underlying system type of the current <see cref="T:System.Type" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C2D RID: 3117 RVA: 0x00033E69 File Offset: 0x00032069
		public override bool Equals(object o)
		{
			return o != null && this.Equals(o as Type);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code for this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C2E RID: 3118 RVA: 0x00033E7C File Offset: 0x0003207C
		public override int GetHashCode()
		{
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType != this)
			{
				return underlyingSystemType.GetHashCode();
			}
			return base.GetHashCode();
		}

		/// <summary>Determines if the underlying system type of the current <see cref="T:System.Type" /> is the same as the underlying system type of the specified <see cref="T:System.Type" />.</summary>
		/// <returns>true if the underlying system type of <paramref name="o" /> is the same as the underlying system type of the current <see cref="T:System.Type" />; otherwise, false.</returns>
		/// <param name="o">The object whose underlying system type is to be compared with the underlying system type of the current <see cref="T:System.Type" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C2F RID: 3119 RVA: 0x00033EA1 File Offset: 0x000320A1
		public virtual bool Equals(Type o)
		{
			return !(o == null) && this.UnderlyingSystemType == o.UnderlyingSystemType;
		}

		/// <summary>Gets a reference to the default binder, which implements internal rules for selecting the appropriate members to be called by <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.</summary>
		/// <returns>A reference to the default binder used by the system.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00033EBC File Offset: 0x000320BC
		public static Binder DefaultBinder
		{
			get
			{
				if (Type.s_defaultBinder == null)
				{
					DefaultBinder defaultBinder = new DefaultBinder();
					Interlocked.CompareExchange<Binder>(ref Type.s_defaultBinder, defaultBinder, null);
				}
				return Type.s_defaultBinder;
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00033EEC File Offset: 0x000320EC
		internal virtual Type InternalResolve()
		{
			return this.UnderlyingSystemType;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00033EF4 File Offset: 0x000320F4
		internal virtual Type RuntimeResolve()
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0000C091 File Offset: 0x0000A291
		internal virtual bool IsUserType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00033EFB File Offset: 0x000320FB
		internal virtual MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00033EFB File Offset: 0x000320FB
		internal virtual ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00033EFB File Offset: 0x000320FB
		internal virtual FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		/// <summary>Gets the type referenced by the specified type handle.</summary>
		/// <returns>The type referenced by the specified <see cref="T:System.RuntimeTypeHandle" />, or null if the <see cref="P:System.RuntimeTypeHandle.Value" /> property of <paramref name="handle" /> is null.</returns>
		/// <param name="handle">The object that refers to the type. </param>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C37 RID: 3127 RVA: 0x00033F07 File Offset: 0x00032107
		public static Type GetTypeFromHandle(RuntimeTypeHandle handle)
		{
			if (handle.Value == IntPtr.Zero)
			{
				return null;
			}
			return Type.internal_from_handle(handle.Value);
		}

		// Token: 0x06000C38 RID: 3128
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type internal_from_handle(IntPtr handle);

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00033991 File Offset: 0x00031B91
		internal virtual bool IsSzArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00033F2A File Offset: 0x0003212A
		internal string FormatTypeName()
		{
			return this.FormatTypeName(false);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00033EF4 File Offset: 0x000320F4
		internal virtual string FormatTypeName(bool serialization)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is an interface; that is, not a class or a value type.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an interface; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x00033F34 File Offset: 0x00032134
		public bool IsInterface
		{
			get
			{
				RuntimeType runtimeType = this as RuntimeType;
				if (runtimeType != null)
				{
					return RuntimeTypeHandle.IsInterface(runtimeType);
				}
				return (this.GetAttributeFlagsImpl() & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, specifying whether to perform a case-sensitive search and whether to throw an exception if the type is not found.</summary>
		/// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError" /> parameter specifies whether null is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError" />. See the Exceptions section. </returns>
		/// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="P:System.Type.AssemblyQualifiedName" />. If the type is in the currently executing assembly or in Mscorlib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null.Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <param name="ignoreCase">true to perform a case-insensitive search for <paramref name="typeName" />, false to perform a case-sensitive search for <paramref name="typeName" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true and the type is not found. -or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> is an empty string.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> represents an array type with an invalid size. -or-<paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid syntax. For example, "MyType[,*,]".-or- <paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments.-or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwOnError" /> is true and the assembly or one of its dependencies was not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C3D RID: 3133 RVA: 0x00033F68 File Offset: 0x00032168
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeType.GetType(typeName, throwOnError, ignoreCase, false, ref stackCrawlMark);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, performing a case-sensitive search and specifying whether to throw an exception if the type is not found.</summary>
		/// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError" /> parameter specifies whether null is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError" />. See the Exceptions section. </returns>
		/// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="P:System.Type.AssemblyQualifiedName" />. If the type is in the currently executing assembly or in Mscorlib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null. Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true and the type is not found. -or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> is an empty string.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> represents an array type with an invalid size. -or-<paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid syntax. For example, "MyType[,*,]".-or- <paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments.-or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwOnError" /> is true and the assembly or one of its dependencies was not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.The assembly or one of its dependencies was found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C3E RID: 3134 RVA: 0x00033F84 File Offset: 0x00032184
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, bool throwOnError)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeType.GetType(typeName, throwOnError, false, false, ref stackCrawlMark);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, performing a case-sensitive search.</summary>
		/// <returns>The type with the specified name, if found; otherwise, null.</returns>
		/// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="P:System.Type.AssemblyQualifiedName" />. If the type is in the currently executing assembly or in Mscorlib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments.-or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />. </exception>
		/// <exception cref="T:System.IO.FileLoadException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.The assembly or one of its dependencies was found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C3F RID: 3135 RVA: 0x00033FA0 File Offset: 0x000321A0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeType.GetType(typeName, false, false, false, ref stackCrawlMark);
		}

		/// <summary>Gets the type with the specified name, specifying whether to throw an exception if the type is not found, and optionally providing custom methods to resolve the assembly and the type.</summary>
		/// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError" /> parameter specifies whether null is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError" />. See the Exceptions section. </returns>
		/// <param name="typeName">The name of the type to get. If the <paramref name="typeResolver" /> parameter is provided, the type name can be any string that <paramref name="typeResolver" /> is capable of resolving. If the <paramref name="assemblyResolver" /> parameter is provided or if standard type resolution is used, <paramref name="typeName" /> must be an assembly-qualified name (see <see cref="P:System.Type.AssemblyQualifiedName" />), unless the type is in the currently executing assembly or in Mscorlib.dll, in which case it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="assemblyResolver">A method that locates and returns the assembly that is specified in <paramref name="typeName" />. The assembly name is passed to <paramref name="assemblyResolver" /> as an <see cref="T:System.Reflection.AssemblyName" /> object. If <paramref name="typeName" /> does not contain the name of an assembly, <paramref name="assemblyResolver" /> is not called. If <paramref name="assemblyResolver" /> is not supplied, standard assembly resolution is performed. Caution   Do not pass methods from unknown or untrusted callers. Doing so could result in elevation of privilege for malicious code. Use only methods that you provide or that you are familiar with. </param>
		/// <param name="typeResolver">A method that locates and returns the type that is specified by <paramref name="typeName" /> from the assembly that is returned by <paramref name="assemblyResolver" /> or by standard assembly resolution. If no assembly is provided, the method can provide one. The method also takes a parameter that specifies whether to perform a case-insensitive search; false is passed to that parameter. Caution   Do not pass methods from unknown or untrusted callers. </param>
		/// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null. Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true and the type is not found. -or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> is an empty string.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> represents an array type with an invalid size. -or-<paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />. </exception>
		/// <exception cref="T:System.ArgumentException">An error occurs when <paramref name="typeName" /> is parsed into a type name and an assembly name (for example, when the simple type name includes an unescaped special character).-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid syntax (for example, "MyType[,*,]").-or- <paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments.-or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwOnError" /> is true and the assembly or one of its dependencies was not found. -or-<paramref name="typeName" /> contains an invalid assembly name.-or-<paramref name="typeName" /> is a valid assembly name without a type name.</exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		// Token: 0x06000C40 RID: 3136 RVA: 0x00033FBC File Offset: 0x000321BC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, throwOnError, false, ref stackCrawlMark);
		}

		/// <summary>Indicates whether two <see cref="T:System.Type" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06000C41 RID: 3137 RVA: 0x00033FD7 File Offset: 0x000321D7
		public static bool operator ==(Type left, Type right)
		{
			return left == right;
		}

		/// <summary>Indicates whether two <see cref="T:System.Type" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06000C42 RID: 3138 RVA: 0x00033FDD File Offset: 0x000321DD
		public static bool operator !=(Type left, Type right)
		{
			return left != right;
		}

		/// <summary>Gets the type associated with the specified class identifier (CLSID) from the specified server, specifying whether to throw an exception if an error occurs while loading the type.</summary>
		/// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
		/// <param name="clsid">The CLSID of the type to get. </param>
		/// <param name="server">The server from which to load the type. If the server name is null, this method automatically reverts to the local machine. </param>
		/// <param name="throwOnError">true to throw any exception that occurs.-or- false to ignore any exception that occurs. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C43 RID: 3139 RVA: 0x00033FE6 File Offset: 0x000321E6
		public static Type GetTypeFromCLSID(Guid clsid, string server, bool throwOnError)
		{
			return RuntimeType.GetTypeFromCLSIDImpl(clsid, server, throwOnError);
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00033FF0 File Offset: 0x000321F0
		internal string FullNameOrDefault
		{
			get
			{
				if (this.InternalNameIfAvailable == null)
				{
					return "UnknownType";
				}
				string text;
				try
				{
					text = this.FullName;
				}
				catch (MissingMetadataException)
				{
					text = "UnknownType";
				}
				return text;
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00034030 File Offset: 0x00032230
		internal bool IsRuntimeImplemented()
		{
			return this.UnderlyingSystemType is RuntimeType;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00034040 File Offset: 0x00032240
		internal virtual string InternalGetNameIfAvailable(ref Type rootCauseForFailure)
		{
			return this.Name;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00034048 File Offset: 0x00032248
		internal string InternalNameIfAvailable
		{
			get
			{
				Type type = null;
				return this.InternalGetNameIfAvailable(ref type);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0003405F File Offset: 0x0003225F
		internal string NameOrDefault
		{
			get
			{
				return this.InternalNameIfAvailable ?? "UnknownType";
			}
		}

		// Token: 0x040004A2 RID: 1186
		private static volatile Binder s_defaultBinder;

		/// <summary>Separates names in the namespace of the <see cref="T:System.Type" />. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040004A3 RID: 1187
		public static readonly char Delimiter = '.';

		/// <summary>Represents an empty array of type <see cref="T:System.Type" />. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040004A4 RID: 1188
		public static readonly Type[] EmptyTypes = Array.Empty<Type>();

		/// <summary>Represents a missing value in the <see cref="T:System.Type" /> information. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040004A5 RID: 1189
		public static readonly object Missing = global::System.Reflection.Missing.Value;

		/// <summary>Represents the member filter used on attributes. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040004A6 RID: 1190
		public static readonly MemberFilter FilterAttribute = new MemberFilter(Type.FilterAttributeImpl);

		/// <summary>Represents the case-sensitive member filter used on names. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040004A7 RID: 1191
		public static readonly MemberFilter FilterName = new MemberFilter(Type.FilterNameImpl);

		/// <summary>Represents the case-insensitive member filter used on names. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040004A8 RID: 1192
		public static readonly MemberFilter FilterNameIgnoreCase = new MemberFilter(Type.FilterNameIgnoreCaseImpl);

		// Token: 0x040004A9 RID: 1193
		internal RuntimeTypeHandle _impl;
	}
}
