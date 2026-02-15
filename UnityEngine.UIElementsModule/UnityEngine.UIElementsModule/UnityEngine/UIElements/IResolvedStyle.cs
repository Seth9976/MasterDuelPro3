using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000342 RID: 834
	public interface IResolvedStyle
	{
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001702 RID: 5890
		Align alignContent { get; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001703 RID: 5891
		Align alignItems { get; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001704 RID: 5892
		Align alignSelf { get; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001705 RID: 5893
		Color backgroundColor { get; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001706 RID: 5894
		Background backgroundImage { get; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001707 RID: 5895
		BackgroundPosition backgroundPositionX { get; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001708 RID: 5896
		BackgroundPosition backgroundPositionY { get; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001709 RID: 5897
		BackgroundRepeat backgroundRepeat { get; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600170A RID: 5898
		BackgroundSize backgroundSize { get; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600170B RID: 5899
		Color borderBottomColor { get; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600170C RID: 5900
		float borderBottomLeftRadius { get; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600170D RID: 5901
		float borderBottomRightRadius { get; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600170E RID: 5902
		float borderBottomWidth { get; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600170F RID: 5903
		Color borderLeftColor { get; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001710 RID: 5904
		float borderLeftWidth { get; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001711 RID: 5905
		Color borderRightColor { get; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001712 RID: 5906
		float borderRightWidth { get; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001713 RID: 5907
		Color borderTopColor { get; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001714 RID: 5908
		float borderTopLeftRadius { get; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001715 RID: 5909
		float borderTopRightRadius { get; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001716 RID: 5910
		float borderTopWidth { get; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001717 RID: 5911
		float bottom { get; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001718 RID: 5912
		Color color { get; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001719 RID: 5913
		DisplayStyle display { get; }

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600171A RID: 5914
		StyleFloat flexBasis { get; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600171B RID: 5915
		FlexDirection flexDirection { get; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600171C RID: 5916
		float flexGrow { get; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600171D RID: 5917
		float flexShrink { get; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600171E RID: 5918
		Wrap flexWrap { get; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600171F RID: 5919
		float fontSize { get; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001720 RID: 5920
		float height { get; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001721 RID: 5921
		Justify justifyContent { get; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001722 RID: 5922
		float left { get; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001723 RID: 5923
		float letterSpacing { get; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001724 RID: 5924
		float marginBottom { get; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001725 RID: 5925
		float marginLeft { get; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001726 RID: 5926
		float marginRight { get; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001727 RID: 5927
		float marginTop { get; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001728 RID: 5928
		StyleFloat maxHeight { get; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001729 RID: 5929
		StyleFloat maxWidth { get; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600172A RID: 5930
		StyleFloat minHeight { get; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600172B RID: 5931
		StyleFloat minWidth { get; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600172C RID: 5932
		float opacity { get; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600172D RID: 5933
		float paddingBottom { get; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600172E RID: 5934
		float paddingLeft { get; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600172F RID: 5935
		float paddingRight { get; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001730 RID: 5936
		float paddingTop { get; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001731 RID: 5937
		Position position { get; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001732 RID: 5938
		float right { get; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001733 RID: 5939
		Rotate rotate { get; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001734 RID: 5940
		Scale scale { get; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001735 RID: 5941
		TextOverflow textOverflow { get; }

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001736 RID: 5942
		float top { get; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001737 RID: 5943
		Vector3 transformOrigin { get; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001738 RID: 5944
		IEnumerable<TimeValue> transitionDelay { get; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001739 RID: 5945
		IEnumerable<TimeValue> transitionDuration { get; }

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600173A RID: 5946
		IEnumerable<StylePropertyName> transitionProperty { get; }

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x0600173B RID: 5947
		IEnumerable<EasingFunction> transitionTimingFunction { get; }

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x0600173C RID: 5948
		Vector3 translate { get; }

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600173D RID: 5949
		Color unityBackgroundImageTintColor { get; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600173E RID: 5950
		EditorTextRenderingMode unityEditorTextRenderingMode { get; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600173F RID: 5951
		Font unityFont { get; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001740 RID: 5952
		FontDefinition unityFontDefinition { get; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001741 RID: 5953
		FontStyle unityFontStyleAndWeight { get; }

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001742 RID: 5954
		float unityParagraphSpacing { get; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001743 RID: 5955
		int unitySliceBottom { get; }

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001744 RID: 5956
		int unitySliceLeft { get; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001745 RID: 5957
		int unitySliceRight { get; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001746 RID: 5958
		float unitySliceScale { get; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001747 RID: 5959
		int unitySliceTop { get; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001748 RID: 5960
		TextAnchor unityTextAlign { get; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001749 RID: 5961
		TextGeneratorType unityTextGenerator { get; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600174A RID: 5962
		Color unityTextOutlineColor { get; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600174B RID: 5963
		float unityTextOutlineWidth { get; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600174C RID: 5964
		TextOverflowPosition unityTextOverflowPosition { get; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600174D RID: 5965
		Visibility visibility { get; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600174E RID: 5966
		WhiteSpace whiteSpace { get; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600174F RID: 5967
		float width { get; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001750 RID: 5968
		float wordSpacing { get; }
	}
}
