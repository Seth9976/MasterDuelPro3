using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024E RID: 590
	internal class NavigateFocusRing : IFocusRing
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x000447CE File Offset: 0x000429CE
		private FocusController focusController
		{
			get
			{
				return this.m_Root.focusController;
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000447DB File Offset: 0x000429DB
		public NavigateFocusRing(VisualElement root)
		{
			this.m_Root = root;
			this.m_Ring = new VisualElementFocusRing(root, VisualElementFocusRing.DefaultFocusOrder.ChildOrder);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x000447FC File Offset: 0x000429FC
		public FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e)
		{
			bool flag = e.eventTypeId == EventBase<PointerDownEvent>.TypeId();
			if (flag)
			{
				Focusable target;
				bool focusableParentForPointerEvent = this.focusController.GetFocusableParentForPointerEvent(e.elementTarget, out target);
				if (focusableParentForPointerEvent)
				{
					return VisualElementFocusChangeTarget.GetPooled(target);
				}
			}
			bool flag2 = e.eventTypeId == EventBase<NavigationMoveEvent>.TypeId();
			if (flag2)
			{
				switch (((NavigationMoveEvent)e).direction)
				{
				case NavigationMoveEvent.Direction.Left:
					return NavigateFocusRing.Left;
				case NavigationMoveEvent.Direction.Up:
					return NavigateFocusRing.Up;
				case NavigationMoveEvent.Direction.Right:
					return NavigateFocusRing.Right;
				case NavigationMoveEvent.Direction.Down:
					return NavigateFocusRing.Down;
				case NavigationMoveEvent.Direction.Next:
					return NavigateFocusRing.Next;
				case NavigationMoveEvent.Direction.Previous:
					return NavigateFocusRing.Previous;
				}
			}
			return FocusChangeDirection.none;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x000448C4 File Offset: 0x00042AC4
		public virtual Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction)
		{
			bool flag = direction == NavigateFocusRing.Up || direction == NavigateFocusRing.Down || direction == NavigateFocusRing.Right || direction == NavigateFocusRing.Left;
			Focusable focusable;
			if (flag)
			{
				focusable = this.GetNextFocusable2D(currentFocusable, (NavigateFocusRing.ChangeDirection)direction);
			}
			else
			{
				focusable = this.m_Ring.GetNextFocusable(currentFocusable, direction);
			}
			return focusable;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0004491C File Offset: 0x00042B1C
		private Focusable GetNextFocusable2D(Focusable currentFocusable, NavigateFocusRing.ChangeDirection direction)
		{
			VisualElement ve = currentFocusable as VisualElement;
			bool flag = ve == null;
			if (flag)
			{
				ve = this.m_Root;
			}
			Rect panelBounds = this.m_Root.worldBoundingBox;
			Rect panelRect = new Rect(panelBounds.position - Vector2.one, panelBounds.size + Vector2.one * 2f);
			Rect rect = ve.worldBound;
			Rect validRect = new Rect(rect.position - Vector2.one, rect.size + Vector2.one * 2f);
			bool flag2 = direction == NavigateFocusRing.Up;
			if (flag2)
			{
				validRect.yMin = panelRect.yMin;
			}
			else
			{
				bool flag3 = direction == NavigateFocusRing.Down;
				if (flag3)
				{
					validRect.yMax = panelRect.yMax;
				}
				else
				{
					bool flag4 = direction == NavigateFocusRing.Left;
					if (flag4)
					{
						validRect.xMin = panelRect.xMin;
					}
					else
					{
						bool flag5 = direction == NavigateFocusRing.Right;
						if (flag5)
						{
							validRect.xMax = panelRect.xMax;
						}
					}
				}
			}
			NavigateFocusRing.FocusableHierarchyTraversal focusableHierarchyTraversal = default(NavigateFocusRing.FocusableHierarchyTraversal);
			focusableHierarchyTraversal.currentFocusable = ve;
			focusableHierarchyTraversal.direction = direction;
			focusableHierarchyTraversal.validRect = validRect;
			focusableHierarchyTraversal.firstPass = true;
			Focusable best = focusableHierarchyTraversal.GetBestOverall(this.m_Root, null);
			bool flag6 = best != null;
			Focusable focusable;
			if (flag6)
			{
				focusable = best;
			}
			else
			{
				validRect = new Rect(rect.position - Vector2.one, rect.size + Vector2.one * 2f);
				bool flag7 = direction == NavigateFocusRing.Down;
				if (flag7)
				{
					validRect.yMin = panelRect.yMin;
				}
				else
				{
					bool flag8 = direction == NavigateFocusRing.Up;
					if (flag8)
					{
						validRect.yMax = panelRect.yMax;
					}
					else
					{
						bool flag9 = direction == NavigateFocusRing.Right;
						if (flag9)
						{
							validRect.xMin = panelRect.xMin;
						}
						else
						{
							bool flag10 = direction == NavigateFocusRing.Left;
							if (flag10)
							{
								validRect.xMax = panelRect.xMax;
							}
						}
					}
				}
				focusableHierarchyTraversal = default(NavigateFocusRing.FocusableHierarchyTraversal);
				focusableHierarchyTraversal.currentFocusable = ve;
				focusableHierarchyTraversal.direction = direction;
				focusableHierarchyTraversal.validRect = validRect;
				focusableHierarchyTraversal.firstPass = false;
				best = focusableHierarchyTraversal.GetBestOverall(this.m_Root, null);
				bool flag11 = best != null;
				if (flag11)
				{
					focusable = best;
				}
				else
				{
					focusable = currentFocusable;
				}
			}
			return focusable;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00044B84 File Offset: 0x00042D84
		private static bool IsActive(VisualElement v)
		{
			return v.resolvedStyle.display != DisplayStyle.None && v.enabledInHierarchy;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00044BB0 File Offset: 0x00042DB0
		private static bool IsNavigable(Focusable focusable)
		{
			return focusable.canGrabFocus && focusable.tabIndex >= 0 && !focusable.delegatesFocus && !focusable.excludeFromFocusRing;
		}

		// Token: 0x040008EE RID: 2286
		public static readonly NavigateFocusRing.ChangeDirection Left = new NavigateFocusRing.ChangeDirection(1);

		// Token: 0x040008EF RID: 2287
		public static readonly NavigateFocusRing.ChangeDirection Right = new NavigateFocusRing.ChangeDirection(2);

		// Token: 0x040008F0 RID: 2288
		public static readonly NavigateFocusRing.ChangeDirection Up = new NavigateFocusRing.ChangeDirection(3);

		// Token: 0x040008F1 RID: 2289
		public static readonly NavigateFocusRing.ChangeDirection Down = new NavigateFocusRing.ChangeDirection(4);

		// Token: 0x040008F2 RID: 2290
		public static readonly FocusChangeDirection Next = VisualElementFocusChangeDirection.right;

		// Token: 0x040008F3 RID: 2291
		public static readonly FocusChangeDirection Previous = VisualElementFocusChangeDirection.left;

		// Token: 0x040008F4 RID: 2292
		private readonly VisualElement m_Root;

		// Token: 0x040008F5 RID: 2293
		private readonly VisualElementFocusRing m_Ring;

		// Token: 0x0200024F RID: 591
		public class ChangeDirection : FocusChangeDirection
		{
			// Token: 0x06000FFC RID: 4092 RVA: 0x00044C35 File Offset: 0x00042E35
			public ChangeDirection(int i)
				: base(i)
			{
			}
		}

		// Token: 0x02000250 RID: 592
		private struct FocusableHierarchyTraversal
		{
			// Token: 0x06000FFD RID: 4093 RVA: 0x00044C40 File Offset: 0x00042E40
			private bool ValidateHierarchyTraversal(VisualElement v)
			{
				return NavigateFocusRing.IsActive(v) && v.worldBoundingBox.Overlaps(this.validRect);
			}

			// Token: 0x06000FFE RID: 4094 RVA: 0x00044C74 File Offset: 0x00042E74
			private bool ValidateElement(VisualElement v)
			{
				return NavigateFocusRing.IsNavigable(v) && v.worldBound.Overlaps(this.validRect);
			}

			// Token: 0x06000FFF RID: 4095 RVA: 0x00044CA8 File Offset: 0x00042EA8
			private int Order(VisualElement a, VisualElement b)
			{
				Rect ra = a.worldBound;
				Rect rb = b.worldBound;
				int result = this.StrictOrder(ra, rb);
				return (result != 0) ? result : this.TieBreaker(ra, rb);
			}

			// Token: 0x06001000 RID: 4096 RVA: 0x00044CE0 File Offset: 0x00042EE0
			private int StrictOrder(VisualElement a, VisualElement b)
			{
				return this.StrictOrder(a.worldBound, b.worldBound);
			}

			// Token: 0x06001001 RID: 4097 RVA: 0x00044D04 File Offset: 0x00042F04
			private int StrictOrder(Rect ra, Rect rb)
			{
				float diff = 0f;
				bool flag = this.direction == NavigateFocusRing.Up;
				if (flag)
				{
					diff = rb.yMax - ra.yMax;
				}
				else
				{
					bool flag2 = this.direction == NavigateFocusRing.Down;
					if (flag2)
					{
						diff = ra.yMin - rb.yMin;
					}
					else
					{
						bool flag3 = this.direction == NavigateFocusRing.Left;
						if (flag3)
						{
							diff = rb.xMax - ra.xMax;
						}
						else
						{
							bool flag4 = this.direction == NavigateFocusRing.Right;
							if (flag4)
							{
								diff = ra.xMin - rb.xMin;
							}
						}
					}
				}
				bool flag5 = !Mathf.Approximately(diff, 0f);
				int num;
				if (flag5)
				{
					num = ((diff > 0f) ? 1 : (-1));
				}
				else
				{
					num = 0;
				}
				return num;
			}

			// Token: 0x06001002 RID: 4098 RVA: 0x00044DD0 File Offset: 0x00042FD0
			private int TieBreaker(Rect ra, Rect rb)
			{
				Rect rc = this.currentFocusable.worldBound;
				float diff = (ra.min - rc.min).sqrMagnitude - (rb.min - rc.min).sqrMagnitude;
				bool flag = !Mathf.Approximately(diff, 0f);
				int num;
				if (flag)
				{
					num = ((diff > 0f) ? 1 : (-1));
				}
				else
				{
					num = 0;
				}
				return num;
			}

			// Token: 0x06001003 RID: 4099 RVA: 0x00044E4C File Offset: 0x0004304C
			public VisualElement GetBestOverall(VisualElement candidate, VisualElement bestSoFar = null)
			{
				bool flag = !this.ValidateHierarchyTraversal(candidate);
				VisualElement visualElement;
				if (flag)
				{
					visualElement = bestSoFar;
				}
				else
				{
					bool flag2 = this.ValidateElement(candidate);
					if (flag2)
					{
						bool flag3 = (!this.firstPass || this.StrictOrder(candidate, this.currentFocusable) > 0) && (bestSoFar == null || this.Order(bestSoFar, candidate) > 0);
						if (flag3)
						{
							bestSoFar = candidate;
						}
						visualElement = bestSoFar;
					}
					else
					{
						int i = candidate.hierarchy.childCount;
						for (int j = 0; j < i; j++)
						{
							VisualElement child = candidate.hierarchy[j];
							bestSoFar = this.GetBestOverall(child, bestSoFar);
						}
						visualElement = bestSoFar;
					}
				}
				return visualElement;
			}

			// Token: 0x040008F6 RID: 2294
			public VisualElement currentFocusable;

			// Token: 0x040008F7 RID: 2295
			public Rect validRect;

			// Token: 0x040008F8 RID: 2296
			public bool firstPass;

			// Token: 0x040008F9 RID: 2297
			public NavigateFocusRing.ChangeDirection direction;
		}
	}
}
