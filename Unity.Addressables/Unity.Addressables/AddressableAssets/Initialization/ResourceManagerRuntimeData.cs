using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.Serialization;

namespace UnityEngine.AddressableAssets.Initialization
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	public class ResourceManagerRuntimeData
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000A59B File Offset: 0x0000879B
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000A5A3 File Offset: 0x000087A3
		public string BuildTarget
		{
			get
			{
				return this.m_buildTarget;
			}
			set
			{
				this.m_buildTarget = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A5AC File Offset: 0x000087AC
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000A5B4 File Offset: 0x000087B4
		public string SettingsHash
		{
			get
			{
				return this.m_SettingsHash;
			}
			set
			{
				this.m_SettingsHash = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000A5BD File Offset: 0x000087BD
		public List<ResourceLocationData> CatalogLocations
		{
			get
			{
				return this.m_CatalogLocations;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000A5C5 File Offset: 0x000087C5
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000A5CD File Offset: 0x000087CD
		public bool LogResourceManagerExceptions
		{
			get
			{
				return this.m_LogResourceManagerExceptions;
			}
			set
			{
				this.m_LogResourceManagerExceptions = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000A5D6 File Offset: 0x000087D6
		public List<ObjectInitializationData> InitializationObjects
		{
			get
			{
				return this.m_ExtraInitializationData;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000A5DE File Offset: 0x000087DE
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000A5E6 File Offset: 0x000087E6
		public bool DisableCatalogUpdateOnStartup
		{
			get
			{
				return this.m_DisableCatalogUpdateOnStart;
			}
			set
			{
				this.m_DisableCatalogUpdateOnStart = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000A5EF File Offset: 0x000087EF
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000A5F7 File Offset: 0x000087F7
		public bool IsLocalCatalogInBundle
		{
			get
			{
				return this.m_IsLocalCatalogInBundle;
			}
			set
			{
				this.m_IsLocalCatalogInBundle = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000A600 File Offset: 0x00008800
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000A60D File Offset: 0x0000880D
		public Type CertificateHandlerType
		{
			get
			{
				return this.m_CertificateHandlerType.Value;
			}
			set
			{
				this.m_CertificateHandlerType.Value = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000A61B File Offset: 0x0000881B
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000A623 File Offset: 0x00008823
		public string AddressablesVersion
		{
			get
			{
				return this.m_AddressablesVersion;
			}
			set
			{
				this.m_AddressablesVersion = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000A62C File Offset: 0x0000882C
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000A634 File Offset: 0x00008834
		public int MaxConcurrentWebRequests
		{
			get
			{
				return this.m_maxConcurrentWebRequests;
			}
			set
			{
				this.m_maxConcurrentWebRequests = Mathf.Clamp(value, 1, 1024);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000A648 File Offset: 0x00008848
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000A650 File Offset: 0x00008850
		public int CatalogRequestsTimeout
		{
			get
			{
				return this.m_CatalogRequestsTimeout;
			}
			set
			{
				this.m_CatalogRequestsTimeout = ((value < 0) ? 0 : value);
			}
		}

		// Token: 0x04000165 RID: 357
		public const string kCatalogAddress = "AddressablesMainContentCatalog";

		// Token: 0x04000166 RID: 358
		[SerializeField]
		private string m_buildTarget;

		// Token: 0x04000167 RID: 359
		[FormerlySerializedAs("m_settingsHash")]
		[SerializeField]
		private string m_SettingsHash;

		// Token: 0x04000168 RID: 360
		[FormerlySerializedAs("m_catalogLocations")]
		[SerializeField]
		private List<ResourceLocationData> m_CatalogLocations = new List<ResourceLocationData>();

		// Token: 0x04000169 RID: 361
		[FormerlySerializedAs("m_logResourceManagerExceptions")]
		[SerializeField]
		private bool m_LogResourceManagerExceptions = true;

		// Token: 0x0400016A RID: 362
		[FormerlySerializedAs("m_extraInitializationData")]
		[SerializeField]
		private List<ObjectInitializationData> m_ExtraInitializationData = new List<ObjectInitializationData>();

		// Token: 0x0400016B RID: 363
		[SerializeField]
		private bool m_DisableCatalogUpdateOnStart;

		// Token: 0x0400016C RID: 364
		[SerializeField]
		private bool m_IsLocalCatalogInBundle;

		// Token: 0x0400016D RID: 365
		[SerializeField]
		private SerializedType m_CertificateHandlerType;

		// Token: 0x0400016E RID: 366
		[SerializeField]
		private string m_AddressablesVersion;

		// Token: 0x0400016F RID: 367
		[SerializeField]
		private int m_maxConcurrentWebRequests = 500;

		// Token: 0x04000170 RID: 368
		[SerializeField]
		private int m_CatalogRequestsTimeout;
	}
}
