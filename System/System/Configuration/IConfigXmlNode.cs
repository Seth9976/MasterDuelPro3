using System;

namespace System.Configuration
{
	// Token: 0x02000120 RID: 288
	internal interface IConfigXmlNode
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000589 RID: 1417
		string Filename { get; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600058A RID: 1418
		int LineNumber { get; }
	}
}
