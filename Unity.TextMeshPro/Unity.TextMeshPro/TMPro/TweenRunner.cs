using System;
using System.Collections;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000025 RID: 37
	internal class TweenRunner<T> where T : struct, ITweenValue
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x0000303D File Offset: 0x0000123D
		private static IEnumerator Start(T tweenInfo)
		{
			if (!tweenInfo.ValidTarget())
			{
				yield break;
			}
			float elapsedTime = 0f;
			while (elapsedTime < tweenInfo.duration)
			{
				elapsedTime += (tweenInfo.ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime);
				float percentage = Mathf.Clamp01(elapsedTime / tweenInfo.duration);
				tweenInfo.TweenValue(percentage);
				yield return null;
			}
			tweenInfo.TweenValue(1f);
			yield break;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000304C File Offset: 0x0000124C
		public void Init(MonoBehaviour coroutineContainer)
		{
			this.m_CoroutineContainer = coroutineContainer;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003058 File Offset: 0x00001258
		public void StartTween(T info)
		{
			if (this.m_CoroutineContainer == null)
			{
				Debug.LogWarning("Coroutine container not configured... did you forget to call Init?");
				return;
			}
			this.StopTween();
			if (!this.m_CoroutineContainer.gameObject.activeInHierarchy)
			{
				info.TweenValue(1f);
				return;
			}
			this.m_Tween = TweenRunner<T>.Start(info);
			this.m_CoroutineContainer.StartCoroutine(this.m_Tween);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000030C7 File Offset: 0x000012C7
		public void StopTween()
		{
			if (this.m_Tween != null)
			{
				this.m_CoroutineContainer.StopCoroutine(this.m_Tween);
				this.m_Tween = null;
			}
		}

		// Token: 0x04000095 RID: 149
		protected MonoBehaviour m_CoroutineContainer;

		// Token: 0x04000096 RID: 150
		protected IEnumerator m_Tween;
	}
}
