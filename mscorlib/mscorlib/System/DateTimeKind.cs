using System;

namespace System
{
	/// <summary>Specifies whether a <see cref="T:System.DateTime" /> object represents a local time, a Coordinated Universal Time (UTC), or is not specified as either local time or UTC.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D5 RID: 213
	public enum DateTimeKind
	{
		/// <summary>The time represented is not specified as either local time or Coordinated Universal Time (UTC).</summary>
		// Token: 0x04000323 RID: 803
		Unspecified,
		/// <summary>The time represented is UTC.</summary>
		// Token: 0x04000324 RID: 804
		Utc,
		/// <summary>The time represented is local time.</summary>
		// Token: 0x04000325 RID: 805
		Local
	}
}
