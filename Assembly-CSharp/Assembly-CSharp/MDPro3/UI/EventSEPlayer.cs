using System;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x02001371 RID: 4977
	public class EventSEPlayer : MonoBehaviour
	{
		// Token: 0x06009023 RID: 36899 RVA: 0x0013AC50 File Offset: 0x00138E50
		private void PlayAnimationEventSe(string se)
		{
			AudioManager.PlaySE(se, 0.4f);
		}

		// Token: 0x06009024 RID: 36900 RVA: 0x0013AC50 File Offset: 0x00138E50
		private void NewEvent(string se)
		{
			AudioManager.PlaySE(se, 0.4f);
		}
	}
}
