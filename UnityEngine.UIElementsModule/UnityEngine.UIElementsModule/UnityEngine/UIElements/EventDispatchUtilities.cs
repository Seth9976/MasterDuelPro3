using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Pool;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D4 RID: 468
	internal static class EventDispatchUtilities
	{
		// Token: 0x06000D24 RID: 3364 RVA: 0x0003DA50 File Offset: 0x0003BC50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void PropagateEvent(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement target, bool isCapturingTarget)
		{
			IPointerEventInternal pointerEventInternal = evt as IPointerEventInternal;
			EventBase compatibilityEvt = ((pointerEventInternal != null) ? pointerEventInternal.compatibilityMouseEvent : null) as EventBase;
			bool flag = compatibilityEvt != null;
			if (flag)
			{
				EventDispatchUtilities.HandleEventAcrossPropagationPathWithCompatibilityEvent(evt, compatibilityEvt, panel, target, isCapturingTarget);
			}
			else
			{
				EventDispatchUtilities.HandleEventAcrossPropagationPath(evt, panel, target, isCapturingTarget);
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0003DA9C File Offset: 0x0003BC9C
		public static void HandleEventAtTargetAndDefaultPhase(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement target)
		{
			int eventCategories = evt.eventCategories;
			bool flag = !target.HasSelfEventInterests(eventCategories) || evt.isPropagationStopped;
			if (!flag)
			{
				evt.currentTarget = target;
				try
				{
					IPointerEventInternal pe = evt as IPointerEventInternal;
					Debug.Assert(pe == null || pe.compatibilityMouseEvent == null, "!(evt is IPointerEventInternal pe) || pe.compatibilityMouseEvent == null");
					evt.propagationPhase = PropagationPhase.TrickleDown;
					bool flag2 = target.HasTrickleDownEventCallbacks(eventCategories);
					if (flag2)
					{
						EventDispatchUtilities.HandleEvent_TrickleDownCallbacks(evt, panel, target);
						bool isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
						if (isImmediatePropagationStopped)
						{
							return;
						}
					}
					bool flag3 = target.HasTrickleDownHandleEvent(eventCategories);
					if (flag3)
					{
						EventDispatchUtilities.HandleEvent_TrickleDownHandleEvent(evt, panel, target, EventDispatchUtilities.Disabled(evt, target));
					}
					bool isPropagationStopped = evt.isPropagationStopped;
					if (!isPropagationStopped)
					{
						evt.propagationPhase = PropagationPhase.BubbleUp;
						bool flag4 = target.HasBubbleUpHandleEvent(eventCategories);
						if (flag4)
						{
							bool disabled = EventDispatchUtilities.Disabled(evt, target);
							EventDispatchUtilities.HandleEvent_DefaultActionAtTarget(evt, panel, target, disabled);
							EventDispatchUtilities.HandleEvent_BubbleUpHandleEvent(evt, panel, target, disabled);
							EventDispatchUtilities.HandleEvent_DefaultAction(evt, panel, target, disabled);
							bool isImmediatePropagationStopped2 = evt.isImmediatePropagationStopped;
							if (isImmediatePropagationStopped2)
							{
								return;
							}
						}
						bool flag5 = target.HasBubbleUpEventCallbacks(eventCategories);
						if (flag5)
						{
							EventDispatchUtilities.HandleEvent_BubbleUpCallbacks(evt, panel, target);
						}
					}
				}
				finally
				{
					evt.currentTarget = null;
					evt.propagationPhase = PropagationPhase.None;
				}
			}
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0003DBE0 File Offset: 0x0003BDE0
		private static void HandleEventAcrossPropagationPath(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement target, bool isCapturingTarget)
		{
			int eventCategories = evt.eventCategories;
			bool flag = !target.HasParentEventInterests(eventCategories) || evt.isPropagationStopped;
			if (!flag)
			{
				using (PropagationPaths path = PropagationPaths.Build(target, evt, eventCategories))
				{
					try
					{
						Debug.Assert(!evt.dispatch, "Event is being dispatched recursively.");
						evt.dispatch = true;
						evt.propagationPhase = PropagationPhase.TrickleDown;
						int i = path.trickleDownPath.Count - 1;
						bool flag2 = isCapturingTarget && i >= 0;
						if (flag2)
						{
							i = ((path.trickleDownPath[0] == target) ? 0 : (-1));
						}
						while (i >= 0)
						{
							VisualElement element = path.trickleDownPath[i];
							evt.currentTarget = element;
							bool flag3 = element.HasTrickleDownEventCallbacks(eventCategories);
							if (flag3)
							{
								EventDispatchUtilities.HandleEvent_TrickleDownCallbacks(evt, panel, element);
								bool isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
								if (isImmediatePropagationStopped)
								{
									return;
								}
							}
							bool flag4 = element.HasTrickleDownHandleEvent(eventCategories);
							if (flag4)
							{
								EventDispatchUtilities.HandleEvent_TrickleDownHandleEvent(evt, panel, element, EventDispatchUtilities.Disabled(evt, element));
							}
							bool isPropagationStopped = evt.isPropagationStopped;
							if (isPropagationStopped)
							{
								return;
							}
							i--;
						}
						evt.propagationPhase = PropagationPhase.BubbleUp;
						foreach (VisualElement element2 in path.bubbleUpPath)
						{
							evt.currentTarget = element2;
							bool flag5 = element2.HasBubbleUpHandleEvent(eventCategories);
							if (flag5)
							{
								EventDispatchUtilities.HandleEvent_BubbleUpAllDefaultActions(evt, panel, element2, EventDispatchUtilities.Disabled(evt, element2), isCapturingTarget);
								bool isImmediatePropagationStopped2 = evt.isImmediatePropagationStopped;
								if (isImmediatePropagationStopped2)
								{
									break;
								}
							}
							bool flag6 = element2.HasBubbleUpEventCallbacks(eventCategories) && (!isCapturingTarget || element2 == target);
							if (flag6)
							{
								EventDispatchUtilities.HandleEvent_BubbleUpCallbacks(evt, panel, element2);
							}
							bool isPropagationStopped2 = evt.isPropagationStopped;
							if (isPropagationStopped2)
							{
								break;
							}
						}
					}
					finally
					{
						evt.currentTarget = null;
						evt.propagationPhase = PropagationPhase.None;
						evt.dispatch = false;
					}
				}
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0003DE30 File Offset: 0x0003C030
		private static void HandleEventAcrossPropagationPathWithCompatibilityEvent(EventBase evt, [NotNull] EventBase compatibilityEvt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement target, bool isCapturingTarget)
		{
			int eventCategories = evt.eventCategories | compatibilityEvt.eventCategories;
			bool flag = !target.HasParentEventInterests(eventCategories) || evt.isPropagationStopped || compatibilityEvt.isPropagationStopped;
			if (!flag)
			{
				compatibilityEvt.elementTarget = target;
				compatibilityEvt.skipDisabledElements = evt.skipDisabledElements;
				using (PropagationPaths path = PropagationPaths.Build(target, evt, eventCategories))
				{
					try
					{
						Debug.Assert(!evt.dispatch, "Event is being dispatched recursively.");
						evt.dispatch = true;
						evt.propagationPhase = PropagationPhase.TrickleDown;
						compatibilityEvt.propagationPhase = PropagationPhase.TrickleDown;
						int i = path.trickleDownPath.Count - 1;
						bool flag2 = isCapturingTarget && i >= 0;
						if (flag2)
						{
							i = ((path.trickleDownPath[0] == target) ? 0 : (-1));
						}
						while (i >= 0)
						{
							VisualElement element = path.trickleDownPath[i];
							evt.currentTarget = element;
							compatibilityEvt.currentTarget = element;
							bool flag3 = element.HasTrickleDownEventCallbacks(eventCategories);
							if (flag3)
							{
								EventDispatchUtilities.HandleEvent_TrickleDownCallbacks(evt, panel, element);
								bool isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
								if (isImmediatePropagationStopped)
								{
									return;
								}
								bool flag4 = panel.ShouldSendCompatibilityMouseEvents((IPointerEvent)evt);
								if (flag4)
								{
									EventDispatchUtilities.HandleEvent_TrickleDownCallbacks(compatibilityEvt, panel, element);
									bool isImmediatePropagationStopped2 = evt.isImmediatePropagationStopped;
									if (isImmediatePropagationStopped2)
									{
										return;
									}
								}
							}
							bool flag5 = element.HasTrickleDownHandleEvent(eventCategories);
							if (flag5)
							{
								bool disabled = EventDispatchUtilities.Disabled(evt, element);
								EventDispatchUtilities.HandleEvent_TrickleDownHandleEvent(evt, panel, element, disabled);
								bool isImmediatePropagationStopped3 = evt.isImmediatePropagationStopped;
								if (isImmediatePropagationStopped3)
								{
									return;
								}
								bool flag6 = panel.ShouldSendCompatibilityMouseEvents((IPointerEvent)evt);
								if (flag6)
								{
									EventDispatchUtilities.HandleEvent_TrickleDownHandleEvent(compatibilityEvt, panel, element, disabled);
									bool isImmediatePropagationStopped4 = compatibilityEvt.isImmediatePropagationStopped;
									if (isImmediatePropagationStopped4)
									{
										return;
									}
								}
							}
							bool flag7 = evt.isPropagationStopped || compatibilityEvt.isPropagationStopped;
							if (flag7)
							{
								return;
							}
							i--;
						}
						evt.propagationPhase = PropagationPhase.BubbleUp;
						compatibilityEvt.propagationPhase = PropagationPhase.BubbleUp;
						foreach (VisualElement element2 in path.bubbleUpPath)
						{
							evt.currentTarget = element2;
							compatibilityEvt.currentTarget = element2;
							bool flag8 = element2.HasBubbleUpHandleEvent(eventCategories);
							if (flag8)
							{
								bool disabled2 = EventDispatchUtilities.Disabled(evt, element2);
								EventDispatchUtilities.HandleEvent_BubbleUpAllDefaultActions(evt, panel, element2, disabled2, isCapturingTarget);
								bool isImmediatePropagationStopped5 = evt.isImmediatePropagationStopped;
								if (isImmediatePropagationStopped5)
								{
									break;
								}
								bool flag9 = panel.ShouldSendCompatibilityMouseEvents((IPointerEvent)evt);
								if (flag9)
								{
									EventDispatchUtilities.HandleEvent_BubbleUpAllDefaultActions(compatibilityEvt, panel, element2, disabled2, isCapturingTarget);
									bool isImmediatePropagationStopped6 = compatibilityEvt.isImmediatePropagationStopped;
									if (isImmediatePropagationStopped6)
									{
										break;
									}
								}
							}
							bool flag10 = element2.HasBubbleUpEventCallbacks(eventCategories) && (!isCapturingTarget || element2 == target);
							if (flag10)
							{
								EventDispatchUtilities.HandleEvent_BubbleUpCallbacks(evt, panel, element2);
								bool isImmediatePropagationStopped7 = evt.isImmediatePropagationStopped;
								if (isImmediatePropagationStopped7)
								{
									break;
								}
								bool flag11 = panel.ShouldSendCompatibilityMouseEvents((IPointerEvent)evt);
								if (flag11)
								{
									EventDispatchUtilities.HandleEvent_BubbleUpCallbacks(compatibilityEvt, panel, element2);
									bool isImmediatePropagationStopped8 = compatibilityEvt.isImmediatePropagationStopped;
									if (isImmediatePropagationStopped8)
									{
										break;
									}
								}
							}
							bool flag12 = evt.isPropagationStopped || compatibilityEvt.isPropagationStopped;
							if (flag12)
							{
								break;
							}
						}
					}
					finally
					{
						evt.currentTarget = null;
						evt.propagationPhase = PropagationPhase.None;
						compatibilityEvt.currentTarget = null;
						compatibilityEvt.propagationPhase = PropagationPhase.None;
						evt.dispatch = false;
					}
				}
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0003E1DC File Offset: 0x0003C3DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void HandleEvent_DefaultActionAtTarget(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement element, bool disabled)
		{
			bool flag = element.elementPanel != panel;
			if (!flag)
			{
				using (new EventDebuggerLogExecuteDefaultAction(evt))
				{
					if (disabled)
					{
						element.ExecuteDefaultActionDisabledAtTargetInternal(evt);
					}
					else
					{
						element.ExecuteDefaultActionAtTargetInternal(evt);
					}
				}
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0003E240 File Offset: 0x0003C440
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void HandleEvent_DefaultAction(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement element, bool disabled)
		{
			bool flag = element.elementPanel != panel;
			if (!flag)
			{
				using (new EventDebuggerLogExecuteDefaultAction(evt))
				{
					if (disabled)
					{
						element.ExecuteDefaultActionDisabledInternal(evt);
					}
					else
					{
						element.ExecuteDefaultActionInternal(evt);
					}
				}
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0003E2A4 File Offset: 0x0003C4A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void HandleEvent_TrickleDownCallbacks(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement element)
		{
			EventCallbackRegistry callbackRegistry = element.m_CallbackRegistry;
			if (callbackRegistry != null)
			{
				callbackRegistry.m_TrickleDownCallbacks.Invoke(evt, panel, element);
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0003E2C1 File Offset: 0x0003C4C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void HandleEvent_BubbleUpCallbacks(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement element)
		{
			EventCallbackRegistry callbackRegistry = element.m_CallbackRegistry;
			if (callbackRegistry != null)
			{
				callbackRegistry.m_BubbleUpCallbacks.Invoke(evt, panel, element);
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0003E2E0 File Offset: 0x0003C4E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void HandleEvent_TrickleDownHandleEvent(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement element, bool disabled)
		{
			bool flag = element.elementPanel != panel;
			if (!flag)
			{
				if (disabled)
				{
					element.HandleEventTrickleDownDisabled(evt);
				}
				else
				{
					element.HandleEventTrickleDownInternal(evt);
				}
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003E318 File Offset: 0x0003C518
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void HandleEvent_BubbleUpHandleEvent(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement element, bool disabled)
		{
			bool flag = element.elementPanel != panel;
			if (!flag)
			{
				if (disabled)
				{
					element.HandleEventBubbleUpDisabled(evt);
				}
				else
				{
					element.HandleEventBubbleUpInternal(evt);
				}
			}
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0003E350 File Offset: 0x0003C550
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void HandleEvent_BubbleUpAllDefaultActions(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement element, bool disabled, bool isCapturingTarget)
		{
			bool handleEvent = element == evt.target || !isCapturingTarget;
			bool executeDefault = element == evt.target || element.isCompositeRoot;
			bool flag = executeDefault;
			if (flag)
			{
				EventDispatchUtilities.HandleEvent_DefaultActionAtTarget(evt, panel, element, disabled);
			}
			bool flag2 = handleEvent;
			if (flag2)
			{
				EventDispatchUtilities.HandleEvent_BubbleUpHandleEvent(evt, panel, element, disabled);
			}
			bool flag3 = executeDefault;
			if (flag3)
			{
				EventDispatchUtilities.HandleEvent_DefaultAction(evt, panel, element, disabled);
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0003E3B4 File Offset: 0x0003C5B4
		private static bool Disabled([NotNull] EventBase evt, [NotNull] VisualElement target)
		{
			return evt.skipDisabledElements && !target.enabledInHierarchy;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0003E3DC File Offset: 0x0003C5DC
		public static void DispatchToFocusedElementOrPanelRoot(EventBase evt, [NotNull] BaseVisualElementPanel panel)
		{
			bool propagateToIMGUI = false;
			VisualElement target = evt.elementTarget;
			bool flag = target == null;
			if (flag)
			{
				Focusable leafFocusedElement = panel.focusController.GetLeafFocusedElement();
				VisualElement ve = leafFocusedElement as VisualElement;
				bool flag2 = ve != null;
				if (flag2)
				{
					target = ve;
				}
				else
				{
					target = panel.visualTree;
					propagateToIMGUI = true;
				}
				VisualElement capturingElement = panel.GetCapturingElement(PointerId.mousePointerId) as VisualElement;
				bool flag3 = capturingElement != null && capturingElement != target && !capturingElement.Contains(target) && capturingElement.HasSelfEventInterests(evt.eventCategories);
				if (flag3)
				{
					evt.elementTarget = capturingElement;
					bool skipDisabledElements = evt.skipDisabledElements;
					evt.skipDisabledElements = false;
					EventDispatchUtilities.HandleEventAtTargetAndDefaultPhase(evt, panel, capturingElement);
					evt.skipDisabledElements = skipDisabledElements;
				}
				evt.elementTarget = target;
			}
			EventDispatchUtilities.PropagateEvent(evt, panel, target, false);
			bool flag4 = propagateToIMGUI && evt.propagateToIMGUI;
			if (flag4)
			{
				EventDispatchUtilities.PropagateToRemainingIMGUIContainers(evt, panel.visualTree);
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0003E4CC File Offset: 0x0003C6CC
		public static void DispatchToElementUnderPointerOrPanelRoot(EventBase evt, [NotNull] BaseVisualElementPanel panel, int pointerId, Vector2 position)
		{
			VisualElement topElement = panel.RecomputeTopElementUnderPointer(pointerId, position, evt);
			bool propagateToIMGUI = false;
			VisualElement target = evt.elementTarget;
			bool flag = target == null;
			if (flag)
			{
				target = topElement;
				bool flag2 = target == null;
				if (flag2)
				{
					target = panel.visualTree;
					propagateToIMGUI = true;
				}
				evt.elementTarget = target;
			}
			EventDispatchUtilities.PropagateEvent(evt, panel, target, false);
			bool flag3 = propagateToIMGUI && evt.propagateToIMGUI;
			if (flag3)
			{
				EventDispatchUtilities.PropagateToRemainingIMGUIContainers(evt, panel.visualTree);
			}
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0003E540 File Offset: 0x0003C740
		public static void DispatchToCachedElementUnderPointerOrPanelRoot(EventBase evt, [NotNull] BaseVisualElementPanel panel, int pointerId, Vector2 position)
		{
			bool propagateToIMGUI = false;
			VisualElement target = evt.elementTarget;
			bool flag = target == null;
			if (flag)
			{
				target = panel.GetTopElementUnderPointer(pointerId);
				bool flag2 = target == null;
				if (flag2)
				{
					target = panel.visualTree;
					propagateToIMGUI = true;
				}
				evt.elementTarget = target;
			}
			EventDispatchUtilities.PropagateEvent(evt, panel, target, false);
			bool flag3 = propagateToIMGUI && evt.propagateToIMGUI;
			if (flag3)
			{
				EventDispatchUtilities.PropagateToRemainingIMGUIContainers(evt, panel.visualTree);
			}
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0003E5B0 File Offset: 0x0003C7B0
		public static void DispatchToAssignedTarget(EventBase evt, [NotNull] BaseVisualElementPanel panel)
		{
			VisualElement target = evt.elementTarget;
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentException(string.Format("Event target not set. Event type {0} requires a target.", evt.GetType()));
			}
			EventDispatchUtilities.PropagateEvent(evt, panel, target, false);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0003E5F0 File Offset: 0x0003C7F0
		public static void DefaultDispatch(EventBase evt, [NotNull] BaseVisualElementPanel panel)
		{
			VisualElement target = evt.elementTarget;
			bool flag = target == null;
			if (!flag)
			{
				bool bubblesOrTricklesDown = evt.bubblesOrTricklesDown;
				if (bubblesOrTricklesDown)
				{
					EventDispatchUtilities.PropagateEvent(evt, panel, target, false);
				}
				else
				{
					EventDispatchUtilities.HandleEventAtTargetAndDefaultPhase(evt, panel, target);
				}
			}
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0003E630 File Offset: 0x0003C830
		public static void DispatchToCapturingElementOrElementUnderPointer(EventBase evt, [NotNull] BaseVisualElementPanel panel, int pointerId, Vector2 position)
		{
			panel.RecomputeTopElementUnderPointer(pointerId, position, evt);
			bool flag = EventDispatchUtilities.DispatchToCapturingElement(evt, panel, pointerId, position);
			if (!flag)
			{
				EventDispatchUtilities.DispatchToCachedElementUnderPointerOrPanelRoot(evt, panel, pointerId, position);
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0003E664 File Offset: 0x0003C864
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool DispatchToCapturingElement(EventBase evt, [NotNull] BaseVisualElementPanel panel, int pointerId, Vector2 position)
		{
			VisualElement capturingElement = panel.GetCapturingElement(pointerId) as VisualElement;
			bool flag = capturingElement == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = capturingElement.panel == null;
				if (flag3)
				{
					panel.ReleasePointer(pointerId);
					flag2 = false;
				}
				else
				{
					bool flag4 = evt.target != null && evt.target != capturingElement;
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						bool flag5 = capturingElement.panel != panel;
						if (flag5)
						{
							flag2 = false;
						}
						else
						{
							evt.skipDisabledElements = false;
							evt.elementTarget = capturingElement;
							EventDispatchUtilities.PropagateEvent(evt, panel, capturingElement, true);
							flag2 = true;
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0003E700 File Offset: 0x0003C900
		internal static void DispatchToPanelRoot(EventBase evt, [NotNull] BaseVisualElementPanel panel)
		{
			VisualElement target = (evt.elementTarget = panel.visualTree);
			EventDispatchUtilities.PropagateEvent(evt, panel, target, false);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0003E72C File Offset: 0x0003C92C
		internal static void PropagateToRemainingIMGUIContainers(EventBase evt, [NotNull] VisualElement root)
		{
			bool flag = evt.imguiEvent != null && root.elementPanel.contextType > ContextType.Player;
			if (flag)
			{
				EventDispatchUtilities.PropagateToRemainingIMGUIContainerRecursive(evt, root);
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0003E760 File Offset: 0x0003C960
		private static void PropagateToRemainingIMGUIContainerRecursive(EventBase evt, [NotNull] VisualElement root)
		{
			bool isIMGUIContainer = root.isIMGUIContainer;
			if (isIMGUIContainer)
			{
				bool flag = root != evt.target;
				if (flag)
				{
					IMGUIContainer imContainer = (IMGUIContainer)root;
					VisualElement elementTarget = evt.elementTarget;
					bool targetIsFocusable = elementTarget != null && elementTarget.focusable;
					bool flag2 = imContainer.SendEventToIMGUI(evt, !targetIsFocusable, true);
					if (flag2)
					{
						evt.StopPropagation();
					}
					bool flag3 = evt.imguiEvent.rawType == EventType.Used;
					if (flag3)
					{
						Debug.Assert(evt.isPropagationStopped, "evt.isPropagationStopped");
					}
				}
			}
			else
			{
				bool flag4 = root.imguiContainerDescendantCount > 0;
				if (flag4)
				{
					List<VisualElement> childrenToNotify;
					using (CollectionPool<List<VisualElement>, VisualElement>.Get(out childrenToNotify))
					{
						childrenToNotify.AddRange(root.hierarchy.children);
						foreach (VisualElement child in childrenToNotify)
						{
							bool flag5 = child.hierarchy.parent != root;
							if (!flag5)
							{
								EventDispatchUtilities.PropagateToRemainingIMGUIContainerRecursive(evt, child);
								bool isPropagationStopped = evt.isPropagationStopped;
								if (isPropagationStopped)
								{
									break;
								}
							}
						}
					}
				}
			}
		}
	}
}
