using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Provides different methods for preventing derivation.</summary>
	// Token: 0x020002CC RID: 716
	[Flags]
	public enum XmlSchemaDerivationMethod
	{
		/// <summary>Override default derivation method to allow any derivation.</summary>
		// Token: 0x04000F48 RID: 3912
		[XmlEnum("")]
		Empty = 0,
		/// <summary>Refers to derivations by Substitution.</summary>
		// Token: 0x04000F49 RID: 3913
		[XmlEnum("substitution")]
		Substitution = 1,
		/// <summary>Refers to derivations by Extension.</summary>
		// Token: 0x04000F4A RID: 3914
		[XmlEnum("extension")]
		Extension = 2,
		/// <summary>Refers to derivations by Restriction.</summary>
		// Token: 0x04000F4B RID: 3915
		[XmlEnum("restriction")]
		Restriction = 4,
		/// <summary>Refers to derivations by List.</summary>
		// Token: 0x04000F4C RID: 3916
		[XmlEnum("list")]
		List = 8,
		/// <summary>Refers to derivations by Union.</summary>
		// Token: 0x04000F4D RID: 3917
		[XmlEnum("union")]
		Union = 16,
		/// <summary>#all. Refers to all derivation methods.</summary>
		// Token: 0x04000F4E RID: 3918
		[XmlEnum("#all")]
		All = 255,
		/// <summary>Accepts the default derivation method.</summary>
		// Token: 0x04000F4F RID: 3919
		[XmlIgnore]
		None = 256
	}
}
