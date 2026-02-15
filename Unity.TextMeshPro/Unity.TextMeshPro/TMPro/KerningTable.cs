using System;
using System.Collections.Generic;
using System.Linq;

namespace TMPro
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	public class KerningTable
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000906B File Offset: 0x0000726B
		public KerningTable()
		{
			this.kerningPairs = new List<KerningPair>();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009080 File Offset: 0x00007280
		public void AddKerningPair()
		{
			if (this.kerningPairs.Count == 0)
			{
				this.kerningPairs.Add(new KerningPair(0U, 0U, 0f));
				return;
			}
			uint left = this.kerningPairs.Last<KerningPair>().firstGlyph;
			uint right = this.kerningPairs.Last<KerningPair>().secondGlyph;
			float xoffset = this.kerningPairs.Last<KerningPair>().xOffset;
			this.kerningPairs.Add(new KerningPair(left, right, xoffset));
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000090F8 File Offset: 0x000072F8
		public int AddKerningPair(uint first, uint second, float offset)
		{
			if (this.kerningPairs.FindIndex((KerningPair item) => item.firstGlyph == first && item.secondGlyph == second) == -1)
			{
				this.kerningPairs.Add(new KerningPair(first, second, offset));
				return 0;
			}
			return -1;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009154 File Offset: 0x00007354
		public int AddGlyphPairAdjustmentRecord(uint first, GlyphValueRecord_Legacy firstAdjustments, uint second, GlyphValueRecord_Legacy secondAdjustments)
		{
			if (this.kerningPairs.FindIndex((KerningPair item) => item.firstGlyph == first && item.secondGlyph == second) == -1)
			{
				this.kerningPairs.Add(new KerningPair(first, firstAdjustments, second, secondAdjustments));
				return 0;
			}
			return -1;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000091B4 File Offset: 0x000073B4
		public void RemoveKerningPair(int left, int right)
		{
			int index = this.kerningPairs.FindIndex((KerningPair item) => (ulong)item.firstGlyph == (ulong)((long)left) && (ulong)item.secondGlyph == (ulong)((long)right));
			if (index != -1)
			{
				this.kerningPairs.RemoveAt(index);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000091FD File Offset: 0x000073FD
		public void RemoveKerningPair(int index)
		{
			this.kerningPairs.RemoveAt(index);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000920C File Offset: 0x0000740C
		public void SortKerningPairs()
		{
			if (this.kerningPairs.Count > 0)
			{
				this.kerningPairs = (from s in this.kerningPairs
					orderby s.firstGlyph, s.secondGlyph
					select s).ToList<KerningPair>();
			}
		}

		// Token: 0x0400015B RID: 347
		public List<KerningPair> kerningPairs;
	}
}
