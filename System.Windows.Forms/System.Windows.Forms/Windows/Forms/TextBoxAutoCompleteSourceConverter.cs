using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000191 RID: 401
	internal class TextBoxAutoCompleteSourceConverter : EnumConverter
	{
		// Token: 0x06000F39 RID: 3897 RVA: 0x00045BEB File Offset: 0x00043DEB
		public TextBoxAutoCompleteSourceConverter(Type type)
			: base(type)
		{
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00045BF4 File Offset: 0x00043DF4
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			TypeConverter.StandardValuesCollection standardValues = base.GetStandardValues(context);
			AutoCompleteSource[] array = new AutoCompleteSource[standardValues.Count];
			standardValues.CopyTo(array, 0);
			return new TypeConverter.StandardValuesCollection(Array.FindAll<AutoCompleteSource>(array, (AutoCompleteSource value) => value != AutoCompleteSource.ListItems));
		}
	}
}
