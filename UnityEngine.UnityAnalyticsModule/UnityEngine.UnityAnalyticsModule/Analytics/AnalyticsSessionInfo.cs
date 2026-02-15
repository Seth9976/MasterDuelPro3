using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000009 RID: 9
	[RequiredByNativeCode]
	[NativeHeader("UnityAnalyticsScriptingClasses.h")]
	[NativeHeader("Modules/UnityAnalytics/Public/UnityAnalytics.h")]
	public static class AnalyticsSessionInfo
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020E8 File Offset: 0x000002E8
		[RequiredByNativeCode]
		internal static void CallSessionStateChanged(AnalyticsSessionState sessionState, long sessionId, long sessionElapsedTime, bool sessionChanged)
		{
			AnalyticsSessionInfo.SessionStateChanged handler = AnalyticsSessionInfo.sessionStateChanged;
			bool flag = handler != null;
			if (flag)
			{
				handler(sessionState, sessionId, sessionElapsedTime, sessionChanged);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002110 File Offset: 0x00000310
		[RequiredByNativeCode]
		internal static void CallIdentityTokenChanged(string token)
		{
			AnalyticsSessionInfo.IdentityTokenChanged handler = AnalyticsSessionInfo.identityTokenChanged;
			bool flag = handler != null;
			if (flag)
			{
				handler(token);
			}
		}

		// Token: 0x04000017 RID: 23
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static AnalyticsSessionInfo.SessionStateChanged sessionStateChanged;

		// Token: 0x04000018 RID: 24
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static AnalyticsSessionInfo.IdentityTokenChanged identityTokenChanged;

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x0600000A RID: 10
		public delegate void SessionStateChanged(AnalyticsSessionState sessionState, long sessionId, long sessionElapsedTime, bool sessionChanged);

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x0600000C RID: 12
		public delegate void IdentityTokenChanged(string token);
	}
}
