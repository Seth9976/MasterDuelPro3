using System;

namespace System.Xml
{
	// Token: 0x0200002F RID: 47
	internal interface IDtdParser
	{
		// Token: 0x06000192 RID: 402
		IDtdInfo ParseInternalDtd(IDtdParserAdapter adapter, bool saveInternalSubset);

		// Token: 0x06000193 RID: 403
		IDtdInfo ParseFreeFloatingDtd(string baseUri, string docTypeName, string publicId, string systemId, string internalSubset, IDtdParserAdapter adapter);
	}
}
