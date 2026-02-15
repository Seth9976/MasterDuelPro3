using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000099 RID: 153
	public interface INativeList<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : IIndexable<T> where T : struct, ValueType
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000780 RID: 1920
		// (set) Token: 0x06000781 RID: 1921
		int Capacity { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000782 RID: 1922
		bool IsEmpty { get; }

		// Token: 0x170000CF RID: 207
		T this[int index] { get; set; }

		// Token: 0x06000785 RID: 1925
		void Clear();
	}
}
