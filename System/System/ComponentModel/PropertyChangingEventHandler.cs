using System;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanging.PropertyChanging" /> event of an <see cref="T:System.ComponentModel.INotifyPropertyChanging" /> interface. </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.ComponentModel.PropertyChangingEventArgs" /> that contains the event data.</param>
	// Token: 0x020002B2 RID: 690
	// (Invoke) Token: 0x06001058 RID: 4184
	public delegate void PropertyChangingEventHandler(object sender, PropertyChangingEventArgs e);
}
