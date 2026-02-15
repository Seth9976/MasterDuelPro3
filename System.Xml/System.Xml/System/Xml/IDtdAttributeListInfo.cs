using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x0200002B RID: 43
	internal interface IDtdAttributeListInfo
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000178 RID: 376
		bool HasNonCDataAttributes { get; }

		// Token: 0x06000179 RID: 377
		IDtdAttributeInfo LookupAttribute(string prefix, string localName);

		// Token: 0x0600017A RID: 378
		IEnumerable<IDtdDefaultAttributeInfo> LookupDefaultAttributes();
	}
}
