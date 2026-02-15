using System;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert components to and from various other representations.</summary>
	// Token: 0x020002B7 RID: 695
	public class ComponentConverter : ReferenceConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComponentConverter" /> class.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to associate with this component converter. </param>
		// Token: 0x06001076 RID: 4214 RVA: 0x00044A33 File Offset: 0x00042C33
		public ComponentConverter(Type type)
			: base(type)
		{
		}

		/// <summary>Gets a collection of properties for the type of component specified by the value parameter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for the component, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of component to get the properties for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that will be used as a filter. </param>
		// Token: 0x06001077 RID: 4215 RVA: 0x00041B90 File Offset: 0x0003FD90
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(value, attributes);
		}

		/// <summary>Gets a value indicating whether this object supports properties using the specified context.</summary>
		/// <returns>true because <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06001078 RID: 4216 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
