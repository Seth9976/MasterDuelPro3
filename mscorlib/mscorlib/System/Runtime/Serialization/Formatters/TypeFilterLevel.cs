using System;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Specifies the level of automatic deserialization for .NET Framework remoting.</summary>
	// Token: 0x020004D2 RID: 1234
	public enum TypeFilterLevel
	{
		/// <summary>The low deserialization level for .NET Framework remoting. It supports types associated with basic remoting functionality.</summary>
		// Token: 0x040012B8 RID: 4792
		Low = 2,
		/// <summary>The full deserialization level for .NET Framework remoting. It supports all types that remoting supports in all situations.</summary>
		// Token: 0x040012B9 RID: 4793
		Full
	}
}
