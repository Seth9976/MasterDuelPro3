using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000674 RID: 1652
	[StructLayout(LayoutKind.Sequential)]
	internal class MethodOnTypeBuilderInst : MethodInfo
	{
		// Token: 0x060032C5 RID: 12997 RVA: 0x000BE226 File Offset: 0x000BC426
		public MethodOnTypeBuilderInst(TypeBuilderInstantiation instantiation, MethodInfo base_method)
		{
			this.instantiation = instantiation;
			this.base_method = base_method;
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000BE23C File Offset: 0x000BC43C
		internal MethodOnTypeBuilderInst(MethodOnTypeBuilderInst gmd, Type[] typeArguments)
		{
			this.instantiation = gmd.instantiation;
			this.base_method = gmd.base_method;
			this.method_arguments = new Type[typeArguments.Length];
			typeArguments.CopyTo(this.method_arguments, 0);
			this.generic_method_definition = gmd;
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000BE28C File Offset: 0x000BC48C
		internal MethodOnTypeBuilderInst(MethodInfo method, Type[] typeArguments)
		{
			this.instantiation = method.DeclaringType;
			this.base_method = MethodOnTypeBuilderInst.ExtractBaseMethod(method);
			this.method_arguments = new Type[typeArguments.Length];
			typeArguments.CopyTo(this.method_arguments, 0);
			if (this.base_method != method)
			{
				this.generic_method_definition = method;
			}
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000BE2E8 File Offset: 0x000BC4E8
		private static MethodInfo ExtractBaseMethod(MethodInfo info)
		{
			if (info is MethodBuilder)
			{
				return info;
			}
			if (info is MethodOnTypeBuilderInst)
			{
				return ((MethodOnTypeBuilderInst)info).base_method;
			}
			if (info.IsGenericMethod)
			{
				info = info.GetGenericMethodDefinition();
			}
			Type declaringType = info.DeclaringType;
			if (!declaringType.IsGenericType || declaringType.IsGenericTypeDefinition)
			{
				return info;
			}
			return (MethodInfo)declaringType.Module.ResolveMethod(info.MetadataToken);
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000BE354 File Offset: 0x000BC554
		internal MethodInfo RuntimeResolve()
		{
			MethodInfo methodInfo = this.instantiation.InternalResolve().GetMethod(this.base_method);
			if (this.method_arguments != null)
			{
				Type[] array = new Type[this.method_arguments.Length];
				for (int i = 0; i < this.method_arguments.Length; i++)
				{
					array[i] = this.method_arguments[i].InternalResolve();
				}
				methodInfo = methodInfo.MakeGenericMethod(array);
			}
			return methodInfo;
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x000BE3BA File Offset: 0x000BC5BA
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060032CB RID: 13003 RVA: 0x000BE3C2 File Offset: 0x000BC5C2
		public override string Name
		{
			get
			{
				return this.base_method.Name;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x000BE3BA File Offset: 0x000BC5BA
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060032CD RID: 13005 RVA: 0x000BE3CF File Offset: 0x000BC5CF
		public override Type ReturnType
		{
			get
			{
				return this.base_method.ReturnType;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x000BE3DC File Offset: 0x000BC5DC
		public override Module Module
		{
			get
			{
				return this.base_method.Module;
			}
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000339FF File Offset: 0x00031BFF
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000339FF File Offset: 0x00031BFF
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000339FF File Offset: 0x00031BFF
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000BE3EC File Offset: 0x000BC5EC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.ReturnType.ToString());
			stringBuilder.Append(" ");
			stringBuilder.Append(this.base_method.Name);
			stringBuilder.Append("(");
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000BE444 File Offset: 0x000BC644
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.base_method.GetMethodImplementationFlags();
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000B3EC6 File Offset: 0x000B20C6
		public override ParameterInfo[] GetParameters()
		{
			return this.GetParametersInternal();
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000339FF File Offset: 0x00031BFF
		internal override ParameterInfo[] GetParametersInternal()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060032D6 RID: 13014 RVA: 0x000B9E26 File Offset: 0x000B8026
		public override int MetadataToken
		{
			get
			{
				return base.MetadataToken;
			}
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000BE451 File Offset: 0x000BC651
		internal override int GetParametersCount()
		{
			return this.base_method.GetParametersCount();
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000339FF File Offset: 0x00031BFF
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x000339FF File Offset: 0x00031BFF
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x000BE45E File Offset: 0x000BC65E
		public override MethodAttributes Attributes
		{
			get
			{
				return this.base_method.Attributes;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060032DB RID: 13019 RVA: 0x000BE46B File Offset: 0x000BC66B
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.base_method.CallingConvention;
			}
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000BE478 File Offset: 0x000BC678
		public override MethodInfo MakeGenericMethod(params Type[] methodInstantiation)
		{
			if (!this.base_method.IsGenericMethodDefinition || this.method_arguments != null)
			{
				throw new InvalidOperationException("Method is not a generic method definition");
			}
			if (methodInstantiation == null)
			{
				throw new ArgumentNullException("methodInstantiation");
			}
			if (this.base_method.GetGenericArguments().Length != methodInstantiation.Length)
			{
				throw new ArgumentException("Incorrect length", "methodInstantiation");
			}
			for (int i = 0; i < methodInstantiation.Length; i++)
			{
				if (methodInstantiation[i] == null)
				{
					throw new ArgumentNullException("methodInstantiation");
				}
			}
			return new MethodOnTypeBuilderInst(this, methodInstantiation);
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000BE504 File Offset: 0x000BC704
		public override Type[] GetGenericArguments()
		{
			if (!this.base_method.IsGenericMethodDefinition)
			{
				return null;
			}
			Type[] array = this.method_arguments ?? this.base_method.GetGenericArguments();
			Type[] array2 = new Type[array.Length];
			array.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000BE546 File Offset: 0x000BC746
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.generic_method_definition ?? this.base_method;
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x000BE558 File Offset: 0x000BC758
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.base_method.ContainsGenericParameters)
				{
					return true;
				}
				if (!this.base_method.IsGenericMethodDefinition)
				{
					throw new NotSupportedException();
				}
				if (this.method_arguments == null)
				{
					return true;
				}
				Type[] array = this.method_arguments;
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

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060032E0 RID: 13024 RVA: 0x000BE5B3 File Offset: 0x000BC7B3
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.base_method.IsGenericMethodDefinition && this.method_arguments == null;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060032E1 RID: 13025 RVA: 0x000BE5CD File Offset: 0x000BC7CD
		public override bool IsGenericMethod
		{
			get
			{
				return this.base_method.IsGenericMethodDefinition;
			}
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000339FF File Offset: 0x00031BFF
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060032E3 RID: 13027 RVA: 0x000339FF File Offset: 0x00031BFF
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040019C3 RID: 6595
		private Type instantiation;

		// Token: 0x040019C4 RID: 6596
		private MethodInfo base_method;

		// Token: 0x040019C5 RID: 6597
		private Type[] method_arguments;

		// Token: 0x040019C6 RID: 6598
		private MethodInfo generic_method_definition;
	}
}
