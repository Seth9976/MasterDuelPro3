using System;

namespace System.ComponentModel
{
	/// <summary>Specifies which event is raised on initialization. This class cannot be inherited.</summary>
	// Token: 0x0200024B RID: 587
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class InitializationEventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InitializationEventAttribute" /> class.</summary>
		/// <param name="eventName">The name of the initialization event.</param>
		// Token: 0x06000E15 RID: 3605 RVA: 0x0003E91B File Offset: 0x0003CB1B
		public InitializationEventAttribute(string eventName)
		{
			this.<EventName>k__BackingField = eventName;
		}
	}
}
