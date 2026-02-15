using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000B4 RID: 180
	[InputControlLayout(displayName = "Light")]
	public class LightSensor : Sensor
	{
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00033AA7 File Offset: 0x00031CA7
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x00033AAF File Offset: 0x00031CAF
		[InputControl(displayName = "Light Level", noisy = true)]
		public AxisControl lightLevel { get; protected set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00033AB8 File Offset: 0x00031CB8
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x00033ABF File Offset: 0x00031CBF
		public static LightSensor current { get; private set; }

		// Token: 0x060009D3 RID: 2515 RVA: 0x00033AC7 File Offset: 0x00031CC7
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			LightSensor.current = this;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00033AD5 File Offset: 0x00031CD5
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (LightSensor.current == this)
			{
				LightSensor.current = null;
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00033AEB File Offset: 0x00031CEB
		protected override void FinishSetup()
		{
			this.lightLevel = base.GetChildControl<AxisControl>("lightLevel");
			base.FinishSetup();
		}
	}
}
