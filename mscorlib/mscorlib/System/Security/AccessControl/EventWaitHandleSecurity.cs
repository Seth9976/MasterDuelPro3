using System;
using System.Runtime.InteropServices;

namespace System.Security.AccessControl
{
	/// <summary>Represents the Windows access control security applied to a named system wait handle. This class cannot be inherited.</summary>
	// Token: 0x020003F3 RID: 1011
	public sealed class EventWaitHandleSecurity : NativeObjectSecurity
	{
		// Token: 0x06002223 RID: 8739 RVA: 0x0008DF96 File Offset: 0x0008C196
		internal EventWaitHandleSecurity(SafeHandle handle, AccessControlSections includeSections)
			: base(false, ResourceType.KernelObject, handle, includeSections)
		{
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x0008DFA2 File Offset: 0x0008C1A2
		internal void Persist(SafeHandle handle)
		{
			base.PersistModifications(handle);
		}
	}
}
