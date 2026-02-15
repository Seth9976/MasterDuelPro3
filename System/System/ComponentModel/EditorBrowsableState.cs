using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the browsable state of a property or method from within an editor.</summary>
	// Token: 0x02000239 RID: 569
	public enum EditorBrowsableState
	{
		/// <summary>The property or method is always browsable from within an editor.</summary>
		// Token: 0x04000973 RID: 2419
		Always,
		/// <summary>The property or method is never browsable from within an editor.</summary>
		// Token: 0x04000974 RID: 2420
		Never,
		/// <summary>The property or method is a feature that only advanced users should see. An editor can either show or hide such properties.</summary>
		// Token: 0x04000975 RID: 2421
		Advanced
	}
}
