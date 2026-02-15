using System;
using UnityEngine;

namespace YgomSystem.UI.InfinityScroll
{
	// Token: 0x02000688 RID: 1672
	[Serializable]
	public class EntitySelectorSettings
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600343C RID: 13372 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isScrollByAnalogMain
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600343D RID: 13373 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isScrollByAnalogSub
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600343E RID: 13374 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float thresInput
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionTransitionSetting GetDirectionSetting(PadInputDirection direction)
		{
			return null;
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTransitionMode(SelectionItem selectionItem, PadInputDirection direction)
		{
		}

		// Token: 0x04002FEC RID: 12268
		[SerializeField]
		private SelectionTransitionSetting m_UpEdgeTransition;

		// Token: 0x04002FED RID: 12269
		[SerializeField]
		private SelectionTransitionSetting m_DownEdgeTransition;

		// Token: 0x04002FEE RID: 12270
		[SerializeField]
		private SelectionTransitionSetting m_RightEdgeTransition;

		// Token: 0x04002FEF RID: 12271
		[SerializeField]
		private SelectionTransitionSetting m_LeftEdgeTransition;

		// Token: 0x04002FF0 RID: 12272
		[SerializeField]
		private bool m_ScrollAnalogMain;

		// Token: 0x04002FF1 RID: 12273
		[SerializeField]
		private bool m_ScrollAnalogSub;

		// Token: 0x04002FF2 RID: 12274
		[SerializeField]
		private float m_ThresInput;
	}
}
