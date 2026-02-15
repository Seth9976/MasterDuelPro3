using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins.Core
{
	// Token: 0x02000096 RID: 150
	public abstract class ABSTweenPlugin<T1, T2, TPlugOptions> : ITweenPlugin where TPlugOptions : struct, IPlugOptions
	{
		// Token: 0x06000375 RID: 885
		public abstract void Reset(TweenerCore<T1, T2, TPlugOptions> t);

		// Token: 0x06000376 RID: 886
		public abstract void SetFrom(TweenerCore<T1, T2, TPlugOptions> t, bool isRelative);

		// Token: 0x06000377 RID: 887
		public abstract void SetFrom(TweenerCore<T1, T2, TPlugOptions> t, T2 fromValue, bool setImmediately, bool isRelative);

		// Token: 0x06000378 RID: 888
		public abstract T2 ConvertToStartValue(TweenerCore<T1, T2, TPlugOptions> t, T1 value);

		// Token: 0x06000379 RID: 889
		public abstract void SetRelativeEndValue(TweenerCore<T1, T2, TPlugOptions> t);

		// Token: 0x0600037A RID: 890
		public abstract void SetChangeValue(TweenerCore<T1, T2, TPlugOptions> t);

		// Token: 0x0600037B RID: 891
		public abstract float GetSpeedBasedDuration(TPlugOptions options, float unitsXSecond, T2 changeValue);

		// Token: 0x0600037C RID: 892
		public abstract void EvaluateAndApply(TPlugOptions options, Tween t, bool isRelative, DOGetter<T1> getter, DOSetter<T1> setter, float elapsed, T2 startValue, T2 changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice);
	}
}
