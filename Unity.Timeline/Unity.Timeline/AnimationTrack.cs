using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace UnityEngine.Timeline
{
	// Token: 0x02000014 RID: 20
	[TrackClipType(typeof(AnimationPlayableAsset), false)]
	[TrackBindingType(typeof(Animator))]
	[ExcludeFromPreset]
	[Serializable]
	public class AnimationTrack : TrackAsset, ILayerable
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000029F6 File Offset: 0x00000BF6
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000029FE File Offset: 0x00000BFE
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002A07 File Offset: 0x00000C07
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002A14 File Offset: 0x00000C14
		public Quaternion rotation
		{
			get
			{
				return Quaternion.Euler(this.m_EulerAngles);
			}
			set
			{
				this.m_EulerAngles = value.eulerAngles;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002A23 File Offset: 0x00000C23
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002A2B File Offset: 0x00000C2B
		public Vector3 eulerAngles
		{
			get
			{
				return this.m_EulerAngles;
			}
			set
			{
				this.m_EulerAngles = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000021D7 File Offset: 0x000003D7
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002811 File Offset: 0x00000A11
		[Obsolete("applyOffset is deprecated. Use trackOffset instead", true)]
		public bool applyOffsets
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002A34 File Offset: 0x00000C34
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002A3C File Offset: 0x00000C3C
		public TrackOffset trackOffset
		{
			get
			{
				return this.m_TrackOffset;
			}
			set
			{
				this.m_TrackOffset = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002A45 File Offset: 0x00000C45
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002A4D File Offset: 0x00000C4D
		public MatchTargetFields matchTargetFields
		{
			get
			{
				return this.m_MatchTargetFields;
			}
			set
			{
				this.m_MatchTargetFields = value & MatchTargetFieldConstants.All;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002A5C File Offset: 0x00000C5C
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002A64 File Offset: 0x00000C64
		public AnimationClip infiniteClip
		{
			get
			{
				return this.m_InfiniteClip;
			}
			internal set
			{
				this.m_InfiniteClip = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002A6D File Offset: 0x00000C6D
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002A75 File Offset: 0x00000C75
		internal bool infiniteClipRemoveOffset
		{
			get
			{
				return this.m_InfiniteClipRemoveOffset;
			}
			set
			{
				this.m_InfiniteClipRemoveOffset = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002A7E File Offset: 0x00000C7E
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002A86 File Offset: 0x00000C86
		public AvatarMask avatarMask
		{
			get
			{
				return this.m_AvatarMask;
			}
			set
			{
				this.m_AvatarMask = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002A8F File Offset: 0x00000C8F
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002A97 File Offset: 0x00000C97
		public bool applyAvatarMask
		{
			get
			{
				return this.m_ApplyAvatarMask;
			}
			set
			{
				this.m_ApplyAvatarMask = value;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002AA0 File Offset: 0x00000CA0
		internal override bool CanCompileClips()
		{
			return !base.muted && (this.m_Clips.Count > 0 || (this.m_InfiniteClip != null && !this.m_InfiniteClip.empty));
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002ADA File Offset: 0x00000CDA
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				yield return AnimationPlayableBinding.Create(base.name, this);
				yield break;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002AEA File Offset: 0x00000CEA
		public bool inClipMode
		{
			get
			{
				return base.clips != null && base.clips.Length != 0;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002B00 File Offset: 0x00000D00
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002B08 File Offset: 0x00000D08
		public Vector3 infiniteClipOffsetPosition
		{
			get
			{
				return this.m_InfiniteClipOffsetPosition;
			}
			set
			{
				this.m_InfiniteClipOffsetPosition = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002B11 File Offset: 0x00000D11
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002B1E File Offset: 0x00000D1E
		public Quaternion infiniteClipOffsetRotation
		{
			get
			{
				return Quaternion.Euler(this.m_InfiniteClipOffsetEulerAngles);
			}
			set
			{
				this.m_InfiniteClipOffsetEulerAngles = value.eulerAngles;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002B2D File Offset: 0x00000D2D
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002B35 File Offset: 0x00000D35
		public Vector3 infiniteClipOffsetEulerAngles
		{
			get
			{
				return this.m_InfiniteClipOffsetEulerAngles;
			}
			set
			{
				this.m_InfiniteClipOffsetEulerAngles = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002B3E File Offset: 0x00000D3E
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002B46 File Offset: 0x00000D46
		internal bool infiniteClipApplyFootIK
		{
			get
			{
				return this.m_InfiniteClipApplyFootIK;
			}
			set
			{
				this.m_InfiniteClipApplyFootIK = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002B4F File Offset: 0x00000D4F
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002B57 File Offset: 0x00000D57
		internal double infiniteClipTimeOffset
		{
			get
			{
				return this.m_InfiniteClipTimeOffset;
			}
			set
			{
				this.m_InfiniteClipTimeOffset = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002B60 File Offset: 0x00000D60
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002B68 File Offset: 0x00000D68
		public TimelineClip.ClipExtrapolation infiniteClipPreExtrapolation
		{
			get
			{
				return this.m_InfiniteClipPreExtrapolation;
			}
			set
			{
				this.m_InfiniteClipPreExtrapolation = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002B71 File Offset: 0x00000D71
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002B79 File Offset: 0x00000D79
		public TimelineClip.ClipExtrapolation infiniteClipPostExtrapolation
		{
			get
			{
				return this.m_InfiniteClipPostExtrapolation;
			}
			set
			{
				this.m_InfiniteClipPostExtrapolation = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002B82 File Offset: 0x00000D82
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002B8A File Offset: 0x00000D8A
		internal AnimationPlayableAsset.LoopMode infiniteClipLoop
		{
			get
			{
				return this.mInfiniteClipLoop;
			}
			set
			{
				this.mInfiniteClipLoop = value;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002B93 File Offset: 0x00000D93
		[ContextMenu("Reset Offsets")]
		private void ResetOffsets()
		{
			this.m_Position = Vector3.zero;
			this.m_EulerAngles = Vector3.zero;
			this.UpdateClipOffsets();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public TimelineClip CreateClip(AnimationClip clip)
		{
			if (clip == null)
			{
				return null;
			}
			TimelineClip newClip = base.CreateClip<AnimationPlayableAsset>();
			this.AssignAnimationClip(newClip, clip);
			return newClip;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002BDC File Offset: 0x00000DDC
		public void CreateInfiniteClip(string infiniteClipName)
		{
			if (this.inClipMode)
			{
				Debug.LogWarning("CreateInfiniteClip cannot create an infinite clip for an AnimationTrack that contains one or more Timeline Clips.");
				return;
			}
			if (this.m_InfiniteClip != null)
			{
				return;
			}
			this.m_InfiniteClip = TimelineCreateUtilities.CreateAnimationClipForTrack(string.IsNullOrEmpty(infiniteClipName) ? "Recorded" : infiniteClipName, this, false);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002C28 File Offset: 0x00000E28
		public TimelineClip CreateRecordableClip(string animClipName)
		{
			AnimationClip clip = TimelineCreateUtilities.CreateAnimationClipForTrack(string.IsNullOrEmpty(animClipName) ? "Recorded" : animClipName, this, false);
			TimelineClip timelineClip = this.CreateClip(clip);
			timelineClip.displayName = animClipName;
			timelineClip.recordable = true;
			timelineClip.start = 0.0;
			timelineClip.duration = 1.0;
			AnimationPlayableAsset apa = timelineClip.asset as AnimationPlayableAsset;
			if (apa != null)
			{
				apa.removeStartOffset = false;
			}
			return timelineClip;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002C9C File Offset: 0x00000E9C
		protected override void OnCreateClip(TimelineClip clip)
		{
			TimelineClip.ClipExtrapolation extrapolation = TimelineClip.ClipExtrapolation.None;
			if (!base.isSubTrack)
			{
				extrapolation = TimelineClip.ClipExtrapolation.Hold;
			}
			clip.preExtrapolationMode = extrapolation;
			clip.postExtrapolationMode = extrapolation;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002CC3 File Offset: 0x00000EC3
		protected internal override int CalculateItemsHash()
		{
			return TrackAsset.GetAnimationClipHash(this.m_InfiniteClip).CombineHash(base.CalculateItemsHash());
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002811 File Offset: 0x00000A11
		internal void UpdateClipOffsets()
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002CDC File Offset: 0x00000EDC
		private Playable CompileTrackPlayable(PlayableGraph graph, AnimationTrack track, GameObject go, IntervalTree<RuntimeElement> tree, AppliedOffsetMode mode)
		{
			AnimationMixerPlayable mixer = AnimationMixerPlayable.Create(graph, track.clips.Length);
			for (int i = 0; i < track.clips.Length; i++)
			{
				TimelineClip c = track.clips[i];
				PlayableAsset asset = c.asset as PlayableAsset;
				if (!(asset == null))
				{
					AnimationPlayableAsset animationAsset = asset as AnimationPlayableAsset;
					if (animationAsset != null)
					{
						animationAsset.appliedOffsetMode = mode;
					}
					Playable source = asset.CreatePlayable(graph, go);
					if (source.IsValid<Playable>())
					{
						RuntimeClip clip = new RuntimeClip(c, source, mixer);
						tree.Add(clip);
						graph.Connect<Playable, AnimationMixerPlayable>(source, 0, mixer, i);
						mixer.SetInputWeight(i, 0f);
					}
				}
			}
			if (!track.AnimatesRootTransform())
			{
				return mixer;
			}
			return this.ApplyTrackOffset(graph, mixer, go, mode);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002DAE File Offset: 0x00000FAE
		Playable ILayerable.CreateLayerMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return Playable.Null;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002DB8 File Offset: 0x00000FB8
		internal override Playable CreateMixerPlayableGraph(PlayableGraph graph, GameObject go, IntervalTree<RuntimeElement> tree)
		{
			if (base.isSubTrack)
			{
				throw new InvalidOperationException("Nested animation tracks should never be asked to create a graph directly");
			}
			List<AnimationTrack> flattenTracks = new List<AnimationTrack>();
			if (this.CanCompileClips())
			{
				flattenTracks.Add(this);
			}
			Transform genericRoot = this.GetGenericRootNode(go);
			bool animatesRootTransformNoMask = this.AnimatesRootTransform();
			bool animatesRootTransform = animatesRootTransformNoMask && !this.IsRootTransformDisabledByMask(go, genericRoot);
			foreach (TrackAsset trackAsset in base.GetChildTracks())
			{
				AnimationTrack child = trackAsset as AnimationTrack;
				if (child != null && child.CanCompileClips())
				{
					bool childAnimatesRoot = child.AnimatesRootTransform();
					animatesRootTransformNoMask |= child.AnimatesRootTransform();
					animatesRootTransform |= childAnimatesRoot && !child.IsRootTransformDisabledByMask(go, genericRoot);
					flattenTracks.Add(child);
				}
			}
			AppliedOffsetMode mode = this.GetOffsetMode(go, animatesRootTransform);
			int defaultBlendCount = this.GetDefaultBlendCount();
			AnimationLayerMixerPlayable layerMixer = AnimationTrack.CreateGroupMixer(graph, go, flattenTracks.Count + defaultBlendCount);
			for (int c = 0; c < flattenTracks.Count; c++)
			{
				int blendIndex = c + defaultBlendCount;
				AppliedOffsetMode childMode = mode;
				if (mode != AppliedOffsetMode.NoRootTransform && flattenTracks[c].IsRootTransformDisabledByMask(go, genericRoot))
				{
					childMode = AppliedOffsetMode.NoRootTransform;
				}
				Playable compiledTrackPlayable = (flattenTracks[c].inClipMode ? this.CompileTrackPlayable(graph, flattenTracks[c], go, tree, childMode) : flattenTracks[c].CreateInfiniteTrackPlayable(graph, go, tree, childMode));
				graph.Connect<Playable, AnimationLayerMixerPlayable>(compiledTrackPlayable, 0, layerMixer, blendIndex);
				layerMixer.SetInputWeight(blendIndex, (float)(flattenTracks[c].inClipMode ? 0 : 1));
				if (flattenTracks[c].applyAvatarMask && flattenTracks[c].avatarMask != null)
				{
					layerMixer.SetLayerMaskFromAvatarMask((uint)blendIndex, flattenTracks[c].avatarMask);
				}
			}
			bool requiresMotionXPlayable = this.RequiresMotionXPlayable(mode, go);
			requiresMotionXPlayable |= defaultBlendCount > 0 && this.RequiresMotionXPlayable(this.GetOffsetMode(go, animatesRootTransformNoMask), go);
			this.AttachDefaultBlend(graph, layerMixer, requiresMotionXPlayable);
			Playable mixer = layerMixer;
			if (requiresMotionXPlayable)
			{
				AnimationMotionXToDeltaPlayable motionXToDelta = AnimationMotionXToDeltaPlayable.Create(graph);
				graph.Connect<Playable, AnimationMotionXToDeltaPlayable>(mixer, 0, motionXToDelta, 0);
				motionXToDelta.SetInputWeight(0, 1f);
				motionXToDelta.SetAbsoluteMotion(AnimationTrack.UsesAbsoluteMotion(mode));
				mixer = motionXToDelta;
			}
			return mixer;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000021D7 File Offset: 0x000003D7
		private int GetDefaultBlendCount()
		{
			return 0;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002811 File Offset: 0x00000A11
		private void AttachDefaultBlend(PlayableGraph graph, AnimationLayerMixerPlayable mixer, bool requireOffset)
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003014 File Offset: 0x00001214
		private Playable AttachOffsetPlayable(PlayableGraph graph, Playable playable, Vector3 pos, Quaternion rot)
		{
			AnimationOffsetPlayable offsetPlayable = AnimationOffsetPlayable.Create(graph, pos, rot, 1);
			offsetPlayable.SetInputWeight(0, 1f);
			graph.Connect<Playable, AnimationOffsetPlayable>(playable, 0, offsetPlayable, 0);
			return offsetPlayable;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000304C File Offset: 0x0000124C
		private bool RequiresMotionXPlayable(AppliedOffsetMode mode, GameObject gameObject)
		{
			if (mode == AppliedOffsetMode.NoRootTransform)
			{
				return false;
			}
			if (mode == AppliedOffsetMode.SceneOffsetLegacy)
			{
				Animator animator = this.GetBinding((gameObject != null) ? gameObject.GetComponent<PlayableDirector>() : null);
				return animator != null && animator.hasRootMotion;
			}
			return true;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000308E File Offset: 0x0000128E
		private static bool UsesAbsoluteMotion(AppliedOffsetMode mode)
		{
			return mode != AppliedOffsetMode.SceneOffset && mode != AppliedOffsetMode.SceneOffsetLegacy;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000030A0 File Offset: 0x000012A0
		private bool HasController(GameObject gameObject)
		{
			Animator animator = this.GetBinding((gameObject != null) ? gameObject.GetComponent<PlayableDirector>() : null);
			return animator != null && animator.runtimeAnimatorController != null;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000030E0 File Offset: 0x000012E0
		internal Animator GetBinding(PlayableDirector director)
		{
			if (director == null)
			{
				return null;
			}
			Object key = this;
			if (base.isSubTrack)
			{
				key = base.parent;
			}
			Object binding = null;
			if (director != null)
			{
				binding = director.GetGenericBinding(key);
			}
			Animator animator = null;
			if (binding != null)
			{
				animator = binding as Animator;
				GameObject gameObject = binding as GameObject;
				if (animator == null && gameObject != null)
				{
					animator = gameObject.GetComponent<Animator>();
				}
			}
			return animator;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000314F File Offset: 0x0000134F
		private static AnimationLayerMixerPlayable CreateGroupMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return AnimationLayerMixerPlayable.Create(graph, inputCount, false);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000315C File Offset: 0x0000135C
		private Playable CreateInfiniteTrackPlayable(PlayableGraph graph, GameObject go, IntervalTree<RuntimeElement> tree, AppliedOffsetMode mode)
		{
			if (this.m_InfiniteClip == null)
			{
				return Playable.Null;
			}
			AnimationMixerPlayable mixer = AnimationMixerPlayable.Create(graph, 1);
			Playable playable = AnimationPlayableAsset.CreatePlayable(graph, this.m_InfiniteClip, this.m_InfiniteClipOffsetPosition, this.m_InfiniteClipOffsetEulerAngles, false, mode, this.infiniteClipApplyFootIK, AnimationPlayableAsset.LoopMode.Off);
			if (playable.IsValid<Playable>())
			{
				tree.Add(new InfiniteRuntimeClip(playable));
				graph.Connect<Playable, AnimationMixerPlayable>(playable, 0, mixer, 0);
				mixer.SetInputWeight(0, 1f);
			}
			if (!this.AnimatesRootTransform())
			{
				return mixer;
			}
			return (base.isSubTrack ? ((AnimationTrack)base.parent) : this).ApplyTrackOffset(graph, mixer, go, mode);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003208 File Offset: 0x00001408
		private Playable ApplyTrackOffset(PlayableGraph graph, Playable root, GameObject go, AppliedOffsetMode mode)
		{
			if (mode == AppliedOffsetMode.SceneOffsetLegacy || mode == AppliedOffsetMode.SceneOffset || mode == AppliedOffsetMode.NoRootTransform)
			{
				return root;
			}
			Vector3 pos = this.position;
			Quaternion rot = this.rotation;
			AnimationOffsetPlayable offsetPlayable = AnimationOffsetPlayable.Create(graph, pos, rot, 1);
			graph.Connect<Playable, AnimationOffsetPlayable>(root, 0, offsetPlayable, 0);
			offsetPlayable.SetInputWeight(0, 1f);
			return offsetPlayable;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000325B File Offset: 0x0000145B
		internal override void GetEvaluationTime(out double outStart, out double outDuration)
		{
			if (this.inClipMode)
			{
				base.GetEvaluationTime(out outStart, out outDuration);
				return;
			}
			outStart = 0.0;
			outDuration = TimelineClip.kMaxTimeValue;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003280 File Offset: 0x00001480
		internal override void GetSequenceTime(out double outStart, out double outDuration)
		{
			if (this.inClipMode)
			{
				base.GetSequenceTime(out outStart, out outDuration);
				return;
			}
			outStart = 0.0;
			outDuration = Math.Max(base.GetNotificationDuration(), TimeUtility.GetAnimationClipLength(this.m_InfiniteClip));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000032B8 File Offset: 0x000014B8
		private void AssignAnimationClip(TimelineClip clip, AnimationClip animClip)
		{
			if (clip == null || animClip == null)
			{
				return;
			}
			if (animClip.legacy)
			{
				throw new InvalidOperationException("Legacy Animation Clips are not supported");
			}
			AnimationPlayableAsset asset = clip.asset as AnimationPlayableAsset;
			if (asset != null)
			{
				asset.clip = animClip;
				asset.name = animClip.name;
				double duration = asset.duration;
				if (!double.IsInfinity(duration) && duration >= TimelineClip.kMinDuration && duration < TimelineClip.kMaxTimeValue)
				{
					clip.duration = duration;
				}
			}
			clip.displayName = animClip.name;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002811 File Offset: 0x00000A11
		public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003340 File Offset: 0x00001540
		private void GetAnimationClips(List<AnimationClip> animClips)
		{
			TimelineClip[] clips = base.clips;
			for (int i = 0; i < clips.Length; i++)
			{
				AnimationPlayableAsset a = clips[i].asset as AnimationPlayableAsset;
				if (a != null && a.clip != null)
				{
					animClips.Add(a.clip);
				}
			}
			if (this.m_InfiniteClip != null)
			{
				animClips.Add(this.m_InfiniteClip);
			}
			foreach (TrackAsset trackAsset in base.GetChildTracks())
			{
				AnimationTrack animChildTrack = trackAsset as AnimationTrack;
				if (animChildTrack != null)
				{
					animChildTrack.GetAnimationClips(animClips);
				}
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003400 File Offset: 0x00001600
		private AppliedOffsetMode GetOffsetMode(GameObject go, bool animatesRootTransform)
		{
			if (!animatesRootTransform)
			{
				return AppliedOffsetMode.NoRootTransform;
			}
			if (this.m_TrackOffset == TrackOffset.ApplyTransformOffsets)
			{
				return AppliedOffsetMode.TransformOffset;
			}
			if (this.m_TrackOffset == TrackOffset.ApplySceneOffsets)
			{
				if (!Application.isPlaying)
				{
					return AppliedOffsetMode.SceneOffsetEditor;
				}
				return AppliedOffsetMode.SceneOffset;
			}
			else
			{
				if (!this.HasController(go))
				{
					return AppliedOffsetMode.TransformOffsetLegacy;
				}
				if (!Application.isPlaying)
				{
					return AppliedOffsetMode.SceneOffsetLegacyEditor;
				}
				return AppliedOffsetMode.SceneOffsetLegacy;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000343C File Offset: 0x0000163C
		private bool IsRootTransformDisabledByMask(GameObject gameObject, Transform genericRootNode)
		{
			if (this.avatarMask == null || !this.applyAvatarMask)
			{
				return false;
			}
			Animator animator = this.GetBinding((gameObject != null) ? gameObject.GetComponent<PlayableDirector>() : null);
			if (animator == null)
			{
				return false;
			}
			if (animator.isHuman)
			{
				return !this.avatarMask.GetHumanoidBodyPartActive(AvatarMaskBodyPart.Root);
			}
			if (this.avatarMask.transformCount == 0)
			{
				return false;
			}
			if (genericRootNode == null)
			{
				return string.IsNullOrEmpty(this.avatarMask.GetTransformPath(0)) && !this.avatarMask.GetTransformActive(0);
			}
			for (int i = 0; i < this.avatarMask.transformCount; i++)
			{
				if (genericRootNode == animator.transform.Find(this.avatarMask.GetTransformPath(i)))
				{
					return !this.avatarMask.GetTransformActive(i);
				}
			}
			return false;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003520 File Offset: 0x00001720
		private Transform GetGenericRootNode(GameObject gameObject)
		{
			Animator animator = this.GetBinding((gameObject != null) ? gameObject.GetComponent<PlayableDirector>() : null);
			if (animator == null)
			{
				return null;
			}
			if (animator.isHuman)
			{
				return null;
			}
			if (animator.avatar == null)
			{
				return null;
			}
			string rootName = animator.avatar.humanDescription.m_RootMotionBoneName;
			if (rootName == animator.name || string.IsNullOrEmpty(rootName))
			{
				return null;
			}
			return AnimationTrack.FindInHierarchyBreadthFirst(animator.transform, rootName);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000035A0 File Offset: 0x000017A0
		internal bool AnimatesRootTransform()
		{
			if (AnimationPlayableAsset.HasRootTransforms(this.m_InfiniteClip))
			{
				return true;
			}
			foreach (TimelineClip timelineClip in base.GetClips())
			{
				AnimationPlayableAsset apa = timelineClip.asset as AnimationPlayableAsset;
				if (apa != null && apa.hasRootTransforms)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003618 File Offset: 0x00001818
		private static Transform FindInHierarchyBreadthFirst(Transform t, string name)
		{
			AnimationTrack.s_CachedQueue.Clear();
			AnimationTrack.s_CachedQueue.Enqueue(t);
			while (AnimationTrack.s_CachedQueue.Count > 0)
			{
				Transform r = AnimationTrack.s_CachedQueue.Dequeue();
				if (r.name == name)
				{
					return r;
				}
				for (int i = 0; i < r.childCount; i++)
				{
					AnimationTrack.s_CachedQueue.Enqueue(r.GetChild(i));
				}
			}
			return null;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003687 File Offset: 0x00001887
		// (set) Token: 0x06000091 RID: 145 RVA: 0x0000368F File Offset: 0x0000188F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("openClipOffsetPosition has been deprecated. Use infiniteClipOffsetPosition instead. (UnityUpgradable) -> infiniteClipOffsetPosition", true)]
		public Vector3 openClipOffsetPosition
		{
			get
			{
				return this.infiniteClipOffsetPosition;
			}
			set
			{
				this.infiniteClipOffsetPosition = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003698 File Offset: 0x00001898
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000036A0 File Offset: 0x000018A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("openClipOffsetRotation has been deprecated. Use infiniteClipOffsetRotation instead. (UnityUpgradable) -> infiniteClipOffsetRotation", true)]
		public Quaternion openClipOffsetRotation
		{
			get
			{
				return this.infiniteClipOffsetRotation;
			}
			set
			{
				this.infiniteClipOffsetRotation = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000036A9 File Offset: 0x000018A9
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000036B1 File Offset: 0x000018B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("openClipOffsetEulerAngles has been deprecated. Use infiniteClipOffsetEulerAngles instead. (UnityUpgradable) -> infiniteClipOffsetEulerAngles", true)]
		public Vector3 openClipOffsetEulerAngles
		{
			get
			{
				return this.infiniteClipOffsetEulerAngles;
			}
			set
			{
				this.infiniteClipOffsetEulerAngles = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000036BA File Offset: 0x000018BA
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000036C2 File Offset: 0x000018C2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("openClipPreExtrapolation has been deprecated. Use infiniteClipPreExtrapolation instead. (UnityUpgradable) -> infiniteClipPreExtrapolation", true)]
		public TimelineClip.ClipExtrapolation openClipPreExtrapolation
		{
			get
			{
				return this.infiniteClipPreExtrapolation;
			}
			set
			{
				this.infiniteClipPreExtrapolation = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000036CB File Offset: 0x000018CB
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000036D3 File Offset: 0x000018D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("openClipPostExtrapolation has been deprecated. Use infiniteClipPostExtrapolation instead. (UnityUpgradable) -> infiniteClipPostExtrapolation", true)]
		public TimelineClip.ClipExtrapolation openClipPostExtrapolation
		{
			get
			{
				return this.infiniteClipPostExtrapolation;
			}
			set
			{
				this.infiniteClipPostExtrapolation = value;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000036DC File Offset: 0x000018DC
		internal override void OnUpgradeFromVersion(int oldVersion)
		{
			if (oldVersion < 1)
			{
				AnimationTrack.AnimationTrackUpgrade.ConvertRotationsToEuler(this);
			}
			if (oldVersion < 2)
			{
				AnimationTrack.AnimationTrackUpgrade.ConvertRootMotion(this);
			}
			if (oldVersion < 3)
			{
				AnimationTrack.AnimationTrackUpgrade.ConvertInfiniteTrack(this);
			}
		}

		// Token: 0x04000047 RID: 71
		private const string k_DefaultInfiniteClipName = "Recorded";

		// Token: 0x04000048 RID: 72
		private const string k_DefaultRecordableClipName = "Recorded";

		// Token: 0x04000049 RID: 73
		[SerializeField]
		[FormerlySerializedAs("m_OpenClipPreExtrapolation")]
		private TimelineClip.ClipExtrapolation m_InfiniteClipPreExtrapolation;

		// Token: 0x0400004A RID: 74
		[SerializeField]
		[FormerlySerializedAs("m_OpenClipPostExtrapolation")]
		private TimelineClip.ClipExtrapolation m_InfiniteClipPostExtrapolation;

		// Token: 0x0400004B RID: 75
		[SerializeField]
		[FormerlySerializedAs("m_OpenClipOffsetPosition")]
		private Vector3 m_InfiniteClipOffsetPosition = Vector3.zero;

		// Token: 0x0400004C RID: 76
		[SerializeField]
		[FormerlySerializedAs("m_OpenClipOffsetEulerAngles")]
		private Vector3 m_InfiniteClipOffsetEulerAngles = Vector3.zero;

		// Token: 0x0400004D RID: 77
		[SerializeField]
		[FormerlySerializedAs("m_OpenClipTimeOffset")]
		private double m_InfiniteClipTimeOffset;

		// Token: 0x0400004E RID: 78
		[SerializeField]
		[FormerlySerializedAs("m_OpenClipRemoveOffset")]
		private bool m_InfiniteClipRemoveOffset;

		// Token: 0x0400004F RID: 79
		[SerializeField]
		private bool m_InfiniteClipApplyFootIK = true;

		// Token: 0x04000050 RID: 80
		[SerializeField]
		[HideInInspector]
		private AnimationPlayableAsset.LoopMode mInfiniteClipLoop;

		// Token: 0x04000051 RID: 81
		[SerializeField]
		private MatchTargetFields m_MatchTargetFields = MatchTargetFieldConstants.All;

		// Token: 0x04000052 RID: 82
		[SerializeField]
		private Vector3 m_Position = Vector3.zero;

		// Token: 0x04000053 RID: 83
		[SerializeField]
		private Vector3 m_EulerAngles = Vector3.zero;

		// Token: 0x04000054 RID: 84
		[SerializeField]
		private AvatarMask m_AvatarMask;

		// Token: 0x04000055 RID: 85
		[SerializeField]
		private bool m_ApplyAvatarMask = true;

		// Token: 0x04000056 RID: 86
		[SerializeField]
		private TrackOffset m_TrackOffset;

		// Token: 0x04000057 RID: 87
		[SerializeField]
		[HideInInspector]
		private AnimationClip m_InfiniteClip;

		// Token: 0x04000058 RID: 88
		private static readonly Queue<Transform> s_CachedQueue = new Queue<Transform>(100);

		// Token: 0x04000059 RID: 89
		[SerializeField]
		[Obsolete("Use m_InfiniteClipOffsetEulerAngles Instead", false)]
		[HideInInspector]
		private Quaternion m_OpenClipOffsetRotation = Quaternion.identity;

		// Token: 0x0400005A RID: 90
		[SerializeField]
		[Obsolete("Use m_RotationEuler Instead", false)]
		[HideInInspector]
		private Quaternion m_Rotation = Quaternion.identity;

		// Token: 0x0400005B RID: 91
		[SerializeField]
		[Obsolete("Use m_RootTransformOffsetMode", false)]
		[HideInInspector]
		private bool m_ApplyOffsets;

		// Token: 0x02000015 RID: 21
		private static class AnimationTrackUpgrade
		{
			// Token: 0x0600009D RID: 157 RVA: 0x00003778 File Offset: 0x00001978
			public static void ConvertRotationsToEuler(AnimationTrack track)
			{
				track.m_EulerAngles = track.m_Rotation.eulerAngles;
				track.m_InfiniteClipOffsetEulerAngles = track.m_OpenClipOffsetRotation.eulerAngles;
			}

			// Token: 0x0600009E RID: 158 RVA: 0x0000379C File Offset: 0x0000199C
			public static void ConvertRootMotion(AnimationTrack track)
			{
				track.m_TrackOffset = TrackOffset.Auto;
				if (!track.m_ApplyOffsets)
				{
					track.m_Position = Vector3.zero;
					track.m_EulerAngles = Vector3.zero;
				}
			}

			// Token: 0x0600009F RID: 159 RVA: 0x000037C3 File Offset: 0x000019C3
			public static void ConvertInfiniteTrack(AnimationTrack track)
			{
				track.m_InfiniteClip = track.m_AnimClip;
				track.m_AnimClip = null;
			}
		}
	}
}
