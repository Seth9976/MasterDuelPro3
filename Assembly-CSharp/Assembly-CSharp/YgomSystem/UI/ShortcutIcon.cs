using System;
using YgomSystem.Utility;

namespace YgomSystem.UI
{
	// Token: 0x020005F8 RID: 1528
	public class ShortcutIcon : DeviceIcon
	{
		// Token: 0x060030EE RID: 12526 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetKeyType(SelectorManager.KeyType keyType, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00, Action onCompleted = null)
		{
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAnalogType(SelectorManager.AnalogType analogType, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00, Action onCompleted = null)
		{
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetMouseType(SelectorManager.MouseType mouseType, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00, Action onCompleted = null)
		{
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void UpdateDisplay(Action onCompleted = null)
		{
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetIcon(Action onCompleted = null)
		{
		}

		// Token: 0x04002D5F RID: 11615
		public SelectorManager.KeyType keyType;

		// Token: 0x04002D60 RID: 11616
		public SelectorManager.AnalogType analogType;

		// Token: 0x04002D61 RID: 11617
		public SelectorManager.MouseType mouseType;

		// Token: 0x04002D62 RID: 11618
		public GamePadIconUtil.Variation iconVariation;

		// Token: 0x04002D63 RID: 11619
		public ShortcutIcon.Mode mode;

		// Token: 0x04002D64 RID: 11620
		public ShortcutIcon.AnalogDirection analogDirection;

		// Token: 0x020005F9 RID: 1529
		public enum Mode
		{
			// Token: 0x04002D66 RID: 11622
			None,
			// Token: 0x04002D67 RID: 11623
			PadKey,
			// Token: 0x04002D68 RID: 11624
			Mouse,
			// Token: 0x04002D69 RID: 11625
			Analog
		}

		// Token: 0x020005FA RID: 1530
		public enum AnalogDirection
		{
			// Token: 0x04002D6B RID: 11627
			Horizontal,
			// Token: 0x04002D6C RID: 11628
			Vertical
		}
	}
}
