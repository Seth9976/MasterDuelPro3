using System;

namespace System.Threading
{
	/// <summary>Contains constants that specify infinite time-out intervals. This class cannot be inherited. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000228 RID: 552
	public static class Timeout
	{
		/// <summary>A constant used to specify an infinite waiting period, for methods that accept a <see cref="T:System.TimeSpan" /> parameter.</summary>
		// Token: 0x04000A19 RID: 2585
		public static readonly TimeSpan InfiniteTimeSpan = new TimeSpan(0, 0, 0, 0, -1);

		/// <summary>A constant used to specify an infinite waiting period. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000A1A RID: 2586
		public const int Infinite = -1;

		// Token: 0x04000A1B RID: 2587
		internal const uint UnsignedInfinite = 4294967295U;
	}
}
