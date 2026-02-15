using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem
{
	// Token: 0x020004C6 RID: 1222
	public class ScrollViewAutoScroll : MonoBehaviour
	{
		// Token: 0x0600273F RID: 10047 RVA: 0x000F175E File Offset: 0x000EF95E
		private void Awake()
		{
			this.m_ScrollRect = base.GetComponent<ScrollRect>();
			this.m_ContentRT = this.m_ScrollRect.content;
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000F177D File Offset: 0x000EF97D
		private void OnEnable()
		{
			this.ResetScroll();
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000F1785 File Offset: 0x000EF985
		private void OnDisable()
		{
			if (this.m_UpdateScrollCoroutine != null)
			{
				base.StopCoroutine(this.m_UpdateScrollCoroutine);
				this.m_UpdateScrollCoroutine = null;
			}
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000F17A4 File Offset: 0x000EF9A4
		public void ResetScroll()
		{
			if (this.m_UpdateScrollCoroutine != null)
			{
				base.StopCoroutine(this.m_UpdateScrollCoroutine);
			}
			this.lastRect = this.m_ContentRT.rect;
			this.m_ScrollRect.normalizedPosition = new Vector2(0f, 1f);
			this.m_UpdateScrollCoroutine = base.StartCoroutine(this.ScrollRoutine());
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000F1802 File Offset: 0x000EFA02
		private void Update()
		{
			if (this.m_ContentRT.rect != this.lastRect)
			{
				this.ResetScroll();
			}
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x0000216D File Offset: 0x0000036D
		private void BeginIdleProcess()
		{
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x0000216D File Offset: 0x0000036D
		private void ScrollProcess()
		{
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x0000216D File Offset: 0x0000036D
		private void EndIdleProcess()
		{
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000F1822 File Offset: 0x000EFA22
		private IEnumerator ScrollRoutine()
		{
			yield return new WaitForSeconds(this.m_WaitTimeBegin);
			float startTime = Time.time;
			float duration = (this.m_ScrollRect.content.rect.width - this.m_ScrollRect.viewport.rect.width) / (this.m_ScrollSpeed * 50f);
			duration = Mathf.Max(duration, (this.m_ScrollRect.content.rect.height - this.m_ScrollRect.viewport.rect.height) / (this.m_ScrollSpeed * 50f));
			while (Time.time - startTime < duration)
			{
				float progress = (Time.time - startTime) / duration;
				this.m_ScrollRect.normalizedPosition = Vector2.Lerp(new Vector2(0f, 1f), new Vector2(1f, 0f), progress);
				yield return null;
			}
			yield return new WaitForSeconds(this.m_WaitTimeEnd);
			this.ResetScroll();
			yield break;
		}

		// Token: 0x04002820 RID: 10272
		[SerializeField]
		private float m_ScrollSpeed;

		// Token: 0x04002821 RID: 10273
		[SerializeField]
		private float m_WaitTimeBegin;

		// Token: 0x04002822 RID: 10274
		[SerializeField]
		private float m_WaitTimeEnd;

		// Token: 0x04002823 RID: 10275
		private ScrollViewAutoScroll.ScrollViewDirection m_Direction;

		// Token: 0x04002824 RID: 10276
		private Coroutine m_UpdateScrollCoroutine;

		// Token: 0x04002825 RID: 10277
		private ScrollRect m_ScrollRect;

		// Token: 0x04002826 RID: 10278
		private RectTransform m_ContentRT;

		// Token: 0x04002827 RID: 10279
		private float m_DeltaTime;

		// Token: 0x04002828 RID: 10280
		private ScrollViewAutoScroll.Step m_Step;

		// Token: 0x04002829 RID: 10281
		private Rect lastRect;

		// Token: 0x020004C7 RID: 1223
		private enum ScrollViewDirection
		{
			// Token: 0x0400282B RID: 10283
			Horizontal,
			// Token: 0x0400282C RID: 10284
			Vertical
		}

		// Token: 0x020004C8 RID: 1224
		private enum Step
		{
			// Token: 0x0400282E RID: 10286
			BeginIdleStep,
			// Token: 0x0400282F RID: 10287
			ScrollStep,
			// Token: 0x04002830 RID: 10288
			EndIdleStep
		}
	}
}
