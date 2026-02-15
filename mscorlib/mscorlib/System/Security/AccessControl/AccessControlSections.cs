using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies which sections of a security descriptor to save or load.</summary>
	// Token: 0x020003E4 RID: 996
	[Flags]
	public enum AccessControlSections
	{
		/// <summary>No sections.</summary>
		// Token: 0x0400103F RID: 4159
		None = 0,
		/// <summary>The system access control list (SACL).</summary>
		// Token: 0x04001040 RID: 4160
		Audit = 1,
		/// <summary>The discretionary access control list (DACL).</summary>
		// Token: 0x04001041 RID: 4161
		Access = 2,
		/// <summary>The owner.</summary>
		// Token: 0x04001042 RID: 4162
		Owner = 4,
		/// <summary>The primary group.</summary>
		// Token: 0x04001043 RID: 4163
		Group = 8,
		/// <summary>The entire security descriptor.</summary>
		// Token: 0x04001044 RID: 4164
		All = 15
	}
}
