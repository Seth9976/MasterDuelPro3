using System;
using System.Collections.Generic;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000352 RID: 850
	internal class ResolvedStyleAccessPropertyBag : PropertyBag<ResolvedStyleAccess>, INamedProperties<ResolvedStyleAccess>
	{
		// Token: 0x06001A08 RID: 6664 RVA: 0x000680D0 File Offset: 0x000662D0
		public ResolvedStyleAccessPropertyBag()
		{
			this.m_PropertiesList = new List<IProperty<ResolvedStyleAccess>>(79);
			this.m_PropertiesHash = new Dictionary<string, IProperty<ResolvedStyleAccess>>(237);
			this.AddProperty<Align>(new ResolvedStyleAccessPropertyBag.AlignContentProperty());
			this.AddProperty<Align>(new ResolvedStyleAccessPropertyBag.AlignItemsProperty());
			this.AddProperty<Align>(new ResolvedStyleAccessPropertyBag.AlignSelfProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.BackgroundColorProperty());
			this.AddProperty<Background>(new ResolvedStyleAccessPropertyBag.BackgroundImageProperty());
			this.AddProperty<BackgroundPosition>(new ResolvedStyleAccessPropertyBag.BackgroundPositionXProperty());
			this.AddProperty<BackgroundPosition>(new ResolvedStyleAccessPropertyBag.BackgroundPositionYProperty());
			this.AddProperty<BackgroundRepeat>(new ResolvedStyleAccessPropertyBag.BackgroundRepeatProperty());
			this.AddProperty<BackgroundSize>(new ResolvedStyleAccessPropertyBag.BackgroundSizeProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.BorderBottomColorProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderBottomLeftRadiusProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderBottomRightRadiusProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderBottomWidthProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.BorderLeftColorProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderLeftWidthProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.BorderRightColorProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderRightWidthProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.BorderTopColorProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderTopLeftRadiusProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderTopRightRadiusProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderTopWidthProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BottomProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.ColorProperty());
			this.AddProperty<DisplayStyle>(new ResolvedStyleAccessPropertyBag.DisplayProperty());
			this.AddProperty<StyleFloat>(new ResolvedStyleAccessPropertyBag.FlexBasisProperty());
			this.AddProperty<FlexDirection>(new ResolvedStyleAccessPropertyBag.FlexDirectionProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.FlexGrowProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.FlexShrinkProperty());
			this.AddProperty<Wrap>(new ResolvedStyleAccessPropertyBag.FlexWrapProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.FontSizeProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.HeightProperty());
			this.AddProperty<Justify>(new ResolvedStyleAccessPropertyBag.JustifyContentProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.LeftProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.LetterSpacingProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.MarginBottomProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.MarginLeftProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.MarginRightProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.MarginTopProperty());
			this.AddProperty<StyleFloat>(new ResolvedStyleAccessPropertyBag.MaxHeightProperty());
			this.AddProperty<StyleFloat>(new ResolvedStyleAccessPropertyBag.MaxWidthProperty());
			this.AddProperty<StyleFloat>(new ResolvedStyleAccessPropertyBag.MinHeightProperty());
			this.AddProperty<StyleFloat>(new ResolvedStyleAccessPropertyBag.MinWidthProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.OpacityProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.PaddingBottomProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.PaddingLeftProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.PaddingRightProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.PaddingTopProperty());
			this.AddProperty<Position>(new ResolvedStyleAccessPropertyBag.PositionProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.RightProperty());
			this.AddProperty<Rotate>(new ResolvedStyleAccessPropertyBag.RotateProperty());
			this.AddProperty<Scale>(new ResolvedStyleAccessPropertyBag.ScaleProperty());
			this.AddProperty<TextOverflow>(new ResolvedStyleAccessPropertyBag.TextOverflowProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.TopProperty());
			this.AddProperty<Vector3>(new ResolvedStyleAccessPropertyBag.TransformOriginProperty());
			this.AddProperty<IEnumerable<TimeValue>>(new ResolvedStyleAccessPropertyBag.TransitionDelayProperty());
			this.AddProperty<IEnumerable<TimeValue>>(new ResolvedStyleAccessPropertyBag.TransitionDurationProperty());
			this.AddProperty<IEnumerable<StylePropertyName>>(new ResolvedStyleAccessPropertyBag.TransitionPropertyProperty());
			this.AddProperty<IEnumerable<EasingFunction>>(new ResolvedStyleAccessPropertyBag.TransitionTimingFunctionProperty());
			this.AddProperty<Vector3>(new ResolvedStyleAccessPropertyBag.TranslateProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.UnityBackgroundImageTintColorProperty());
			this.AddProperty<EditorTextRenderingMode>(new ResolvedStyleAccessPropertyBag.UnityEditorTextRenderingModeProperty());
			this.AddProperty<Font>(new ResolvedStyleAccessPropertyBag.UnityFontProperty());
			this.AddProperty<FontDefinition>(new ResolvedStyleAccessPropertyBag.UnityFontDefinitionProperty());
			this.AddProperty<FontStyle>(new ResolvedStyleAccessPropertyBag.UnityFontStyleAndWeightProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.UnityParagraphSpacingProperty());
			this.AddProperty<int>(new ResolvedStyleAccessPropertyBag.UnitySliceBottomProperty());
			this.AddProperty<int>(new ResolvedStyleAccessPropertyBag.UnitySliceLeftProperty());
			this.AddProperty<int>(new ResolvedStyleAccessPropertyBag.UnitySliceRightProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.UnitySliceScaleProperty());
			this.AddProperty<int>(new ResolvedStyleAccessPropertyBag.UnitySliceTopProperty());
			this.AddProperty<TextAnchor>(new ResolvedStyleAccessPropertyBag.UnityTextAlignProperty());
			this.AddProperty<TextGeneratorType>(new ResolvedStyleAccessPropertyBag.UnityTextGeneratorProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.UnityTextOutlineColorProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.UnityTextOutlineWidthProperty());
			this.AddProperty<TextOverflowPosition>(new ResolvedStyleAccessPropertyBag.UnityTextOverflowPositionProperty());
			this.AddProperty<Visibility>(new ResolvedStyleAccessPropertyBag.VisibilityProperty());
			this.AddProperty<WhiteSpace>(new ResolvedStyleAccessPropertyBag.WhiteSpaceProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.WidthProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.WordSpacingProperty());
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000684B8 File Offset: 0x000666B8
		private void AddProperty<TValue>(ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<TValue> property)
		{
			this.m_PropertiesList.Add(property);
			this.m_PropertiesHash.Add(property.Name, property);
			bool flag = string.CompareOrdinal(property.Name, property.ussName) != 0;
			if (flag)
			{
				this.m_PropertiesHash.Add(property.ussName, property);
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00068511 File Offset: 0x00066711
		public override PropertyCollection<ResolvedStyleAccess> GetProperties()
		{
			return new PropertyCollection<ResolvedStyleAccess>(this.m_PropertiesList);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00068511 File Offset: 0x00066711
		public override PropertyCollection<ResolvedStyleAccess> GetProperties(ref ResolvedStyleAccess container)
		{
			return new PropertyCollection<ResolvedStyleAccess>(this.m_PropertiesList);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0006851E File Offset: 0x0006671E
		public bool TryGetProperty(ref ResolvedStyleAccess container, string name, out IProperty<ResolvedStyleAccess> property)
		{
			return this.m_PropertiesHash.TryGetValue(name, out property);
		}

		// Token: 0x04000C10 RID: 3088
		private readonly List<IProperty<ResolvedStyleAccess>> m_PropertiesList;

		// Token: 0x04000C11 RID: 3089
		private readonly Dictionary<string, IProperty<ResolvedStyleAccess>> m_PropertiesHash;

		// Token: 0x02000353 RID: 851
		private class AlignContentProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<Align>
		{
			// Token: 0x17000707 RID: 1799
			// (get) Token: 0x06001A0D RID: 6669 RVA: 0x0005ED5D File Offset: 0x0005CF5D
			public override string Name
			{
				get
				{
					return "alignContent";
				}
			}

			// Token: 0x17000708 RID: 1800
			// (get) Token: 0x06001A0E RID: 6670 RVA: 0x0005ED64 File Offset: 0x0005CF64
			public override string ussName
			{
				get
				{
					return "align-content";
				}
			}

			// Token: 0x17000709 RID: 1801
			// (get) Token: 0x06001A0F RID: 6671 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A10 RID: 6672 RVA: 0x0006852D File Offset: 0x0006672D
			public override Align GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).alignContent;
			}

			// Token: 0x06001A11 RID: 6673 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Align value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000354 RID: 852
		private class AlignItemsProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<Align>
		{
			// Token: 0x1700070A RID: 1802
			// (get) Token: 0x06001A13 RID: 6675 RVA: 0x0005ED88 File Offset: 0x0005CF88
			public override string Name
			{
				get
				{
					return "alignItems";
				}
			}

			// Token: 0x1700070B RID: 1803
			// (get) Token: 0x06001A14 RID: 6676 RVA: 0x0005ED8F File Offset: 0x0005CF8F
			public override string ussName
			{
				get
				{
					return "align-items";
				}
			}

			// Token: 0x1700070C RID: 1804
			// (get) Token: 0x06001A15 RID: 6677 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A16 RID: 6678 RVA: 0x00068546 File Offset: 0x00066746
			public override Align GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).alignItems;
			}

			// Token: 0x06001A17 RID: 6679 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Align value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000355 RID: 853
		private class AlignSelfProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<Align>
		{
			// Token: 0x1700070D RID: 1805
			// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0005EDAA File Offset: 0x0005CFAA
			public override string Name
			{
				get
				{
					return "alignSelf";
				}
			}

			// Token: 0x1700070E RID: 1806
			// (get) Token: 0x06001A1A RID: 6682 RVA: 0x0005EDB1 File Offset: 0x0005CFB1
			public override string ussName
			{
				get
				{
					return "align-self";
				}
			}

			// Token: 0x1700070F RID: 1807
			// (get) Token: 0x06001A1B RID: 6683 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A1C RID: 6684 RVA: 0x0006854F File Offset: 0x0006674F
			public override Align GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).alignSelf;
			}

			// Token: 0x06001A1D RID: 6685 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Align value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000356 RID: 854
		private class BackgroundColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x17000710 RID: 1808
			// (get) Token: 0x06001A1F RID: 6687 RVA: 0x0005EDCC File Offset: 0x0005CFCC
			public override string Name
			{
				get
				{
					return "backgroundColor";
				}
			}

			// Token: 0x17000711 RID: 1809
			// (get) Token: 0x06001A20 RID: 6688 RVA: 0x0005EDD3 File Offset: 0x0005CFD3
			public override string ussName
			{
				get
				{
					return "background-color";
				}
			}

			// Token: 0x17000712 RID: 1810
			// (get) Token: 0x06001A21 RID: 6689 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A22 RID: 6690 RVA: 0x00068558 File Offset: 0x00066758
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).backgroundColor;
			}

			// Token: 0x06001A23 RID: 6691 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000357 RID: 855
		private class BackgroundImageProperty : ResolvedStyleAccessPropertyBag.ResolvedBackgroundProperty
		{
			// Token: 0x17000713 RID: 1811
			// (get) Token: 0x06001A25 RID: 6693 RVA: 0x0005EDF7 File Offset: 0x0005CFF7
			public override string Name
			{
				get
				{
					return "backgroundImage";
				}
			}

			// Token: 0x17000714 RID: 1812
			// (get) Token: 0x06001A26 RID: 6694 RVA: 0x0005EDFE File Offset: 0x0005CFFE
			public override string ussName
			{
				get
				{
					return "background-image";
				}
			}

			// Token: 0x17000715 RID: 1813
			// (get) Token: 0x06001A27 RID: 6695 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A28 RID: 6696 RVA: 0x0006856A File Offset: 0x0006676A
			public override Background GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).backgroundImage;
			}

			// Token: 0x06001A29 RID: 6697 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Background value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000358 RID: 856
		private class BackgroundPositionXProperty : ResolvedStyleAccessPropertyBag.ResolvedBackgroundPositionProperty
		{
			// Token: 0x17000716 RID: 1814
			// (get) Token: 0x06001A2B RID: 6699 RVA: 0x0005EE22 File Offset: 0x0005D022
			public override string Name
			{
				get
				{
					return "backgroundPositionX";
				}
			}

			// Token: 0x17000717 RID: 1815
			// (get) Token: 0x06001A2C RID: 6700 RVA: 0x0005EE29 File Offset: 0x0005D029
			public override string ussName
			{
				get
				{
					return "background-position-x";
				}
			}

			// Token: 0x17000718 RID: 1816
			// (get) Token: 0x06001A2D RID: 6701 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A2E RID: 6702 RVA: 0x0006857C File Offset: 0x0006677C
			public override BackgroundPosition GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).backgroundPositionX;
			}

			// Token: 0x06001A2F RID: 6703 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, BackgroundPosition value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000359 RID: 857
		private class BackgroundPositionYProperty : ResolvedStyleAccessPropertyBag.ResolvedBackgroundPositionProperty
		{
			// Token: 0x17000719 RID: 1817
			// (get) Token: 0x06001A31 RID: 6705 RVA: 0x0005EE4D File Offset: 0x0005D04D
			public override string Name
			{
				get
				{
					return "backgroundPositionY";
				}
			}

			// Token: 0x1700071A RID: 1818
			// (get) Token: 0x06001A32 RID: 6706 RVA: 0x0005EE54 File Offset: 0x0005D054
			public override string ussName
			{
				get
				{
					return "background-position-y";
				}
			}

			// Token: 0x1700071B RID: 1819
			// (get) Token: 0x06001A33 RID: 6707 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A34 RID: 6708 RVA: 0x0006858E File Offset: 0x0006678E
			public override BackgroundPosition GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).backgroundPositionY;
			}

			// Token: 0x06001A35 RID: 6709 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, BackgroundPosition value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200035A RID: 858
		private class BackgroundRepeatProperty : ResolvedStyleAccessPropertyBag.ResolvedBackgroundRepeatProperty
		{
			// Token: 0x1700071C RID: 1820
			// (get) Token: 0x06001A37 RID: 6711 RVA: 0x0005EE6F File Offset: 0x0005D06F
			public override string Name
			{
				get
				{
					return "backgroundRepeat";
				}
			}

			// Token: 0x1700071D RID: 1821
			// (get) Token: 0x06001A38 RID: 6712 RVA: 0x0005EE76 File Offset: 0x0005D076
			public override string ussName
			{
				get
				{
					return "background-repeat";
				}
			}

			// Token: 0x1700071E RID: 1822
			// (get) Token: 0x06001A39 RID: 6713 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A3A RID: 6714 RVA: 0x00068597 File Offset: 0x00066797
			public override BackgroundRepeat GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).backgroundRepeat;
			}

			// Token: 0x06001A3B RID: 6715 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, BackgroundRepeat value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200035B RID: 859
		private class BackgroundSizeProperty : ResolvedStyleAccessPropertyBag.ResolvedBackgroundSizeProperty
		{
			// Token: 0x1700071F RID: 1823
			// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0005EE9A File Offset: 0x0005D09A
			public override string Name
			{
				get
				{
					return "backgroundSize";
				}
			}

			// Token: 0x17000720 RID: 1824
			// (get) Token: 0x06001A3E RID: 6718 RVA: 0x0005EEA1 File Offset: 0x0005D0A1
			public override string ussName
			{
				get
				{
					return "background-size";
				}
			}

			// Token: 0x17000721 RID: 1825
			// (get) Token: 0x06001A3F RID: 6719 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A40 RID: 6720 RVA: 0x000685A9 File Offset: 0x000667A9
			public override BackgroundSize GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).backgroundSize;
			}

			// Token: 0x06001A41 RID: 6721 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, BackgroundSize value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200035C RID: 860
		private class BorderBottomColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x17000722 RID: 1826
			// (get) Token: 0x06001A43 RID: 6723 RVA: 0x0005EEC5 File Offset: 0x0005D0C5
			public override string Name
			{
				get
				{
					return "borderBottomColor";
				}
			}

			// Token: 0x17000723 RID: 1827
			// (get) Token: 0x06001A44 RID: 6724 RVA: 0x0005EECC File Offset: 0x0005D0CC
			public override string ussName
			{
				get
				{
					return "border-bottom-color";
				}
			}

			// Token: 0x17000724 RID: 1828
			// (get) Token: 0x06001A45 RID: 6725 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A46 RID: 6726 RVA: 0x000685BB File Offset: 0x000667BB
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderBottomColor;
			}

			// Token: 0x06001A47 RID: 6727 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200035D RID: 861
		private class BorderBottomLeftRadiusProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000725 RID: 1829
			// (get) Token: 0x06001A49 RID: 6729 RVA: 0x0005EEE7 File Offset: 0x0005D0E7
			public override string Name
			{
				get
				{
					return "borderBottomLeftRadius";
				}
			}

			// Token: 0x17000726 RID: 1830
			// (get) Token: 0x06001A4A RID: 6730 RVA: 0x0005EEEE File Offset: 0x0005D0EE
			public override string ussName
			{
				get
				{
					return "border-bottom-left-radius";
				}
			}

			// Token: 0x17000727 RID: 1831
			// (get) Token: 0x06001A4B RID: 6731 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A4C RID: 6732 RVA: 0x000685C4 File Offset: 0x000667C4
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderBottomLeftRadius;
			}

			// Token: 0x06001A4D RID: 6733 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200035E RID: 862
		private class BorderBottomRightRadiusProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000728 RID: 1832
			// (get) Token: 0x06001A4F RID: 6735 RVA: 0x0005EF12 File Offset: 0x0005D112
			public override string Name
			{
				get
				{
					return "borderBottomRightRadius";
				}
			}

			// Token: 0x17000729 RID: 1833
			// (get) Token: 0x06001A50 RID: 6736 RVA: 0x0005EF19 File Offset: 0x0005D119
			public override string ussName
			{
				get
				{
					return "border-bottom-right-radius";
				}
			}

			// Token: 0x1700072A RID: 1834
			// (get) Token: 0x06001A51 RID: 6737 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A52 RID: 6738 RVA: 0x000685D6 File Offset: 0x000667D6
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderBottomRightRadius;
			}

			// Token: 0x06001A53 RID: 6739 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200035F RID: 863
		private class BorderBottomWidthProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x1700072B RID: 1835
			// (get) Token: 0x06001A55 RID: 6741 RVA: 0x0005EF34 File Offset: 0x0005D134
			public override string Name
			{
				get
				{
					return "borderBottomWidth";
				}
			}

			// Token: 0x1700072C RID: 1836
			// (get) Token: 0x06001A56 RID: 6742 RVA: 0x0005EF3B File Offset: 0x0005D13B
			public override string ussName
			{
				get
				{
					return "border-bottom-width";
				}
			}

			// Token: 0x1700072D RID: 1837
			// (get) Token: 0x06001A57 RID: 6743 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A58 RID: 6744 RVA: 0x000685DF File Offset: 0x000667DF
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderBottomWidth;
			}

			// Token: 0x06001A59 RID: 6745 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000360 RID: 864
		private class BorderLeftColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x1700072E RID: 1838
			// (get) Token: 0x06001A5B RID: 6747 RVA: 0x0005EF5F File Offset: 0x0005D15F
			public override string Name
			{
				get
				{
					return "borderLeftColor";
				}
			}

			// Token: 0x1700072F RID: 1839
			// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0005EF66 File Offset: 0x0005D166
			public override string ussName
			{
				get
				{
					return "border-left-color";
				}
			}

			// Token: 0x17000730 RID: 1840
			// (get) Token: 0x06001A5D RID: 6749 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A5E RID: 6750 RVA: 0x000685E8 File Offset: 0x000667E8
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderLeftColor;
			}

			// Token: 0x06001A5F RID: 6751 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000361 RID: 865
		private class BorderLeftWidthProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000731 RID: 1841
			// (get) Token: 0x06001A61 RID: 6753 RVA: 0x0005EF81 File Offset: 0x0005D181
			public override string Name
			{
				get
				{
					return "borderLeftWidth";
				}
			}

			// Token: 0x17000732 RID: 1842
			// (get) Token: 0x06001A62 RID: 6754 RVA: 0x0005EF88 File Offset: 0x0005D188
			public override string ussName
			{
				get
				{
					return "border-left-width";
				}
			}

			// Token: 0x17000733 RID: 1843
			// (get) Token: 0x06001A63 RID: 6755 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A64 RID: 6756 RVA: 0x000685F1 File Offset: 0x000667F1
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderLeftWidth;
			}

			// Token: 0x06001A65 RID: 6757 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000362 RID: 866
		private class BorderRightColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x17000734 RID: 1844
			// (get) Token: 0x06001A67 RID: 6759 RVA: 0x0005EFA3 File Offset: 0x0005D1A3
			public override string Name
			{
				get
				{
					return "borderRightColor";
				}
			}

			// Token: 0x17000735 RID: 1845
			// (get) Token: 0x06001A68 RID: 6760 RVA: 0x0005EFAA File Offset: 0x0005D1AA
			public override string ussName
			{
				get
				{
					return "border-right-color";
				}
			}

			// Token: 0x17000736 RID: 1846
			// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A6A RID: 6762 RVA: 0x000685FA File Offset: 0x000667FA
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderRightColor;
			}

			// Token: 0x06001A6B RID: 6763 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000363 RID: 867
		private class BorderRightWidthProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000737 RID: 1847
			// (get) Token: 0x06001A6D RID: 6765 RVA: 0x0005EFC5 File Offset: 0x0005D1C5
			public override string Name
			{
				get
				{
					return "borderRightWidth";
				}
			}

			// Token: 0x17000738 RID: 1848
			// (get) Token: 0x06001A6E RID: 6766 RVA: 0x0005EFCC File Offset: 0x0005D1CC
			public override string ussName
			{
				get
				{
					return "border-right-width";
				}
			}

			// Token: 0x17000739 RID: 1849
			// (get) Token: 0x06001A6F RID: 6767 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A70 RID: 6768 RVA: 0x00068603 File Offset: 0x00066803
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderRightWidth;
			}

			// Token: 0x06001A71 RID: 6769 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000364 RID: 868
		private class BorderTopColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x1700073A RID: 1850
			// (get) Token: 0x06001A73 RID: 6771 RVA: 0x0005EFE7 File Offset: 0x0005D1E7
			public override string Name
			{
				get
				{
					return "borderTopColor";
				}
			}

			// Token: 0x1700073B RID: 1851
			// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0005EFEE File Offset: 0x0005D1EE
			public override string ussName
			{
				get
				{
					return "border-top-color";
				}
			}

			// Token: 0x1700073C RID: 1852
			// (get) Token: 0x06001A75 RID: 6773 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A76 RID: 6774 RVA: 0x0006860C File Offset: 0x0006680C
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderTopColor;
			}

			// Token: 0x06001A77 RID: 6775 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000365 RID: 869
		private class BorderTopLeftRadiusProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x1700073D RID: 1853
			// (get) Token: 0x06001A79 RID: 6777 RVA: 0x0005F009 File Offset: 0x0005D209
			public override string Name
			{
				get
				{
					return "borderTopLeftRadius";
				}
			}

			// Token: 0x1700073E RID: 1854
			// (get) Token: 0x06001A7A RID: 6778 RVA: 0x0005F010 File Offset: 0x0005D210
			public override string ussName
			{
				get
				{
					return "border-top-left-radius";
				}
			}

			// Token: 0x1700073F RID: 1855
			// (get) Token: 0x06001A7B RID: 6779 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A7C RID: 6780 RVA: 0x00068615 File Offset: 0x00066815
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderTopLeftRadius;
			}

			// Token: 0x06001A7D RID: 6781 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000366 RID: 870
		private class BorderTopRightRadiusProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000740 RID: 1856
			// (get) Token: 0x06001A7F RID: 6783 RVA: 0x0005F02B File Offset: 0x0005D22B
			public override string Name
			{
				get
				{
					return "borderTopRightRadius";
				}
			}

			// Token: 0x17000741 RID: 1857
			// (get) Token: 0x06001A80 RID: 6784 RVA: 0x0005F032 File Offset: 0x0005D232
			public override string ussName
			{
				get
				{
					return "border-top-right-radius";
				}
			}

			// Token: 0x17000742 RID: 1858
			// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A82 RID: 6786 RVA: 0x0006861E File Offset: 0x0006681E
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderTopRightRadius;
			}

			// Token: 0x06001A83 RID: 6787 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000367 RID: 871
		private class BorderTopWidthProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000743 RID: 1859
			// (get) Token: 0x06001A85 RID: 6789 RVA: 0x0005F04D File Offset: 0x0005D24D
			public override string Name
			{
				get
				{
					return "borderTopWidth";
				}
			}

			// Token: 0x17000744 RID: 1860
			// (get) Token: 0x06001A86 RID: 6790 RVA: 0x0005F054 File Offset: 0x0005D254
			public override string ussName
			{
				get
				{
					return "border-top-width";
				}
			}

			// Token: 0x17000745 RID: 1861
			// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A88 RID: 6792 RVA: 0x00068627 File Offset: 0x00066827
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderTopWidth;
			}

			// Token: 0x06001A89 RID: 6793 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000368 RID: 872
		private class BottomProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000746 RID: 1862
			// (get) Token: 0x06001A8B RID: 6795 RVA: 0x0005F06F File Offset: 0x0005D26F
			public override string Name
			{
				get
				{
					return "bottom";
				}
			}

			// Token: 0x17000747 RID: 1863
			// (get) Token: 0x06001A8C RID: 6796 RVA: 0x0005F06F File Offset: 0x0005D26F
			public override string ussName
			{
				get
				{
					return "bottom";
				}
			}

			// Token: 0x17000748 RID: 1864
			// (get) Token: 0x06001A8D RID: 6797 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A8E RID: 6798 RVA: 0x00068630 File Offset: 0x00066830
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).bottom;
			}

			// Token: 0x06001A8F RID: 6799 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000369 RID: 873
		private class ColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x17000749 RID: 1865
			// (get) Token: 0x06001A91 RID: 6801 RVA: 0x0005F08A File Offset: 0x0005D28A
			public override string Name
			{
				get
				{
					return "color";
				}
			}

			// Token: 0x1700074A RID: 1866
			// (get) Token: 0x06001A92 RID: 6802 RVA: 0x0005F08A File Offset: 0x0005D28A
			public override string ussName
			{
				get
				{
					return "color";
				}
			}

			// Token: 0x1700074B RID: 1867
			// (get) Token: 0x06001A93 RID: 6803 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A94 RID: 6804 RVA: 0x00068639 File Offset: 0x00066839
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).color;
			}

			// Token: 0x06001A95 RID: 6805 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200036A RID: 874
		private class DisplayProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<DisplayStyle>
		{
			// Token: 0x1700074C RID: 1868
			// (get) Token: 0x06001A97 RID: 6807 RVA: 0x0005F0C9 File Offset: 0x0005D2C9
			public override string Name
			{
				get
				{
					return "display";
				}
			}

			// Token: 0x1700074D RID: 1869
			// (get) Token: 0x06001A98 RID: 6808 RVA: 0x0005F0C9 File Offset: 0x0005D2C9
			public override string ussName
			{
				get
				{
					return "display";
				}
			}

			// Token: 0x1700074E RID: 1870
			// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001A9A RID: 6810 RVA: 0x00068642 File Offset: 0x00066842
			public override DisplayStyle GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).display;
			}

			// Token: 0x06001A9B RID: 6811 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, DisplayStyle value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200036B RID: 875
		private class FlexBasisProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleFloatProperty
		{
			// Token: 0x1700074F RID: 1871
			// (get) Token: 0x06001A9D RID: 6813 RVA: 0x0005F0ED File Offset: 0x0005D2ED
			public override string Name
			{
				get
				{
					return "flexBasis";
				}
			}

			// Token: 0x17000750 RID: 1872
			// (get) Token: 0x06001A9E RID: 6814 RVA: 0x0005F0F4 File Offset: 0x0005D2F4
			public override string ussName
			{
				get
				{
					return "flex-basis";
				}
			}

			// Token: 0x17000751 RID: 1873
			// (get) Token: 0x06001A9F RID: 6815 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001AA0 RID: 6816 RVA: 0x00068654 File Offset: 0x00066854
			public override StyleFloat GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).flexBasis;
			}

			// Token: 0x06001AA1 RID: 6817 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, StyleFloat value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200036C RID: 876
		private class FlexDirectionProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<FlexDirection>
		{
			// Token: 0x17000752 RID: 1874
			// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x0005F10F File Offset: 0x0005D30F
			public override string Name
			{
				get
				{
					return "flexDirection";
				}
			}

			// Token: 0x17000753 RID: 1875
			// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x0005F116 File Offset: 0x0005D316
			public override string ussName
			{
				get
				{
					return "flex-direction";
				}
			}

			// Token: 0x17000754 RID: 1876
			// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001AA6 RID: 6822 RVA: 0x00068666 File Offset: 0x00066866
			public override FlexDirection GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).flexDirection;
			}

			// Token: 0x06001AA7 RID: 6823 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, FlexDirection value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200036D RID: 877
		private class FlexGrowProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000755 RID: 1877
			// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x0005F13A File Offset: 0x0005D33A
			public override string Name
			{
				get
				{
					return "flexGrow";
				}
			}

			// Token: 0x17000756 RID: 1878
			// (get) Token: 0x06001AAA RID: 6826 RVA: 0x0005F141 File Offset: 0x0005D341
			public override string ussName
			{
				get
				{
					return "flex-grow";
				}
			}

			// Token: 0x17000757 RID: 1879
			// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001AAC RID: 6828 RVA: 0x00068678 File Offset: 0x00066878
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).flexGrow;
			}

			// Token: 0x06001AAD RID: 6829 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200036E RID: 878
		private class FlexShrinkProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000758 RID: 1880
			// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0005F15C File Offset: 0x0005D35C
			public override string Name
			{
				get
				{
					return "flexShrink";
				}
			}

			// Token: 0x17000759 RID: 1881
			// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x0005F163 File Offset: 0x0005D363
			public override string ussName
			{
				get
				{
					return "flex-shrink";
				}
			}

			// Token: 0x1700075A RID: 1882
			// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001AB2 RID: 6834 RVA: 0x00068681 File Offset: 0x00066881
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).flexShrink;
			}

			// Token: 0x06001AB3 RID: 6835 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200036F RID: 879
		private class FlexWrapProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<Wrap>
		{
			// Token: 0x1700075B RID: 1883
			// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0005F17E File Offset: 0x0005D37E
			public override string Name
			{
				get
				{
					return "flexWrap";
				}
			}

			// Token: 0x1700075C RID: 1884
			// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x0005F185 File Offset: 0x0005D385
			public override string ussName
			{
				get
				{
					return "flex-wrap";
				}
			}

			// Token: 0x1700075D RID: 1885
			// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001AB8 RID: 6840 RVA: 0x0006868A File Offset: 0x0006688A
			public override Wrap GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).flexWrap;
			}

			// Token: 0x06001AB9 RID: 6841 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Wrap value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000370 RID: 880
		private class FontSizeProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x1700075E RID: 1886
			// (get) Token: 0x06001ABB RID: 6843 RVA: 0x0005F1A9 File Offset: 0x0005D3A9
			public override string Name
			{
				get
				{
					return "fontSize";
				}
			}

			// Token: 0x1700075F RID: 1887
			// (get) Token: 0x06001ABC RID: 6844 RVA: 0x0005F1B0 File Offset: 0x0005D3B0
			public override string ussName
			{
				get
				{
					return "font-size";
				}
			}

			// Token: 0x17000760 RID: 1888
			// (get) Token: 0x06001ABD RID: 6845 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001ABE RID: 6846 RVA: 0x0006869C File Offset: 0x0006689C
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).fontSize;
			}

			// Token: 0x06001ABF RID: 6847 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000371 RID: 881
		private class HeightProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000761 RID: 1889
			// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x0005F1CB File Offset: 0x0005D3CB
			public override string Name
			{
				get
				{
					return "height";
				}
			}

			// Token: 0x17000762 RID: 1890
			// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x0005F1CB File Offset: 0x0005D3CB
			public override string ussName
			{
				get
				{
					return "height";
				}
			}

			// Token: 0x17000763 RID: 1891
			// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001AC4 RID: 6852 RVA: 0x000686A5 File Offset: 0x000668A5
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).height;
			}

			// Token: 0x06001AC5 RID: 6853 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000372 RID: 882
		private class JustifyContentProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<Justify>
		{
			// Token: 0x17000764 RID: 1892
			// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x0005F1E6 File Offset: 0x0005D3E6
			public override string Name
			{
				get
				{
					return "justifyContent";
				}
			}

			// Token: 0x17000765 RID: 1893
			// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x0005F1ED File Offset: 0x0005D3ED
			public override string ussName
			{
				get
				{
					return "justify-content";
				}
			}

			// Token: 0x17000766 RID: 1894
			// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001ACA RID: 6858 RVA: 0x000686AE File Offset: 0x000668AE
			public override Justify GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).justifyContent;
			}

			// Token: 0x06001ACB RID: 6859 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Justify value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000373 RID: 883
		private class LeftProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000767 RID: 1895
			// (get) Token: 0x06001ACD RID: 6861 RVA: 0x0005F211 File Offset: 0x0005D411
			public override string Name
			{
				get
				{
					return "left";
				}
			}

			// Token: 0x17000768 RID: 1896
			// (get) Token: 0x06001ACE RID: 6862 RVA: 0x0005F211 File Offset: 0x0005D411
			public override string ussName
			{
				get
				{
					return "left";
				}
			}

			// Token: 0x17000769 RID: 1897
			// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001AD0 RID: 6864 RVA: 0x000686C0 File Offset: 0x000668C0
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).left;
			}

			// Token: 0x06001AD1 RID: 6865 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000374 RID: 884
		private class LetterSpacingProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x1700076A RID: 1898
			// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x0005F22C File Offset: 0x0005D42C
			public override string Name
			{
				get
				{
					return "letterSpacing";
				}
			}

			// Token: 0x1700076B RID: 1899
			// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x0005F233 File Offset: 0x0005D433
			public override string ussName
			{
				get
				{
					return "letter-spacing";
				}
			}

			// Token: 0x1700076C RID: 1900
			// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001AD6 RID: 6870 RVA: 0x000686C9 File Offset: 0x000668C9
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).letterSpacing;
			}

			// Token: 0x06001AD7 RID: 6871 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000375 RID: 885
		private class MarginBottomProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x1700076D RID: 1901
			// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x0005F24E File Offset: 0x0005D44E
			public override string Name
			{
				get
				{
					return "marginBottom";
				}
			}

			// Token: 0x1700076E RID: 1902
			// (get) Token: 0x06001ADA RID: 6874 RVA: 0x0005F255 File Offset: 0x0005D455
			public override string ussName
			{
				get
				{
					return "margin-bottom";
				}
			}

			// Token: 0x1700076F RID: 1903
			// (get) Token: 0x06001ADB RID: 6875 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001ADC RID: 6876 RVA: 0x000686D2 File Offset: 0x000668D2
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).marginBottom;
			}

			// Token: 0x06001ADD RID: 6877 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000376 RID: 886
		private class MarginLeftProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000770 RID: 1904
			// (get) Token: 0x06001ADF RID: 6879 RVA: 0x0005F270 File Offset: 0x0005D470
			public override string Name
			{
				get
				{
					return "marginLeft";
				}
			}

			// Token: 0x17000771 RID: 1905
			// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x0005F277 File Offset: 0x0005D477
			public override string ussName
			{
				get
				{
					return "margin-left";
				}
			}

			// Token: 0x17000772 RID: 1906
			// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001AE2 RID: 6882 RVA: 0x000686DB File Offset: 0x000668DB
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).marginLeft;
			}

			// Token: 0x06001AE3 RID: 6883 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000377 RID: 887
		private class MarginRightProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000773 RID: 1907
			// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x0005F292 File Offset: 0x0005D492
			public override string Name
			{
				get
				{
					return "marginRight";
				}
			}

			// Token: 0x17000774 RID: 1908
			// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x0005F299 File Offset: 0x0005D499
			public override string ussName
			{
				get
				{
					return "margin-right";
				}
			}

			// Token: 0x17000775 RID: 1909
			// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001AE8 RID: 6888 RVA: 0x000686E4 File Offset: 0x000668E4
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).marginRight;
			}

			// Token: 0x06001AE9 RID: 6889 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000378 RID: 888
		private class MarginTopProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000776 RID: 1910
			// (get) Token: 0x06001AEB RID: 6891 RVA: 0x0005F2B4 File Offset: 0x0005D4B4
			public override string Name
			{
				get
				{
					return "marginTop";
				}
			}

			// Token: 0x17000777 RID: 1911
			// (get) Token: 0x06001AEC RID: 6892 RVA: 0x0005F2BB File Offset: 0x0005D4BB
			public override string ussName
			{
				get
				{
					return "margin-top";
				}
			}

			// Token: 0x17000778 RID: 1912
			// (get) Token: 0x06001AED RID: 6893 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001AEE RID: 6894 RVA: 0x000686ED File Offset: 0x000668ED
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).marginTop;
			}

			// Token: 0x06001AEF RID: 6895 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000379 RID: 889
		private class MaxHeightProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleFloatProperty
		{
			// Token: 0x17000779 RID: 1913
			// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x0005F2D6 File Offset: 0x0005D4D6
			public override string Name
			{
				get
				{
					return "maxHeight";
				}
			}

			// Token: 0x1700077A RID: 1914
			// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x0005F2DD File Offset: 0x0005D4DD
			public override string ussName
			{
				get
				{
					return "max-height";
				}
			}

			// Token: 0x1700077B RID: 1915
			// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001AF4 RID: 6900 RVA: 0x000686F6 File Offset: 0x000668F6
			public override StyleFloat GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).maxHeight;
			}

			// Token: 0x06001AF5 RID: 6901 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, StyleFloat value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200037A RID: 890
		private class MaxWidthProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleFloatProperty
		{
			// Token: 0x1700077C RID: 1916
			// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x0005F2F8 File Offset: 0x0005D4F8
			public override string Name
			{
				get
				{
					return "maxWidth";
				}
			}

			// Token: 0x1700077D RID: 1917
			// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x0005F2FF File Offset: 0x0005D4FF
			public override string ussName
			{
				get
				{
					return "max-width";
				}
			}

			// Token: 0x1700077E RID: 1918
			// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001AFA RID: 6906 RVA: 0x000686FF File Offset: 0x000668FF
			public override StyleFloat GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).maxWidth;
			}

			// Token: 0x06001AFB RID: 6907 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, StyleFloat value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200037B RID: 891
		private class MinHeightProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleFloatProperty
		{
			// Token: 0x1700077F RID: 1919
			// (get) Token: 0x06001AFD RID: 6909 RVA: 0x0005F31A File Offset: 0x0005D51A
			public override string Name
			{
				get
				{
					return "minHeight";
				}
			}

			// Token: 0x17000780 RID: 1920
			// (get) Token: 0x06001AFE RID: 6910 RVA: 0x0005F321 File Offset: 0x0005D521
			public override string ussName
			{
				get
				{
					return "min-height";
				}
			}

			// Token: 0x17000781 RID: 1921
			// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B00 RID: 6912 RVA: 0x00068708 File Offset: 0x00066908
			public override StyleFloat GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).minHeight;
			}

			// Token: 0x06001B01 RID: 6913 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, StyleFloat value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200037C RID: 892
		private class MinWidthProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleFloatProperty
		{
			// Token: 0x17000782 RID: 1922
			// (get) Token: 0x06001B03 RID: 6915 RVA: 0x0005F33C File Offset: 0x0005D53C
			public override string Name
			{
				get
				{
					return "minWidth";
				}
			}

			// Token: 0x17000783 RID: 1923
			// (get) Token: 0x06001B04 RID: 6916 RVA: 0x0005F343 File Offset: 0x0005D543
			public override string ussName
			{
				get
				{
					return "min-width";
				}
			}

			// Token: 0x17000784 RID: 1924
			// (get) Token: 0x06001B05 RID: 6917 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B06 RID: 6918 RVA: 0x00068711 File Offset: 0x00066911
			public override StyleFloat GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).minWidth;
			}

			// Token: 0x06001B07 RID: 6919 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, StyleFloat value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200037D RID: 893
		private class OpacityProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000785 RID: 1925
			// (get) Token: 0x06001B09 RID: 6921 RVA: 0x0005F35E File Offset: 0x0005D55E
			public override string Name
			{
				get
				{
					return "opacity";
				}
			}

			// Token: 0x17000786 RID: 1926
			// (get) Token: 0x06001B0A RID: 6922 RVA: 0x0005F35E File Offset: 0x0005D55E
			public override string ussName
			{
				get
				{
					return "opacity";
				}
			}

			// Token: 0x17000787 RID: 1927
			// (get) Token: 0x06001B0B RID: 6923 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B0C RID: 6924 RVA: 0x0006871A File Offset: 0x0006691A
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).opacity;
			}

			// Token: 0x06001B0D RID: 6925 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200037E RID: 894
		private class PaddingBottomProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000788 RID: 1928
			// (get) Token: 0x06001B0F RID: 6927 RVA: 0x0005F39D File Offset: 0x0005D59D
			public override string Name
			{
				get
				{
					return "paddingBottom";
				}
			}

			// Token: 0x17000789 RID: 1929
			// (get) Token: 0x06001B10 RID: 6928 RVA: 0x0005F3A4 File Offset: 0x0005D5A4
			public override string ussName
			{
				get
				{
					return "padding-bottom";
				}
			}

			// Token: 0x1700078A RID: 1930
			// (get) Token: 0x06001B11 RID: 6929 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B12 RID: 6930 RVA: 0x00068723 File Offset: 0x00066923
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).paddingBottom;
			}

			// Token: 0x06001B13 RID: 6931 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200037F RID: 895
		private class PaddingLeftProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x1700078B RID: 1931
			// (get) Token: 0x06001B15 RID: 6933 RVA: 0x0005F3BF File Offset: 0x0005D5BF
			public override string Name
			{
				get
				{
					return "paddingLeft";
				}
			}

			// Token: 0x1700078C RID: 1932
			// (get) Token: 0x06001B16 RID: 6934 RVA: 0x0005F3C6 File Offset: 0x0005D5C6
			public override string ussName
			{
				get
				{
					return "padding-left";
				}
			}

			// Token: 0x1700078D RID: 1933
			// (get) Token: 0x06001B17 RID: 6935 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B18 RID: 6936 RVA: 0x0006872C File Offset: 0x0006692C
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).paddingLeft;
			}

			// Token: 0x06001B19 RID: 6937 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000380 RID: 896
		private class PaddingRightProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x1700078E RID: 1934
			// (get) Token: 0x06001B1B RID: 6939 RVA: 0x0005F3E1 File Offset: 0x0005D5E1
			public override string Name
			{
				get
				{
					return "paddingRight";
				}
			}

			// Token: 0x1700078F RID: 1935
			// (get) Token: 0x06001B1C RID: 6940 RVA: 0x0005F3E8 File Offset: 0x0005D5E8
			public override string ussName
			{
				get
				{
					return "padding-right";
				}
			}

			// Token: 0x17000790 RID: 1936
			// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B1E RID: 6942 RVA: 0x00068735 File Offset: 0x00066935
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).paddingRight;
			}

			// Token: 0x06001B1F RID: 6943 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000381 RID: 897
		private class PaddingTopProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000791 RID: 1937
			// (get) Token: 0x06001B21 RID: 6945 RVA: 0x0005F403 File Offset: 0x0005D603
			public override string Name
			{
				get
				{
					return "paddingTop";
				}
			}

			// Token: 0x17000792 RID: 1938
			// (get) Token: 0x06001B22 RID: 6946 RVA: 0x0005F40A File Offset: 0x0005D60A
			public override string ussName
			{
				get
				{
					return "padding-top";
				}
			}

			// Token: 0x17000793 RID: 1939
			// (get) Token: 0x06001B23 RID: 6947 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B24 RID: 6948 RVA: 0x0006873E File Offset: 0x0006693E
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).paddingTop;
			}

			// Token: 0x06001B25 RID: 6949 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000382 RID: 898
		private class PositionProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<Position>
		{
			// Token: 0x17000794 RID: 1940
			// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0005F425 File Offset: 0x0005D625
			public override string Name
			{
				get
				{
					return "position";
				}
			}

			// Token: 0x17000795 RID: 1941
			// (get) Token: 0x06001B28 RID: 6952 RVA: 0x0005F425 File Offset: 0x0005D625
			public override string ussName
			{
				get
				{
					return "position";
				}
			}

			// Token: 0x17000796 RID: 1942
			// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B2A RID: 6954 RVA: 0x00068747 File Offset: 0x00066947
			public override Position GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).position;
			}

			// Token: 0x06001B2B RID: 6955 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Position value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000383 RID: 899
		private class RightProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000797 RID: 1943
			// (get) Token: 0x06001B2D RID: 6957 RVA: 0x0005F449 File Offset: 0x0005D649
			public override string Name
			{
				get
				{
					return "right";
				}
			}

			// Token: 0x17000798 RID: 1944
			// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0005F449 File Offset: 0x0005D649
			public override string ussName
			{
				get
				{
					return "right";
				}
			}

			// Token: 0x17000799 RID: 1945
			// (get) Token: 0x06001B2F RID: 6959 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B30 RID: 6960 RVA: 0x00068759 File Offset: 0x00066959
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).right;
			}

			// Token: 0x06001B31 RID: 6961 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000384 RID: 900
		private class RotateProperty : ResolvedStyleAccessPropertyBag.ResolvedRotateProperty
		{
			// Token: 0x1700079A RID: 1946
			// (get) Token: 0x06001B33 RID: 6963 RVA: 0x0005F464 File Offset: 0x0005D664
			public override string Name
			{
				get
				{
					return "rotate";
				}
			}

			// Token: 0x1700079B RID: 1947
			// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0005F464 File Offset: 0x0005D664
			public override string ussName
			{
				get
				{
					return "rotate";
				}
			}

			// Token: 0x1700079C RID: 1948
			// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B36 RID: 6966 RVA: 0x00068762 File Offset: 0x00066962
			public override Rotate GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).rotate;
			}

			// Token: 0x06001B37 RID: 6967 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Rotate value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000385 RID: 901
		private class ScaleProperty : ResolvedStyleAccessPropertyBag.ResolvedScaleProperty
		{
			// Token: 0x1700079D RID: 1949
			// (get) Token: 0x06001B39 RID: 6969 RVA: 0x0005F488 File Offset: 0x0005D688
			public override string Name
			{
				get
				{
					return "scale";
				}
			}

			// Token: 0x1700079E RID: 1950
			// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0005F488 File Offset: 0x0005D688
			public override string ussName
			{
				get
				{
					return "scale";
				}
			}

			// Token: 0x1700079F RID: 1951
			// (get) Token: 0x06001B3B RID: 6971 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B3C RID: 6972 RVA: 0x00068774 File Offset: 0x00066974
			public override Scale GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).scale;
			}

			// Token: 0x06001B3D RID: 6973 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Scale value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000386 RID: 902
		private class TextOverflowProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<TextOverflow>
		{
			// Token: 0x170007A0 RID: 1952
			// (get) Token: 0x06001B3F RID: 6975 RVA: 0x0005F4AC File Offset: 0x0005D6AC
			public override string Name
			{
				get
				{
					return "textOverflow";
				}
			}

			// Token: 0x170007A1 RID: 1953
			// (get) Token: 0x06001B40 RID: 6976 RVA: 0x0005F4B3 File Offset: 0x0005D6B3
			public override string ussName
			{
				get
				{
					return "text-overflow";
				}
			}

			// Token: 0x170007A2 RID: 1954
			// (get) Token: 0x06001B41 RID: 6977 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B42 RID: 6978 RVA: 0x00068786 File Offset: 0x00066986
			public override TextOverflow GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).textOverflow;
			}

			// Token: 0x06001B43 RID: 6979 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, TextOverflow value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000387 RID: 903
		private class TopProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170007A3 RID: 1955
			// (get) Token: 0x06001B45 RID: 6981 RVA: 0x0005F502 File Offset: 0x0005D702
			public override string Name
			{
				get
				{
					return "top";
				}
			}

			// Token: 0x170007A4 RID: 1956
			// (get) Token: 0x06001B46 RID: 6982 RVA: 0x0005F502 File Offset: 0x0005D702
			public override string ussName
			{
				get
				{
					return "top";
				}
			}

			// Token: 0x170007A5 RID: 1957
			// (get) Token: 0x06001B47 RID: 6983 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B48 RID: 6984 RVA: 0x00068798 File Offset: 0x00066998
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).top;
			}

			// Token: 0x06001B49 RID: 6985 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000388 RID: 904
		private class TransformOriginProperty : ResolvedStyleAccessPropertyBag.ResolvedVector3Property
		{
			// Token: 0x170007A6 RID: 1958
			// (get) Token: 0x06001B4B RID: 6987 RVA: 0x0005F51D File Offset: 0x0005D71D
			public override string Name
			{
				get
				{
					return "transformOrigin";
				}
			}

			// Token: 0x170007A7 RID: 1959
			// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0005F524 File Offset: 0x0005D724
			public override string ussName
			{
				get
				{
					return "transform-origin";
				}
			}

			// Token: 0x170007A8 RID: 1960
			// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B4E RID: 6990 RVA: 0x000687A1 File Offset: 0x000669A1
			public override Vector3 GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).transformOrigin;
			}

			// Token: 0x06001B4F RID: 6991 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Vector3 value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000389 RID: 905
		private class TransitionDelayProperty : ResolvedStyleAccessPropertyBag.ResolvedListProperty<TimeValue>
		{
			// Token: 0x170007A9 RID: 1961
			// (get) Token: 0x06001B51 RID: 6993 RVA: 0x0005F548 File Offset: 0x0005D748
			public override string Name
			{
				get
				{
					return "transitionDelay";
				}
			}

			// Token: 0x170007AA RID: 1962
			// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0005F54F File Offset: 0x0005D74F
			public override string ussName
			{
				get
				{
					return "transition-delay";
				}
			}

			// Token: 0x170007AB RID: 1963
			// (get) Token: 0x06001B53 RID: 6995 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B54 RID: 6996 RVA: 0x000687B3 File Offset: 0x000669B3
			public override IEnumerable<TimeValue> GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).transitionDelay;
			}

			// Token: 0x06001B55 RID: 6997 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, IEnumerable<TimeValue> value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200038A RID: 906
		private class TransitionDurationProperty : ResolvedStyleAccessPropertyBag.ResolvedListProperty<TimeValue>
		{
			// Token: 0x170007AC RID: 1964
			// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0005F573 File Offset: 0x0005D773
			public override string Name
			{
				get
				{
					return "transitionDuration";
				}
			}

			// Token: 0x170007AD RID: 1965
			// (get) Token: 0x06001B58 RID: 7000 RVA: 0x0005F57A File Offset: 0x0005D77A
			public override string ussName
			{
				get
				{
					return "transition-duration";
				}
			}

			// Token: 0x170007AE RID: 1966
			// (get) Token: 0x06001B59 RID: 7001 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B5A RID: 7002 RVA: 0x000687C5 File Offset: 0x000669C5
			public override IEnumerable<TimeValue> GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).transitionDuration;
			}

			// Token: 0x06001B5B RID: 7003 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, IEnumerable<TimeValue> value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200038B RID: 907
		private class TransitionPropertyProperty : ResolvedStyleAccessPropertyBag.ResolvedListProperty<StylePropertyName>
		{
			// Token: 0x170007AF RID: 1967
			// (get) Token: 0x06001B5D RID: 7005 RVA: 0x0005F595 File Offset: 0x0005D795
			public override string Name
			{
				get
				{
					return "transitionProperty";
				}
			}

			// Token: 0x170007B0 RID: 1968
			// (get) Token: 0x06001B5E RID: 7006 RVA: 0x0005F59C File Offset: 0x0005D79C
			public override string ussName
			{
				get
				{
					return "transition-property";
				}
			}

			// Token: 0x170007B1 RID: 1969
			// (get) Token: 0x06001B5F RID: 7007 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B60 RID: 7008 RVA: 0x000687CE File Offset: 0x000669CE
			public override IEnumerable<StylePropertyName> GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).transitionProperty;
			}

			// Token: 0x06001B61 RID: 7009 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, IEnumerable<StylePropertyName> value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200038C RID: 908
		private class TransitionTimingFunctionProperty : ResolvedStyleAccessPropertyBag.ResolvedListProperty<EasingFunction>
		{
			// Token: 0x170007B2 RID: 1970
			// (get) Token: 0x06001B63 RID: 7011 RVA: 0x0005F5C0 File Offset: 0x0005D7C0
			public override string Name
			{
				get
				{
					return "transitionTimingFunction";
				}
			}

			// Token: 0x170007B3 RID: 1971
			// (get) Token: 0x06001B64 RID: 7012 RVA: 0x0005F5C7 File Offset: 0x0005D7C7
			public override string ussName
			{
				get
				{
					return "transition-timing-function";
				}
			}

			// Token: 0x170007B4 RID: 1972
			// (get) Token: 0x06001B65 RID: 7013 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B66 RID: 7014 RVA: 0x000687E0 File Offset: 0x000669E0
			public override IEnumerable<EasingFunction> GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).transitionTimingFunction;
			}

			// Token: 0x06001B67 RID: 7015 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, IEnumerable<EasingFunction> value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200038D RID: 909
		private class TranslateProperty : ResolvedStyleAccessPropertyBag.ResolvedVector3Property
		{
			// Token: 0x170007B5 RID: 1973
			// (get) Token: 0x06001B69 RID: 7017 RVA: 0x0005F5EB File Offset: 0x0005D7EB
			public override string Name
			{
				get
				{
					return "translate";
				}
			}

			// Token: 0x170007B6 RID: 1974
			// (get) Token: 0x06001B6A RID: 7018 RVA: 0x0005F5EB File Offset: 0x0005D7EB
			public override string ussName
			{
				get
				{
					return "translate";
				}
			}

			// Token: 0x170007B7 RID: 1975
			// (get) Token: 0x06001B6B RID: 7019 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B6C RID: 7020 RVA: 0x000687F2 File Offset: 0x000669F2
			public override Vector3 GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).translate;
			}

			// Token: 0x06001B6D RID: 7021 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Vector3 value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200038E RID: 910
		private class UnityBackgroundImageTintColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x170007B8 RID: 1976
			// (get) Token: 0x06001B6F RID: 7023 RVA: 0x0005F60F File Offset: 0x0005D80F
			public override string Name
			{
				get
				{
					return "unityBackgroundImageTintColor";
				}
			}

			// Token: 0x170007B9 RID: 1977
			// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0005F616 File Offset: 0x0005D816
			public override string ussName
			{
				get
				{
					return "-unity-background-image-tint-color";
				}
			}

			// Token: 0x170007BA RID: 1978
			// (get) Token: 0x06001B71 RID: 7025 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B72 RID: 7026 RVA: 0x000687FB File Offset: 0x000669FB
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityBackgroundImageTintColor;
			}

			// Token: 0x06001B73 RID: 7027 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200038F RID: 911
		private class UnityEditorTextRenderingModeProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<EditorTextRenderingMode>
		{
			// Token: 0x170007BB RID: 1979
			// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0005F631 File Offset: 0x0005D831
			public override string Name
			{
				get
				{
					return "unityEditorTextRenderingMode";
				}
			}

			// Token: 0x170007BC RID: 1980
			// (get) Token: 0x06001B76 RID: 7030 RVA: 0x0005F638 File Offset: 0x0005D838
			public override string ussName
			{
				get
				{
					return "-unity-editor-text-rendering-mode";
				}
			}

			// Token: 0x170007BD RID: 1981
			// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B78 RID: 7032 RVA: 0x00068804 File Offset: 0x00066A04
			public override EditorTextRenderingMode GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityEditorTextRenderingMode;
			}

			// Token: 0x06001B79 RID: 7033 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, EditorTextRenderingMode value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000390 RID: 912
		private class UnityFontProperty : ResolvedStyleAccessPropertyBag.ResolvedFontProperty
		{
			// Token: 0x170007BE RID: 1982
			// (get) Token: 0x06001B7B RID: 7035 RVA: 0x0005F65C File Offset: 0x0005D85C
			public override string Name
			{
				get
				{
					return "unityFont";
				}
			}

			// Token: 0x170007BF RID: 1983
			// (get) Token: 0x06001B7C RID: 7036 RVA: 0x0005F663 File Offset: 0x0005D863
			public override string ussName
			{
				get
				{
					return "-unity-font";
				}
			}

			// Token: 0x170007C0 RID: 1984
			// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B7E RID: 7038 RVA: 0x00068816 File Offset: 0x00066A16
			public override Font GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityFont;
			}

			// Token: 0x06001B7F RID: 7039 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Font value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000391 RID: 913
		private class UnityFontDefinitionProperty : ResolvedStyleAccessPropertyBag.ResolvedFontDefinitionProperty
		{
			// Token: 0x170007C1 RID: 1985
			// (get) Token: 0x06001B81 RID: 7041 RVA: 0x0005F687 File Offset: 0x0005D887
			public override string Name
			{
				get
				{
					return "unityFontDefinition";
				}
			}

			// Token: 0x170007C2 RID: 1986
			// (get) Token: 0x06001B82 RID: 7042 RVA: 0x0005F68E File Offset: 0x0005D88E
			public override string ussName
			{
				get
				{
					return "-unity-font-definition";
				}
			}

			// Token: 0x170007C3 RID: 1987
			// (get) Token: 0x06001B83 RID: 7043 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B84 RID: 7044 RVA: 0x00068828 File Offset: 0x00066A28
			public override FontDefinition GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityFontDefinition;
			}

			// Token: 0x06001B85 RID: 7045 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, FontDefinition value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000392 RID: 914
		private class UnityFontStyleAndWeightProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<FontStyle>
		{
			// Token: 0x170007C4 RID: 1988
			// (get) Token: 0x06001B87 RID: 7047 RVA: 0x0005F6B2 File Offset: 0x0005D8B2
			public override string Name
			{
				get
				{
					return "unityFontStyleAndWeight";
				}
			}

			// Token: 0x170007C5 RID: 1989
			// (get) Token: 0x06001B88 RID: 7048 RVA: 0x0005F6B9 File Offset: 0x0005D8B9
			public override string ussName
			{
				get
				{
					return "-unity-font-style";
				}
			}

			// Token: 0x170007C6 RID: 1990
			// (get) Token: 0x06001B89 RID: 7049 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B8A RID: 7050 RVA: 0x0006883A File Offset: 0x00066A3A
			public override FontStyle GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityFontStyleAndWeight;
			}

			// Token: 0x06001B8B RID: 7051 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, FontStyle value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000393 RID: 915
		private class UnityParagraphSpacingProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170007C7 RID: 1991
			// (get) Token: 0x06001B8D RID: 7053 RVA: 0x0005F708 File Offset: 0x0005D908
			public override string Name
			{
				get
				{
					return "unityParagraphSpacing";
				}
			}

			// Token: 0x170007C8 RID: 1992
			// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0005F70F File Offset: 0x0005D90F
			public override string ussName
			{
				get
				{
					return "-unity-paragraph-spacing";
				}
			}

			// Token: 0x170007C9 RID: 1993
			// (get) Token: 0x06001B8F RID: 7055 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B90 RID: 7056 RVA: 0x0006884C File Offset: 0x00066A4C
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityParagraphSpacing;
			}

			// Token: 0x06001B91 RID: 7057 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000394 RID: 916
		private class UnitySliceBottomProperty : ResolvedStyleAccessPropertyBag.ResolvedIntProperty
		{
			// Token: 0x170007CA RID: 1994
			// (get) Token: 0x06001B93 RID: 7059 RVA: 0x0005F72A File Offset: 0x0005D92A
			public override string Name
			{
				get
				{
					return "unitySliceBottom";
				}
			}

			// Token: 0x170007CB RID: 1995
			// (get) Token: 0x06001B94 RID: 7060 RVA: 0x0005F731 File Offset: 0x0005D931
			public override string ussName
			{
				get
				{
					return "-unity-slice-bottom";
				}
			}

			// Token: 0x170007CC RID: 1996
			// (get) Token: 0x06001B95 RID: 7061 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B96 RID: 7062 RVA: 0x00068855 File Offset: 0x00066A55
			public override int GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unitySliceBottom;
			}

			// Token: 0x06001B97 RID: 7063 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, int value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000395 RID: 917
		private class UnitySliceLeftProperty : ResolvedStyleAccessPropertyBag.ResolvedIntProperty
		{
			// Token: 0x170007CD RID: 1997
			// (get) Token: 0x06001B99 RID: 7065 RVA: 0x0005F755 File Offset: 0x0005D955
			public override string Name
			{
				get
				{
					return "unitySliceLeft";
				}
			}

			// Token: 0x170007CE RID: 1998
			// (get) Token: 0x06001B9A RID: 7066 RVA: 0x0005F75C File Offset: 0x0005D95C
			public override string ussName
			{
				get
				{
					return "-unity-slice-left";
				}
			}

			// Token: 0x170007CF RID: 1999
			// (get) Token: 0x06001B9B RID: 7067 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B9C RID: 7068 RVA: 0x00068867 File Offset: 0x00066A67
			public override int GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unitySliceLeft;
			}

			// Token: 0x06001B9D RID: 7069 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, int value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000396 RID: 918
		private class UnitySliceRightProperty : ResolvedStyleAccessPropertyBag.ResolvedIntProperty
		{
			// Token: 0x170007D0 RID: 2000
			// (get) Token: 0x06001B9F RID: 7071 RVA: 0x0005F777 File Offset: 0x0005D977
			public override string Name
			{
				get
				{
					return "unitySliceRight";
				}
			}

			// Token: 0x170007D1 RID: 2001
			// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0005F77E File Offset: 0x0005D97E
			public override string ussName
			{
				get
				{
					return "-unity-slice-right";
				}
			}

			// Token: 0x170007D2 RID: 2002
			// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001BA2 RID: 7074 RVA: 0x00068870 File Offset: 0x00066A70
			public override int GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unitySliceRight;
			}

			// Token: 0x06001BA3 RID: 7075 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, int value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000397 RID: 919
		private class UnitySliceScaleProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170007D3 RID: 2003
			// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x0005F799 File Offset: 0x0005D999
			public override string Name
			{
				get
				{
					return "unitySliceScale";
				}
			}

			// Token: 0x170007D4 RID: 2004
			// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x0005F7A0 File Offset: 0x0005D9A0
			public override string ussName
			{
				get
				{
					return "-unity-slice-scale";
				}
			}

			// Token: 0x170007D5 RID: 2005
			// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001BA8 RID: 7080 RVA: 0x00068879 File Offset: 0x00066A79
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unitySliceScale;
			}

			// Token: 0x06001BA9 RID: 7081 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000398 RID: 920
		private class UnitySliceTopProperty : ResolvedStyleAccessPropertyBag.ResolvedIntProperty
		{
			// Token: 0x170007D6 RID: 2006
			// (get) Token: 0x06001BAB RID: 7083 RVA: 0x0005F7BB File Offset: 0x0005D9BB
			public override string Name
			{
				get
				{
					return "unitySliceTop";
				}
			}

			// Token: 0x170007D7 RID: 2007
			// (get) Token: 0x06001BAC RID: 7084 RVA: 0x0005F7C2 File Offset: 0x0005D9C2
			public override string ussName
			{
				get
				{
					return "-unity-slice-top";
				}
			}

			// Token: 0x170007D8 RID: 2008
			// (get) Token: 0x06001BAD RID: 7085 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001BAE RID: 7086 RVA: 0x00068882 File Offset: 0x00066A82
			public override int GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unitySliceTop;
			}

			// Token: 0x06001BAF RID: 7087 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, int value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000399 RID: 921
		private class UnityTextAlignProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<TextAnchor>
		{
			// Token: 0x170007D9 RID: 2009
			// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x0005F7DD File Offset: 0x0005D9DD
			public override string Name
			{
				get
				{
					return "unityTextAlign";
				}
			}

			// Token: 0x170007DA RID: 2010
			// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x0005F7E4 File Offset: 0x0005D9E4
			public override string ussName
			{
				get
				{
					return "-unity-text-align";
				}
			}

			// Token: 0x170007DB RID: 2011
			// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001BB4 RID: 7092 RVA: 0x0006888B File Offset: 0x00066A8B
			public override TextAnchor GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityTextAlign;
			}

			// Token: 0x06001BB5 RID: 7093 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, TextAnchor value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200039A RID: 922
		private class UnityTextGeneratorProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<TextGeneratorType>
		{
			// Token: 0x170007DC RID: 2012
			// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x0005F808 File Offset: 0x0005DA08
			public override string Name
			{
				get
				{
					return "unityTextGenerator";
				}
			}

			// Token: 0x170007DD RID: 2013
			// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0005F80F File Offset: 0x0005DA0F
			public override string ussName
			{
				get
				{
					return "-unity-text-generator";
				}
			}

			// Token: 0x170007DE RID: 2014
			// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001BBA RID: 7098 RVA: 0x0006889D File Offset: 0x00066A9D
			public override TextGeneratorType GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityTextGenerator;
			}

			// Token: 0x06001BBB RID: 7099 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, TextGeneratorType value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200039B RID: 923
		private class UnityTextOutlineColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x170007DF RID: 2015
			// (get) Token: 0x06001BBD RID: 7101 RVA: 0x0005F833 File Offset: 0x0005DA33
			public override string Name
			{
				get
				{
					return "unityTextOutlineColor";
				}
			}

			// Token: 0x170007E0 RID: 2016
			// (get) Token: 0x06001BBE RID: 7102 RVA: 0x0005F83A File Offset: 0x0005DA3A
			public override string ussName
			{
				get
				{
					return "-unity-text-outline-color";
				}
			}

			// Token: 0x170007E1 RID: 2017
			// (get) Token: 0x06001BBF RID: 7103 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001BC0 RID: 7104 RVA: 0x000688AF File Offset: 0x00066AAF
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityTextOutlineColor;
			}

			// Token: 0x06001BC1 RID: 7105 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200039C RID: 924
		private class UnityTextOutlineWidthProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170007E2 RID: 2018
			// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x0005F855 File Offset: 0x0005DA55
			public override string Name
			{
				get
				{
					return "unityTextOutlineWidth";
				}
			}

			// Token: 0x170007E3 RID: 2019
			// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x0005F85C File Offset: 0x0005DA5C
			public override string ussName
			{
				get
				{
					return "-unity-text-outline-width";
				}
			}

			// Token: 0x170007E4 RID: 2020
			// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001BC6 RID: 7110 RVA: 0x000688B8 File Offset: 0x00066AB8
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityTextOutlineWidth;
			}

			// Token: 0x06001BC7 RID: 7111 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200039D RID: 925
		private class UnityTextOverflowPositionProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<TextOverflowPosition>
		{
			// Token: 0x170007E5 RID: 2021
			// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x0005F877 File Offset: 0x0005DA77
			public override string Name
			{
				get
				{
					return "unityTextOverflowPosition";
				}
			}

			// Token: 0x170007E6 RID: 2022
			// (get) Token: 0x06001BCA RID: 7114 RVA: 0x0005F87E File Offset: 0x0005DA7E
			public override string ussName
			{
				get
				{
					return "-unity-text-overflow-position";
				}
			}

			// Token: 0x170007E7 RID: 2023
			// (get) Token: 0x06001BCB RID: 7115 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001BCC RID: 7116 RVA: 0x000688C1 File Offset: 0x00066AC1
			public override TextOverflowPosition GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityTextOverflowPosition;
			}

			// Token: 0x06001BCD RID: 7117 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, TextOverflowPosition value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200039E RID: 926
		private class VisibilityProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<Visibility>
		{
			// Token: 0x170007E8 RID: 2024
			// (get) Token: 0x06001BCF RID: 7119 RVA: 0x0005F8A2 File Offset: 0x0005DAA2
			public override string Name
			{
				get
				{
					return "visibility";
				}
			}

			// Token: 0x170007E9 RID: 2025
			// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x0005F8A2 File Offset: 0x0005DAA2
			public override string ussName
			{
				get
				{
					return "visibility";
				}
			}

			// Token: 0x170007EA RID: 2026
			// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001BD2 RID: 7122 RVA: 0x000688D3 File Offset: 0x00066AD3
			public override Visibility GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).visibility;
			}

			// Token: 0x06001BD3 RID: 7123 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, Visibility value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200039F RID: 927
		private class WhiteSpaceProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<WhiteSpace>
		{
			// Token: 0x170007EB RID: 2027
			// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x0005F8C6 File Offset: 0x0005DAC6
			public override string Name
			{
				get
				{
					return "whiteSpace";
				}
			}

			// Token: 0x170007EC RID: 2028
			// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0005F8CD File Offset: 0x0005DACD
			public override string ussName
			{
				get
				{
					return "white-space";
				}
			}

			// Token: 0x170007ED RID: 2029
			// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001BD8 RID: 7128 RVA: 0x000688E5 File Offset: 0x00066AE5
			public override WhiteSpace GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).whiteSpace;
			}

			// Token: 0x06001BD9 RID: 7129 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, WhiteSpace value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003A0 RID: 928
		private class WidthProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170007EE RID: 2030
			// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0005F8F1 File Offset: 0x0005DAF1
			public override string Name
			{
				get
				{
					return "width";
				}
			}

			// Token: 0x170007EF RID: 2031
			// (get) Token: 0x06001BDC RID: 7132 RVA: 0x0005F8F1 File Offset: 0x0005DAF1
			public override string ussName
			{
				get
				{
					return "width";
				}
			}

			// Token: 0x170007F0 RID: 2032
			// (get) Token: 0x06001BDD RID: 7133 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001BDE RID: 7134 RVA: 0x000688F7 File Offset: 0x00066AF7
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).width;
			}

			// Token: 0x06001BDF RID: 7135 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003A1 RID: 929
		private class WordSpacingProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170007F1 RID: 2033
			// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x0005F90C File Offset: 0x0005DB0C
			public override string Name
			{
				get
				{
					return "wordSpacing";
				}
			}

			// Token: 0x170007F2 RID: 2034
			// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x0005F913 File Offset: 0x0005DB13
			public override string ussName
			{
				get
				{
					return "word-spacing";
				}
			}

			// Token: 0x170007F3 RID: 2035
			// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x0000C45B File Offset: 0x0000A65B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001BE4 RID: 7140 RVA: 0x00068900 File Offset: 0x00066B00
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).wordSpacing;
			}

			// Token: 0x06001BE5 RID: 7141 RVA: 0x00068536 File Offset: 0x00066736
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003A2 RID: 930
		private abstract class ResolvedStyleProperty<TValue> : Property<ResolvedStyleAccess, TValue>
		{
			// Token: 0x170007F4 RID: 2036
			// (get) Token: 0x06001BE7 RID: 7143
			public abstract string ussName { get; }
		}

		// Token: 0x020003A3 RID: 931
		private abstract class ResolvedEnumProperty<TValue> : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<TValue> where TValue : struct, IConvertible
		{
		}

		// Token: 0x020003A4 RID: 932
		private abstract class ResolvedColorProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<Color>
		{
		}

		// Token: 0x020003A5 RID: 933
		private abstract class ResolvedBackgroundProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<Background>
		{
		}

		// Token: 0x020003A6 RID: 934
		private abstract class ResolvedFloatProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<float>
		{
		}

		// Token: 0x020003A7 RID: 935
		private abstract class ResolvedStyleFloatProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<StyleFloat>
		{
		}

		// Token: 0x020003A8 RID: 936
		private abstract class ResolvedListProperty<T> : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<IEnumerable<T>>
		{
		}

		// Token: 0x020003A9 RID: 937
		private abstract class ResolvedFontProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<Font>
		{
		}

		// Token: 0x020003AA RID: 938
		private abstract class ResolvedFontDefinitionProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<FontDefinition>
		{
		}

		// Token: 0x020003AB RID: 939
		private abstract class ResolvedIntProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<int>
		{
		}

		// Token: 0x020003AC RID: 940
		private abstract class ResolvedRotateProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<Rotate>
		{
		}

		// Token: 0x020003AD RID: 941
		private abstract class ResolvedScaleProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<Scale>
		{
		}

		// Token: 0x020003AE RID: 942
		private abstract class ResolvedVector3Property : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<Vector3>
		{
		}

		// Token: 0x020003AF RID: 943
		private abstract class ResolvedBackgroundPositionProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<BackgroundPosition>
		{
		}

		// Token: 0x020003B0 RID: 944
		private abstract class ResolvedBackgroundRepeatProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<BackgroundRepeat>
		{
		}

		// Token: 0x020003B1 RID: 945
		private abstract class ResolvedBackgroundSizeProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<BackgroundSize>
		{
		}
	}
}
