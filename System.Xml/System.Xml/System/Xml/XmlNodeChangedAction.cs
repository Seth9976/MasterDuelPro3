using System;

namespace System.Xml
{
	/// <summary>Specifies the type of node change.</summary>
	// Token: 0x020000E8 RID: 232
	public enum XmlNodeChangedAction
	{
		/// <summary>A node is being inserted in the tree.</summary>
		// Token: 0x04000644 RID: 1604
		Insert,
		/// <summary>A node is being removed from the tree.</summary>
		// Token: 0x04000645 RID: 1605
		Remove,
		/// <summary>A node value is being changed.</summary>
		// Token: 0x04000646 RID: 1606
		Change
	}
}
