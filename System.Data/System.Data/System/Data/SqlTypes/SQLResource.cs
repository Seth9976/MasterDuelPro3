using System;

namespace System.Data.SqlTypes
{
	// Token: 0x020000BC RID: 188
	internal static class SQLResource
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x000377C5 File Offset: 0x000359C5
		internal static string NullString
		{
			get
			{
				return "Null";
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x000377CC File Offset: 0x000359CC
		internal static string ArithOverflowMessage
		{
			get
			{
				return "Arithmetic Overflow.";
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x000377D3 File Offset: 0x000359D3
		internal static string DivideByZeroMessage
		{
			get
			{
				return "Divide by zero error encountered.";
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x000377DA File Offset: 0x000359DA
		internal static string NullValueMessage
		{
			get
			{
				return "Data is Null. This method or property cannot be called on Null values.";
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x000377E1 File Offset: 0x000359E1
		internal static string TruncationMessage
		{
			get
			{
				return "Numeric arithmetic causes truncation.";
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x000377E8 File Offset: 0x000359E8
		internal static string DateTimeOverflowMessage
		{
			get
			{
				return "SqlDateTime overflow. Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM.";
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x000377EF File Offset: 0x000359EF
		internal static string ConcatDiffCollationMessage
		{
			get
			{
				return "Two strings to be concatenated have different collation.";
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x000377F6 File Offset: 0x000359F6
		internal static string CompareDiffCollationMessage
		{
			get
			{
				return "Two strings to be compared have different collation.";
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x000377FD File Offset: 0x000359FD
		internal static string ConversionOverflowMessage
		{
			get
			{
				return "Conversion overflows.";
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00037804 File Offset: 0x00035A04
		internal static string TimeZoneSpecifiedMessage
		{
			get
			{
				return "A time zone was specified. SqlDateTime does not support time zones.";
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0003780B File Offset: 0x00035A0B
		internal static string InvalidPrecScaleMessage
		{
			get
			{
				return "Invalid numeric precision/scale.";
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00037812 File Offset: 0x00035A12
		internal static string FormatMessage
		{
			get
			{
				return "The input wasn't in a correct format.";
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00037819 File Offset: 0x00035A19
		internal static string InvalidOpStreamClosed(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream is closed.", method);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00037826 File Offset: 0x00035A26
		internal static string InvalidOpStreamNonWritable(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream non-writable.", method);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00037833 File Offset: 0x00035A33
		internal static string InvalidOpStreamNonReadable(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream non-readable.", method);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00037840 File Offset: 0x00035A40
		internal static string InvalidOpStreamNonSeekable(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream is non-seekable.", method);
		}
	}
}
