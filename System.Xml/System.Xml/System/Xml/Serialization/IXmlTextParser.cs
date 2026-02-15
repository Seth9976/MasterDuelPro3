using System;

namespace System.Xml.Serialization
{
	/// <summary>Establishes a <see cref="P:System.Xml.Serialization.IXmlTextParser.Normalized" /> property for use by the .NET Framework infrastructure.</summary>
	// Token: 0x02000159 RID: 345
	public interface IXmlTextParser
	{
		/// <summary>Gets or sets whether white space and attribute values are normalized.</summary>
		/// <returns>true if white space attributes values are normalized; otherwise, false.</returns>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060010DD RID: 4317
		// (set) Token: 0x060010DE RID: 4318
		bool Normalized { get; set; }

		/// <summary>Gets or sets how white space is handled when parsing XML.</summary>
		/// <returns>A member of the <see cref="T:System.Xml.WhitespaceHandling" /> enumeration that describes how whites pace is handled when parsing XML.</returns>
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060010DF RID: 4319
		// (set) Token: 0x060010E0 RID: 4320
		WhitespaceHandling WhitespaceHandling { get; set; }
	}
}
