using System;

namespace System.Reflection
{
	/// <summary>Specifies the attributes of an event.</summary>
	// Token: 0x020005F7 RID: 1527
	[Flags]
	public enum EventAttributes
	{
		/// <summary>Specifies that the event has no attributes.</summary>
		// Token: 0x040016EB RID: 5867
		None = 0,
		/// <summary>Specifies that the event is special in a way described by the name.</summary>
		// Token: 0x040016EC RID: 5868
		SpecialName = 512,
		/// <summary>Specifies that the common language runtime should check name encoding.</summary>
		// Token: 0x040016ED RID: 5869
		RTSpecialName = 1024,
		/// <summary>Specifies a reserved flag for common language runtime use only.</summary>
		// Token: 0x040016EE RID: 5870
		ReservedMask = 1024
	}
}
