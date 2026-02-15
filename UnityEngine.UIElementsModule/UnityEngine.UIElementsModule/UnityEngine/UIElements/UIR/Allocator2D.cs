using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x020004FD RID: 1277
	internal class Allocator2D
	{
		// Token: 0x060023B9 RID: 9145 RVA: 0x00082FF0 File Offset: 0x000811F0
		public Allocator2D(Vector2Int minSize, Vector2Int maxSize, int rowHeightBias)
		{
			Debug.Assert(minSize.x > 0 && minSize.x <= maxSize.x && minSize.y > 0 && minSize.y <= maxSize.y);
			Debug.Assert(minSize.x == UIRUtility.GetNextPow2(minSize.x) && minSize.y == UIRUtility.GetNextPow2(minSize.y) && maxSize.x == UIRUtility.GetNextPow2(maxSize.x) && maxSize.y == UIRUtility.GetNextPow2(maxSize.y));
			Debug.Assert(rowHeightBias >= 0);
			this.m_MinSize = minSize;
			this.m_MaxSize = maxSize;
			this.m_RowHeightBias = rowHeightBias;
			Allocator2D.BuildAreas(this.m_Areas, minSize, maxSize);
			this.m_MaxAllocSize = Allocator2D.ComputeMaxAllocSize(this.m_Areas, rowHeightBias);
			this.m_Rows = Allocator2D.BuildRowArray(this.m_MaxAllocSize.y, rowHeightBias);
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x00083108 File Offset: 0x00081308
		public bool TryAllocate(int width, int height, out Allocator2D.Alloc2D alloc2D)
		{
			bool flag = width < 1 || width > this.m_MaxAllocSize.x || height < 1 || height > this.m_MaxAllocSize.y;
			bool flag2;
			if (flag)
			{
				alloc2D = default(Allocator2D.Alloc2D);
				flag2 = false;
			}
			else
			{
				int i = UIRUtility.GetNextPow2Exp(Mathf.Max(height - this.m_RowHeightBias, 1));
				for (Allocator2D.Row row = this.m_Rows[i]; row != null; row = row.next)
				{
					bool flag3 = row.rect.width >= width;
					if (flag3)
					{
						Alloc alloc = row.allocator.Allocate((uint)width);
						bool flag4 = alloc.size > 0U;
						if (flag4)
						{
							alloc2D = new Allocator2D.Alloc2D(row, alloc, width, height);
							return true;
						}
					}
				}
				int rowHeight = (1 << i) + this.m_RowHeightBias;
				Debug.Assert(rowHeight >= height);
				for (int j = 0; j < this.m_Areas.Count; j++)
				{
					Allocator2D.Area area = this.m_Areas[j];
					bool flag5 = area.rect.height >= rowHeight && area.rect.width >= width;
					if (flag5)
					{
						Alloc rowAlloc = area.allocator.Allocate((uint)rowHeight);
						bool flag6 = rowAlloc.size > 0U;
						if (flag6)
						{
							Allocator2D.Row row = Allocator2D.Row.pool.Get();
							row.alloc = rowAlloc;
							row.allocator = new BestFitAllocator((uint)area.rect.width);
							row.area = area;
							row.next = this.m_Rows[i];
							row.rect = new RectInt(area.rect.xMin, area.rect.yMin + (int)rowAlloc.start, area.rect.width, rowHeight);
							this.m_Rows[i] = row;
							Alloc alloc2 = row.allocator.Allocate((uint)width);
							Debug.Assert(alloc2.size > 0U);
							alloc2D = new Allocator2D.Alloc2D(row, alloc2, width, height);
							return true;
						}
					}
				}
				alloc2D = default(Allocator2D.Alloc2D);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x00083344 File Offset: 0x00081544
		public void Free(Allocator2D.Alloc2D alloc2D)
		{
			bool flag = alloc2D.alloc.size == 0U;
			if (!flag)
			{
				Allocator2D.Row row = alloc2D.row;
				row.allocator.Free(alloc2D.alloc);
				bool flag2 = row.allocator.highWatermark == 0U;
				if (flag2)
				{
					row.area.allocator.Free(row.alloc);
					int i = UIRUtility.GetNextPow2Exp(row.rect.height - this.m_RowHeightBias);
					Allocator2D.Row first = this.m_Rows[i];
					bool flag3 = first == row;
					if (flag3)
					{
						this.m_Rows[i] = row.next;
					}
					else
					{
						Allocator2D.Row prev = first;
						while (prev.next != row)
						{
							prev = prev.next;
						}
						prev.next = row.next;
					}
					Allocator2D.Row.pool.Return(row);
				}
			}
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x0008342C File Offset: 0x0008162C
		private static void BuildAreas(List<Allocator2D.Area> areas, Vector2Int minSize, Vector2Int maxSize)
		{
			int xMax = Mathf.Min(minSize.x, minSize.y);
			int yMax = xMax;
			areas.Add(new Allocator2D.Area(new RectInt(0, 0, xMax, yMax)));
			while (xMax < maxSize.x || yMax < maxSize.y)
			{
				bool flag = xMax < maxSize.x;
				if (flag)
				{
					areas.Add(new Allocator2D.Area(new RectInt(xMax, 0, xMax, yMax)));
					xMax *= 2;
				}
				bool flag2 = yMax < maxSize.y;
				if (flag2)
				{
					areas.Add(new Allocator2D.Area(new RectInt(0, yMax, xMax, yMax)));
					yMax *= 2;
				}
			}
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000834D8 File Offset: 0x000816D8
		private static Vector2Int ComputeMaxAllocSize(List<Allocator2D.Area> areas, int rowHeightBias)
		{
			int maxWidth = 0;
			int maxHeight = 0;
			for (int i = 0; i < areas.Count; i++)
			{
				Allocator2D.Area area = areas[i];
				maxWidth = Mathf.Max(area.rect.width, maxWidth);
				maxHeight = Mathf.Max(area.rect.height, maxHeight);
			}
			return new Vector2Int(maxWidth, UIRUtility.GetPrevPow2(maxHeight - rowHeightBias) + rowHeightBias);
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x00083548 File Offset: 0x00081748
		private static Allocator2D.Row[] BuildRowArray(int maxRowHeight, int rowHeightBias)
		{
			int i = UIRUtility.GetNextPow2Exp(maxRowHeight - rowHeightBias) + 1;
			return new Allocator2D.Row[i];
		}

		// Token: 0x04001043 RID: 4163
		private readonly Vector2Int m_MinSize;

		// Token: 0x04001044 RID: 4164
		private readonly Vector2Int m_MaxSize;

		// Token: 0x04001045 RID: 4165
		private readonly Vector2Int m_MaxAllocSize;

		// Token: 0x04001046 RID: 4166
		private readonly int m_RowHeightBias;

		// Token: 0x04001047 RID: 4167
		private readonly Allocator2D.Row[] m_Rows;

		// Token: 0x04001048 RID: 4168
		private readonly List<Allocator2D.Area> m_Areas = new List<Allocator2D.Area>();

		// Token: 0x020004FE RID: 1278
		public class Area
		{
			// Token: 0x060023BF RID: 9151 RVA: 0x0008356B File Offset: 0x0008176B
			public Area(RectInt rect)
			{
				this.rect = rect;
				this.allocator = new BestFitAllocator((uint)rect.height);
			}

			// Token: 0x04001049 RID: 4169
			public RectInt rect;

			// Token: 0x0400104A RID: 4170
			public BestFitAllocator allocator;
		}

		// Token: 0x020004FF RID: 1279
		public class Row : LinkedPoolItem<Allocator2D.Row>
		{
			// Token: 0x060023C0 RID: 9152 RVA: 0x0008358E File Offset: 0x0008178E
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static Allocator2D.Row Create()
			{
				return new Allocator2D.Row();
			}

			// Token: 0x060023C1 RID: 9153 RVA: 0x00083595 File Offset: 0x00081795
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void Reset(Allocator2D.Row row)
			{
				row.rect = default(RectInt);
				row.area = null;
				row.allocator = null;
				row.alloc = default(Alloc);
				row.next = null;
			}

			// Token: 0x0400104B RID: 4171
			public RectInt rect;

			// Token: 0x0400104C RID: 4172
			public Allocator2D.Area area;

			// Token: 0x0400104D RID: 4173
			public BestFitAllocator allocator;

			// Token: 0x0400104E RID: 4174
			public Alloc alloc;

			// Token: 0x0400104F RID: 4175
			public Allocator2D.Row next;

			// Token: 0x04001050 RID: 4176
			public static readonly LinkedPool<Allocator2D.Row> pool = new LinkedPool<Allocator2D.Row>(new Func<Allocator2D.Row>(Allocator2D.Row.Create), new Action<Allocator2D.Row>(Allocator2D.Row.Reset), 256);
		}

		// Token: 0x02000500 RID: 1280
		public struct Alloc2D
		{
			// Token: 0x060023C4 RID: 9156 RVA: 0x000835F7 File Offset: 0x000817F7
			public Alloc2D(Allocator2D.Row row, Alloc alloc, int width, int height)
			{
				this.alloc = alloc;
				this.row = row;
				this.rect = new RectInt(row.rect.xMin + (int)alloc.start, row.rect.yMin, width, height);
			}

			// Token: 0x04001051 RID: 4177
			public RectInt rect;

			// Token: 0x04001052 RID: 4178
			public Allocator2D.Row row;

			// Token: 0x04001053 RID: 4179
			public Alloc alloc;
		}
	}
}
