using System;
using System.Text;

namespace AssetStudio
{
	// Token: 0x0200016B RID: 363
	public static class Logger
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x00015EE4 File Offset: 0x000140E4
		public static void Verbose(string message)
		{
			Logger.Default.Log(LoggerEvent.Verbose, message);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00015EF2 File Offset: 0x000140F2
		public static void Debug(string message)
		{
			Logger.Default.Log(LoggerEvent.Debug, message);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00015F00 File Offset: 0x00014100
		public static void Info(string message)
		{
			Logger.Default.Log(LoggerEvent.Info, message);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00015F0E File Offset: 0x0001410E
		public static void Warning(string message)
		{
			Logger.Default.Log(LoggerEvent.Warning, message);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00015F1C File Offset: 0x0001411C
		public static void Error(string message)
		{
			Logger.Default.Log(LoggerEvent.Error, message);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00015F2C File Offset: 0x0001412C
		public static void Error(string message, Exception e)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(message);
			sb.AppendLine(e.ToString());
			Logger.Default.Log(LoggerEvent.Error, sb.ToString());
		}

		// Token: 0x0400098A RID: 2442
		public static ILogger Default = new DummyLogger();
	}
}
