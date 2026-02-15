using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009A RID: 154
	public class DebugOverlay
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000D7B6 File Offset: 0x0000B9B6
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x0000D7BE File Offset: 0x0000B9BE
		public int x { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000D7C7 File Offset: 0x0000B9C7
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0000D7CF File Offset: 0x0000B9CF
		public int y { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
		public int overlaySize { get; private set; }

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000D7E9 File Offset: 0x0000B9E9
		public void StartOverlay(int initialX, int initialY, int overlaySize, int screenWidth)
		{
			this.x = initialX;
			this.y = initialY;
			this.overlaySize = overlaySize;
			this.m_InitialPositionX = initialX;
			this.m_ScreenWidth = screenWidth;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000D810 File Offset: 0x0000BA10
		public Rect Next(float aspect = 1f)
		{
			int overlayWidth = (int)((float)this.overlaySize * aspect);
			if (this.x + overlayWidth > this.m_ScreenWidth && this.x > this.m_InitialPositionX)
			{
				this.x = this.m_InitialPositionX;
				this.y -= this.overlaySize;
			}
			Rect rect = new Rect((float)this.x, (float)this.y, (float)overlayWidth, (float)this.overlaySize);
			this.x += overlayWidth;
			return rect;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000D88F File Offset: 0x0000BA8F
		public void SetViewport(CommandBuffer cmd)
		{
			cmd.SetViewport(new Rect((float)this.x, (float)this.y, (float)this.overlaySize, (float)this.overlaySize));
		}

		// Token: 0x04000202 RID: 514
		private int m_InitialPositionX;

		// Token: 0x04000203 RID: 515
		private int m_ScreenWidth;
	}
}
