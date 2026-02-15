using System;

namespace WindBot
{
	// Token: 0x020001E9 RID: 489
	public static class Logger
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x00027F88 File Offset: 0x00026188
		public static void WriteLine(string message)
		{
			Console.WriteLine("[" + DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "] " + message);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DebugWriteLine(string message)
		{
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00027FBC File Offset: 0x000261BC
		public static void WriteErrorLine(string message)
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Error.WriteLine("[" + DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "] " + message);
			Console.ResetColor();
		}
	}
}
