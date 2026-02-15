using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace MDPro3.UI
{
	// Token: 0x02001430 RID: 5168
	public class SubMenu : SidePanel
	{
		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x0600961D RID: 38429 RVA: 0x0015B974 File Offset: 0x00159B74
		private ScrollRect MenuList
		{
			get
			{
				return this.m_MenuList = ((this.m_MenuList != null) ? this.m_MenuList : base.Manager.GetElement<ScrollRect>("MenuList"));
			}
		}

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x0600961E RID: 38430 RVA: 0x0015B9B0 File Offset: 0x00159BB0
		private GameObject TitleTemplate
		{
			get
			{
				return this.m_TitleTemplate = ((this.m_TitleTemplate != null) ? this.m_TitleTemplate : base.Manager.GetNestedElement("MenuList/TitleTemplate"));
			}
		}

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x0600961F RID: 38431 RVA: 0x0015B9EC File Offset: 0x00159BEC
		private GameObject ButtonTemplate
		{
			get
			{
				return this.m_ButtonTemplate = ((this.m_ButtonTemplate != null) ? this.m_ButtonTemplate : base.Manager.GetNestedElement("MenuList/ButtonTemplate"));
			}
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06009620 RID: 38432 RVA: 0x0015BA28 File Offset: 0x00159C28
		private GameObject Spacer
		{
			get
			{
				return this.m_Spacer = ((this.m_Spacer != null) ? this.m_Spacer : base.Manager.GetNestedElement("MenuList/Spacer"));
			}
		}

		// Token: 0x06009621 RID: 38433 RVA: 0x0015BA64 File Offset: 0x00159C64
		protected override void Awake()
		{
			base.Awake();
			this.TitleTemplate.SetActive(false);
			this.ButtonTemplate.SetActive(false);
			this.Spacer.SetActive(false);
		}

		// Token: 0x06009622 RID: 38434 RVA: 0x0015BA90 File Offset: 0x00159C90
		public void Show(List<string> menus, List<Action> actions)
		{
			base.Show();
			base.StartCoroutine(this.GenItemsAsync(menus, actions));
			this.MenuList.verticalNormalizedPosition = 1f;
		}

		// Token: 0x06009623 RID: 38435 RVA: 0x0015BAB7 File Offset: 0x00159CB7
		private IEnumerator GenItemsAsync(List<string> menus, List<Action> actions)
		{
			int index = -1;
			int num;
			for (int i = 0; i < menus.Count; i = num + 1)
			{
				if (actions[i] == null)
				{
					if (i != 0)
					{
						GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.Spacer);
						gameObject.transform.SetParent(this.MenuList.content, false);
						gameObject.SetActive(true);
					}
					GameObject gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(this.TitleTemplate);
					gameObject2.transform.SetParent(this.MenuList.content, false);
					gameObject2.GetComponent<ElementObjectManager>().GetElement<TextMeshProUGUI>("TitleText").text = menus[i];
					gameObject2.SetActive(true);
					RectTransform rect = gameObject2.transform.GetChild(0).GetComponent<RectTransform>();
					rect.anchoredPosition3D = new Vector3(150f, 0f, 0f);
					DOTween.Sequence().AppendInterval(0.09f).Append(rect.DOAnchorPosX(0f, 0.3f, false).SetEase(Ease.OutQuart));
					CanvasGroup cg = rect.GetComponent<CanvasGroup>();
					cg.alpha = 0f;
					DOTween.Sequence().AppendInterval(0.09f).Append(cg.DOFade(1f, 0.3f));
					yield return new WaitForSeconds(0.03f);
				}
				else
				{
					num = index;
					index = num + 1;
					GameObject gameObject3 = global::UnityEngine.Object.Instantiate<GameObject>(this.ButtonTemplate);
					gameObject3.transform.SetParent(this.MenuList.content, false);
					gameObject3.SetActive(true);
					SelectionButton selection = gameObject3.GetComponent<SelectionButton>();
					selection.SetButtonText(menus[i]);
					selection.index = index;
					if (this.defaultSelectable == null)
					{
						this.defaultSelectable = selection.GetSelectable();
						if (UserInput.gamepadType != UserInput.GamepadType.None)
						{
							selection.GetSelectable().Select();
						}
					}
					int j = i;
					selection.SetClickEvent(delegate
					{
						actions[j]();
						this.Hide();
					});
					RectTransform rect2 = gameObject3.transform.GetChild(0).GetComponent<RectTransform>();
					rect2.anchoredPosition3D = new Vector3(150f, 0f, 0f);
					DOTween.Sequence().AppendInterval(0.12f).Append(rect2.DOAnchorPosX(0f, 0.3f, false).SetEase(Ease.OutQuart));
					CanvasGroup cg2 = rect2.GetComponent<CanvasGroup>();
					cg2.alpha = 0f;
					DOTween.Sequence().AppendInterval(0.12f).Append(cg2.DOFade(1f, 0.3f));
					yield return new WaitForSeconds(0.03f);
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x0400D461 RID: 54369
		private const string LABEL_SR_MENULIST = "MenuList";

		// Token: 0x0400D462 RID: 54370
		private ScrollRect m_MenuList;

		// Token: 0x0400D463 RID: 54371
		private const string LABEL_GO_TITLETEMPLATE = "MenuList/TitleTemplate";

		// Token: 0x0400D464 RID: 54372
		private GameObject m_TitleTemplate;

		// Token: 0x0400D465 RID: 54373
		private const string LABEL_GO_BUTTONTEMPLATE = "MenuList/ButtonTemplate";

		// Token: 0x0400D466 RID: 54374
		private GameObject m_ButtonTemplate;

		// Token: 0x0400D467 RID: 54375
		private const string LABEL_GO_SPACER = "MenuList/Spacer";

		// Token: 0x0400D468 RID: 54376
		private GameObject m_Spacer;
	}
}
