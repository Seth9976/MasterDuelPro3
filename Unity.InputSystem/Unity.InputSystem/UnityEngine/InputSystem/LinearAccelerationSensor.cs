using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000B2 RID: 178
	[InputControlLayout(stateType = typeof(LinearAccelerationState), displayName = "Linear Acceleration")]
	public class LinearAccelerationSensor : Sensor
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x000339ED File Offset: 0x00031BED
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x000339F5 File Offset: 0x00031BF5
		public Vector3Control acceleration { get; protected set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000339FE File Offset: 0x00031BFE
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00033A05 File Offset: 0x00031C05
		public static LinearAccelerationSensor current { get; private set; }

		// Token: 0x060009C3 RID: 2499 RVA: 0x00033A0D File Offset: 0x00031C0D
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			LinearAccelerationSensor.current = this;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00033A1B File Offset: 0x00031C1B
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (LinearAccelerationSensor.current == this)
			{
				LinearAccelerationSensor.current = null;
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00033A31 File Offset: 0x00031C31
		protected override void FinishSetup()
		{
			this.acceleration = base.GetChildControl<Vector3Control>("acceleration");
			base.FinishSetup();
		}
	}
}
