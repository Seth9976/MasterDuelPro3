using System;

namespace System.Xml
{
	/// <summary>Provides an interface to enable a class to return line and position information.</summary>
	// Token: 0x02000101 RID: 257
	public interface IXmlLineInfo
	{
		/// <summary>Gets a value indicating whether the class can return line information.</summary>
		/// <returns>true if <see cref="P:System.Xml.IXmlLineInfo.LineNumber" /> and <see cref="P:System.Xml.IXmlLineInfo.LinePosition" /> can be provided; otherwise, false.</returns>
		// Token: 0x06000D85 RID: 3461
		bool HasLineInfo();

		/// <summary>Gets the current line number.</summary>
		/// <returns>The current line number or 0 if no line information is available (for example, <see cref="M:System.Xml.IXmlLineInfo.HasLineInfo" /> returns false).</returns>
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000D86 RID: 3462
		int LineNumber { get; }

		/// <summary>Gets the current line position.</summary>
		/// <returns>The current line position or 0 if no line information is available (for example, <see cref="M:System.Xml.IXmlLineInfo.HasLineInfo" /> returns false).</returns>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000D87 RID: 3463
		int LinePosition { get; }
	}
}
