using System;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200006D RID: 109
	public class TimelinePlayable : PlayableBehaviour
	{
		// Token: 0x0600031B RID: 795 RVA: 0x0000A50C File Offset: 0x0000870C
		public static ScriptPlayable<TimelinePlayable> Create(PlayableGraph graph, IEnumerable<TrackAsset> tracks, GameObject go, bool autoRebalance, bool createOutputs)
		{
			if (tracks == null)
			{
				throw new ArgumentNullException("Tracks list is null", "tracks");
			}
			if (go == null)
			{
				throw new ArgumentNullException("GameObject parameter is null", "go");
			}
			ScriptPlayable<TimelinePlayable> playable = ScriptPlayable<TimelinePlayable>.Create(graph, 0);
			playable.SetTraversalMode(PlayableTraversalMode.Passthrough);
			playable.GetBehaviour().Compile(graph, playable, tracks, go, autoRebalance, createOutputs);
			return playable;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000A570 File Offset: 0x00008770
		public void Compile(PlayableGraph graph, Playable timelinePlayable, IEnumerable<TrackAsset> tracks, GameObject go, bool autoRebalance, bool createOutputs)
		{
			if (tracks == null)
			{
				throw new ArgumentNullException("Tracks list is null", "tracks");
			}
			if (go == null)
			{
				throw new ArgumentNullException("GameObject parameter is null", "go");
			}
			List<TrackAsset> outputTrackList = new List<TrackAsset>(tracks);
			int maximumNumberOfIntersections = outputTrackList.Count * 2 + outputTrackList.Count;
			this.m_CurrentListOfActiveClips = new List<RuntimeElement>(maximumNumberOfIntersections);
			this.m_ActiveClips = new List<RuntimeElement>(maximumNumberOfIntersections);
			this.m_EvaluateCallbacks.Clear();
			this.m_PlayableCache.Clear();
			this.CompileTrackList(graph, timelinePlayable, outputTrackList, go, createOutputs);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000A5FC File Offset: 0x000087FC
		private void CompileTrackList(PlayableGraph graph, Playable timelinePlayable, IEnumerable<TrackAsset> tracks, GameObject go, bool createOutputs)
		{
			foreach (TrackAsset track in tracks)
			{
				if (track.IsCompilable() && !this.m_PlayableCache.ContainsKey(track))
				{
					track.SortClips();
					this.CreateTrackPlayable(graph, timelinePlayable, track, go, createOutputs);
				}
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000A668 File Offset: 0x00008868
		private void CreateTrackOutput(PlayableGraph graph, TrackAsset track, GameObject go, Playable playable, int port)
		{
			if (track.isSubTrack)
			{
				return;
			}
			foreach (PlayableBinding binding in track.outputs)
			{
				PlayableOutput playableOutput = binding.CreateOutput(graph);
				playableOutput.SetReferenceObject(binding.sourceObject);
				playableOutput.SetSourcePlayable(playable, port);
				playableOutput.SetWeight(1f);
				if (track as AnimationTrack != null)
				{
					this.EvaluateWeightsForAnimationPlayableOutput(track, (AnimationPlayableOutput)playableOutput);
				}
				if (playableOutput.IsPlayableOutputOfType<AudioPlayableOutput>())
				{
					((AudioPlayableOutput)playableOutput).SetEvaluateOnSeek(!TimelinePlayable.muteAudioScrubbing);
				}
				if (track.timelineAsset.markerTrack == track)
				{
					PlayableDirector director = go.GetComponent<PlayableDirector>();
					playableOutput.SetUserData(director);
					foreach (INotificationReceiver c in go.GetComponents<INotificationReceiver>())
					{
						playableOutput.AddNotificationReceiver(c);
					}
				}
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000A770 File Offset: 0x00008970
		private void EvaluateWeightsForAnimationPlayableOutput(TrackAsset track, AnimationPlayableOutput animOutput)
		{
			this.m_EvaluateCallbacks.Add(new AnimationOutputWeightProcessor(animOutput));
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000A783 File Offset: 0x00008983
		private void EvaluateAnimationPreviewUpdateCallback(TrackAsset track, AnimationPlayableOutput animOutput)
		{
			this.m_EvaluateCallbacks.Add(new AnimationPreviewUpdateCallback(animOutput));
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000A798 File Offset: 0x00008998
		private Playable CreateTrackPlayable(PlayableGraph graph, Playable timelinePlayable, TrackAsset track, GameObject go, bool createOutputs)
		{
			if (!track.IsCompilable())
			{
				return timelinePlayable;
			}
			Playable playable;
			if (this.m_PlayableCache.TryGetValue(track, out playable))
			{
				return playable;
			}
			if (track.name == "root")
			{
				return timelinePlayable;
			}
			TrackAsset parentActor = track.parent as TrackAsset;
			Playable parentPlayable = ((parentActor != null) ? this.CreateTrackPlayable(graph, timelinePlayable, parentActor, go, createOutputs) : timelinePlayable);
			Playable actorPlayable = track.CreatePlayableGraph(graph, go, this.m_IntervalTree, timelinePlayable);
			bool connected = false;
			if (!actorPlayable.IsValid<Playable>())
			{
				string name = track.name;
				string text = "(";
				Type type = track.GetType();
				throw new InvalidOperationException(name + text + ((type != null) ? type.ToString() : null) + ") did not produce a valid playable.");
			}
			if (parentPlayable.IsValid<Playable>() && actorPlayable.IsValid<Playable>())
			{
				int port = parentPlayable.GetInputCount<Playable>();
				parentPlayable.SetInputCount(port + 1);
				connected = graph.Connect<Playable, Playable>(actorPlayable, 0, parentPlayable, port);
				parentPlayable.SetInputWeight(port, 1f);
			}
			if (createOutputs && connected)
			{
				this.CreateTrackOutput(graph, track, go, parentPlayable, parentPlayable.GetInputCount<Playable>() - 1);
			}
			this.CacheTrack(track, actorPlayable, connected ? (parentPlayable.GetInputCount<Playable>() - 1) : (-1), parentPlayable);
			return actorPlayable;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000A8B4 File Offset: 0x00008AB4
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			this.Evaluate(playable, info);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		private void Evaluate(Playable playable, FrameData frameData)
		{
			if (this.m_IntervalTree == null)
			{
				return;
			}
			double localTime = playable.GetTime<Playable>();
			this.m_ActiveBit = ((this.m_ActiveBit == 0) ? 1 : 0);
			this.m_CurrentListOfActiveClips.Clear();
			this.m_IntervalTree.IntersectsWith(DiscreteTime.GetNearestTick(localTime), this.m_CurrentListOfActiveClips);
			foreach (RuntimeElement runtimeElement in this.m_CurrentListOfActiveClips)
			{
				runtimeElement.intervalBit = this.m_ActiveBit;
			}
			double timelineEnd = (double)new DiscreteTime(playable.GetDuration<Playable>());
			foreach (RuntimeElement c in this.m_ActiveClips)
			{
				if (c.intervalBit != this.m_ActiveBit)
				{
					c.DisableAt(localTime, timelineEnd, frameData);
				}
			}
			this.m_ActiveClips.Clear();
			for (int a = 0; a < this.m_CurrentListOfActiveClips.Count; a++)
			{
				this.m_CurrentListOfActiveClips[a].EvaluateAt(localTime, frameData);
				this.m_ActiveClips.Add(this.m_CurrentListOfActiveClips[a]);
			}
			int count = this.m_EvaluateCallbacks.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_EvaluateCallbacks[i].Evaluate();
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000AA40 File Offset: 0x00008C40
		private void CacheTrack(TrackAsset track, Playable playable, int port, Playable parent)
		{
			this.m_PlayableCache[track] = playable;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000AA4F File Offset: 0x00008C4F
		private static void ForAOTCompilationOnly()
		{
			new List<IntervalTree<RuntimeElement>.Entry>();
		}

		// Token: 0x0400016D RID: 365
		private IntervalTree<RuntimeElement> m_IntervalTree = new IntervalTree<RuntimeElement>();

		// Token: 0x0400016E RID: 366
		private List<RuntimeElement> m_ActiveClips = new List<RuntimeElement>();

		// Token: 0x0400016F RID: 367
		private List<RuntimeElement> m_CurrentListOfActiveClips;

		// Token: 0x04000170 RID: 368
		private int m_ActiveBit;

		// Token: 0x04000171 RID: 369
		private List<ITimelineEvaluateCallback> m_EvaluateCallbacks = new List<ITimelineEvaluateCallback>();

		// Token: 0x04000172 RID: 370
		private Dictionary<TrackAsset, Playable> m_PlayableCache = new Dictionary<TrackAsset, Playable>();

		// Token: 0x04000173 RID: 371
		internal static bool muteAudioScrubbing = true;
	}
}
