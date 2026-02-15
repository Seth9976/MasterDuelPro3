using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.Scripting;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000217 RID: 535
	[Preserve]
	public class DeltaControl : Vector2Control
	{
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0005B73A File Offset: 0x0005993A
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x0005B742 File Offset: 0x00059942
		[InputControl(useStateFrom = "y", parameters = "clamp=1,clampMin=0,clampMax=3.402823E+38", synthetic = true, displayName = "Up")]
		[Preserve]
		public AxisControl up { get; set; }

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0005B74B File Offset: 0x0005994B
		// (set) Token: 0x060013C3 RID: 5059 RVA: 0x0005B753 File Offset: 0x00059953
		[InputControl(useStateFrom = "y", parameters = "clamp=1,clampMin=-3.402823E+38,clampMax=0,invert", synthetic = true, displayName = "Down")]
		[Preserve]
		public AxisControl down { get; set; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x0005B75C File Offset: 0x0005995C
		// (set) Token: 0x060013C5 RID: 5061 RVA: 0x0005B764 File Offset: 0x00059964
		[InputControl(useStateFrom = "x", parameters = "clamp=1,clampMin=-3.402823E+38,clampMax=0,invert", synthetic = true, displayName = "Left")]
		[Preserve]
		public AxisControl left { get; set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0005B76D File Offset: 0x0005996D
		// (set) Token: 0x060013C7 RID: 5063 RVA: 0x0005B775 File Offset: 0x00059975
		[InputControl(useStateFrom = "x", parameters = "clamp=1,clampMin=0,clampMax=3.402823E+38", synthetic = true, displayName = "Right")]
		[Preserve]
		public AxisControl right { get; set; }

		// Token: 0x060013C8 RID: 5064 RVA: 0x0005B780 File Offset: 0x00059980
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.up = base.GetChildControl<AxisControl>("up");
			this.down = base.GetChildControl<AxisControl>("down");
			this.left = base.GetChildControl<AxisControl>("left");
			this.right = base.GetChildControl<AxisControl>("right");
		}
	}
}
