using System;

namespace AssetsTools.NET.Extra
{
	// Token: 0x02000081 RID: 129
	public class LambdaAssetReplacer : SerializingAssetReplacer
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x00019AF5 File Offset: 0x00017CF5
		public LambdaAssetReplacer(AssetsManager manager, AssetsFileInstance assetsFile, AssetFileInfo asset, Action<AssetTypeValueField> modify)
			: base(manager, assetsFile, asset)
		{
			this.modify = modify;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00019B0A File Offset: 0x00017D0A
		protected override void Modify(AssetTypeValueField baseField)
		{
			this.modify(baseField);
		}

		// Token: 0x04000417 RID: 1047
		private readonly Action<AssetTypeValueField> modify;
	}
}
