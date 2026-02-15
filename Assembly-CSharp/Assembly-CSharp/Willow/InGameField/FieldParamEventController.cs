using System;
using UnityEngine;

namespace Willow.InGameField
{
	// Token: 0x02001565 RID: 5477
	public class FieldParamEventController : MonoBehaviour
	{
		// Token: 0x06009EE5 RID: 40677 RVA: 0x0000216D File Offset: 0x0000036D
		public void Init()
		{
		}

		// Token: 0x06009EE6 RID: 40678 RVA: 0x0000216D File Offset: 0x0000036D
		public void AttachAnimationEventReceiver(GameObject target)
		{
		}

		// Token: 0x06009EE7 RID: 40679 RVA: 0x0000216D File Offset: 0x0000036D
		public void AttachFieldParamEventController_SignalReceiver(GameObject root)
		{
		}

		// Token: 0x06009EE8 RID: 40680 RVA: 0x0000216D File Offset: 0x0000036D
		public void RaiseFromFieldCommandSet(global::UnityEngine.Object fieldEventCommandSet)
		{
		}

		// Token: 0x06009EE9 RID: 40681 RVA: 0x0000216D File Offset: 0x0000036D
		public void RaiseFromFieldCommandSetInt(IntFieldEventCommandSet intFieldEventCommandSet)
		{
		}

		// Token: 0x06009EEA RID: 40682 RVA: 0x0000216D File Offset: 0x0000036D
		public void RaiseFromFieldCommandSetBool(BoolFieldEventCommandSet boolFieldEventCommandSet)
		{
		}

		// Token: 0x06009EEB RID: 40683 RVA: 0x0000216D File Offset: 0x0000036D
		public void RaiseFromFieldCommandSetTrigger(TriggerFieldEventCommandSet triggerFieldEventCommandSet)
		{
		}

		// Token: 0x06009EEC RID: 40684 RVA: 0x0000216D File Offset: 0x0000036D
		public void RaiseIntEvent(string targetEventName, int intValue)
		{
		}

		// Token: 0x06009EED RID: 40685 RVA: 0x0000216D File Offset: 0x0000036D
		public void RaiseBoolEvent(string targetEventName, bool boolValue)
		{
		}

		// Token: 0x06009EEE RID: 40686 RVA: 0x0000216D File Offset: 0x0000036D
		public void RaiseTriggerEvent(string targetEventName)
		{
		}

		// Token: 0x0400DE3B RID: 56891
		public Blackboard blackboard;
	}
}
