using System;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x02001408 RID: 5128
	public class SafeAreaAdapter : MonoBehaviour
	{
		// Token: 0x0600946A RID: 37994 RVA: 0x00152A54 File Offset: 0x00150C54
		private void Awake()
		{
			this._rectTransform = base.GetComponent<RectTransform>();
			SystemEvent.OnSafeAreaUpdate += this.ApplySafeArea;
			this.ApplySafeArea();
		}

		// Token: 0x0600946B RID: 37995 RVA: 0x00152A79 File Offset: 0x00150C79
		private void OnDestroy()
		{
			SystemEvent.OnSafeAreaUpdate -= this.ApplySafeArea;
		}

		// Token: 0x0600946C RID: 37996 RVA: 0x00152A8C File Offset: 0x00150C8C
		private void ApplySafeArea()
		{
			if (this._rectTransform == null)
			{
				Debug.LogError("RectTransform is null");
				return;
			}
			Rect safeArea = Screen.safeArea;
			if (Screen.height == 0 || safeArea.width == 0f || safeArea.height == 0f)
			{
				return;
			}
			int width = Screen.width * 1080 / Screen.height;
			Vector2 offsetMin = new Vector2(safeArea.position.x * (float)width / safeArea.width, safeArea.position.y * 1080f / safeArea.height);
			Vector2 offsetMax = new Vector2((safeArea.position.x + safeArea.width - (float)Screen.width) * (float)width / safeArea.width, (safeArea.position.y + safeArea.height - (float)Screen.height) * 1080f / safeArea.height);
			this._rectTransform.offsetMin = offsetMin;
			this._rectTransform.offsetMax = offsetMax;
		}

		// Token: 0x0600946D RID: 37997 RVA: 0x00152B94 File Offset: 0x00150D94
		public static float GetSafeAreaRightOffset()
		{
			return ((float)Screen.width - (Screen.safeArea.x + Screen.safeArea.width)) * (float)Screen.height / 1080f;
		}

		// Token: 0x0600946E RID: 37998 RVA: 0x00152BD0 File Offset: 0x00150DD0
		public static float GetSafeAreaLeftOffset()
		{
			return Screen.safeArea.x * (float)Screen.height / 1080f;
		}

		// Token: 0x0400D2AA RID: 53930
		private RectTransform _rectTransform;
	}
}
