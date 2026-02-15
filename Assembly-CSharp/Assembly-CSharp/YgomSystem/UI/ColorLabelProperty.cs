using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000587 RID: 1415
	[Serializable]
	public class ColorLabelProperty
	{
		// Token: 0x06002CC8 RID: 11464 RVA: 0x000F1F28 File Offset: 0x000F0128
		public Color GetColor()
		{
			return default(Color);
		}

		// Token: 0x04002B0E RID: 11022
		[ColorLabelString]
		public string label;

		// Token: 0x04002B0F RID: 11023
		public Color rgba;
	}
}
