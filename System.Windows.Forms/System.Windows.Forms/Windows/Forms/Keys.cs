using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies key codes and modifiers.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F7 RID: 247
	[Flags]
	[ComVisible(true)]
	[TypeConverter(typeof(KeysConverter))]
	[Editor("System.Windows.Forms.Design.ShortcutKeysEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public enum Keys
	{
		/// <summary>No key pressed.</summary>
		// Token: 0x0400058A RID: 1418
		None = 0,
		/// <summary>The left mouse button.</summary>
		// Token: 0x0400058B RID: 1419
		LButton = 1,
		/// <summary>The right mouse button.</summary>
		// Token: 0x0400058C RID: 1420
		RButton = 2,
		/// <summary>The CANCEL key.</summary>
		// Token: 0x0400058D RID: 1421
		Cancel = 3,
		/// <summary>The middle mouse button (three-button mouse).</summary>
		// Token: 0x0400058E RID: 1422
		MButton = 4,
		/// <summary>The first x mouse button (five-button mouse).</summary>
		// Token: 0x0400058F RID: 1423
		XButton1 = 5,
		/// <summary>The second x mouse button (five-button mouse).</summary>
		// Token: 0x04000590 RID: 1424
		XButton2 = 6,
		/// <summary>The BACKSPACE key.</summary>
		// Token: 0x04000591 RID: 1425
		Back = 8,
		/// <summary>The TAB key.</summary>
		// Token: 0x04000592 RID: 1426
		Tab = 9,
		/// <summary>The LINEFEED key.</summary>
		// Token: 0x04000593 RID: 1427
		LineFeed = 10,
		/// <summary>The CLEAR key.</summary>
		// Token: 0x04000594 RID: 1428
		Clear = 12,
		/// <summary>The RETURN key.</summary>
		// Token: 0x04000595 RID: 1429
		Return = 13,
		/// <summary>The ENTER key.</summary>
		// Token: 0x04000596 RID: 1430
		Enter = 13,
		/// <summary>The SHIFT key.</summary>
		// Token: 0x04000597 RID: 1431
		ShiftKey = 16,
		/// <summary>The CTRL key.</summary>
		// Token: 0x04000598 RID: 1432
		ControlKey = 17,
		/// <summary>The ALT key.</summary>
		// Token: 0x04000599 RID: 1433
		Menu = 18,
		/// <summary>The PAUSE key.</summary>
		// Token: 0x0400059A RID: 1434
		Pause = 19,
		/// <summary>The CAPS LOCK key.</summary>
		// Token: 0x0400059B RID: 1435
		CapsLock = 20,
		/// <summary>The CAPS LOCK key.</summary>
		// Token: 0x0400059C RID: 1436
		Capital = 20,
		/// <summary>The IME Kana mode key.</summary>
		// Token: 0x0400059D RID: 1437
		KanaMode = 21,
		/// <summary>The IME Hanguel mode key. (maintained for compatibility; use HangulMode) </summary>
		// Token: 0x0400059E RID: 1438
		HanguelMode = 21,
		/// <summary>The IME Hangul mode key.</summary>
		// Token: 0x0400059F RID: 1439
		HangulMode = 21,
		/// <summary>The IME Junja mode key.</summary>
		// Token: 0x040005A0 RID: 1440
		JunjaMode = 23,
		/// <summary>The IME final mode key.</summary>
		// Token: 0x040005A1 RID: 1441
		FinalMode = 24,
		/// <summary>The IME Kanji mode key.</summary>
		// Token: 0x040005A2 RID: 1442
		KanjiMode = 25,
		/// <summary>The IME Hanja mode key.</summary>
		// Token: 0x040005A3 RID: 1443
		HanjaMode = 25,
		/// <summary>The ESC key.</summary>
		// Token: 0x040005A4 RID: 1444
		Escape = 27,
		/// <summary>The IME convert key.</summary>
		// Token: 0x040005A5 RID: 1445
		IMEConvert = 28,
		/// <summary>The IME nonconvert key.</summary>
		// Token: 0x040005A6 RID: 1446
		IMENonconvert = 29,
		/// <summary>The IME accept key. Obsolete, use <see cref="F:System.Windows.Forms.Keys.IMEAccept" /> instead.</summary>
		// Token: 0x040005A7 RID: 1447
		IMEAceept = 30,
		/// <summary>The IME mode change key.</summary>
		// Token: 0x040005A8 RID: 1448
		IMEModeChange = 31,
		/// <summary>The SPACEBAR key.</summary>
		// Token: 0x040005A9 RID: 1449
		Space = 32,
		/// <summary>The PAGE UP key.</summary>
		// Token: 0x040005AA RID: 1450
		PageUp = 33,
		/// <summary>The PAGE UP key.</summary>
		// Token: 0x040005AB RID: 1451
		Prior = 33,
		/// <summary>The PAGE DOWN key.</summary>
		// Token: 0x040005AC RID: 1452
		PageDown = 34,
		/// <summary>The PAGE DOWN key.</summary>
		// Token: 0x040005AD RID: 1453
		Next = 34,
		/// <summary>The END key.</summary>
		// Token: 0x040005AE RID: 1454
		End = 35,
		/// <summary>The HOME key.</summary>
		// Token: 0x040005AF RID: 1455
		Home = 36,
		/// <summary>The LEFT ARROW key.</summary>
		// Token: 0x040005B0 RID: 1456
		Left = 37,
		/// <summary>The UP ARROW key.</summary>
		// Token: 0x040005B1 RID: 1457
		Up = 38,
		/// <summary>The RIGHT ARROW key.</summary>
		// Token: 0x040005B2 RID: 1458
		Right = 39,
		/// <summary>The DOWN ARROW key.</summary>
		// Token: 0x040005B3 RID: 1459
		Down = 40,
		/// <summary>The SELECT key.</summary>
		// Token: 0x040005B4 RID: 1460
		Select = 41,
		/// <summary>The PRINT key.</summary>
		// Token: 0x040005B5 RID: 1461
		Print = 42,
		/// <summary>The EXECUTE key.</summary>
		// Token: 0x040005B6 RID: 1462
		Execute = 43,
		/// <summary>The PRINT SCREEN key.</summary>
		// Token: 0x040005B7 RID: 1463
		PrintScreen = 44,
		/// <summary>The PRINT SCREEN key.</summary>
		// Token: 0x040005B8 RID: 1464
		Snapshot = 44,
		/// <summary>The INS key.</summary>
		// Token: 0x040005B9 RID: 1465
		Insert = 45,
		/// <summary>The DEL key.</summary>
		// Token: 0x040005BA RID: 1466
		Delete = 46,
		/// <summary>The HELP key.</summary>
		// Token: 0x040005BB RID: 1467
		Help = 47,
		/// <summary>The 0 key.</summary>
		// Token: 0x040005BC RID: 1468
		D0 = 48,
		/// <summary>The 1 key.</summary>
		// Token: 0x040005BD RID: 1469
		D1 = 49,
		/// <summary>The 2 key.</summary>
		// Token: 0x040005BE RID: 1470
		D2 = 50,
		/// <summary>The 3 key.</summary>
		// Token: 0x040005BF RID: 1471
		D3 = 51,
		/// <summary>The 4 key.</summary>
		// Token: 0x040005C0 RID: 1472
		D4 = 52,
		/// <summary>The 5 key.</summary>
		// Token: 0x040005C1 RID: 1473
		D5 = 53,
		/// <summary>The 6 key.</summary>
		// Token: 0x040005C2 RID: 1474
		D6 = 54,
		/// <summary>The 7 key.</summary>
		// Token: 0x040005C3 RID: 1475
		D7 = 55,
		/// <summary>The 8 key.</summary>
		// Token: 0x040005C4 RID: 1476
		D8 = 56,
		/// <summary>The 9 key.</summary>
		// Token: 0x040005C5 RID: 1477
		D9 = 57,
		/// <summary>The A key.</summary>
		// Token: 0x040005C6 RID: 1478
		A = 65,
		/// <summary>The B key.</summary>
		// Token: 0x040005C7 RID: 1479
		B = 66,
		/// <summary>The C key.</summary>
		// Token: 0x040005C8 RID: 1480
		C = 67,
		/// <summary>The D key.</summary>
		// Token: 0x040005C9 RID: 1481
		D = 68,
		/// <summary>The E key.</summary>
		// Token: 0x040005CA RID: 1482
		E = 69,
		/// <summary>The F key.</summary>
		// Token: 0x040005CB RID: 1483
		F = 70,
		/// <summary>The G key.</summary>
		// Token: 0x040005CC RID: 1484
		G = 71,
		/// <summary>The H key.</summary>
		// Token: 0x040005CD RID: 1485
		H = 72,
		/// <summary>The I key.</summary>
		// Token: 0x040005CE RID: 1486
		I = 73,
		/// <summary>The J key.</summary>
		// Token: 0x040005CF RID: 1487
		J = 74,
		/// <summary>The K key.</summary>
		// Token: 0x040005D0 RID: 1488
		K = 75,
		/// <summary>The L key.</summary>
		// Token: 0x040005D1 RID: 1489
		L = 76,
		/// <summary>The M key.</summary>
		// Token: 0x040005D2 RID: 1490
		M = 77,
		/// <summary>The N key.</summary>
		// Token: 0x040005D3 RID: 1491
		N = 78,
		/// <summary>The O key.</summary>
		// Token: 0x040005D4 RID: 1492
		O = 79,
		/// <summary>The P key.</summary>
		// Token: 0x040005D5 RID: 1493
		P = 80,
		/// <summary>The Q key.</summary>
		// Token: 0x040005D6 RID: 1494
		Q = 81,
		/// <summary>The R key.</summary>
		// Token: 0x040005D7 RID: 1495
		R = 82,
		/// <summary>The S key.</summary>
		// Token: 0x040005D8 RID: 1496
		S = 83,
		/// <summary>The T key.</summary>
		// Token: 0x040005D9 RID: 1497
		T = 84,
		/// <summary>The U key.</summary>
		// Token: 0x040005DA RID: 1498
		U = 85,
		/// <summary>The V key.</summary>
		// Token: 0x040005DB RID: 1499
		V = 86,
		/// <summary>The W key.</summary>
		// Token: 0x040005DC RID: 1500
		W = 87,
		/// <summary>The X key.</summary>
		// Token: 0x040005DD RID: 1501
		X = 88,
		/// <summary>The Y key.</summary>
		// Token: 0x040005DE RID: 1502
		Y = 89,
		/// <summary>The Z key.</summary>
		// Token: 0x040005DF RID: 1503
		Z = 90,
		/// <summary>The left Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x040005E0 RID: 1504
		LWin = 91,
		/// <summary>The right Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x040005E1 RID: 1505
		RWin = 92,
		/// <summary>The application key (Microsoft Natural Keyboard).</summary>
		// Token: 0x040005E2 RID: 1506
		Apps = 93,
		/// <summary>The 0 key on the numeric keypad.</summary>
		// Token: 0x040005E3 RID: 1507
		NumPad0 = 96,
		/// <summary>The 1 key on the numeric keypad.</summary>
		// Token: 0x040005E4 RID: 1508
		NumPad1 = 97,
		/// <summary>The 2 key on the numeric keypad.</summary>
		// Token: 0x040005E5 RID: 1509
		NumPad2 = 98,
		/// <summary>The 3 key on the numeric keypad.</summary>
		// Token: 0x040005E6 RID: 1510
		NumPad3 = 99,
		/// <summary>The 4 key on the numeric keypad.</summary>
		// Token: 0x040005E7 RID: 1511
		NumPad4 = 100,
		/// <summary>The 5 key on the numeric keypad.</summary>
		// Token: 0x040005E8 RID: 1512
		NumPad5 = 101,
		/// <summary>The 6 key on the numeric keypad.</summary>
		// Token: 0x040005E9 RID: 1513
		NumPad6 = 102,
		/// <summary>The 7 key on the numeric keypad.</summary>
		// Token: 0x040005EA RID: 1514
		NumPad7 = 103,
		/// <summary>The 8 key on the numeric keypad.</summary>
		// Token: 0x040005EB RID: 1515
		NumPad8 = 104,
		/// <summary>The 9 key on the numeric keypad.</summary>
		// Token: 0x040005EC RID: 1516
		NumPad9 = 105,
		/// <summary>The multiply key.</summary>
		// Token: 0x040005ED RID: 1517
		Multiply = 106,
		/// <summary>The add key.</summary>
		// Token: 0x040005EE RID: 1518
		Add = 107,
		/// <summary>The separator key.</summary>
		// Token: 0x040005EF RID: 1519
		Separator = 108,
		/// <summary>The subtract key.</summary>
		// Token: 0x040005F0 RID: 1520
		Subtract = 109,
		/// <summary>The decimal key.</summary>
		// Token: 0x040005F1 RID: 1521
		Decimal = 110,
		/// <summary>The divide key.</summary>
		// Token: 0x040005F2 RID: 1522
		Divide = 111,
		/// <summary>The F1 key.</summary>
		// Token: 0x040005F3 RID: 1523
		F1 = 112,
		/// <summary>The F2 key.</summary>
		// Token: 0x040005F4 RID: 1524
		F2 = 113,
		/// <summary>The F3 key.</summary>
		// Token: 0x040005F5 RID: 1525
		F3 = 114,
		/// <summary>The F4 key.</summary>
		// Token: 0x040005F6 RID: 1526
		F4 = 115,
		/// <summary>The F5 key.</summary>
		// Token: 0x040005F7 RID: 1527
		F5 = 116,
		/// <summary>The F6 key.</summary>
		// Token: 0x040005F8 RID: 1528
		F6 = 117,
		/// <summary>The F7 key.</summary>
		// Token: 0x040005F9 RID: 1529
		F7 = 118,
		/// <summary>The F8 key.</summary>
		// Token: 0x040005FA RID: 1530
		F8 = 119,
		/// <summary>The F9 key.</summary>
		// Token: 0x040005FB RID: 1531
		F9 = 120,
		/// <summary>The F10 key.</summary>
		// Token: 0x040005FC RID: 1532
		F10 = 121,
		/// <summary>The F11 key.</summary>
		// Token: 0x040005FD RID: 1533
		F11 = 122,
		/// <summary>The F12 key.</summary>
		// Token: 0x040005FE RID: 1534
		F12 = 123,
		/// <summary>The F13 key.</summary>
		// Token: 0x040005FF RID: 1535
		F13 = 124,
		/// <summary>The F14 key.</summary>
		// Token: 0x04000600 RID: 1536
		F14 = 125,
		/// <summary>The F15 key.</summary>
		// Token: 0x04000601 RID: 1537
		F15 = 126,
		/// <summary>The F16 key.</summary>
		// Token: 0x04000602 RID: 1538
		F16 = 127,
		/// <summary>The F17 key.</summary>
		// Token: 0x04000603 RID: 1539
		F17 = 128,
		/// <summary>The F18 key.</summary>
		// Token: 0x04000604 RID: 1540
		F18 = 129,
		/// <summary>The F19 key.</summary>
		// Token: 0x04000605 RID: 1541
		F19 = 130,
		/// <summary>The F20 key.</summary>
		// Token: 0x04000606 RID: 1542
		F20 = 131,
		/// <summary>The F21 key.</summary>
		// Token: 0x04000607 RID: 1543
		F21 = 132,
		/// <summary>The F22 key.</summary>
		// Token: 0x04000608 RID: 1544
		F22 = 133,
		/// <summary>The F23 key.</summary>
		// Token: 0x04000609 RID: 1545
		F23 = 134,
		/// <summary>The F24 key.</summary>
		// Token: 0x0400060A RID: 1546
		F24 = 135,
		/// <summary>The NUM LOCK key.</summary>
		// Token: 0x0400060B RID: 1547
		NumLock = 144,
		/// <summary>The SCROLL LOCK key.</summary>
		// Token: 0x0400060C RID: 1548
		Scroll = 145,
		/// <summary>The left SHIFT key.</summary>
		// Token: 0x0400060D RID: 1549
		LShiftKey = 160,
		/// <summary>The right SHIFT key.</summary>
		// Token: 0x0400060E RID: 1550
		RShiftKey = 161,
		/// <summary>The left CTRL key.</summary>
		// Token: 0x0400060F RID: 1551
		LControlKey = 162,
		/// <summary>The right CTRL key.</summary>
		// Token: 0x04000610 RID: 1552
		RControlKey = 163,
		/// <summary>The left ALT key.</summary>
		// Token: 0x04000611 RID: 1553
		LMenu = 164,
		/// <summary>The right ALT key.</summary>
		// Token: 0x04000612 RID: 1554
		RMenu = 165,
		/// <summary>The browser back key (Windows 2000 or later).</summary>
		// Token: 0x04000613 RID: 1555
		BrowserBack = 166,
		/// <summary>The browser forward key (Windows 2000 or later).</summary>
		// Token: 0x04000614 RID: 1556
		BrowserForward = 167,
		/// <summary>The browser refresh key (Windows 2000 or later).</summary>
		// Token: 0x04000615 RID: 1557
		BrowserRefresh = 168,
		/// <summary>The browser stop key (Windows 2000 or later).</summary>
		// Token: 0x04000616 RID: 1558
		BrowserStop = 169,
		/// <summary>The browser search key (Windows 2000 or later).</summary>
		// Token: 0x04000617 RID: 1559
		BrowserSearch = 170,
		/// <summary>The browser favorites key (Windows 2000 or later).</summary>
		// Token: 0x04000618 RID: 1560
		BrowserFavorites = 171,
		/// <summary>The browser home key (Windows 2000 or later).</summary>
		// Token: 0x04000619 RID: 1561
		BrowserHome = 172,
		/// <summary>The volume mute key (Windows 2000 or later).</summary>
		// Token: 0x0400061A RID: 1562
		VolumeMute = 173,
		/// <summary>The volume down key (Windows 2000 or later).</summary>
		// Token: 0x0400061B RID: 1563
		VolumeDown = 174,
		/// <summary>The volume up key (Windows 2000 or later).</summary>
		// Token: 0x0400061C RID: 1564
		VolumeUp = 175,
		/// <summary>The media next track key (Windows 2000 or later).</summary>
		// Token: 0x0400061D RID: 1565
		MediaNextTrack = 176,
		/// <summary>The media previous track key (Windows 2000 or later).</summary>
		// Token: 0x0400061E RID: 1566
		MediaPreviousTrack = 177,
		/// <summary>The media Stop key (Windows 2000 or later).</summary>
		// Token: 0x0400061F RID: 1567
		MediaStop = 178,
		/// <summary>The media play pause key (Windows 2000 or later).</summary>
		// Token: 0x04000620 RID: 1568
		MediaPlayPause = 179,
		/// <summary>The launch mail key (Windows 2000 or later).</summary>
		// Token: 0x04000621 RID: 1569
		LaunchMail = 180,
		/// <summary>The select media key (Windows 2000 or later).</summary>
		// Token: 0x04000622 RID: 1570
		SelectMedia = 181,
		/// <summary>The start application one key (Windows 2000 or later).</summary>
		// Token: 0x04000623 RID: 1571
		LaunchApplication1 = 182,
		/// <summary>The start application two key (Windows 2000 or later).</summary>
		// Token: 0x04000624 RID: 1572
		LaunchApplication2 = 183,
		/// <summary>The OEM Semicolon key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000625 RID: 1573
		OemSemicolon = 186,
		/// <summary>The OEM plus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000626 RID: 1574
		Oemplus = 187,
		/// <summary>The OEM comma key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000627 RID: 1575
		Oemcomma = 188,
		/// <summary>The OEM minus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000628 RID: 1576
		OemMinus = 189,
		/// <summary>The OEM period key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000629 RID: 1577
		OemPeriod = 190,
		/// <summary>The OEM question mark key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400062A RID: 1578
		OemQuestion = 191,
		/// <summary>The OEM tilde key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400062B RID: 1579
		Oemtilde = 192,
		/// <summary>The OEM open bracket key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400062C RID: 1580
		OemOpenBrackets = 219,
		/// <summary>The OEM pipe key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400062D RID: 1581
		OemPipe = 220,
		/// <summary>The OEM close bracket key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400062E RID: 1582
		OemCloseBrackets = 221,
		/// <summary>The OEM singled/double quote key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400062F RID: 1583
		OemQuotes = 222,
		/// <summary>The OEM 8 key.</summary>
		// Token: 0x04000630 RID: 1584
		Oem8 = 223,
		/// <summary>The OEM angle bracket or backslash key on the RT 102 key keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000631 RID: 1585
		OemBackslash = 226,
		/// <summary>The PROCESS KEY key.</summary>
		// Token: 0x04000632 RID: 1586
		ProcessKey = 229,
		/// <summary>The ATTN key.</summary>
		// Token: 0x04000633 RID: 1587
		Attn = 246,
		/// <summary>The CRSEL key.</summary>
		// Token: 0x04000634 RID: 1588
		Crsel = 247,
		/// <summary>The EXSEL key.</summary>
		// Token: 0x04000635 RID: 1589
		Exsel = 248,
		/// <summary>The ERASE EOF key.</summary>
		// Token: 0x04000636 RID: 1590
		EraseEof = 249,
		/// <summary>The PLAY key.</summary>
		// Token: 0x04000637 RID: 1591
		Play = 250,
		/// <summary>The ZOOM key.</summary>
		// Token: 0x04000638 RID: 1592
		Zoom = 251,
		/// <summary>A constant reserved for future use.</summary>
		// Token: 0x04000639 RID: 1593
		NoName = 252,
		/// <summary>The PA1 key.</summary>
		// Token: 0x0400063A RID: 1594
		Pa1 = 253,
		/// <summary>The CLEAR key.</summary>
		// Token: 0x0400063B RID: 1595
		OemClear = 254,
		/// <summary>The bitmask to extract a key code from a key value.</summary>
		// Token: 0x0400063C RID: 1596
		KeyCode = 65535,
		/// <summary>The SHIFT modifier key.</summary>
		// Token: 0x0400063D RID: 1597
		Shift = 65536,
		/// <summary>The CTRL modifier key.</summary>
		// Token: 0x0400063E RID: 1598
		Control = 131072,
		/// <summary>The ALT modifier key.</summary>
		// Token: 0x0400063F RID: 1599
		Alt = 262144,
		/// <summary>The bitmask to extract modifiers from a key value.</summary>
		// Token: 0x04000640 RID: 1600
		Modifiers = -65536,
		/// <summary>The IME accept key, replaces <see cref="F:System.Windows.Forms.Keys.IMEAceept" />.</summary>
		// Token: 0x04000641 RID: 1601
		IMEAccept = 30,
		/// <summary>The OEM 1 key.</summary>
		// Token: 0x04000642 RID: 1602
		Oem1 = 186,
		/// <summary>The OEM 102 key.</summary>
		// Token: 0x04000643 RID: 1603
		Oem102 = 226,
		/// <summary>The OEM 2 key.</summary>
		// Token: 0x04000644 RID: 1604
		Oem2 = 191,
		/// <summary>The OEM 3 key.</summary>
		// Token: 0x04000645 RID: 1605
		Oem3 = 192,
		/// <summary>The OEM 4 key.</summary>
		// Token: 0x04000646 RID: 1606
		Oem4 = 219,
		/// <summary>The OEM 5 key.</summary>
		// Token: 0x04000647 RID: 1607
		Oem5 = 220,
		/// <summary>The OEM 6 key.</summary>
		// Token: 0x04000648 RID: 1608
		Oem6 = 221,
		/// <summary>The OEM 7 key.</summary>
		// Token: 0x04000649 RID: 1609
		Oem7 = 222,
		/// <summary>Used to pass Unicode characters as if they were keystrokes. The Packet key value is the low word of a 32-bit virtual-key value used for non-keyboard input methods.</summary>
		// Token: 0x0400064A RID: 1610
		Packet = 231,
		/// <summary>The computer sleep key.</summary>
		// Token: 0x0400064B RID: 1611
		Sleep = 95
	}
}
