using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace MDPro3
{
	// Token: 0x02001222 RID: 4642
	public static class PlayableDirectorExtensions
	{
		// Token: 0x0600898A RID: 35210 RVA: 0x0010AE2C File Offset: 0x0010902C
		public static UniTask WaitAsync(this PlayableDirector director, bool needPlay = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (director == null)
			{
				return UniTask.CompletedTask;
			}
			director.extrapolationMode = DirectorWrapMode.None;
			if (needPlay)
			{
				director.Play();
			}
			return UniTask.WaitUntil(() => director == null || director.state != PlayState.Playing, PlayerLoopTiming.Update, cancellationToken, false);
		}

		// Token: 0x0600898B RID: 35211 RVA: 0x0010AE88 File Offset: 0x00109088
		public static UniTask WaitToTimeAsync(this PlayableDirector director, double time, bool needPlay = true, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (director == null)
			{
				return UniTask.CompletedTask;
			}
			director.extrapolationMode = DirectorWrapMode.None;
			if (needPlay)
			{
				director.Play();
			}
			return UniTask.WaitUntil(() => director == null || director.state != PlayState.Playing || director.time > time, PlayerLoopTiming.Update, cancellationToken, false);
		}

		// Token: 0x0600898C RID: 35212 RVA: 0x0010AEEC File Offset: 0x001090EC
		public static async UniTask AutoDestroy(this PlayableDirector director, bool needPlay = true)
		{
			await director.WaitAsync(needPlay, default(CancellationToken));
			if (director != null)
			{
				global::UnityEngine.Object.Destroy(director.gameObject);
			}
		}
	}
}
