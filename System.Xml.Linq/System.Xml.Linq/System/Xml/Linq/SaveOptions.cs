using System;

namespace System.Xml.Linq
{
	/// <summary>Specifies serialization options.</summary>
	// Token: 0x0200001B RID: 27
	[Flags]
	public enum SaveOptions
	{
		/// <summary>Format (indent) the XML while serializing.</summary>
		// Token: 0x04000042 RID: 66
		None = 0,
		/// <summary>Preserve all insignificant white space while serializing.</summary>
		// Token: 0x04000043 RID: 67
		DisableFormatting = 1,
		/// <summary>Remove the duplicate namespace declarations while serializing.</summary>
		// Token: 0x04000044 RID: 68
		OmitDuplicateNamespaces = 2
	}
}
