using System;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x0200001E RID: 30
	public static class DOTweenModuleSprite
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000030F0 File Offset: 0x000012F0
		public static TweenerCore<Color, Color, ColorOptions> DOColor(this SpriteRenderer target, Color endValue, float duration)
		{
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003138 File Offset: 0x00001338
		public static TweenerCore<Color, Color, ColorOptions> DOFade(this SpriteRenderer target, float endValue, float duration)
		{
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003180 File Offset: 0x00001380
		public static Sequence DOGradientColor(this SpriteRenderer target, Gradient gradient, float duration)
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

		// Token: 0x06000069 RID: 105 RVA: 0x00003238 File Offset: 0x00001438
		public static Tweener DOBlendableColor(this SpriteRenderer target, Color endValue, float duration)
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
	}
}
