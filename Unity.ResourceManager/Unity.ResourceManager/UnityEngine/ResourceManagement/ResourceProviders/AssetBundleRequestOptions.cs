using System;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.Serialization;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	public class AssetBundleRequestOptions : ILocationSizeData
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00007029 File Offset: 0x00005229
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00007031 File Offset: 0x00005231
		public string Hash
		{
			get
			{
				return this.m_Hash;
			}
			set
			{
				this.m_Hash = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000703A File Offset: 0x0000523A
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00007042 File Offset: 0x00005242
		public uint Crc
		{
			get
			{
				return this.m_Crc;
			}
			set
			{
				this.m_Crc = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000704B File Offset: 0x0000524B
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00007053 File Offset: 0x00005253
		public int Timeout
		{
			get
			{
				return this.m_Timeout;
			}
			set
			{
				this.m_Timeout = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000705C File Offset: 0x0000525C
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00007064 File Offset: 0x00005264
		public bool ChunkedTransfer
		{
			get
			{
				return this.m_ChunkedTransfer;
			}
			set
			{
				this.m_ChunkedTransfer = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000706D File Offset: 0x0000526D
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00007088 File Offset: 0x00005288
		public int RedirectLimit
		{
			get
			{
				if (this.m_RedirectLimit <= 128)
				{
					return this.m_RedirectLimit;
				}
				return 128;
			}
			set
			{
				this.m_RedirectLimit = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00007091 File Offset: 0x00005291
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00007099 File Offset: 0x00005299
		public int RetryCount
		{
			get
			{
				return this.m_RetryCount;
			}
			set
			{
				this.m_RetryCount = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000070A2 File Offset: 0x000052A2
		// (set) Token: 0x06000185 RID: 389 RVA: 0x000070AA File Offset: 0x000052AA
		public string BundleName
		{
			get
			{
				return this.m_BundleName;
			}
			set
			{
				this.m_BundleName = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000070B3 File Offset: 0x000052B3
		// (set) Token: 0x06000187 RID: 391 RVA: 0x000070BB File Offset: 0x000052BB
		public AssetLoadMode AssetLoadMode
		{
			get
			{
				return this.m_AssetLoadMode;
			}
			set
			{
				this.m_AssetLoadMode = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000070C4 File Offset: 0x000052C4
		// (set) Token: 0x06000189 RID: 393 RVA: 0x000070CC File Offset: 0x000052CC
		public long BundleSize
		{
			get
			{
				return this.m_BundleSize;
			}
			set
			{
				this.m_BundleSize = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000070D5 File Offset: 0x000052D5
		// (set) Token: 0x0600018B RID: 395 RVA: 0x000070DD File Offset: 0x000052DD
		public bool UseCrcForCachedBundle
		{
			get
			{
				return this.m_UseCrcForCachedBundles;
			}
			set
			{
				this.m_UseCrcForCachedBundles = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000070E6 File Offset: 0x000052E6
		// (set) Token: 0x0600018D RID: 397 RVA: 0x000070EE File Offset: 0x000052EE
		public bool UseUnityWebRequestForLocalBundles
		{
			get
			{
				return this.m_UseUWRForLocalBundles;
			}
			set
			{
				this.m_UseUWRForLocalBundles = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000070F7 File Offset: 0x000052F7
		// (set) Token: 0x0600018F RID: 399 RVA: 0x000070FF File Offset: 0x000052FF
		public bool ClearOtherCachedVersionsWhenLoaded
		{
			get
			{
				return this.m_ClearOtherCachedVersionsWhenLoaded;
			}
			set
			{
				this.m_ClearOtherCachedVersionsWhenLoaded = value;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007108 File Offset: 0x00005308
		public virtual long ComputeSize(IResourceLocation location, ResourceManager resourceManager)
		{
			if (!ResourceManagerConfig.IsPathRemote((resourceManager == null) ? location.InternalId : resourceManager.TransformInternalId(location)))
			{
				return 0L;
			}
			Hash128 locHash = Hash128.Parse(this.Hash);
			if (!locHash.isValid)
			{
				return this.BundleSize;
			}
			if (Caching.IsVersionCached(new CachedAssetBundle(this.BundleName, locHash)))
			{
				return 0L;
			}
			return this.BundleSize;
		}

		// Token: 0x040000A6 RID: 166
		[FormerlySerializedAs("m_hash")]
		[SerializeField]
		private string m_Hash = "";

		// Token: 0x040000A7 RID: 167
		[FormerlySerializedAs("m_crc")]
		[SerializeField]
		private uint m_Crc;

		// Token: 0x040000A8 RID: 168
		[FormerlySerializedAs("m_timeout")]
		[SerializeField]
		private int m_Timeout;

		// Token: 0x040000A9 RID: 169
		[FormerlySerializedAs("m_chunkedTransfer")]
		[SerializeField]
		private bool m_ChunkedTransfer;

		// Token: 0x040000AA RID: 170
		[FormerlySerializedAs("m_redirectLimit")]
		[SerializeField]
		private int m_RedirectLimit = -1;

		// Token: 0x040000AB RID: 171
		[FormerlySerializedAs("m_retryCount")]
		[SerializeField]
		private int m_RetryCount;

		// Token: 0x040000AC RID: 172
		[SerializeField]
		private string m_BundleName;

		// Token: 0x040000AD RID: 173
		[SerializeField]
		private AssetLoadMode m_AssetLoadMode;

		// Token: 0x040000AE RID: 174
		[SerializeField]
		private long m_BundleSize;

		// Token: 0x040000AF RID: 175
		[SerializeField]
		private bool m_UseCrcForCachedBundles;

		// Token: 0x040000B0 RID: 176
		[SerializeField]
		private bool m_UseUWRForLocalBundles;

		// Token: 0x040000B1 RID: 177
		[SerializeField]
		private bool m_ClearOtherCachedVersionsWhenLoaded;
	}
}
