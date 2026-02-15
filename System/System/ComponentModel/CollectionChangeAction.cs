using System;

namespace System.ComponentModel
{
	/// <summary>Specifies how the collection is changed.</summary>
	// Token: 0x0200025F RID: 607
	public enum CollectionChangeAction
	{
		/// <summary>Specifies that an element was added to the collection.</summary>
		// Token: 0x040009CB RID: 2507
		Add = 1,
		/// <summary>Specifies that an element was removed from the collection.</summary>
		// Token: 0x040009CC RID: 2508
		Remove,
		/// <summary>Specifies that the entire collection has changed. This is caused by using methods that manipulate the entire collection, such as <see cref="M:System.Collections.CollectionBase.Clear" />.</summary>
		// Token: 0x040009CD RID: 2509
		Refresh
	}
}
