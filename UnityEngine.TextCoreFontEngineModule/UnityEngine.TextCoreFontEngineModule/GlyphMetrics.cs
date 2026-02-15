using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore
{
	// Token: 0x02000005 RID: 5
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphMetrics : IEquatable<GlyphMetrics>
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002384 File Offset: 0x00000584
		public float width
		{
			get
			{
				return this.m_Width;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000239C File Offset: 0x0000059C
		public float height
		{
			get
			{
				return this.m_Height;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023B4 File Offset: 0x000005B4
		public float horizontalBearingX
		{
			get
			{
				return this.m_HorizontalBearingX;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000023CC File Offset: 0x000005CC
		public float horizontalBearingY
		{
			get
			{
				return this.m_HorizontalBearingY;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023E4 File Offset: 0x000005E4
		public float horizontalAdvance
		{
			get
			{
				return this.m_HorizontalAdvance;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023FC File Offset: 0x000005FC
		public GlyphMetrics(float width, float height, float bearingX, float bearingY, float advance)
		{
			this.m_Width = width;
			this.m_Height = height;
			this.m_HorizontalBearingX = bearingX;
			this.m_HorizontalBearingY = bearingY;
			this.m_HorizontalAdvance = advance;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002424 File Offset: 0x00000624
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002448 File Offset: 0x00000648
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000246C File Offset: 0x0000066C
		public bool Equals(GlyphMetrics other)
		{
			return base.Equals(other);
		}

		// Token: 0x04000021 RID: 33
		[SerializeField]
		[NativeName("width")]
		private float m_Width;

		// Token: 0x04000022 RID: 34
		[SerializeField]
		[NativeName("height")]
		private float m_Height;

		// Token: 0x04000023 RID: 35
		[SerializeField]
		[NativeName("horizontalBearingX")]
		private float m_HorizontalBearingX;

		// Token: 0x04000024 RID: 36
		[SerializeField]
		[NativeName("horizontalBearingY")]
		private float m_HorizontalBearingY;

		// Token: 0x04000025 RID: 37
		[SerializeField]
		[NativeName("horizontalAdvance")]
		private float m_HorizontalAdvance;
	}
}
