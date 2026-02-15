using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a field and provides access to field metadata. </summary>
	// Token: 0x020005FE RID: 1534
	[Serializable]
	public abstract class FieldInfo : MemberInfo
	{
		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a field.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a field.</returns>
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x00019A7F File Offset: 0x00017C7F
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		/// <summary>Gets the attributes associated with this field.</summary>
		/// <returns>The FieldAttributes for this field.</returns>
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06002CC5 RID: 11461
		public abstract FieldAttributes Attributes { get; }

		/// <summary>Gets the type of this field object.</summary>
		/// <returns>The type of this field object.</returns>
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06002CC6 RID: 11462
		public abstract Type FieldType { get; }

		/// <summary>Gets a value indicating whether the field can only be set in the body of the constructor.</summary>
		/// <returns>true if the field has the InitOnly attribute set; otherwise, false.</returns>
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x000B1A2E File Offset: 0x000AFC2E
		public bool IsInitOnly
		{
			get
			{
				return (this.Attributes & FieldAttributes.InitOnly) > FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the value is written at compile time and cannot be changed.</summary>
		/// <returns>true if the field has the Literal attribute set; otherwise, false.</returns>
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x000B1A3C File Offset: 0x000AFC3C
		public bool IsLiteral
		{
			get
			{
				return (this.Attributes & FieldAttributes.Literal) > FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether this field has the NotSerialized attribute.</summary>
		/// <returns>true if the field has the NotSerialized attribute set; otherwise, false.</returns>
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x000B1A4A File Offset: 0x000AFC4A
		public bool IsNotSerialized
		{
			get
			{
				return (this.Attributes & FieldAttributes.NotSerialized) > FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the corresponding SpecialName attribute is set in the <see cref="T:System.Reflection.FieldAttributes" /> enumerator.</summary>
		/// <returns>true if the SpecialName attribute is set in <see cref="T:System.Reflection.FieldAttributes" />; otherwise, false.</returns>
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x000B1A5B File Offset: 0x000AFC5B
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & FieldAttributes.SpecialName) > FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the field is static.</summary>
		/// <returns>true if this field is static; otherwise, false.</returns>
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x000B1A6C File Offset: 0x000AFC6C
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & FieldAttributes.Static) > FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the field is private.</summary>
		/// <returns>true if the field is private; otherwise; false.</returns>
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002CCC RID: 11468 RVA: 0x000B1A7A File Offset: 0x000AFC7A
		public bool IsPrivate
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Private;
			}
		}

		/// <summary>Gets a value indicating whether the field is public.</summary>
		/// <returns>true if this field is public; otherwise, false.</returns>
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002CCD RID: 11469 RVA: 0x000B1A87 File Offset: 0x000AFC87
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
			}
		}

		/// <summary>Gets a RuntimeFieldHandle, which is a handle to the internal metadata representation of a field.</summary>
		/// <returns>A handle to the internal metadata representation of a field.</returns>
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002CCE RID: 11470
		public abstract RuntimeFieldHandle FieldHandle { get; }

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06002CCF RID: 11471 RVA: 0x000B1944 File Offset: 0x000AFB44
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002CD0 RID: 11472 RVA: 0x000B194D File Offset: 0x000AFB4D
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.FieldInfo" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002CD1 RID: 11473 RVA: 0x0004AE04 File Offset: 0x00049004
		public static bool operator ==(FieldInfo left, FieldInfo right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.FieldInfo" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002CD2 RID: 11474 RVA: 0x000B1A94 File Offset: 0x000AFC94
		public static bool operator !=(FieldInfo left, FieldInfo right)
		{
			return !(left == right);
		}

		/// <summary>When overridden in a derived class, returns the value of a field supported by a given object.</summary>
		/// <returns>An object containing the value of the field reflected by this instance.</returns>
		/// <param name="obj">The object whose field value will be returned. </param>
		/// <exception cref="T:System.Reflection.TargetException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.The field is non-static and <paramref name="obj" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">A field is marked literal, but the field does not have one of the accepted literal types. </exception>
		/// <exception cref="T:System.FieldAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to access this field. </exception>
		/// <exception cref="T:System.ArgumentException">The method is neither declared nor inherited by the class of <paramref name="obj" />. </exception>
		// Token: 0x06002CD3 RID: 11475
		public abstract object GetValue(object obj);

		/// <summary>Sets the value of the field supported by the given object.</summary>
		/// <param name="obj">The object whose field value will be set. </param>
		/// <param name="value">The value to assign to the field. </param>
		/// <exception cref="T:System.FieldAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to access this field. </exception>
		/// <exception cref="T:System.Reflection.TargetException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.The <paramref name="obj" /> parameter is null and the field is an instance field. </exception>
		/// <exception cref="T:System.ArgumentException">The field does not exist on the object.-or- The <paramref name="value" /> parameter cannot be converted and stored in the field. </exception>
		// Token: 0x06002CD4 RID: 11476 RVA: 0x000B1AA0 File Offset: 0x000AFCA0
		[DebuggerHidden]
		[DebuggerStepThrough]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, BindingFlags.Default, Type.DefaultBinder, null);
		}

		/// <summary>When overridden in a derived class, sets the value of the field supported by the given object.</summary>
		/// <param name="obj">The object whose field value will be set. </param>
		/// <param name="value">The value to assign to the field. </param>
		/// <param name="invokeAttr">A field of Binder that specifies the type of binding that is desired (for example, Binder.CreateInstance or Binder.ExactBinding). </param>
		/// <param name="binder">A set of properties that enables the binding, coercion of argument types, and invocation of members through reflection. If <paramref name="binder" /> is null, then Binder.DefaultBinding is used. </param>
		/// <param name="culture">The software preferences of a particular culture. </param>
		/// <exception cref="T:System.FieldAccessException">The caller does not have permission to access this field. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The <paramref name="obj" /> parameter is null and the field is an instance field. </exception>
		/// <exception cref="T:System.ArgumentException">The field does not exist on the object.-or- The <paramref name="value" /> parameter cannot be converted and stored in the field. </exception>
		// Token: 0x06002CD5 RID: 11477
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		/// <summary>Sets the value of the field supported by the given object.</summary>
		/// <param name="obj">A <see cref="T:System.TypedReference" /> structure that encapsulates a managed pointer to a location and a runtime representation of the type that can be stored at that location. </param>
		/// <param name="value">The value to assign to the field. </param>
		/// <exception cref="T:System.NotSupportedException">The caller requires the Common Language Specification (CLS) alternative, but called this method instead. </exception>
		// Token: 0x06002CD6 RID: 11478 RVA: 0x000B1AB1 File Offset: 0x000AFCB1
		[CLSCompliant(false)]
		public virtual void SetValueDirect(TypedReference obj, object value)
		{
			throw new NotSupportedException("This non-CLS method is not implemented.");
		}

		/// <summary>Returns a literal value associated with the field by a compiler. </summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the literal value associated with the field. If the literal value is a class type with an element value of zero, the return value is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current field.</exception>
		/// <exception cref="T:System.FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification Metadata Logical Format: Other Structures, Element Types used in Signatures. </exception>
		/// <exception cref="T:System.NotSupportedException">The constant value for the field is not set. </exception>
		// Token: 0x06002CD7 RID: 11479 RVA: 0x000B1AB1 File Offset: 0x000AFCB1
		public virtual object GetRawConstantValue()
		{
			throw new NotSupportedException("This non-CLS method is not implemented.");
		}

		// Token: 0x06002CD8 RID: 11480
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern FieldInfo internal_from_handle_type(IntPtr field_handle, IntPtr type_handle);

		/// <summary>Gets a <see cref="T:System.Reflection.FieldInfo" /> for the field represented by the specified handle.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field specified by <paramref name="handle" />.</returns>
		/// <param name="handle">A <see cref="T:System.RuntimeFieldHandle" /> structure that contains the handle to the internal metadata representation of a field. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.</exception>
		// Token: 0x06002CD9 RID: 11481 RVA: 0x000B1ABD File Offset: 0x000AFCBD
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			return FieldInfo.internal_from_handle_type(handle.Value, IntPtr.Zero);
		}

		/// <summary>Gets a <see cref="T:System.Reflection.FieldInfo" /> for the field represented by the specified handle, for the specified generic type.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field specified by <paramref name="handle" />, in the generic type specified by <paramref name="declaringType" />.</returns>
		/// <param name="handle">A <see cref="T:System.RuntimeFieldHandle" /> structure that contains the handle to the internal metadata representation of a field.</param>
		/// <param name="declaringType">A <see cref="T:System.RuntimeTypeHandle" /> structure that contains the handle to the generic type that defines the field.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.-or-<paramref name="declaringType" /> is not compatible with <paramref name="handle" />. For example, <paramref name="declaringType" /> is the runtime type handle of the generic type definition, and <paramref name="handle" /> comes from a constructed type. See Remarks.</exception>
		// Token: 0x06002CDA RID: 11482 RVA: 0x000B1AF0 File Offset: 0x000AFCF0
		[ComVisible(false)]
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			FieldInfo fieldInfo = FieldInfo.internal_from_handle_type(handle.Value, declaringType.Value);
			if (fieldInfo == null)
			{
				throw new ArgumentException("The field handle and the type handle are incompatible.");
			}
			return fieldInfo;
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000B1B42 File Offset: 0x000AFD42
		internal virtual int GetFieldOffset()
		{
			throw new SystemException("This method should not be called");
		}

		// Token: 0x06002CDC RID: 11484
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MarshalAsAttribute get_marshal_info();

		// Token: 0x06002CDD RID: 11485 RVA: 0x000B1B50 File Offset: 0x000AFD50
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			if (this.IsNotSerialized)
			{
				num++;
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				num++;
			}
			MarshalAsAttribute marshal_info = this.get_marshal_info();
			if (marshal_info != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if (this.IsNotSerialized)
			{
				array[num++] = new NonSerializedAttribute();
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				array[num++] = new FieldOffsetAttribute(this.GetFieldOffset());
			}
			if (marshal_info != null)
			{
				array[num++] = marshal_info;
			}
			return array;
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000B1BD8 File Offset: 0x000AFDD8
		internal CustomAttributeData[] GetPseudoCustomAttributesData()
		{
			int num = 0;
			if (this.IsNotSerialized)
			{
				num++;
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				num++;
			}
			MarshalAsAttribute marshal_info = this.get_marshal_info();
			if (marshal_info != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			CustomAttributeData[] array = new CustomAttributeData[num];
			num = 0;
			if (this.IsNotSerialized)
			{
				array[num++] = new CustomAttributeData(typeof(NonSerializedAttribute).GetConstructor(Type.EmptyTypes));
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				CustomAttributeTypedArgument[] array2 = new CustomAttributeTypedArgument[]
				{
					new CustomAttributeTypedArgument(typeof(int), this.GetFieldOffset())
				};
				array[num++] = new CustomAttributeData(typeof(FieldOffsetAttribute).GetConstructor(new Type[] { typeof(int) }), array2, EmptyArray<CustomAttributeNamedArgument>.Value);
			}
			if (marshal_info != null)
			{
				CustomAttributeTypedArgument[] array3 = new CustomAttributeTypedArgument[]
				{
					new CustomAttributeTypedArgument(typeof(UnmanagedType), marshal_info.Value)
				};
				array[num++] = new CustomAttributeData(typeof(MarshalAsAttribute).GetConstructor(new Type[] { typeof(UnmanagedType) }), array3, EmptyArray<CustomAttributeNamedArgument>.Value);
			}
			return array;
		}
	}
}
