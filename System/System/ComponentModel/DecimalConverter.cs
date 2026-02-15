using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Decimal" /> objects to and from various other representations.</summary>
	// Token: 0x02000269 RID: 617
	public class DecimalConverter : BaseNumberConverter
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x000028AE File Offset: 0x00000AAE
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0004102E File Offset: 0x0003F22E
		internal override Type TargetType
		{
			get
			{
				return typeof(decimal);
			}
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to. </param>
		// Token: 0x06000E9B RID: 3739 RVA: 0x0004103A File Offset: 0x0003F23A
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given value object to a <see cref="T:System.Decimal" /> using the arguments.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">An optional <see cref="T:System.Globalization.CultureInfo" />. If not supplied, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000E9C RID: 3740 RVA: 0x00041058 File Offset: 0x0003F258
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(InstanceDescriptor)) || !(value is decimal))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			object[] array = new object[] { decimal.GetBits((decimal)value) };
			MemberInfo constructor = typeof(decimal).GetConstructor(new Type[] { typeof(int[]) });
			if (constructor != null)
			{
				return new InstanceDescriptor(constructor, array);
			}
			return null;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x000410EB File Offset: 0x0003F2EB
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDecimal(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x000410FD File Offset: 0x0003F2FD
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return decimal.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00041110 File Offset: 0x0003F310
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((decimal)value).ToString("G", formatInfo);
		}
	}
}
