using System;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x02000298 RID: 664
	internal class UIRAtlasAllocator : IDisposable
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x0004AE22 File Offset: 0x00049022
		public int maxAtlasSize { get; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0004AE2A File Offset: 0x0004902A
		public int maxImageWidth { get; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x0004AE32 File Offset: 0x00049032
		public int maxImageHeight { get; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x0004AE3A File Offset: 0x0004903A
		// (set) Token: 0x060011ED RID: 4589 RVA: 0x0004AE42 File Offset: 0x00049042
		public int virtualWidth { get; private set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0004AE4B File Offset: 0x0004904B
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x0004AE53 File Offset: 0x00049053
		public int virtualHeight { get; private set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0004AE5C File Offset: 0x0004905C
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x0004AE64 File Offset: 0x00049064
		public int physicalWidth { get; private set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0004AE6D File Offset: 0x0004906D
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x0004AE75 File Offset: 0x00049075
		public int physicalHeight { get; private set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0004AE7E File Offset: 0x0004907E
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x0004AE86 File Offset: 0x00049086
		private protected bool disposed { protected get; private set; }

		// Token: 0x060011F6 RID: 4598 RVA: 0x0004AE8F File Offset: 0x0004908F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0004AEA4 File Offset: 0x000490A4
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					for (int i = 0; i < this.m_OpenRows.Length; i++)
					{
						UIRAtlasAllocator.Row row = this.m_OpenRows[i];
						bool flag = row != null;
						if (flag)
						{
							row.Release();
						}
					}
					this.m_OpenRows = null;
					UIRAtlasAllocator.AreaNode temp;
					for (UIRAtlasAllocator.AreaNode area = this.m_FirstUnpartitionedArea; area != null; area = temp)
					{
						temp = area.next;
						area.Release();
					}
					this.m_FirstUnpartitionedArea = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0004AF3C File Offset: 0x0004913C
		private static int GetLog2OfNextPower(int n)
		{
			float next = (float)Mathf.NextPowerOfTwo(n);
			float pow = Mathf.Log(next, 2f);
			return Mathf.RoundToInt(pow);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0004AF68 File Offset: 0x00049168
		public UIRAtlasAllocator(int initialAtlasSize, int maxAtlasSize, int sidePadding = 1)
		{
			Assert.IsTrue(initialAtlasSize > 0 && initialAtlasSize <= maxAtlasSize);
			Assert.IsTrue(initialAtlasSize == Mathf.NextPowerOfTwo(initialAtlasSize));
			Assert.IsTrue(maxAtlasSize == Mathf.NextPowerOfTwo(maxAtlasSize));
			this.m_1SidePadding = sidePadding;
			this.m_2SidePadding = sidePadding << 1;
			this.maxAtlasSize = maxAtlasSize;
			this.maxImageWidth = maxAtlasSize;
			this.maxImageHeight = ((initialAtlasSize == maxAtlasSize) ? (maxAtlasSize / 2 + this.m_2SidePadding) : (maxAtlasSize / 4 + this.m_2SidePadding));
			this.virtualWidth = initialAtlasSize;
			this.virtualHeight = initialAtlasSize;
			int maxOpenRows = UIRAtlasAllocator.GetLog2OfNextPower(maxAtlasSize) + 1;
			this.m_OpenRows = new UIRAtlasAllocator.Row[maxOpenRows];
			RectInt area = new RectInt(0, 0, initialAtlasSize, initialAtlasSize);
			this.m_FirstUnpartitionedArea = UIRAtlasAllocator.AreaNode.Acquire(area);
			this.BuildAreas();
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0004B030 File Offset: 0x00049230
		public bool TryAllocate(int width, int height, out RectInt location)
		{
			bool flag;
			using (UIRAtlasAllocator.s_MarkerTryAllocate.Auto())
			{
				location = default(RectInt);
				bool disposed = this.disposed;
				if (disposed)
				{
					flag = false;
				}
				else
				{
					bool flag2 = width < 1 || height < 1;
					if (flag2)
					{
						flag = false;
					}
					else
					{
						bool flag3 = width > this.maxImageWidth || height > this.maxImageHeight;
						if (flag3)
						{
							flag = false;
						}
						else
						{
							int rowIndex = UIRAtlasAllocator.GetLog2OfNextPower(Mathf.Max(height - this.m_2SidePadding, 1));
							int rowHeight = (1 << rowIndex) + this.m_2SidePadding;
							UIRAtlasAllocator.Row row = this.m_OpenRows[rowIndex];
							bool flag4 = row != null && row.width - row.Cursor < width;
							if (flag4)
							{
								row = null;
							}
							bool flag5 = row == null;
							if (flag5)
							{
								for (UIRAtlasAllocator.AreaNode areaNode = this.m_FirstUnpartitionedArea; areaNode != null; areaNode = areaNode.next)
								{
									bool flag6 = this.TryPartitionArea(areaNode, rowIndex, rowHeight, width);
									if (flag6)
									{
										row = this.m_OpenRows[rowIndex];
										break;
									}
								}
								bool flag7 = row == null;
								if (flag7)
								{
									return false;
								}
							}
							location = new RectInt(row.offsetX + row.Cursor, row.offsetY, width, height);
							row.Cursor += width;
							Assert.IsTrue(row.Cursor <= row.width);
							this.physicalWidth = Mathf.NextPowerOfTwo(Mathf.Max(this.physicalWidth, location.xMax));
							this.physicalHeight = Mathf.NextPowerOfTwo(Mathf.Max(this.physicalHeight, location.yMax));
							flag = true;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0004B1F8 File Offset: 0x000493F8
		private bool TryPartitionArea(UIRAtlasAllocator.AreaNode areaNode, int rowIndex, int rowHeight, int minWidth)
		{
			RectInt area = areaNode.rect;
			bool flag = area.height < rowHeight || area.width < minWidth;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				UIRAtlasAllocator.Row row = this.m_OpenRows[rowIndex];
				bool flag3 = row != null;
				if (flag3)
				{
					row.Release();
				}
				row = UIRAtlasAllocator.Row.Acquire(area.x, area.y, area.width, rowHeight);
				this.m_OpenRows[rowIndex] = row;
				area.y += rowHeight;
				area.height -= rowHeight;
				bool flag4 = area.height == 0;
				if (flag4)
				{
					bool flag5 = areaNode == this.m_FirstUnpartitionedArea;
					if (flag5)
					{
						this.m_FirstUnpartitionedArea = areaNode.next;
					}
					areaNode.RemoveFromChain();
					areaNode.Release();
				}
				else
				{
					areaNode.rect = area;
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0004B2D8 File Offset: 0x000494D8
		private void BuildAreas()
		{
			UIRAtlasAllocator.AreaNode current = this.m_FirstUnpartitionedArea;
			while (this.virtualWidth < this.maxAtlasSize || this.virtualHeight < this.maxAtlasSize)
			{
				bool flag = this.virtualWidth > this.virtualHeight;
				RectInt newArea;
				if (flag)
				{
					newArea = new RectInt(0, this.virtualHeight, this.virtualWidth, this.virtualHeight);
					this.virtualHeight *= 2;
				}
				else
				{
					newArea = new RectInt(this.virtualWidth, 0, this.virtualWidth, this.virtualHeight);
					this.virtualWidth *= 2;
				}
				UIRAtlasAllocator.AreaNode newAreaNode = UIRAtlasAllocator.AreaNode.Acquire(newArea);
				newAreaNode.AddAfter(current);
				current = newAreaNode;
			}
		}

		// Token: 0x04000A56 RID: 2646
		private UIRAtlasAllocator.AreaNode m_FirstUnpartitionedArea;

		// Token: 0x04000A57 RID: 2647
		private UIRAtlasAllocator.Row[] m_OpenRows;

		// Token: 0x04000A58 RID: 2648
		private int m_1SidePadding;

		// Token: 0x04000A59 RID: 2649
		private int m_2SidePadding;

		// Token: 0x04000A5A RID: 2650
		private static ProfilerMarker s_MarkerTryAllocate = new ProfilerMarker("UIRAtlasAllocator.TryAllocate");

		// Token: 0x02000299 RID: 665
		private class Row
		{
			// Token: 0x17000377 RID: 887
			// (get) Token: 0x060011FE RID: 4606 RVA: 0x0004B3A5 File Offset: 0x000495A5
			// (set) Token: 0x060011FF RID: 4607 RVA: 0x0004B3AD File Offset: 0x000495AD
			public int offsetX { get; private set; }

			// Token: 0x17000378 RID: 888
			// (get) Token: 0x06001200 RID: 4608 RVA: 0x0004B3B6 File Offset: 0x000495B6
			// (set) Token: 0x06001201 RID: 4609 RVA: 0x0004B3BE File Offset: 0x000495BE
			public int offsetY { get; private set; }

			// Token: 0x17000379 RID: 889
			// (get) Token: 0x06001202 RID: 4610 RVA: 0x0004B3C7 File Offset: 0x000495C7
			// (set) Token: 0x06001203 RID: 4611 RVA: 0x0004B3CF File Offset: 0x000495CF
			public int width { get; private set; }

			// Token: 0x1700037A RID: 890
			// (set) Token: 0x06001204 RID: 4612 RVA: 0x0004B3D8 File Offset: 0x000495D8
			private int height
			{
				[CompilerGenerated]
				set
				{
					this.<height>k__BackingField = value;
				}
			}

			// Token: 0x06001205 RID: 4613 RVA: 0x0004B3E4 File Offset: 0x000495E4
			public static UIRAtlasAllocator.Row Acquire(int offsetX, int offsetY, int width, int height)
			{
				UIRAtlasAllocator.Row row = UIRAtlasAllocator.Row.s_Pool.Get();
				row.offsetX = offsetX;
				row.offsetY = offsetY;
				row.width = width;
				row.height = height;
				row.Cursor = 0;
				return row;
			}

			// Token: 0x06001206 RID: 4614 RVA: 0x0004B429 File Offset: 0x00049629
			public void Release()
			{
				UIRAtlasAllocator.Row.s_Pool.Release(this);
				this.offsetX = -1;
				this.offsetY = -1;
				this.width = -1;
				this.height = -1;
				this.Cursor = -1;
			}

			// Token: 0x04000A5C RID: 2652
			private static ObjectPool<UIRAtlasAllocator.Row> s_Pool = new ObjectPool<UIRAtlasAllocator.Row>(() => new UIRAtlasAllocator.Row(), 100);

			// Token: 0x04000A61 RID: 2657
			public int Cursor;
		}

		// Token: 0x0200029B RID: 667
		private class AreaNode
		{
			// Token: 0x0600120C RID: 4620 RVA: 0x0004B490 File Offset: 0x00049690
			public static UIRAtlasAllocator.AreaNode Acquire(RectInt rect)
			{
				UIRAtlasAllocator.AreaNode node = UIRAtlasAllocator.AreaNode.s_Pool.Get();
				node.rect = rect;
				node.previous = null;
				node.next = null;
				return node;
			}

			// Token: 0x0600120D RID: 4621 RVA: 0x0004B4C3 File Offset: 0x000496C3
			public void Release()
			{
				UIRAtlasAllocator.AreaNode.s_Pool.Release(this);
			}

			// Token: 0x0600120E RID: 4622 RVA: 0x0004B4D4 File Offset: 0x000496D4
			public void RemoveFromChain()
			{
				bool flag = this.previous != null;
				if (flag)
				{
					this.previous.next = this.next;
				}
				bool flag2 = this.next != null;
				if (flag2)
				{
					this.next.previous = this.previous;
				}
				this.previous = null;
				this.next = null;
			}

			// Token: 0x0600120F RID: 4623 RVA: 0x0004B52C File Offset: 0x0004972C
			public void AddAfter(UIRAtlasAllocator.AreaNode previous)
			{
				Assert.IsNull<UIRAtlasAllocator.AreaNode>(this.previous);
				Assert.IsNull<UIRAtlasAllocator.AreaNode>(this.next);
				this.previous = previous;
				bool flag = previous != null;
				if (flag)
				{
					this.next = previous.next;
					previous.next = this;
				}
				bool flag2 = this.next != null;
				if (flag2)
				{
					this.next.previous = this;
				}
			}

			// Token: 0x04000A63 RID: 2659
			private static ObjectPool<UIRAtlasAllocator.AreaNode> s_Pool = new ObjectPool<UIRAtlasAllocator.AreaNode>(() => new UIRAtlasAllocator.AreaNode(), 100);

			// Token: 0x04000A64 RID: 2660
			public RectInt rect;

			// Token: 0x04000A65 RID: 2661
			public UIRAtlasAllocator.AreaNode previous;

			// Token: 0x04000A66 RID: 2662
			public UIRAtlasAllocator.AreaNode next;
		}
	}
}
