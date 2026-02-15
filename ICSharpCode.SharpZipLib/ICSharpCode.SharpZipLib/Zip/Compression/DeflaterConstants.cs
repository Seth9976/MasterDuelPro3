using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000056 RID: 86
	public static class DeflaterConstants
	{
		// Token: 0x040001B4 RID: 436
		public const bool DEBUGGING = false;

		// Token: 0x040001B5 RID: 437
		public const int STORED_BLOCK = 0;

		// Token: 0x040001B6 RID: 438
		public const int STATIC_TREES = 1;

		// Token: 0x040001B7 RID: 439
		public const int DYN_TREES = 2;

		// Token: 0x040001B8 RID: 440
		public const int PRESET_DICT = 32;

		// Token: 0x040001B9 RID: 441
		public const int DEFAULT_MEM_LEVEL = 8;

		// Token: 0x040001BA RID: 442
		public const int MAX_MATCH = 258;

		// Token: 0x040001BB RID: 443
		public const int MIN_MATCH = 3;

		// Token: 0x040001BC RID: 444
		public const int MAX_WBITS = 15;

		// Token: 0x040001BD RID: 445
		public const int WSIZE = 32768;

		// Token: 0x040001BE RID: 446
		public const int WMASK = 32767;

		// Token: 0x040001BF RID: 447
		public const int HASH_BITS = 15;

		// Token: 0x040001C0 RID: 448
		public const int HASH_SIZE = 32768;

		// Token: 0x040001C1 RID: 449
		public const int HASH_MASK = 32767;

		// Token: 0x040001C2 RID: 450
		public const int HASH_SHIFT = 5;

		// Token: 0x040001C3 RID: 451
		public const int MIN_LOOKAHEAD = 262;

		// Token: 0x040001C4 RID: 452
		public const int MAX_DIST = 32506;

		// Token: 0x040001C5 RID: 453
		public const int PENDING_BUF_SIZE = 65536;

		// Token: 0x040001C6 RID: 454
		public static int MAX_BLOCK_SIZE = Math.Min(65535, 65531);

		// Token: 0x040001C7 RID: 455
		public const int DEFLATE_STORED = 0;

		// Token: 0x040001C8 RID: 456
		public const int DEFLATE_FAST = 1;

		// Token: 0x040001C9 RID: 457
		public const int DEFLATE_SLOW = 2;

		// Token: 0x040001CA RID: 458
		public static int[] GOOD_LENGTH = new int[] { 0, 4, 4, 4, 4, 8, 8, 8, 32, 32 };

		// Token: 0x040001CB RID: 459
		public static int[] MAX_LAZY = new int[] { 0, 4, 5, 6, 4, 16, 16, 32, 128, 258 };

		// Token: 0x040001CC RID: 460
		public static int[] NICE_LENGTH = new int[] { 0, 8, 16, 32, 16, 32, 128, 128, 258, 258 };

		// Token: 0x040001CD RID: 461
		public static int[] MAX_CHAIN = new int[] { 0, 4, 8, 32, 16, 32, 128, 256, 1024, 4096 };

		// Token: 0x040001CE RID: 462
		public static int[] COMPR_FUNC = new int[] { 0, 1, 1, 1, 1, 2, 2, 2, 2, 2 };
	}
}
