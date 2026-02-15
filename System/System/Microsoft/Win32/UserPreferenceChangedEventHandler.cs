using System;

namespace Microsoft.Win32
{
	/// <summary>Represents the method that will handle the <see cref="E:Microsoft.Win32.SystemEvents.UserPreferenceChanged" /> event.</summary>
	/// <param name="sender">The source of the event. When this event is raised by the <see cref="T:Microsoft.Win32.SystemEvents" /> class, this object is always null. </param>
	/// <param name="e">A <see cref="T:Microsoft.Win32.UserPreferenceChangedEventArgs" /> that contains the event data. </param>
	// Token: 0x020000DF RID: 223
	// (Invoke) Token: 0x06000426 RID: 1062
	public delegate void UserPreferenceChangedEventHandler(object sender, UserPreferenceChangedEventArgs e);
}
