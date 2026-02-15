using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.Padding" /> values to and from various other representations.</summary>
	// Token: 0x0200015D RID: 349
	public class PaddingConverter : TypeConverter
	{
		/// <summary>Returns whether this converter can convert an object of one type to the type of this converter.</summary>
		/// <returns>true if this object can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from.</param>
		// Token: 0x06000D85 RID: 3461 RVA: 0x00022145 File Offset: 0x00020345
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="destinationType">A T:System.Type that represents the type you want to convert to. </param>
		// Token: 0x06000D86 RID: 3462 RVA: 0x0003B29E File Offset: 0x0003949E
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor);
		}

		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		// Token: 0x06000D87 RID: 3463 RVA: 0x0003B2CC File Offset: 0x000394CC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || !(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string[] array = ((string)value).Split(culture.TextInfo.ListSeparator.ToCharArray());
			return new Padding(int.Parse(array[0].Trim()), int.Parse(array[1].Trim()), int.Parse(array[2].Trim()), int.Parse(array[3].Trim()));
		}

		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
		// Token: 0x06000D88 RID: 3464 RVA: 0x0003B354 File Offset: 0x00039554
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is Padding)
			{
				Padding padding = (Padding)value;
				if (destinationType == typeof(string))
				{
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					return string.Format("{0}{4} {1}{4} {2}{4} {3}", new object[]
					{
						padding.Left,
						padding.Top,
						padding.Right,
						padding.Bottom,
						culture.TextInfo.ListSeparator
					});
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					Type[] array;
					object[] array2;
					if (padding.All != -1)
					{
						array = new Type[] { typeof(int) };
						array2 = new object[] { padding.All };
					}
					else
					{
						array = new Type[]
						{
							typeof(int),
							typeof(int),
							typeof(int),
							typeof(int)
						};
						array2 = new object[] { padding.Left, padding.Top, padding.Right, padding.Bottom };
					}
					return new InstanceDescriptor(typeof(Padding).GetConstructor(array), array2);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Creates an instance of the type that this <see cref="T:System.ComponentModel.TypeConverter" /> is associated with, using the specified context, given a set of property values for the object.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or null if the object cannot be created. This method always returns null.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values. </param>
		// Token: 0x06000D89 RID: 3465 RVA: 0x0003B4D8 File Offset: 0x000396D8
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (((Padding)context.PropertyDescriptor.GetValue(context.Instance)).All == (int)propertyValues["All"])
			{
				return new Padding((int)propertyValues["Left"], (int)propertyValues["Top"], (int)propertyValues["Right"], (int)propertyValues["Bottom"]);
			}
			return new Padding((int)propertyValues["All"]);
		}

		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000D8A RID: 3466 RVA: 0x00006F54 File Offset: 0x00005154
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter. </param>
		// Token: 0x06000D8B RID: 3467 RVA: 0x0003B596 File Offset: 0x00039796
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(Padding), attributes);
		}

		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000D8C RID: 3468 RVA: 0x00006F54 File Offset: 0x00005154
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
