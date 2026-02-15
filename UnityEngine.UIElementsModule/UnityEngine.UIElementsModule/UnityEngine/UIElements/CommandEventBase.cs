using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001BD RID: 445
	[EventCategory(EventCategory.Command)]
	public abstract class CommandEventBase<T> : EventBase<T> where T : CommandEventBase<T>, new()
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0003C0CC File Offset: 0x0003A2CC
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x0003C10B File Offset: 0x0003A30B
		public string commandName
		{
			get
			{
				bool flag = this.m_CommandName == null && base.imguiEvent != null;
				string text;
				if (flag)
				{
					text = base.imguiEvent.commandName;
				}
				else
				{
					text = this.m_CommandName;
				}
				return text;
			}
			protected set
			{
				this.m_CommandName = value;
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0003C115 File Offset: 0x0003A315
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0003C126 File Offset: 0x0003A326
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			this.commandName = null;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0003C13C File Offset: 0x0003A33C
		public static T GetPooled(Event systemEvent)
		{
			T e = EventBase<T>.GetPooled();
			e.imguiEvent = systemEvent;
			return e;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0003C164 File Offset: 0x0003A364
		public static T GetPooled(string commandName)
		{
			T e = EventBase<T>.GetPooled();
			e.commandName = commandName;
			return e;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0003C18A File Offset: 0x0003A38A
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToFocusedElementOrPanelRoot(this, panel);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0003C195 File Offset: 0x0003A395
		protected CommandEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x040007EE RID: 2030
		private string m_CommandName;
	}
}
