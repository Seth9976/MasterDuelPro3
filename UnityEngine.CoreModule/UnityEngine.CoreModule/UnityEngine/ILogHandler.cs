using System;

namespace UnityEngine
{
	// Token: 0x0200013F RID: 319
	public interface ILogHandler
	{
		// Token: 0x06000D40 RID: 3392
		void LogFormat(LogType logType, Object context, string format, params object[] args);

		// Token: 0x06000D41 RID: 3393
		void LogException(Exception exception, Object context);
	}
}
