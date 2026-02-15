using System;

namespace System
{
	/// <summary>Specifies the standard keys on a console.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000174 RID: 372
	public enum ConsoleKey
	{
		/// <summary>The BACKSPACE key.</summary>
		// Token: 0x040004FF RID: 1279
		Backspace = 8,
		/// <summary>The TAB key.</summary>
		// Token: 0x04000500 RID: 1280
		Tab,
		/// <summary>The CLEAR key.</summary>
		// Token: 0x04000501 RID: 1281
		Clear = 12,
		/// <summary>The ENTER key.</summary>
		// Token: 0x04000502 RID: 1282
		Enter,
		/// <summary>The PAUSE key.</summary>
		// Token: 0x04000503 RID: 1283
		Pause = 19,
		/// <summary>The ESC (ESCAPE) key.</summary>
		// Token: 0x04000504 RID: 1284
		Escape = 27,
		/// <summary>The SPACEBAR key.</summary>
		// Token: 0x04000505 RID: 1285
		Spacebar = 32,
		/// <summary>The PAGE UP key.</summary>
		// Token: 0x04000506 RID: 1286
		PageUp,
		/// <summary>The PAGE DOWN key.</summary>
		// Token: 0x04000507 RID: 1287
		PageDown,
		/// <summary>The END key.</summary>
		// Token: 0x04000508 RID: 1288
		End,
		/// <summary>The HOME key.</summary>
		// Token: 0x04000509 RID: 1289
		Home,
		/// <summary>The LEFT ARROW key.</summary>
		// Token: 0x0400050A RID: 1290
		LeftArrow,
		/// <summary>The UP ARROW key.</summary>
		// Token: 0x0400050B RID: 1291
		UpArrow,
		/// <summary>The RIGHT ARROW key.</summary>
		// Token: 0x0400050C RID: 1292
		RightArrow,
		/// <summary>The DOWN ARROW key.</summary>
		// Token: 0x0400050D RID: 1293
		DownArrow,
		/// <summary>The SELECT key.</summary>
		// Token: 0x0400050E RID: 1294
		Select,
		/// <summary>The PRINT key.</summary>
		// Token: 0x0400050F RID: 1295
		Print,
		/// <summary>The EXECUTE key.</summary>
		// Token: 0x04000510 RID: 1296
		Execute,
		/// <summary>The PRINT SCREEN key.</summary>
		// Token: 0x04000511 RID: 1297
		PrintScreen,
		/// <summary>The INS (INSERT) key.</summary>
		// Token: 0x04000512 RID: 1298
		Insert,
		/// <summary>The DEL (DELETE) key.</summary>
		// Token: 0x04000513 RID: 1299
		Delete,
		/// <summary>The HELP key.</summary>
		// Token: 0x04000514 RID: 1300
		Help,
		/// <summary>The 0 key.</summary>
		// Token: 0x04000515 RID: 1301
		D0,
		/// <summary>The 1 key.</summary>
		// Token: 0x04000516 RID: 1302
		D1,
		/// <summary>The 2 key.</summary>
		// Token: 0x04000517 RID: 1303
		D2,
		/// <summary>The 3 key.</summary>
		// Token: 0x04000518 RID: 1304
		D3,
		/// <summary>The 4 key.</summary>
		// Token: 0x04000519 RID: 1305
		D4,
		/// <summary>The 5 key.</summary>
		// Token: 0x0400051A RID: 1306
		D5,
		/// <summary>The 6 key.</summary>
		// Token: 0x0400051B RID: 1307
		D6,
		/// <summary>The 7 key.</summary>
		// Token: 0x0400051C RID: 1308
		D7,
		/// <summary>The 8 key.</summary>
		// Token: 0x0400051D RID: 1309
		D8,
		/// <summary>The 9 key.</summary>
		// Token: 0x0400051E RID: 1310
		D9,
		/// <summary>The A key.</summary>
		// Token: 0x0400051F RID: 1311
		A = 65,
		/// <summary>The B key.</summary>
		// Token: 0x04000520 RID: 1312
		B,
		/// <summary>The C key.</summary>
		// Token: 0x04000521 RID: 1313
		C,
		/// <summary>The D key.</summary>
		// Token: 0x04000522 RID: 1314
		D,
		/// <summary>The E key.</summary>
		// Token: 0x04000523 RID: 1315
		E,
		/// <summary>The F key.</summary>
		// Token: 0x04000524 RID: 1316
		F,
		/// <summary>The G key.</summary>
		// Token: 0x04000525 RID: 1317
		G,
		/// <summary>The H key.</summary>
		// Token: 0x04000526 RID: 1318
		H,
		/// <summary>The I key.</summary>
		// Token: 0x04000527 RID: 1319
		I,
		/// <summary>The J key.</summary>
		// Token: 0x04000528 RID: 1320
		J,
		/// <summary>The K key.</summary>
		// Token: 0x04000529 RID: 1321
		K,
		/// <summary>The L key.</summary>
		// Token: 0x0400052A RID: 1322
		L,
		/// <summary>The M key.</summary>
		// Token: 0x0400052B RID: 1323
		M,
		/// <summary>The N key.</summary>
		// Token: 0x0400052C RID: 1324
		N,
		/// <summary>The O key.</summary>
		// Token: 0x0400052D RID: 1325
		O,
		/// <summary>The P key.</summary>
		// Token: 0x0400052E RID: 1326
		P,
		/// <summary>The Q key.</summary>
		// Token: 0x0400052F RID: 1327
		Q,
		/// <summary>The R key.</summary>
		// Token: 0x04000530 RID: 1328
		R,
		/// <summary>The S key.</summary>
		// Token: 0x04000531 RID: 1329
		S,
		/// <summary>The T key.</summary>
		// Token: 0x04000532 RID: 1330
		T,
		/// <summary>The U key.</summary>
		// Token: 0x04000533 RID: 1331
		U,
		/// <summary>The V key.</summary>
		// Token: 0x04000534 RID: 1332
		V,
		/// <summary>The W key.</summary>
		// Token: 0x04000535 RID: 1333
		W,
		/// <summary>The X key.</summary>
		// Token: 0x04000536 RID: 1334
		X,
		/// <summary>The Y key.</summary>
		// Token: 0x04000537 RID: 1335
		Y,
		/// <summary>The Z key.</summary>
		// Token: 0x04000538 RID: 1336
		Z,
		/// <summary>The left Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x04000539 RID: 1337
		LeftWindows,
		/// <summary>The right Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x0400053A RID: 1338
		RightWindows,
		/// <summary>The Application key (Microsoft Natural Keyboard).</summary>
		// Token: 0x0400053B RID: 1339
		Applications,
		/// <summary>The Computer Sleep key.</summary>
		// Token: 0x0400053C RID: 1340
		Sleep = 95,
		/// <summary>The 0 key on the numeric keypad.</summary>
		// Token: 0x0400053D RID: 1341
		NumPad0,
		/// <summary>The 1 key on the numeric keypad.</summary>
		// Token: 0x0400053E RID: 1342
		NumPad1,
		/// <summary>The 2 key on the numeric keypad.</summary>
		// Token: 0x0400053F RID: 1343
		NumPad2,
		/// <summary>The 3 key on the numeric keypad.</summary>
		// Token: 0x04000540 RID: 1344
		NumPad3,
		/// <summary>The 4 key on the numeric keypad.</summary>
		// Token: 0x04000541 RID: 1345
		NumPad4,
		/// <summary>The 5 key on the numeric keypad.</summary>
		// Token: 0x04000542 RID: 1346
		NumPad5,
		/// <summary>The 6 key on the numeric keypad.</summary>
		// Token: 0x04000543 RID: 1347
		NumPad6,
		/// <summary>The 7 key on the numeric keypad.</summary>
		// Token: 0x04000544 RID: 1348
		NumPad7,
		/// <summary>The 8 key on the numeric keypad.</summary>
		// Token: 0x04000545 RID: 1349
		NumPad8,
		/// <summary>The 9 key on the numeric keypad.</summary>
		// Token: 0x04000546 RID: 1350
		NumPad9,
		/// <summary>The Multiply key (the multiplication key on the numeric keypad).</summary>
		// Token: 0x04000547 RID: 1351
		Multiply,
		/// <summary>The Add key (the addition key on the numeric keypad).</summary>
		// Token: 0x04000548 RID: 1352
		Add,
		/// <summary>The Separator key.</summary>
		// Token: 0x04000549 RID: 1353
		Separator,
		/// <summary>The Subtract key (the subtraction key on the numeric keypad).</summary>
		// Token: 0x0400054A RID: 1354
		Subtract,
		/// <summary>The Decimal key (the decimal key on the numeric keypad). </summary>
		// Token: 0x0400054B RID: 1355
		Decimal,
		/// <summary>The Divide key (the division key on the numeric keypad). </summary>
		// Token: 0x0400054C RID: 1356
		Divide,
		/// <summary>The F1 key.</summary>
		// Token: 0x0400054D RID: 1357
		F1,
		/// <summary>The F2 key.</summary>
		// Token: 0x0400054E RID: 1358
		F2,
		/// <summary>The F3 key.</summary>
		// Token: 0x0400054F RID: 1359
		F3,
		/// <summary>The F4 key.</summary>
		// Token: 0x04000550 RID: 1360
		F4,
		/// <summary>The F5 key.</summary>
		// Token: 0x04000551 RID: 1361
		F5,
		/// <summary>The F6 key.</summary>
		// Token: 0x04000552 RID: 1362
		F6,
		/// <summary>The F7 key.</summary>
		// Token: 0x04000553 RID: 1363
		F7,
		/// <summary>The F8 key.</summary>
		// Token: 0x04000554 RID: 1364
		F8,
		/// <summary>The F9 key.</summary>
		// Token: 0x04000555 RID: 1365
		F9,
		/// <summary>The F10 key.</summary>
		// Token: 0x04000556 RID: 1366
		F10,
		/// <summary>The F11 key.</summary>
		// Token: 0x04000557 RID: 1367
		F11,
		/// <summary>The F12 key.</summary>
		// Token: 0x04000558 RID: 1368
		F12,
		/// <summary>The F13 key.</summary>
		// Token: 0x04000559 RID: 1369
		F13,
		/// <summary>The F14 key.</summary>
		// Token: 0x0400055A RID: 1370
		F14,
		/// <summary>The F15 key.</summary>
		// Token: 0x0400055B RID: 1371
		F15,
		/// <summary>The F16 key.</summary>
		// Token: 0x0400055C RID: 1372
		F16,
		/// <summary>The F17 key.</summary>
		// Token: 0x0400055D RID: 1373
		F17,
		/// <summary>The F18 key.</summary>
		// Token: 0x0400055E RID: 1374
		F18,
		/// <summary>The F19 key.</summary>
		// Token: 0x0400055F RID: 1375
		F19,
		/// <summary>The F20 key.</summary>
		// Token: 0x04000560 RID: 1376
		F20,
		/// <summary>The F21 key.</summary>
		// Token: 0x04000561 RID: 1377
		F21,
		/// <summary>The F22 key.</summary>
		// Token: 0x04000562 RID: 1378
		F22,
		/// <summary>The F23 key.</summary>
		// Token: 0x04000563 RID: 1379
		F23,
		/// <summary>The F24 key.</summary>
		// Token: 0x04000564 RID: 1380
		F24,
		/// <summary>The Browser Back key (Windows 2000 or later).</summary>
		// Token: 0x04000565 RID: 1381
		BrowserBack = 166,
		/// <summary>The Browser Forward key (Windows 2000 or later).</summary>
		// Token: 0x04000566 RID: 1382
		BrowserForward,
		/// <summary>The Browser Refresh key (Windows 2000 or later).</summary>
		// Token: 0x04000567 RID: 1383
		BrowserRefresh,
		/// <summary>The Browser Stop key (Windows 2000 or later).</summary>
		// Token: 0x04000568 RID: 1384
		BrowserStop,
		/// <summary>The Browser Search key (Windows 2000 or later).</summary>
		// Token: 0x04000569 RID: 1385
		BrowserSearch,
		/// <summary>The Browser Favorites key (Windows 2000 or later).</summary>
		// Token: 0x0400056A RID: 1386
		BrowserFavorites,
		/// <summary>The Browser Home key (Windows 2000 or later).</summary>
		// Token: 0x0400056B RID: 1387
		BrowserHome,
		/// <summary>The Volume Mute key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x0400056C RID: 1388
		VolumeMute,
		/// <summary>The Volume Down key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x0400056D RID: 1389
		VolumeDown,
		/// <summary>The Volume Up key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x0400056E RID: 1390
		VolumeUp,
		/// <summary>The Media Next Track key (Windows 2000 or later).</summary>
		// Token: 0x0400056F RID: 1391
		MediaNext,
		/// <summary>The Media Previous Track key (Windows 2000 or later).</summary>
		// Token: 0x04000570 RID: 1392
		MediaPrevious,
		/// <summary>The Media Stop key (Windows 2000 or later).</summary>
		// Token: 0x04000571 RID: 1393
		MediaStop,
		/// <summary>The Media Play/Pause key (Windows 2000 or later).</summary>
		// Token: 0x04000572 RID: 1394
		MediaPlay,
		/// <summary>The Start Mail key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000573 RID: 1395
		LaunchMail,
		/// <summary>The Select Media key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000574 RID: 1396
		LaunchMediaSelect,
		/// <summary>The Start Application 1 key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000575 RID: 1397
		LaunchApp1,
		/// <summary>The Start Application 2 key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000576 RID: 1398
		LaunchApp2,
		/// <summary>The OEM 1 key (OEM specific).</summary>
		// Token: 0x04000577 RID: 1399
		Oem1 = 186,
		/// <summary>The OEM Plus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000578 RID: 1400
		OemPlus,
		/// <summary>The OEM Comma key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000579 RID: 1401
		OemComma,
		/// <summary>The OEM Minus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400057A RID: 1402
		OemMinus,
		/// <summary>The OEM Period key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400057B RID: 1403
		OemPeriod,
		/// <summary>The OEM 2 key (OEM specific).</summary>
		// Token: 0x0400057C RID: 1404
		Oem2,
		/// <summary>The OEM 3 key (OEM specific).</summary>
		// Token: 0x0400057D RID: 1405
		Oem3,
		/// <summary>The OEM 4 key (OEM specific).</summary>
		// Token: 0x0400057E RID: 1406
		Oem4 = 219,
		/// <summary>The OEM 5 (OEM specific).</summary>
		// Token: 0x0400057F RID: 1407
		Oem5,
		/// <summary>The OEM 6 key (OEM specific).</summary>
		// Token: 0x04000580 RID: 1408
		Oem6,
		/// <summary>The OEM 7 key (OEM specific).</summary>
		// Token: 0x04000581 RID: 1409
		Oem7,
		/// <summary>The OEM 8 key (OEM specific).</summary>
		// Token: 0x04000582 RID: 1410
		Oem8,
		/// <summary>The OEM 102 key (OEM specific).</summary>
		// Token: 0x04000583 RID: 1411
		Oem102 = 226,
		/// <summary>The IME PROCESS key.</summary>
		// Token: 0x04000584 RID: 1412
		Process = 229,
		/// <summary>The PACKET key (used to pass Unicode characters with keystrokes).</summary>
		// Token: 0x04000585 RID: 1413
		Packet = 231,
		/// <summary>The ATTN key.</summary>
		// Token: 0x04000586 RID: 1414
		Attention = 246,
		/// <summary>The CRSEL (CURSOR SELECT) key.</summary>
		// Token: 0x04000587 RID: 1415
		CrSel,
		/// <summary>The EXSEL (EXTEND SELECTION) key.</summary>
		// Token: 0x04000588 RID: 1416
		ExSel,
		/// <summary>The ERASE EOF key.</summary>
		// Token: 0x04000589 RID: 1417
		EraseEndOfFile,
		/// <summary>The PLAY key.</summary>
		// Token: 0x0400058A RID: 1418
		Play,
		/// <summary>The ZOOM key.</summary>
		// Token: 0x0400058B RID: 1419
		Zoom,
		/// <summary>A constant reserved for future use.</summary>
		// Token: 0x0400058C RID: 1420
		NoName,
		/// <summary>The PA1 key.</summary>
		// Token: 0x0400058D RID: 1421
		Pa1,
		/// <summary>The CLEAR key (OEM specific).</summary>
		// Token: 0x0400058E RID: 1422
		OemClear
	}
}
