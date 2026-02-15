using System;

namespace System.CodeDom
{
	/// <summary>Specifies how the code type reference is to be resolved.</summary>
	// Token: 0x020001D6 RID: 470
	[Flags]
	public enum CodeTypeReferenceOptions
	{
		/// <summary>Resolve the type from the root namespace.</summary>
		// Token: 0x04000869 RID: 2153
		GlobalReference = 1,
		/// <summary>Resolve the type from the type parameter.</summary>
		// Token: 0x0400086A RID: 2154
		GenericTypeParameter = 2
	}
}
