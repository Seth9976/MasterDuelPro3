using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/UnityAnalytics/RemoteSettings/RemoteSettings.h")]
	[NativeHeader("UnityAnalyticsScriptingClasses.h")]
	public static class RemoteSettings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[RequiredByNativeCode]
		internal static void RemoteSettingsUpdated(bool wasLastUpdatedFromServer)
		{
			RemoteSettings.UpdatedEventHandler handler = RemoteSettings.Updated;
			bool flag = handler != null;
			if (flag)
			{
				handler();
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002074 File Offset: 0x00000274
		[RequiredByNativeCode]
		internal static void RemoteSettingsBeforeFetchFromServer()
		{
			Action handler = RemoteSettings.BeforeFetchFromServer;
			bool flag = handler != null;
			if (flag)
			{
				handler();
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002098 File Offset: 0x00000298
		[RequiredByNativeCode]
		internal static void RemoteSettingsUpdateCompleted(bool wasLastUpdatedFromServer, bool settingsChanged, int response)
		{
			Action<bool, bool, int> handler = RemoteSettings.Completed;
			bool flag = handler != null;
			if (flag)
			{
				handler(wasLastUpdatedFromServer, settingsChanged, response);
			}
		}

		// Token: 0x04000001 RID: 1
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static RemoteSettings.UpdatedEventHandler Updated;

		// Token: 0x04000002 RID: 2
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action BeforeFetchFromServer;

		// Token: 0x04000003 RID: 3
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<bool, bool, int> Completed;

		// Token: 0x02000003 RID: 3
		// (Invoke) Token: 0x06000005 RID: 5
		public delegate void UpdatedEventHandler();
	}
}
