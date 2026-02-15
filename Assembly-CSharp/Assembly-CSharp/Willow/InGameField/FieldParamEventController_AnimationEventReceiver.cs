using System;
using UnityEngine;

namespace Willow.InGameField
{
	// Token: 0x02001566 RID: 5478
	public class FieldParamEventController_AnimationEventReceiver : MonoBehaviour
	{
		// Token: 0x06009EF0 RID: 40688 RVA: 0x0000216D File Offset: 0x0000036D
		public void RaiseFromFieldCommandSet(global::UnityEngine.Object fieldEventCommandSet)
		{
		}

		// Token: 0x06009EF1 RID: 40689 RVA: 0x0000216D File Offset: 0x0000036D
		public void RaiseFromFieldCommandSetInt(IntFieldEventCommandSet intFieldEventCommandSet)
		{
		}

		// Token: 0x06009EF2 RID: 40690 RVA: 0x0019B660 File Offset: 0x00199860
		public void RaiseFromFieldCommandSetBool(BoolFieldEventCommandSet boolFieldEventCommandSet)
		{
			string name = boolFieldEventCommandSet.boolFieldEvent.name.Replace("_event", "");
			foreach (Transform t in base.transform.GetComponentsInChildren<Transform>(true))
			{
				if (t.name.Contains(name))
				{
					t.gameObject.SetActive(boolFieldEventCommandSet.boolValue);
				}
			}
		}

		// Token: 0x0400DE3C RID: 56892
		public FieldParamEventController fieldParamEventController;
	}
}
