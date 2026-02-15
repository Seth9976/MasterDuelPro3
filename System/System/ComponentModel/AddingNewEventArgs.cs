using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.BindingSource.AddingNew" /> event.</summary>
	// Token: 0x02000250 RID: 592
	public class AddingNewEventArgs : EventArgs
	{
		/// <summary>Gets or sets the object to be added to the binding list. </summary>
		/// <returns>The <see cref="T:System.Object" /> to be added as a new item to the associated collection. </returns>
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0003EB36 File Offset: 0x0003CD36
		public object NewObject { get; }
	}
}
