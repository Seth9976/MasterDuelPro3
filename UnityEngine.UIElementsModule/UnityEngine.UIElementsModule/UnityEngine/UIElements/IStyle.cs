using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000343 RID: 835
	public interface IStyle
	{
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001751 RID: 5969
		// (set) Token: 0x06001752 RID: 5970
		StyleEnum<Align> alignContent { get; set; }

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001753 RID: 5971
		// (set) Token: 0x06001754 RID: 5972
		StyleEnum<Align> alignItems { get; set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001755 RID: 5973
		// (set) Token: 0x06001756 RID: 5974
		StyleEnum<Align> alignSelf { get; set; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001757 RID: 5975
		// (set) Token: 0x06001758 RID: 5976
		StyleColor backgroundColor { get; set; }

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001759 RID: 5977
		// (set) Token: 0x0600175A RID: 5978
		StyleBackground backgroundImage { get; set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600175B RID: 5979
		// (set) Token: 0x0600175C RID: 5980
		StyleBackgroundPosition backgroundPositionX { get; set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600175D RID: 5981
		// (set) Token: 0x0600175E RID: 5982
		StyleBackgroundPosition backgroundPositionY { get; set; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600175F RID: 5983
		// (set) Token: 0x06001760 RID: 5984
		StyleBackgroundRepeat backgroundRepeat { get; set; }

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001761 RID: 5985
		// (set) Token: 0x06001762 RID: 5986
		StyleBackgroundSize backgroundSize { get; set; }

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001763 RID: 5987
		// (set) Token: 0x06001764 RID: 5988
		StyleColor borderBottomColor { get; set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001765 RID: 5989
		// (set) Token: 0x06001766 RID: 5990
		StyleLength borderBottomLeftRadius { get; set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001767 RID: 5991
		// (set) Token: 0x06001768 RID: 5992
		StyleLength borderBottomRightRadius { get; set; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001769 RID: 5993
		// (set) Token: 0x0600176A RID: 5994
		StyleFloat borderBottomWidth { get; set; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x0600176B RID: 5995
		// (set) Token: 0x0600176C RID: 5996
		StyleColor borderLeftColor { get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600176D RID: 5997
		// (set) Token: 0x0600176E RID: 5998
		StyleFloat borderLeftWidth { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600176F RID: 5999
		// (set) Token: 0x06001770 RID: 6000
		StyleColor borderRightColor { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001771 RID: 6001
		// (set) Token: 0x06001772 RID: 6002
		StyleFloat borderRightWidth { get; set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001773 RID: 6003
		// (set) Token: 0x06001774 RID: 6004
		StyleColor borderTopColor { get; set; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001775 RID: 6005
		// (set) Token: 0x06001776 RID: 6006
		StyleLength borderTopLeftRadius { get; set; }

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001777 RID: 6007
		// (set) Token: 0x06001778 RID: 6008
		StyleLength borderTopRightRadius { get; set; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001779 RID: 6009
		// (set) Token: 0x0600177A RID: 6010
		StyleFloat borderTopWidth { get; set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600177B RID: 6011
		// (set) Token: 0x0600177C RID: 6012
		StyleLength bottom { get; set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600177D RID: 6013
		// (set) Token: 0x0600177E RID: 6014
		StyleColor color { get; set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600177F RID: 6015
		// (set) Token: 0x06001780 RID: 6016
		StyleCursor cursor { get; set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001781 RID: 6017
		// (set) Token: 0x06001782 RID: 6018
		StyleEnum<DisplayStyle> display { get; set; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001783 RID: 6019
		// (set) Token: 0x06001784 RID: 6020
		StyleLength flexBasis { get; set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001785 RID: 6021
		// (set) Token: 0x06001786 RID: 6022
		StyleEnum<FlexDirection> flexDirection { get; set; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001787 RID: 6023
		// (set) Token: 0x06001788 RID: 6024
		StyleFloat flexGrow { get; set; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001789 RID: 6025
		// (set) Token: 0x0600178A RID: 6026
		StyleFloat flexShrink { get; set; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x0600178B RID: 6027
		// (set) Token: 0x0600178C RID: 6028
		StyleEnum<Wrap> flexWrap { get; set; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x0600178D RID: 6029
		// (set) Token: 0x0600178E RID: 6030
		StyleLength fontSize { get; set; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600178F RID: 6031
		// (set) Token: 0x06001790 RID: 6032
		StyleLength height { get; set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001791 RID: 6033
		// (set) Token: 0x06001792 RID: 6034
		StyleEnum<Justify> justifyContent { get; set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001793 RID: 6035
		// (set) Token: 0x06001794 RID: 6036
		StyleLength left { get; set; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001795 RID: 6037
		// (set) Token: 0x06001796 RID: 6038
		StyleLength letterSpacing { get; set; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001797 RID: 6039
		// (set) Token: 0x06001798 RID: 6040
		StyleLength marginBottom { get; set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001799 RID: 6041
		// (set) Token: 0x0600179A RID: 6042
		StyleLength marginLeft { get; set; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600179B RID: 6043
		// (set) Token: 0x0600179C RID: 6044
		StyleLength marginRight { get; set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600179D RID: 6045
		// (set) Token: 0x0600179E RID: 6046
		StyleLength marginTop { get; set; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600179F RID: 6047
		// (set) Token: 0x060017A0 RID: 6048
		StyleLength maxHeight { get; set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060017A1 RID: 6049
		// (set) Token: 0x060017A2 RID: 6050
		StyleLength maxWidth { get; set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060017A3 RID: 6051
		// (set) Token: 0x060017A4 RID: 6052
		StyleLength minHeight { get; set; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060017A5 RID: 6053
		// (set) Token: 0x060017A6 RID: 6054
		StyleLength minWidth { get; set; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060017A7 RID: 6055
		// (set) Token: 0x060017A8 RID: 6056
		StyleFloat opacity { get; set; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060017A9 RID: 6057
		// (set) Token: 0x060017AA RID: 6058
		StyleEnum<Overflow> overflow { get; set; }

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060017AB RID: 6059
		// (set) Token: 0x060017AC RID: 6060
		StyleLength paddingBottom { get; set; }

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060017AD RID: 6061
		// (set) Token: 0x060017AE RID: 6062
		StyleLength paddingLeft { get; set; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060017AF RID: 6063
		// (set) Token: 0x060017B0 RID: 6064
		StyleLength paddingRight { get; set; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060017B1 RID: 6065
		// (set) Token: 0x060017B2 RID: 6066
		StyleLength paddingTop { get; set; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060017B3 RID: 6067
		// (set) Token: 0x060017B4 RID: 6068
		StyleEnum<Position> position { get; set; }

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060017B5 RID: 6069
		// (set) Token: 0x060017B6 RID: 6070
		StyleLength right { get; set; }

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060017B7 RID: 6071
		// (set) Token: 0x060017B8 RID: 6072
		StyleRotate rotate { get; set; }

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060017B9 RID: 6073
		// (set) Token: 0x060017BA RID: 6074
		StyleScale scale { get; set; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060017BB RID: 6075
		// (set) Token: 0x060017BC RID: 6076
		StyleEnum<TextOverflow> textOverflow { get; set; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060017BD RID: 6077
		// (set) Token: 0x060017BE RID: 6078
		StyleTextShadow textShadow { get; set; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060017BF RID: 6079
		// (set) Token: 0x060017C0 RID: 6080
		StyleLength top { get; set; }

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060017C1 RID: 6081
		// (set) Token: 0x060017C2 RID: 6082
		StyleTransformOrigin transformOrigin { get; set; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060017C3 RID: 6083
		// (set) Token: 0x060017C4 RID: 6084
		StyleList<TimeValue> transitionDelay { get; set; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060017C5 RID: 6085
		// (set) Token: 0x060017C6 RID: 6086
		StyleList<TimeValue> transitionDuration { get; set; }

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060017C7 RID: 6087
		// (set) Token: 0x060017C8 RID: 6088
		StyleList<StylePropertyName> transitionProperty { get; set; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060017C9 RID: 6089
		// (set) Token: 0x060017CA RID: 6090
		StyleList<EasingFunction> transitionTimingFunction { get; set; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060017CB RID: 6091
		// (set) Token: 0x060017CC RID: 6092
		StyleTranslate translate { get; set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060017CD RID: 6093
		// (set) Token: 0x060017CE RID: 6094
		StyleColor unityBackgroundImageTintColor { get; set; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060017CF RID: 6095
		// (set) Token: 0x060017D0 RID: 6096
		StyleEnum<EditorTextRenderingMode> unityEditorTextRenderingMode { get; set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060017D1 RID: 6097
		// (set) Token: 0x060017D2 RID: 6098
		StyleFont unityFont { get; set; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060017D3 RID: 6099
		// (set) Token: 0x060017D4 RID: 6100
		StyleFontDefinition unityFontDefinition { get; set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060017D5 RID: 6101
		// (set) Token: 0x060017D6 RID: 6102
		StyleEnum<FontStyle> unityFontStyleAndWeight { get; set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060017D7 RID: 6103
		// (set) Token: 0x060017D8 RID: 6104
		StyleEnum<OverflowClipBox> unityOverflowClipBox { get; set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060017D9 RID: 6105
		// (set) Token: 0x060017DA RID: 6106
		StyleLength unityParagraphSpacing { get; set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060017DB RID: 6107
		// (set) Token: 0x060017DC RID: 6108
		StyleInt unitySliceBottom { get; set; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060017DD RID: 6109
		// (set) Token: 0x060017DE RID: 6110
		StyleInt unitySliceLeft { get; set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060017DF RID: 6111
		// (set) Token: 0x060017E0 RID: 6112
		StyleInt unitySliceRight { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060017E1 RID: 6113
		// (set) Token: 0x060017E2 RID: 6114
		StyleFloat unitySliceScale { get; set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060017E3 RID: 6115
		// (set) Token: 0x060017E4 RID: 6116
		StyleInt unitySliceTop { get; set; }

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060017E5 RID: 6117
		// (set) Token: 0x060017E6 RID: 6118
		StyleEnum<TextAnchor> unityTextAlign { get; set; }

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060017E7 RID: 6119
		// (set) Token: 0x060017E8 RID: 6120
		StyleEnum<TextGeneratorType> unityTextGenerator { get; set; }

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060017E9 RID: 6121
		// (set) Token: 0x060017EA RID: 6122
		StyleColor unityTextOutlineColor { get; set; }

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060017EB RID: 6123
		// (set) Token: 0x060017EC RID: 6124
		StyleFloat unityTextOutlineWidth { get; set; }

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060017ED RID: 6125
		// (set) Token: 0x060017EE RID: 6126
		StyleEnum<TextOverflowPosition> unityTextOverflowPosition { get; set; }

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060017EF RID: 6127
		// (set) Token: 0x060017F0 RID: 6128
		StyleEnum<Visibility> visibility { get; set; }

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060017F1 RID: 6129
		// (set) Token: 0x060017F2 RID: 6130
		StyleEnum<WhiteSpace> whiteSpace { get; set; }

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060017F3 RID: 6131
		// (set) Token: 0x060017F4 RID: 6132
		StyleLength width { get; set; }

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060017F5 RID: 6133
		// (set) Token: 0x060017F6 RID: 6134
		StyleLength wordSpacing { get; set; }
	}
}
