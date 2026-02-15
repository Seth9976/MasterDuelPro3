using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000BA RID: 186
	[InputControlLayout(displayName = "Hinge Angle")]
	public class HingeAngle : Sensor
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00033CD5 File Offset: 0x00031ED5
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x00033CDD File Offset: 0x00031EDD
		public AxisControl angle { get; protected set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00033CE6 File Offset: 0x00031EE6
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x00033CED File Offset: 0x00031EED
		public static HingeAngle current { get; private set; }

		// Token: 0x06000A03 RID: 2563 RVA: 0x00033CF5 File Offset: 0x00031EF5
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			HingeAngle.current = this;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00033D03 File Offset: 0x00031F03
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (HingeAngle.current == this)
			{
				HingeAngle.current = null;
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00033D19 File Offset: 0x00031F19
		protected override void FinishSetup()
		{
			this.angle = base.GetChildControl<AxisControl>("angle");
			base.FinishSetup();
		}
	}
}
