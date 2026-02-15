using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines and creates generic type parameters for dynamically defined generic types and methods. This class cannot be inherited. </summary>
	// Token: 0x02000667 RID: 1639
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GenericTypeParameterBuilder : TypeInfo
	{
		// Token: 0x06003208 RID: 12808 RVA: 0x0000C091 File Offset: 0x0000A291
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return TypeAttributes.Public;
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x000BB449 File Offset: 0x000B9649
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases. </exception>
		// Token: 0x0600320A RID: 12810 RVA: 0x000BB449 File Offset: 0x000B9649
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported. </param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600320B RID: 12811 RVA: 0x000BB449 File Offset: 0x000B9649
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600320C RID: 12812 RVA: 0x000BB449 File Offset: 0x000B9649
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600320D RID: 12813 RVA: 0x000BB449 File Offset: 0x000B9649
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600320E RID: 12814 RVA: 0x000BB449 File Offset: 0x000B9649
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">The name of the interface.</param>
		/// <param name="ignoreCase">true to search without regard for case; false to make a case-sensitive search.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600320F RID: 12815 RVA: 0x000BB449 File Offset: 0x000B9649
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06003210 RID: 12816 RVA: 0x000BB449 File Offset: 0x000B9649
		public override Type[] GetInterfaces()
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06003211 RID: 12817 RVA: 0x000BB449 File Offset: 0x000B9649
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06003212 RID: 12818 RVA: 0x000BB449 File Offset: 0x000B9649
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000BB449 File Offset: 0x000B9649
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06003214 RID: 12820 RVA: 0x000BB449 File Offset: 0x000B9649
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06003215 RID: 12821 RVA: 0x000BB449 File Offset: 0x000B9649
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x000BB449 File Offset: 0x000B9649
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">Not supported. </param>
		/// <param name="invokeAttr">Not supported.</param>
		/// <param name="binder">Not supported.</param>
		/// <param name="target">Not supported.</param>
		/// <param name="args">Not supported.</param>
		/// <param name="modifiers">Not supported.</param>
		/// <param name="culture">Not supported.</param>
		/// <param name="namedParameters">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600321D RID: 12829 RVA: 0x000BB449 File Offset: 0x000B9649
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw this.not_supported();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> in all cases. </summary>
		/// <returns>The type referred to by the current array type, pointer type, or ByRef type; or null if the current type is not an array type, is not a pointer type, and is not passed by reference.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600321E RID: 12830 RVA: 0x000BB449 File Offset: 0x000B9649
		public override Type GetElementType()
		{
			throw this.not_supported();
		}

		/// <summary>Gets the current generic type parameter.</summary>
		/// <returns>The current <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> object.</returns>
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600321F RID: 12831 RVA: 0x00002645 File Offset: 0x00000845
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Assembly" /> object representing the dynamic assembly that contains the generic type definition the current type parameter belongs to.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object representing the dynamic assembly that contains the generic type definition the current type parameter belongs to.</returns>
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x000BB451 File Offset: 0x000B9651
		public override Assembly Assembly
		{
			get
			{
				return this.tbuilder.Assembly;
			}
		}

		/// <summary>Gets null in all cases.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) in all cases.</returns>
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x000082D2 File Offset: 0x000064D2
		public override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the base type constraint of the current generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the base type constraint of the generic type parameter, or null if the type parameter has no base type constraint.</returns>
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x000BB45E File Offset: 0x000B965E
		public override Type BaseType
		{
			get
			{
				return this.base_type;
			}
		}

		/// <summary>Gets null in all cases.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) in all cases.</returns>
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000082D2 File Offset: 0x000064D2
		public override string FullName
		{
			get
			{
				return null;
			}
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases. </exception>
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x000BB449 File Offset: 0x000B9649
		public override Guid GUID
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="attributeType">Not supported.</param>
		/// <param name="inherit">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06003225 RID: 12837 RVA: 0x000BB449 File Offset: 0x000B9649
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06003226 RID: 12838 RVA: 0x000BB449 File Offset: 0x000B9649
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06003227 RID: 12839 RVA: 0x000BB449 File Offset: 0x000B9649
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Gets the name of the generic type parameter.</summary>
		/// <returns>The name of the generic type parameter.</returns>
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x000BB466 File Offset: 0x000B9666
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets null in all cases.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) in all cases.</returns>
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x000082D2 File Offset: 0x000064D2
		public override string Namespace
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the dynamic module that contains the generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> object that represents the dynamic module that contains the generic type parameter.</returns>
		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x000BB46E File Offset: 0x000B966E
		public override Module Module
		{
			get
			{
				return this.tbuilder.Module;
			}
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x0003988C File Offset: 0x00037A8C
		private Exception not_supported()
		{
			return new NotSupportedException();
		}

		// Token: 0x04001969 RID: 6505
		private TypeBuilder tbuilder;

		// Token: 0x0400196A RID: 6506
		private MethodBuilder mbuilder;

		// Token: 0x0400196B RID: 6507
		private string name;

		// Token: 0x0400196C RID: 6508
		private int index;

		// Token: 0x0400196D RID: 6509
		private Type base_type;

		// Token: 0x0400196E RID: 6510
		private Type[] iface_constraints;

		// Token: 0x0400196F RID: 6511
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04001970 RID: 6512
		private GenericParameterAttributes attrs;
	}
}
