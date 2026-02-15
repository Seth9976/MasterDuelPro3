using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000B5 RID: 181
	[InputControlLayout(displayName = "Pressure")]
	public class PressureSensor : Sensor
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00033B04 File Offset: 0x00031D04
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x00033B0C File Offset: 0x00031D0C
		[InputControl(displayName = "Atmospheric Pressure", noisy = true)]
		public AxisControl atmosphericPressure { get; protected set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00033B15 File Offset: 0x00031D15
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x00033B1C File Offset: 0x00031D1C
		public static PressureSensor current { get; private set; }

		// Token: 0x060009DB RID: 2523 RVA: 0x00033B24 File Offset: 0x00031D24
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			PressureSensor.current = this;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00033B32 File Offset: 0x00031D32
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (PressureSensor.current == this)
			{
				PressureSensor.current = null;
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00033B48 File Offset: 0x00031D48
		protected override void FinishSetup()
		{
			this.atmosphericPressure = base.GetChildControl<AxisControl>("atmosphericPressure");
			base.FinishSetup();
		}
	}
}
