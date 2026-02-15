using System;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x02001366 RID: 4966
	public class AutoScaleOnce : MonoBehaviour
	{
		// Token: 0x06008FFB RID: 36859 RVA: 0x00139B03 File Offset: 0x00137D03
		private void Start()
		{
			this.width = base.transform.localScale.x;
			this.height = base.transform.localScale.y;
			this.Scale();
		}

		// Token: 0x06008FFC RID: 36860 RVA: 0x00139B38 File Offset: 0x00137D38
		private void Scale()
		{
			float screenAspect = (float)Screen.width / (float)Screen.height;
			if (screenAspect > 1.7777778f)
			{
				base.transform.localScale = new Vector3(this.width * screenAspect * 9f / 16f, this.height * screenAspect * 9f / 16f, base.transform.localScale.z);
				return;
			}
			base.transform.localScale = new Vector3(this.width, this.height, base.transform.localScale.z);
		}

		// Token: 0x0400CE8F RID: 52879
		private float width;

		// Token: 0x0400CE90 RID: 52880
		private float height;
	}
}
