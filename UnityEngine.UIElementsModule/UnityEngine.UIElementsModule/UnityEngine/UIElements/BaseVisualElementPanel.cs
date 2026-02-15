using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.UIElements.Layout;

namespace UnityEngine.UIElements
{
	// Token: 0x02000288 RID: 648
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal abstract class BaseVisualElementPanel : IPanel, IDisposable, IGroupBox
	{
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x0600111C RID: 4380
		// (set) Token: 0x0600111D RID: 4381
		public abstract EventInterests IMGUIEventInterests { get; set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600111E RID: 4382
		// (set) Token: 0x0600111F RID: 4383
		public abstract ScriptableObject ownerObject { get; protected set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001120 RID: 4384
		public abstract SavePersistentViewData saveViewData { get; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001121 RID: 4385
		public abstract GetViewDataDictionary getViewDataDictionary { get; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001122 RID: 4386
		// (set) Token: 0x06001123 RID: 4387
		public abstract int IMGUIContainersCount { get; set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001124 RID: 4388
		// (set) Token: 0x06001125 RID: 4389
		public abstract FocusController focusController { get; set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001126 RID: 4390
		public abstract IMGUIContainer rootIMGUIContainer { get; }

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001127 RID: 4391 RVA: 0x00049104 File Offset: 0x00047304
		// (remove) Token: 0x06001128 RID: 4392 RVA: 0x0004913C File Offset: 0x0004733C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<BaseVisualElementPanel> panelDisposed;

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x00049174 File Offset: 0x00047374
		internal UIElementsBridge uiElementsBridge
		{
			get
			{
				bool flag = this.m_UIElementsBridge != null;
				if (flag)
				{
					return this.m_UIElementsBridge;
				}
				throw new Exception("Panel has no UIElementsBridge.");
			}
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x000491A4 File Offset: 0x000473A4
		protected BaseVisualElementPanel()
		{
			this.layoutConfig = LayoutManager.SharedManager.CreateConfig();
			this.m_UIElementsBridge = new RuntimeUIElementsBridge();
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00049232 File Offset: 0x00047432
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00049244 File Offset: 0x00047444
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = this.ownerObject != null;
					if (flag)
					{
						UIElementsUtility.RemoveCachedPanel(this.ownerObject.GetInstanceID());
					}
					PointerDeviceState.RemovePanelData(this);
				}
				Action<BaseVisualElementPanel> action = this.panelDisposed;
				if (action != null)
				{
					action(this);
				}
				LayoutManager.SharedManager.DestroyConfig(ref this.layoutConfig);
				this.disposed = true;
			}
		}

		// Token: 0x0600112D RID: 4397
		public abstract void Repaint(Event e);

		// Token: 0x0600112E RID: 4398
		public abstract void ValidateFocus();

		// Token: 0x0600112F RID: 4399
		public abstract void ValidateLayout();

		// Token: 0x06001130 RID: 4400
		public abstract void UpdateAnimations();

		// Token: 0x06001131 RID: 4401
		public abstract void UpdateBindings();

		// Token: 0x06001132 RID: 4402
		public abstract void ApplyStyles();

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x000492BC File Offset: 0x000474BC
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x000492D4 File Offset: 0x000474D4
		internal unsafe float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_Scale, value);
				if (flag)
				{
					this.m_Scale = value;
					this.visualTree.IncrementVersion(VersionChangeType.Layout);
					*this.layoutConfig.PointScaleFactor = this.scaledPixelsPerPoint;
					this.visualTree.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0004932C File Offset: 0x0004752C
		public float scaledPixelsPerPoint
		{
			get
			{
				return this.m_PixelsPerPoint * this.m_Scale;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0004934B File Offset: 0x0004754B
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x00049353 File Offset: 0x00047553
		public float referenceSpritePixelsPerUnit { get; set; } = 100f;

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0004935C File Offset: 0x0004755C
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x00049364 File Offset: 0x00047564
		internal PanelClearSettings clearSettings { get; set; } = new PanelClearSettings
		{
			clearDepthStencil = true,
			clearColor = true,
			color = Color.clear
		};

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x0004936D File Offset: 0x0004756D
		// (set) Token: 0x0600113B RID: 4411 RVA: 0x00049375 File Offset: 0x00047575
		internal bool duringLayoutPhase { get; set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600113C RID: 4412
		internal abstract uint version { get; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x0600113D RID: 4413
		internal abstract uint hierarchyVersion { get; }

		// Token: 0x0600113E RID: 4414
		internal abstract void OnVersionChanged(VisualElement ele, VersionChangeType changeTypeFlag);

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x0004937E File Offset: 0x0004757E
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x00049386 File Offset: 0x00047586
		internal virtual RepaintData repaintData { get; set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0004938F File Offset: 0x0004758F
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x00049397 File Offset: 0x00047597
		internal virtual ICursorManager cursorManager { get; set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x000493A0 File Offset: 0x000475A0
		// (set) Token: 0x06001144 RID: 4420 RVA: 0x000493A8 File Offset: 0x000475A8
		public ContextualMenuManager contextualMenuManager { get; internal set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x000493B1 File Offset: 0x000475B1
		// (set) Token: 0x06001146 RID: 4422 RVA: 0x000493B9 File Offset: 0x000475B9
		internal virtual DataBindingManager dataBindingManager { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001147 RID: 4423
		public abstract VisualElement visualTree { get; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001148 RID: 4424
		// (set) Token: 0x06001149 RID: 4425
		public abstract EventDispatcher dispatcher { get; set; }

		// Token: 0x0600114A RID: 4426 RVA: 0x000493C2 File Offset: 0x000475C2
		internal void SendEvent(EventBase e, DispatchMode dispatchMode = DispatchMode.Default)
		{
			Debug.Assert(this.dispatcher != null, "dispatcher != null");
			EventDispatcher dispatcher = this.dispatcher;
			if (dispatcher != null)
			{
				dispatcher.Dispatch(e, this, dispatchMode);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600114B RID: 4427
		internal abstract IScheduler scheduler { get; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600114C RID: 4428
		// (set) Token: 0x0600114D RID: 4429
		internal abstract IStylePropertyAnimationSystem styleAnimationSystem
		{
			get; [VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			set;
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600114E RID: 4430
		public abstract ContextType contextType { get; }

		// Token: 0x0600114F RID: 4431
		public abstract VisualElement Pick(Vector2 point);

		// Token: 0x06001150 RID: 4432
		public abstract VisualElement PickAll(Vector2 point, List<VisualElement> picked);

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x000493EE File Offset: 0x000475EE
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x000493F6 File Offset: 0x000475F6
		internal bool disposed { get; private set; }

		// Token: 0x06001153 RID: 4435
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal abstract IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase);

		// Token: 0x06001154 RID: 4436 RVA: 0x00049400 File Offset: 0x00047600
		internal VisualElement GetTopElementUnderPointer(int pointerId)
		{
			return this.m_TopElementUnderPointers.GetTopElementUnderPointer(pointerId);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00049420 File Offset: 0x00047620
		internal VisualElement RecomputeTopElementUnderPointer(int pointerId, Vector2 pointerPos, EventBase triggerEvent)
		{
			VisualElement element = null;
			bool flag = PointerDeviceState.GetPanel(pointerId, this.contextType) == this && !PointerDeviceState.HasLocationFlag(pointerId, this.contextType, PointerDeviceState.LocationFlag.OutsidePanel);
			if (flag)
			{
				element = this.Pick(pointerPos);
			}
			this.m_TopElementUnderPointers.SetElementUnderPointer(element, pointerId, triggerEvent);
			IPointerEventInternal pe = triggerEvent as IPointerEventInternal;
			bool flag2 = pe != null && pe.compatibilityMouseEvent != null;
			if (flag2)
			{
				this.m_TopElementUnderPointers.SetElementUnderPointer(element, PointerId.mousePointerId, (EventBase)pe.compatibilityMouseEvent);
			}
			return element;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000494AF File Offset: 0x000476AF
		internal void ClearCachedElementUnderPointer(int pointerId, EventBase triggerEvent)
		{
			this.m_TopElementUnderPointers.SetTemporaryElementUnderPointer(null, pointerId, triggerEvent);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000494C1 File Offset: 0x000476C1
		internal void CommitElementUnderPointers()
		{
			this.m_TopElementUnderPointers.CommitElementUnderPointers(this.dispatcher, this.contextType);
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001158 RID: 4440 RVA: 0x000494DC File Offset: 0x000476DC
		// (remove) Token: 0x06001159 RID: 4441 RVA: 0x00049514 File Offset: 0x00047714
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action isFlatChanged;

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x00049549 File Offset: 0x00047749
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x00049554 File Offset: 0x00047754
		public bool isFlat
		{
			get
			{
				return this.m_IsFlat;
			}
			set
			{
				bool flag = this.m_IsFlat == value;
				if (!flag)
				{
					this.m_IsFlat = value;
					Action action = this.isFlatChanged;
					if (action != null)
					{
						action();
					}
				}
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x0600115C RID: 4444 RVA: 0x0004958C File Offset: 0x0004778C
		// (remove) Token: 0x0600115D RID: 4445 RVA: 0x000495C4 File Offset: 0x000477C4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action atlasChanged;

		// Token: 0x0600115E RID: 4446 RVA: 0x000495F9 File Offset: 0x000477F9
		protected void InvokeAtlasChanged()
		{
			Action action = this.atlasChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x0600115F RID: 4447
		// (set) Token: 0x06001160 RID: 4448
		public abstract AtlasBase atlas { get; set; }

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001161 RID: 4449 RVA: 0x00049610 File Offset: 0x00047810
		// (remove) Token: 0x06001162 RID: 4450 RVA: 0x00049648 File Offset: 0x00047848
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event HierarchyEvent hierarchyChanged;

		// Token: 0x06001163 RID: 4451 RVA: 0x00049680 File Offset: 0x00047880
		internal void InvokeHierarchyChanged(VisualElement ve, HierarchyChangeType changeType)
		{
			bool flag = this.hierarchyChanged != null;
			if (flag)
			{
				this.hierarchyChanged(ve, changeType);
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x000496A9 File Offset: 0x000478A9
		internal void InvokeBeforeUpdate()
		{
			Action<IPanel> action = this.beforeUpdate;
			if (action != null)
			{
				action(this);
			}
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x000496C0 File Offset: 0x000478C0
		internal void UpdateElementUnderPointers()
		{
			foreach (int pointerId in PointerId.hoveringPointers)
			{
				bool flag = PointerDeviceState.GetPanel(pointerId, this.contextType) != this || PointerDeviceState.HasLocationFlag(pointerId, this.contextType, PointerDeviceState.LocationFlag.OutsidePanel);
				if (flag)
				{
					this.m_TopElementUnderPointers.SetElementUnderPointer(null, pointerId, new Vector2(-2.1474836E+09f, -2.1474836E+09f));
				}
				else
				{
					Vector2 pointerPos = PointerDeviceState.GetPointerPosition(pointerId, this.contextType);
					VisualElement elementUnderPointer = this.PickAll(pointerPos, null);
					this.m_TopElementUnderPointers.SetElementUnderPointer(elementUnderPointer, pointerId, pointerPos);
				}
			}
			this.CommitElementUnderPointers();
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x000020EA File Offset: 0x000002EA
		void IGroupBox.OnOptionAdded(IGroupBoxOption option)
		{
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x000020EA File Offset: 0x000002EA
		void IGroupBox.OnOptionRemoved(IGroupBoxOption option)
		{
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00049760 File Offset: 0x00047960
		public virtual void Render()
		{
			this.panelRenderer.Render();
		}

		// Token: 0x04000A06 RID: 2566
		private UIElementsBridge m_UIElementsBridge;

		// Token: 0x04000A07 RID: 2567
		private float m_Scale = 1f;

		// Token: 0x04000A08 RID: 2568
		internal LayoutConfig layoutConfig;

		// Token: 0x04000A09 RID: 2569
		private float m_PixelsPerPoint = 1f;

		// Token: 0x04000A0C RID: 2572
		internal IPanelRenderer panelRenderer;

		// Token: 0x04000A13 RID: 2579
		internal ElementUnderPointer m_TopElementUnderPointers = new ElementUnderPointer();

		// Token: 0x04000A15 RID: 2581
		private bool m_IsFlat = true;

		// Token: 0x04000A18 RID: 2584
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<IPanel> beforeUpdate;
	}
}
