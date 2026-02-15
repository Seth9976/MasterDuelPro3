using System;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x0200055B RID: 1371
	public class UITransitionUtil
	{
		// Token: 0x06002BD8 RID: 11224 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup(GameObject target)
		{
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play(string label, UITransitionUtil.BlockType blockType, Action onPlayFinished, bool stop, string stopLabel)
		{
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x0000216D File Offset: 0x0000036D
		public void Immediate(string label)
		{
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Update()
		{
			return false;
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetBlockPriority(UITransitionUtil.BlockType blockType)
		{
			return 0;
		}

		// Token: 0x04002A5E RID: 10846
		private TweenContainer tweenContainer;

		// Token: 0x0200055C RID: 1372
		public enum BlockType
		{
			// Token: 0x04002A60 RID: 10848
			None,
			// Token: 0x04002A61 RID: 10849
			Game,
			// Token: 0x04002A62 RID: 10850
			System
		}
	}
}
