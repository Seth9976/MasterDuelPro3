using System;
using System.Runtime.CompilerServices;

namespace YgomSystem.UI
{
	// Token: 0x020005B8 RID: 1464
	public class RootViewControllerManager : ViewControllerManager
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06002E28 RID: 11816 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002E29 RID: 11817 RVA: 0x0000216D File Offset: 0x0000036D
		public static RootViewControllerManager instance
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Awake()
		{
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnDestroy()
		{
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Update()
		{
		}

		// Token: 0x04002BDA RID: 11226
		public Action<ViewControllerManager> onBackEvent;
	}
}
