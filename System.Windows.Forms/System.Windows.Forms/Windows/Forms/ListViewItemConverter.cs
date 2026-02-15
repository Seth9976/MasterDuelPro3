using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.ListViewItem" /> objects to and from various other representations.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200011E RID: 286
	public class ListViewItemConverter : ExpandableObjectConverter
	{
		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B71 RID: 2929 RVA: 0x0001BC4F File Offset: 0x00019E4F
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given object to another type.</summary>
		/// <returns>The converted object.</returns>
		/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <param name="culture">An optional culture info. If not supplied the current culture is assumed. </param>
		/// <param name="value">The object to convert. </param>
		/// <param name="destinationType">The type to convert the object to. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B72 RID: 2930 RVA: 0x00030EE7 File Offset: 0x0002F0E7
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return value.ToString();
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
