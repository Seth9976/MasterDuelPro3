using System;
using UnityEngine;
using YgomSystem.Timeline;

namespace YgomSample
{
	// Token: 0x0200079A RID: 1946
	public class LabelCallbackSampleController : MonoBehaviour
	{
		// Token: 0x06003C8F RID: 15503 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x0000216D File Offset: 0x0000036D
		public void Progress()
		{
		}

		// Token: 0x06003C91 RID: 15505 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnTMLabelPaused(string label, LabeledPlayableController pd)
		{
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnTMLabelPlayed(string label, LabeledPlayableController pd)
		{
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnTMLabelStopped(string label, LabeledPlayableController pd)
		{
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x0000216D File Offset: 0x0000036D
		private void Play()
		{
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayNextLabel()
		{
		}

		// Token: 0x04003505 RID: 13573
		[SerializeField]
		private float m_TimeScale;

		// Token: 0x04003506 RID: 13574
		[SerializeField]
		private string m_Label;

		// Token: 0x04003507 RID: 13575
		[SerializeField]
		private LabelDirectorWrapMode m_WrapMode;

		// Token: 0x04003508 RID: 13576
		private LabeledPlayableController m_LabeledPlayableController;
	}
}
