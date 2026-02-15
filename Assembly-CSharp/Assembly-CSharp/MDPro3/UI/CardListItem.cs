using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using MDPro3.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200135C RID: 4956
	public class CardListItem : MonoBehaviour
	{
		// Token: 0x06008F99 RID: 36761 RVA: 0x0013745C File Offset: 0x0013565C
		private void Start()
		{
			this.RefreshFace();
			this.cardBack.SetActive(((long)this.card.p.position & 5L) == 0L);
			if (this.card.GetData().Id != 0)
			{
				if (this.card.GetData().HasType(CardType.Monster))
				{
					this.levelIcon.sprite = TextureManager.GetCardLevelIcon(this.card.GetData());
					if (this.card.GetData().HasType(CardType.Link))
					{
						this.textLevel.text = this.card.GetData().GetLinkCount().ToString();
					}
					else
					{
						this.textLevel.text = this.card.GetData().Level.ToString();
					}
				}
				else
				{
					this.levelIcon.sprite = TextureManager.container.typeNone;
					this.textLevel.text = string.Empty;
				}
				if (this.card.chains.Count > 0)
				{
					this.chain.SetActive(true);
					this.chainText.text = this.card.chains[0].i.ToString();
					if (this.card.p.controller == 0U)
					{
						this.chainText.color = Color.cyan;
					}
					else
					{
						this.chainText.color = Color.red;
					}
					this.target.SetActive(false);
				}
				else
				{
					this.chain.SetActive(false);
					if (OcgCore.cardsBeTarget.Contains(this.card))
					{
						this.target.SetActive(true);
					}
					else
					{
						this.target.SetActive(false);
					}
				}
			}
			else
			{
				this.levelIcon.gameObject.SetActive(false);
				this.textLevel.text = "";
				this.chain.SetActive(false);
				this.cardBack.SetActive(false);
			}
			this.button.onClick.AddListener(new UnityAction(this.OnClick));
		}

		// Token: 0x06008F9A RID: 36762 RVA: 0x00137670 File Offset: 0x00135870
		private async UniTask RefreshFace()
		{
			this.face.texture = TextureManager.container.unknownCard.texture;
			int code = this.card.GetData().Id;
			if (code != 0)
			{
				RawImage rawImage = this.face;
				Texture texture = await CardImageLoader.LoadCardAsync(code, false, default(CancellationToken), false);
				rawImage.texture = texture;
				rawImage = null;
				this.face.material = MaterialLoader.GetCardMaterial(code, false);
				this.face.material.mainTexture = this.face.texture;
			}
			else
			{
				this.face.texture = null;
				switch (OcgCore.condition)
				{
				case OcgCore.Condition.Duel:
					if (this.card.p.controller == 0U)
					{
						this.face.material = Appearance.duelProtector0;
					}
					else
					{
						this.face.material = Appearance.duelProtector1;
					}
					break;
				case OcgCore.Condition.Watch:
					if (this.card.p.controller == 0U)
					{
						this.face.material = Appearance.watchProtector0;
					}
					else
					{
						this.face.material = Appearance.watchProtector1;
					}
					break;
				case OcgCore.Condition.Replay:
					if (this.card.p.controller == 0U)
					{
						this.face.material = Appearance.replayProtector0;
					}
					else
					{
						this.face.material = Appearance.replayProtector1;
					}
					break;
				}
			}
		}

		// Token: 0x06008F9B RID: 36763 RVA: 0x001376B3 File Offset: 0x001358B3
		private void OnClick()
		{
			Program.instance.ocgcore.GetUI<OcgCoreUI>().CardDescription.Show(this.card, this.face.material, -1, null);
		}

		// Token: 0x0400CDF5 RID: 52725
		public RawImage face;

		// Token: 0x0400CDF6 RID: 52726
		public GameObject cardBack;

		// Token: 0x0400CDF7 RID: 52727
		public Image levelIcon;

		// Token: 0x0400CDF8 RID: 52728
		public TextMeshProUGUI textLevel;

		// Token: 0x0400CDF9 RID: 52729
		public GameObject chain;

		// Token: 0x0400CDFA RID: 52730
		public Text chainText;

		// Token: 0x0400CDFB RID: 52731
		public GameObject target;

		// Token: 0x0400CDFC RID: 52732
		public Button button;

		// Token: 0x0400CDFD RID: 52733
		private static Color myColor = Color.cyan;

		// Token: 0x0400CDFE RID: 52734
		private static Color opColor = Color.red;

		// Token: 0x0400CDFF RID: 52735
		public GameCard card;
	}
}
