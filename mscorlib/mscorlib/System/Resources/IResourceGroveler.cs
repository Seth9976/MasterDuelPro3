using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System.Resources
{
	// Token: 0x020005CE RID: 1486
	internal interface IResourceGroveler
	{
		// Token: 0x06002BE6 RID: 11238
		ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark);
	}
}
