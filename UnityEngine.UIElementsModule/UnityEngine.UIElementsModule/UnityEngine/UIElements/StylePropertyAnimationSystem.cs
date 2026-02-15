using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Assertions;
using UnityEngine.Bindings;
using UnityEngine.Pool;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000405 RID: 1029
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class StylePropertyAnimationSystem : IStylePropertyAnimationSystem
	{
		// Token: 0x06001E18 RID: 7704 RVA: 0x0006D474 File Offset: 0x0006B674
		public StylePropertyAnimationSystem()
		{
			this.m_CurrentTimeMs = Panel.TimeSinceStartupMs();
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x0006D4A8 File Offset: 0x0006B6A8
		private T GetOrCreate<T>(ref T values) where T : new()
		{
			T t = values;
			return (t != null) ? t : (values = new T());
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x0006D4DC File Offset: 0x0006B6DC
		private bool StartTransition<T>(VisualElement owner, StylePropertyId prop, T startValue, T endValue, int durationMs, int delayMs, Func<float, float> easingCurve, StylePropertyAnimationSystem.Values<T> values)
		{
			this.m_PropertyToValues[prop] = values;
			bool result = values.StartTransition(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.CurrentTimeMs());
			this.UpdateTracking<T>(values);
			return result;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0006D520 File Offset: 0x0006B720
		public bool StartTransition(VisualElement owner, StylePropertyId prop, float startValue, float endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<float>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesFloat>(ref this.m_Floats));
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x0006D550 File Offset: 0x0006B750
		public bool StartTransition(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<int>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesInt>(ref this.m_Ints));
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x0006D580 File Offset: 0x0006B780
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Length startValue, Length endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Length>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesLength>(ref this.m_Lengths));
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x0006D5B0 File Offset: 0x0006B7B0
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Color startValue, Color endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Color>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesColor>(ref this.m_Colors));
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x0006D5E0 File Offset: 0x0006B7E0
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Background startValue, Background endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Background>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesBackground>(ref this.m_Backgrounds));
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0006D610 File Offset: 0x0006B810
		public bool StartTransition(VisualElement owner, StylePropertyId prop, FontDefinition startValue, FontDefinition endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<FontDefinition>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesFontDefinition>(ref this.m_FontDefinitions));
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x0006D640 File Offset: 0x0006B840
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Font startValue, Font endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Font>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesFont>(ref this.m_Fonts));
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x0006D670 File Offset: 0x0006B870
		public bool StartTransition(VisualElement owner, StylePropertyId prop, TextShadow startValue, TextShadow endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<TextShadow>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesTextShadow>(ref this.m_TextShadows));
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x0006D6A0 File Offset: 0x0006B8A0
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Scale startValue, Scale endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Scale>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesScale>(ref this.m_Scale));
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0006D6D0 File Offset: 0x0006B8D0
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Rotate startValue, Rotate endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Rotate>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesRotate>(ref this.m_Rotate));
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0006D700 File Offset: 0x0006B900
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Translate startValue, Translate endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Translate>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesTranslate>(ref this.m_Translate));
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x0006D730 File Offset: 0x0006B930
		public bool StartTransition(VisualElement owner, StylePropertyId prop, TransformOrigin startValue, TransformOrigin endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<TransformOrigin>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesTransformOrigin>(ref this.m_TransformOrigin));
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0006D760 File Offset: 0x0006B960
		public bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundPosition startValue, BackgroundPosition endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<BackgroundPosition>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesBackgroundPosition>(ref this.m_BackgroundPosition));
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0006D790 File Offset: 0x0006B990
		public bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundRepeat startValue, BackgroundRepeat endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<BackgroundRepeat>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesBackgroundRepeat>(ref this.m_BackgroundRepeat));
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0006D7C0 File Offset: 0x0006B9C0
		public bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundSize startValue, BackgroundSize endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<BackgroundSize>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesBackgroundSize>(ref this.m_BackgroundSize));
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0006D7F0 File Offset: 0x0006B9F0
		public void CancelAllAnimations()
		{
			foreach (StylePropertyAnimationSystem.Values values in this.m_AllValues)
			{
				values.CancelAllAnimations();
			}
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x0006D848 File Offset: 0x0006BA48
		public void CancelAllAnimations(VisualElement owner)
		{
			foreach (StylePropertyAnimationSystem.Values values in this.m_AllValues)
			{
				values.CancelAllAnimations(owner);
			}
			Assert.AreEqual(0, owner.styleAnimation.runningAnimationCount);
			Assert.AreEqual(0, owner.styleAnimation.completedAnimationCount);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0006D8C8 File Offset: 0x0006BAC8
		public void CancelAnimation(VisualElement owner, StylePropertyId id)
		{
			StylePropertyAnimationSystem.Values values;
			bool flag = this.m_PropertyToValues.TryGetValue(id, out values);
			if (flag)
			{
				values.CancelAnimation(owner, id);
			}
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0006D8F4 File Offset: 0x0006BAF4
		public void UpdateAnimation(VisualElement owner, StylePropertyId id)
		{
			StylePropertyAnimationSystem.Values values;
			bool flag = this.m_PropertyToValues.TryGetValue(id, out values);
			if (flag)
			{
				values.UpdateAnimation(owner, id);
			}
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0006D920 File Offset: 0x0006BB20
		public void GetAllAnimations(VisualElement owner, List<StylePropertyId> propertyIds)
		{
			foreach (StylePropertyAnimationSystem.Values values in this.m_AllValues)
			{
				values.GetAllAnimations(owner, propertyIds);
			}
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x0006D978 File Offset: 0x0006BB78
		private void UpdateTracking<T>(StylePropertyAnimationSystem.Values<T> values)
		{
			bool flag = !values.isEmpty && !this.m_AllValues.Contains(values);
			if (flag)
			{
				this.m_AllValues.Add(values);
			}
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0006D9B4 File Offset: 0x0006BBB4
		private long CurrentTimeMs()
		{
			return this.m_CurrentTimeMs;
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x0006D9CC File Offset: 0x0006BBCC
		public void Update()
		{
			this.m_CurrentTimeMs = Panel.TimeSinceStartupMs();
			int count = this.m_AllValues.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_AllValues[i].Update(this.m_CurrentTimeMs);
			}
		}

		// Token: 0x04000D20 RID: 3360
		private long m_CurrentTimeMs = 0L;

		// Token: 0x04000D21 RID: 3361
		private StylePropertyAnimationSystem.ValuesFloat m_Floats;

		// Token: 0x04000D22 RID: 3362
		private StylePropertyAnimationSystem.ValuesInt m_Ints;

		// Token: 0x04000D23 RID: 3363
		private StylePropertyAnimationSystem.ValuesLength m_Lengths;

		// Token: 0x04000D24 RID: 3364
		private StylePropertyAnimationSystem.ValuesColor m_Colors;

		// Token: 0x04000D25 RID: 3365
		private StylePropertyAnimationSystem.ValuesBackground m_Backgrounds;

		// Token: 0x04000D26 RID: 3366
		private StylePropertyAnimationSystem.ValuesFontDefinition m_FontDefinitions;

		// Token: 0x04000D27 RID: 3367
		private StylePropertyAnimationSystem.ValuesFont m_Fonts;

		// Token: 0x04000D28 RID: 3368
		private StylePropertyAnimationSystem.ValuesTextShadow m_TextShadows;

		// Token: 0x04000D29 RID: 3369
		private StylePropertyAnimationSystem.ValuesScale m_Scale;

		// Token: 0x04000D2A RID: 3370
		private StylePropertyAnimationSystem.ValuesRotate m_Rotate;

		// Token: 0x04000D2B RID: 3371
		private StylePropertyAnimationSystem.ValuesTranslate m_Translate;

		// Token: 0x04000D2C RID: 3372
		private StylePropertyAnimationSystem.ValuesTransformOrigin m_TransformOrigin;

		// Token: 0x04000D2D RID: 3373
		private StylePropertyAnimationSystem.ValuesBackgroundPosition m_BackgroundPosition;

		// Token: 0x04000D2E RID: 3374
		private StylePropertyAnimationSystem.ValuesBackgroundRepeat m_BackgroundRepeat;

		// Token: 0x04000D2F RID: 3375
		private StylePropertyAnimationSystem.ValuesBackgroundSize m_BackgroundSize;

		// Token: 0x04000D30 RID: 3376
		private readonly List<StylePropertyAnimationSystem.Values> m_AllValues = new List<StylePropertyAnimationSystem.Values>();

		// Token: 0x04000D31 RID: 3377
		private readonly Dictionary<StylePropertyId, StylePropertyAnimationSystem.Values> m_PropertyToValues = new Dictionary<StylePropertyId, StylePropertyAnimationSystem.Values>();

		// Token: 0x02000406 RID: 1030
		[Flags]
		private enum TransitionState
		{
			// Token: 0x04000D33 RID: 3379
			None = 0,
			// Token: 0x04000D34 RID: 3380
			Running = 1,
			// Token: 0x04000D35 RID: 3381
			Started = 2,
			// Token: 0x04000D36 RID: 3382
			Ended = 4,
			// Token: 0x04000D37 RID: 3383
			Canceled = 8
		}

		// Token: 0x02000407 RID: 1031
		private struct AnimationDataSet<TTimingData, TStyleData>
		{
			// Token: 0x17000849 RID: 2121
			// (get) Token: 0x06001E32 RID: 7730 RVA: 0x0006DA1B File Offset: 0x0006BC1B
			// (set) Token: 0x06001E33 RID: 7731 RVA: 0x0006DA25 File Offset: 0x0006BC25
			private int capacity
			{
				get
				{
					return this.elements.Length;
				}
				set
				{
					Array.Resize<VisualElement>(ref this.elements, value);
					Array.Resize<StylePropertyId>(ref this.properties, value);
					Array.Resize<TTimingData>(ref this.timing, value);
					Array.Resize<TStyleData>(ref this.style, value);
				}
			}

			// Token: 0x06001E34 RID: 7732 RVA: 0x0006DA5C File Offset: 0x0006BC5C
			private void LocalInit()
			{
				this.elements = new VisualElement[2];
				this.properties = new StylePropertyId[2];
				this.timing = new TTimingData[2];
				this.style = new TStyleData[2];
				this.indices = new Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, int>(StylePropertyAnimationSystem.ElementPropertyPair.Comparer);
			}

			// Token: 0x06001E35 RID: 7733 RVA: 0x0006DAAC File Offset: 0x0006BCAC
			public static StylePropertyAnimationSystem.AnimationDataSet<TTimingData, TStyleData> Create()
			{
				StylePropertyAnimationSystem.AnimationDataSet<TTimingData, TStyleData> result = default(StylePropertyAnimationSystem.AnimationDataSet<TTimingData, TStyleData>);
				result.LocalInit();
				return result;
			}

			// Token: 0x06001E36 RID: 7734 RVA: 0x0006DAD0 File Offset: 0x0006BCD0
			public bool IndexOf(VisualElement ve, StylePropertyId prop, out int index)
			{
				return this.indices.TryGetValue(new StylePropertyAnimationSystem.ElementPropertyPair(ve, prop), out index);
			}

			// Token: 0x06001E37 RID: 7735 RVA: 0x0006DAF8 File Offset: 0x0006BCF8
			public void Add(VisualElement owner, StylePropertyId prop, TTimingData timingData, TStyleData styleData)
			{
				bool flag = this.count >= this.capacity;
				if (flag)
				{
					this.capacity *= 2;
				}
				int num = this.count;
				this.count = num + 1;
				int index = num;
				this.elements[index] = owner;
				this.properties[index] = prop;
				this.timing[index] = timingData;
				this.style[index] = styleData;
				this.indices.Add(new StylePropertyAnimationSystem.ElementPropertyPair(owner, prop), index);
			}

			// Token: 0x06001E38 RID: 7736 RVA: 0x0006DB80 File Offset: 0x0006BD80
			public void Remove(int cancelledIndex)
			{
				int num = this.count - 1;
				this.count = num;
				int lastIndex = num;
				this.indices.Remove(new StylePropertyAnimationSystem.ElementPropertyPair(this.elements[cancelledIndex], this.properties[cancelledIndex]));
				bool flag = cancelledIndex != lastIndex;
				if (flag)
				{
					VisualElement movedElement = (this.elements[cancelledIndex] = this.elements[lastIndex]);
					StylePropertyId movedProperty = (this.properties[cancelledIndex] = this.properties[lastIndex]);
					this.timing[cancelledIndex] = this.timing[lastIndex];
					this.style[cancelledIndex] = this.style[lastIndex];
					this.indices[new StylePropertyAnimationSystem.ElementPropertyPair(movedElement, movedProperty)] = cancelledIndex;
				}
				this.elements[lastIndex] = null;
				this.properties[lastIndex] = StylePropertyId.Unknown;
				this.timing[lastIndex] = default(TTimingData);
				this.style[lastIndex] = default(TStyleData);
			}

			// Token: 0x06001E39 RID: 7737 RVA: 0x0006DC7E File Offset: 0x0006BE7E
			public void Replace(int index, TTimingData timingData, TStyleData styleData)
			{
				this.timing[index] = timingData;
				this.style[index] = styleData;
			}

			// Token: 0x06001E3A RID: 7738 RVA: 0x0006DC9C File Offset: 0x0006BE9C
			public void RemoveAll(VisualElement ve)
			{
				int i = this.count;
				for (int j = i - 1; j >= 0; j--)
				{
					bool flag = this.elements[j] == ve;
					if (flag)
					{
						this.Remove(j);
					}
				}
			}

			// Token: 0x06001E3B RID: 7739 RVA: 0x0006DCE0 File Offset: 0x0006BEE0
			public void RemoveAll()
			{
				this.capacity = 2;
				int usedSize = Mathf.Min(this.count, this.capacity);
				Array.Clear(this.elements, 0, usedSize);
				Array.Clear(this.properties, 0, usedSize);
				Array.Clear(this.timing, 0, usedSize);
				Array.Clear(this.style, 0, usedSize);
				this.count = 0;
				this.indices.Clear();
			}

			// Token: 0x06001E3C RID: 7740 RVA: 0x0006DD54 File Offset: 0x0006BF54
			public void GetActivePropertiesForElement(VisualElement ve, List<StylePropertyId> outProperties)
			{
				int i = this.count;
				for (int j = i - 1; j >= 0; j--)
				{
					bool flag = this.elements[j] == ve;
					if (flag)
					{
						outProperties.Add(this.properties[j]);
					}
				}
			}

			// Token: 0x04000D38 RID: 3384
			public VisualElement[] elements;

			// Token: 0x04000D39 RID: 3385
			public StylePropertyId[] properties;

			// Token: 0x04000D3A RID: 3386
			public TTimingData[] timing;

			// Token: 0x04000D3B RID: 3387
			public TStyleData[] style;

			// Token: 0x04000D3C RID: 3388
			public int count;

			// Token: 0x04000D3D RID: 3389
			private Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, int> indices;
		}

		// Token: 0x02000408 RID: 1032
		private struct ElementPropertyPair
		{
			// Token: 0x06001E3D RID: 7741 RVA: 0x0006DD9E File Offset: 0x0006BF9E
			public ElementPropertyPair(VisualElement element, StylePropertyId property)
			{
				this.element = element;
				this.property = property;
			}

			// Token: 0x04000D3E RID: 3390
			public static readonly IEqualityComparer<StylePropertyAnimationSystem.ElementPropertyPair> Comparer = new StylePropertyAnimationSystem.ElementPropertyPair.EqualityComparer();

			// Token: 0x04000D3F RID: 3391
			public readonly VisualElement element;

			// Token: 0x04000D40 RID: 3392
			public readonly StylePropertyId property;

			// Token: 0x02000409 RID: 1033
			private class EqualityComparer : IEqualityComparer<StylePropertyAnimationSystem.ElementPropertyPair>
			{
				// Token: 0x06001E3F RID: 7743 RVA: 0x0006DDBC File Offset: 0x0006BFBC
				public bool Equals(StylePropertyAnimationSystem.ElementPropertyPair x, StylePropertyAnimationSystem.ElementPropertyPair y)
				{
					return x.element == y.element && x.property == y.property;
				}

				// Token: 0x06001E40 RID: 7744 RVA: 0x0006DDF0 File Offset: 0x0006BFF0
				public int GetHashCode(StylePropertyAnimationSystem.ElementPropertyPair obj)
				{
					return (obj.element.GetHashCode() * 397) ^ (int)obj.property;
				}
			}
		}

		// Token: 0x0200040A RID: 1034
		private abstract class Values
		{
			// Token: 0x06001E42 RID: 7746
			public abstract void CancelAllAnimations();

			// Token: 0x06001E43 RID: 7747
			public abstract void CancelAllAnimations(VisualElement ve);

			// Token: 0x06001E44 RID: 7748
			public abstract void CancelAnimation(VisualElement ve, StylePropertyId id);

			// Token: 0x06001E45 RID: 7749
			public abstract void UpdateAnimation(VisualElement ve, StylePropertyId id);

			// Token: 0x06001E46 RID: 7750
			public abstract void GetAllAnimations(VisualElement ve, List<StylePropertyId> outPropertyIds);

			// Token: 0x06001E47 RID: 7751
			public abstract void Update(long currentTimeMs);

			// Token: 0x06001E48 RID: 7752
			protected abstract void UpdateValues();

			// Token: 0x06001E49 RID: 7753
			protected abstract void UpdateComputedStyle();

			// Token: 0x06001E4A RID: 7754
			protected abstract void UpdateComputedStyle(int i);
		}

		// Token: 0x0200040B RID: 1035
		private abstract class Values<T> : StylePropertyAnimationSystem.Values
		{
			// Token: 0x1700084A RID: 2122
			// (get) Token: 0x06001E4C RID: 7756 RVA: 0x0006DE1B File Offset: 0x0006C01B
			public bool isEmpty
			{
				get
				{
					return this.running.count + this.completed.count == 0;
				}
			}

			// Token: 0x1700084B RID: 2123
			// (get) Token: 0x06001E4D RID: 7757
			public abstract Func<T, T, bool> SameFunc { get; }

			// Token: 0x06001E4E RID: 7758 RVA: 0x0006DE38 File Offset: 0x0006C038
			protected virtual bool ConvertUnits(VisualElement owner, StylePropertyId prop, ref T a, ref T b)
			{
				return true;
			}

			// Token: 0x06001E4F RID: 7759 RVA: 0x0006DE4C File Offset: 0x0006C04C
			protected Values()
			{
				this.running = StylePropertyAnimationSystem.AnimationDataSet<StylePropertyAnimationSystem.Values<T>.TimingData, StylePropertyAnimationSystem.Values<T>.StyleData>.Create();
				this.completed = StylePropertyAnimationSystem.AnimationDataSet<StylePropertyAnimationSystem.Values<T>.EmptyData, T>.Create();
				this.m_CurrentTimeMs = Panel.TimeSinceStartupMs();
			}

			// Token: 0x06001E50 RID: 7760 RVA: 0x0006DEA0 File Offset: 0x0006C0A0
			private void SwapFrameStates()
			{
				StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState temp = this.m_CurrentFrameEventsState;
				this.m_CurrentFrameEventsState = this.m_NextFrameEventsState;
				this.m_NextFrameEventsState = temp;
			}

			// Token: 0x06001E51 RID: 7761 RVA: 0x0006DEC8 File Offset: 0x0006C0C8
			private void QueueEvent(EventBase evt, StylePropertyAnimationSystem.ElementPropertyPair epp)
			{
				evt.elementTarget = epp.element;
				Queue<EventBase> queue;
				bool flag = !this.m_NextFrameEventsState.elementPropertyQueuedEvents.TryGetValue(epp, out queue);
				if (flag)
				{
					queue = StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState.GetPooledQueue();
					this.m_NextFrameEventsState.elementPropertyQueuedEvents.Add(epp, queue);
				}
				queue.Enqueue(evt);
				bool flag2 = this.m_NextFrameEventsState.panel == null;
				if (flag2)
				{
					this.m_NextFrameEventsState.panel = epp.element.panel;
				}
				this.m_NextFrameEventsState.RegisterChange();
			}

			// Token: 0x06001E52 RID: 7762 RVA: 0x0006DF54 File Offset: 0x0006C154
			private void ClearEventQueue(StylePropertyAnimationSystem.ElementPropertyPair epp)
			{
				Queue<EventBase> queue;
				bool flag = this.m_NextFrameEventsState.elementPropertyQueuedEvents.TryGetValue(epp, out queue);
				if (flag)
				{
					while (queue.Count > 0)
					{
						queue.Dequeue().Dispose();
						this.m_NextFrameEventsState.UnregisterChange();
					}
				}
			}

			// Token: 0x06001E53 RID: 7763 RVA: 0x0006DFA4 File Offset: 0x0006C1A4
			private void QueueTransitionRunEvent(VisualElement ve, int runningIndex)
			{
				bool flag = !ve.HasParentEventInterests(EventCategory.StyleTransition);
				if (!flag)
				{
					StylePropertyId stylePropertyId = this.running.properties[runningIndex];
					StylePropertyAnimationSystem.ElementPropertyPair epp = new StylePropertyAnimationSystem.ElementPropertyPair(ve, stylePropertyId);
					StylePropertyAnimationSystem.TransitionState state;
					bool flag2 = this.m_NextFrameEventsState.elementPropertyStateDelta.TryGetValue(epp, out state);
					if (flag2)
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta[epp] = state | StylePropertyAnimationSystem.TransitionState.Running;
					}
					else
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta.Add(epp, StylePropertyAnimationSystem.TransitionState.Running);
					}
					ref StylePropertyAnimationSystem.Values<T>.TimingData timingData = ref this.running.timing[runningIndex];
					int elapsedTimeMs = ((timingData.delayMs < 0) ? Mathf.Min(Mathf.Max(-timingData.delayMs, 0), timingData.durationMs) : 0);
					TransitionRunEvent evt = TransitionEventBase<TransitionRunEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)elapsedTimeMs / 1000f));
					this.QueueEvent(evt, epp);
				}
			}

			// Token: 0x06001E54 RID: 7764 RVA: 0x0006E07C File Offset: 0x0006C27C
			private void QueueTransitionStartEvent(VisualElement ve, int runningIndex)
			{
				bool flag = !ve.HasParentEventInterests(EventCategory.StyleTransition);
				if (!flag)
				{
					StylePropertyId stylePropertyId = this.running.properties[runningIndex];
					StylePropertyAnimationSystem.ElementPropertyPair epp = new StylePropertyAnimationSystem.ElementPropertyPair(ve, stylePropertyId);
					StylePropertyAnimationSystem.TransitionState state;
					bool flag2 = this.m_NextFrameEventsState.elementPropertyStateDelta.TryGetValue(epp, out state);
					if (flag2)
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta[epp] = state | StylePropertyAnimationSystem.TransitionState.Started;
					}
					else
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta.Add(epp, StylePropertyAnimationSystem.TransitionState.Started);
					}
					ref StylePropertyAnimationSystem.Values<T>.TimingData timingData = ref this.running.timing[runningIndex];
					int elapsedTimeMs = ((timingData.delayMs < 0) ? Mathf.Min(Mathf.Max(-timingData.delayMs, 0), timingData.durationMs) : 0);
					TransitionStartEvent evt = TransitionEventBase<TransitionStartEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)elapsedTimeMs / 1000f));
					this.QueueEvent(evt, epp);
				}
			}

			// Token: 0x06001E55 RID: 7765 RVA: 0x0006E154 File Offset: 0x0006C354
			private void QueueTransitionEndEvent(VisualElement ve, int runningIndex)
			{
				bool flag = !ve.HasParentEventInterests(EventCategory.StyleTransition);
				if (!flag)
				{
					StylePropertyId stylePropertyId = this.running.properties[runningIndex];
					StylePropertyAnimationSystem.ElementPropertyPair epp = new StylePropertyAnimationSystem.ElementPropertyPair(ve, stylePropertyId);
					StylePropertyAnimationSystem.TransitionState state;
					bool flag2 = this.m_NextFrameEventsState.elementPropertyStateDelta.TryGetValue(epp, out state);
					if (flag2)
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta[epp] = state | StylePropertyAnimationSystem.TransitionState.Ended;
					}
					else
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta.Add(epp, StylePropertyAnimationSystem.TransitionState.Ended);
					}
					ref StylePropertyAnimationSystem.Values<T>.TimingData timingData = ref this.running.timing[runningIndex];
					TransitionEndEvent evt = TransitionEventBase<TransitionEndEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)timingData.durationMs / 1000f));
					this.QueueEvent(evt, epp);
				}
			}

			// Token: 0x06001E56 RID: 7766 RVA: 0x0006E20C File Offset: 0x0006C40C
			private void QueueTransitionCancelEvent(VisualElement ve, int runningIndex, long panelElapsedMs)
			{
				bool flag = !ve.HasParentEventInterests(EventCategory.StyleTransition);
				if (!flag)
				{
					StylePropertyId stylePropertyId = this.running.properties[runningIndex];
					StylePropertyAnimationSystem.ElementPropertyPair epp = new StylePropertyAnimationSystem.ElementPropertyPair(ve, stylePropertyId);
					StylePropertyAnimationSystem.TransitionState state;
					bool flag2 = this.m_NextFrameEventsState.elementPropertyStateDelta.TryGetValue(epp, out state);
					bool sendCancelEvent;
					if (flag2)
					{
						bool flag3 = state == StylePropertyAnimationSystem.TransitionState.None || (state & StylePropertyAnimationSystem.TransitionState.Canceled) == StylePropertyAnimationSystem.TransitionState.Canceled;
						if (flag3)
						{
							this.m_NextFrameEventsState.elementPropertyStateDelta[epp] = StylePropertyAnimationSystem.TransitionState.Canceled;
							this.ClearEventQueue(epp);
							sendCancelEvent = true;
						}
						else
						{
							this.m_NextFrameEventsState.elementPropertyStateDelta[epp] = StylePropertyAnimationSystem.TransitionState.None;
							this.ClearEventQueue(epp);
							sendCancelEvent = false;
						}
					}
					else
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta.Add(epp, StylePropertyAnimationSystem.TransitionState.Canceled);
						sendCancelEvent = true;
					}
					bool flag4 = !sendCancelEvent;
					if (!flag4)
					{
						ref StylePropertyAnimationSystem.Values<T>.TimingData timingData = ref this.running.timing[runningIndex];
						long elapsedTimeMs = (timingData.isStarted ? (panelElapsedMs - timingData.startTimeMs) : 0L);
						bool flag5 = timingData.delayMs < 0;
						if (flag5)
						{
							elapsedTimeMs = (long)(-(long)timingData.delayMs) + elapsedTimeMs;
						}
						TransitionCancelEvent evt = TransitionEventBase<TransitionCancelEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)elapsedTimeMs / 1000f));
						this.QueueEvent(evt, epp);
					}
				}
			}

			// Token: 0x06001E57 RID: 7767 RVA: 0x0006E348 File Offset: 0x0006C548
			private void SendTransitionCancelEvent(VisualElement ve, int runningIndex, long panelElapsedMs)
			{
				bool flag = !ve.HasParentEventInterests(EventBase<TransitionCancelEvent>.EventCategory);
				if (!flag)
				{
					ref StylePropertyAnimationSystem.Values<T>.TimingData timingData = ref this.running.timing[runningIndex];
					StylePropertyId stylePropertyId = this.running.properties[runningIndex];
					long elapsedTimeMs = (timingData.isStarted ? (panelElapsedMs - timingData.startTimeMs) : 0L);
					bool flag2 = timingData.delayMs < 0;
					if (flag2)
					{
						elapsedTimeMs = (long)(-(long)timingData.delayMs) + elapsedTimeMs;
					}
					using (TransitionCancelEvent evt = TransitionEventBase<TransitionCancelEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)elapsedTimeMs / 1000f)))
					{
						evt.elementTarget = ve;
						ve.SendEvent(evt);
					}
				}
			}

			// Token: 0x06001E58 RID: 7768 RVA: 0x0006E408 File Offset: 0x0006C608
			public sealed override void CancelAllAnimations()
			{
				int runningCount = this.running.count;
				bool flag = runningCount > 0;
				if (flag)
				{
					using (new EventDispatcherGate(this.running.elements[0].panel.dispatcher))
					{
						for (int i = 0; i < runningCount; i++)
						{
							VisualElement ve = this.running.elements[i];
							this.SendTransitionCancelEvent(ve, i, this.m_CurrentTimeMs);
							this.ForceComputedStyleEndValue(i);
							IStylePropertyAnimations styleAnimation = ve.styleAnimation;
							int num = styleAnimation.runningAnimationCount;
							styleAnimation.runningAnimationCount = num - 1;
						}
					}
					this.running.RemoveAll();
				}
				int completedCount = this.completed.count;
				for (int j = 0; j < completedCount; j++)
				{
					VisualElement ve2 = this.completed.elements[j];
					IStylePropertyAnimations styleAnimation2 = ve2.styleAnimation;
					int num = styleAnimation2.completedAnimationCount;
					styleAnimation2.completedAnimationCount = num - 1;
				}
				this.completed.RemoveAll();
			}

			// Token: 0x06001E59 RID: 7769 RVA: 0x0006E530 File Offset: 0x0006C730
			public sealed override void CancelAllAnimations(VisualElement ve)
			{
				int count = this.running.count;
				bool flag = count > 0;
				if (flag)
				{
					using (new EventDispatcherGate(this.running.elements[0].panel.dispatcher))
					{
						for (int i = 0; i < count; i++)
						{
							bool flag2 = this.running.elements[i] == ve;
							if (flag2)
							{
								this.SendTransitionCancelEvent(ve, i, this.m_CurrentTimeMs);
								this.ForceComputedStyleEndValue(i);
								IStylePropertyAnimations styleAnimation = this.running.elements[i].styleAnimation;
								int num = styleAnimation.runningAnimationCount;
								styleAnimation.runningAnimationCount = num - 1;
							}
						}
					}
				}
				this.running.RemoveAll(ve);
				int completedCount = this.completed.count;
				for (int j = 0; j < completedCount; j++)
				{
					bool flag3 = this.completed.elements[j] == ve;
					if (flag3)
					{
						IStylePropertyAnimations styleAnimation2 = this.completed.elements[j].styleAnimation;
						int num = styleAnimation2.completedAnimationCount;
						styleAnimation2.completedAnimationCount = num - 1;
					}
				}
				this.completed.RemoveAll(ve);
			}

			// Token: 0x06001E5A RID: 7770 RVA: 0x0006E684 File Offset: 0x0006C884
			public sealed override void CancelAnimation(VisualElement ve, StylePropertyId id)
			{
				int runningIndex;
				bool flag = this.running.IndexOf(ve, id, out runningIndex);
				if (flag)
				{
					this.QueueTransitionCancelEvent(ve, runningIndex, this.m_CurrentTimeMs);
					this.ForceComputedStyleEndValue(runningIndex);
					this.running.Remove(runningIndex);
					IStylePropertyAnimations styleAnimation = ve.styleAnimation;
					int num = styleAnimation.runningAnimationCount;
					styleAnimation.runningAnimationCount = num - 1;
				}
				int completedIndex;
				bool flag2 = this.completed.IndexOf(ve, id, out completedIndex);
				if (flag2)
				{
					this.completed.Remove(completedIndex);
					IStylePropertyAnimations styleAnimation2 = ve.styleAnimation;
					int num = styleAnimation2.completedAnimationCount;
					styleAnimation2.completedAnimationCount = num - 1;
				}
			}

			// Token: 0x06001E5B RID: 7771 RVA: 0x0006E71C File Offset: 0x0006C91C
			public sealed override void UpdateAnimation(VisualElement ve, StylePropertyId id)
			{
				int runningIndex;
				bool flag = this.running.IndexOf(ve, id, out runningIndex);
				if (flag)
				{
					this.UpdateComputedStyle(runningIndex);
				}
			}

			// Token: 0x06001E5C RID: 7772 RVA: 0x0006E745 File Offset: 0x0006C945
			public sealed override void GetAllAnimations(VisualElement ve, List<StylePropertyId> outPropertyIds)
			{
				this.running.GetActivePropertiesForElement(ve, outPropertyIds);
				this.completed.GetActivePropertiesForElement(ve, outPropertyIds);
			}

			// Token: 0x06001E5D RID: 7773 RVA: 0x0006E764 File Offset: 0x0006C964
			private float ComputeReversingShorteningFactor(int oldIndex)
			{
				ref StylePropertyAnimationSystem.Values<T>.TimingData timingData = ref this.running.timing[oldIndex];
				return Mathf.Clamp01(Mathf.Abs(1f - (1f - timingData.easedProgress) * timingData.reversingShorteningFactor));
			}

			// Token: 0x06001E5E RID: 7774 RVA: 0x0006E7AC File Offset: 0x0006C9AC
			private int ComputeReversingDuration(int newTransitionDurationMs, float newReversingShorteningFactor)
			{
				return Mathf.RoundToInt((float)newTransitionDurationMs * newReversingShorteningFactor);
			}

			// Token: 0x06001E5F RID: 7775 RVA: 0x0006E7C8 File Offset: 0x0006C9C8
			private int ComputeReversingDelay(int delayMs, float newReversingShorteningFactor)
			{
				return (delayMs < 0) ? Mathf.RoundToInt((float)delayMs * newReversingShorteningFactor) : delayMs;
			}

			// Token: 0x06001E60 RID: 7776 RVA: 0x0006E7EC File Offset: 0x0006C9EC
			public bool StartTransition(VisualElement owner, StylePropertyId prop, T startValue, T endValue, int durationMs, int delayMs, Func<float, float> easingCurve, long currentTimeMs)
			{
				long startTimeMs = currentTimeMs + (long)delayMs;
				StylePropertyAnimationSystem.Values<T>.TimingData timing = new StylePropertyAnimationSystem.Values<T>.TimingData
				{
					startTimeMs = startTimeMs,
					durationMs = durationMs,
					easingCurve = easingCurve,
					reversingShorteningFactor = 1f,
					delayMs = delayMs
				};
				StylePropertyAnimationSystem.Values<T>.StyleData style = new StylePropertyAnimationSystem.Values<T>.StyleData
				{
					startValue = startValue,
					endValue = endValue,
					currentValue = startValue,
					reversingAdjustedStartValue = startValue
				};
				int combinedDuration = Mathf.Max(0, durationMs) + delayMs;
				bool flag = !this.ConvertUnits(owner, prop, ref style.startValue, ref style.endValue);
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					int completedIndex;
					bool flag3 = this.completed.IndexOf(owner, prop, out completedIndex);
					if (flag3)
					{
						bool flag4 = this.SameFunc(endValue, this.completed.style[completedIndex]);
						if (flag4)
						{
							return false;
						}
						bool flag5 = combinedDuration <= 0;
						if (flag5)
						{
							return false;
						}
						this.completed.Remove(completedIndex);
						IStylePropertyAnimations styleAnimation = owner.styleAnimation;
						int num = styleAnimation.completedAnimationCount;
						styleAnimation.completedAnimationCount = num - 1;
					}
					int index;
					bool flag6 = this.running.IndexOf(owner, prop, out index);
					if (flag6)
					{
						bool flag7 = this.SameFunc(endValue, this.running.style[index].endValue);
						if (flag7)
						{
							flag2 = false;
						}
						else
						{
							bool flag8 = this.SameFunc(endValue, this.running.style[index].currentValue);
							if (flag8)
							{
								this.QueueTransitionCancelEvent(owner, index, currentTimeMs);
								this.running.Remove(index);
								IStylePropertyAnimations styleAnimation2 = owner.styleAnimation;
								int num = styleAnimation2.runningAnimationCount;
								styleAnimation2.runningAnimationCount = num - 1;
								flag2 = false;
							}
							else
							{
								bool flag9 = combinedDuration <= 0;
								if (flag9)
								{
									this.QueueTransitionCancelEvent(owner, index, currentTimeMs);
									this.running.Remove(index);
									IStylePropertyAnimations styleAnimation3 = owner.styleAnimation;
									int num = styleAnimation3.runningAnimationCount;
									styleAnimation3.runningAnimationCount = num - 1;
									flag2 = false;
								}
								else
								{
									style.startValue = this.running.style[index].currentValue;
									bool flag10 = !this.ConvertUnits(owner, prop, ref style.startValue, ref style.endValue);
									if (flag10)
									{
										this.QueueTransitionCancelEvent(owner, index, currentTimeMs);
										this.running.Remove(index);
										IStylePropertyAnimations styleAnimation4 = owner.styleAnimation;
										int num = styleAnimation4.runningAnimationCount;
										styleAnimation4.runningAnimationCount = num - 1;
										flag2 = false;
									}
									else
									{
										style.currentValue = style.startValue;
										bool flag11 = this.SameFunc(endValue, this.running.style[index].reversingAdjustedStartValue);
										if (flag11)
										{
											float rsf = (timing.reversingShorteningFactor = this.ComputeReversingShorteningFactor(index));
											timing.startTimeMs = currentTimeMs + (long)this.ComputeReversingDelay(delayMs, rsf);
											timing.durationMs = this.ComputeReversingDuration(durationMs, rsf);
											style.reversingAdjustedStartValue = this.running.style[index].endValue;
										}
										this.running.timing[index].isStarted = false;
										this.QueueTransitionCancelEvent(owner, index, currentTimeMs);
										this.QueueTransitionRunEvent(owner, index);
										this.running.Replace(index, timing, style);
										flag2 = true;
									}
								}
							}
						}
					}
					else
					{
						bool flag12 = combinedDuration <= 0;
						if (flag12)
						{
							flag2 = false;
						}
						else
						{
							bool flag13 = this.SameFunc(startValue, endValue);
							if (flag13)
							{
								flag2 = false;
							}
							else
							{
								this.running.Add(owner, prop, timing, style);
								IStylePropertyAnimations styleAnimation5 = owner.styleAnimation;
								int num = styleAnimation5.runningAnimationCount;
								styleAnimation5.runningAnimationCount = num + 1;
								this.QueueTransitionRunEvent(owner, this.running.count - 1);
								flag2 = true;
							}
						}
					}
				}
				return flag2;
			}

			// Token: 0x06001E61 RID: 7777 RVA: 0x0006EBD4 File Offset: 0x0006CDD4
			private void ForceComputedStyleEndValue(int runningIndex)
			{
				ref StylePropertyAnimationSystem.Values<T>.StyleData style = ref this.running.style[runningIndex];
				style.currentValue = style.endValue;
				this.UpdateComputedStyle(runningIndex);
			}

			// Token: 0x06001E62 RID: 7778 RVA: 0x0006EC08 File Offset: 0x0006CE08
			public sealed override void Update(long currentTimeMs)
			{
				this.m_CurrentTimeMs = currentTimeMs;
				this.UpdateProgress(currentTimeMs);
				this.UpdateValues();
				this.UpdateComputedStyle();
				bool flag = this.m_NextFrameEventsState.StateChanged();
				if (flag)
				{
					this.ProcessEventQueue();
				}
			}

			// Token: 0x06001E63 RID: 7779 RVA: 0x0006EC4C File Offset: 0x0006CE4C
			private void ProcessEventQueue()
			{
				this.SwapFrameStates();
				IPanel panel = this.m_CurrentFrameEventsState.panel;
				EventDispatcher d = ((panel != null) ? panel.dispatcher : null);
				using (new EventDispatcherGate(d))
				{
					foreach (KeyValuePair<StylePropertyAnimationSystem.ElementPropertyPair, Queue<EventBase>> kvp in this.m_CurrentFrameEventsState.elementPropertyQueuedEvents)
					{
						StylePropertyAnimationSystem.ElementPropertyPair epp = kvp.Key;
						Queue<EventBase> queue = kvp.Value;
						VisualElement element = kvp.Key.element;
						while (queue.Count > 0)
						{
							EventBase evt = queue.Dequeue();
							element.SendEvent(evt);
							evt.Dispose();
						}
					}
					this.m_CurrentFrameEventsState.Clear();
				}
			}

			// Token: 0x06001E64 RID: 7780 RVA: 0x0006ED44 File Offset: 0x0006CF44
			private void UpdateProgress(long currentTimeMs)
			{
				int i = this.running.count;
				bool flag = i > 0;
				if (flag)
				{
					for (int j = 0; j < i; j++)
					{
						ref StylePropertyAnimationSystem.Values<T>.TimingData timing = ref this.running.timing[j];
						bool flag2 = currentTimeMs < timing.startTimeMs;
						if (flag2)
						{
							timing.easedProgress = 0f;
						}
						else
						{
							bool flag3 = currentTimeMs >= timing.startTimeMs + (long)timing.durationMs;
							if (flag3)
							{
								ref StylePropertyAnimationSystem.Values<T>.StyleData style = ref this.running.style[j];
								ref VisualElement owner = ref this.running.elements[j];
								style.currentValue = style.endValue;
								this.UpdateComputedStyle(j);
								this.completed.Add(owner, this.running.properties[j], StylePropertyAnimationSystem.Values<T>.EmptyData.Default, style.endValue);
								IStylePropertyAnimations styleAnimation = owner.styleAnimation;
								int num = styleAnimation.runningAnimationCount;
								styleAnimation.runningAnimationCount = num - 1;
								IStylePropertyAnimations styleAnimation2 = owner.styleAnimation;
								num = styleAnimation2.completedAnimationCount;
								styleAnimation2.completedAnimationCount = num + 1;
								this.QueueTransitionEndEvent(owner, j);
								this.running.Remove(j);
								j--;
								i--;
							}
							else
							{
								bool flag4 = !timing.isStarted;
								if (flag4)
								{
									timing.isStarted = true;
									this.QueueTransitionStartEvent(this.running.elements[j], j);
								}
								float progress = (float)(currentTimeMs - timing.startTimeMs) / (float)timing.durationMs;
								timing.easedProgress = timing.easingCurve(progress);
							}
						}
					}
				}
			}

			// Token: 0x04000D41 RID: 3393
			private long m_CurrentTimeMs = 0L;

			// Token: 0x04000D42 RID: 3394
			private StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState m_CurrentFrameEventsState = new StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState();

			// Token: 0x04000D43 RID: 3395
			private StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState m_NextFrameEventsState = new StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState();

			// Token: 0x04000D44 RID: 3396
			public StylePropertyAnimationSystem.AnimationDataSet<StylePropertyAnimationSystem.Values<T>.TimingData, StylePropertyAnimationSystem.Values<T>.StyleData> running;

			// Token: 0x04000D45 RID: 3397
			public StylePropertyAnimationSystem.AnimationDataSet<StylePropertyAnimationSystem.Values<T>.EmptyData, T> completed;

			// Token: 0x0200040C RID: 1036
			private class TransitionEventsFrameState
			{
				// Token: 0x06001E65 RID: 7781 RVA: 0x0006EEE8 File Offset: 0x0006D0E8
				public static Queue<EventBase> GetPooledQueue()
				{
					return StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState.k_EventQueuePool.Get();
				}

				// Token: 0x06001E66 RID: 7782 RVA: 0x0006EF04 File Offset: 0x0006D104
				public void RegisterChange()
				{
					this.m_ChangesCount++;
				}

				// Token: 0x06001E67 RID: 7783 RVA: 0x0006EF15 File Offset: 0x0006D115
				public void UnregisterChange()
				{
					this.m_ChangesCount--;
				}

				// Token: 0x06001E68 RID: 7784 RVA: 0x0006EF28 File Offset: 0x0006D128
				public bool StateChanged()
				{
					return this.m_ChangesCount > 0;
				}

				// Token: 0x06001E69 RID: 7785 RVA: 0x0006EF44 File Offset: 0x0006D144
				public void Clear()
				{
					foreach (KeyValuePair<StylePropertyAnimationSystem.ElementPropertyPair, Queue<EventBase>> kvp in this.elementPropertyQueuedEvents)
					{
						kvp.Value.Clear();
						StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState.k_EventQueuePool.Release(kvp.Value);
					}
					this.elementPropertyQueuedEvents.Clear();
					this.elementPropertyStateDelta.Clear();
					this.panel = null;
					this.m_ChangesCount = 0;
				}

				// Token: 0x04000D46 RID: 3398
				private static readonly ObjectPool<Queue<EventBase>> k_EventQueuePool = new ObjectPool<Queue<EventBase>>(() => new Queue<EventBase>(4), null, null, null, true, 10, 10000);

				// Token: 0x04000D47 RID: 3399
				public readonly Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, StylePropertyAnimationSystem.TransitionState> elementPropertyStateDelta = new Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, StylePropertyAnimationSystem.TransitionState>(StylePropertyAnimationSystem.ElementPropertyPair.Comparer);

				// Token: 0x04000D48 RID: 3400
				public readonly Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, Queue<EventBase>> elementPropertyQueuedEvents = new Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, Queue<EventBase>>(StylePropertyAnimationSystem.ElementPropertyPair.Comparer);

				// Token: 0x04000D49 RID: 3401
				public IPanel panel;

				// Token: 0x04000D4A RID: 3402
				private int m_ChangesCount;
			}

			// Token: 0x0200040E RID: 1038
			public struct TimingData
			{
				// Token: 0x04000D4C RID: 3404
				public long startTimeMs;

				// Token: 0x04000D4D RID: 3405
				public int durationMs;

				// Token: 0x04000D4E RID: 3406
				public Func<float, float> easingCurve;

				// Token: 0x04000D4F RID: 3407
				public float easedProgress;

				// Token: 0x04000D50 RID: 3408
				public float reversingShorteningFactor;

				// Token: 0x04000D51 RID: 3409
				public bool isStarted;

				// Token: 0x04000D52 RID: 3410
				public int delayMs;
			}

			// Token: 0x0200040F RID: 1039
			public struct StyleData
			{
				// Token: 0x04000D53 RID: 3411
				public T startValue;

				// Token: 0x04000D54 RID: 3412
				public T endValue;

				// Token: 0x04000D55 RID: 3413
				public T reversingAdjustedStartValue;

				// Token: 0x04000D56 RID: 3414
				public T currentValue;
			}

			// Token: 0x02000410 RID: 1040
			public struct EmptyData
			{
				// Token: 0x04000D57 RID: 3415
				public static StylePropertyAnimationSystem.Values<T>.EmptyData Default = default(StylePropertyAnimationSystem.Values<T>.EmptyData);
			}
		}

		// Token: 0x02000411 RID: 1041
		private class ValuesFloat : StylePropertyAnimationSystem.Values<float>
		{
			// Token: 0x1700084C RID: 2124
			// (get) Token: 0x06001E70 RID: 7792 RVA: 0x0006F04D File Offset: 0x0006D24D
			public override Func<float, float, bool> SameFunc { get; } = new Func<float, float, bool>(StylePropertyAnimationSystem.ValuesFloat.IsSame);

			// Token: 0x06001E71 RID: 7793 RVA: 0x0006F055 File Offset: 0x0006D255
			private static bool IsSame(float a, float b)
			{
				return Mathf.Approximately(a, b);
			}

			// Token: 0x06001E72 RID: 7794 RVA: 0x0006F05E File Offset: 0x0006D25E
			private static float Lerp(float a, float b, float t)
			{
				return Mathf.LerpUnclamped(a, b, t);
			}

			// Token: 0x06001E73 RID: 7795 RVA: 0x0006F068 File Offset: 0x0006D268
			protected sealed override void UpdateValues()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					ref StylePropertyAnimationSystem.Values<float>.TimingData timing = ref this.running.timing[j];
					ref StylePropertyAnimationSystem.Values<float>.StyleData style = ref this.running.style[j];
					style.currentValue = StylePropertyAnimationSystem.ValuesFloat.Lerp(style.startValue, style.endValue, timing.easedProgress);
				}
			}

			// Token: 0x06001E74 RID: 7796 RVA: 0x0006F0D8 File Offset: 0x0006D2D8
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001E75 RID: 7797 RVA: 0x0006F14C File Offset: 0x0006D34C
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x02000412 RID: 1042
		private class ValuesInt : StylePropertyAnimationSystem.Values<int>
		{
			// Token: 0x1700084D RID: 2125
			// (get) Token: 0x06001E77 RID: 7799 RVA: 0x0006F1BD File Offset: 0x0006D3BD
			public override Func<int, int, bool> SameFunc { get; } = new Func<int, int, bool>(StylePropertyAnimationSystem.ValuesInt.IsSame);

			// Token: 0x06001E78 RID: 7800 RVA: 0x0006F1C5 File Offset: 0x0006D3C5
			private static bool IsSame(int a, int b)
			{
				return a == b;
			}

			// Token: 0x06001E79 RID: 7801 RVA: 0x0006F1CB File Offset: 0x0006D3CB
			private static int Lerp(int a, int b, float t)
			{
				return Mathf.RoundToInt(Mathf.LerpUnclamped((float)a, (float)b, t));
			}

			// Token: 0x06001E7A RID: 7802 RVA: 0x0006F1DC File Offset: 0x0006D3DC
			protected sealed override void UpdateValues()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					ref StylePropertyAnimationSystem.Values<int>.TimingData timing = ref this.running.timing[j];
					ref StylePropertyAnimationSystem.Values<int>.StyleData style = ref this.running.style[j];
					style.currentValue = StylePropertyAnimationSystem.ValuesInt.Lerp(style.startValue, style.endValue, timing.easedProgress);
				}
			}

			// Token: 0x06001E7B RID: 7803 RVA: 0x0006F24C File Offset: 0x0006D44C
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001E7C RID: 7804 RVA: 0x0006F2C0 File Offset: 0x0006D4C0
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x02000413 RID: 1043
		private class ValuesLength : StylePropertyAnimationSystem.Values<Length>
		{
			// Token: 0x1700084E RID: 2126
			// (get) Token: 0x06001E7E RID: 7806 RVA: 0x0006F331 File Offset: 0x0006D531
			public override Func<Length, Length, bool> SameFunc { get; } = new Func<Length, Length, bool>(StylePropertyAnimationSystem.ValuesLength.IsSame);

			// Token: 0x06001E7F RID: 7807 RVA: 0x0006F339 File Offset: 0x0006D539
			private static bool IsSame(Length a, Length b)
			{
				return a.unit == b.unit && Mathf.Approximately(a.value, b.value);
			}

			// Token: 0x06001E80 RID: 7808 RVA: 0x0006F364 File Offset: 0x0006D564
			protected sealed override bool ConvertUnits(VisualElement owner, StylePropertyId prop, ref Length a, ref Length b)
			{
				return owner.TryConvertLengthUnits(prop, ref a, ref b, 0);
			}

			// Token: 0x06001E81 RID: 7809 RVA: 0x0006F381 File Offset: 0x0006D581
			internal static Length Lerp(Length a, Length b, float t)
			{
				return new Length(Mathf.LerpUnclamped(a.value, b.value, t), b.unit);
			}

			// Token: 0x06001E82 RID: 7810 RVA: 0x0006F3A4 File Offset: 0x0006D5A4
			protected sealed override void UpdateValues()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					ref StylePropertyAnimationSystem.Values<Length>.TimingData timing = ref this.running.timing[j];
					ref StylePropertyAnimationSystem.Values<Length>.StyleData style = ref this.running.style[j];
					style.currentValue = StylePropertyAnimationSystem.ValuesLength.Lerp(style.startValue, style.endValue, timing.easedProgress);
				}
			}

			// Token: 0x06001E83 RID: 7811 RVA: 0x0006F414 File Offset: 0x0006D614
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001E84 RID: 7812 RVA: 0x0006F488 File Offset: 0x0006D688
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x02000414 RID: 1044
		private class ValuesColor : StylePropertyAnimationSystem.Values<Color>
		{
			// Token: 0x1700084F RID: 2127
			// (get) Token: 0x06001E86 RID: 7814 RVA: 0x0006F4F9 File Offset: 0x0006D6F9
			public override Func<Color, Color, bool> SameFunc { get; } = new Func<Color, Color, bool>(StylePropertyAnimationSystem.ValuesColor.IsSame);

			// Token: 0x06001E87 RID: 7815 RVA: 0x0006F504 File Offset: 0x0006D704
			private static bool IsSame(Color c, Color d)
			{
				return Mathf.Approximately(c.r, d.r) && Mathf.Approximately(c.g, d.g) && Mathf.Approximately(c.b, d.b) && Mathf.Approximately(c.a, d.a);
			}

			// Token: 0x06001E88 RID: 7816 RVA: 0x0006F55E File Offset: 0x0006D75E
			private static Color Lerp(Color a, Color b, float t)
			{
				return Color.LerpUnclamped(a, b, t);
			}

			// Token: 0x06001E89 RID: 7817 RVA: 0x0006F568 File Offset: 0x0006D768
			protected sealed override void UpdateValues()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					ref StylePropertyAnimationSystem.Values<Color>.TimingData timing = ref this.running.timing[j];
					ref StylePropertyAnimationSystem.Values<Color>.StyleData style = ref this.running.style[j];
					style.currentValue = StylePropertyAnimationSystem.ValuesColor.Lerp(style.startValue, style.endValue, timing.easedProgress);
				}
			}

			// Token: 0x06001E8A RID: 7818 RVA: 0x0006F5D8 File Offset: 0x0006D7D8
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001E8B RID: 7819 RVA: 0x0006F64C File Offset: 0x0006D84C
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x02000415 RID: 1045
		private abstract class ValuesDiscrete<T> : StylePropertyAnimationSystem.Values<T>
		{
			// Token: 0x17000850 RID: 2128
			// (get) Token: 0x06001E8D RID: 7821 RVA: 0x0006F6BD File Offset: 0x0006D8BD
			public override Func<T, T, bool> SameFunc { get; } = new Func<T, T, bool>(StylePropertyAnimationSystem.ValuesDiscrete<T>.IsSame);

			// Token: 0x06001E8E RID: 7822 RVA: 0x0006F6C5 File Offset: 0x0006D8C5
			private static bool IsSame(T a, T b)
			{
				return EqualityComparer<T>.Default.Equals(a, b);
			}

			// Token: 0x06001E8F RID: 7823 RVA: 0x0006F6D3 File Offset: 0x0006D8D3
			private static T Lerp(T a, T b, float t)
			{
				return (t < 0.5f) ? a : b;
			}

			// Token: 0x06001E90 RID: 7824 RVA: 0x0006F6E4 File Offset: 0x0006D8E4
			protected sealed override void UpdateValues()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					ref StylePropertyAnimationSystem.Values<T>.TimingData timing = ref this.running.timing[j];
					ref StylePropertyAnimationSystem.Values<T>.StyleData style = ref this.running.style[j];
					style.currentValue = StylePropertyAnimationSystem.ValuesDiscrete<T>.Lerp(style.startValue, style.endValue, timing.easedProgress);
				}
			}
		}

		// Token: 0x02000416 RID: 1046
		private class ValuesBackground : StylePropertyAnimationSystem.ValuesDiscrete<Background>
		{
			// Token: 0x06001E92 RID: 7826 RVA: 0x0006F770 File Offset: 0x0006D970
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001E93 RID: 7827 RVA: 0x0006F7E4 File Offset: 0x0006D9E4
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x02000417 RID: 1047
		private class ValuesFontDefinition : StylePropertyAnimationSystem.ValuesDiscrete<FontDefinition>
		{
			// Token: 0x06001E95 RID: 7829 RVA: 0x0006F844 File Offset: 0x0006DA44
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001E96 RID: 7830 RVA: 0x0006F8B8 File Offset: 0x0006DAB8
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x02000418 RID: 1048
		private class ValuesFont : StylePropertyAnimationSystem.ValuesDiscrete<Font>
		{
			// Token: 0x06001E98 RID: 7832 RVA: 0x0006F918 File Offset: 0x0006DB18
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001E99 RID: 7833 RVA: 0x0006F98C File Offset: 0x0006DB8C
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x02000419 RID: 1049
		private class ValuesTextShadow : StylePropertyAnimationSystem.Values<TextShadow>
		{
			// Token: 0x17000851 RID: 2129
			// (get) Token: 0x06001E9B RID: 7835 RVA: 0x0006F9EB File Offset: 0x0006DBEB
			public override Func<TextShadow, TextShadow, bool> SameFunc { get; } = new Func<TextShadow, TextShadow, bool>(StylePropertyAnimationSystem.ValuesTextShadow.IsSame);

			// Token: 0x06001E9C RID: 7836 RVA: 0x0006F9F3 File Offset: 0x0006DBF3
			private static bool IsSame(TextShadow a, TextShadow b)
			{
				return a == b;
			}

			// Token: 0x06001E9D RID: 7837 RVA: 0x0006F9FC File Offset: 0x0006DBFC
			private static TextShadow Lerp(TextShadow a, TextShadow b, float t)
			{
				return TextShadow.LerpUnclamped(a, b, t);
			}

			// Token: 0x06001E9E RID: 7838 RVA: 0x0006FA08 File Offset: 0x0006DC08
			protected sealed override void UpdateValues()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					ref StylePropertyAnimationSystem.Values<TextShadow>.TimingData timing = ref this.running.timing[j];
					ref StylePropertyAnimationSystem.Values<TextShadow>.StyleData style = ref this.running.style[j];
					style.currentValue = StylePropertyAnimationSystem.ValuesTextShadow.Lerp(style.startValue, style.endValue, timing.easedProgress);
				}
			}

			// Token: 0x06001E9F RID: 7839 RVA: 0x0006FA78 File Offset: 0x0006DC78
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001EA0 RID: 7840 RVA: 0x0006FAEC File Offset: 0x0006DCEC
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x0200041A RID: 1050
		private class ValuesScale : StylePropertyAnimationSystem.Values<Scale>
		{
			// Token: 0x17000852 RID: 2130
			// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x0006FB5D File Offset: 0x0006DD5D
			public override Func<Scale, Scale, bool> SameFunc { get; } = new Func<Scale, Scale, bool>(StylePropertyAnimationSystem.ValuesScale.IsSame);

			// Token: 0x06001EA3 RID: 7843 RVA: 0x0006FB65 File Offset: 0x0006DD65
			private static bool IsSame(Scale a, Scale b)
			{
				return a == b;
			}

			// Token: 0x06001EA4 RID: 7844 RVA: 0x0006FB70 File Offset: 0x0006DD70
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001EA5 RID: 7845 RVA: 0x0006FBE4 File Offset: 0x0006DDE4
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x06001EA6 RID: 7846 RVA: 0x0006FC3A File Offset: 0x0006DE3A
			private static Scale Lerp(Scale a, Scale b, float t)
			{
				return new Scale(Vector3.LerpUnclamped(a.value, b.value, t));
			}

			// Token: 0x06001EA7 RID: 7847 RVA: 0x0006FC58 File Offset: 0x0006DE58
			protected sealed override void UpdateValues()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					ref StylePropertyAnimationSystem.Values<Scale>.TimingData timing = ref this.running.timing[j];
					ref StylePropertyAnimationSystem.Values<Scale>.StyleData style = ref this.running.style[j];
					style.currentValue = StylePropertyAnimationSystem.ValuesScale.Lerp(style.startValue, style.endValue, timing.easedProgress);
				}
			}
		}

		// Token: 0x0200041B RID: 1051
		private class ValuesRotate : StylePropertyAnimationSystem.Values<Rotate>
		{
			// Token: 0x17000853 RID: 2131
			// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x0006FCE2 File Offset: 0x0006DEE2
			public override Func<Rotate, Rotate, bool> SameFunc { get; } = new Func<Rotate, Rotate, bool>(StylePropertyAnimationSystem.ValuesRotate.IsSame);

			// Token: 0x06001EAA RID: 7850 RVA: 0x0006FCEA File Offset: 0x0006DEEA
			private static bool IsSame(Rotate a, Rotate b)
			{
				return a == b;
			}

			// Token: 0x06001EAB RID: 7851 RVA: 0x0006FCF4 File Offset: 0x0006DEF4
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001EAC RID: 7852 RVA: 0x0006FD68 File Offset: 0x0006DF68
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x06001EAD RID: 7853 RVA: 0x0006FDC0 File Offset: 0x0006DFC0
			private static Rotate Lerp(Rotate a, Rotate b, float t)
			{
				return new Rotate(Mathf.LerpUnclamped(a.angle.ToDegrees(), b.angle.ToDegrees(), t));
			}

			// Token: 0x06001EAE RID: 7854 RVA: 0x0006FDFC File Offset: 0x0006DFFC
			protected sealed override void UpdateValues()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					ref StylePropertyAnimationSystem.Values<Rotate>.TimingData timing = ref this.running.timing[j];
					ref StylePropertyAnimationSystem.Values<Rotate>.StyleData style = ref this.running.style[j];
					style.currentValue = StylePropertyAnimationSystem.ValuesRotate.Lerp(style.startValue, style.endValue, timing.easedProgress);
				}
			}
		}

		// Token: 0x0200041C RID: 1052
		private class ValuesTranslate : StylePropertyAnimationSystem.Values<Translate>
		{
			// Token: 0x17000854 RID: 2132
			// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x0006FE86 File Offset: 0x0006E086
			public override Func<Translate, Translate, bool> SameFunc { get; } = new Func<Translate, Translate, bool>(StylePropertyAnimationSystem.ValuesTranslate.IsSame);

			// Token: 0x06001EB1 RID: 7857 RVA: 0x0006FE8E File Offset: 0x0006E08E
			private static bool IsSame(Translate a, Translate b)
			{
				return a == b;
			}

			// Token: 0x06001EB2 RID: 7858 RVA: 0x0006FE98 File Offset: 0x0006E098
			protected sealed override bool ConvertUnits(VisualElement owner, StylePropertyId prop, ref Translate a, ref Translate b)
			{
				return owner.TryConvertTranslateUnits(ref a, ref b);
			}

			// Token: 0x06001EB3 RID: 7859 RVA: 0x0006FEB4 File Offset: 0x0006E0B4
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001EB4 RID: 7860 RVA: 0x0006FF28 File Offset: 0x0006E128
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x06001EB5 RID: 7861 RVA: 0x0006FF80 File Offset: 0x0006E180
			private static Translate Lerp(Translate a, Translate b, float t)
			{
				return new Translate(StylePropertyAnimationSystem.ValuesLength.Lerp(a.x, b.x, t), StylePropertyAnimationSystem.ValuesLength.Lerp(a.y, b.y, t), Mathf.Lerp(a.z, b.z, t));
			}

			// Token: 0x06001EB6 RID: 7862 RVA: 0x0006FFD0 File Offset: 0x0006E1D0
			protected sealed override void UpdateValues()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					ref StylePropertyAnimationSystem.Values<Translate>.TimingData timing = ref this.running.timing[j];
					ref StylePropertyAnimationSystem.Values<Translate>.StyleData style = ref this.running.style[j];
					style.currentValue = StylePropertyAnimationSystem.ValuesTranslate.Lerp(style.startValue, style.endValue, timing.easedProgress);
				}
			}
		}

		// Token: 0x0200041D RID: 1053
		private class ValuesTransformOrigin : StylePropertyAnimationSystem.Values<TransformOrigin>
		{
			// Token: 0x17000855 RID: 2133
			// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x0007005A File Offset: 0x0006E25A
			public override Func<TransformOrigin, TransformOrigin, bool> SameFunc { get; } = new Func<TransformOrigin, TransformOrigin, bool>(StylePropertyAnimationSystem.ValuesTransformOrigin.IsSame);

			// Token: 0x06001EB9 RID: 7865 RVA: 0x00070062 File Offset: 0x0006E262
			private static bool IsSame(TransformOrigin a, TransformOrigin b)
			{
				return a == b;
			}

			// Token: 0x06001EBA RID: 7866 RVA: 0x0007006C File Offset: 0x0006E26C
			protected sealed override bool ConvertUnits(VisualElement owner, StylePropertyId prop, ref TransformOrigin a, ref TransformOrigin b)
			{
				return owner.TryConvertTransformOriginUnits(ref a, ref b);
			}

			// Token: 0x06001EBB RID: 7867 RVA: 0x00070088 File Offset: 0x0006E288
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001EBC RID: 7868 RVA: 0x000700FC File Offset: 0x0006E2FC
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x06001EBD RID: 7869 RVA: 0x00070154 File Offset: 0x0006E354
			private static TransformOrigin Lerp(TransformOrigin a, TransformOrigin b, float t)
			{
				return new TransformOrigin(StylePropertyAnimationSystem.ValuesLength.Lerp(a.x, b.x, t), StylePropertyAnimationSystem.ValuesLength.Lerp(a.y, b.y, t), Mathf.Lerp(a.z, b.z, t));
			}

			// Token: 0x06001EBE RID: 7870 RVA: 0x000701A4 File Offset: 0x0006E3A4
			protected sealed override void UpdateValues()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					ref StylePropertyAnimationSystem.Values<TransformOrigin>.TimingData timing = ref this.running.timing[j];
					ref StylePropertyAnimationSystem.Values<TransformOrigin>.StyleData style = ref this.running.style[j];
					style.currentValue = StylePropertyAnimationSystem.ValuesTransformOrigin.Lerp(style.startValue, style.endValue, timing.easedProgress);
				}
			}
		}

		// Token: 0x0200041E RID: 1054
		private class ValuesBackgroundPosition : StylePropertyAnimationSystem.ValuesDiscrete<BackgroundPosition>
		{
			// Token: 0x06001EC0 RID: 7872 RVA: 0x00070230 File Offset: 0x0006E430
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001EC1 RID: 7873 RVA: 0x000702A4 File Offset: 0x0006E4A4
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x0200041F RID: 1055
		private class ValuesBackgroundRepeat : StylePropertyAnimationSystem.ValuesDiscrete<BackgroundRepeat>
		{
			// Token: 0x06001EC3 RID: 7875 RVA: 0x00070304 File Offset: 0x0006E504
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001EC4 RID: 7876 RVA: 0x00070378 File Offset: 0x0006E578
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x02000420 RID: 1056
		private class ValuesBackgroundSize : StylePropertyAnimationSystem.Values<BackgroundSize>
		{
			// Token: 0x17000856 RID: 2134
			// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x000703D7 File Offset: 0x0006E5D7
			public override Func<BackgroundSize, BackgroundSize, bool> SameFunc { get; } = new Func<BackgroundSize, BackgroundSize, bool>(StylePropertyAnimationSystem.ValuesBackgroundSize.IsSame);

			// Token: 0x06001EC7 RID: 7879 RVA: 0x000703DF File Offset: 0x0006E5DF
			private static bool IsSame(BackgroundSize a, BackgroundSize b)
			{
				return a == b;
			}

			// Token: 0x06001EC8 RID: 7880 RVA: 0x000703E8 File Offset: 0x0006E5E8
			protected sealed override bool ConvertUnits(VisualElement owner, StylePropertyId prop, ref BackgroundSize a, ref BackgroundSize b)
			{
				return owner.TryConvertBackgroundSizeUnits(ref a, ref b);
			}

			// Token: 0x06001EC9 RID: 7881 RVA: 0x00070404 File Offset: 0x0006E604
			protected sealed override void UpdateComputedStyle()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					this.running.elements[j].computedStyle.ApplyPropertyAnimation(this.running.elements[j], this.running.properties[j], this.running.style[j].currentValue);
				}
			}

			// Token: 0x06001ECA RID: 7882 RVA: 0x00070478 File Offset: 0x0006E678
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x06001ECB RID: 7883 RVA: 0x000704CE File Offset: 0x0006E6CE
			private static BackgroundSize Lerp(BackgroundSize a, BackgroundSize b, float t)
			{
				return new BackgroundSize(StylePropertyAnimationSystem.ValuesLength.Lerp(a.x, b.x, t), StylePropertyAnimationSystem.ValuesLength.Lerp(a.y, b.y, t));
			}

			// Token: 0x06001ECC RID: 7884 RVA: 0x00070500 File Offset: 0x0006E700
			protected sealed override void UpdateValues()
			{
				int i = this.running.count;
				for (int j = 0; j < i; j++)
				{
					ref StylePropertyAnimationSystem.Values<BackgroundSize>.TimingData timing = ref this.running.timing[j];
					ref StylePropertyAnimationSystem.Values<BackgroundSize>.StyleData style = ref this.running.style[j];
					style.currentValue = StylePropertyAnimationSystem.ValuesBackgroundSize.Lerp(style.startValue, style.endValue, timing.easedProgress);
				}
			}
		}
	}
}
