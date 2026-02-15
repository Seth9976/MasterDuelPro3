using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.Utility;

namespace YgomSystem
{
	// Token: 0x020004B5 RID: 1205
	[DisallowMultipleComponent]
	public class Rebooter : MonoBehaviour
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060026B5 RID: 9909 RVA: 0x0000216D File Offset: 0x0000036D
		public static Rebooter instance
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool rebooting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Execute(string bootpage, AppInfo.BootType boottype, UnityAction onEnd)
		{
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x0000216D File Offset: 0x0000036D
		private void RebootStart(string bootpage, AppInfo.BootType boottype, UnityAction onEnd)
		{
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator RebootProcess(string bootpage, AppInfo.BootType boottype, UnityAction onEnd)
		{
			return null;
		}

		// Token: 0x040027B3 RID: 10163
		private IEnumerator _proc;
	}
}
