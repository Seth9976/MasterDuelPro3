using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x0200004B RID: 75
	internal class XmlAsyncCheckReaderWithLineInfoNSSchema : XmlAsyncCheckReaderWithLineInfoNS, IXmlSchemaInfo
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000DBC3 File Offset: 0x0000BDC3
		public XmlAsyncCheckReaderWithLineInfoNSSchema(XmlReader reader)
			: base(reader)
		{
			this.readerAsIXmlSchemaInfo = (IXmlSchemaInfo)reader;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		XmlSchemaValidity IXmlSchemaInfo.Validity
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.Validity;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000DBE5 File Offset: 0x0000BDE5
		bool IXmlSchemaInfo.IsDefault
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.IsDefault;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000DBF2 File Offset: 0x0000BDF2
		bool IXmlSchemaInfo.IsNil
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.IsNil;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000DBFF File Offset: 0x0000BDFF
		XmlSchemaSimpleType IXmlSchemaInfo.MemberType
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.MemberType;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		XmlSchemaType IXmlSchemaInfo.SchemaType
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.SchemaType;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000DC19 File Offset: 0x0000BE19
		XmlSchemaElement IXmlSchemaInfo.SchemaElement
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.SchemaElement;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000DC26 File Offset: 0x0000BE26
		XmlSchemaAttribute IXmlSchemaInfo.SchemaAttribute
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.SchemaAttribute;
			}
		}

		// Token: 0x04000177 RID: 375
		private readonly IXmlSchemaInfo readerAsIXmlSchemaInfo;
	}
}
