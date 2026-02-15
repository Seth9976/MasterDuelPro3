using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies shortcut keys that can be used by menu items.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000189 RID: 393
	[ComVisible(true)]
	public enum Shortcut
	{
		/// <summary>The shortcut keys ALT+0.</summary>
		// Token: 0x04000985 RID: 2437
		Alt0 = 262192,
		/// <summary>The shortcut keys ALT+1.</summary>
		// Token: 0x04000986 RID: 2438
		Alt1,
		/// <summary>The shortcut keys ALT+2.</summary>
		// Token: 0x04000987 RID: 2439
		Alt2,
		/// <summary>The shortcut keys ALT+3.</summary>
		// Token: 0x04000988 RID: 2440
		Alt3,
		/// <summary>The shortcut keys ALT+4.</summary>
		// Token: 0x04000989 RID: 2441
		Alt4,
		/// <summary>The shortcut keys ALT+5.</summary>
		// Token: 0x0400098A RID: 2442
		Alt5,
		/// <summary>The shortcut keys ALT+6.</summary>
		// Token: 0x0400098B RID: 2443
		Alt6,
		/// <summary>The shortcut keys ALT+7.</summary>
		// Token: 0x0400098C RID: 2444
		Alt7,
		/// <summary>The shortcut keys ALT+8.</summary>
		// Token: 0x0400098D RID: 2445
		Alt8,
		/// <summary>The shortcut keys ALT+9.</summary>
		// Token: 0x0400098E RID: 2446
		Alt9,
		/// <summary>The shortcut keys ALT+BACKSPACE.</summary>
		// Token: 0x0400098F RID: 2447
		AltBksp = 262152,
		/// <summary>The shortcut keys ALT+DOWNARROW.</summary>
		// Token: 0x04000990 RID: 2448
		AltDownArrow = 262184,
		/// <summary>The shortcut keys ALT+F1.</summary>
		// Token: 0x04000991 RID: 2449
		AltF1 = 262256,
		/// <summary>The shortcut keys ALT+F10.</summary>
		// Token: 0x04000992 RID: 2450
		AltF10 = 262265,
		/// <summary>The shortcut keys ALT+F11.</summary>
		// Token: 0x04000993 RID: 2451
		AltF11,
		/// <summary>The shortcut keys ALT+F12.</summary>
		// Token: 0x04000994 RID: 2452
		AltF12,
		/// <summary>The shortcut keys ALT+F2.</summary>
		// Token: 0x04000995 RID: 2453
		AltF2 = 262257,
		/// <summary>The shortcut keys ALT+F3.</summary>
		// Token: 0x04000996 RID: 2454
		AltF3,
		/// <summary>The shortcut keys ALT+F4.</summary>
		// Token: 0x04000997 RID: 2455
		AltF4,
		/// <summary>The shortcut keys ALT+F5.</summary>
		// Token: 0x04000998 RID: 2456
		AltF5,
		/// <summary>The shortcut keys ALT+F6.</summary>
		// Token: 0x04000999 RID: 2457
		AltF6,
		/// <summary>The shortcut keys ALT+F7.</summary>
		// Token: 0x0400099A RID: 2458
		AltF7,
		/// <summary>The shortcut keys ALT+F8.</summary>
		// Token: 0x0400099B RID: 2459
		AltF8,
		/// <summary>The shortcut keys ALT+F9.</summary>
		// Token: 0x0400099C RID: 2460
		AltF9,
		/// <summary>The shortcut keys ALT+LEFTARROW.</summary>
		// Token: 0x0400099D RID: 2461
		AltLeftArrow = 262181,
		/// <summary>The shortcut keys ALT+RIGHTARROW.</summary>
		// Token: 0x0400099E RID: 2462
		AltRightArrow = 262183,
		/// <summary>The shortcut keys ALT+UPARROW.</summary>
		// Token: 0x0400099F RID: 2463
		AltUpArrow = 262182,
		/// <summary>The shortcut keys CTRL+0.</summary>
		// Token: 0x040009A0 RID: 2464
		Ctrl0 = 131120,
		/// <summary>The shortcut keys CTRL+1.</summary>
		// Token: 0x040009A1 RID: 2465
		Ctrl1,
		/// <summary>The shortcut keys CTRL+2.</summary>
		// Token: 0x040009A2 RID: 2466
		Ctrl2,
		/// <summary>The shortcut keys CTRL+3.</summary>
		// Token: 0x040009A3 RID: 2467
		Ctrl3,
		/// <summary>The shortcut keys CTRL+4.</summary>
		// Token: 0x040009A4 RID: 2468
		Ctrl4,
		/// <summary>The shortcut keys CTRL+5.</summary>
		// Token: 0x040009A5 RID: 2469
		Ctrl5,
		/// <summary>The shortcut keys CTRL+6.</summary>
		// Token: 0x040009A6 RID: 2470
		Ctrl6,
		/// <summary>The shortcut keys CTRL+7.</summary>
		// Token: 0x040009A7 RID: 2471
		Ctrl7,
		/// <summary>The shortcut keys CTRL+8.</summary>
		// Token: 0x040009A8 RID: 2472
		Ctrl8,
		/// <summary>The shortcut keys CTRL+9.</summary>
		// Token: 0x040009A9 RID: 2473
		Ctrl9,
		/// <summary>The shortcut keys CTRL+A.</summary>
		// Token: 0x040009AA RID: 2474
		CtrlA = 131137,
		/// <summary>The shortcut keys CTRL+B.</summary>
		// Token: 0x040009AB RID: 2475
		CtrlB,
		/// <summary>The shortcut keys CTRL+C.</summary>
		// Token: 0x040009AC RID: 2476
		CtrlC,
		/// <summary>The shortcut keys CTRL+D.</summary>
		// Token: 0x040009AD RID: 2477
		CtrlD,
		/// <summary>The shortcut keys CTRL+DELETE.</summary>
		// Token: 0x040009AE RID: 2478
		CtrlDel = 131118,
		/// <summary>The shortcut keys CTRL+E.</summary>
		// Token: 0x040009AF RID: 2479
		CtrlE = 131141,
		/// <summary>The shortcut keys CTRL+F.</summary>
		// Token: 0x040009B0 RID: 2480
		CtrlF,
		/// <summary>The shortcut keys CTRL+F1.</summary>
		// Token: 0x040009B1 RID: 2481
		CtrlF1 = 131184,
		/// <summary>The shortcut keys CTRL+F10.</summary>
		// Token: 0x040009B2 RID: 2482
		CtrlF10 = 131193,
		/// <summary>The shortcut keys CTRL+F11.</summary>
		// Token: 0x040009B3 RID: 2483
		CtrlF11,
		/// <summary>The shortcut keys CTRL+F12.</summary>
		// Token: 0x040009B4 RID: 2484
		CtrlF12,
		/// <summary>The shortcut keys CTRL+F2.</summary>
		// Token: 0x040009B5 RID: 2485
		CtrlF2 = 131185,
		/// <summary>The shortcut keys CTRL+F3.</summary>
		// Token: 0x040009B6 RID: 2486
		CtrlF3,
		/// <summary>The shortcut keys CTRL+F4.</summary>
		// Token: 0x040009B7 RID: 2487
		CtrlF4,
		/// <summary>The shortcut keys CTRL+F5.</summary>
		// Token: 0x040009B8 RID: 2488
		CtrlF5,
		/// <summary>The shortcut keys CTRL+F6.</summary>
		// Token: 0x040009B9 RID: 2489
		CtrlF6,
		/// <summary>The shortcut keys CTRL+F7.</summary>
		// Token: 0x040009BA RID: 2490
		CtrlF7,
		/// <summary>The shortcut keys CTRL+F8.</summary>
		// Token: 0x040009BB RID: 2491
		CtrlF8,
		/// <summary>The shortcut keys CTRL+F9.</summary>
		// Token: 0x040009BC RID: 2492
		CtrlF9,
		/// <summary>The shortcut keys CTRL+G.</summary>
		// Token: 0x040009BD RID: 2493
		CtrlG = 131143,
		/// <summary>The shortcut keys CTRL+H.</summary>
		// Token: 0x040009BE RID: 2494
		CtrlH,
		/// <summary>The shortcut keys CTRL+I.</summary>
		// Token: 0x040009BF RID: 2495
		CtrlI,
		/// <summary>The shortcut keys CTRL+INSERT.</summary>
		// Token: 0x040009C0 RID: 2496
		CtrlIns = 131117,
		/// <summary>The shortcut keys CTRL+J.</summary>
		// Token: 0x040009C1 RID: 2497
		CtrlJ = 131146,
		/// <summary>The shortcut keys CTRL+K.</summary>
		// Token: 0x040009C2 RID: 2498
		CtrlK,
		/// <summary>The shortcut keys CTRL+L.</summary>
		// Token: 0x040009C3 RID: 2499
		CtrlL,
		/// <summary>The shortcut keys CTRL+M.</summary>
		// Token: 0x040009C4 RID: 2500
		CtrlM,
		/// <summary>The shortcut keys CTRL+N.</summary>
		// Token: 0x040009C5 RID: 2501
		CtrlN,
		/// <summary>The shortcut keys CTRL+O.</summary>
		// Token: 0x040009C6 RID: 2502
		CtrlO,
		/// <summary>The shortcut keys CTRL+P.</summary>
		// Token: 0x040009C7 RID: 2503
		CtrlP,
		/// <summary>The shortcut keys CTRL+Q.</summary>
		// Token: 0x040009C8 RID: 2504
		CtrlQ,
		/// <summary>The shortcut keys CTRL+R.</summary>
		// Token: 0x040009C9 RID: 2505
		CtrlR,
		/// <summary>The shortcut keys CTRL+S.</summary>
		// Token: 0x040009CA RID: 2506
		CtrlS,
		/// <summary>The shortcut keys CTRL+SHIFT+0.</summary>
		// Token: 0x040009CB RID: 2507
		CtrlShift0 = 196656,
		/// <summary>The shortcut keys CTRL+SHIFT+1.</summary>
		// Token: 0x040009CC RID: 2508
		CtrlShift1,
		/// <summary>The shortcut keys CTRL+SHIFT+2.</summary>
		// Token: 0x040009CD RID: 2509
		CtrlShift2,
		/// <summary>The shortcut keys CTRL+SHIFT+3.</summary>
		// Token: 0x040009CE RID: 2510
		CtrlShift3,
		/// <summary>The shortcut keys CTRL+SHIFT+4.</summary>
		// Token: 0x040009CF RID: 2511
		CtrlShift4,
		/// <summary>The shortcut keys CTRL+SHIFT+5.</summary>
		// Token: 0x040009D0 RID: 2512
		CtrlShift5,
		/// <summary>The shortcut keys CTRL+SHIFT+6.</summary>
		// Token: 0x040009D1 RID: 2513
		CtrlShift6,
		/// <summary>The shortcut keys CTRL+SHIFT+7.</summary>
		// Token: 0x040009D2 RID: 2514
		CtrlShift7,
		/// <summary>The shortcut keys CTRL+SHIFT+8.</summary>
		// Token: 0x040009D3 RID: 2515
		CtrlShift8,
		/// <summary>The shortcut keys CTRL+SHIFT+9.</summary>
		// Token: 0x040009D4 RID: 2516
		CtrlShift9,
		/// <summary>The shortcut keys CTRL+SHIFT+A.</summary>
		// Token: 0x040009D5 RID: 2517
		CtrlShiftA = 196673,
		/// <summary>The shortcut keys CTRL+SHIFT+B.</summary>
		// Token: 0x040009D6 RID: 2518
		CtrlShiftB,
		/// <summary>The shortcut keys CTRL+SHIFT+C.</summary>
		// Token: 0x040009D7 RID: 2519
		CtrlShiftC,
		/// <summary>The shortcut keys CTRL+SHIFT+D.</summary>
		// Token: 0x040009D8 RID: 2520
		CtrlShiftD,
		/// <summary>The shortcut keys CTRL+SHIFT+E.</summary>
		// Token: 0x040009D9 RID: 2521
		CtrlShiftE,
		/// <summary>The shortcut keys CTRL+SHIFT+F.</summary>
		// Token: 0x040009DA RID: 2522
		CtrlShiftF,
		/// <summary>The shortcut keys CTRL+SHIFT+F1.</summary>
		// Token: 0x040009DB RID: 2523
		CtrlShiftF1 = 196720,
		/// <summary>The shortcut keys CTRL+SHIFT+F10.</summary>
		// Token: 0x040009DC RID: 2524
		CtrlShiftF10 = 196729,
		/// <summary>The shortcut keys CTRL+SHIFT+F11.</summary>
		// Token: 0x040009DD RID: 2525
		CtrlShiftF11,
		/// <summary>The shortcut keys CTRL+SHIFT+F12.</summary>
		// Token: 0x040009DE RID: 2526
		CtrlShiftF12,
		/// <summary>The shortcut keys CTRL+SHIFT+F2.</summary>
		// Token: 0x040009DF RID: 2527
		CtrlShiftF2 = 196721,
		/// <summary>The shortcut keys CTRL+SHIFT+F3.</summary>
		// Token: 0x040009E0 RID: 2528
		CtrlShiftF3,
		/// <summary>The shortcut keys CTRL+SHIFT+F4.</summary>
		// Token: 0x040009E1 RID: 2529
		CtrlShiftF4,
		/// <summary>The shortcut keys CTRL+SHIFT+F5.</summary>
		// Token: 0x040009E2 RID: 2530
		CtrlShiftF5,
		/// <summary>The shortcut keys CTRL+SHIFT+F6.</summary>
		// Token: 0x040009E3 RID: 2531
		CtrlShiftF6,
		/// <summary>The shortcut keys CTRL+SHIFT+F7.</summary>
		// Token: 0x040009E4 RID: 2532
		CtrlShiftF7,
		/// <summary>The shortcut keys CTRL+SHIFT+F8.</summary>
		// Token: 0x040009E5 RID: 2533
		CtrlShiftF8,
		/// <summary>The shortcut keys CTRL+SHIFT+F9.</summary>
		// Token: 0x040009E6 RID: 2534
		CtrlShiftF9,
		/// <summary>The shortcut keys CTRL+SHIFT+G.</summary>
		// Token: 0x040009E7 RID: 2535
		CtrlShiftG = 196679,
		/// <summary>The shortcut keys CTRL+SHIFT+H.</summary>
		// Token: 0x040009E8 RID: 2536
		CtrlShiftH,
		/// <summary>The shortcut keys CTRL+SHIFT+I.</summary>
		// Token: 0x040009E9 RID: 2537
		CtrlShiftI,
		/// <summary>The shortcut keys CTRL+SHIFT+J.</summary>
		// Token: 0x040009EA RID: 2538
		CtrlShiftJ,
		/// <summary>The shortcut keys CTRL+SHIFT+K.</summary>
		// Token: 0x040009EB RID: 2539
		CtrlShiftK,
		/// <summary>The shortcut keys CTRL+SHIFT+L.</summary>
		// Token: 0x040009EC RID: 2540
		CtrlShiftL,
		/// <summary>The shortcut keys CTRL+SHIFT+M.</summary>
		// Token: 0x040009ED RID: 2541
		CtrlShiftM,
		/// <summary>The shortcut keys CTRL+SHIFT+N.</summary>
		// Token: 0x040009EE RID: 2542
		CtrlShiftN,
		/// <summary>The shortcut keys CTRL+SHIFT+O.</summary>
		// Token: 0x040009EF RID: 2543
		CtrlShiftO,
		/// <summary>The shortcut keys CTRL+SHIFT+P.</summary>
		// Token: 0x040009F0 RID: 2544
		CtrlShiftP,
		/// <summary>The shortcut keys CTRL+SHIFT+Q.</summary>
		// Token: 0x040009F1 RID: 2545
		CtrlShiftQ,
		/// <summary>The shortcut keys CTRL+SHIFT+R.</summary>
		// Token: 0x040009F2 RID: 2546
		CtrlShiftR,
		/// <summary>The shortcut keys CTRL+SHIFT+S.</summary>
		// Token: 0x040009F3 RID: 2547
		CtrlShiftS,
		/// <summary>The shortcut keys CTRL+SHIFT+T.</summary>
		// Token: 0x040009F4 RID: 2548
		CtrlShiftT,
		/// <summary>The shortcut keys CTRL+SHIFT+U.</summary>
		// Token: 0x040009F5 RID: 2549
		CtrlShiftU,
		/// <summary>The shortcut keys CTRL+SHIFT+V.</summary>
		// Token: 0x040009F6 RID: 2550
		CtrlShiftV,
		/// <summary>The shortcut keys CTRL+SHIFT+W.</summary>
		// Token: 0x040009F7 RID: 2551
		CtrlShiftW,
		/// <summary>The shortcut keys CTRL+SHIFT+X.</summary>
		// Token: 0x040009F8 RID: 2552
		CtrlShiftX,
		/// <summary>The shortcut keys CTRL+SHIFT+Y.</summary>
		// Token: 0x040009F9 RID: 2553
		CtrlShiftY,
		/// <summary>The shortcut keys CTRL+SHIFT+Z.</summary>
		// Token: 0x040009FA RID: 2554
		CtrlShiftZ,
		/// <summary>The shortcut keys CTRL+T.</summary>
		// Token: 0x040009FB RID: 2555
		CtrlT = 131156,
		/// <summary>The shortcut keys CTRL+U.</summary>
		// Token: 0x040009FC RID: 2556
		CtrlU,
		/// <summary>The shortcut keys CTRL+V.</summary>
		// Token: 0x040009FD RID: 2557
		CtrlV,
		/// <summary>The shortcut keys CTRL+W.</summary>
		// Token: 0x040009FE RID: 2558
		CtrlW,
		/// <summary>The shortcut keys CTRL+X.</summary>
		// Token: 0x040009FF RID: 2559
		CtrlX,
		/// <summary>The shortcut keys CTRL+Y.</summary>
		// Token: 0x04000A00 RID: 2560
		CtrlY,
		/// <summary>The shortcut keys CTRL+Z.</summary>
		// Token: 0x04000A01 RID: 2561
		CtrlZ,
		/// <summary>The shortcut key DELETE.</summary>
		// Token: 0x04000A02 RID: 2562
		Del = 46,
		/// <summary>The shortcut key F1.</summary>
		// Token: 0x04000A03 RID: 2563
		F1 = 112,
		/// <summary>The shortcut key F10.</summary>
		// Token: 0x04000A04 RID: 2564
		F10 = 121,
		/// <summary>The shortcut key F11.</summary>
		// Token: 0x04000A05 RID: 2565
		F11,
		/// <summary>The shortcut key F12.</summary>
		// Token: 0x04000A06 RID: 2566
		F12,
		/// <summary>The shortcut key F2.</summary>
		// Token: 0x04000A07 RID: 2567
		F2 = 113,
		/// <summary>The shortcut key F3.</summary>
		// Token: 0x04000A08 RID: 2568
		F3,
		/// <summary>The shortcut key F4.</summary>
		// Token: 0x04000A09 RID: 2569
		F4,
		/// <summary>The shortcut key F5.</summary>
		// Token: 0x04000A0A RID: 2570
		F5,
		/// <summary>The shortcut key F6.</summary>
		// Token: 0x04000A0B RID: 2571
		F6,
		/// <summary>The shortcut key F7.</summary>
		// Token: 0x04000A0C RID: 2572
		F7,
		/// <summary>The shortcut key F8.</summary>
		// Token: 0x04000A0D RID: 2573
		F8,
		/// <summary>The shortcut key F9.</summary>
		// Token: 0x04000A0E RID: 2574
		F9,
		/// <summary>The shortcut key INSERT.</summary>
		// Token: 0x04000A0F RID: 2575
		Ins = 45,
		/// <summary>No shortcut key is associated with the menu item.</summary>
		// Token: 0x04000A10 RID: 2576
		None = 0,
		/// <summary>The shortcut keys SHIFT+DELETE.</summary>
		// Token: 0x04000A11 RID: 2577
		ShiftDel = 65582,
		/// <summary>The shortcut keys SHIFT+F1.</summary>
		// Token: 0x04000A12 RID: 2578
		ShiftF1 = 65648,
		/// <summary>The shortcut keys SHIFT+F10.</summary>
		// Token: 0x04000A13 RID: 2579
		ShiftF10 = 65657,
		/// <summary>The shortcut keys SHIFT+F11.</summary>
		// Token: 0x04000A14 RID: 2580
		ShiftF11,
		/// <summary>The shortcut keys SHIFT+F12.</summary>
		// Token: 0x04000A15 RID: 2581
		ShiftF12,
		/// <summary>The shortcut keys SHIFT+F2.</summary>
		// Token: 0x04000A16 RID: 2582
		ShiftF2 = 65649,
		/// <summary>The shortcut keys SHIFT+F3.</summary>
		// Token: 0x04000A17 RID: 2583
		ShiftF3,
		/// <summary>The shortcut keys SHIFT+F4.</summary>
		// Token: 0x04000A18 RID: 2584
		ShiftF4,
		/// <summary>The shortcut keys SHIFT+F5.</summary>
		// Token: 0x04000A19 RID: 2585
		ShiftF5,
		/// <summary>The shortcut keys SHIFT+F6.</summary>
		// Token: 0x04000A1A RID: 2586
		ShiftF6,
		/// <summary>The shortcut keys SHIFT+F7.</summary>
		// Token: 0x04000A1B RID: 2587
		ShiftF7,
		/// <summary>The shortcut keys SHIFT+F8.</summary>
		// Token: 0x04000A1C RID: 2588
		ShiftF8,
		/// <summary>The shortcut keys SHIFT+F9.</summary>
		// Token: 0x04000A1D RID: 2589
		ShiftF9,
		/// <summary>The shortcut keys SHIFT+INSERT.</summary>
		// Token: 0x04000A1E RID: 2590
		ShiftIns = 65581
	}
}
