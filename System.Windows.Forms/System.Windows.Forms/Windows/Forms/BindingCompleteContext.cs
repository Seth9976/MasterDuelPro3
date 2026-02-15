using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the direction of the binding operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200001A RID: 26
	public enum BindingCompleteContext
	{
		/// <summary>An indication that the control property value is being updated from the data source.</summary>
		// Token: 0x040000B0 RID: 176
		ControlUpdate,
		/// <summary>An indication that the data source value is being updated from the control property.</summary>
		// Token: 0x040000B1 RID: 177
		DataSourceUpdate
	}
}
