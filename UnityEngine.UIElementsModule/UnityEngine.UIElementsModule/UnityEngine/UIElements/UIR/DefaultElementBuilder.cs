using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000507 RID: 1287
	internal class DefaultElementBuilder : BaseElementBuilder
	{
		// Token: 0x060023E4 RID: 9188 RVA: 0x00084B63 File Offset: 0x00082D63
		public DefaultElementBuilder(RenderChain renderChain)
		{
			this.m_RenderChain = renderChain;
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x00084B74 File Offset: 0x00082D74
		public override bool RequiresStencilMask(VisualElement ve)
		{
			return UIRUtility.IsRoundRect(ve) || UIRUtility.IsVectorImageBackground(ve);
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x00084B98 File Offset: 0x00082D98
		protected unsafe override void DrawVisualElementBackground(MeshGenerationContext mgc)
		{
			VisualElement ve = mgc.visualElement;
			bool flag = ve.layout.width <= 1E-30f || ve.layout.height <= 1E-30f;
			if (!flag)
			{
				ComputedStyle style = *ve.computedStyle;
				Color backgroundColor = style.backgroundColor;
				ve.renderChainData.backgroundAlpha = backgroundColor.a;
				bool flag2 = backgroundColor.a > 1E-30f;
				if (flag2)
				{
					MeshGenerator.RectangleParams rectParams = new MeshGenerator.RectangleParams
					{
						rect = ve.rect,
						color = backgroundColor,
						colorPage = ColorPage.Init(this.m_RenderChain, ve.renderChainData.backgroundColorID),
						playmodeTintColor = ve.playModeTintColor
					};
					MeshGenerator.GetVisualElementRadii(ve, out rectParams.topLeftRadius, out rectParams.bottomLeftRadius, out rectParams.topRightRadius, out rectParams.bottomRightRadius);
					MeshGenerator.AdjustBackgroundSizeForBorders(ve, ref rectParams);
					mgc.meshGenerator.DrawRectangle(rectParams);
				}
				Vector4 slices = new Vector4((float)style.unitySliceLeft, (float)style.unitySliceTop, (float)style.unitySliceRight, (float)style.unitySliceBottom);
				MeshGenerator.RectangleParams radiusParams = default(MeshGenerator.RectangleParams);
				MeshGenerator.GetVisualElementRadii(ve, out radiusParams.topLeftRadius, out radiusParams.bottomLeftRadius, out radiusParams.topRightRadius, out radiusParams.bottomRightRadius);
				Background background = style.backgroundImage;
				bool flag3 = background.texture != null || background.sprite != null || background.vectorImage != null || background.renderTexture != null;
				if (flag3)
				{
					MeshGenerator.RectangleParams rectParams2 = default(MeshGenerator.RectangleParams);
					float sliceScale = ve.resolvedStyle.unitySliceScale;
					Color playModeTintColor = ve.playModeTintColor;
					bool validScaleMode;
					ScaleMode scaleMode = BackgroundPropertyHelper.ResolveUnityBackgroundScaleMode(style.backgroundPositionX, style.backgroundPositionY, style.backgroundRepeat, style.backgroundSize, out validScaleMode);
					bool flag4 = background.texture != null;
					if (flag4)
					{
						bool areSlicesPresent = Mathf.RoundToInt(slices.x) != 0 || Mathf.RoundToInt(slices.y) != 0 || Mathf.RoundToInt(slices.z) != 0 || Mathf.RoundToInt(slices.w) != 0;
						rectParams2 = MeshGenerator.RectangleParams.MakeTextured(ve.rect, new Rect(0f, 0f, 1f, 1f), background.texture, areSlicesPresent ? (validScaleMode ? scaleMode : ScaleMode.StretchToFill) : ScaleMode.ScaleToFit, playModeTintColor);
						rectParams2.rect = new Rect(0f, 0f, (float)rectParams2.texture.width, (float)rectParams2.texture.height);
					}
					else
					{
						bool flag5 = background.sprite != null;
						if (flag5)
						{
							bool useRepeat = !validScaleMode || scaleMode == ScaleMode.ScaleAndCrop;
							rectParams2 = MeshGenerator.RectangleParams.MakeSprite(ve.rect, new Rect(0f, 0f, 1f, 1f), background.sprite, useRepeat ? ScaleMode.StretchToFill : scaleMode, playModeTintColor, radiusParams.HasRadius(0.001f), ref slices, useRepeat);
							bool flag6 = rectParams2.texture != null;
							if (flag6)
							{
								rectParams2.rect = new Rect(0f, 0f, background.sprite.rect.width, background.sprite.rect.height);
							}
							sliceScale *= UIElementsUtility.PixelsPerUnitScaleForElement(ve, background.sprite);
						}
						else
						{
							bool flag7 = background.renderTexture != null;
							if (flag7)
							{
								rectParams2 = MeshGenerator.RectangleParams.MakeTextured(ve.rect, new Rect(0f, 0f, 1f, 1f), background.renderTexture, ScaleMode.ScaleToFit, playModeTintColor);
								rectParams2.rect = new Rect(0f, 0f, (float)rectParams2.texture.width, (float)rectParams2.texture.height);
							}
							else
							{
								bool flag8 = background.vectorImage != null;
								if (flag8)
								{
									bool useRepeat2 = !validScaleMode || scaleMode == ScaleMode.ScaleAndCrop;
									rectParams2 = MeshGenerator.RectangleParams.MakeVectorTextured(ve.rect, new Rect(0f, 0f, 1f, 1f), background.vectorImage, useRepeat2 ? ScaleMode.StretchToFill : scaleMode, playModeTintColor);
									rectParams2.rect = new Rect(0f, 0f, rectParams2.vectorImage.size.x, rectParams2.vectorImage.size.y);
								}
							}
						}
					}
					rectParams2.topLeftRadius = radiusParams.topLeftRadius;
					rectParams2.topRightRadius = radiusParams.topRightRadius;
					rectParams2.bottomRightRadius = radiusParams.bottomRightRadius;
					rectParams2.bottomLeftRadius = radiusParams.bottomLeftRadius;
					bool flag9 = slices != Vector4.zero;
					if (flag9)
					{
						rectParams2.leftSlice = Mathf.RoundToInt(slices.x);
						rectParams2.topSlice = Mathf.RoundToInt(slices.y);
						rectParams2.rightSlice = Mathf.RoundToInt(slices.z);
						rectParams2.bottomSlice = Mathf.RoundToInt(slices.w);
						rectParams2.sliceScale = sliceScale;
						bool flag10 = !validScaleMode;
						if (flag10)
						{
							rectParams2.backgroundPositionX = BackgroundPropertyHelper.ConvertScaleModeToBackgroundPosition(ScaleMode.StretchToFill);
							rectParams2.backgroundPositionY = BackgroundPropertyHelper.ConvertScaleModeToBackgroundPosition(ScaleMode.StretchToFill);
							rectParams2.backgroundRepeat = BackgroundPropertyHelper.ConvertScaleModeToBackgroundRepeat(ScaleMode.StretchToFill);
							rectParams2.backgroundSize = BackgroundPropertyHelper.ConvertScaleModeToBackgroundSize(ScaleMode.StretchToFill);
						}
						else
						{
							rectParams2.backgroundPositionX = style.backgroundPositionX;
							rectParams2.backgroundPositionY = style.backgroundPositionY;
							rectParams2.backgroundRepeat = style.backgroundRepeat;
							rectParams2.backgroundSize = style.backgroundSize;
						}
					}
					else
					{
						rectParams2.backgroundPositionX = style.backgroundPositionX;
						rectParams2.backgroundPositionY = style.backgroundPositionY;
						rectParams2.backgroundRepeat = style.backgroundRepeat;
						rectParams2.backgroundSize = style.backgroundSize;
					}
					rectParams2.color = style.unityBackgroundImageTintColor;
					rectParams2.colorPage = ColorPage.Init(this.m_RenderChain, ve.renderChainData.tintColorID);
					MeshGenerator.AdjustBackgroundSizeForBorders(ve, ref rectParams2);
					bool flag11 = rectParams2.texture != null || rectParams2.vectorImage != null;
					if (flag11)
					{
						mgc.meshGenerator.DrawRectangleRepeat(rectParams2, ve.rect, ve.scaledPixelsPerPoint);
					}
					else
					{
						mgc.meshGenerator.DrawRectangle(rectParams2);
					}
				}
			}
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x00085220 File Offset: 0x00083420
		protected override void DrawVisualElementBorder(MeshGenerationContext mgc)
		{
			VisualElement ve = mgc.visualElement;
			bool flag = ve.layout.width >= 1E-30f && ve.layout.height >= 1E-30f;
			if (flag)
			{
				IResolvedStyle style = ve.resolvedStyle;
				bool flag2 = (style.borderLeftColor != Color.clear && style.borderLeftWidth > 0f) || (style.borderTopColor != Color.clear && style.borderTopWidth > 0f) || (style.borderRightColor != Color.clear && style.borderRightWidth > 0f) || (style.borderBottomColor != Color.clear && style.borderBottomWidth > 0f);
				if (flag2)
				{
					MeshGenerator.BorderParams borderParams = new MeshGenerator.BorderParams
					{
						rect = ve.rect,
						leftColor = style.borderLeftColor,
						topColor = style.borderTopColor,
						rightColor = style.borderRightColor,
						bottomColor = style.borderBottomColor,
						leftWidth = style.borderLeftWidth,
						topWidth = style.borderTopWidth,
						rightWidth = style.borderRightWidth,
						bottomWidth = style.borderBottomWidth,
						leftColorPage = ColorPage.Init(this.m_RenderChain, ve.renderChainData.borderLeftColorID),
						topColorPage = ColorPage.Init(this.m_RenderChain, ve.renderChainData.borderTopColorID),
						rightColorPage = ColorPage.Init(this.m_RenderChain, ve.renderChainData.borderRightColorID),
						bottomColorPage = ColorPage.Init(this.m_RenderChain, ve.renderChainData.borderBottomColorID),
						playmodeTintColor = ve.playModeTintColor
					};
					MeshGenerator.GetVisualElementRadii(ve, out borderParams.topLeftRadius, out borderParams.bottomLeftRadius, out borderParams.topRightRadius, out borderParams.bottomRightRadius);
					mgc.meshGenerator.DrawBorder(borderParams);
				}
			}
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x00085438 File Offset: 0x00083638
		protected override void DrawVisualElementStencilMask(MeshGenerationContext mgc)
		{
			bool flag = UIRUtility.IsVectorImageBackground(mgc.visualElement);
			if (flag)
			{
				this.DrawVisualElementBackground(mgc);
			}
			else
			{
				DefaultElementBuilder.GenerateStencilClipEntryForRoundedRectBackground(mgc);
			}
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x00085468 File Offset: 0x00083668
		private static void GenerateStencilClipEntryForRoundedRectBackground(MeshGenerationContext mgc)
		{
			VisualElement ve = mgc.visualElement;
			bool flag = ve.layout.width <= 1E-30f || ve.layout.height <= 1E-30f;
			if (!flag)
			{
				IResolvedStyle resolvedStyle = ve.resolvedStyle;
				Vector2 radTL;
				Vector2 radBL;
				Vector2 radTR;
				Vector2 radBR;
				MeshGenerator.GetVisualElementRadii(ve, out radTL, out radBL, out radTR, out radBR);
				float widthT = resolvedStyle.borderTopWidth;
				float widthL = resolvedStyle.borderLeftWidth;
				float widthB = resolvedStyle.borderBottomWidth;
				float widthR = resolvedStyle.borderRightWidth;
				MeshGenerator.RectangleParams rp = new MeshGenerator.RectangleParams
				{
					rect = ve.rect,
					color = Color.white,
					topLeftRadius = Vector2.Max(Vector2.zero, radTL - new Vector2(widthL, widthT)),
					topRightRadius = Vector2.Max(Vector2.zero, radTR - new Vector2(widthR, widthT)),
					bottomLeftRadius = Vector2.Max(Vector2.zero, radBL - new Vector2(widthL, widthB)),
					bottomRightRadius = Vector2.Max(Vector2.zero, radBR - new Vector2(widthR, widthB)),
					playmodeTintColor = ve.playModeTintColor
				};
				rp.rect.x = rp.rect.x + widthL;
				rp.rect.y = rp.rect.y + widthT;
				rp.rect.width = rp.rect.width - (widthL + widthR);
				rp.rect.height = rp.rect.height - (widthT + widthB);
				bool flag2 = ve.computedStyle.unityOverflowClipBox == OverflowClipBox.ContentBox;
				if (flag2)
				{
					rp.rect.x = rp.rect.x + resolvedStyle.paddingLeft;
					rp.rect.y = rp.rect.y + resolvedStyle.paddingTop;
					rp.rect.width = rp.rect.width - (resolvedStyle.paddingLeft + resolvedStyle.paddingRight);
					rp.rect.height = rp.rect.height - (resolvedStyle.paddingTop + resolvedStyle.paddingBottom);
				}
				mgc.meshGenerator.DrawRectangle(rp);
			}
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x0008569C File Offset: 0x0008389C
		public override void ScheduleMeshGenerationJobs(MeshGenerationContext mgc)
		{
			mgc.meshGenerator.ScheduleJobs(mgc);
			bool hasPainter2D = mgc.hasPainter2D;
			if (hasPainter2D)
			{
				mgc.painter2D.ScheduleJobs(mgc);
			}
		}

		// Token: 0x04001071 RID: 4209
		private RenderChain m_RenderChain;
	}
}
