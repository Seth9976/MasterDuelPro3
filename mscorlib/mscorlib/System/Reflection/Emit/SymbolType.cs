using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000651 RID: 1617
	[StructLayout(LayoutKind.Sequential)]
	internal abstract class SymbolType : TypeInfo
	{
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
			}
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x000B7FD0 File Offset: 0x000B61D0
		public override Module Module
		{
			get
			{
				Type type = this.m_baseType;
				while (type is SymbolType)
				{
					type = ((SymbolType)type).m_baseType;
				}
				return type.Module;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060030AB RID: 12459 RVA: 0x000B8000 File Offset: 0x000B6200
		public override Assembly Assembly
		{
			get
			{
				Type type = this.m_baseType;
				while (type is SymbolType)
				{
					type = ((SymbolType)type).m_baseType;
				}
				return type.Assembly;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x000B8030 File Offset: 0x000B6230
		public override string Namespace
		{
			get
			{
				return this.m_baseType.Namespace;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x000B803D File Offset: 0x000B623D
		public override Type BaseType
		{
			get
			{
				return typeof(Array);
			}
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000B7FBD File Offset: 0x000B61BD
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000B7FBD File Offset: 0x000B61BD
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000B7FBD File Offset: 0x000B61BD
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000B7FBD File Offset: 0x000B61BD
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000B7FBD File Offset: 0x000B61BD
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000B804C File Offset: 0x000B624C
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			Type type = this.m_baseType;
			while (type is SymbolType)
			{
				type = ((SymbolType)type).m_baseType;
			}
			return type.Attributes;
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsValueTypeImpl()
		{
			return false;
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x00033991 File Offset: 0x00031B91
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000B807C File Offset: 0x000B627C
		public override Type GetElementType()
		{
			return this.m_baseType;
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000B8084 File Offset: 0x000B6284
		protected override bool HasElementTypeImpl()
		{
			return this.m_baseType != null;
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000B7FBD File Offset: 0x000B61BD
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000B8092 File Offset: 0x000B6292
		internal SymbolType(Type elementType)
		{
			this.m_baseType = elementType;
		}

		// Token: 0x060030CA RID: 12490
		internal abstract string FormatName(string elementName);

		// Token: 0x060030CB RID: 12491 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000B80A1 File Offset: 0x000B62A1
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x000B80AA File Offset: 0x000B62AA
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000B80BD File Offset: 0x000B62BD
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x000B80C5 File Offset: 0x000B62C5
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000B80CD File Offset: 0x000B62CD
		public override string ToString()
		{
			return this.FormatName(this.m_baseType.ToString());
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x000B80E0 File Offset: 0x000B62E0
		public override string AssemblyQualifiedName
		{
			get
			{
				string text = this.FormatName(this.m_baseType.FullName);
				if (text == null)
				{
					return null;
				}
				return text + ", " + this.m_baseType.Assembly.FullName;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x000B811F File Offset: 0x000B631F
		public override string FullName
		{
			get
			{
				return this.FormatName(this.m_baseType.FullName);
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x000B8132 File Offset: 0x000B6332
		public override string Name
		{
			get
			{
				return this.FormatName(this.m_baseType.Name);
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x00002645 File Offset: 0x00000845
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060030D7 RID: 12503 RVA: 0x000B8145 File Offset: 0x000B6345
		internal override bool IsUserType
		{
			get
			{
				return this.m_baseType.IsUserType;
			}
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000B8152 File Offset: 0x000B6352
		internal override Type RuntimeResolve()
		{
			return this.InternalResolve();
		}

		// Token: 0x040018DA RID: 6362
		internal Type m_baseType;
	}
}
