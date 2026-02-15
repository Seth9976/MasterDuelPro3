using System;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged" /> event raised when a property is changed on a component.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data. </param>
	// Token: 0x020002B0 RID: 688
	// (Invoke) Token: 0x06001055 RID: 4181
	public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
}
