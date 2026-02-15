using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001C9 RID: 457
	[NullableContext(2)]
	[Nullable(0)]
	internal class XDeclarationWrapper : XObjectWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x00042A6A File Offset: 0x00040C6A
		[Nullable(1)]
		internal XDeclaration Declaration
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00042A72 File Offset: 0x00040C72
		[NullableContext(1)]
		public XDeclarationWrapper(XDeclaration declaration)
			: base(null)
		{
			this.Declaration = declaration;
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00042A82 File Offset: 0x00040C82
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x00042A86 File Offset: 0x00040C86
		public string Version
		{
			get
			{
				return this.Declaration.Version;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00042A93 File Offset: 0x00040C93
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x00042AA0 File Offset: 0x00040CA0
		public string Encoding
		{
			get
			{
				return this.Declaration.Encoding;
			}
			set
			{
				this.Declaration.Encoding = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00042AAE File Offset: 0x00040CAE
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x00042ABB File Offset: 0x00040CBB
		public string Standalone
		{
			get
			{
				return this.Declaration.Standalone;
			}
			set
			{
				this.Declaration.Standalone = value;
			}
		}
	}
}
