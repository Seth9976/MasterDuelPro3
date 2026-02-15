using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001CA RID: 458
	[NullableContext(2)]
	[Nullable(0)]
	internal class XDocumentTypeWrapper : XObjectWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000F5A RID: 3930 RVA: 0x00042AC9 File Offset: 0x00040CC9
		[NullableContext(1)]
		public XDocumentTypeWrapper(XDocumentType documentType)
			: base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x00042AD9 File Offset: 0x00040CD9
		[Nullable(1)]
		public string Name
		{
			[NullableContext(1)]
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00042AE6 File Offset: 0x00040CE6
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x00042AF3 File Offset: 0x00040CF3
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00042B00 File Offset: 0x00040D00
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x000427A1 File Offset: 0x000409A1
		public override string LocalName
		{
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x0400081A RID: 2074
		[Nullable(1)]
		private readonly XDocumentType _documentType;
	}
}
