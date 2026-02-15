using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000569 RID: 1385
	internal struct BitmapAllocator32
	{
		// Token: 0x06002601 RID: 9729 RVA: 0x000973E0 File Offset: 0x000955E0
		public void Construct(int pageHeight, int entryWidth = 1, int entryHeight = 1)
		{
			this.m_PageHeight = pageHeight;
			this.m_Pages = new List<BitmapAllocator32.Page>(1);
			this.m_AllocMap = new List<uint>(this.m_PageHeight * this.m_Pages.Capacity);
			this.m_EntryWidth = entryWidth;
			this.m_EntryHeight = entryHeight;
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x0009742C File Offset: 0x0009562C
		public void ForceFirstAlloc(ushort firstPageX, ushort firstPageY)
		{
			this.m_AllocMap.Add(4294967294U);
			for (int i = 1; i < this.m_PageHeight; i++)
			{
				this.m_AllocMap.Add(uint.MaxValue);
			}
			this.m_Pages.Add(new BitmapAllocator32.Page
			{
				x = firstPageX,
				y = firstPageY,
				freeSlots = 32 * this.m_PageHeight - 1
			});
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x000974A4 File Offset: 0x000956A4
		public BMPAlloc Allocate(BaseShaderInfoStorage storage)
		{
			int pageCount = this.m_Pages.Count;
			for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
			{
				BitmapAllocator32.Page pageInfo = this.m_Pages[pageIndex];
				bool flag = pageInfo.freeSlots == 0;
				if (!flag)
				{
					int line = pageIndex * this.m_PageHeight;
					int endLine = line + this.m_PageHeight;
					while (line < endLine)
					{
						uint allocBits = this.m_AllocMap[line];
						bool flag2 = allocBits == 0U;
						if (!flag2)
						{
							byte allocIndex = BitmapAllocator32.CountTrailingZeroes(allocBits);
							this.m_AllocMap[line] = allocBits & ~(1U << (int)allocIndex);
							pageInfo.freeSlots--;
							this.m_Pages[pageIndex] = pageInfo;
							return new BMPAlloc
							{
								page = pageIndex,
								pageLine = (ushort)(line - pageIndex * this.m_PageHeight),
								bitIndex = allocIndex,
								ownedState = OwnedState.Owned
							};
						}
						line++;
					}
				}
			}
			RectInt uvRect;
			bool flag3 = storage == null || !storage.AllocateRect(32 * this.m_EntryWidth, this.m_PageHeight * this.m_EntryHeight, out uvRect);
			if (flag3)
			{
				return BMPAlloc.Invalid;
			}
			this.m_AllocMap.Capacity += this.m_PageHeight;
			this.m_AllocMap.Add(4294967294U);
			for (int i = 1; i < this.m_PageHeight; i++)
			{
				this.m_AllocMap.Add(uint.MaxValue);
			}
			this.m_Pages.Add(new BitmapAllocator32.Page
			{
				x = (ushort)uvRect.xMin,
				y = (ushort)uvRect.yMin,
				freeSlots = 32 * this.m_PageHeight - 1
			});
			return new BMPAlloc
			{
				page = this.m_Pages.Count - 1,
				ownedState = OwnedState.Owned
			};
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000976B8 File Offset: 0x000958B8
		public void Free(BMPAlloc alloc)
		{
			Debug.Assert(alloc.ownedState == OwnedState.Owned);
			int line = alloc.page * this.m_PageHeight + (int)alloc.pageLine;
			this.m_AllocMap[line] = this.m_AllocMap[line] | (1U << (int)alloc.bitIndex);
			BitmapAllocator32.Page page = this.m_Pages[alloc.page];
			page.freeSlots++;
			this.m_Pages[alloc.page] = page;
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x00097740 File Offset: 0x00095940
		public int entryWidth
		{
			get
			{
				return this.m_EntryWidth;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x00097758 File Offset: 0x00095958
		public int entryHeight
		{
			get
			{
				return this.m_EntryHeight;
			}
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x00097770 File Offset: 0x00095970
		internal void GetAllocPageAtlasLocation(int page, out ushort x, out ushort y)
		{
			BitmapAllocator32.Page p = this.m_Pages[page];
			x = p.x;
			y = p.y;
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x0009779C File Offset: 0x0009599C
		private static byte CountTrailingZeroes(uint val)
		{
			byte trailingZeroes = 0;
			bool flag = (val & 65535U) == 0U;
			if (flag)
			{
				val >>= 16;
				trailingZeroes = 16;
			}
			bool flag2 = (val & 255U) == 0U;
			if (flag2)
			{
				val >>= 8;
				trailingZeroes += 8;
			}
			bool flag3 = (val & 15U) == 0U;
			if (flag3)
			{
				val >>= 4;
				trailingZeroes += 4;
			}
			bool flag4 = (val & 3U) == 0U;
			if (flag4)
			{
				val >>= 2;
				trailingZeroes += 2;
			}
			bool flag5 = (val & 1U) == 0U;
			if (flag5)
			{
				trailingZeroes += 1;
			}
			return trailingZeroes;
		}

		// Token: 0x04001336 RID: 4918
		private int m_PageHeight;

		// Token: 0x04001337 RID: 4919
		private List<BitmapAllocator32.Page> m_Pages;

		// Token: 0x04001338 RID: 4920
		private List<uint> m_AllocMap;

		// Token: 0x04001339 RID: 4921
		private int m_EntryWidth;

		// Token: 0x0400133A RID: 4922
		private int m_EntryHeight;

		// Token: 0x0200056A RID: 1386
		private struct Page
		{
			// Token: 0x0400133B RID: 4923
			public ushort x;

			// Token: 0x0400133C RID: 4924
			public ushort y;

			// Token: 0x0400133D RID: 4925
			public int freeSlots;
		}
	}
}
