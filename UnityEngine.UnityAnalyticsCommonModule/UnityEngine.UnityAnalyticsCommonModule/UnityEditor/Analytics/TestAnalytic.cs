using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000022 RID: 34
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class TestAnalytic : AnalyticsEventBase
	{
		// Token: 0x0600003E RID: 62 RVA: 0x0000254B File Offset: 0x0000074B
		public TestAnalytic()
			: base("TestAnalytic", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002564 File Offset: 0x00000764
		[RequiredByNativeCode]
		public static TestAnalytic CreateTestAnalytic()
		{
			return new TestAnalytic();
		}

		// Token: 0x0400005B RID: 91
		public int param;
	}
}
