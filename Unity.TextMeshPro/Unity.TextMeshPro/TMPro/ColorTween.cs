using System;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro
{
	// Token: 0x02000020 RID: 32
	internal struct ColorTween : ITweenValue
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002E73 File Offset: 0x00001073
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002E7B File Offset: 0x0000107B
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002E84 File Offset: 0x00001084
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002E8C File Offset: 0x0000108C
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002E95 File Offset: 0x00001095
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002E9D File Offset: 0x0000109D
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002EA6 File Offset: 0x000010A6
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00002EAE File Offset: 0x000010AE
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002EB7 File Offset: 0x000010B7
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002EBF File Offset: 0x000010BF
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

		// Token: 0x06000092 RID: 146 RVA: 0x00002EC8 File Offset: 0x000010C8
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

		// Token: 0x06000093 RID: 147 RVA: 0x00002F59 File Offset: 0x00001159
		public void AddOnChangedCallback(UnityAction<Color> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new ColorTween.ColorTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002EB7 File Offset: 0x000010B7
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002EA6 File Offset: 0x000010A6
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002F7A File Offset: 0x0000117A
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x04000086 RID: 134
		private ColorTween.ColorTweenCallback m_Target;

		// Token: 0x04000087 RID: 135
		private Color m_StartColor;

		// Token: 0x04000088 RID: 136
		private Color m_TargetColor;

		// Token: 0x04000089 RID: 137
		private ColorTween.ColorTweenMode m_TweenMode;

		// Token: 0x0400008A RID: 138
		private float m_Duration;

		// Token: 0x0400008B RID: 139
		private bool m_IgnoreTimeScale;

		// Token: 0x02000021 RID: 33
		public enum ColorTweenMode
		{
			// Token: 0x0400008D RID: 141
			All,
			// Token: 0x0400008E RID: 142
			RGB,
			// Token: 0x0400008F RID: 143
			Alpha
		}

		// Token: 0x02000022 RID: 34
		public class ColorTweenCallback : UnityEvent<Color>
		{
		}
	}
}
