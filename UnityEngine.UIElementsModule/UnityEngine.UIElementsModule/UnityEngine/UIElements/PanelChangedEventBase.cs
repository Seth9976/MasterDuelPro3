using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000212 RID: 530
	[EventCategory(EventCategory.ChangePanel)]
	public abstract class PanelChangedEventBase<T> : EventBase<T> where T : PanelChangedEventBase<T>, new()
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x00040B24 File Offset: 0x0003ED24
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x00040B2C File Offset: 0x0003ED2C
		public IPanel originPanel { get; private set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00040B35 File Offset: 0x0003ED35
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x00040B3D File Offset: 0x0003ED3D
		public IPanel destinationPanel { get; private set; }

		// Token: 0x06000E7D RID: 3709 RVA: 0x00040B46 File Offset: 0x0003ED46
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00040B57 File Offset: 0x0003ED57
		private void LocalInit()
		{
			this.originPanel = null;
			this.destinationPanel = null;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00040B6C File Offset: 0x0003ED6C
		public static T GetPooled(IPanel originPanel, IPanel destinationPanel)
		{
			T e = EventBase<T>.GetPooled();
			e.originPanel = originPanel;
			e.destinationPanel = destinationPanel;
			return e;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00040B9F File Offset: 0x0003ED9F
		protected PanelChangedEventBase()
		{
			this.LocalInit();
		}
	}
}
