using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000B9 RID: 185
	[InputControlLayout(displayName = "Step Counter")]
	public class StepCounter : Sensor
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00033C78 File Offset: 0x00031E78
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x00033C80 File Offset: 0x00031E80
		[InputControl(displayName = "Step Counter", noisy = true)]
		public IntegerControl stepCounter { get; protected set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00033C89 File Offset: 0x00031E89
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x00033C90 File Offset: 0x00031E90
		public static StepCounter current { get; private set; }

		// Token: 0x060009FB RID: 2555 RVA: 0x00033C98 File Offset: 0x00031E98
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			StepCounter.current = this;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00033CA6 File Offset: 0x00031EA6
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (StepCounter.current == this)
			{
				StepCounter.current = null;
			}
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00033CBC File Offset: 0x00031EBC
		protected override void FinishSetup()
		{
			this.stepCounter = base.GetChildControl<IntegerControl>("stepCounter");
			base.FinishSetup();
		}
	}
}
