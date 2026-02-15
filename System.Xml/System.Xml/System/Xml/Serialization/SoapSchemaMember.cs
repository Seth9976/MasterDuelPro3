using System;

namespace System.Xml.Serialization
{
	/// <summary>Represents certain attributes of a XSD &lt;part&gt; element in a WSDL document for generating classes from the document. </summary>
	// Token: 0x02000193 RID: 403
	public class SoapSchemaMember
	{
		/// <summary>Gets or sets a value that corresponds to the type attribute of the WSDL part element.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> that corresponds to the XML type.</returns>
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0005DF05 File Offset: 0x0005C105
		// (set) Token: 0x06001320 RID: 4896 RVA: 0x0005DF0D File Offset: 0x0005C10D
		public XmlQualifiedName MemberType
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets a value that corresponds to the name attribute of the WSDL part element. </summary>
		/// <returns>The element name.</returns>
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x0005DF16 File Offset: 0x0005C116
		// (set) Token: 0x06001322 RID: 4898 RVA: 0x0005DF2C File Offset: 0x0005C12C
		public string MemberName
		{
			get
			{
				if (this.memberName != null)
				{
					return this.memberName;
				}
				return string.Empty;
			}
			set
			{
				this.memberName = value;
			}
		}

		// Token: 0x040008DF RID: 2271
		private string memberName;

		// Token: 0x040008E0 RID: 2272
		private XmlQualifiedName type = XmlQualifiedName.Empty;
	}
}
