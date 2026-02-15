using System;

namespace System.Windows.Forms
{
	/// <summary>Provides the functionality for a control to act as a parent for other controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C9 RID: 201
	public interface IContainerControl
	{
		/// <summary>Gets or sets the control that is active on the container control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that is currently active on the container control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060007B8 RID: 1976
		// (set) Token: 0x060007B9 RID: 1977
		Control ActiveControl { get; set; }
	}
}
