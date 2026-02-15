using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B45 RID: 2885
	[Serializable]
	public class MateCaptureContext
	{
		// Token: 0x060053B8 RID: 21432 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Equals(object obj)
		{
			return false;
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x04009137 RID: 37175
		public int mateId;

		// Token: 0x04009138 RID: 37176
		public Vector3 position;

		// Token: 0x04009139 RID: 37177
		public Quaternion rotate;

		// Token: 0x0400913A RID: 37178
		public Vector3 scale;

		// Token: 0x0400913B RID: 37179
		public string settingPath;

		// Token: 0x0400913C RID: 37180
		public GameObject locator;

		// Token: 0x0400913D RID: 37181
		public int width;

		// Token: 0x0400913E RID: 37182
		public int height;
	}
}
