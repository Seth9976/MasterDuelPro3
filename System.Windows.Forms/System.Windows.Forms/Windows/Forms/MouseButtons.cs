using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants that define which mouse button was pressed.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000151 RID: 337
	[Flags]
	[ComVisible(true)]
	public enum MouseButtons
	{
		/// <summary>No mouse button was pressed.</summary>
		// Token: 0x0400085F RID: 2143
		None = 0,
		/// <summary>The left mouse button was pressed.</summary>
		// Token: 0x04000860 RID: 2144
		Left = 1048576,
		/// <summary>The right mouse button was pressed.</summary>
		// Token: 0x04000861 RID: 2145
		Right = 2097152,
		/// <summary>The middle mouse button was pressed.</summary>
		// Token: 0x04000862 RID: 2146
		Middle = 4194304,
		/// <summary>The first XButton was pressed.</summary>
		// Token: 0x04000863 RID: 2147
		XButton1 = 8388608,
		/// <summary>The second XButton was pressed.</summary>
		// Token: 0x04000864 RID: 2148
		XButton2 = 16777216
	}
}
