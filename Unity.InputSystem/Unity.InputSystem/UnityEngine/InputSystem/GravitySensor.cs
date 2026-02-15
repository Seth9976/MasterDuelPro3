using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000B0 RID: 176
	[InputControlLayout(stateType = typeof(GravityState), displayName = "Gravity")]
	public class GravitySensor : Sensor
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00033933 File Offset: 0x00031B33
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0003393B File Offset: 0x00031B3B
		public Vector3Control gravity { get; protected set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00033944 File Offset: 0x00031B44
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0003394B File Offset: 0x00031B4B
		public static GravitySensor current { get; private set; }

		// Token: 0x060009B3 RID: 2483 RVA: 0x00033953 File Offset: 0x00031B53
		protected override void FinishSetup()
		{
			this.gravity = base.GetChildControl<Vector3Control>("gravity");
			base.FinishSetup();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0003396C File Offset: 0x00031B6C
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			GravitySensor.current = this;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0003397A File Offset: 0x00031B7A
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (GravitySensor.current == this)
			{
				GravitySensor.current = null;
			}
		}
	}
}
