using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/UnityAnalytics/RemoteSettings/RemoteSettings.h")]
	[NativeHeader("UnityAnalyticsScriptingClasses.h")]
	[ExcludeFromDocs]
	[NativeHeader("Modules/UnityAnalyticsCommon/Public/UnityAnalyticsCommon.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class RemoteConfigSettings
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020C0 File Offset: 0x000002C0
		[RequiredByNativeCode]
		internal static void RemoteConfigSettingsUpdated(RemoteConfigSettings rcs, bool wasLastUpdatedFromServer)
		{
			Action<bool> handler = rcs.Updated;
			bool flag = handler != null;
			if (flag)
			{
				handler(wasLastUpdatedFromServer);
			}
		}

		// Token: 0x04000004 RID: 4
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000005 RID: 5
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<bool> Updated;
	}
}
