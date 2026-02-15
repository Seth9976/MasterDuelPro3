using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000403 RID: 1027
	internal interface IStylePropertyAnimations
	{
		// Token: 0x06001DEB RID: 7659
		bool Start(StylePropertyId id, float from, float to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DEC RID: 7660
		bool Start(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DED RID: 7661
		bool Start(StylePropertyId id, Length from, Length to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DEE RID: 7662
		bool Start(StylePropertyId id, Color from, Color to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DEF RID: 7663
		bool StartEnum(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DF0 RID: 7664
		bool Start(StylePropertyId id, Background from, Background to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DF1 RID: 7665
		bool Start(StylePropertyId id, FontDefinition from, FontDefinition to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DF2 RID: 7666
		bool Start(StylePropertyId id, Font from, Font to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DF3 RID: 7667
		bool Start(StylePropertyId id, TextShadow from, TextShadow to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DF4 RID: 7668
		bool Start(StylePropertyId id, Scale from, Scale to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DF5 RID: 7669
		bool Start(StylePropertyId id, Translate from, Translate to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DF6 RID: 7670
		bool Start(StylePropertyId id, Rotate from, Rotate to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DF7 RID: 7671
		bool Start(StylePropertyId id, TransformOrigin from, TransformOrigin to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DF8 RID: 7672
		bool Start(StylePropertyId id, BackgroundPosition from, BackgroundPosition to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DF9 RID: 7673
		bool Start(StylePropertyId id, BackgroundRepeat from, BackgroundRepeat to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DFA RID: 7674
		bool Start(StylePropertyId id, BackgroundSize from, BackgroundSize to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06001DFB RID: 7675
		void UpdateAnimation(StylePropertyId id);

		// Token: 0x06001DFC RID: 7676
		void GetAllAnimations(List<StylePropertyId> outPropertyIds);

		// Token: 0x06001DFD RID: 7677
		void CancelAnimation(StylePropertyId id);

		// Token: 0x06001DFE RID: 7678
		void CancelAllAnimations();

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001DFF RID: 7679
		// (set) Token: 0x06001E00 RID: 7680
		int runningAnimationCount { get; set; }

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001E01 RID: 7681
		// (set) Token: 0x06001E02 RID: 7682
		int completedAnimationCount { get; set; }
	}
}
