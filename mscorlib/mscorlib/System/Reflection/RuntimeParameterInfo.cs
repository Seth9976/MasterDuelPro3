using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection
{
	// Token: 0x02000645 RID: 1605
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ParameterInfo))]
	[ComVisible(true)]
	[Serializable]
	internal class RuntimeParameterInfo : ParameterInfo
	{
		// Token: 0x06003060 RID: 12384 RVA: 0x000B725E File Offset: 0x000B545E
		internal RuntimeParameterInfo(string name, Type type, int position, int attrs, object defaultValue, MemberInfo member, MarshalAsAttribute marshalAs)
		{
			this.NameImpl = name;
			this.ClassImpl = type;
			this.PositionImpl = position;
			this.AttrsImpl = (ParameterAttributes)attrs;
			this.DefaultValueImpl = defaultValue;
			this.MemberImpl = member;
			this.marshalAs = marshalAs;
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000B729C File Offset: 0x000B549C
		internal static void FormatParameters(StringBuilder sb, ParameterInfo[] p, CallingConventions callingConvention, bool serialization)
		{
			for (int i = 0; i < p.Length; i++)
			{
				if (i > 0)
				{
					sb.Append(", ");
				}
				Type parameterType = p[i].ParameterType;
				string text = parameterType.FormatTypeName(serialization);
				if (parameterType.IsByRef && !serialization)
				{
					sb.Append(text.TrimEnd(new char[] { '&' }));
					sb.Append(" ByRef");
				}
				else
				{
					sb.Append(text);
				}
			}
			if ((callingConvention & CallingConventions.VarArgs) != (CallingConventions)0)
			{
				if (p.Length != 0)
				{
					sb.Append(", ");
				}
				sb.Append("...");
			}
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000B7330 File Offset: 0x000B5530
		internal RuntimeParameterInfo(ParameterBuilder pb, Type type, MemberInfo member, int position)
		{
			this.ClassImpl = type;
			this.MemberImpl = member;
			if (pb != null)
			{
				this.NameImpl = pb.Name;
				this.PositionImpl = pb.Position - 1;
				this.AttrsImpl = (ParameterAttributes)pb.Attributes;
				return;
			}
			this.NameImpl = null;
			this.PositionImpl = position - 1;
			this.AttrsImpl = ParameterAttributes.None;
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000B7393 File Offset: 0x000B5593
		internal static ParameterInfo New(ParameterBuilder pb, Type type, MemberInfo member, int position)
		{
			return new RuntimeParameterInfo(pb, type, member, position);
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000B73A0 File Offset: 0x000B55A0
		internal RuntimeParameterInfo(ParameterInfo pinfo, Type type, MemberInfo member, int position)
		{
			this.ClassImpl = type;
			this.MemberImpl = member;
			if (pinfo != null)
			{
				this.NameImpl = pinfo.Name;
				this.PositionImpl = pinfo.Position - 1;
				this.AttrsImpl = pinfo.Attributes;
				return;
			}
			this.NameImpl = null;
			this.PositionImpl = position - 1;
			this.AttrsImpl = ParameterAttributes.None;
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000B7404 File Offset: 0x000B5604
		internal RuntimeParameterInfo(ParameterInfo pinfo, MemberInfo member)
		{
			this.ClassImpl = pinfo.ParameterType;
			this.MemberImpl = member;
			this.NameImpl = pinfo.Name;
			this.PositionImpl = pinfo.Position;
			this.AttrsImpl = pinfo.Attributes;
			this.DefaultValueImpl = this.GetDefaultValueImpl(pinfo);
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000B745B File Offset: 0x000B565B
		internal RuntimeParameterInfo(Type type, MemberInfo member, MarshalAsAttribute marshalAs)
		{
			this.ClassImpl = type;
			this.MemberImpl = member;
			this.NameImpl = null;
			this.PositionImpl = -1;
			this.AttrsImpl = ParameterAttributes.Retval;
			this.marshalAs = marshalAs;
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06003067 RID: 12391 RVA: 0x000B7490 File Offset: 0x000B5690
		public override object DefaultValue
		{
			get
			{
				if (this.ClassImpl == typeof(decimal) || this.ClassImpl == typeof(decimal?))
				{
					DecimalConstantAttribute[] array = (DecimalConstantAttribute[])this.GetCustomAttributes(typeof(DecimalConstantAttribute), false);
					if (array.Length != 0)
					{
						return array[0].Value;
					}
				}
				else if (this.ClassImpl == typeof(DateTime) || this.ClassImpl == typeof(DateTime?))
				{
					DateTimeConstantAttribute[] array2 = (DateTimeConstantAttribute[])this.GetCustomAttributes(typeof(DateTimeConstantAttribute), false);
					if (array2.Length != 0)
					{
						return array2[0].Value;
					}
				}
				return this.DefaultValueImpl;
			}
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000B754C File Offset: 0x000B574C
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, false);
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000B7555 File Offset: 0x000B5755
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, false);
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000B755F File Offset: 0x000B575F
		internal object GetDefaultValueImpl(ParameterInfo pinfo)
		{
			return typeof(ParameterInfo).GetField("DefaultValueImpl", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(pinfo);
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x0003DC2A File Offset: 0x0003BE2A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000B7580 File Offset: 0x000B5780
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			if (base.IsIn)
			{
				num++;
			}
			if (base.IsOut)
			{
				num++;
			}
			if (base.IsOptional)
			{
				num++;
			}
			if (this.marshalAs != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if (base.IsIn)
			{
				array[num++] = new InAttribute();
			}
			if (base.IsOut)
			{
				array[num++] = new OutAttribute();
			}
			if (base.IsOptional)
			{
				array[num++] = new OptionalAttribute();
			}
			if (this.marshalAs != null)
			{
				array[num++] = this.marshalAs.Copy();
			}
			return array;
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000B7624 File Offset: 0x000B5824
		internal CustomAttributeData[] GetPseudoCustomAttributesData()
		{
			int num = 0;
			if (base.IsIn)
			{
				num++;
			}
			if (base.IsOut)
			{
				num++;
			}
			if (base.IsOptional)
			{
				num++;
			}
			if (this.marshalAs != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			CustomAttributeData[] array = new CustomAttributeData[num];
			num = 0;
			if (base.IsIn)
			{
				array[num++] = new CustomAttributeData(typeof(InAttribute).GetConstructor(Type.EmptyTypes));
			}
			if (base.IsOut)
			{
				array[num++] = new CustomAttributeData(typeof(OutAttribute).GetConstructor(Type.EmptyTypes));
			}
			if (base.IsOptional)
			{
				array[num++] = new CustomAttributeData(typeof(OptionalAttribute).GetConstructor(Type.EmptyTypes));
			}
			if (this.marshalAs != null)
			{
				CustomAttributeTypedArgument[] array2 = new CustomAttributeTypedArgument[]
				{
					new CustomAttributeTypedArgument(typeof(UnmanagedType), this.marshalAs.Value)
				};
				array[num++] = new CustomAttributeData(typeof(MarshalAsAttribute).GetConstructor(new Type[] { typeof(UnmanagedType) }), array2, EmptyArray<CustomAttributeNamedArgument>.Value);
			}
			return array;
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000B7753 File Offset: 0x000B5953
		internal static ParameterInfo New(ParameterInfo pinfo, Type type, MemberInfo member, int position)
		{
			return new RuntimeParameterInfo(pinfo, type, member, position);
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000B775E File Offset: 0x000B595E
		internal static ParameterInfo New(ParameterInfo pinfo, MemberInfo member)
		{
			return new RuntimeParameterInfo(pinfo, member);
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000B7767 File Offset: 0x000B5967
		internal static ParameterInfo New(Type type, MemberInfo member, MarshalAsAttribute marshalAs)
		{
			return new RuntimeParameterInfo(type, member, marshalAs);
		}

		// Token: 0x04001880 RID: 6272
		internal MarshalAsAttribute marshalAs;
	}
}
