using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000133 RID: 307
	[NullableContext(1)]
	[Nullable(0)]
	public class MemoryTraceWriter : ITraceWriter
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0002E875 File Offset: 0x0002CA75
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x0002E87D File Offset: 0x0002CA7D
		public TraceLevel LevelFilter { get; set; }

		// Token: 0x06000977 RID: 2423 RVA: 0x0002E886 File Offset: 0x0002CA86
		public MemoryTraceWriter()
		{
			this.LevelFilter = TraceLevel.Verbose;
			this._traceMessages = new Queue<string>();
			this._lock = new object();
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0002E8AC File Offset: 0x0002CAAC
		public void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", CultureInfo.InvariantCulture));
			stringBuilder.Append(" ");
			stringBuilder.Append(level.ToString("g"));
			stringBuilder.Append(" ");
			stringBuilder.Append(message);
			string text = stringBuilder.ToString();
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._traceMessages.Count >= 1000)
				{
					this._traceMessages.Dequeue();
				}
				this._traceMessages.Enqueue(text);
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0002E970 File Offset: 0x0002CB70
		public IEnumerable<string> GetTraceMessages()
		{
			return this._traceMessages;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0002E978 File Offset: 0x0002CB78
		public override string ToString()
		{
			object @lock = this._lock;
			string text2;
			lock (@lock)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in this._traceMessages)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.AppendLine();
					}
					stringBuilder.Append(text);
				}
				text2 = stringBuilder.ToString();
			}
			return text2;
		}

		// Token: 0x040005B6 RID: 1462
		private readonly Queue<string> _traceMessages;

		// Token: 0x040005B7 RID: 1463
		private readonly object _lock;
	}
}
