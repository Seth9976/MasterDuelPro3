using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Duel;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x02000961 RID: 2401
	public class ShopPreviewContainer : ElementWidgetBase
	{
		// Token: 0x14000058 RID: 88
		// (add) Token: 0x0600461D RID: 17949 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600461E RID: 17950 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onPlaySummonStart
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x0600461F RID: 17951 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06004620 RID: 17952 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onPlaySummonEnd
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ShopPreviewContainer(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x0000216D File Offset: 0x0000036D
		public void Release()
		{
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayStrongSummon(RawImage rawImage, int mrk, Action onReady = null, Action onFinish = null)
		{
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayStrongSummon(RawImage rawImage, int mrk, Action onReady = null, Action onFinish = null)
		{
			return null;
		}

		// Token: 0x0400848E RID: 33934
		private readonly string k_ELabelSummonCamera;

		// Token: 0x0400848F RID: 33935
		private readonly string k_ELabelSummonFieldRoot;

		// Token: 0x04008490 RID: 33936
		private readonly GameObject m_RootGo;

		// Token: 0x04008491 RID: 33937
		private readonly Camera m_SummonCamera;

		// Token: 0x04008492 RID: 33938
		private readonly GameObject m_SummonFieldRoot;

		// Token: 0x04008493 RID: 33939
		private RenderTexture m_StrongSummonRenderTexture;

		// Token: 0x04008494 RID: 33940
		private MonsterCutinEffect m_StrongSummonEffect;

		// Token: 0x04008495 RID: 33941
		private Coroutine m_SummonPlayRoutine;
	}
}
