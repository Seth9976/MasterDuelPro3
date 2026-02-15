using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006B8 RID: 1720
	public class SignalNotificator : MonoBehaviour, INotificationReceiver
	{
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060035BD RID: 13757 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060035BE RID: 13758 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<string, double> callback
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnNotify(Playable origin, INotification notification, object context)
		{
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}
	}
}
