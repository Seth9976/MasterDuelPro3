using System;

namespace System.Collections.Specialized
{
	/// <summary>Describes the action that caused a <see cref="E:System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged" /> event. </summary>
	// Token: 0x02000309 RID: 777
	public enum NotifyCollectionChangedAction
	{
		/// <summary>One or more items were added to the collection.</summary>
		// Token: 0x04000B78 RID: 2936
		Add,
		/// <summary>One or more items were removed from the collection.</summary>
		// Token: 0x04000B79 RID: 2937
		Remove,
		/// <summary>One or more items were replaced in the collection.</summary>
		// Token: 0x04000B7A RID: 2938
		Replace,
		/// <summary>One or more items were moved within the collection.</summary>
		// Token: 0x04000B7B RID: 2939
		Move,
		/// <summary>The content of the collection changed dramatically.</summary>
		// Token: 0x04000B7C RID: 2940
		Reset
	}
}
