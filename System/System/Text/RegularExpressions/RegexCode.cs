using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000139 RID: 313
	internal sealed class RegexCode
	{
		// Token: 0x06000672 RID: 1650 RVA: 0x00020D24 File Offset: 0x0001EF24
		public RegexCode(int[] codes, List<string> stringlist, int trackcount, Hashtable caps, int capsize, RegexBoyerMoore bmPrefix, RegexPrefix? fcPrefix, int anchors, bool rightToLeft)
		{
			this.Codes = codes;
			this.Strings = stringlist.ToArray();
			this.TrackCount = trackcount;
			this.Caps = caps;
			this.CapSize = capsize;
			this.BMPrefix = bmPrefix;
			this.FCPrefix = fcPrefix;
			this.Anchors = anchors;
			this.RightToLeft = rightToLeft;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00020D84 File Offset: 0x0001EF84
		public static bool OpcodeBacktracks(int Op)
		{
			Op &= 63;
			switch (Op)
			{
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 38:
				return true;
			}
			return false;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00020E34 File Offset: 0x0001F034
		public static int OpcodeSize(int opcode)
		{
			opcode &= 63;
			switch (opcode)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 28:
			case 29:
			case 32:
				return 3;
			case 9:
			case 10:
			case 11:
			case 12:
			case 13:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 37:
			case 38:
			case 39:
				return 2;
			case 14:
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 30:
			case 31:
			case 33:
			case 34:
			case 35:
			case 36:
			case 40:
			case 41:
			case 42:
				return 1;
			default:
				throw new ArgumentException(SR.Format("Unexpected opcode in regular expression generation: {0}.", opcode.ToString(CultureInfo.CurrentCulture)));
			}
		}

		// Token: 0x0400052E RID: 1326
		public const int Onerep = 0;

		// Token: 0x0400052F RID: 1327
		public const int Notonerep = 1;

		// Token: 0x04000530 RID: 1328
		public const int Setrep = 2;

		// Token: 0x04000531 RID: 1329
		public const int Oneloop = 3;

		// Token: 0x04000532 RID: 1330
		public const int Notoneloop = 4;

		// Token: 0x04000533 RID: 1331
		public const int Setloop = 5;

		// Token: 0x04000534 RID: 1332
		public const int Onelazy = 6;

		// Token: 0x04000535 RID: 1333
		public const int Notonelazy = 7;

		// Token: 0x04000536 RID: 1334
		public const int Setlazy = 8;

		// Token: 0x04000537 RID: 1335
		public const int One = 9;

		// Token: 0x04000538 RID: 1336
		public const int Notone = 10;

		// Token: 0x04000539 RID: 1337
		public const int Set = 11;

		// Token: 0x0400053A RID: 1338
		public const int Multi = 12;

		// Token: 0x0400053B RID: 1339
		public const int Ref = 13;

		// Token: 0x0400053C RID: 1340
		public const int Bol = 14;

		// Token: 0x0400053D RID: 1341
		public const int Eol = 15;

		// Token: 0x0400053E RID: 1342
		public const int Boundary = 16;

		// Token: 0x0400053F RID: 1343
		public const int Nonboundary = 17;

		// Token: 0x04000540 RID: 1344
		public const int Beginning = 18;

		// Token: 0x04000541 RID: 1345
		public const int Start = 19;

		// Token: 0x04000542 RID: 1346
		public const int EndZ = 20;

		// Token: 0x04000543 RID: 1347
		public const int End = 21;

		// Token: 0x04000544 RID: 1348
		public const int Nothing = 22;

		// Token: 0x04000545 RID: 1349
		public const int Lazybranch = 23;

		// Token: 0x04000546 RID: 1350
		public const int Branchmark = 24;

		// Token: 0x04000547 RID: 1351
		public const int Lazybranchmark = 25;

		// Token: 0x04000548 RID: 1352
		public const int Nullcount = 26;

		// Token: 0x04000549 RID: 1353
		public const int Setcount = 27;

		// Token: 0x0400054A RID: 1354
		public const int Branchcount = 28;

		// Token: 0x0400054B RID: 1355
		public const int Lazybranchcount = 29;

		// Token: 0x0400054C RID: 1356
		public const int Nullmark = 30;

		// Token: 0x0400054D RID: 1357
		public const int Setmark = 31;

		// Token: 0x0400054E RID: 1358
		public const int Capturemark = 32;

		// Token: 0x0400054F RID: 1359
		public const int Getmark = 33;

		// Token: 0x04000550 RID: 1360
		public const int Setjump = 34;

		// Token: 0x04000551 RID: 1361
		public const int Backjump = 35;

		// Token: 0x04000552 RID: 1362
		public const int Forejump = 36;

		// Token: 0x04000553 RID: 1363
		public const int Testref = 37;

		// Token: 0x04000554 RID: 1364
		public const int Goto = 38;

		// Token: 0x04000555 RID: 1365
		public const int Prune = 39;

		// Token: 0x04000556 RID: 1366
		public const int Stop = 40;

		// Token: 0x04000557 RID: 1367
		public const int ECMABoundary = 41;

		// Token: 0x04000558 RID: 1368
		public const int NonECMABoundary = 42;

		// Token: 0x04000559 RID: 1369
		public const int Mask = 63;

		// Token: 0x0400055A RID: 1370
		public const int Rtl = 64;

		// Token: 0x0400055B RID: 1371
		public const int Back = 128;

		// Token: 0x0400055C RID: 1372
		public const int Back2 = 256;

		// Token: 0x0400055D RID: 1373
		public const int Ci = 512;

		// Token: 0x0400055E RID: 1374
		public readonly int[] Codes;

		// Token: 0x0400055F RID: 1375
		public readonly string[] Strings;

		// Token: 0x04000560 RID: 1376
		public readonly int TrackCount;

		// Token: 0x04000561 RID: 1377
		public readonly Hashtable Caps;

		// Token: 0x04000562 RID: 1378
		public readonly int CapSize;

		// Token: 0x04000563 RID: 1379
		public readonly RegexPrefix? FCPrefix;

		// Token: 0x04000564 RID: 1380
		public readonly RegexBoyerMoore BMPrefix;

		// Token: 0x04000565 RID: 1381
		public readonly int Anchors;

		// Token: 0x04000566 RID: 1382
		public readonly bool RightToLeft;
	}
}
