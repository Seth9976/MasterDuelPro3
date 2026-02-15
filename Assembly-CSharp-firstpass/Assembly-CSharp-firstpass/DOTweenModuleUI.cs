using System;
using System.Globalization;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace DG.Tweening
{
	// Token: 0x02000022 RID: 34
	public static class DOTweenModuleUI
	{
		// Token: 0x06000073 RID: 115 RVA: 0x0000332C File Offset: 0x0000152C
		public static TweenerCore<float, float, FloatOptions> DOFade(this CanvasGroup target, float endValue, float duration)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.alpha, delegate(float x)
			{
				target.alpha = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003374 File Offset: 0x00001574
		public static TweenerCore<Color, Color, ColorOptions> DOColor(this Graphic target, Color endValue, float duration)
		{
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000033BC File Offset: 0x000015BC
		public static TweenerCore<Color, Color, ColorOptions> DOFade(this Graphic target, float endValue, float duration)
		{
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003404 File Offset: 0x00001604
		public static TweenerCore<Color, Color, ColorOptions> DOColor(this Image target, Color endValue, float duration)
		{
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000344C File Offset: 0x0000164C
		public static TweenerCore<Color, Color, ColorOptions> DOFade(this Image target, float endValue, float duration)
		{
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003494 File Offset: 0x00001694
		public static TweenerCore<float, float, FloatOptions> DOFillAmount(this Image target, float endValue, float duration)
		{
			if (endValue > 1f)
			{
				endValue = 1f;
			}
			else if (endValue < 0f)
			{
				endValue = 0f;
			}
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.fillAmount, delegate(float x)
			{
				target.fillAmount = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000034FC File Offset: 0x000016FC
		public static Sequence DOGradientColor(this Image target, Gradient gradient, float duration)
		{
			Sequence s = DOTween.Sequence();
			GradientColorKey[] colors = gradient.colorKeys;
			int len = colors.Length;
			for (int i = 0; i < len; i++)
			{
				GradientColorKey c = colors[i];
				if (i == 0 && c.time <= 0f)
				{
					target.color = c.color;
				}
				else
				{
					float colorDuration = ((i == len - 1) ? (duration - s.Duration(false)) : (duration * ((i == 0) ? c.time : (c.time - colors[i - 1].time))));
					s.Append(target.DOColor(c.color, colorDuration).SetEase(Ease.Linear));
				}
			}
			s.SetTarget(target);
			return s;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000035B4 File Offset: 0x000017B4
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOFlexibleSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => new Vector2(target.flexibleWidth, target.flexibleHeight), delegate(Vector2 x)
			{
				target.flexibleWidth = x.x;
				target.flexibleHeight = x.y;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003600 File Offset: 0x00001800
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOMinSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => new Vector2(target.minWidth, target.minHeight), delegate(Vector2 x)
			{
				target.minWidth = x.x;
				target.minHeight = x.y;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000364C File Offset: 0x0000184C
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOPreferredSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => new Vector2(target.preferredWidth, target.preferredHeight), delegate(Vector2 x)
			{
				target.preferredWidth = x.x;
				target.preferredHeight = x.y;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003698 File Offset: 0x00001898
		public static TweenerCore<Color, Color, ColorOptions> DOColor(this Outline target, Color endValue, float duration)
		{
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.To(() => target.effectColor, delegate(Color x)
			{
				target.effectColor = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000036E0 File Offset: 0x000018E0
		public static TweenerCore<Color, Color, ColorOptions> DOFade(this Outline target, float endValue, float duration)
		{
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.ToAlpha(() => target.effectColor, delegate(Color x)
			{
				target.effectColor = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003728 File Offset: 0x00001928
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOScale(this Outline target, Vector2 endValue, float duration)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.effectDistance, delegate(Vector2 x)
			{
				target.effectDistance = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003770 File Offset: 0x00001970
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorPos(this RectTransform target, Vector2 endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.anchoredPosition, delegate(Vector2 x)
			{
				target.anchoredPosition = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000037BC File Offset: 0x000019BC
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorPosX(this RectTransform target, float endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.anchoredPosition, delegate(Vector2 x)
			{
				target.anchoredPosition = x;
			}, new Vector2(endValue, 0f), duration);
			tweenerCore.SetOptions(AxisConstraint.X, snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003814 File Offset: 0x00001A14
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorPosY(this RectTransform target, float endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.anchoredPosition, delegate(Vector2 x)
			{
				target.anchoredPosition = x;
			}, new Vector2(0f, endValue), duration);
			tweenerCore.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000386C File Offset: 0x00001A6C
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOAnchorPos3D(this RectTransform target, Vector3 endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.anchoredPosition3D, delegate(Vector3 x)
			{
				target.anchoredPosition3D = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000038B8 File Offset: 0x00001AB8
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOAnchorPos3DX(this RectTransform target, float endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.anchoredPosition3D, delegate(Vector3 x)
			{
				target.anchoredPosition3D = x;
			}, new Vector3(endValue, 0f, 0f), duration);
			tweenerCore.SetOptions(AxisConstraint.X, snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003914 File Offset: 0x00001B14
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOAnchorPos3DY(this RectTransform target, float endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.anchoredPosition3D, delegate(Vector3 x)
			{
				target.anchoredPosition3D = x;
			}, new Vector3(0f, endValue, 0f), duration);
			tweenerCore.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003970 File Offset: 0x00001B70
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOAnchorPos3DZ(this RectTransform target, float endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.anchoredPosition3D, delegate(Vector3 x)
			{
				target.anchoredPosition3D = x;
			}, new Vector3(0f, 0f, endValue), duration);
			tweenerCore.SetOptions(AxisConstraint.Z, snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000039CC File Offset: 0x00001BCC
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorMax(this RectTransform target, Vector2 endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.anchorMax, delegate(Vector2 x)
			{
				target.anchorMax = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003A18 File Offset: 0x00001C18
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorMin(this RectTransform target, Vector2 endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.anchorMin, delegate(Vector2 x)
			{
				target.anchorMin = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003A64 File Offset: 0x00001C64
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOPivot(this RectTransform target, Vector2 endValue, float duration)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.pivot, delegate(Vector2 x)
			{
				target.pivot = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003AAC File Offset: 0x00001CAC
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOPivotX(this RectTransform target, float endValue, float duration)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.pivot, delegate(Vector2 x)
			{
				target.pivot = x;
			}, new Vector2(endValue, 0f), duration);
			tweenerCore.SetOptions(AxisConstraint.X, false).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003B04 File Offset: 0x00001D04
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOPivotY(this RectTransform target, float endValue, float duration)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.pivot, delegate(Vector2 x)
			{
				target.pivot = x;
			}, new Vector2(0f, endValue), duration);
			tweenerCore.SetOptions(AxisConstraint.Y, false).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003B5C File Offset: 0x00001D5C
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOSizeDelta(this RectTransform target, Vector2 endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.sizeDelta, delegate(Vector2 x)
			{
				target.sizeDelta = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public static Tweener DOPunchAnchorPos(this RectTransform target, Vector2 punch, float duration, int vibrato = 10, float elasticity = 1f, bool snapping = false)
		{
			return DOTween.Punch(() => target.anchoredPosition, delegate(Vector3 x)
			{
				target.anchoredPosition = x;
			}, punch, duration, vibrato, elasticity).SetTarget(target).SetOptions(snapping);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003BFC File Offset: 0x00001DFC
		public static Tweener DOShakeAnchorPos(this RectTransform target, float duration, float strength = 100f, int vibrato = 10, float randomness = 90f, bool snapping = false, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
		{
			return DOTween.Shake(() => target.anchoredPosition, delegate(Vector3 x)
			{
				target.anchoredPosition = x;
			}, duration, strength, vibrato, randomness, true, fadeOut, randomnessMode).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake)
				.SetOptions(snapping);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003C58 File Offset: 0x00001E58
		public static Tweener DOShakeAnchorPos(this RectTransform target, float duration, Vector2 strength, int vibrato = 10, float randomness = 90f, bool snapping = false, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
		{
			return DOTween.Shake(() => target.anchoredPosition, delegate(Vector3 x)
			{
				target.anchoredPosition = x;
			}, duration, strength, vibrato, randomness, fadeOut, randomnessMode).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake)
				.SetOptions(snapping);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public static Sequence DOJumpAnchorPos(this RectTransform target, Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = 0f;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence();
			Tween yTween = DOTween.To(() => target.anchoredPosition, delegate(Vector2 x)
			{
				target.anchoredPosition = x;
			}, new Vector2(0f, jumpPower), duration / (float)(numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad)
				.SetRelative<Tweener>()
				.SetLoops(numJumps * 2, LoopType.Yoyo)
				.OnStart(delegate
				{
					startPosY = target.anchoredPosition.y;
				});
			s.Append(DOTween.To(() => target.anchoredPosition, delegate(Vector2 x)
			{
				target.anchoredPosition = x;
			}, new Vector2(endValue.x, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)).Join(yTween).SetTarget(target)
				.SetEase(DOTween.defaultEaseType);
			s.OnUpdate(delegate
			{
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = (s.isRelative ? endValue.y : (endValue.y - startPosY));
				}
				Vector2 pos = target.anchoredPosition;
				pos.y += DOVirtual.EasedValue(0f, offsetY, s.ElapsedDirectionalPercentage(), Ease.OutQuad);
				target.anchoredPosition = pos;
			});
			return s;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003DEC File Offset: 0x00001FEC
		public static Tweener DONormalizedPos(this ScrollRect target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => new Vector2(target.horizontalNormalizedPosition, target.verticalNormalizedPosition), delegate(Vector2 x)
			{
				target.horizontalNormalizedPosition = x.x;
				target.verticalNormalizedPosition = x.y;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003E38 File Offset: 0x00002038
		public static Tweener DOHorizontalNormalizedPos(this ScrollRect target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.horizontalNormalizedPosition, delegate(float x)
			{
				target.horizontalNormalizedPosition = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003E84 File Offset: 0x00002084
		public static Tweener DOVerticalNormalizedPos(this ScrollRect target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.verticalNormalizedPosition, delegate(float x)
			{
				target.verticalNormalizedPosition = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003ED0 File Offset: 0x000020D0
		public static TweenerCore<float, float, FloatOptions> DOValue(this Slider target, float endValue, float duration, bool snapping = false)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.value, delegate(float x)
			{
				target.value = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003F1C File Offset: 0x0000211C
		public static TweenerCore<Color, Color, ColorOptions> DOColor(this Text target, Color endValue, float duration)
		{
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003F64 File Offset: 0x00002164
		public static TweenerCore<int, int, NoOptions> DOCounter(this Text target, int fromValue, int endValue, float duration, bool addThousandsSeparator = true, CultureInfo culture = null)
		{
			CultureInfo cInfo = ((!addThousandsSeparator) ? null : (culture ?? CultureInfo.InvariantCulture));
			TweenerCore<int, int, NoOptions> tweenerCore = DOTween.To(() => fromValue, delegate(int x)
			{
				fromValue = x;
				target.text = (addThousandsSeparator ? fromValue.ToString("N0", cInfo) : fromValue.ToString());
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003FD8 File Offset: 0x000021D8
		public static TweenerCore<Color, Color, ColorOptions> DOFade(this Text target, float endValue, float duration)
		{
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004020 File Offset: 0x00002220
		public static TweenerCore<string, string, StringOptions> DOText(this Text target, string endValue, float duration, bool richTextEnabled = true, ScrambleMode scrambleMode = ScrambleMode.None, string scrambleChars = null)
		{
			if (endValue == null)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogWarning("You can't pass a NULL string to DOText: an empty string will be used instead to avoid errors", null);
				}
				endValue = "";
			}
			TweenerCore<string, string, StringOptions> tweenerCore = DOTween.To(() => target.text, delegate(string x)
			{
				target.text = x;
			}, endValue, duration);
			tweenerCore.SetOptions(richTextEnabled, scrambleMode, scrambleChars).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004090 File Offset: 0x00002290
		public static Tweener DOBlendableColor(this Graphic target, Color endValue, float duration)
		{
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate(Color x)
			{
				Color diff = x - to;
				to = x;
				target.color += diff;
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget(target);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000410C File Offset: 0x0000230C
		public static Tweener DOBlendableColor(this Image target, Color endValue, float duration)
		{
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate(Color x)
			{
				Color diff = x - to;
				to = x;
				target.color += diff;
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget(target);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004188 File Offset: 0x00002388
		public static Tweener DOBlendableColor(this Text target, Color endValue, float duration)
		{
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate(Color x)
			{
				Color diff = x - to;
				to = x;
				target.color += diff;
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget(target);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004204 File Offset: 0x00002404
		public static TweenerCore<Vector2, Vector2, CircleOptions> DOShapeCircle(this RectTransform target, Vector2 center, float endValueDegrees, float duration, bool relativeCenter = false, bool snapping = false)
		{
			TweenerCore<Vector2, Vector2, CircleOptions> tweenerCore = DOTween.To<Vector2, Vector2, CircleOptions>(CirclePlugin.Get(), () => target.anchoredPosition, delegate(Vector2 x)
			{
				target.anchoredPosition = x;
			}, center, duration);
			tweenerCore.SetOptions(endValueDegrees, relativeCenter, snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x02000023 RID: 35
		public static class Utils
		{
			// Token: 0x0600009D RID: 157 RVA: 0x0000425C File Offset: 0x0000245C
			public static Vector2 SwitchToRectTransform(RectTransform from, RectTransform to)
			{
				Vector2 fromPivotDerivedOffset = new Vector2(from.rect.width * 0.5f + from.rect.xMin, from.rect.height * 0.5f + from.rect.yMin);
				Vector2 screenP = RectTransformUtility.WorldToScreenPoint(null, from.position);
				screenP += fromPivotDerivedOffset;
				Vector2 localPoint;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(to, screenP, null, out localPoint);
				Vector2 pivotDerivedOffset = new Vector2(to.rect.width * 0.5f + to.rect.xMin, to.rect.height * 0.5f + to.rect.yMin);
				return to.anchoredPosition + localPoint - pivotDerivedOffset;
			}
		}
	}
}
