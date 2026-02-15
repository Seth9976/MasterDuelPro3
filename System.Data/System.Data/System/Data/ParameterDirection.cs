using System;

namespace System.Data
{
	/// <summary>Specifies the type of a parameter within a query relative to the <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000085 RID: 133
	public enum ParameterDirection
	{
		/// <summary>The parameter is an input parameter.</summary>
		// Token: 0x040002A2 RID: 674
		Input = 1,
		/// <summary>The parameter is an output parameter.</summary>
		// Token: 0x040002A3 RID: 675
		Output,
		/// <summary>The parameter is capable of both input and output.</summary>
		// Token: 0x040002A4 RID: 676
		InputOutput,
		/// <summary>The parameter represents a return value from an operation such as a stored procedure, built-in function, or user-defined function.</summary>
		// Token: 0x040002A5 RID: 677
		ReturnValue = 6
	}
}
