using System;

namespace System.Xml.Serialization
{
	/// <summary>Provides mappings between code entities in .NET Framework Web service methods and the content of Web Services Description Language (WSDL) messages that are defined for SOAP Web services. </summary>
	// Token: 0x020001BC RID: 444
	public class XmlReflectionMember
	{
		/// <summary>Gets or sets the type of the Web service method member code entity that is represented by this mapping. </summary>
		/// <returns>The <see cref="T:System.Type" /> of the Web service method member code entity that is represented by this mapping.</returns>
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x000691B6 File Offset: 0x000673B6
		// (set) Token: 0x06001550 RID: 5456 RVA: 0x000691BE File Offset: 0x000673BE
		public Type MemberType
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

		/// <summary>Gets or sets an <see cref="T:System.Xml.Serialization.XmlAttributes" /> with the collection of <see cref="T:System.Xml.Serialization.XmlSerializer" />-related attributes that have been applied to the member code entity. </summary>
		/// <returns>An <see cref="T:System.XML.Serialization.XmlAttributes" /> that represents XML attributes that have been applied to the member code.</returns>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x000691C7 File Offset: 0x000673C7
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x000691CF File Offset: 0x000673CF
		public XmlAttributes XmlAttributes
		{
			get
			{
				return this.xmlAttributes;
			}
			set
			{
				this.xmlAttributes = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Xml.Serialization.SoapAttributes" /> with the collection of SOAP-related attributes that have been applied to the member code entity. </summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapAttributes" /> that contains the objects that represent SOAP attributes applied to the member.</returns>
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x000691D8 File Offset: 0x000673D8
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x000691E0 File Offset: 0x000673E0
		public SoapAttributes SoapAttributes
		{
			get
			{
				return this.soapAttributes;
			}
			set
			{
				this.soapAttributes = value;
			}
		}

		/// <summary>Gets or sets the name of the Web service method member for this mapping. </summary>
		/// <returns>The name of the Web service method.</returns>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x000691E9 File Offset: 0x000673E9
		// (set) Token: 0x06001556 RID: 5462 RVA: 0x000691FF File Offset: 0x000673FF
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlReflectionMember" /> represents a Web service method return value, as opposed to an output parameter. </summary>
		/// <returns>true, if the member represents a Web service return value; otherwise, false.</returns>
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x00069208 File Offset: 0x00067408
		// (set) Token: 0x06001558 RID: 5464 RVA: 0x00069210 File Offset: 0x00067410
		public bool IsReturnValue
		{
			get
			{
				return this.isReturnValue;
			}
			set
			{
				this.isReturnValue = value;
			}
		}

		/// <summary>Gets or sets a value that indicates that the value of the corresponding XML element definition's isNullable attribute is false.</summary>
		/// <returns>True to override the <see cref="P:System.Xml.Serialization.XmlElementAttribute.IsNullable" /> property; otherwise, false.</returns>
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x00069219 File Offset: 0x00067419
		// (set) Token: 0x0600155A RID: 5466 RVA: 0x00069221 File Offset: 0x00067421
		public bool OverrideIsNullable
		{
			get
			{
				return this.overrideIsNullable;
			}
			set
			{
				this.overrideIsNullable = value;
			}
		}

		// Token: 0x0400099B RID: 2459
		private string memberName;

		// Token: 0x0400099C RID: 2460
		private Type type;

		// Token: 0x0400099D RID: 2461
		private XmlAttributes xmlAttributes = new XmlAttributes();

		// Token: 0x0400099E RID: 2462
		private SoapAttributes soapAttributes = new SoapAttributes();

		// Token: 0x0400099F RID: 2463
		private bool isReturnValue;

		// Token: 0x040009A0 RID: 2464
		private bool overrideIsNullable;
	}
}
