using System;
using System.Collections.Generic;

namespace AssetsTools.NET
{
	// Token: 0x0200004A RID: 74
	public class AssetsRemover : AssetsReplacer
	{
		// Token: 0x0600023A RID: 570 RVA: 0x000116E5 File Offset: 0x0000F8E5
		public AssetsRemover(long pathID)
		{
			this.pathID = pathID;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000116F8 File Offset: 0x0000F8F8
		public override AssetsReplacementType GetReplacementType()
		{
			return AssetsReplacementType.Remove;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0001170C File Offset: 0x0000F90C
		public override long GetPathID()
		{
			return this.pathID;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00011724 File Offset: 0x0000F924
		public override int GetClassID()
		{
			return 0;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00011738 File Offset: 0x0000F938
		public override ushort GetMonoScriptID()
		{
			return 0;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0001174B File Offset: 0x0000F94B
		public override void SetMonoScriptID(ushort scriptId)
		{
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00011750 File Offset: 0x0000F950
		public override long GetSize()
		{
			return 0L;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00011764 File Offset: 0x0000F964
		public override bool GetPropertiesHash(out Hash128 propertiesHash)
		{
			propertiesHash = default(Hash128);
			return false;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00011780 File Offset: 0x0000F980
		public override bool SetPropertiesHash(Hash128 propertiesHash)
		{
			return false;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00011794 File Offset: 0x0000F994
		public override bool GetScriptIDHash(out Hash128 scriptIdHash)
		{
			scriptIdHash = default(Hash128);
			return false;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000117B0 File Offset: 0x0000F9B0
		public override bool SetScriptIDHash(Hash128 scriptIdHash)
		{
			return false;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000117C4 File Offset: 0x0000F9C4
		public override bool GetTypeInfo(out ClassDatabaseFile file, out ClassDatabaseType type)
		{
			file = null;
			type = null;
			return false;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000117E0 File Offset: 0x0000F9E0
		public override bool SetTypeInfo(ClassDatabaseFile file, ClassDatabaseType type, bool localCopy)
		{
			return false;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000117F4 File Offset: 0x0000F9F4
		public override bool GetPreloadDependencies(out List<AssetPPtr> preloadList)
		{
			preloadList = null;
			return false;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0001180C File Offset: 0x0000FA0C
		public override bool SetPreloadDependencies(List<AssetPPtr> preloadList)
		{
			return false;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00011820 File Offset: 0x0000FA20
		public override bool AddPreloadDependency(AssetPPtr dependency)
		{
			return false;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00011834 File Offset: 0x0000FA34
		public override long Write(AssetsFileWriter writer)
		{
			return writer.Position;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0001184C File Offset: 0x0000FA4C
		public override long WriteReplacer(AssetsFileWriter writer)
		{
			writer.Write(0);
			writer.Write(1);
			writer.Write(1);
			writer.Write(0);
			writer.Write(this.pathID);
			writer.Write(0);
			writer.Write(0);
			writer.Write(0);
			return writer.Position;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0001174B File Offset: 0x0000F94B
		public override void Dispose()
		{
		}

		// Token: 0x040001B5 RID: 437
		private readonly long pathID;
	}
}
