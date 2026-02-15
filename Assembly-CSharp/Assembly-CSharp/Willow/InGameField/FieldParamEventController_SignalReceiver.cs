using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace Willow.InGameField
{
	// Token: 0x02001568 RID: 5480
	[Preserve]
	public class FieldParamEventController_SignalReceiver : MonoBehaviour, INotificationReceiver
	{
		// Token: 0x06009EF7 RID: 40695 RVA: 0x0000216D File Offset: 0x0000036D
		public void RaiseFromFieldCommandSet(global::UnityEngine.Object fieldEventCommandSet)
		{
		}

		// Token: 0x06009EF8 RID: 40696 RVA: 0x0000216D File Offset: 0x0000036D
		public void RaiseFromFieldCommandSetInt(IntFieldEventCommandSet intFieldEventCommandSet)
		{
		}

		// Token: 0x06009EF9 RID: 40697 RVA: 0x0000216D File Offset: 0x0000036D
		public void RaiseFromFieldCommandSetBool(BoolFieldEventCommandSet boolFieldEventCommandSet)
		{
		}

		// Token: 0x06009EFA RID: 40698 RVA: 0x0000216D File Offset: 0x0000036D
		public void RaiseFromFieldCommandSetTrigger(TriggerFieldEventCommandSet triggerFieldEventCommandSet)
		{
		}

		// Token: 0x06009EFB RID: 40699 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnNotify(Playable origin, INotification notification, object context)
		{
		}

		// Token: 0x0400DE3E RID: 56894
		public FieldParamEventController fieldParamEventController;
	}
}
