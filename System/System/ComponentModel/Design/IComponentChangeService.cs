using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface to add and remove the event handlers for events that add, change, remove or rename components, and provides methods to raise a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> or <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
	// Token: 0x020002E1 RID: 737
	public interface IComponentChangeService
	{
		/// <summary>Announces to the component change service that a particular component has changed.</summary>
		/// <param name="component">The component that has changed. </param>
		/// <param name="member">The member that has changed. This is null if this change is not related to a single member. </param>
		/// <param name="oldValue">The old value of the member. This is valid only if the member is not null. </param>
		/// <param name="newValue">The new value of the member. This is valid only if the member is not null. </param>
		// Token: 0x060011F0 RID: 4592
		void OnComponentChanged(object component, MemberDescriptor member, object oldValue, object newValue);

		/// <summary>Announces to the component change service that a particular component is changing.</summary>
		/// <param name="component">The component that is about to change. </param>
		/// <param name="member">The member that is changing. This is null if this change is not related to a single member. </param>
		// Token: 0x060011F1 RID: 4593
		void OnComponentChanging(object component, MemberDescriptor member);
	}
}
