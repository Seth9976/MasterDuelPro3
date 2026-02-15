using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem
{
	// Token: 0x020004C2 RID: 1218
	public class SafeAreaManager : MonoBehaviour
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600272F RID: 10031 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002730 RID: 10032 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool reqUpdate
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Add(SafeAreaScreen screen)
		{
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Remove(SafeAreaScreen screen)
		{
		}

		// Token: 0x04002810 RID: 10256
		private static List<SafeAreaScreen> screenList;

		// Token: 0x04002811 RID: 10257
		private Rect currentSafeArea;
	}
}
