using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000040 RID: 64
	public interface IDataSourceProvider
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001F8 RID: 504
		object dataSource { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001F9 RID: 505
		PropertyPath dataSourcePath { get; }
	}
}
