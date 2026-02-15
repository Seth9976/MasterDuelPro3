using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000223 RID: 547
	[EventCategory(EventCategory.PointerMove)]
	public sealed class PointerMoveEvent : PointerEventBase<PointerMoveEvent>
	{
		// Token: 0x06000F14 RID: 3860 RVA: 0x00042A82 File Offset: 0x00040C82
		static PointerMoveEvent()
		{
			EventBase<PointerMoveEvent>.SetCreateFunction(() => new PointerMoveEvent());
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00042A9B File Offset: 0x00040C9B
		// (set) Token: 0x06000F16 RID: 3862 RVA: 0x00042AA3 File Offset: 0x00040CA3
		internal bool isHandledByDraggable { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00042AAC File Offset: 0x00040CAC
		internal bool isPointerDown
		{
			get
			{
				return base.button >= 0 && (base.pressedButtons & (1 << base.button)) != 0;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00042ACF File Offset: 0x00040CCF
		internal bool isPointerUp
		{
			get
			{
				return base.button >= 0 && (base.pressedButtons & (1 << base.button)) == 0;
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00042AF2 File Offset: 0x00040CF2
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00042B03 File Offset: 0x00040D03
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			((IPointerEventInternal)this).triggeredByOS = true;
			this.isHandledByDraggable = false;
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00042B1E File Offset: 0x00040D1E
		public PointerMoveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00042B30 File Offset: 0x00040D30
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool flag = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag)
			{
				bool flag2 = base.imguiEvent != null && base.imguiEvent.rawType == EventType.MouseDown;
				if (flag2)
				{
					((IPointerEventInternal)this).compatibilityMouseEvent = MouseDownEvent.GetPooled(this);
				}
				else
				{
					bool flag3 = base.imguiEvent != null && base.imguiEvent.rawType == EventType.MouseUp;
					if (flag3)
					{
						((IPointerEventInternal)this).compatibilityMouseEvent = MouseUpEvent.GetPooled(this);
					}
					else
					{
						((IPointerEventInternal)this).compatibilityMouseEvent = MouseMoveEvent.GetPooled(this);
					}
				}
			}
		}
	}
}
