using System;

namespace System.Xml
{
	// Token: 0x02000031 RID: 49
	internal interface IDtdParserAdapterWithValidation : IDtdParserAdapter
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001AD RID: 429
		bool DtdValidation { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001AE RID: 430
		IValidationEventHandling ValidationEventHandling { get; }
	}
}
