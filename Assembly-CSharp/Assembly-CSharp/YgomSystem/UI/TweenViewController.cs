using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000645 RID: 1605
	public class TweenViewController : ViewController
	{
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual GameObject m_TweenTarget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x0000216A File Offset: 0x0000036A
		protected string k_SwapLabelAlphaFade
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x0000216A File Offset: 0x0000036A
		protected string k_SwapLabelFlip
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddLabelSuffix(string suffix)
		{
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetTransitionType2Label(ViewController.TransitionType type)
		{
			return null;
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x0000216D File Offset: 0x0000036D
		public override void TransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool TransitionUpdate(ViewController.TransitionType type)
		{
			return false;
		}

		// Token: 0x04002EC9 RID: 11977
		public string PushLabel;

		// Token: 0x04002ECA RID: 11978
		public string PopLabel;

		// Token: 0x04002ECB RID: 11979
		public string CoverLabel;

		// Token: 0x04002ECC RID: 11980
		public string UncoverLabel;

		// Token: 0x04002ECD RID: 11981
		public string SwapInLabel;

		// Token: 0x04002ECE RID: 11982
		public string SwapOutLabel;

		// Token: 0x04002ECF RID: 11983
		private string m_LastTweenLabel;
	}
}
