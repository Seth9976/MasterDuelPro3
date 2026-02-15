using System;
using System.Collections.Generic;

namespace Unity.Properties.Internal
{
	// Token: 0x0200006E RID: 110
	internal interface IAttributes
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000265 RID: 613
		// (set) Token: 0x06000266 RID: 614
		List<Attribute> Attributes { get; set; }

		// Token: 0x06000267 RID: 615
		void AddAttribute(Attribute attribute);

		// Token: 0x06000268 RID: 616
		void AddAttributes(IEnumerable<Attribute> attributes);

		// Token: 0x06000269 RID: 617
		AttributesScope CreateAttributesScope(IAttributes attributes);
	}
}
