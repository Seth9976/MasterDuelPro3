using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Enables a non-control component to emulate the data-binding behavior of a Windows Forms control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C7 RID: 199
	public interface IBindableComponent : IComponent, IDisposable
	{
		/// <summary>Gets or sets the collection of currency managers for the <see cref="T:System.Windows.Forms.IBindableComponent" />. </summary>
		/// <returns>The collection of <see cref="T:System.Windows.Forms.BindingManagerBase" /> objects for this <see cref="T:System.Windows.Forms.IBindableComponent" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060007B3 RID: 1971
		BindingContext BindingContext { get; }
	}
}
