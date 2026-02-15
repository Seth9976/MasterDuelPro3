using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000222 RID: 546
	[InputControlLayout(stateType = typeof(TouchState))]
	public class TouchControl : InputControl<TouchState>
	{
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x0005C1D7 File Offset: 0x0005A3D7
		// (set) Token: 0x06001405 RID: 5125 RVA: 0x0005C1DF File Offset: 0x0005A3DF
		public TouchPressControl press { get; set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x0005C1E8 File Offset: 0x0005A3E8
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x0005C1F0 File Offset: 0x0005A3F0
		public IntegerControl displayIndex { get; set; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x0005C1F9 File Offset: 0x0005A3F9
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x0005C201 File Offset: 0x0005A401
		public IntegerControl touchId { get; set; }

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x0005C20A File Offset: 0x0005A40A
		// (set) Token: 0x0600140B RID: 5131 RVA: 0x0005C212 File Offset: 0x0005A412
		public Vector2Control position { get; set; }

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x0005C21B File Offset: 0x0005A41B
		// (set) Token: 0x0600140D RID: 5133 RVA: 0x0005C223 File Offset: 0x0005A423
		public DeltaControl delta { get; set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x0005C22C File Offset: 0x0005A42C
		// (set) Token: 0x0600140F RID: 5135 RVA: 0x0005C234 File Offset: 0x0005A434
		public AxisControl pressure { get; set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x0005C23D File Offset: 0x0005A43D
		// (set) Token: 0x06001411 RID: 5137 RVA: 0x0005C245 File Offset: 0x0005A445
		public Vector2Control radius { get; set; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x0005C24E File Offset: 0x0005A44E
		// (set) Token: 0x06001413 RID: 5139 RVA: 0x0005C256 File Offset: 0x0005A456
		public TouchPhaseControl phase { get; set; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0005C25F File Offset: 0x0005A45F
		// (set) Token: 0x06001415 RID: 5141 RVA: 0x0005C267 File Offset: 0x0005A467
		public ButtonControl indirectTouch { get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0005C270 File Offset: 0x0005A470
		// (set) Token: 0x06001417 RID: 5143 RVA: 0x0005C278 File Offset: 0x0005A478
		public ButtonControl tap { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0005C281 File Offset: 0x0005A481
		// (set) Token: 0x06001419 RID: 5145 RVA: 0x0005C289 File Offset: 0x0005A489
		public IntegerControl tapCount { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x0005C292 File Offset: 0x0005A492
		// (set) Token: 0x0600141B RID: 5147 RVA: 0x0005C29A File Offset: 0x0005A49A
		public DoubleControl startTime { get; set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0005C2A3 File Offset: 0x0005A4A3
		// (set) Token: 0x0600141D RID: 5149 RVA: 0x0005C2AB File Offset: 0x0005A4AB
		public Vector2Control startPosition { get; set; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x0005C2B4 File Offset: 0x0005A4B4
		public unsafe bool isInProgress
		{
			get
			{
				TouchPhase touchPhase = (TouchPhase)(*this.phase.value);
				return touchPhase - TouchPhase.Began <= 1 || touchPhase == TouchPhase.Stationary;
			}
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0005C2DB File Offset: 0x0005A4DB
		public TouchControl()
		{
			this.m_StateBlock.format = new FourCC('T', 'O', 'U', 'C');
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0005C2FC File Offset: 0x0005A4FC
		protected override void FinishSetup()
		{
			this.press = base.GetChildControl<TouchPressControl>("press");
			this.displayIndex = base.GetChildControl<IntegerControl>("displayIndex");
			this.touchId = base.GetChildControl<IntegerControl>("touchId");
			this.position = base.GetChildControl<Vector2Control>("position");
			this.delta = base.GetChildControl<DeltaControl>("delta");
			this.pressure = base.GetChildControl<AxisControl>("pressure");
			this.radius = base.GetChildControl<Vector2Control>("radius");
			this.phase = base.GetChildControl<TouchPhaseControl>("phase");
			this.indirectTouch = base.GetChildControl<ButtonControl>("indirectTouch");
			this.tap = base.GetChildControl<ButtonControl>("tap");
			this.tapCount = base.GetChildControl<IntegerControl>("tapCount");
			this.startTime = base.GetChildControl<DoubleControl>("startTime");
			this.startPosition = base.GetChildControl<Vector2Control>("startPosition");
			base.FinishSetup();
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0005C3EC File Offset: 0x0005A5EC
		public unsafe override TouchState ReadUnprocessedValueFromState(void* statePtr)
		{
			TouchState* valuePtr = (TouchState*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			return *valuePtr;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0005C410 File Offset: 0x0005A610
		public unsafe override void WriteValueIntoState(TouchState value, void* statePtr)
		{
			TouchState* valuePtr = (TouchState*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			UnsafeUtility.MemCpy((void*)valuePtr, UnsafeUtility.AddressOf<TouchState>(ref value), (long)UnsafeUtility.SizeOf<TouchState>());
		}
	}
}
