using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000631 RID: 1585
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoArrayMethod : MethodInfo
	{
		// Token: 0x06002E8E RID: 11918 RVA: 0x000B3E87 File Offset: 0x000B2087
		internal MonoArrayMethod(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			this.name = methodName;
			this.parent = arrayClass;
			this.ret = returnType;
			this.parameters = (Type[])parameterTypes.Clone();
			this.call_conv = callingConvention;
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x00002645 File Offset: 0x00000845
		[MonoTODO("Always returns this")]
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06002E90 RID: 11920 RVA: 0x000B3EBE File Offset: 0x000B20BE
		public override Type ReturnType
		{
			get
			{
				return this.ret;
			}
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x00033991 File Offset: 0x00031B91
		[MonoTODO("Not implemented.  Always returns zero")]
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MethodImplAttributes.IL;
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000B3EC6 File Offset: 0x000B20C6
		[MonoTODO("Not implemented.  Always returns an empty array")]
		public override ParameterInfo[] GetParameters()
		{
			return this.GetParametersInternal();
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000B3ECE File Offset: 0x000B20CE
		internal override ParameterInfo[] GetParametersInternal()
		{
			return EmptyArray<ParameterInfo>.Value;
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x00033991 File Offset: 0x00031B91
		[MonoTODO("Not implemented.  Always returns 0")]
		internal override int GetParametersCount()
		{
			return 0;
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO("Not implemented")]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x000B3ED5 File Offset: 0x000B20D5
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.mhandle;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06002E97 RID: 11927 RVA: 0x00033991 File Offset: 0x00031B91
		[MonoTODO("Not implemented.  Always returns zero")]
		public override MethodAttributes Attributes
		{
			get
			{
				return MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06002E98 RID: 11928 RVA: 0x000B3EDD File Offset: 0x000B20DD
		public override Type ReflectedType
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06002E99 RID: 11929 RVA: 0x000B3EDD File Offset: 0x000B20DD
		public override Type DeclaringType
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06002E9A RID: 11930 RVA: 0x000B3EE5 File Offset: 0x000B20E5
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x0003DC2A File Offset: 0x0003BE2A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000B3EED File Offset: 0x000B20ED
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000B3EF6 File Offset: 0x000B20F6
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000B3F00 File Offset: 0x000B2100
		public override string ToString()
		{
			string text = string.Empty;
			ParameterInfo[] array = this.GetParameters();
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					text += ", ";
				}
				text += array[i].ParameterType.Name;
			}
			if (this.ReturnType != null)
			{
				return string.Concat(new string[]
				{
					this.ReturnType.Name,
					" ",
					this.Name,
					"(",
					text,
					")"
				});
			}
			return string.Concat(new string[] { "void ", this.Name, "(", text, ")" });
		}

		// Token: 0x04001821 RID: 6177
		internal RuntimeMethodHandle mhandle;

		// Token: 0x04001822 RID: 6178
		internal Type parent;

		// Token: 0x04001823 RID: 6179
		internal Type ret;

		// Token: 0x04001824 RID: 6180
		internal Type[] parameters;

		// Token: 0x04001825 RID: 6181
		internal string name;

		// Token: 0x04001826 RID: 6182
		internal int table_idx;

		// Token: 0x04001827 RID: 6183
		internal CallingConventions call_conv;
	}
}
