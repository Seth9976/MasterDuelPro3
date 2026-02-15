using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MDPro3.Duel
{
	// Token: 0x020014C3 RID: 5315
	public class DuelLog : MonoBehaviour
	{
		// Token: 0x06009B30 RID: 39728 RVA: 0x001806DA File Offset: 0x0017E8DA
		private void Start()
		{
			this.scrollRect.verticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.Refresh));
			this.scrollRect.GetComponent<DoWhenOnDrag>().action = delegate
			{
				this.draged = true;
			};
		}

		// Token: 0x06009B31 RID: 39729 RVA: 0x0018071C File Offset: 0x0017E91C
		public void Show()
		{
			this.showing = true;
			AudioManager.PlaySE("SE_LOG_OPEN", 1f);
			this.baseRect.DOAnchorPosX(-20f, 0.2f, false).SetUpdate(true);
			this.baseRect.localScale = Vector3.one * Config.GetUIScale(1.15f);
		}

		// Token: 0x06009B32 RID: 39730 RVA: 0x0018077C File Offset: 0x0017E97C
		public void Hide(bool mute = false)
		{
			this.showing = false;
			this.draged = false;
			this.baseRect.DOAnchorPosX(400f * Config.GetUIScale(1.15f) + SafeAreaAdapter.GetSafeAreaRightOffset(), 0.2f, false).SetUpdate(true);
			if (!mute)
			{
				AudioManager.PlaySE("SE_LOG_CLOSE", 1f);
			}
		}

		// Token: 0x06009B33 RID: 39731 RVA: 0x001807D8 File Offset: 0x0017E9D8
		public void AddLog(GameObject item, bool indent = false)
		{
			RectTransform rect = item.GetComponent<RectTransform>();
			float height = rect.rect.height;
			rect.SetParent(this.scrollRect.content, false);
			rect.sizeDelta = new Vector2(0f, height);
			rect.anchoredPosition = new Vector2(0f, -this.fullHeight);
			this.fullHeight += height;
			this.scrollRect.content.sizeDelta = new Vector2(0f, this.fullHeight);
			if (indent || (LogMessage.chainSolvingIndex > 0 && rect.GetChild(1).name == "Image Side"))
			{
				rect.GetChild(0).gameObject.SetActive(false);
				rect.GetChild(1).gameObject.SetActive(false);
				rect.offsetMin = new Vector2(50f, rect.offsetMin.y);
				rect.offsetMax = new Vector2(0f, rect.offsetMax.y);
			}
			if (!this.showing && this.fullHeight > this.scrollRect.viewport.rect.height)
			{
				item.SetActive(false);
			}
			if (!this.draged)
			{
				this.scrollRect.DOVerticalNormalizedPos(0f, 0.1f, false).SetUpdate(true);
			}
		}

		// Token: 0x06009B34 RID: 39732 RVA: 0x00180934 File Offset: 0x0017EB34
		public void ClearLog()
		{
			this.scrollRect.content.DestroyAllChildren();
			this.fullHeight = 0f;
		}

		// Token: 0x06009B35 RID: 39733 RVA: 0x00180954 File Offset: 0x0017EB54
		private void Refresh(float value)
		{
			if (!this.showing)
			{
				return;
			}
			Rect visibleRect = this.GetVisibleRect();
			int stage = 0;
			bool visible = false;
			if (value > 0.5f)
			{
				for (int i = 0; i < this.scrollRect.content.childCount; i++)
				{
					RectTransform childRect = this.scrollRect.content.GetChild(i) as RectTransform;
					if (stage < 2)
					{
						bool isVisible = this.IsRectVisible(childRect, visibleRect);
						if (visible != isVisible)
						{
							visible = isVisible;
							stage++;
						}
						childRect.gameObject.SetActive(isVisible);
					}
					else
					{
						childRect.gameObject.SetActive(false);
					}
				}
				return;
			}
			for (int j = this.scrollRect.content.childCount - 1; j >= 0; j--)
			{
				RectTransform childRect2 = this.scrollRect.content.GetChild(j) as RectTransform;
				if (stage < 2)
				{
					bool isVisible2 = this.IsRectVisible(childRect2, visibleRect);
					if (visible != isVisible2)
					{
						visible = isVisible2;
						stage++;
					}
					childRect2.gameObject.SetActive(isVisible2);
				}
				else
				{
					childRect2.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x06009B36 RID: 39734 RVA: 0x00180A5C File Offset: 0x0017EC5C
		private Rect GetVisibleRect()
		{
			Rect viewportRect = this.scrollRect.viewport.rect;
			float bottom = -this.scrollRect.content.anchoredPosition.y - viewportRect.height;
			return new Rect(0f, bottom, viewportRect.width, viewportRect.height);
		}

		// Token: 0x06009B37 RID: 39735 RVA: 0x00180AB4 File Offset: 0x0017ECB4
		private bool IsRectVisible(RectTransform rectTransform, Rect visibleRect)
		{
			float y = rectTransform.anchoredPosition.y;
			float bottom = y - rectTransform.rect.height;
			return y > visibleRect.yMin && bottom < visibleRect.yMax;
		}

		// Token: 0x06009B38 RID: 39736 RVA: 0x00180AF4 File Offset: 0x0017ECF4
		public void AddSingleCardMessageToLog(int code, GPS from, GPS to, string reason, bool indent = false)
		{
			OcgCore core = Program.instance.ocgcore;
			GameObject item = ABLoader.LoadMasterDuelGameObject((code > 0) ? "DuelLogSingleCard" : "DuelLogSingleCard2");
			Color targetColor = (to.InMyControl() ? DuelLog.myColor : DuelLog.opColor);
			targetColor.a = 0.75f;
			item.transform.GetChild(1).GetComponent<Image>().color = targetColor;
			if (code > 0)
			{
				item.transform.GetChild(2).GetComponent<Text>().text = CardsManager.Get(code, false).Name;
			}
			item.transform.GetChild(3).GetComponent<Text>().text = reason;
			RawImage cardFace = item.transform.GetChild(4).GetComponent<RawImage>();
			if (code > 0)
			{
				Program.instance.texture_.LoadCardToRawImageWithoutMaterialAsync(cardFace, code, true);
			}
			else
			{
				cardFace.texture = null;
				cardFace.material = ((to.controller == 0U) ? OcgCore.myProtector : OcgCore.opProtector);
				cardFace.transform.GetChild(0).gameObject.SetActive(false);
			}
			cardFace.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
			{
				core.GetUI<OcgCoreUI>().CardDescription.Show(null, null, code, to);
			});
			if (to.InPosition(CardPosition.Defence) && to.InLocation(CardLocation.MonsterZone))
			{
				cardFace.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
			}
			if (to.InPosition(CardPosition.FaceUp))
			{
				cardFace.transform.GetChild(0).gameObject.SetActive(false);
			}
			List<Sprite> icons = TextureManager.container.GetLocationIcons(from ?? to);
			if (icons.Count == 2)
			{
				item.transform.GetChild(5).GetComponent<Image>().sprite = icons[1];
				item.transform.GetChild(5).GetChild(0).GetComponent<Image>()
					.sprite = icons[0];
			}
			else
			{
				item.transform.GetChild(5).GetComponent<Image>().sprite = icons[0];
				item.transform.GetChild(5).GetChild(0).gameObject.SetActive(false);
			}
			if (from != null)
			{
				if (to.controller == 0U)
				{
					item.transform.GetChild(6).GetComponent<Image>().color = DuelLog.myArrowColor;
				}
				else
				{
					item.transform.GetChild(6).GetComponent<Image>().color = DuelLog.opArrowColor;
				}
				icons = TextureManager.container.GetLocationIcons(to);
				if (icons.Count == 2)
				{
					item.transform.GetChild(7).GetComponent<Image>().sprite = icons[1];
					item.transform.GetChild(7).GetChild(0).GetComponent<Image>()
						.sprite = icons[0];
				}
				else
				{
					item.transform.GetChild(7).GetComponent<Image>().sprite = icons[0];
					item.transform.GetChild(7).GetChild(0).gameObject.SetActive(false);
				}
			}
			else
			{
				item.transform.GetChild(6).gameObject.SetActive(false);
				item.transform.GetChild(7).gameObject.SetActive(false);
			}
			this.AddLog(item, indent);
		}

		// Token: 0x06009B39 RID: 39737 RVA: 0x00180E80 File Offset: 0x0017F080
		public void AddLpPChangeMessageToLog(int player, string reason, int value, bool red = true, bool indent = false)
		{
			OcgCore core = Program.instance.ocgcore;
			GameObject item = ABLoader.LoadMasterDuelGameObject("DuelLogLpChange");
			Color targetColor = ((player == 0) ? DuelLog.myColor : DuelLog.opColor);
			item.transform.GetChild(1).GetComponent<Image>().color = targetColor;
			Image component = item.transform.GetChild(2).GetComponent<Image>();
			component.material = ((player == 0) ? core.GetUI<OcgCoreUI>().AvatarPlayer0.material : core.GetUI<OcgCoreUI>().AvatarPlayer1.material);
			component.sprite = ((player == 0) ? core.GetUI<OcgCoreUI>().AvatarPlayer0.sprite : core.GetUI<OcgCoreUI>().AvatarPlayer1.sprite);
			item.transform.GetChild(3).GetComponent<Text>().text = reason;
			item.transform.GetChild(4).GetComponent<Text>().text = value.ToString();
			item.transform.GetChild(4).GetComponent<Text>().color = (red ? DuelLog.damageColor : DuelLog.recoverColor);
			int lp = ((player == 0) ? LogMessage.life0 : LogMessage.life1);
			if (lp < 0)
			{
				lp = 0;
			}
			item.transform.GetChild(6).GetComponent<Text>().text = lp.ToString();
			this.AddLog(item, indent);
		}

		// Token: 0x0400D931 RID: 55601
		public static Color myColor = Color.blue;

		// Token: 0x0400D932 RID: 55602
		public static Color opColor = Color.red;

		// Token: 0x0400D933 RID: 55603
		public static Color myArrowColor = new Color(0f, 0.5f, 1f, 1f);

		// Token: 0x0400D934 RID: 55604
		public static Color opArrowColor = new Color(1f, 0.2f, 0.2f, 1f);

		// Token: 0x0400D935 RID: 55605
		public static Color myChainColor = new Color(0.2f, 0.6f, 1f, 1f);

		// Token: 0x0400D936 RID: 55606
		public static Color opChainColor = new Color(1f, 0.2f, 0.2f, 1f);

		// Token: 0x0400D937 RID: 55607
		public static Color damageColor = Color.red;

		// Token: 0x0400D938 RID: 55608
		public static Color recoverColor = new Color(0f, 0.7f, 1f, 1f);

		// Token: 0x0400D939 RID: 55609
		public RectTransform baseRect;

		// Token: 0x0400D93A RID: 55610
		public ScrollRect scrollRect;

		// Token: 0x0400D93B RID: 55611
		public bool showing;

		// Token: 0x0400D93C RID: 55612
		private bool draged;

		// Token: 0x0400D93D RID: 55613
		private float fullHeight;
	}
}
