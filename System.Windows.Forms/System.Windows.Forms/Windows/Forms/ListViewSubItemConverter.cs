using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x0200011F RID: 287
	internal class ListViewSubItemConverter : ExpandableObjectConverter
	{
		// Token: 0x06000B73 RID: 2931 RVA: 0x00005924 File Offset: 0x00003B24
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00030F10 File Offset: 0x0002F110
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor) && value is ListViewItem.ListViewSubItem)
			{
				ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)value;
				Type[] array = new Type[]
				{
					typeof(ListViewItem),
					typeof(string),
					typeof(Color),
					typeof(Color),
					typeof(Font)
				};
				ConstructorInfo constructor = typeof(ListViewItem.ListViewSubItem).GetConstructor(array);
				if (constructor != null)
				{
					object[] array2 = new object[] { listViewSubItem.Text, listViewSubItem.ForeColor, listViewSubItem.BackColor, listViewSubItem.Font };
					return new InstanceDescriptor(constructor, array2, true);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
