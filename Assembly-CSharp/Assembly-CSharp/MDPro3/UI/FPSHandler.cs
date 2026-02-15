using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013E7 RID: 5095
	public class FPSHandler : UIHandler
	{
		// Token: 0x06009382 RID: 37762 RVA: 0x0014DC8E File Offset: 0x0014BE8E
		public override void Initialize()
		{
			this.m_lastUpdateShowTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06009383 RID: 37763 RVA: 0x0014DC9C File Offset: 0x0014BE9C
		public override void PerframeFunction()
		{
			this.m_frames++;
			if (Time.realtimeSinceStartup - this.m_lastUpdateShowTime >= this.m_updateTime)
			{
				this.m_FPS = (float)this.m_frames / (Time.realtimeSinceStartup - this.m_lastUpdateShowTime);
				this.m_lastUpdateShowTime = Time.realtimeSinceStartup;
				this.m_frames = 0;
				this.text.text = ((int)this.m_FPS).ToString();
			}
		}

		// Token: 0x0400D1E8 RID: 53736
		private float m_lastUpdateShowTime;

		// Token: 0x0400D1E9 RID: 53737
		private readonly float m_updateTime = 0.5f;

		// Token: 0x0400D1EA RID: 53738
		private int m_frames;

		// Token: 0x0400D1EB RID: 53739
		private float m_FPS;

		// Token: 0x0400D1EC RID: 53740
		public Text text;
	}
}
