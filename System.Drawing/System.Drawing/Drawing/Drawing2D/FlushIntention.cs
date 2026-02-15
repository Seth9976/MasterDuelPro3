using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies whether commands in the graphics stack are terminated (flushed) immediately or executed as soon as possible.</summary>
	// Token: 0x02000097 RID: 151
	public enum FlushIntention
	{
		/// <summary>Specifies that the stack of all graphics operations is flushed immediately.</summary>
		// Token: 0x040002DE RID: 734
		Flush,
		/// <summary>Specifies that all graphics operations on the stack are executed as soon as possible. This synchronizes the graphics state.</summary>
		// Token: 0x040002DF RID: 735
		Sync
	}
}
