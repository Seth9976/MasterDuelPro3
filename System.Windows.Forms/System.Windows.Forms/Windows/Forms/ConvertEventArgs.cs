using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Binding.Format" /> and <see cref="E:System.Windows.Forms.Binding.Parse" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005A RID: 90
	public class ConvertEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ConvertEventArgs" /> class.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that contains the value of the current property. </param>
		/// <param name="desiredType">The <see cref="T:System.Type" /> of the value. </param>
		// Token: 0x06000453 RID: 1107 RVA: 0x00010FD6 File Offset: 0x0000F1D6
		public ConvertEventArgs(object value, Type desiredType)
		{
			this.object_value = value;
			this.desired_type = desiredType;
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Windows.Forms.ConvertEventArgs" />.</summary>
		/// <returns>The value of the <see cref="T:System.Windows.Forms.ConvertEventArgs" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00010FEC File Offset: 0x0000F1EC
		public object Value
		{
			get
			{
				return this.object_value;
			}
		}

		// Token: 0x04000244 RID: 580
		private object object_value;

		// Token: 0x04000245 RID: 581
		private Type desired_type;
	}
}
