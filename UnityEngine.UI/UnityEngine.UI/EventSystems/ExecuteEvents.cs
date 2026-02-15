using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.EventSystems
{
	// Token: 0x020000B7 RID: 183
	public static class ExecuteEvents
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x00019CC5 File Offset: 0x00017EC5
		public static T ValidateEventData<T>(BaseEventData data) where T : class
		{
			if (!(data is T))
			{
				throw new ArgumentException(string.Format("Invalid type: {0} passed to event expecting {1}", data.GetType(), typeof(T)));
			}
			return data as T;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00019D04 File Offset: 0x00017F04
		private static void Execute(IPointerMoveHandler handler, BaseEventData eventData)
		{
			handler.OnPointerMove(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00019D12 File Offset: 0x00017F12
		private static void Execute(IPointerEnterHandler handler, BaseEventData eventData)
		{
			handler.OnPointerEnter(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00019D20 File Offset: 0x00017F20
		private static void Execute(IPointerExitHandler handler, BaseEventData eventData)
		{
			handler.OnPointerExit(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00019D2E File Offset: 0x00017F2E
		private static void Execute(IPointerDownHandler handler, BaseEventData eventData)
		{
			handler.OnPointerDown(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00019D3C File Offset: 0x00017F3C
		private static void Execute(IPointerUpHandler handler, BaseEventData eventData)
		{
			handler.OnPointerUp(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00019D4A File Offset: 0x00017F4A
		private static void Execute(IPointerClickHandler handler, BaseEventData eventData)
		{
			handler.OnPointerClick(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00019D58 File Offset: 0x00017F58
		private static void Execute(IInitializePotentialDragHandler handler, BaseEventData eventData)
		{
			handler.OnInitializePotentialDrag(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00019D66 File Offset: 0x00017F66
		private static void Execute(IBeginDragHandler handler, BaseEventData eventData)
		{
			handler.OnBeginDrag(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00019D74 File Offset: 0x00017F74
		private static void Execute(IDragHandler handler, BaseEventData eventData)
		{
			handler.OnDrag(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00019D82 File Offset: 0x00017F82
		private static void Execute(IEndDragHandler handler, BaseEventData eventData)
		{
			handler.OnEndDrag(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00019D90 File Offset: 0x00017F90
		private static void Execute(IDropHandler handler, BaseEventData eventData)
		{
			handler.OnDrop(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00019D9E File Offset: 0x00017F9E
		private static void Execute(IScrollHandler handler, BaseEventData eventData)
		{
			handler.OnScroll(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00019DAC File Offset: 0x00017FAC
		private static void Execute(IUpdateSelectedHandler handler, BaseEventData eventData)
		{
			handler.OnUpdateSelected(eventData);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00019DB5 File Offset: 0x00017FB5
		private static void Execute(ISelectHandler handler, BaseEventData eventData)
		{
			handler.OnSelect(eventData);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00019DBE File Offset: 0x00017FBE
		private static void Execute(IDeselectHandler handler, BaseEventData eventData)
		{
			handler.OnDeselect(eventData);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00019DC7 File Offset: 0x00017FC7
		private static void Execute(IMoveHandler handler, BaseEventData eventData)
		{
			handler.OnMove(ExecuteEvents.ValidateEventData<AxisEventData>(eventData));
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00019DD5 File Offset: 0x00017FD5
		private static void Execute(ISubmitHandler handler, BaseEventData eventData)
		{
			handler.OnSubmit(eventData);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00019DDE File Offset: 0x00017FDE
		private static void Execute(ICancelHandler handler, BaseEventData eventData)
		{
			handler.OnCancel(eventData);
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00019DE7 File Offset: 0x00017FE7
		public static ExecuteEvents.EventFunction<IPointerMoveHandler> pointerMoveHandler
		{
			get
			{
				return ExecuteEvents.s_PointerMoveHandler;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00019DEE File Offset: 0x00017FEE
		public static ExecuteEvents.EventFunction<IPointerEnterHandler> pointerEnterHandler
		{
			get
			{
				return ExecuteEvents.s_PointerEnterHandler;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00019DF5 File Offset: 0x00017FF5
		public static ExecuteEvents.EventFunction<IPointerExitHandler> pointerExitHandler
		{
			get
			{
				return ExecuteEvents.s_PointerExitHandler;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00019DFC File Offset: 0x00017FFC
		public static ExecuteEvents.EventFunction<IPointerDownHandler> pointerDownHandler
		{
			get
			{
				return ExecuteEvents.s_PointerDownHandler;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00019E03 File Offset: 0x00018003
		public static ExecuteEvents.EventFunction<IPointerUpHandler> pointerUpHandler
		{
			get
			{
				return ExecuteEvents.s_PointerUpHandler;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00019E0A File Offset: 0x0001800A
		public static ExecuteEvents.EventFunction<IPointerClickHandler> pointerClickHandler
		{
			get
			{
				return ExecuteEvents.s_PointerClickHandler;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00019E11 File Offset: 0x00018011
		public static ExecuteEvents.EventFunction<IInitializePotentialDragHandler> initializePotentialDrag
		{
			get
			{
				return ExecuteEvents.s_InitializePotentialDragHandler;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00019E18 File Offset: 0x00018018
		public static ExecuteEvents.EventFunction<IBeginDragHandler> beginDragHandler
		{
			get
			{
				return ExecuteEvents.s_BeginDragHandler;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x00019E1F File Offset: 0x0001801F
		public static ExecuteEvents.EventFunction<IDragHandler> dragHandler
		{
			get
			{
				return ExecuteEvents.s_DragHandler;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00019E26 File Offset: 0x00018026
		public static ExecuteEvents.EventFunction<IEndDragHandler> endDragHandler
		{
			get
			{
				return ExecuteEvents.s_EndDragHandler;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x00019E2D File Offset: 0x0001802D
		public static ExecuteEvents.EventFunction<IDropHandler> dropHandler
		{
			get
			{
				return ExecuteEvents.s_DropHandler;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00019E34 File Offset: 0x00018034
		public static ExecuteEvents.EventFunction<IScrollHandler> scrollHandler
		{
			get
			{
				return ExecuteEvents.s_ScrollHandler;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00019E3B File Offset: 0x0001803B
		public static ExecuteEvents.EventFunction<IUpdateSelectedHandler> updateSelectedHandler
		{
			get
			{
				return ExecuteEvents.s_UpdateSelectedHandler;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00019E42 File Offset: 0x00018042
		public static ExecuteEvents.EventFunction<ISelectHandler> selectHandler
		{
			get
			{
				return ExecuteEvents.s_SelectHandler;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00019E49 File Offset: 0x00018049
		public static ExecuteEvents.EventFunction<IDeselectHandler> deselectHandler
		{
			get
			{
				return ExecuteEvents.s_DeselectHandler;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00019E50 File Offset: 0x00018050
		public static ExecuteEvents.EventFunction<IMoveHandler> moveHandler
		{
			get
			{
				return ExecuteEvents.s_MoveHandler;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00019E57 File Offset: 0x00018057
		public static ExecuteEvents.EventFunction<ISubmitHandler> submitHandler
		{
			get
			{
				return ExecuteEvents.s_SubmitHandler;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00019E5E File Offset: 0x0001805E
		public static ExecuteEvents.EventFunction<ICancelHandler> cancelHandler
		{
			get
			{
				return ExecuteEvents.s_CancelHandler;
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00019E68 File Offset: 0x00018068
		private static void GetEventChain(GameObject root, IList<Transform> eventChain)
		{
			eventChain.Clear();
			if (root == null)
			{
				return;
			}
			Transform t = root.transform;
			while (t != null)
			{
				eventChain.Add(t);
				t = t.parent;
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00019EA8 File Offset: 0x000180A8
		public static bool Execute<T>(GameObject target, BaseEventData eventData, ExecuteEvents.EventFunction<T> functor) where T : IEventSystemHandler
		{
			List<IEventSystemHandler> internalHandlers = CollectionPool<List<IEventSystemHandler>, IEventSystemHandler>.Get();
			ExecuteEvents.GetEventList<T>(target, internalHandlers);
			int internalHandlersCount = internalHandlers.Count;
			int i = 0;
			while (i < internalHandlersCount)
			{
				T arg;
				try
				{
					arg = (T)((object)internalHandlers[i]);
				}
				catch (Exception e)
				{
					IEventSystemHandler temp = internalHandlers[i];
					Debug.LogException(new Exception(string.Format("Type {0} expected {1} received.", typeof(T).Name, temp.GetType().Name), e));
					goto IL_0078;
				}
				goto IL_0066;
				IL_0078:
				i++;
				continue;
				IL_0066:
				try
				{
					functor(arg, eventData);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
				goto IL_0078;
			}
			int count = internalHandlers.Count;
			CollectionPool<List<IEventSystemHandler>, IEventSystemHandler>.Release(internalHandlers);
			return count > 0;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00019F60 File Offset: 0x00018160
		public static GameObject ExecuteHierarchy<T>(GameObject root, BaseEventData eventData, ExecuteEvents.EventFunction<T> callbackFunction) where T : IEventSystemHandler
		{
			ExecuteEvents.GetEventChain(root, ExecuteEvents.s_InternalTransformList);
			int internalTransformListCount = ExecuteEvents.s_InternalTransformList.Count;
			for (int i = 0; i < internalTransformListCount; i++)
			{
				Transform transform = ExecuteEvents.s_InternalTransformList[i];
				if (ExecuteEvents.Execute<T>(transform.gameObject, eventData, callbackFunction))
				{
					return transform.gameObject;
				}
			}
			return null;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00019FB4 File Offset: 0x000181B4
		private static bool ShouldSendToComponent<T>(Component component) where T : IEventSystemHandler
		{
			if (!(component is T))
			{
				return false;
			}
			Behaviour behaviour = component as Behaviour;
			return !(behaviour != null) || behaviour.isActiveAndEnabled;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00019FE8 File Offset: 0x000181E8
		private static void GetEventList<T>(GameObject go, IList<IEventSystemHandler> results) where T : IEventSystemHandler
		{
			if (results == null)
			{
				throw new ArgumentException("Results array is null", "results");
			}
			if (go == null || !go.activeInHierarchy)
			{
				return;
			}
			List<Component> components = CollectionPool<List<Component>, Component>.Get();
			go.GetComponents<Component>(components);
			int componentsCount = components.Count;
			for (int i = 0; i < componentsCount; i++)
			{
				if (ExecuteEvents.ShouldSendToComponent<T>(components[i]))
				{
					results.Add(components[i] as IEventSystemHandler);
				}
			}
			CollectionPool<List<Component>, Component>.Release(components);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001A060 File Offset: 0x00018260
		public static bool CanHandleEvent<T>(GameObject go) where T : IEventSystemHandler
		{
			List<IEventSystemHandler> internalHandlers = CollectionPool<List<IEventSystemHandler>, IEventSystemHandler>.Get();
			ExecuteEvents.GetEventList<T>(go, internalHandlers);
			int count = internalHandlers.Count;
			CollectionPool<List<IEventSystemHandler>, IEventSystemHandler>.Release(internalHandlers);
			return count != 0;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001A08C File Offset: 0x0001828C
		public static GameObject GetEventHandler<T>(GameObject root) where T : IEventSystemHandler
		{
			if (root == null)
			{
				return null;
			}
			Transform t = root.transform;
			while (t != null)
			{
				if (ExecuteEvents.CanHandleEvent<T>(t.gameObject))
				{
					return t.gameObject;
				}
				t = t.parent;
			}
			return null;
		}

		// Token: 0x04000303 RID: 771
		private static readonly ExecuteEvents.EventFunction<IPointerMoveHandler> s_PointerMoveHandler = new ExecuteEvents.EventFunction<IPointerMoveHandler>(ExecuteEvents.Execute);

		// Token: 0x04000304 RID: 772
		private static readonly ExecuteEvents.EventFunction<IPointerEnterHandler> s_PointerEnterHandler = new ExecuteEvents.EventFunction<IPointerEnterHandler>(ExecuteEvents.Execute);

		// Token: 0x04000305 RID: 773
		private static readonly ExecuteEvents.EventFunction<IPointerExitHandler> s_PointerExitHandler = new ExecuteEvents.EventFunction<IPointerExitHandler>(ExecuteEvents.Execute);

		// Token: 0x04000306 RID: 774
		private static readonly ExecuteEvents.EventFunction<IPointerDownHandler> s_PointerDownHandler = new ExecuteEvents.EventFunction<IPointerDownHandler>(ExecuteEvents.Execute);

		// Token: 0x04000307 RID: 775
		private static readonly ExecuteEvents.EventFunction<IPointerUpHandler> s_PointerUpHandler = new ExecuteEvents.EventFunction<IPointerUpHandler>(ExecuteEvents.Execute);

		// Token: 0x04000308 RID: 776
		private static readonly ExecuteEvents.EventFunction<IPointerClickHandler> s_PointerClickHandler = new ExecuteEvents.EventFunction<IPointerClickHandler>(ExecuteEvents.Execute);

		// Token: 0x04000309 RID: 777
		private static readonly ExecuteEvents.EventFunction<IInitializePotentialDragHandler> s_InitializePotentialDragHandler = new ExecuteEvents.EventFunction<IInitializePotentialDragHandler>(ExecuteEvents.Execute);

		// Token: 0x0400030A RID: 778
		private static readonly ExecuteEvents.EventFunction<IBeginDragHandler> s_BeginDragHandler = new ExecuteEvents.EventFunction<IBeginDragHandler>(ExecuteEvents.Execute);

		// Token: 0x0400030B RID: 779
		private static readonly ExecuteEvents.EventFunction<IDragHandler> s_DragHandler = new ExecuteEvents.EventFunction<IDragHandler>(ExecuteEvents.Execute);

		// Token: 0x0400030C RID: 780
		private static readonly ExecuteEvents.EventFunction<IEndDragHandler> s_EndDragHandler = new ExecuteEvents.EventFunction<IEndDragHandler>(ExecuteEvents.Execute);

		// Token: 0x0400030D RID: 781
		private static readonly ExecuteEvents.EventFunction<IDropHandler> s_DropHandler = new ExecuteEvents.EventFunction<IDropHandler>(ExecuteEvents.Execute);

		// Token: 0x0400030E RID: 782
		private static readonly ExecuteEvents.EventFunction<IScrollHandler> s_ScrollHandler = new ExecuteEvents.EventFunction<IScrollHandler>(ExecuteEvents.Execute);

		// Token: 0x0400030F RID: 783
		private static readonly ExecuteEvents.EventFunction<IUpdateSelectedHandler> s_UpdateSelectedHandler = new ExecuteEvents.EventFunction<IUpdateSelectedHandler>(ExecuteEvents.Execute);

		// Token: 0x04000310 RID: 784
		private static readonly ExecuteEvents.EventFunction<ISelectHandler> s_SelectHandler = new ExecuteEvents.EventFunction<ISelectHandler>(ExecuteEvents.Execute);

		// Token: 0x04000311 RID: 785
		private static readonly ExecuteEvents.EventFunction<IDeselectHandler> s_DeselectHandler = new ExecuteEvents.EventFunction<IDeselectHandler>(ExecuteEvents.Execute);

		// Token: 0x04000312 RID: 786
		private static readonly ExecuteEvents.EventFunction<IMoveHandler> s_MoveHandler = new ExecuteEvents.EventFunction<IMoveHandler>(ExecuteEvents.Execute);

		// Token: 0x04000313 RID: 787
		private static readonly ExecuteEvents.EventFunction<ISubmitHandler> s_SubmitHandler = new ExecuteEvents.EventFunction<ISubmitHandler>(ExecuteEvents.Execute);

		// Token: 0x04000314 RID: 788
		private static readonly ExecuteEvents.EventFunction<ICancelHandler> s_CancelHandler = new ExecuteEvents.EventFunction<ICancelHandler>(ExecuteEvents.Execute);

		// Token: 0x04000315 RID: 789
		private static readonly List<Transform> s_InternalTransformList = new List<Transform>(30);

		// Token: 0x020000B8 RID: 184
		// (Invoke) Token: 0x060006BF RID: 1727
		public delegate void EventFunction<T1>(T1 handler, BaseEventData eventData);
	}
}
