using System;

namespace System
{
	/// <summary>Represents the method that will handle the event raised by an exception that is not handled by the application domain.</summary>
	/// <param name="sender">The source of the unhandled exception event. </param>
	/// <param name="e">An <paramref name="UnhandledExceptionEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000161 RID: 353
	// (Invoke) Token: 0x06000CB7 RID: 3255
	[Serializable]
	public delegate void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e);
}
