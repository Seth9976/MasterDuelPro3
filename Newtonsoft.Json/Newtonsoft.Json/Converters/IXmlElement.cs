using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001C7 RID: 455
	[NullableContext(1)]
	internal interface IXmlElement : IXmlNode
	{
		// Token: 0x06000F45 RID: 3909
		void SetAttributeNode(IXmlNode attribute);

		// Token: 0x06000F46 RID: 3910
		[return: Nullable(2)]
		string GetPrefixOfNamespace(string namespaceUri);

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000F47 RID: 3911
		bool IsEmpty { get; }
	}
}
