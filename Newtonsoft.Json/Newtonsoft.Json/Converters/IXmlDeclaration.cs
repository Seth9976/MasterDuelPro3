using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001C5 RID: 453
	[NullableContext(2)]
	internal interface IXmlDeclaration : IXmlNode
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000F3C RID: 3900
		string Version { get; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000F3D RID: 3901
		// (set) Token: 0x06000F3E RID: 3902
		string Encoding { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000F3F RID: 3903
		// (set) Token: 0x06000F40 RID: 3904
		string Standalone { get; set; }
	}
}
