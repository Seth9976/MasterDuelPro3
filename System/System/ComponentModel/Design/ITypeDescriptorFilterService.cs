using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface to modify the set of member descriptors for a component in design mode.</summary>
	// Token: 0x020002E8 RID: 744
	public interface ITypeDescriptorFilterService
	{
		/// <summary>Filters the attributes that a component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <returns>true if the set of filtered attributes is to be cached; false if the filter service must query again.</returns>
		/// <param name="component">The component to filter the attributes of. </param>
		/// <param name="attributes">A dictionary of attributes that can be modified. </param>
		// Token: 0x060011FA RID: 4602
		bool FilterAttributes(IComponent component, IDictionary attributes);

		/// <summary>Filters the events that a component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <returns>true if the set of filtered events is to be cached; false if the filter service must query again.</returns>
		/// <param name="component">The component to filter events for. </param>
		/// <param name="events">A dictionary of events that can be modified. </param>
		// Token: 0x060011FB RID: 4603
		bool FilterEvents(IComponent component, IDictionary events);

		/// <summary>Filters the properties that a component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <returns>true if the set of filtered properties is to be cached; false if the filter service must query again.</returns>
		/// <param name="component">The component to filter properties for. </param>
		/// <param name="properties">A dictionary of properties that can be modified. </param>
		// Token: 0x060011FC RID: 4604
		bool FilterProperties(IComponent component, IDictionary properties);
	}
}
