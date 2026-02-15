using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x02000621 RID: 1569
	internal abstract class SignatureType : Type
	{
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06002DC9 RID: 11721 RVA: 0x0000C091 File Offset: 0x0000A291
		public sealed override bool IsSignatureType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002DCA RID: 11722
		protected abstract override bool HasElementTypeImpl();

		// Token: 0x06002DCB RID: 11723
		protected abstract override bool IsArrayImpl();

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06002DCC RID: 11724
		public abstract override bool IsSZArray { get; }

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06002DCD RID: 11725
		public abstract override bool IsVariableBoundArray { get; }

		// Token: 0x06002DCE RID: 11726
		protected abstract override bool IsByRefImpl();

		// Token: 0x06002DCF RID: 11727
		protected abstract override bool IsPointerImpl();

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x000B29B3 File Offset: 0x000B0BB3
		public sealed override bool IsGenericType
		{
			get
			{
				return this.IsGenericTypeDefinition || this.IsConstructedGenericType;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06002DD1 RID: 11729
		public abstract override bool IsGenericTypeDefinition { get; }

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06002DD2 RID: 11730
		public abstract override bool IsConstructedGenericType { get; }

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06002DD3 RID: 11731
		public abstract override bool IsGenericParameter { get; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06002DD4 RID: 11732
		public abstract override bool IsGenericMethodParameter { get; }

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06002DD5 RID: 11733
		public abstract override bool ContainsGenericParameters { get; }

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06002DD6 RID: 11734 RVA: 0x00033958 File Offset: 0x00031B58
		public sealed override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.TypeInfo;
			}
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000B29C5 File Offset: 0x000B0BC5
		public sealed override Type MakeArrayType()
		{
			return new SignatureArrayType(this, 1, false);
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000B29CF File Offset: 0x000B0BCF
		public sealed override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			return new SignatureArrayType(this, rank, true);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000B29E3 File Offset: 0x000B0BE3
		public sealed override Type MakeByRefType()
		{
			return new SignatureByRefType(this);
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000B29EB File Offset: 0x000B0BEB
		public sealed override Type MakePointerType()
		{
			return new SignaturePointerType(this);
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override Type MakeGenericType(params Type[] typeArguments)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000B29FF File Offset: 0x000B0BFF
		public sealed override Type GetElementType()
		{
			return this.ElementType;
		}

		// Token: 0x06002DDD RID: 11741
		public abstract override int GetArrayRank();

		// Token: 0x06002DDE RID: 11742
		public abstract override Type GetGenericTypeDefinition();

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06002DDF RID: 11743
		public abstract override Type[] GenericTypeArguments { get; }

		// Token: 0x06002DE0 RID: 11744
		public abstract override Type[] GetGenericArguments();

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06002DE1 RID: 11745
		public abstract override int GenericParameterPosition { get; }

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06002DE2 RID: 11746
		internal abstract SignatureType ElementType { get; }

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x00002645 File Offset: 0x00000845
		public sealed override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002DE4 RID: 11748
		public abstract override string Name { get; }

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06002DE5 RID: 11749
		public abstract override string Namespace { get; }

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x000082D2 File Offset: 0x000064D2
		public sealed override string FullName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06002DE7 RID: 11751 RVA: 0x000082D2 File Offset: 0x000064D2
		public sealed override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002DE8 RID: 11752
		public abstract override string ToString();

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override Assembly Assembly
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06002DEA RID: 11754 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override Module Module
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override Type ReflectedType
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override Type BaseType
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override Type[] GetInterfaces()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override bool IsAssignableFrom(Type c)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override int MetadataToken
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override Type DeclaringType
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override MethodBase DeclaringMethod
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override Type[] GetGenericParameterConstraints()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override bool IsEnumDefined(object value)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override string GetEnumName(object value)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override string[] GetEnumNames()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override Type GetEnumUnderlyingType()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override Array GetEnumValues()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06002DF9 RID: 11769 RVA: 0x000B2A08 File Offset: 0x000B0C08
		public sealed override Guid GUID
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		protected sealed override TypeCode GetTypeCodeImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		protected sealed override TypeAttributes GetAttributeFlagsImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		protected sealed override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		protected sealed override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override MemberInfo[] GetDefaultMembers()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		protected sealed override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		protected sealed override bool IsCOMObjectImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		protected sealed override bool IsPrimitiveImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x000B2A20 File Offset: 0x000B0C20
		public sealed override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		protected sealed override bool IsContextfulImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06002E14 RID: 11796 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override bool IsEnum
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override bool IsEquivalentTo(Type other)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override bool IsInstanceOfType(object o)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		protected sealed override bool IsMarshalByRefImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06002E18 RID: 11800 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override bool IsSerializable
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public sealed override bool IsSubclassOf(Type c)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		protected sealed override bool IsValueTypeImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x000B2A38 File Offset: 0x000B0C38
		public sealed override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}
	}
}
