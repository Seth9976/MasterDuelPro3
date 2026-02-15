using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality required by sites.</summary>
	// Token: 0x02000248 RID: 584
	public interface ISite : IServiceProvider
	{
		/// <summary>Gets the component associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> instance associated with the <see cref="T:System.ComponentModel.ISite" />.</returns>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000E08 RID: 3592
		IComponent Component { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.IContainer" /> associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> instance associated with the <see cref="T:System.ComponentModel.ISite" />.</returns>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000E09 RID: 3593
		IContainer Container { get; }

		/// <summary>Determines whether the component is in design mode when implemented by a class.</summary>
		/// <returns>true if the component is in design mode; otherwise, false.</returns>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000E0A RID: 3594
		bool DesignMode { get; }

		/// <summary>Gets or sets the name of the component associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The name of the component associated with the <see cref="T:System.ComponentModel.ISite" />; or null, if no name is assigned to the component.</returns>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000E0B RID: 3595
		string Name { get; }
	}
}
