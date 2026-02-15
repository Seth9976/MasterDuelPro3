using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001C2 RID: 450
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlDocumentTypeWrapper : XmlNodeWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000F1B RID: 3867 RVA: 0x0004275D File Offset: 0x0004095D
		[NullableContext(1)]
		public XmlDocumentTypeWrapper(XmlDocumentType documentType)
			: base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0004276D File Offset: 0x0004096D
		[Nullable(1)]
		public string Name
		{
			[NullableContext(1)]
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0004277A File Offset: 0x0004097A
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x00042787 File Offset: 0x00040987
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x00042794 File Offset: 0x00040994
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x000427A1 File Offset: 0x000409A1
		public override string LocalName
		{
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x04000815 RID: 2069
		[Nullable(1)]
		private readonly XmlDocumentType _documentType;
	}
}
