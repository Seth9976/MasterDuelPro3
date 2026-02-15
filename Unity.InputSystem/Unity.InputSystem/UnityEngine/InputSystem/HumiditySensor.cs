using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000B7 RID: 183
	[InputControlLayout(displayName = "Humidity")]
	public class HumiditySensor : Sensor
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00033BBE File Offset: 0x00031DBE
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x00033BC6 File Offset: 0x00031DC6
		[InputControl(displayName = "Relative Humidity", noisy = true)]
		public AxisControl relativeHumidity { get; protected set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00033BCF File Offset: 0x00031DCF
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x00033BD6 File Offset: 0x00031DD6
		public static HumiditySensor current { get; private set; }

		// Token: 0x060009EB RID: 2539 RVA: 0x00033BDE File Offset: 0x00031DDE
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			HumiditySensor.current = this;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00033BEC File Offset: 0x00031DEC
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (HumiditySensor.current == this)
			{
				HumiditySensor.current = null;
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00033C02 File Offset: 0x00031E02
		protected override void FinishSetup()
		{
			this.relativeHumidity = base.GetChildControl<AxisControl>("relativeHumidity");
			base.FinishSetup();
		}
	}
}
