using System;

namespace System.Xml
{
	// Token: 0x02000032 RID: 50
	internal interface IDtdParserAdapterV1 : IDtdParserAdapterWithValidation, IDtdParserAdapter
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001AF RID: 431
		bool V1CompatibilityMode { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001B0 RID: 432
		bool Normalization { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001B1 RID: 433
		bool Namespaces { get; }
	}
}
