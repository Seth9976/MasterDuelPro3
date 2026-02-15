using System;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000221 RID: 545
	public class StickControl : Vector2Control
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x0005C13B File Offset: 0x0005A33B
		// (set) Token: 0x060013FB RID: 5115 RVA: 0x0005C143 File Offset: 0x0005A343
		[InputControl(useStateFrom = "y", processors = "axisDeadzone", parameters = "clamp=2,clampMin=0,clampMax=1", synthetic = true, displayName = "Up")]
		[InputControl(name = "x", minValue = -1f, maxValue = 1f, layout = "Axis", processors = "axisDeadzone")]
		[InputControl(name = "y", minValue = -1f, maxValue = 1f, layout = "Axis", processors = "axisDeadzone")]
		public ButtonControl up { get; set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x0005C14C File Offset: 0x0005A34C
		// (set) Token: 0x060013FD RID: 5117 RVA: 0x0005C154 File Offset: 0x0005A354
		[InputControl(useStateFrom = "y", processors = "axisDeadzone", parameters = "clamp=2,clampMin=-1,clampMax=0,invert", synthetic = true, displayName = "Down")]
		public ButtonControl down { get; set; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x0005C15D File Offset: 0x0005A35D
		// (set) Token: 0x060013FF RID: 5119 RVA: 0x0005C165 File Offset: 0x0005A365
		[InputControl(useStateFrom = "x", processors = "axisDeadzone", parameters = "clamp=2,clampMin=-1,clampMax=0,invert", synthetic = true, displayName = "Left")]
		public ButtonControl left { get; set; }

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x0005C16E File Offset: 0x0005A36E
		// (set) Token: 0x06001401 RID: 5121 RVA: 0x0005C176 File Offset: 0x0005A376
		[InputControl(useStateFrom = "x", processors = "axisDeadzone", parameters = "clamp=2,clampMin=0,clampMax=1", synthetic = true, displayName = "Right")]
		public ButtonControl right { get; set; }

		// Token: 0x06001402 RID: 5122 RVA: 0x0005C180 File Offset: 0x0005A380
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.up = base.GetChildControl<ButtonControl>("up");
			this.down = base.GetChildControl<ButtonControl>("down");
			this.left = base.GetChildControl<ButtonControl>("left");
			this.right = base.GetChildControl<ButtonControl>("right");
		}
	}
}
