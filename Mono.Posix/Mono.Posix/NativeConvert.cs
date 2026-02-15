using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000004 RID: 4
	[CLSCompliant(false)]
	public sealed class NativeConvert
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000206A File Offset: 0x0000026A
		private static void ThrowArgumentException(object value)
		{
			throw new ArgumentOutOfRangeException("value", value, Locale.GetText("Current platform doesn't support this value."));
		}

		// Token: 0x06000005 RID: 5
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromPollEvents")]
		private static extern int FromPollEvents(PollEvents value, out short rval);

		// Token: 0x06000006 RID: 6 RVA: 0x00002084 File Offset: 0x00000284
		public static short FromPollEvents(PollEvents value)
		{
			short num;
			if (NativeConvert.FromPollEvents(value, out num) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return num;
		}

		// Token: 0x06000007 RID: 7
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToPollEvents")]
		private static extern int ToPollEvents(short value, out PollEvents rval);

		// Token: 0x06000008 RID: 8 RVA: 0x000020A8 File Offset: 0x000002A8
		public static PollEvents ToPollEvents(short value)
		{
			PollEvents pollEvents;
			if (NativeConvert.ToPollEvents(value, out pollEvents) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return pollEvents;
		}

		// Token: 0x04000002 RID: 2
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000003 RID: 3
		public static readonly DateTime LocalUnixEpoch = new DateTime(1970, 1, 1);

		// Token: 0x04000004 RID: 4
		public static readonly TimeSpan LocalUtcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.UtcNow);

		// Token: 0x04000005 RID: 5
		private static readonly string[][] fopen_modes = new string[][]
		{
			new string[] { "Can't Read+Create", "wb", "w+b" },
			new string[] { "Can't Read+Create", "wb", "w+b" },
			new string[] { "rb", "wb", "r+b" },
			new string[] { "rb", "wb", "r+b" },
			new string[] { "Cannot Truncate and Read", "wb", "w+b" },
			new string[] { "Cannot Append and Read", "ab", "a+b" }
		};
	}
}
