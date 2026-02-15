using System;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x020004FF RID: 1279
	public class AssetPathAttribute : PropertyAttribute
	{
		// Token: 0x06002833 RID: 10291 RVA: 0x000F1A32 File Offset: 0x000EFC32
		public AssetPathAttribute()
		{
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x000F1A32 File Offset: 0x000EFC32
		public AssetPathAttribute(Type assetType)
		{
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x000F1A32 File Offset: 0x000EFC32
		public AssetPathAttribute(Type assetType, AssetPathAttribute.PathType pathType)
		{
		}

		// Token: 0x040028F6 RID: 10486
		public readonly Type assetType;

		// Token: 0x040028F7 RID: 10487
		public readonly AssetPathAttribute.PathType pathType;

		// Token: 0x02000500 RID: 1280
		public enum PathType
		{
			// Token: 0x040028F9 RID: 10489
			ResourceManager,
			// Token: 0x040028FA RID: 10490
			AssetDatabase
		}
	}
}
