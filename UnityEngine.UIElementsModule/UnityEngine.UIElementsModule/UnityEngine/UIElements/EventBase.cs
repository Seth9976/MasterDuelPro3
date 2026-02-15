using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C3 RID: 451
	public abstract class EventBase : IDisposable
	{
		// Token: 0x06000CA5 RID: 3237 RVA: 0x0003C78C File Offset: 0x0003A98C
		protected static long RegisterEventType()
		{
			return EventBase.s_LastTypeId += 1L;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0003C7AC File Offset: 0x0003A9AC
		public virtual long eventTypeId
		{
			get
			{
				return -1L;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0003C7B0 File Offset: 0x0003A9B0
		internal int eventCategories { get; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0003C7B8 File Offset: 0x0003A9B8
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x0003C7C0 File Offset: 0x0003A9C0
		public long timestamp { get; private set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0003C7C9 File Offset: 0x0003A9C9
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x0003C7D1 File Offset: 0x0003A9D1
		internal ulong eventId { get; private set; }

		// Token: 0x17000246 RID: 582
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x0003C7DA File Offset: 0x0003A9DA
		private ulong triggerEventId
		{
			[CompilerGenerated]
			set
			{
				this.<triggerEventId>k__BackingField = value;
			}
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0003C7E3 File Offset: 0x0003A9E3
		internal void SetTriggerEventId(ulong id)
		{
			this.triggerEventId = id;
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0003C7EE File Offset: 0x0003A9EE
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x0003C7F6 File Offset: 0x0003A9F6
		internal EventBase.EventPropagation propagation { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0003C7FF File Offset: 0x0003A9FF
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x0003C807 File Offset: 0x0003AA07
		private EventBase.LifeCycleStatus lifeCycleStatus { get; set; }

		// Token: 0x06000CB2 RID: 3250 RVA: 0x000020EA File Offset: 0x000002EA
		[Obsolete("Override PreDispatch(IPanel panel) instead.")]
		protected virtual void PreDispatch()
		{
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0003C810 File Offset: 0x0003AA10
		protected internal virtual void PreDispatch(IPanel panel)
		{
			this.PreDispatch();
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x000020EA File Offset: 0x000002EA
		[Obsolete("Override PostDispatch(IPanel panel) instead.")]
		protected virtual void PostDispatch()
		{
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0003C81A File Offset: 0x0003AA1A
		protected internal virtual void PostDispatch(IPanel panel)
		{
			this.PostDispatch();
			this.processed = true;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0003C82C File Offset: 0x0003AA2C
		internal virtual void Dispatch([JetBrains.Annotations.NotNull] BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DefaultDispatch(this, panel);
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0003C838 File Offset: 0x0003AA38
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x0003C858 File Offset: 0x0003AA58
		public bool bubbles
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.Bubbles) > EventBase.EventPropagation.None;
			}
			protected set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.Bubbles;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.Bubbles;
				}
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0003C890 File Offset: 0x0003AA90
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x0003C8B0 File Offset: 0x0003AAB0
		public bool tricklesDown
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.TricklesDown) > EventBase.EventPropagation.None;
			}
			protected set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.TricklesDown;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.TricklesDown;
				}
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0003C8E8 File Offset: 0x0003AAE8
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x0003C908 File Offset: 0x0003AB08
		internal bool skipDisabledElements
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.SkipDisabledElements) > EventBase.EventPropagation.None;
			}
			set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.SkipDisabledElements;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.SkipDisabledElements;
				}
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0003C940 File Offset: 0x0003AB40
		internal bool bubblesOrTricklesDown
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.BubblesOrTricklesDown) > EventBase.EventPropagation.None;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0003C94D File Offset: 0x0003AB4D
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x0003C955 File Offset: 0x0003AB55
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal VisualElement elementTarget { get; set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0003C95E File Offset: 0x0003AB5E
		// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x0003C966 File Offset: 0x0003AB66
		public IEventHandler target
		{
			get
			{
				return this.elementTarget;
			}
			set
			{
				this.elementTarget = value as VisualElement;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0003C978 File Offset: 0x0003AB78
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x0003C998 File Offset: 0x0003AB98
		public bool isPropagationStopped
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.PropagationStopped) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.PropagationStopped;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.PropagationStopped;
				}
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0003C9D0 File Offset: 0x0003ABD0
		public void StopPropagation()
		{
			this.isPropagationStopped = true;
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0003C9DC File Offset: 0x0003ABDC
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x0003C9FC File Offset: 0x0003ABFC
		public bool isImmediatePropagationStopped
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.ImmediatePropagationStopped) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.ImmediatePropagationStopped;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.ImmediatePropagationStopped;
				}
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0003CA34 File Offset: 0x0003AC34
		public void StopImmediatePropagation()
		{
			this.isPropagationStopped = true;
			this.isImmediatePropagationStopped = true;
		}

		// Token: 0x17000251 RID: 593
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x0003CA47 File Offset: 0x0003AC47
		internal PropagationPhase propagationPhase
		{
			[CompilerGenerated]
			set
			{
				this.<propagationPhase>k__BackingField = value;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0003CA50 File Offset: 0x0003AC50
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x0003CA68 File Offset: 0x0003AC68
		public virtual IEventHandler currentTarget
		{
			get
			{
				return this.m_CurrentTarget;
			}
			internal set
			{
				this.m_CurrentTarget = value;
				bool flag = this.imguiEvent != null;
				if (flag)
				{
					VisualElement element = this.currentTarget as VisualElement;
					bool flag2 = element != null;
					if (flag2)
					{
						this.imguiEvent.mousePosition = element.WorldToLocal(this.originalMousePosition);
					}
					else
					{
						this.imguiEvent.mousePosition = this.originalMousePosition;
					}
				}
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0003CAD0 File Offset: 0x0003ACD0
		// (set) Token: 0x06000CCC RID: 3276 RVA: 0x0003CAF0 File Offset: 0x0003ACF0
		public bool dispatch
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Dispatching) > EventBase.LifeCycleStatus.None;
			}
			internal set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Dispatching;
					this.dispatched = true;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Dispatching;
				}
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0003CB30 File Offset: 0x0003AD30
		internal void MarkReceivedByDispatcher()
		{
			Debug.Assert(!this.dispatched, "Events cannot be dispatched more than once.");
			this.dispatched = true;
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0003CB50 File Offset: 0x0003AD50
		// (set) Token: 0x06000CCF RID: 3279 RVA: 0x0003CB70 File Offset: 0x0003AD70
		private bool dispatched
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Dispatched) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Dispatched;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Dispatched;
				}
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0003CBAC File Offset: 0x0003ADAC
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x0003CBD0 File Offset: 0x0003ADD0
		internal bool processed
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Processed) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Processed;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Processed;
				}
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0003CC10 File Offset: 0x0003AE10
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x0003CC34 File Offset: 0x0003AE34
		internal bool processedByFocusController
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.ProcessedByFocusController) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.ProcessedByFocusController;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.ProcessedByFocusController;
				}
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0003CC74 File Offset: 0x0003AE74
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x0003CC94 File Offset: 0x0003AE94
		internal bool propagateToIMGUI
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.PropagateToIMGUI) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.PropagateToIMGUI;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.PropagateToIMGUI;
				}
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0003CCD0 File Offset: 0x0003AED0
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x0003CCF0 File Offset: 0x0003AEF0
		private bool imguiEventIsValid
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.IMGUIEventIsValid) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.IMGUIEventIsValid;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.IMGUIEventIsValid;
				}
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0003CD2C File Offset: 0x0003AF2C
		// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x0003CD50 File Offset: 0x0003AF50
		public Event imguiEvent
		{
			get
			{
				return this.imguiEventIsValid ? this.m_ImguiEvent : null;
			}
			protected set
			{
				bool flag = this.m_ImguiEvent == null;
				if (flag)
				{
					this.m_ImguiEvent = new Event();
				}
				bool flag2 = value != null;
				if (flag2)
				{
					this.m_ImguiEvent.CopyFrom(value);
					this.imguiEventIsValid = true;
					this.originalMousePosition = value.mousePosition;
				}
				else
				{
					this.imguiEventIsValid = false;
				}
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0003CDB0 File Offset: 0x0003AFB0
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x0003CDB8 File Offset: 0x0003AFB8
		public Vector2 originalMousePosition { get; private set; }

		// Token: 0x06000CDC RID: 3292 RVA: 0x0003CDC1 File Offset: 0x0003AFC1
		protected virtual void Init()
		{
			this.LocalInit();
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0003CDCC File Offset: 0x0003AFCC
		private void LocalInit()
		{
			this.timestamp = Panel.TimeSinceStartupMs();
			this.triggerEventId = 0UL;
			ulong num = EventBase.s_NextEventId;
			EventBase.s_NextEventId = num + 1UL;
			this.eventId = num;
			this.propagation = EventBase.EventPropagation.None;
			this.elementTarget = null;
			this.isPropagationStopped = false;
			this.isImmediatePropagationStopped = false;
			this.propagationPhase = PropagationPhase.None;
			this.originalMousePosition = Vector2.zero;
			this.m_CurrentTarget = null;
			this.dispatch = false;
			this.propagateToIMGUI = true;
			this.dispatched = false;
			this.processed = false;
			this.processedByFocusController = false;
			this.imguiEventIsValid = false;
			this.pooled = false;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0003CE77 File Offset: 0x0003B077
		internal EventBase(EventCategory category)
		{
			this.eventCategories = 1 << (int)category;
			this.m_ImguiEvent = null;
			this.LocalInit();
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0003CE9C File Offset: 0x0003B09C
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x0003CEBC File Offset: 0x0003B0BC
		protected bool pooled
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Pooled) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Pooled;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Pooled;
				}
			}
		}

		// Token: 0x06000CE1 RID: 3297
		internal abstract void Acquire();

		// Token: 0x06000CE2 RID: 3298
		public abstract void Dispose();

		// Token: 0x040007F6 RID: 2038
		private static long s_LastTypeId;

		// Token: 0x040007F8 RID: 2040
		private static ulong s_NextEventId;

		// Token: 0x04000800 RID: 2048
		private IEventHandler m_CurrentTarget;

		// Token: 0x04000801 RID: 2049
		private Event m_ImguiEvent;

		// Token: 0x020001C4 RID: 452
		[Flags]
		internal enum EventPropagation
		{
			// Token: 0x04000804 RID: 2052
			None = 0,
			// Token: 0x04000805 RID: 2053
			Bubbles = 1,
			// Token: 0x04000806 RID: 2054
			TricklesDown = 2,
			// Token: 0x04000807 RID: 2055
			SkipDisabledElements = 4,
			// Token: 0x04000808 RID: 2056
			BubblesOrTricklesDown = 3
		}

		// Token: 0x020001C5 RID: 453
		[Flags]
		private enum LifeCycleStatus
		{
			// Token: 0x0400080A RID: 2058
			None = 0,
			// Token: 0x0400080B RID: 2059
			PropagationStopped = 1,
			// Token: 0x0400080C RID: 2060
			ImmediatePropagationStopped = 2,
			// Token: 0x0400080D RID: 2061
			Dispatching = 4,
			// Token: 0x0400080E RID: 2062
			Pooled = 8,
			// Token: 0x0400080F RID: 2063
			IMGUIEventIsValid = 16,
			// Token: 0x04000810 RID: 2064
			PropagateToIMGUI = 32,
			// Token: 0x04000811 RID: 2065
			Dispatched = 64,
			// Token: 0x04000812 RID: 2066
			Processed = 128,
			// Token: 0x04000813 RID: 2067
			ProcessedByFocusController = 256
		}
	}
}
