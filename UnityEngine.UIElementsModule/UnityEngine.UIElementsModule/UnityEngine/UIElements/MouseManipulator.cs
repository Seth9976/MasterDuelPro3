using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000274 RID: 628
	public abstract class MouseManipulator : Manipulator
	{
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x00048D18 File Offset: 0x00046F18
		// (set) Token: 0x060010EB RID: 4331 RVA: 0x00048D20 File Offset: 0x00046F20
		public List<ManipulatorActivationFilter> activators { get; private set; }

		// Token: 0x060010EC RID: 4332 RVA: 0x00048D29 File Offset: 0x00046F29
		protected MouseManipulator()
		{
			this.activators = new List<ManipulatorActivationFilter>();
		}
	}
}
