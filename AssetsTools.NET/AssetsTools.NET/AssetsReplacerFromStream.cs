using System;
using System.Collections.Generic;
using System.IO;
using AssetsTools.NET.Extra;

namespace AssetsTools.NET
{
	// Token: 0x0200004E RID: 78
	public class AssetsReplacerFromStream : AssetsReplacer
	{
		// Token: 0x06000275 RID: 629 RVA: 0x00011CF0 File Offset: 0x0000FEF0
		public AssetsReplacerFromStream(long pathId, int classId, ushort monoScriptIndex, Stream stream, long offset, long size)
		{
			this.pathId = pathId;
			this.classId = classId;
			this.monoScriptIndex = monoScriptIndex;
			this.stream = stream;
			this.offset = offset;
			this.size = size;
			this.preloadList = new List<AssetPPtr>();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00011D40 File Offset: 0x0000FF40
		public AssetsReplacerFromStream(AssetsFile assetsFile, AssetFileInfo info, Stream stream, long offset, long size)
		{
			this.pathId = info.PathId;
			this.classId = info.TypeId;
			this.monoScriptIndex = assetsFile.GetScriptIndex(info);
			this.stream = stream;
			this.offset = offset;
			this.size = size;
			this.preloadList = new List<AssetPPtr>();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00011D9C File Offset: 0x0000FF9C
		public override AssetsReplacementType GetReplacementType()
		{
			return AssetsReplacementType.AddOrModify;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00011DB0 File Offset: 0x0000FFB0
		public override long GetPathID()
		{
			return this.pathId;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00011DC8 File Offset: 0x0000FFC8
		public override int GetClassID()
		{
			return this.classId;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00011DE0 File Offset: 0x0000FFE0
		public override ushort GetMonoScriptID()
		{
			return this.monoScriptIndex;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00011DF8 File Offset: 0x0000FFF8
		public override void SetMonoScriptID(ushort scriptId)
		{
			this.monoScriptIndex = scriptId;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00011E04 File Offset: 0x00010004
		public override long GetSize()
		{
			return this.size;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00011E1C File Offset: 0x0001001C
		public override bool GetPropertiesHash(out Hash128 propertiesHash)
		{
			propertiesHash = this.propertiesHash;
			return true;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00011E3C File Offset: 0x0001003C
		public override bool SetPropertiesHash(Hash128 propertiesHash)
		{
			this.propertiesHash = propertiesHash;
			return true;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00011E58 File Offset: 0x00010058
		public override bool GetScriptIDHash(out Hash128 scriptIdHash)
		{
			scriptIdHash = this.scriptIdHash;
			return true;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00011E78 File Offset: 0x00010078
		public override bool SetScriptIDHash(Hash128 scriptIdHash)
		{
			this.scriptIdHash = scriptIdHash;
			return true;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00011E94 File Offset: 0x00010094
		public override bool GetTypeInfo(out ClassDatabaseFile file, out ClassDatabaseType type)
		{
			file = this.file;
			type = this.type;
			return true;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00011EB8 File Offset: 0x000100B8
		public override bool SetTypeInfo(ClassDatabaseFile file, ClassDatabaseType type, bool localCopy)
		{
			this.file = file;
			this.type = type;
			return true;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00011EDC File Offset: 0x000100DC
		public override bool GetPreloadDependencies(out List<AssetPPtr> preloadList)
		{
			preloadList = new List<AssetPPtr>(this.preloadList);
			return true;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00011EFC File Offset: 0x000100FC
		public override bool SetPreloadDependencies(List<AssetPPtr> preloadList)
		{
			this.preloadList = preloadList;
			return true;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00011F18 File Offset: 0x00010118
		public override bool AddPreloadDependency(AssetPPtr dependency)
		{
			this.preloadList.Add(dependency);
			return true;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00011F38 File Offset: 0x00010138
		public override long Write(AssetsFileWriter writer)
		{
			this.stream.Position = this.offset;
			this.stream.CopyToCompat(writer.BaseStream, this.size, 81920);
			return writer.Position;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00011F80 File Offset: 0x00010180
		public override long WriteReplacer(AssetsFileWriter writer)
		{
			writer.Write(2);
			writer.Write(1);
			writer.Write(1);
			writer.Write(0);
			writer.Write(this.pathId);
			writer.Write(this.classId);
			writer.Write(this.monoScriptIndex);
			writer.Write(this.preloadList.Count);
			for (int i = 0; i < this.preloadList.Count; i++)
			{
				writer.Write(this.preloadList[i].FileId);
				writer.Write(this.preloadList[i].PathId);
			}
			writer.Write(0);
			bool flag = this.propertiesHash.data != null;
			if (flag)
			{
				writer.Write(1);
				writer.Write(this.propertiesHash.data);
			}
			else
			{
				writer.Write(0);
			}
			bool flag2 = this.scriptIdHash.data != null;
			if (flag2)
			{
				writer.Write(1);
				writer.Write(this.scriptIdHash.data);
			}
			else
			{
				writer.Write(0);
			}
			bool flag3 = this.file != null;
			if (flag3)
			{
				writer.Write(1);
				this.file.Write(writer, ClassFileCompressionType.Uncompressed);
			}
			else
			{
				writer.Write(0);
			}
			writer.Write(this.GetSize());
			this.Write(writer);
			return writer.Position;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00012107 File Offset: 0x00010307
		public override void Dispose()
		{
			this.stream.Dispose();
		}

		// Token: 0x040001C2 RID: 450
		private readonly long pathId;

		// Token: 0x040001C3 RID: 451
		private readonly int classId;

		// Token: 0x040001C4 RID: 452
		private readonly Stream stream;

		// Token: 0x040001C5 RID: 453
		private readonly long offset;

		// Token: 0x040001C6 RID: 454
		private readonly long size;

		// Token: 0x040001C7 RID: 455
		private ushort monoScriptIndex;

		// Token: 0x040001C8 RID: 456
		private Hash128 propertiesHash;

		// Token: 0x040001C9 RID: 457
		private Hash128 scriptIdHash;

		// Token: 0x040001CA RID: 458
		private ClassDatabaseFile file;

		// Token: 0x040001CB RID: 459
		private ClassDatabaseType type;

		// Token: 0x040001CC RID: 460
		private List<AssetPPtr> preloadList;
	}
}
