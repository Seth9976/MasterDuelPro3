using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace System.Drawing
{
	/// <summary>Converts an <see cref="T:System.Drawing.Icon" /> object from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200004D RID: 77
	public class IconConverter : ExpandableObjectConverter
	{
		/// <summary>Determines whether this <see cref="T:System.Drawing.IconConverter" /> can convert an instance of a specified type to an <see cref="T:System.Drawing.Icon" />, using the specified context.</summary>
		/// <returns>This method returns true if this <see cref="T:System.Drawing.IconConverter" /> can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that specifies the type you want to convert from. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002D1 RID: 721 RVA: 0x0000A7DE File Offset: 0x000089DE
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(byte[]);
		}

		/// <summary>Determines whether this <see cref="T:System.Drawing.IconConverter" /> can convert an <see cref="T:System.Drawing.Icon" /> to an instance of a specified type, using the specified context.</summary>
		/// <returns>This method returns true if this <see cref="T:System.Drawing.IconConverter" /> can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that specifies the type you want to convert to. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002D2 RID: 722 RVA: 0x0000A7F5 File Offset: 0x000089F5
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(byte[]) || destinationType == typeof(string);
		}

		/// <summary>Converts a specified object to an <see cref="T:System.Drawing.Icon" />.</summary>
		/// <returns>If this method succeeds, it returns the <see cref="T:System.Drawing.Icon" /> that it created by converting the specified object. Otherwise, it throws an exception.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that holds information about a specific culture. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to be converted. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion could not be performed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002D3 RID: 723 RVA: 0x0000A820 File Offset: 0x00008A20
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			byte[] array = value as byte[];
			if (array == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			return new Icon(new MemoryStream(array));
		}

		/// <summary>Converts an <see cref="T:System.Drawing.Icon" /> (or an object that can be cast to an <see cref="T:System.Drawing.Icon" />) to a specified type.</summary>
		/// <returns>This method returns the converted object.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that specifies formatting conventions used by a particular culture. </param>
		/// <param name="value">The object to convert. This object should be of type icon or some type that can be cast to <see cref="T:System.Drawing.Icon" />. </param>
		/// <param name="destinationType">The type to convert the icon to. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion could not be performed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002D4 RID: 724 RVA: 0x0000A84C File Offset: 0x00008A4C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is Icon && destinationType == typeof(string))
			{
				return value.ToString();
			}
			if (value == null && destinationType == typeof(string))
			{
				return "(none)";
			}
			if (this.CanConvertTo(null, destinationType))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					((Icon)value).Save(memoryStream);
					return memoryStream.ToArray();
				}
			}
			string text = "IconConverter can not convert from ";
			Type type = value.GetType();
			return new NotSupportedException(text + ((type != null) ? type.ToString() : null));
		}
	}
}
