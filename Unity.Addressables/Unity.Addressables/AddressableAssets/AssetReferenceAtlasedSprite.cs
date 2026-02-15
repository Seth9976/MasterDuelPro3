using System;
using UnityEngine.U2D;

namespace UnityEngine.AddressableAssets
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	public class AssetReferenceAtlasedSprite : AssetReferenceT<Sprite>
	{
		// Token: 0x0600016B RID: 363 RVA: 0x000060F3 File Offset: 0x000042F3
		public AssetReferenceAtlasedSprite(string guid)
			: base(guid)
		{
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000060FC File Offset: 0x000042FC
		public override bool ValidateAsset(Object obj)
		{
			return obj is SpriteAtlas;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000060CC File Offset: 0x000042CC
		public override bool ValidateAsset(string path)
		{
			return false;
		}
	}
}
