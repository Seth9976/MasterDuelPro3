using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001C6 RID: 454
	[NullableContext(2)]
	internal interface IXmlDocumentType : IXmlNode
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000F41 RID: 3905
		[Nullable(1)]
		string Name
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000F42 RID: 3906
		string System { get; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000F43 RID: 3907
		string Public { get; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000F44 RID: 3908
		string InternalSubset { get; }
	}
}
