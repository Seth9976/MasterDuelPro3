using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000010 RID: 16
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphValueRecord : IEquatable<GlyphValueRecord>
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003538 File Offset: 0x00001738
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003550 File Offset: 0x00001750
		public float xPlacement
		{
			get
			{
				return this.m_XPlacement;
			}
			set
			{
				this.m_XPlacement = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000355C File Offset: 0x0000175C
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003574 File Offset: 0x00001774
		public float yPlacement
		{
			get
			{
				return this.m_YPlacement;
			}
			set
			{
				this.m_YPlacement = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003580 File Offset: 0x00001780
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003598 File Offset: 0x00001798
		public float xAdvance
		{
			get
			{
				return this.m_XAdvance;
			}
			set
			{
				this.m_XAdvance = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000035A4 File Offset: 0x000017A4
		public float yAdvance
		{
			get
			{
				return this.m_YAdvance;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000035BC File Offset: 0x000017BC
		public GlyphValueRecord(float xPlacement, float yPlacement, float xAdvance, float yAdvance)
		{
			this.m_XPlacement = xPlacement;
			this.m_YPlacement = yPlacement;
			this.m_XAdvance = xAdvance;
			this.m_YAdvance = yAdvance;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000035DC File Offset: 0x000017DC
		public static GlyphValueRecord operator +(GlyphValueRecord a, GlyphValueRecord b)
		{
			GlyphValueRecord c;
			c.m_XPlacement = a.xPlacement + b.xPlacement;
			c.m_YPlacement = a.yPlacement + b.yPlacement;
			c.m_XAdvance = a.xAdvance + b.xAdvance;
			c.m_YAdvance = a.yAdvance + b.yAdvance;
			return c;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003648 File Offset: 0x00001848
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000366C File Offset: 0x0000186C
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003690 File Offset: 0x00001890
		public bool Equals(GlyphValueRecord other)
		{
			return base.Equals(other);
		}

		// Token: 0x04000074 RID: 116
		[SerializeField]
		[NativeName("xPlacement")]
		private float m_XPlacement;

		// Token: 0x04000075 RID: 117
		[NativeName("yPlacement")]
		[SerializeField]
		private float m_YPlacement;

		// Token: 0x04000076 RID: 118
		[NativeName("xAdvance")]
		[SerializeField]
		private float m_XAdvance;

		// Token: 0x04000077 RID: 119
		[SerializeField]
		[NativeName("yAdvance")]
		private float m_YAdvance;
	}
}
