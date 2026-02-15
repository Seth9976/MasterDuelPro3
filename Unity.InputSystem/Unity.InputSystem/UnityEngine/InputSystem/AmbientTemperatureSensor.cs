using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000B8 RID: 184
	[InputControlLayout(displayName = "Ambient Temperature")]
	public class AmbientTemperatureSensor : Sensor
	{
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00033C1B File Offset: 0x00031E1B
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x00033C23 File Offset: 0x00031E23
		[InputControl(displayName = "Ambient Temperature", noisy = true)]
		public AxisControl ambientTemperature { get; protected set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00033C2C File Offset: 0x00031E2C
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x00033C33 File Offset: 0x00031E33
		public static AmbientTemperatureSensor current { get; private set; }

		// Token: 0x060009F3 RID: 2547 RVA: 0x00033C3B File Offset: 0x00031E3B
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			AmbientTemperatureSensor.current = this;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00033C49 File Offset: 0x00031E49
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (AmbientTemperatureSensor.current == this)
			{
				AmbientTemperatureSensor.current = null;
			}
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00033C5F File Offset: 0x00031E5F
		protected override void FinishSetup()
		{
			this.ambientTemperature = base.GetChildControl<AxisControl>("ambientTemperature");
			base.FinishSetup();
		}
	}
}
