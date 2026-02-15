using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000087 RID: 135
	public sealed class CinemachineEmbeddedAssetPropertyAttribute : PropertyAttribute
	{
		// Token: 0x06000324 RID: 804 RVA: 0x00013887 File Offset: 0x00011A87
		public CinemachineEmbeddedAssetPropertyAttribute(bool warnIfNull = false)
		{
			this.WarnIfNull = warnIfNull;
		}

		// Token: 0x040002DB RID: 731
		public bool WarnIfNull;
	}
}
