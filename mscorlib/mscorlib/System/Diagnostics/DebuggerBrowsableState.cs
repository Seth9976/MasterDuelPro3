using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Provides display instructions for the debugger.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006D8 RID: 1752
	[ComVisible(true)]
	public enum DebuggerBrowsableState
	{
		/// <summary>Never show the element.</summary>
		// Token: 0x04001DE8 RID: 7656
		Never,
		/// <summary>Show the element as collapsed.</summary>
		// Token: 0x04001DE9 RID: 7657
		Collapsed = 2,
		/// <summary>Do not display the root element; display the child elements if the element is a collection or array of items.</summary>
		// Token: 0x04001DEA RID: 7658
		RootHidden
	}
}
