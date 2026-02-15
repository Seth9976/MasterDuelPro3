using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using MDPro3.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace MDPro3
{
	// Token: 0x020011F6 RID: 4598
	public class CardDescription : MonoBehaviour
	{
		// Token: 0x06008876 RID: 34934 RVA: 0x000FD216 File Offset: 0x000FB416
		private void Start()
		{
			this.manager = base.GetComponent<ElementObjectManager>();
			this.manager.GetElement<Button>("CardButton").onClick.AddListener(new UnityAction(this.ShowDetail));
		}

		// Token: 0x06008877 RID: 34935 RVA: 0x000FD24A File Offset: 0x000FB44A
		public void Hide()
		{
			this.showing = false;
			this.manager.GetElement<RectTransform>("Window").DOAnchorPosX(-1020f - SafeAreaAdapter.GetSafeAreaLeftOffset(), 0.01f, false);
		}

		// Token: 0x06008878 RID: 34936 RVA: 0x000FD27A File Offset: 0x000FB47A
		private void ShowDetail()
		{
			Program.instance.ocgcore.GetUI<OcgCoreUI>().CardList.Hide();
			Program.instance.ui_.chatPanel.Hide();
			UIManager.ShowCardInfoDetail(this.data);
		}

		// Token: 0x06008879 RID: 34937 RVA: 0x000FD2B4 File Offset: 0x000FB4B4
		private async UniTask RefreshFace(int code)
		{
			Material mat = MaterialLoader.GetCardMaterial(code, false);
			Material material = mat;
			Texture texture = await CardImageLoader.LoadCardAsync(code, false, base.destroyCancellationToken, false);
			material.mainTexture = texture;
			material = null;
			this.manager.GetElement<RawImage>("Card").material = mat;
		}

		// Token: 0x0600887A RID: 34938 RVA: 0x000FD300 File Offset: 0x000FB500
		public void Show(GameCard card, Material mat, int code = -1, GPS gps = null)
		{
			Card data;
			if (code > -1)
			{
				data = CardsManager.Get(code, false);
			}
			else
			{
				data = card.GetData();
			}
			Card origin = CardsManager.Get(data.Id, false);
			if (origin.Id == 0)
			{
				return;
			}
			string tails = string.Empty;
			if (code == -1)
			{
				tails = "<color=#0FFF0F>" + card.tails.managedString + "</color>";
			}
			GPS p;
			if (code > -1)
			{
				p = gps;
			}
			else
			{
				p = card.p;
			}
			this.data = data;
			this.manager.GetElement<RectTransform>("Window").DOAnchorPosX(20f, 0.01f, false);
			this.showing = true;
			if (p == null || (p.location & 2048U) > 0U)
			{
				this.manager.GetElement<Image>("Player").color = new Color(0f, 0f, 0f, 0.3f);
				this.manager.GetElement("BaseActivated").SetActive(false);
			}
			else if (p.controller == 0U)
			{
				this.manager.GetElement<Image>("Player").color = new Color(0f, 0f, 1f, 0.3f);
				if (OcgCore.myActivated.Contains(data.Id))
				{
					this.manager.GetElement("BaseActivated").SetActive(true);
				}
				else
				{
					this.manager.GetElement("BaseActivated").SetActive(false);
				}
			}
			else
			{
				this.manager.GetElement<Image>("Player").color = new Color(1f, 0f, 0f, 0.3f);
				if (OcgCore.opActivated.Contains(data.Id))
				{
					this.manager.GetElement("BaseActivated").SetActive(true);
				}
				else
				{
					this.manager.GetElement("BaseActivated").SetActive(false);
				}
			}
			this.manager.GetElement<Text>("TextName").text = data.Name;
			CardDescription.AttributeSprite attributeSprite = CardDescription.GetCardAttribute(data);
			this.manager.GetElement<Image>("Attribute").sprite = attributeSprite.sprite;
			this.manager.GetElement("ArributeOutline").SetActive(attributeSprite.notOriginal);
			Color[] frameColors = CardDescription.GetCardFrameColor(data);
			this.manager.GetElement<Image>("BaseName").color = frameColors[0];
			this.manager.GetElement<Image>("BaseType").color = frameColors[1];
			this.manager.GetElement<Image>("BaseActivated").color = frameColors[1];
			if (mat == null)
			{
				this.RefreshFace(data.Id);
			}
			else
			{
				this.manager.GetElement<RawImage>("Card").material = global::UnityEngine.Object.Instantiate<Material>(mat);
				this.manager.GetElement<RawImage>("Card").material.SetFloat("_Monochrome", 0f);
				this.manager.GetElement<RawImage>("Card").material.renderQueue = 3000;
			}
			this.manager.GetElement<Text>("TextType").text = data.GetTypeForUI();
			if (data.HasType(CardType.Pendulum))
			{
				string[] texts = origin.GetDescriptionSplit(false);
				string monster = InterString.Get("【怪兽效果】", 0);
				if (!data.HasType(CardType.Effect))
				{
					monster = InterString.Get("【怪兽描述】", 0);
				}
				if (p != null && ((p.location & 512U) > 0U || ((p.location & 8U) > 0U && !data.HasType(CardType.Equip) && !data.HasType(CardType.Continuous) && !data.HasType(CardType.Trap))))
				{
					this.manager.GetElement<TextMeshProUGUI>("TextDescription").text = string.Concat(new string[]
					{
						tails,
						data.GetSetNameWithColor(),
						InterString.Get("【灵摆效果】", 0),
						"\n",
						texts[0],
						"\n<color=#666666>",
						monster,
						"\n",
						texts[1],
						"</color>"
					});
				}
				else if (p != null && (p.location & 4U) > 0U)
				{
					this.manager.GetElement<TextMeshProUGUI>("TextDescription").text = string.Concat(new string[]
					{
						tails,
						data.GetSetNameWithColor(),
						monster,
						"\n",
						texts[1],
						"\n<color=#666666>",
						InterString.Get("【灵摆效果】", 0),
						"\n",
						texts[0],
						"</color>"
					});
				}
				else
				{
					this.manager.GetElement<TextMeshProUGUI>("TextDescription").text = string.Concat(new string[]
					{
						tails,
						data.GetSetNameWithColor(),
						InterString.Get("【灵摆效果】", 0),
						"\n",
						texts[0],
						"\n",
						monster,
						"\n",
						texts[1]
					});
				}
			}
			else
			{
				this.manager.GetElement<TextMeshProUGUI>("TextDescription").text = tails + data.GetSetNameWithColor() + data.Desc;
			}
			this.manager.GetElement<TextMeshProUGUI>("TextDescription").fontSize = 25f * Config.GetUIScale(1.35f);
			if (CardDescription.CardIsMonster(data))
			{
				this.manager.GetElement("PropertyMonster").SetActive(true);
				this.manager.GetElement("PropertySpell").SetActive(false);
				CardDescription.RaceSprite raceSprite = CardDescription.GetCardRace(data);
				this.manager.GetElement<Image>("Race").sprite = raceSprite.sprite;
				this.manager.GetElement("RaceOutline").SetActive(raceSprite.notOriginal);
				bool isTuner = false;
				if (data.HasType(CardType.Tuner))
				{
					isTuner = true;
					this.manager.GetElement("Tuner").SetActive(true);
				}
				else
				{
					this.manager.GetElement("Tuner").SetActive(false);
				}
				if (isTuner)
				{
					if (origin.HasType(CardType.Tuner))
					{
						this.manager.GetElement("TunerOutline").SetActive(false);
					}
					else
					{
						this.manager.GetElement("TunerOutline").SetActive(true);
					}
				}
				else
				{
					this.manager.GetElement("TunerOutline").SetActive(false);
				}
				this.manager.GetElement<Image>("Level").sprite = TextureManager.GetCardLevelIcon(data);
				if (data.HasType(CardType.Link))
				{
					this.manager.GetElement<Text>("TextLevel").text = data.GetLinkCount().ToString();
					this.manager.GetElement("Scale").SetActive(false);
					this.manager.GetElement("TextScale").SetActive(false);
					this.manager.GetElement("Defense").SetActive(false);
					this.manager.GetElement("TextDefense").SetActive(false);
					this.manager.GetElement<RectTransform>("Attack").anchoredPosition = new Vector2(0f, -45f);
					this.manager.GetElement<RectTransform>("TextAttack").anchoredPosition = new Vector2(40f, -45f);
				}
				else
				{
					this.manager.GetElement<Text>("TextLevel").text = data.Level.ToString();
					if (data.Level > origin.Level)
					{
						this.manager.GetElement<Text>("TextLevel").color = CardDescription.upColor;
					}
					else if (data.Level < origin.Level)
					{
						this.manager.GetElement<Text>("TextLevel").color = CardDescription.downColor;
					}
					else
					{
						this.manager.GetElement<Text>("TextLevel").color = CardDescription.equalColor;
					}
					this.manager.GetElement("Defense").SetActive(true);
					this.manager.GetElement("TextDefense").SetActive(true);
					this.manager.GetElement<Text>("TextDefense").text = ((data.Defense == -2) ? "?" : data.Defense.ToString());
					if (data.Defense > ((origin.Defense < 0) ? 0 : origin.Defense))
					{
						this.manager.GetElement<Text>("TextDefense").color = CardDescription.upColor;
					}
					else if (data.Defense < origin.Defense)
					{
						this.manager.GetElement<Text>("TextDefense").color = CardDescription.downColor;
					}
					else
					{
						this.manager.GetElement<Text>("TextDefense").color = CardDescription.equalColor;
					}
					if (data.HasType(CardType.Pendulum))
					{
						this.manager.GetElement("Scale").SetActive(true);
						this.manager.GetElement("TextScale").SetActive(true);
						this.manager.GetElement<RectTransform>("Attack").anchoredPosition = new Vector2(0f, -90f);
						this.manager.GetElement<RectTransform>("TextAttack").anchoredPosition = new Vector2(40f, -90f);
						this.manager.GetElement<RectTransform>("Defense").anchoredPosition = new Vector2(0f, -135f);
						this.manager.GetElement<RectTransform>("TextDefense").anchoredPosition = new Vector2(40f, -135f);
						this.manager.GetElement<Text>("TextScale").text = data.LScale.ToString();
						if (data.LScale > origin.LScale)
						{
							this.manager.GetElement<Text>("TextScale").color = CardDescription.upColor;
						}
						else if (data.LScale < origin.LScale)
						{
							this.manager.GetElement<Text>("TextScale").color = CardDescription.downColor;
						}
						else
						{
							this.manager.GetElement<Text>("TextScale").color = CardDescription.equalColor;
						}
					}
					else
					{
						this.manager.GetElement("Scale").SetActive(false);
						this.manager.GetElement("TextScale").SetActive(false);
						this.manager.GetElement<RectTransform>("Attack").anchoredPosition = new Vector2(0f, -45f);
						this.manager.GetElement<RectTransform>("TextAttack").anchoredPosition = new Vector2(40f, -45f);
						this.manager.GetElement<RectTransform>("Defense").anchoredPosition = new Vector2(0f, -90f);
						this.manager.GetElement<RectTransform>("TextDefense").anchoredPosition = new Vector2(40f, -90f);
					}
				}
				this.manager.GetElement<Text>("TextAttack").text = ((data.Attack == -2) ? "?" : data.Attack.ToString());
				if (data.Attack > ((origin.Attack < 0) ? 0 : origin.Attack))
				{
					this.manager.GetElement<Text>("TextAttack").color = CardDescription.upColor;
				}
				else if (data.Attack < origin.Attack)
				{
					this.manager.GetElement<Text>("TextAttack").color = CardDescription.downColor;
				}
				else
				{
					this.manager.GetElement<Text>("TextAttack").color = CardDescription.equalColor;
				}
			}
			else
			{
				this.manager.GetElement("PropertyMonster").SetActive(false);
				this.manager.GetElement("PropertySpell").SetActive(true);
				this.manager.GetElement<Image>("SpellType").sprite = TextureManager.GetSpellTrapTypeIcon(data);
				this.manager.GetElement<Text>("TextSpellType").text = data.GetSpellTrapType(false);
			}
			this.RefreshLimitIcon(data.Id);
		}

		// Token: 0x0600887B RID: 34939 RVA: 0x000FDF0C File Offset: 0x000FC10C
		private void RefreshLimitIcon(int code)
		{
			int limit = DeckEditor.banlist.GetQuantity(code);
			if (limit == 3)
			{
				this.manager.GetElement<Image>("Limit").sprite = TextureManager.container.typeNone;
				return;
			}
			if (limit == 2)
			{
				this.manager.GetElement<Image>("Limit").sprite = TextureManager.container.limit2;
				return;
			}
			if (limit == 1)
			{
				this.manager.GetElement<Image>("Limit").sprite = TextureManager.container.limit1;
				return;
			}
			this.manager.GetElement<Image>("Limit").sprite = TextureManager.container.banned;
		}

		// Token: 0x0600887C RID: 34940 RVA: 0x000FDFB0 File Offset: 0x000FC1B0
		public static bool CardIsMonster(Card data)
		{
			if (!CardsManager.Get(data.Id, false).HasType(CardType.Monster))
			{
				return data.HasType(CardType.Monster);
			}
			return !data.HasType(CardType.Spell) && !data.HasType(CardType.Trap);
		}

		// Token: 0x0600887D RID: 34941 RVA: 0x000FDFEC File Offset: 0x000FC1EC
		private static CardDescription.AttributeSprite GetCardAttribute(Card data)
		{
			Card origin = CardsManager.Get(data.Id, false);
			CardDescription.AttributeSprite returnValue = default(CardDescription.AttributeSprite);
			if (CardDescription.CardIsMonster(data))
			{
				if (!origin.HasType(CardType.Monster))
				{
					returnValue.notOriginal = true;
					returnValue.sprite = TextureManager.container.GetCardAttributeIcon(data, false);
				}
				else if (origin.HasType(CardType.Trap))
				{
					returnValue.notOriginal = true;
					returnValue.sprite = TextureManager.container.GetCardAttributeIcon(data, false);
				}
				else if (data.Attribute == origin.Attribute)
				{
					returnValue.notOriginal = false;
					returnValue.sprite = TextureManager.container.GetCardAttributeIcon(data, false);
				}
				else
				{
					returnValue.notOriginal = true;
					Card newData = data.Clone();
					newData.Attribute = data.Attribute ^ origin.Attribute;
					returnValue.sprite = TextureManager.container.GetCardAttributeIcon(newData, false);
				}
			}
			else if (!origin.HasType(CardType.Monster))
			{
				if (((long)data.Type & 2L & (long)origin.Type) > 0L)
				{
					returnValue.sprite = TextureManager.container.attributeSpell;
					returnValue.notOriginal = false;
				}
				else if (((long)data.Type & 4L & (long)origin.Type) > 0L)
				{
					returnValue.sprite = TextureManager.container.attributeTrap;
					returnValue.notOriginal = false;
				}
				else
				{
					returnValue.notOriginal = true;
					if (data.HasType(CardType.Spell))
					{
						returnValue.sprite = TextureManager.container.attributeSpell;
					}
					else
					{
						returnValue.sprite = TextureManager.container.attributeTrap;
					}
				}
			}
			else
			{
				returnValue.notOriginal = true;
				if (data.HasType(CardType.Spell))
				{
					returnValue.sprite = TextureManager.container.attributeSpell;
				}
				else
				{
					returnValue.sprite = TextureManager.container.attributeTrap;
				}
			}
			return returnValue;
		}

		// Token: 0x0600887E RID: 34942 RVA: 0x000FE1B4 File Offset: 0x000FC3B4
		public static CardDescription.RaceSprite GetCardRace(Card data)
		{
			CardDescription.RaceSprite returnValue = default(CardDescription.RaceSprite);
			returnValue.notOriginal = false;
			Card origin = CardsManager.Get(data.Id, false);
			if (!origin.HasType(CardType.Monster))
			{
				returnValue.notOriginal = true;
			}
			else if (data.Race != origin.Race)
			{
				returnValue.notOriginal = true;
			}
			returnValue.sprite = TextureManager.GetCardRaceIcon(data.Race);
			return returnValue;
		}

		// Token: 0x0600887F RID: 34943 RVA: 0x000FE21C File Offset: 0x000FC41C
		public static Color[] GetCardFrameColor(Card data)
		{
			Color[] returnValue = new Color[2];
			returnValue[0] = new Color(0.7764f, 0.6784f, 0.6274f, 1f);
			returnValue[1] = returnValue[0];
			if (data.Id == 0)
			{
				return returnValue;
			}
			Card origin = CardsManager.Get(data.Id, false);
			if (data.Id == 10000000)
			{
				returnValue[0] = new Color(0.4745f, 0.4549f, 1f, 1f);
				returnValue[1] = returnValue[0];
			}
			else if (data.Id == 10000020)
			{
				returnValue[0] = new Color(1f, 0.247f, 0.2156f, 1f);
				returnValue[1] = returnValue[0];
			}
			else if (data.Id == 10000010)
			{
				returnValue[0] = new Color(1f, 0.9882f, 0.1882f, 1f);
				returnValue[1] = returnValue[0];
			}
			else if (origin.HasType(CardType.Pendulum))
			{
				if (origin.HasType(CardType.Fusion))
				{
					returnValue[0] = new Color(0.8823f, 0.345f, 1f, 1f);
					returnValue[1] = new Color(0f, 0.8901f, 0.7411f, 1f);
				}
				else if (origin.HasType(CardType.Synchro))
				{
					returnValue[0] = new Color(1f, 1f, 1f, 1f);
					returnValue[1] = new Color(0f, 0.8901f, 0.7411f, 1f);
				}
				else if (origin.HasType(CardType.Xyz))
				{
					returnValue[0] = new Color(0f, 0f, 0f, 1f);
					returnValue[1] = new Color(0f, 0.8901f, 0.7411f, 1f);
				}
				else if (origin.HasType(CardType.Ritual))
				{
					returnValue[0] = new Color(0.3176f, 0.5882f, 1f, 1f);
					returnValue[1] = new Color(0f, 0.8901f, 0.7411f, 1f);
				}
				else if (origin.HasType(CardType.Effect))
				{
					returnValue[0] = new Color(1f, 0.4745f, 0.1882f, 1f);
					returnValue[1] = new Color(0f, 0.8901f, 0.7411f, 1f);
				}
				else if (origin.HasType(CardType.Normal))
				{
					returnValue[0] = new Color(1f, 0.745f, 0.3294f, 1f);
					returnValue[1] = new Color(0f, 0.8901f, 0.7411f, 1f);
				}
			}
			else if (origin.HasType(CardType.Fusion))
			{
				returnValue[0] = new Color(0.8823f, 0.345f, 1f, 1f);
				returnValue[1] = returnValue[0];
			}
			else if (origin.HasType(CardType.Synchro))
			{
				returnValue[0] = new Color(1f, 1f, 1f, 1f);
				returnValue[1] = returnValue[0];
			}
			else if (origin.HasType(CardType.Xyz))
			{
				returnValue[0] = new Color(0f, 0f, 0f, 1f);
				returnValue[1] = returnValue[0];
			}
			else if (origin.HasType(CardType.Link))
			{
				returnValue[0] = new Color(0f, 0.3764f, 0.7764f, 1f);
				returnValue[1] = returnValue[0];
			}
			else if (origin.HasType(CardType.Ritual) && origin.HasType(CardType.Monster))
			{
				returnValue[0] = new Color(0.3176f, 0.5882f, 1f, 1f);
				returnValue[1] = returnValue[0];
			}
			else if (origin.HasType(CardType.Token))
			{
				returnValue[0] = new Color(0.7764f, 0.6784f, 0.6274f, 1f);
				returnValue[1] = returnValue[0];
			}
			else if (origin.HasType(CardType.Effect))
			{
				returnValue[0] = new Color(1f, 0.4745f, 0.1882f, 1f);
				returnValue[1] = returnValue[0];
			}
			else if (origin.HasType(CardType.Normal))
			{
				returnValue[0] = new Color(1f, 0.745f, 0.3294f, 1f);
				returnValue[1] = returnValue[0];
			}
			else if (origin.HasType(CardType.Spell))
			{
				if (data.HasType(CardType.Effect))
				{
					returnValue[0] = new Color(1f, 0.4745f, 0.1882f, 1f);
					returnValue[1] = new Color(0f, 0.8901f, 0.7411f, 1f);
				}
				else if (data.HasType(CardType.Normal))
				{
					returnValue[0] = new Color(1f, 0.745f, 0.3294f, 1f);
					returnValue[1] = new Color(0f, 0.8901f, 0.7411f, 1f);
				}
				else
				{
					returnValue[0] = new Color(0f, 0.8901f, 0.7411f, 1f);
					returnValue[1] = returnValue[0];
				}
			}
			else if (origin.HasType(CardType.Trap))
			{
				if (data.HasType(CardType.Effect))
				{
					returnValue[0] = new Color(1f, 0.4745f, 0.1882f, 1f);
					returnValue[1] = new Color(1f, 0.0509f, 0.6784f, 1f);
				}
				else if (data.HasType(CardType.Normal))
				{
					returnValue[0] = new Color(1f, 0.745f, 0.3294f, 1f);
					returnValue[1] = new Color(1f, 0.0509f, 0.6784f, 1f);
				}
				else
				{
					returnValue[0] = new Color(1f, 0.0509f, 0.6784f, 1f);
					returnValue[1] = returnValue[0];
				}
			}
			return returnValue;
		}

		// Token: 0x0400C397 RID: 50071
		private ElementObjectManager manager;

		// Token: 0x0400C398 RID: 50072
		public static Color upColor = Color.cyan;

		// Token: 0x0400C399 RID: 50073
		public static Color downColor = Color.red;

		// Token: 0x0400C39A RID: 50074
		public static Color equalColor = Color.white;

		// Token: 0x0400C39B RID: 50075
		public Card data;

		// Token: 0x0400C39C RID: 50076
		public bool showing;

		// Token: 0x020011F7 RID: 4599
		public struct AttributeSprite
		{
			// Token: 0x0400C39D RID: 50077
			public Sprite sprite;

			// Token: 0x0400C39E RID: 50078
			public bool notOriginal;
		}

		// Token: 0x020011F8 RID: 4600
		public struct RaceSprite
		{
			// Token: 0x0400C39F RID: 50079
			public Sprite sprite;

			// Token: 0x0400C3A0 RID: 50080
			public bool notOriginal;
		}
	}
}
