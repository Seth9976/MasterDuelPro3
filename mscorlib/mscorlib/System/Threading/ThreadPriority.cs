using System;

namespace System.Threading
{
	/// <summary>Specifies the scheduling priority of a <see cref="T:System.Threading.Thread" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000223 RID: 547
	public enum ThreadPriority
	{
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with any other priority.</summary>
		// Token: 0x04000A09 RID: 2569
		Lowest,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with Normal priority and before those with Lowest priority.</summary>
		// Token: 0x04000A0A RID: 2570
		BelowNormal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with AboveNormal priority and before those with BelowNormal priority. Threads have Normal priority by default.</summary>
		// Token: 0x04000A0B RID: 2571
		Normal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with Highest priority and before those with Normal priority.</summary>
		// Token: 0x04000A0C RID: 2572
		AboveNormal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled before threads with any other priority.</summary>
		// Token: 0x04000A0D RID: 2573
		Highest
	}
}
