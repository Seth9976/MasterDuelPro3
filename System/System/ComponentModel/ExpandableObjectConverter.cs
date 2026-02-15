using System;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert expandable objects to and from various other representations.</summary>
	// Token: 0x02000277 RID: 631
	public class ExpandableObjectConverter : TypeConverter
	{
		/// <summary>Gets a collection of properties for the type of object specified by the value parameter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for the component, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of object to get the properties for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that will be used as a filter. </param>
		// Token: 0x06000F08 RID: 3848 RVA: 0x00041B90 File Offset: 0x0003FD90
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(value, attributes);
		}

		/// <summary>Gets a value indicating whether this object supports properties using the specified context.</summary>
		/// <returns>true because <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000F09 RID: 3849 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
