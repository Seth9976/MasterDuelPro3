using System;

namespace UnityEngine.AddressableAssets
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	public class AssetReferenceSprite : AssetReferenceT<Sprite>
	{
		// Token: 0x06000169 RID: 361 RVA: 0x000060F3 File Offset: 0x000042F3
		public AssetReferenceSprite(string guid)
			: base(guid)
		{
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000060CC File Offset: 0x000042CC
		public override bool ValidateAsset(string path)
		{
			return false;
		}
	}
}
