using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x0200053E RID: 1342
	public class RuntimeRimParamController : MonoBehaviour
	{
		// Token: 0x06002ADD RID: 10973 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeMaterials()
		{
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateMaterialParams()
		{
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000F1C92 File Offset: 0x000EFE92
		private bool GetMaterialParams(out Vector4 specParams, out float intensity, out Color color, out Texture cubeTex)
		{
			specParams = default(Vector4);
			intensity = 0f;
			color = default(Color);
			cubeTex = null;
			return false;
		}

		// Token: 0x040029EF RID: 10735
		private int shaderID_SpecParams;

		// Token: 0x040029F0 RID: 10736
		private int shaderID_BackLightDirIntensity;

		// Token: 0x040029F1 RID: 10737
		private int shaderID_BackLightColor;

		// Token: 0x040029F2 RID: 10738
		private int shaderID_CubeTex;

		// Token: 0x040029F3 RID: 10739
		private bool initialized;

		// Token: 0x040029F4 RID: 10740
		private bool initializedMaterials;

		// Token: 0x040029F5 RID: 10741
		private HashSet<Material> mtrls;

		// Token: 0x040029F6 RID: 10742
		private Vector4 prevSrcSpecParams;

		// Token: 0x040029F7 RID: 10743
		private Vector3 prevF;

		// Token: 0x040029F8 RID: 10744
		private float prevIntensity;

		// Token: 0x040029F9 RID: 10745
		private Color prevColor;

		// Token: 0x040029FA RID: 10746
		private Texture prevCubeTex;

		// Token: 0x040029FB RID: 10747
		private static readonly Vector3 forward;
	}
}
