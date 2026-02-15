using System;
using System.Globalization;

namespace UnityEngine
{
	// Token: 0x02000140 RID: 320
	public class Logger : ILogger, ILogHandler
	{
		// Token: 0x06000D42 RID: 3394 RVA: 0x000199B7 File Offset: 0x00017BB7
		public Logger(ILogHandler logHandler)
		{
			this.logHandler = logHandler;
			this.logEnabled = true;
			this.filterLogType = LogType.Log;
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x000199D9 File Offset: 0x00017BD9
		// (set) Token: 0x06000D44 RID: 3396 RVA: 0x000199E1 File Offset: 0x00017BE1
		public ILogHandler logHandler { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x000199EA File Offset: 0x00017BEA
		// (set) Token: 0x06000D46 RID: 3398 RVA: 0x000199F2 File Offset: 0x00017BF2
		public bool logEnabled { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x000199FB File Offset: 0x00017BFB
		// (set) Token: 0x06000D48 RID: 3400 RVA: 0x00019A03 File Offset: 0x00017C03
		public LogType filterLogType { get; set; }

		// Token: 0x06000D49 RID: 3401 RVA: 0x00019A0C File Offset: 0x00017C0C
		public bool IsLogTypeAllowed(LogType logType)
		{
			bool logEnabled = this.logEnabled;
			if (logEnabled)
			{
				bool flag = logType == LogType.Exception;
				if (flag)
				{
					return true;
				}
				bool flag2 = this.filterLogType != LogType.Exception;
				if (flag2)
				{
					return logType <= this.filterLogType;
				}
			}
			return false;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00019A58 File Offset: 0x00017C58
		private static string GetString(object message)
		{
			bool flag = message == null;
			string text;
			if (flag)
			{
				text = "Null";
			}
			else
			{
				IFormattable formattable = message as IFormattable;
				bool flag2 = formattable != null;
				if (flag2)
				{
					text = formattable.ToString(null, CultureInfo.InvariantCulture);
				}
				else
				{
					text = message.ToString();
				}
			}
			return text;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00019AA4 File Offset: 0x00017CA4
		public void Log(LogType logType, object message)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, null, "{0}", new object[] { Logger.GetString(message) });
			}
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00019AE0 File Offset: 0x00017CE0
		public void Log(LogType logType, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, context, "{0}", new object[] { Logger.GetString(message) });
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00019B1C File Offset: 0x00017D1C
		public void LogError(string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Error);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Error, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00019B5C File Offset: 0x00017D5C
		public void LogException(Exception exception, Object context)
		{
			bool logEnabled = this.logEnabled;
			if (logEnabled)
			{
				this.logHandler.LogException(exception, context);
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00019B84 File Offset: 0x00017D84
		public void LogFormat(LogType logType, string format, params object[] args)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, null, format, args);
			}
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00019BB0 File Offset: 0x00017DB0
		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, context, format, args);
			}
		}
	}
}
