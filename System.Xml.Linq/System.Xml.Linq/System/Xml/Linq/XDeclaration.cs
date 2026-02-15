using System;
using System.Text;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML declaration.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200000C RID: 12
	public class XDeclaration
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDeclaration" /> class with the specified version, encoding, and standalone status.</summary>
		/// <param name="version">The version of the XML, usually "1.0".</param>
		/// <param name="encoding">The encoding for the XML document.</param>
		/// <param name="standalone">A string containing "yes" or "no" that specifies whether the XML is standalone or requires external entities to be resolved.</param>
		// Token: 0x06000041 RID: 65 RVA: 0x0000351F File Offset: 0x0000171F
		public XDeclaration(string version, string encoding, string standalone)
		{
			this._version = version;
			this._encoding = encoding;
			this._standalone = standalone;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDeclaration" /> class from another <see cref="T:System.Xml.Linq.XDeclaration" /> object. </summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XDeclaration" /> used to initialize this <see cref="T:System.Xml.Linq.XDeclaration" /> object.</param>
		// Token: 0x06000042 RID: 66 RVA: 0x0000353C File Offset: 0x0000173C
		public XDeclaration(XDeclaration other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this._version = other._version;
			this._encoding = other._encoding;
			this._standalone = other._standalone;
		}

		/// <summary>Gets or sets the encoding for this document.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the code page name for this document.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003576 File Offset: 0x00001776
		// (set) Token: 0x06000044 RID: 68 RVA: 0x0000357E File Offset: 0x0000177E
		public string Encoding
		{
			get
			{
				return this._encoding;
			}
			set
			{
				this._encoding = value;
			}
		}

		/// <summary>Gets or sets the standalone property for this document.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the standalone property for this document.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003587 File Offset: 0x00001787
		// (set) Token: 0x06000046 RID: 70 RVA: 0x0000358F File Offset: 0x0000178F
		public string Standalone
		{
			get
			{
				return this._standalone;
			}
			set
			{
				this._standalone = value;
			}
		}

		/// <summary>Gets or sets the version property for this document.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the version property for this document.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003598 File Offset: 0x00001798
		public string Version
		{
			get
			{
				return this._version;
			}
		}

		/// <summary>Provides the declaration as a formatted string.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the formatted XML string.</returns>
		// Token: 0x06000048 RID: 72 RVA: 0x000035A0 File Offset: 0x000017A0
		public override string ToString()
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			stringBuilder.Append("<?xml");
			if (this._version != null)
			{
				stringBuilder.Append(" version=\"");
				stringBuilder.Append(this._version);
				stringBuilder.Append('"');
			}
			if (this._encoding != null)
			{
				stringBuilder.Append(" encoding=\"");
				stringBuilder.Append(this._encoding);
				stringBuilder.Append('"');
			}
			if (this._standalone != null)
			{
				stringBuilder.Append(" standalone=\"");
				stringBuilder.Append(this._standalone);
				stringBuilder.Append('"');
			}
			stringBuilder.Append("?>");
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x04000013 RID: 19
		private string _version;

		// Token: 0x04000014 RID: 20
		private string _encoding;

		// Token: 0x04000015 RID: 21
		private string _standalone;
	}
}
