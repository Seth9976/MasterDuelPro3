using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000098 RID: 152
	public interface IIndexable<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600077D RID: 1917
		// (set) Token: 0x0600077E RID: 1918
		int Length { get; set; }

		// Token: 0x0600077F RID: 1919
		ref T ElementAt(int index);
	}
}
