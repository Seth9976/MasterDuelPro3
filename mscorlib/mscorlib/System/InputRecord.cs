using System;

namespace System
{
	// Token: 0x020001FE RID: 510
	internal struct InputRecord
	{
		// Token: 0x0400099A RID: 2458
		public short EventType;

		// Token: 0x0400099B RID: 2459
		public bool KeyDown;

		// Token: 0x0400099C RID: 2460
		public short RepeatCount;

		// Token: 0x0400099D RID: 2461
		public short VirtualKeyCode;

		// Token: 0x0400099E RID: 2462
		public short VirtualScanCode;

		// Token: 0x0400099F RID: 2463
		public char Character;

		// Token: 0x040009A0 RID: 2464
		public int ControlKeyState;

		// Token: 0x040009A1 RID: 2465
		private int pad1;

		// Token: 0x040009A2 RID: 2466
		private bool pad2;
	}
}
