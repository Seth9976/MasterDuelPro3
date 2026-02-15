using System;
using System.Collections.Generic;

namespace AssetsTools.NET
{
	// Token: 0x0200004D RID: 77
	public class AssetsReplacerFromMemory : AssetsReplacer
	{
		// Token: 0x06000260 RID: 608 RVA: 0x000118B5 File Offset: 0x0000FAB5
		public AssetsReplacerFromMemory(long pathId, int classId, ushort monoScriptIndex, byte[] buffer)
		{
			this.pathId = pathId;
			this.classId = classId;
			this.monoScriptIndex = monoScriptIndex;
			this.buffer = buffer;
			this.preloadList = new List<AssetPPtr>();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000118E8 File Offset: 0x0000FAE8
		public AssetsReplacerFromMemory(AssetsFile file, AssetFileInfo info, AssetTypeValueField field)
		{
			this.pathId = info.PathId;
			this.classId = info.TypeId;
			this.monoScriptIndex = file.GetScriptIndex(info);
			this.buffer = field.WriteToByteArray(false);
			this.preloadList = new List<AssetPPtr>();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0001193C File Offset: 0x0000FB3C
		public AssetsReplacerFromMemory(AssetsFile file, AssetFileInfo info, byte[] buffer)
		{
			this.pathId = info.PathId;
			this.classId = info.TypeId;
			this.monoScriptIndex = file.GetScriptIndex(info);
			this.buffer = buffer;
			this.preloadList = new List<AssetPPtr>();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00011988 File Offset: 0x0000FB88
		public override AssetsReplacementType GetReplacementType()
		{
			return AssetsReplacementType.AddOrModify;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0001199C File Offset: 0x0000FB9C
		public override long GetPathID()
		{
			return this.pathId;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000119B4 File Offset: 0x0000FBB4
		public override int GetClassID()
		{
			return this.classId;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000119CC File Offset: 0x0000FBCC
		public override ushort GetMonoScriptID()
		{
			return this.monoScriptIndex;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000119E4 File Offset: 0x0000FBE4
		public override void SetMonoScriptID(ushort scriptId)
		{
			this.monoScriptIndex = scriptId;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000119F0 File Offset: 0x0000FBF0
		public override bool GetPropertiesHash(out Hash128 propertiesHash)
		{
			propertiesHash = this.propertiesHash;
			return true;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00011A10 File Offset: 0x0000FC10
		public override bool SetPropertiesHash(Hash128 propertiesHash)
		{
			this.propertiesHash = propertiesHash;
			return true;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00011A2C File Offset: 0x0000FC2C
		public override bool GetScriptIDHash(out Hash128 scriptIdHash)
		{
			scriptIdHash = this.scriptIdHash;
			return true;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00011A4C File Offset: 0x0000FC4C
		public override bool SetScriptIDHash(Hash128 scriptIdHash)
		{
			this.scriptIdHash = scriptIdHash;
			return true;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00011A68 File Offset: 0x0000FC68
		public override bool GetTypeInfo(out ClassDatabaseFile file, out ClassDatabaseType type)
		{
			file = this.file;
			type = this.type;
			return true;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00011A8C File Offset: 0x0000FC8C
		public override bool SetTypeInfo(ClassDatabaseFile file, ClassDatabaseType type, bool localCopy)
		{
			this.file = file;
			this.type = type;
			return true;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00011AB0 File Offset: 0x0000FCB0
		public override bool GetPreloadDependencies(out List<AssetPPtr> preloadList)
		{
			preloadList = new List<AssetPPtr>(this.preloadList);
			return true;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00011AD0 File Offset: 0x0000FCD0
		public override bool SetPreloadDependencies(List<AssetPPtr> preloadList)
		{
			this.preloadList = preloadList;
			return true;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00011AEC File Offset: 0x0000FCEC
		public override bool AddPreloadDependency(AssetPPtr dependency)
		{
			bool flag = this.preloadList == null;
			if (flag)
			{
				this.preloadList = new List<AssetPPtr>();
			}
			this.preloadList.Add(dependency);
			return true;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00011B24 File Offset: 0x0000FD24
		public override long GetSize()
		{
			return (long)this.buffer.Length;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00011B40 File Offset: 0x0000FD40
		public override long Write(AssetsFileWriter writer)
		{
			writer.Write(this.buffer);
			return writer.Position;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00011B68 File Offset: 0x0000FD68
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

		// Token: 0x06000274 RID: 628 RVA: 0x0001174B File Offset: 0x0000F94B
		public override void Dispose()
		{
		}

		// Token: 0x040001B9 RID: 441
		private readonly long pathId;

		// Token: 0x040001BA RID: 442
		private readonly int classId;

		// Token: 0x040001BB RID: 443
		private readonly byte[] buffer;

		// Token: 0x040001BC RID: 444
		private ushort monoScriptIndex;

		// Token: 0x040001BD RID: 445
		private Hash128 propertiesHash;

		// Token: 0x040001BE RID: 446
		private Hash128 scriptIdHash;

		// Token: 0x040001BF RID: 447
		private ClassDatabaseFile file;

		// Token: 0x040001C0 RID: 448
		private ClassDatabaseType type;

		// Token: 0x040001C1 RID: 449
		private List<AssetPPtr> preloadList;
	}
}
