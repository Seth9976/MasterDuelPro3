using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Bindings;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000404 RID: 1028
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal interface IStylePropertyAnimationSystem
	{
		// Token: 0x06001E03 RID: 7683
		bool StartTransition(VisualElement owner, StylePropertyId prop, float startValue, float endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E04 RID: 7684
		bool StartTransition(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E05 RID: 7685
		bool StartTransition(VisualElement owner, StylePropertyId prop, Length startValue, Length endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E06 RID: 7686
		bool StartTransition(VisualElement owner, StylePropertyId prop, Color startValue, Color endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E07 RID: 7687
		bool StartTransition(VisualElement owner, StylePropertyId prop, Background startValue, Background endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E08 RID: 7688
		bool StartTransition(VisualElement owner, StylePropertyId prop, FontDefinition startValue, FontDefinition endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E09 RID: 7689
		bool StartTransition(VisualElement owner, StylePropertyId prop, Font startValue, Font endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E0A RID: 7690
		bool StartTransition(VisualElement owner, StylePropertyId prop, TextShadow startValue, TextShadow endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E0B RID: 7691
		bool StartTransition(VisualElement owner, StylePropertyId prop, Scale startValue, Scale endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E0C RID: 7692
		bool StartTransition(VisualElement owner, StylePropertyId prop, TransformOrigin startValue, TransformOrigin endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E0D RID: 7693
		bool StartTransition(VisualElement owner, StylePropertyId prop, Translate startValue, Translate endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E0E RID: 7694
		bool StartTransition(VisualElement owner, StylePropertyId prop, Rotate startValue, Rotate endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E0F RID: 7695
		bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundPosition startValue, BackgroundPosition endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E10 RID: 7696
		bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundRepeat startValue, BackgroundRepeat endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E11 RID: 7697
		bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundSize startValue, BackgroundSize endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06001E12 RID: 7698
		void CancelAllAnimations();

		// Token: 0x06001E13 RID: 7699
		void CancelAllAnimations(VisualElement owner);

		// Token: 0x06001E14 RID: 7700
		void CancelAnimation(VisualElement owner, StylePropertyId id);

		// Token: 0x06001E15 RID: 7701
		void UpdateAnimation(VisualElement owner, StylePropertyId id);

		// Token: 0x06001E16 RID: 7702
		void GetAllAnimations(VisualElement owner, List<StylePropertyId> propertyIds);

		// Token: 0x06001E17 RID: 7703
		void Update();
	}
}
