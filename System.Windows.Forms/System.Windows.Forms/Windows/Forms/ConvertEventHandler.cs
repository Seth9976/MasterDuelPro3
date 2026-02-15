using System;

namespace System.Windows.Forms
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.Binding.Parse" /> and <see cref="E:System.Windows.Forms.Binding.Format" /> events of a <see cref="T:System.Windows.Forms.Binding" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ConvertEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005B RID: 91
	// (Invoke) Token: 0x06000456 RID: 1110
	public delegate void ConvertEventHandler(object sender, ConvertEventArgs e);
}
