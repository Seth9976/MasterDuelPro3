using System;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Specifies a reliability contract.</summary>
	// Token: 0x02000573 RID: 1395
	public enum Consistency
	{
		/// <summary>In the face of exceptional conditions, the CLR makes no guarantees regarding state consistency; that is, the condition might corrupt the process.</summary>
		// Token: 0x040015BB RID: 5563
		MayCorruptProcess,
		/// <summary>In the face of exceptional conditions, the common language runtime (CLR) makes no guarantees regarding state consistency in the current application domain.</summary>
		// Token: 0x040015BC RID: 5564
		MayCorruptAppDomain,
		/// <summary>In the face of exceptional conditions, the method is guaranteed to limit state corruption to the current instance.</summary>
		// Token: 0x040015BD RID: 5565
		MayCorruptInstance,
		/// <summary>In the face of exceptional conditions, the method is guaranteed not to corrupt state. </summary>
		// Token: 0x040015BE RID: 5566
		WillNotCorruptState
	}
}
