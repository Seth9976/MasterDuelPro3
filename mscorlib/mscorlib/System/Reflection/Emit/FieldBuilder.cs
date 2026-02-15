using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a field. This class cannot be inherited.</summary>
	// Token: 0x02000664 RID: 1636
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_FieldBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class FieldBuilder : FieldInfo, _FieldBuilder
	{
		// Token: 0x060031E0 RID: 12768 RVA: 0x000BB1DC File Offset: 0x000B93DC
		internal FieldBuilder(TypeBuilder tb, string fieldName, Type type, FieldAttributes attributes, Type[] modReq, Type[] modOpt)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.attrs = attributes;
			this.name = fieldName;
			this.type = type;
			this.modReq = modReq;
			this.modOpt = modOpt;
			this.offset = -1;
			this.typeb = tb;
			((ModuleBuilder)tb.Module).RegisterToken(this, this.GetToken().Token);
		}

		/// <summary>Indicates the attributes of this field. This property is read-only.</summary>
		/// <returns>The attributes of this field.</returns>
		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x000BB256 File Offset: 0x000B9456
		public override FieldAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		/// <summary>Indicates a reference to the <see cref="T:System.Type" /> object for the type that declares this field. This property is read-only.</summary>
		/// <returns>A reference to the <see cref="T:System.Type" /> object for the type that declares this field.</returns>
		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x000BB25E File Offset: 0x000B945E
		public override Type DeclaringType
		{
			get
			{
				return this.typeb;
			}
		}

		/// <summary>Indicates the internal metadata handle for this field. This property is read-only.</summary>
		/// <returns>The internal metadata handle for this field.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x000BB266 File Offset: 0x000B9466
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw this.CreateNotSupportedException();
			}
		}

		/// <summary>Indicates the <see cref="T:System.Type" /> object that represents the type of this field. This property is read-only.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that represents the type of this field.</returns>
		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x000BB26E File Offset: 0x000B946E
		public override Type FieldType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Indicates the name of this field. This property is read-only.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of this field.</returns>
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x000BB276 File Offset: 0x000B9476
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Indicates the reference to the <see cref="T:System.Type" /> object from which this object was obtained. This property is read-only.</summary>
		/// <returns>A reference to the <see cref="T:System.Type" /> object from which this instance was obtained.</returns>
		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060031E6 RID: 12774 RVA: 0x000BB25E File Offset: 0x000B945E
		public override Type ReflectedType
		{
			get
			{
				return this.typeb;
			}
		}

		/// <summary>Returns all the custom attributes defined for this field.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> representing all the custom attributes of the constructor represented by this <see cref="T:System.Reflection.Emit.FieldBuilder" /> instance.</returns>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060031E7 RID: 12775 RVA: 0x000BB27E File Offset: 0x000B947E
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.typeb.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, inherit);
			}
			throw this.CreateNotSupportedException();
		}

		/// <summary>Returns all the custom attributes defined for this field identified by the given type.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> representing all the custom attributes of the constructor represented by this <see cref="T:System.Reflection.Emit.FieldBuilder" /> instance.</returns>
		/// <param name="attributeType">The custom attribute type. </param>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060031E8 RID: 12776 RVA: 0x000BB29B File Offset: 0x000B949B
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.typeb.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
			}
			throw this.CreateNotSupportedException();
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x000BB2B9 File Offset: 0x000B94B9
		public override int MetadataToken
		{
			get
			{
				return ((ModuleBuilder)this.typeb.Module).GetToken(this);
			}
		}

		/// <summary>Returns the token representing this field.</summary>
		/// <returns>Returns the <see cref="T:System.Reflection.Emit.FieldToken" /> object that represents the token for this field.</returns>
		// Token: 0x060031EA RID: 12778 RVA: 0x000BB2D1 File Offset: 0x000B94D1
		public FieldToken GetToken()
		{
			return new FieldToken(this.MetadataToken);
		}

		/// <summary>Retrieves the value of the field supported by the given object.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the value of the field reflected by this instance.</returns>
		/// <param name="obj">The object on which to access the field. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060031EB RID: 12779 RVA: 0x000BB266 File Offset: 0x000B9466
		public override object GetValue(object obj)
		{
			throw this.CreateNotSupportedException();
		}

		/// <summary>Indicates whether an attribute having the specified type is defined on a field.</summary>
		/// <returns>true if one or more instance of <paramref name="attributeType" /> is defined on this field; otherwise, false.</returns>
		/// <param name="attributeType">The type of the attribute. </param>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the field using <see cref="M:System.Type.GetField(System.String,System.Reflection.BindingFlags)" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Reflection.FieldInfo" />. </exception>
		// Token: 0x060031EC RID: 12780 RVA: 0x000BB266 File Offset: 0x000B9466
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x00033991 File Offset: 0x00031B91
		internal override int GetFieldOffset()
		{
			return 0;
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000BB2DE File Offset: 0x000B94DE
		internal void SetRVAData(byte[] data)
		{
			this.rva_data = (byte[])data.Clone();
		}

		/// <summary>Sets the value of the field supported by the given object.</summary>
		/// <param name="obj">The object on which to access the field. </param>
		/// <param name="val">The value to assign to the field. </param>
		/// <param name="invokeAttr">A member of IBinder that specifies the type of binding that is desired (for example, IBinder.CreateInstance, IBinder.ExactBinding). </param>
		/// <param name="binder">A set of properties and enabling for binding, coercion of argument types, and invocation of members using reflection. If binder is null, then IBinder.DefaultBinding is used. </param>
		/// <param name="culture">The software preferences of a particular culture. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060031EF RID: 12783 RVA: 0x000BB266 File Offset: 0x000B9466
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000B9312 File Offset: 0x000B7512
		private Exception CreateNotSupportedException()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000BB2F4 File Offset: 0x000B94F4
		internal void ResolveUserTypes()
		{
			this.type = TypeBuilder.ResolveUserType(this.type);
			TypeBuilder.ResolveUserTypes(this.modReq);
			TypeBuilder.ResolveUserTypes(this.modOpt);
			if (this.marshal_info != null)
			{
				this.marshal_info.marshaltyperef = TypeBuilder.ResolveUserType(this.marshal_info.marshaltyperef);
			}
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000BB34C File Offset: 0x000B954C
		internal FieldInfo RuntimeResolve()
		{
			RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(this.typeb.CreateType() as RuntimeType);
			return FieldInfo.GetFieldFromHandle(this.handle, runtimeTypeHandle);
		}

		/// <summary>Gets the module in which the type that contains this field is being defined.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> that represents the dynamic module in which this field is being defined.</returns>
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x000BB37C File Offset: 0x000B957C
		public override Module Module
		{
			get
			{
				return base.Module;
			}
		}

		// Token: 0x0400195A RID: 6490
		private FieldAttributes attrs;

		// Token: 0x0400195B RID: 6491
		private Type type;

		// Token: 0x0400195C RID: 6492
		private string name;

		// Token: 0x0400195D RID: 6493
		private object def_value;

		// Token: 0x0400195E RID: 6494
		private int offset;

		// Token: 0x0400195F RID: 6495
		internal TypeBuilder typeb;

		// Token: 0x04001960 RID: 6496
		private byte[] rva_data;

		// Token: 0x04001961 RID: 6497
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04001962 RID: 6498
		private UnmanagedMarshal marshal_info;

		// Token: 0x04001963 RID: 6499
		private RuntimeFieldHandle handle;

		// Token: 0x04001964 RID: 6500
		private Type[] modReq;

		// Token: 0x04001965 RID: 6501
		private Type[] modOpt;
	}
}
