using System;

namespace System.Windows.Forms
{
	/// <summary>Indicates the result of a completed binding operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200001D RID: 29
	public enum BindingCompleteState
	{
		/// <summary>An indication that the binding operation completed successfully.</summary>
		// Token: 0x040000B8 RID: 184
		Success,
		/// <summary>An indication that the binding operation failed with a data error.</summary>
		// Token: 0x040000B9 RID: 185
		DataError,
		/// <summary>An indication that the binding operation failed with an exception.</summary>
		// Token: 0x040000BA RID: 186
		Exception
	}
}
