using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024D RID: 589
	[Serializable]
	public class DynamicAtlasSettings
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00044739 File Offset: 0x00042939
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x00044741 File Offset: 0x00042941
		public int minAtlasSize
		{
			get
			{
				return this.m_MinAtlasSize;
			}
			set
			{
				this.m_MinAtlasSize = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0004474A File Offset: 0x0004294A
		// (set) Token: 0x06000FEA RID: 4074 RVA: 0x00044752 File Offset: 0x00042952
		public int maxAtlasSize
		{
			get
			{
				return this.m_MaxAtlasSize;
			}
			set
			{
				this.m_MaxAtlasSize = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x0004475B File Offset: 0x0004295B
		// (set) Token: 0x06000FEC RID: 4076 RVA: 0x00044763 File Offset: 0x00042963
		public int maxSubTextureSize
		{
			get
			{
				return this.m_MaxSubTextureSize;
			}
			set
			{
				this.m_MaxSubTextureSize = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0004476C File Offset: 0x0004296C
		// (set) Token: 0x06000FEE RID: 4078 RVA: 0x00044774 File Offset: 0x00042974
		public DynamicAtlasFilters activeFilters
		{
			get
			{
				return this.m_ActiveFilters;
			}
			set
			{
				this.m_ActiveFilters = value;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x0004477D File Offset: 0x0004297D
		public static DynamicAtlasFilters defaultFilters
		{
			get
			{
				return DynamicAtlas.defaultFilters;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x00044784 File Offset: 0x00042984
		// (set) Token: 0x06000FF1 RID: 4081 RVA: 0x0004478C File Offset: 0x0004298C
		public DynamicAtlasCustomFilter customFilter
		{
			get
			{
				return this.m_CustomFilter;
			}
			set
			{
				this.m_CustomFilter = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x00044795 File Offset: 0x00042995
		public static DynamicAtlasSettings defaults
		{
			get
			{
				return new DynamicAtlasSettings
				{
					minAtlasSize = 64,
					maxAtlasSize = 4096,
					maxSubTextureSize = 64,
					activeFilters = DynamicAtlasSettings.defaultFilters,
					customFilter = null
				};
			}
		}

		// Token: 0x040008E9 RID: 2281
		[HideInInspector]
		[SerializeField]
		private int m_MinAtlasSize;

		// Token: 0x040008EA RID: 2282
		[SerializeField]
		[HideInInspector]
		private int m_MaxAtlasSize;

		// Token: 0x040008EB RID: 2283
		[HideInInspector]
		[SerializeField]
		private int m_MaxSubTextureSize;

		// Token: 0x040008EC RID: 2284
		[HideInInspector]
		[SerializeField]
		private DynamicAtlasFilters m_ActiveFilters;

		// Token: 0x040008ED RID: 2285
		private DynamicAtlasCustomFilter m_CustomFilter;
	}
}
