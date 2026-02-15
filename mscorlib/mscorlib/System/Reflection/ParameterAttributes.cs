using System;

namespace System.Reflection
{
	/// <summary>Defines the attributes that can be associated with a parameter. These are defined in CorHdr.h.</summary>
	// Token: 0x02000611 RID: 1553
	[Flags]
	public enum ParameterAttributes
	{
		/// <summary>Specifies that there is no parameter attribute.</summary>
		// Token: 0x04001758 RID: 5976
		None = 0,
		/// <summary>Specifies that the parameter is an input parameter.</summary>
		// Token: 0x04001759 RID: 5977
		In = 1,
		/// <summary>Specifies that the parameter is an output parameter.</summary>
		// Token: 0x0400175A RID: 5978
		Out = 2,
		/// <summary>Specifies that the parameter is a locale identifier (lcid).</summary>
		// Token: 0x0400175B RID: 5979
		Lcid = 4,
		/// <summary>Specifies that the parameter is a return value.</summary>
		// Token: 0x0400175C RID: 5980
		Retval = 8,
		/// <summary>Specifies that the parameter is optional.</summary>
		// Token: 0x0400175D RID: 5981
		Optional = 16,
		/// <summary>Specifies that the parameter has a default value.</summary>
		// Token: 0x0400175E RID: 5982
		HasDefault = 4096,
		/// <summary>Specifies that the parameter has field marshaling information.</summary>
		// Token: 0x0400175F RID: 5983
		HasFieldMarshal = 8192,
		/// <summary>Reserved.</summary>
		// Token: 0x04001760 RID: 5984
		Reserved3 = 16384,
		/// <summary>Reserved.</summary>
		// Token: 0x04001761 RID: 5985
		Reserved4 = 32768,
		/// <summary>Specifies that the parameter is reserved.</summary>
		// Token: 0x04001762 RID: 5986
		ReservedMask = 61440
	}
}
