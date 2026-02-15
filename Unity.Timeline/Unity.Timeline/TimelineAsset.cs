using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200001D RID: 29
	[ExcludeFromPreset]
	[Serializable]
	public class TimelineAsset : PlayableAsset, ISerializationCallbackReceiver, ITimelineClipAsset, IPropertyPreview
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00002811 File Offset: 0x00000A11
		private void UpgradeToLatestVersion()
		{
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000462B File Offset: 0x0000282B
		public TimelineAsset.EditorSettings editorSettings
		{
			get
			{
				return this.m_EditorSettings;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004634 File Offset: 0x00002834
		public override double duration
		{
			get
			{
				if (this.m_DurationMode != TimelineAsset.DurationMode.BasedOnClips)
				{
					return this.m_FixedDuration;
				}
				DiscreteTime discreteDuration = this.CalculateItemsDuration();
				if (discreteDuration <= 0)
				{
					return 0.0;
				}
				return (double)discreteDuration.OneTickBefore();
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000467C File Offset: 0x0000287C
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000046BA File Offset: 0x000028BA
		public double fixedDuration
		{
			get
			{
				DiscreteTime discreteDuration = (DiscreteTime)this.m_FixedDuration;
				if (discreteDuration <= 0)
				{
					return 0.0;
				}
				return (double)discreteDuration.OneTickBefore();
			}
			set
			{
				this.m_FixedDuration = Math.Max(0.0, value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000046D1 File Offset: 0x000028D1
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000046D9 File Offset: 0x000028D9
		public TimelineAsset.DurationMode durationMode
		{
			get
			{
				return this.m_DurationMode;
			}
			set
			{
				this.m_DurationMode = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000046E2 File Offset: 0x000028E2
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				foreach (TrackAsset outputTracks in this.GetOutputTracks())
				{
					foreach (PlayableBinding output in outputTracks.outputs)
					{
						yield return output;
					}
					IEnumerator<PlayableBinding> enumerator2 = null;
				}
				IEnumerator<TrackAsset> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000046F4 File Offset: 0x000028F4
		public ClipCaps clipCaps
		{
			get
			{
				ClipCaps caps = ClipCaps.All;
				foreach (TrackAsset trackAsset in this.GetRootTracks())
				{
					foreach (TimelineClip clip in trackAsset.clips)
					{
						caps &= clip.clipCaps;
					}
				}
				return caps;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004760 File Offset: 0x00002960
		public int outputTrackCount
		{
			get
			{
				this.UpdateOutputTrackCache();
				return this.m_CacheOutputTracks.Length;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004770 File Offset: 0x00002970
		public int rootTrackCount
		{
			get
			{
				this.UpdateRootTrackCache();
				return this.m_CacheRootTracks.Count;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004783 File Offset: 0x00002983
		private void OnValidate()
		{
			this.editorSettings.frameRate = TimelineAsset.GetValidFrameRate(this.editorSettings.frameRate);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000047A0 File Offset: 0x000029A0
		public TrackAsset GetRootTrack(int index)
		{
			this.UpdateRootTrackCache();
			return this.m_CacheRootTracks[index];
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000047B4 File Offset: 0x000029B4
		public IEnumerable<TrackAsset> GetRootTracks()
		{
			this.UpdateRootTrackCache();
			return this.m_CacheRootTracks;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000047C2 File Offset: 0x000029C2
		public TrackAsset GetOutputTrack(int index)
		{
			this.UpdateOutputTrackCache();
			return this.m_CacheOutputTracks[index];
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000047D2 File Offset: 0x000029D2
		public IEnumerable<TrackAsset> GetOutputTracks()
		{
			this.UpdateOutputTrackCache();
			return this.m_CacheOutputTracks;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000047E0 File Offset: 0x000029E0
		private static double GetValidFrameRate(double frameRate)
		{
			return Math.Min(Math.Max(frameRate, TimelineAsset.EditorSettings.kMinFrameRate), TimelineAsset.EditorSettings.kMaxFrameRate);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000047F8 File Offset: 0x000029F8
		private void UpdateRootTrackCache()
		{
			if (this.m_CacheRootTracks == null)
			{
				if (this.m_Tracks == null)
				{
					this.m_CacheRootTracks = new List<TrackAsset>();
					return;
				}
				this.m_CacheRootTracks = new List<TrackAsset>(this.m_Tracks.Count);
				if (this.markerTrack != null)
				{
					this.m_CacheRootTracks.Add(this.markerTrack);
				}
				foreach (ScriptableObject scriptableObject in this.m_Tracks)
				{
					TrackAsset trackAsset = scriptableObject as TrackAsset;
					if (trackAsset != null)
					{
						this.m_CacheRootTracks.Add(trackAsset);
					}
				}
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000048B4 File Offset: 0x00002AB4
		private void UpdateOutputTrackCache()
		{
			if (this.m_CacheOutputTracks == null)
			{
				List<TrackAsset> outputTracks = new List<TrackAsset>();
				foreach (TrackAsset flattenedTrack in this.flattenedTracks)
				{
					if (flattenedTrack != null && flattenedTrack.GetType() != typeof(GroupTrack) && !flattenedTrack.isSubTrack)
					{
						outputTracks.Add(flattenedTrack);
					}
				}
				this.m_CacheOutputTracks = outputTracks.ToArray();
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00004924 File Offset: 0x00002B24
		internal TrackAsset[] flattenedTracks
		{
			get
			{
				if (this.m_CacheFlattenedTracks == null)
				{
					List<TrackAsset> list = new List<TrackAsset>(this.m_Tracks.Count * 2);
					this.UpdateRootTrackCache();
					list.AddRange(this.m_CacheRootTracks);
					for (int i = 0; i < this.m_CacheRootTracks.Count; i++)
					{
						TimelineAsset.AddSubTracksRecursive(this.m_CacheRootTracks[i], ref list);
					}
					this.m_CacheFlattenedTracks = list.ToArray();
				}
				return this.m_CacheFlattenedTracks;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004999 File Offset: 0x00002B99
		public MarkerTrack markerTrack
		{
			get
			{
				return this.m_MarkerTrack;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000049A1 File Offset: 0x00002BA1
		internal List<ScriptableObject> trackObjects
		{
			get
			{
				return this.m_Tracks;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000049A9 File Offset: 0x00002BA9
		internal void AddTrackInternal(TrackAsset track)
		{
			this.m_Tracks.Add(track);
			track.parent = this;
			this.Invalidate();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000049C4 File Offset: 0x00002BC4
		internal void RemoveTrack(TrackAsset track)
		{
			this.m_Tracks.Remove(track);
			this.Invalidate();
			TrackAsset parentTrack = track.parent as TrackAsset;
			if (parentTrack != null)
			{
				parentTrack.RemoveSubTrack(track);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004A04 File Offset: 0x00002C04
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			bool autoRebalanceTree = false;
			bool createOutputs = graph.GetPlayableCount() == 0;
			ScriptPlayable<TimelinePlayable> timeline = TimelinePlayable.Create(graph, this.GetOutputTracks(), go, autoRebalanceTree, createOutputs);
			timeline.SetDuration(this.duration);
			timeline.SetPropagateSetTime(true);
			if (!timeline.IsValid<ScriptPlayable<TimelinePlayable>>())
			{
				return Playable.Null;
			}
			return timeline;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004A55 File Offset: 0x00002C55
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.m_Version = 0;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004A5E File Offset: 0x00002C5E
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.Invalidate();
			if (this.m_Version < 0)
			{
				this.UpgradeToLatestVersion();
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004A78 File Offset: 0x00002C78
		private void __internalAwake()
		{
			if (this.m_Tracks == null)
			{
				this.m_Tracks = new List<ScriptableObject>();
			}
			for (int i = this.m_Tracks.Count - 1; i >= 0; i--)
			{
				TrackAsset asset = this.m_Tracks[i] as TrackAsset;
				if (asset != null)
				{
					asset.parent = this;
				}
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public void GatherProperties(PlayableDirector director, IPropertyCollector driver)
		{
			foreach (TrackAsset track in this.GetOutputTracks())
			{
				if (!track.mutedInHierarchy)
				{
					track.GatherProperties(director, driver);
				}
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004B2C File Offset: 0x00002D2C
		public void CreateMarkerTrack()
		{
			if (this.m_MarkerTrack == null)
			{
				this.m_MarkerTrack = ScriptableObject.CreateInstance<MarkerTrack>();
				TimelineCreateUtilities.SaveAssetIntoObject(this.m_MarkerTrack, this);
				this.m_MarkerTrack.parent = this;
				this.m_MarkerTrack.name = "Markers";
				this.Invalidate();
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004B80 File Offset: 0x00002D80
		internal void RemoveMarkerTrack()
		{
			if (this.m_MarkerTrack != null)
			{
				Object markerTrack = this.m_MarkerTrack;
				this.m_MarkerTrack = null;
				TimelineCreateUtilities.RemoveAssetFromObject(markerTrack, this);
				this.Invalidate();
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004BA9 File Offset: 0x00002DA9
		internal void Invalidate()
		{
			this.m_CacheRootTracks = null;
			this.m_CacheOutputTracks = null;
			this.m_CacheFlattenedTracks = null;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004BC0 File Offset: 0x00002DC0
		internal void UpdateFixedDurationWithItemsDuration()
		{
			this.m_FixedDuration = (double)this.CalculateItemsDuration();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004BD4 File Offset: 0x00002DD4
		private DiscreteTime CalculateItemsDuration()
		{
			DiscreteTime discreteDuration = new DiscreteTime(0);
			foreach (TrackAsset track in this.flattenedTracks)
			{
				if (!track.muted)
				{
					discreteDuration = DiscreteTime.Max(discreteDuration, (DiscreteTime)track.end);
				}
			}
			if (discreteDuration <= 0)
			{
				return new DiscreteTime(0);
			}
			return discreteDuration;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004C34 File Offset: 0x00002E34
		private static void AddSubTracksRecursive(TrackAsset track, ref List<TrackAsset> allTracks)
		{
			if (track == null)
			{
				return;
			}
			allTracks.AddRange(track.GetChildTracks());
			foreach (TrackAsset trackAsset in track.GetChildTracks())
			{
				TimelineAsset.AddSubTracksRecursive(trackAsset, ref allTracks);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004C98 File Offset: 0x00002E98
		public TrackAsset CreateTrack(Type type, TrackAsset parent, string name)
		{
			if (parent != null && parent.timelineAsset != this)
			{
				throw new InvalidOperationException("Addtrack cannot parent to a track not in the Timeline");
			}
			if (!typeof(TrackAsset).IsAssignableFrom(type))
			{
				throw new InvalidOperationException("Supplied type must be a track asset");
			}
			if (parent != null && !TimelineCreateUtilities.ValidateParentTrack(parent, type))
			{
				throw new InvalidOperationException("Cannot assign a child of type " + type.Name + " to a parent of type " + parent.GetType().Name);
			}
			string baseName = name;
			if (string.IsNullOrEmpty(baseName))
			{
				baseName = type.Name;
			}
			string trackName;
			if (parent != null)
			{
				trackName = TimelineCreateUtilities.GenerateUniqueActorName(parent.subTracksObjects, baseName);
			}
			else
			{
				trackName = TimelineCreateUtilities.GenerateUniqueActorName(this.trackObjects, baseName);
			}
			return this.AllocateTrack(parent, trackName, type);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004D5D File Offset: 0x00002F5D
		public T CreateTrack<T>(TrackAsset parent, string trackName) where T : TrackAsset, new()
		{
			return (T)((object)this.CreateTrack(typeof(T), parent, trackName));
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004D76 File Offset: 0x00002F76
		public T CreateTrack<T>(string trackName) where T : TrackAsset, new()
		{
			return (T)((object)this.CreateTrack(typeof(T), null, trackName));
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004D8F File Offset: 0x00002F8F
		public T CreateTrack<T>() where T : TrackAsset, new()
		{
			return (T)((object)this.CreateTrack(typeof(T), null, null));
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public bool DeleteClip(TimelineClip clip)
		{
			if (clip == null || clip.GetParentTrack() == null)
			{
				return false;
			}
			if (this != clip.GetParentTrack().timelineAsset)
			{
				Debug.LogError("Cannot delete a clip from this timeline");
				return false;
			}
			if (clip.curves != null)
			{
				TimelineUndo.PushDestroyUndo(this, clip.GetParentTrack(), clip.curves);
			}
			if (clip.asset != null)
			{
				this.DeleteRecordedAnimation(clip);
				TimelineUndo.PushDestroyUndo(this, clip.GetParentTrack(), clip.asset);
			}
			TrackAsset parentTrack = clip.GetParentTrack();
			parentTrack.RemoveClip(clip);
			parentTrack.CalculateExtrapolationTimes();
			return true;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004E44 File Offset: 0x00003044
		public bool DeleteTrack(TrackAsset track)
		{
			if (track.timelineAsset != this)
			{
				return false;
			}
			track.parent as TrackAsset != null;
			foreach (TrackAsset child in track.GetChildTracks())
			{
				this.DeleteTrack(child);
			}
			this.DeleteRecordedAnimation(track);
			foreach (TimelineClip clip in new List<TimelineClip>(track.clips))
			{
				this.DeleteClip(clip);
			}
			this.RemoveTrack(track);
			TimelineUndo.PushDestroyUndo(this, this, track);
			return true;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004F14 File Offset: 0x00003114
		internal void MoveLastTrackBefore(TrackAsset asset)
		{
			if (this.m_Tracks == null || this.m_Tracks.Count < 2 || asset == null)
			{
				return;
			}
			ScriptableObject lastTrack = this.m_Tracks[this.m_Tracks.Count - 1];
			if (lastTrack == asset)
			{
				return;
			}
			for (int i = 0; i < this.m_Tracks.Count - 1; i++)
			{
				if (this.m_Tracks[i] == asset)
				{
					for (int j = this.m_Tracks.Count - 1; j > i; j--)
					{
						this.m_Tracks[j] = this.m_Tracks[j - 1];
					}
					this.m_Tracks[i] = lastTrack;
					this.Invalidate();
					return;
				}
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004FD8 File Offset: 0x000031D8
		private TrackAsset AllocateTrack(TrackAsset trackAssetParent, string trackName, Type trackType)
		{
			if (trackAssetParent != null && trackAssetParent.timelineAsset != this)
			{
				throw new InvalidOperationException("Addtrack cannot parent to a track not in the Timeline");
			}
			if (!typeof(TrackAsset).IsAssignableFrom(trackType))
			{
				throw new InvalidOperationException("Supplied type must be a track asset");
			}
			TrackAsset asset = (TrackAsset)ScriptableObject.CreateInstance(trackType);
			asset.name = trackName;
			PlayableAsset parent = ((trackAssetParent != null) ? trackAssetParent : this);
			TimelineCreateUtilities.SaveAssetIntoObject(asset, parent);
			if (trackAssetParent != null)
			{
				trackAssetParent.AddChild(asset);
			}
			else
			{
				this.AddTrackInternal(asset);
			}
			return asset;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005068 File Offset: 0x00003268
		private void DeleteRecordedAnimation(TrackAsset track)
		{
			AnimationTrack animTrack = track as AnimationTrack;
			if (animTrack != null && animTrack.infiniteClip != null)
			{
				TimelineUndo.PushDestroyUndo(this, track, animTrack.infiniteClip);
			}
			if (track.curves != null)
			{
				TimelineUndo.PushDestroyUndo(this, track, track.curves);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000050BC File Offset: 0x000032BC
		private void DeleteRecordedAnimation(TimelineClip clip)
		{
			if (clip == null)
			{
				return;
			}
			if (clip.curves != null)
			{
				TimelineUndo.PushDestroyUndo(this, clip.GetParentTrack(), clip.curves);
			}
			if (!clip.recordable)
			{
				return;
			}
			AnimationPlayableAsset asset = clip.asset as AnimationPlayableAsset;
			if (asset == null || asset.clip == null)
			{
				return;
			}
			TimelineUndo.PushDestroyUndo(this, asset, asset.clip);
		}

		// Token: 0x0400008B RID: 139
		private const int k_LatestVersion = 0;

		// Token: 0x0400008C RID: 140
		[SerializeField]
		[HideInInspector]
		private int m_Version;

		// Token: 0x0400008D RID: 141
		[HideInInspector]
		[SerializeField]
		private List<ScriptableObject> m_Tracks;

		// Token: 0x0400008E RID: 142
		[HideInInspector]
		[SerializeField]
		private double m_FixedDuration;

		// Token: 0x0400008F RID: 143
		[HideInInspector]
		[NonSerialized]
		private TrackAsset[] m_CacheOutputTracks;

		// Token: 0x04000090 RID: 144
		[HideInInspector]
		[NonSerialized]
		private List<TrackAsset> m_CacheRootTracks;

		// Token: 0x04000091 RID: 145
		[HideInInspector]
		[NonSerialized]
		private TrackAsset[] m_CacheFlattenedTracks;

		// Token: 0x04000092 RID: 146
		[HideInInspector]
		[SerializeField]
		private TimelineAsset.EditorSettings m_EditorSettings = new TimelineAsset.EditorSettings();

		// Token: 0x04000093 RID: 147
		[SerializeField]
		private TimelineAsset.DurationMode m_DurationMode;

		// Token: 0x04000094 RID: 148
		[HideInInspector]
		[SerializeField]
		private MarkerTrack m_MarkerTrack;

		// Token: 0x0200001E RID: 30
		private enum Versions
		{
			// Token: 0x04000096 RID: 150
			Initial
		}

		// Token: 0x0200001F RID: 31
		private static class TimelineAssetUpgrade
		{
		}

		// Token: 0x02000020 RID: 32
		[Obsolete("MediaType has been deprecated. It is no longer required, and will be removed in a future release.", false)]
		public enum MediaType
		{
			// Token: 0x04000098 RID: 152
			Animation,
			// Token: 0x04000099 RID: 153
			Audio,
			// Token: 0x0400009A RID: 154
			Texture,
			// Token: 0x0400009B RID: 155
			[Obsolete("Use Texture MediaType instead. (UnityUpgradable) -> UnityEngine.Timeline.TimelineAsset/MediaType.Texture", false)]
			Video = 2,
			// Token: 0x0400009C RID: 156
			Script,
			// Token: 0x0400009D RID: 157
			Hybrid,
			// Token: 0x0400009E RID: 158
			Group
		}

		// Token: 0x02000021 RID: 33
		public enum DurationMode
		{
			// Token: 0x040000A0 RID: 160
			BasedOnClips,
			// Token: 0x040000A1 RID: 161
			FixedLength
		}

		// Token: 0x02000022 RID: 34
		[Serializable]
		public class EditorSettings
		{
			// Token: 0x1700006B RID: 107
			// (get) Token: 0x06000138 RID: 312 RVA: 0x0000513A File Offset: 0x0000333A
			// (set) Token: 0x06000139 RID: 313 RVA: 0x00005143 File Offset: 0x00003343
			[Obsolete("EditorSettings.fps has been deprecated. Use editorSettings.frameRate instead.", false)]
			public float fps
			{
				get
				{
					return (float)this.m_Framerate;
				}
				set
				{
					this.m_Framerate = (double)Mathf.Clamp(value, (float)TimelineAsset.EditorSettings.kMinFrameRate, (float)TimelineAsset.EditorSettings.kMaxFrameRate);
				}
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x0600013A RID: 314 RVA: 0x0000515E File Offset: 0x0000335E
			// (set) Token: 0x0600013B RID: 315 RVA: 0x00005166 File Offset: 0x00003366
			public double frameRate
			{
				get
				{
					return this.m_Framerate;
				}
				set
				{
					this.m_Framerate = TimelineAsset.GetValidFrameRate(value);
				}
			}

			// Token: 0x0600013C RID: 316 RVA: 0x00005174 File Offset: 0x00003374
			public void SetStandardFrameRate(StandardFrameRates enumValue)
			{
				FrameRate rate = TimeUtility.ToFrameRate(enumValue);
				if (rate.IsValid())
				{
					throw new ArgumentException(string.Format("StandardFrameRates {0}, is not defined", enumValue.ToString()));
				}
				this.m_Framerate = rate.rate;
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x0600013D RID: 317 RVA: 0x000051BB File Offset: 0x000033BB
			// (set) Token: 0x0600013E RID: 318 RVA: 0x000051C3 File Offset: 0x000033C3
			public bool scenePreview
			{
				get
				{
					return this.m_ScenePreview;
				}
				set
				{
					this.m_ScenePreview = value;
				}
			}

			// Token: 0x040000A2 RID: 162
			internal static readonly double kMinFrameRate = TimeUtility.kFrameRateEpsilon;

			// Token: 0x040000A3 RID: 163
			internal static readonly double kMaxFrameRate = 1000.0;

			// Token: 0x040000A4 RID: 164
			internal static readonly double kDefaultFrameRate = 60.0;

			// Token: 0x040000A5 RID: 165
			[HideInInspector]
			[SerializeField]
			[FrameRateField]
			private double m_Framerate = TimelineAsset.EditorSettings.kDefaultFrameRate;

			// Token: 0x040000A6 RID: 166
			[HideInInspector]
			[SerializeField]
			private bool m_ScenePreview = true;
		}
	}
}
