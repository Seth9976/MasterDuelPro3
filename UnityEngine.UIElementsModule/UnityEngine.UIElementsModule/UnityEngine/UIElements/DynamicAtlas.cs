using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000008 RID: 8
	internal class DynamicAtlas : AtlasBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002174 File Offset: 0x00000374
		internal bool isInitialized
		{
			get
			{
				return this.m_PointPage != null || this.m_BilinearPage != null;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000218C File Offset: 0x0000038C
		protected override void OnAssignedToPanel(IPanel panel)
		{
			base.OnAssignedToPanel(panel);
			this.m_Panels.Add(panel);
			bool flag = this.m_Panels.Count == 1;
			if (flag)
			{
				this.m_ColorSpace = QualitySettings.activeColorSpace;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021CC File Offset: 0x000003CC
		protected override void OnRemovedFromPanel(IPanel panel)
		{
			this.m_Panels.Remove(panel);
			bool flag = this.m_Panels.Count == 0 && this.isInitialized;
			if (flag)
			{
				this.DestroyPages();
			}
			base.OnRemovedFromPanel(panel);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002210 File Offset: 0x00000410
		public override void Reset()
		{
			bool isInitialized = this.isInitialized;
			if (isInitialized)
			{
				this.DestroyPages();
				int i = 0;
				int count = this.m_Panels.Count;
				while (i < count)
				{
					AtlasBase.RepaintTexturedElements(this.m_Panels[i]);
					i++;
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002260 File Offset: 0x00000460
		private void InitPages()
		{
			int cleanMaxSubTextureSize = Mathf.Max(this.m_MaxSubTextureSize, 1);
			cleanMaxSubTextureSize = Mathf.NextPowerOfTwo(cleanMaxSubTextureSize);
			int cleanMaxAtlasSize = Mathf.Max(this.m_MaxAtlasSize, 1);
			cleanMaxAtlasSize = Mathf.NextPowerOfTwo(cleanMaxAtlasSize);
			cleanMaxAtlasSize = Mathf.Min(cleanMaxAtlasSize, SystemInfo.maxRenderTextureSize);
			int cleanMinAtlasSize = Mathf.Max(this.m_MinAtlasSize, 1);
			cleanMinAtlasSize = Mathf.NextPowerOfTwo(cleanMinAtlasSize);
			cleanMinAtlasSize = Mathf.Min(cleanMinAtlasSize, cleanMaxAtlasSize);
			Vector2Int cleanMinSize = new Vector2Int(cleanMinAtlasSize, cleanMinAtlasSize);
			Vector2Int cleanMaxSize = new Vector2Int(cleanMaxAtlasSize, cleanMaxAtlasSize);
			this.m_PointPage = new DynamicAtlasPage(RenderTextureFormat.ARGB32, FilterMode.Point, cleanMinSize, cleanMaxSize);
			this.m_BilinearPage = new DynamicAtlasPage(RenderTextureFormat.ARGB32, FilterMode.Bilinear, cleanMinSize, cleanMaxSize);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022F0 File Offset: 0x000004F0
		private void DestroyPages()
		{
			this.m_PointPage.Dispose();
			this.m_PointPage = null;
			this.m_BilinearPage.Dispose();
			this.m_BilinearPage = null;
			this.m_Database.Clear();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002328 File Offset: 0x00000528
		public override bool TryGetAtlas(VisualElement ve, Texture2D src, out TextureId atlas, out RectInt atlasRect)
		{
			bool flag = this.m_Panels.Count == 0 || src == null;
			bool flag2;
			if (flag)
			{
				atlas = TextureId.invalid;
				atlasRect = default(RectInt);
				flag2 = false;
			}
			else
			{
				bool flag3 = !this.isInitialized;
				if (flag3)
				{
					this.InitPages();
				}
				DynamicAtlas.TextureInfo info;
				bool flag4 = this.m_Database.TryGetValue(src, out info);
				if (flag4)
				{
					atlas = info.page.textureId;
					atlasRect = info.rect;
					info.counter++;
					flag2 = true;
				}
				else
				{
					Allocator2D.Alloc2D alloc;
					bool flag5 = this.IsTextureValid(src, FilterMode.Bilinear) && this.m_BilinearPage.TryAdd(src, out alloc, out atlasRect);
					if (flag5)
					{
						info = DynamicAtlas.TextureInfo.pool.Get();
						info.alloc = alloc;
						info.counter = 1;
						info.page = this.m_BilinearPage;
						info.rect = atlasRect;
						this.m_Database[src] = info;
						atlas = this.m_BilinearPage.textureId;
						flag2 = true;
					}
					else
					{
						bool flag6 = this.IsTextureValid(src, FilterMode.Point) && this.m_PointPage.TryAdd(src, out alloc, out atlasRect);
						if (flag6)
						{
							info = DynamicAtlas.TextureInfo.pool.Get();
							info.alloc = alloc;
							info.counter = 1;
							info.page = this.m_PointPage;
							info.rect = atlasRect;
							this.m_Database[src] = info;
							atlas = this.m_PointPage.textureId;
							flag2 = true;
						}
						else
						{
							atlas = TextureId.invalid;
							atlasRect = default(RectInt);
							flag2 = false;
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024D8 File Offset: 0x000006D8
		public override void ReturnAtlas(VisualElement ve, Texture2D src, TextureId atlas)
		{
			DynamicAtlas.TextureInfo info;
			bool flag = this.m_Database.TryGetValue(src, out info);
			if (flag)
			{
				info.counter--;
				bool flag2 = info.counter == 0;
				if (flag2)
				{
					info.page.Remove(info.alloc);
					this.m_Database.Remove(src);
					DynamicAtlas.TextureInfo.pool.Return(info);
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002544 File Offset: 0x00000744
		protected override void OnUpdateDynamicTextures(IPanel panel)
		{
			bool flag = this.m_PointPage != null;
			if (flag)
			{
				this.m_PointPage.Commit();
				base.SetDynamicTexture(this.m_PointPage.textureId, this.m_PointPage.atlas);
			}
			bool flag2 = this.m_BilinearPage != null;
			if (flag2)
			{
				this.m_BilinearPage.Commit();
				base.SetDynamicTexture(this.m_BilinearPage.textureId, this.m_BilinearPage.atlas);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025C4 File Offset: 0x000007C4
		internal static bool IsTextureFormatSupported(TextureFormat format)
		{
			switch (format)
			{
			case TextureFormat.Alpha8:
			case TextureFormat.ARGB4444:
			case TextureFormat.RGB24:
			case TextureFormat.RGBA32:
			case TextureFormat.ARGB32:
			case TextureFormat.RGB565:
			case TextureFormat.R16:
			case TextureFormat.DXT1:
			case TextureFormat.DXT5:
			case TextureFormat.RGBA4444:
			case TextureFormat.BGRA32:
			case TextureFormat.BC7:
			case TextureFormat.BC4:
			case TextureFormat.BC5:
			case TextureFormat.DXT1Crunched:
			case TextureFormat.DXT5Crunched:
			case TextureFormat.PVRTC_RGB2:
			case TextureFormat.PVRTC_RGBA2:
			case TextureFormat.PVRTC_RGB4:
			case TextureFormat.PVRTC_RGBA4:
			case TextureFormat.ETC_RGB4:
			case TextureFormat.EAC_R:
			case TextureFormat.EAC_R_SIGNED:
			case TextureFormat.EAC_RG:
			case TextureFormat.EAC_RG_SIGNED:
			case TextureFormat.ETC2_RGB:
			case TextureFormat.ETC2_RGBA1:
			case TextureFormat.ETC2_RGBA8:
			case TextureFormat.ASTC_4x4:
			case TextureFormat.ASTC_5x5:
			case TextureFormat.ASTC_6x6:
			case TextureFormat.ASTC_8x8:
			case TextureFormat.ASTC_10x10:
			case TextureFormat.ASTC_12x12:
			case TextureFormat.RG16:
			case TextureFormat.R8:
			case TextureFormat.ETC_RGB4Crunched:
			case TextureFormat.ETC2_RGBA8Crunched:
				return true;
			case TextureFormat.RHalf:
			case TextureFormat.RGHalf:
			case TextureFormat.RGBAHalf:
			case TextureFormat.RFloat:
			case TextureFormat.RGFloat:
			case TextureFormat.RGBAFloat:
			case TextureFormat.YUY2:
			case TextureFormat.RGB9e5Float:
			case TextureFormat.BC6H:
			case TextureFormat.ASTC_HDR_4x4:
			case TextureFormat.ASTC_HDR_5x5:
			case TextureFormat.ASTC_HDR_6x6:
			case TextureFormat.ASTC_HDR_8x8:
			case TextureFormat.ASTC_HDR_10x10:
			case TextureFormat.ASTC_HDR_12x12:
			case TextureFormat.RG32:
			case TextureFormat.RGB48:
			case TextureFormat.RGBA64:
			case TextureFormat.R8_SIGNED:
			case TextureFormat.RG16_SIGNED:
			case TextureFormat.RGB24_SIGNED:
			case TextureFormat.RGBA32_SIGNED:
			case TextureFormat.R16_SIGNED:
			case TextureFormat.RG32_SIGNED:
			case TextureFormat.RGB48_SIGNED:
			case TextureFormat.RGBA64_SIGNED:
				return false;
			}
			return false;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002738 File Offset: 0x00000938
		public virtual bool IsTextureValid(Texture2D texture, FilterMode atlasFilterMode)
		{
			DynamicAtlasFilters filters = this.m_ActiveFilters;
			bool flag = this.m_CustomFilter != null && !this.m_CustomFilter(texture, ref filters);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool filterReadability = (filters & DynamicAtlasFilters.Readability) > DynamicAtlasFilters.None;
				bool filterSize = (filters & DynamicAtlasFilters.Size) > DynamicAtlasFilters.None;
				bool filterFormat = (filters & DynamicAtlasFilters.Format) > DynamicAtlasFilters.None;
				bool filterColorSpace = (filters & DynamicAtlasFilters.ColorSpace) > DynamicAtlasFilters.None;
				bool filterFilterMode = (filters & DynamicAtlasFilters.FilterMode) > DynamicAtlasFilters.None;
				bool flag3 = filterReadability && texture.isReadable;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = filterSize && (texture.width > this.maxSubTextureSize || texture.height > this.maxSubTextureSize);
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						bool flag5 = filterFormat && !DynamicAtlas.IsTextureFormatSupported(texture.format);
						if (flag5)
						{
							flag2 = false;
						}
						else
						{
							bool flag6 = filterColorSpace && this.m_ColorSpace == ColorSpace.Linear && texture.activeTextureColorSpace > ColorSpace.Gamma;
							if (flag6)
							{
								flag2 = false;
							}
							else
							{
								bool flag7 = filterFilterMode && texture.filterMode != atlasFilterMode;
								flag2 = !flag7;
							}
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x17000002 RID: 2
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002850 File Offset: 0x00000A50
		public int minAtlasSize
		{
			set
			{
				bool flag = this.m_MinAtlasSize == value;
				if (!flag)
				{
					this.m_MinAtlasSize = value;
					this.Reset();
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000287C File Offset: 0x00000A7C
		public int maxAtlasSize
		{
			set
			{
				bool flag = this.m_MaxAtlasSize == value;
				if (!flag)
				{
					this.m_MaxAtlasSize = value;
					this.Reset();
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000028A7 File Offset: 0x00000AA7
		public static DynamicAtlasFilters defaultFilters
		{
			get
			{
				return DynamicAtlasFilters.Readability | DynamicAtlasFilters.Size | DynamicAtlasFilters.Format | DynamicAtlasFilters.ColorSpace | DynamicAtlasFilters.FilterMode;
			}
		}

		// Token: 0x17000005 RID: 5
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000028AC File Offset: 0x00000AAC
		public DynamicAtlasFilters activeFilters
		{
			set
			{
				bool flag = this.m_ActiveFilters == value;
				if (!flag)
				{
					this.m_ActiveFilters = value;
					this.Reset();
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000028D8 File Offset: 0x00000AD8
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000028F0 File Offset: 0x00000AF0
		public int maxSubTextureSize
		{
			get
			{
				return this.m_MaxSubTextureSize;
			}
			set
			{
				bool flag = this.m_MaxSubTextureSize == value;
				if (!flag)
				{
					this.m_MaxSubTextureSize = value;
					this.Reset();
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000291C File Offset: 0x00000B1C
		public DynamicAtlasCustomFilter customFilter
		{
			set
			{
				bool flag = this.m_CustomFilter == value;
				if (!flag)
				{
					this.m_CustomFilter = value;
					this.Reset();
				}
			}
		}

		// Token: 0x04000009 RID: 9
		private Dictionary<Texture, DynamicAtlas.TextureInfo> m_Database = new Dictionary<Texture, DynamicAtlas.TextureInfo>();

		// Token: 0x0400000A RID: 10
		private DynamicAtlasPage m_PointPage;

		// Token: 0x0400000B RID: 11
		private DynamicAtlasPage m_BilinearPage;

		// Token: 0x0400000C RID: 12
		private ColorSpace m_ColorSpace;

		// Token: 0x0400000D RID: 13
		private List<IPanel> m_Panels = new List<IPanel>(1);

		// Token: 0x0400000E RID: 14
		private int m_MinAtlasSize = 64;

		// Token: 0x0400000F RID: 15
		private int m_MaxAtlasSize = 4096;

		// Token: 0x04000010 RID: 16
		private int m_MaxSubTextureSize = 64;

		// Token: 0x04000011 RID: 17
		private DynamicAtlasFilters m_ActiveFilters = DynamicAtlas.defaultFilters;

		// Token: 0x04000012 RID: 18
		private DynamicAtlasCustomFilter m_CustomFilter;

		// Token: 0x02000009 RID: 9
		internal class TextureInfo : LinkedPoolItem<DynamicAtlas.TextureInfo>
		{
			// Token: 0x06000027 RID: 39 RVA: 0x0000299D File Offset: 0x00000B9D
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static DynamicAtlas.TextureInfo Create()
			{
				return new DynamicAtlas.TextureInfo();
			}

			// Token: 0x06000028 RID: 40 RVA: 0x000029A4 File Offset: 0x00000BA4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void Reset(DynamicAtlas.TextureInfo info)
			{
				info.page = null;
				info.counter = 0;
				info.alloc = default(Allocator2D.Alloc2D);
				info.rect = default(RectInt);
			}

			// Token: 0x04000013 RID: 19
			public DynamicAtlasPage page;

			// Token: 0x04000014 RID: 20
			public int counter;

			// Token: 0x04000015 RID: 21
			public Allocator2D.Alloc2D alloc;

			// Token: 0x04000016 RID: 22
			public RectInt rect;

			// Token: 0x04000017 RID: 23
			public static readonly LinkedPool<DynamicAtlas.TextureInfo> pool = new LinkedPool<DynamicAtlas.TextureInfo>(new Func<DynamicAtlas.TextureInfo>(DynamicAtlas.TextureInfo.Create), new Action<DynamicAtlas.TextureInfo>(DynamicAtlas.TextureInfo.Reset), 1024);
		}
	}
}
