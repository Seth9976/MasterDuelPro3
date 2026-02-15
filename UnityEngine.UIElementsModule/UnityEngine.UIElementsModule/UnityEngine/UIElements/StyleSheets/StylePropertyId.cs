using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005AB RID: 1451
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal enum StylePropertyId
	{
		// Token: 0x04001476 RID: 5238
		Unknown,
		// Token: 0x04001477 RID: 5239
		Custom = -1,
		// Token: 0x04001478 RID: 5240
		AlignContent = 131072,
		// Token: 0x04001479 RID: 5241
		AlignItems,
		// Token: 0x0400147A RID: 5242
		AlignSelf,
		// Token: 0x0400147B RID: 5243
		All = 262144,
		// Token: 0x0400147C RID: 5244
		BackgroundColor = 458752,
		// Token: 0x0400147D RID: 5245
		BackgroundImage,
		// Token: 0x0400147E RID: 5246
		BackgroundPosition = 262145,
		// Token: 0x0400147F RID: 5247
		BackgroundPositionX = 458754,
		// Token: 0x04001480 RID: 5248
		BackgroundPositionY,
		// Token: 0x04001481 RID: 5249
		BackgroundRepeat,
		// Token: 0x04001482 RID: 5250
		BackgroundSize,
		// Token: 0x04001483 RID: 5251
		BorderBottomColor,
		// Token: 0x04001484 RID: 5252
		BorderBottomLeftRadius,
		// Token: 0x04001485 RID: 5253
		BorderBottomRightRadius,
		// Token: 0x04001486 RID: 5254
		BorderBottomWidth = 131075,
		// Token: 0x04001487 RID: 5255
		BorderColor = 262146,
		// Token: 0x04001488 RID: 5256
		BorderLeftColor = 458761,
		// Token: 0x04001489 RID: 5257
		BorderLeftWidth = 131076,
		// Token: 0x0400148A RID: 5258
		BorderRadius = 262147,
		// Token: 0x0400148B RID: 5259
		BorderRightColor = 458762,
		// Token: 0x0400148C RID: 5260
		BorderRightWidth = 131077,
		// Token: 0x0400148D RID: 5261
		BorderTopColor = 458763,
		// Token: 0x0400148E RID: 5262
		BorderTopLeftRadius,
		// Token: 0x0400148F RID: 5263
		BorderTopRightRadius,
		// Token: 0x04001490 RID: 5264
		BorderTopWidth = 131078,
		// Token: 0x04001491 RID: 5265
		BorderWidth = 262148,
		// Token: 0x04001492 RID: 5266
		Bottom = 131079,
		// Token: 0x04001493 RID: 5267
		Color = 65536,
		// Token: 0x04001494 RID: 5268
		Cursor = 196608,
		// Token: 0x04001495 RID: 5269
		Display = 131080,
		// Token: 0x04001496 RID: 5270
		Flex = 262149,
		// Token: 0x04001497 RID: 5271
		FlexBasis = 131081,
		// Token: 0x04001498 RID: 5272
		FlexDirection,
		// Token: 0x04001499 RID: 5273
		FlexGrow,
		// Token: 0x0400149A RID: 5274
		FlexShrink,
		// Token: 0x0400149B RID: 5275
		FlexWrap,
		// Token: 0x0400149C RID: 5276
		FontSize = 65537,
		// Token: 0x0400149D RID: 5277
		Height = 131086,
		// Token: 0x0400149E RID: 5278
		JustifyContent,
		// Token: 0x0400149F RID: 5279
		Left,
		// Token: 0x040014A0 RID: 5280
		LetterSpacing = 65538,
		// Token: 0x040014A1 RID: 5281
		Margin = 262150,
		// Token: 0x040014A2 RID: 5282
		MarginBottom = 131089,
		// Token: 0x040014A3 RID: 5283
		MarginLeft,
		// Token: 0x040014A4 RID: 5284
		MarginRight,
		// Token: 0x040014A5 RID: 5285
		MarginTop,
		// Token: 0x040014A6 RID: 5286
		MaxHeight,
		// Token: 0x040014A7 RID: 5287
		MaxWidth,
		// Token: 0x040014A8 RID: 5288
		MinHeight,
		// Token: 0x040014A9 RID: 5289
		MinWidth,
		// Token: 0x040014AA RID: 5290
		Opacity = 458766,
		// Token: 0x040014AB RID: 5291
		Overflow,
		// Token: 0x040014AC RID: 5292
		Padding = 262151,
		// Token: 0x040014AD RID: 5293
		PaddingBottom = 131097,
		// Token: 0x040014AE RID: 5294
		PaddingLeft,
		// Token: 0x040014AF RID: 5295
		PaddingRight,
		// Token: 0x040014B0 RID: 5296
		PaddingTop,
		// Token: 0x040014B1 RID: 5297
		Position,
		// Token: 0x040014B2 RID: 5298
		Right,
		// Token: 0x040014B3 RID: 5299
		Rotate = 327680,
		// Token: 0x040014B4 RID: 5300
		Scale,
		// Token: 0x040014B5 RID: 5301
		TextOverflow = 196609,
		// Token: 0x040014B6 RID: 5302
		TextShadow = 65539,
		// Token: 0x040014B7 RID: 5303
		Top = 131103,
		// Token: 0x040014B8 RID: 5304
		TransformOrigin = 327682,
		// Token: 0x040014B9 RID: 5305
		Transition = 262152,
		// Token: 0x040014BA RID: 5306
		TransitionDelay = 393216,
		// Token: 0x040014BB RID: 5307
		TransitionDuration,
		// Token: 0x040014BC RID: 5308
		TransitionProperty,
		// Token: 0x040014BD RID: 5309
		TransitionTimingFunction,
		// Token: 0x040014BE RID: 5310
		Translate = 327683,
		// Token: 0x040014BF RID: 5311
		UnityBackgroundImageTintColor = 196610,
		// Token: 0x040014C0 RID: 5312
		UnityBackgroundScaleMode = 262153,
		// Token: 0x040014C1 RID: 5313
		UnityEditorTextRenderingMode = 65540,
		// Token: 0x040014C2 RID: 5314
		UnityFont,
		// Token: 0x040014C3 RID: 5315
		UnityFontDefinition,
		// Token: 0x040014C4 RID: 5316
		UnityFontStyleAndWeight,
		// Token: 0x040014C5 RID: 5317
		UnityOverflowClipBox = 196611,
		// Token: 0x040014C6 RID: 5318
		UnityParagraphSpacing = 65544,
		// Token: 0x040014C7 RID: 5319
		UnitySliceBottom = 196612,
		// Token: 0x040014C8 RID: 5320
		UnitySliceLeft,
		// Token: 0x040014C9 RID: 5321
		UnitySliceRight,
		// Token: 0x040014CA RID: 5322
		UnitySliceScale,
		// Token: 0x040014CB RID: 5323
		UnitySliceTop,
		// Token: 0x040014CC RID: 5324
		UnityTextAlign = 65545,
		// Token: 0x040014CD RID: 5325
		UnityTextGenerator,
		// Token: 0x040014CE RID: 5326
		UnityTextOutline = 262154,
		// Token: 0x040014CF RID: 5327
		UnityTextOutlineColor = 65547,
		// Token: 0x040014D0 RID: 5328
		UnityTextOutlineWidth,
		// Token: 0x040014D1 RID: 5329
		UnityTextOverflowPosition = 196617,
		// Token: 0x040014D2 RID: 5330
		Visibility = 65549,
		// Token: 0x040014D3 RID: 5331
		WhiteSpace,
		// Token: 0x040014D4 RID: 5332
		Width = 131104,
		// Token: 0x040014D5 RID: 5333
		WordSpacing = 65551
	}
}
