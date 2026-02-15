using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace MDPro3
{
	// Token: 0x02001220 RID: 4640
	public static class DOTweenExtensions
	{
		// Token: 0x06008985 RID: 35205 RVA: 0x0010ACCC File Offset: 0x00108ECC
		public static UniTask WaitAsync(this Sequence sequence, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (sequence == null || !sequence.active || cancellationToken.IsCancellationRequested)
			{
				return UniTask.CompletedTask;
			}
			UniTaskCompletionSource utcs = new UniTaskCompletionSource();
			TweenCallback originalOnComplete = sequence.onComplete;
			sequence.OnComplete(delegate
			{
				try
				{
					TweenCallback originalOnComplete2 = originalOnComplete;
					if (originalOnComplete2 != null)
					{
						originalOnComplete2();
					}
				}
				finally
				{
					utcs.TrySetResult();
				}
			});
			sequence.Play<Sequence>();
			cancellationToken.RegisterWithoutCaptureExecutionContext(delegate
			{
				sequence.Kill(false);
				utcs.TrySetCanceled(cancellationToken);
			});
			sequence.OnKill(delegate
			{
				if (!utcs.Task.Status.IsCompleted())
				{
					utcs.TrySetCanceled(default(CancellationToken));
				}
			});
			return utcs.Task;
		}
	}
}
