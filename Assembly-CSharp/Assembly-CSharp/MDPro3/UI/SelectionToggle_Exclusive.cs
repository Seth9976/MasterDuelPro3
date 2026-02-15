using System;

namespace MDPro3.UI
{
	// Token: 0x020013C9 RID: 5065
	public class SelectionToggle_Exclusive : SelectionToggle
	{
		// Token: 0x060092B9 RID: 37561 RVA: 0x00149E76 File Offset: 0x00148076
		protected override void Awake()
		{
			base.Awake();
			this.exclusiveToggle = true;
			this.canToggleOffSelf = false;
		}
	}
}
