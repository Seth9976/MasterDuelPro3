using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Security.Policy;
using System.Threading;
using Unity;

namespace System
{
	/// <summary>Contains methods to create types of objects locally or remotely, or obtain references to existing remote objects. This class cannot be inherited. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000194 RID: 404
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Activator))]
	public sealed class Activator : _Activator
	{
		/// <summary>Creates an instance of the specified type using the constructor that best matches the specified parameters.</summary>
		/// <returns>A reference to the newly created object.</returns>
		/// <param name="type">The type of object to create. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="type" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="type" /> constructor. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="type" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a RuntimeType. -or-<paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns true).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.-or- Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported. -or-The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.-or-The constructor that best matches <paramref name="args" /> has varargs arguments.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">
		///   <paramref name="type" /> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="type" /> is not a valid type. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003D653 File Offset: 0x0003B853
		public static object CreateInstance(Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture)
		{
			return Activator.CreateInstance(type, bindingAttr, binder, args, culture, null);
		}

		/// <summary>Creates an instance of the specified type using the constructor that best matches the specified parameters.</summary>
		/// <returns>A reference to the newly created object.</returns>
		/// <param name="type">The type of object to create. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="type" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="type" /> constructor. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="type" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a RuntimeType. -or-<paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns true).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.-or- Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.-or- <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />. -or-The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.-or-The constructor that best matches <paramref name="args" /> has varargs arguments.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">
		///   <paramref name="type" /> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="type" /> is not a valid type. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06000EB3 RID: 3763 RVA: 0x0003D664 File Offset: 0x0003B864
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static object CreateInstance(Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (RuntimeFeature.IsDynamicCodeSupported && type is TypeBuilder)
			{
				throw new NotSupportedException(Environment.GetResourceString("CreateInstance cannot be used with an object of type TypeBuilder."));
			}
			if ((bindingAttr & (BindingFlags)255) == BindingFlags.Default)
			{
				bindingAttr |= BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
			}
			if (activationAttributes != null && activationAttributes.Length != 0)
			{
				if (!type.IsMarshalByRef)
				{
					throw new NotSupportedException(Environment.GetResourceString("Activation Attributes are not supported for types not deriving from MarshalByRefObject."));
				}
				if (!type.IsContextful && (activationAttributes.Length > 1 || !(activationAttributes[0] is UrlAttribute)))
				{
					throw new NotSupportedException(Environment.GetResourceString("UrlAttribute is the only attribute supported for MarshalByRefObject."));
				}
			}
			RuntimeType runtimeType = type.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "type");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return runtimeType.CreateInstanceImpl(bindingAttr, binder, args, culture, activationAttributes, ref stackCrawlMark);
		}

		/// <summary>Creates an instance of the specified type using the constructor that best matches the specified parameters.</summary>
		/// <returns>A reference to the newly created object.</returns>
		/// <param name="type">The type of object to create. </param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a RuntimeType. -or-<paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns true).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.-or- Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported. -or-The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.-or-The constructor that best matches <paramref name="args" /> has varargs arguments. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception. </exception>
		/// <exception cref="T:System.MethodAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
		/// <exception cref="T:System.MissingMethodException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MissingMemberException" />, instead.No matching public constructor was found. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">
		///   <paramref name="type" /> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="type" /> is not a valid type. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06000EB4 RID: 3764 RVA: 0x0003D734 File Offset: 0x0003B934
		public static object CreateInstance(Type type, params object[] args)
		{
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, args, null, null);
		}

		/// <summary>Creates an instance of the specified type using the constructor that best matches the specified parameters.</summary>
		/// <returns>A reference to the newly created object.</returns>
		/// <param name="type">The type of object to create. </param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a RuntimeType. -or-<paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns true).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.-or- Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.-or- <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />. -or-The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.-or-The constructor that best matches <paramref name="args" /> has varargs arguments.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">
		///   <paramref name="type" /> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="type" /> is not a valid type. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003D745 File Offset: 0x0003B945
		public static object CreateInstance(Type type, object[] args, object[] activationAttributes)
		{
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, args, null, activationAttributes);
		}

		/// <summary>Creates an instance of the specified type using that type's default constructor.</summary>
		/// <returns>A reference to the newly created object.</returns>
		/// <param name="type">The type of object to create. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a RuntimeType. -or-<paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns true).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.-or- Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.-or-The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception. </exception>
		/// <exception cref="T:System.MethodAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
		/// <exception cref="T:System.MissingMethodException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MissingMemberException" />, instead.No matching public constructor was found. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">
		///   <paramref name="type" /> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="type" /> is not a valid type. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06000EB6 RID: 3766 RVA: 0x0003D756 File Offset: 0x0003B956
		public static object CreateInstance(Type type)
		{
			return Activator.CreateInstance(type, false);
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly and default constructor.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <param name="assemblyName">The name of the assembly where the type named <paramref name="typeName" /> is sought. For more information, see the Remarks section. If <paramref name="assemblyName" /> is null, the executing assembly is searched. </param>
		/// <param name="typeName">The fully qualified name of the preferred type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">You cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. -or-The assembly name or code base is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06000EB7 RID: 3767 RVA: 0x0003D760 File Offset: 0x0003B960
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ObjectHandle CreateInstance(string assemblyName, string typeName)
		{
			if (assemblyName == null)
			{
				assemblyName = Assembly.GetCallingAssembly().GetName().Name;
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstance(assemblyName, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, null, null, ref stackCrawlMark);
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly and default constructor.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <param name="assemblyName">The name of the assembly where the type named <paramref name="typeName" /> is sought. If <paramref name="assemblyName" /> is null, the executing assembly is searched. </param>
		/// <param name="typeName">The fully qualified name of the preferred type. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.-or- <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />.-or-<paramref name="activationAttributes" /> is not a <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />array. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. -or-The assembly name or code base is invalid. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred when attempting remote activation in a target specified in <paramref name="activationAttributes" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06000EB8 RID: 3768 RVA: 0x0003D798 File Offset: 0x0003B998
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes)
		{
			if (assemblyName == null)
			{
				assemblyName = Assembly.GetCallingAssembly().GetName().Name;
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstance(assemblyName, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, activationAttributes, null, ref stackCrawlMark);
		}

		/// <summary>Creates an instance of the specified type using that type's default constructor.</summary>
		/// <returns>A reference to the newly created object.</returns>
		/// <param name="type">The type of object to create. </param>
		/// <param name="nonPublic">true if a public or nonpublic default constructor can match; false if only a public default constructor can match. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a RuntimeType. -or-<paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns true).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.-or- Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported. -or-The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">
		///   <paramref name="type" /> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="type" /> is not a valid type. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06000EB9 RID: 3769 RVA: 0x0003D7CF File Offset: 0x0003B9CF
		public static object CreateInstance(Type type, bool nonPublic)
		{
			return Activator.CreateInstance(type, nonPublic, true);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0003D7DC File Offset: 0x0003B9DC
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static object CreateInstance(Type type, bool nonPublic, bool wrapExceptions)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "type");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return runtimeType.CreateInstanceDefaultCtor(!nonPublic, false, true, wrapExceptions, ref stackCrawlMark);
		}

		/// <summary>Creates an instance of the type designated by the specified generic type parameter, using the parameterless constructor .</summary>
		/// <returns>A reference to the newly created object.</returns>
		/// <typeparam name="T">The type to create.</typeparam>
		/// <exception cref="T:System.MissingMethodException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MissingMemberException" />, instead.The type that is specified for <paramref name="T" /> does not have a parameterless constructor. </exception>
		// Token: 0x06000EBB RID: 3771 RVA: 0x0003D830 File Offset: 0x0003BA30
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static T CreateInstance<T>()
		{
			RuntimeType runtimeType = typeof(T) as RuntimeType;
			if (runtimeType.HasElementType)
			{
				throw new MissingMethodException(Environment.GetResourceString("No parameterless constructor defined for this object."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return (T)((object)runtimeType.CreateInstanceDefaultCtor(true, true, true, true, ref stackCrawlMark));
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly file and default constructor.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <param name="assemblyFile">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought. </param>
		/// <param name="typeName">The name of the preferred type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyFile" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does have the required <see cref="T:System.Security.Permissions.FileIOPermission" />. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06000EBC RID: 3772 RVA: 0x0003D876 File Offset: 0x0003BA76
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
		{
			return Activator.CreateInstanceFrom(assemblyFile, typeName, null);
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly file and default constructor.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <param name="assemblyFile">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought. </param>
		/// <param name="typeName">The name of the preferred type. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyFile" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does have the required <see cref="T:System.Security.Permissions.FileIOPermission" />. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06000EBD RID: 3773 RVA: 0x0003D880 File Offset: 0x0003BA80
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes)
		{
			return Activator.CreateInstanceFrom(assemblyFile, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, activationAttributes);
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly and the constructor that best matches the specified parameters.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <param name="assemblyName">The name of the assembly where the type named <paramref name="typeName" /> is sought. If <paramref name="assemblyName" /> is null, the executing assembly is searched. </param>
		/// <param name="typeName">The fully qualified name of the preferred type. </param>
		/// <param name="ignoreCase">true to specify that the search for <paramref name="typeName" /> is not case-sensitive; false to specify that the search is case-sensitive. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="typeName" /> constructor. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <param name="securityInfo">Information used to make security policy decisions and grant code permissions. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.-or- <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />. -or-The constructor that best matches <paramref name="args" /> has varargs arguments.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. -or-The assembly name or code base is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06000EBE RID: 3774 RVA: 0x0003D894 File Offset: 0x0003BA94
		[Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstance which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
		{
			if (assemblyName == null)
			{
				assemblyName = Assembly.GetCallingAssembly().GetName().Name;
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityInfo, ref stackCrawlMark);
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly and the constructor that best matches the specified parameters.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <param name="assemblyName">The name of the assembly where the type named <paramref name="typeName" /> is sought. If <paramref name="assemblyName" /> is null, the executing assembly is searched. </param>
		/// <param name="typeName">The fully qualified name of the preferred type. </param>
		/// <param name="ignoreCase">true to specify that the search for <paramref name="typeName" /> is not case-sensitive; false to specify that the search is case-sensitive. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="typeName" /> constructor. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.-or- <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />. -or-The constructor that best matches <paramref name="args" /> has varargs arguments.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. -or-The assembly name or code base is invalid. </exception>
		// Token: 0x06000EBF RID: 3775 RVA: 0x0003D8CC File Offset: 0x0003BACC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (assemblyName == null)
			{
				assemblyName = Assembly.GetCallingAssembly().GetName().Name;
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null, ref stackCrawlMark);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003D904 File Offset: 0x0003BB04
		internal static ObjectHandle CreateInstance(string assemblyString, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo, ref StackCrawlMark stackMark)
		{
			Type type = null;
			Assembly assembly = null;
			if (assemblyString == null)
			{
				assembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
			}
			else
			{
				RuntimeAssembly runtimeAssembly;
				AssemblyName assemblyName = RuntimeAssembly.CreateAssemblyName(assemblyString, false, out runtimeAssembly);
				if (runtimeAssembly != null)
				{
					assembly = runtimeAssembly;
				}
				else if (assemblyName.ContentType == AssemblyContentType.WindowsRuntime)
				{
					type = Type.GetType(typeName + ", " + assemblyString, true, ignoreCase);
				}
				else
				{
					assembly = RuntimeAssembly.InternalLoadAssemblyName(assemblyName, securityInfo, null, ref stackMark, true, false, false);
				}
			}
			if (type == null)
			{
				if (assembly == null)
				{
					return null;
				}
				type = assembly.GetType(typeName, true, ignoreCase);
			}
			object obj = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			if (obj == null)
			{
				return null;
			}
			return new ObjectHandle(obj);
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly file and the constructor that best matches the specified parameters.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <param name="assemblyFile">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought. </param>
		/// <param name="typeName">The name of the preferred type. </param>
		/// <param name="ignoreCase">true to specify that the search for <paramref name="typeName" /> is not case-sensitive; false to specify that the search is case-sensitive. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="typeName" /> constructor. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <param name="securityInfo">Information used to make security policy decisions and grant code permissions. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyFile" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Security.Permissions.FileIOPermission" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003D9A4 File Offset: 0x0003BBA4
		[Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
		{
			return Activator.CreateInstanceFromInternal(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityInfo);
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly file and the constructor that best matches the specified parameters.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <param name="assemblyFile">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought. </param>
		/// <param name="typeName">The name of the preferred type. </param>
		/// <param name="ignoreCase">true to specify that the search for <paramref name="typeName" /> is not case-sensitive; false to specify that the search is case-sensitive. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="typeName" /> constructor. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyFile" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Security.Permissions.FileIOPermission" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		// Token: 0x06000EC2 RID: 3778 RVA: 0x0003D9C4 File Offset: 0x0003BBC4
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			return Activator.CreateInstanceFromInternal(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0003D9E4 File Offset: 0x0003BBE4
		private static ObjectHandle CreateInstanceFromInternal(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
		{
			object obj = Activator.CreateInstance(Assembly.LoadFrom(assemblyFile, securityInfo).GetType(typeName, true, ignoreCase), bindingAttr, binder, args, culture, activationAttributes);
			if (obj == null)
			{
				return null;
			}
			return new ObjectHandle(obj);
		}

		/// <summary>Creates an instance of the type whose name is specified in the specified remote domain, using the named assembly and default constructor.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <param name="domain">The remote domain where the type named <paramref name="typeName" /> is created.</param>
		/// <param name="assemblyName">The name of the assembly where the type named <paramref name="typeName" /> is sought. If <paramref name="assemblyName" /> is null, the executing assembly is searched. </param>
		/// <param name="typeName">The fully qualified name of the preferred type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> or <paramref name="domain" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract type. -or-This member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. -or-The assembly name or code base is invalid. </exception>
		// Token: 0x06000EC4 RID: 3780 RVA: 0x0003DA1B File Offset: 0x0003BC1B
		public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName);
		}

		/// <summary>Creates an instance of the type whose name is specified in the specified remote domain, using the named assembly and the constructor that best matches the specified parameters.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <param name="domain">The domain where the type named <paramref name="typeName" /> is created.</param>
		/// <param name="assemblyName">The name of the assembly where the type named <paramref name="typeName" /> is sought. If <paramref name="assemblyName" /> is null, the executing assembly is searched. </param>
		/// <param name="typeName">The fully qualified name of the preferred type. </param>
		/// <param name="ignoreCase">true to specify that the search for <paramref name="typeName" /> is not case-sensitive; false to specify that the search is case-sensitive. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="typeName" /> constructor. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <param name="securityAttributes">Information used to make security policy decisions and grant code permissions. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domain" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.-or- <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />. -or-The constructor that best matches <paramref name="args" /> has varargs arguments.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. -or-The assembly name or code base is invalid. </exception>
		// Token: 0x06000EC5 RID: 3781 RVA: 0x0003DA34 File Offset: 0x0003BC34
		[Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstance which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		/// <summary>Creates an instance of the type whose name is specified in the specified remote domain, using the named assembly and the constructor that best matches the specified parameters.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <param name="domain">The domain where the type named <paramref name="typeName" /> is created.</param>
		/// <param name="assemblyName">The name of the assembly where the type named <paramref name="typeName" /> is sought. If <paramref name="assemblyName" /> is null, the executing assembly is searched. </param>
		/// <param name="typeName">The fully qualified name of the preferred type. </param>
		/// <param name="ignoreCase">true to specify that the search for <paramref name="typeName" /> is not case-sensitive; false to specify that the search is case-sensitive. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="typeName" /> constructor. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domain" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />. </exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.-or- <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />. -or-The constructor that best matches <paramref name="args" /> has varargs arguments.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. -or-The assembly name or code base is invalid. </exception>
		// Token: 0x06000EC6 RID: 3782 RVA: 0x0003DA64 File Offset: 0x0003BC64
		public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null);
		}

		/// <summary>Creates an instance of the COM object whose name is specified, using the named assembly file and the constructor that best matches the specified parameters.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <param name="assemblyName">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought. </param>
		/// <param name="typeName">The name of the preferred type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> or <paramref name="assemblyName" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">An instance cannot be created through COM. -or-<paramref name="typename" /> was not found in <paramref name="assemblyName" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> is not found, or the module you are trying to load does not specify a file name extension. </exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class.-or-This member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyName" /> is the empty string (""). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06000EC7 RID: 3783 RVA: 0x0003DA93 File Offset: 0x0003BC93
		public static ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName)
		{
			return Activator.CreateComInstanceFrom(assemblyName, typeName, null, AssemblyHashAlgorithm.None);
		}

		/// <summary>Creates an instance of the COM object whose name is specified, using the named assembly file and the default constructor.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <param name="assemblyName">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought. </param>
		/// <param name="typeName">The name of the preferred type. </param>
		/// <param name="hashValue">The value of the computed hash code. </param>
		/// <param name="hashAlgorithm">The hash algorithm used for hashing files and generating the strong name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> or <paramref name="assemblyName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyName" /> is the empty string (""). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">An assembly or module was loaded twice with two different evidences, or the assembly name is longer than MAX_PATH characters. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> is not found, or the module you are trying to load does not specify a file name extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="assemblyName" /> is found but cannot be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. </exception>
		/// <exception cref="T:System.Security.SecurityException">A code base that does not start with "file://" was specified without the required WebPermission. </exception>
		/// <exception cref="T:System.TypeLoadException">An instance cannot be created through COM.-or- <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.MemberAccessException">An instance of an abstract class cannot be created. -or-This member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06000EC8 RID: 3784 RVA: 0x0003DAA0 File Offset: 0x0003BCA0
		public static ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyName, hashValue, hashAlgorithm);
			Type type = assembly.GetType(typeName, true, false);
			object[] customAttributes = type.GetCustomAttributes(typeof(ComVisibleAttribute), false);
			if (customAttributes.Length != 0 && !((ComVisibleAttribute)customAttributes[0]).Value)
			{
				throw new TypeLoadException(Environment.GetResourceString("The specified type must be visible from COM."));
			}
			if (assembly == null)
			{
				return null;
			}
			object obj = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, null);
			if (obj == null)
			{
				return null;
			}
			return new ObjectHandle(obj);
		}

		/// <summary>Creates an instance of the type designated by the specified <see cref="T:System.ActivationContext" /> object.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created object.</returns>
		/// <param name="activationContext">An activation context object that specifies the object to create.</param>
		// Token: 0x06000EC9 RID: 3785 RVA: 0x0003DB19 File Offset: 0x0003BD19
		public static ObjectHandle CreateInstance(ActivationContext activationContext)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Creates an instance of the type that is designated by the specified <see cref="T:System.ActivationContext" /> object and activated with the specified custom activation data.</summary>
		/// <returns>A handle that must be unwrapped to access the newly created object.</returns>
		/// <param name="activationContext">An activation context object that specifies the object to create.</param>
		/// <param name="activationCustomData">An array of Unicode strings that contain custom activation data.</param>
		// Token: 0x06000ECA RID: 3786 RVA: 0x0003DB19 File Offset: 0x0003BD19
		public static ObjectHandle CreateInstance(ActivationContext activationContext, string[] activationCustomData)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
