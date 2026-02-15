using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore
{
	// Token: 0x02000004 RID: 4
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphRect : IEquatable<GlyphRect>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000226C File Offset: 0x0000046C
		public int x
		{
			get
			{
				return this.m_X;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002284 File Offset: 0x00000484
		public int y
		{
			get
			{
				return this.m_Y;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000229C File Offset: 0x0000049C
		public int width
		{
			get
			{
				return this.m_Width;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022B4 File Offset: 0x000004B4
		public int height
		{
			get
			{
				return this.m_Height;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022CC File Offset: 0x000004CC
		public static GlyphRect zero
		{
			get
			{
				return GlyphRect.s_ZeroGlyphRect;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022E3 File Offset: 0x000004E3
		public GlyphRect(int x, int y, int width, int height)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002304 File Offset: 0x00000504
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002328 File Offset: 0x00000528
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000234C File Offset: 0x0000054C
		public bool Equals(GlyphRect other)
		{
			return base.Equals(other);
		}

		// Token: 0x0400001C RID: 28
		[SerializeField]
		[NativeName("x")]
		private int m_X;

		// Token: 0x0400001D RID: 29
		[SerializeField]
		[NativeName("y")]
		private int m_Y;

		// Token: 0x0400001E RID: 30
		[NativeName("width")]
		[SerializeField]
		private int m_Width;

		// Token: 0x0400001F RID: 31
		[SerializeField]
		[NativeName("height")]
		private int m_Height;

		// Token: 0x04000020 RID: 32
		private static readonly GlyphRect s_ZeroGlyphRect = new GlyphRect(0, 0, 0, 0);
	}
}
