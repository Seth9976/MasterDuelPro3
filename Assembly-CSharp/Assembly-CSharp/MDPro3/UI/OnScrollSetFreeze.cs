using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001406 RID: 5126
	public class OnScrollSetFreeze : MonoBehaviour
	{
		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x0600945F RID: 37983 RVA: 0x001528C4 File Offset: 0x00150AC4
		private ScrollRect ScrollRect
		{
			get
			{
				return this.m_ScrollRect = ((this.m_ScrollRect != null) ? this.m_ScrollRect : base.GetComponent<ScrollRect>());
			}
		}

		// Token: 0x06009460 RID: 37984 RVA: 0x001528F6 File Offset: 0x00150AF6
		private void Awake()
		{
			this.ScrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.SetFreeze));
		}

		// Token: 0x06009461 RID: 37985 RVA: 0x00152914 File Offset: 0x00150B14
		public void SetFreeze(Vector2 position)
		{
			float y = Mathf.Abs(this.ScrollRect.velocity.y);
			if (y > 0f && y < this.minVelocity)
			{
				return;
			}
			if (this.coroutine != null)
			{
				base.StopCoroutine(this.coroutine);
			}
			this.coroutine = base.StartCoroutine(this.SetFreezeAsync());
		}

		// Token: 0x06009462 RID: 37986 RVA: 0x0015296F File Offset: 0x00150B6F
		private IEnumerator SetFreezeAsync()
		{
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				yield break;
			}
			OnScrollSetFreeze.Freeze = true;
			float timeElapsed = 0f;
			while (timeElapsed < 0.15f)
			{
				yield return null;
				timeElapsed += Time.unscaledDeltaTime;
				float y = Mathf.Abs(this.ScrollRect.velocity.y);
				if (y > 0f && y < this.minVelocity)
				{
					break;
				}
			}
			OnScrollSetFreeze.Freeze = false;
			yield break;
		}

		// Token: 0x0400D2A2 RID: 53922
		public static bool Freeze;

		// Token: 0x0400D2A3 RID: 53923
		private Coroutine coroutine;

		// Token: 0x0400D2A4 RID: 53924
		private ScrollRect m_ScrollRect;

		// Token: 0x0400D2A5 RID: 53925
		private float minVelocity = 200f;
	}
}
