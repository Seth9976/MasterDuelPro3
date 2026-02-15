using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers for types of inheritance levels.</summary>
	// Token: 0x0200026F RID: 623
	public enum InheritanceLevel
	{
		/// <summary>The object is inherited.</summary>
		// Token: 0x040009E4 RID: 2532
		Inherited = 1,
		/// <summary>The object is inherited, but has read-only access.</summary>
		// Token: 0x040009E5 RID: 2533
		InheritedReadOnly,
		/// <summary>The object is not inherited.</summary>
		// Token: 0x040009E6 RID: 2534
		NotInherited
	}
}
