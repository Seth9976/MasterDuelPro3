using System;
using UnityEngine.Events;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x0200008B RID: 139
	internal struct ColorTween : ITweenValue
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0001781F File Offset: 0x00015A1F
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x00017827 File Offset: 0x00015A27
		public Color startColor
		{
			get
			{
				return this.m_StartColor;
			}
			set
			{
				this.m_StartColor = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00017830 File Offset: 0x00015A30
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00017838 File Offset: 0x00015A38
		public Color targetColor
		{
			get
			{
				return this.m_TargetColor;
			}
			set
			{
				this.m_TargetColor = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00017841 File Offset: 0x00015A41
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00017849 File Offset: 0x00015A49
		public ColorTween.ColorTweenMode tweenMode
		{
			get
			{
				return this.m_TweenMode;
			}
			set
			{
				this.m_TweenMode = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00017852 File Offset: 0x00015A52
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0001785A File Offset: 0x00015A5A
		public float duration
		{
			get
			{
				return this.m_Duration;
			}
			set
			{
				this.m_Duration = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00017863 File Offset: 0x00015A63
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0001786B File Offset: 0x00015A6B
		public bool ignoreTimeScale
		{
			get
			{
				return this.m_IgnoreTimeScale;
			}
			set
			{
				this.m_IgnoreTimeScale = value;
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00017874 File Offset: 0x00015A74
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			Color newColor = Color.Lerp(this.m_StartColor, this.m_TargetColor, floatPercentage);
			if (this.m_TweenMode == ColorTween.ColorTweenMode.Alpha)
			{
				newColor.r = this.m_StartColor.r;
				newColor.g = this.m_StartColor.g;
				newColor.b = this.m_StartColor.b;
			}
			else if (this.m_TweenMode == ColorTween.ColorTweenMode.RGB)
			{
				newColor.a = this.m_StartColor.a;
			}
			this.m_Target.Invoke(newColor);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00017905 File Offset: 0x00015B05
		public void AddOnChangedCallback(UnityAction<Color> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new ColorTween.ColorTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00017863 File Offset: 0x00015A63
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00017852 File Offset: 0x00015A52
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00017926 File Offset: 0x00015B26
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x04000276 RID: 630
		private ColorTween.ColorTweenCallback m_Target;

		// Token: 0x04000277 RID: 631
		private Color m_StartColor;

		// Token: 0x04000278 RID: 632
		private Color m_TargetColor;

		// Token: 0x04000279 RID: 633
		private ColorTween.ColorTweenMode m_TweenMode;

		// Token: 0x0400027A RID: 634
		private float m_Duration;

		// Token: 0x0400027B RID: 635
		private bool m_IgnoreTimeScale;

		// Token: 0x0200008C RID: 140
		public enum ColorTweenMode
		{
			// Token: 0x0400027D RID: 637
			All,
			// Token: 0x0400027E RID: 638
			RGB,
			// Token: 0x0400027F RID: 639
			Alpha
		}

		// Token: 0x0200008D RID: 141
		public class ColorTweenCallback : UnityEvent<Color>
		{
		}
	}
}
