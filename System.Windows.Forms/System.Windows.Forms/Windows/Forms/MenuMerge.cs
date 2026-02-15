using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the behavior of a <see cref="T:System.Windows.Forms.MenuItem" /> when it is merged with items in another menu.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000138 RID: 312
	public enum MenuMerge
	{
		/// <summary>The <see cref="T:System.Windows.Forms.MenuItem" /> is added to the collection of existing <see cref="T:System.Windows.Forms.MenuItem" /> objects in a merged menu.</summary>
		// Token: 0x040007F0 RID: 2032
		Add,
		/// <summary>The <see cref="T:System.Windows.Forms.MenuItem" /> replaces an existing <see cref="T:System.Windows.Forms.MenuItem" /> at the same position in a merged menu.</summary>
		// Token: 0x040007F1 RID: 2033
		Replace,
		/// <summary>All submenu items of this <see cref="T:System.Windows.Forms.MenuItem" /> are merged with those of existing <see cref="T:System.Windows.Forms.MenuItem" /> objects at the same position in a merged menu.</summary>
		// Token: 0x040007F2 RID: 2034
		MergeItems,
		/// <summary>The <see cref="T:System.Windows.Forms.MenuItem" /> is not included in a merged menu.</summary>
		// Token: 0x040007F3 RID: 2035
		Remove
	}
}
