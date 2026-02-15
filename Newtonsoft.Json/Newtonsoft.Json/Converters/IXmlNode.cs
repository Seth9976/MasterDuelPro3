using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001C8 RID: 456
	[NullableContext(2)]
	internal interface IXmlNode
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000F48 RID: 3912
		XmlNodeType NodeType { get; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000F49 RID: 3913
		string LocalName { get; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000F4A RID: 3914
		[Nullable(1)]
		List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000F4B RID: 3915
		[Nullable(1)]
		List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000F4C RID: 3916
		IXmlNode ParentNode { get; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000F4D RID: 3917
		// (set) Token: 0x06000F4E RID: 3918
		string Value { get; set; }

		// Token: 0x06000F4F RID: 3919
		[NullableContext(1)]
		IXmlNode AppendChild(IXmlNode newChild);

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000F50 RID: 3920
		string NamespaceUri { get; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000F51 RID: 3921
		object WrappedNode { get; }
	}
}
