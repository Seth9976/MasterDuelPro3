using System;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x02001365 RID: 4965
	public class AutoScale : MonoBehaviour
	{
		// Token: 0x06008FF7 RID: 36855 RVA: 0x00139A00 File Offset: 0x00137C00
		private void Awake()
		{
			this.width = base.transform.localScale.x;
			this.height = base.transform.localScale.y;
			this.Scale();
			SystemEvent.OnResolutionChange += this.Scale;
		}

		// Token: 0x06008FF8 RID: 36856 RVA: 0x00139A50 File Offset: 0x00137C50
		private void Scale()
		{
			if (Screen.height == 0)
			{
				return;
			}
			float screenAspect = (float)Screen.width / (float)Screen.height;
			if (screenAspect > 1.7777778f)
			{
				base.transform.localScale = new Vector3(this.width * screenAspect * 9f / 16f, this.height * screenAspect * 9f / 16f, base.transform.localScale.z);
				return;
			}
			base.transform.localScale = new Vector3(this.width, this.height, base.transform.localScale.z);
		}

		// Token: 0x06008FF9 RID: 36857 RVA: 0x00139AF0 File Offset: 0x00137CF0
		private void OnDestroy()
		{
			SystemEvent.OnResolutionChange -= this.Scale;
		}

		// Token: 0x0400CE8D RID: 52877
		private float width;

		// Token: 0x0400CE8E RID: 52878
		private float height;
	}
}
