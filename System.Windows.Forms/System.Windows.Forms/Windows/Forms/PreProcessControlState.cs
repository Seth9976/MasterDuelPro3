using System;

namespace System.Windows.Forms
{
	/// <summary>Provides options that specify the relationship between the control and preprocessing messages.</summary>
	// Token: 0x02000165 RID: 357
	public enum PreProcessControlState
	{
		/// <summary>Specifies that the message has been processed and no further processing is required.</summary>
		// Token: 0x04000898 RID: 2200
		MessageProcessed,
		/// <summary>Specifies that the control requires the message and that processing should continue.</summary>
		// Token: 0x04000899 RID: 2201
		MessageNeeded,
		/// <summary>Specifies that the control does not require the message.</summary>
		// Token: 0x0400089A RID: 2202
		MessageNotNeeded
	}
}
