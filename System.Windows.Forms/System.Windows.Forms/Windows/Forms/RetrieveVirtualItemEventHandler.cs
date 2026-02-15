using System;

namespace System.Windows.Forms
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event of a <see cref="T:System.Windows.Forms.ListView" />. </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.RetrieveVirtualItemEventArgs" />  that contains the event data. </param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.RetrieveVirtualItemEventArgs.Item" /> property is not set to an item when the <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event is handled. </exception>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000172 RID: 370
	// (Invoke) Token: 0x06000E0D RID: 3597
	public delegate void RetrieveVirtualItemEventHandler(object sender, RetrieveVirtualItemEventArgs e);
}
