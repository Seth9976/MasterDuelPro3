using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001C1 RID: 449
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlDeclarationWrapper : XmlNodeWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x06000F15 RID: 3861 RVA: 0x0004270A File Offset: 0x0004090A
		[NullableContext(1)]
		public XmlDeclarationWrapper(XmlDeclaration declaration)
			: base(declaration)
		{
			this._declaration = declaration;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0004271A File Offset: 0x0004091A
		public string Version
		{
			get
			{
				return this._declaration.Version;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00042727 File Offset: 0x00040927
		// (set) Token: 0x06000F18 RID: 3864 RVA: 0x00042734 File Offset: 0x00040934
		public string Encoding
		{
			get
			{
				return this._declaration.Encoding;
			}
			set
			{
				this._declaration.Encoding = value;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00042742 File Offset: 0x00040942
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x0004274F File Offset: 0x0004094F
		public string Standalone
		{
			get
			{
				return this._declaration.Standalone;
			}
			set
			{
				this._declaration.Standalone = value;
			}
		}

		// Token: 0x04000814 RID: 2068
		[Nullable(1)]
		private readonly XmlDeclaration _declaration;
	}
}
