using System;

namespace System.ComponentModel
{
	/// <summary>Provides a base class for the container filter service.</summary>
	// Token: 0x02000263 RID: 611
	public abstract class ContainerFilterService
	{
		/// <summary>Filters the component collection.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ComponentCollection" /> that represents a modified collection.</returns>
		/// <param name="components">The component collection to filter.</param>
		// Token: 0x06000E77 RID: 3703 RVA: 0x00030668 File Offset: 0x0002E868
		public virtual ComponentCollection FilterComponents(ComponentCollection components)
		{
			return components;
		}
	}
}
