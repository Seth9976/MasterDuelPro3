using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000074 RID: 116
	public class SpineAtlasRegion : PropertyAttribute
	{
		// Token: 0x06000343 RID: 835 RVA: 0x00013001 File Offset: 0x00011201
		public SpineAtlasRegion(string atlasAssetField = "")
		{
			this.atlasAssetField = atlasAssetField;
		}

		// Token: 0x04000225 RID: 549
		public string atlasAssetField;
	}
}
