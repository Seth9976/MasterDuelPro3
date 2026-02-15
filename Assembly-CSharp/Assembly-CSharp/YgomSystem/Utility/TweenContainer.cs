using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.UI;

namespace YgomSystem.Utility
{
	// Token: 0x02000558 RID: 1368
	public class TweenContainer
	{
		// Token: 0x06002BC5 RID: 11205 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup(GameObject target)
		{
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddTweens(Tween[] tweens)
		{
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play(string playLabel, Action onPlayFinished, bool stop = true, string stopLabel = "")
		{
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x0000216D File Offset: 0x0000036D
		public void Stop(string label)
		{
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsPlaying(string label = null)
		{
			return false;
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Update()
		{
			return false;
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x0000216D File Offset: 0x0000036D
		public void Immediate(string label)
		{
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x0000216D File Offset: 0x0000036D
		public void CaptureFrom(string label = null)
		{
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x0000216D File Offset: 0x0000036D
		private void InvokeOnPlayFinished()
		{
		}

		// Token: 0x04002A56 RID: 10838
		private List<Tween> tweens;

		// Token: 0x04002A57 RID: 10839
		private Action onPlayFinished;

		// Token: 0x04002A58 RID: 10840
		private string playingLabel;

		// Token: 0x04002A59 RID: 10841
		private bool playing;
	}
}
