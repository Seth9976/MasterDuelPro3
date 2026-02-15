using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000683 RID: 1667
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class TypeBuilderInstantiation : TypeInfo
	{
		// Token: 0x060033FC RID: 13308 RVA: 0x000C3F40 File Offset: 0x000C2140
		internal TypeBuilderInstantiation(Type tb, Type[] args)
		{
			this.generic_type = tb;
			this.type_arguments = args;
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000C3F58 File Offset: 0x000C2158
		internal override Type InternalResolve()
		{
			Type type = this.generic_type.InternalResolve();
			Type[] array = new Type[this.type_arguments.Length];
			for (int i = 0; i < this.type_arguments.Length; i++)
			{
				array[i] = this.type_arguments[i].InternalResolve();
			}
			return type.MakeGenericType(array);
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000C3FAC File Offset: 0x000C21AC
		internal override Type RuntimeResolve()
		{
			TypeBuilder typeBuilder = this.generic_type as TypeBuilder;
			if (typeBuilder != null && !typeBuilder.IsCreated())
			{
				AppDomain.CurrentDomain.DoTypeBuilderResolve(typeBuilder);
			}
			for (int i = 0; i < this.type_arguments.Length; i++)
			{
				TypeBuilder typeBuilder2 = this.type_arguments[i] as TypeBuilder;
				if (typeBuilder2 != null && !typeBuilder2.IsCreated())
				{
					AppDomain.CurrentDomain.DoTypeBuilderResolve(typeBuilder2);
				}
			}
			return this.InternalResolve();
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060033FF RID: 13311 RVA: 0x000C401C File Offset: 0x000C221C
		internal bool IsCreated
		{
			get
			{
				TypeBuilder typeBuilder = this.generic_type as TypeBuilder;
				return !(typeBuilder != null) || typeBuilder.is_created;
			}
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000C4046 File Offset: 0x000C2246
		internal Type InflateType(Type type)
		{
			return TypeBuilderInstantiation.InflateType(type, this.type_arguments, null);
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000C4058 File Offset: 0x000C2258
		internal static Type InflateType(Type type, Type[] type_args, Type[] method_args)
		{
			if (type == null)
			{
				return null;
			}
			if (!type.IsGenericParameter && !type.ContainsGenericParameters)
			{
				return type;
			}
			if (type.IsGenericParameter)
			{
				if (type.DeclaringMethod == null)
				{
					if (type_args != null)
					{
						return type_args[type.GenericParameterPosition];
					}
					return type;
				}
				else
				{
					if (method_args != null)
					{
						return method_args[type.GenericParameterPosition];
					}
					return type;
				}
			}
			else
			{
				if (type.IsPointer)
				{
					return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakePointerType();
				}
				if (type.IsByRef)
				{
					return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakeByRefType();
				}
				if (!type.IsArray)
				{
					Type[] genericArguments = type.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						genericArguments[i] = TypeBuilderInstantiation.InflateType(genericArguments[i], type_args, method_args);
					}
					return (type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition()).MakeGenericType(genericArguments);
				}
				if (type.GetArrayRank() > 1)
				{
					return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakeArrayType(type.GetArrayRank());
				}
				if (type.ToString().EndsWith("[*]", StringComparison.Ordinal))
				{
					return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakeArrayType(1);
				}
				return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakeArrayType();
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06003402 RID: 13314 RVA: 0x000C4187 File Offset: 0x000C2387
		public override Type BaseType
		{
			get
			{
				return this.generic_type.BaseType;
			}
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000339FF File Offset: 0x00031BFF
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000C4194 File Offset: 0x000C2394
		protected override bool IsValueTypeImpl()
		{
			return this.generic_type.IsValueType;
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000C41A4 File Offset: 0x000C23A4
		internal override MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			if (this.methods == null)
			{
				this.methods = new Hashtable();
			}
			if (!this.methods.ContainsKey(fromNoninstanciated))
			{
				this.methods[fromNoninstanciated] = new MethodOnTypeBuilderInst(this, fromNoninstanciated);
			}
			return (MethodInfo)this.methods[fromNoninstanciated];
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000C41F8 File Offset: 0x000C23F8
		internal override ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			if (this.ctors == null)
			{
				this.ctors = new Hashtable();
			}
			if (!this.ctors.ContainsKey(fromNoninstanciated))
			{
				this.ctors[fromNoninstanciated] = new ConstructorOnTypeBuilderInst(this, fromNoninstanciated);
			}
			return (ConstructorInfo)this.ctors[fromNoninstanciated];
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x000C424C File Offset: 0x000C244C
		internal override FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			if (this.fields == null)
			{
				this.fields = new Hashtable();
			}
			if (!this.fields.ContainsKey(fromNoninstanciated))
			{
				this.fields[fromNoninstanciated] = new FieldOnTypeBuilderInst(this, fromNoninstanciated);
			}
			return (FieldInfo)this.fields[fromNoninstanciated];
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x000339FF File Offset: 0x00031BFF
		public override MethodInfo[] GetMethods(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x000339FF File Offset: 0x00031BFF
		public override ConstructorInfo[] GetConstructors(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x000339FF File Offset: 0x00031BFF
		public override FieldInfo[] GetFields(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x000339FF File Offset: 0x00031BFF
		public override PropertyInfo[] GetProperties(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x000339FF File Offset: 0x00031BFF
		public override EventInfo[] GetEvents(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x000339FF File Offset: 0x00031BFF
		public override bool IsAssignableFrom(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x0600340E RID: 13326 RVA: 0x00002645 File Offset: 0x00000845
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x0600340F RID: 13327 RVA: 0x000C429E File Offset: 0x000C249E
		public override Assembly Assembly
		{
			get
			{
				return this.generic_type.Assembly;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06003410 RID: 13328 RVA: 0x000C42AB File Offset: 0x000C24AB
		public override Module Module
		{
			get
			{
				return this.generic_type.Module;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x000C42B8 File Offset: 0x000C24B8
		public override string Name
		{
			get
			{
				return this.generic_type.Name;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x000C42C5 File Offset: 0x000C24C5
		public override string Namespace
		{
			get
			{
				return this.generic_type.Namespace;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x000C42D2 File Offset: 0x000C24D2
		public override string FullName
		{
			get
			{
				return this.format_name(true, false);
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06003414 RID: 13332 RVA: 0x000C42DC File Offset: 0x000C24DC
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.format_name(true, true);
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x000339FF File Offset: 0x00031BFF
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000C42E8 File Offset: 0x000C24E8
		private string format_name(bool full_name, bool assembly_qualified)
		{
			StringBuilder stringBuilder = new StringBuilder(this.generic_type.FullName);
			stringBuilder.Append("[");
			for (int i = 0; i < this.type_arguments.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				string text;
				if (full_name)
				{
					string fullName = this.type_arguments[i].Assembly.FullName;
					text = this.type_arguments[i].FullName;
					if (text != null && fullName != null)
					{
						text = text + ", " + fullName;
					}
				}
				else
				{
					text = this.type_arguments[i].ToString();
				}
				if (text == null)
				{
					return null;
				}
				if (full_name)
				{
					stringBuilder.Append("[");
				}
				stringBuilder.Append(text);
				if (full_name)
				{
					stringBuilder.Append("]");
				}
			}
			stringBuilder.Append("]");
			if (assembly_qualified)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(this.generic_type.Assembly.FullName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000C43E5 File Offset: 0x000C25E5
		public override string ToString()
		{
			return this.format_name(false, false);
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000C43EF File Offset: 0x000C25EF
		public override Type GetGenericTypeDefinition()
		{
			return this.generic_type;
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000C43F8 File Offset: 0x000C25F8
		public override Type[] GetGenericArguments()
		{
			Type[] array = new Type[this.type_arguments.Length];
			this.type_arguments.CopyTo(array, 0);
			return array;
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x0600341A RID: 13338 RVA: 0x000C4424 File Offset: 0x000C2624
		public override bool ContainsGenericParameters
		{
			get
			{
				Type[] array = this.type_arguments;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x0600341B RID: 13339 RVA: 0x00033991 File Offset: 0x00031B91
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600341C RID: 13340 RVA: 0x0000C091 File Offset: 0x0000A291
		public override bool IsGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600341D RID: 13341 RVA: 0x000C4453 File Offset: 0x000C2653
		public override Type DeclaringType
		{
			get
			{
				return this.generic_type.DeclaringType;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x0600341E RID: 13342 RVA: 0x000339FF File Offset: 0x00031BFF
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000B80A1 File Offset: 0x000B62A1
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000B80AA File Offset: 0x000B62AA
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000B80BD File Offset: 0x000B62BD
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000B80C5 File Offset: 0x000B62C5
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000339FF File Offset: 0x00031BFF
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x000C4460 File Offset: 0x000C2660
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.generic_type.Attributes;
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000339FF File Offset: 0x00031BFF
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x000339FF File Offset: 0x00031BFF
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000339FF File Offset: 0x00031BFF
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000339FF File Offset: 0x00031BFF
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000339FF File Offset: 0x00031BFF
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000339FF File Offset: 0x00031BFF
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000339FF File Offset: 0x00031BFF
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000339FF File Offset: 0x00031BFF
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x000339FF File Offset: 0x00031BFF
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000339FF File Offset: 0x00031BFF
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000C446D File Offset: 0x000C266D
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.IsCreated)
			{
				return this.generic_type.GetCustomAttributes(inherit);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000C4489 File Offset: 0x000C2689
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.IsCreated)
			{
				return this.generic_type.GetCustomAttributes(attributeType, inherit);
			}
			throw new NotSupportedException();
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x000C44A8 File Offset: 0x000C26A8
		internal override bool IsUserType
		{
			get
			{
				Type[] array = this.type_arguments;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].IsUserType)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000C44D7 File Offset: 0x000C26D7
		internal static Type MakeGenericType(Type type, Type[] typeArguments)
		{
			return new TypeBuilderInstantiation(type, typeArguments);
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x0000C091 File Offset: 0x0000A291
		public override bool IsConstructedGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001B1E RID: 6942
		internal Type generic_type;

		// Token: 0x04001B1F RID: 6943
		private Type[] type_arguments;

		// Token: 0x04001B20 RID: 6944
		private Hashtable fields;

		// Token: 0x04001B21 RID: 6945
		private Hashtable ctors;

		// Token: 0x04001B22 RID: 6946
		private Hashtable methods;
	}
}
