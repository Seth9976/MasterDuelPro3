using System;

namespace System.Data
{
	// Token: 0x02000072 RID: 114
	internal interface IFilter
	{
		// Token: 0x06000664 RID: 1636
		bool Invoke(DataRow row, DataRowVersion version);
	}
}
