using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000665 RID: 1637
	[StructLayout(LayoutKind.Sequential)]
	internal class FieldOnTypeBuilderInst : FieldInfo
	{
		// Token: 0x060031F4 RID: 12788 RVA: 0x000BB384 File Offset: 0x000B9584
		public FieldOnTypeBuilderInst(TypeBuilderInstantiation instantiation, FieldInfo fb)
		{
			this.instantiation = instantiation;
			this.fb = fb;
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060031F5 RID: 12789 RVA: 0x000BB39A File Offset: 0x000B959A
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x000BB3A2 File Offset: 0x000B95A2
		public override string Name
		{
			get
			{
				return this.fb.Name;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060031F7 RID: 12791 RVA: 0x000BB39A File Offset: 0x000B959A
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x000339FF File Offset: 0x00031BFF
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000339FF File Offset: 0x00031BFF
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000339FF File Offset: 0x00031BFF
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000BB3AF File Offset: 0x000B95AF
		public override string ToString()
		{
			return this.fb.FieldType.ToString() + " " + this.Name;
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060031FC RID: 12796 RVA: 0x000BB3D1 File Offset: 0x000B95D1
		public override FieldAttributes Attributes
		{
			get
			{
				return this.fb.Attributes;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x000339FF File Offset: 0x00031BFF
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x000B1DD9 File Offset: 0x000AFFD9
		public override int MetadataToken
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060031FF RID: 12799 RVA: 0x000339FF File Offset: 0x00031BFF
		public override Type FieldType
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x000339FF File Offset: 0x00031BFF
		public override object GetValue(object obj)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x000339FF File Offset: 0x00031BFF
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x000BB3DE File Offset: 0x000B95DE
		internal FieldInfo RuntimeResolve()
		{
			return this.instantiation.RuntimeResolve().GetField(this.fb);
		}

		// Token: 0x04001966 RID: 6502
		internal TypeBuilderInstantiation instantiation;

		// Token: 0x04001967 RID: 6503
		internal FieldInfo fb;
	}
}
