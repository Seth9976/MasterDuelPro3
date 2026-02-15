using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x02000536 RID: 1334
	public class PrioritySE : MonoBehaviour
	{
		// Token: 0x06002AA9 RID: 10921 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPriority(string se, int prio)
		{
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DisableFrame()
		{
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Play(string se)
		{
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ExecPlay()
		{
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x040029CB RID: 10699
		private static SortedDictionary<string, int> priority;

		// Token: 0x040029CC RID: 10700
		private static string crntSe;

		// Token: 0x040029CD RID: 10701
		private static int crntSePrio;

		// Token: 0x040029CE RID: 10702
		private static bool disableFrame;
	}
}
