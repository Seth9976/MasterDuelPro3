using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000053 RID: 83
	public class DirectorControlPlayable : PlayableBehaviour
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00009650 File Offset: 0x00007850
		public static ScriptPlayable<DirectorControlPlayable> Create(PlayableGraph graph, PlayableDirector director)
		{
			if (director == null)
			{
				return ScriptPlayable<DirectorControlPlayable>.Null;
			}
			ScriptPlayable<DirectorControlPlayable> handle = ScriptPlayable<DirectorControlPlayable>.Create(graph, 0);
			handle.GetBehaviour().director = director;
			return handle;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00009682 File Offset: 0x00007882
		public override void OnPlayableDestroy(Playable playable)
		{
			if (this.director != null && this.director.playableAsset != null)
			{
				this.director.Stop();
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000096B0 File Offset: 0x000078B0
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			if (this.director == null || !this.director.isActiveAndEnabled || this.director.playableAsset == null)
			{
				return;
			}
			this.m_SyncTime |= info.evaluationType == FrameData.EvaluationType.Evaluate || this.DetectDiscontinuity(playable, info);
			this.SyncSpeed((double)info.effectiveSpeed);
			this.SyncStart(playable.GetGraph<Playable>(), playable.GetTime<Playable>());
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000972D File Offset: 0x0000792D
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			this.m_SyncTime = true;
			if (this.director != null && this.director.playableAsset != null)
			{
				this.m_AssetDuration = this.director.playableAsset.duration;
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00009770 File Offset: 0x00007970
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.director != null && this.director.playableAsset != null)
			{
				if (info.effectivePlayState == PlayState.Playing || (info.effectivePlayState == PlayState.Paused && this.pauseAction == DirectorControlPlayable.PauseAction.PauseDirector))
				{
					this.director.Pause();
					return;
				}
				this.director.Stop();
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000097D4 File Offset: 0x000079D4
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			if (this.director == null || !this.director.isActiveAndEnabled || this.director.playableAsset == null)
			{
				return;
			}
			if (this.m_SyncTime || this.DetectOutOfSync(playable))
			{
				this.UpdateTime(playable);
				if (this.director.playableGraph.IsValid())
				{
					this.director.playableGraph.Evaluate();
					this.director.playableGraph.SynchronizeEvaluation(playable.GetGraph<Playable>());
				}
				else
				{
					this.director.Evaluate();
				}
			}
			this.m_SyncTime = false;
			this.SyncStop(playable.GetGraph<Playable>(), playable.GetTime<Playable>());
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00009890 File Offset: 0x00007A90
		private void SyncSpeed(double speed)
		{
			if (this.director.playableGraph.IsValid())
			{
				int roots = this.director.playableGraph.GetRootPlayableCount();
				for (int i = 0; i < roots; i++)
				{
					Playable rootPlayable = this.director.playableGraph.GetRootPlayable(i);
					if (rootPlayable.IsValid<Playable>())
					{
						rootPlayable.SetSpeed(speed);
					}
				}
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000098F8 File Offset: 0x00007AF8
		private void SyncStart(PlayableGraph graph, double time)
		{
			if (this.director.state == PlayState.Playing || !graph.IsPlaying() || (this.director.extrapolationMode == DirectorWrapMode.None && time > this.m_AssetDuration))
			{
				return;
			}
			if (graph.IsMatchFrameRateEnabled())
			{
				this.director.Play(graph.GetFrameRate());
				return;
			}
			this.director.Play();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000995C File Offset: 0x00007B5C
		private void SyncStop(PlayableGraph graph, double time)
		{
			if (this.director.state == PlayState.Paused || (graph.IsPlaying() && (this.director.extrapolationMode != DirectorWrapMode.None || time < this.m_AssetDuration)))
			{
				return;
			}
			if (this.director.state == PlayState.Paused)
			{
				return;
			}
			if ((this.director.extrapolationMode == DirectorWrapMode.None && time > this.m_AssetDuration) || !graph.IsPlaying())
			{
				this.director.Pause();
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000099D5 File Offset: 0x00007BD5
		private bool DetectDiscontinuity(Playable playable, FrameData info)
		{
			return Math.Abs(playable.GetTime<Playable>() - playable.GetPreviousTime<Playable>() - info.m_DeltaTime * (double)info.m_EffectiveSpeed) > DiscreteTime.tickValue;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00009A00 File Offset: 0x00007C00
		private bool DetectOutOfSync(Playable playable)
		{
			double expectedTime = playable.GetTime<Playable>();
			if (playable.GetTime<Playable>() >= this.m_AssetDuration)
			{
				switch (this.director.extrapolationMode)
				{
				case DirectorWrapMode.Hold:
					expectedTime = this.m_AssetDuration;
					break;
				case DirectorWrapMode.Loop:
					expectedTime %= this.m_AssetDuration;
					break;
				case DirectorWrapMode.None:
					expectedTime = this.m_AssetDuration;
					break;
				}
			}
			return !Mathf.Approximately((float)expectedTime, (float)this.director.time);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00009A78 File Offset: 0x00007C78
		private void UpdateTime(Playable playable)
		{
			double duration = Math.Max(0.1, this.director.playableAsset.duration);
			switch (this.director.extrapolationMode)
			{
			case DirectorWrapMode.Hold:
				this.director.time = Math.Min(duration, Math.Max(0.0, playable.GetTime<Playable>()));
				return;
			case DirectorWrapMode.Loop:
				this.director.time = Math.Max(0.0, playable.GetTime<Playable>() % duration);
				return;
			case DirectorWrapMode.None:
				this.director.time = Math.Min(duration, Math.Max(0.0, playable.GetTime<Playable>()));
				return;
			default:
				return;
			}
		}

		// Token: 0x0400013F RID: 319
		public PlayableDirector director;

		// Token: 0x04000140 RID: 320
		public DirectorControlPlayable.PauseAction pauseAction;

		// Token: 0x04000141 RID: 321
		private bool m_SyncTime;

		// Token: 0x04000142 RID: 322
		private double m_AssetDuration = double.MaxValue;

		// Token: 0x02000054 RID: 84
		public enum PauseAction
		{
			// Token: 0x04000144 RID: 324
			StopDirector,
			// Token: 0x04000145 RID: 325
			PauseDirector
		}
	}
}
