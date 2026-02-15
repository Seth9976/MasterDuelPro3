using System;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro
{
	// Token: 0x02000023 RID: 35
	internal struct FloatTween : ITweenValue
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002F8D File Offset: 0x0000118D
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00002F95 File Offset: 0x00001195
		public float startValue
		{
			get
			{
				return this.m_StartValue;
			}
			set
			{
				this.m_StartValue = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002F9E File Offset: 0x0000119E
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00002FA6 File Offset: 0x000011A6
		public float targetValue
		{
			get
			{
				return this.m_TargetValue;
			}
			set
			{
				this.m_TargetValue = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002FAF File Offset: 0x000011AF
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00002FB7 File Offset: 0x000011B7
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002FC0 File Offset: 0x000011C0
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00002FC8 File Offset: 0x000011C8
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

		// Token: 0x060000A0 RID: 160 RVA: 0x00002FD4 File Offset: 0x000011D4
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			float newValue = Mathf.Lerp(this.m_StartValue, this.m_TargetValue, floatPercentage);
			this.m_Target.Invoke(newValue);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003009 File Offset: 0x00001209
		public void AddOnChangedCallback(UnityAction<float> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new FloatTween.FloatTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002FC0 File Offset: 0x000011C0
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002FAF File Offset: 0x000011AF
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000302A File Offset: 0x0000122A
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x04000090 RID: 144
		private FloatTween.FloatTweenCallback m_Target;

		// Token: 0x04000091 RID: 145
		private float m_StartValue;

		// Token: 0x04000092 RID: 146
		private float m_TargetValue;

		// Token: 0x04000093 RID: 147
		private float m_Duration;

		// Token: 0x04000094 RID: 148
		private bool m_IgnoreTimeScale;

		// Token: 0x02000024 RID: 36
		public class FloatTweenCallback : UnityEvent<float>
		{
		}
	}
}
