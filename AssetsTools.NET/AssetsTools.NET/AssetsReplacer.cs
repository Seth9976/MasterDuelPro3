using System;
using System.Collections.Generic;

namespace AssetsTools.NET
{
	// Token: 0x0200004C RID: 76
	public abstract class AssetsReplacer : IDisposable
	{
		// Token: 0x0600024D RID: 589
		public abstract AssetsReplacementType GetReplacementType();

		// Token: 0x0600024E RID: 590
		public abstract long GetPathID();

		// Token: 0x0600024F RID: 591
		public abstract int GetClassID();

		// Token: 0x06000250 RID: 592
		public abstract ushort GetMonoScriptID();

		// Token: 0x06000251 RID: 593
		public abstract long Write(AssetsFileWriter writer);

		// Token: 0x06000252 RID: 594
		public abstract long WriteReplacer(AssetsFileWriter writer);

		// Token: 0x06000253 RID: 595 RVA: 0x0001174B File Offset: 0x0000F94B
		public virtual void Dispose()
		{
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000118AD File Offset: 0x0000FAAD
		public virtual long GetSize()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000118AD File Offset: 0x0000FAAD
		public virtual void SetMonoScriptID(ushort scriptId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000118AD File Offset: 0x0000FAAD
		public virtual bool GetPropertiesHash(out Hash128 propertiesHash)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000118AD File Offset: 0x0000FAAD
		public virtual bool SetPropertiesHash(Hash128 propertiesHash)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000118AD File Offset: 0x0000FAAD
		public virtual bool GetScriptIDHash(out Hash128 scriptIdHash)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000118AD File Offset: 0x0000FAAD
		public virtual bool SetScriptIDHash(Hash128 scriptIdHash)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000118AD File Offset: 0x0000FAAD
		public virtual bool GetTypeInfo(out ClassDatabaseFile file, out ClassDatabaseType type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000118AD File Offset: 0x0000FAAD
		public virtual bool SetTypeInfo(ClassDatabaseFile file, ClassDatabaseType type, bool localCopy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000118AD File Offset: 0x0000FAAD
		public virtual bool GetPreloadDependencies(out List<AssetPPtr> preloadList)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000118AD File Offset: 0x0000FAAD
		public virtual bool SetPreloadDependencies(List<AssetPPtr> preloadList)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000118AD File Offset: 0x0000FAAD
		public virtual bool AddPreloadDependency(AssetPPtr dependency)
		{
			throw new NotImplementedException();
		}
	}
}
