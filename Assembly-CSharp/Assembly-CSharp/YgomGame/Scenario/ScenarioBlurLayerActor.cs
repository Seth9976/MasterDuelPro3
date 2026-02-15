using System;
using UnityEngine;
using YgomSystem.Effect;
using YgomSystem.ElementSystem;

namespace YgomGame.Scenario
{
	// Token: 0x020009CA RID: 2506
	public class ScenarioBlurLayerActor
	{
		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060048D6 RID: 18646 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x060048D7 RID: 18647 RVA: 0x0000216D File Offset: 0x0000036D
		public float effect
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060048D8 RID: 18648 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060048D9 RID: 18649 RVA: 0x0000216D File Offset: 0x0000036D
		public int depth
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x00002739 File Offset: 0x00000939
		public ScenarioBlurLayerActor(ElementObject eo, ScenarioObjectContainer3D objectContainer)
		{
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x0000216D File Offset: 0x0000036D
		private void ApplyDepthLayers()
		{
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateBlurLayerCard(ScenarioCardActor targetCard)
		{
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateBlurLayerBGPref(GameObject target)
		{
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateBlurLayerGameObject(GameObject target, int targetDepth)
		{
		}

		// Token: 0x040086CB RID: 34507
		private readonly int k_DepthBg;

		// Token: 0x040086CC RID: 34508
		private readonly int k_DepthBgPrefab;

		// Token: 0x040086CD RID: 34509
		private readonly int k_DepthCard;

		// Token: 0x040086CE RID: 34510
		private readonly int k_DepthCardPop;

		// Token: 0x040086CF RID: 34511
		private readonly string k_MatKeyEffect;

		// Token: 0x040086D0 RID: 34512
		private readonly ElementObject m_Eo;

		// Token: 0x040086D1 RID: 34513
		private readonly ScenarioObjectContainer3D m_ObjectContainer3D;

		// Token: 0x040086D2 RID: 34514
		private readonly SpriteRenderer m_SpriteRenderer;

		// Token: 0x040086D3 RID: 34515
		public readonly SpriteScaler spriteScaler;

		// Token: 0x040086D4 RID: 34516
		private int m_Depth;
	}
}
