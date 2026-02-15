using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a method and provides access to method metadata.</summary>
	// Token: 0x0200060D RID: 1549
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_MethodInfo))]
	[Serializable]
	public abstract class MethodInfo : MethodBase, _MethodInfo
	{
		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a method.</returns>
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06002D1D RID: 11549 RVA: 0x00034325 File Offset: 0x00032525
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Method;
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.ParameterInfo" /> object that contains information about the return type of the method, such as whether the return type has custom modifiers. </summary>
		/// <returns>A <see cref="T:System.Reflection.ParameterInfo" /> object that contains information about the return type.</returns>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented.</exception>
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06002D1E RID: 11550 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual ParameterInfo ReturnParameter
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets the return type of this method.</summary>
		/// <returns>The return type of this method.</returns>
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual Type ReturnType
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic method or the type parameters of a generic method definition.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic method or the type parameters of a generic method definition. Returns an empty array if the current method is not a generic method.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06002D20 RID: 11552 RVA: 0x000339C9 File Offset: 0x00031BC9
		public override Type[] GetGenericArguments()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Returns a <see cref="T:System.Reflection.MethodInfo" /> object that represents a generic method definition from which the current method can be constructed.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing a generic method definition from which the current method can be constructed.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current method is not a generic method. That is, <see cref="P:System.Reflection.MethodInfo.IsGenericMethod" /> returns false. </exception>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06002D21 RID: 11553 RVA: 0x000339C9 File Offset: 0x00031BC9
		public virtual MethodInfo GetGenericMethodDefinition()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Substitutes the elements of an array of types for the type parameters of the current generic method definition, and returns a <see cref="T:System.Reflection.MethodInfo" /> object representing the resulting constructed method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object that represents the constructed method formed by substituting the elements of <paramref name="typeArguments" /> for the type parameters of the current generic method definition.</returns>
		/// <param name="typeArguments">An array of types to be substituted for the type parameters of the current generic method definition.</param>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Reflection.MethodInfo" /> does not represent a generic method definition. That is, <see cref="P:System.Reflection.MethodInfo.IsGenericMethodDefinition" /> returns false.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeArguments" /> is null.-or- Any element of <paramref name="typeArguments" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in <paramref name="typeArguments" /> is not the same as the number of type parameters of the current generic method definition.-or- An element of <paramref name="typeArguments" /> does not satisfy the constraints specified for the corresponding type parameter of the current generic method definition. </exception>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06002D22 RID: 11554 RVA: 0x000339C9 File Offset: 0x00031BC9
		public virtual MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>When overridden in a derived class, returns the MethodInfo object for the method on the direct or indirect base class in which the method represented by this instance was first declared.</summary>
		/// <returns>A MethodInfo object for the first implementation of this method.</returns>
		// Token: 0x06002D23 RID: 11555
		public abstract MethodInfo GetBaseDefinition();

		/// <summary>Creates a delegate of the specified type from this method.</summary>
		/// <returns>The delegate for this method.</returns>
		/// <param name="delegateType">The type of the delegate to create.</param>
		// Token: 0x06002D24 RID: 11556 RVA: 0x000339C9 File Offset: 0x00031BC9
		public virtual Delegate CreateDelegate(Type delegateType)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Creates a delegate of the specified type with the specified target from this method.</summary>
		/// <returns>The delegate for this method.</returns>
		/// <param name="delegateType">The type of the delegate to create.</param>
		/// <param name="target">The object targeted by the delegate.</param>
		// Token: 0x06002D25 RID: 11557 RVA: 0x000339C9 File Offset: 0x00031BC9
		public virtual Delegate CreateDelegate(Type delegateType, object target)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06002D26 RID: 11558 RVA: 0x000B186A File Offset: 0x000AFA6A
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002D27 RID: 11559 RVA: 0x000B1873 File Offset: 0x000AFA73
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.MethodInfo" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002D28 RID: 11560 RVA: 0x0004AE04 File Offset: 0x00049004
		public static bool operator ==(MethodInfo left, MethodInfo right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.MethodInfo" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002D29 RID: 11561 RVA: 0x000B2218 File Offset: 0x000B0418
		public static bool operator !=(MethodInfo left, MethodInfo right)
		{
			return !(left == right);
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x000B2224 File Offset: 0x000B0424
		internal virtual int GenericParameterCount
		{
			get
			{
				return this.GetGenericArguments().Length;
			}
		}
	}
}
