using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013AD RID: 5037
	[RequireComponent(typeof(Button))]
	public class SelectionButton_Setting : SelectionButton
	{
		// Token: 0x060091F3 RID: 37363 RVA: 0x00145666 File Offset: 0x00143866
		public void SetModeText(string text)
		{
			if (this.modeText == null)
			{
				return;
			}
			this.modeText.text = text;
		}

		// Token: 0x060091F4 RID: 37364 RVA: 0x00145683 File Offset: 0x00143883
		public string GetModeText()
		{
			if (this.modeText == null)
			{
				return string.Empty;
			}
			return this.modeText.text;
		}

		// Token: 0x060091F5 RID: 37365 RVA: 0x001456A4 File Offset: 0x001438A4
		public void SetNoteText(string text)
		{
			if (this.noteText == null)
			{
				return;
			}
			this.noteText.text = text;
		}

		// Token: 0x060091F6 RID: 37366 RVA: 0x001456C1 File Offset: 0x001438C1
		public void SetSliderEvent(UnityAction<float> onValueChanged)
		{
			if (this.slider == null)
			{
				return;
			}
			this.slider.onValueChanged.AddListener(onValueChanged);
		}

		// Token: 0x060091F7 RID: 37367 RVA: 0x001456E3 File Offset: 0x001438E3
		public void SetSliderValue(float value)
		{
			if (this.slider == null)
			{
				return;
			}
			this.slider.value = value;
		}

		// Token: 0x060091F8 RID: 37368 RVA: 0x00145700 File Offset: 0x00143900
		public float GetSliderValue()
		{
			if (this.slider == null)
			{
				return 0f;
			}
			return this.slider.value;
		}

		// Token: 0x060091F9 RID: 37369 RVA: 0x00145724 File Offset: 0x00143924
		protected override void HoverOn()
		{
			if (this.hoverd)
			{
				return;
			}
			base.HoverOn();
			if (this.arrow != null)
			{
				this.arrow.anchoredPosition = new Vector3(-36f, 0f, 0f);
				TweenerCore<Vector2, Vector2, VectorOptions> tween = this.arrow.DOAnchorPosX(0f, 0.3f, false).SetEase(Ease.OutCubic);
				this.hoverOnTweens.Add(tween);
			}
		}

		// Token: 0x060091FA RID: 37370 RVA: 0x0014579C File Offset: 0x0014399C
		protected override void HoverOff(bool force = false)
		{
			base.HoverOff(false);
			if (this.arrow != null)
			{
				TweenerCore<Vector2, Vector2, VectorOptions> tween = this.arrow.DOAnchorPosX(0f, 0.3f, false).SetEase(Ease.OutCubic);
				this.hoverOffTweens.Add(tween);
			}
		}

		// Token: 0x060091FB RID: 37371 RVA: 0x001457E8 File Offset: 0x001439E8
		protected override void OnNavigation(AxisEventData eventData)
		{
			base.OnNavigation(eventData);
			if (this.slider == null)
			{
				return;
			}
			float addtion = eventData.moveVector.x * this.slider.maxValue * 0.05f;
			if (this.slider.wholeNumbers)
			{
				addtion = eventData.moveVector.x;
			}
			this.slider.value += addtion;
		}

		// Token: 0x060091FC RID: 37372 RVA: 0x00145855 File Offset: 0x00143A55
		protected override void OnSelect(bool playSE)
		{
			base.OnSelect(playSE);
			Program.instance.setting.lastSelectedButton = this;
		}

		// Token: 0x0400D094 RID: 53396
		[Header("Setting")]
		[SerializeField]
		private Slider slider;

		// Token: 0x0400D095 RID: 53397
		[SerializeField]
		private RectTransform arrow;

		// Token: 0x0400D096 RID: 53398
		[SerializeField]
		private TextMeshProUGUI modeText;

		// Token: 0x0400D097 RID: 53399
		[SerializeField]
		private TextMeshProUGUI noteText;
	}
}
