using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020004DF RID: 1247
	public class VisualElementFocusRing : IFocusRing
	{
		// Token: 0x060022FD RID: 8957 RVA: 0x0008065D File Offset: 0x0007E85D
		public VisualElementFocusRing(VisualElement root, VisualElementFocusRing.DefaultFocusOrder dfo = VisualElementFocusRing.DefaultFocusOrder.ChildOrder)
		{
			this.defaultFocusOrder = dfo;
			this.root = root;
			this.m_FocusRing = new List<VisualElementFocusRing.FocusRingRecord>();
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x00080681 File Offset: 0x0007E881
		private FocusController focusController
		{
			get
			{
				return this.root.focusController;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x0008068E File Offset: 0x0007E88E
		// (set) Token: 0x06002300 RID: 8960 RVA: 0x00080696 File Offset: 0x0007E896
		public VisualElementFocusRing.DefaultFocusOrder defaultFocusOrder { get; set; }

		// Token: 0x06002301 RID: 8961 RVA: 0x000806A0 File Offset: 0x0007E8A0
		private int FocusRingAutoIndexSort(VisualElementFocusRing.FocusRingRecord a, VisualElementFocusRing.FocusRingRecord b)
		{
			int num;
			switch (this.defaultFocusOrder)
			{
			default:
				num = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			case VisualElementFocusRing.DefaultFocusOrder.PositionXY:
			{
				VisualElement ave = a.m_Focusable as VisualElement;
				VisualElement bve = b.m_Focusable as VisualElement;
				bool flag = ave != null && bve != null;
				if (flag)
				{
					bool flag2 = ave.layout.position.x < bve.layout.position.x;
					if (flag2)
					{
						num = -1;
						break;
					}
					bool flag3 = ave.layout.position.x > bve.layout.position.x;
					if (flag3)
					{
						num = 1;
						break;
					}
					bool flag4 = ave.layout.position.y < bve.layout.position.y;
					if (flag4)
					{
						num = -1;
						break;
					}
					bool flag5 = ave.layout.position.y > bve.layout.position.y;
					if (flag5)
					{
						num = 1;
						break;
					}
				}
				num = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			}
			case VisualElementFocusRing.DefaultFocusOrder.PositionYX:
			{
				VisualElement ave2 = a.m_Focusable as VisualElement;
				VisualElement bve2 = b.m_Focusable as VisualElement;
				bool flag6 = ave2 != null && bve2 != null;
				if (flag6)
				{
					bool flag7 = ave2.layout.position.y < bve2.layout.position.y;
					if (flag7)
					{
						num = -1;
						break;
					}
					bool flag8 = ave2.layout.position.y > bve2.layout.position.y;
					if (flag8)
					{
						num = 1;
						break;
					}
					bool flag9 = ave2.layout.position.x < bve2.layout.position.x;
					if (flag9)
					{
						num = -1;
						break;
					}
					bool flag10 = ave2.layout.position.x > bve2.layout.position.x;
					if (flag10)
					{
						num = 1;
						break;
					}
				}
				num = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			}
			}
			return num;
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x0008094C File Offset: 0x0007EB4C
		private int FocusRingSort(VisualElementFocusRing.FocusRingRecord a, VisualElementFocusRing.FocusRingRecord b)
		{
			bool flag = a.m_Focusable.tabIndex == 0 && b.m_Focusable.tabIndex == 0;
			int num;
			if (flag)
			{
				num = this.FocusRingAutoIndexSort(a, b);
			}
			else
			{
				bool flag2 = a.m_Focusable.tabIndex == 0;
				if (flag2)
				{
					num = 1;
				}
				else
				{
					bool flag3 = b.m_Focusable.tabIndex == 0;
					if (flag3)
					{
						num = -1;
					}
					else
					{
						int result = Comparer<int>.Default.Compare(a.m_Focusable.tabIndex, b.m_Focusable.tabIndex);
						bool flag4 = result == 0;
						if (flag4)
						{
							result = this.FocusRingAutoIndexSort(a, b);
						}
						num = result;
					}
				}
			}
			return num;
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x000809F8 File Offset: 0x0007EBF8
		private void DoUpdate()
		{
			this.m_FocusRing.Clear();
			bool flag = this.root != null;
			if (flag)
			{
				List<VisualElementFocusRing.FocusRingRecord> rootScopeList = new List<VisualElementFocusRing.FocusRingRecord>();
				int autoIndex = 0;
				this.BuildRingForScopeRecursive(this.root, ref autoIndex, rootScopeList);
				this.SortAndFlattenScopeLists(rootScopeList);
			}
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x00080A44 File Offset: 0x0007EC44
		private void BuildRingForScopeRecursive(VisualElement ve, ref int scopeIndex, List<VisualElementFocusRing.FocusRingRecord> scopeList)
		{
			int veChildCount = ve.hierarchy.childCount;
			for (int i = 0; i < veChildCount; i++)
			{
				VisualElement child = ve.hierarchy[i];
				bool isSlot = child.parent != null && child == child.parent.contentContainer;
				bool flag = child.isCompositeRoot || isSlot;
				if (flag)
				{
					VisualElementFocusRing.FocusRingRecord focusRingRecord = new VisualElementFocusRing.FocusRingRecord();
					int num = scopeIndex;
					scopeIndex = num + 1;
					focusRingRecord.m_AutoIndex = num;
					focusRingRecord.m_Focusable = child;
					focusRingRecord.m_IsSlot = isSlot;
					focusRingRecord.m_ScopeNavigationOrder = new List<VisualElementFocusRing.FocusRingRecord>();
					VisualElementFocusRing.FocusRingRecord childRecord = focusRingRecord;
					scopeList.Add(childRecord);
					int autoIndex = 0;
					this.BuildRingForScopeRecursive(child, ref autoIndex, childRecord.m_ScopeNavigationOrder);
				}
				else
				{
					bool flag2 = child.canGrabFocus && child.areAncestorsAndSelfDisplayed && child.tabIndex >= 0;
					if (flag2)
					{
						VisualElementFocusRing.FocusRingRecord focusRingRecord2 = new VisualElementFocusRing.FocusRingRecord();
						int num = scopeIndex;
						scopeIndex = num + 1;
						focusRingRecord2.m_AutoIndex = num;
						focusRingRecord2.m_Focusable = child;
						focusRingRecord2.m_IsSlot = false;
						focusRingRecord2.m_ScopeNavigationOrder = null;
						scopeList.Add(focusRingRecord2);
					}
					this.BuildRingForScopeRecursive(child, ref scopeIndex, scopeList);
				}
			}
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x00080B70 File Offset: 0x0007ED70
		private void SortAndFlattenScopeLists(List<VisualElementFocusRing.FocusRingRecord> rootScopeList)
		{
			bool flag = rootScopeList != null;
			if (flag)
			{
				rootScopeList.Sort(new Comparison<VisualElementFocusRing.FocusRingRecord>(this.FocusRingSort));
				foreach (VisualElementFocusRing.FocusRingRecord record in rootScopeList)
				{
					bool flag2 = record.m_Focusable.canGrabFocus && record.m_Focusable.tabIndex >= 0;
					if (flag2)
					{
						bool flag3 = !record.m_Focusable.excludeFromFocusRing;
						if (flag3)
						{
							this.m_FocusRing.Add(record);
						}
						this.SortAndFlattenScopeLists(record.m_ScopeNavigationOrder);
					}
					else
					{
						bool isSlot = record.m_IsSlot;
						if (isSlot)
						{
							this.SortAndFlattenScopeLists(record.m_ScopeNavigationOrder);
						}
					}
					record.m_ScopeNavigationOrder = null;
				}
			}
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x00080C60 File Offset: 0x0007EE60
		private int GetFocusableInternalIndex(Focusable f)
		{
			bool flag = f != null;
			if (flag)
			{
				for (int i = 0; i < this.m_FocusRing.Count; i++)
				{
					bool flag2 = f == this.m_FocusRing[i].m_Focusable;
					if (flag2)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x00080CB8 File Offset: 0x0007EEB8
		public FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e)
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			bool flag2 = e.eventTypeId == EventBase<PointerDownEvent>.TypeId();
			if (flag2)
			{
				Focusable target;
				bool focusableParentForPointerEvent = this.focusController.GetFocusableParentForPointerEvent(e.elementTarget, out target);
				if (focusableParentForPointerEvent)
				{
					return VisualElementFocusChangeTarget.GetPooled(target);
				}
			}
			bool flag3 = currentFocusable != null && currentFocusable.isIMGUIContainer;
			FocusChangeDirection focusChangeDirection;
			if (flag3)
			{
				focusChangeDirection = FocusChangeDirection.none;
			}
			else
			{
				bool flag4 = e.eventTypeId == EventBase<NavigationMoveEvent>.TypeId();
				if (flag4)
				{
					NavigationMoveEvent.Direction direction = ((NavigationMoveEvent)e).direction;
					focusChangeDirection = ((direction == NavigationMoveEvent.Direction.Next) ? VisualElementFocusChangeDirection.right : ((direction == NavigationMoveEvent.Direction.Previous) ? VisualElementFocusChangeDirection.left : FocusChangeDirection.none));
				}
				else
				{
					focusChangeDirection = FocusChangeDirection.none;
				}
			}
			return focusChangeDirection;
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x00080D78 File Offset: 0x0007EF78
		public Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction)
		{
			bool flag = direction == FocusChangeDirection.none || direction == FocusChangeDirection.unspecified;
			Focusable focusable;
			if (flag)
			{
				focusable = currentFocusable;
			}
			else
			{
				VisualElementFocusChangeTarget changeTarget = direction as VisualElementFocusChangeTarget;
				bool flag2 = changeTarget != null;
				if (flag2)
				{
					focusable = changeTarget.target;
				}
				else
				{
					this.DoUpdate();
					bool flag3 = this.m_FocusRing.Count == 0;
					if (flag3)
					{
						focusable = null;
					}
					else
					{
						int previousIndex = this.GetFocusableInternalIndex(currentFocusable);
						bool flag4 = currentFocusable != null && previousIndex == -1;
						if (flag4)
						{
							bool flag5 = direction == VisualElementFocusChangeDirection.right;
							if (flag5)
							{
								return VisualElementFocusRing.GetNextFocusableInTree(currentFocusable as VisualElement);
							}
							bool flag6 = direction == VisualElementFocusChangeDirection.left;
							if (flag6)
							{
								return VisualElementFocusRing.GetPreviousFocusableInTree(currentFocusable as VisualElement);
							}
						}
						int index = 0;
						bool flag7 = direction == VisualElementFocusChangeDirection.right;
						if (flag7)
						{
							index = previousIndex + 1;
							bool flag8 = index == this.m_FocusRing.Count;
							if (flag8)
							{
								index = 0;
							}
							while (this.m_FocusRing[index].m_Focusable.delegatesFocus)
							{
								index++;
								bool flag9 = index == this.m_FocusRing.Count;
								if (flag9)
								{
									return null;
								}
							}
						}
						else
						{
							bool flag10 = direction == VisualElementFocusChangeDirection.left;
							if (flag10)
							{
								index = previousIndex - 1;
								bool flag11 = index < 0;
								if (flag11)
								{
									index = this.m_FocusRing.Count - 1;
								}
								while (this.m_FocusRing[index].m_Focusable.delegatesFocus)
								{
									index--;
									bool flag12 = index == -1;
									if (flag12)
									{
										return null;
									}
								}
							}
						}
						focusable = this.m_FocusRing[index].m_Focusable;
					}
				}
			}
			return focusable;
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x00080F30 File Offset: 0x0007F130
		internal static Focusable GetNextFocusableInTree(VisualElement currentFocusable)
		{
			bool flag = currentFocusable == null;
			Focusable focusable;
			if (flag)
			{
				focusable = null;
			}
			else
			{
				VisualElement ve = currentFocusable.GetNextElementDepthFirst();
				while (!ve.canGrabFocus || ve.tabIndex < 0 || ve.excludeFromFocusRing)
				{
					ve = ve.GetNextElementDepthFirst();
					bool flag2 = ve == null;
					if (flag2)
					{
						ve = currentFocusable.GetRoot();
					}
					bool flag3 = ve == currentFocusable;
					if (flag3)
					{
						return currentFocusable;
					}
				}
				focusable = ve;
			}
			return focusable;
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x00080FA4 File Offset: 0x0007F1A4
		internal static Focusable GetPreviousFocusableInTree(VisualElement currentFocusable)
		{
			bool flag = currentFocusable == null;
			Focusable focusable;
			if (flag)
			{
				focusable = null;
			}
			else
			{
				VisualElement ve = currentFocusable.GetPreviousElementDepthFirst();
				while (!ve.canGrabFocus || ve.tabIndex < 0 || ve.excludeFromFocusRing)
				{
					ve = ve.GetPreviousElementDepthFirst();
					bool flag2 = ve == null;
					if (flag2)
					{
						ve = currentFocusable.GetRoot();
						while (ve.childCount > 0)
						{
							ve = ve.hierarchy.ElementAt(ve.childCount - 1);
						}
					}
					bool flag3 = ve == currentFocusable;
					if (flag3)
					{
						return currentFocusable;
					}
				}
				focusable = ve;
			}
			return focusable;
		}

		// Token: 0x04000FDA RID: 4058
		private readonly VisualElement root;

		// Token: 0x04000FDC RID: 4060
		private List<VisualElementFocusRing.FocusRingRecord> m_FocusRing;

		// Token: 0x020004E0 RID: 1248
		public enum DefaultFocusOrder
		{
			// Token: 0x04000FDE RID: 4062
			ChildOrder,
			// Token: 0x04000FDF RID: 4063
			PositionXY,
			// Token: 0x04000FE0 RID: 4064
			PositionYX
		}

		// Token: 0x020004E1 RID: 1249
		private class FocusRingRecord
		{
			// Token: 0x04000FE1 RID: 4065
			public int m_AutoIndex;

			// Token: 0x04000FE2 RID: 4066
			public Focusable m_Focusable;

			// Token: 0x04000FE3 RID: 4067
			public bool m_IsSlot;

			// Token: 0x04000FE4 RID: 4068
			public List<VisualElementFocusRing.FocusRingRecord> m_ScopeNavigationOrder;
		}
	}
}
