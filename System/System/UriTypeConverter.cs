using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System
{
	/// <summary>Converts a <see cref="T:System.String" /> type to a <see cref="T:System.Uri" /> type, and vice versa.</summary>
	// Token: 0x0200010F RID: 271
	public class UriTypeConverter : TypeConverter
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x0001C2F9 File Offset: 0x0001A4F9
		private bool CanConvert(Type type)
		{
			return type == typeof(string) || type == typeof(Uri) || type == typeof(InstanceDescriptor);
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter.</summary>
		/// <returns>true if <paramref name="sourceType" /> is a <see cref="T:System.String" /> type or a <see cref="T:System.Uri" /> type can be assigned from <paramref name="sourceType" />; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type that you want to convert from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceType" /> parameter is null.</exception>
		// Token: 0x0600055A RID: 1370 RVA: 0x0001C333 File Offset: 0x0001A533
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == null)
			{
				throw new ArgumentNullException("sourceType");
			}
			return this.CanConvert(sourceType);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <returns>true if <paramref name="destinationType" /> is of type <see cref="T:System.ComponentModel.Design.Serialization.InstanceDescriptor" />, <see cref="T:System.String" />, or <see cref="T:System.Uri" />; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type that you want to convert to.</param>
		// Token: 0x0600055B RID: 1371 RVA: 0x0001C350 File Offset: 0x0001A550
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return !(destinationType == null) && this.CanConvert(destinationType);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x0600055C RID: 1372 RVA: 0x0001C364 File Offset: 0x0001A564
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.CanConvertFrom(context, value.GetType()))
			{
				throw new NotSupportedException(global::Locale.GetText("Cannot convert from value."));
			}
			if (value is Uri)
			{
				return value;
			}
			string text = value as string;
			if (text != null)
			{
				return new Uri(text, UriKind.RelativeOrAbsolute);
			}
			InstanceDescriptor instanceDescriptor = value as InstanceDescriptor;
			if (instanceDescriptor != null)
			{
				return instanceDescriptor.Invoke();
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts a given value object to the specified type, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x0600055D RID: 1373 RVA: 0x0001C3D4 File Offset: 0x0001A5D4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!this.CanConvertTo(context, destinationType))
			{
				throw new NotSupportedException(global::Locale.GetText("Cannot convert to destination type."));
			}
			Uri uri = value as Uri;
			if (uri != null)
			{
				if (destinationType == typeof(string))
				{
					return uri.ToString();
				}
				if (destinationType == typeof(Uri))
				{
					return uri;
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					return new InstanceDescriptor(typeof(Uri).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(UriKind)
					}), new object[]
					{
						uri.ToString(),
						uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Returns whether the given value object is a <see cref="T:System.Uri" /> or a <see cref="T:System.Uri" /> can be created from it.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Uri" /> or a <see cref="T:System.String" /> from which a <see cref="T:System.Uri" /> can be created; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that provides a format context.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to test for validity.</param>
		// Token: 0x0600055E RID: 1374 RVA: 0x0001C4B4 File Offset: 0x0001A6B4
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			return value != null && (value is string || value is Uri);
		}
	}
}
