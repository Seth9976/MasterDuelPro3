using System;
using System.ComponentModel;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.U2D;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x0200004C RID: 76
	[DisplayName("Sprites from Atlases Provider")]
	public class AtlasSpriteProvider : ResourceProviderBase
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x000081A0 File Offset: 0x000063A0
		public override void Provide(ProvideHandle providerInterface)
		{
			SpriteAtlas atlas = providerInterface.GetDependency<SpriteAtlas>(0);
			if (atlas == null)
			{
				providerInterface.Complete<Sprite>(null, false, new Exception("Sprite atlas failed to load for location " + providerInterface.Location.PrimaryKey + "."));
				return;
			}
			string mainKey;
			string subKey;
			ResourceManagerConfig.ExtractKeyAndSubKey(providerInterface.ResourceManager.TransformInternalId(providerInterface.Location), out mainKey, out subKey);
			string spriteKey = (string.IsNullOrEmpty(subKey) ? mainKey : subKey);
			Sprite sprite = atlas.GetSprite(spriteKey);
			providerInterface.Complete<Sprite>(sprite, sprite != null, (sprite != null) ? null : new Exception("Sprite failed to load for location " + providerInterface.Location.PrimaryKey + "."));
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000825C File Offset: 0x0000645C
		public override void Release(IResourceLocation location, object obj)
		{
			Sprite sprite = obj as Sprite;
			if (sprite != null)
			{
				Object.Destroy(sprite);
			}
		}
	}
}
