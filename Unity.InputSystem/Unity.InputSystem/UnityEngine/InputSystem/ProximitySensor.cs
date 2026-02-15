using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000B6 RID: 182
	[InputControlLayout(displayName = "Proximity")]
	public class ProximitySensor : Sensor
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00033B61 File Offset: 0x00031D61
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x00033B69 File Offset: 0x00031D69
		[InputControl(displayName = "Distance", noisy = true)]
		public AxisControl distance { get; protected set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00033B72 File Offset: 0x00031D72
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x00033B79 File Offset: 0x00031D79
		public static ProximitySensor current { get; private set; }

		// Token: 0x060009E3 RID: 2531 RVA: 0x00033B81 File Offset: 0x00031D81
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			ProximitySensor.current = this;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00033B8F File Offset: 0x00031D8F
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (ProximitySensor.current == this)
			{
				ProximitySensor.current = null;
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00033BA5 File Offset: 0x00031DA5
		protected override void FinishSetup()
		{
			this.distance = base.GetChildControl<AxisControl>("distance");
			base.FinishSetup();
		}
	}
}
