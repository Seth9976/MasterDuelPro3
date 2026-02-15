using System;
using UnityEngine;
using YgomSystem.Timeline;

namespace YgomSample
{
	// Token: 0x0200079B RID: 1947
	public class LabeledTimelineSampleController : MonoBehaviour
	{
		// Token: 0x06003C97 RID: 15511 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play()
		{
		}

		// Token: 0x04003509 RID: 13577
		[SerializeField]
		private string m_PlayLabel;

		// Token: 0x0400350A RID: 13578
		[SerializeField]
		private LabelDirectorWrapMode m_WrapMode;

		// Token: 0x0400350B RID: 13579
		private LabeledPlayableController m_LabeledPlayableController;
	}
}
