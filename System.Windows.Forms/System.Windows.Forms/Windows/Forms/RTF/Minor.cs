using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000387 RID: 903
	internal enum Minor
	{
		// Token: 0x04001888 RID: 6280
		Undefined,
		// Token: 0x04001889 RID: 6281
		Skip,
		// Token: 0x0400188A RID: 6282
		AnsiCharSet,
		// Token: 0x0400188B RID: 6283
		MacCharSet,
		// Token: 0x0400188C RID: 6284
		PcCharSet,
		// Token: 0x0400188D RID: 6285
		PcaCharSet,
		// Token: 0x0400188E RID: 6286
		FontTbl,
		// Token: 0x0400188F RID: 6287
		FontAltName,
		// Token: 0x04001890 RID: 6288
		EmbeddedFont,
		// Token: 0x04001891 RID: 6289
		FontFile,
		// Token: 0x04001892 RID: 6290
		FileTbl,
		// Token: 0x04001893 RID: 6291
		FileInfo,
		// Token: 0x04001894 RID: 6292
		ColorTbl,
		// Token: 0x04001895 RID: 6293
		StyleSheet,
		// Token: 0x04001896 RID: 6294
		KeyCode,
		// Token: 0x04001897 RID: 6295
		RevisionTbl,
		// Token: 0x04001898 RID: 6296
		Info,
		// Token: 0x04001899 RID: 6297
		ITitle,
		// Token: 0x0400189A RID: 6298
		ISubject,
		// Token: 0x0400189B RID: 6299
		IAuthor,
		// Token: 0x0400189C RID: 6300
		IOperator,
		// Token: 0x0400189D RID: 6301
		IKeywords,
		// Token: 0x0400189E RID: 6302
		IComment,
		// Token: 0x0400189F RID: 6303
		IVersion,
		// Token: 0x040018A0 RID: 6304
		IDoccomm,
		// Token: 0x040018A1 RID: 6305
		IVerscomm,
		// Token: 0x040018A2 RID: 6306
		NextFile,
		// Token: 0x040018A3 RID: 6307
		Template,
		// Token: 0x040018A4 RID: 6308
		FNSep,
		// Token: 0x040018A5 RID: 6309
		FNContSep,
		// Token: 0x040018A6 RID: 6310
		FNContNotice,
		// Token: 0x040018A7 RID: 6311
		ENSep,
		// Token: 0x040018A8 RID: 6312
		ENContSep,
		// Token: 0x040018A9 RID: 6313
		ENContNotice,
		// Token: 0x040018AA RID: 6314
		PageNumLevel,
		// Token: 0x040018AB RID: 6315
		ParNumLevelStyle,
		// Token: 0x040018AC RID: 6316
		Header,
		// Token: 0x040018AD RID: 6317
		Footer,
		// Token: 0x040018AE RID: 6318
		HeaderLeft,
		// Token: 0x040018AF RID: 6319
		HeaderRight,
		// Token: 0x040018B0 RID: 6320
		HeaderFirst,
		// Token: 0x040018B1 RID: 6321
		FooterLeft,
		// Token: 0x040018B2 RID: 6322
		FooterRight,
		// Token: 0x040018B3 RID: 6323
		FooterFirst,
		// Token: 0x040018B4 RID: 6324
		ParNumText,
		// Token: 0x040018B5 RID: 6325
		ParNumbering,
		// Token: 0x040018B6 RID: 6326
		ParNumTextAfter,
		// Token: 0x040018B7 RID: 6327
		ParNumTextBefore,
		// Token: 0x040018B8 RID: 6328
		BookmarkStart,
		// Token: 0x040018B9 RID: 6329
		BookmarkEnd,
		// Token: 0x040018BA RID: 6330
		Pict,
		// Token: 0x040018BB RID: 6331
		Object,
		// Token: 0x040018BC RID: 6332
		ObjClass,
		// Token: 0x040018BD RID: 6333
		ObjName,
		// Token: 0x040018BE RID: 6334
		ObjTime,
		// Token: 0x040018BF RID: 6335
		ObjData,
		// Token: 0x040018C0 RID: 6336
		ObjAlias,
		// Token: 0x040018C1 RID: 6337
		ObjSection,
		// Token: 0x040018C2 RID: 6338
		ObjResult,
		// Token: 0x040018C3 RID: 6339
		ObjItem,
		// Token: 0x040018C4 RID: 6340
		ObjTopic,
		// Token: 0x040018C5 RID: 6341
		DrawObject,
		// Token: 0x040018C6 RID: 6342
		Footnote,
		// Token: 0x040018C7 RID: 6343
		AnnotRefStart,
		// Token: 0x040018C8 RID: 6344
		AnnotRefEnd,
		// Token: 0x040018C9 RID: 6345
		AnnotID,
		// Token: 0x040018CA RID: 6346
		AnnotAuthor,
		// Token: 0x040018CB RID: 6347
		Annotation,
		// Token: 0x040018CC RID: 6348
		AnnotRef,
		// Token: 0x040018CD RID: 6349
		AnnotTime,
		// Token: 0x040018CE RID: 6350
		AnnotIcon,
		// Token: 0x040018CF RID: 6351
		Field,
		// Token: 0x040018D0 RID: 6352
		FieldInst,
		// Token: 0x040018D1 RID: 6353
		FieldResult,
		// Token: 0x040018D2 RID: 6354
		DataField,
		// Token: 0x040018D3 RID: 6355
		Index,
		// Token: 0x040018D4 RID: 6356
		IndexText,
		// Token: 0x040018D5 RID: 6357
		IndexRange,
		// Token: 0x040018D6 RID: 6358
		TOC,
		// Token: 0x040018D7 RID: 6359
		NeXTGraphic,
		// Token: 0x040018D8 RID: 6360
		MaxDestination,
		// Token: 0x040018D9 RID: 6361
		FFNil,
		// Token: 0x040018DA RID: 6362
		FFRoman,
		// Token: 0x040018DB RID: 6363
		FFSwiss,
		// Token: 0x040018DC RID: 6364
		FFModern,
		// Token: 0x040018DD RID: 6365
		FFScript,
		// Token: 0x040018DE RID: 6366
		FFDecor,
		// Token: 0x040018DF RID: 6367
		FFTech,
		// Token: 0x040018E0 RID: 6368
		FFBidirectional,
		// Token: 0x040018E1 RID: 6369
		Red,
		// Token: 0x040018E2 RID: 6370
		Green,
		// Token: 0x040018E3 RID: 6371
		Blue,
		// Token: 0x040018E4 RID: 6372
		IIntVersion,
		// Token: 0x040018E5 RID: 6373
		ICreateTime,
		// Token: 0x040018E6 RID: 6374
		IRevisionTime,
		// Token: 0x040018E7 RID: 6375
		IPrintTime,
		// Token: 0x040018E8 RID: 6376
		IBackupTime,
		// Token: 0x040018E9 RID: 6377
		IEditTime,
		// Token: 0x040018EA RID: 6378
		IYear,
		// Token: 0x040018EB RID: 6379
		IMonth,
		// Token: 0x040018EC RID: 6380
		IDay,
		// Token: 0x040018ED RID: 6381
		IHour,
		// Token: 0x040018EE RID: 6382
		IMinute,
		// Token: 0x040018EF RID: 6383
		ISecond,
		// Token: 0x040018F0 RID: 6384
		INPages,
		// Token: 0x040018F1 RID: 6385
		INWords,
		// Token: 0x040018F2 RID: 6386
		INChars,
		// Token: 0x040018F3 RID: 6387
		IIntID,
		// Token: 0x040018F4 RID: 6388
		CurHeadDate,
		// Token: 0x040018F5 RID: 6389
		CurHeadDateLong,
		// Token: 0x040018F6 RID: 6390
		CurHeadDateAbbrev,
		// Token: 0x040018F7 RID: 6391
		CurHeadTime,
		// Token: 0x040018F8 RID: 6392
		CurHeadPage,
		// Token: 0x040018F9 RID: 6393
		SectNum,
		// Token: 0x040018FA RID: 6394
		CurFNote,
		// Token: 0x040018FB RID: 6395
		CurAnnotRef,
		// Token: 0x040018FC RID: 6396
		FNoteSep,
		// Token: 0x040018FD RID: 6397
		FNoteCont,
		// Token: 0x040018FE RID: 6398
		Cell,
		// Token: 0x040018FF RID: 6399
		Row,
		// Token: 0x04001900 RID: 6400
		Par,
		// Token: 0x04001901 RID: 6401
		Sect,
		// Token: 0x04001902 RID: 6402
		Page,
		// Token: 0x04001903 RID: 6403
		Column,
		// Token: 0x04001904 RID: 6404
		Line,
		// Token: 0x04001905 RID: 6405
		SoftPage,
		// Token: 0x04001906 RID: 6406
		SoftColumn,
		// Token: 0x04001907 RID: 6407
		SoftLine,
		// Token: 0x04001908 RID: 6408
		SoftLineHt,
		// Token: 0x04001909 RID: 6409
		Tab,
		// Token: 0x0400190A RID: 6410
		EmDash,
		// Token: 0x0400190B RID: 6411
		EnDash,
		// Token: 0x0400190C RID: 6412
		EmSpace,
		// Token: 0x0400190D RID: 6413
		EnSpace,
		// Token: 0x0400190E RID: 6414
		Bullet,
		// Token: 0x0400190F RID: 6415
		LQuote,
		// Token: 0x04001910 RID: 6416
		RQuote,
		// Token: 0x04001911 RID: 6417
		LDblQuote,
		// Token: 0x04001912 RID: 6418
		RDblQuote,
		// Token: 0x04001913 RID: 6419
		Formula,
		// Token: 0x04001914 RID: 6420
		NoBrkSpace,
		// Token: 0x04001915 RID: 6421
		NoReqHyphen,
		// Token: 0x04001916 RID: 6422
		NoBrkHyphen,
		// Token: 0x04001917 RID: 6423
		OptDest,
		// Token: 0x04001918 RID: 6424
		LTRMark,
		// Token: 0x04001919 RID: 6425
		RTLMark,
		// Token: 0x0400191A RID: 6426
		NoWidthJoiner,
		// Token: 0x0400191B RID: 6427
		NoWidthNonJoiner,
		// Token: 0x0400191C RID: 6428
		CurHeadPict,
		// Token: 0x0400191D RID: 6429
		Additive,
		// Token: 0x0400191E RID: 6430
		BasedOn,
		// Token: 0x0400191F RID: 6431
		Next,
		// Token: 0x04001920 RID: 6432
		DefTab,
		// Token: 0x04001921 RID: 6433
		HyphHotZone,
		// Token: 0x04001922 RID: 6434
		HyphConsecLines,
		// Token: 0x04001923 RID: 6435
		HyphCaps,
		// Token: 0x04001924 RID: 6436
		HyphAuto,
		// Token: 0x04001925 RID: 6437
		LineStart,
		// Token: 0x04001926 RID: 6438
		FracWidth,
		// Token: 0x04001927 RID: 6439
		MakeBackup,
		// Token: 0x04001928 RID: 6440
		RTFDefault,
		// Token: 0x04001929 RID: 6441
		PSOverlay,
		// Token: 0x0400192A RID: 6442
		DocTemplate,
		// Token: 0x0400192B RID: 6443
		DefLanguage,
		// Token: 0x0400192C RID: 6444
		FENoteType,
		// Token: 0x0400192D RID: 6445
		FNoteEndSect,
		// Token: 0x0400192E RID: 6446
		FNoteEndDoc,
		// Token: 0x0400192F RID: 6447
		FNoteText,
		// Token: 0x04001930 RID: 6448
		FNoteBottom,
		// Token: 0x04001931 RID: 6449
		ENoteEndSect,
		// Token: 0x04001932 RID: 6450
		ENoteEndDoc,
		// Token: 0x04001933 RID: 6451
		ENoteText,
		// Token: 0x04001934 RID: 6452
		ENoteBottom,
		// Token: 0x04001935 RID: 6453
		FNoteStart,
		// Token: 0x04001936 RID: 6454
		ENoteStart,
		// Token: 0x04001937 RID: 6455
		FNoteRestartPage,
		// Token: 0x04001938 RID: 6456
		FNoteRestart,
		// Token: 0x04001939 RID: 6457
		FNoteRestartCont,
		// Token: 0x0400193A RID: 6458
		ENoteRestart,
		// Token: 0x0400193B RID: 6459
		ENoteRestartCont,
		// Token: 0x0400193C RID: 6460
		FNoteNumArabic,
		// Token: 0x0400193D RID: 6461
		FNoteNumLLetter,
		// Token: 0x0400193E RID: 6462
		FNoteNumULetter,
		// Token: 0x0400193F RID: 6463
		FNoteNumLRoman,
		// Token: 0x04001940 RID: 6464
		FNoteNumURoman,
		// Token: 0x04001941 RID: 6465
		FNoteNumChicago,
		// Token: 0x04001942 RID: 6466
		ENoteNumArabic,
		// Token: 0x04001943 RID: 6467
		ENoteNumLLetter,
		// Token: 0x04001944 RID: 6468
		ENoteNumULetter,
		// Token: 0x04001945 RID: 6469
		ENoteNumLRoman,
		// Token: 0x04001946 RID: 6470
		ENoteNumURoman,
		// Token: 0x04001947 RID: 6471
		ENoteNumChicago,
		// Token: 0x04001948 RID: 6472
		PaperWidth,
		// Token: 0x04001949 RID: 6473
		PaperHeight,
		// Token: 0x0400194A RID: 6474
		PaperSize,
		// Token: 0x0400194B RID: 6475
		LeftMargin,
		// Token: 0x0400194C RID: 6476
		RightMargin,
		// Token: 0x0400194D RID: 6477
		TopMargin,
		// Token: 0x0400194E RID: 6478
		BottomMargin,
		// Token: 0x0400194F RID: 6479
		FacingPage,
		// Token: 0x04001950 RID: 6480
		GutterWid,
		// Token: 0x04001951 RID: 6481
		MirrorMargin,
		// Token: 0x04001952 RID: 6482
		Landscape,
		// Token: 0x04001953 RID: 6483
		PageStart,
		// Token: 0x04001954 RID: 6484
		WidowCtrl,
		// Token: 0x04001955 RID: 6485
		LinkStyles,
		// Token: 0x04001956 RID: 6486
		NoAutoTabIndent,
		// Token: 0x04001957 RID: 6487
		WrapSpaces,
		// Token: 0x04001958 RID: 6488
		PrintColorsBlack,
		// Token: 0x04001959 RID: 6489
		NoExtraSpaceRL,
		// Token: 0x0400195A RID: 6490
		NoColumnBalance,
		// Token: 0x0400195B RID: 6491
		CvtMailMergeQuote,
		// Token: 0x0400195C RID: 6492
		SuppressTopSpace,
		// Token: 0x0400195D RID: 6493
		SuppressPreParSpace,
		// Token: 0x0400195E RID: 6494
		CombineTblBorders,
		// Token: 0x0400195F RID: 6495
		TranspMetafiles,
		// Token: 0x04001960 RID: 6496
		SwapBorders,
		// Token: 0x04001961 RID: 6497
		ShowHardBreaks,
		// Token: 0x04001962 RID: 6498
		FormProtected,
		// Token: 0x04001963 RID: 6499
		AllProtected,
		// Token: 0x04001964 RID: 6500
		FormShading,
		// Token: 0x04001965 RID: 6501
		FormDisplay,
		// Token: 0x04001966 RID: 6502
		PrintData,
		// Token: 0x04001967 RID: 6503
		RevProtected,
		// Token: 0x04001968 RID: 6504
		Revisions,
		// Token: 0x04001969 RID: 6505
		RevDisplay,
		// Token: 0x0400196A RID: 6506
		RevBar,
		// Token: 0x0400196B RID: 6507
		AnnotProtected,
		// Token: 0x0400196C RID: 6508
		RTLDoc,
		// Token: 0x0400196D RID: 6509
		LTRDoc,
		// Token: 0x0400196E RID: 6510
		SectDef,
		// Token: 0x0400196F RID: 6511
		ENoteHere,
		// Token: 0x04001970 RID: 6512
		PrtBinFirst,
		// Token: 0x04001971 RID: 6513
		PrtBin,
		// Token: 0x04001972 RID: 6514
		SectStyleNum,
		// Token: 0x04001973 RID: 6515
		NoBreak,
		// Token: 0x04001974 RID: 6516
		ColBreak,
		// Token: 0x04001975 RID: 6517
		PageBreak,
		// Token: 0x04001976 RID: 6518
		EvenBreak,
		// Token: 0x04001977 RID: 6519
		OddBreak,
		// Token: 0x04001978 RID: 6520
		Columns,
		// Token: 0x04001979 RID: 6521
		ColumnSpace,
		// Token: 0x0400197A RID: 6522
		ColumnNumber,
		// Token: 0x0400197B RID: 6523
		ColumnSpRight,
		// Token: 0x0400197C RID: 6524
		ColumnWidth,
		// Token: 0x0400197D RID: 6525
		ColumnLine,
		// Token: 0x0400197E RID: 6526
		LineModulus,
		// Token: 0x0400197F RID: 6527
		LineDist,
		// Token: 0x04001980 RID: 6528
		LineStarts,
		// Token: 0x04001981 RID: 6529
		LineRestart,
		// Token: 0x04001982 RID: 6530
		LineRestartPg,
		// Token: 0x04001983 RID: 6531
		LineCont,
		// Token: 0x04001984 RID: 6532
		SectPageWid,
		// Token: 0x04001985 RID: 6533
		SectPageHt,
		// Token: 0x04001986 RID: 6534
		SectMarginLeft,
		// Token: 0x04001987 RID: 6535
		SectMarginRight,
		// Token: 0x04001988 RID: 6536
		SectMarginTop,
		// Token: 0x04001989 RID: 6537
		SectMarginBottom,
		// Token: 0x0400198A RID: 6538
		SectMarginGutter,
		// Token: 0x0400198B RID: 6539
		SectLandscape,
		// Token: 0x0400198C RID: 6540
		TitleSpecial,
		// Token: 0x0400198D RID: 6541
		HeaderY,
		// Token: 0x0400198E RID: 6542
		FooterY,
		// Token: 0x0400198F RID: 6543
		PageStarts,
		// Token: 0x04001990 RID: 6544
		PageCont,
		// Token: 0x04001991 RID: 6545
		PageRestart,
		// Token: 0x04001992 RID: 6546
		PageNumRight,
		// Token: 0x04001993 RID: 6547
		PageNumTop,
		// Token: 0x04001994 RID: 6548
		PageDecimal,
		// Token: 0x04001995 RID: 6549
		PageURoman,
		// Token: 0x04001996 RID: 6550
		PageLRoman,
		// Token: 0x04001997 RID: 6551
		PageULetter,
		// Token: 0x04001998 RID: 6552
		PageLLetter,
		// Token: 0x04001999 RID: 6553
		PageNumHyphSep,
		// Token: 0x0400199A RID: 6554
		PageNumSpaceSep,
		// Token: 0x0400199B RID: 6555
		PageNumColonSep,
		// Token: 0x0400199C RID: 6556
		PageNumEmdashSep,
		// Token: 0x0400199D RID: 6557
		PageNumEndashSep,
		// Token: 0x0400199E RID: 6558
		TopVAlign,
		// Token: 0x0400199F RID: 6559
		BottomVAlign,
		// Token: 0x040019A0 RID: 6560
		CenterVAlign,
		// Token: 0x040019A1 RID: 6561
		JustVAlign,
		// Token: 0x040019A2 RID: 6562
		RTLSect,
		// Token: 0x040019A3 RID: 6563
		LTRSect,
		// Token: 0x040019A4 RID: 6564
		RowDef,
		// Token: 0x040019A5 RID: 6565
		RowGapH,
		// Token: 0x040019A6 RID: 6566
		CellPos,
		// Token: 0x040019A7 RID: 6567
		MergeRngFirst,
		// Token: 0x040019A8 RID: 6568
		MergePrevious,
		// Token: 0x040019A9 RID: 6569
		RowLeft,
		// Token: 0x040019AA RID: 6570
		RowRight,
		// Token: 0x040019AB RID: 6571
		RowCenter,
		// Token: 0x040019AC RID: 6572
		RowLeftEdge,
		// Token: 0x040019AD RID: 6573
		RowHt,
		// Token: 0x040019AE RID: 6574
		RowHeader,
		// Token: 0x040019AF RID: 6575
		RowKeep,
		// Token: 0x040019B0 RID: 6576
		RTLRow,
		// Token: 0x040019B1 RID: 6577
		LTRRow,
		// Token: 0x040019B2 RID: 6578
		RowBordTop,
		// Token: 0x040019B3 RID: 6579
		RowBordLeft,
		// Token: 0x040019B4 RID: 6580
		RowBordBottom,
		// Token: 0x040019B5 RID: 6581
		RowBordRight,
		// Token: 0x040019B6 RID: 6582
		RowBordHoriz,
		// Token: 0x040019B7 RID: 6583
		RowBordVert,
		// Token: 0x040019B8 RID: 6584
		CellBordBottom,
		// Token: 0x040019B9 RID: 6585
		CellBordTop,
		// Token: 0x040019BA RID: 6586
		CellBordLeft,
		// Token: 0x040019BB RID: 6587
		CellBordRight,
		// Token: 0x040019BC RID: 6588
		CellShading,
		// Token: 0x040019BD RID: 6589
		CellBgPatH,
		// Token: 0x040019BE RID: 6590
		CellBgPatV,
		// Token: 0x040019BF RID: 6591
		CellFwdDiagBgPat,
		// Token: 0x040019C0 RID: 6592
		CellBwdDiagBgPat,
		// Token: 0x040019C1 RID: 6593
		CellHatchBgPat,
		// Token: 0x040019C2 RID: 6594
		CellDiagHatchBgPat,
		// Token: 0x040019C3 RID: 6595
		CellDarkBgPatH,
		// Token: 0x040019C4 RID: 6596
		CellDarkBgPatV,
		// Token: 0x040019C5 RID: 6597
		CellFwdDarkBgPat,
		// Token: 0x040019C6 RID: 6598
		CellBwdDarkBgPat,
		// Token: 0x040019C7 RID: 6599
		CellDarkHatchBgPat,
		// Token: 0x040019C8 RID: 6600
		CellDarkDiagHatchBgPat,
		// Token: 0x040019C9 RID: 6601
		CellBgPatLineColor,
		// Token: 0x040019CA RID: 6602
		CellBgPatColor,
		// Token: 0x040019CB RID: 6603
		ParDef,
		// Token: 0x040019CC RID: 6604
		StyleNum,
		// Token: 0x040019CD RID: 6605
		Hyphenate,
		// Token: 0x040019CE RID: 6606
		InTable,
		// Token: 0x040019CF RID: 6607
		Keep,
		// Token: 0x040019D0 RID: 6608
		NoWidowControl,
		// Token: 0x040019D1 RID: 6609
		KeepNext,
		// Token: 0x040019D2 RID: 6610
		OutlineLevel,
		// Token: 0x040019D3 RID: 6611
		NoLineNum,
		// Token: 0x040019D4 RID: 6612
		PBBefore,
		// Token: 0x040019D5 RID: 6613
		SideBySide,
		// Token: 0x040019D6 RID: 6614
		QuadLeft,
		// Token: 0x040019D7 RID: 6615
		QuadRight,
		// Token: 0x040019D8 RID: 6616
		QuadJust,
		// Token: 0x040019D9 RID: 6617
		QuadCenter,
		// Token: 0x040019DA RID: 6618
		FirstIndent,
		// Token: 0x040019DB RID: 6619
		LeftIndent,
		// Token: 0x040019DC RID: 6620
		RightIndent,
		// Token: 0x040019DD RID: 6621
		SpaceBefore,
		// Token: 0x040019DE RID: 6622
		SpaceAfter,
		// Token: 0x040019DF RID: 6623
		SpaceBetween,
		// Token: 0x040019E0 RID: 6624
		SpaceMultiply,
		// Token: 0x040019E1 RID: 6625
		SubDocument,
		// Token: 0x040019E2 RID: 6626
		RTLPar,
		// Token: 0x040019E3 RID: 6627
		LTRPar,
		// Token: 0x040019E4 RID: 6628
		TabPos,
		// Token: 0x040019E5 RID: 6629
		TabLeft,
		// Token: 0x040019E6 RID: 6630
		TabRight,
		// Token: 0x040019E7 RID: 6631
		TabCenter,
		// Token: 0x040019E8 RID: 6632
		TabDecimal,
		// Token: 0x040019E9 RID: 6633
		TabBar,
		// Token: 0x040019EA RID: 6634
		LeaderDot,
		// Token: 0x040019EB RID: 6635
		LeaderHyphen,
		// Token: 0x040019EC RID: 6636
		LeaderUnder,
		// Token: 0x040019ED RID: 6637
		LeaderThick,
		// Token: 0x040019EE RID: 6638
		LeaderEqual,
		// Token: 0x040019EF RID: 6639
		ParLevel,
		// Token: 0x040019F0 RID: 6640
		ParBullet,
		// Token: 0x040019F1 RID: 6641
		ParSimple,
		// Token: 0x040019F2 RID: 6642
		ParNumCont,
		// Token: 0x040019F3 RID: 6643
		ParNumOnce,
		// Token: 0x040019F4 RID: 6644
		ParNumAcross,
		// Token: 0x040019F5 RID: 6645
		ParHangIndent,
		// Token: 0x040019F6 RID: 6646
		ParNumRestart,
		// Token: 0x040019F7 RID: 6647
		ParNumCardinal,
		// Token: 0x040019F8 RID: 6648
		ParNumDecimal,
		// Token: 0x040019F9 RID: 6649
		ParNumULetter,
		// Token: 0x040019FA RID: 6650
		ParNumURoman,
		// Token: 0x040019FB RID: 6651
		ParNumLLetter,
		// Token: 0x040019FC RID: 6652
		ParNumLRoman,
		// Token: 0x040019FD RID: 6653
		ParNumOrdinal,
		// Token: 0x040019FE RID: 6654
		ParNumOrdinalText,
		// Token: 0x040019FF RID: 6655
		ParNumBold,
		// Token: 0x04001A00 RID: 6656
		ParNumItalic,
		// Token: 0x04001A01 RID: 6657
		ParNumAllCaps,
		// Token: 0x04001A02 RID: 6658
		ParNumSmallCaps,
		// Token: 0x04001A03 RID: 6659
		ParNumUnder,
		// Token: 0x04001A04 RID: 6660
		ParNumDotUnder,
		// Token: 0x04001A05 RID: 6661
		ParNumDbUnder,
		// Token: 0x04001A06 RID: 6662
		ParNumNoUnder,
		// Token: 0x04001A07 RID: 6663
		ParNumWordUnder,
		// Token: 0x04001A08 RID: 6664
		ParNumStrikethru,
		// Token: 0x04001A09 RID: 6665
		ParNumForeColor,
		// Token: 0x04001A0A RID: 6666
		ParNumFont,
		// Token: 0x04001A0B RID: 6667
		ParNumFontSize,
		// Token: 0x04001A0C RID: 6668
		ParNumIndent,
		// Token: 0x04001A0D RID: 6669
		ParNumSpacing,
		// Token: 0x04001A0E RID: 6670
		ParNumInclPrev,
		// Token: 0x04001A0F RID: 6671
		ParNumCenter,
		// Token: 0x04001A10 RID: 6672
		ParNumLeft,
		// Token: 0x04001A11 RID: 6673
		ParNumRight,
		// Token: 0x04001A12 RID: 6674
		ParNumStartAt,
		// Token: 0x04001A13 RID: 6675
		BorderTop,
		// Token: 0x04001A14 RID: 6676
		BorderBottom,
		// Token: 0x04001A15 RID: 6677
		BorderLeft,
		// Token: 0x04001A16 RID: 6678
		BorderRight,
		// Token: 0x04001A17 RID: 6679
		BorderBetween,
		// Token: 0x04001A18 RID: 6680
		BorderBar,
		// Token: 0x04001A19 RID: 6681
		BorderBox,
		// Token: 0x04001A1A RID: 6682
		BorderSingle,
		// Token: 0x04001A1B RID: 6683
		BorderThick,
		// Token: 0x04001A1C RID: 6684
		BorderShadow,
		// Token: 0x04001A1D RID: 6685
		BorderDouble,
		// Token: 0x04001A1E RID: 6686
		BorderDot,
		// Token: 0x04001A1F RID: 6687
		BorderDash,
		// Token: 0x04001A20 RID: 6688
		BorderHair,
		// Token: 0x04001A21 RID: 6689
		BorderWidth,
		// Token: 0x04001A22 RID: 6690
		BorderColor,
		// Token: 0x04001A23 RID: 6691
		BorderSpace,
		// Token: 0x04001A24 RID: 6692
		Shading,
		// Token: 0x04001A25 RID: 6693
		BgPatH,
		// Token: 0x04001A26 RID: 6694
		BgPatV,
		// Token: 0x04001A27 RID: 6695
		FwdDiagBgPat,
		// Token: 0x04001A28 RID: 6696
		BwdDiagBgPat,
		// Token: 0x04001A29 RID: 6697
		HatchBgPat,
		// Token: 0x04001A2A RID: 6698
		DiagHatchBgPat,
		// Token: 0x04001A2B RID: 6699
		DarkBgPatH,
		// Token: 0x04001A2C RID: 6700
		DarkBgPatV,
		// Token: 0x04001A2D RID: 6701
		FwdDarkBgPat,
		// Token: 0x04001A2E RID: 6702
		BwdDarkBgPat,
		// Token: 0x04001A2F RID: 6703
		DarkHatchBgPat,
		// Token: 0x04001A30 RID: 6704
		DarkDiagHatchBgPat,
		// Token: 0x04001A31 RID: 6705
		BgPatLineColor,
		// Token: 0x04001A32 RID: 6706
		BgPatColor,
		// Token: 0x04001A33 RID: 6707
		Plain,
		// Token: 0x04001A34 RID: 6708
		Bold,
		// Token: 0x04001A35 RID: 6709
		AllCaps,
		// Token: 0x04001A36 RID: 6710
		Deleted,
		// Token: 0x04001A37 RID: 6711
		SubScript,
		// Token: 0x04001A38 RID: 6712
		SubScrShrink,
		// Token: 0x04001A39 RID: 6713
		NoSuperSub,
		// Token: 0x04001A3A RID: 6714
		Expand,
		// Token: 0x04001A3B RID: 6715
		ExpandTwips,
		// Token: 0x04001A3C RID: 6716
		Kerning,
		// Token: 0x04001A3D RID: 6717
		FontNum,
		// Token: 0x04001A3E RID: 6718
		FontSize,
		// Token: 0x04001A3F RID: 6719
		Italic,
		// Token: 0x04001A40 RID: 6720
		Outline,
		// Token: 0x04001A41 RID: 6721
		Revised,
		// Token: 0x04001A42 RID: 6722
		RevAuthor,
		// Token: 0x04001A43 RID: 6723
		RevDTTM,
		// Token: 0x04001A44 RID: 6724
		SmallCaps,
		// Token: 0x04001A45 RID: 6725
		Shadow,
		// Token: 0x04001A46 RID: 6726
		StrikeThru,
		// Token: 0x04001A47 RID: 6727
		Underline,
		// Token: 0x04001A48 RID: 6728
		DotUnderline,
		// Token: 0x04001A49 RID: 6729
		DbUnderline,
		// Token: 0x04001A4A RID: 6730
		NoUnderline,
		// Token: 0x04001A4B RID: 6731
		WordUnderline,
		// Token: 0x04001A4C RID: 6732
		SuperScript,
		// Token: 0x04001A4D RID: 6733
		SuperScrShrink,
		// Token: 0x04001A4E RID: 6734
		Invisible,
		// Token: 0x04001A4F RID: 6735
		ForeColor,
		// Token: 0x04001A50 RID: 6736
		BackColor,
		// Token: 0x04001A51 RID: 6737
		RTLChar,
		// Token: 0x04001A52 RID: 6738
		LTRChar,
		// Token: 0x04001A53 RID: 6739
		CharStyleNum,
		// Token: 0x04001A54 RID: 6740
		CharCharSet,
		// Token: 0x04001A55 RID: 6741
		Language,
		// Token: 0x04001A56 RID: 6742
		Gray,
		// Token: 0x04001A57 RID: 6743
		MacQD,
		// Token: 0x04001A58 RID: 6744
		PMMetafile,
		// Token: 0x04001A59 RID: 6745
		WinMetafile,
		// Token: 0x04001A5A RID: 6746
		DevIndBitmap,
		// Token: 0x04001A5B RID: 6747
		WinBitmap,
		// Token: 0x04001A5C RID: 6748
		PngBlip,
		// Token: 0x04001A5D RID: 6749
		PixelBits,
		// Token: 0x04001A5E RID: 6750
		BitmapPlanes,
		// Token: 0x04001A5F RID: 6751
		BitmapWid,
		// Token: 0x04001A60 RID: 6752
		PicWid,
		// Token: 0x04001A61 RID: 6753
		PicHt,
		// Token: 0x04001A62 RID: 6754
		PicGoalWid,
		// Token: 0x04001A63 RID: 6755
		PicGoalHt,
		// Token: 0x04001A64 RID: 6756
		PicScaleX,
		// Token: 0x04001A65 RID: 6757
		PicScaleY,
		// Token: 0x04001A66 RID: 6758
		PicScaled,
		// Token: 0x04001A67 RID: 6759
		PicCropTop,
		// Token: 0x04001A68 RID: 6760
		PicCropBottom,
		// Token: 0x04001A69 RID: 6761
		PicCropLeft,
		// Token: 0x04001A6A RID: 6762
		PicCropRight,
		// Token: 0x04001A6B RID: 6763
		PicMFHasBitmap,
		// Token: 0x04001A6C RID: 6764
		PicMFBitsPerPixel,
		// Token: 0x04001A6D RID: 6765
		PicBinary,
		// Token: 0x04001A6E RID: 6766
		BookmarkFirstCol,
		// Token: 0x04001A6F RID: 6767
		BookmarkLastCol,
		// Token: 0x04001A70 RID: 6768
		NeXTGWidth,
		// Token: 0x04001A71 RID: 6769
		NeXTGHeight,
		// Token: 0x04001A72 RID: 6770
		FieldDirty,
		// Token: 0x04001A73 RID: 6771
		FieldEdited,
		// Token: 0x04001A74 RID: 6772
		FieldLocked,
		// Token: 0x04001A75 RID: 6773
		FieldPrivate,
		// Token: 0x04001A76 RID: 6774
		FieldAlt,
		// Token: 0x04001A77 RID: 6775
		TOCType,
		// Token: 0x04001A78 RID: 6776
		TOCLevel,
		// Token: 0x04001A79 RID: 6777
		AbsWid,
		// Token: 0x04001A7A RID: 6778
		AbsHt,
		// Token: 0x04001A7B RID: 6779
		RPosMargH,
		// Token: 0x04001A7C RID: 6780
		RPosPageH,
		// Token: 0x04001A7D RID: 6781
		RPosColH,
		// Token: 0x04001A7E RID: 6782
		PosX,
		// Token: 0x04001A7F RID: 6783
		PosNegX,
		// Token: 0x04001A80 RID: 6784
		PosXCenter,
		// Token: 0x04001A81 RID: 6785
		PosXInside,
		// Token: 0x04001A82 RID: 6786
		PosXOutSide,
		// Token: 0x04001A83 RID: 6787
		PosXRight,
		// Token: 0x04001A84 RID: 6788
		PosXLeft,
		// Token: 0x04001A85 RID: 6789
		RPosMargV,
		// Token: 0x04001A86 RID: 6790
		RPosPageV,
		// Token: 0x04001A87 RID: 6791
		RPosParaV,
		// Token: 0x04001A88 RID: 6792
		PosY,
		// Token: 0x04001A89 RID: 6793
		PosNegY,
		// Token: 0x04001A8A RID: 6794
		PosYInline,
		// Token: 0x04001A8B RID: 6795
		PosYTop,
		// Token: 0x04001A8C RID: 6796
		PosYCenter,
		// Token: 0x04001A8D RID: 6797
		PosYBottom,
		// Token: 0x04001A8E RID: 6798
		NoWrap,
		// Token: 0x04001A8F RID: 6799
		DistFromTextAll,
		// Token: 0x04001A90 RID: 6800
		DistFromTextX,
		// Token: 0x04001A91 RID: 6801
		DistFromTextY,
		// Token: 0x04001A92 RID: 6802
		TextDistY,
		// Token: 0x04001A93 RID: 6803
		DropCapLines,
		// Token: 0x04001A94 RID: 6804
		DropCapType,
		// Token: 0x04001A95 RID: 6805
		ObjEmb,
		// Token: 0x04001A96 RID: 6806
		ObjLink,
		// Token: 0x04001A97 RID: 6807
		ObjAutoLink,
		// Token: 0x04001A98 RID: 6808
		ObjSubscriber,
		// Token: 0x04001A99 RID: 6809
		ObjPublisher,
		// Token: 0x04001A9A RID: 6810
		ObjICEmb,
		// Token: 0x04001A9B RID: 6811
		ObjLinkSelf,
		// Token: 0x04001A9C RID: 6812
		ObjLock,
		// Token: 0x04001A9D RID: 6813
		ObjUpdate,
		// Token: 0x04001A9E RID: 6814
		ObjHt,
		// Token: 0x04001A9F RID: 6815
		ObjWid,
		// Token: 0x04001AA0 RID: 6816
		ObjSetSize,
		// Token: 0x04001AA1 RID: 6817
		ObjAlign,
		// Token: 0x04001AA2 RID: 6818
		ObjTransposeY,
		// Token: 0x04001AA3 RID: 6819
		ObjCropTop,
		// Token: 0x04001AA4 RID: 6820
		ObjCropBottom,
		// Token: 0x04001AA5 RID: 6821
		ObjCropLeft,
		// Token: 0x04001AA6 RID: 6822
		ObjCropRight,
		// Token: 0x04001AA7 RID: 6823
		ObjScaleX,
		// Token: 0x04001AA8 RID: 6824
		ObjScaleY,
		// Token: 0x04001AA9 RID: 6825
		ObjResRTF,
		// Token: 0x04001AAA RID: 6826
		ObjResPict,
		// Token: 0x04001AAB RID: 6827
		ObjResBitmap,
		// Token: 0x04001AAC RID: 6828
		ObjResText,
		// Token: 0x04001AAD RID: 6829
		ObjResMerge,
		// Token: 0x04001AAE RID: 6830
		ObjBookmarkPubObj,
		// Token: 0x04001AAF RID: 6831
		ObjPubAutoUpdate,
		// Token: 0x04001AB0 RID: 6832
		FNAlt,
		// Token: 0x04001AB1 RID: 6833
		AltKey,
		// Token: 0x04001AB2 RID: 6834
		ShiftKey,
		// Token: 0x04001AB3 RID: 6835
		ControlKey,
		// Token: 0x04001AB4 RID: 6836
		FunctionKey,
		// Token: 0x04001AB5 RID: 6837
		ACBold,
		// Token: 0x04001AB6 RID: 6838
		ACAllCaps,
		// Token: 0x04001AB7 RID: 6839
		ACForeColor,
		// Token: 0x04001AB8 RID: 6840
		ACSubScript,
		// Token: 0x04001AB9 RID: 6841
		ACExpand,
		// Token: 0x04001ABA RID: 6842
		ACFontNum,
		// Token: 0x04001ABB RID: 6843
		ACFontSize,
		// Token: 0x04001ABC RID: 6844
		ACItalic,
		// Token: 0x04001ABD RID: 6845
		ACLanguage,
		// Token: 0x04001ABE RID: 6846
		ACOutline,
		// Token: 0x04001ABF RID: 6847
		ACSmallCaps,
		// Token: 0x04001AC0 RID: 6848
		ACShadow,
		// Token: 0x04001AC1 RID: 6849
		ACStrikeThru,
		// Token: 0x04001AC2 RID: 6850
		ACUnderline,
		// Token: 0x04001AC3 RID: 6851
		ACDotUnderline,
		// Token: 0x04001AC4 RID: 6852
		ACDbUnderline,
		// Token: 0x04001AC5 RID: 6853
		ACNoUnderline,
		// Token: 0x04001AC6 RID: 6854
		ACWordUnderline,
		// Token: 0x04001AC7 RID: 6855
		ACSuperScript,
		// Token: 0x04001AC8 RID: 6856
		FontCharSet,
		// Token: 0x04001AC9 RID: 6857
		FontPitch,
		// Token: 0x04001ACA RID: 6858
		FontCodePage,
		// Token: 0x04001ACB RID: 6859
		FTypeNil,
		// Token: 0x04001ACC RID: 6860
		FTypeTrueType,
		// Token: 0x04001ACD RID: 6861
		FileNum,
		// Token: 0x04001ACE RID: 6862
		FileRelPath,
		// Token: 0x04001ACF RID: 6863
		FileOSNum,
		// Token: 0x04001AD0 RID: 6864
		SrcMacintosh,
		// Token: 0x04001AD1 RID: 6865
		SrcDOS,
		// Token: 0x04001AD2 RID: 6866
		SrcNTFS,
		// Token: 0x04001AD3 RID: 6867
		SrcHPFS,
		// Token: 0x04001AD4 RID: 6868
		SrcNetwork,
		// Token: 0x04001AD5 RID: 6869
		DrawLock,
		// Token: 0x04001AD6 RID: 6870
		DrawPageRelX,
		// Token: 0x04001AD7 RID: 6871
		DrawColumnRelX,
		// Token: 0x04001AD8 RID: 6872
		DrawMarginRelX,
		// Token: 0x04001AD9 RID: 6873
		DrawPageRelY,
		// Token: 0x04001ADA RID: 6874
		DrawColumnRelY,
		// Token: 0x04001ADB RID: 6875
		DrawMarginRelY,
		// Token: 0x04001ADC RID: 6876
		DrawHeight,
		// Token: 0x04001ADD RID: 6877
		DrawBeginGroup,
		// Token: 0x04001ADE RID: 6878
		DrawGroupCount,
		// Token: 0x04001ADF RID: 6879
		DrawEndGroup,
		// Token: 0x04001AE0 RID: 6880
		DrawArc,
		// Token: 0x04001AE1 RID: 6881
		DrawCallout,
		// Token: 0x04001AE2 RID: 6882
		DrawEllipse,
		// Token: 0x04001AE3 RID: 6883
		DrawLine,
		// Token: 0x04001AE4 RID: 6884
		DrawPolygon,
		// Token: 0x04001AE5 RID: 6885
		DrawPolyLine,
		// Token: 0x04001AE6 RID: 6886
		DrawRect,
		// Token: 0x04001AE7 RID: 6887
		DrawTextBox,
		// Token: 0x04001AE8 RID: 6888
		DrawOffsetX,
		// Token: 0x04001AE9 RID: 6889
		DrawSizeX,
		// Token: 0x04001AEA RID: 6890
		DrawOffsetY,
		// Token: 0x04001AEB RID: 6891
		DrawSizeY,
		// Token: 0x04001AEC RID: 6892
		COAngle,
		// Token: 0x04001AED RID: 6893
		COAccentBar,
		// Token: 0x04001AEE RID: 6894
		COBestFit,
		// Token: 0x04001AEF RID: 6895
		COBorder,
		// Token: 0x04001AF0 RID: 6896
		COAttachAbsDist,
		// Token: 0x04001AF1 RID: 6897
		COAttachBottom,
		// Token: 0x04001AF2 RID: 6898
		COAttachCenter,
		// Token: 0x04001AF3 RID: 6899
		COAttachTop,
		// Token: 0x04001AF4 RID: 6900
		COLength,
		// Token: 0x04001AF5 RID: 6901
		CONegXQuadrant,
		// Token: 0x04001AF6 RID: 6902
		CONegYQuadrant,
		// Token: 0x04001AF7 RID: 6903
		COOffset,
		// Token: 0x04001AF8 RID: 6904
		COAttachSmart,
		// Token: 0x04001AF9 RID: 6905
		CODoubleLine,
		// Token: 0x04001AFA RID: 6906
		CORightAngle,
		// Token: 0x04001AFB RID: 6907
		COSingleLine,
		// Token: 0x04001AFC RID: 6908
		COTripleLine,
		// Token: 0x04001AFD RID: 6909
		DrawTextBoxMargin,
		// Token: 0x04001AFE RID: 6910
		DrawTextBoxText,
		// Token: 0x04001AFF RID: 6911
		DrawRoundRect,
		// Token: 0x04001B00 RID: 6912
		DrawPointX,
		// Token: 0x04001B01 RID: 6913
		DrawPointY,
		// Token: 0x04001B02 RID: 6914
		DrawPolyCount,
		// Token: 0x04001B03 RID: 6915
		DrawArcFlipX,
		// Token: 0x04001B04 RID: 6916
		DrawArcFlipY,
		// Token: 0x04001B05 RID: 6917
		DrawLineBlue,
		// Token: 0x04001B06 RID: 6918
		DrawLineGreen,
		// Token: 0x04001B07 RID: 6919
		DrawLineRed,
		// Token: 0x04001B08 RID: 6920
		DrawLinePalette,
		// Token: 0x04001B09 RID: 6921
		DrawLineDashDot,
		// Token: 0x04001B0A RID: 6922
		DrawLineDashDotDot,
		// Token: 0x04001B0B RID: 6923
		DrawLineDash,
		// Token: 0x04001B0C RID: 6924
		DrawLineDot,
		// Token: 0x04001B0D RID: 6925
		DrawLineGray,
		// Token: 0x04001B0E RID: 6926
		DrawLineHollow,
		// Token: 0x04001B0F RID: 6927
		DrawLineSolid,
		// Token: 0x04001B10 RID: 6928
		DrawLineWidth,
		// Token: 0x04001B11 RID: 6929
		DrawHollowEndArrow,
		// Token: 0x04001B12 RID: 6930
		DrawEndArrowLength,
		// Token: 0x04001B13 RID: 6931
		DrawSolidEndArrow,
		// Token: 0x04001B14 RID: 6932
		DrawEndArrowWidth,
		// Token: 0x04001B15 RID: 6933
		DrawHollowStartArrow,
		// Token: 0x04001B16 RID: 6934
		DrawStartArrowLength,
		// Token: 0x04001B17 RID: 6935
		DrawSolidStartArrow,
		// Token: 0x04001B18 RID: 6936
		DrawStartArrowWidth,
		// Token: 0x04001B19 RID: 6937
		DrawBgFillBlue,
		// Token: 0x04001B1A RID: 6938
		DrawBgFillGreen,
		// Token: 0x04001B1B RID: 6939
		DrawBgFillRed,
		// Token: 0x04001B1C RID: 6940
		DrawBgFillPalette,
		// Token: 0x04001B1D RID: 6941
		DrawBgFillGray,
		// Token: 0x04001B1E RID: 6942
		DrawFgFillBlue,
		// Token: 0x04001B1F RID: 6943
		DrawFgFillGreen,
		// Token: 0x04001B20 RID: 6944
		DrawFgFillRed,
		// Token: 0x04001B21 RID: 6945
		DrawFgFillPalette,
		// Token: 0x04001B22 RID: 6946
		DrawFgFillGray,
		// Token: 0x04001B23 RID: 6947
		DrawFillPatIndex,
		// Token: 0x04001B24 RID: 6948
		DrawShadow,
		// Token: 0x04001B25 RID: 6949
		DrawShadowXOffset,
		// Token: 0x04001B26 RID: 6950
		DrawShadowYOffset,
		// Token: 0x04001B27 RID: 6951
		IndexNumber,
		// Token: 0x04001B28 RID: 6952
		IndexBold,
		// Token: 0x04001B29 RID: 6953
		IndexItalic,
		// Token: 0x04001B2A RID: 6954
		UnicodeCharBytes,
		// Token: 0x04001B2B RID: 6955
		UnicodeChar,
		// Token: 0x04001B2C RID: 6956
		UnicodeDestination,
		// Token: 0x04001B2D RID: 6957
		UnicodeDualDestination,
		// Token: 0x04001B2E RID: 6958
		UnicodeAnsiCodepage
	}
}
