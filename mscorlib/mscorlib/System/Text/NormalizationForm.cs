using System;

namespace System.Text
{
	/// <summary>Defines the type of normalization to perform.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002F5 RID: 757
	public enum NormalizationForm
	{
		/// <summary>Indicates that a Unicode string is normalized using full canonical decomposition, followed by the replacement of sequences with their primary composites, if possible.</summary>
		// Token: 0x04000C94 RID: 3220
		FormC = 1,
		/// <summary>Indicates that a Unicode string is normalized using full canonical decomposition.</summary>
		// Token: 0x04000C95 RID: 3221
		FormD,
		/// <summary>Indicates that a Unicode string is normalized using full compatibility decomposition, followed by the replacement of sequences with their primary composites, if possible.</summary>
		// Token: 0x04000C96 RID: 3222
		FormKC = 5,
		/// <summary>Indicates that a Unicode string is normalized using full compatibility decomposition.</summary>
		// Token: 0x04000C97 RID: 3223
		FormKD
	}
}
