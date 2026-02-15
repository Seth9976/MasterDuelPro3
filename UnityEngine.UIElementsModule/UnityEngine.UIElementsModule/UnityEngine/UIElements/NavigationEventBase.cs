using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200020A RID: 522
	[EventCategory(EventCategory.Navigation)]
	public abstract class NavigationEventBase<T> : EventBase<T>, INavigationEvent where T : NavigationEventBase<T>, new()
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x000407C4 File Offset: 0x0003E9C4
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x000407CC File Offset: 0x0003E9CC
		public EventModifiers modifiers { get; protected set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x000407D8 File Offset: 0x0003E9D8
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x000407F8 File Offset: 0x0003E9F8
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00040815 File Offset: 0x0003EA15
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x0004081D File Offset: 0x0003EA1D
		internal NavigationDeviceType deviceType { get; private set; }

		// Token: 0x06000E59 RID: 3673 RVA: 0x00040826 File Offset: 0x0003EA26
		protected NavigationEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00040837 File Offset: 0x0003EA37
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00040848 File Offset: 0x0003EA48
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
			this.modifiers = EventModifiers.None;
			this.deviceType = NavigationDeviceType.Unknown;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00040864 File Offset: 0x0003EA64
		public static T GetPooled(EventModifiers modifiers = EventModifiers.None)
		{
			T e = EventBase<T>.GetPooled();
			e.modifiers = modifiers;
			e.deviceType = NavigationDeviceType.Unknown;
			return e;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00040898 File Offset: 0x0003EA98
		internal static T GetPooled(NavigationDeviceType deviceType, EventModifiers modifiers = EventModifiers.None)
		{
			T e = EventBase<T>.GetPooled();
			e.modifiers = modifiers;
			e.deviceType = deviceType;
			return e;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0003C18A File Offset: 0x0003A38A
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToFocusedElementOrPanelRoot(this, panel);
		}
	}
}
