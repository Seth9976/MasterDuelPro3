using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000B1 RID: 177
	[InputControlLayout(stateType = typeof(AttitudeState), displayName = "Attitude")]
	public class AttitudeSensor : Sensor
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00033990 File Offset: 0x00031B90
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x00033998 File Offset: 0x00031B98
		public QuaternionControl attitude { get; protected set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x000339A1 File Offset: 0x00031BA1
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x000339A8 File Offset: 0x00031BA8
		public static AttitudeSensor current { get; private set; }

		// Token: 0x060009BB RID: 2491 RVA: 0x000339B0 File Offset: 0x00031BB0
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			AttitudeSensor.current = this;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x000339BE File Offset: 0x00031BBE
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (AttitudeSensor.current == this)
			{
				AttitudeSensor.current = null;
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x000339D4 File Offset: 0x00031BD4
		protected override void FinishSetup()
		{
			this.attitude = base.GetChildControl<QuaternionControl>("attitude");
			base.FinishSetup();
		}
	}
}
