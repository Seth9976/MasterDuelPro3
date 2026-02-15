using System;

namespace System.Windows.Forms
{
	// Token: 0x02000289 RID: 649
	internal enum XRequest : byte
	{
		// Token: 0x04001182 RID: 4482
		X_CreateWindow = 1,
		// Token: 0x04001183 RID: 4483
		X_ChangeWindowAttributes,
		// Token: 0x04001184 RID: 4484
		X_GetWindowAttributes,
		// Token: 0x04001185 RID: 4485
		X_DestroyWindow,
		// Token: 0x04001186 RID: 4486
		X_DestroySubwindows,
		// Token: 0x04001187 RID: 4487
		X_ChangeSaveSet,
		// Token: 0x04001188 RID: 4488
		X_ReparentWindow,
		// Token: 0x04001189 RID: 4489
		X_MapWindow,
		// Token: 0x0400118A RID: 4490
		X_MapSubwindows,
		// Token: 0x0400118B RID: 4491
		X_UnmapWindow,
		// Token: 0x0400118C RID: 4492
		X_UnmapSubwindows,
		// Token: 0x0400118D RID: 4493
		X_ConfigureWindow,
		// Token: 0x0400118E RID: 4494
		X_CirculateWindow,
		// Token: 0x0400118F RID: 4495
		X_GetGeometry,
		// Token: 0x04001190 RID: 4496
		X_QueryTree,
		// Token: 0x04001191 RID: 4497
		X_InternAtom,
		// Token: 0x04001192 RID: 4498
		X_GetAtomName,
		// Token: 0x04001193 RID: 4499
		X_ChangeProperty,
		// Token: 0x04001194 RID: 4500
		X_DeleteProperty,
		// Token: 0x04001195 RID: 4501
		X_GetProperty,
		// Token: 0x04001196 RID: 4502
		X_ListProperties,
		// Token: 0x04001197 RID: 4503
		X_SetSelectionOwner,
		// Token: 0x04001198 RID: 4504
		X_GetSelectionOwner,
		// Token: 0x04001199 RID: 4505
		X_ConvertSelection,
		// Token: 0x0400119A RID: 4506
		X_SendEvent,
		// Token: 0x0400119B RID: 4507
		X_GrabPointer,
		// Token: 0x0400119C RID: 4508
		X_UngrabPointer,
		// Token: 0x0400119D RID: 4509
		X_GrabButton,
		// Token: 0x0400119E RID: 4510
		X_UngrabButton,
		// Token: 0x0400119F RID: 4511
		X_ChangeActivePointerGrab,
		// Token: 0x040011A0 RID: 4512
		X_GrabKeyboard,
		// Token: 0x040011A1 RID: 4513
		X_UngrabKeyboard,
		// Token: 0x040011A2 RID: 4514
		X_GrabKey,
		// Token: 0x040011A3 RID: 4515
		X_UngrabKey,
		// Token: 0x040011A4 RID: 4516
		X_AllowEvents,
		// Token: 0x040011A5 RID: 4517
		X_GrabServer,
		// Token: 0x040011A6 RID: 4518
		X_UngrabServer,
		// Token: 0x040011A7 RID: 4519
		X_QueryPointer,
		// Token: 0x040011A8 RID: 4520
		X_GetMotionEvents,
		// Token: 0x040011A9 RID: 4521
		X_TranslateCoords,
		// Token: 0x040011AA RID: 4522
		X_WarpPointer,
		// Token: 0x040011AB RID: 4523
		X_SetInputFocus,
		// Token: 0x040011AC RID: 4524
		X_GetInputFocus,
		// Token: 0x040011AD RID: 4525
		X_QueryKeymap,
		// Token: 0x040011AE RID: 4526
		X_OpenFont,
		// Token: 0x040011AF RID: 4527
		X_CloseFont,
		// Token: 0x040011B0 RID: 4528
		X_QueryFont,
		// Token: 0x040011B1 RID: 4529
		X_QueryTextExtents,
		// Token: 0x040011B2 RID: 4530
		X_ListFonts,
		// Token: 0x040011B3 RID: 4531
		X_ListFontsWithInfo,
		// Token: 0x040011B4 RID: 4532
		X_SetFontPath,
		// Token: 0x040011B5 RID: 4533
		X_GetFontPath,
		// Token: 0x040011B6 RID: 4534
		X_CreatePixmap,
		// Token: 0x040011B7 RID: 4535
		X_FreePixmap,
		// Token: 0x040011B8 RID: 4536
		X_CreateGC,
		// Token: 0x040011B9 RID: 4537
		X_ChangeGC,
		// Token: 0x040011BA RID: 4538
		X_CopyGC,
		// Token: 0x040011BB RID: 4539
		X_SetDashes,
		// Token: 0x040011BC RID: 4540
		X_SetClipRectangles,
		// Token: 0x040011BD RID: 4541
		X_FreeGC,
		// Token: 0x040011BE RID: 4542
		X_ClearArea,
		// Token: 0x040011BF RID: 4543
		X_CopyArea,
		// Token: 0x040011C0 RID: 4544
		X_CopyPlane,
		// Token: 0x040011C1 RID: 4545
		X_PolyPoint,
		// Token: 0x040011C2 RID: 4546
		X_PolyLine,
		// Token: 0x040011C3 RID: 4547
		X_PolySegment,
		// Token: 0x040011C4 RID: 4548
		X_PolyRectangle,
		// Token: 0x040011C5 RID: 4549
		X_PolyArc,
		// Token: 0x040011C6 RID: 4550
		X_FillPoly,
		// Token: 0x040011C7 RID: 4551
		X_PolyFillRectangle,
		// Token: 0x040011C8 RID: 4552
		X_PolyFillArc,
		// Token: 0x040011C9 RID: 4553
		X_PutImage,
		// Token: 0x040011CA RID: 4554
		X_GetImage,
		// Token: 0x040011CB RID: 4555
		X_PolyText8,
		// Token: 0x040011CC RID: 4556
		X_PolyText16,
		// Token: 0x040011CD RID: 4557
		X_ImageText8,
		// Token: 0x040011CE RID: 4558
		X_ImageText16,
		// Token: 0x040011CF RID: 4559
		X_CreateColormap,
		// Token: 0x040011D0 RID: 4560
		X_FreeColormap,
		// Token: 0x040011D1 RID: 4561
		X_CopyColormapAndFree,
		// Token: 0x040011D2 RID: 4562
		X_InstallColormap,
		// Token: 0x040011D3 RID: 4563
		X_UninstallColormap,
		// Token: 0x040011D4 RID: 4564
		X_ListInstalledColormaps,
		// Token: 0x040011D5 RID: 4565
		X_AllocColor,
		// Token: 0x040011D6 RID: 4566
		X_AllocNamedColor,
		// Token: 0x040011D7 RID: 4567
		X_AllocColorCells,
		// Token: 0x040011D8 RID: 4568
		X_AllocColorPlanes,
		// Token: 0x040011D9 RID: 4569
		X_FreeColors,
		// Token: 0x040011DA RID: 4570
		X_StoreColors,
		// Token: 0x040011DB RID: 4571
		X_StoreNamedColor,
		// Token: 0x040011DC RID: 4572
		X_QueryColors,
		// Token: 0x040011DD RID: 4573
		X_LookupColor,
		// Token: 0x040011DE RID: 4574
		X_CreateCursor,
		// Token: 0x040011DF RID: 4575
		X_CreateGlyphCursor,
		// Token: 0x040011E0 RID: 4576
		X_FreeCursor,
		// Token: 0x040011E1 RID: 4577
		X_RecolorCursor,
		// Token: 0x040011E2 RID: 4578
		X_QueryBestSize,
		// Token: 0x040011E3 RID: 4579
		X_QueryExtension,
		// Token: 0x040011E4 RID: 4580
		X_ListExtensions,
		// Token: 0x040011E5 RID: 4581
		X_ChangeKeyboardMapping,
		// Token: 0x040011E6 RID: 4582
		X_GetKeyboardMapping,
		// Token: 0x040011E7 RID: 4583
		X_ChangeKeyboardControl,
		// Token: 0x040011E8 RID: 4584
		X_GetKeyboardControl,
		// Token: 0x040011E9 RID: 4585
		X_Bell,
		// Token: 0x040011EA RID: 4586
		X_ChangePointerControl,
		// Token: 0x040011EB RID: 4587
		X_GetPointerControl,
		// Token: 0x040011EC RID: 4588
		X_SetScreenSaver,
		// Token: 0x040011ED RID: 4589
		X_GetScreenSaver,
		// Token: 0x040011EE RID: 4590
		X_ChangeHosts,
		// Token: 0x040011EF RID: 4591
		X_ListHosts,
		// Token: 0x040011F0 RID: 4592
		X_SetAccessControl,
		// Token: 0x040011F1 RID: 4593
		X_SetCloseDownMode,
		// Token: 0x040011F2 RID: 4594
		X_KillClient,
		// Token: 0x040011F3 RID: 4595
		X_RotateProperties,
		// Token: 0x040011F4 RID: 4596
		X_ForceScreenSaver,
		// Token: 0x040011F5 RID: 4597
		X_SetPointerMapping,
		// Token: 0x040011F6 RID: 4598
		X_GetPointerMapping,
		// Token: 0x040011F7 RID: 4599
		X_SetModifierMapping,
		// Token: 0x040011F8 RID: 4600
		X_GetModifierMapping,
		// Token: 0x040011F9 RID: 4601
		X_NoOperation = 127
	}
}
