using System;
using System.Collections.Generic;
using YgomGame.Card;
using YgomGame.Deck;

// Token: 0x0200004D RID: 77
public class SortComparer
{
	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000147 RID: 327 RVA: 0x0000216A File Offset: 0x0000036A
	private static Content m_cci
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000148 RID: 328 RVA: 0x00002739 File Offset: 0x00000939
	public SortComparer(SortComparer.Sorter s)
	{
	}

	// Token: 0x06000149 RID: 329 RVA: 0x000029CC File Offset: 0x00000BCC
	public int Compare(int a, int b)
	{
		return 0;
	}

	// Token: 0x0600014A RID: 330 RVA: 0x000029CC File Offset: 0x00000BCC
	public int Compare(CardBaseData a, CardBaseData b)
	{
		return 0;
	}

	// Token: 0x0600014B RID: 331 RVA: 0x000029CC File Offset: 0x00000BCC
	public static int Compare(int a, int b, SortComparer.Sorter sorter)
	{
		return 0;
	}

	// Token: 0x0600014C RID: 332 RVA: 0x000029CC File Offset: 0x00000BCC
	public static int Compare(CardBaseData a, CardBaseData b, SortComparer.Sorter sorter)
	{
		return 0;
	}

	// Token: 0x0600014D RID: 333 RVA: 0x000029CC File Offset: 0x00000BCC
	public static bool isDesc(SortComparer.ORDER o)
	{
		return false;
	}

	// Token: 0x0600014E RID: 334 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int DefaultComparer(int a, int b, SortComparer.ORDER o = SortComparer.ORDER.Desc)
	{
		return 0;
	}

	// Token: 0x0600014F RID: 335 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int DefaultComparer(CardBaseData a, CardBaseData b, SortComparer.ORDER o = SortComparer.ORDER.Desc)
	{
		return 0;
	}

	// Token: 0x06000150 RID: 336 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int ObtainedDateComparer(CardBaseData a, CardBaseData b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000151 RID: 337 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int ObtainedDateComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000152 RID: 338 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int InventoryComparer(CardBaseData a, CardBaseData b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000153 RID: 339 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int InventoryComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000154 RID: 340 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int RarityComparer(CardBaseData a, CardBaseData b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000155 RID: 341 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int RarityComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int StarsComparer(CardBaseData a, CardBaseData b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000157 RID: 343 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int StarsComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int AtkComparer(CardBaseData a, CardBaseData b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int AtkComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int DefComparer(CardBaseData a, CardBaseData b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x0600015B RID: 347 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int DefComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int NameComparer(CardBaseData a, CardBaseData b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int NameComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x0600015E RID: 350 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int PremiumComparer(CardBaseData a, CardBaseData b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x0600015F RID: 351 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int PremiumComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000160 RID: 352 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int ShopComparer(CardBaseData a, CardBaseData b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int ShopComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int baseComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000163 RID: 355 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int frameComparer(Content.Frame fa, Content.Frame fb, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int shopFrameComparer(Content.Frame fa, Content.Frame fb, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int iconComparer(Content.Icon ia, Content.Icon ib, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000166 RID: 358 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int obtainedDateComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int starComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int linkComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int inventoryComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int rarityComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x0600016B RID: 363 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int atkComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x0600016C RID: 364 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int nameComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x0600016D RID: 365 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int defComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x0600016E RID: 366 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int premiumIDComparer(int a, int b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int ownedComparer(bool a, bool b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000170 RID: 368 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int rentaledComparer(bool a, bool b, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x000029CC File Offset: 0x00000BCC
	private static int starsandlinkConverter(int a, SortComparer.ORDER o)
	{
		return 0;
	}

	// Token: 0x040001D8 RID: 472
	private const int MAX_DEF = 9999999;

	// Token: 0x040001D9 RID: 473
	private const int MAX_ATK = 9999999;

	// Token: 0x040001DA RID: 474
	private const int MAX_STAR = 9999999;

	// Token: 0x040001DB RID: 475
	private SortComparer.Sorter sorter;

	// Token: 0x040001DC RID: 476
	private static Dictionary<SortComparer.METHOD, Func<int, int, SortComparer.ORDER, int>> comparers;

	// Token: 0x040001DD RID: 477
	private static Dictionary<SortComparer.METHOD, Func<CardBaseData, CardBaseData, SortComparer.ORDER, int>> comparers2;

	// Token: 0x040001DE RID: 478
	private const int FRAMESORTORDER_RITUAL = 30;

	// Token: 0x040001DF RID: 479
	private const int FRAMESORTORDER_RITUALPEND = 31;

	// Token: 0x040001E0 RID: 480
	private const int FRAMESORTORDER_EFFECT = 20;

	// Token: 0x040001E1 RID: 481
	private const int FRAMESORTORDER_PENDFX = 21;

	// Token: 0x040001E2 RID: 482
	private const int FRAMESORTORDER_NORMAL = 10;

	// Token: 0x040001E3 RID: 483
	private const int FRAMESORTORDER_PEND = 11;

	// Token: 0x040001E4 RID: 484
	private const int FRAMESORTORDER_FUSION = 100;

	// Token: 0x040001E5 RID: 485
	private const int FRAMESORTORDER_FUSIONPEND = 110;

	// Token: 0x040001E6 RID: 486
	private const int FRAMESORTORDER_SYNC = 120;

	// Token: 0x040001E7 RID: 487
	private const int FRAMESORTORDER_DSYNC = 130;

	// Token: 0x040001E8 RID: 488
	private const int FRAMESORTORDER_SYNCPEND = 140;

	// Token: 0x040001E9 RID: 489
	private const int FRAMESORTORDER_XYZ = 150;

	// Token: 0x040001EA RID: 490
	private const int FRAMESORTORDER_XYZPEND = 160;

	// Token: 0x040001EB RID: 491
	private const int FRAMESORTORDER_LINK = 170;

	// Token: 0x040001EC RID: 492
	private const int FRAMESORTORDER_MAGIC = 200;

	// Token: 0x040001ED RID: 493
	private const int FRAMESORTORDER_TRAP = 210;

	// Token: 0x040001EE RID: 494
	private const int FRAMESORTORDER_TOKEN = 220;

	// Token: 0x040001EF RID: 495
	private const int FRAMESORTORDER_RA = 230;

	// Token: 0x040001F0 RID: 496
	private const int FRAMESORTORDER_OSIRIS = 240;

	// Token: 0x040001F1 RID: 497
	private const int FRAMESORTORDER_OBERISK = 250;

	// Token: 0x040001F2 RID: 498
	private static Dictionary<Content.Frame, int> frameOrder;

	// Token: 0x040001F3 RID: 499
	private static Dictionary<Content.Frame, int> shopFrameOrder;

	// Token: 0x040001F4 RID: 500
	private const int ICONSORTORDER_NONE = 1;

	// Token: 0x040001F5 RID: 501
	private const int ICONSORTORDER_COUNTER = 2;

	// Token: 0x040001F6 RID: 502
	private const int ICONSORTORDER_FIELD = 4;

	// Token: 0x040001F7 RID: 503
	private const int ICONSORTORDER_EQUIP = 3;

	// Token: 0x040001F8 RID: 504
	private const int ICONSORTORDER_CONTINUOUS = 6;

	// Token: 0x040001F9 RID: 505
	private const int ICONSORTORDER_QUICKPLAY = 7;

	// Token: 0x040001FA RID: 506
	private const int ICONSORTORDER_RITUAL = 5;

	// Token: 0x040001FB RID: 507
	private static Dictionary<Content.Icon, int> iconOrder;

	// Token: 0x0200004E RID: 78
	public enum METHOD
	{
		// Token: 0x040001FD RID: 509
		Default,
		// Token: 0x040001FE RID: 510
		Obtained,
		// Token: 0x040001FF RID: 511
		Inventory,
		// Token: 0x04000200 RID: 512
		Rarity,
		// Token: 0x04000201 RID: 513
		StarsAndLink,
		// Token: 0x04000202 RID: 514
		Atk,
		// Token: 0x04000203 RID: 515
		Def,
		// Token: 0x04000204 RID: 516
		Name,
		// Token: 0x04000205 RID: 517
		Premium,
		// Token: 0x04000206 RID: 518
		Shop,
		// Token: 0x04000207 RID: 519
		None
	}

	// Token: 0x0200004F RID: 79
	public enum ORDER
	{
		// Token: 0x04000209 RID: 521
		Asc,
		// Token: 0x0400020A RID: 522
		Desc
	}

	// Token: 0x02000050 RID: 80
	public struct Sorter
	{
		// Token: 0x0400020B RID: 523
		public SortComparer.METHOD Method;

		// Token: 0x0400020C RID: 524
		public SortComparer.ORDER Order;
	}
}
