using System;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Core
{
	// Token: 0x020000AF RID: 175
	public static class Extensions
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x00011310 File Offset: 0x0000F510
		public static T SetSpecialStartupMode<T>(this T t, SpecialStartupMode mode) where T : Tween
		{
			t.specialStartupMode = mode;
			return t;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001131F File Offset: 0x0000F51F
		public static TweenerCore<T1, T2, TPlugOptions> Blendable<T1, T2, TPlugOptions>(this TweenerCore<T1, T2, TPlugOptions> t) where TPlugOptions : struct, IPlugOptions
		{
			t.isBlendable = true;
			return t;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00011329 File Offset: 0x0000F529
		public static TweenerCore<T1, T2, TPlugOptions> NoFrom<T1, T2, TPlugOptions>(this TweenerCore<T1, T2, TPlugOptions> t) where TPlugOptions : struct, IPlugOptions
		{
			t.isFromAllowed = false;
			return t;
		}
	}
}
