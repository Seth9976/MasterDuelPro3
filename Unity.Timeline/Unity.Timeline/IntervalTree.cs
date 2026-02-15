using System;
using System.Collections.Generic;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003B RID: 59
	internal class IntervalTree<T> where T : IInterval
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000812C File Offset: 0x0000632C
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00008134 File Offset: 0x00006334
		public bool dirty { get; internal set; }

		// Token: 0x06000245 RID: 581 RVA: 0x00008140 File Offset: 0x00006340
		public void Add(T item)
		{
			if (item == null)
			{
				return;
			}
			this.m_Entries.Add(new IntervalTree<T>.Entry
			{
				intervalStart = item.intervalStart,
				intervalEnd = item.intervalEnd,
				item = item
			});
			this.dirty = true;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000081A4 File Offset: 0x000063A4
		public void IntersectsWith(long value, List<T> results)
		{
			if (this.m_Entries.Count == 0)
			{
				return;
			}
			if (this.dirty)
			{
				this.Rebuild();
				this.dirty = false;
			}
			if (this.m_Nodes.Count > 0)
			{
				this.Query(this.m_Nodes[0], value, results);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000081F8 File Offset: 0x000063F8
		public void IntersectsWithRange(long start, long end, List<T> results)
		{
			if (start > end)
			{
				return;
			}
			if (this.m_Entries.Count == 0)
			{
				return;
			}
			if (this.dirty)
			{
				this.Rebuild();
				this.dirty = false;
			}
			if (this.m_Nodes.Count > 0)
			{
				this.QueryRange(this.m_Nodes[0], start, end, results);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00008250 File Offset: 0x00006450
		public void UpdateIntervals()
		{
			bool isDirty = false;
			for (int i = 0; i < this.m_Entries.Count; i++)
			{
				IntervalTree<T>.Entry j = this.m_Entries[i];
				long s = j.item.intervalStart;
				long e = j.item.intervalEnd;
				isDirty |= j.intervalStart != s;
				isDirty |= j.intervalEnd != e;
				this.m_Entries[i] = new IntervalTree<T>.Entry
				{
					intervalStart = s,
					intervalEnd = e,
					item = j.item
				};
			}
			this.dirty = this.dirty || isDirty;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008310 File Offset: 0x00006510
		private void Query(IntervalTreeNode intervalTreeNode, long value, List<T> results)
		{
			for (int i = intervalTreeNode.first; i <= intervalTreeNode.last; i++)
			{
				IntervalTree<T>.Entry entry = this.m_Entries[i];
				if (value >= entry.intervalStart && value < entry.intervalEnd)
				{
					results.Add(entry.item);
				}
			}
			if (intervalTreeNode.center == 9223372036854775807L)
			{
				return;
			}
			if (intervalTreeNode.left != -1 && value < intervalTreeNode.center)
			{
				this.Query(this.m_Nodes[intervalTreeNode.left], value, results);
			}
			if (intervalTreeNode.right != -1 && value > intervalTreeNode.center)
			{
				this.Query(this.m_Nodes[intervalTreeNode.right], value, results);
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000083C8 File Offset: 0x000065C8
		private void QueryRange(IntervalTreeNode intervalTreeNode, long start, long end, List<T> results)
		{
			for (int i = intervalTreeNode.first; i <= intervalTreeNode.last; i++)
			{
				IntervalTree<T>.Entry entry = this.m_Entries[i];
				if (end >= entry.intervalStart && start < entry.intervalEnd)
				{
					results.Add(entry.item);
				}
			}
			if (intervalTreeNode.center == 9223372036854775807L)
			{
				return;
			}
			if (intervalTreeNode.left != -1 && start < intervalTreeNode.center)
			{
				this.QueryRange(this.m_Nodes[intervalTreeNode.left], start, end, results);
			}
			if (intervalTreeNode.right != -1 && end > intervalTreeNode.center)
			{
				this.QueryRange(this.m_Nodes[intervalTreeNode.right], start, end, results);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008483 File Offset: 0x00006683
		private void Rebuild()
		{
			this.m_Nodes.Clear();
			this.m_Nodes.Capacity = this.m_Entries.Capacity;
			this.Rebuild(0, this.m_Entries.Count - 1);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000084BC File Offset: 0x000066BC
		private int Rebuild(int start, int end)
		{
			IntervalTreeNode intervalTreeNode = default(IntervalTreeNode);
			if (end - start + 1 < 10)
			{
				intervalTreeNode = new IntervalTreeNode
				{
					center = long.MaxValue,
					first = start,
					last = end,
					left = -1,
					right = -1
				};
				this.m_Nodes.Add(intervalTreeNode);
				return this.m_Nodes.Count - 1;
			}
			long min = long.MaxValue;
			long max = long.MinValue;
			for (int i = start; i <= end; i++)
			{
				IntervalTree<T>.Entry o = this.m_Entries[i];
				min = Math.Min(min, o.intervalStart);
				max = Math.Max(max, o.intervalEnd);
			}
			long center = (max + min) / 2L;
			intervalTreeNode.center = center;
			int x = start;
			int y = end;
			for (;;)
			{
				if (x <= end)
				{
					if (this.m_Entries[x].intervalEnd < center)
					{
						x++;
						continue;
					}
				}
				while (y >= start && this.m_Entries[y].intervalEnd >= center)
				{
					y--;
				}
				if (x > y)
				{
					break;
				}
				IntervalTree<T>.Entry nodeX = this.m_Entries[x];
				IntervalTree<T>.Entry nodeY = this.m_Entries[y];
				this.m_Entries[y] = nodeX;
				this.m_Entries[x] = nodeY;
			}
			intervalTreeNode.first = x;
			y = end;
			for (;;)
			{
				if (x <= end)
				{
					if (this.m_Entries[x].intervalStart <= center)
					{
						x++;
						continue;
					}
				}
				while (y >= start && this.m_Entries[y].intervalStart > center)
				{
					y--;
				}
				if (x > y)
				{
					break;
				}
				IntervalTree<T>.Entry nodeX2 = this.m_Entries[x];
				IntervalTree<T>.Entry nodeY2 = this.m_Entries[y];
				this.m_Entries[y] = nodeX2;
				this.m_Entries[x] = nodeY2;
			}
			intervalTreeNode.last = y;
			this.m_Nodes.Add(default(IntervalTreeNode));
			int index = this.m_Nodes.Count - 1;
			intervalTreeNode.left = -1;
			intervalTreeNode.right = -1;
			if (start < intervalTreeNode.first)
			{
				intervalTreeNode.left = this.Rebuild(start, intervalTreeNode.first - 1);
			}
			if (end > intervalTreeNode.last)
			{
				intervalTreeNode.right = this.Rebuild(intervalTreeNode.last + 1, end);
			}
			this.m_Nodes[index] = intervalTreeNode;
			return index;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000873A File Offset: 0x0000693A
		public void Clear()
		{
			this.m_Entries.Clear();
			this.m_Nodes.Clear();
		}

		// Token: 0x04000114 RID: 276
		private const int kMinNodeSize = 10;

		// Token: 0x04000115 RID: 277
		private const int kInvalidNode = -1;

		// Token: 0x04000116 RID: 278
		private const long kCenterUnknown = 9223372036854775807L;

		// Token: 0x04000117 RID: 279
		private readonly List<IntervalTree<T>.Entry> m_Entries = new List<IntervalTree<T>.Entry>();

		// Token: 0x04000118 RID: 280
		private readonly List<IntervalTreeNode> m_Nodes = new List<IntervalTreeNode>();

		// Token: 0x0200003C RID: 60
		internal struct Entry
		{
			// Token: 0x0400011A RID: 282
			public long intervalStart;

			// Token: 0x0400011B RID: 283
			public long intervalEnd;

			// Token: 0x0400011C RID: 284
			public T item;
		}
	}
}
