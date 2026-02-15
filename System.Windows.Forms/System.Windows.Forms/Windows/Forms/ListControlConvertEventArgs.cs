using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListControl.Format" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000105 RID: 261
	public class ListControlConvertEventArgs : ConvertEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListControlConvertEventArgs" /> class with the specified object, type, and list item.</summary>
		/// <param name="value">The value displayed in the <see cref="T:System.Windows.Forms.ListControl" />.</param>
		/// <param name="desiredType">The <see cref="T:System.Type" /> for the displayed item.</param>
		/// <param name="listItem">The data source item to be displayed in the <see cref="T:System.Windows.Forms.ListControl" />.</param>
		// Token: 0x0600094E RID: 2382 RVA: 0x00027495 File Offset: 0x00025695
		public ListControlConvertEventArgs(object value, Type desiredType, object listItem)
			: base(value, desiredType)
		{
			this.list_item = listItem;
		}

		// Token: 0x04000694 RID: 1684
		private object list_item;
	}
}
