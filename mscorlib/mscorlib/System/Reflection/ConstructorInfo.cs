using System;
using System.Diagnostics;
using System.Globalization;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a class constructor and provides access to constructor metadata. </summary>
	// Token: 0x020005F4 RID: 1524
	[Serializable]
	public abstract class ConstructorInfo : MethodBase
	{
		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a constructor.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a constructor.</returns>
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x0000C091 File Offset: 0x0000A291
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Constructor;
			}
		}

		/// <summary>Invokes the constructor reflected by the instance that has the specified parameters, providing default values for the parameters not commonly used.</summary>
		/// <returns>An instance of the class associated with the constructor.</returns>
		/// <param name="parameters">An array of values that matches the number, order and type (under the constraints of the default binder) of the parameters for this constructor. If this constructor takes no parameters, then use either an array with zero elements or null, as in Object[] parameters = new Object[0]. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or false, depending on the specific element type. </param>
		/// <exception cref="T:System.MemberAccessException">The class is abstract.-or- The constructor is a class initializer. </exception>
		/// <exception cref="T:System.MethodAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The constructor is private or protected, and the caller lacks <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.MemberAccess" />. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="parameters" /> array does not contain values that match the types accepted by this constructor. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked constructor throws an exception. </exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">An incorrect number of parameters was passed. </exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, and <see cref="T:System.RuntimeArgumentHandle" /> types is not supported.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the necessary code access permission.</exception>
		// Token: 0x06002CA0 RID: 11424 RVA: 0x000B185A File Offset: 0x000AFA5A
		[DebuggerStepThrough]
		[DebuggerHidden]
		public object Invoke(object[] parameters)
		{
			return this.Invoke(BindingFlags.CreateInstance, null, parameters, null);
		}

		/// <summary>When implemented in a derived class, invokes the constructor reflected by this ConstructorInfo with the specified arguments, under the constraints of the specified Binder.</summary>
		/// <returns>An instance of the class associated with the constructor.</returns>
		/// <param name="invokeAttr">One of the BindingFlags values that specifies the type of binding. </param>
		/// <param name="binder">A Binder that defines a set of properties and enables the binding, coercion of argument types, and invocation of members using reflection. If <paramref name="binder" /> is null, then Binder.DefaultBinding is used. </param>
		/// <param name="parameters">An array of type Object used to match the number, order and type of the parameters for this constructor, under the constraints of <paramref name="binder" />. If this constructor does not require parameters, pass an array with zero elements, as in Object[] parameters = new Object[0]. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or false, depending on the specific element type. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="parameters" /> array does not contain values that match the types accepted by this constructor, under the constraints of the <paramref name="binder" />. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked constructor throws an exception. </exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">An incorrect number of parameters was passed. </exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, and <see cref="T:System.RuntimeArgumentHandle" /> types is not supported.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the necessary code access permissions.</exception>
		/// <exception cref="T:System.MemberAccessException">The class is abstract.-or- The constructor is a class initializer. </exception>
		/// <exception cref="T:System.MethodAccessException">The constructor is private or protected, and the caller lacks <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.MemberAccess" />. </exception>
		// Token: 0x06002CA1 RID: 11425
		public abstract object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06002CA2 RID: 11426 RVA: 0x000B186A File Offset: 0x000AFA6A
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002CA3 RID: 11427 RVA: 0x000B1873 File Offset: 0x000AFA73
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.ConstructorInfo" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise false.</returns>
		/// <param name="left">The first <see cref="T:System.Reflection.ConstructorInfo" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Reflection.ConstructorInfo" /> to compare.</param>
		// Token: 0x06002CA4 RID: 11428 RVA: 0x0004AE04 File Offset: 0x00049004
		public static bool operator ==(ConstructorInfo left, ConstructorInfo right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.ConstructorInfo" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise false.</returns>
		/// <param name="left">The first <see cref="T:System.Reflection.ConstructorInfo" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Reflection.ConstructorInfo" /> to compare.</param>
		// Token: 0x06002CA5 RID: 11429 RVA: 0x000B187B File Offset: 0x000AFA7B
		public static bool operator !=(ConstructorInfo left, ConstructorInfo right)
		{
			return !(left == right);
		}

		/// <summary>Represents the name of the class constructor method as it is stored in metadata. This name is always ".ctor". This field is read-only.</summary>
		// Token: 0x040016E7 RID: 5863
		public static readonly string ConstructorName = ".ctor";

		/// <summary>Represents the name of the type constructor method as it is stored in metadata. This name is always ".cctor". This property is read-only.</summary>
		// Token: 0x040016E8 RID: 5864
		public static readonly string TypeConstructorName = ".cctor";
	}
}
