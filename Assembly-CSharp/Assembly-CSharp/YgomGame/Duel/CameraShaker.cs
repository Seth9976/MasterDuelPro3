using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CAD RID: 3245
	public class CameraShaker : MonoBehaviour
	{
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06005C76 RID: 23670 RVA: 0x000F4F94 File Offset: 0x000F3194
		// (set) Token: 0x06005C77 RID: 23671 RVA: 0x0000216D File Offset: 0x0000036D
		public Vector3 shakeOffset
		{
			[CompilerGenerated]
			get
			{
				return default(Vector3);
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06005C78 RID: 23672 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool LoadSetting(string label)
		{
			return false;
		}

		// Token: 0x06005C79 RID: 23673 RVA: 0x0000216D File Offset: 0x0000036D
		public void Shake(CameraShaker.Type type)
		{
		}

		// Token: 0x06005C7A RID: 23674 RVA: 0x0000216D File Offset: 0x0000036D
		public void Shake(string label)
		{
		}

		// Token: 0x06005C7B RID: 23675 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06005C7C RID: 23676 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateShake(bool countup_time = true)
		{
		}

		// Token: 0x06005C7D RID: 23677 RVA: 0x0000216D File Offset: 0x0000036D
		public void FinishShake()
		{
		}

		// Token: 0x040097FE RID: 38910
		public bool isShaking;

		// Token: 0x040097FF RID: 38911
		private float shakeTimer;

		// Token: 0x04009800 RID: 38912
		private Vector3 big;

		// Token: 0x04009801 RID: 38913
		private Vector3 lit;

		// Token: 0x04009802 RID: 38914
		private int loopCount;

		// Token: 0x04009803 RID: 38915
		private string settingPath;

		// Token: 0x04009804 RID: 38916
		private CameraShakerSetting setting;

		// Token: 0x04009805 RID: 38917
		private CameraShakerSetting.Info shaker;

		// Token: 0x02000CAE RID: 3246
		public enum Type
		{
			// Token: 0x04009807 RID: 38919
			LIFEDAMAGE,
			// Token: 0x04009808 RID: 38920
			ATTACKGUARD,
			// Token: 0x04009809 RID: 38921
			ATTACKBREAK,
			// Token: 0x0400980A RID: 38922
			ATTACKDIRECT,
			// Token: 0x0400980B RID: 38923
			ACEMONST1,
			// Token: 0x0400980C RID: 38924
			ACEMONST2,
			// Token: 0x0400980D RID: 38925
			ACEMONST3,
			// Token: 0x0400980E RID: 38926
			EFFECT4354,
			// Token: 0x0400980F RID: 38927
			EFFECT4342
		}
	}
}
