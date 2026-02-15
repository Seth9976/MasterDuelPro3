using System;
using UnityEngine.Serialization;

namespace UnityEngine.AddressableAssets.Initialization
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	public class CacheInitializationData
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00009BF9 File Offset: 0x00007DF9
		// (set) Token: 0x06000258 RID: 600 RVA: 0x00009C01 File Offset: 0x00007E01
		public bool CompressionEnabled
		{
			get
			{
				return this.m_CompressionEnabled;
			}
			set
			{
				this.m_CompressionEnabled = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00009C0A File Offset: 0x00007E0A
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00009C12 File Offset: 0x00007E12
		public string CacheDirectoryOverride
		{
			get
			{
				return this.m_CacheDirectoryOverride;
			}
			set
			{
				this.m_CacheDirectoryOverride = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00009C1B File Offset: 0x00007E1B
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00009C23 File Offset: 0x00007E23
		public bool LimitCacheSize
		{
			get
			{
				return this.m_LimitCacheSize;
			}
			set
			{
				this.m_LimitCacheSize = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00009C2C File Offset: 0x00007E2C
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00009C34 File Offset: 0x00007E34
		public long MaximumCacheSize
		{
			get
			{
				return this.m_MaximumCacheSize;
			}
			set
			{
				this.m_MaximumCacheSize = value;
			}
		}

		// Token: 0x04000152 RID: 338
		[FormerlySerializedAs("m_compressionEnabled")]
		[SerializeField]
		private bool m_CompressionEnabled = true;

		// Token: 0x04000153 RID: 339
		[FormerlySerializedAs("m_cacheDirectoryOverride")]
		[SerializeField]
		private string m_CacheDirectoryOverride = "";

		// Token: 0x04000154 RID: 340
		[FormerlySerializedAs("m_limitCacheSize")]
		[SerializeField]
		private bool m_LimitCacheSize;

		// Token: 0x04000155 RID: 341
		[FormerlySerializedAs("m_maximumCacheSize")]
		[SerializeField]
		private long m_MaximumCacheSize = long.MaxValue;
	}
}
