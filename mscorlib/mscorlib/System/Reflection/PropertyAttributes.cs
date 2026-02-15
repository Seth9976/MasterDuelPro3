using System;

namespace System.Reflection
{
	/// <summary>Defines the attributes that can be associated with a property. These attribute values are defined in corhdr.h.</summary>
	// Token: 0x02000617 RID: 1559
	[Flags]
	public enum PropertyAttributes
	{
		/// <summary>Specifies that no attributes are associated with a property.</summary>
		// Token: 0x0400177C RID: 6012
		None = 0,
		/// <summary>Specifies that the property is special, with the name describing how the property is special.</summary>
		// Token: 0x0400177D RID: 6013
		SpecialName = 512,
		/// <summary>Specifies that the metadata internal APIs check the name encoding.</summary>
		// Token: 0x0400177E RID: 6014
		RTSpecialName = 1024,
		/// <summary>Specifies that the property has a default value.</summary>
		// Token: 0x0400177F RID: 6015
		HasDefault = 4096,
		/// <summary>Reserved.</summary>
		// Token: 0x04001780 RID: 6016
		Reserved2 = 8192,
		/// <summary>Reserved.</summary>
		// Token: 0x04001781 RID: 6017
		Reserved3 = 16384,
		/// <summary>Reserved.</summary>
		// Token: 0x04001782 RID: 6018
		Reserved4 = 32768,
		/// <summary>Specifies a flag reserved for runtime use only.</summary>
		// Token: 0x04001783 RID: 6019
		ReservedMask = 62464
	}
}
