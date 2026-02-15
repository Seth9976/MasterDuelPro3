using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how the elements of a control are drawn.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200007B RID: 123
	public enum DrawMode
	{
		/// <summary>All the elements in a control are drawn by the operating system and are of the same size.</summary>
		// Token: 0x04000318 RID: 792
		Normal,
		/// <summary>All the elements in the control are drawn manually and are of the same size.</summary>
		// Token: 0x04000319 RID: 793
		OwnerDrawFixed,
		/// <summary>All the elements in the control are drawn manually and can differ in size.</summary>
		// Token: 0x0400031A RID: 794
		OwnerDrawVariable
	}
}
