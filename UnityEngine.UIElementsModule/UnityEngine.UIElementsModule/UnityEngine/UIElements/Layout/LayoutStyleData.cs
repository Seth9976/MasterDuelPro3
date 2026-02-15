using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020005A3 RID: 1443
	[NativeHeader("Modules/UIElements/Core/Layout/Native/LayoutModel.h")]
	[RequiredByNativeCode]
	internal struct LayoutStyleData
	{
		// Token: 0x04001437 RID: 5175
		public static LayoutStyleData Default = new LayoutStyleData
		{
			Direction = LayoutDirection.Inherit,
			FlexDirection = LayoutFlexDirection.Column,
			JustifyContent = LayoutJustify.FlexStart,
			AlignContent = LayoutAlign.Auto,
			AlignItems = LayoutAlign.Stretch,
			AlignSelf = LayoutAlign.Auto,
			PositionType = LayoutPositionType.Relative,
			AspectRatio = float.NaN,
			FlexWrap = LayoutWrap.NoWrap,
			Overflow = LayoutOverflow.Visible,
			Display = LayoutDisplay.Flex,
			FlexGrow = float.NaN,
			FlexShrink = float.NaN,
			FlexBasis = LayoutValue.Auto(),
			border = LayoutDefaults.EdgeValuesUnit,
			position = LayoutDefaults.EdgeValuesUnit,
			margin = LayoutDefaults.EdgeValuesUnit,
			padding = LayoutDefaults.EdgeValuesUnit,
			dimensions = LayoutDefaults.DimensionValuesAutoUnit,
			minDimensions = LayoutDefaults.DimensionValuesUnit
		};

		// Token: 0x04001438 RID: 5176
		public LayoutDirection Direction;

		// Token: 0x04001439 RID: 5177
		public LayoutFlexDirection FlexDirection;

		// Token: 0x0400143A RID: 5178
		public LayoutJustify JustifyContent;

		// Token: 0x0400143B RID: 5179
		public LayoutAlign AlignContent;

		// Token: 0x0400143C RID: 5180
		public LayoutAlign AlignItems;

		// Token: 0x0400143D RID: 5181
		public LayoutAlign AlignSelf;

		// Token: 0x0400143E RID: 5182
		public LayoutPositionType PositionType;

		// Token: 0x0400143F RID: 5183
		public float AspectRatio;

		// Token: 0x04001440 RID: 5184
		public LayoutWrap FlexWrap;

		// Token: 0x04001441 RID: 5185
		public LayoutOverflow Overflow;

		// Token: 0x04001442 RID: 5186
		public LayoutDisplay Display;

		// Token: 0x04001443 RID: 5187
		public float FlexGrow;

		// Token: 0x04001444 RID: 5188
		public float FlexShrink;

		// Token: 0x04001445 RID: 5189
		public LayoutValue FlexBasis;

		// Token: 0x04001446 RID: 5190
		public FixedBuffer9<LayoutValue> border;

		// Token: 0x04001447 RID: 5191
		public FixedBuffer9<LayoutValue> position;

		// Token: 0x04001448 RID: 5192
		public FixedBuffer9<LayoutValue> margin;

		// Token: 0x04001449 RID: 5193
		public FixedBuffer9<LayoutValue> padding;

		// Token: 0x0400144A RID: 5194
		public FixedBuffer2<LayoutValue> maxDimensions;

		// Token: 0x0400144B RID: 5195
		public FixedBuffer2<LayoutValue> minDimensions;

		// Token: 0x0400144C RID: 5196
		public FixedBuffer2<LayoutValue> dimensions;
	}
}
