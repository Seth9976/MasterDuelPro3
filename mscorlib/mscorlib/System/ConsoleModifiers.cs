using System;

namespace System
{
	/// <summary>Represents the SHIFT, ALT, and CTRL modifier keys on a keyboard.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000176 RID: 374
	[Flags]
	public enum ConsoleModifiers
	{
		/// <summary>The left or right ALT modifier key.</summary>
		// Token: 0x04000593 RID: 1427
		Alt = 1,
		/// <summary>The left or right SHIFT modifier key.</summary>
		// Token: 0x04000594 RID: 1428
		Shift = 2,
		/// <summary>The left or right CTRL modifier key.</summary>
		// Token: 0x04000595 RID: 1429
		Control = 4
	}
}
