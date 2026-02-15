using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.Utility
{
	// Token: 0x02000539 RID: 1337
	public class RenderTextureBuilder : MonoBehaviour
	{
		// Token: 0x06002AC0 RID: 10944 RVA: 0x0000216D File Offset: 0x0000036D
		public void Restart()
		{
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPostRender()
		{
		}

		// Token: 0x040029D2 RID: 10706
		public int textureW;

		// Token: 0x040029D3 RID: 10707
		public int textureH;

		// Token: 0x040029D4 RID: 10708
		public int depth;

		// Token: 0x040029D5 RID: 10709
		public RenderTexture texture;

		// Token: 0x040029D6 RID: 10710
		public RawImage targetRawImage;

		// Token: 0x040029D7 RID: 10711
		public int snapshotDelay;

		// Token: 0x040029D8 RID: 10712
		public bool snapshotAndDestroy;

		// Token: 0x040029D9 RID: 10713
		public Action<RenderTextureBuilder> snapshotAction;

		// Token: 0x040029DA RID: 10714
		public bool useHDRFormat;

		// Token: 0x040029DB RID: 10715
		public int antiAliasing;

		// Token: 0x040029DC RID: 10716
		private Camera renderCamera;
	}
}
