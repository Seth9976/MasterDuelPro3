using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D5 RID: 725
	internal class InlineStyleAccessPropertyBag : PropertyBag<InlineStyleAccess>, INamedProperties<InlineStyleAccess>
	{
		// Token: 0x060014D4 RID: 5332 RVA: 0x0005E8D0 File Offset: 0x0005CAD0
		public InlineStyleAccessPropertyBag()
		{
			this.m_PropertiesList = new List<IProperty<InlineStyleAccess>>(83);
			this.m_PropertiesHash = new Dictionary<string, IProperty<InlineStyleAccess>>(249);
			this.AddProperty<StyleEnum<Align>, Align>(new InlineStyleAccessPropertyBag.AlignContentProperty());
			this.AddProperty<StyleEnum<Align>, Align>(new InlineStyleAccessPropertyBag.AlignItemsProperty());
			this.AddProperty<StyleEnum<Align>, Align>(new InlineStyleAccessPropertyBag.AlignSelfProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.BackgroundColorProperty());
			this.AddProperty<StyleBackground, Background>(new InlineStyleAccessPropertyBag.BackgroundImageProperty());
			this.AddProperty<StyleBackgroundPosition, BackgroundPosition>(new InlineStyleAccessPropertyBag.BackgroundPositionXProperty());
			this.AddProperty<StyleBackgroundPosition, BackgroundPosition>(new InlineStyleAccessPropertyBag.BackgroundPositionYProperty());
			this.AddProperty<StyleBackgroundRepeat, BackgroundRepeat>(new InlineStyleAccessPropertyBag.BackgroundRepeatProperty());
			this.AddProperty<StyleBackgroundSize, BackgroundSize>(new InlineStyleAccessPropertyBag.BackgroundSizeProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.BorderBottomColorProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.BorderBottomLeftRadiusProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.BorderBottomRightRadiusProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.BorderBottomWidthProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.BorderLeftColorProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.BorderLeftWidthProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.BorderRightColorProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.BorderRightWidthProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.BorderTopColorProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.BorderTopLeftRadiusProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.BorderTopRightRadiusProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.BorderTopWidthProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.BottomProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.ColorProperty());
			this.AddProperty<StyleCursor, Cursor>(new InlineStyleAccessPropertyBag.CursorProperty());
			this.AddProperty<StyleEnum<DisplayStyle>, DisplayStyle>(new InlineStyleAccessPropertyBag.DisplayProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.FlexBasisProperty());
			this.AddProperty<StyleEnum<FlexDirection>, FlexDirection>(new InlineStyleAccessPropertyBag.FlexDirectionProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.FlexGrowProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.FlexShrinkProperty());
			this.AddProperty<StyleEnum<Wrap>, Wrap>(new InlineStyleAccessPropertyBag.FlexWrapProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.FontSizeProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.HeightProperty());
			this.AddProperty<StyleEnum<Justify>, Justify>(new InlineStyleAccessPropertyBag.JustifyContentProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.LeftProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.LetterSpacingProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MarginBottomProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MarginLeftProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MarginRightProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MarginTopProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MaxHeightProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MaxWidthProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MinHeightProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MinWidthProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.OpacityProperty());
			this.AddProperty<StyleEnum<Overflow>, Overflow>(new InlineStyleAccessPropertyBag.OverflowProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.PaddingBottomProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.PaddingLeftProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.PaddingRightProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.PaddingTopProperty());
			this.AddProperty<StyleEnum<Position>, Position>(new InlineStyleAccessPropertyBag.PositionProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.RightProperty());
			this.AddProperty<StyleRotate, Rotate>(new InlineStyleAccessPropertyBag.RotateProperty());
			this.AddProperty<StyleScale, Scale>(new InlineStyleAccessPropertyBag.ScaleProperty());
			this.AddProperty<StyleEnum<TextOverflow>, TextOverflow>(new InlineStyleAccessPropertyBag.TextOverflowProperty());
			this.AddProperty<StyleTextShadow, TextShadow>(new InlineStyleAccessPropertyBag.TextShadowProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.TopProperty());
			this.AddProperty<StyleTransformOrigin, TransformOrigin>(new InlineStyleAccessPropertyBag.TransformOriginProperty());
			this.AddProperty<StyleList<TimeValue>, List<TimeValue>>(new InlineStyleAccessPropertyBag.TransitionDelayProperty());
			this.AddProperty<StyleList<TimeValue>, List<TimeValue>>(new InlineStyleAccessPropertyBag.TransitionDurationProperty());
			this.AddProperty<StyleList<StylePropertyName>, List<StylePropertyName>>(new InlineStyleAccessPropertyBag.TransitionPropertyProperty());
			this.AddProperty<StyleList<EasingFunction>, List<EasingFunction>>(new InlineStyleAccessPropertyBag.TransitionTimingFunctionProperty());
			this.AddProperty<StyleTranslate, Translate>(new InlineStyleAccessPropertyBag.TranslateProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.UnityBackgroundImageTintColorProperty());
			this.AddProperty<StyleEnum<EditorTextRenderingMode>, EditorTextRenderingMode>(new InlineStyleAccessPropertyBag.UnityEditorTextRenderingModeProperty());
			this.AddProperty<StyleFont, Font>(new InlineStyleAccessPropertyBag.UnityFontProperty());
			this.AddProperty<StyleFontDefinition, FontDefinition>(new InlineStyleAccessPropertyBag.UnityFontDefinitionProperty());
			this.AddProperty<StyleEnum<FontStyle>, FontStyle>(new InlineStyleAccessPropertyBag.UnityFontStyleAndWeightProperty());
			this.AddProperty<StyleEnum<OverflowClipBox>, OverflowClipBox>(new InlineStyleAccessPropertyBag.UnityOverflowClipBoxProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.UnityParagraphSpacingProperty());
			this.AddProperty<StyleInt, int>(new InlineStyleAccessPropertyBag.UnitySliceBottomProperty());
			this.AddProperty<StyleInt, int>(new InlineStyleAccessPropertyBag.UnitySliceLeftProperty());
			this.AddProperty<StyleInt, int>(new InlineStyleAccessPropertyBag.UnitySliceRightProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.UnitySliceScaleProperty());
			this.AddProperty<StyleInt, int>(new InlineStyleAccessPropertyBag.UnitySliceTopProperty());
			this.AddProperty<StyleEnum<TextAnchor>, TextAnchor>(new InlineStyleAccessPropertyBag.UnityTextAlignProperty());
			this.AddProperty<StyleEnum<TextGeneratorType>, TextGeneratorType>(new InlineStyleAccessPropertyBag.UnityTextGeneratorProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.UnityTextOutlineColorProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.UnityTextOutlineWidthProperty());
			this.AddProperty<StyleEnum<TextOverflowPosition>, TextOverflowPosition>(new InlineStyleAccessPropertyBag.UnityTextOverflowPositionProperty());
			this.AddProperty<StyleEnum<Visibility>, Visibility>(new InlineStyleAccessPropertyBag.VisibilityProperty());
			this.AddProperty<StyleEnum<WhiteSpace>, WhiteSpace>(new InlineStyleAccessPropertyBag.WhiteSpaceProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.WidthProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.WordSpacingProperty());
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0005ECE8 File Offset: 0x0005CEE8
		private void AddProperty<TStyleValue, TValue>(InlineStyleAccessPropertyBag.InlineStyleProperty<TStyleValue, TValue> property) where TStyleValue : IStyleValue<TValue>, new()
		{
			this.m_PropertiesList.Add(property);
			this.m_PropertiesHash.Add(property.Name, property);
			bool flag = string.CompareOrdinal(property.Name, property.ussName) != 0;
			if (flag)
			{
				this.m_PropertiesHash.Add(property.ussName, property);
			}
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0005ED41 File Offset: 0x0005CF41
		public override PropertyCollection<InlineStyleAccess> GetProperties()
		{
			return new PropertyCollection<InlineStyleAccess>(this.m_PropertiesList);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0005ED41 File Offset: 0x0005CF41
		public override PropertyCollection<InlineStyleAccess> GetProperties(ref InlineStyleAccess container)
		{
			return new PropertyCollection<InlineStyleAccess>(this.m_PropertiesList);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0005ED4E File Offset: 0x0005CF4E
		public bool TryGetProperty(ref InlineStyleAccess container, string name, out IProperty<InlineStyleAccess> property)
		{
			return this.m_PropertiesHash.TryGetValue(name, out property);
		}

		// Token: 0x04000B61 RID: 2913
		private readonly List<IProperty<InlineStyleAccess>> m_PropertiesList;

		// Token: 0x04000B62 RID: 2914
		private readonly Dictionary<string, IProperty<InlineStyleAccess>> m_PropertiesHash;

		// Token: 0x020002D6 RID: 726
		private class AlignContentProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Align>
		{
			// Token: 0x17000464 RID: 1124
			// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0005ED5D File Offset: 0x0005CF5D
			public override string Name
			{
				get
				{
					return "alignContent";
				}
			}

			// Token: 0x17000465 RID: 1125
			// (get) Token: 0x060014DA RID: 5338 RVA: 0x0005ED64 File Offset: 0x0005CF64
			public override string ussName
			{
				get
				{
					return "align-content";
				}
			}

			// Token: 0x17000466 RID: 1126
			// (get) Token: 0x060014DB RID: 5339 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060014DC RID: 5340 RVA: 0x0005ED6B File Offset: 0x0005CF6B
			public override StyleEnum<Align> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).alignContent;
			}

			// Token: 0x060014DD RID: 5341 RVA: 0x0005ED74 File Offset: 0x0005CF74
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Align> value)
			{
				((IStyle)container).alignContent = value;
			}
		}

		// Token: 0x020002D7 RID: 727
		private class AlignItemsProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Align>
		{
			// Token: 0x17000467 RID: 1127
			// (get) Token: 0x060014DF RID: 5343 RVA: 0x0005ED88 File Offset: 0x0005CF88
			public override string Name
			{
				get
				{
					return "alignItems";
				}
			}

			// Token: 0x17000468 RID: 1128
			// (get) Token: 0x060014E0 RID: 5344 RVA: 0x0005ED8F File Offset: 0x0005CF8F
			public override string ussName
			{
				get
				{
					return "align-items";
				}
			}

			// Token: 0x17000469 RID: 1129
			// (get) Token: 0x060014E1 RID: 5345 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060014E2 RID: 5346 RVA: 0x0005ED96 File Offset: 0x0005CF96
			public override StyleEnum<Align> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).alignItems;
			}

			// Token: 0x060014E3 RID: 5347 RVA: 0x0005ED9F File Offset: 0x0005CF9F
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Align> value)
			{
				((IStyle)container).alignItems = value;
			}
		}

		// Token: 0x020002D8 RID: 728
		private class AlignSelfProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Align>
		{
			// Token: 0x1700046A RID: 1130
			// (get) Token: 0x060014E5 RID: 5349 RVA: 0x0005EDAA File Offset: 0x0005CFAA
			public override string Name
			{
				get
				{
					return "alignSelf";
				}
			}

			// Token: 0x1700046B RID: 1131
			// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0005EDB1 File Offset: 0x0005CFB1
			public override string ussName
			{
				get
				{
					return "align-self";
				}
			}

			// Token: 0x1700046C RID: 1132
			// (get) Token: 0x060014E7 RID: 5351 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060014E8 RID: 5352 RVA: 0x0005EDB8 File Offset: 0x0005CFB8
			public override StyleEnum<Align> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).alignSelf;
			}

			// Token: 0x060014E9 RID: 5353 RVA: 0x0005EDC1 File Offset: 0x0005CFC1
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Align> value)
			{
				((IStyle)container).alignSelf = value;
			}
		}

		// Token: 0x020002D9 RID: 729
		private class BackgroundColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x1700046D RID: 1133
			// (get) Token: 0x060014EB RID: 5355 RVA: 0x0005EDCC File Offset: 0x0005CFCC
			public override string Name
			{
				get
				{
					return "backgroundColor";
				}
			}

			// Token: 0x1700046E RID: 1134
			// (get) Token: 0x060014EC RID: 5356 RVA: 0x0005EDD3 File Offset: 0x0005CFD3
			public override string ussName
			{
				get
				{
					return "background-color";
				}
			}

			// Token: 0x1700046F RID: 1135
			// (get) Token: 0x060014ED RID: 5357 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060014EE RID: 5358 RVA: 0x0005EDDA File Offset: 0x0005CFDA
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).backgroundColor;
			}

			// Token: 0x060014EF RID: 5359 RVA: 0x0005EDE3 File Offset: 0x0005CFE3
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).backgroundColor = value;
			}
		}

		// Token: 0x020002DA RID: 730
		private class BackgroundImageProperty : InlineStyleAccessPropertyBag.InlineStyleBackgroundProperty
		{
			// Token: 0x17000470 RID: 1136
			// (get) Token: 0x060014F1 RID: 5361 RVA: 0x0005EDF7 File Offset: 0x0005CFF7
			public override string Name
			{
				get
				{
					return "backgroundImage";
				}
			}

			// Token: 0x17000471 RID: 1137
			// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0005EDFE File Offset: 0x0005CFFE
			public override string ussName
			{
				get
				{
					return "background-image";
				}
			}

			// Token: 0x17000472 RID: 1138
			// (get) Token: 0x060014F3 RID: 5363 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060014F4 RID: 5364 RVA: 0x0005EE05 File Offset: 0x0005D005
			public override StyleBackground GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).backgroundImage;
			}

			// Token: 0x060014F5 RID: 5365 RVA: 0x0005EE0E File Offset: 0x0005D00E
			public override void SetValue(ref InlineStyleAccess container, StyleBackground value)
			{
				((IStyle)container).backgroundImage = value;
			}
		}

		// Token: 0x020002DB RID: 731
		private class BackgroundPositionXProperty : InlineStyleAccessPropertyBag.InlineStyleBackgroundPositionProperty
		{
			// Token: 0x17000473 RID: 1139
			// (get) Token: 0x060014F7 RID: 5367 RVA: 0x0005EE22 File Offset: 0x0005D022
			public override string Name
			{
				get
				{
					return "backgroundPositionX";
				}
			}

			// Token: 0x17000474 RID: 1140
			// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0005EE29 File Offset: 0x0005D029
			public override string ussName
			{
				get
				{
					return "background-position-x";
				}
			}

			// Token: 0x17000475 RID: 1141
			// (get) Token: 0x060014F9 RID: 5369 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060014FA RID: 5370 RVA: 0x0005EE30 File Offset: 0x0005D030
			public override StyleBackgroundPosition GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).backgroundPositionX;
			}

			// Token: 0x060014FB RID: 5371 RVA: 0x0005EE39 File Offset: 0x0005D039
			public override void SetValue(ref InlineStyleAccess container, StyleBackgroundPosition value)
			{
				((IStyle)container).backgroundPositionX = value;
			}
		}

		// Token: 0x020002DC RID: 732
		private class BackgroundPositionYProperty : InlineStyleAccessPropertyBag.InlineStyleBackgroundPositionProperty
		{
			// Token: 0x17000476 RID: 1142
			// (get) Token: 0x060014FD RID: 5373 RVA: 0x0005EE4D File Offset: 0x0005D04D
			public override string Name
			{
				get
				{
					return "backgroundPositionY";
				}
			}

			// Token: 0x17000477 RID: 1143
			// (get) Token: 0x060014FE RID: 5374 RVA: 0x0005EE54 File Offset: 0x0005D054
			public override string ussName
			{
				get
				{
					return "background-position-y";
				}
			}

			// Token: 0x17000478 RID: 1144
			// (get) Token: 0x060014FF RID: 5375 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001500 RID: 5376 RVA: 0x0005EE5B File Offset: 0x0005D05B
			public override StyleBackgroundPosition GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).backgroundPositionY;
			}

			// Token: 0x06001501 RID: 5377 RVA: 0x0005EE64 File Offset: 0x0005D064
			public override void SetValue(ref InlineStyleAccess container, StyleBackgroundPosition value)
			{
				((IStyle)container).backgroundPositionY = value;
			}
		}

		// Token: 0x020002DD RID: 733
		private class BackgroundRepeatProperty : InlineStyleAccessPropertyBag.InlineStyleBackgroundRepeatProperty
		{
			// Token: 0x17000479 RID: 1145
			// (get) Token: 0x06001503 RID: 5379 RVA: 0x0005EE6F File Offset: 0x0005D06F
			public override string Name
			{
				get
				{
					return "backgroundRepeat";
				}
			}

			// Token: 0x1700047A RID: 1146
			// (get) Token: 0x06001504 RID: 5380 RVA: 0x0005EE76 File Offset: 0x0005D076
			public override string ussName
			{
				get
				{
					return "background-repeat";
				}
			}

			// Token: 0x1700047B RID: 1147
			// (get) Token: 0x06001505 RID: 5381 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001506 RID: 5382 RVA: 0x0005EE7D File Offset: 0x0005D07D
			public override StyleBackgroundRepeat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).backgroundRepeat;
			}

			// Token: 0x06001507 RID: 5383 RVA: 0x0005EE86 File Offset: 0x0005D086
			public override void SetValue(ref InlineStyleAccess container, StyleBackgroundRepeat value)
			{
				((IStyle)container).backgroundRepeat = value;
			}
		}

		// Token: 0x020002DE RID: 734
		private class BackgroundSizeProperty : InlineStyleAccessPropertyBag.InlineStyleBackgroundSizeProperty
		{
			// Token: 0x1700047C RID: 1148
			// (get) Token: 0x06001509 RID: 5385 RVA: 0x0005EE9A File Offset: 0x0005D09A
			public override string Name
			{
				get
				{
					return "backgroundSize";
				}
			}

			// Token: 0x1700047D RID: 1149
			// (get) Token: 0x0600150A RID: 5386 RVA: 0x0005EEA1 File Offset: 0x0005D0A1
			public override string ussName
			{
				get
				{
					return "background-size";
				}
			}

			// Token: 0x1700047E RID: 1150
			// (get) Token: 0x0600150B RID: 5387 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600150C RID: 5388 RVA: 0x0005EEA8 File Offset: 0x0005D0A8
			public override StyleBackgroundSize GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).backgroundSize;
			}

			// Token: 0x0600150D RID: 5389 RVA: 0x0005EEB1 File Offset: 0x0005D0B1
			public override void SetValue(ref InlineStyleAccess container, StyleBackgroundSize value)
			{
				((IStyle)container).backgroundSize = value;
			}
		}

		// Token: 0x020002DF RID: 735
		private class BorderBottomColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x1700047F RID: 1151
			// (get) Token: 0x0600150F RID: 5391 RVA: 0x0005EEC5 File Offset: 0x0005D0C5
			public override string Name
			{
				get
				{
					return "borderBottomColor";
				}
			}

			// Token: 0x17000480 RID: 1152
			// (get) Token: 0x06001510 RID: 5392 RVA: 0x0005EECC File Offset: 0x0005D0CC
			public override string ussName
			{
				get
				{
					return "border-bottom-color";
				}
			}

			// Token: 0x17000481 RID: 1153
			// (get) Token: 0x06001511 RID: 5393 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001512 RID: 5394 RVA: 0x0005EED3 File Offset: 0x0005D0D3
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderBottomColor;
			}

			// Token: 0x06001513 RID: 5395 RVA: 0x0005EEDC File Offset: 0x0005D0DC
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).borderBottomColor = value;
			}
		}

		// Token: 0x020002E0 RID: 736
		private class BorderBottomLeftRadiusProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x17000482 RID: 1154
			// (get) Token: 0x06001515 RID: 5397 RVA: 0x0005EEE7 File Offset: 0x0005D0E7
			public override string Name
			{
				get
				{
					return "borderBottomLeftRadius";
				}
			}

			// Token: 0x17000483 RID: 1155
			// (get) Token: 0x06001516 RID: 5398 RVA: 0x0005EEEE File Offset: 0x0005D0EE
			public override string ussName
			{
				get
				{
					return "border-bottom-left-radius";
				}
			}

			// Token: 0x17000484 RID: 1156
			// (get) Token: 0x06001517 RID: 5399 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001518 RID: 5400 RVA: 0x0005EEF5 File Offset: 0x0005D0F5
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderBottomLeftRadius;
			}

			// Token: 0x06001519 RID: 5401 RVA: 0x0005EEFE File Offset: 0x0005D0FE
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).borderBottomLeftRadius = value;
			}
		}

		// Token: 0x020002E1 RID: 737
		private class BorderBottomRightRadiusProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x17000485 RID: 1157
			// (get) Token: 0x0600151B RID: 5403 RVA: 0x0005EF12 File Offset: 0x0005D112
			public override string Name
			{
				get
				{
					return "borderBottomRightRadius";
				}
			}

			// Token: 0x17000486 RID: 1158
			// (get) Token: 0x0600151C RID: 5404 RVA: 0x0005EF19 File Offset: 0x0005D119
			public override string ussName
			{
				get
				{
					return "border-bottom-right-radius";
				}
			}

			// Token: 0x17000487 RID: 1159
			// (get) Token: 0x0600151D RID: 5405 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600151E RID: 5406 RVA: 0x0005EF20 File Offset: 0x0005D120
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderBottomRightRadius;
			}

			// Token: 0x0600151F RID: 5407 RVA: 0x0005EF29 File Offset: 0x0005D129
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).borderBottomRightRadius = value;
			}
		}

		// Token: 0x020002E2 RID: 738
		private class BorderBottomWidthProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x17000488 RID: 1160
			// (get) Token: 0x06001521 RID: 5409 RVA: 0x0005EF34 File Offset: 0x0005D134
			public override string Name
			{
				get
				{
					return "borderBottomWidth";
				}
			}

			// Token: 0x17000489 RID: 1161
			// (get) Token: 0x06001522 RID: 5410 RVA: 0x0005EF3B File Offset: 0x0005D13B
			public override string ussName
			{
				get
				{
					return "border-bottom-width";
				}
			}

			// Token: 0x1700048A RID: 1162
			// (get) Token: 0x06001523 RID: 5411 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001524 RID: 5412 RVA: 0x0005EF42 File Offset: 0x0005D142
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderBottomWidth;
			}

			// Token: 0x06001525 RID: 5413 RVA: 0x0005EF4B File Offset: 0x0005D14B
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).borderBottomWidth = value;
			}
		}

		// Token: 0x020002E3 RID: 739
		private class BorderLeftColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x1700048B RID: 1163
			// (get) Token: 0x06001527 RID: 5415 RVA: 0x0005EF5F File Offset: 0x0005D15F
			public override string Name
			{
				get
				{
					return "borderLeftColor";
				}
			}

			// Token: 0x1700048C RID: 1164
			// (get) Token: 0x06001528 RID: 5416 RVA: 0x0005EF66 File Offset: 0x0005D166
			public override string ussName
			{
				get
				{
					return "border-left-color";
				}
			}

			// Token: 0x1700048D RID: 1165
			// (get) Token: 0x06001529 RID: 5417 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600152A RID: 5418 RVA: 0x0005EF6D File Offset: 0x0005D16D
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderLeftColor;
			}

			// Token: 0x0600152B RID: 5419 RVA: 0x0005EF76 File Offset: 0x0005D176
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).borderLeftColor = value;
			}
		}

		// Token: 0x020002E4 RID: 740
		private class BorderLeftWidthProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x1700048E RID: 1166
			// (get) Token: 0x0600152D RID: 5421 RVA: 0x0005EF81 File Offset: 0x0005D181
			public override string Name
			{
				get
				{
					return "borderLeftWidth";
				}
			}

			// Token: 0x1700048F RID: 1167
			// (get) Token: 0x0600152E RID: 5422 RVA: 0x0005EF88 File Offset: 0x0005D188
			public override string ussName
			{
				get
				{
					return "border-left-width";
				}
			}

			// Token: 0x17000490 RID: 1168
			// (get) Token: 0x0600152F RID: 5423 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001530 RID: 5424 RVA: 0x0005EF8F File Offset: 0x0005D18F
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderLeftWidth;
			}

			// Token: 0x06001531 RID: 5425 RVA: 0x0005EF98 File Offset: 0x0005D198
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).borderLeftWidth = value;
			}
		}

		// Token: 0x020002E5 RID: 741
		private class BorderRightColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x17000491 RID: 1169
			// (get) Token: 0x06001533 RID: 5427 RVA: 0x0005EFA3 File Offset: 0x0005D1A3
			public override string Name
			{
				get
				{
					return "borderRightColor";
				}
			}

			// Token: 0x17000492 RID: 1170
			// (get) Token: 0x06001534 RID: 5428 RVA: 0x0005EFAA File Offset: 0x0005D1AA
			public override string ussName
			{
				get
				{
					return "border-right-color";
				}
			}

			// Token: 0x17000493 RID: 1171
			// (get) Token: 0x06001535 RID: 5429 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001536 RID: 5430 RVA: 0x0005EFB1 File Offset: 0x0005D1B1
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderRightColor;
			}

			// Token: 0x06001537 RID: 5431 RVA: 0x0005EFBA File Offset: 0x0005D1BA
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).borderRightColor = value;
			}
		}

		// Token: 0x020002E6 RID: 742
		private class BorderRightWidthProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x17000494 RID: 1172
			// (get) Token: 0x06001539 RID: 5433 RVA: 0x0005EFC5 File Offset: 0x0005D1C5
			public override string Name
			{
				get
				{
					return "borderRightWidth";
				}
			}

			// Token: 0x17000495 RID: 1173
			// (get) Token: 0x0600153A RID: 5434 RVA: 0x0005EFCC File Offset: 0x0005D1CC
			public override string ussName
			{
				get
				{
					return "border-right-width";
				}
			}

			// Token: 0x17000496 RID: 1174
			// (get) Token: 0x0600153B RID: 5435 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600153C RID: 5436 RVA: 0x0005EFD3 File Offset: 0x0005D1D3
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderRightWidth;
			}

			// Token: 0x0600153D RID: 5437 RVA: 0x0005EFDC File Offset: 0x0005D1DC
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).borderRightWidth = value;
			}
		}

		// Token: 0x020002E7 RID: 743
		private class BorderTopColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x17000497 RID: 1175
			// (get) Token: 0x0600153F RID: 5439 RVA: 0x0005EFE7 File Offset: 0x0005D1E7
			public override string Name
			{
				get
				{
					return "borderTopColor";
				}
			}

			// Token: 0x17000498 RID: 1176
			// (get) Token: 0x06001540 RID: 5440 RVA: 0x0005EFEE File Offset: 0x0005D1EE
			public override string ussName
			{
				get
				{
					return "border-top-color";
				}
			}

			// Token: 0x17000499 RID: 1177
			// (get) Token: 0x06001541 RID: 5441 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001542 RID: 5442 RVA: 0x0005EFF5 File Offset: 0x0005D1F5
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderTopColor;
			}

			// Token: 0x06001543 RID: 5443 RVA: 0x0005EFFE File Offset: 0x0005D1FE
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).borderTopColor = value;
			}
		}

		// Token: 0x020002E8 RID: 744
		private class BorderTopLeftRadiusProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x1700049A RID: 1178
			// (get) Token: 0x06001545 RID: 5445 RVA: 0x0005F009 File Offset: 0x0005D209
			public override string Name
			{
				get
				{
					return "borderTopLeftRadius";
				}
			}

			// Token: 0x1700049B RID: 1179
			// (get) Token: 0x06001546 RID: 5446 RVA: 0x0005F010 File Offset: 0x0005D210
			public override string ussName
			{
				get
				{
					return "border-top-left-radius";
				}
			}

			// Token: 0x1700049C RID: 1180
			// (get) Token: 0x06001547 RID: 5447 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001548 RID: 5448 RVA: 0x0005F017 File Offset: 0x0005D217
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderTopLeftRadius;
			}

			// Token: 0x06001549 RID: 5449 RVA: 0x0005F020 File Offset: 0x0005D220
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).borderTopLeftRadius = value;
			}
		}

		// Token: 0x020002E9 RID: 745
		private class BorderTopRightRadiusProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x1700049D RID: 1181
			// (get) Token: 0x0600154B RID: 5451 RVA: 0x0005F02B File Offset: 0x0005D22B
			public override string Name
			{
				get
				{
					return "borderTopRightRadius";
				}
			}

			// Token: 0x1700049E RID: 1182
			// (get) Token: 0x0600154C RID: 5452 RVA: 0x0005F032 File Offset: 0x0005D232
			public override string ussName
			{
				get
				{
					return "border-top-right-radius";
				}
			}

			// Token: 0x1700049F RID: 1183
			// (get) Token: 0x0600154D RID: 5453 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600154E RID: 5454 RVA: 0x0005F039 File Offset: 0x0005D239
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderTopRightRadius;
			}

			// Token: 0x0600154F RID: 5455 RVA: 0x0005F042 File Offset: 0x0005D242
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).borderTopRightRadius = value;
			}
		}

		// Token: 0x020002EA RID: 746
		private class BorderTopWidthProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x170004A0 RID: 1184
			// (get) Token: 0x06001551 RID: 5457 RVA: 0x0005F04D File Offset: 0x0005D24D
			public override string Name
			{
				get
				{
					return "borderTopWidth";
				}
			}

			// Token: 0x170004A1 RID: 1185
			// (get) Token: 0x06001552 RID: 5458 RVA: 0x0005F054 File Offset: 0x0005D254
			public override string ussName
			{
				get
				{
					return "border-top-width";
				}
			}

			// Token: 0x170004A2 RID: 1186
			// (get) Token: 0x06001553 RID: 5459 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001554 RID: 5460 RVA: 0x0005F05B File Offset: 0x0005D25B
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderTopWidth;
			}

			// Token: 0x06001555 RID: 5461 RVA: 0x0005F064 File Offset: 0x0005D264
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).borderTopWidth = value;
			}
		}

		// Token: 0x020002EB RID: 747
		private class BottomProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004A3 RID: 1187
			// (get) Token: 0x06001557 RID: 5463 RVA: 0x0005F06F File Offset: 0x0005D26F
			public override string Name
			{
				get
				{
					return "bottom";
				}
			}

			// Token: 0x170004A4 RID: 1188
			// (get) Token: 0x06001558 RID: 5464 RVA: 0x0005F06F File Offset: 0x0005D26F
			public override string ussName
			{
				get
				{
					return "bottom";
				}
			}

			// Token: 0x170004A5 RID: 1189
			// (get) Token: 0x06001559 RID: 5465 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600155A RID: 5466 RVA: 0x0005F076 File Offset: 0x0005D276
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).bottom;
			}

			// Token: 0x0600155B RID: 5467 RVA: 0x0005F07F File Offset: 0x0005D27F
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).bottom = value;
			}
		}

		// Token: 0x020002EC RID: 748
		private class ColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x170004A6 RID: 1190
			// (get) Token: 0x0600155D RID: 5469 RVA: 0x0005F08A File Offset: 0x0005D28A
			public override string Name
			{
				get
				{
					return "color";
				}
			}

			// Token: 0x170004A7 RID: 1191
			// (get) Token: 0x0600155E RID: 5470 RVA: 0x0005F08A File Offset: 0x0005D28A
			public override string ussName
			{
				get
				{
					return "color";
				}
			}

			// Token: 0x170004A8 RID: 1192
			// (get) Token: 0x0600155F RID: 5471 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001560 RID: 5472 RVA: 0x0005F091 File Offset: 0x0005D291
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).color;
			}

			// Token: 0x06001561 RID: 5473 RVA: 0x0005F09A File Offset: 0x0005D29A
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).color = value;
			}
		}

		// Token: 0x020002ED RID: 749
		private class CursorProperty : InlineStyleAccessPropertyBag.InlineStyleCursorProperty
		{
			// Token: 0x170004A9 RID: 1193
			// (get) Token: 0x06001563 RID: 5475 RVA: 0x0005F0A5 File Offset: 0x0005D2A5
			public override string Name
			{
				get
				{
					return "cursor";
				}
			}

			// Token: 0x170004AA RID: 1194
			// (get) Token: 0x06001564 RID: 5476 RVA: 0x0005F0A5 File Offset: 0x0005D2A5
			public override string ussName
			{
				get
				{
					return "cursor";
				}
			}

			// Token: 0x170004AB RID: 1195
			// (get) Token: 0x06001565 RID: 5477 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001566 RID: 5478 RVA: 0x0005F0AC File Offset: 0x0005D2AC
			public override StyleCursor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).cursor;
			}

			// Token: 0x06001567 RID: 5479 RVA: 0x0005F0B5 File Offset: 0x0005D2B5
			public override void SetValue(ref InlineStyleAccess container, StyleCursor value)
			{
				((IStyle)container).cursor = value;
			}
		}

		// Token: 0x020002EE RID: 750
		private class DisplayProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<DisplayStyle>
		{
			// Token: 0x170004AC RID: 1196
			// (get) Token: 0x06001569 RID: 5481 RVA: 0x0005F0C9 File Offset: 0x0005D2C9
			public override string Name
			{
				get
				{
					return "display";
				}
			}

			// Token: 0x170004AD RID: 1197
			// (get) Token: 0x0600156A RID: 5482 RVA: 0x0005F0C9 File Offset: 0x0005D2C9
			public override string ussName
			{
				get
				{
					return "display";
				}
			}

			// Token: 0x170004AE RID: 1198
			// (get) Token: 0x0600156B RID: 5483 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600156C RID: 5484 RVA: 0x0005F0D0 File Offset: 0x0005D2D0
			public override StyleEnum<DisplayStyle> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).display;
			}

			// Token: 0x0600156D RID: 5485 RVA: 0x0005F0D9 File Offset: 0x0005D2D9
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<DisplayStyle> value)
			{
				((IStyle)container).display = value;
			}
		}

		// Token: 0x020002EF RID: 751
		private class FlexBasisProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004AF RID: 1199
			// (get) Token: 0x0600156F RID: 5487 RVA: 0x0005F0ED File Offset: 0x0005D2ED
			public override string Name
			{
				get
				{
					return "flexBasis";
				}
			}

			// Token: 0x170004B0 RID: 1200
			// (get) Token: 0x06001570 RID: 5488 RVA: 0x0005F0F4 File Offset: 0x0005D2F4
			public override string ussName
			{
				get
				{
					return "flex-basis";
				}
			}

			// Token: 0x170004B1 RID: 1201
			// (get) Token: 0x06001571 RID: 5489 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001572 RID: 5490 RVA: 0x0005F0FB File Offset: 0x0005D2FB
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).flexBasis;
			}

			// Token: 0x06001573 RID: 5491 RVA: 0x0005F104 File Offset: 0x0005D304
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).flexBasis = value;
			}
		}

		// Token: 0x020002F0 RID: 752
		private class FlexDirectionProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<FlexDirection>
		{
			// Token: 0x170004B2 RID: 1202
			// (get) Token: 0x06001575 RID: 5493 RVA: 0x0005F10F File Offset: 0x0005D30F
			public override string Name
			{
				get
				{
					return "flexDirection";
				}
			}

			// Token: 0x170004B3 RID: 1203
			// (get) Token: 0x06001576 RID: 5494 RVA: 0x0005F116 File Offset: 0x0005D316
			public override string ussName
			{
				get
				{
					return "flex-direction";
				}
			}

			// Token: 0x170004B4 RID: 1204
			// (get) Token: 0x06001577 RID: 5495 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001578 RID: 5496 RVA: 0x0005F11D File Offset: 0x0005D31D
			public override StyleEnum<FlexDirection> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).flexDirection;
			}

			// Token: 0x06001579 RID: 5497 RVA: 0x0005F126 File Offset: 0x0005D326
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<FlexDirection> value)
			{
				((IStyle)container).flexDirection = value;
			}
		}

		// Token: 0x020002F1 RID: 753
		private class FlexGrowProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x170004B5 RID: 1205
			// (get) Token: 0x0600157B RID: 5499 RVA: 0x0005F13A File Offset: 0x0005D33A
			public override string Name
			{
				get
				{
					return "flexGrow";
				}
			}

			// Token: 0x170004B6 RID: 1206
			// (get) Token: 0x0600157C RID: 5500 RVA: 0x0005F141 File Offset: 0x0005D341
			public override string ussName
			{
				get
				{
					return "flex-grow";
				}
			}

			// Token: 0x170004B7 RID: 1207
			// (get) Token: 0x0600157D RID: 5501 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600157E RID: 5502 RVA: 0x0005F148 File Offset: 0x0005D348
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).flexGrow;
			}

			// Token: 0x0600157F RID: 5503 RVA: 0x0005F151 File Offset: 0x0005D351
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).flexGrow = value;
			}
		}

		// Token: 0x020002F2 RID: 754
		private class FlexShrinkProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x170004B8 RID: 1208
			// (get) Token: 0x06001581 RID: 5505 RVA: 0x0005F15C File Offset: 0x0005D35C
			public override string Name
			{
				get
				{
					return "flexShrink";
				}
			}

			// Token: 0x170004B9 RID: 1209
			// (get) Token: 0x06001582 RID: 5506 RVA: 0x0005F163 File Offset: 0x0005D363
			public override string ussName
			{
				get
				{
					return "flex-shrink";
				}
			}

			// Token: 0x170004BA RID: 1210
			// (get) Token: 0x06001583 RID: 5507 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001584 RID: 5508 RVA: 0x0005F16A File Offset: 0x0005D36A
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).flexShrink;
			}

			// Token: 0x06001585 RID: 5509 RVA: 0x0005F173 File Offset: 0x0005D373
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).flexShrink = value;
			}
		}

		// Token: 0x020002F3 RID: 755
		private class FlexWrapProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Wrap>
		{
			// Token: 0x170004BB RID: 1211
			// (get) Token: 0x06001587 RID: 5511 RVA: 0x0005F17E File Offset: 0x0005D37E
			public override string Name
			{
				get
				{
					return "flexWrap";
				}
			}

			// Token: 0x170004BC RID: 1212
			// (get) Token: 0x06001588 RID: 5512 RVA: 0x0005F185 File Offset: 0x0005D385
			public override string ussName
			{
				get
				{
					return "flex-wrap";
				}
			}

			// Token: 0x170004BD RID: 1213
			// (get) Token: 0x06001589 RID: 5513 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600158A RID: 5514 RVA: 0x0005F18C File Offset: 0x0005D38C
			public override StyleEnum<Wrap> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).flexWrap;
			}

			// Token: 0x0600158B RID: 5515 RVA: 0x0005F195 File Offset: 0x0005D395
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Wrap> value)
			{
				((IStyle)container).flexWrap = value;
			}
		}

		// Token: 0x020002F4 RID: 756
		private class FontSizeProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004BE RID: 1214
			// (get) Token: 0x0600158D RID: 5517 RVA: 0x0005F1A9 File Offset: 0x0005D3A9
			public override string Name
			{
				get
				{
					return "fontSize";
				}
			}

			// Token: 0x170004BF RID: 1215
			// (get) Token: 0x0600158E RID: 5518 RVA: 0x0005F1B0 File Offset: 0x0005D3B0
			public override string ussName
			{
				get
				{
					return "font-size";
				}
			}

			// Token: 0x170004C0 RID: 1216
			// (get) Token: 0x0600158F RID: 5519 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001590 RID: 5520 RVA: 0x0005F1B7 File Offset: 0x0005D3B7
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).fontSize;
			}

			// Token: 0x06001591 RID: 5521 RVA: 0x0005F1C0 File Offset: 0x0005D3C0
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).fontSize = value;
			}
		}

		// Token: 0x020002F5 RID: 757
		private class HeightProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004C1 RID: 1217
			// (get) Token: 0x06001593 RID: 5523 RVA: 0x0005F1CB File Offset: 0x0005D3CB
			public override string Name
			{
				get
				{
					return "height";
				}
			}

			// Token: 0x170004C2 RID: 1218
			// (get) Token: 0x06001594 RID: 5524 RVA: 0x0005F1CB File Offset: 0x0005D3CB
			public override string ussName
			{
				get
				{
					return "height";
				}
			}

			// Token: 0x170004C3 RID: 1219
			// (get) Token: 0x06001595 RID: 5525 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001596 RID: 5526 RVA: 0x0005F1D2 File Offset: 0x0005D3D2
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).height;
			}

			// Token: 0x06001597 RID: 5527 RVA: 0x0005F1DB File Offset: 0x0005D3DB
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).height = value;
			}
		}

		// Token: 0x020002F6 RID: 758
		private class JustifyContentProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Justify>
		{
			// Token: 0x170004C4 RID: 1220
			// (get) Token: 0x06001599 RID: 5529 RVA: 0x0005F1E6 File Offset: 0x0005D3E6
			public override string Name
			{
				get
				{
					return "justifyContent";
				}
			}

			// Token: 0x170004C5 RID: 1221
			// (get) Token: 0x0600159A RID: 5530 RVA: 0x0005F1ED File Offset: 0x0005D3ED
			public override string ussName
			{
				get
				{
					return "justify-content";
				}
			}

			// Token: 0x170004C6 RID: 1222
			// (get) Token: 0x0600159B RID: 5531 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600159C RID: 5532 RVA: 0x0005F1F4 File Offset: 0x0005D3F4
			public override StyleEnum<Justify> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).justifyContent;
			}

			// Token: 0x0600159D RID: 5533 RVA: 0x0005F1FD File Offset: 0x0005D3FD
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Justify> value)
			{
				((IStyle)container).justifyContent = value;
			}
		}

		// Token: 0x020002F7 RID: 759
		private class LeftProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004C7 RID: 1223
			// (get) Token: 0x0600159F RID: 5535 RVA: 0x0005F211 File Offset: 0x0005D411
			public override string Name
			{
				get
				{
					return "left";
				}
			}

			// Token: 0x170004C8 RID: 1224
			// (get) Token: 0x060015A0 RID: 5536 RVA: 0x0005F211 File Offset: 0x0005D411
			public override string ussName
			{
				get
				{
					return "left";
				}
			}

			// Token: 0x170004C9 RID: 1225
			// (get) Token: 0x060015A1 RID: 5537 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015A2 RID: 5538 RVA: 0x0005F218 File Offset: 0x0005D418
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).left;
			}

			// Token: 0x060015A3 RID: 5539 RVA: 0x0005F221 File Offset: 0x0005D421
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).left = value;
			}
		}

		// Token: 0x020002F8 RID: 760
		private class LetterSpacingProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004CA RID: 1226
			// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0005F22C File Offset: 0x0005D42C
			public override string Name
			{
				get
				{
					return "letterSpacing";
				}
			}

			// Token: 0x170004CB RID: 1227
			// (get) Token: 0x060015A6 RID: 5542 RVA: 0x0005F233 File Offset: 0x0005D433
			public override string ussName
			{
				get
				{
					return "letter-spacing";
				}
			}

			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015A8 RID: 5544 RVA: 0x0005F23A File Offset: 0x0005D43A
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).letterSpacing;
			}

			// Token: 0x060015A9 RID: 5545 RVA: 0x0005F243 File Offset: 0x0005D443
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).letterSpacing = value;
			}
		}

		// Token: 0x020002F9 RID: 761
		private class MarginBottomProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x060015AB RID: 5547 RVA: 0x0005F24E File Offset: 0x0005D44E
			public override string Name
			{
				get
				{
					return "marginBottom";
				}
			}

			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x060015AC RID: 5548 RVA: 0x0005F255 File Offset: 0x0005D455
			public override string ussName
			{
				get
				{
					return "margin-bottom";
				}
			}

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x060015AD RID: 5549 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015AE RID: 5550 RVA: 0x0005F25C File Offset: 0x0005D45C
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).marginBottom;
			}

			// Token: 0x060015AF RID: 5551 RVA: 0x0005F265 File Offset: 0x0005D465
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).marginBottom = value;
			}
		}

		// Token: 0x020002FA RID: 762
		private class MarginLeftProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0005F270 File Offset: 0x0005D470
			public override string Name
			{
				get
				{
					return "marginLeft";
				}
			}

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x060015B2 RID: 5554 RVA: 0x0005F277 File Offset: 0x0005D477
			public override string ussName
			{
				get
				{
					return "margin-left";
				}
			}

			// Token: 0x170004D2 RID: 1234
			// (get) Token: 0x060015B3 RID: 5555 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015B4 RID: 5556 RVA: 0x0005F27E File Offset: 0x0005D47E
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).marginLeft;
			}

			// Token: 0x060015B5 RID: 5557 RVA: 0x0005F287 File Offset: 0x0005D487
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).marginLeft = value;
			}
		}

		// Token: 0x020002FB RID: 763
		private class MarginRightProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004D3 RID: 1235
			// (get) Token: 0x060015B7 RID: 5559 RVA: 0x0005F292 File Offset: 0x0005D492
			public override string Name
			{
				get
				{
					return "marginRight";
				}
			}

			// Token: 0x170004D4 RID: 1236
			// (get) Token: 0x060015B8 RID: 5560 RVA: 0x0005F299 File Offset: 0x0005D499
			public override string ussName
			{
				get
				{
					return "margin-right";
				}
			}

			// Token: 0x170004D5 RID: 1237
			// (get) Token: 0x060015B9 RID: 5561 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015BA RID: 5562 RVA: 0x0005F2A0 File Offset: 0x0005D4A0
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).marginRight;
			}

			// Token: 0x060015BB RID: 5563 RVA: 0x0005F2A9 File Offset: 0x0005D4A9
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).marginRight = value;
			}
		}

		// Token: 0x020002FC RID: 764
		private class MarginTopProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004D6 RID: 1238
			// (get) Token: 0x060015BD RID: 5565 RVA: 0x0005F2B4 File Offset: 0x0005D4B4
			public override string Name
			{
				get
				{
					return "marginTop";
				}
			}

			// Token: 0x170004D7 RID: 1239
			// (get) Token: 0x060015BE RID: 5566 RVA: 0x0005F2BB File Offset: 0x0005D4BB
			public override string ussName
			{
				get
				{
					return "margin-top";
				}
			}

			// Token: 0x170004D8 RID: 1240
			// (get) Token: 0x060015BF RID: 5567 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015C0 RID: 5568 RVA: 0x0005F2C2 File Offset: 0x0005D4C2
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).marginTop;
			}

			// Token: 0x060015C1 RID: 5569 RVA: 0x0005F2CB File Offset: 0x0005D4CB
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).marginTop = value;
			}
		}

		// Token: 0x020002FD RID: 765
		private class MaxHeightProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004D9 RID: 1241
			// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0005F2D6 File Offset: 0x0005D4D6
			public override string Name
			{
				get
				{
					return "maxHeight";
				}
			}

			// Token: 0x170004DA RID: 1242
			// (get) Token: 0x060015C4 RID: 5572 RVA: 0x0005F2DD File Offset: 0x0005D4DD
			public override string ussName
			{
				get
				{
					return "max-height";
				}
			}

			// Token: 0x170004DB RID: 1243
			// (get) Token: 0x060015C5 RID: 5573 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015C6 RID: 5574 RVA: 0x0005F2E4 File Offset: 0x0005D4E4
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).maxHeight;
			}

			// Token: 0x060015C7 RID: 5575 RVA: 0x0005F2ED File Offset: 0x0005D4ED
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).maxHeight = value;
			}
		}

		// Token: 0x020002FE RID: 766
		private class MaxWidthProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004DC RID: 1244
			// (get) Token: 0x060015C9 RID: 5577 RVA: 0x0005F2F8 File Offset: 0x0005D4F8
			public override string Name
			{
				get
				{
					return "maxWidth";
				}
			}

			// Token: 0x170004DD RID: 1245
			// (get) Token: 0x060015CA RID: 5578 RVA: 0x0005F2FF File Offset: 0x0005D4FF
			public override string ussName
			{
				get
				{
					return "max-width";
				}
			}

			// Token: 0x170004DE RID: 1246
			// (get) Token: 0x060015CB RID: 5579 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015CC RID: 5580 RVA: 0x0005F306 File Offset: 0x0005D506
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).maxWidth;
			}

			// Token: 0x060015CD RID: 5581 RVA: 0x0005F30F File Offset: 0x0005D50F
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).maxWidth = value;
			}
		}

		// Token: 0x020002FF RID: 767
		private class MinHeightProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004DF RID: 1247
			// (get) Token: 0x060015CF RID: 5583 RVA: 0x0005F31A File Offset: 0x0005D51A
			public override string Name
			{
				get
				{
					return "minHeight";
				}
			}

			// Token: 0x170004E0 RID: 1248
			// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0005F321 File Offset: 0x0005D521
			public override string ussName
			{
				get
				{
					return "min-height";
				}
			}

			// Token: 0x170004E1 RID: 1249
			// (get) Token: 0x060015D1 RID: 5585 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015D2 RID: 5586 RVA: 0x0005F328 File Offset: 0x0005D528
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).minHeight;
			}

			// Token: 0x060015D3 RID: 5587 RVA: 0x0005F331 File Offset: 0x0005D531
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).minHeight = value;
			}
		}

		// Token: 0x02000300 RID: 768
		private class MinWidthProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004E2 RID: 1250
			// (get) Token: 0x060015D5 RID: 5589 RVA: 0x0005F33C File Offset: 0x0005D53C
			public override string Name
			{
				get
				{
					return "minWidth";
				}
			}

			// Token: 0x170004E3 RID: 1251
			// (get) Token: 0x060015D6 RID: 5590 RVA: 0x0005F343 File Offset: 0x0005D543
			public override string ussName
			{
				get
				{
					return "min-width";
				}
			}

			// Token: 0x170004E4 RID: 1252
			// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015D8 RID: 5592 RVA: 0x0005F34A File Offset: 0x0005D54A
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).minWidth;
			}

			// Token: 0x060015D9 RID: 5593 RVA: 0x0005F353 File Offset: 0x0005D553
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).minWidth = value;
			}
		}

		// Token: 0x02000301 RID: 769
		private class OpacityProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x170004E5 RID: 1253
			// (get) Token: 0x060015DB RID: 5595 RVA: 0x0005F35E File Offset: 0x0005D55E
			public override string Name
			{
				get
				{
					return "opacity";
				}
			}

			// Token: 0x170004E6 RID: 1254
			// (get) Token: 0x060015DC RID: 5596 RVA: 0x0005F35E File Offset: 0x0005D55E
			public override string ussName
			{
				get
				{
					return "opacity";
				}
			}

			// Token: 0x170004E7 RID: 1255
			// (get) Token: 0x060015DD RID: 5597 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015DE RID: 5598 RVA: 0x0005F365 File Offset: 0x0005D565
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).opacity;
			}

			// Token: 0x060015DF RID: 5599 RVA: 0x0005F36E File Offset: 0x0005D56E
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).opacity = value;
			}
		}

		// Token: 0x02000302 RID: 770
		private class OverflowProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Overflow>
		{
			// Token: 0x170004E8 RID: 1256
			// (get) Token: 0x060015E1 RID: 5601 RVA: 0x0005F379 File Offset: 0x0005D579
			public override string Name
			{
				get
				{
					return "overflow";
				}
			}

			// Token: 0x170004E9 RID: 1257
			// (get) Token: 0x060015E2 RID: 5602 RVA: 0x0005F379 File Offset: 0x0005D579
			public override string ussName
			{
				get
				{
					return "overflow";
				}
			}

			// Token: 0x170004EA RID: 1258
			// (get) Token: 0x060015E3 RID: 5603 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015E4 RID: 5604 RVA: 0x0005F380 File Offset: 0x0005D580
			public override StyleEnum<Overflow> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).overflow;
			}

			// Token: 0x060015E5 RID: 5605 RVA: 0x0005F389 File Offset: 0x0005D589
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Overflow> value)
			{
				((IStyle)container).overflow = value;
			}
		}

		// Token: 0x02000303 RID: 771
		private class PaddingBottomProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004EB RID: 1259
			// (get) Token: 0x060015E7 RID: 5607 RVA: 0x0005F39D File Offset: 0x0005D59D
			public override string Name
			{
				get
				{
					return "paddingBottom";
				}
			}

			// Token: 0x170004EC RID: 1260
			// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0005F3A4 File Offset: 0x0005D5A4
			public override string ussName
			{
				get
				{
					return "padding-bottom";
				}
			}

			// Token: 0x170004ED RID: 1261
			// (get) Token: 0x060015E9 RID: 5609 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015EA RID: 5610 RVA: 0x0005F3AB File Offset: 0x0005D5AB
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).paddingBottom;
			}

			// Token: 0x060015EB RID: 5611 RVA: 0x0005F3B4 File Offset: 0x0005D5B4
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).paddingBottom = value;
			}
		}

		// Token: 0x02000304 RID: 772
		private class PaddingLeftProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004EE RID: 1262
			// (get) Token: 0x060015ED RID: 5613 RVA: 0x0005F3BF File Offset: 0x0005D5BF
			public override string Name
			{
				get
				{
					return "paddingLeft";
				}
			}

			// Token: 0x170004EF RID: 1263
			// (get) Token: 0x060015EE RID: 5614 RVA: 0x0005F3C6 File Offset: 0x0005D5C6
			public override string ussName
			{
				get
				{
					return "padding-left";
				}
			}

			// Token: 0x170004F0 RID: 1264
			// (get) Token: 0x060015EF RID: 5615 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015F0 RID: 5616 RVA: 0x0005F3CD File Offset: 0x0005D5CD
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).paddingLeft;
			}

			// Token: 0x060015F1 RID: 5617 RVA: 0x0005F3D6 File Offset: 0x0005D5D6
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).paddingLeft = value;
			}
		}

		// Token: 0x02000305 RID: 773
		private class PaddingRightProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004F1 RID: 1265
			// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0005F3E1 File Offset: 0x0005D5E1
			public override string Name
			{
				get
				{
					return "paddingRight";
				}
			}

			// Token: 0x170004F2 RID: 1266
			// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0005F3E8 File Offset: 0x0005D5E8
			public override string ussName
			{
				get
				{
					return "padding-right";
				}
			}

			// Token: 0x170004F3 RID: 1267
			// (get) Token: 0x060015F5 RID: 5621 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015F6 RID: 5622 RVA: 0x0005F3EF File Offset: 0x0005D5EF
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).paddingRight;
			}

			// Token: 0x060015F7 RID: 5623 RVA: 0x0005F3F8 File Offset: 0x0005D5F8
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).paddingRight = value;
			}
		}

		// Token: 0x02000306 RID: 774
		private class PaddingTopProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004F4 RID: 1268
			// (get) Token: 0x060015F9 RID: 5625 RVA: 0x0005F403 File Offset: 0x0005D603
			public override string Name
			{
				get
				{
					return "paddingTop";
				}
			}

			// Token: 0x170004F5 RID: 1269
			// (get) Token: 0x060015FA RID: 5626 RVA: 0x0005F40A File Offset: 0x0005D60A
			public override string ussName
			{
				get
				{
					return "padding-top";
				}
			}

			// Token: 0x170004F6 RID: 1270
			// (get) Token: 0x060015FB RID: 5627 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015FC RID: 5628 RVA: 0x0005F411 File Offset: 0x0005D611
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).paddingTop;
			}

			// Token: 0x060015FD RID: 5629 RVA: 0x0005F41A File Offset: 0x0005D61A
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).paddingTop = value;
			}
		}

		// Token: 0x02000307 RID: 775
		private class PositionProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Position>
		{
			// Token: 0x170004F7 RID: 1271
			// (get) Token: 0x060015FF RID: 5631 RVA: 0x0005F425 File Offset: 0x0005D625
			public override string Name
			{
				get
				{
					return "position";
				}
			}

			// Token: 0x170004F8 RID: 1272
			// (get) Token: 0x06001600 RID: 5632 RVA: 0x0005F425 File Offset: 0x0005D625
			public override string ussName
			{
				get
				{
					return "position";
				}
			}

			// Token: 0x170004F9 RID: 1273
			// (get) Token: 0x06001601 RID: 5633 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001602 RID: 5634 RVA: 0x0005F42C File Offset: 0x0005D62C
			public override StyleEnum<Position> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).position;
			}

			// Token: 0x06001603 RID: 5635 RVA: 0x0005F435 File Offset: 0x0005D635
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Position> value)
			{
				((IStyle)container).position = value;
			}
		}

		// Token: 0x02000308 RID: 776
		private class RightProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170004FA RID: 1274
			// (get) Token: 0x06001605 RID: 5637 RVA: 0x0005F449 File Offset: 0x0005D649
			public override string Name
			{
				get
				{
					return "right";
				}
			}

			// Token: 0x170004FB RID: 1275
			// (get) Token: 0x06001606 RID: 5638 RVA: 0x0005F449 File Offset: 0x0005D649
			public override string ussName
			{
				get
				{
					return "right";
				}
			}

			// Token: 0x170004FC RID: 1276
			// (get) Token: 0x06001607 RID: 5639 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001608 RID: 5640 RVA: 0x0005F450 File Offset: 0x0005D650
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).right;
			}

			// Token: 0x06001609 RID: 5641 RVA: 0x0005F459 File Offset: 0x0005D659
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).right = value;
			}
		}

		// Token: 0x02000309 RID: 777
		private class RotateProperty : InlineStyleAccessPropertyBag.InlineStyleRotateProperty
		{
			// Token: 0x170004FD RID: 1277
			// (get) Token: 0x0600160B RID: 5643 RVA: 0x0005F464 File Offset: 0x0005D664
			public override string Name
			{
				get
				{
					return "rotate";
				}
			}

			// Token: 0x170004FE RID: 1278
			// (get) Token: 0x0600160C RID: 5644 RVA: 0x0005F464 File Offset: 0x0005D664
			public override string ussName
			{
				get
				{
					return "rotate";
				}
			}

			// Token: 0x170004FF RID: 1279
			// (get) Token: 0x0600160D RID: 5645 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600160E RID: 5646 RVA: 0x0005F46B File Offset: 0x0005D66B
			public override StyleRotate GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).rotate;
			}

			// Token: 0x0600160F RID: 5647 RVA: 0x0005F474 File Offset: 0x0005D674
			public override void SetValue(ref InlineStyleAccess container, StyleRotate value)
			{
				((IStyle)container).rotate = value;
			}
		}

		// Token: 0x0200030A RID: 778
		private class ScaleProperty : InlineStyleAccessPropertyBag.InlineStyleScaleProperty
		{
			// Token: 0x17000500 RID: 1280
			// (get) Token: 0x06001611 RID: 5649 RVA: 0x0005F488 File Offset: 0x0005D688
			public override string Name
			{
				get
				{
					return "scale";
				}
			}

			// Token: 0x17000501 RID: 1281
			// (get) Token: 0x06001612 RID: 5650 RVA: 0x0005F488 File Offset: 0x0005D688
			public override string ussName
			{
				get
				{
					return "scale";
				}
			}

			// Token: 0x17000502 RID: 1282
			// (get) Token: 0x06001613 RID: 5651 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001614 RID: 5652 RVA: 0x0005F48F File Offset: 0x0005D68F
			public override StyleScale GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).scale;
			}

			// Token: 0x06001615 RID: 5653 RVA: 0x0005F498 File Offset: 0x0005D698
			public override void SetValue(ref InlineStyleAccess container, StyleScale value)
			{
				((IStyle)container).scale = value;
			}
		}

		// Token: 0x0200030B RID: 779
		private class TextOverflowProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<TextOverflow>
		{
			// Token: 0x17000503 RID: 1283
			// (get) Token: 0x06001617 RID: 5655 RVA: 0x0005F4AC File Offset: 0x0005D6AC
			public override string Name
			{
				get
				{
					return "textOverflow";
				}
			}

			// Token: 0x17000504 RID: 1284
			// (get) Token: 0x06001618 RID: 5656 RVA: 0x0005F4B3 File Offset: 0x0005D6B3
			public override string ussName
			{
				get
				{
					return "text-overflow";
				}
			}

			// Token: 0x17000505 RID: 1285
			// (get) Token: 0x06001619 RID: 5657 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600161A RID: 5658 RVA: 0x0005F4BA File Offset: 0x0005D6BA
			public override StyleEnum<TextOverflow> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).textOverflow;
			}

			// Token: 0x0600161B RID: 5659 RVA: 0x0005F4C3 File Offset: 0x0005D6C3
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<TextOverflow> value)
			{
				((IStyle)container).textOverflow = value;
			}
		}

		// Token: 0x0200030C RID: 780
		private class TextShadowProperty : InlineStyleAccessPropertyBag.InlineStyleTextShadowProperty
		{
			// Token: 0x17000506 RID: 1286
			// (get) Token: 0x0600161D RID: 5661 RVA: 0x0005F4D7 File Offset: 0x0005D6D7
			public override string Name
			{
				get
				{
					return "textShadow";
				}
			}

			// Token: 0x17000507 RID: 1287
			// (get) Token: 0x0600161E RID: 5662 RVA: 0x0005F4DE File Offset: 0x0005D6DE
			public override string ussName
			{
				get
				{
					return "text-shadow";
				}
			}

			// Token: 0x17000508 RID: 1288
			// (get) Token: 0x0600161F RID: 5663 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001620 RID: 5664 RVA: 0x0005F4E5 File Offset: 0x0005D6E5
			public override StyleTextShadow GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).textShadow;
			}

			// Token: 0x06001621 RID: 5665 RVA: 0x0005F4EE File Offset: 0x0005D6EE
			public override void SetValue(ref InlineStyleAccess container, StyleTextShadow value)
			{
				((IStyle)container).textShadow = value;
			}
		}

		// Token: 0x0200030D RID: 781
		private class TopProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x17000509 RID: 1289
			// (get) Token: 0x06001623 RID: 5667 RVA: 0x0005F502 File Offset: 0x0005D702
			public override string Name
			{
				get
				{
					return "top";
				}
			}

			// Token: 0x1700050A RID: 1290
			// (get) Token: 0x06001624 RID: 5668 RVA: 0x0005F502 File Offset: 0x0005D702
			public override string ussName
			{
				get
				{
					return "top";
				}
			}

			// Token: 0x1700050B RID: 1291
			// (get) Token: 0x06001625 RID: 5669 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001626 RID: 5670 RVA: 0x0005F509 File Offset: 0x0005D709
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).top;
			}

			// Token: 0x06001627 RID: 5671 RVA: 0x0005F512 File Offset: 0x0005D712
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).top = value;
			}
		}

		// Token: 0x0200030E RID: 782
		private class TransformOriginProperty : InlineStyleAccessPropertyBag.InlineStyleTransformOriginProperty
		{
			// Token: 0x1700050C RID: 1292
			// (get) Token: 0x06001629 RID: 5673 RVA: 0x0005F51D File Offset: 0x0005D71D
			public override string Name
			{
				get
				{
					return "transformOrigin";
				}
			}

			// Token: 0x1700050D RID: 1293
			// (get) Token: 0x0600162A RID: 5674 RVA: 0x0005F524 File Offset: 0x0005D724
			public override string ussName
			{
				get
				{
					return "transform-origin";
				}
			}

			// Token: 0x1700050E RID: 1294
			// (get) Token: 0x0600162B RID: 5675 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600162C RID: 5676 RVA: 0x0005F52B File Offset: 0x0005D72B
			public override StyleTransformOrigin GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).transformOrigin;
			}

			// Token: 0x0600162D RID: 5677 RVA: 0x0005F534 File Offset: 0x0005D734
			public override void SetValue(ref InlineStyleAccess container, StyleTransformOrigin value)
			{
				((IStyle)container).transformOrigin = value;
			}
		}

		// Token: 0x0200030F RID: 783
		private class TransitionDelayProperty : InlineStyleAccessPropertyBag.InlineStyleListProperty<TimeValue>
		{
			// Token: 0x1700050F RID: 1295
			// (get) Token: 0x0600162F RID: 5679 RVA: 0x0005F548 File Offset: 0x0005D748
			public override string Name
			{
				get
				{
					return "transitionDelay";
				}
			}

			// Token: 0x17000510 RID: 1296
			// (get) Token: 0x06001630 RID: 5680 RVA: 0x0005F54F File Offset: 0x0005D74F
			public override string ussName
			{
				get
				{
					return "transition-delay";
				}
			}

			// Token: 0x17000511 RID: 1297
			// (get) Token: 0x06001631 RID: 5681 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001632 RID: 5682 RVA: 0x0005F556 File Offset: 0x0005D756
			public override StyleList<TimeValue> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).transitionDelay;
			}

			// Token: 0x06001633 RID: 5683 RVA: 0x0005F55F File Offset: 0x0005D75F
			public override void SetValue(ref InlineStyleAccess container, StyleList<TimeValue> value)
			{
				((IStyle)container).transitionDelay = value;
			}
		}

		// Token: 0x02000310 RID: 784
		private class TransitionDurationProperty : InlineStyleAccessPropertyBag.InlineStyleListProperty<TimeValue>
		{
			// Token: 0x17000512 RID: 1298
			// (get) Token: 0x06001635 RID: 5685 RVA: 0x0005F573 File Offset: 0x0005D773
			public override string Name
			{
				get
				{
					return "transitionDuration";
				}
			}

			// Token: 0x17000513 RID: 1299
			// (get) Token: 0x06001636 RID: 5686 RVA: 0x0005F57A File Offset: 0x0005D77A
			public override string ussName
			{
				get
				{
					return "transition-duration";
				}
			}

			// Token: 0x17000514 RID: 1300
			// (get) Token: 0x06001637 RID: 5687 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001638 RID: 5688 RVA: 0x0005F581 File Offset: 0x0005D781
			public override StyleList<TimeValue> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).transitionDuration;
			}

			// Token: 0x06001639 RID: 5689 RVA: 0x0005F58A File Offset: 0x0005D78A
			public override void SetValue(ref InlineStyleAccess container, StyleList<TimeValue> value)
			{
				((IStyle)container).transitionDuration = value;
			}
		}

		// Token: 0x02000311 RID: 785
		private class TransitionPropertyProperty : InlineStyleAccessPropertyBag.InlineStyleListProperty<StylePropertyName>
		{
			// Token: 0x17000515 RID: 1301
			// (get) Token: 0x0600163B RID: 5691 RVA: 0x0005F595 File Offset: 0x0005D795
			public override string Name
			{
				get
				{
					return "transitionProperty";
				}
			}

			// Token: 0x17000516 RID: 1302
			// (get) Token: 0x0600163C RID: 5692 RVA: 0x0005F59C File Offset: 0x0005D79C
			public override string ussName
			{
				get
				{
					return "transition-property";
				}
			}

			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x0600163D RID: 5693 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600163E RID: 5694 RVA: 0x0005F5A3 File Offset: 0x0005D7A3
			public override StyleList<StylePropertyName> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).transitionProperty;
			}

			// Token: 0x0600163F RID: 5695 RVA: 0x0005F5AC File Offset: 0x0005D7AC
			public override void SetValue(ref InlineStyleAccess container, StyleList<StylePropertyName> value)
			{
				((IStyle)container).transitionProperty = value;
			}
		}

		// Token: 0x02000312 RID: 786
		private class TransitionTimingFunctionProperty : InlineStyleAccessPropertyBag.InlineStyleListProperty<EasingFunction>
		{
			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x06001641 RID: 5697 RVA: 0x0005F5C0 File Offset: 0x0005D7C0
			public override string Name
			{
				get
				{
					return "transitionTimingFunction";
				}
			}

			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x06001642 RID: 5698 RVA: 0x0005F5C7 File Offset: 0x0005D7C7
			public override string ussName
			{
				get
				{
					return "transition-timing-function";
				}
			}

			// Token: 0x1700051A RID: 1306
			// (get) Token: 0x06001643 RID: 5699 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001644 RID: 5700 RVA: 0x0005F5CE File Offset: 0x0005D7CE
			public override StyleList<EasingFunction> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).transitionTimingFunction;
			}

			// Token: 0x06001645 RID: 5701 RVA: 0x0005F5D7 File Offset: 0x0005D7D7
			public override void SetValue(ref InlineStyleAccess container, StyleList<EasingFunction> value)
			{
				((IStyle)container).transitionTimingFunction = value;
			}
		}

		// Token: 0x02000313 RID: 787
		private class TranslateProperty : InlineStyleAccessPropertyBag.InlineStyleTranslateProperty
		{
			// Token: 0x1700051B RID: 1307
			// (get) Token: 0x06001647 RID: 5703 RVA: 0x0005F5EB File Offset: 0x0005D7EB
			public override string Name
			{
				get
				{
					return "translate";
				}
			}

			// Token: 0x1700051C RID: 1308
			// (get) Token: 0x06001648 RID: 5704 RVA: 0x0005F5EB File Offset: 0x0005D7EB
			public override string ussName
			{
				get
				{
					return "translate";
				}
			}

			// Token: 0x1700051D RID: 1309
			// (get) Token: 0x06001649 RID: 5705 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600164A RID: 5706 RVA: 0x0005F5F2 File Offset: 0x0005D7F2
			public override StyleTranslate GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).translate;
			}

			// Token: 0x0600164B RID: 5707 RVA: 0x0005F5FB File Offset: 0x0005D7FB
			public override void SetValue(ref InlineStyleAccess container, StyleTranslate value)
			{
				((IStyle)container).translate = value;
			}
		}

		// Token: 0x02000314 RID: 788
		private class UnityBackgroundImageTintColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x1700051E RID: 1310
			// (get) Token: 0x0600164D RID: 5709 RVA: 0x0005F60F File Offset: 0x0005D80F
			public override string Name
			{
				get
				{
					return "unityBackgroundImageTintColor";
				}
			}

			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x0600164E RID: 5710 RVA: 0x0005F616 File Offset: 0x0005D816
			public override string ussName
			{
				get
				{
					return "-unity-background-image-tint-color";
				}
			}

			// Token: 0x17000520 RID: 1312
			// (get) Token: 0x0600164F RID: 5711 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001650 RID: 5712 RVA: 0x0005F61D File Offset: 0x0005D81D
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityBackgroundImageTintColor;
			}

			// Token: 0x06001651 RID: 5713 RVA: 0x0005F626 File Offset: 0x0005D826
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).unityBackgroundImageTintColor = value;
			}
		}

		// Token: 0x02000315 RID: 789
		private class UnityEditorTextRenderingModeProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<EditorTextRenderingMode>
		{
			// Token: 0x17000521 RID: 1313
			// (get) Token: 0x06001653 RID: 5715 RVA: 0x0005F631 File Offset: 0x0005D831
			public override string Name
			{
				get
				{
					return "unityEditorTextRenderingMode";
				}
			}

			// Token: 0x17000522 RID: 1314
			// (get) Token: 0x06001654 RID: 5716 RVA: 0x0005F638 File Offset: 0x0005D838
			public override string ussName
			{
				get
				{
					return "-unity-editor-text-rendering-mode";
				}
			}

			// Token: 0x17000523 RID: 1315
			// (get) Token: 0x06001655 RID: 5717 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001656 RID: 5718 RVA: 0x0005F63F File Offset: 0x0005D83F
			public override StyleEnum<EditorTextRenderingMode> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityEditorTextRenderingMode;
			}

			// Token: 0x06001657 RID: 5719 RVA: 0x0005F648 File Offset: 0x0005D848
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<EditorTextRenderingMode> value)
			{
				((IStyle)container).unityEditorTextRenderingMode = value;
			}
		}

		// Token: 0x02000316 RID: 790
		private class UnityFontProperty : InlineStyleAccessPropertyBag.InlineStyleFontProperty
		{
			// Token: 0x17000524 RID: 1316
			// (get) Token: 0x06001659 RID: 5721 RVA: 0x0005F65C File Offset: 0x0005D85C
			public override string Name
			{
				get
				{
					return "unityFont";
				}
			}

			// Token: 0x17000525 RID: 1317
			// (get) Token: 0x0600165A RID: 5722 RVA: 0x0005F663 File Offset: 0x0005D863
			public override string ussName
			{
				get
				{
					return "-unity-font";
				}
			}

			// Token: 0x17000526 RID: 1318
			// (get) Token: 0x0600165B RID: 5723 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600165C RID: 5724 RVA: 0x0005F66A File Offset: 0x0005D86A
			public override StyleFont GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityFont;
			}

			// Token: 0x0600165D RID: 5725 RVA: 0x0005F673 File Offset: 0x0005D873
			public override void SetValue(ref InlineStyleAccess container, StyleFont value)
			{
				((IStyle)container).unityFont = value;
			}
		}

		// Token: 0x02000317 RID: 791
		private class UnityFontDefinitionProperty : InlineStyleAccessPropertyBag.InlineStyleFontDefinitionProperty
		{
			// Token: 0x17000527 RID: 1319
			// (get) Token: 0x0600165F RID: 5727 RVA: 0x0005F687 File Offset: 0x0005D887
			public override string Name
			{
				get
				{
					return "unityFontDefinition";
				}
			}

			// Token: 0x17000528 RID: 1320
			// (get) Token: 0x06001660 RID: 5728 RVA: 0x0005F68E File Offset: 0x0005D88E
			public override string ussName
			{
				get
				{
					return "-unity-font-definition";
				}
			}

			// Token: 0x17000529 RID: 1321
			// (get) Token: 0x06001661 RID: 5729 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001662 RID: 5730 RVA: 0x0005F695 File Offset: 0x0005D895
			public override StyleFontDefinition GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityFontDefinition;
			}

			// Token: 0x06001663 RID: 5731 RVA: 0x0005F69E File Offset: 0x0005D89E
			public override void SetValue(ref InlineStyleAccess container, StyleFontDefinition value)
			{
				((IStyle)container).unityFontDefinition = value;
			}
		}

		// Token: 0x02000318 RID: 792
		private class UnityFontStyleAndWeightProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<FontStyle>
		{
			// Token: 0x1700052A RID: 1322
			// (get) Token: 0x06001665 RID: 5733 RVA: 0x0005F6B2 File Offset: 0x0005D8B2
			public override string Name
			{
				get
				{
					return "unityFontStyleAndWeight";
				}
			}

			// Token: 0x1700052B RID: 1323
			// (get) Token: 0x06001666 RID: 5734 RVA: 0x0005F6B9 File Offset: 0x0005D8B9
			public override string ussName
			{
				get
				{
					return "-unity-font-style";
				}
			}

			// Token: 0x1700052C RID: 1324
			// (get) Token: 0x06001667 RID: 5735 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001668 RID: 5736 RVA: 0x0005F6C0 File Offset: 0x0005D8C0
			public override StyleEnum<FontStyle> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityFontStyleAndWeight;
			}

			// Token: 0x06001669 RID: 5737 RVA: 0x0005F6C9 File Offset: 0x0005D8C9
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<FontStyle> value)
			{
				((IStyle)container).unityFontStyleAndWeight = value;
			}
		}

		// Token: 0x02000319 RID: 793
		private class UnityOverflowClipBoxProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<OverflowClipBox>
		{
			// Token: 0x1700052D RID: 1325
			// (get) Token: 0x0600166B RID: 5739 RVA: 0x0005F6DD File Offset: 0x0005D8DD
			public override string Name
			{
				get
				{
					return "unityOverflowClipBox";
				}
			}

			// Token: 0x1700052E RID: 1326
			// (get) Token: 0x0600166C RID: 5740 RVA: 0x0005F6E4 File Offset: 0x0005D8E4
			public override string ussName
			{
				get
				{
					return "-unity-overflow-clip-box";
				}
			}

			// Token: 0x1700052F RID: 1327
			// (get) Token: 0x0600166D RID: 5741 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600166E RID: 5742 RVA: 0x0005F6EB File Offset: 0x0005D8EB
			public override StyleEnum<OverflowClipBox> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityOverflowClipBox;
			}

			// Token: 0x0600166F RID: 5743 RVA: 0x0005F6F4 File Offset: 0x0005D8F4
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<OverflowClipBox> value)
			{
				((IStyle)container).unityOverflowClipBox = value;
			}
		}

		// Token: 0x0200031A RID: 794
		private class UnityParagraphSpacingProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x17000530 RID: 1328
			// (get) Token: 0x06001671 RID: 5745 RVA: 0x0005F708 File Offset: 0x0005D908
			public override string Name
			{
				get
				{
					return "unityParagraphSpacing";
				}
			}

			// Token: 0x17000531 RID: 1329
			// (get) Token: 0x06001672 RID: 5746 RVA: 0x0005F70F File Offset: 0x0005D90F
			public override string ussName
			{
				get
				{
					return "-unity-paragraph-spacing";
				}
			}

			// Token: 0x17000532 RID: 1330
			// (get) Token: 0x06001673 RID: 5747 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001674 RID: 5748 RVA: 0x0005F716 File Offset: 0x0005D916
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityParagraphSpacing;
			}

			// Token: 0x06001675 RID: 5749 RVA: 0x0005F71F File Offset: 0x0005D91F
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).unityParagraphSpacing = value;
			}
		}

		// Token: 0x0200031B RID: 795
		private class UnitySliceBottomProperty : InlineStyleAccessPropertyBag.InlineStyleIntProperty
		{
			// Token: 0x17000533 RID: 1331
			// (get) Token: 0x06001677 RID: 5751 RVA: 0x0005F72A File Offset: 0x0005D92A
			public override string Name
			{
				get
				{
					return "unitySliceBottom";
				}
			}

			// Token: 0x17000534 RID: 1332
			// (get) Token: 0x06001678 RID: 5752 RVA: 0x0005F731 File Offset: 0x0005D931
			public override string ussName
			{
				get
				{
					return "-unity-slice-bottom";
				}
			}

			// Token: 0x17000535 RID: 1333
			// (get) Token: 0x06001679 RID: 5753 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600167A RID: 5754 RVA: 0x0005F738 File Offset: 0x0005D938
			public override StyleInt GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unitySliceBottom;
			}

			// Token: 0x0600167B RID: 5755 RVA: 0x0005F741 File Offset: 0x0005D941
			public override void SetValue(ref InlineStyleAccess container, StyleInt value)
			{
				((IStyle)container).unitySliceBottom = value;
			}
		}

		// Token: 0x0200031C RID: 796
		private class UnitySliceLeftProperty : InlineStyleAccessPropertyBag.InlineStyleIntProperty
		{
			// Token: 0x17000536 RID: 1334
			// (get) Token: 0x0600167D RID: 5757 RVA: 0x0005F755 File Offset: 0x0005D955
			public override string Name
			{
				get
				{
					return "unitySliceLeft";
				}
			}

			// Token: 0x17000537 RID: 1335
			// (get) Token: 0x0600167E RID: 5758 RVA: 0x0005F75C File Offset: 0x0005D95C
			public override string ussName
			{
				get
				{
					return "-unity-slice-left";
				}
			}

			// Token: 0x17000538 RID: 1336
			// (get) Token: 0x0600167F RID: 5759 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001680 RID: 5760 RVA: 0x0005F763 File Offset: 0x0005D963
			public override StyleInt GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unitySliceLeft;
			}

			// Token: 0x06001681 RID: 5761 RVA: 0x0005F76C File Offset: 0x0005D96C
			public override void SetValue(ref InlineStyleAccess container, StyleInt value)
			{
				((IStyle)container).unitySliceLeft = value;
			}
		}

		// Token: 0x0200031D RID: 797
		private class UnitySliceRightProperty : InlineStyleAccessPropertyBag.InlineStyleIntProperty
		{
			// Token: 0x17000539 RID: 1337
			// (get) Token: 0x06001683 RID: 5763 RVA: 0x0005F777 File Offset: 0x0005D977
			public override string Name
			{
				get
				{
					return "unitySliceRight";
				}
			}

			// Token: 0x1700053A RID: 1338
			// (get) Token: 0x06001684 RID: 5764 RVA: 0x0005F77E File Offset: 0x0005D97E
			public override string ussName
			{
				get
				{
					return "-unity-slice-right";
				}
			}

			// Token: 0x1700053B RID: 1339
			// (get) Token: 0x06001685 RID: 5765 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001686 RID: 5766 RVA: 0x0005F785 File Offset: 0x0005D985
			public override StyleInt GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unitySliceRight;
			}

			// Token: 0x06001687 RID: 5767 RVA: 0x0005F78E File Offset: 0x0005D98E
			public override void SetValue(ref InlineStyleAccess container, StyleInt value)
			{
				((IStyle)container).unitySliceRight = value;
			}
		}

		// Token: 0x0200031E RID: 798
		private class UnitySliceScaleProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x1700053C RID: 1340
			// (get) Token: 0x06001689 RID: 5769 RVA: 0x0005F799 File Offset: 0x0005D999
			public override string Name
			{
				get
				{
					return "unitySliceScale";
				}
			}

			// Token: 0x1700053D RID: 1341
			// (get) Token: 0x0600168A RID: 5770 RVA: 0x0005F7A0 File Offset: 0x0005D9A0
			public override string ussName
			{
				get
				{
					return "-unity-slice-scale";
				}
			}

			// Token: 0x1700053E RID: 1342
			// (get) Token: 0x0600168B RID: 5771 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600168C RID: 5772 RVA: 0x0005F7A7 File Offset: 0x0005D9A7
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unitySliceScale;
			}

			// Token: 0x0600168D RID: 5773 RVA: 0x0005F7B0 File Offset: 0x0005D9B0
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).unitySliceScale = value;
			}
		}

		// Token: 0x0200031F RID: 799
		private class UnitySliceTopProperty : InlineStyleAccessPropertyBag.InlineStyleIntProperty
		{
			// Token: 0x1700053F RID: 1343
			// (get) Token: 0x0600168F RID: 5775 RVA: 0x0005F7BB File Offset: 0x0005D9BB
			public override string Name
			{
				get
				{
					return "unitySliceTop";
				}
			}

			// Token: 0x17000540 RID: 1344
			// (get) Token: 0x06001690 RID: 5776 RVA: 0x0005F7C2 File Offset: 0x0005D9C2
			public override string ussName
			{
				get
				{
					return "-unity-slice-top";
				}
			}

			// Token: 0x17000541 RID: 1345
			// (get) Token: 0x06001691 RID: 5777 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001692 RID: 5778 RVA: 0x0005F7C9 File Offset: 0x0005D9C9
			public override StyleInt GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unitySliceTop;
			}

			// Token: 0x06001693 RID: 5779 RVA: 0x0005F7D2 File Offset: 0x0005D9D2
			public override void SetValue(ref InlineStyleAccess container, StyleInt value)
			{
				((IStyle)container).unitySliceTop = value;
			}
		}

		// Token: 0x02000320 RID: 800
		private class UnityTextAlignProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<TextAnchor>
		{
			// Token: 0x17000542 RID: 1346
			// (get) Token: 0x06001695 RID: 5781 RVA: 0x0005F7DD File Offset: 0x0005D9DD
			public override string Name
			{
				get
				{
					return "unityTextAlign";
				}
			}

			// Token: 0x17000543 RID: 1347
			// (get) Token: 0x06001696 RID: 5782 RVA: 0x0005F7E4 File Offset: 0x0005D9E4
			public override string ussName
			{
				get
				{
					return "-unity-text-align";
				}
			}

			// Token: 0x17000544 RID: 1348
			// (get) Token: 0x06001697 RID: 5783 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001698 RID: 5784 RVA: 0x0005F7EB File Offset: 0x0005D9EB
			public override StyleEnum<TextAnchor> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityTextAlign;
			}

			// Token: 0x06001699 RID: 5785 RVA: 0x0005F7F4 File Offset: 0x0005D9F4
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<TextAnchor> value)
			{
				((IStyle)container).unityTextAlign = value;
			}
		}

		// Token: 0x02000321 RID: 801
		private class UnityTextGeneratorProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<TextGeneratorType>
		{
			// Token: 0x17000545 RID: 1349
			// (get) Token: 0x0600169B RID: 5787 RVA: 0x0005F808 File Offset: 0x0005DA08
			public override string Name
			{
				get
				{
					return "unityTextGenerator";
				}
			}

			// Token: 0x17000546 RID: 1350
			// (get) Token: 0x0600169C RID: 5788 RVA: 0x0005F80F File Offset: 0x0005DA0F
			public override string ussName
			{
				get
				{
					return "-unity-text-generator";
				}
			}

			// Token: 0x17000547 RID: 1351
			// (get) Token: 0x0600169D RID: 5789 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600169E RID: 5790 RVA: 0x0005F816 File Offset: 0x0005DA16
			public override StyleEnum<TextGeneratorType> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityTextGenerator;
			}

			// Token: 0x0600169F RID: 5791 RVA: 0x0005F81F File Offset: 0x0005DA1F
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<TextGeneratorType> value)
			{
				((IStyle)container).unityTextGenerator = value;
			}
		}

		// Token: 0x02000322 RID: 802
		private class UnityTextOutlineColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x17000548 RID: 1352
			// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0005F833 File Offset: 0x0005DA33
			public override string Name
			{
				get
				{
					return "unityTextOutlineColor";
				}
			}

			// Token: 0x17000549 RID: 1353
			// (get) Token: 0x060016A2 RID: 5794 RVA: 0x0005F83A File Offset: 0x0005DA3A
			public override string ussName
			{
				get
				{
					return "-unity-text-outline-color";
				}
			}

			// Token: 0x1700054A RID: 1354
			// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060016A4 RID: 5796 RVA: 0x0005F841 File Offset: 0x0005DA41
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityTextOutlineColor;
			}

			// Token: 0x060016A5 RID: 5797 RVA: 0x0005F84A File Offset: 0x0005DA4A
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).unityTextOutlineColor = value;
			}
		}

		// Token: 0x02000323 RID: 803
		private class UnityTextOutlineWidthProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x1700054B RID: 1355
			// (get) Token: 0x060016A7 RID: 5799 RVA: 0x0005F855 File Offset: 0x0005DA55
			public override string Name
			{
				get
				{
					return "unityTextOutlineWidth";
				}
			}

			// Token: 0x1700054C RID: 1356
			// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0005F85C File Offset: 0x0005DA5C
			public override string ussName
			{
				get
				{
					return "-unity-text-outline-width";
				}
			}

			// Token: 0x1700054D RID: 1357
			// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060016AA RID: 5802 RVA: 0x0005F863 File Offset: 0x0005DA63
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityTextOutlineWidth;
			}

			// Token: 0x060016AB RID: 5803 RVA: 0x0005F86C File Offset: 0x0005DA6C
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).unityTextOutlineWidth = value;
			}
		}

		// Token: 0x02000324 RID: 804
		private class UnityTextOverflowPositionProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<TextOverflowPosition>
		{
			// Token: 0x1700054E RID: 1358
			// (get) Token: 0x060016AD RID: 5805 RVA: 0x0005F877 File Offset: 0x0005DA77
			public override string Name
			{
				get
				{
					return "unityTextOverflowPosition";
				}
			}

			// Token: 0x1700054F RID: 1359
			// (get) Token: 0x060016AE RID: 5806 RVA: 0x0005F87E File Offset: 0x0005DA7E
			public override string ussName
			{
				get
				{
					return "-unity-text-overflow-position";
				}
			}

			// Token: 0x17000550 RID: 1360
			// (get) Token: 0x060016AF RID: 5807 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060016B0 RID: 5808 RVA: 0x0005F885 File Offset: 0x0005DA85
			public override StyleEnum<TextOverflowPosition> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityTextOverflowPosition;
			}

			// Token: 0x060016B1 RID: 5809 RVA: 0x0005F88E File Offset: 0x0005DA8E
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<TextOverflowPosition> value)
			{
				((IStyle)container).unityTextOverflowPosition = value;
			}
		}

		// Token: 0x02000325 RID: 805
		private class VisibilityProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Visibility>
		{
			// Token: 0x17000551 RID: 1361
			// (get) Token: 0x060016B3 RID: 5811 RVA: 0x0005F8A2 File Offset: 0x0005DAA2
			public override string Name
			{
				get
				{
					return "visibility";
				}
			}

			// Token: 0x17000552 RID: 1362
			// (get) Token: 0x060016B4 RID: 5812 RVA: 0x0005F8A2 File Offset: 0x0005DAA2
			public override string ussName
			{
				get
				{
					return "visibility";
				}
			}

			// Token: 0x17000553 RID: 1363
			// (get) Token: 0x060016B5 RID: 5813 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060016B6 RID: 5814 RVA: 0x0005F8A9 File Offset: 0x0005DAA9
			public override StyleEnum<Visibility> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).visibility;
			}

			// Token: 0x060016B7 RID: 5815 RVA: 0x0005F8B2 File Offset: 0x0005DAB2
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Visibility> value)
			{
				((IStyle)container).visibility = value;
			}
		}

		// Token: 0x02000326 RID: 806
		private class WhiteSpaceProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<WhiteSpace>
		{
			// Token: 0x17000554 RID: 1364
			// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0005F8C6 File Offset: 0x0005DAC6
			public override string Name
			{
				get
				{
					return "whiteSpace";
				}
			}

			// Token: 0x17000555 RID: 1365
			// (get) Token: 0x060016BA RID: 5818 RVA: 0x0005F8CD File Offset: 0x0005DACD
			public override string ussName
			{
				get
				{
					return "white-space";
				}
			}

			// Token: 0x17000556 RID: 1366
			// (get) Token: 0x060016BB RID: 5819 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060016BC RID: 5820 RVA: 0x0005F8D4 File Offset: 0x0005DAD4
			public override StyleEnum<WhiteSpace> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).whiteSpace;
			}

			// Token: 0x060016BD RID: 5821 RVA: 0x0005F8DD File Offset: 0x0005DADD
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<WhiteSpace> value)
			{
				((IStyle)container).whiteSpace = value;
			}
		}

		// Token: 0x02000327 RID: 807
		private class WidthProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x17000557 RID: 1367
			// (get) Token: 0x060016BF RID: 5823 RVA: 0x0005F8F1 File Offset: 0x0005DAF1
			public override string Name
			{
				get
				{
					return "width";
				}
			}

			// Token: 0x17000558 RID: 1368
			// (get) Token: 0x060016C0 RID: 5824 RVA: 0x0005F8F1 File Offset: 0x0005DAF1
			public override string ussName
			{
				get
				{
					return "width";
				}
			}

			// Token: 0x17000559 RID: 1369
			// (get) Token: 0x060016C1 RID: 5825 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060016C2 RID: 5826 RVA: 0x0005F8F8 File Offset: 0x0005DAF8
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).width;
			}

			// Token: 0x060016C3 RID: 5827 RVA: 0x0005F901 File Offset: 0x0005DB01
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).width = value;
			}
		}

		// Token: 0x02000328 RID: 808
		private class WordSpacingProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x1700055A RID: 1370
			// (get) Token: 0x060016C5 RID: 5829 RVA: 0x0005F90C File Offset: 0x0005DB0C
			public override string Name
			{
				get
				{
					return "wordSpacing";
				}
			}

			// Token: 0x1700055B RID: 1371
			// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0005F913 File Offset: 0x0005DB13
			public override string ussName
			{
				get
				{
					return "word-spacing";
				}
			}

			// Token: 0x1700055C RID: 1372
			// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060016C8 RID: 5832 RVA: 0x0005F91A File Offset: 0x0005DB1A
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).wordSpacing;
			}

			// Token: 0x060016C9 RID: 5833 RVA: 0x0005F923 File Offset: 0x0005DB23
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).wordSpacing = value;
			}
		}

		// Token: 0x02000329 RID: 809
		private abstract class InlineStyleProperty<TStyleValue, TValue> : Property<InlineStyleAccess, TStyleValue> where TStyleValue : IStyleValue<TValue>, new()
		{
			// Token: 0x060016CB RID: 5835 RVA: 0x0005F930 File Offset: 0x0005DB30
			protected InlineStyleProperty()
			{
				ConverterGroups.RegisterGlobal<TStyleValue, TValue>(delegate(ref TStyleValue sv)
				{
					return sv.value;
				});
				ConverterGroups.RegisterGlobal<TValue, TStyleValue>(delegate(ref TValue v)
				{
					TStyleValue tstyleValue = new TStyleValue();
					tstyleValue.value = v;
					return tstyleValue;
				});
				ConverterGroups.RegisterGlobal<TStyleValue, StyleKeyword>(delegate(ref TStyleValue sv)
				{
					return sv.keyword;
				});
				ConverterGroups.RegisterGlobal<StyleKeyword, TStyleValue>(delegate(ref StyleKeyword kw)
				{
					TStyleValue tstyleValue2 = new TStyleValue();
					tstyleValue2.keyword = kw;
					return tstyleValue2;
				});
			}

			// Token: 0x1700055D RID: 1373
			// (get) Token: 0x060016CC RID: 5836
			public abstract string ussName { get; }
		}

		// Token: 0x0200032B RID: 811
		private abstract class InlineStyleEnumProperty<TValue> : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleEnum<TValue>, TValue> where TValue : struct, IConvertible
		{
		}

		// Token: 0x0200032C RID: 812
		private abstract class InlineStyleColorProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleColor, Color>
		{
			// Token: 0x060016D4 RID: 5844 RVA: 0x0005FA5C File Offset: 0x0005DC5C
			protected InlineStyleColorProperty()
			{
				ConverterGroups.RegisterGlobal<Color32, StyleColor>(delegate(ref Color32 v)
				{
					return new StyleColor(v);
				});
				ConverterGroups.RegisterGlobal<StyleColor, Color32>(delegate(ref StyleColor sv)
				{
					return sv.value;
				});
			}
		}

		// Token: 0x0200032E RID: 814
		private abstract class InlineStyleBackgroundProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleBackground, Background>
		{
			// Token: 0x060016D9 RID: 5849 RVA: 0x0005FAE8 File Offset: 0x0005DCE8
			protected InlineStyleBackgroundProperty()
			{
				ConverterGroups.RegisterGlobal<Texture2D, StyleBackground>(delegate(ref Texture2D v)
				{
					return new StyleBackground(v);
				});
				ConverterGroups.RegisterGlobal<Sprite, StyleBackground>(delegate(ref Sprite v)
				{
					return new StyleBackground(v);
				});
				ConverterGroups.RegisterGlobal<VectorImage, StyleBackground>(delegate(ref VectorImage v)
				{
					return new StyleBackground(v);
				});
				ConverterGroups.RegisterGlobal<StyleBackground, Texture2D>(delegate(ref StyleBackground sv)
				{
					return sv.value.texture;
				});
				ConverterGroups.RegisterGlobal<StyleBackground, Sprite>(delegate(ref StyleBackground sv)
				{
					return sv.value.sprite;
				});
				ConverterGroups.RegisterGlobal<StyleBackground, RenderTexture>(delegate(ref StyleBackground sv)
				{
					return sv.value.renderTexture;
				});
				ConverterGroups.RegisterGlobal<StyleBackground, VectorImage>(delegate(ref StyleBackground sv)
				{
					return sv.value.vectorImage;
				});
			}
		}

		// Token: 0x02000330 RID: 816
		private abstract class InlineStyleLengthProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleLength, Length>
		{
			// Token: 0x060016E3 RID: 5859 RVA: 0x0005FC98 File Offset: 0x0005DE98
			protected InlineStyleLengthProperty()
			{
				ConverterGroups.RegisterGlobal<float, StyleLength>(delegate(ref float v)
				{
					return new StyleLength(v);
				});
				ConverterGroups.RegisterGlobal<int, StyleLength>(delegate(ref int v)
				{
					return new StyleLength((float)v);
				});
				ConverterGroups.RegisterGlobal<StyleLength, float>(delegate(ref StyleLength sv)
				{
					return sv.value.value;
				});
				ConverterGroups.RegisterGlobal<StyleLength, int>(delegate(ref StyleLength sv)
				{
					return (int)sv.value.value;
				});
			}
		}

		// Token: 0x02000332 RID: 818
		private abstract class InlineStyleFloatProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleFloat, float>
		{
			// Token: 0x060016EA RID: 5866 RVA: 0x0005FD98 File Offset: 0x0005DF98
			protected InlineStyleFloatProperty()
			{
				ConverterGroups.RegisterGlobal<int, StyleFloat>(delegate(ref int v)
				{
					return new StyleFloat((float)v);
				});
				ConverterGroups.RegisterGlobal<StyleFloat, int>(delegate(ref StyleFloat sv)
				{
					return (int)sv.value;
				});
			}
		}

		// Token: 0x02000334 RID: 820
		private abstract class InlineStyleListProperty<T> : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleList<T>, List<T>>
		{
		}

		// Token: 0x02000335 RID: 821
		private abstract class InlineStyleFontProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleFont, Font>
		{
		}

		// Token: 0x02000336 RID: 822
		private abstract class InlineStyleFontDefinitionProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleFontDefinition, FontDefinition>
		{
			// Token: 0x060016F1 RID: 5873 RVA: 0x0005FE28 File Offset: 0x0005E028
			protected InlineStyleFontDefinitionProperty()
			{
				ConverterGroups.RegisterGlobal<Font, StyleFontDefinition>(delegate(ref Font v)
				{
					return new StyleFontDefinition(v);
				});
				ConverterGroups.RegisterGlobal<FontAsset, StyleFontDefinition>(delegate(ref FontAsset v)
				{
					return new StyleFontDefinition(v);
				});
				ConverterGroups.RegisterGlobal<StyleFontDefinition, Font>(delegate(ref StyleFontDefinition sv)
				{
					return sv.value.font;
				});
				ConverterGroups.RegisterGlobal<StyleFontDefinition, FontAsset>(delegate(ref StyleFontDefinition sv)
				{
					return sv.value.fontAsset;
				});
			}
		}

		// Token: 0x02000338 RID: 824
		private abstract class InlineStyleIntProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleInt, int>
		{
		}

		// Token: 0x02000339 RID: 825
		private abstract class InlineStyleRotateProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleRotate, Rotate>
		{
		}

		// Token: 0x0200033A RID: 826
		private abstract class InlineStyleScaleProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleScale, Scale>
		{
		}

		// Token: 0x0200033B RID: 827
		private abstract class InlineStyleCursorProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleCursor, Cursor>
		{
		}

		// Token: 0x0200033C RID: 828
		private abstract class InlineStyleTextShadowProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleTextShadow, TextShadow>
		{
		}

		// Token: 0x0200033D RID: 829
		private abstract class InlineStyleTransformOriginProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleTransformOrigin, TransformOrigin>
		{
		}

		// Token: 0x0200033E RID: 830
		private abstract class InlineStyleTranslateProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleTranslate, Translate>
		{
		}

		// Token: 0x0200033F RID: 831
		private abstract class InlineStyleBackgroundPositionProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleBackgroundPosition, BackgroundPosition>
		{
		}

		// Token: 0x02000340 RID: 832
		private abstract class InlineStyleBackgroundRepeatProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleBackgroundRepeat, BackgroundRepeat>
		{
		}

		// Token: 0x02000341 RID: 833
		private abstract class InlineStyleBackgroundSizeProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleBackgroundSize, BackgroundSize>
		{
		}
	}
}
