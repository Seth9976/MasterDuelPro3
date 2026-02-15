using System;

namespace System.ComponentModel
{
	/// <summary>Provides information about an event.</summary>
	// Token: 0x02000274 RID: 628
	public abstract class EventDescriptor : MemberDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptor" /> class with the specified name and attribute array.</summary>
		/// <param name="name">The name of the event. </param>
		/// <param name="attrs">An array of type <see cref="T:System.Attribute" /> that contains the event attributes. </param>
		// Token: 0x06000EDB RID: 3803 RVA: 0x000415E3 File Offset: 0x0003F7E3
		protected EventDescriptor(string name, Attribute[] attrs)
			: base(name, attrs)
		{
		}

		/// <summary>When overridden in a derived class, gets the type of delegate for the event.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of delegate for the event.</returns>
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000EDC RID: 3804
		public abstract Type EventType { get; }

		/// <summary>When overridden in a derived class, binds the event to the component.</summary>
		/// <param name="component">A component that provides events to the delegate. </param>
		/// <param name="value">A delegate that represents the method that handles the event. </param>
		// Token: 0x06000EDD RID: 3805
		public abstract void AddEventHandler(object component, Delegate value);

		/// <summary>When overridden in a derived class, unbinds the delegate from the component so that the delegate will no longer receive events from the component.</summary>
		/// <param name="component">The component that the delegate is bound to. </param>
		/// <param name="value">The delegate to unbind from the component. </param>
		// Token: 0x06000EDE RID: 3806
		public abstract void RemoveEventHandler(object component, Delegate value);
	}
}
