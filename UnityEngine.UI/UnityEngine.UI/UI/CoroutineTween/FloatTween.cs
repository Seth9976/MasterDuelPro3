using System;
using UnityEngine.Events;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x0200008E RID: 142
	internal struct FloatTween : ITweenValue
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00017939 File Offset: 0x00015B39
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x00017941 File Offset: 0x00015B41
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

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0001794A File Offset: 0x00015B4A
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00017952 File Offset: 0x00015B52
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

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0001795B File Offset: 0x00015B5B
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x00017963 File Offset: 0x00015B63
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

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001796C File Offset: 0x00015B6C
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00017974 File Offset: 0x00015B74
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

		// Token: 0x06000577 RID: 1399 RVA: 0x00017980 File Offset: 0x00015B80
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			float newValue = Mathf.Lerp(this.m_StartValue, this.m_TargetValue, floatPercentage);
			this.m_Target.Invoke(newValue);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000179B5 File Offset: 0x00015BB5
		public void AddOnChangedCallback(UnityAction<float> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new FloatTween.FloatTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001796C File Offset: 0x00015B6C
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001795B File Offset: 0x00015B5B
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000179D6 File Offset: 0x00015BD6
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x04000280 RID: 640
		private FloatTween.FloatTweenCallback m_Target;

		// Token: 0x04000281 RID: 641
		private float m_StartValue;

		// Token: 0x04000282 RID: 642
		private float m_TargetValue;

		// Token: 0x04000283 RID: 643
		private float m_Duration;

		// Token: 0x04000284 RID: 644
		private bool m_IgnoreTimeScale;

		// Token: 0x0200008F RID: 143
		public class FloatTweenCallback : UnityEvent<float>
		{
		}
	}
}
