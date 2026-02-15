using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x0200008B RID: 139
	internal abstract class RBTree<K> : IEnumerable
	{
		// Token: 0x060006F3 RID: 1779
		protected abstract int CompareNode(K record1, K record2);

		// Token: 0x060006F4 RID: 1780
		protected abstract int CompareSateliteTreeNode(K record1, K record2);

		// Token: 0x060006F5 RID: 1781 RVA: 0x00022723 File Offset: 0x00020923
		protected RBTree(TreeAccessMethod accessMethod)
		{
			this._accessMethod = accessMethod;
			this.InitTree();
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00022738 File Offset: 0x00020938
		private void InitTree()
		{
			this.root = 0;
			this._pageTable = new RBTree<K>.TreePage[32];
			this._pageTableMap = new int[(this._pageTable.Length + 32 - 1) / 32];
			this._inUsePageCount = 0;
			this._nextFreePageLine = 0;
			this.AllocPage(32);
			this._pageTable[0]._slots[0]._nodeColor = RBTree<K>.NodeColor.black;
			this._pageTable[0]._slotMap[0] = 1;
			this._pageTable[0].InUseCount = 1;
			this._inUseNodeCount = 1;
			this._inUseSatelliteTreeCount = 0;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000227D0 File Offset: 0x000209D0
		private void FreePage(RBTree<K>.TreePage page)
		{
			this.MarkPageFree(page);
			this._pageTable[page.PageId] = null;
			this._inUsePageCount--;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000227F8 File Offset: 0x000209F8
		private RBTree<K>.TreePage AllocPage(int size)
		{
			int num = this.GetIndexOfPageWithFreeSlot(false);
			if (num != -1)
			{
				this._pageTable[num] = new RBTree<K>.TreePage(size);
				this._nextFreePageLine = num / 32;
			}
			else
			{
				RBTree<K>.TreePage[] array = new RBTree<K>.TreePage[this._pageTable.Length * 2];
				Array.Copy(this._pageTable, 0, array, 0, this._pageTable.Length);
				int[] array2 = new int[(array.Length + 32 - 1) / 32];
				Array.Copy(this._pageTableMap, 0, array2, 0, this._pageTableMap.Length);
				this._nextFreePageLine = this._pageTableMap.Length;
				num = this._pageTable.Length;
				this._pageTable = array;
				this._pageTableMap = array2;
				this._pageTable[num] = new RBTree<K>.TreePage(size);
			}
			this._pageTable[num].PageId = num;
			this._inUsePageCount++;
			return this._pageTable[num];
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000228D2 File Offset: 0x00020AD2
		private void MarkPageFull(RBTree<K>.TreePage page)
		{
			this._pageTableMap[page.PageId / 32] |= 1 << page.PageId % 32;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000228FA File Offset: 0x00020AFA
		private void MarkPageFree(RBTree<K>.TreePage page)
		{
			this._pageTableMap[page.PageId / 32] &= ~(1 << page.PageId % 32);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00022924 File Offset: 0x00020B24
		private static int GetIntValueFromBitMap(uint bitMap)
		{
			int num = 0;
			if ((bitMap & 4294901760U) != 0U)
			{
				num += 16;
				bitMap >>= 16;
			}
			if ((bitMap & 65280U) != 0U)
			{
				num += 8;
				bitMap >>= 8;
			}
			if ((bitMap & 240U) != 0U)
			{
				num += 4;
				bitMap >>= 4;
			}
			if ((bitMap & 12U) != 0U)
			{
				num += 2;
				bitMap >>= 2;
			}
			if ((bitMap & 2U) != 0U)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00022984 File Offset: 0x00020B84
		private void FreeNode(int nodeId)
		{
			RBTree<K>.TreePage treePage = this._pageTable[nodeId >> 16];
			int num = nodeId & 65535;
			treePage._slots[num] = default(RBTree<K>.Node);
			treePage._slotMap[num / 32] &= ~(1 << num % 32);
			RBTree<K>.TreePage treePage2 = treePage;
			int inUseCount = treePage2.InUseCount;
			treePage2.InUseCount = inUseCount - 1;
			this._inUseNodeCount--;
			if (treePage.InUseCount == 0)
			{
				this.FreePage(treePage);
				return;
			}
			if (treePage.InUseCount == treePage._slots.Length - 1)
			{
				this.MarkPageFree(treePage);
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00022A1C File Offset: 0x00020C1C
		private int GetIndexOfPageWithFreeSlot(bool allocatedPage)
		{
			int i = this._nextFreePageLine;
			int num = -1;
			while (i < this._pageTableMap.Length)
			{
				if (this._pageTableMap[i] < -1)
				{
					uint num2 = (uint)this._pageTableMap[i];
					while ((num2 ^ 4294967295U) != 0U)
					{
						uint num3 = ~num2 & (num2 + 1U);
						if (((long)this._pageTableMap[i] & (long)((ulong)num3)) != 0L)
						{
							throw ExceptionBuilder.InternalRBTreeError(RBTreeError.PagePositionInSlotInUse);
						}
						num = i * 32 + RBTree<K>.GetIntValueFromBitMap(num3);
						if (allocatedPage)
						{
							if (this._pageTable[num] != null)
							{
								return num;
							}
						}
						else if (this._pageTable[num] == null)
						{
							return num;
						}
						num = -1;
						num2 |= num3;
					}
				}
				i++;
			}
			if (this._nextFreePageLine != 0)
			{
				this._nextFreePageLine = 0;
				num = this.GetIndexOfPageWithFreeSlot(allocatedPage);
			}
			return num;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00022ABF File Offset: 0x00020CBF
		public int Count
		{
			get
			{
				return this._inUseNodeCount - 1;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00022AC9 File Offset: 0x00020CC9
		public bool HasDuplicates
		{
			get
			{
				return this._inUseSatelliteTreeCount != 0;
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00022AD4 File Offset: 0x00020CD4
		private int GetNewNode(K key)
		{
			int indexOfPageWithFreeSlot = this.GetIndexOfPageWithFreeSlot(true);
			RBTree<K>.TreePage treePage;
			if (indexOfPageWithFreeSlot != -1)
			{
				treePage = this._pageTable[indexOfPageWithFreeSlot];
			}
			else if (this._inUsePageCount < 4)
			{
				treePage = this.AllocPage(32);
			}
			else if (this._inUsePageCount < 32)
			{
				treePage = this.AllocPage(256);
			}
			else if (this._inUsePageCount < 128)
			{
				treePage = this.AllocPage(1024);
			}
			else if (this._inUsePageCount < 4096)
			{
				treePage = this.AllocPage(4096);
			}
			else if (this._inUsePageCount < 32768)
			{
				treePage = this.AllocPage(8192);
			}
			else
			{
				treePage = this.AllocPage(65536);
			}
			int num = treePage.AllocSlot(this);
			if (num == -1)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.NoFreeSlots);
			}
			treePage._slots[num]._selfId = (treePage.PageId << 16) | num;
			treePage._slots[num]._subTreeSize = 1;
			treePage._slots[num]._keyOfNode = key;
			return treePage._slots[num]._selfId;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00022BEC File Offset: 0x00020DEC
		private int Successor(int x_id)
		{
			if (this.Right(x_id) != 0)
			{
				return this.Minimum(this.Right(x_id));
			}
			int num = this.Parent(x_id);
			while (num != 0 && x_id == this.Right(num))
			{
				x_id = num;
				num = this.Parent(num);
			}
			return num;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00022C34 File Offset: 0x00020E34
		private bool Successor(ref int nodeId, ref int mainTreeNodeId)
		{
			if (nodeId == 0)
			{
				nodeId = this.Minimum(mainTreeNodeId);
				mainTreeNodeId = 0;
			}
			else
			{
				nodeId = this.Successor(nodeId);
				if (nodeId == 0 && mainTreeNodeId != 0)
				{
					nodeId = this.Successor(mainTreeNodeId);
					mainTreeNodeId = 0;
				}
			}
			if (nodeId != 0)
			{
				if (this.Next(nodeId) != 0)
				{
					if (mainTreeNodeId != 0)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.NestedSatelliteTreeEnumerator);
					}
					mainTreeNodeId = nodeId;
					nodeId = this.Minimum(this.Next(nodeId));
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00022CA4 File Offset: 0x00020EA4
		private int Minimum(int x_id)
		{
			while (this.Left(x_id) != 0)
			{
				x_id = this.Left(x_id);
			}
			return x_id;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00022CBC File Offset: 0x00020EBC
		private int LeftRotate(int root_id, int x_id, int mainTreeNode)
		{
			int num = this.Right(x_id);
			this.SetRight(x_id, this.Left(num));
			if (this.Left(num) != 0)
			{
				this.SetParent(this.Left(num), x_id);
			}
			this.SetParent(num, this.Parent(x_id));
			if (this.Parent(x_id) == 0)
			{
				if (root_id == 0)
				{
					this.root = num;
				}
				else
				{
					this.SetNext(mainTreeNode, num);
					this.SetKey(mainTreeNode, this.Key(num));
					root_id = num;
				}
			}
			else if (x_id == this.Left(this.Parent(x_id)))
			{
				this.SetLeft(this.Parent(x_id), num);
			}
			else
			{
				this.SetRight(this.Parent(x_id), num);
			}
			this.SetLeft(num, x_id);
			this.SetParent(x_id, num);
			if (x_id != 0)
			{
				this.SetSubTreeSize(x_id, this.SubTreeSize(this.Left(x_id)) + this.SubTreeSize(this.Right(x_id)) + ((this.Next(x_id) == 0) ? 1 : this.SubTreeSize(this.Next(x_id))));
			}
			if (num != 0)
			{
				this.SetSubTreeSize(num, this.SubTreeSize(this.Left(num)) + this.SubTreeSize(this.Right(num)) + ((this.Next(num) == 0) ? 1 : this.SubTreeSize(this.Next(num))));
			}
			return root_id;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00022DF4 File Offset: 0x00020FF4
		private int RightRotate(int root_id, int x_id, int mainTreeNode)
		{
			int num = this.Left(x_id);
			this.SetLeft(x_id, this.Right(num));
			if (this.Right(num) != 0)
			{
				this.SetParent(this.Right(num), x_id);
			}
			this.SetParent(num, this.Parent(x_id));
			if (this.Parent(x_id) == 0)
			{
				if (root_id == 0)
				{
					this.root = num;
				}
				else
				{
					this.SetNext(mainTreeNode, num);
					this.SetKey(mainTreeNode, this.Key(num));
					root_id = num;
				}
			}
			else if (x_id == this.Left(this.Parent(x_id)))
			{
				this.SetLeft(this.Parent(x_id), num);
			}
			else
			{
				this.SetRight(this.Parent(x_id), num);
			}
			this.SetRight(num, x_id);
			this.SetParent(x_id, num);
			if (x_id != 0)
			{
				this.SetSubTreeSize(x_id, this.SubTreeSize(this.Left(x_id)) + this.SubTreeSize(this.Right(x_id)) + ((this.Next(x_id) == 0) ? 1 : this.SubTreeSize(this.Next(x_id))));
			}
			if (num != 0)
			{
				this.SetSubTreeSize(num, this.SubTreeSize(this.Left(num)) + this.SubTreeSize(this.Right(num)) + ((this.Next(num) == 0) ? 1 : this.SubTreeSize(this.Next(num))));
			}
			return root_id;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00022F2C File Offset: 0x0002112C
		private int RBInsert(int root_id, int x_id, int mainTreeNodeID, int position, bool append)
		{
			this._version++;
			int num = 0;
			int num2 = ((root_id == 0) ? this.root : root_id);
			if (this._accessMethod == TreeAccessMethod.KEY_SEARCH_AND_INDEX && !append)
			{
				while (num2 != 0)
				{
					this.IncreaseSize(num2);
					num = num2;
					int num3 = ((root_id == 0) ? this.CompareNode(this.Key(x_id), this.Key(num2)) : this.CompareSateliteTreeNode(this.Key(x_id), this.Key(num2)));
					if (num3 < 0)
					{
						num2 = this.Left(num2);
					}
					else if (num3 > 0)
					{
						num2 = this.Right(num2);
					}
					else
					{
						if (root_id != 0)
						{
							throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidStateinInsert);
						}
						if (this.Next(num2) != 0)
						{
							root_id = this.RBInsert(this.Next(num2), x_id, num2, -1, false);
							this.SetKey(num2, this.Key(this.Next(num2)));
						}
						else
						{
							int newNode = this.GetNewNode(this.Key(num2));
							this._inUseSatelliteTreeCount++;
							this.SetNext(newNode, num2);
							this.SetColor(newNode, this.color(num2));
							this.SetParent(newNode, this.Parent(num2));
							this.SetLeft(newNode, this.Left(num2));
							this.SetRight(newNode, this.Right(num2));
							if (this.Left(this.Parent(num2)) == num2)
							{
								this.SetLeft(this.Parent(num2), newNode);
							}
							else if (this.Right(this.Parent(num2)) == num2)
							{
								this.SetRight(this.Parent(num2), newNode);
							}
							if (this.Left(num2) != 0)
							{
								this.SetParent(this.Left(num2), newNode);
							}
							if (this.Right(num2) != 0)
							{
								this.SetParent(this.Right(num2), newNode);
							}
							if (this.root == num2)
							{
								this.root = newNode;
							}
							this.SetColor(num2, RBTree<K>.NodeColor.black);
							this.SetParent(num2, 0);
							this.SetLeft(num2, 0);
							this.SetRight(num2, 0);
							int num4 = this.SubTreeSize(num2);
							this.SetSubTreeSize(num2, 1);
							root_id = this.RBInsert(num2, x_id, newNode, -1, false);
							this.SetSubTreeSize(newNode, num4);
						}
						return root_id;
					}
				}
			}
			else
			{
				if (this._accessMethod != TreeAccessMethod.INDEX_ONLY && !append)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.UnsupportedAccessMethod1);
				}
				if (position == -1)
				{
					position = this.SubTreeSize(this.root);
				}
				while (num2 != 0)
				{
					this.IncreaseSize(num2);
					num = num2;
					int num5 = position - this.SubTreeSize(this.Left(num));
					if (num5 <= 0)
					{
						num2 = this.Left(num2);
					}
					else
					{
						num2 = this.Right(num2);
						if (num2 != 0)
						{
							position = num5 - 1;
						}
					}
				}
			}
			this.SetParent(x_id, num);
			if (num == 0)
			{
				if (root_id == 0)
				{
					this.root = x_id;
				}
				else
				{
					this.SetNext(mainTreeNodeID, x_id);
					this.SetKey(mainTreeNodeID, this.Key(x_id));
					root_id = x_id;
				}
			}
			else
			{
				int num6;
				if (this._accessMethod == TreeAccessMethod.KEY_SEARCH_AND_INDEX)
				{
					num6 = ((root_id == 0) ? this.CompareNode(this.Key(x_id), this.Key(num)) : this.CompareSateliteTreeNode(this.Key(x_id), this.Key(num)));
				}
				else
				{
					if (this._accessMethod != TreeAccessMethod.INDEX_ONLY)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.UnsupportedAccessMethod2);
					}
					num6 = ((position <= 0) ? (-1) : 1);
				}
				if (num6 < 0)
				{
					this.SetLeft(num, x_id);
				}
				else
				{
					this.SetRight(num, x_id);
				}
			}
			this.SetLeft(x_id, 0);
			this.SetRight(x_id, 0);
			this.SetColor(x_id, RBTree<K>.NodeColor.red);
			while (this.color(this.Parent(x_id)) == RBTree<K>.NodeColor.red)
			{
				if (this.Parent(x_id) == this.Left(this.Parent(this.Parent(x_id))))
				{
					num = this.Right(this.Parent(this.Parent(x_id)));
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(num, RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						x_id = this.Parent(this.Parent(x_id));
					}
					else
					{
						if (x_id == this.Right(this.Parent(x_id)))
						{
							x_id = this.Parent(x_id);
							root_id = this.LeftRotate(root_id, x_id, mainTreeNodeID);
						}
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						root_id = this.RightRotate(root_id, this.Parent(this.Parent(x_id)), mainTreeNodeID);
					}
				}
				else
				{
					num = this.Left(this.Parent(this.Parent(x_id)));
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(num, RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						x_id = this.Parent(this.Parent(x_id));
					}
					else
					{
						if (x_id == this.Left(this.Parent(x_id)))
						{
							x_id = this.Parent(x_id);
							root_id = this.RightRotate(root_id, x_id, mainTreeNodeID);
						}
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						root_id = this.LeftRotate(root_id, this.Parent(this.Parent(x_id)), mainTreeNodeID);
					}
				}
			}
			if (root_id == 0)
			{
				this.SetColor(this.root, RBTree<K>.NodeColor.black);
			}
			else
			{
				this.SetColor(root_id, RBTree<K>.NodeColor.black);
			}
			return root_id;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00023420 File Offset: 0x00021620
		public void UpdateNodeKey(K currentKey, K newKey)
		{
			RBTree<K>.NodePath nodeByKey = this.GetNodeByKey(currentKey);
			if (this.Parent(nodeByKey._nodeID) == 0 && nodeByKey._nodeID != this.root)
			{
				this.SetKey(nodeByKey._mainTreeNodeID, newKey);
			}
			this.SetKey(nodeByKey._nodeID, newKey);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0002346C File Offset: 0x0002166C
		public K DeleteByIndex(int i)
		{
			RBTree<K>.NodePath nodeByIndex = this.GetNodeByIndex(i);
			K k = this.Key(nodeByIndex._nodeID);
			this.RBDeleteX(0, nodeByIndex._nodeID, nodeByIndex._mainTreeNodeID);
			return k;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000234A1 File Offset: 0x000216A1
		public int RBDelete(int z_id)
		{
			return this.RBDeleteX(0, z_id, 0);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000234AC File Offset: 0x000216AC
		private int RBDeleteX(int root_id, int z_id, int mainTreeNodeID)
		{
			if (this.Next(z_id) != 0)
			{
				return this.RBDeleteX(this.Next(z_id), this.Next(z_id), z_id);
			}
			bool flag = false;
			int num = ((this._accessMethod == TreeAccessMethod.KEY_SEARCH_AND_INDEX) ? mainTreeNodeID : z_id);
			if (this.Next(num) != 0)
			{
				root_id = this.Next(num);
			}
			if (this.SubTreeSize(this.Next(num)) == 2)
			{
				flag = true;
			}
			else if (this.SubTreeSize(this.Next(num)) == 1)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidNextSizeInDelete);
			}
			int num2;
			if (this.Left(z_id) == 0 || this.Right(z_id) == 0)
			{
				num2 = z_id;
			}
			else
			{
				num2 = this.Successor(z_id);
			}
			int num3;
			if (this.Left(num2) != 0)
			{
				num3 = this.Left(num2);
			}
			else
			{
				num3 = this.Right(num2);
			}
			int num4 = this.Parent(num2);
			if (num3 != 0)
			{
				this.SetParent(num3, num4);
			}
			if (num4 == 0)
			{
				if (root_id == 0)
				{
					this.root = num3;
				}
				else
				{
					root_id = num3;
				}
			}
			else if (num2 == this.Left(num4))
			{
				this.SetLeft(num4, num3);
			}
			else
			{
				this.SetRight(num4, num3);
			}
			if (num2 != z_id)
			{
				this.SetKey(z_id, this.Key(num2));
				this.SetNext(z_id, this.Next(num2));
			}
			if (this.Next(num) != 0)
			{
				if (root_id == 0 && z_id != num)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidStateinDelete);
				}
				if (root_id != 0)
				{
					this.SetNext(num, root_id);
					this.SetKey(num, this.Key(root_id));
				}
			}
			for (int num5 = num4; num5 != 0; num5 = this.Parent(num5))
			{
				this.RecomputeSize(num5);
			}
			if (root_id != 0)
			{
				for (int num6 = num; num6 != 0; num6 = this.Parent(num6))
				{
					this.DecreaseSize(num6);
				}
			}
			if (this.color(num2) == RBTree<K>.NodeColor.black)
			{
				root_id = this.RBDeleteFixup(root_id, num3, num4, mainTreeNodeID);
			}
			if (flag)
			{
				if (num == 0 || this.SubTreeSize(this.Next(num)) != 1)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidNodeSizeinDelete);
				}
				this._inUseSatelliteTreeCount--;
				int num7 = this.Next(num);
				this.SetLeft(num7, this.Left(num));
				this.SetRight(num7, this.Right(num));
				this.SetSubTreeSize(num7, this.SubTreeSize(num));
				this.SetColor(num7, this.color(num));
				if (this.Parent(num) != 0)
				{
					this.SetParent(num7, this.Parent(num));
					if (this.Left(this.Parent(num)) == num)
					{
						this.SetLeft(this.Parent(num), num7);
					}
					else
					{
						this.SetRight(this.Parent(num), num7);
					}
				}
				if (this.Left(num) != 0)
				{
					this.SetParent(this.Left(num), num7);
				}
				if (this.Right(num) != 0)
				{
					this.SetParent(this.Right(num), num7);
				}
				if (this.root == num)
				{
					this.root = num7;
				}
				this.FreeNode(num);
				num = 0;
			}
			else if (this.Next(num) != 0)
			{
				if (root_id == 0 && z_id != num)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidStateinEndDelete);
				}
				if (root_id != 0)
				{
					this.SetNext(num, root_id);
					this.SetKey(num, this.Key(root_id));
				}
			}
			if (num2 != z_id)
			{
				this.SetLeft(num2, this.Left(z_id));
				this.SetRight(num2, this.Right(z_id));
				this.SetColor(num2, this.color(z_id));
				this.SetSubTreeSize(num2, this.SubTreeSize(z_id));
				if (this.Parent(z_id) != 0)
				{
					this.SetParent(num2, this.Parent(z_id));
					if (this.Left(this.Parent(z_id)) == z_id)
					{
						this.SetLeft(this.Parent(z_id), num2);
					}
					else
					{
						this.SetRight(this.Parent(z_id), num2);
					}
				}
				else
				{
					this.SetParent(num2, 0);
				}
				if (this.Left(z_id) != 0)
				{
					this.SetParent(this.Left(z_id), num2);
				}
				if (this.Right(z_id) != 0)
				{
					this.SetParent(this.Right(z_id), num2);
				}
				if (this.root == z_id)
				{
					this.root = num2;
				}
				else if (root_id == z_id)
				{
					root_id = num2;
				}
				if (num != 0 && this.Next(num) == z_id)
				{
					this.SetNext(num, num2);
				}
			}
			this.FreeNode(z_id);
			this._version++;
			return z_id;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000238A0 File Offset: 0x00021AA0
		private int RBDeleteFixup(int root_id, int x_id, int px_id, int mainTreeNodeID)
		{
			if (x_id == 0 && px_id == 0)
			{
				return 0;
			}
			while (((root_id == 0) ? this.root : root_id) != x_id && this.color(x_id) == RBTree<K>.NodeColor.black)
			{
				if ((x_id != 0 && x_id == this.Left(this.Parent(x_id))) || (x_id == 0 && this.Left(px_id) == 0))
				{
					int num = ((x_id == 0) ? this.Right(px_id) : this.Right(this.Parent(x_id)));
					if (num == 0)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.RBDeleteFixup);
					}
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(num, RBTree<K>.NodeColor.black);
						this.SetColor(px_id, RBTree<K>.NodeColor.red);
						root_id = this.LeftRotate(root_id, px_id, mainTreeNodeID);
						num = ((x_id == 0) ? this.Right(px_id) : this.Right(this.Parent(x_id)));
					}
					if (this.color(this.Left(num)) == RBTree<K>.NodeColor.black && this.color(this.Right(num)) == RBTree<K>.NodeColor.black)
					{
						this.SetColor(num, RBTree<K>.NodeColor.red);
						x_id = px_id;
						px_id = this.Parent(px_id);
					}
					else
					{
						if (this.color(this.Right(num)) == RBTree<K>.NodeColor.black)
						{
							this.SetColor(this.Left(num), RBTree<K>.NodeColor.black);
							this.SetColor(num, RBTree<K>.NodeColor.red);
							root_id = this.RightRotate(root_id, num, mainTreeNodeID);
							num = ((x_id == 0) ? this.Right(px_id) : this.Right(this.Parent(x_id)));
						}
						this.SetColor(num, this.color(px_id));
						this.SetColor(px_id, RBTree<K>.NodeColor.black);
						this.SetColor(this.Right(num), RBTree<K>.NodeColor.black);
						root_id = this.LeftRotate(root_id, px_id, mainTreeNodeID);
						x_id = ((root_id == 0) ? this.root : root_id);
						px_id = this.Parent(x_id);
					}
				}
				else
				{
					int num = this.Left(px_id);
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(num, RBTree<K>.NodeColor.black);
						if (x_id != 0)
						{
							this.SetColor(px_id, RBTree<K>.NodeColor.red);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							num = ((x_id == 0) ? this.Left(px_id) : this.Left(this.Parent(x_id)));
						}
						else
						{
							this.SetColor(px_id, RBTree<K>.NodeColor.red);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							num = ((x_id == 0) ? this.Left(px_id) : this.Left(this.Parent(x_id)));
							if (num == 0)
							{
								throw ExceptionBuilder.InternalRBTreeError(RBTreeError.CannotRotateInvalidsuccessorNodeinDelete);
							}
						}
					}
					if (this.color(this.Right(num)) == RBTree<K>.NodeColor.black && this.color(this.Left(num)) == RBTree<K>.NodeColor.black)
					{
						this.SetColor(num, RBTree<K>.NodeColor.red);
						x_id = px_id;
						px_id = this.Parent(px_id);
					}
					else
					{
						if (this.color(this.Left(num)) == RBTree<K>.NodeColor.black)
						{
							this.SetColor(this.Right(num), RBTree<K>.NodeColor.black);
							this.SetColor(num, RBTree<K>.NodeColor.red);
							root_id = this.LeftRotate(root_id, num, mainTreeNodeID);
							num = ((x_id == 0) ? this.Left(px_id) : this.Left(this.Parent(x_id)));
						}
						if (x_id != 0)
						{
							this.SetColor(num, this.color(px_id));
							this.SetColor(px_id, RBTree<K>.NodeColor.black);
							this.SetColor(this.Left(num), RBTree<K>.NodeColor.black);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							x_id = ((root_id == 0) ? this.root : root_id);
							px_id = this.Parent(x_id);
						}
						else
						{
							this.SetColor(num, this.color(px_id));
							this.SetColor(px_id, RBTree<K>.NodeColor.black);
							this.SetColor(this.Left(num), RBTree<K>.NodeColor.black);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							x_id = ((root_id == 0) ? this.root : root_id);
							px_id = this.Parent(x_id);
						}
					}
				}
			}
			this.SetColor(x_id, RBTree<K>.NodeColor.black);
			return root_id;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00023BD8 File Offset: 0x00021DD8
		private int SearchSubTree(int root_id, K key)
		{
			if (root_id != 0 && this._accessMethod != TreeAccessMethod.KEY_SEARCH_AND_INDEX)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.UnsupportedAccessMethodInNonNillRootSubtree);
			}
			int num = ((root_id == 0) ? this.root : root_id);
			while (num != 0)
			{
				int num2 = ((root_id == 0) ? this.CompareNode(key, this.Key(num)) : this.CompareSateliteTreeNode(key, this.Key(num)));
				if (num2 == 0)
				{
					break;
				}
				if (num2 < 0)
				{
					num = this.Left(num);
				}
				else
				{
					num = this.Right(num);
				}
			}
			return num;
		}

		// Token: 0x17000123 RID: 291
		public K this[int index]
		{
			get
			{
				return this.Key(this.GetNodeByIndex(index)._nodeID);
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00023C5C File Offset: 0x00021E5C
		private RBTree<K>.NodePath GetNodeByKey(K key)
		{
			int num = this.SearchSubTree(0, key);
			if (this.Next(num) != 0)
			{
				return new RBTree<K>.NodePath(this.SearchSubTree(this.Next(num), key), num);
			}
			K k = this.Key(num);
			if (!k.Equals(key))
			{
				num = 0;
			}
			return new RBTree<K>.NodePath(num, 0);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00023CB8 File Offset: 0x00021EB8
		public int GetIndexByKey(K key)
		{
			int num = -1;
			RBTree<K>.NodePath nodeByKey = this.GetNodeByKey(key);
			if (nodeByKey._nodeID != 0)
			{
				num = this.GetIndexByNodePath(nodeByKey);
			}
			return num;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00023CE0 File Offset: 0x00021EE0
		public int GetIndexByNode(int node)
		{
			if (this._inUseSatelliteTreeCount == 0)
			{
				return this.ComputeIndexByNode(node);
			}
			if (this.Next(node) != 0)
			{
				return this.ComputeIndexWithSatelliteByNode(node);
			}
			int num = this.SearchSubTree(0, this.Key(node));
			if (num == node)
			{
				return this.ComputeIndexWithSatelliteByNode(node);
			}
			return this.ComputeIndexWithSatelliteByNode(num) + this.ComputeIndexByNode(node);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00023D38 File Offset: 0x00021F38
		private int GetIndexByNodePath(RBTree<K>.NodePath path)
		{
			if (this._inUseSatelliteTreeCount == 0)
			{
				return this.ComputeIndexByNode(path._nodeID);
			}
			if (path._mainTreeNodeID == 0)
			{
				return this.ComputeIndexWithSatelliteByNode(path._nodeID);
			}
			return this.ComputeIndexWithSatelliteByNode(path._mainTreeNodeID) + this.ComputeIndexByNode(path._nodeID);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00023D88 File Offset: 0x00021F88
		private int ComputeIndexByNode(int nodeId)
		{
			int num = this.SubTreeSize(this.Left(nodeId));
			while (nodeId != 0)
			{
				int num2 = this.Parent(nodeId);
				if (nodeId == this.Right(num2))
				{
					num += this.SubTreeSize(this.Left(num2)) + 1;
				}
				nodeId = num2;
			}
			return num;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00023DD0 File Offset: 0x00021FD0
		private int ComputeIndexWithSatelliteByNode(int nodeId)
		{
			int num = this.SubTreeSize(this.Left(nodeId));
			while (nodeId != 0)
			{
				int num2 = this.Parent(nodeId);
				if (nodeId == this.Right(num2))
				{
					num += this.SubTreeSize(this.Left(num2)) + ((this.Next(num2) == 0) ? 1 : this.SubTreeSize(this.Next(num2)));
				}
				nodeId = num2;
			}
			return num;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00023E30 File Offset: 0x00022030
		private RBTree<K>.NodePath GetNodeByIndex(int userIndex)
		{
			int num;
			int num2;
			if (this._inUseSatelliteTreeCount == 0)
			{
				num = this.ComputeNodeByIndex(this.root, userIndex + 1);
				num2 = 0;
			}
			else
			{
				num = this.ComputeNodeByIndex(userIndex, out num2);
			}
			if (num != 0)
			{
				return new RBTree<K>.NodePath(num, num2);
			}
			if (TreeAccessMethod.INDEX_ONLY == this._accessMethod)
			{
				throw ExceptionBuilder.RowOutOfRange(userIndex);
			}
			throw ExceptionBuilder.InternalRBTreeError(RBTreeError.IndexOutOFRangeinGetNodeByIndex);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00023E88 File Offset: 0x00022088
		private int ComputeNodeByIndex(int index, out int satelliteRootId)
		{
			index++;
			satelliteRootId = 0;
			int num = this.root;
			int num2;
			while (num != 0 && ((num2 = this.SubTreeSize(this.Left(num)) + 1) != index || this.Next(num) != 0))
			{
				if (index < num2)
				{
					num = this.Left(num);
				}
				else
				{
					if (this.Next(num) != 0 && index >= num2 && index <= num2 + this.SubTreeSize(this.Next(num)) - 1)
					{
						satelliteRootId = num;
						index = index - num2 + 1;
						return this.ComputeNodeByIndex(this.Next(num), index);
					}
					if (this.Next(num) == 0)
					{
						index -= num2;
					}
					else
					{
						index -= num2 + this.SubTreeSize(this.Next(num)) - 1;
					}
					num = this.Right(num);
				}
			}
			return num;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00023F44 File Offset: 0x00022144
		private int ComputeNodeByIndex(int x_id, int index)
		{
			while (x_id != 0)
			{
				int num = this.Left(x_id);
				int num2 = this.SubTreeSize(num) + 1;
				if (index < num2)
				{
					x_id = num;
				}
				else
				{
					if (num2 >= index)
					{
						break;
					}
					x_id = this.Right(x_id);
					index -= num2;
				}
			}
			return x_id;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00023F84 File Offset: 0x00022184
		public int Insert(K item)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, -1, false);
			return newNode;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00023FA8 File Offset: 0x000221A8
		public int Add(K item)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, -1, false);
			return newNode;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00023FCA File Offset: 0x000221CA
		public IEnumerator GetEnumerator()
		{
			return new RBTree<K>.RBTreeEnumerator(this);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00023FD8 File Offset: 0x000221D8
		public int IndexOf(int nodeId, K item)
		{
			int num = -1;
			if (nodeId == 0)
			{
				return num;
			}
			if (this.Key(nodeId) == item)
			{
				return this.GetIndexByNode(nodeId);
			}
			if ((num = this.IndexOf(this.Left(nodeId), item)) != -1)
			{
				return num;
			}
			return this.IndexOf(this.Right(nodeId), item);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00024031 File Offset: 0x00022231
		public int Insert(int position, K item)
		{
			return this.InsertAt(position, item, false);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0002403C File Offset: 0x0002223C
		public int InsertAt(int position, K item, bool append)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, position, append);
			return newNode;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0002405E File Offset: 0x0002225E
		public void RemoveAt(int position)
		{
			this.DeleteByIndex(position);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00024068 File Offset: 0x00022268
		public void Clear()
		{
			this.InitTree();
			this._version++;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00024080 File Offset: 0x00022280
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			int count = this.Count;
			if (array.Length - index < this.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			int num = this.Minimum(this.root);
			for (int i = 0; i < count; i++)
			{
				array.SetValue(this.Key(num), index + i);
				num = this.Successor(num);
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00024100 File Offset: 0x00022300
		public void CopyTo(K[] array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			int count = this.Count;
			if (array.Length - index < this.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			int num = this.Minimum(this.root);
			for (int i = 0; i < count; i++)
			{
				array[index + i] = this.Key(num);
				num = this.Successor(num);
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00024175 File Offset: 0x00022375
		private void SetRight(int nodeId, int rightNodeId)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._rightId = rightNodeId;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00024199 File Offset: 0x00022399
		private void SetLeft(int nodeId, int leftNodeId)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._leftId = leftNodeId;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000241BD File Offset: 0x000223BD
		private void SetParent(int nodeId, int parentNodeId)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._parentId = parentNodeId;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000241E1 File Offset: 0x000223E1
		private void SetColor(int nodeId, RBTree<K>.NodeColor color)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._nodeColor = color;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00024205 File Offset: 0x00022405
		private void SetKey(int nodeId, K key)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._keyOfNode = key;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00024229 File Offset: 0x00022429
		private void SetNext(int nodeId, int nextNodeId)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._nextId = nextNodeId;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0002424D File Offset: 0x0002244D
		private void SetSubTreeSize(int nodeId, int size)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._subTreeSize = size;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00024271 File Offset: 0x00022471
		private void IncreaseSize(int nodeId)
		{
			RBTree<K>.Node[] slots = this._pageTable[nodeId >> 16]._slots;
			int num = nodeId & 65535;
			slots[num]._subTreeSize = slots[num]._subTreeSize + 1;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0002429C File Offset: 0x0002249C
		private void RecomputeSize(int nodeId)
		{
			int num = this.SubTreeSize(this.Left(nodeId)) + this.SubTreeSize(this.Right(nodeId)) + ((this.Next(nodeId) == 0) ? 1 : this.SubTreeSize(this.Next(nodeId)));
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._subTreeSize = num;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00024301 File Offset: 0x00022501
		private void DecreaseSize(int nodeId)
		{
			RBTree<K>.Node[] slots = this._pageTable[nodeId >> 16]._slots;
			int num = nodeId & 65535;
			slots[num]._subTreeSize = slots[num]._subTreeSize - 1;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00024329 File Offset: 0x00022529
		public int Right(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._rightId;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0002434C File Offset: 0x0002254C
		public int Left(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._leftId;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0002436F File Offset: 0x0002256F
		public int Parent(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._parentId;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00024392 File Offset: 0x00022592
		private RBTree<K>.NodeColor color(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._nodeColor;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000243B5 File Offset: 0x000225B5
		public int Next(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._nextId;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000243D8 File Offset: 0x000225D8
		public int SubTreeSize(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._subTreeSize;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000243FB File Offset: 0x000225FB
		public K Key(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._keyOfNode;
		}

		// Token: 0x040002BF RID: 703
		private RBTree<K>.TreePage[] _pageTable;

		// Token: 0x040002C0 RID: 704
		private int[] _pageTableMap;

		// Token: 0x040002C1 RID: 705
		private int _inUsePageCount;

		// Token: 0x040002C2 RID: 706
		private int _nextFreePageLine;

		// Token: 0x040002C3 RID: 707
		public int root;

		// Token: 0x040002C4 RID: 708
		private int _version;

		// Token: 0x040002C5 RID: 709
		private int _inUseNodeCount;

		// Token: 0x040002C6 RID: 710
		private int _inUseSatelliteTreeCount;

		// Token: 0x040002C7 RID: 711
		private readonly TreeAccessMethod _accessMethod;

		// Token: 0x0200008C RID: 140
		private enum NodeColor
		{
			// Token: 0x040002C9 RID: 713
			red,
			// Token: 0x040002CA RID: 714
			black
		}

		// Token: 0x0200008D RID: 141
		private struct Node
		{
			// Token: 0x040002CB RID: 715
			internal int _selfId;

			// Token: 0x040002CC RID: 716
			internal int _leftId;

			// Token: 0x040002CD RID: 717
			internal int _rightId;

			// Token: 0x040002CE RID: 718
			internal int _parentId;

			// Token: 0x040002CF RID: 719
			internal int _nextId;

			// Token: 0x040002D0 RID: 720
			internal int _subTreeSize;

			// Token: 0x040002D1 RID: 721
			internal K _keyOfNode;

			// Token: 0x040002D2 RID: 722
			internal RBTree<K>.NodeColor _nodeColor;
		}

		// Token: 0x0200008E RID: 142
		private readonly struct NodePath
		{
			// Token: 0x06000732 RID: 1842 RVA: 0x0002441E File Offset: 0x0002261E
			internal NodePath(int nodeID, int mainTreeNodeID)
			{
				this._nodeID = nodeID;
				this._mainTreeNodeID = mainTreeNodeID;
			}

			// Token: 0x040002D3 RID: 723
			internal readonly int _nodeID;

			// Token: 0x040002D4 RID: 724
			internal readonly int _mainTreeNodeID;
		}

		// Token: 0x0200008F RID: 143
		private sealed class TreePage
		{
			// Token: 0x06000733 RID: 1843 RVA: 0x0002442E File Offset: 0x0002262E
			internal TreePage(int size)
			{
				if (size > 65536)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidPageSize);
				}
				this._slots = new RBTree<K>.Node[size];
				this._slotMap = new int[(size + 32 - 1) / 32];
			}

			// Token: 0x06000734 RID: 1844 RVA: 0x00024468 File Offset: 0x00022668
			internal int AllocSlot(RBTree<K> tree)
			{
				int num = -1;
				if (this._inUseCount < this._slots.Length)
				{
					for (int i = this._nextFreeSlotLine; i < this._slotMap.Length; i++)
					{
						if (this._slotMap[i] < -1)
						{
							int num2 = ~this._slotMap[i] & (this._slotMap[i] + 1);
							this._slotMap[i] |= num2;
							this._inUseCount++;
							if (this._inUseCount == this._slots.Length)
							{
								tree.MarkPageFull(this);
							}
							tree._inUseNodeCount++;
							num = RBTree<K>.GetIntValueFromBitMap((uint)num2);
							this._nextFreeSlotLine = i;
							num = i * 32 + num;
							break;
						}
					}
					if (num == -1 && this._nextFreeSlotLine != 0)
					{
						this._nextFreeSlotLine = 0;
						num = this.AllocSlot(tree);
					}
				}
				return num;
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x06000735 RID: 1845 RVA: 0x00024545 File Offset: 0x00022745
			// (set) Token: 0x06000736 RID: 1846 RVA: 0x0002454D File Offset: 0x0002274D
			internal int InUseCount
			{
				get
				{
					return this._inUseCount;
				}
				set
				{
					this._inUseCount = value;
				}
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x06000737 RID: 1847 RVA: 0x00024556 File Offset: 0x00022756
			// (set) Token: 0x06000738 RID: 1848 RVA: 0x0002455E File Offset: 0x0002275E
			internal int PageId
			{
				get
				{
					return this._pageId;
				}
				set
				{
					this._pageId = value;
				}
			}

			// Token: 0x040002D5 RID: 725
			internal readonly RBTree<K>.Node[] _slots;

			// Token: 0x040002D6 RID: 726
			internal readonly int[] _slotMap;

			// Token: 0x040002D7 RID: 727
			private int _inUseCount;

			// Token: 0x040002D8 RID: 728
			private int _pageId;

			// Token: 0x040002D9 RID: 729
			private int _nextFreeSlotLine;
		}

		// Token: 0x02000090 RID: 144
		internal struct RBTreeEnumerator : IEnumerator<K>, IDisposable, IEnumerator
		{
			// Token: 0x06000739 RID: 1849 RVA: 0x00024567 File Offset: 0x00022767
			internal RBTreeEnumerator(RBTree<K> tree)
			{
				this._tree = tree;
				this._version = tree._version;
				this._index = 0;
				this._mainTreeNodeId = tree.root;
				this._current = default(K);
			}

			// Token: 0x0600073A RID: 1850 RVA: 0x0002459C File Offset: 0x0002279C
			internal RBTreeEnumerator(RBTree<K> tree, int position)
			{
				this._tree = tree;
				this._version = tree._version;
				if (position == 0)
				{
					this._index = 0;
					this._mainTreeNodeId = tree.root;
				}
				else
				{
					this._index = tree.ComputeNodeByIndex(position - 1, out this._mainTreeNodeId);
					if (this._index == 0)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.IndexOutOFRangeinGetNodeByIndex);
					}
				}
				this._current = default(K);
			}

			// Token: 0x0600073B RID: 1851 RVA: 0x00003FD2 File Offset: 0x000021D2
			public void Dispose()
			{
			}

			// Token: 0x0600073C RID: 1852 RVA: 0x00024608 File Offset: 0x00022808
			public bool MoveNext()
			{
				if (this._version != this._tree._version)
				{
					throw ExceptionBuilder.EnumeratorModified();
				}
				bool flag = this._tree.Successor(ref this._index, ref this._mainTreeNodeId);
				this._current = this._tree.Key(this._index);
				return flag;
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x0600073D RID: 1853 RVA: 0x0002465C File Offset: 0x0002285C
			public K Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x0600073E RID: 1854 RVA: 0x00024664 File Offset: 0x00022864
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600073F RID: 1855 RVA: 0x00024671 File Offset: 0x00022871
			void IEnumerator.Reset()
			{
				if (this._version != this._tree._version)
				{
					throw ExceptionBuilder.EnumeratorModified();
				}
				this._index = 0;
				this._mainTreeNodeId = this._tree.root;
				this._current = default(K);
			}

			// Token: 0x040002DA RID: 730
			private readonly RBTree<K> _tree;

			// Token: 0x040002DB RID: 731
			private readonly int _version;

			// Token: 0x040002DC RID: 732
			private int _index;

			// Token: 0x040002DD RID: 733
			private int _mainTreeNodeId;

			// Token: 0x040002DE RID: 734
			private K _current;
		}
	}
}
