using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000012 RID: 18
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class ProjectSettingsInformationAnalytic : AnalyticsEventBase
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000022AB File Offset: 0x000004AB
		public ProjectSettingsInformationAnalytic()
			: base("navigation_project_settings_info", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022C4 File Offset: 0x000004C4
		[RequiredByNativeCode]
		internal static ProjectSettingsInformationAnalytic CreateProjectSettingsInformationAnalytic()
		{
			return new ProjectSettingsInformationAnalytic();
		}

		// Token: 0x04000036 RID: 54
		private int agent_types_count;

		// Token: 0x04000037 RID: 55
		private int areas_count;
	}
}
