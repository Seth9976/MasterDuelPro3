using System;

namespace System.Data
{
	/// <summary>Specifies how query command results are applied to the row being updated.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000A5 RID: 165
	public enum UpdateRowSource
	{
		/// <summary>Any returned parameters or rows are ignored.</summary>
		// Token: 0x04000336 RID: 822
		None,
		/// <summary>Output parameters are mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x04000337 RID: 823
		OutputParameters,
		/// <summary>The data in the first returned row is mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x04000338 RID: 824
		FirstReturnedRecord,
		/// <summary>Both the output parameters and the first returned row are mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x04000339 RID: 825
		Both
	}
}
