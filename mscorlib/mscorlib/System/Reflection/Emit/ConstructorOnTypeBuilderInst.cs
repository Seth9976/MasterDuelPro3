using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000659 RID: 1625
	[StructLayout(LayoutKind.Sequential)]
	internal class ConstructorOnTypeBuilderInst : ConstructorInfo
	{
		// Token: 0x0600314D RID: 12621 RVA: 0x000B9C58 File Offset: 0x000B7E58
		public ConstructorOnTypeBuilderInst(TypeBuilderInstantiation instantiation, ConstructorInfo cb)
		{
			this.instantiation = instantiation;
			this.cb = cb;
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x000B9C6E File Offset: 0x000B7E6E
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x000B9C76 File Offset: 0x000B7E76
		public override string Name
		{
			get
			{
				return this.cb.Name;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x000B9C6E File Offset: 0x000B7E6E
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000B9C83 File Offset: 0x000B7E83
		public override Module Module
		{
			get
			{
				return this.cb.Module;
			}
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000B9C90 File Offset: 0x000B7E90
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.cb.IsDefined(attributeType, inherit);
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000B9C9F File Offset: 0x000B7E9F
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.cb.GetCustomAttributes(inherit);
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000B9CAD File Offset: 0x000B7EAD
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.cb.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000B9CBC File Offset: 0x000B7EBC
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.cb.GetMethodImplementationFlags();
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000B9CC9 File Offset: 0x000B7EC9
		public override ParameterInfo[] GetParameters()
		{
			if (!this.instantiation.IsCreated)
			{
				throw new NotSupportedException();
			}
			return this.GetParametersInternal();
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000B9CE4 File Offset: 0x000B7EE4
		internal override ParameterInfo[] GetParametersInternal()
		{
			ParameterInfo[] array;
			if (this.cb is ConstructorBuilder)
			{
				ConstructorBuilder constructorBuilder = (ConstructorBuilder)this.cb;
				array = new ParameterInfo[constructorBuilder.parameters.Length];
				for (int i = 0; i < constructorBuilder.parameters.Length; i++)
				{
					Type type = this.instantiation.InflateType(constructorBuilder.parameters[i]);
					ParameterInfo[] array2 = array;
					int num = i;
					ParameterBuilder[] pinfo = constructorBuilder.pinfo;
					array2[num] = RuntimeParameterInfo.New((pinfo != null) ? pinfo[i] : null, type, this, i + 1);
				}
			}
			else
			{
				ParameterInfo[] parameters = this.cb.GetParameters();
				array = new ParameterInfo[parameters.Length];
				for (int j = 0; j < parameters.Length; j++)
				{
					Type type2 = this.instantiation.InflateType(parameters[j].ParameterType);
					array[j] = RuntimeParameterInfo.New(parameters[j], type2, this, j + 1);
				}
			}
			return array;
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x000B9DB4 File Offset: 0x000B7FB4
		internal override Type[] GetParameterTypes()
		{
			if (this.cb is ConstructorBuilder)
			{
				return (this.cb as ConstructorBuilder).parameters;
			}
			ParameterInfo[] parameters = this.cb.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			return array;
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x000B9E0E File Offset: 0x000B800E
		internal ConstructorInfo RuntimeResolve()
		{
			return this.instantiation.InternalResolve().GetConstructor(this.cb);
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x000B9E26 File Offset: 0x000B8026
		public override int MetadataToken
		{
			get
			{
				return base.MetadataToken;
			}
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000B9E2E File Offset: 0x000B802E
		internal override int GetParametersCount()
		{
			return this.cb.GetParametersCount();
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000B9E3B File Offset: 0x000B803B
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.cb.Invoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x000B9E4F File Offset: 0x000B804F
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.cb.MethodHandle;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x000B9E5C File Offset: 0x000B805C
		public override MethodAttributes Attributes
		{
			get
			{
				return this.cb.Attributes;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x000B9E69 File Offset: 0x000B8069
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.cb.CallingConvention;
			}
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000B9E76 File Offset: 0x000B8076
		public override Type[] GetGenericArguments()
		{
			return this.cb.GetGenericArguments();
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x00033991 File Offset: 0x00031B91
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x00033991 File Offset: 0x00031B91
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x00033991 File Offset: 0x00031B91
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000B1DD9 File Offset: 0x000AFFD9
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0400192E RID: 6446
		internal TypeBuilderInstantiation instantiation;

		// Token: 0x0400192F RID: 6447
		internal ConstructorInfo cb;
	}
}
