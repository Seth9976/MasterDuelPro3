using System;

namespace System.Data.ProviderBase
{
	// Token: 0x020000D7 RID: 215
	internal sealed class FieldNameLookup : BasicFieldNameLookup
	{
		// Token: 0x06000B38 RID: 2872 RVA: 0x0003EA9E File Offset: 0x0003CC9E
		public FieldNameLookup(IDataReader reader, int defaultLocaleID)
			: base(reader)
		{
			this._defaultLocaleID = defaultLocaleID;
		}

		// Token: 0x04000490 RID: 1168
		private readonly int _defaultLocaleID;
	}
}
