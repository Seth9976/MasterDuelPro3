using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000B3 RID: 179
	[InputControlLayout(displayName = "Magnetic Field")]
	public class MagneticFieldSensor : Sensor
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00033A4A File Offset: 0x00031C4A
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x00033A52 File Offset: 0x00031C52
		[InputControl(displayName = "Magnetic Field", noisy = true)]
		public Vector3Control magneticField { get; protected set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00033A5B File Offset: 0x00031C5B
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x00033A62 File Offset: 0x00031C62
		public static MagneticFieldSensor current { get; private set; }

		// Token: 0x060009CB RID: 2507 RVA: 0x00033A6A File Offset: 0x00031C6A
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			MagneticFieldSensor.current = this;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00033A78 File Offset: 0x00031C78
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (MagneticFieldSensor.current == this)
			{
				MagneticFieldSensor.current = null;
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00033A8E File Offset: 0x00031C8E
		protected override void FinishSetup()
		{
			this.magneticField = base.GetChildControl<Vector3Control>("magneticField");
			base.FinishSetup();
		}
	}
}
