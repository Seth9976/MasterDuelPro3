using System;

namespace AssetsTools.NET.Extra
{
	// Token: 0x02000082 RID: 130
	public abstract class SerializingAssetReplacer : AssetsReplacer
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x00019B1A File Offset: 0x00017D1A
		protected SerializingAssetReplacer(AssetsManager manager, AssetsFileInstance assetsFile, AssetFileInfo asset)
		{
			this.manager = manager;
			this.assetsFile = assetsFile;
			this.asset = asset;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00019B3C File Offset: 0x00017D3C
		public override AssetsReplacementType GetReplacementType()
		{
			return AssetsReplacementType.AddOrModify;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00019B50 File Offset: 0x00017D50
		public override long GetPathID()
		{
			return this.asset.PathId;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00019B70 File Offset: 0x00017D70
		public override int GetClassID()
		{
			return this.asset.TypeId;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00019B90 File Offset: 0x00017D90
		public override ushort GetMonoScriptID()
		{
			return this.assetsFile.file.GetScriptIndex(this.asset);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00019BB8 File Offset: 0x00017DB8
		public override long Write(AssetsFileWriter writer)
		{
			AssetTypeValueField baseField = this.manager.GetBaseField(this.assetsFile, this.asset, AssetReadFlags.None);
			this.Modify(baseField);
			baseField.Write(writer);
			return writer.Position;
		}

		// Token: 0x060004A7 RID: 1191
		protected abstract void Modify(AssetTypeValueField baseField);

		// Token: 0x060004A8 RID: 1192 RVA: 0x000118AD File Offset: 0x0000FAAD
		public override long WriteReplacer(AssetsFileWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000418 RID: 1048
		private readonly AssetsManager manager;

		// Token: 0x04000419 RID: 1049
		private readonly AssetsFileInstance assetsFile;

		// Token: 0x0400041A RID: 1050
		private readonly AssetFileInfo asset;
	}
}
