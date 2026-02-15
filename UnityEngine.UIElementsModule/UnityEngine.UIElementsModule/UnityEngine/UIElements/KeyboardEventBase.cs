using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E3 RID: 483
	[EventCategory(EventCategory.Keyboard)]
	public abstract class KeyboardEventBase<T> : EventBase<T>, IKeyboardEvent where T : KeyboardEventBase<T>, new()
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0003EDC9 File Offset: 0x0003CFC9
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x0003EDD1 File Offset: 0x0003CFD1
		public EventModifiers modifiers { get; protected set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0003EDDA File Offset: 0x0003CFDA
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x0003EDE2 File Offset: 0x0003CFE2
		public char character { get; protected set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0003EDEB File Offset: 0x0003CFEB
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x0003EDF3 File Offset: 0x0003CFF3
		public KeyCode keyCode { get; protected set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0003EDFC File Offset: 0x0003CFFC
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x0003EE1C File Offset: 0x0003D01C
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0003EE3C File Offset: 0x0003D03C
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0003EE5C File Offset: 0x0003D05C
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0003EE7C File Offset: 0x0003D07C
		internal bool functionKey
		{
			get
			{
				return (this.modifiers & EventModifiers.FunctionKey) > EventModifiers.None;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x0003EE9C File Offset: 0x0003D09C
		public bool actionKey
		{
			get
			{
				bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
				bool flag2;
				if (flag)
				{
					flag2 = this.commandKey;
				}
				else
				{
					flag2 = this.ctrlKey;
				}
				return flag2;
			}
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0003EED5 File Offset: 0x0003D0D5
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0003EEE6 File Offset: 0x0003D0E6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
			this.modifiers = EventModifiers.None;
			this.character = '\0';
			this.keyCode = KeyCode.None;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0003EF0C File Offset: 0x0003D10C
		public static T GetPooled(char c, KeyCode keyCode, EventModifiers modifiers)
		{
			T e = EventBase<T>.GetPooled();
			e.modifiers = modifiers;
			e.character = c;
			e.keyCode = keyCode;
			return e;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0003EF4C File Offset: 0x0003D14C
		public static T GetPooled(Event systemEvent)
		{
			T e = EventBase<T>.GetPooled();
			e.imguiEvent = systemEvent;
			bool flag = systemEvent != null;
			if (flag)
			{
				e.modifiers = systemEvent.modifiers;
				e.character = systemEvent.character;
				e.keyCode = systemEvent.keyCode;
			}
			return e;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0003C18A File Offset: 0x0003A38A
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToFocusedElementOrPanelRoot(this, panel);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0003EFB2 File Offset: 0x0003D1B2
		protected KeyboardEventBase()
		{
			this.LocalInit();
		}
	}
}
