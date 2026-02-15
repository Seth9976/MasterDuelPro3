using System;
using System.Diagnostics;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000250 RID: 592
	internal readonly struct ValueStopwatch
	{
		// Token: 0x06000D37 RID: 3383 RVA: 0x0002DF1A File Offset: 0x0002C11A
		public static ValueStopwatch StartNew()
		{
			return new ValueStopwatch(Stopwatch.GetTimestamp());
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0002DF26 File Offset: 0x0002C126
		private ValueStopwatch(long startTimestamp)
		{
			this.startTimestamp = startTimestamp;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0002DF2F File Offset: 0x0002C12F
		public TimeSpan Elapsed
		{
			get
			{
				return TimeSpan.FromTicks(this.ElapsedTicks);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0002DF3C File Offset: 0x0002C13C
		public bool IsInvalid
		{
			get
			{
				return this.startTimestamp == 0L;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0002DF48 File Offset: 0x0002C148
		public long ElapsedTicks
		{
			get
			{
				if (this.startTimestamp == 0L)
				{
					throw new InvalidOperationException("Detected invalid initialization(use 'default'), only to create from StartNew().");
				}
				return (long)((double)(Stopwatch.GetTimestamp() - this.startTimestamp) * ValueStopwatch.TimestampToTicks);
			}
		}

		// Token: 0x040006A9 RID: 1705
		private static readonly double TimestampToTicks = 10000000.0 / (double)Stopwatch.Frequency;

		// Token: 0x040006AA RID: 1706
		private readonly long startTimestamp;
	}
}
