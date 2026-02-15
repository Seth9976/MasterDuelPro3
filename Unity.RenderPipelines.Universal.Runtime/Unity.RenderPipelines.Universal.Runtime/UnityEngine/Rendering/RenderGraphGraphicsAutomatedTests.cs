using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200000D RID: 13
	public static class RenderGraphGraphicsAutomatedTests
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002702 File Offset: 0x00000902
		private static bool activatedFromCommandLine
		{
			get
			{
				return Array.Exists<string>(Environment.GetCommandLineArgs(), (string arg) => arg == "-render-graph-reuse-tests");
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000272D File Offset: 0x0000092D
		public static bool enabled { get; } = RenderGraphGraphicsAutomatedTests.activatedFromCommandLine;
	}
}
