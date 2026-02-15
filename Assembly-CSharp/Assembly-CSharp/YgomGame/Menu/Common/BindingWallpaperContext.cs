using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B23 RID: 2851
	[Serializable]
	public class BindingWallpaperContext
	{
		// Token: 0x0600531B RID: 21275 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Equals(object obj)
		{
			return false;
		}

		// Token: 0x0600531C RID: 21276 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0400910A RID: 37130
		[SerializeField]
		public int id;

		// Token: 0x0400910B RID: 37131
		[SerializeField]
		public int width;

		// Token: 0x0400910C RID: 37132
		[SerializeField]
		public int height;

		// Token: 0x0400910D RID: 37133
		[SerializeField]
		public BindingGameObjectEx.FitMode fitMode;

		// Token: 0x0400910E RID: 37134
		[SerializeField]
		public bool useImage;
	}
}
