using System;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Ensures that all finalization code in derived classes is marked as critical.</summary>
	// Token: 0x02000576 RID: 1398
	public abstract class CriticalFinalizerObject
	{
		// Token: 0x06002AC7 RID: 10951 RVA: 0x000AA694 File Offset: 0x000A8894
		~CriticalFinalizerObject()
		{
		}
	}
}
