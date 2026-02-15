using System;

namespace System
{
	// Token: 0x020001CF RID: 463
	internal interface IConsoleDriver
	{
		// Token: 0x170001D1 RID: 465
		// (set) Token: 0x0600121F RID: 4639
		ConsoleColor BackgroundColor { set; }

		// Token: 0x170001D2 RID: 466
		// (set) Token: 0x06001220 RID: 4640
		ConsoleColor ForegroundColor { set; }

		// Token: 0x06001221 RID: 4641
		ConsoleKeyInfo ReadKey(bool intercept);

		// Token: 0x06001222 RID: 4642
		void ResetColor();
	}
}
