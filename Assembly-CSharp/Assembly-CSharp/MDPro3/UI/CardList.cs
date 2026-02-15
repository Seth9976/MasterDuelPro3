using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200135B RID: 4955
	public class CardList : MonoBehaviour
	{
		// Token: 0x06008F92 RID: 36754 RVA: 0x001370B4 File Offset: 0x001352B4
		public void Show(List<GameCard> cards, CardLocation location, int controller)
		{
			if (OcgCore.cantCheckGrave && location == CardLocation.Grave)
			{
				MessageManager.Cast(InterString.Get("现在不能查看此处的卡片。", 0));
				return;
			}
			this.cards = cards;
			this.location = location;
			this.controller = controller;
			if (!this.showing)
			{
				this.RefreshList();
				this.baseRect.DOAnchorPosX(-30f, this.transitionTime, false);
				if (Program.instance.ocgcore.GetUI<OcgCoreUI>().DuelLog.showing)
				{
					Program.instance.ocgcore.GetUI<OcgCoreUI>().OnLog(true);
					this.showWithCloseDuelLog = true;
				}
			}
			else
			{
				this.baseRect.DOAnchorPosX(150f, this.transitionTime, false).OnComplete(delegate
				{
					this.RefreshList();
					this.baseRect.DOAnchorPosX(-30f, this.transitionTime, false);
				});
			}
			this.showing = true;
			this.baseRect.localScale = Vector3.one * Config.GetUIScale(1.18f);
		}

		// Token: 0x06008F93 RID: 36755 RVA: 0x001371A4 File Offset: 0x001353A4
		public void Hide()
		{
			if (!this.showing)
			{
				return;
			}
			this.showing = false;
			this.baseRect.DOAnchorPosX(150f * Config.GetUIScale(1.18f) + SafeAreaAdapter.GetSafeAreaRightOffset(), 0.3f, false);
			if (this.showWithCloseDuelLog)
			{
				this.showWithCloseDuelLog = false;
				Program.instance.ocgcore.GetUI<OcgCoreUI>().OnLog(false);
			}
		}

		// Token: 0x06008F94 RID: 36756 RVA: 0x00137210 File Offset: 0x00135410
		private void RefreshList()
		{
			this.locationIcon.sprite = CardList.GetListLocationIcon(this.location, this.controller);
			this.ClearList();
			this.scrollRect.content.sizeDelta = new Vector2(this.scrollRect.content.sizeDelta.x, (float)(140 * this.cards.Count));
			for (int i = 0; i < this.cards.Count; i++)
			{
				GameObject go = global::UnityEngine.Object.Instantiate<GameObject>(this.item);
				go.SetActive(true);
				this.cardObjs.Add(go);
				go.transform.SetParent(this.scrollRect.content, false);
				go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, (float)(-140 * (this.cards.Count - 1 - i)));
				go.GetComponent<CardListItem>().card = this.cards[i];
			}
		}

		// Token: 0x06008F95 RID: 36757 RVA: 0x0013730C File Offset: 0x0013550C
		private void ClearList()
		{
			foreach (GameObject gameObject in this.cardObjs)
			{
				global::UnityEngine.Object.Destroy(gameObject);
			}
			this.cardObjs.Clear();
		}

		// Token: 0x06008F96 RID: 36758 RVA: 0x00137368 File Offset: 0x00135568
		public static Sprite GetListLocationIcon(CardLocation location, int controller)
		{
			if (controller == 0)
			{
				if ((location & CardLocation.Deck) > CardLocation.Unknown)
				{
					return TextureManager.container.listMyDeck;
				}
				if ((location & CardLocation.Extra) > CardLocation.Unknown)
				{
					return TextureManager.container.listMyExtra;
				}
				if ((location & CardLocation.Grave) > CardLocation.Unknown)
				{
					return TextureManager.container.listMyGrave;
				}
				if ((location & CardLocation.Removed) > CardLocation.Unknown)
				{
					return TextureManager.container.listMyRemoved;
				}
				return TextureManager.container.listMyXyz;
			}
			else
			{
				if ((location & CardLocation.Deck) > CardLocation.Unknown)
				{
					return TextureManager.container.listOpDeck;
				}
				if ((location & CardLocation.Extra) > CardLocation.Unknown)
				{
					return TextureManager.container.listOpExtra;
				}
				if ((location & CardLocation.Grave) > CardLocation.Unknown)
				{
					return TextureManager.container.listOpGrave;
				}
				if ((location & CardLocation.Removed) > CardLocation.Unknown)
				{
					return TextureManager.container.listOpRemoved;
				}
				return TextureManager.container.listOpXyz;
			}
		}

		// Token: 0x0400CDEA RID: 52714
		public RectTransform baseRect;

		// Token: 0x0400CDEB RID: 52715
		public Image locationIcon;

		// Token: 0x0400CDEC RID: 52716
		public ScrollRect scrollRect;

		// Token: 0x0400CDED RID: 52717
		public GameObject item;

		// Token: 0x0400CDEE RID: 52718
		private bool showing;

		// Token: 0x0400CDEF RID: 52719
		private List<GameCard> cards;

		// Token: 0x0400CDF0 RID: 52720
		private List<GameObject> cardObjs = new List<GameObject>();

		// Token: 0x0400CDF1 RID: 52721
		private float transitionTime = 0.15f;

		// Token: 0x0400CDF2 RID: 52722
		private CardLocation location;

		// Token: 0x0400CDF3 RID: 52723
		private int controller;

		// Token: 0x0400CDF4 RID: 52724
		private bool showWithCloseDuelLog;
	}
}
