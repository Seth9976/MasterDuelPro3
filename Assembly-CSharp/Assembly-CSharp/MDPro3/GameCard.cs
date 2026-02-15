using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using MDPro3.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using YgomGame.Duel;
using YgomSystem.ElementSystem;

namespace MDPro3
{
	// Token: 0x020011FC RID: 4604
	public class GameCard : MonoBehaviour
	{
		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06008890 RID: 34960 RVA: 0x000FEAEE File Offset: 0x000FCCEE
		// (set) Token: 0x06008891 RID: 34961 RVA: 0x000FEAF6 File Offset: 0x000FCCF6
		public bool Disabled
		{
			get
			{
				return this.m_disabled;
			}
			set
			{
				this.m_disabled = value;
				this.SetDisabled();
			}
		}

		// Token: 0x06008892 RID: 34962 RVA: 0x000FEB05 File Offset: 0x000FCD05
		private void Awake()
		{
			SystemEvent.OnVideoCardConfigChange += this.ReloadFaceWhenConfigChange;
		}

		// Token: 0x06008893 RID: 34963 RVA: 0x000FEB18 File Offset: 0x000FCD18
		public void Dispose()
		{
			global::UnityEngine.Object.Destroy(this.model);
			global::UnityEngine.Object.Destroy(this);
			SystemEvent.OnVideoCardConfigChange -= this.ReloadFaceWhenConfigChange;
		}

		// Token: 0x06008894 RID: 34964 RVA: 0x000FEB3C File Offset: 0x000FCD3C
		private void LateUpdate()
		{
			if (this.model == null)
			{
				return;
			}
			this.hover = UserInput.HoverObject == this.manager.GetElement("CardModel");
			if (this.hover)
			{
				if (this.p.InMyControl() && OcgCore.HideMyHandCard)
				{
					OcgCore.HideMyHandCard = false;
				}
				else if (!this.p.InMyControl() && OcgCore.HideOpHandCard)
				{
					OcgCore.HideOpHandCard = false;
				}
			}
			if (!this.hover)
			{
				this.hoving = false;
			}
			if (this.hover && UserInput.MouseLeftUp && !OcgCore.handCardDraged)
			{
				this.OnClick();
			}
			else if (!this.hover && UserInput.MouseLeftUp)
			{
				if (UserInput.HoverObject == null)
				{
					this.NotClickThis();
				}
				else if (UserInput.HoverObject.name != "PlaceSelector")
				{
					this.NotClickThis();
				}
			}
			if (Math.Abs(OcgCore.handOffset - OcgCore.lastHandOffset) > 10f)
			{
				this.NotClickThis();
			}
			if (this.p.InLocation(CardLocation.Hand))
			{
				if (this.hover && !this.hoving && !this.clicked)
				{
					this.hoving = true;
					this.handDefault = false;
					this.AnimationHandHover();
					this.MoveToHandDefault(0.1f);
				}
				if (this.hover && UserInput.MouseLeftUp && !OcgCore.handCardDraged)
				{
					this.clicked = true;
					this.handDefault = false;
					this.AnimationHandAppeal();
				}
				if (!this.hover && UserInput.MouseLeftDown)
				{
					this.clicked = false;
				}
				if (!this.hover && !this.clicked && !this.handDefault)
				{
					this.AnimationHandDefault(0.1f, false);
				}
				if (Math.Abs(OcgCore.handOffset - OcgCore.lastHandOffset) > 10f)
				{
					this.SetHandDefault();
				}
			}
		}

		// Token: 0x06008895 RID: 34965 RVA: 0x000FED09 File Offset: 0x000FCF09
		public Material GetMaterial()
		{
			if (this.model == null)
			{
				return null;
			}
			return this.manager.GetElement<Transform>("CardModel").GetChild(1).GetComponent<Renderer>()
				.material;
		}

		// Token: 0x06008896 RID: 34966 RVA: 0x000FED3C File Offset: 0x000FCF3C
		public void OnClick()
		{
			if (this.model == null)
			{
				return;
			}
			if ((this.p.location & 2U) == 0U)
			{
				AudioManager.PlaySE("SE_DUEL_SELECT", 1f);
			}
			Program.instance.ocgcore.GetUI<OcgCoreUI>().CardDescription.Show(this, this.GetMaterial(), -1, null);
			if (this.data.HasType(CardType.Xyz) && (this.p.location & 4U) > 0U)
			{
				Program.instance.ocgcore.GetUI<OcgCoreUI>().CardList.Show(Program.instance.ocgcore.GCS_GetOverlays(this), CardLocation.Overlay, (int)this.p.controller);
			}
			else
			{
				Program.instance.ocgcore.GetUI<OcgCoreUI>().CardList.Hide();
			}
			if (this.equipedCard != null)
			{
				Program.instance.ocgcore.ShowEquipLine(this.model.transform.position, this.equipedCard.model.transform.position);
			}
			if (this.targets != null)
			{
				Program.instance.ocgcore.ShowTargetLines(this.model.transform.position, this.targets);
			}
			if (this.buttons.Count == 0 || Program.instance.ocgcore.currentPopup != null)
			{
				return;
			}
			foreach (DuelButton duelButton in this.buttonObjs)
			{
				duelButton.Show();
			}
			if (this.hightYellow)
			{
				this.manager.GetElement("EffectHighlightYellowSelect").SetActive(true);
				return;
			}
			this.manager.GetElement("EffectHighlightBlueSelect").SetActive(true);
		}

		// Token: 0x06008897 RID: 34967 RVA: 0x000FEF1C File Offset: 0x000FD11C
		public void NotClickThis()
		{
			if (this.model == null || this.buttons.Count == 0)
			{
				return;
			}
			foreach (DuelButton duelButton in this.buttonObjs)
			{
				duelButton.Hide();
			}
			if (this.hightYellow)
			{
				this.manager.GetElement("EffectHighlightYellowSelect").SetActive(false);
				return;
			}
			this.manager.GetElement("EffectHighlightBlueSelect").SetActive(false);
		}

		// Token: 0x06008898 RID: 34968 RVA: 0x000FEFC0 File Offset: 0x000FD1C0
		private GameObject CreateModel(bool real = true)
		{
			this.model = ABLoader.LoadMasterDuelGameObject("DuelCardModel");
			this.manager = this.model.GetComponent<ElementObjectManager>();
			this.manager.GetElement("StatusLabelRoot").SetActive(false);
			this.manager.GetElement<GameCardMono>("CardModel").cookieCard = this;
			GameObject cardParmUp = ABLoader.LoadMasterDuelGameObject("fxp_cardparm_up_001");
			GameObject cardParmDown = ABLoader.LoadMasterDuelGameObject("fxp_cardparm_down_001");
			GameObject cardParmChange = ABLoader.LoadMasterDuelGameObject("fxp_cardparm_change_001");
			GameObject cardBuffActive = ABLoader.LoadMasterDuelGameObject("fxp_bff_active_001");
			GameObject cardNegate = ABLoader.LoadMasterDuelGameObject("fxp_bff_disable_001");
			GameObject cardDisquiet = ABLoader.LoadMasterDuelGameObject("fxp_bff_disquiet_001");
			GameObject cardBlueHighlight = ABLoader.LoadMasterDuelGameObject("fxp_HL_set_001");
			GameObject cardBlueHighlightSelect = ABLoader.LoadMasterDuelGameObject("fxp_HL_set_sct_001");
			GameObject cardYellowHighlight = ABLoader.LoadMasterDuelGameObject("fxp_HL_SPsom_001");
			GameObject cardYellowHighlightSelect = ABLoader.LoadMasterDuelGameObject("fxp_HL_SPsom_sct_001");
			cardParmUp.transform.SetParent(this.manager.GetElement<Transform>("Turn").GetChild(1), false);
			cardParmDown.transform.SetParent(this.manager.GetElement<Transform>("Turn").GetChild(1), false);
			cardParmChange.transform.SetParent(this.manager.GetElement<Transform>("Turn").GetChild(1), false);
			cardBuffActive.transform.SetParent(this.manager.GetElement<Transform>("Turn").GetChild(1), false);
			cardBuffActive.transform.localPosition = new Vector3(0f, 0.1f, 0f);
			cardNegate.transform.SetParent(this.manager.GetElement<Transform>("Turn").GetChild(1), false);
			cardNegate.transform.localPosition = new Vector3(0f, 0.1f, 0f);
			cardDisquiet.transform.SetParent(this.manager.GetElement<Transform>("Turn").GetChild(1), false);
			cardDisquiet.transform.localPosition = new Vector3(0f, 0f, 0f);
			cardDisquiet.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
			GameObject highlight = new GameObject("Highlight");
			cardBlueHighlight.transform.SetParent(highlight.transform, false);
			cardBlueHighlightSelect.transform.SetParent(highlight.transform, false);
			cardYellowHighlight.transform.SetParent(highlight.transform, false);
			cardYellowHighlightSelect.transform.SetParent(highlight.transform, false);
			highlight.transform.SetParent(this.manager.GetElement<Transform>("Turn").GetChild(1), false);
			Tools.ChangeMaterialRenderQueue(highlight, 3001);
			ElementObject e = cardParmUp.AddComponent<ElementObject>();
			e.label = "EffectBuff";
			ElementObject e2 = cardParmDown.AddComponent<ElementObject>();
			e2.label = "EffectDebuff";
			ElementObject e3 = cardParmChange.AddComponent<ElementObject>();
			e3.label = "EffectChange";
			ElementObject e4 = cardBuffActive.AddComponent<ElementObject>();
			e4.label = "EffectBuffActive";
			ElementObject e5 = cardNegate.AddComponent<ElementObject>();
			e5.label = "EffectNegate";
			ElementObject e6 = cardDisquiet.AddComponent<ElementObject>();
			e6.label = "EffectDisquiet";
			ElementObject e7 = cardBlueHighlight.AddComponent<ElementObject>();
			e7.label = "EffectHighlightBlue";
			ElementObject e8 = cardBlueHighlightSelect.AddComponent<ElementObject>();
			e8.label = "EffectHighlightBlueSelect";
			ElementObject e9 = cardYellowHighlight.AddComponent<ElementObject>();
			e9.label = "EffectHighlightYellow";
			ElementObject e10 = cardYellowHighlightSelect.AddComponent<ElementObject>();
			e10.label = "EffectHighlightYellowSelect";
			e7.transform.localScale = new Vector3(1.03f, 1f, 1.025f);
			e9.transform.localScale = new Vector3(1.02f, 1f, 1f);
			List<ElementObject> list = this.manager.serializedElements.ToList<ElementObject>();
			list.Add(e);
			list.Add(e2);
			list.Add(e3);
			list.Add(e4);
			list.Add(e5);
			list.Add(e6);
			list.Add(e7);
			list.Add(e8);
			list.Add(e9);
			list.Add(e10);
			this.manager.serializedElements = list.ToArray();
			cardParmUp.SetActive(false);
			cardParmDown.SetActive(false);
			cardParmChange.SetActive(false);
			cardBuffActive.SetActive(false);
			cardNegate.SetActive(false);
			cardDisquiet.SetActive(false);
			cardBlueHighlight.SetActive(false);
			cardBlueHighlightSelect.SetActive(false);
			cardYellowHighlight.SetActive(false);
			cardYellowHighlightSelect.SetActive(false);
			Renderer component = this.manager.GetElement<Transform>("CardModel").GetChild(0).GetComponent<Renderer>();
			component.material = ((this.p.controller == 0U) ? OcgCore.myProtector : OcgCore.opProtector);
			component.material.renderQueue = 3000;
			this.SetFace();
			this.model.transform.SetParent(Program.instance.ocgcore.GetFieldTransform(this.p.controller));
			if (real)
			{
				return this.model;
			}
			GameObject gameObject = this.model;
			this.model = null;
			return gameObject;
		}

		// Token: 0x06008899 RID: 34969 RVA: 0x000FF4CE File Offset: 0x000FD6CE
		private void SetFace()
		{
			if (this.cts != null)
			{
				this.cts.Cancel();
				this.cts.Dispose();
			}
			this.cts = new CancellationTokenSource();
			this.SetFaceAsync(this.cts.Token);
		}

		// Token: 0x0600889A RID: 34970 RVA: 0x000FF50C File Offset: 0x000FD70C
		private void ReloadFaceWhenConfigChange()
		{
			bool config = Config.GetBool("VideoCard", true);
			if (config && CardImageLoader.CardHasVideoArt(this.data.Id))
			{
				this.SetFace();
				return;
			}
			if (!config && this.isRenderTexture)
			{
				this.SetFace();
			}
		}

		// Token: 0x0600889B RID: 34971 RVA: 0x000FF554 File Offset: 0x000FD754
		private async UniTask SetFaceAsync(CancellationToken cancellationToken)
		{
			Renderer cardFace = this.manager.GetElement<Transform>("CardModel").GetChild(1).GetComponent<Renderer>();
			Material mat = MaterialLoader.GetCardMaterial(this.data.Id, true);
			if (!(this.model == null))
			{
				cardFace.material = mat;
				cardFace.material.renderQueue = 2999;
				Texture texture = await CardImageLoader.LoadCardAsync(this.data.Id, true, cancellationToken, false);
				this.isRenderTexture = texture is RenderTexture;
				if (!(this.model == null))
				{
					cardFace.material.mainTexture = texture;
					this.SetDisabled();
				}
			}
		}

		// Token: 0x0600889C RID: 34972 RVA: 0x000FF59F File Offset: 0x000FD79F
		public Card GetData()
		{
			return this.data;
		}

		// Token: 0x0600889D RID: 34973 RVA: 0x000FF5A7 File Offset: 0x000FD7A7
		public Card GetValidData()
		{
			if (this.data.Id > 0)
			{
				return this.data;
			}
			return this.lastValidData;
		}

		// Token: 0x0600889E RID: 34974 RVA: 0x000FF5C4 File Offset: 0x000FD7C4
		public void SetData(Card d)
		{
			if (d.Id > 0)
			{
				d.CloneTo(this.lastValidData);
			}
			else if (this.data.Id > 0)
			{
				this.data.CloneTo(this.lastValidData);
			}
			if (d.Attack < 0)
			{
				d.Attack = 0;
			}
			if (d.Defense < 0)
			{
				d.Defense = 0;
			}
			if (d.Id != this.data.Id)
			{
				this.data = d;
				if (this.model != null && this.data.Id > 0)
				{
					this.SetFace();
					this.ShowFaceDownCardOrNot(this.NeedShowFaceDownCard());
				}
			}
			this.data = d;
			this.RefreshLabel();
			this.UpdateExDeckTop();
		}

		// Token: 0x0600889F RID: 34975 RVA: 0x000FF684 File Offset: 0x000FD884
		public void SetCode(int code)
		{
			if (code > 0 && this.data.Id != code)
			{
				this.SetData(CardsManager.Get(code, false));
				this.data.Id = code;
				if (this.p.controller == 1U && OcgCore.condition == OcgCore.Condition.Duel && !OcgCore.sideReference.Main.Contains(code))
				{
					OcgCore.sideReference.Main.Add(code);
				}
			}
		}

		// Token: 0x060088A0 RID: 34976 RVA: 0x000FF6F4 File Offset: 0x000FD8F4
		public void RefreshData()
		{
			CardsManager.Get(this.data.Id, false).CloneTo(this.data);
			this.SetData(this.data);
			this.ClearAllTails();
		}

		// Token: 0x060088A1 RID: 34977 RVA: 0x000FF724 File Offset: 0x000FD924
		public void EraseData()
		{
			this.SetData(CardsManager.Get(0, false));
			this.Disabled = false;
			this.ClearAllTails();
		}

		// Token: 0x060088A2 RID: 34978 RVA: 0x000FF740 File Offset: 0x000FD940
		public void AddTarget(GameCard card)
		{
			if (!this.targets.Contains(card))
			{
				this.targets.Add(card);
			}
		}

		// Token: 0x060088A3 RID: 34979 RVA: 0x000FF75C File Offset: 0x000FD95C
		public void RemoveTarget(GameCard card)
		{
			this.targets.Remove(card);
		}

		// Token: 0x060088A4 RID: 34980 RVA: 0x000FF76B File Offset: 0x000FD96B
		public void AddEffectTarget(GameCard card)
		{
			if (!this.effectTargets.Contains(card))
			{
				this.effectTargets.Add(card);
			}
		}

		// Token: 0x060088A5 RID: 34981 RVA: 0x000FF787 File Offset: 0x000FD987
		public void RemoveEffectTarget(GameCard card)
		{
			this.effectTargets.Remove(card);
		}

		// Token: 0x060088A6 RID: 34982 RVA: 0x000FF798 File Offset: 0x000FD998
		public static Vector3 GetCardPosition(GPS p, GameCard c = null, GameCard overlayParent = null)
		{
			Vector3 returnValue = Vector3.zero;
			if (p.InLocation(CardLocation.Search))
			{
				return new Vector3(0f, -50f, 0f);
			}
			if (p.InLocation(CardLocation.Unknown))
			{
				return new Vector3(0f, 10f, 0f);
			}
			if (!p.InLocation(CardLocation.Hand))
			{
				if (p.InLocation(CardLocation.Deck))
				{
					if (p.controller == 0U)
					{
						returnValue = new Vector3(26.6f, 1.5f, -23.5f);
					}
					else
					{
						returnValue = new Vector3(-26.6f, 1.5f, 23.5f);
					}
					returnValue.y += p.sequence * 0.11f;
				}
				else if (p.InLocation(CardLocation.Extra))
				{
					if (p.InMyControl())
					{
						returnValue = new Vector3(-26.6f, 1.5f, -23.5f);
					}
					else
					{
						returnValue = new Vector3(26.6f, 1.5f, 23.5f);
					}
					returnValue.y += p.sequence * 0.11f;
				}
				else if (p.InLocation(CardLocation.Grave))
				{
					int offset = (p.InMyControl() ? OcgCore.movingToMyGrave : OcgCore.movingToOpGrave) - 1;
					if (offset < 0)
					{
						offset = 0;
					}
					if (p.InMyControl())
					{
						returnValue = new Vector3(25.74f - 3.75f * (float)offset, 5f + 0.1f * (float)offset, -14.26f);
					}
					else
					{
						returnValue = new Vector3(-25.74f + 3.75f * (float)offset, 5f + 0.1f * (float)offset, 14.26f);
					}
				}
				else if (p.InLocation(CardLocation.Removed))
				{
					int offset2 = (p.InMyControl() ? OcgCore.movingToMyExclude : OcgCore.movingToOpExclude) - 1;
					if (offset2 < 0)
					{
						offset2 = 0;
					}
					if (p.controller == 0U)
					{
						returnValue = new Vector3(27.582972f - 3.75f * (float)offset2, 5f + 0.1f * (float)offset2, -8.023989f);
					}
					else
					{
						returnValue = new Vector3(-27.582972f + 3.75f * (float)offset2, 5f + 0.1f * (float)offset2, 8.023989f);
					}
				}
				else if (p.InLocation(CardLocation.MonsterZone))
				{
					uint realIndex = p.sequence;
					if (p.controller == 0U)
					{
						realIndex = p.sequence;
						returnValue.y = 0.2f;
						returnValue.z = -9.48f;
					}
					else
					{
						if (realIndex <= 4U)
						{
							realIndex = 4U - p.sequence;
						}
						else if (realIndex == 5U)
						{
							realIndex = 6U;
						}
						else if (realIndex == 6U)
						{
							realIndex = 5U;
						}
						returnValue.y = 0.2f;
						returnValue.z = 9.51f;
					}
					switch (realIndex)
					{
					case 0U:
						returnValue.x = -17.2f;
						break;
					case 1U:
						returnValue.x = -8.6f;
						break;
					case 2U:
						returnValue.x = 0f;
						break;
					case 3U:
						returnValue.x = 8.6f;
						break;
					case 4U:
						returnValue.x = 17.2f;
						break;
					case 5U:
						returnValue.x = -8.6f;
						returnValue.z = 0f;
						break;
					case 6U:
						returnValue.x = 8.6f;
						returnValue.z = 0f;
						break;
					}
				}
				else if (p.InLocation(CardLocation.SpellZone))
				{
					if (p.sequence < 5U || ((p.sequence == 6U || p.sequence == 7U) && OcgCore.MasterRule >= 4))
					{
						uint realIndex2 = p.sequence;
						if (p.controller == 0U)
						{
							realIndex2 = p.sequence;
							returnValue.y = 0.2f;
							returnValue.z = -18f;
						}
						else
						{
							if (realIndex2 <= 4U)
							{
								realIndex2 = 4U - p.sequence;
							}
							else if (realIndex2 == 7U)
							{
								realIndex2 = 6U;
							}
							else if (realIndex2 == 6U)
							{
								realIndex2 = 7U;
							}
							returnValue.y = 0.2f;
							returnValue.z = 18f;
						}
						switch (realIndex2)
						{
						case 0U:
							returnValue.x = -17.2f;
							break;
						case 1U:
							returnValue.x = -8.6f;
							break;
						case 2U:
							returnValue.x = 0f;
							break;
						case 3U:
							returnValue.x = 8.6f;
							break;
						case 4U:
							returnValue.x = 17.2f;
							break;
						case 6U:
							returnValue.x = -8.6f;
							break;
						case 7U:
							returnValue.x = 8.6f;
							break;
						}
					}
					if (p.sequence == 5U)
					{
						if (p.controller == 0U)
						{
							returnValue = new Vector3(-25f, 0.1f, -10f);
						}
						else
						{
							returnValue = new Vector3(25f, 0.1f, 10f);
						}
					}
					if (OcgCore.MasterRule <= 3)
					{
						if (p.sequence == 6U)
						{
							if (p.controller == 0U)
							{
								returnValue = new Vector3(-30f, 10f, -15f);
							}
							else
							{
								returnValue = new Vector3(30f, 10f, 10f);
							}
						}
						if (p.sequence == 7U)
						{
							if (p.controller == 0U)
							{
								returnValue = new Vector3(30f, 10f, -15f);
							}
							else
							{
								returnValue = new Vector3(-30f, 10f, 10f);
							}
						}
					}
				}
				if (p.InLocation(CardLocation.Overlay))
				{
					if (overlayParent != null)
					{
						int pposition = overlayParent.overFatherCount - 1 - p.position;
						returnValue.y -= (float)(pposition + 2) * 0.02f;
						returnValue.x += (float)(pposition + 1) * 0.2f;
					}
					else
					{
						returnValue.y -= (float)(p.position + 2) * 0.02f;
						returnValue.x += (float)(p.position + 1) * 0.2f;
					}
				}
				return returnValue;
			}
			if (c == null)
			{
				return Vector3.zero;
			}
			int handsCount;
			if (c.p.InMyControl())
			{
				handsCount = Program.instance.ocgcore.GetMyHandCount();
			}
			else
			{
				handsCount = Program.instance.ocgcore.GetOpHandCount();
			}
			float x = (float)((ulong)(p.sequence * 4U) - (ulong)((long)((handsCount - 1) * 2)));
			if (p.controller == 0U)
			{
				float z = -28f + (30f - Program.instance.camera_.cameraMain.fieldOfView) * 0.7f;
				return new Vector3(x + OcgCore.handOffset * UIManager.ScreenLengthWithoutScalerX(0.038f), 15f, z);
			}
			float z2 = 17f - (30f - Program.instance.camera_.cameraMain.fieldOfView) * 0.7f;
			return new Vector3(-x, 15f, z2);
		}

		// Token: 0x060088A7 RID: 34983 RVA: 0x000FFE58 File Offset: 0x000FE058
		public static Vector3 GetCardRotation(GPS p, int code = 0)
		{
			GameCard.CardRuleCondition condition = GameCard.CardRuleCondition.MeUpAtk;
			if (p.InLocation(CardLocation.Deck))
			{
				if (((long)p.position & 5L) > 0L)
				{
					condition = GameCard.CardRuleCondition.MeUpDeck;
				}
				else
				{
					condition = GameCard.CardRuleCondition.MeDownDeck;
				}
			}
			else if (p.InLocation(CardLocation.Extra))
			{
				if (((long)p.position & 5L) > 0L)
				{
					condition = GameCard.CardRuleCondition.MeUpExDeck;
				}
				else
				{
					condition = GameCard.CardRuleCondition.MeDownExDeck;
				}
			}
			else if (p.InLocation(CardLocation.Grave))
			{
				if (((long)p.position & 5L) > 0L)
				{
					condition = GameCard.CardRuleCondition.MeUpGrave;
				}
				else
				{
					condition = GameCard.CardRuleCondition.MeDownGrave;
				}
			}
			else if (p.InLocation(CardLocation.Removed))
			{
				if (((long)p.position & 5L) > 0L)
				{
					condition = GameCard.CardRuleCondition.MeUpRemoved;
				}
				else
				{
					condition = GameCard.CardRuleCondition.MeDownRemoved;
				}
			}
			else if (p.InLocation(CardLocation.MonsterZone))
			{
				if (((long)p.position & 5L) > 0L)
				{
					if (((long)p.position & 3L) > 0L)
					{
						condition = GameCard.CardRuleCondition.MeUpAtk;
					}
					else
					{
						condition = GameCard.CardRuleCondition.MeUpDef;
					}
				}
				else if (((long)p.position & 3L) > 0L)
				{
					condition = GameCard.CardRuleCondition.MeDownAtk;
				}
				else
				{
					condition = GameCard.CardRuleCondition.MeDownDef;
				}
			}
			else if (p.InLocation(CardLocation.SpellZone))
			{
				if (((long)p.position & 5L) > 0L)
				{
					condition = GameCard.CardRuleCondition.MeUpAtk;
				}
				else
				{
					condition = GameCard.CardRuleCondition.MeDownAtk;
				}
			}
			else if (p.InLocation(CardLocation.Hand))
			{
				if (code != 0)
				{
					condition = GameCard.CardRuleCondition.MeUpHand;
				}
				else
				{
					condition = GameCard.CardRuleCondition.MeDownHand;
				}
			}
			if (p.InLocation(CardLocation.Overlay))
			{
				condition = GameCard.CardRuleCondition.MeUpAtk;
			}
			if (p.controller != 0U)
			{
				switch (condition)
				{
				case GameCard.CardRuleCondition.MeUpAtk:
					condition = GameCard.CardRuleCondition.OpUpAtk;
					break;
				case GameCard.CardRuleCondition.MeUpDef:
					condition = GameCard.CardRuleCondition.OpUpDef;
					break;
				case GameCard.CardRuleCondition.MeDownAtk:
					condition = GameCard.CardRuleCondition.OpDownAtk;
					break;
				case GameCard.CardRuleCondition.MeDownDef:
					condition = GameCard.CardRuleCondition.OpDownDef;
					break;
				case GameCard.CardRuleCondition.MeUpDeck:
					condition = GameCard.CardRuleCondition.OpUpDeck;
					break;
				case GameCard.CardRuleCondition.MeDownDeck:
					condition = GameCard.CardRuleCondition.OpDownDeck;
					break;
				case GameCard.CardRuleCondition.MeUpExDeck:
					condition = GameCard.CardRuleCondition.OpUpExDeck;
					break;
				case GameCard.CardRuleCondition.MeDownExDeck:
					condition = GameCard.CardRuleCondition.OpDownExDeck;
					break;
				case GameCard.CardRuleCondition.MeUpGrave:
					condition = GameCard.CardRuleCondition.OpUpGrave;
					break;
				case GameCard.CardRuleCondition.MeDownGrave:
					condition = GameCard.CardRuleCondition.OpDownGrave;
					break;
				case GameCard.CardRuleCondition.MeUpRemoved:
					condition = GameCard.CardRuleCondition.OpUpRemoved;
					break;
				case GameCard.CardRuleCondition.MeDownRemoved:
					condition = GameCard.CardRuleCondition.OpDownRemoved;
					break;
				case GameCard.CardRuleCondition.MeUpHand:
					condition = GameCard.CardRuleCondition.OpUpHand;
					break;
				case GameCard.CardRuleCondition.MeDownHand:
					condition = GameCard.CardRuleCondition.OpDownHand;
					break;
				}
			}
			Vector3 vector;
			switch (condition)
			{
			case GameCard.CardRuleCondition.MeUpAtk:
				vector = new Vector3(0f, 0f, 0f);
				break;
			case GameCard.CardRuleCondition.MeUpDef:
				vector = new Vector3(0f, 270f, 0f);
				break;
			case GameCard.CardRuleCondition.MeDownAtk:
				vector = new Vector3(0f, 0f, 180f);
				break;
			case GameCard.CardRuleCondition.MeDownDef:
				vector = new Vector3(0f, 270f, 180f);
				break;
			case GameCard.CardRuleCondition.OpUpAtk:
				vector = new Vector3(0f, 180f, 0f);
				break;
			case GameCard.CardRuleCondition.OpUpDef:
				vector = new Vector3(0f, 90f, 0f);
				break;
			case GameCard.CardRuleCondition.OpDownAtk:
				vector = new Vector3(0f, 180f, 180f);
				break;
			case GameCard.CardRuleCondition.OpDownDef:
				vector = new Vector3(0f, 90f, 180f);
				break;
			case GameCard.CardRuleCondition.MeUpDeck:
				vector = new Vector3(0f, -19.5f, 0f);
				break;
			case GameCard.CardRuleCondition.MeDownDeck:
				vector = new Vector3(0f, -19.5f, 180f);
				break;
			case GameCard.CardRuleCondition.OpUpDeck:
				vector = new Vector3(0f, 160.5f, 0f);
				break;
			case GameCard.CardRuleCondition.OpDownDeck:
				vector = new Vector3(0f, 160.5f, 180f);
				break;
			case GameCard.CardRuleCondition.MeUpExDeck:
				vector = new Vector3(0f, 19.5f, 0f);
				break;
			case GameCard.CardRuleCondition.MeDownExDeck:
				vector = new Vector3(0f, 19.5f, 180f);
				break;
			case GameCard.CardRuleCondition.OpUpExDeck:
				vector = new Vector3(0f, 199.5f, 0f);
				break;
			case GameCard.CardRuleCondition.OpDownExDeck:
				vector = new Vector3(0f, 199.5f, 180f);
				break;
			case GameCard.CardRuleCondition.MeUpGrave:
				vector = new Vector3(0f, 0f, 0f);
				break;
			case GameCard.CardRuleCondition.MeDownGrave:
				vector = new Vector3(0f, 270f, 0f);
				break;
			case GameCard.CardRuleCondition.OpUpGrave:
				vector = new Vector3(0f, 180f, 0f);
				break;
			case GameCard.CardRuleCondition.OpDownGrave:
				vector = new Vector3(0f, 180f, 180f);
				break;
			case GameCard.CardRuleCondition.MeUpRemoved:
				vector = new Vector3(0f, 90f, 0f);
				break;
			case GameCard.CardRuleCondition.MeDownRemoved:
				vector = new Vector3(0f, 90f, 180f);
				break;
			case GameCard.CardRuleCondition.OpUpRemoved:
				vector = new Vector3(0f, 270f, 0f);
				break;
			case GameCard.CardRuleCondition.OpDownRemoved:
				vector = new Vector3(0f, 270f, 180f);
				break;
			case GameCard.CardRuleCondition.MeUpHand:
				vector = new Vector3(-20f, 0f, 0f);
				break;
			case GameCard.CardRuleCondition.MeDownHand:
				vector = new Vector3(-20f, 0f, 180f);
				break;
			case GameCard.CardRuleCondition.OpUpHand:
				vector = new Vector3(20f, 180f, 0f);
				break;
			case GameCard.CardRuleCondition.OpDownHand:
				vector = new Vector3(20f, 180f, 180f);
				break;
			default:
				vector = Vector3.zero;
				break;
			}
			return vector;
		}

		// Token: 0x060088A8 RID: 34984 RVA: 0x00100388 File Offset: 0x000FE588
		public static Vector3 GetEffectRotaion(GPS p)
		{
			if (p.InMyControl())
			{
				if (p.InPosition(CardPosition.Attack))
				{
					return new Vector3(0f, 0f, 0f);
				}
				return new Vector3(0f, 270f, 0f);
			}
			else
			{
				if (p.InPosition(CardPosition.Attack))
				{
					return new Vector3(0f, 180f, 0f);
				}
				return new Vector3(0f, 90f, 0f);
			}
		}

		// Token: 0x060088A9 RID: 34985 RVA: 0x00100404 File Offset: 0x000FE604
		public static Vector3 GetCardScale(GPS p)
		{
			if (p.InLocation(CardLocation.SpellZone))
			{
				return new Vector3(0.8f, 1f, 0.8f);
			}
			if (p.InLocation(CardLocation.Deck, CardLocation.Extra))
			{
				return new Vector3(0.9f, 1f, 0.9f);
			}
			return Vector3.one;
		}

		// Token: 0x060088AA RID: 34986 RVA: 0x00100454 File Offset: 0x000FE654
		private bool ThisLocationShouldHaveModel(GPS p)
		{
			return p.InLocation(CardLocation.Hand) || (!p.InLocation(CardLocation.Overlay) && p.InLocation(CardLocation.Onfield));
		}

		// Token: 0x060088AB RID: 34987 RVA: 0x0010047D File Offset: 0x000FE67D
		public bool InPendulumZone()
		{
			return GameCard.InPendulumZoneIf(this.p, this.data.Id);
		}

		// Token: 0x060088AC RID: 34988 RVA: 0x00100498 File Offset: 0x000FE698
		public static bool InPendulumZoneIf(GPS p, int code)
		{
			Card data = CardsManager.Get(code, false);
			if ((p.location & 8U) == 0U)
			{
				return false;
			}
			if (OcgCore.MasterRule > 3)
			{
				if (p.sequence != 0U && p.sequence != 4U)
				{
					return false;
				}
			}
			else if (p.sequence != 6U && p.sequence != 7U)
			{
				return false;
			}
			return data.HasType(CardType.Pendulum) && ((long)p.position & 10L) <= 0L;
		}

		// Token: 0x060088AD RID: 34989 RVA: 0x0010050C File Offset: 0x000FE70C
		public void UpdateExDeckTop()
		{
			uint player = this.p.controller;
			if (this.cacheP != null)
			{
				player = this.cacheP.controller;
			}
			if (this.p.InLocation(CardLocation.Extra) || (this.cacheP != null && this.cacheP.InLocation(CardLocation.Extra)))
			{
				Program.instance.ocgcore.UpdateExDeckTop(player);
			}
		}

		// Token: 0x060088AE RID: 34990 RVA: 0x00100570 File Offset: 0x000FE770
		public async UniTask MoveAsync(GPS gps, bool rush = false, float wait = 0f, float overrideMoveTime = 0f)
		{
			OcgCore.lastMoveCard = this;
			if (this.p.location != gps.location || gps.InPosition(CardPosition.FaceDown))
			{
				this.targets.Clear();
				this.equipedCard = null;
				foreach (GameCard gameCard in OcgCore.cards)
				{
					gameCard.RemoveTarget(this);
				}
				this.Disabled = false;
				this.setOverTurn = false;
				this.RefreshData();
			}
			this.overlays = Program.instance.ocgcore.GCS_GetOverlays(this);
			this.cacheP = this.p;
			this.p = gps;
			if (this.p.InLocation(CardLocation.Hand) && this.p.InMyControl())
			{
				this.p.position = 1;
			}
			if (!this.SemiNomiSummoned && CardsManager.Get(this.data.Id, false).HasType(CardType.Monster) && (CardsManager.Get(this.data.Id, false).Type & 109060288) > 0 && this.p.InLocation(CardLocation.Grave, CardLocation.Removed))
			{
				this.AddStringTail(InterString.Get("未正规登场", 0));
			}
			else
			{
				this.RemoveStringTail(InterString.Get("未正规登场", 0), true);
			}
			for (int i = 0; i < this.overlays.Count; i++)
			{
				this.overlays[i].overlayParent = this;
				this.overlays[i].p.controller = gps.controller;
				this.overlays[i].p.location = gps.location | 128U;
				this.overlays[i].p.sequence = gps.sequence;
				this.overlays[i].p.position = i;
			}
			Program.instance.ocgcore.ArrangeCards();
			if (OcgCore.currentMessage == GameMessage.Move && this.cacheP.location != this.p.location && this.p.IsReason(CardReason.MATERIAL) && !this.cacheP.InLocation(CardLocation.Overlay))
			{
				OcgCore.materialCards.Add(this);
			}
			if (this.ThisLocationShouldHaveModel(this.p) || this.cacheP.location != this.p.location)
			{
				if (!this.cacheP.InLocation(CardLocation.Overlay) || (!this.p.IsReason(CardReason.RULE) && !this.p.InLocation(CardLocation.Extra)))
				{
					float moveTime = 0.3f;
					if (rush)
					{
						if (this.ThisLocationShouldHaveModel(this.p) && this.model == null)
						{
							this.CreateModel(true);
							this.ModelAt(this.p, null);
							this.ShowFaceDownCardOrNot(this.NeedShowFaceDownCard());
							if (this.IsFaceDownOnSpellZone())
							{
								this.setOverTurn = true;
							}
							if (this.p.InLocation(CardLocation.Hand))
							{
								if (this.p.InMyControl())
								{
									OcgCore.needRefreshMyHand = true;
								}
								else
								{
									OcgCore.needRefreshOpHand = true;
								}
							}
						}
					}
					else
					{
						if (!this.ThisLocationShouldHaveModel(this.cacheP) && this.model != null)
						{
							global::UnityEngine.Object.Destroy(this.model, 1f);
							this.model = null;
						}
						if (this.model == null)
						{
							this.CreateModel(true);
							if (this.cacheP.InLocation(CardLocation.Deck))
							{
								this.cacheP.position = 2;
							}
							this.model.transform.SetParent(Program.instance.ocgcore.GetFieldTransform(this.p.controller));
							this.ModelAt(this.cacheP, null);
						}
						if (OcgCore.nextMoveAction != null)
						{
							this.model.SetActive(false);
							int code = this.data.Id;
							if (code == 0)
							{
								code = Program.instance.ocgcore.GetNextConfirmedCardCode();
								this.SetCode(code);
							}
							OcgCore.nextMoveAction(code);
							await UniTask.WaitForSeconds(OcgCore.nextMoveActionDuration, false, PlayerLoopTiming.Update, default(CancellationToken), false);
						}
						else
						{
							this.inAnimation = true;
							OcgCore.needRefreshMyHand = true;
							OcgCore.needRefreshOpHand = true;
							string se = string.Empty;
							Sequence sequence = DOTween.Sequence();
							float timePassed = 0f;
							if (this.p.InLocation(CardLocation.Grave, CardLocation.Removed))
							{
								if (this.p.InLocation(CardLocation.Grave))
								{
									if (this.p.InMyControl())
									{
										OcgCore.movingToMyGrave++;
									}
									else
									{
										OcgCore.movingToOpGrave++;
									}
								}
								else if (this.p.InMyControl())
								{
									OcgCore.movingToMyExclude++;
								}
								else
								{
									OcgCore.movingToOpExclude++;
								}
							}
							Vector3 position = GameCard.GetCardPosition(this.p, this, null);
							Vector3 rotation = GameCard.GetCardRotation(this.p, this.data.Id);
							bool handAppeal = false;
							float curveHeight = 20f;
							Ease ease = Ease.Unset;
							if (overrideMoveTime > 0f)
							{
								moveTime = overrideMoveTime;
							}
							else
							{
								GameMessage currentMessage = OcgCore.currentMessage;
								if (currentMessage <= GameMessage.PosChange)
								{
									if (currentMessage != GameMessage.ShuffleSetCard)
									{
										if (currentMessage != GameMessage.Move)
										{
											if (currentMessage != GameMessage.PosChange)
											{
												goto IL_074B;
											}
										}
										else
										{
											moveTime = 0.4f;
											if (this.p.InLocation(CardLocation.Hand) && this.p.InMyControl())
											{
												moveTime = 0.5f;
												handAppeal = true;
											}
											if (this.cacheP != null && !this.cacheP.InLocation(CardLocation.Onfield) && this.p.InLocation(CardLocation.Onfield))
											{
												curveHeight = 40f;
											}
											if (this.p.InLocation(CardLocation.Overlay))
											{
												curveHeight = 20f;
												goto IL_074B;
											}
											goto IL_074B;
										}
									}
								}
								else if (currentMessage != GameMessage.Swap && currentMessage != GameMessage.FlipSummoning)
								{
									if (currentMessage != GameMessage.Draw)
									{
										goto IL_074B;
									}
									curveHeight = 5f;
									if (this.p.controller == 0U)
									{
										moveTime = 0.5f;
										handAppeal = true;
										goto IL_074B;
									}
									moveTime = 0.25f;
									goto IL_074B;
								}
								curveHeight = 10f;
								moveTime = 0.2f;
							}
							IL_074B:
							if (this.cacheP.InLocation(CardLocation.Grave, CardLocation.Removed))
							{
								timePassed += this.SequenceFromGrave(sequence, this.cacheP);
							}
							if (this.cacheP.InLocation(CardLocation.Deck) && this.p.InLocation(CardLocation.Hand))
							{
								se = "SE_CARD_MOVE_0" + global::UnityEngine.Random.Range(1, 5).ToString();
							}
							if (this.p.IsReason(CardReason.DESTROY) && this.model != null && this.cacheP.InLocation(CardLocation.Onfield, CardLocation.Hand))
							{
								moveTime = 0.4f;
								se = "SE_CARDBREAK_01";
								if (!this.data.HasType(CardType.Token))
								{
									string breakEffectPath = "fxp_cardbrk_bff_001";
									string trail1Path = "fxp_grave_brksol_trail_001";
									string trail2Path = "fxp_grave_ReCard_move_001";
									if (this.p.InLocation(CardLocation.Removed))
									{
										breakEffectPath = "fxp_exclude_001";
										trail1Path = "fxp_exclude_brksol_trail_001";
										trail2Path = "fxp_exclude_ReCard_move_001";
									}
									GameObject gameObject = ABLoader.LoadMasterDuelGameObject(breakEffectPath);
									gameObject.transform.position = this.model.transform.position;
									global::UnityEngine.Object.Destroy(gameObject, 3f);
									this.manager.GetElement<Transform>("CardPlane").localScale = Vector3.zero;
									GameObject gameObject2 = ABLoader.LoadMasterDuelGameObject(trail1Path);
									GameObject trail2 = ABLoader.LoadMasterDuelGameObject(trail2Path);
									gameObject2.transform.SetParent(this.model.transform, false);
									trail2.transform.SetParent(this.model.transform, false);
									global::UnityEngine.Object.Destroy(gameObject2, 3f);
									global::UnityEngine.Object.Destroy(trail2, 3f);
								}
							}
							if (this.cacheP.InLocation(CardLocation.MonsterZone) && this.p.InLocation(CardLocation.Overlay) && this.p.InLocation(CardLocation.Extra))
							{
								AudioManager.PlaySE(se, 1f);
								AudioManager.PlaySE("SE_SUMMON_XYZ_MATERIAL", 1f);
								GameObject fx = ABLoader.LoadMasterDuelGameObject("XYZTrailFieldCard01");
								fx.transform.localPosition = this.model.transform.position;
								fx.transform.localEulerAngles = GameCard.GetEffectRotaion(this.cacheP);
								fx.GetComponent<ElementObjectManager>().GetNestedElement<MeshRenderer>("DummyCard01/DummyCardModel_front").material = this.GetMaterial();
								if (this.cacheP.InPosition(CardPosition.Defence))
								{
									fx.transform.eulerAngles = new Vector3(0f, 90f, 0f);
								}
								global::UnityEngine.Object.Destroy(this.model);
								fx.GetComponent<PlayableDirector>().AutoDestroy(true);
								await UniTask.WaitForSeconds(0.2f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
							}
							else
							{
								if (this.ThisLocationShouldHaveModel(this.cacheP) && this.p.IsReason(CardReason.MATERIAL) && !this.p.InLocation(CardLocation.Onfield) && (this.p.reason & 270270464U) > 0U)
								{
									AudioManager.PlaySE(se, 1f);
									string se2 = string.Empty;
									string trail3 = string.Empty;
									if (this.p.IsReason(CardReason.Ritual))
									{
										se2 = "SE_SUMMON_RITUAL_MATERIAL";
										trail3 = "RitualTrailFieldCard01";
									}
									else if (this.p.IsReason(CardReason.Fusion))
									{
										se2 = "SE_SUMMON_FUS_MATERIAL";
										trail3 = "FusionTrailFieldCard01";
									}
									else if (this.p.IsReason(CardReason.Synchro))
									{
										se2 = "SE_SUMMON_SYNC_MATERIAL";
										trail3 = ((!this.GetData().HasType(CardType.Tuner)) ? "Synchro01TrailFieldCard01" : "Synchro00TrailFieldCard01");
									}
									else if (this.p.IsReason(CardReason.Link))
									{
										se2 = "SE_SUMMON_LINK_MATERIAL";
										trail3 = "LinkTrailFieldCard01";
									}
									AudioManager.PlaySE(se2, 1f);
									GameObject gameObject3 = ABLoader.LoadMasterDuelGameObject(trail3);
									gameObject3.transform.localPosition = this.model.transform.position;
									gameObject3.transform.localEulerAngles = GameCard.GetEffectRotaion(this.cacheP);
									gameObject3.GetComponent<ElementObjectManager>().GetNestedElement<MeshRenderer>("DummyCard01/DummyCardModel_front").material = this.GetMaterial();
									if (this.p.InLocation(CardLocation.Extra) && !this.p.InLocation(CardLocation.Overlay) && this.p.InPosition(CardPosition.FaceUp))
									{
										Program.instance.ocgcore.SetExDeckTop(this);
									}
									gameObject3.GetComponent<PlayableDirector>().AutoDestroy(true);
								}
								if (this.cacheP.location == 0U)
								{
									if (this.data.HasType(CardType.Token))
									{
										AudioManager.PlaySE("SE_CARD_TOKEN_SUMMON", 1f);
										GameObject gameObject4 = ABLoader.LoadMasterDuelGameObject("SummonToken01");
										gameObject4.transform.position = GameCard.GetCardPosition(this.p, null, null);
										gameObject4.transform.localEulerAngles = GameCard.GetEffectRotaion(this.p);
										this.ModelAt(this.p, null);
										this.model.SetActive(false);
										ElementObjectManager tokenManager = gameObject4.GetComponent<ElementObjectManager>();
										Program.instance.texture_.LoadDummyCard(tokenManager.GetElement<ElementObjectManager>("DummyCard01"), this.data.Id, this.cacheP.controller, false, null, null);
										UniTask.WaitForSeconds(1.25f, false, PlayerLoopTiming.Update, default(CancellationToken), false).ContinueWith(delegate
										{
											this.model.SetActive(true);
										});
										global::UnityEngine.Object.Destroy(gameObject4, 2f);
										await UniTask.WaitForSeconds(0.5f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
										return;
									}
									this.model.SetActive(false);
									GameObject fx2 = await ABLoader.LoadFromFolderAsync<PlayableDirector>("MasterDuel/Timeline/Summon/CardCheat/CardCheatAppear", true, true, null);
									fx2.transform.position = new Vector3(0f, 48.6f, -17.7f);
									fx2.transform.localEulerAngles = new Vector3(-20f, 0f, 0f);
									AudioManager.PlaySE("SE_CARDCHEAT_APPEAR", 1f);
									this.model.transform.position = fx2.transform.position;
									this.model.transform.localEulerAngles = fx2.transform.localEulerAngles;
									sequence.Pause<Sequence>();
									if (this.data.Id == 0)
									{
										this.SetCode(Program.instance.ocgcore.GetUpdateDataIdByGameCard(this));
									}
									ElementObjectManager fxManager = fx2.GetComponent<ElementObjectManager>();
									Program.instance.texture_.LoadDummyCard(fxManager.GetElement<ElementObjectManager>("DummyCard01"), this.data.Id, this.cacheP.controller, false, fxManager.GetElement<Renderer>("DummyCardModel_front02"), fxManager.GetElement<Renderer>("DummyCardModel_front03"));
									overrideMoveTime = 0.4f;
									await UniTask.WaitForSeconds(1.25f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
									global::UnityEngine.Object.Destroy(fx2);
									this.model.SetActive(true);
									fx2 = null;
								}
								if (this.p.location == 0U)
								{
									AudioManager.PlaySE("SE_CARD_TOKEN_BREAK", 1f);
									GameObject gameObject5 = ABLoader.LoadMasterDuelGameObject("fxp_bff_tokese_001");
									gameObject5.transform.position = this.model.transform.position;
									gameObject5.transform.localEulerAngles = GameCard.GetEffectRotaion(this.p);
									global::UnityEngine.Object.Destroy(this.model);
									global::UnityEngine.Object.Destroy(gameObject5, 3f);
									await UniTask.WaitForSeconds(0.2f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
								}
								else
								{
									if ((this.p.reason & 2U) > 0U && this.model != null)
									{
										se = "SE_SUMMON_ADVANCE";
										GameObject gameObject6 = ABLoader.LoadMasterDuelGameObject("fxp_sacrifice_rls_001");
										gameObject6.transform.position = this.model.transform.position;
										global::UnityEngine.Object.Destroy(gameObject6, 5f);
									}
									if (this.p.IsReason(CardReason.SPSUMMON) && this.p.InLocation(CardLocation.MonsterZone) && !this.cacheP.InLocation(CardLocation.MonsterZone) && !this.p.InLocation(CardLocation.Overlay))
									{
										bool needSummonEffect = true;
										if (OcgCore.condition == OcgCore.Condition.Duel && !Config.GetBool("DuelSummon", true))
										{
											needSummonEffect = false;
										}
										if (OcgCore.condition == OcgCore.Condition.Watch && !Config.GetBool("WatchSummon", true))
										{
											needSummonEffect = false;
										}
										if (OcgCore.condition == OcgCore.Condition.Replay && !Config.GetBool("ReplaySummon", true))
										{
											needSummonEffect = false;
										}
										if (needSummonEffect && OcgCore.materialCards.Count > 0 && (OcgCore.TypeMatchReason(this.data.Type, (int)OcgCore.materialCards[0].p.reason) || OcgCore.TypeMatchReason(this.data.Type, OcgCore.materialCards[0].GetData().Reason)))
										{
											Program.instance.ocgcore.GetUI<OcgCoreUI>().CardDescription.Hide();
											Program.instance.ocgcore.GetUI<OcgCoreUI>().CardList.Hide();
											AudioManager.PlaySE(se, 1f);
											OcgCore.summonCard = this;
											await TimelineHelper.PlaySummonTimelineAsync();
										}
										else
										{
											AudioManager.PlaySE(se, 1f);
											bool flag = CutinViewer.HasCutin(this.data.Id);
											sequence.Pause<Sequence>();
											if (flag)
											{
												if (this.cacheP.InLocation(CardLocation.Grave, CardLocation.Removed))
												{
													GameObject gameObject7 = this.model;
													if (gameObject7 != null)
													{
														gameObject7.SetActive(false);
													}
												}
												await CutinViewer.Play(this.data.Id, (int)this.p.controller);
											}
											if (this.data.IsHighLevel())
											{
												await this.SequenceStrongSummon(sequence, position, rotation, 0f, timePassed).WaitAsync(default(CancellationToken));
											}
											else
											{
												await this.SequenceNormalSummon(sequence, position, rotation, 0f, timePassed).WaitAsync(default(CancellationToken));
											}
										}
									}
									else if (this.p.InPosition(CardPosition.FaceUp) && this.p.InLocation(CardLocation.MonsterZone) && this.cacheP.InLocation(CardLocation.Hand) && !this.p.InLocation(CardLocation.Overlay))
									{
										AudioManager.PlaySE(se, 1f);
										sequence.Pause<Sequence>();
										if (CutinViewer.HasCutin(this.data.Id))
										{
											await CutinViewer.Play(this.data.Id, (int)this.p.controller);
										}
										if (this.data.IsHighLevel())
										{
											await this.SequenceStrongSummon(sequence, position, rotation, 0f, timePassed).WaitAsync(default(CancellationToken));
										}
										else
										{
											await this.SequenceNormalSummon(sequence, position, rotation, 0f, timePassed).WaitAsync(default(CancellationToken));
										}
									}
									else
									{
										Transform cardPlane = this.manager.GetElement<Transform>("CardPlane");
										Transform pivot = this.manager.GetElement<Transform>("Pivot");
										Transform offset = this.manager.GetElement<Transform>("Offset");
										Transform turn = this.manager.GetElement<Transform>("Turn");
										sequence.AppendInterval(wait);
										if (handAppeal)
										{
											ease = Ease.OutCubic;
										}
										sequence.Append(this.model.transform.DOPath(this.GenerateCurvePath(this.model.transform.position, position, curveHeight), moveTime, PathType.Linear, PathMode.Full3D, 10, null).OnStart(delegate
										{
											this.MoveStartAction();
										}));
										sequence.Join(this.model.transform.DOLocalRotate(Vector3.zero, moveTime, RotateMode.Fast));
										sequence.Join(pivot.DOScale(GameCard.GetCardScale(this.p), moveTime * 0.95f));
										if (this.p.InLocation(CardLocation.Removed) || this.p.InLocation(CardLocation.Deck) || this.p.InLocation(CardLocation.Extra))
										{
											sequence.Join(turn.DOLocalRotate(new Vector3(0f, 0f, rotation.z), moveTime * 0.6f, RotateMode.Fast));
										}
										else
										{
											sequence.Join(turn.DOLocalRotate(new Vector3(0f, (float)((rotation.y == 0f || rotation.y == 180f) ? 0 : 270), rotation.z), moveTime * 0.6f, RotateMode.Fast).SetEase(ease));
										}
										if (handAppeal && overrideMoveTime == 0f)
										{
											sequence.Join(turn.DOLocalMove(new Vector3(0f, 0f, 10f), moveTime, false).SetEase(Ease.OutCubic).OnComplete(delegate
											{
												turn.DOLocalMove(Vector3.zero, 0.2f, false).SetEase(Ease.InCubic);
											}));
										}
										if (this.p.InLocation(CardLocation.Deck) || this.p.InLocation(CardLocation.Extra) || this.p.InLocation(CardLocation.Removed))
										{
											sequence.Join(cardPlane.DOLocalRotate(new Vector3(rotation.x, rotation.y, 0f), moveTime * 0.5f, RotateMode.Fast));
										}
										else
										{
											sequence.Join(cardPlane.DOLocalRotate(new Vector3(rotation.x, (float)((rotation.y == 0f || rotation.y == 270f) ? 0 : 180), 0f), moveTime * 0.5f, RotateMode.Fast));
										}
										if (this.p.InLocation(CardLocation.Hand))
										{
											sequence.Join(pivot.DOLocalMove(new Vector3(0f, 0f, this.HandOffsetPositionByX(position.x)), moveTime / 4f, false));
											sequence.Join(offset.DOLocalRotate(new Vector3(0f, this.HandOffsetRotationByX(position.x), GameCard.handAngle), moveTime / 4f, RotateMode.Fast));
											this.handDefault = true;
										}
										else
										{
											sequence.Join(offset.DOLocalMove(Vector3.zero, moveTime / 4f, false));
											sequence.Join(offset.DOLocalRotate(Vector3.zero, 0.21f, RotateMode.Fast));
											sequence.Join(pivot.DOLocalMove(Vector3.zero, moveTime / 4f, false));
											sequence.Join(pivot.DOLocalRotate(Vector3.zero, moveTime / 4f, RotateMode.Fast));
										}
										if (this.p.InLocation(CardLocation.Grave, CardLocation.Removed))
										{
											if (this.p.InLocation(CardLocation.Grave))
											{
												timePassed += this.SequenceToGrave(sequence, this.p);
											}
											else
											{
												timePassed += this.SequenceToExclude(sequence, this.p);
											}
										}
										if (this.cacheP.InLocation(CardLocation.Overlay) && (this.p.IsReason(CardReason.EFFECT) || !this.p.InLocation(CardLocation.Overlay)) && !this.p.IsReason(CardReason.RULE))
										{
											se = "SE_CARD_XYZ_OUT";
											GameObject gameObject8 = ABLoader.LoadMasterDuelGameObject("fxp_bff_overlay_out_001");
											gameObject8.transform.position = GameCard.GetCardPosition(this.cacheP, null, null);
											global::UnityEngine.Object.Destroy(gameObject8, 3f);
											ABLoader.LoadMasterDuelGameObject("fxp_bff_overlay_trail_001").transform.SetParent(this.model.transform, false);
										}
										if (!this.cacheP.InLocation(CardLocation.Overlay) && !this.p.InLocation(CardLocation.Extra) && this.p.InLocation(CardLocation.Overlay) && this.p.IsReason(CardReason.Xyz))
										{
											se = string.Empty;
											DOTween.To(delegate(float v)
											{
											}, 0f, 0f, moveTime + timePassed).OnComplete(delegate
											{
												AudioManager.PlaySE("SE_CARD_XYZ_IN", 1f);
												GameObject gameObject9 = ABLoader.LoadMasterDuelGameObject("fxp_bff_overlay_in_001");
												gameObject9.transform.position = GameCard.GetCardPosition(this.p, null, null);
												global::UnityEngine.Object.Destroy(gameObject9, 3f);
											});
											ABLoader.LoadMasterDuelGameObject("fxp_bff_overlay_trail_001").transform.SetParent(this.model.transform, false);
										}
										if (handAppeal)
										{
											sequence.AppendInterval(0.2f);
										}
										sequence.OnComplete(delegate
										{
											this.MoveEndAction();
										});
										AudioManager.PlaySE(se, 1f);
										sequence.Play<Sequence>();
										await sequence.WaitAsync(default(CancellationToken));
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060088AF RID: 34991 RVA: 0x001005D4 File Offset: 0x000FE7D4
		private Vector3[] GenerateCurvePath(Vector3 start, Vector3 end, float curveHeight = 40f)
		{
			float endBias = 0.9f;
			Vector3 controlPoint = start * (1f - endBias) + end * endBias + Vector3.up * curveHeight;
			Vector3[] path = new Vector3[10];
			for (int i = 0; i < 10; i++)
			{
				float mappedT = Mathf.Pow((float)i / 9f, 0.5f);
				path[i] = this.CalculateBezierPoint(start, controlPoint, end, mappedT);
			}
			return path;
		}

		// Token: 0x060088B0 RID: 34992 RVA: 0x00100650 File Offset: 0x000FE850
		private Vector3 CalculateBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			float u = 1f - t;
			return u * u * p0 + 2f * u * t * p1 + t * t * p2;
		}

		// Token: 0x060088B1 RID: 34993 RVA: 0x00100694 File Offset: 0x000FE894
		private void MoveStartAction()
		{
			if (this.cacheP != null && this.cacheP.InLocation(CardLocation.Extra))
			{
				this.UpdateExDeckTop();
			}
			if ((this.cacheP != null && this.cacheP.InLocation(CardLocation.Hand)) || this.p.InLocation(CardLocation.Hand))
			{
				Program.instance.ocgcore.RefreshHandCardPosition();
			}
			if (this.cacheP != null && this.cacheP.InLocation(CardLocation.Deck, CardLocation.Extra))
			{
				Program.instance.ocgcore.DuelBGManager.ResizeDecks();
			}
			if (this.cacheP != null && this.cacheP.InLocation(CardLocation.Grave, CardLocation.Removed))
			{
				Program.instance.ocgcore.DuelBGManager.RefreshGravesState();
			}
			if (((long)this.p.position & 10L) > 0L || (this.p.location & 4U) == 0U)
			{
				this.HideLabel();
			}
		}

		// Token: 0x060088B2 RID: 34994 RVA: 0x00100774 File Offset: 0x000FE974
		private void MoveEndAction()
		{
			this.inAnimation = false;
			if (!this.ThisLocationShouldHaveModel(this.p) && this.model != null)
			{
				global::UnityEngine.Object.Destroy(this.model);
			}
			else
			{
				this.manager.GetElement<Transform>("CardPlane").localScale = Vector3.one;
			}
			if (this.p.InLocation(CardLocation.Extra) && !this.p.InLocation(CardLocation.Overlay) && this.p.InPosition(CardPosition.FaceUp))
			{
				Program.instance.ocgcore.SetExDeckTop(this);
			}
			this.ShowFaceDownCardOrNot(this.NeedShowFaceDownCard());
			if (this.p.InLocation(CardLocation.Deck, CardLocation.Extra))
			{
				Program.instance.ocgcore.DuelBGManager.ResizeDecks();
			}
			if (this.p.InLocation(CardLocation.Grave, CardLocation.Removed))
			{
				Program.instance.ocgcore.DuelBGManager.RefreshGravesState();
			}
		}

		// Token: 0x060088B3 RID: 34995 RVA: 0x00100860 File Offset: 0x000FEA60
		public Sequence StartCardSequence(Vector3 fromPosition, Vector3 fromRotation, float interval = 0f)
		{
			if (this.model == null)
			{
				return null;
			}
			this.ResetModelPositon();
			this.model.transform.localPosition = fromPosition;
			this.model.transform.eulerAngles = fromRotation;
			Vector3 position = GameCard.GetCardPosition(this.p, null, null);
			Vector3 rotaion = GameCard.GetCardRotation(this.p, 0);
			Sequence sequence = DOTween.Sequence();
			if (this.data.IsHighLevel())
			{
				return this.SequenceStrongSummon(sequence, position, rotaion, interval, 0f);
			}
			return this.SequenceNormalSummon(sequence, position, rotaion, interval, 0f);
		}

		// Token: 0x060088B4 RID: 34996 RVA: 0x001008F4 File Offset: 0x000FEAF4
		private Sequence SequenceStrongSummon(Sequence sequence, Vector3 position, Vector3 angle, float interval, float timePassed)
		{
			Transform cardPlane = this.manager.GetElement<Transform>("CardPlane");
			Transform pivot = this.manager.GetElement<Transform>("Pivot");
			Transform offset = this.manager.GetElement<Transform>("Offset");
			Transform turn = this.manager.GetElement<Transform>("Turn");
			sequence.AppendInterval(interval);
			sequence.AppendCallback(delegate
			{
				this.UpdateExDeckTop();
			});
			Vector3 midP = position;
			midP.y = 15f;
			sequence.Append(this.manager.transform.DOMove(midP, 0.4f, false).SetEase(Ease.InOutSine).OnStart(delegate
			{
				this.MoveStartAction();
			}));
			sequence.Join(this.manager.transform.DOLocalRotate(Vector3.zero, 0.4f, RotateMode.Fast).SetEase(Ease.InOutSine));
			sequence.Join(cardPlane.DOLocalRotate(new Vector3(0f, (float)((angle.y == 0f || angle.y == 270f) ? 0 : 180), 0f), 0.4f, RotateMode.Fast).SetEase(Ease.InOutSine));
			sequence.Join(pivot.DOScale(GameCard.GetCardScale(this.p), 0.16f).SetEase(Ease.InOutSine));
			sequence.Join(pivot.DOLocalMove(Vector3.zero, 0.16f, false).SetEase(Ease.InOutSine));
			sequence.Join(pivot.DOLocalRotate(Vector3.zero, 0.16f, RotateMode.Fast).SetEase(Ease.InOutSine));
			sequence.Join(offset.DOLocalMove(Vector3.zero, 0.4f, false).SetEase(Ease.InOutSine));
			sequence.Join(offset.DOLocalRotate(Vector3.zero, 0.4f, RotateMode.Fast).SetEase(Ease.InOutSine));
			sequence.Join(turn.DOLocalRotate(new Vector3(0f, (float)((angle.y == 0f || angle.y == 180f) ? 0 : 270), angle.z), 0.2f, RotateMode.Fast).SetEase(Ease.InOutSine));
			sequence.AppendInterval(0.26f);
			sequence.Insert(timePassed + interval + 0.26f, pivot.DOLocalMoveY(10f, 0.4f, false).SetEase(Ease.InOutQuart));
			sequence.Insert(timePassed + interval + 0.26f, pivot.DOLocalRotate(new Vector3(-35f, 0f, 0f), 0.4f, RotateMode.Fast).SetEase(Ease.InOutQuart));
			sequence.Append(pivot.DOLocalRotate(Vector3.zero, 0.14f, RotateMode.Fast).SetEase(Ease.InQuart));
			sequence.Join(pivot.DOLocalMoveY(0f, 0.14f, false).SetEase(Ease.InQuart));
			sequence.Join(this.manager.transform.DOMove(position, 0.14f, false).SetEase(Ease.InQuart));
			sequence.OnComplete(delegate
			{
				this.MoveEndAction();
			});
			return sequence;
		}

		// Token: 0x060088B5 RID: 34997 RVA: 0x00100BE8 File Offset: 0x000FEDE8
		private Sequence SequenceNormalSummon(Sequence sequence, Vector3 position, Vector3 angle, float interval, float timePassed)
		{
			Transform cardPlane = this.manager.GetElement<Transform>("CardPlane");
			Transform pivot = this.manager.GetElement<Transform>("Pivot");
			Transform offset = this.manager.GetElement<Transform>("Offset");
			Transform turn = this.manager.GetElement<Transform>("Turn");
			Vector3 midP = position;
			midP.y = 10f;
			sequence.AppendInterval(interval);
			sequence.Append(this.manager.transform.DOMove(midP, 0.3f, false).SetEase(Ease.InOutSine).OnStart(delegate
			{
				this.MoveStartAction();
			}));
			sequence.Join(this.manager.transform.DOLocalRotate(Vector3.zero, 0.3f, RotateMode.Fast).SetEase(Ease.InOutSine));
			sequence.Join(cardPlane.DOLocalRotate(new Vector3(0f, (float)((angle.y == 0f || angle.y == 270f) ? 0 : 180), 0f), 0.3f, RotateMode.Fast).SetEase(Ease.InOutSine));
			sequence.Join(pivot.DOScale(GameCard.GetCardScale(this.p), 0.16f).SetEase(Ease.InOutSine));
			sequence.Join(pivot.DOLocalMove(Vector3.zero, 0.16f, false).SetEase(Ease.InOutSine));
			sequence.Join(pivot.DOLocalRotate(Vector3.zero, 0.16f, RotateMode.Fast).SetEase(Ease.InOutSine));
			sequence.Join(offset.DOLocalMove(Vector3.zero, 0.3f, false).SetEase(Ease.InOutSine));
			sequence.Join(offset.DOLocalRotate(Vector3.zero, 0.3f, RotateMode.Fast).SetEase(Ease.InOutSine));
			sequence.Join(turn.DOLocalRotate(new Vector3(0f, (float)((angle.y == 0f || angle.y == 180f) ? 0 : 270), angle.z), 0.1f, RotateMode.Fast).SetEase(Ease.InOutSine));
			sequence.AppendInterval(0.16f);
			sequence.Insert(timePassed + interval + 0.16f, pivot.DOLocalMoveY(5f, 0.3f, false).SetEase(Ease.InOutQuart));
			sequence.Insert(timePassed + interval + 0.16f, pivot.DOLocalRotate(new Vector3(-15f, 0f, 0f), 0.3f, RotateMode.Fast).SetEase(Ease.InOutQuart));
			sequence.Append(pivot.DOLocalRotate(Vector3.zero, 0.14f, RotateMode.Fast).SetEase(Ease.InQuart));
			sequence.Join(pivot.DOLocalMoveY(0f, 0.14f, false).SetEase(Ease.InQuart));
			sequence.Join(this.manager.transform.DOMove(position, 0.14f, false).SetEase(Ease.InQuart));
			sequence.OnComplete(delegate
			{
				this.MoveEndAction();
			});
			return sequence;
		}

		// Token: 0x060088B6 RID: 34998 RVA: 0x00100EC8 File Offset: 0x000FF0C8
		private float SequenceFromGrave(Sequence sequence, GPS p)
		{
			GameObject dummy = ABLoader.LoadMasterDuelGameObject(p.InLocation(CardLocation.Grave) ? "DuelFromGrave01" : "DuelFromExclude01");
			dummy.transform.position = GameCard.GetCardPosition(p, null, null);
			dummy.transform.eulerAngles = GameCard.GetCardRotation(p, 0);
			dummy.SetActive(false);
			float time = 0.5833333f;
			ElementObjectManager dummyManager = dummy.GetComponent<ElementObjectManager>();
			Program.instance.texture_.LoadDummyCard(dummyManager.GetElement<ElementObjectManager>("DummyCard01"), this.data.Id, p.controller, false, null, null);
			sequence.AppendCallback(delegate
			{
				GameObject gameObject = this.model;
				if (gameObject != null)
				{
					gameObject.SetActive(false);
				}
				dummy.SetActive(true);
				Program.instance.ocgcore.PlayGraveEffect(p, false);
			});
			sequence.AppendInterval(time);
			sequence.AppendCallback(delegate
			{
				GameObject gameObject2 = this.model;
				if (gameObject2 != null)
				{
					gameObject2.SetActive(true);
				}
				global::UnityEngine.Object.Destroy(dummy);
			});
			return time;
		}

		// Token: 0x060088B7 RID: 34999 RVA: 0x00100FCC File Offset: 0x000FF1CC
		private float SequenceToGrave(Sequence sequence, GPS p)
		{
			int count = (p.InMyControl() ? OcgCore.movingToMyGrave : OcgCore.movingToOpGrave);
			if (count > 5)
			{
				Debug.Log("movingToGrave Over 5");
				count = 5;
			}
			if (count < 1)
			{
				Debug.Log("movingToGrave Less than 1");
				count = 1;
			}
			GameObject dummy = ABLoader.LoadMasterDuelGameObject("DuelToGrave0" + count.ToString());
			dummy.transform.position = GameCard.GetCardPosition(p, null, null);
			dummy.transform.eulerAngles = GameCard.GetCardRotation(p, 0);
			float time = 0.5f;
			dummy.SetActive(false);
			ElementObjectManager dummyManager = dummy.GetComponent<ElementObjectManager>();
			Program.instance.texture_.LoadDummyCard(dummyManager.GetElement<ElementObjectManager>("DummyCard01"), this.data.Id, p.controller, false, null, null);
			sequence.AppendCallback(delegate
			{
				global::UnityEngine.Object.Destroy(this.model);
				dummy.SetActive(true);
				if (!OcgCore.NextMessageIsMovingToGrave(p.controller))
				{
					Program.instance.ocgcore.PlayGraveEffect(p, false);
				}
				if (p.InMyControl())
				{
					OcgCore.movingToMyGrave--;
					return;
				}
				OcgCore.movingToOpGrave--;
			});
			sequence.AppendInterval(time);
			sequence.AppendCallback(delegate
			{
				global::UnityEngine.Object.Destroy(dummy);
			});
			return time;
		}

		// Token: 0x060088B8 RID: 35000 RVA: 0x00101100 File Offset: 0x000FF300
		private float SequenceToExclude(Sequence sequence, GPS p)
		{
			int count = (p.InMyControl() ? OcgCore.movingToMyExclude : OcgCore.movingToOpExclude);
			if (count > 5)
			{
				Debug.Log("movingToExclude Over 5");
				count = 5;
			}
			if (count < 1)
			{
				Debug.Log("movingToExclude Less than 1");
				count = 1;
			}
			GameObject dummy = ABLoader.LoadMasterDuelGameObject("DuelToExclude0" + count.ToString());
			dummy.transform.position = GameCard.GetCardPosition(p, null, null);
			dummy.transform.eulerAngles = GameCard.GetCardRotation(p, 0);
			float time = 0.5f;
			dummy.SetActive(false);
			ElementObjectManager dummyManager = dummy.GetComponent<ElementObjectManager>();
			Program.instance.texture_.LoadDummyCard(dummyManager.GetElement<ElementObjectManager>("DummyCard01"), this.data.Id, p.controller, false, null, null);
			sequence.AppendCallback(delegate
			{
				global::UnityEngine.Object.Destroy(this.model);
				dummy.SetActive(true);
				if (!OcgCore.NextMessageIsMovingToExclude(p.controller))
				{
					Program.instance.ocgcore.PlayGraveEffect(p, false);
				}
				if (p.InMyControl())
				{
					OcgCore.movingToMyExclude--;
					return;
				}
				OcgCore.movingToOpExclude--;
			});
			sequence.AppendInterval(time);
			sequence.AppendCallback(delegate
			{
				global::UnityEngine.Object.Destroy(dummy);
			});
			return time;
		}

		// Token: 0x060088B9 RID: 35001 RVA: 0x00101234 File Offset: 0x000FF434
		private void ResetModelPositon()
		{
			if (this.model == null)
			{
				return;
			}
			this.model.transform.localEulerAngles = Vector3.zero;
			this.manager.GetElement<Transform>("CardPlane").localEulerAngles = Vector3.zero;
			this.manager.GetElement<Transform>("CardPlane").localPosition = Vector3.zero;
			this.manager.GetElement<Transform>("Pivot").localScale = GameCard.GetCardScale(this.p);
			this.manager.GetElement<Transform>("Pivot").localEulerAngles = Vector3.zero;
			this.manager.GetElement<Transform>("Pivot").localPosition = Vector3.zero;
			this.manager.GetElement<Transform>("Offset").localEulerAngles = Vector3.zero;
			this.manager.GetElement<Transform>("Offset").localPosition = Vector3.zero;
			this.manager.GetElement<Transform>("Turn").localEulerAngles = Vector3.zero;
			this.manager.GetElement<Transform>("Turn").localPosition = Vector3.zero;
		}

		// Token: 0x060088BA RID: 35002 RVA: 0x00101358 File Offset: 0x000FF558
		public void ResetModelRotation()
		{
			if (this.model == null)
			{
				return;
			}
			this.manager.transform.localEulerAngles = Vector3.zero;
			this.manager.GetElement<Transform>("CardPlane").localEulerAngles = Vector3.zero;
			this.manager.GetElement<Transform>("Pivot").localEulerAngles = Vector3.zero;
			this.manager.GetElement<Transform>("Offset").localEulerAngles = Vector3.zero;
			this.manager.GetElement<Transform>("Turn").localEulerAngles = Vector3.zero;
			this.manager.GetElement<Transform>("CardModel").localEulerAngles = Vector3.zero;
		}

		// Token: 0x060088BB RID: 35003 RVA: 0x0010140C File Offset: 0x000FF60C
		private void ModelAt(GPS gps, GameObject model = null)
		{
			ElementObjectManager manager;
			if (model == null)
			{
				model = this.model;
				manager = model.GetComponent<ElementObjectManager>();
			}
			else
			{
				manager = this.manager;
			}
			model.transform.localPosition = GameCard.GetCardPosition(gps, this, null);
			Vector3 rotation = GameCard.GetCardRotation(gps, this.data.Id);
			Transform cardPlane = manager.GetElement<Transform>("CardPlane");
			if ((gps.location & 1U) > 0U || (gps.location & 64U) > 0U || (gps.location & 32U) > 0U)
			{
				cardPlane.localEulerAngles = new Vector3(rotation.x, rotation.y, 0f);
			}
			else
			{
				cardPlane.localEulerAngles = new Vector3(rotation.x, (float)((rotation.y == 0f || rotation.y == 270f) ? 0 : 180), 0f);
			}
			manager.GetElement<Transform>("Pivot").localScale = GameCard.GetCardScale(this.p);
			if ((rotation.y == 90f || rotation.y == 270f) && (gps.location & 32U) == 0U)
			{
				manager.GetElement<Transform>("Turn").localEulerAngles = new Vector3(0f, 270f, rotation.z);
				return;
			}
			manager.GetElement<Transform>("Turn").localEulerAngles = new Vector3(0f, 0f, rotation.z);
		}

		// Token: 0x060088BC RID: 35004 RVA: 0x00101570 File Offset: 0x000FF770
		public void AnimationShuffle(float shuffleTime)
		{
			this.inAnimation = true;
			if (!this.p.InLocation(CardLocation.Hand))
			{
				return;
			}
			if (this.model != null)
			{
				this.manager.GetElement<Transform>("Pivot").DOLocalMoveZ(0f, shuffleTime, false);
				this.manager.GetElement<Transform>("Offset").DOLocalRotate(Vector3.zero, shuffleTime, RotateMode.Fast);
				this.manager.GetElement<Transform>("Turn").DOLocalRotate(new Vector3(0f, 0f, 180f), shuffleTime, RotateMode.Fast);
				this.model.transform.DOLocalMoveX(0f, shuffleTime, false).OnComplete(delegate
				{
					if (OcgCore.cards.Contains(this))
					{
						this.AnimationHandDefault(shuffleTime, true);
						return;
					}
					this.Dispose();
				});
				return;
			}
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, shuffleTime).OnComplete(delegate
			{
				this.CreateModel(true);
				this.ModelAt(this.p, null);
				this.manager.GetElement<Transform>("Pivot").localPosition = Vector3.zero;
				this.manager.GetElement<Transform>("Offset").localEulerAngles = Vector3.zero;
				this.manager.GetElement<Transform>("Turn").localEulerAngles = new Vector3(0f, 0f, 180f);
				Vector3 position = this.model.transform.localPosition;
				this.model.transform.localPosition = new Vector3(0f, position.y, position.z);
				this.AnimationHandDefault(shuffleTime, true);
			});
		}

		// Token: 0x060088BD RID: 35005 RVA: 0x001016A4 File Offset: 0x000FF8A4
		public float HandOffsetRotationByX(float x)
		{
			float abs = ((x > 0f) ? x : (-x));
			return x * (abs * -0.006f + 1.2f) * (float)((this.p.controller == 0U) ? 1 : (-1));
		}

		// Token: 0x060088BE RID: 35006 RVA: 0x001016E4 File Offset: 0x000FF8E4
		public float HandOffsetPositionByX(float x)
		{
			float abs = ((x > 0f) ? x : (-x));
			return -abs * (abs * 0.0055f + 0.08f);
		}

		// Token: 0x060088BF RID: 35007 RVA: 0x00101710 File Offset: 0x000FF910
		public void AnimationHandDefault(float time, bool ignore = false)
		{
			if (this.model == null || !this.p.InLocation(CardLocation.Hand) || (this.inAnimation && !ignore))
			{
				return;
			}
			this.model.transform.SetParent(null, true);
			this.handDefault = true;
			this.appealed = false;
			this.MoveToHandDefault(time);
			float x = GameCard.GetCardPosition(this.p, this, null).x;
			Transform element = this.manager.GetElement<Transform>("Pivot");
			Transform offset = this.manager.GetElement<Transform>("Offset");
			Transform turn = this.manager.GetElement<Transform>("Turn");
			element.DOLocalMove(new Vector3(0f, 0f, this.HandOffsetPositionByX(x)), time, false).OnComplete(delegate
			{
				if (ignore)
				{
					this.inAnimation = false;
				}
			});
			offset.DOLocalMove(Vector3.zero, time, false);
			offset.DOLocalRotate(new Vector3(0f, this.HandOffsetRotationByX(x), GameCard.handAngle), time, RotateMode.Fast);
			if (this.data.Id == 0)
			{
				turn.DOLocalRotate(new Vector3(0f, 0f, 180f), time, RotateMode.Fast);
				return;
			}
			turn.DOLocalRotate(Vector3.zero, time, RotateMode.Fast);
		}

		// Token: 0x060088C0 RID: 35008 RVA: 0x00101860 File Offset: 0x000FFA60
		private void MoveToHandDefault(float time)
		{
			Vector3 targetPosition = GameCard.GetCardPosition(this.p, this, null);
			if (OcgCore.HideMyHandCard && this.p.InMyControl())
			{
				targetPosition.z = -28f;
			}
			if (OcgCore.HideOpHandCard && !this.p.InMyControl())
			{
				targetPosition.z = 17f;
			}
			this.model.transform.DOLocalMove(targetPosition, time, false);
		}

		// Token: 0x060088C1 RID: 35009 RVA: 0x001018CF File Offset: 0x000FFACF
		public void SetHandToDefault()
		{
			if (this.model == null || !this.p.InLocation(CardLocation.Hand) || this.inAnimation)
			{
				return;
			}
			this.clicked = false;
			this.handDefault = false;
		}

		// Token: 0x060088C2 RID: 35010 RVA: 0x00101904 File Offset: 0x000FFB04
		public void SetHandDefault()
		{
			if (this.model == null || !this.p.InLocation(CardLocation.Hand))
			{
				return;
			}
			this.appealed = false;
			this.model.transform.localPosition = GameCard.GetCardPosition(this.p, this, null);
			float x = this.model.transform.localPosition.x;
			this.manager.GetElement<Transform>("Pivot").localPosition = new Vector3(0f, 0f, this.HandOffsetPositionByX(x));
			this.manager.GetElement<Transform>("Offset").localPosition = Vector3.zero;
			this.manager.GetElement<Transform>("Offset").localEulerAngles = new Vector3(0f, this.HandOffsetRotationByX(x), GameCard.handAngle);
			this.manager.GetElement<Transform>("Turn").localEulerAngles = new Vector3(0f, 0f, (float)((this.data.Id == 0) ? 180 : 0));
		}

		// Token: 0x060088C3 RID: 35011 RVA: 0x00101A12 File Offset: 0x000FFC12
		private void AnimationHandHover()
		{
			if (this.inAnimation)
			{
				return;
			}
			this.manager.GetElement<Transform>("Offset").DOLocalMove(new Vector3(0f, 2f, 1f), 0.1f, false);
		}

		// Token: 0x060088C4 RID: 35012 RVA: 0x00101A50 File Offset: 0x000FFC50
		public void AnimationHandAppeal()
		{
			if (this.appealed || this.inAnimation)
			{
				return;
			}
			this.appealed = true;
			this.manager.GetElement<Transform>("Pivot").DOLocalMove(new Vector3(0f, 2f, 3f), 0.1f, false);
			this.manager.GetElement<Transform>("Offset").DOLocalRotate(Vector3.zero, 0.1f, RotateMode.Fast);
			this.manager.GetElement<Transform>("Offset").DOLocalMove(Vector3.zero, 0.1f, false);
			AudioManager.PlaySE("SE_CARD_MOVE_0" + global::UnityEngine.Random.Range(1, 5).ToString(), 1f);
		}

		// Token: 0x060088C5 RID: 35013 RVA: 0x00101B0C File Offset: 0x000FFD0C
		public Sequence AnimationNegate()
		{
			Action nextNegateAction = OcgCore.nextNegateAction;
			if (nextNegateAction != null)
			{
				nextNegateAction();
			}
			OcgCore.nextNegateAction = null;
			AudioManager.PlaySE("SE_EFFECT_INVALID", 1f);
			CameraManager.BlackInOut(0f, 0.1f, 0.7f, 0.2f);
			GameObject model;
			ElementObjectManager manager;
			if (this.ThisLocationShouldHaveModel(this.p))
			{
				model = this.model;
				manager = this.manager;
			}
			else
			{
				model = this.CreateModel(false);
				this.ModelAt(this.p, model);
				manager = model.GetComponent<ElementObjectManager>();
			}
			Renderer cardFace = manager.GetElement<Transform>("CardModel").GetChild(1).GetComponent<Renderer>();
			float originMono = cardFace.material.GetFloat("_Monochrome");
			Tools.ChangeLayer(model, "DuelOverlay3D", false);
			CameraManager.DuelOverlay3DPlus();
			manager.GetElement("EffectNegate").SetActive(false);
			manager.GetElement("EffectNegate").SetActive(true);
			Transform pivot = manager.GetElement<Transform>("Pivot");
			Transform offset = manager.GetElement<Transform>("Offset");
			Vector3 scale = pivot.localScale;
			bool additional = false;
			if (OcgCore.nextNegateAction_Additional != null)
			{
				Action nextNegateAction_Additional = OcgCore.nextNegateAction_Additional;
				if (nextNegateAction_Additional != null)
				{
					nextNegateAction_Additional();
				}
				OcgCore.nextNegateAction_AdditionalManager.transform.SetParent(offset, false);
				OcgCore.nextNegateAction_Additional = null;
				additional = true;
			}
			float showTime = 0.7f;
			if (additional)
			{
				showTime += OcgCore.nextNegateAction_AdditionalTime;
			}
			Sequence sequence = DOTween.Sequence();
			if (this.p.InLocation(CardLocation.Deck, CardLocation.Extra, CardLocation.Onfield))
			{
				this.HideLabel();
				float height = 10f;
				if (this.p.InLocation(CardLocation.Deck, CardLocation.Extra))
				{
					height = 5f;
				}
				sequence.Append(offset.DOLocalMoveY(height, 0.1f, false));
				sequence.Join(DOTween.To(() => originMono, delegate(float x)
				{
					cardFace.material.SetFloat("_Monochrome", x);
				}, 1f, 0.1f));
				sequence.Join(pivot.DOScale(1f, 0.1f));
				sequence.AppendInterval(showTime);
				sequence.Append(offset.DOLocalMoveY(0f, 0.2f, false));
				if (this.Disabled)
				{
					originMono = 1f;
				}
				if (originMono != 1f)
				{
					sequence.Join(DOTween.To(() => 1f, delegate(float x)
					{
						cardFace.material.SetFloat("_Monochrome", x);
					}, originMono, 0.2f));
				}
				sequence.Join(pivot.DOScale(scale, 0.2f));
				sequence.OnComplete(delegate
				{
					Tools.ChangeLayer(model, "Default", false);
					CameraManager.DuelOverlay3DMinus();
					this.RefreshLabel();
					if (!this.ThisLocationShouldHaveModel(this.p))
					{
						global::UnityEngine.Object.Destroy(model);
					}
				});
			}
			else if ((this.p.location & 2U) > 0U)
			{
				this.inAnimation = true;
				if (this.p.controller != 0U)
				{
					manager.GetElement<Transform>("Turn").DOLocalRotate(Vector3.zero, 0.1f, RotateMode.Fast);
				}
				Vector3 originRotaion = pivot.localEulerAngles;
				sequence.Append(offset.DOLocalMoveY(1f, 0.1f, false));
				sequence.Join(offset.DOLocalMoveZ(5f, 0.1f, false));
				sequence.Join(offset.DOLocalRotate(Vector3.zero, 0.1f, RotateMode.Fast));
				sequence.Join(pivot.DOLocalRotate(Vector3.zero, 0.1f, RotateMode.Fast));
				sequence.Join(manager.GetElement<Transform>("Turn").DOLocalRotate(Vector3.zero, 0.1f, RotateMode.Fast));
				sequence.Join(DOTween.To(() => originMono, delegate(float x)
				{
					cardFace.material.SetFloat("_Monochrome", x);
				}, 1f, 0.1f));
				sequence.Append(offset.DOLocalMoveY(1.2f, showTime, false));
				sequence.Join(offset.DOLocalMoveZ(5.5f, showTime, false));
				sequence.Append(offset.DOLocalMoveY(0f, 0.2f, false));
				sequence.Join(offset.DOLocalMoveZ(0f, 0.2f, false));
				sequence.Join(pivot.DOLocalRotate(originRotaion, 0.15f, RotateMode.Fast));
				sequence.Join(DOTween.To(() => 1, delegate(int x)
				{
					cardFace.material.SetFloat("_Monochrome", (float)x);
				}, 0, 0.2f));
				sequence.Insert(0f, pivot.DOScale(1.2f, 0.2f));
				sequence.Insert(showTime + 0.1f, pivot.DOScale(scale, 0.2f));
				sequence.OnComplete(delegate
				{
					Tools.ChangeLayer(model, "Default", false);
					CameraManager.DuelOverlay3DMinus();
					this.inAnimation = false;
					cardFace.material.SetFloat("_Monochrome", originMono);
				});
			}
			else if ((this.p.location & 16U) > 0U || (this.p.location & 32U) > 0U)
			{
				offset.localPosition = new Vector3(0f, -2f, 0f);
				sequence.Append(offset.DOLocalMoveY(0f, 0.1f, false));
				sequence.Join(DOTween.To(() => originMono, delegate(float x)
				{
					cardFace.material.SetFloat("_Monochrome", x);
				}, 1f, 0.1f));
				sequence.Join(offset.DOScale(1f, 0.1f));
				sequence.AppendInterval(showTime);
				sequence.Append(offset.DOLocalMoveY(-2f, 0.2f, false));
				sequence.Join(DOTween.To(() => 1, delegate(int x)
				{
					cardFace.material.SetFloat("_Monochrome", (float)x);
				}, 0, 0.2f));
				sequence.Join(offset.DOScale(Vector3.one * 0.5f, 0.2f));
				sequence.OnComplete(delegate
				{
					global::UnityEngine.Object.Destroy(model);
					CameraManager.DuelOverlay3DMinus();
					cardFace.material.SetFloat("_Monochrome", originMono);
				});
			}
			return sequence;
		}

		// Token: 0x060088C6 RID: 35014 RVA: 0x0010211C File Offset: 0x0010031C
		public Sequence AnimationActivate()
		{
			AudioManager.PlaySE("SE_CARDVIEW_01", 1f);
			CameraManager.BlackInOut(0f, 0.3f, 0.4f, 0.3f);
			GameObject model;
			ElementObjectManager manager;
			if (this.ThisLocationShouldHaveModel(this.p))
			{
				model = this.model;
				manager = this.manager;
			}
			else
			{
				model = this.CreateModel(false);
				this.ModelAt(this.p, model);
				manager = model.GetComponent<ElementObjectManager>();
			}
			Tools.ChangeLayer(model, "DuelOverlay3D", false);
			CameraManager.DuelOverlay3DPlus();
			manager.GetElement("EffectBuffActive").SetActive(false);
			manager.GetElement("EffectBuffActive").SetActive(true);
			Transform pivot = manager.GetElement<Transform>("Pivot");
			Transform offset = manager.GetElement<Transform>("Offset");
			Vector3 scale = pivot.localScale;
			Sequence sequence = DOTween.Sequence();
			Ease ease = Ease.OutCubic;
			if (this.p.InLocation(CardLocation.Deck, CardLocation.Extra, CardLocation.Onfield))
			{
				this.HideLabel();
				float height = 10f;
				if (this.p.InLocation(CardLocation.Deck, CardLocation.Extra))
				{
					height = 5f;
				}
				sequence.Append(offset.DOLocalMoveY(height, 0.2f, false).SetEase(ease));
				sequence.Join(pivot.DOScale(1f, 0.2f).SetEase(ease));
				sequence.Append(offset.DOLocalMoveY(height * 1.1f, 0.7f, false).SetEase(ease));
				sequence.Append(offset.DOLocalMoveY(0f, 0.2f, false));
				sequence.Join(pivot.DOScale(scale, 0.2f));
				sequence.OnComplete(delegate
				{
					Tools.ChangeLayer(model, "Default", false);
					CameraManager.DuelOverlay3DMinus();
					this.RefreshLabel();
					if (!this.ThisLocationShouldHaveModel(this.p))
					{
						global::UnityEngine.Object.Destroy(model);
					}
				});
			}
			else if ((this.p.location & 2U) > 0U)
			{
				this.inAnimation = true;
				if (this.p.controller != 0U)
				{
					manager.GetElement<Transform>("Turn").DOLocalRotate(Vector3.zero, 0.1f, RotateMode.Fast);
				}
				Vector3 originRotaion = pivot.localEulerAngles;
				sequence.Append(offset.DOLocalMoveY(1f, 0.2f, false).SetEase(ease));
				sequence.Join(offset.DOLocalMoveZ(5f, 0.2f, false).SetEase(ease));
				sequence.Join(offset.DOLocalRotate(Vector3.zero, 0.2f, RotateMode.Fast).SetEase(ease));
				sequence.Join(pivot.DOLocalRotate(Vector3.zero, 0.2f, RotateMode.Fast).SetEase(ease));
				sequence.Join(manager.GetElement<Transform>("Turn").DOLocalRotate(Vector3.zero, 0.2f, RotateMode.Fast).SetEase(ease));
				sequence.Append(offset.DOLocalMoveY(1.1f, 0.7f, false));
				sequence.Join(offset.DOLocalMoveZ(5.1f, 0.7f, false));
				sequence.Append(offset.DOLocalMoveY(0f, 0.2f, false));
				sequence.Join(offset.DOLocalMoveZ(0f, 0.2f, false));
				sequence.Join(pivot.DOLocalRotate(originRotaion, 0.2f, RotateMode.Fast));
				sequence.Insert(0f, pivot.DOScale(1.1f, 0.15f).SetEase(ease));
				sequence.Insert(0.9f, pivot.DOScale(scale, 0.2f));
				sequence.OnComplete(delegate
				{
					Tools.ChangeLayer(model, "Default", false);
					CameraManager.DuelOverlay3DMinus();
					this.inAnimation = false;
				});
			}
			else if ((this.p.location & 16U) > 0U || (this.p.location & 32U) > 0U)
			{
				offset.localPosition = new Vector3(0f, -1f, 0f);
				offset.localScale = Vector3.one * 0.5f;
				sequence.Append(offset.DOLocalMoveY(5f, 0.2f, false).SetEase(ease));
				sequence.Join(offset.DOScale(1f, 0.2f).SetEase(ease));
				sequence.Append(offset.DOLocalMoveY(5.5f, 0.7f, false).SetEase(ease));
				sequence.Append(offset.DOLocalMoveY(-1f, 0.2f, false));
				sequence.Join(offset.DOScale(0.5f, 0.2f));
				sequence.OnComplete(delegate
				{
					global::UnityEngine.Object.Destroy(model);
					CameraManager.DuelOverlay3DMinus();
				});
			}
			return sequence;
		}

		// Token: 0x060088C7 RID: 35015 RVA: 0x001025B0 File Offset: 0x001007B0
		public Sequence AnimationConfirm(int id)
		{
			if (!this.ThisLocationShouldHaveModel(this.p))
			{
				this.CreateModel(true);
				this.ModelAt(this.p, null);
				this.model.SetActive(false);
			}
			this.inAnimation = true;
			Transform offset = this.manager.GetElement<Transform>("Offset");
			Vector3 offsetPosition = offset.localPosition;
			Transform turn = this.manager.GetElement<Transform>("Turn");
			Vector3 turnEulerAngles = turn.localEulerAngles;
			Sequence sequence = DOTween.Sequence();
			if (id > 0)
			{
				sequence.AppendInterval((float)id);
			}
			Vector3 endPosition = new Vector3(0f, 2f, 3f);
			if (this.p.InLocation(CardLocation.Grave, CardLocation.Removed))
			{
				endPosition.z = 0f;
			}
			sequence.Append(offset.DOLocalMove(endPosition, 0.1f, false).OnStart(delegate
			{
				this.model.SetActive(true);
				this.ShowFaceDownCardOrNot(false);
				AudioManager.PlaySE("SE_CARDVIEW_02", 1f);
				if (Program.instance.ocgcore.GetAutoInfo())
				{
					Program.instance.ocgcore.GetUI<OcgCoreUI>().CardDescription.Show(this, null, -1, null);
				}
			}));
			sequence.Join(turn.DOLocalRotate(Vector3.zero, 0.1f, RotateMode.Fast).OnComplete(delegate
			{
				GameObject gameObject = ABLoader.LoadMasterDuelGameObject("fxp_card_decide_001");
				gameObject.transform.SetPositionAndRotation(offset.position, offset.rotation);
				gameObject.transform.localScale = GameCard.GetCardScale(this.p);
				global::UnityEngine.Object.Destroy(gameObject, 1f);
			}));
			sequence.AppendInterval(0.8f);
			TweenCallback <>9__3;
			sequence.AppendCallback(delegate
			{
				this.inAnimation = false;
				if ((this.p.location & 2U) > 0U)
				{
					this.SetHandToDefault();
				}
				else
				{
					offset.DOLocalMove(offsetPosition, 0.1f, false);
					TweenerCore<Quaternion, Vector3, QuaternionOptions> tweenerCore = turn.DOLocalRotate(turnEulerAngles, 0.1f, RotateMode.Fast);
					TweenCallback tweenCallback;
					if ((tweenCallback = <>9__3) == null)
					{
						tweenCallback = (<>9__3 = delegate
						{
							if (!this.ThisLocationShouldHaveModel(this.p) && this.model != null)
							{
								global::UnityEngine.Object.Destroy(this.model);
							}
						});
					}
					tweenerCore.OnComplete(tweenCallback);
				}
				this.ShowFaceDownCardOrNot(this.NeedShowFaceDownCard());
			});
			return sequence;
		}

		// Token: 0x060088C8 RID: 35016 RVA: 0x0010270C File Offset: 0x0010090C
		public void AnimationPositon(float delay = 0f)
		{
			if (this.model == null)
			{
				return;
			}
			ElementObjectManager positionManager = this.manager.GetElement<ElementObjectManager>("FieldCardChangeIcon");
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(delay);
			if (((long)this.p.position & 3L) > 0L)
			{
				AudioManager.PlaySE("SE_DISP_ATTACK", 0.6f);
				positionManager.GetElement<Transform>("Sword1").localEulerAngles = new Vector3(90f, 0f, 0f);
				positionManager.GetElement<Transform>("Sword2").localEulerAngles = new Vector3(90f, 0f, 0f);
				sequence.Append(positionManager.GetElement<Transform>("Sword1").DOLocalRotate(new Vector3(90f, 0f, 60f), 0.2f, RotateMode.Fast).SetEase(Ease.OutCubic));
				sequence.Join(positionManager.GetElement<Transform>("Sword2").DOLocalRotate(new Vector3(90f, 0f, -60f), 0.2f, RotateMode.Fast).SetEase(Ease.OutCubic));
				sequence.Join(positionManager.GetElement<SpriteRenderer>("Sword1").DOFade(1f, 0.35f).SetEase(Ease.OutCubic));
				sequence.Join(positionManager.GetElement<SpriteRenderer>("Sword2").DOFade(1f, 0.35f).SetEase(Ease.OutCubic));
				sequence.Insert(0.2f, positionManager.GetElement<Transform>("Sword1").DOLocalRotate(new Vector3(90f, 0f, 45f), 0.15f, RotateMode.Fast).SetEase(Ease.OutCubic));
				sequence.Insert(0.2f, positionManager.GetElement<Transform>("Sword2").DOLocalRotate(new Vector3(90f, 0f, -45f), 0.15f, RotateMode.Fast).SetEase(Ease.OutCubic));
				sequence.AppendInterval(0.2f);
				sequence.Append(positionManager.GetElement<SpriteRenderer>("Sword1").DOFade(0f, 0.3f).SetEase(Ease.InQuad));
				sequence.Join(positionManager.GetElement<SpriteRenderer>("Sword2").DOFade(0f, 0.3f).SetEase(Ease.InQuad));
				return;
			}
			AudioManager.PlaySE("SE_DISP_DEFENS", 0.6f);
			positionManager.GetElement<Transform>("Defense").localScale = new Vector3(3f, 3f, 3f);
			sequence.Append(positionManager.GetElement<Transform>("Defense").DOScale(new Vector3(3.8f, 3.8f, 3.8f), 0.35f));
			sequence.Join(positionManager.GetElement<SpriteRenderer>("Defense").DOFade(1f, 0.3f).SetEase(Ease.OutCubic));
			sequence.AppendInterval(0.2f);
			sequence.Append(positionManager.GetElement<SpriteRenderer>("Defense").DOFade(0f, 0.3f).SetEase(Ease.InCubic));
		}

		// Token: 0x060088C9 RID: 35017 RVA: 0x00102A08 File Offset: 0x00100C08
		public void AnimationTarget()
		{
			AudioManager.PlaySE("SE_CEMETERY_CARD", 1f);
			GameObject model;
			if (this.ThisLocationShouldHaveModel(this.p))
			{
				model = this.model;
			}
			else
			{
				model = this.CreateModel(false);
				this.ModelAt(this.p, model);
				global::UnityEngine.Object.Destroy(model, 0.49f);
			}
			GameObject fx = ABLoader.LoadMasterDuelGameObject("fxp_card_decide_001");
			fx.transform.position = model.transform.position;
			if (this.p.InLocation(CardLocation.MonsterZone) && this.p.InPosition(CardPosition.Defence))
			{
				fx.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
			}
			if (this.p.InLocation(CardLocation.Removed))
			{
				fx.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
			}
			if (this.p.InLocation(CardLocation.SpellZone))
			{
				fx.transform.localScale = Vector3.one * 0.8f;
			}
			if (this.p.InLocation(CardLocation.Deck, CardLocation.Extra))
			{
				fx.transform.localEulerAngles = new Vector3(0f, GameCard.GetCardRotation(this.p, 0).y, 0f);
			}
			global::UnityEngine.Object.Destroy(fx, 1f);
		}

		// Token: 0x060088CA RID: 35018 RVA: 0x00102B54 File Offset: 0x00100D54
		public void AnimationLandShake(GameCard card, int shakeLevel)
		{
			if (shakeLevel == 0)
			{
				return;
			}
			if (card == this)
			{
				return;
			}
			if (!this.p.InLocation(CardLocation.Onfield))
			{
				return;
			}
			if (this.model == null)
			{
				return;
			}
			float distance = Vector3.Distance(card.model.transform.position, this.model.transform.position);
			float delay = Math.Max(0f, distance - 8.6f) * 0.01f;
			Vector3 direction = (card.model.transform.position - this.model.transform.position).normalized;
			direction.y = 0f;
			if (shakeLevel == 2)
			{
				int bounceCount = 6;
				float height = 5f;
				float angle = 50f;
				Sequence seq = DOTween.Sequence();
				seq.AppendInterval(delay);
				Vector3 originalPosition;
				Quaternion originalRotation;
				this.model.transform.GetPositionAndRotation(out originalPosition, out originalRotation);
				for (int i = 0; i < bounceCount; i++)
				{
					float decay = Mathf.Pow(0.7f, (float)i);
					float duration = this.bounceDutationsHuge[i];
					float currentHeight = height * decay;
					float num = angle * decay;
					Vector3 axis = Vector3.Cross((i % 2 == 0) ? direction : (-direction), Vector3.up).normalized;
					Quaternion rotation = Quaternion.AngleAxis(num, axis);
					seq.Append(this.model.transform.DOMoveY(originalPosition.y + currentHeight, duration / 2f, false).SetEase(Ease.OutQuad)).Join(this.model.transform.DORotateQuaternion(rotation, duration / 2f).SetEase(Ease.OutQuad));
					seq.Append(this.model.transform.DOMoveY(originalPosition.y, duration / 2f, false).SetEase(Ease.InQuad)).Join(this.model.transform.DORotateQuaternion(originalRotation, duration / 2f).SetEase(Ease.InQuad));
				}
				return;
			}
			if (shakeLevel == 1)
			{
				int bounceCount2 = 4;
				float height2 = 3f;
				float angle2 = 10f;
				Sequence seq2 = DOTween.Sequence();
				seq2.AppendInterval(delay);
				Vector3 originalPosition2;
				Quaternion originalRotation2;
				this.model.transform.GetPositionAndRotation(out originalPosition2, out originalRotation2);
				for (int j = 0; j < bounceCount2; j++)
				{
					float decay2 = Mathf.Pow(0.7f, (float)j);
					float duration2 = this.bounceDutationsSlight[j];
					float currentHeight2 = height2 * decay2;
					float num2 = angle2 * decay2;
					Vector3 axis2 = Vector3.Cross((j % 2 == 0) ? direction : (-direction), Vector3.up).normalized;
					Quaternion rotation2 = Quaternion.AngleAxis(num2, axis2);
					seq2.Append(this.model.transform.DOMoveY(originalPosition2.y + currentHeight2, duration2 / 2f, false).SetEase(Ease.OutQuad)).Join(this.model.transform.DORotateQuaternion(rotation2, duration2 / 2f).SetEase(Ease.OutQuad));
					seq2.Append(this.model.transform.DOMoveY(originalPosition2.y, duration2 / 2f, false).SetEase(Ease.InQuad)).Join(this.model.transform.DORotateQuaternion(originalRotation2, duration2 / 2f).SetEase(Ease.InQuad));
				}
			}
		}

		// Token: 0x060088CB RID: 35019 RVA: 0x00102EA4 File Offset: 0x001010A4
		public Sequence AnimationConfirmDeckTop(int id)
		{
			this.CreateModel(true);
			GPS pTop = this.p.Copy();
			pTop.sequence = (uint)(Program.instance.ocgcore.GetLocationCardCount((CardLocation)this.p.location, this.p.controller) - 1);
			this.ModelAt(pTop, null);
			this.model.SetActive(false);
			Transform turn = this.manager.GetElement<Transform>("Turn");
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(1f * (float)id);
			sequence.Append(turn.DOLocalMoveY(2f, 0.1f, false).OnStart(delegate
			{
				if (Program.instance.ocgcore.GetAutoInfo())
				{
					Program.instance.ocgcore.GetUI<OcgCoreUI>().CardDescription.Show(this, null, -1, null);
				}
				this.model.SetActive(true);
				if (Program.instance.ocgcore.GetLocationCardCount(CardLocation.Deck, this.p.controller) == 1)
				{
					ElementObjectManager deckModel = Program.instance.ocgcore.GetDeckModel(this.p.controller, CardLocation.Deck);
					Program.instance.ocgcore.SetDeckModelActive(deckModel, false);
				}
			}));
			sequence.Join(turn.DOLocalRotate(Vector3.zero, 0.1f, RotateMode.Fast));
			sequence.Append(turn.DOLocalMoveY(0.1f * (float)(id + 1), 0.1f, false).OnComplete(delegate
			{
				AudioManager.PlaySE("SE_CARDVIEW_02", 1f);
				GameObject gameObject = ABLoader.LoadMasterDuelGameObject("fxp_card_decide_deck_001");
				gameObject.transform.SetPositionAndRotation(turn.position, turn.rotation);
				gameObject.transform.localScale = GameCard.GetCardScale(this.p);
				global::UnityEngine.Object.Destroy(gameObject, 1f);
			}));
			sequence.AppendInterval(0.6f);
			sequence.Append(turn.DOLocalMoveY(2f, 0.1f, false));
			sequence.Join(turn.DOLocalRotate(new Vector3(0f, 0f, 180f), 0.1f, RotateMode.Fast));
			sequence.Append(turn.DOLocalMoveY(0f, 0.1f, false));
			sequence.OnComplete(delegate
			{
				global::UnityEngine.Object.Destroy(this.model);
				ElementObjectManager deckModel2 = Program.instance.ocgcore.GetDeckModel(this.p.controller, CardLocation.Deck);
				Program.instance.ocgcore.SetDeckModelActive(deckModel2, true);
			});
			return sequence;
		}

		// Token: 0x060088CC RID: 35020 RVA: 0x00103040 File Offset: 0x00101240
		public void AddButton(int response, string hint, ButtonType type)
		{
			bool exist = false;
			foreach (GameCard.DuelButtonInfo button in this.buttons)
			{
				if (button.type == type)
				{
					exist = true;
					button.response.Add(response);
				}
			}
			if (!exist)
			{
				this.buttons.Add(new GameCard.DuelButtonInfo
				{
					response = new List<int> { response },
					hint = hint,
					type = type
				});
			}
		}

		// Token: 0x060088CD RID: 35021 RVA: 0x001030E0 File Offset: 0x001012E0
		public void CreateButtons()
		{
			if (this.model == null || this.buttons.Count == 0)
			{
				return;
			}
			this.buttons.Sort((GameCard.DuelButtonInfo x, GameCard.DuelButtonInfo y) => x.type.CompareTo(y.type));
			for (int i = 0; i < this.buttons.Count; i++)
			{
				DuelButton mono = ABLoader.LoadMasterDuelGameObject("DuelButton").GetComponent<DuelButton>();
				this.buttonObjs.Add(mono);
				mono.response = this.buttons[i].response;
				mono.hint = this.buttons[i].hint;
				mono.type = this.buttons[i].type;
				mono.id = i;
				mono.buttonsCount = this.buttons.Count;
				mono.cookieCard = this;
			}
			this.hightYellow = false;
			foreach (GameCard.DuelButtonInfo button in this.buttons)
			{
				if (button.type == ButtonType.Activate || button.type == ButtonType.PenSummon || button.type == ButtonType.SetPendulum || button.type == ButtonType.SpSummon)
				{
					this.hightYellow = true;
					break;
				}
			}
			if (this.hightYellow)
			{
				this.manager.GetElement("EffectHighlightYellow").SetActive(true);
			}
			else
			{
				this.manager.GetElement("EffectHighlightBlue").SetActive(true);
			}
			GameObject highlightParent = this.manager.GetElement("EffectHighlightBlue").transform.parent.gameObject;
			if ((this.p.location & 2U) > 0U)
			{
				Tools.ChangeSortingLayer(highlightParent, "Default");
				return;
			}
			Tools.ChangeSortingLayer(highlightParent, "DuelEffect_Low");
		}

		// Token: 0x060088CE RID: 35022 RVA: 0x001032C8 File Offset: 0x001014C8
		public void ClearButtons()
		{
			this.buttons.Clear();
			if (this.model == null)
			{
				return;
			}
			foreach (DuelButton duelButton in this.buttonObjs)
			{
				global::UnityEngine.Object.Destroy(duelButton.gameObject);
			}
			this.buttonObjs.Clear();
			this.manager.GetElement("EffectHighlightBlue").SetActive(false);
			this.manager.GetElement("EffectHighlightYellow").SetActive(false);
			this.manager.GetElement("EffectHighlightBlueSelect").SetActive(false);
			this.manager.GetElement("EffectHighlightYellowSelect").SetActive(false);
		}

		// Token: 0x060088CF RID: 35023 RVA: 0x0010339C File Offset: 0x0010159C
		public void RefreshLabel()
		{
			if (this.model == null)
			{
				return;
			}
			if ((this.p.location & 12U) == 0U || ((long)this.p.position & 5L) == 0L)
			{
				this.HideLabel();
				return;
			}
			Card origin = CardsManager.Get(this.data.Id, false);
			if ((this.p.location & 4U) > 0U)
			{
				if (this.data.HasType(CardType.Link))
				{
					this.manager.GetElement("LinkMarker0").SetActive(((long)this.data.LinkMarker & 64L) > 0L);
					this.manager.GetElement("LinkMarker1").SetActive(((long)this.data.LinkMarker & 128L) > 0L);
					this.manager.GetElement("LinkMarker2").SetActive(((long)this.data.LinkMarker & 256L) > 0L);
					this.manager.GetElement("LinkMarker3").SetActive(((long)this.data.LinkMarker & 8L) > 0L);
					this.manager.GetElement("LinkMarker4").SetActive(((long)this.data.LinkMarker & 32L) > 0L);
					this.manager.GetElement("LinkMarker5").SetActive(((long)this.data.LinkMarker & 1L) > 0L);
					this.manager.GetElement("LinkMarker6").SetActive(((long)this.data.LinkMarker & 2L) > 0L);
					this.manager.GetElement("LinkMarker7").SetActive(((long)this.data.LinkMarker & 4L) > 0L);
				}
				else
				{
					for (int i = 0; i < 8; i++)
					{
						this.manager.GetElement("LinkMarker" + i.ToString()).SetActive(false);
					}
				}
				if (this.data.HasType(CardType.Xyz))
				{
					this.manager.GetElement("MonsterMaterialsRoot").SetActive(true);
					int overlayCounts = Program.instance.ocgcore.GCS_GetOverlays(this).Count;
					this.manager.GetElement<TextMeshPro>("TextMonsterMaterials").text = overlayCounts.ToString();
				}
				else
				{
					this.manager.GetElement("MonsterMaterialsRoot").SetActive(false);
				}
				this.manager.GetElement("CardAttackBody").SetActive(true);
				TextMeshPro text = this.manager.GetElement<TextMeshPro>("TextPowerPoint");
				if (!this.labelShowing)
				{
					string atkDef = string.Empty;
					if (this.data.HasType(CardType.Link))
					{
						if (this.data.Attack > this.data.rAttack)
						{
							atkDef = "<color=#00FFFF>" + this.data.Attack.ToString() + "</color>";
						}
						else if (this.data.Attack < this.data.rAttack)
						{
							atkDef = "<color=#FF0000>" + this.data.Attack.ToString() + "</color>";
						}
						else
						{
							atkDef = "<color=#FFFFFF>" + this.data.Attack.ToString() + "</color>";
						}
					}
					else
					{
						int rAtk = this.data.rAttack;
						int rDef = this.data.rDefense;
						if (rAtk < 0)
						{
							rAtk = 0;
						}
						if (rDef < 0)
						{
							rDef = 0;
						}
						if (this.data.Attack > rAtk)
						{
							if (this.p.InPosition(CardPosition.Attack))
							{
								atkDef += "<color=#00FFFF>";
							}
							else
							{
								atkDef += "<color=#009999><size=75%>";
							}
						}
						else if (this.data.Attack < rAtk)
						{
							if (this.p.InPosition(CardPosition.Attack))
							{
								atkDef += "<color=#FF0000>";
							}
							else
							{
								atkDef += "<color=#990000><size=75%>";
							}
						}
						else if (this.p.InPosition(CardPosition.Attack))
						{
							atkDef += "<color=#FFFFFF>";
						}
						else
						{
							atkDef += "<color=#999999><size=75%>";
						}
						atkDef = atkDef + this.data.Attack.ToString() + "</color>";
						if (!this.p.InPosition(CardPosition.Attack))
						{
							atkDef += "</size>";
						}
						atkDef += "/";
						if (this.data.Defense > rDef)
						{
							if (((long)this.p.position & 3L) > 0L)
							{
								atkDef += "<color=#009999><size=75%>";
							}
							else
							{
								atkDef += "<color=#00FFFF>";
							}
						}
						else if (this.data.Defense < rDef)
						{
							if (((long)this.p.position & 3L) > 0L)
							{
								atkDef += "<color=#990000><size=75%>";
							}
							else
							{
								atkDef += "<color=#FF0000>";
							}
						}
						else if (((long)this.p.position & 3L) > 0L)
						{
							atkDef += "<color=#999999><size=75%>";
						}
						else
						{
							atkDef += "<color=#FFFFFF>";
						}
						atkDef = atkDef + this.data.Defense.ToString() + "</color>";
					}
					text.text = atkDef;
				}
				else
				{
					if (this.data.Attack > this.attack)
					{
						AudioManager.PlaySE("SE_BUFF_ATTACK", 1f);
						GameObject element = this.manager.GetElement("EffectBuff");
						element.SetActive(false);
						element.SetActive(true);
					}
					else if (this.data.Attack < this.attack)
					{
						AudioManager.PlaySE("SE_DEBUFF_ATTACK", 1f);
						GameObject element2 = this.manager.GetElement("EffectDebuff");
						element2.SetActive(false);
						element2.SetActive(true);
					}
					if (this.data.HasType(CardType.Link))
					{
						string s5 = string.Empty;
						if (this.data.Attack > this.data.rAttack)
						{
							s5 = "<color=#00FFFF>";
						}
						else if (this.data.Attack < this.data.rAttack)
						{
							s5 = "<color=#FF0000>";
						}
						else
						{
							s5 = "<color=#FFFFFF>";
						}
						if (this.attack != this.data.Attack)
						{
							int originAttack2 = this.attack;
							DOTween.To(() => originAttack2, delegate(int x)
							{
								text.text = s5 + x.ToString() + "</color>";
							}, this.data.Attack, this.changeTime);
						}
						else
						{
							text.text = s5 + this.data.Attack.ToString() + "</color>";
						}
					}
					else
					{
						int rAtk2 = this.data.rAttack;
						int rDef2 = this.data.rDefense;
						if (rAtk2 < 0)
						{
							rAtk2 = 0;
						}
						if (rDef2 < 0)
						{
							rDef2 = 0;
						}
						string s1 = string.Empty;
						if (this.data.Attack > rAtk2)
						{
							if (this.p.InPosition(CardPosition.Attack))
							{
								s1 += "<color=#00FFFF>";
							}
							else
							{
								s1 += "<color=#009999><size=75%>";
							}
						}
						else if (this.data.Attack < rAtk2)
						{
							if (this.p.InPosition(CardPosition.Attack))
							{
								s1 += "<color=#FF0000>";
							}
							else
							{
								s1 += "<color=#990000><size=75%>";
							}
						}
						else if (this.p.InPosition(CardPosition.Attack))
						{
							s1 += "<color=#FFFFFF>";
						}
						else
						{
							s1 += "<color=#999999><size=75%>";
						}
						string s2 = "</color>";
						if (!this.p.InPosition(CardPosition.Attack))
						{
							s2 += "</size>";
						}
						s2 += "/";
						string s3 = string.Empty;
						if (this.data.Defense > rDef2)
						{
							if (this.p.InPosition(CardPosition.Attack))
							{
								s3 += "<color=#009999><size=75%>";
							}
							else
							{
								s3 += "<color=#00FFFF>";
							}
						}
						else if (this.data.Defense < rDef2)
						{
							if (this.p.InPosition(CardPosition.Attack))
							{
								s3 += "<color=#990000><size=75%>";
							}
							else
							{
								s3 += "<color=#FF0000>";
							}
						}
						else if (this.p.InPosition(CardPosition.Attack))
						{
							s3 += "<color=#999999><size=75%>";
						}
						else
						{
							s3 += "<color=#FFFFFF>";
						}
						string s4 = "</color>";
						if (this.p.InPosition(CardPosition.Attack))
						{
							s4 += "</size>";
						}
						int originAttack = this.attack;
						int originDefense = this.defense;
						if (this.data.Attack != this.attack && this.data.Defense == this.defense)
						{
							DOTween.To(() => originAttack, delegate(int x)
							{
								text.text = string.Concat(new string[]
								{
									s1,
									x.ToString(),
									s2,
									s3,
									this.data.Defense.ToString(),
									s4
								});
							}, this.data.Attack, this.changeTime);
						}
						else if (this.data.Attack == this.attack && this.data.Defense != this.defense)
						{
							DOTween.To(() => originDefense, delegate(int x)
							{
								text.text = string.Concat(new string[]
								{
									s1,
									this.data.Attack.ToString(),
									s2,
									s3,
									x.ToString(),
									s4
								});
							}, this.data.Defense, this.changeTime);
						}
						else if (this.data.Attack != this.attack && this.data.Defense != this.defense)
						{
							DOTween.To(() => originAttack, delegate(int x)
							{
								text.text = s1 + x.ToString() + s2;
							}, this.data.Attack, this.changeTime);
							DOTween.To(() => originDefense, delegate(int x)
							{
								TextMeshPro text2 = text;
								text2.text = text2.text + s3 + x.ToString() + s4;
							}, this.data.Defense, this.changeTime);
						}
						else
						{
							text.text = string.Concat(new string[]
							{
								s1,
								this.data.Attack.ToString(),
								s2,
								s3,
								this.data.Defense.ToString(),
								s4
							});
						}
					}
				}
				this.attack = this.data.Attack;
				this.defense = this.data.Defense;
				this.manager.GetElement("CardPendulumBody").SetActive(false);
				if (this.data.HasType(CardType.Link))
				{
					this.manager.GetElement("LinkCount").SetActive(true);
					this.manager.GetElement<TextMeshPro>("TextLinkCount").text = this.data.GetLinkCount().ToString();
					this.manager.GetElement("CardLevel").SetActive(false);
				}
				else
				{
					this.manager.GetElement("LinkCount").SetActive(false);
					this.manager.GetElement("CardLevel").SetActive(true);
					if (this.data.HasType(CardType.Xyz))
					{
						this.manager.GetElement<SpriteRenderer>("IconLevel").sprite = TextureManager.container.typeRank;
					}
					else
					{
						this.manager.GetElement<SpriteRenderer>("IconLevel").sprite = TextureManager.container.typeLevel;
					}
					string lv = "";
					if (this.data.Level > origin.Level)
					{
						lv += "<color=#00FFFF>";
					}
					else if (this.data.Level < origin.Level)
					{
						lv += "<color=#FF0000>";
					}
					else
					{
						lv += "<color=#FFFFFF>";
					}
					lv = lv + this.data.Level.ToString() + "</color>";
					this.manager.GetElement<TextMeshPro>("TextLevel").text = lv;
				}
				if (this.data.HasType(CardType.Tuner))
				{
					this.manager.GetElement("TunerIconRoot").SetActive(true);
					if (origin.HasType(CardType.Tuner))
					{
						this.manager.GetElement("TunerIconOutline").SetActive(false);
					}
					else
					{
						this.manager.GetElement("TunerIconOutline").SetActive(true);
					}
				}
				else
				{
					this.manager.GetElement("TunerIconRoot").SetActive(false);
				}
				if (((long)this.data.Attribute & 16L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconAttribute").sprite = TextureManager.container.attributeLight;
					this.manager.GetElement<MeshRenderer>("Closeup").material.SetColor("_Color", new Color(1f, 1f, 0f, 1f) * 1.5f);
				}
				else if (((long)this.data.Attribute & 32L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconAttribute").sprite = TextureManager.container.attributeDark;
					this.manager.GetElement<MeshRenderer>("Closeup").material.SetColor("_Color", new Color(1f, 0f, 1f, 1f) * 1.5f);
				}
				else if (((long)this.data.Attribute & 2L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconAttribute").sprite = TextureManager.container.attributeWater;
					this.manager.GetElement<MeshRenderer>("Closeup").material.SetColor("_Color", new Color(0f, 1f, 1f, 1f) * 1.5f);
				}
				else if (((long)this.data.Attribute & 4L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconAttribute").sprite = TextureManager.container.attributeFire;
					this.manager.GetElement<MeshRenderer>("Closeup").material.SetColor("_Color", new Color(1f, 0f, 0f, 1f) * 1.5f);
				}
				else if (((long)this.data.Attribute & 1L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconAttribute").sprite = TextureManager.container.attributeEarth;
					this.manager.GetElement<MeshRenderer>("Closeup").material.SetColor("_Color", new Color(1f, 0.2f, 0f, 1f) * 1.5f);
				}
				else if (((long)this.data.Attribute & 8L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconAttribute").sprite = TextureManager.container.attributeWind;
					this.manager.GetElement<MeshRenderer>("Closeup").material.SetColor("_Color", new Color(0f, 1f, 0f, 1f) * 1.5f);
				}
				else if (((long)this.data.Attribute & 64L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconAttribute").sprite = TextureManager.container.attributeDivine;
					this.manager.GetElement<MeshRenderer>("Closeup").material.SetColor("_Color", new Color(1f, 1f, 0f, 1f) * 1.5f);
				}
				if (this.data.Id > 0 && this.data.Attribute != origin.Attribute)
				{
					this.manager.GetElement("IconAttributeChange").SetActive(true);
					if (this.lastAttribute != this.data.Attribute)
					{
						this.lastAttribute = this.data.Attribute;
						AudioManager.PlaySE("SE_BUFF_CHANGE", 1f);
						this.manager.GetElement("EffectChange").SetActive(false);
						this.manager.GetElement("EffectChange").SetActive(true);
					}
				}
				else
				{
					this.manager.GetElement("IconAttributeChange").SetActive(false);
				}
				this.manager.GetElement<SpriteRenderer>("MagicType").sprite = TextureManager.container.typeNone;
				this.manager.GetElement("MagicTypeChange").SetActive(false);
				if (((long)this.data.Race & 1L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceWarrior;
				}
				else if (((long)this.data.Race & 2L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceSpellCaster;
				}
				else if (((long)this.data.Race & 4L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceFairy;
				}
				else if (((long)this.data.Race & 8L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceFiend;
				}
				else if (((long)this.data.Race & 16L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceZombie;
				}
				else if (((long)this.data.Race & 32L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceMachine;
				}
				else if (((long)this.data.Race & 64L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceAqua;
				}
				else if (((long)this.data.Race & 128L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.racePyro;
				}
				else if (((long)this.data.Race & 256L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceRock;
				}
				else if (((long)this.data.Race & 512L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceWindBeast;
				}
				else if (((long)this.data.Race & 1024L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.racePlant;
				}
				else if (((long)this.data.Race & 2048L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceInsect;
				}
				else if (((long)this.data.Race & 4096L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceThunder;
				}
				else if (((long)this.data.Race & 8192L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceDragon;
				}
				else if (((long)this.data.Race & 16384L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceBeast;
				}
				else if (((long)this.data.Race & 32768L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceBeastWarrior;
				}
				else if (((long)this.data.Race & 65536L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceDinosaur;
				}
				else if (((long)this.data.Race & 131072L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceFish;
				}
				else if (((long)this.data.Race & 262144L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceSeaSerpent;
				}
				else if (((long)this.data.Race & 524288L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceReptile;
				}
				else if (((long)this.data.Race & 1048576L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.racePsycho;
				}
				else if (((long)this.data.Race & 2097152L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceDivineBeast;
				}
				else if (((long)this.data.Race & 4194304L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceCreatorGod;
				}
				else if (((long)this.data.Race & 8388608L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceWyrm;
				}
				else if (((long)this.data.Race & 16777216L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceCyberse;
				}
				else if (((long)this.data.Race & 33554432L) > 0L)
				{
					this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.raceIllustion;
				}
				if (this.data.Id > 0 && this.data.Race != origin.Race)
				{
					this.manager.GetElement("IconTypeChange").SetActive(true);
					if (this.lastRace != this.data.Race)
					{
						this.lastRace = this.data.Race;
						AudioManager.PlaySE("SE_BUFF_CHANGE", 1f);
						this.manager.GetElement("EffectChange").SetActive(false);
						this.manager.GetElement("EffectChange").SetActive(true);
					}
				}
				else
				{
					this.manager.GetElement("IconTypeChange").SetActive(false);
				}
			}
			else
			{
				for (int j = 0; j < 8; j++)
				{
					this.manager.GetElement("LinkMarker" + j.ToString()).SetActive(false);
				}
				this.manager.GetElement("MonsterMaterialsRoot").SetActive(false);
				this.manager.GetElement("CardAttackBody").SetActive(false);
				int p = 0;
				int p2 = 4;
				if (OcgCore.MasterRule <= 3)
				{
					p = 6;
					p2 = 7;
				}
				if ((this.p.location & 512U) > 0U || (this.data.HasType(CardType.Pendulum) && (this.p.location & 8U) > 0U && !this.data.HasType(CardType.Equip) && !this.data.HasType(CardType.Continuous) && !this.data.HasType(CardType.Trap) && ((ulong)this.p.sequence == (ulong)((long)p) || (ulong)this.p.sequence == (ulong)((long)p2))))
				{
					this.manager.GetElement("CardPendulumBody").SetActive(true);
					string pendulum = "";
					if ((ulong)this.p.sequence == (ulong)((long)p))
					{
						this.manager.GetElement("PendulumLeft").SetActive(true);
						this.manager.GetElement("PendulumRight").SetActive(false);
						if (this.data.LScale > origin.LScale)
						{
							pendulum += "<color=#00FFFF>";
						}
						else if (this.data.LScale < origin.LScale)
						{
							pendulum += "<color=#FF0000>";
						}
						else
						{
							pendulum += "<color=#FFFFFF>";
						}
						pendulum = pendulum + this.data.LScale.ToString() + "</color>";
						this.manager.GetElement<TextMeshPro>("TextPendulumLeft").text = pendulum;
					}
					else
					{
						this.manager.GetElement("PendulumLeft").SetActive(false);
						this.manager.GetElement("PendulumRight").SetActive(true);
						if (this.data.RScale > origin.RScale)
						{
							pendulum += "<color=#00FFFF>";
						}
						else if (this.data.RScale < origin.RScale)
						{
							pendulum += "<color=#FF0000>";
						}
						else
						{
							pendulum += "<color=#FFFFFF>";
						}
						pendulum = pendulum + this.data.RScale.ToString() + "</color>";
						this.manager.GetElement<TextMeshPro>("TextPendulumRight").text = pendulum;
					}
				}
				else
				{
					this.manager.GetElement("CardPendulumBody").SetActive(false);
				}
				this.manager.GetElement("LinkCount").SetActive(false);
				this.manager.GetElement("CardLevel").SetActive(false);
				this.manager.GetElement("TunerIconRoot").SetActive(false);
				if (this.data.HasType(CardType.Spell))
				{
					this.manager.GetElement<SpriteRenderer>("IconAttribute").sprite = TextureManager.container.attributeSpell;
				}
				else
				{
					this.manager.GetElement<SpriteRenderer>("IconAttribute").sprite = TextureManager.container.attributeTrap;
				}
				this.manager.GetElement("IconAttributeChange").SetActive(false);
				if (this.data.HasType(CardType.Counter))
				{
					this.manager.GetElement<SpriteRenderer>("MagicType").sprite = TextureManager.container.typeCounter;
				}
				else if (this.data.HasType(CardType.Field))
				{
					this.manager.GetElement<SpriteRenderer>("MagicType").sprite = TextureManager.container.typeField;
				}
				else if (this.data.HasType(CardType.Equip))
				{
					this.manager.GetElement<SpriteRenderer>("MagicType").sprite = TextureManager.container.typeEquip;
				}
				else if (this.data.HasType(CardType.Continuous))
				{
					this.manager.GetElement<SpriteRenderer>("MagicType").sprite = TextureManager.container.typeContinuous;
				}
				else if (this.data.HasType(CardType.QuickPlay))
				{
					this.manager.GetElement<SpriteRenderer>("MagicType").sprite = TextureManager.container.typeQuickPlay;
				}
				else if (this.data.HasType(CardType.Ritual) && !origin.HasType(CardType.Monster))
				{
					this.manager.GetElement<SpriteRenderer>("MagicType").sprite = TextureManager.container.typeRitual;
				}
				else
				{
					this.manager.GetElement<SpriteRenderer>("MagicType").sprite = TextureManager.container.typeNone;
				}
				this.manager.GetElement("MagicTypeChange").SetActive(false);
				this.manager.GetElement<SpriteRenderer>("IconType").sprite = TextureManager.container.typeNone;
				this.manager.GetElement("IconTypeChange").SetActive(false);
			}
			if (this.cardCounters.Count > 0)
			{
				int counter = 0;
				int count = 0;
				using (Dictionary<int, int>.Enumerator enumerator = this.cardCounters.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						KeyValuePair<int, int> cc = enumerator.Current;
						counter = cc.Key;
						count = cc.Value;
					}
				}
				this.manager.GetElement<SpriteRenderer>("IconCounter").sprite = TextureManager.GetCardCounterIcon(counter);
				this.manager.GetElement<TextMeshPro>("TextCounter").text = count.ToString();
			}
			else
			{
				this.manager.GetElement<SpriteRenderer>("IconCounter").sprite = TextureManager.container.typeNone;
				this.manager.GetElement<TextMeshPro>("TextCounter").text = string.Empty;
			}
			this.manager.GetElement<Transform>("DefaultShow").localEulerAngles = new Vector3(0f, 0f, 0f);
			this.manager.GetElement<Transform>("DefaultHide").localEulerAngles = new Vector3(0f, 0f, 0f);
			this.manager.GetElement<Transform>("CloseupOffset").localEulerAngles = new Vector3(0f, 0f, 0f);
			if (this.p.controller == 0U)
			{
				this.manager.GetElement<Transform>("MonsterMaterialsRoot").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("CardAttackBody").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("CardPendulumBody").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("LinkCount").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("CardLevel").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("TunerIconRoot").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("CardAttribute").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("MagicTypeBase").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("CardType").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("CardCounter").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("StatusIcon").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("LinkMarkerRoot").localEulerAngles = new Vector3(0f, 0f, 0f);
			}
			else if (this.CloseupConfig() && (this.p.location & 4U) > 0U)
			{
				this.manager.GetElement<Transform>("DefaultShow").localEulerAngles = new Vector3(0f, 180f, 0f);
				this.manager.GetElement<Transform>("DefaultHide").localEulerAngles = new Vector3(0f, 180f, 0f);
				this.manager.GetElement<Transform>("CloseupOffset").localEulerAngles = new Vector3(0f, 180f, 0f);
				this.manager.GetElement<Transform>("MonsterMaterialsRoot").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("CardAttackBody").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("CardPendulumBody").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("LinkCount").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("CardLevel").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("TunerIconRoot").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("CardAttribute").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("MagicTypeBase").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("CardType").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("CardCounter").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("StatusIcon").localEulerAngles = new Vector3(0f, 0f, 0f);
				this.manager.GetElement<Transform>("LinkMarkerRoot").localEulerAngles = new Vector3(0f, 180f, 0f);
			}
			else
			{
				this.manager.GetElement<Transform>("MonsterMaterialsRoot").localEulerAngles = new Vector3(0f, 180f, 0f);
				this.manager.GetElement<Transform>("CardAttackBody").localEulerAngles = new Vector3(0f, 180f, 0f);
				this.manager.GetElement<Transform>("CardPendulumBody").localEulerAngles = new Vector3(0f, 180f, 0f);
				this.manager.GetElement<Transform>("LinkCount").localEulerAngles = new Vector3(0f, 180f, 0f);
				this.manager.GetElement<Transform>("CardLevel").localEulerAngles = new Vector3(0f, 180f, 0f);
				this.manager.GetElement<Transform>("TunerIconRoot").localEulerAngles = new Vector3(0f, 180f, 0f);
				this.manager.GetElement<Transform>("CardAttribute").localEulerAngles = new Vector3(0f, 180f, 0f);
				this.manager.GetElement<Transform>("MagicTypeBase").localEulerAngles = new Vector3(0f, 180f, 0f);
				this.manager.GetElement<Transform>("CardType").localEulerAngles = new Vector3(0f, 180f, 0f);
				this.manager.GetElement<Transform>("CardCounter").localEulerAngles = new Vector3(0f, 180f, 0f);
				this.manager.GetElement<Transform>("StatusIcon").localEulerAngles = new Vector3(0f, 180f, 0f);
				this.manager.GetElement<Transform>("LinkMarkerRoot").localEulerAngles = new Vector3(0f, 0f, 0f);
			}
			this.ShowLabel();
		}

		// Token: 0x060088D0 RID: 35024 RVA: 0x001058EC File Offset: 0x00103AEC
		public void ShowLabel()
		{
			this.labelShowing = true;
			Transform element = this.manager.GetElement<Transform>("StatusLabelRoot");
			element.gameObject.SetActive(true);
			element.DOScale(1f, 0.2f).SetEase(Ease.InCubic);
			if (this.NeedShowCloseup())
			{
				MeshRenderer renderer = this.manager.GetElement<MeshRenderer>("Closeup");
				if (renderer.material.mainTexture == null)
				{
					this.closeupShowing = false;
				}
				if (!this.closeupShowing)
				{
					this.closeupShowing = true;
					Program.instance.texture_.LoadCloseupAsync(this.data.Id, renderer);
				}
			}
			else
			{
				this.closeupShowing = false;
				this.manager.GetElement("Closeup").SetActive(false);
			}
			this.HideHiddenLabel();
		}

		// Token: 0x060088D1 RID: 35025 RVA: 0x001059B8 File Offset: 0x00103BB8
		public void HideLabel()
		{
			this.labelShowing = false;
			Transform labelRoot = this.manager.GetElement<Transform>("StatusLabelRoot");
			labelRoot.DOScale(0f, 0.2f).SetEase(Ease.OutCubic).OnComplete(delegate
			{
				labelRoot.gameObject.SetActive(false);
			});
			this.manager.GetElement("Closeup").SetActive(false);
			this.closeupShowing = false;
		}

		// Token: 0x060088D2 RID: 35026 RVA: 0x00105A34 File Offset: 0x00103C34
		public void ShowHiddenLabel()
		{
			if (this.model == null)
			{
				return;
			}
			this.manager.GetElement("CardAttribute").SetActive(true);
			this.manager.GetElement("MagicTypeBase").SetActive(true);
			this.manager.GetElement("CardType").SetActive(true);
			Transform element = this.manager.GetElement<Transform>("LinkMarkerRoot");
			element.DOScale(1.7f, 0.2f).SetEase(Ease.InOutCubic);
			SpriteRenderer[] componentsInChildren = element.GetComponentsInChildren<SpriteRenderer>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].sortingLayerName = "CardStatus";
			}
		}

		// Token: 0x060088D3 RID: 35027 RVA: 0x00105ADC File Offset: 0x00103CDC
		public void HideHiddenLabel()
		{
			if (this.model == null || !this.labelShowing)
			{
				return;
			}
			if (!this.manager.GetElement("IconAttributeChange").activeSelf)
			{
				this.manager.GetElement("CardAttribute").SetActive(false);
			}
			else
			{
				this.manager.GetElement("CardAttribute").SetActive(true);
			}
			if ((this.p.location & 8U) == 0U || !this.manager.GetElement("MagicTypeChange").activeSelf)
			{
				this.manager.GetElement("MagicTypeBase").SetActive(false);
			}
			else
			{
				this.manager.GetElement("MagicTypeBase").SetActive(true);
			}
			if ((this.p.location & 4U) == 0U || !this.manager.GetElement("IconTypeChange").activeSelf)
			{
				this.manager.GetElement("CardType").SetActive(false);
			}
			else
			{
				this.manager.GetElement("CardType").SetActive(true);
			}
			Transform element = this.manager.GetElement<Transform>("LinkMarkerRoot");
			element.DOScale(1f, 0.2f).SetEase(Ease.InOutCubic);
			SpriteRenderer[] componentsInChildren = element.GetComponentsInChildren<SpriteRenderer>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].sortingLayerName = "Default";
			}
		}

		// Token: 0x060088D4 RID: 35028 RVA: 0x00105C38 File Offset: 0x00103E38
		private void SetDisabled()
		{
			if (this.model == null)
			{
				return;
			}
			Renderer cardFace = this.manager.GetElement<Transform>("CardModel").GetChild(1).GetComponent<Renderer>();
			if ((this.p.location & 12U) == 0U)
			{
				this.m_disabled = false;
			}
			if (((long)this.p.position & 10L) > 0L)
			{
				this.m_disabled = false;
			}
			if (this.Disabled)
			{
				cardFace.material.SetFloat("_Monochrome", 1f);
				this.manager.GetElement<Renderer>("Closeup").material.SetVector("RGBA", Vector4.zero);
				this.manager.GetElement<Renderer>("Closeup").material.SetFloat("Mono", 1f);
				return;
			}
			cardFace.material.SetFloat("_Monochrome", 0f);
			this.manager.GetElement<Renderer>("Closeup").material.SetVector("RGBA", Vector4.one);
			this.manager.GetElement<Renderer>("Closeup").material.SetFloat("Mono", 0f);
		}

		// Token: 0x060088D5 RID: 35029 RVA: 0x00105D68 File Offset: 0x00103F68
		private bool NeedShowCloseup()
		{
			return !(this.model == null) && this.CloseupConfig() && (this.p.location & 4U) != 0U && ((long)this.p.position & 10L) <= 0L && File.Exists("Picture/Closeup/" + this.data.Id.ToString() + ".png");
		}

		// Token: 0x060088D6 RID: 35030 RVA: 0x00105DE0 File Offset: 0x00103FE0
		private bool CloseupConfig()
		{
			return (OcgCore.condition != OcgCore.Condition.Duel || Config.GetBool("DuelCloseup", true)) && (OcgCore.condition != OcgCore.Condition.Watch || Config.GetBool("WatchCloseup", true)) && (OcgCore.condition != OcgCore.Condition.Replay || Config.GetBool("ReplayCloseup", true));
		}

		// Token: 0x060088D7 RID: 35031 RVA: 0x00105E33 File Offset: 0x00104033
		public bool NeedShowFaceDownCard()
		{
			return this.data.Id != 0 && ((long)this.p.position & 5L) <= 0L && (this.p.location & 12U) != 0U;
		}

		// Token: 0x060088D8 RID: 35032 RVA: 0x00105E6C File Offset: 0x0010406C
		public bool IsFaceDownOnSpellZone()
		{
			return ((long)this.p.position & 5L) <= 0L && (this.p.location & 8U) != 0U;
		}

		// Token: 0x060088D9 RID: 35033 RVA: 0x00105E98 File Offset: 0x00104098
		public void ShowFaceDownCardOrNot(bool show)
		{
			if (this.model == null)
			{
				return;
			}
			if (this.IsFaceDownOnSpellZone())
			{
				this.setTurn = OcgCore.turns;
			}
			else
			{
				this.setTurn = 0;
			}
			Renderer back = this.manager.GetElement<Transform>("CardModel").GetChild(0).GetComponent<Renderer>();
			Renderer face = this.manager.GetElement<Transform>("CardModel").GetChild(1).GetComponent<Renderer>();
			if (OcgCore.condition == OcgCore.Condition.Duel && !Config.GetBool("DuelFaceDown", true))
			{
				show = false;
			}
			if (OcgCore.condition == OcgCore.Condition.Watch && !Config.GetBool("WatchFaceDown", true))
			{
				show = false;
			}
			if (OcgCore.condition == OcgCore.Condition.Replay && !Config.GetBool("ReplayFaceDown", true))
			{
				show = false;
			}
			if (show)
			{
				face.GetComponent<Animator>().SetBool("Show", true);
				face.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
				face.material.SetTexture("_LoadingTex", back.material.mainTexture);
				back.gameObject.SetActive(false);
				return;
			}
			back.gameObject.SetActive(true);
			face.GetComponent<Animator>().SetBool("Show", false);
			face.transform.localEulerAngles = new Vector3(180f, 0f, -180f);
			this.manager.GetElement("EffectDisquiet").SetActive(false);
		}

		// Token: 0x060088DA RID: 35034 RVA: 0x00106000 File Offset: 0x00104200
		public void ShowDisquiet()
		{
			if (this.model == null)
			{
				return;
			}
			if (this.setTurn == 0)
			{
				return;
			}
			if (this.setTurn >= OcgCore.turns)
			{
				return;
			}
			if ((this.p.location & 8U) == 0U)
			{
				return;
			}
			if (((long)this.p.position & 10L) == 0L)
			{
				return;
			}
			this.manager.GetElement("EffectDisquiet").SetActive(true);
			this.setOverTurn = true;
		}

		// Token: 0x060088DB RID: 35035 RVA: 0x00106074 File Offset: 0x00104274
		public void AddCounter(int counter, int count)
		{
			AudioManager.PlaySE("SE_CARD_COUNTER", 1f);
			int fullCount = count;
			if (this.cardCounters.Any((KeyValuePair<int, int> cc) => cc.Key == counter))
			{
				fullCount += this.cardCounters[counter];
				this.cardCounters.Remove(counter);
			}
			this.cardCounters.Add(counter, fullCount);
			string counterName = StringHelper.Get("counter", counter, 0);
			for (int i = 0; i < count; i++)
			{
				this.AddStringTail(counterName);
			}
			this.RefreshLabel();
		}

		// Token: 0x060088DC RID: 35036 RVA: 0x0010611C File Offset: 0x0010431C
		public void RemoveCounter(int counter, int count)
		{
			AudioManager.PlaySE("SE_CARD_COUNTER", 1f);
			int fullCount = this.cardCounters[counter] - count;
			this.cardCounters.Remove(counter);
			if (fullCount > 0)
			{
				this.cardCounters.Add(counter, fullCount);
			}
			string counterName = StringHelper.Get("counter", counter, 0);
			for (int i = 0; i < count; i++)
			{
				this.RemoveStringTail(counterName, false);
			}
			this.RefreshLabel();
		}

		// Token: 0x060088DD RID: 35037 RVA: 0x0010618C File Offset: 0x0010438C
		public void ClearCounter()
		{
			this.cardCounters.Clear();
		}

		// Token: 0x060088DE RID: 35038 RVA: 0x0010619C File Offset: 0x0010439C
		public int GetCounterCount(int type)
		{
			int count;
			this.cardCounters.TryGetValue(type, out count);
			return count;
		}

		// Token: 0x060088DF RID: 35039 RVA: 0x001061B9 File Offset: 0x001043B9
		public void AddStringTail(string tail)
		{
			this.tails.Add(tail);
		}

		// Token: 0x060088E0 RID: 35040 RVA: 0x001061C7 File Offset: 0x001043C7
		public void RemoveStringTail(string tail, bool all = false)
		{
			this.tails.Remove(tail, all);
		}

		// Token: 0x060088E1 RID: 35041 RVA: 0x001061D6 File Offset: 0x001043D6
		public void ClearAllTails()
		{
			this.ClearCounter();
			this.tails.Clear();
		}

		// Token: 0x060088E2 RID: 35042 RVA: 0x001061EC File Offset: 0x001043EC
		public void AddChain(int i)
		{
			GameObject obj = ABLoader.LoadMasterDuelGameObject("ChainSpot");
			Program.instance.ocgcore.allGameObjects.Add(obj);
			this.chains.Add(new GameCard.Chain
			{
				i = i,
				chainSpot = obj.GetComponent<DuelChainSpot>()
			});
			List<GameCard.Chain> list = this.chains;
			list[list.Count - 1].chainSpot.Play(i, this.p, this.model != null);
		}

		// Token: 0x060088E3 RID: 35043 RVA: 0x0010626C File Offset: 0x0010446C
		public void ResolveChain(int i)
		{
			foreach (GameCard.Chain chain in this.chains)
			{
				if (chain.i == i)
				{
					chain.chainSpot.OnChainResolveBegin();
					break;
				}
			}
		}

		// Token: 0x060088E4 RID: 35044 RVA: 0x001062D0 File Offset: 0x001044D0
		public void RemoveChain(int i)
		{
			foreach (GameCard.Chain chain in this.chains)
			{
				if (chain.i == i)
				{
					chain.chainSpot.OnChainResolveEnd();
					global::UnityEngine.Object.Destroy(chain.chainSpot.gameObject, 1f);
					this.chains.Remove(chain);
					break;
				}
			}
		}

		// Token: 0x060088E5 RID: 35045 RVA: 0x00106354 File Offset: 0x00104554
		public void RemoveAllChain()
		{
			foreach (GameCard.Chain chain in this.chains)
			{
				global::UnityEngine.Object.Destroy(chain.chainSpot.gameObject, 1f);
			}
			this.chains.Clear();
		}

		// Token: 0x0400C3B1 RID: 50097
		private Card data = new Card();

		// Token: 0x0400C3B2 RID: 50098
		private readonly Card lastValidData = new Card();

		// Token: 0x0400C3B3 RID: 50099
		public GPS p;

		// Token: 0x0400C3B4 RID: 50100
		private bool m_disabled;

		// Token: 0x0400C3B5 RID: 50101
		public bool negated;

		// Token: 0x0400C3B6 RID: 50102
		public bool disabledInChain;

		// Token: 0x0400C3B7 RID: 50103
		public bool SemiNomiSummoned;

		// Token: 0x0400C3B8 RID: 50104
		public int selectPtr;

		// Token: 0x0400C3B9 RID: 50105
		public int levelForSelect_1;

		// Token: 0x0400C3BA RID: 50106
		public int levelForSelect_2;

		// Token: 0x0400C3BB RID: 50107
		public int counterCanCount;

		// Token: 0x0400C3BC RID: 50108
		public int counterSelected;

		// Token: 0x0400C3BD RID: 50109
		public List<GameCard> targets = new List<GameCard>();

		// Token: 0x0400C3BE RID: 50110
		public List<GameCard> effectTargets = new List<GameCard>();

		// Token: 0x0400C3BF RID: 50111
		public List<GameCard> overlays = new List<GameCard>();

		// Token: 0x0400C3C0 RID: 50112
		public GameCard overlayParent;

		// Token: 0x0400C3C1 RID: 50113
		public GameCard equipedCard;

		// Token: 0x0400C3C2 RID: 50114
		public List<Effect> effects = new List<Effect>();

		// Token: 0x0400C3C3 RID: 50115
		public bool forSelect;

		// Token: 0x0400C3C4 RID: 50116
		public int overFatherCount;

		// Token: 0x0400C3C5 RID: 50117
		private const float closeupLineColorIntensity = 1.5f;

		// Token: 0x0400C3C6 RID: 50118
		public GameObject model;

		// Token: 0x0400C3C7 RID: 50119
		public ElementObjectManager manager;

		// Token: 0x0400C3C8 RID: 50120
		public GPS cacheP;

		// Token: 0x0400C3C9 RID: 50121
		private bool inAnimation;

		// Token: 0x0400C3CA RID: 50122
		private bool clicked;

		// Token: 0x0400C3CB RID: 50123
		private bool hover;

		// Token: 0x0400C3CC RID: 50124
		private bool hoving;

		// Token: 0x0400C3CD RID: 50125
		private CancellationTokenSource cts;

		// Token: 0x0400C3CE RID: 50126
		private bool isRenderTexture;

		// Token: 0x0400C3CF RID: 50127
		public static float handAngle = -10f;

		// Token: 0x0400C3D0 RID: 50128
		private bool handDefault;

		// Token: 0x0400C3D1 RID: 50129
		private bool appealed;

		// Token: 0x0400C3D2 RID: 50130
		private readonly float[] bounceDutationsHuge = new float[] { 0.4f, 0.2f, 0.133f, 0.133f, 0.067f, 0.067f };

		// Token: 0x0400C3D3 RID: 50131
		private readonly float[] bounceDutationsSlight = new float[] { 0.2f, 0.133f, 0.133f, 0.067f };

		// Token: 0x0400C3D4 RID: 50132
		private bool hightYellow;

		// Token: 0x0400C3D5 RID: 50133
		public List<GameCard.DuelButtonInfo> buttons = new List<GameCard.DuelButtonInfo>();

		// Token: 0x0400C3D6 RID: 50134
		private readonly List<DuelButton> buttonObjs = new List<DuelButton>();

		// Token: 0x0400C3D7 RID: 50135
		public bool labelShowing;

		// Token: 0x0400C3D8 RID: 50136
		private const string upColor = "<color=#00FFFF>";

		// Token: 0x0400C3D9 RID: 50137
		private const string upGrayColor = "<color=#009999>";

		// Token: 0x0400C3DA RID: 50138
		private const string normalColor = "<color=#FFFFFF>";

		// Token: 0x0400C3DB RID: 50139
		private const string normalGrayColor = "<color=#999999>";

		// Token: 0x0400C3DC RID: 50140
		private const string downColor = "<color=#FF0000>";

		// Token: 0x0400C3DD RID: 50141
		private const string downGrayColor = "<color=#990000>";

		// Token: 0x0400C3DE RID: 50142
		private const string colorEndLabel = "</color>";

		// Token: 0x0400C3DF RID: 50143
		private const string smallSize = "<size=75%>";

		// Token: 0x0400C3E0 RID: 50144
		private const string sizeEndLabel = "</size>";

		// Token: 0x0400C3E1 RID: 50145
		private int attack;

		// Token: 0x0400C3E2 RID: 50146
		private int defense;

		// Token: 0x0400C3E3 RID: 50147
		private float changeTime = 0.6f;

		// Token: 0x0400C3E4 RID: 50148
		private int lastAttribute;

		// Token: 0x0400C3E5 RID: 50149
		private int lastRace;

		// Token: 0x0400C3E6 RID: 50150
		private bool closeupShowing;

		// Token: 0x0400C3E7 RID: 50151
		private int setTurn;

		// Token: 0x0400C3E8 RID: 50152
		public bool setOverTurn;

		// Token: 0x0400C3E9 RID: 50153
		private Dictionary<int, int> cardCounters = new Dictionary<int, int>();

		// Token: 0x0400C3EA RID: 50154
		public MultiStringMaster tails = new MultiStringMaster();

		// Token: 0x0400C3EB RID: 50155
		public List<GameCard.Chain> chains = new List<GameCard.Chain>();

		// Token: 0x020011FD RID: 4605
		public struct DuelButtonInfo
		{
			// Token: 0x0400C3EC RID: 50156
			public List<int> response;

			// Token: 0x0400C3ED RID: 50157
			public string hint;

			// Token: 0x0400C3EE RID: 50158
			public ButtonType type;
		}

		// Token: 0x020011FE RID: 4606
		public class Chain
		{
			// Token: 0x0400C3EF RID: 50159
			public int i;

			// Token: 0x0400C3F0 RID: 50160
			public DuelChainSpot chainSpot;
		}

		// Token: 0x020011FF RID: 4607
		public enum Condition
		{
			// Token: 0x0400C3F2 RID: 50162
			None,
			// Token: 0x0400C3F3 RID: 50163
			Chaining,
			// Token: 0x0400C3F4 RID: 50164
			Selected
		}

		// Token: 0x02001200 RID: 4608
		private enum CardRuleCondition
		{
			// Token: 0x0400C3F6 RID: 50166
			MeUpAtk,
			// Token: 0x0400C3F7 RID: 50167
			MeUpDef,
			// Token: 0x0400C3F8 RID: 50168
			MeDownAtk,
			// Token: 0x0400C3F9 RID: 50169
			MeDownDef,
			// Token: 0x0400C3FA RID: 50170
			OpUpAtk,
			// Token: 0x0400C3FB RID: 50171
			OpUpDef,
			// Token: 0x0400C3FC RID: 50172
			OpDownAtk,
			// Token: 0x0400C3FD RID: 50173
			OpDownDef,
			// Token: 0x0400C3FE RID: 50174
			MeUpDeck,
			// Token: 0x0400C3FF RID: 50175
			MeDownDeck,
			// Token: 0x0400C400 RID: 50176
			OpUpDeck,
			// Token: 0x0400C401 RID: 50177
			OpDownDeck,
			// Token: 0x0400C402 RID: 50178
			MeUpExDeck,
			// Token: 0x0400C403 RID: 50179
			MeDownExDeck,
			// Token: 0x0400C404 RID: 50180
			OpUpExDeck,
			// Token: 0x0400C405 RID: 50181
			OpDownExDeck,
			// Token: 0x0400C406 RID: 50182
			MeUpGrave,
			// Token: 0x0400C407 RID: 50183
			MeDownGrave,
			// Token: 0x0400C408 RID: 50184
			OpUpGrave,
			// Token: 0x0400C409 RID: 50185
			OpDownGrave,
			// Token: 0x0400C40A RID: 50186
			MeUpRemoved,
			// Token: 0x0400C40B RID: 50187
			MeDownRemoved,
			// Token: 0x0400C40C RID: 50188
			OpUpRemoved,
			// Token: 0x0400C40D RID: 50189
			OpDownRemoved,
			// Token: 0x0400C40E RID: 50190
			MeUpHand,
			// Token: 0x0400C40F RID: 50191
			MeDownHand,
			// Token: 0x0400C410 RID: 50192
			OpUpHand,
			// Token: 0x0400C411 RID: 50193
			OpDownHand
		}
	}
}
