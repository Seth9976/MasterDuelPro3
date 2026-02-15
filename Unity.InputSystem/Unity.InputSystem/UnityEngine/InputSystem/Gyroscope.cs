using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000AF RID: 175
	[InputControlLayout(stateType = typeof(GyroscopeState))]
	public class Gyroscope : Sensor
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x000338D6 File Offset: 0x00031AD6
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x000338DE File Offset: 0x00031ADE
		public Vector3Control angularVelocity { get; protected set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x000338E7 File Offset: 0x00031AE7
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x000338EE File Offset: 0x00031AEE
		public static Gyroscope current { get; private set; }

		// Token: 0x060009AB RID: 2475 RVA: 0x000338F6 File Offset: 0x00031AF6
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Gyroscope.current = this;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00033904 File Offset: 0x00031B04
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Gyroscope.current == this)
			{
				Gyroscope.current = null;
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0003391A File Offset: 0x00031B1A
		protected override void FinishSetup()
		{
			this.angularVelocity = base.GetChildControl<Vector3Control>("angularVelocity");
			base.FinishSetup();
		}
	}
}
