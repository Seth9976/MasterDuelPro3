using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;
using YgomSystem.UI;

namespace MDPro3.UI
{
	// Token: 0x020013AB RID: 5035
	public class SelectionButton_MainMenu : SelectionButton
	{
		// Token: 0x060091E3 RID: 37347 RVA: 0x0014519E File Offset: 0x0014339E
		protected override void Awake()
		{
			this.ElementsReset();
		}

		// Token: 0x060091E4 RID: 37348 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnDisable()
		{
		}

		// Token: 0x060091E5 RID: 37349 RVA: 0x001451A8 File Offset: 0x001433A8
		private void ElementsReset()
		{
			base.Manager.GetElement<CanvasGroup>("Out").alpha = 1f;
			base.Manager.GetElement<RectTransform>("Line").localScale = Vector3.one;
			base.Manager.GetElement<CanvasGroup>("Hover").alpha = 0f;
			base.Manager.GetElement<RectTransform>("PlateTween").localScale = new Vector3(0.5f, 1f, 1f);
			base.Manager.GetElement<RectTransform>("HoverTextMask").offsetMax = new Vector2(-340f, 0f);
			base.Manager.GetElement<RectTransform>("Arrow").localPosition = new Vector2(-5f, 0f);
			base.Manager.GetElement<RectTransform>("Corner").offsetMin = new Vector2(0f, 0f);
			base.Manager.GetElement<RectTransform>("Corner").offsetMax = new Vector2(0f, 0f);
			ColorContainerGraphic[] componentsInChildren = base.transform.GetComponentsInChildren<ColorContainerGraphic>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetColor(ColorContainer.SelectMode.Unselected, ColorContainer.StatusMode.Normal, true);
			}
		}

		// Token: 0x060091E6 RID: 37350 RVA: 0x001452E8 File Offset: 0x001434E8
		protected override void HoverOn()
		{
			if (this.hoverd)
			{
				return;
			}
			base.HoverOn();
			base.Manager.GetElement<CanvasGroup>("Out").alpha = 0f;
			base.Manager.GetElement<RectTransform>("PlateTween").localScale = new Vector3(0.5f, 1f, 1f);
			TweenerCore<Vector3, Vector3, VectorOptions> tween = base.Manager.GetElement<RectTransform>("PlateTween").DOScaleX(1f, 0.33f).SetEase(Ease.OutQuart);
			this.hoverOnTweens.Add(tween);
			base.Manager.GetElement<DOTweenAnimation>("PlateBlink").DOPlay();
			base.Manager.GetElement<RectTransform>("HoverTextMask").offsetMax = new Vector2(-340f, 0f);
			base.Manager.GetElement<RectTransform>("HoverTextMask").DOSizeDelta(Vector2.zero, 0.2f, false);
			base.Manager.GetElement<RectTransform>("Arrow").anchoredPosition = new Vector2(-262f, 0f);
			TweenerCore<Vector2, Vector2, VectorOptions> tween2 = base.Manager.GetElement<RectTransform>("Arrow").DOAnchorPosX(-5f, 0.33f, false).SetEase(Ease.OutQuart);
			this.hoverOnTweens.Add(tween2);
		}

		// Token: 0x060091E7 RID: 37351 RVA: 0x0014542C File Offset: 0x0014362C
		protected override void HoverOff(bool force = false)
		{
			base.HoverOff(false);
			base.Manager.GetElement<CanvasGroup>("Out").alpha = 1f;
			base.Manager.GetElement<RectTransform>("Line").localScale = new Vector3(7f, 1f, 1f);
			TweenerCore<Vector3, Vector3, VectorOptions> tween = base.Manager.GetElement<RectTransform>("Line").DOScaleX(1f, 0.4f).SetEase(Ease.OutQuart);
			this.hoverOffTweens.Add(tween);
			base.Manager.GetElement<DOTweenAnimation>("PlateBlink").DOPause();
		}

		// Token: 0x060091E8 RID: 37352 RVA: 0x001454CC File Offset: 0x001436CC
		protected override void OnNavigation(AxisEventData eventData)
		{
			base.OnNavigation(eventData);
			if (eventData.moveVector.y > 0f && base.Selectable.navigation.selectOnUp != null)
			{
				UserInput.RumbleForUp();
				return;
			}
			if (eventData.moveVector.y < 0f && base.Selectable.navigation.selectOnDown != null)
			{
				UserInput.RumbleForDown();
			}
		}

		// Token: 0x060091E9 RID: 37353 RVA: 0x00145545 File Offset: 0x00143745
		protected override void OnSelect(bool playSE)
		{
			base.OnSelect(playSE);
			Program.instance.menu.lastSelectedButton = this;
		}
	}
}
