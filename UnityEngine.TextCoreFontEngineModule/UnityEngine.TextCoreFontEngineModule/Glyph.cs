using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore
{
	// Token: 0x02000006 RID: 6
	[UsedByNativeCode]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class Glyph
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002494 File Offset: 0x00000694
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000024AC File Offset: 0x000006AC
		public uint index
		{
			get
			{
				return this.m_Index;
			}
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000024B8 File Offset: 0x000006B8
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000024D0 File Offset: 0x000006D0
		public GlyphMetrics metrics
		{
			get
			{
				return this.m_Metrics;
			}
			set
			{
				this.m_Metrics = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000024DC File Offset: 0x000006DC
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000024F4 File Offset: 0x000006F4
		public GlyphRect glyphRect
		{
			get
			{
				return this.m_GlyphRect;
			}
			set
			{
				this.m_GlyphRect = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002500 File Offset: 0x00000700
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002518 File Offset: 0x00000718
		public float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002524 File Offset: 0x00000724
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000253C File Offset: 0x0000073C
		public int atlasIndex
		{
			get
			{
				return this.m_AtlasIndex;
			}
			set
			{
				this.m_AtlasIndex = value;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002546 File Offset: 0x00000746
		public Glyph()
		{
			this.m_Index = 0U;
			this.m_Metrics = default(GlyphMetrics);
			this.m_GlyphRect = default(GlyphRect);
			this.m_Scale = 1f;
			this.m_AtlasIndex = 0;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002584 File Offset: 0x00000784
		internal Glyph(GlyphMarshallingStruct glyphStruct)
		{
			this.m_Index = glyphStruct.index;
			this.m_Metrics = glyphStruct.metrics;
			this.m_GlyphRect = glyphStruct.glyphRect;
			this.m_Scale = glyphStruct.scale;
			this.m_AtlasIndex = glyphStruct.atlasIndex;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000025D5 File Offset: 0x000007D5
		public Glyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			this.m_Index = index;
			this.m_Metrics = metrics;
			this.m_GlyphRect = glyphRect;
			this.m_Scale = scale;
			this.m_AtlasIndex = atlasIndex;
		}

		// Token: 0x04000026 RID: 38
		[NativeName("index")]
		[SerializeField]
		private uint m_Index;

		// Token: 0x04000027 RID: 39
		[SerializeField]
		[NativeName("metrics")]
		private GlyphMetrics m_Metrics;

		// Token: 0x04000028 RID: 40
		[SerializeField]
		[NativeName("glyphRect")]
		private GlyphRect m_GlyphRect;

		// Token: 0x04000029 RID: 41
		[SerializeField]
		[NativeName("scale")]
		private float m_Scale;

		// Token: 0x0400002A RID: 42
		[NativeName("atlasIndex")]
		[SerializeField]
		private int m_AtlasIndex;

		// Token: 0x0400002B RID: 43
		[SerializeField]
		[NativeName("type")]
		private GlyphClassDefinitionType m_ClassDefinitionType;
	}
}
