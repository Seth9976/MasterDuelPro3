using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004DC RID: 1244
	public class VisualElementFocusChangeDirection : FocusChangeDirection
	{
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x0008057C File Offset: 0x0007E77C
		public static FocusChangeDirection left
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Left;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x00080583 File Offset: 0x0007E783
		public static FocusChangeDirection right
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Right;
			}
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x00044C35 File Offset: 0x00042E35
		protected VisualElementFocusChangeDirection(int value)
			: base(value)
		{
		}

		// Token: 0x04000FD5 RID: 4053
		private static readonly VisualElementFocusChangeDirection s_Left = new VisualElementFocusChangeDirection(FocusChangeDirection.lastValue + 1);

		// Token: 0x04000FD6 RID: 4054
		private static readonly VisualElementFocusChangeDirection s_Right = new VisualElementFocusChangeDirection(FocusChangeDirection.lastValue + 2);
	}
}
