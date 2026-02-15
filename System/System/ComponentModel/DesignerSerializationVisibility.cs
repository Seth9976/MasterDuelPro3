using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the visibility a property has to the design-time serializer.</summary>
	// Token: 0x02000242 RID: 578
	public enum DesignerSerializationVisibility
	{
		/// <summary>The code generator does not produce code for the object.</summary>
		// Token: 0x04000993 RID: 2451
		Hidden,
		/// <summary>The code generator produces code for the object.</summary>
		// Token: 0x04000994 RID: 2452
		Visible,
		/// <summary>The code generator produces code for the contents of the object, rather than for the object itself.</summary>
		// Token: 0x04000995 RID: 2453
		Content
	}
}
