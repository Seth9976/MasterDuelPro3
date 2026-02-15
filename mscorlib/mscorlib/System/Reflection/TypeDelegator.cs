using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Wraps a <see cref="T:System.Type" /> object and delegates methods to that Type.</summary>
	// Token: 0x02000627 RID: 1575
	public class TypeDelegator : TypeInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TypeDelegator" /> class specifying the encapsulating instance.</summary>
		/// <param name="delegatingType">The instance of the class <see cref="T:System.Type" /> that encapsulates the call to the method of an object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="delegatingType" /> is null. </exception>
		// Token: 0x06002E30 RID: 11824 RVA: 0x000B2E5E File Offset: 0x000B105E
		public TypeDelegator(Type delegatingType)
		{
			if (delegatingType == null)
			{
				throw new ArgumentNullException("delegatingType");
			}
			this.typeImpl = delegatingType;
		}

		/// <summary>Gets the GUID (globally unique identifier) of the implemented type.</summary>
		/// <returns>A GUID.</returns>
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06002E31 RID: 11825 RVA: 0x000B2E81 File Offset: 0x000B1081
		public override Guid GUID
		{
			get
			{
				return this.typeImpl.GUID;
			}
		}

		/// <summary>Gets a value that identifies this entity in metadata.</summary>
		/// <returns>A value which, in combination with the module, uniquely identifies this entity in metadata.</returns>
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002E32 RID: 11826 RVA: 0x000B2E8E File Offset: 0x000B108E
		public override int MetadataToken
		{
			get
			{
				return this.typeImpl.MetadataToken;
			}
		}

		/// <summary>Invokes the specified member. The method that is to be invoked must be accessible and provide the most specific match with the specified argument list, under the constraints of the specified binder and invocation attributes.</summary>
		/// <returns>An Object representing the return value of the invoked member.</returns>
		/// <param name="name">The name of the member to invoke. This may be a constructor, method, property, or field. If an empty string ("") is passed, the default member is invoked. </param>
		/// <param name="invokeAttr">The invocation attribute. This must be one of the following <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty, or SetProperty. A suitable invocation attribute must be specified. If a static member is to be invoked, the Static flag must be set. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects via reflection. If <paramref name="binder" /> is null, the default binder is used. See <see cref="T:System.Reflection.Binder" />. </param>
		/// <param name="target">The object on which to invoke the specified member. </param>
		/// <param name="args">An array of type Object that contains the number, order, and type of the parameters of the member to be invoked. If <paramref name="args" /> contains an uninitialized Object, it is treated as empty, which, with the default binder, can be widened to 0, 0.0 or a string. </param>
		/// <param name="modifiers">An array of type ParameterModifer that is the same length as <paramref name="args" />, with elements that represent the attributes associated with the arguments of the member to be invoked. A parameter has attributes associated with it in the member's signature. For ByRef, use ParameterModifer.ByRef, and for none, use ParameterModifer.None. The default binder does exact matching on these. Attributes such as In and InOut are not used in binding, and can be viewed using ParameterInfo. </param>
		/// <param name="culture">An instance of CultureInfo used to govern the coercion of types. This is necessary, for example, to convert a string that represents 1000 to a Double value, since 1000 is represented differently by different cultures. If <paramref name="culture" /> is null, the CultureInfo for the current thread's CultureInfo is used. </param>
		/// <param name="namedParameters">An array of type String containing parameter names that match up, starting at element zero, with the <paramref name="args" /> array. There must be no holes in the array. If <paramref name="args" />. Length is greater than <paramref name="namedParameters" />. Length, the remaining parameters are filled in order. </param>
		// Token: 0x06002E33 RID: 11827 RVA: 0x000B2E9C File Offset: 0x000B109C
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this.typeImpl.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		/// <summary>Gets the module that contains the implemented type.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> object representing the module of the implemented type.</returns>
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06002E34 RID: 11828 RVA: 0x000B2EC1 File Offset: 0x000B10C1
		public override Module Module
		{
			get
			{
				return this.typeImpl.Module;
			}
		}

		/// <summary>Gets the assembly of the implemented type.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object representing the assembly of the implemented type.</returns>
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x000B2ECE File Offset: 0x000B10CE
		public override Assembly Assembly
		{
			get
			{
				return this.typeImpl.Assembly;
			}
		}

		/// <summary>Gets a handle to the internal metadata representation of an implemented type.</summary>
		/// <returns>A RuntimeTypeHandle object.</returns>
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002E36 RID: 11830 RVA: 0x000B2EDB File Offset: 0x000B10DB
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this.typeImpl.TypeHandle;
			}
		}

		/// <summary>Gets the name of the implemented type, with the path removed.</summary>
		/// <returns>A String containing the type's non-qualified name.</returns>
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002E37 RID: 11831 RVA: 0x000B2EE8 File Offset: 0x000B10E8
		public override string Name
		{
			get
			{
				return this.typeImpl.Name;
			}
		}

		/// <summary>Gets the fully qualified name of the implemented type.</summary>
		/// <returns>A String containing the type's fully qualified name.</returns>
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002E38 RID: 11832 RVA: 0x000B2EF5 File Offset: 0x000B10F5
		public override string FullName
		{
			get
			{
				return this.typeImpl.FullName;
			}
		}

		/// <summary>Gets the namespace of the implemented type.</summary>
		/// <returns>A String containing the type's namespace.</returns>
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002E39 RID: 11833 RVA: 0x000B2F02 File Offset: 0x000B1102
		public override string Namespace
		{
			get
			{
				return this.typeImpl.Namespace;
			}
		}

		/// <summary>Gets the assembly's fully qualified name.</summary>
		/// <returns>A String containing the assembly's fully qualified name.</returns>
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06002E3A RID: 11834 RVA: 0x000B2F0F File Offset: 0x000B110F
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.typeImpl.AssemblyQualifiedName;
			}
		}

		/// <summary>Gets the base type for the current type.</summary>
		/// <returns>The base type for a type.</returns>
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x000B2F1C File Offset: 0x000B111C
		public override Type BaseType
		{
			get
			{
				return this.typeImpl.BaseType;
			}
		}

		/// <summary>Gets the constructor that implemented the TypeDelegator.</summary>
		/// <returns>A ConstructorInfo object for the method that matches the specified criteria, or null if a match cannot be found.</returns>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects using reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="callConvention">The calling conventions. </param>
		/// <param name="types">An array of type Type containing a list of the parameter number, order, and types. Types cannot be null; use an appropriate GetMethod method or an empty array to search for a method without parameters. </param>
		/// <param name="modifiers">An array of type ParameterModifier having the same length as the <paramref name="types" /> array, whose elements represent the attributes associated with the parameters of the method to get. </param>
		// Token: 0x06002E3C RID: 11836 RVA: 0x000B2F29 File Offset: 0x000B1129
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.typeImpl.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing constructors defined for the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <returns>An array of type ConstructorInfo containing the specified constructors defined for this class. If no constructors are defined, an empty array is returned. Depending on the value of a specified parameter, only public constructors or both public and non-public constructors will be returned.</returns>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		// Token: 0x06002E3D RID: 11837 RVA: 0x000B2F3D File Offset: 0x000B113D
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetConstructors(bindingAttr);
		}

		/// <summary>Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <returns>A MethodInfoInfo object for the implementation method that matches the specified criteria, or null if a match cannot be found.</returns>
		/// <param name="name">The method name. </param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects using reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="callConvention">The calling conventions. </param>
		/// <param name="types">An array of type Type containing a list of the parameter number, order, and types. Types cannot be null; use an appropriate GetMethod method or an empty array to search for a method without parameters. </param>
		/// <param name="modifiers">An array of type ParameterModifier having the same length as the <paramref name="types" /> array, whose elements represent the attributes associated with the parameters of the method to get. </param>
		// Token: 0x06002E3E RID: 11838 RVA: 0x000B2F4B File Offset: 0x000B114B
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this.typeImpl.GetMethod(name, bindingAttr);
			}
			return this.typeImpl.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.MethodInfo" /> objects representing specified methods of the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <returns>An array of MethodInfo objects representing the methods defined on this TypeDelegator.</returns>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		// Token: 0x06002E3F RID: 11839 RVA: 0x000B2F73 File Offset: 0x000B1173
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMethods(bindingAttr);
		}

		/// <summary>Returns a <see cref="T:System.Reflection.FieldInfo" /> object representing the field with the specified name.</summary>
		/// <returns>A FieldInfo object representing the field declared or inherited by this TypeDelegator with the specified name. Returns null if no such field is found.</returns>
		/// <param name="name">The name of the field to find. </param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06002E40 RID: 11840 RVA: 0x000B2F81 File Offset: 0x000B1181
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetField(name, bindingAttr);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.FieldInfo" /> objects representing the data fields defined for the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <returns>An array of type FieldInfo containing the fields declared or inherited by the current TypeDelegator. An empty array is returned if there are no matched fields.</returns>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		// Token: 0x06002E41 RID: 11841 RVA: 0x000B2F90 File Offset: 0x000B1190
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetFields(bindingAttr);
		}

		/// <summary>Returns the specified interface implemented by the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <returns>A Type object representing the interface implemented (directly or indirectly) by the current class with the fully qualified name matching the specified name. If no interface that matches name is found, null is returned.</returns>
		/// <param name="name">The fully qualified name of the interface implemented by the current class. </param>
		/// <param name="ignoreCase">true if the case is to be ignored; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06002E42 RID: 11842 RVA: 0x000B2F9E File Offset: 0x000B119E
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this.typeImpl.GetInterface(name, ignoreCase);
		}

		/// <summary>Returns all the interfaces implemented on the current class and its base classes.</summary>
		/// <returns>An array of type Type containing all the interfaces implemented on the current class and its base classes. If none are defined, an empty array is returned.</returns>
		// Token: 0x06002E43 RID: 11843 RVA: 0x000B2FAD File Offset: 0x000B11AD
		public override Type[] GetInterfaces()
		{
			return this.typeImpl.GetInterfaces();
		}

		/// <summary>Returns the specified event.</summary>
		/// <returns>An <see cref="T:System.Reflection.EventInfo" /> object representing the event declared or inherited by this type with the specified name. This method returns null if no such event is found.</returns>
		/// <param name="name">The name of the event to get. </param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06002E44 RID: 11844 RVA: 0x000B2FBA File Offset: 0x000B11BA
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvent(name, bindingAttr);
		}

		/// <summary>When overridden in a derived class, searches for the specified property whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object for the property that matches the specified criteria, or null if a match cannot be found.</returns>
		/// <param name="name">The property to get. </param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects via reflection. If <paramref name="binder" /> is null, the default binder is used. See <see cref="T:System.Reflection.Binder" />. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="types">A list of parameter types. The list represents the number, order, and types of the parameters. Types cannot be null; use an appropriate GetMethod method or an empty array to search for a method without parameters. </param>
		/// <param name="modifiers">An array of the same length as types with elements that represent the attributes associated with the parameters of the method to get. </param>
		// Token: 0x06002E45 RID: 11845 RVA: 0x000B2FC9 File Offset: 0x000B11C9
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (returnType == null && types == null)
			{
				return this.typeImpl.GetProperty(name, bindingAttr);
			}
			return this.typeImpl.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing properties of the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <returns>An array of PropertyInfo objects representing properties defined on this TypeDelegator.</returns>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		// Token: 0x06002E46 RID: 11846 RVA: 0x000B2FFB File Offset: 0x000B11FB
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetProperties(bindingAttr);
		}

		/// <summary>Returns the events specified in <paramref name="bindingAttr" /> that are declared or inherited by the current TypeDelegator.</summary>
		/// <returns>An array of type EventInfo containing the events specified in <paramref name="bindingAttr" />. If there are no events, an empty array is returned.</returns>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		// Token: 0x06002E47 RID: 11847 RVA: 0x000B3009 File Offset: 0x000B1209
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvents(bindingAttr);
		}

		/// <summary>Returns a nested type specified by <paramref name="name" /> and in <paramref name="bindingAttr" /> that are declared or inherited by the type represented by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <returns>A Type object representing the nested type.</returns>
		/// <param name="name">The nested type's name. </param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06002E48 RID: 11848 RVA: 0x000B3017 File Offset: 0x000B1217
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetNestedType(name, bindingAttr);
		}

		/// <summary>Returns members (properties, methods, constructors, fields, events, and nested types) specified by the given <paramref name="name" />, <paramref name="type" />, and <paramref name="bindingAttr" />.</summary>
		/// <returns>An array of type MemberInfo containing all the members of the current class and its base class meeting the specified criteria.</returns>
		/// <param name="name">The name of the member to get. </param>
		/// <param name="type">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		/// <param name="bindingAttr">The type of members to get. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06002E49 RID: 11849 RVA: 0x000B3026 File Offset: 0x000B1226
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMember(name, type, bindingAttr);
		}

		/// <summary>Returns members specified by <paramref name="bindingAttr" />.</summary>
		/// <returns>An array of type MemberInfo containing all the members of the current class and its base classes that meet the <paramref name="bindingAttr" /> filter.</returns>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		// Token: 0x06002E4A RID: 11850 RVA: 0x000B3036 File Offset: 0x000B1236
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMembers(bindingAttr);
		}

		/// <summary>Gets the attributes assigned to the TypeDelegator.</summary>
		/// <returns>A TypeAttributes object representing the implementation attribute flags.</returns>
		// Token: 0x06002E4B RID: 11851 RVA: 0x000B3044 File Offset: 0x000B1244
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.typeImpl.Attributes;
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06002E4C RID: 11852 RVA: 0x000B3051 File Offset: 0x000B1251
		public override bool IsSZArray
		{
			get
			{
				return this.typeImpl.IsSZArray;
			}
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is an array.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an array; otherwise, false.</returns>
		// Token: 0x06002E4D RID: 11853 RVA: 0x000B305E File Offset: 0x000B125E
		protected override bool IsArrayImpl()
		{
			return this.typeImpl.IsArray;
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is one of the primitive types.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is one of the primitive types; otherwise, false.</returns>
		// Token: 0x06002E4E RID: 11854 RVA: 0x000B306B File Offset: 0x000B126B
		protected override bool IsPrimitiveImpl()
		{
			return this.typeImpl.IsPrimitive;
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is passed by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is passed by reference; otherwise, false.</returns>
		// Token: 0x06002E4F RID: 11855 RVA: 0x000B3078 File Offset: 0x000B1278
		protected override bool IsByRefImpl()
		{
			return this.typeImpl.IsByRef;
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06002E50 RID: 11856 RVA: 0x000B3085 File Offset: 0x000B1285
		public override bool IsGenericMethodParameter
		{
			get
			{
				return this.typeImpl.IsGenericMethodParameter;
			}
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is a pointer.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a pointer; otherwise, false.</returns>
		// Token: 0x06002E51 RID: 11857 RVA: 0x000B3092 File Offset: 0x000B1292
		protected override bool IsPointerImpl()
		{
			return this.typeImpl.IsPointer;
		}

		/// <summary>Returns a value that indicates whether the type is a value type; that is, not a class or an interface.</summary>
		/// <returns>true if the type is a value type; otherwise, false.</returns>
		// Token: 0x06002E52 RID: 11858 RVA: 0x000B309F File Offset: 0x000B129F
		protected override bool IsValueTypeImpl()
		{
			return this.typeImpl.IsValueType;
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is a COM object.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a COM object; otherwise, false.</returns>
		// Token: 0x06002E53 RID: 11859 RVA: 0x000B30AC File Offset: 0x000B12AC
		protected override bool IsCOMObjectImpl()
		{
			return this.typeImpl.IsCOMObject;
		}

		/// <summary>Gets a value that indicates whether this object represents a constructed generic type.</summary>
		/// <returns>true if this object represents a constructed generic type; otherwise, false.</returns>
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002E54 RID: 11860 RVA: 0x000B30B9 File Offset: 0x000B12B9
		public override bool IsConstructedGenericType
		{
			get
			{
				return this.typeImpl.IsConstructedGenericType;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x000B30C6 File Offset: 0x000B12C6
		public override bool IsCollectible
		{
			get
			{
				return this.typeImpl.IsCollectible;
			}
		}

		/// <summary>Returns the <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer or ByRef.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer or ByRef, or null if the current <see cref="T:System.Type" /> is not an array, a pointer or a ByRef.</returns>
		// Token: 0x06002E56 RID: 11862 RVA: 0x000B30D3 File Offset: 0x000B12D3
		public override Type GetElementType()
		{
			return this.typeImpl.GetElementType();
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> encompasses or refers to another type; that is, whether the current <see cref="T:System.Type" /> is an array, a pointer or a ByRef.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an array, a pointer or a ByRef; otherwise, false.</returns>
		// Token: 0x06002E57 RID: 11863 RVA: 0x000B30E0 File Offset: 0x000B12E0
		protected override bool HasElementTypeImpl()
		{
			return this.typeImpl.HasElementType;
		}

		/// <summary>Gets the underlying <see cref="T:System.Type" /> that represents the implemented type.</summary>
		/// <returns>The underlying type.</returns>
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000B30ED File Offset: 0x000B12ED
		public override Type UnderlyingSystemType
		{
			get
			{
				return this.typeImpl.UnderlyingSystemType;
			}
		}

		/// <summary>Returns all the custom attributes defined for this type, specifying whether to search the type's inheritance chain.</summary>
		/// <returns>An array of objects containing all the custom attributes defined for this type.</returns>
		/// <param name="inherit">Specifies whether to search this type's inheritance chain to find the attributes. </param>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002E59 RID: 11865 RVA: 0x000B30FA File Offset: 0x000B12FA
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(inherit);
		}

		/// <summary>Returns an array of custom attributes identified by type.</summary>
		/// <returns>An array of objects containing the custom attributes defined in this type that match the <paramref name="attributeType" /> parameter, specifying whether to search the type's inheritance chain, or null if no custom attributes are defined on this type.</returns>
		/// <param name="attributeType">An array of custom attributes identified by type.</param>
		/// <param name="inherit">Specifies whether to search this type's inheritance chain to find the attributes. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002E5A RID: 11866 RVA: 0x000B3108 File Offset: 0x000B1308
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Indicates whether a custom attribute identified by <paramref name="attributeType" /> is defined.</summary>
		/// <returns>true if a custom attribute identified by <paramref name="attributeType" /> is defined; otherwise, false.</returns>
		/// <param name="attributeType">Specifies whether to search this type's inheritance chain to find the attributes. </param>
		/// <param name="inherit">An array of custom attributes identified by type. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">The custom attribute type cannot be loaded. </exception>
		// Token: 0x06002E5B RID: 11867 RVA: 0x000B3117 File Offset: 0x000B1317
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.typeImpl.IsDefined(attributeType, inherit);
		}

		/// <summary>Returns an interface mapping for the specified interface type.</summary>
		/// <returns>An <see cref="T:System.Reflection.InterfaceMapping" /> object representing the interface mapping for <paramref name="interfaceType" />.</returns>
		/// <param name="interfaceType">The <see cref="T:System.Type" /> of the interface to retrieve a mapping of. </param>
		// Token: 0x06002E5C RID: 11868 RVA: 0x000B3126 File Offset: 0x000B1326
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this.typeImpl.GetInterfaceMap(interfaceType);
		}

		/// <summary>A value indicating type information.</summary>
		// Token: 0x040017B3 RID: 6067
		protected Type typeImpl;
	}
}
