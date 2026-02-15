using System;
using System.Collections;

namespace Microsoft.Win32
{
	/// <summary>Provides access to system event notifications. This class cannot be inherited.</summary>
	// Token: 0x020000DC RID: 220
	public sealed class SystemEvents
	{
		/// <summary>Occurs when a user preference has changed.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000421 RID: 1057 RVA: 0x00002FA0 File Offset: 0x000011A0
		// (remove) Token: 0x06000422 RID: 1058 RVA: 0x00002FA0 File Offset: 0x000011A0
		[MonoTODO("Currently does nothing on Mono")]
		public static event UserPreferenceChangedEventHandler UserPreferenceChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x04000364 RID: 868
		private static Hashtable TimerStore = new Hashtable();
	}
}
