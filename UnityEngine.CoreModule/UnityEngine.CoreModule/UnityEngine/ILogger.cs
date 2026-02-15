using System;

namespace UnityEngine
{
	// Token: 0x0200013E RID: 318
	public interface ILogger : ILogHandler
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000D39 RID: 3385
		ILogHandler logHandler { get; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000D3A RID: 3386
		bool logEnabled { get; }

		// Token: 0x06000D3B RID: 3387
		bool IsLogTypeAllowed(LogType logType);

		// Token: 0x06000D3C RID: 3388
		void Log(LogType logType, object message);

		// Token: 0x06000D3D RID: 3389
		void Log(LogType logType, object message, Object context);

		// Token: 0x06000D3E RID: 3390
		void LogError(string tag, object message);

		// Token: 0x06000D3F RID: 3391
		void LogFormat(LogType logType, string format, params object[] args);
	}
}
