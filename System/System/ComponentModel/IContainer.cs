using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality for containers. Containers are objects that logically contain zero or more components.</summary>
	// Token: 0x02000247 RID: 583
	public interface IContainer : IDisposable
	{
		/// <summary>Adds the specified <see cref="T:System.ComponentModel.IComponent" /> to the <see cref="T:System.ComponentModel.IContainer" /> at the end of the list.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to add. </param>
		// Token: 0x06000E05 RID: 3589
		void Add(IComponent component);

		/// <summary>Gets all the components in the <see cref="T:System.ComponentModel.IContainer" />.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.IComponent" /> objects that represents all the components in the <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000E06 RID: 3590
		ComponentCollection Components { get; }

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.IContainer" />.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to remove. </param>
		// Token: 0x06000E07 RID: 3591
		void Remove(IComponent component);
	}
}
