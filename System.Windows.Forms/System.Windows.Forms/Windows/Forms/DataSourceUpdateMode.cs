using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies when a data source is updated when changes occur in the bound control.</summary>
	// Token: 0x02000069 RID: 105
	public enum DataSourceUpdateMode
	{
		/// <summary>Data source is updated when the control property is validated, </summary>
		// Token: 0x040002C1 RID: 705
		OnValidation,
		/// <summary>Data source is updated whenever the value of the control property changes. </summary>
		// Token: 0x040002C2 RID: 706
		OnPropertyChanged,
		/// <summary>Data source is never updated and values entered into the control are not parsed, validated or re-formatted.</summary>
		// Token: 0x040002C3 RID: 707
		Never
	}
}
