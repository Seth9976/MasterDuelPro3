using System;
using System.Collections;
using MDPro3;
using MDPro3.Duel.YGOSharp;
using UnityEngine;
using UnityEngine.Playables;
using YgomSystem.ElementSystem;
using YgomSystem.Timeline;

namespace YgomGame.Duel
{
	// Token: 0x02000D30 RID: 3376
	public class DuelChainSpot : MonoBehaviour
	{
		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060061F3 RID: 25075 RVA: 0x0000216A File Offset: 0x0000036A
		private DuelIconSprites m_DuelIconSprites
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060061F4 RID: 25076 RVA: 0x000F567A File Offset: 0x000F387A
		private ElementObjectManager m_EOManager
		{
			get
			{
				return base.GetComponent<ElementObjectManager>();
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060061F5 RID: 25077 RVA: 0x000F5682 File Offset: 0x000F3882
		private PlayableDirector m_PlayableDirector
		{
			get
			{
				return base.GetComponent<PlayableDirector>();
			}
		}

		// Token: 0x060061F6 RID: 25078 RVA: 0x000F568C File Offset: 0x000F388C
		private void Awake()
		{
			this.m_LPController = LabeledPlayableController.Create(base.GetComponent<PlayableDirector>());
			SpriteRenderer[] componentsInChildren = base.transform.GetComponentsInChildren<SpriteRenderer>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].sortingLayerName = "ChainSpot";
			}
		}

		// Token: 0x060061F7 RID: 25079 RVA: 0x000F56D4 File Offset: 0x000F38D4
		public void Play(int chainNum, GPS p, bool modelExist)
		{
			if (chainNum < 10)
			{
				this.m_EOManager.GetElement("DummyNum02_01").SetActive(false);
				this.m_EOManager.GetElement("DummyNum02_02").SetActive(false);
				this.m_EOManager.GetElement<SpriteRenderer>("DummyNum01").sprite = this.GetNumSprite(chainNum);
			}
			else
			{
				this.m_EOManager.GetElement("DummyNum01").SetActive(false);
				int tensDigit = chainNum / 10 % 10;
				int onesDigit = chainNum % 10;
				this.m_EOManager.GetElement<SpriteRenderer>("DummyNum02_01").sprite = this.GetNumSprite(tensDigit);
				this.m_EOManager.GetElement<SpriteRenderer>("DummyNum02_02").sprite = this.GetNumSprite(onesDigit);
			}
			Vector3 position = GameCard.GetCardPosition(p, null, null);
			float offsetY = 0f;
			if (p.InLocation(CardLocation.Hand))
			{
				offsetY = 3f;
			}
			else if (p.InLocation(CardLocation.Deck, CardLocation.Extra))
			{
				offsetY = 0.11f * (float)Program.instance.ocgcore.GetLocationCardCount((CardLocation)p.location, p.controller);
			}
			position.y += offsetY;
			base.transform.position = position;
			base.transform.localScale = GameCard.GetCardScale(p);
			if (!modelExist)
			{
				global::UnityEngine.Object.Destroy(this.m_EOManager.GetElement("ChainWrapSet"));
			}
			if (p.InLocation(CardLocation.MonsterZone) && p.InPosition(CardPosition.Defence))
			{
				this.m_EOManager.GetElement<Transform>("ChainWrapSet").localEulerAngles = new Vector3(0f, 90f, 0f);
			}
			if (chainNum == 1)
			{
				AudioManager.nextMuteSE = "SE_DUELCHAIN_01";
			}
		}

		// Token: 0x060061F8 RID: 25080 RVA: 0x000F5864 File Offset: 0x000F3A64
		private Sprite GetNumSprite(int num)
		{
			switch (num)
			{
			case 0:
				return TextureManager.container.chainCircleNum0;
			case 1:
				return TextureManager.container.chainCircleNum1;
			case 2:
				return TextureManager.container.chainCircleNum2;
			case 3:
				return TextureManager.container.chainCircleNum3;
			case 4:
				return TextureManager.container.chainCircleNum4;
			case 5:
				return TextureManager.container.chainCircleNum5;
			case 6:
				return TextureManager.container.chainCircleNum6;
			case 7:
				return TextureManager.container.chainCircleNum7;
			case 8:
				return TextureManager.container.chainCircleNum8;
			case 9:
				return TextureManager.container.chainCircleNum9;
			default:
				return TextureManager.container.typeNone;
			}
		}

		// Token: 0x060061F9 RID: 25081 RVA: 0x000F5919 File Offset: 0x000F3B19
		private IEnumerator PlayLabel(int type)
		{
			while (this.m_LPController.loopMixerBehaviour == null)
			{
				yield return null;
			}
			if (type == 0)
			{
				this.m_LPController.PlayLabel("ChainResolveBegin", this.m_LPController.loopMixerBehaviour.loopClips[1]);
			}
			else if (type == 1)
			{
				this.m_LPController.PlayLabel("ChainResolveEnd", null);
			}
			yield break;
		}

		// Token: 0x060061FA RID: 25082 RVA: 0x000F592F File Offset: 0x000F3B2F
		public void OnChainResolveBegin()
		{
			base.StartCoroutine(this.PlayLabel(0));
		}

		// Token: 0x060061FB RID: 25083 RVA: 0x000F593F File Offset: 0x000F3B3F
		public void OnChainResolveEnd()
		{
			base.StartCoroutine(this.PlayLabel(1));
		}

		// Token: 0x060061FC RID: 25084 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnChainSetMore()
		{
		}

		// Token: 0x060061FD RID: 25085 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060061FE RID: 25086 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitChainResolveBegin()
		{
		}

		// Token: 0x060061FF RID: 25087 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitChainResolveEnd()
		{
		}

		// Token: 0x06006200 RID: 25088 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitTimelineEnd()
		{
		}

		// Token: 0x04009CDF RID: 40159
		private const string LABEL_ICON_CHAINWRAP = "ChainWrapSet";

		// Token: 0x04009CE0 RID: 40160
		private const string LABEL_ICON_NUM0 = "DummyNum01";

		// Token: 0x04009CE1 RID: 40161
		private const string LABEL_ICON_NUM1 = "DummyNum02_01";

		// Token: 0x04009CE2 RID: 40162
		private const string LABEL_ICON_NUM2 = "DummyNum02_02";

		// Token: 0x04009CE3 RID: 40163
		private ElementObjectManager m_EOManager_Origin;

		// Token: 0x04009CE4 RID: 40164
		private Vector3 m_OriginPos;

		// Token: 0x04009CE5 RID: 40165
		private DuelChainSpot.Step m_Step;

		// Token: 0x04009CE6 RID: 40166
		private bool m_OnChainResolveBeginFlag;

		// Token: 0x04009CE7 RID: 40167
		private bool m_OnChainResolveEndFlag;

		// Token: 0x04009CE8 RID: 40168
		private LabeledPlayableController m_LPController;

		// Token: 0x02000D31 RID: 3377
		private enum Step
		{
			// Token: 0x04009CEA RID: 40170
			WaitChainResolveBegin,
			// Token: 0x04009CEB RID: 40171
			WaitChainResolveEnd,
			// Token: 0x04009CEC RID: 40172
			WaitTimelineEnd,
			// Token: 0x04009CED RID: 40173
			Idle
		}
	}
}
