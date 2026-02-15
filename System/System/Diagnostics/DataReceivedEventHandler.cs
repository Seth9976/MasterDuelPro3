using System;

namespace System.Diagnostics
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Diagnostics.Process.OutputDataReceived" /> event or <see cref="E:System.Diagnostics.Process.ErrorDataReceived" /> event of a <see cref="T:System.Diagnostics.Process" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Diagnostics.DataReceivedEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017D RID: 381
	// (Invoke) Token: 0x060008FE RID: 2302
	public delegate void DataReceivedEventHandler(object sender, DataReceivedEventArgs e);
}
