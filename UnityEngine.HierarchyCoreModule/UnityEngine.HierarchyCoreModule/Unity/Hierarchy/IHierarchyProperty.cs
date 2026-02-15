using System;

namespace Unity.Hierarchy
{
	// Token: 0x02000015 RID: 21
	public interface IHierarchyProperty<T>
	{
		// Token: 0x0600004B RID: 75
		T GetValue(in HierarchyNode node);

		// Token: 0x0600004C RID: 76
		void SetValue(in HierarchyNode node, T value);
	}
}
