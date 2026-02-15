using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>Performs reflection on a module.</summary>
	// Token: 0x0200060F RID: 1551
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class Module : ICustomAttributeProvider, ISerializable, _Module
	{
		/// <summary>Gets the appropriate <see cref="T:System.Reflection.Assembly" /> for this instance of <see cref="T:System.Reflection.Module" />.</summary>
		/// <returns>An Assembly object.</returns>
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual Assembly Assembly
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a string representing the fully qualified name and path to this module.</summary>
		/// <returns>The fully qualified module name.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06002D30 RID: 11568 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual string FullyQualifiedName
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a String representing the name of the module with the path removed.</summary>
		/// <returns>The module name with no path.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual string Name
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a universally unique identifier (UUID) that can be used to distinguish between two versions of a module.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that can be used to distinguish between two versions of a module.</returns>
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002D32 RID: 11570 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual Guid ModuleVersionId
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a string representing the name of the module.</summary>
		/// <returns>The module name.</returns>
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06002D33 RID: 11571 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual string ScopeName
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a value indicating whether the object is a resource.</summary>
		/// <returns>true if the object is a resource; otherwise, false.</returns>
		// Token: 0x06002D34 RID: 11572 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual bool IsResource()
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns a value that indicates whether the specified attribute type has been applied to this module.</summary>
		/// <returns>true if one or more instances of <paramref name="attributeType" /> have been applied to this module; otherwise, false.</returns>
		/// <param name="attributeType">The type of custom attribute to test for. </param>
		/// <param name="inherit">This argument is ignored for objects of this type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the runtime. For example, <paramref name="attributeType" /> is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> object.</exception>
		// Token: 0x06002D35 RID: 11573 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns all custom attributes.</summary>
		/// <returns>An array of type Object containing all custom attributes.</returns>
		/// <param name="inherit">This argument is ignored for objects of this type. </param>
		// Token: 0x06002D36 RID: 11574 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Gets custom attributes of the specified type.</summary>
		/// <returns>An array of type Object containing all custom attributes of the specified type.</returns>
		/// <param name="attributeType">The type of attribute to get. </param>
		/// <param name="inherit">This argument is ignored for objects of this type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the runtime. For example, <paramref name="attributeType" /> is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> object.</exception>
		// Token: 0x06002D37 RID: 11575 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns the method implementation in accordance with the specified criteria.</summary>
		/// <returns>A MethodInfo object containing implementation information as specified, or null if the method does not exist.</returns>
		/// <param name="name">The method name. </param>
		/// <param name="bindingAttr">One of the BindingFlags bit flags used to control the search. </param>
		/// <param name="binder">An object that implements Binder, containing properties related to this method. </param>
		/// <param name="callConvention">The calling convention for the method. </param>
		/// <param name="types">The parameter types to search for. </param>
		/// <param name="modifiers">An array of parameter modifiers used to make binding work with parameter signatures in which the types have been modified. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">
		///   <paramref name="types" /> is null. </exception>
		// Token: 0x06002D38 RID: 11576 RVA: 0x0003398A File Offset: 0x00031B8A
		protected virtual MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns the global methods defined on the module that match the specified binding flags.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.MethodInfo" /> representing the global methods defined on the module that match the specified binding flags; if no global methods match the binding flags, an empty array is returned.</returns>
		/// <param name="bindingFlags">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values that limit the search.</param>
		// Token: 0x06002D39 RID: 11577 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns a field having the specified name and binding attributes.</summary>
		/// <returns>A FieldInfo object having the specified name and binding attributes, or null if the field does not exist.</returns>
		/// <param name="name">The field name. </param>
		/// <param name="bindingAttr">One of the BindingFlags bit flags used to control the search. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06002D3A RID: 11578 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns the global fields defined on the module that match the specified binding flags.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.FieldInfo" /> representing the global fields defined on the module that match the specified binding flags; if no global fields match the binding flags, an empty array is returned.</returns>
		/// <param name="bindingFlags">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values that limit the search.</param>
		// Token: 0x06002D3B RID: 11579 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns all the types defined within this module.</summary>
		/// <returns>An array of type Type containing types defined within the module that is reflected by this instance.</returns>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">One or more classes in a module could not be loaded. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002D3C RID: 11580 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual Type[] GetTypes()
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns the specified type, performing a case-sensitive search.</summary>
		/// <returns>A Type object representing the given type, if the type is in this module; otherwise, null.</returns>
		/// <param name="className">The name of the type to locate. The name must be fully qualified with the namespace. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The class initializers are invoked and an exception is thrown. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="className" /> is a zero-length string. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="className" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="className" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="className" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="className" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="className" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x06002D3D RID: 11581 RVA: 0x000B223A File Offset: 0x000B043A
		public virtual Type GetType(string className)
		{
			return this.GetType(className, false, false);
		}

		/// <summary>Returns the specified type, searching the module with the specified case sensitivity.</summary>
		/// <returns>A Type object representing the given type, if the type is in this module; otherwise, null.</returns>
		/// <param name="className">The name of the type to locate. The name must be fully qualified with the namespace. </param>
		/// <param name="ignoreCase">true for case-insensitive search; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The class initializers are invoked and an exception is thrown. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="className" /> is a zero-length string. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="className" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="className" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="className" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="className" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="className" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x06002D3E RID: 11582 RVA: 0x000B2245 File Offset: 0x000B0445
		public virtual Type GetType(string className, bool ignoreCase)
		{
			return this.GetType(className, false, ignoreCase);
		}

		/// <summary>Returns the specified type, specifying whether to make a case-sensitive search of the module and whether to throw an exception if the type cannot be found.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the specified type, if the type is declared in this module; otherwise, null.</returns>
		/// <param name="className">The name of the type to locate. The name must be fully qualified with the namespace. </param>
		/// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null. </param>
		/// <param name="ignoreCase">true for case-insensitive search; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The class initializers are invoked and an exception is thrown. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="className" /> is a zero-length string. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true, and the type cannot be found. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="className" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="className" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="className" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="className" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="className" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x06002D3F RID: 11583 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Gets a token that identifies the module in metadata.</summary>
		/// <returns>An integer token that identifies the current module in metadata.</returns>
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual int MetadataToken
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Returns the field identified by the specified metadata token, in the context defined by the specified generic type parameters.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a field in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a field in the scope of the current module.-or-<paramref name="metadataToken" /> identifies a field whose parent TypeSpec has a signature containing element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06002D41 RID: 11585 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns the type or member identified by the specified metadata token, in the context defined by the specified generic type parameters.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberInfo" /> object representing the type or member that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a type or member in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a type or member in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec or TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.-or-<paramref name="metadataToken" /> identifies a property or event.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06002D42 RID: 11586 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns the method or constructor identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> object representing the method or constructor that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a method or constructor in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06002D43 RID: 11587 RVA: 0x000B2250 File Offset: 0x000B0450
		public MethodBase ResolveMethod(int metadataToken)
		{
			return this.ResolveMethod(metadataToken, null, null);
		}

		/// <summary>Returns the method or constructor identified by the specified metadata token, in the context defined by the specified generic type parameters. </summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> object representing the method that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a method or constructor in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06002D44 RID: 11588 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns the signature blob identified by a metadata token.</summary>
		/// <returns>An array of bytes representing the signature blob.</returns>
		/// <param name="metadataToken">A metadata token that identifies a signature in the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a valid MemberRef, MethodDef, TypeSpec, signature, or FieldDef token in the scope of the current module.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06002D45 RID: 11589 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual byte[] ResolveSignature(int metadataToken)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns the string identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a string value from the metadata string heap.</returns>
		/// <param name="metadataToken">A metadata token that identifies a string in the string heap of the module. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a string in the scope of the current module. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06002D46 RID: 11590 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual string ResolveString(int metadataToken)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns the type identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the type that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a type in the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a type in the scope of the current module.-or-<paramref name="metadataToken" /> is a TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06002D47 RID: 11591 RVA: 0x000B225B File Offset: 0x000B045B
		public Type ResolveType(int metadataToken)
		{
			return this.ResolveType(metadataToken, null, null);
		}

		/// <summary>Returns the type identified by the specified metadata token, in the context defined by the specified generic type parameters.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the type that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a type in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a type in the scope of the current module.-or-<paramref name="metadataToken" /> is a TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06002D48 RID: 11592 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Provides an <see cref="T:System.Runtime.Serialization.ISerializable" /> implementation for serialized objects.</summary>
		/// <param name="info">The information and data needed to serialize or deserialize an object. </param>
		/// <param name="context">The context for the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06002D49 RID: 11593 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Determines whether this module and the specified object are equal.</summary>
		/// <returns>true if <paramref name="o" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="o">The object to compare with this instance. </param>
		// Token: 0x06002D4A RID: 11594 RVA: 0x000726BE File Offset: 0x000708BE
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002D4B RID: 11595 RVA: 0x0006EF14 File Offset: 0x0006D114
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Module" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare. </param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002D4C RID: 11596 RVA: 0x0004AE04 File Offset: 0x00049004
		public static bool operator ==(Module left, Module right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Module" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002D4D RID: 11597 RVA: 0x000B2266 File Offset: 0x000B0466
		public static bool operator !=(Module left, Module right)
		{
			return !(left == right);
		}

		/// <summary>Returns the name of the module.</summary>
		/// <returns>A String representing the name of this module.</returns>
		// Token: 0x06002D4E RID: 11598 RVA: 0x000B2272 File Offset: 0x000B0472
		public override string ToString()
		{
			return this.ScopeName;
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000B227C File Offset: 0x000B047C
		private static bool FilterTypeNameImpl(Type cls, object filterCriteria)
		{
			if (filterCriteria == null || !(filterCriteria is string))
			{
				throw new InvalidFilterCriteriaException("A String must be provided for the filter criteria.");
			}
			string text = (string)filterCriteria;
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				text = text.Substring(0, text.Length - 1);
				return cls.Name.StartsWith(text, StringComparison.Ordinal);
			}
			return cls.Name.Equals(text);
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000B22EC File Offset: 0x000B04EC
		private static bool FilterTypeNameIgnoreCaseImpl(Type cls, object filterCriteria)
		{
			if (filterCriteria == null || !(filterCriteria is string))
			{
				throw new InvalidFilterCriteriaException("A String must be provided for the filter criteria.");
			}
			string text = (string)filterCriteria;
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				text = text.Substring(0, text.Length - 1);
				string name = cls.Name;
				return name.Length >= text.Length && string.Compare(name, 0, text, 0, text.Length, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return string.Compare(text, cls.Name, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x00033EF4 File Offset: 0x000320F4
		internal virtual Guid GetModuleVersionId()
		{
			throw new NotImplementedException();
		}

		/// <summary>A TypeFilter object that filters the list of types defined in this module based upon the name. This field is case-sensitive and read-only.</summary>
		// Token: 0x04001754 RID: 5972
		public static readonly TypeFilter FilterTypeName = new TypeFilter(Module.FilterTypeNameImpl);

		/// <summary>A TypeFilter object that filters the list of types defined in this module based upon the name. This field is case-insensitive and read-only.</summary>
		// Token: 0x04001755 RID: 5973
		public static readonly TypeFilter FilterTypeNameIgnoreCase = new TypeFilter(Module.FilterTypeNameIgnoreCaseImpl);

		// Token: 0x04001756 RID: 5974
		private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
	}
}
