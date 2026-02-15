using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000033 RID: 51
	internal static class TimelineClipCapsExtensions
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x000072F3 File Offset: 0x000054F3
		public static bool SupportsLooping(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.Looping) > ClipCaps.None;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007305 File Offset: 0x00005505
		public static bool SupportsExtrapolation(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.Extrapolation) > ClipCaps.None;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00007317 File Offset: 0x00005517
		public static bool SupportsClipIn(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.ClipIn) > ClipCaps.None;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007329 File Offset: 0x00005529
		public static bool SupportsSpeedMultiplier(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.SpeedMultiplier) > ClipCaps.None;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000733B File Offset: 0x0000553B
		public static bool SupportsBlending(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.Blending) > ClipCaps.None;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000734E File Offset: 0x0000554E
		public static bool HasAll(this ClipCaps caps, ClipCaps flags)
		{
			return (caps & flags) == flags;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007356 File Offset: 0x00005556
		public static bool HasAny(this ClipCaps caps, ClipCaps flags)
		{
			return (caps & flags) > ClipCaps.None;
		}
	}
}
