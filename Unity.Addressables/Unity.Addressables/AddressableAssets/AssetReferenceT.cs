using System;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UnityEngine.AddressableAssets
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	public class AssetReferenceT<TObject> : AssetReference where TObject : Object
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00006094 File Offset: 0x00004294
		public AssetReferenceT(string guid)
			: base(guid)
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000609D File Offset: 0x0000429D
		public virtual AsyncOperationHandle<TObject> LoadAssetAsync()
		{
			return this.LoadAssetAsync<TObject>();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000060A8 File Offset: 0x000042A8
		public override bool ValidateAsset(Object obj)
		{
			Type type = obj.GetType();
			return typeof(TObject).IsAssignableFrom(type);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000060CC File Offset: 0x000042CC
		public override bool ValidateAsset(string mainAssetPath)
		{
			return false;
		}
	}
}
