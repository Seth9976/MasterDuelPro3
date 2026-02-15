using System;

namespace System.Windows.Forms
{
	/// <summary>Determines when changes to a data source value get propagated to the corresponding data-bound control property.</summary>
	// Token: 0x02000059 RID: 89
	public enum ControlUpdateMode
	{
		/// <summary>The bound control is updated when the data source value changes, or the data source position changes.</summary>
		// Token: 0x04000242 RID: 578
		OnPropertyChanged,
		/// <summary>The bound control is never updated when a data source value changes. The data source is write-only.</summary>
		// Token: 0x04000243 RID: 579
		Never
	}
}
