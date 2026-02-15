using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000AE RID: 174
	[InputControlLayout(stateType = typeof(AccelerometerState))]
	public class Accelerometer : Sensor
	{
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00033871 File Offset: 0x00031A71
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x00033879 File Offset: 0x00031A79
		public Vector3Control acceleration { get; protected set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00033882 File Offset: 0x00031A82
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x00033889 File Offset: 0x00031A89
		public static Accelerometer current { get; private set; }

		// Token: 0x060009A3 RID: 2467 RVA: 0x00033891 File Offset: 0x00031A91
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Accelerometer.current = this;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0003389F File Offset: 0x00031A9F
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Accelerometer.current == this)
			{
				Accelerometer.current = null;
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000338B5 File Offset: 0x00031AB5
		protected override void FinishSetup()
		{
			this.acceleration = base.GetChildControl<Vector3Control>("acceleration");
			base.FinishSetup();
		}
	}
}
