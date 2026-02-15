using System;

namespace UnityEngine.Playables
{
	// Token: 0x0200030B RID: 779
	public static class PlayableExtensions
	{
		// Token: 0x0600156B RID: 5483 RVA: 0x0002D298 File Offset: 0x0002B498
		public static bool IsValid<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsValid();
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0002D2C0 File Offset: 0x0002B4C0
		public static PlayableGraph GetGraph<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetGraph();
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0002D2E8 File Offset: 0x0002B4E8
		public static PlayState GetPlayState<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetPlayState();
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0002D310 File Offset: 0x0002B510
		public static void Play<U>(this U playable) where U : struct, IPlayable
		{
			playable.GetHandle().Play();
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0002D334 File Offset: 0x0002B534
		public static void Pause<U>(this U playable) where U : struct, IPlayable
		{
			playable.GetHandle().Pause();
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0002D358 File Offset: 0x0002B558
		public static void SetSpeed<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetSpeed(value);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0002D380 File Offset: 0x0002B580
		public static double GetSpeed<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetSpeed();
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0002D3A8 File Offset: 0x0002B5A8
		public static void SetDuration<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetDuration(value);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0002D3D0 File Offset: 0x0002B5D0
		public static double GetDuration<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetDuration();
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0002D3F8 File Offset: 0x0002B5F8
		public static void SetTime<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetTime(value);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0002D420 File Offset: 0x0002B620
		public static double GetTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetTime();
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0002D448 File Offset: 0x0002B648
		public static double GetPreviousTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetPreviousTime();
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0002D470 File Offset: 0x0002B670
		public static bool IsDone<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsDone();
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0002D498 File Offset: 0x0002B698
		public static void SetPropagateSetTime<U>(this U playable, bool value) where U : struct, IPlayable
		{
			playable.GetHandle().SetPropagateSetTime(value);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0002D4C0 File Offset: 0x0002B6C0
		public static void SetInputCount<U>(this U playable, int value) where U : struct, IPlayable
		{
			playable.GetHandle().SetInputCount(value);
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0002D4E8 File Offset: 0x0002B6E8
		public static int GetInputCount<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInputCount();
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0002D510 File Offset: 0x0002B710
		public static Playable GetInput<U>(this U playable, int inputPort) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInput(inputPort);
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0002D538 File Offset: 0x0002B738
		public static void SetInputWeight<U>(this U playable, int inputIndex, float weight) where U : struct, IPlayable
		{
			playable.GetHandle().SetInputWeight(inputIndex, weight);
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0002D560 File Offset: 0x0002B760
		public static void SetInputWeight<U, V>(this U playable, V input, float weight) where U : struct, IPlayable where V : struct, IPlayable
		{
			playable.GetHandle().SetInputWeight(input.GetHandle(), weight);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0002D594 File Offset: 0x0002B794
		public static float GetInputWeight<U>(this U playable, int inputIndex) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInputWeight(inputIndex);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0002D5BC File Offset: 0x0002B7BC
		public static void SetTraversalMode<U>(this U playable, PlayableTraversalMode mode) where U : struct, IPlayable
		{
			playable.GetHandle().SetTraversalMode(mode);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0002D5E4 File Offset: 0x0002B7E4
		internal static DirectorWrapMode GetTimeWrapMode<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetTimeWrapMode();
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0002D60C File Offset: 0x0002B80C
		internal static void SetTimeWrapMode<U>(this U playable, DirectorWrapMode value) where U : struct, IPlayable
		{
			playable.GetHandle().SetTimeWrapMode(value);
		}
	}
}
