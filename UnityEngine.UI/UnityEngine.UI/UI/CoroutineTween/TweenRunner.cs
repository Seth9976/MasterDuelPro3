using System;
using System.Collections;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000090 RID: 144
	internal class TweenRunner<T> where T : struct, ITweenValue
	{
		// Token: 0x0600057D RID: 1405 RVA: 0x000179E1 File Offset: 0x00015BE1
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

		// Token: 0x0600057E RID: 1406 RVA: 0x000179F0 File Offset: 0x00015BF0
		public void Init(MonoBehaviour coroutineContainer)
		{
			this.m_CoroutineContainer = coroutineContainer;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000179FC File Offset: 0x00015BFC
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

		// Token: 0x06000580 RID: 1408 RVA: 0x00017A6B File Offset: 0x00015C6B
		public void StopTween()
		{
			if (this.m_Tween != null)
			{
				this.m_CoroutineContainer.StopCoroutine(this.m_Tween);
				this.m_Tween = null;
			}
		}

		// Token: 0x04000285 RID: 645
		protected MonoBehaviour m_CoroutineContainer;

		// Token: 0x04000286 RID: 646
		protected IEnumerator m_Tween;
	}
}
