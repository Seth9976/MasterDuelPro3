using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the appearance of a control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000A9 RID: 169
	public enum FlatStyle
	{
		/// <summary>The control appears flat.</summary>
		// Token: 0x04000434 RID: 1076
		Flat,
		/// <summary>A control appears flat until the mouse pointer moves over it, at which point it appears three-dimensional.</summary>
		// Token: 0x04000435 RID: 1077
		Popup,
		/// <summary>The control appears three-dimensional.</summary>
		// Token: 0x04000436 RID: 1078
		Standard,
		/// <summary>The appearance of the control is determined by the user's operating system.</summary>
		// Token: 0x04000437 RID: 1079
		System
	}
}
