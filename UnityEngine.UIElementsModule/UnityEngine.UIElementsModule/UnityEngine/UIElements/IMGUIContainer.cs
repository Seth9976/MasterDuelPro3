using System;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.Properties;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x02000264 RID: 612
	public class IMGUIContainer : VisualElement, IDisposable
	{
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x000471E8 File Offset: 0x000453E8
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x00047200 File Offset: 0x00045400
		public Action onGUIHandler
		{
			get
			{
				return this.m_OnGUIHandler;
			}
			set
			{
				bool flag = this.m_OnGUIHandler != value;
				if (flag)
				{
					this.m_OnGUIHandler = value;
					base.IncrementVersion(VersionChangeType.Layout);
					base.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0004723C File Offset: 0x0004543C
		internal ObjectGUIState guiState
		{
			get
			{
				Debug.Assert(!this.useOwnerObjectGUIState, "!useOwnerObjectGUIState");
				bool flag = this.m_ObjectGUIState == null;
				if (flag)
				{
					this.m_ObjectGUIState = new ObjectGUIState();
				}
				return this.m_ObjectGUIState;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x00047282 File Offset: 0x00045482
		// (set) Token: 0x0600109E RID: 4254 RVA: 0x0004728A File Offset: 0x0004548A
		internal Rect lastWorldClip { get; set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x00047294 File Offset: 0x00045494
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x000472AC File Offset: 0x000454AC
		[CreateProperty]
		public bool cullingEnabled
		{
			get
			{
				return this.m_CullingEnabled;
			}
			set
			{
				bool flag = this.m_CullingEnabled == value;
				if (!flag)
				{
					this.m_CullingEnabled = value;
					base.IncrementVersion(VersionChangeType.Repaint);
					base.NotifyPropertyChanged(in IMGUIContainer.cullingEnabledProperty);
				}
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x000472E8 File Offset: 0x000454E8
		private GUILayoutUtility.LayoutCache cache
		{
			get
			{
				bool flag = this.m_Cache == null;
				if (flag)
				{
					this.m_Cache = new GUILayoutUtility.LayoutCache(-1);
				}
				return this.m_Cache;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x0004731C File Offset: 0x0004551C
		private float layoutMeasuredWidth
		{
			get
			{
				return Mathf.Ceil(this.cache.topLevel.maxWidth);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00047344 File Offset: 0x00045544
		private float layoutMeasuredHeight
		{
			get
			{
				return Mathf.Ceil(this.cache.topLevel.maxHeight);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x0004736B File Offset: 0x0004556B
		// (set) Token: 0x060010A5 RID: 4261 RVA: 0x00047374 File Offset: 0x00045574
		[CreateProperty]
		public ContextType contextType
		{
			get
			{
				return this.m_ContextType;
			}
			set
			{
				bool flag = this.m_ContextType == value;
				if (!flag)
				{
					this.m_ContextType = value;
					base.NotifyPropertyChanged(in IMGUIContainer.contextTypeProperty);
				}
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x000473A4 File Offset: 0x000455A4
		internal bool focusOnlyIfHasFocusableControls { get; } = true;

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x000473AC File Offset: 0x000455AC
		public override bool canGrabFocus
		{
			get
			{
				return this.focusOnlyIfHasFocusableControls ? (this.hasFocusableControls && base.canGrabFocus) : base.canGrabFocus;
			}
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x000473D0 File Offset: 0x000455D0
		static IMGUIContainer()
		{
			for (int i = 0; i <= Foldout.ussFoldoutMaxDepth; i++)
			{
				IMGUIContainer.ussFoldoutChildDepthClassNames.Add(IMGUIContainer.ussFoldoutChildDepthClassName + i.ToString());
			}
			IMGUIContainer.ussFoldoutChildDepthClassNames.Add(IMGUIContainer.ussFoldoutChildDepthClassName + "max");
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x000474D8 File Offset: 0x000456D8
		public IMGUIContainer()
			: this(null)
		{
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x000474E4 File Offset: 0x000456E4
		public IMGUIContainer(Action onGUIHandler)
		{
			this.isIMGUIContainer = true;
			base.AddToClassList(IMGUIContainer.ussClassName);
			this.onGUIHandler = onGUIHandler;
			this.contextType = ContextType.Editor;
			this.focusable = true;
			base.requireMeasureFunction = true;
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x000475B0 File Offset: 0x000457B0
		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			BaseRuntimePanel baseRuntimePanel = base.elementPanel as BaseRuntimePanel;
			bool flag = baseRuntimePanel != null && baseRuntimePanel.drawsInCameras;
			if (flag)
			{
				Debug.LogError("IMGUIContainer cannot be used in a panel drawn by cameras.");
			}
			else
			{
				this.lastWorldClip = base.elementPanel.repaintData.currentWorldClip;
				mgc.entryRecorder.DrawImmediate(mgc.parentEntry, new Action(this.DoIMGUIRepaint), this.cullingEnabled);
			}
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00047624 File Offset: 0x00045824
		private void SaveGlobals()
		{
			this.m_GUIGlobals.matrix = GUI.matrix;
			this.m_GUIGlobals.color = GUI.color;
			this.m_GUIGlobals.contentColor = GUI.contentColor;
			this.m_GUIGlobals.backgroundColor = GUI.backgroundColor;
			this.m_GUIGlobals.enabled = GUI.enabled;
			this.m_GUIGlobals.changed = GUI.changed;
			bool flag = Event.current != null;
			if (flag)
			{
				this.m_GUIGlobals.displayIndex = Event.current.displayIndex;
			}
			this.m_GUIGlobals.pixelsPerPoint = GUIUtility.pixelsPerPoint;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x000476C8 File Offset: 0x000458C8
		private void RestoreGlobals()
		{
			GUI.matrix = this.m_GUIGlobals.matrix;
			GUI.color = this.m_GUIGlobals.color;
			GUI.contentColor = this.m_GUIGlobals.contentColor;
			GUI.backgroundColor = this.m_GUIGlobals.backgroundColor;
			GUI.enabled = this.m_GUIGlobals.enabled;
			GUI.changed = this.m_GUIGlobals.changed;
			bool flag = Event.current != null;
			if (flag)
			{
				Event.current.displayIndex = this.m_GUIGlobals.displayIndex;
			}
			GUIUtility.pixelsPerPoint = this.m_GUIGlobals.pixelsPerPoint;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00047774 File Offset: 0x00045974
		private void DoOnGUI(Event evt, Matrix4x4 parentTransform, Rect clippingRect, bool isComputingLayout, Rect layoutSize, Action onGUIHandler, bool canAffectFocus = true)
		{
			bool flag = onGUIHandler == null || base.panel == null;
			if (!flag)
			{
				int guiClipCount = GUIClip.Internal_GetCount();
				int guiDepthBeforeOnGUI = GUIUtility.guiDepth;
				this.SaveGlobals();
				float previousMeasuredWidth = this.layoutMeasuredWidth;
				float previousMeasuredHeight = this.layoutMeasuredHeight;
				UIElementsUtility.BeginContainerGUI(this.cache, evt, this);
				GUI.color = base.playModeTintColor;
				GUIUtility.pixelsPerPoint = base.scaledPixelsPerPoint;
				bool flag2 = Event.current.type != EventType.Layout;
				if (flag2)
				{
					bool flag3 = this.lostFocus;
					if (flag3)
					{
						bool flag4 = this.focusController != null;
						if (flag4)
						{
							bool flag5 = GUIUtility.OwnsId(GUIUtility.keyboardControl);
							if (flag5)
							{
								GUIUtility.keyboardControl = 0;
								this.focusController.imguiKeyboardControl = 0;
							}
						}
						this.lostFocus = false;
					}
					bool flag6 = this.receivedFocus;
					if (flag6)
					{
						bool flag7 = this.hasFocusableControls;
						if (flag7)
						{
							bool flag8 = this.focusChangeDirection != FocusChangeDirection.unspecified && this.focusChangeDirection != FocusChangeDirection.none;
							if (flag8)
							{
								bool flag9;
								if (Event.current.type == EventType.KeyDown)
								{
									char character = Event.current.character;
									flag9 = character == '\t' || character == '\u0019';
								}
								else
								{
									flag9 = false;
								}
								bool flag10 = flag9;
								if (flag10)
								{
									Event.current.Use();
								}
								bool flag11 = this.focusChangeDirection == VisualElementFocusChangeDirection.left;
								if (flag11)
								{
									GUIUtility.SetKeyboardControlToLastControlId();
								}
								else
								{
									bool flag12 = this.focusChangeDirection == VisualElementFocusChangeDirection.right;
									if (flag12)
									{
										GUIUtility.SetKeyboardControlToFirstControlId();
									}
								}
							}
							else
							{
								bool flag13 = GUIUtility.keyboardControl == 0 && this.m_IsFocusDelegated;
								if (flag13)
								{
									GUIUtility.SetKeyboardControlToFirstControlId();
								}
							}
						}
						bool flag14 = this.focusController != null;
						if (flag14)
						{
							bool flag15 = this.focusController.imguiKeyboardControl != GUIUtility.keyboardControl && this.focusChangeDirection != FocusChangeDirection.unspecified;
							if (flag15)
							{
								this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
							}
							this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
						}
						this.receivedFocus = false;
						this.focusChangeDirection = FocusChangeDirection.unspecified;
					}
				}
				EventType originalEventType = Event.current.type;
				bool isExitGUIException = false;
				bool restoreContainerGUIDepth = true;
				int guiClipFinalCount = 0;
				try
				{
					using (new GUIClip.ParentClipScope(parentTransform, clippingRect))
					{
						using (IMGUIContainer.k_OnGUIMarker.Auto())
						{
							onGUIHandler();
						}
					}
				}
				catch (Exception exception)
				{
					bool flag16 = originalEventType == EventType.Layout;
					if (!flag16)
					{
						bool flag17 = guiDepthBeforeOnGUI > 0;
						if (flag17)
						{
							restoreContainerGUIDepth = false;
						}
						throw;
					}
					isExitGUIException = GUIUtility.IsExitGUIException(exception);
					bool flag18 = !isExitGUIException;
					if (flag18)
					{
						Debug.LogException(exception);
					}
				}
				finally
				{
					bool flag19 = Event.current.type != EventType.Layout && canAffectFocus;
					if (flag19)
					{
						bool alreadyUsed = Event.current.type == EventType.Used;
						int currentKeyboardFocus = GUIUtility.keyboardControl;
						int result = GUIUtility.CheckForTabEvent(Event.current);
						bool flag20 = this.focusController != null;
						if (flag20)
						{
							bool flag21 = result < 0 && !alreadyUsed;
							if (flag21)
							{
								Focusable currentFocusedElement = this.focusController.GetLeafFocusedElement();
								Focusable nextFocusedElement = this.focusController.FocusNextInDirection(this, (result == -1) ? VisualElementFocusChangeDirection.right : VisualElementFocusChangeDirection.left);
								bool flag22 = currentFocusedElement == this;
								if (flag22)
								{
									bool flag23 = nextFocusedElement == this;
									if (flag23)
									{
										bool flag24 = result == -2;
										if (flag24)
										{
											GUIUtility.SetKeyboardControlToLastControlId();
										}
										else
										{
											bool flag25 = result == -1;
											if (flag25)
											{
												GUIUtility.SetKeyboardControlToFirstControlId();
											}
										}
										this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
										this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
									}
									else
									{
										GUIUtility.keyboardControl = 0;
										this.focusController.imguiKeyboardControl = 0;
									}
								}
							}
							else
							{
								bool flag26 = result > 0 && !alreadyUsed;
								if (flag26)
								{
									this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
									this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
								}
								else
								{
									bool flag27 = result == 0;
									if (flag27)
									{
										bool flag28 = originalEventType == EventType.MouseDown && !this.focusOnlyIfHasFocusableControls;
										if (flag28)
										{
											this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, true);
										}
										else
										{
											bool flag29 = currentKeyboardFocus != GUIUtility.keyboardControl || originalEventType == EventType.MouseDown;
											if (flag29)
											{
												this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, false);
											}
											else
											{
												bool flag30 = GUIUtility.keyboardControl != this.focusController.imguiKeyboardControl;
												if (flag30)
												{
													this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
													bool flag31 = this.focusController.GetLeafFocusedElement() == this;
													if (flag31)
													{
														this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
													}
													else
													{
														this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, false);
													}
												}
											}
										}
									}
								}
							}
						}
						this.hasFocusableControls = GUIUtility.HasFocusableControls();
					}
					bool flag32 = restoreContainerGUIDepth;
					if (flag32)
					{
						UIElementsUtility.EndContainerGUI(evt, layoutSize);
						this.RestoreGlobals();
					}
					guiClipFinalCount = GUIClip.Internal_GetCount();
					while (GUIClip.Internal_GetCount() > guiClipCount)
					{
						GUIClip.Internal_Pop();
					}
				}
				bool flag33 = evt.type == EventType.Layout && (!Mathf.Approximately(previousMeasuredWidth, this.layoutMeasuredWidth) || !Mathf.Approximately(previousMeasuredHeight, this.layoutMeasuredHeight));
				if (flag33)
				{
					bool flag34 = isComputingLayout && clippingRect == Rect.zero;
					if (flag34)
					{
						base.schedule.Execute(delegate
						{
							base.IncrementVersion(VersionChangeType.Layout);
						});
					}
					else
					{
						base.IncrementVersion(VersionChangeType.Layout);
					}
				}
				bool flag35 = !isExitGUIException;
				if (flag35)
				{
					bool flag36 = evt.type != EventType.Ignore && evt.type != EventType.Used;
					if (flag36)
					{
						bool flag37 = guiClipFinalCount > guiClipCount;
						if (flag37)
						{
							Debug.LogError("GUI Error: You are pushing more GUIClips than you are popping. Make sure they are balanced.");
						}
						else
						{
							bool flag38 = guiClipFinalCount < guiClipCount;
							if (flag38)
							{
								Debug.LogError("GUI Error: You are popping more GUIClips than you are pushing. Make sure they are balanced.");
							}
						}
					}
				}
				bool flag39 = evt.type == EventType.Used;
				if (flag39)
				{
					base.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00047DF8 File Offset: 0x00045FF8
		public void MarkDirtyLayout()
		{
			this.m_RefreshCachedLayout = true;
			base.IncrementVersion(VersionChangeType.Layout);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00047E0C File Offset: 0x0004600C
		private void DoIMGUIRepaint()
		{
			using (IMGUIContainer.k_ImmediateCallbackMarker.Auto())
			{
				Matrix4x4 offset = base.elementPanel.repaintData.currentOffset;
				this.m_CachedClippingRect = VisualElement.ComputeAAAlignedBound(base.worldClip, offset);
				this.m_CachedTransform = offset * base.worldTransform;
				this.HandleIMGUIEvent(base.elementPanel.repaintData.repaintEvent, this.m_CachedTransform, this.m_CachedClippingRect, this.onGUIHandler, true);
			}
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00047EAC File Offset: 0x000460AC
		internal bool SendEventToIMGUI(EventBase evt, bool canAffectFocus = true, bool verifyBounds = true)
		{
			bool flag = evt is IPointerEvent;
			bool flag9;
			if (flag)
			{
				bool flag2 = evt.imguiEvent != null && evt.imguiEvent.isDirectManipulationDevice;
				if (flag2)
				{
					bool sendPointerEvent = false;
					EventType originalEventType = evt.imguiEvent.rawType;
					bool flag3 = evt is PointerDownEvent;
					if (flag3)
					{
						sendPointerEvent = true;
						evt.imguiEvent.type = EventType.TouchDown;
					}
					else
					{
						bool flag4 = evt is PointerUpEvent;
						if (flag4)
						{
							sendPointerEvent = true;
							evt.imguiEvent.type = EventType.TouchUp;
						}
						else
						{
							bool flag5 = evt is PointerMoveEvent && evt.imguiEvent.rawType == EventType.MouseDrag;
							if (flag5)
							{
								sendPointerEvent = true;
								evt.imguiEvent.type = EventType.TouchMove;
							}
							else
							{
								bool flag6 = evt is PointerLeaveEvent;
								if (flag6)
								{
									sendPointerEvent = true;
									evt.imguiEvent.type = EventType.TouchLeave;
								}
								else
								{
									bool flag7 = evt is PointerEnterEvent;
									if (flag7)
									{
										sendPointerEvent = true;
										evt.imguiEvent.type = EventType.TouchEnter;
									}
								}
							}
						}
					}
					bool flag8 = sendPointerEvent;
					if (flag8)
					{
						bool result = this.SendEventToIMGUIRaw(evt, canAffectFocus, verifyBounds);
						evt.imguiEvent.type = originalEventType;
						return result;
					}
				}
				flag9 = false;
			}
			else
			{
				flag9 = this.SendEventToIMGUIRaw(evt, canAffectFocus, verifyBounds);
			}
			return flag9;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00047FF4 File Offset: 0x000461F4
		private bool SendEventToIMGUIRaw(EventBase evt, bool canAffectFocus, bool verifyBounds)
		{
			bool flag = verifyBounds && !this.VerifyBounds(evt);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool result;
				using (new EventDebuggerLogIMGUICall(evt))
				{
					result = this.HandleIMGUIEvent(evt.imguiEvent, canAffectFocus);
				}
				flag2 = result;
			}
			return flag2;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00048058 File Offset: 0x00046258
		private bool VerifyBounds(EventBase evt)
		{
			return this.IsContainerCapturingTheMouse() || !this.IsLocalEvent(evt) || this.IsEventInsideLocalWindow(evt) || IMGUIContainer.IsDockAreaMouseUp(evt);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00048090 File Offset: 0x00046290
		private bool IsContainerCapturingTheMouse()
		{
			IPanel panel = base.panel;
			IMGUIContainer imguicontainer;
			if (panel == null)
			{
				imguicontainer = null;
			}
			else
			{
				EventDispatcher dispatcher = panel.dispatcher;
				imguicontainer = ((dispatcher != null) ? dispatcher.pointerState.GetCapturingElement(PointerId.mousePointerId) : null);
			}
			return this == imguicontainer;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000480D0 File Offset: 0x000462D0
		private bool IsLocalEvent(EventBase evt)
		{
			long evtType = evt.eventTypeId;
			return evtType == EventBase<MouseDownEvent>.TypeId() || evtType == EventBase<MouseUpEvent>.TypeId() || evtType == EventBase<MouseMoveEvent>.TypeId() || evtType == EventBase<PointerDownEvent>.TypeId() || evtType == EventBase<PointerUpEvent>.TypeId() || evtType == EventBase<PointerMoveEvent>.TypeId();
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0004811C File Offset: 0x0004631C
		private bool IsEventInsideLocalWindow(EventBase evt)
		{
			Rect clippingRect = this.GetCurrentClipRect();
			IPointerEvent pointerEvent = evt as IPointerEvent;
			string pointerType = ((pointerEvent != null) ? pointerEvent.pointerType : null);
			bool isDirectManipulationDevice = pointerType == PointerType.touch || pointerType == PointerType.pen;
			return GUIUtility.HitTest(clippingRect, evt.originalMousePosition, isDirectManipulationDevice);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00048174 File Offset: 0x00046374
		private static bool IsDockAreaMouseUp(EventBase evt)
		{
			bool flag;
			if (evt.eventTypeId == EventBase<MouseUpEvent>.TypeId())
			{
				IMGUIContainer elementTarget = evt.elementTarget;
				VisualElement elementTarget2 = evt.elementTarget;
				flag = elementTarget == ((elementTarget2 != null) ? elementTarget2.elementPanel.rootIMGUIContainer : null);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x000481B8 File Offset: 0x000463B8
		internal bool HandleIMGUIEvent(Event e, bool canAffectFocus)
		{
			return this.HandleIMGUIEvent(e, this.onGUIHandler, canAffectFocus);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x000481D8 File Offset: 0x000463D8
		internal bool HandleIMGUIEvent(Event e, Action onGUIHandler, bool canAffectFocus)
		{
			IMGUIContainer.GetCurrentTransformAndClip(this, e, out this.m_CachedTransform, out this.m_CachedClippingRect);
			return this.HandleIMGUIEvent(e, this.m_CachedTransform, this.m_CachedClippingRect, onGUIHandler, canAffectFocus);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00048214 File Offset: 0x00046414
		private bool HandleIMGUIEvent(Event e, Matrix4x4 worldTransform, Rect clippingRect, Action onGUIHandler, bool canAffectFocus)
		{
			bool flag = e == null || onGUIHandler == null || base.elementPanel == null || !base.elementPanel.IMGUIEventInterests.WantsEvent(e.rawType);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				EventType originalEventType = e.rawType;
				bool flag3 = originalEventType != EventType.Layout;
				if (flag3)
				{
					bool flag4 = this.m_RefreshCachedLayout || base.elementPanel.IMGUIEventInterests.WantsLayoutPass(e.rawType);
					if (flag4)
					{
						e.type = EventType.Layout;
						this.DoOnGUI(e, worldTransform, clippingRect, false, base.layout, onGUIHandler, canAffectFocus);
						this.m_RefreshCachedLayout = false;
						e.type = originalEventType;
					}
					else
					{
						this.cache.ResetCursor();
					}
				}
				this.DoOnGUI(e, worldTransform, clippingRect, false, base.layout, onGUIHandler, canAffectFocus);
				bool flag5 = this.newKeyboardFocusControlID > 0;
				if (flag5)
				{
					this.newKeyboardFocusControlID = 0;
					Event focusCommand = new Event
					{
						type = EventType.ExecuteCommand,
						commandName = "NewKeyboardFocus"
					};
					this.HandleIMGUIEvent(focusCommand, true);
				}
				bool flag6 = e.rawType == EventType.Used;
				if (flag6)
				{
					flag2 = true;
				}
				else
				{
					bool flag7 = e.rawType == EventType.MouseUp && this.HasMouseCapture();
					if (flag7)
					{
						GUIUtility.hotControl = 0;
					}
					bool flag8 = base.elementPanel == null;
					if (flag8)
					{
						GUIUtility.ExitGUI();
					}
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0004837F File Offset: 0x0004657F
		[EventInterest(new Type[]
		{
			typeof(NavigationMoveEvent),
			typeof(NavigationSubmitEvent),
			typeof(NavigationCancelEvent),
			typeof(BlurEvent),
			typeof(FocusEvent),
			typeof(DetachFromPanelEvent),
			typeof(AttachToPanelEvent)
		})]
		[EventInterest(EventInterestOptionsInternal.TriggeredByOS)]
		internal override void HandleEventBubbleUpDisabled(EventBase evt)
		{
			this.HandleEventBubbleUp(evt);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0004838C File Offset: 0x0004658C
		[EventInterest(new Type[]
		{
			typeof(NavigationMoveEvent),
			typeof(NavigationSubmitEvent),
			typeof(NavigationCancelEvent),
			typeof(BlurEvent),
			typeof(FocusEvent),
			typeof(DetachFromPanelEvent),
			typeof(AttachToPanelEvent)
		})]
		[EventInterest(EventInterestOptionsInternal.TriggeredByOS)]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			bool flag = (evt.imguiEvent != null && this.SendEventToIMGUI(evt, true, true)) || evt.eventTypeId == EventBase<NavigationMoveEvent>.TypeId() || evt.eventTypeId == EventBase<NavigationSubmitEvent>.TypeId() || evt.eventTypeId == EventBase<NavigationCancelEvent>.TypeId();
			if (flag)
			{
				evt.StopPropagation();
				FocusController focusController = this.focusController;
				if (focusController != null)
				{
					focusController.IgnoreEvent(evt);
				}
			}
			else
			{
				bool flag2 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
				if (flag2)
				{
					this.lostFocus = true;
					base.IncrementVersion(VersionChangeType.Repaint);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
					if (flag3)
					{
						FocusEvent fe = evt as FocusEvent;
						this.receivedFocus = true;
						this.focusChangeDirection = fe.direction;
						this.m_IsFocusDelegated = fe.IsFocusDelegated;
					}
					else
					{
						bool flag4 = evt.eventTypeId == EventBase<DetachFromPanelEvent>.TypeId();
						if (flag4)
						{
							bool flag5 = base.elementPanel != null;
							if (flag5)
							{
								BaseVisualElementPanel elementPanel = base.elementPanel;
								int num = elementPanel.IMGUIContainersCount;
								elementPanel.IMGUIContainersCount = num - 1;
							}
						}
						else
						{
							bool flag6 = evt.eventTypeId == EventBase<AttachToPanelEvent>.TypeId();
							if (flag6)
							{
								bool flag7 = base.elementPanel != null;
								if (flag7)
								{
									BaseVisualElementPanel elementPanel2 = base.elementPanel;
									int num = elementPanel2.IMGUIContainersCount;
									elementPanel2.IMGUIContainersCount = num + 1;
									this.SetFoldoutDepthClass();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000484EC File Offset: 0x000466EC
		private void SetFoldoutDepthClass()
		{
			for (int i = 0; i < IMGUIContainer.ussFoldoutChildDepthClassNames.Count; i++)
			{
				base.RemoveFromClassList(IMGUIContainer.ussFoldoutChildDepthClassNames[i]);
			}
			int depth = this.GetFoldoutDepth();
			bool flag = depth == 0;
			if (!flag)
			{
				depth = Mathf.Min(depth, IMGUIContainer.ussFoldoutChildDepthClassNames.Count - 1);
				base.AddToClassList(IMGUIContainer.ussFoldoutChildDepthClassNames[depth]);
			}
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00048560 File Offset: 0x00046760
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			float measuredWidth = float.NaN;
			float measuredHeight = float.NaN;
			bool restoreCurrentEvent = false;
			bool flag = widthMode != VisualElement.MeasureMode.Exactly || heightMode != VisualElement.MeasureMode.Exactly;
			if (flag)
			{
				bool flag2 = Event.current != null;
				if (flag2)
				{
					IMGUIContainer.s_CurrentEvent.CopyFrom(Event.current);
					restoreCurrentEvent = true;
				}
				IMGUIContainer.s_MeasureEvent.CopyFrom(IMGUIContainer.s_DefaultMeasureEvent);
				Rect layoutRect = base.layout;
				if (widthMode == VisualElement.MeasureMode.Exactly)
				{
					layoutRect.width = desiredWidth;
				}
				if (heightMode == VisualElement.MeasureMode.Exactly)
				{
					layoutRect.height = desiredHeight;
				}
				this.DoOnGUI(IMGUIContainer.s_MeasureEvent, this.m_CachedTransform, this.m_CachedClippingRect, true, layoutRect, this.onGUIHandler, true);
				measuredWidth = this.layoutMeasuredWidth;
				measuredHeight = this.layoutMeasuredHeight;
				bool flag3 = restoreCurrentEvent;
				if (flag3)
				{
					Event.current.CopyFrom(IMGUIContainer.s_CurrentEvent);
				}
			}
			if (widthMode != VisualElement.MeasureMode.Exactly)
			{
				if (widthMode == VisualElement.MeasureMode.AtMost)
				{
					measuredWidth = Mathf.Min(measuredWidth, desiredWidth);
				}
			}
			else
			{
				measuredWidth = desiredWidth;
			}
			if (heightMode != VisualElement.MeasureMode.Exactly)
			{
				if (heightMode == VisualElement.MeasureMode.AtMost)
				{
					measuredHeight = Mathf.Min(measuredHeight, desiredHeight);
				}
			}
			else
			{
				measuredHeight = desiredHeight;
			}
			return new Vector2(measuredWidth, measuredHeight);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0004869C File Offset: 0x0004689C
		private Rect GetCurrentClipRect()
		{
			Rect clipRect = this.lastWorldClip;
			bool flag = clipRect.width == 0f || clipRect.height == 0f;
			if (flag)
			{
				clipRect = base.worldBound;
			}
			return clipRect;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x000486E4 File Offset: 0x000468E4
		private static void GetCurrentTransformAndClip(IMGUIContainer container, Event evt, out Matrix4x4 transform, out Rect clipRect)
		{
			clipRect = container.GetCurrentClipRect();
			transform = container.worldTransform;
			bool flag = evt != null && evt.rawType == EventType.Repaint && container.elementPanel != null;
			if (flag)
			{
				transform = container.elementPanel.repaintData.currentOffset * container.worldTransform;
			}
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00048749 File Offset: 0x00046949
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0004875C File Offset: 0x0004695C
		protected virtual void Dispose(bool disposeManaged)
		{
			if (disposeManaged)
			{
				ObjectGUIState objectGUIState = this.m_ObjectGUIState;
				if (objectGUIState != null)
				{
					objectGUIState.Dispose();
				}
			}
		}

		// Token: 0x04000950 RID: 2384
		internal static readonly BindingId cullingEnabledProperty = "cullingEnabled";

		// Token: 0x04000951 RID: 2385
		internal static readonly BindingId contextTypeProperty = "contextType";

		// Token: 0x04000952 RID: 2386
		private Action m_OnGUIHandler;

		// Token: 0x04000953 RID: 2387
		private ObjectGUIState m_ObjectGUIState;

		// Token: 0x04000954 RID: 2388
		internal bool useOwnerObjectGUIState;

		// Token: 0x04000956 RID: 2390
		private bool m_CullingEnabled = false;

		// Token: 0x04000957 RID: 2391
		private bool m_IsFocusDelegated = false;

		// Token: 0x04000958 RID: 2392
		private bool m_RefreshCachedLayout = true;

		// Token: 0x04000959 RID: 2393
		private GUILayoutUtility.LayoutCache m_Cache = null;

		// Token: 0x0400095A RID: 2394
		private Rect m_CachedClippingRect = Rect.zero;

		// Token: 0x0400095B RID: 2395
		private Matrix4x4 m_CachedTransform = Matrix4x4.identity;

		// Token: 0x0400095C RID: 2396
		private ContextType m_ContextType;

		// Token: 0x0400095D RID: 2397
		private bool lostFocus = false;

		// Token: 0x0400095E RID: 2398
		private bool receivedFocus = false;

		// Token: 0x0400095F RID: 2399
		private FocusChangeDirection focusChangeDirection = FocusChangeDirection.unspecified;

		// Token: 0x04000960 RID: 2400
		private bool hasFocusableControls = false;

		// Token: 0x04000961 RID: 2401
		private int newKeyboardFocusControlID = 0;

		// Token: 0x04000963 RID: 2403
		public static readonly string ussClassName = "unity-imgui-container";

		// Token: 0x04000964 RID: 2404
		internal static readonly string ussFoldoutChildDepthClassName = Foldout.ussClassName + "__" + IMGUIContainer.ussClassName + "--depth-";

		// Token: 0x04000965 RID: 2405
		internal static readonly List<string> ussFoldoutChildDepthClassNames = new List<string>(Foldout.ussFoldoutMaxDepth + 1);

		// Token: 0x04000966 RID: 2406
		private IMGUIContainer.GUIGlobals m_GUIGlobals;

		// Token: 0x04000967 RID: 2407
		private static readonly ProfilerMarker k_OnGUIMarker = new ProfilerMarker("OnGUI");

		// Token: 0x04000968 RID: 2408
		private static readonly ProfilerMarker k_ImmediateCallbackMarker = new ProfilerMarker("IMGUIContainer");

		// Token: 0x04000969 RID: 2409
		private static Event s_DefaultMeasureEvent = new Event
		{
			type = EventType.Layout
		};

		// Token: 0x0400096A RID: 2410
		private static Event s_MeasureEvent = new Event
		{
			type = EventType.Layout
		};

		// Token: 0x0400096B RID: 2411
		private static Event s_CurrentEvent = new Event
		{
			type = EventType.Layout
		};

		// Token: 0x02000265 RID: 613
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<IMGUIContainer, IMGUIContainer.UxmlTraits>
		{
		}

		// Token: 0x02000266 RID: 614
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x060010C5 RID: 4293 RVA: 0x00048796 File Offset: 0x00046996
			public UxmlTraits()
			{
				base.focusIndex.defaultValue = 0;
				base.focusable.defaultValue = true;
			}
		}

		// Token: 0x02000267 RID: 615
		private struct GUIGlobals
		{
			// Token: 0x0400096C RID: 2412
			public Matrix4x4 matrix;

			// Token: 0x0400096D RID: 2413
			public Color color;

			// Token: 0x0400096E RID: 2414
			public Color contentColor;

			// Token: 0x0400096F RID: 2415
			public Color backgroundColor;

			// Token: 0x04000970 RID: 2416
			public bool enabled;

			// Token: 0x04000971 RID: 2417
			public bool changed;

			// Token: 0x04000972 RID: 2418
			public int displayIndex;

			// Token: 0x04000973 RID: 2419
			public float pixelsPerPoint;
		}
	}
}
