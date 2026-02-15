using System;

namespace System.Xml
{
	/// <summary>Specifies whether to remove duplicate namespace declarations in the <see cref="T:System.Xml.XmlWriter" />. </summary>
	// Token: 0x02000037 RID: 55
	[Flags]
	public enum NamespaceHandling
	{
		/// <summary>Specifies that duplicate namespace declarations will not be removed.</summary>
		// Token: 0x04000125 RID: 293
		Default = 0,
		/// <summary>Specifies that duplicate namespace declarations will be removed. For the duplicate namespace to be removed, the prefix and the namespace must match.</summary>
		// Token: 0x04000126 RID: 294
		OmitDuplicates = 1
	}
}
