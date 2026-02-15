using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200063B RID: 1595
	public class TweenShaderFloat : Tween
	{
		// Token: 0x06003200 RID: 12800 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeMaterials(Dictionary<int, Material> initMaterialList = null)
		{
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<int, Material> GetMaterials()
		{
			return null;
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E98 RID: 11928
		[SerializeField]
		public float from;

		// Token: 0x04002E99 RID: 11929
		[SerializeField]
		public float to;

		// Token: 0x04002E9A RID: 11930
		[SerializeField]
		public string paramName;

		// Token: 0x04002E9B RID: 11931
		[SerializeField]
		public Material[] materials;

		// Token: 0x04002E9C RID: 11932
		private Dictionary<int, Material> materialList;

		// Token: 0x04002E9D RID: 11933
		public bool isRecusive;

		// Token: 0x04002E9E RID: 11934
		public bool isShared;
	}
}
