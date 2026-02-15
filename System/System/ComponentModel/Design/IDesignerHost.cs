using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for managing designer transactions and components.</summary>
	// Token: 0x020002E3 RID: 739
	public interface IDesignerHost : IServiceProvider
	{
		/// <summary>Gets the instance of the base class used as the root component for the current design.</summary>
		/// <returns>The instance of the root component class.</returns>
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060011F2 RID: 4594
		IComponent RootComponent { get; }

		/// <summary>Gets the designer instance that contains the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" />, or null if there is no designer for the specified component.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to retrieve the designer for. </param>
		// Token: 0x060011F3 RID: 4595
		IDesigner GetDesigner(IComponent component);
	}
}
