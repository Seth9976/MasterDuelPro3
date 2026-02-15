using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>This class represents the keyref element from XMLSchema as specified by the World Wide Web Consortium (W3C).</summary>
	// Token: 0x020002E8 RID: 744
	public class XmlSchemaKeyref : XmlSchemaIdentityConstraint
	{
		/// <summary>Gets or sets the name of the key that this constraint refers to in another simple or complex type.</summary>
		/// <returns>The QName of the key that this constraint refers to.</returns>
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x000C0473 File Offset: 0x000BE673
		// (set) Token: 0x0600216D RID: 8557 RVA: 0x000C047B File Offset: 0x000BE67B
		[XmlAttribute("refer")]
		public XmlQualifiedName Refer
		{
			get
			{
				return this.refer;
			}
			set
			{
				this.refer = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		// Token: 0x04000F9A RID: 3994
		private XmlQualifiedName refer = XmlQualifiedName.Empty;
	}
}
