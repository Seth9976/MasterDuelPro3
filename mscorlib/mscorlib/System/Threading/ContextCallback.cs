using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents a method to be called within a new context.  </summary>
	/// <param name="state">An object containing information to be used by the callback method each time it executes.</param>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000254 RID: 596
	// (Invoke) Token: 0x060015C4 RID: 5572
	[ComVisible(true)]
	public delegate void ContextCallback(object state);
}
