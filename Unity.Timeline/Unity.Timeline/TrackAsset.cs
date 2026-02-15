using System;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace UnityEngine.Timeline
{
	// Token: 0x02000024 RID: 36
	[IgnoreOnPlayableTrack]
	[Serializable]
	public abstract class TrackAsset : PlayableAsset, ISerializationCallbackReceiver, IPropertyPreview, ICurvesOwner
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00002811 File Offset: 0x00000A11
		protected virtual void OnBeforeTrackSerialize()
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00002811 File Offset: 0x00000A11
		protected virtual void OnAfterTrackDeserialize()
		{
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00002811 File Offset: 0x00000A11
		internal virtual void OnUpgradeFromVersion(int oldVersion)
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005408 File Offset: 0x00003608
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.m_Version = 3;
			if (this.m_Children != null)
			{
				for (int i = this.m_Children.Count - 1; i >= 0; i--)
				{
					TrackAsset asset = this.m_Children[i] as TrackAsset;
					if (asset != null && asset.parent != this)
					{
						asset.parent = this;
					}
				}
			}
			this.OnBeforeTrackSerialize();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005474 File Offset: 0x00003674
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_ClipsCache = null;
			this.Invalidate();
			if (this.m_Version < 3)
			{
				this.UpgradeToLatestVersion();
				this.OnUpgradeFromVersion(this.m_Version);
			}
			foreach (IMarker marker in this.GetMarkers())
			{
				marker.Initialize(this);
			}
			this.OnAfterTrackDeserialize();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00002811 File Offset: 0x00000A11
		private void UpgradeToLatestVersion()
		{
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000151 RID: 337 RVA: 0x000054F0 File Offset: 0x000036F0
		// (remove) Token: 0x06000152 RID: 338 RVA: 0x00005524 File Offset: 0x00003724
		internal static event Action<TimelineClip, GameObject, Playable> OnClipPlayableCreate;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000153 RID: 339 RVA: 0x00005558 File Offset: 0x00003758
		// (remove) Token: 0x06000154 RID: 340 RVA: 0x0000558C File Offset: 0x0000378C
		internal static event Action<TrackAsset, GameObject, Playable> OnTrackAnimationPlayableCreate;

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000055BF File Offset: 0x000037BF
		public double start
		{
			get
			{
				this.UpdateDuration();
				return (double)this.m_Start;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000055D3 File Offset: 0x000037D3
		public double end
		{
			get
			{
				this.UpdateDuration();
				return (double)this.m_End;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000055E7 File Offset: 0x000037E7
		public sealed override double duration
		{
			get
			{
				this.UpdateDuration();
				return (double)(this.m_End - this.m_Start);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005606 File Offset: 0x00003806
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000560E File Offset: 0x0000380E
		public bool muted
		{
			get
			{
				return this.m_Muted;
			}
			set
			{
				this.m_Muted = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005618 File Offset: 0x00003818
		public bool mutedInHierarchy
		{
			get
			{
				if (this.muted)
				{
					return true;
				}
				TrackAsset p = this;
				while (p.parent as TrackAsset != null)
				{
					p = (TrackAsset)p.parent;
					if (p as GroupTrack != null)
					{
						return p.mutedInHierarchy;
					}
				}
				return false;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005668 File Offset: 0x00003868
		public TimelineAsset timelineAsset
		{
			get
			{
				TrackAsset node = this;
				while (node != null)
				{
					if (node.parent == null)
					{
						return null;
					}
					TimelineAsset seq = node.parent as TimelineAsset;
					if (seq != null)
					{
						return seq;
					}
					node = node.parent as TrackAsset;
				}
				return null;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000056B6 File Offset: 0x000038B6
		// (set) Token: 0x0600015D RID: 349 RVA: 0x000056BE File Offset: 0x000038BE
		public PlayableAsset parent
		{
			get
			{
				return this.m_Parent;
			}
			internal set
			{
				this.m_Parent = value;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000056C7 File Offset: 0x000038C7
		public IEnumerable<TimelineClip> GetClips()
		{
			return this.clips;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000056CF File Offset: 0x000038CF
		internal TimelineClip[] clips
		{
			get
			{
				if (this.m_Clips == null)
				{
					this.m_Clips = new List<TimelineClip>();
				}
				if (this.m_ClipsCache == null)
				{
					this.m_CacheSorted = false;
					this.m_ClipsCache = this.m_Clips.ToArray();
				}
				return this.m_ClipsCache;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000570A File Offset: 0x0000390A
		public virtual bool isEmpty
		{
			get
			{
				return !this.hasClips && !this.hasCurves && this.GetMarkerCount() == 0;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005727 File Offset: 0x00003927
		public bool hasClips
		{
			get
			{
				return this.m_Clips != null && this.m_Clips.Count != 0;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005741 File Offset: 0x00003941
		public bool hasCurves
		{
			get
			{
				return this.m_Curves != null && !this.m_Curves.empty;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00005764 File Offset: 0x00003964
		public bool isSubTrack
		{
			get
			{
				TrackAsset owner = this.parent as TrackAsset;
				return owner != null && owner.GetType() == base.GetType();
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00005799 File Offset: 0x00003999
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				TrackBindingTypeAttribute attribute;
				if (!TrackAsset.s_TrackBindingTypeAttributeCache.TryGetValue(base.GetType(), out attribute))
				{
					attribute = (TrackBindingTypeAttribute)Attribute.GetCustomAttribute(base.GetType(), typeof(TrackBindingTypeAttribute));
					TrackAsset.s_TrackBindingTypeAttributeCache.Add(base.GetType(), attribute);
				}
				Type trackBindingType = ((attribute != null) ? attribute.type : null);
				yield return ScriptPlayableBinding.Create(base.name, this, trackBindingType);
				yield break;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000057A9 File Offset: 0x000039A9
		public IEnumerable<TrackAsset> GetChildTracks()
		{
			this.UpdateChildTrackCache();
			return this.m_ChildTrackCache;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000057B7 File Offset: 0x000039B7
		// (set) Token: 0x06000167 RID: 359 RVA: 0x000057BF File Offset: 0x000039BF
		internal string customPlayableTypename
		{
			get
			{
				return this.m_CustomPlayableFullTypename;
			}
			set
			{
				this.m_CustomPlayableFullTypename = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000057C8 File Offset: 0x000039C8
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000057D0 File Offset: 0x000039D0
		public AnimationClip curves
		{
			get
			{
				return this.m_Curves;
			}
			internal set
			{
				this.m_Curves = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000057D9 File Offset: 0x000039D9
		string ICurvesOwner.defaultCurvesName
		{
			get
			{
				return "Track Parameters";
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000057E0 File Offset: 0x000039E0
		Object ICurvesOwner.asset
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000057E3 File Offset: 0x000039E3
		Object ICurvesOwner.assetOwner
		{
			get
			{
				return this.timelineAsset;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000057E0 File Offset: 0x000039E0
		TrackAsset ICurvesOwner.targetTrack
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000057EB File Offset: 0x000039EB
		internal List<ScriptableObject> subTracksObjects
		{
			get
			{
				return this.m_Children;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000057F3 File Offset: 0x000039F3
		// (set) Token: 0x06000170 RID: 368 RVA: 0x000057FB File Offset: 0x000039FB
		public bool locked
		{
			get
			{
				return this.m_Locked;
			}
			set
			{
				this.m_Locked = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005804 File Offset: 0x00003A04
		public bool lockedInHierarchy
		{
			get
			{
				if (this.locked)
				{
					return true;
				}
				TrackAsset p = this;
				while (p.parent as TrackAsset != null)
				{
					p = (TrackAsset)p.parent;
					if (p as GroupTrack != null)
					{
						return p.lockedInHierarchy;
					}
				}
				return false;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00005854 File Offset: 0x00003A54
		public bool supportsNotifications
		{
			get
			{
				if (this.m_SupportsNotifications == null)
				{
					this.m_SupportsNotifications = new bool?(NotificationUtilities.TrackTypeSupportsNotifications(base.GetType()));
				}
				return this.m_SupportsNotifications.Value;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005884 File Offset: 0x00003A84
		private void __internalAwake()
		{
			if (this.m_Clips == null)
			{
				this.m_Clips = new List<TimelineClip>();
			}
			this.m_ChildTrackCache = null;
			if (this.m_Children == null)
			{
				this.m_Children = new List<ScriptableObject>();
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000058B3 File Offset: 0x00003AB3
		public void CreateCurves(string curvesClipName)
		{
			if (this.m_Curves != null)
			{
				return;
			}
			this.m_Curves = TimelineCreateUtilities.CreateAnimationClipForTrack(string.IsNullOrEmpty(curvesClipName) ? "Track Parameters" : curvesClipName, this, true);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000058E1 File Offset: 0x00003AE1
		public virtual Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return Playable.Create(graph, inputCount);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00002DAE File Offset: 0x00000FAE
		public sealed override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			return Playable.Null;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000058EC File Offset: 0x00003AEC
		public TimelineClip CreateDefaultClip()
		{
			object[] customAttributes = base.GetType().GetCustomAttributes(typeof(TrackClipTypeAttribute), true);
			Type playableAssetType = null;
			object[] array = customAttributes;
			for (int i = 0; i < array.Length; i++)
			{
				TrackClipTypeAttribute attribute = array[i] as TrackClipTypeAttribute;
				if (attribute != null && typeof(IPlayableAsset).IsAssignableFrom(attribute.inspectedType) && typeof(ScriptableObject).IsAssignableFrom(attribute.inspectedType))
				{
					playableAssetType = attribute.inspectedType;
					break;
				}
			}
			if (playableAssetType == null)
			{
				string text = "Cannot create a default clip for type ";
				Type type = base.GetType();
				Debug.LogWarning(text + ((type != null) ? type.ToString() : null));
				return null;
			}
			return this.CreateAndAddNewClipOfType(playableAssetType);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005996 File Offset: 0x00003B96
		public TimelineClip CreateClip<T>() where T : ScriptableObject, IPlayableAsset
		{
			return this.CreateClip(typeof(T));
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000059A8 File Offset: 0x00003BA8
		public bool DeleteClip(TimelineClip clip)
		{
			if (!this.m_Clips.Contains(clip))
			{
				throw new InvalidOperationException("Cannot delete clip since it is not a child of the TrackAsset.");
			}
			return this.timelineAsset != null && this.timelineAsset.DeleteClip(clip);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000059DF File Offset: 0x00003BDF
		public IMarker CreateMarker(Type type, double time)
		{
			return this.m_Markers.CreateMarker(type, time, this);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000059EF File Offset: 0x00003BEF
		public T CreateMarker<T>(double time) where T : ScriptableObject, IMarker
		{
			return (T)((object)this.CreateMarker(typeof(T), time));
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005A07 File Offset: 0x00003C07
		public bool DeleteMarker(IMarker marker)
		{
			return this.m_Markers.Remove(marker);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005A15 File Offset: 0x00003C15
		public IEnumerable<IMarker> GetMarkers()
		{
			return this.m_Markers.GetMarkers();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005A22 File Offset: 0x00003C22
		public int GetMarkerCount()
		{
			return this.m_Markers.Count;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005A2F File Offset: 0x00003C2F
		public IMarker GetMarker(int idx)
		{
			return this.m_Markers[idx];
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005A40 File Offset: 0x00003C40
		internal TimelineClip CreateClip(Type requestedType)
		{
			if (this.ValidateClipType(requestedType))
			{
				return this.CreateAndAddNewClipOfType(requestedType);
			}
			string text = "Clips of type ";
			string text2 = ((requestedType != null) ? requestedType.ToString() : null);
			string text3 = " are not permitted on tracks of type ";
			Type type = base.GetType();
			throw new InvalidOperationException(text + text2 + text3 + ((type != null) ? type.ToString() : null));
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005A94 File Offset: 0x00003C94
		internal TimelineClip CreateAndAddNewClipOfType(Type requestedType)
		{
			TimelineClip newClip = this.CreateClipOfType(requestedType);
			this.AddClip(newClip);
			return newClip;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005AB4 File Offset: 0x00003CB4
		internal TimelineClip CreateClipOfType(Type requestedType)
		{
			if (!this.ValidateClipType(requestedType))
			{
				string text = "Clips of type ";
				string text2 = ((requestedType != null) ? requestedType.ToString() : null);
				string text3 = " are not permitted on tracks of type ";
				Type type = base.GetType();
				throw new InvalidOperationException(text + text2 + text3 + ((type != null) ? type.ToString() : null));
			}
			ScriptableObject playableAsset = ScriptableObject.CreateInstance(requestedType);
			if (playableAsset == null)
			{
				throw new InvalidOperationException("Could not create an instance of the ScriptableObject type " + requestedType.Name);
			}
			playableAsset.name = requestedType.Name;
			TimelineCreateUtilities.SaveAssetIntoObject(playableAsset, this);
			return this.CreateClipFromAsset(playableAsset);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005B40 File Offset: 0x00003D40
		internal TimelineClip CreateClipFromPlayableAsset(IPlayableAsset asset)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (asset as ScriptableObject == null)
			{
				throw new ArgumentException("CreateClipFromPlayableAsset  only supports ScriptableObject-derived Types");
			}
			if (!this.ValidateClipType(asset.GetType()))
			{
				string text = "Clips of type ";
				Type type = asset.GetType();
				string text2 = ((type != null) ? type.ToString() : null);
				string text3 = " are not permitted on tracks of type ";
				Type type2 = base.GetType();
				throw new InvalidOperationException(text + text2 + text3 + ((type2 != null) ? type2.ToString() : null));
			}
			return this.CreateClipFromAsset(asset as ScriptableObject);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005BC8 File Offset: 0x00003DC8
		private TimelineClip CreateClipFromAsset(ScriptableObject playableAsset)
		{
			TimelineClip newClip = this.CreateNewClipContainerInternal();
			newClip.displayName = playableAsset.name;
			newClip.asset = playableAsset;
			IPlayableAsset iPlayableAsset = playableAsset as IPlayableAsset;
			if (iPlayableAsset != null)
			{
				double candidateDuration = iPlayableAsset.duration;
				if (!double.IsInfinity(candidateDuration) && candidateDuration > 0.0)
				{
					newClip.duration = Math.Min(Math.Max(candidateDuration, TimelineClip.kMinDuration), TimelineClip.kMaxTimeValue);
				}
			}
			try
			{
				this.OnCreateClip(newClip);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message, playableAsset);
				return null;
			}
			return newClip;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005C5C File Offset: 0x00003E5C
		internal IEnumerable<ScriptableObject> GetMarkersRaw()
		{
			return this.m_Markers.GetRawMarkerList();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005C69 File Offset: 0x00003E69
		internal void ClearMarkers()
		{
			this.m_Markers.Clear();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005C76 File Offset: 0x00003E76
		internal void AddMarker(ScriptableObject e)
		{
			this.m_Markers.Add(e);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005C84 File Offset: 0x00003E84
		internal bool DeleteMarkerRaw(ScriptableObject marker)
		{
			return this.m_Markers.Remove(marker, this.timelineAsset, this);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005C9C File Offset: 0x00003E9C
		private int GetTimeRangeHash()
		{
			double start = double.MaxValue;
			double end = double.MinValue;
			int count = this.m_Markers.Count;
			for (int i = 0; i < this.m_Markers.Count; i++)
			{
				IMarker marker = this.m_Markers[i];
				if (marker is INotification)
				{
					if (marker.time < start)
					{
						start = marker.time;
					}
					if (marker.time > end)
					{
						end = marker.time;
					}
				}
			}
			return start.GetHashCode().CombineHash(end.GetHashCode());
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005D27 File Offset: 0x00003F27
		internal void AddClip(TimelineClip newClip)
		{
			if (!this.m_Clips.Contains(newClip))
			{
				this.m_Clips.Add(newClip);
				this.m_ClipsCache = null;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005D4C File Offset: 0x00003F4C
		private Playable CreateNotificationsPlayable(PlayableGraph graph, Playable mixerPlayable, GameObject go, Playable timelinePlayable)
		{
			TrackAsset.s_BuildData.markerList.Clear();
			this.GatherNotifications(TrackAsset.s_BuildData.markerList);
			PlayableDirector director;
			ScriptPlayable<TimeNotificationBehaviour> notificationPlayable;
			if (go.TryGetComponent<PlayableDirector>(out director))
			{
				notificationPlayable = NotificationUtilities.CreateNotificationsPlayable(graph, TrackAsset.s_BuildData.markerList, director);
			}
			else
			{
				notificationPlayable = NotificationUtilities.CreateNotificationsPlayable(graph, TrackAsset.s_BuildData.markerList, this.timelineAsset);
			}
			if (notificationPlayable.IsValid<ScriptPlayable<TimeNotificationBehaviour>>())
			{
				notificationPlayable.GetBehaviour().timeSource = timelinePlayable;
				if (mixerPlayable.IsValid<Playable>())
				{
					notificationPlayable.SetInputCount(1);
					graph.Connect<Playable, ScriptPlayable<TimeNotificationBehaviour>>(mixerPlayable, 0, notificationPlayable, 0);
					notificationPlayable.SetInputWeight(mixerPlayable, 1f);
				}
			}
			return notificationPlayable;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005DF0 File Offset: 0x00003FF0
		internal Playable CreatePlayableGraph(PlayableGraph graph, GameObject go, IntervalTree<RuntimeElement> tree, Playable timelinePlayable)
		{
			this.UpdateDuration();
			Playable mixerPlayable = Playable.Null;
			if (this.CanCreateMixerRecursive())
			{
				mixerPlayable = this.CreateMixerPlayableGraph(graph, go, tree);
			}
			Playable notificationsPlayable = this.CreateNotificationsPlayable(graph, mixerPlayable, go, timelinePlayable);
			TrackAsset.s_BuildData.Clear();
			if (!notificationsPlayable.IsValid<Playable>() && !mixerPlayable.IsValid<Playable>())
			{
				Debug.LogErrorFormat("Track {0} of type {1} has no notifications and returns an invalid mixer Playable", new object[]
				{
					base.name,
					base.GetType().FullName
				});
				return Playable.Create(graph, 0);
			}
			if (!notificationsPlayable.IsValid<Playable>())
			{
				return mixerPlayable;
			}
			return notificationsPlayable;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005E7C File Offset: 0x0000407C
		internal virtual Playable CompileClips(PlayableGraph graph, GameObject go, IList<TimelineClip> timelineClips, IntervalTree<RuntimeElement> tree)
		{
			Playable blend = this.CreateTrackMixer(graph, go, timelineClips.Count);
			for (int c = 0; c < timelineClips.Count; c++)
			{
				Playable source = this.CreatePlayable(graph, go, timelineClips[c]);
				if (source.IsValid<Playable>())
				{
					source.SetDuration(timelineClips[c].duration);
					RuntimeClip clip = new RuntimeClip(timelineClips[c], source, blend);
					tree.Add(clip);
					graph.Connect<Playable, Playable>(source, 0, blend, c);
					blend.SetInputWeight(c, 0f);
				}
			}
			this.ConfigureTrackAnimation(tree, go, blend);
			return blend;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005F10 File Offset: 0x00004110
		private void GatherCompilableTracks(IList<TrackAsset> tracks)
		{
			if (!this.muted && this.CanCreateTrackMixer())
			{
				tracks.Add(this);
			}
			foreach (TrackAsset c in this.GetChildTracks())
			{
				if (c != null)
				{
					c.GatherCompilableTracks(tracks);
				}
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005F80 File Offset: 0x00004180
		private void GatherNotifications(List<IMarker> markers)
		{
			if (!this.muted && this.CanCompileNotifications())
			{
				markers.AddRange(this.GetMarkers());
			}
			foreach (TrackAsset c in this.GetChildTracks())
			{
				if (c != null)
				{
					c.GatherNotifications(markers);
				}
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005FF4 File Offset: 0x000041F4
		internal virtual Playable CreateMixerPlayableGraph(PlayableGraph graph, GameObject go, IntervalTree<RuntimeElement> tree)
		{
			if (tree == null)
			{
				throw new ArgumentException("IntervalTree argument cannot be null", "tree");
			}
			if (go == null)
			{
				throw new ArgumentException("GameObject argument cannot be null", "go");
			}
			TrackAsset.s_BuildData.Clear();
			this.GatherCompilableTracks(TrackAsset.s_BuildData.trackList);
			if (TrackAsset.s_BuildData.trackList.Count == 0)
			{
				return Playable.Null;
			}
			Playable layerMixer = Playable.Null;
			ILayerable layerable = this as ILayerable;
			if (layerable != null)
			{
				layerMixer = layerable.CreateLayerMixer(graph, go, TrackAsset.s_BuildData.trackList.Count);
			}
			if (layerMixer.IsValid<Playable>())
			{
				for (int i = 0; i < TrackAsset.s_BuildData.trackList.Count; i++)
				{
					Playable mixer = TrackAsset.s_BuildData.trackList[i].CompileClips(graph, go, TrackAsset.s_BuildData.trackList[i].clips, tree);
					if (mixer.IsValid<Playable>())
					{
						graph.Connect<Playable, Playable>(mixer, 0, layerMixer, i);
						layerMixer.SetInputWeight(i, 1f);
					}
				}
				return layerMixer;
			}
			if (TrackAsset.s_BuildData.trackList.Count == 1)
			{
				return TrackAsset.s_BuildData.trackList[0].CompileClips(graph, go, TrackAsset.s_BuildData.trackList[0].clips, tree);
			}
			for (int j = 0; j < TrackAsset.s_BuildData.trackList.Count; j++)
			{
				TrackAsset.s_BuildData.clipList.AddRange(TrackAsset.s_BuildData.trackList[j].clips);
			}
			return this.CompileClips(graph, go, TrackAsset.s_BuildData.clipList, tree);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000618E File Offset: 0x0000438E
		internal void ConfigureTrackAnimation(IntervalTree<RuntimeElement> tree, GameObject go, Playable blend)
		{
			if (!this.hasCurves)
			{
				return;
			}
			blend.SetAnimatedProperties(this.m_Curves);
			tree.Add(new InfiniteRuntimeClip(blend));
			if (TrackAsset.OnTrackAnimationPlayableCreate != null)
			{
				TrackAsset.OnTrackAnimationPlayableCreate(this, go, blend);
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000061C8 File Offset: 0x000043C8
		internal void SortClips()
		{
			TimelineClip[] clips = this.clips;
			if (!this.m_CacheSorted)
			{
				Array.Sort<TimelineClip>(this.clips, (TimelineClip clip1, TimelineClip clip2) => clip1.start.CompareTo(clip2.start));
				this.m_CacheSorted = true;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006215 File Offset: 0x00004415
		internal void ClearClipsInternal()
		{
			this.m_Clips = new List<TimelineClip>();
			this.m_ClipsCache = null;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006229 File Offset: 0x00004429
		internal void ClearSubTracksInternal()
		{
			this.m_Children = new List<ScriptableObject>();
			this.Invalidate();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000623C File Offset: 0x0000443C
		internal void OnClipMove()
		{
			this.m_CacheSorted = false;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006248 File Offset: 0x00004448
		internal TimelineClip CreateNewClipContainerInternal()
		{
			TimelineClip clipContainer = new TimelineClip(this);
			clipContainer.asset = null;
			double newClipStart = 0.0;
			for (int a = 0; a < this.m_Clips.Count - 1; a++)
			{
				double clipDuration = this.m_Clips[a].duration;
				if (double.IsInfinity(clipDuration))
				{
					clipDuration = (double)TimelineClip.kDefaultClipDurationInSeconds;
				}
				newClipStart = Math.Max(newClipStart, this.m_Clips[a].start + clipDuration);
			}
			clipContainer.mixInCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
			clipContainer.mixOutCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
			clipContainer.start = newClipStart;
			clipContainer.duration = (double)TimelineClip.kDefaultClipDurationInSeconds;
			clipContainer.displayName = "untitled";
			return clipContainer;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000631D File Offset: 0x0000451D
		internal void AddChild(TrackAsset child)
		{
			if (child == null)
			{
				return;
			}
			this.m_Children.Add(child);
			child.parent = this;
			this.Invalidate();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006344 File Offset: 0x00004544
		internal void MoveLastTrackBefore(TrackAsset asset)
		{
			if (this.m_Children == null || this.m_Children.Count < 2 || asset == null)
			{
				return;
			}
			ScriptableObject lastTrack = this.m_Children[this.m_Children.Count - 1];
			if (lastTrack == asset)
			{
				return;
			}
			for (int i = 0; i < this.m_Children.Count - 1; i++)
			{
				if (this.m_Children[i] == asset)
				{
					for (int j = this.m_Children.Count - 1; j > i; j--)
					{
						this.m_Children[j] = this.m_Children[j - 1];
					}
					this.m_Children[i] = lastTrack;
					this.Invalidate();
					return;
				}
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006406 File Offset: 0x00004606
		internal bool RemoveSubTrack(TrackAsset child)
		{
			if (this.m_Children.Remove(child))
			{
				this.Invalidate();
				child.parent = null;
				return true;
			}
			return false;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006426 File Offset: 0x00004626
		internal void RemoveClip(TimelineClip clip)
		{
			this.m_Clips.Remove(clip);
			this.m_ClipsCache = null;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000643C File Offset: 0x0000463C
		internal virtual void GetEvaluationTime(out double outStart, out double outDuration)
		{
			outStart = 0.0;
			outDuration = 1.0;
			outStart = double.PositiveInfinity;
			double outEnd = double.NegativeInfinity;
			if (this.hasCurves)
			{
				outStart = 0.0;
				outEnd = TimeUtility.GetAnimationClipLength(this.curves);
			}
			foreach (TimelineClip clip in this.clips)
			{
				outStart = Math.Min(clip.start, outStart);
				outEnd = Math.Max(clip.end, outEnd);
			}
			if (this.HasNotifications())
			{
				double notificationDuration = this.GetNotificationDuration();
				outStart = Math.Min(notificationDuration, outStart);
				outEnd = Math.Max(notificationDuration, outEnd);
			}
			if (double.IsInfinity(outStart) || double.IsInfinity(outEnd))
			{
				outStart = (outDuration = 0.0);
				return;
			}
			outDuration = outEnd - outStart;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006516 File Offset: 0x00004716
		internal virtual void GetSequenceTime(out double outStart, out double outDuration)
		{
			this.GetEvaluationTime(out outStart, out outDuration);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006520 File Offset: 0x00004720
		public virtual void GatherProperties(PlayableDirector director, IPropertyCollector driver)
		{
			GameObject gameObject = this.GetGameObjectBinding(director);
			if (gameObject != null)
			{
				driver.PushActiveGameObject(gameObject);
			}
			if (this.hasCurves)
			{
				driver.AddObjectProperties(this, this.m_Curves);
			}
			foreach (TimelineClip clip in this.clips)
			{
				if (clip.curves != null && clip.asset != null)
				{
					driver.AddObjectProperties(clip.asset, clip.curves);
				}
				IPropertyPreview modifier = clip.asset as IPropertyPreview;
				if (modifier != null)
				{
					modifier.GatherProperties(director, driver);
				}
			}
			foreach (TrackAsset subtrack in this.GetChildTracks())
			{
				if (subtrack != null)
				{
					subtrack.GatherProperties(director, driver);
				}
			}
			if (gameObject != null)
			{
				driver.PopActiveGameObject();
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000661C File Offset: 0x0000481C
		internal GameObject GetGameObjectBinding(PlayableDirector director)
		{
			if (director == null)
			{
				return null;
			}
			Object binding = director.GetGenericBinding(this);
			GameObject gameObject = binding as GameObject;
			if (gameObject != null)
			{
				return gameObject;
			}
			Component comp = binding as Component;
			if (comp != null)
			{
				return comp.gameObject;
			}
			return null;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006668 File Offset: 0x00004868
		internal bool ValidateClipType(Type clipType)
		{
			object[] attrs = base.GetType().GetCustomAttributes(typeof(TrackClipTypeAttribute), true);
			for (int c = 0; c < attrs.Length; c++)
			{
				if (((TrackClipTypeAttribute)attrs[c]).inspectedType.IsAssignableFrom(clipType))
				{
					return true;
				}
			}
			return typeof(PlayableTrack).IsAssignableFrom(base.GetType()) && typeof(IPlayableAsset).IsAssignableFrom(clipType) && typeof(ScriptableObject).IsAssignableFrom(clipType);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00002811 File Offset: 0x00000A11
		protected virtual void OnCreateClip(TimelineClip clip)
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000066EC File Offset: 0x000048EC
		private void UpdateDuration()
		{
			int itemsHash = this.CalculateItemsHash();
			if (itemsHash == this.m_ItemsHash)
			{
				return;
			}
			this.m_ItemsHash = itemsHash;
			double trackStart;
			double trackDuration;
			this.GetSequenceTime(out trackStart, out trackDuration);
			this.m_Start = (DiscreteTime)trackStart;
			this.m_End = (DiscreteTime)(trackStart + trackDuration);
			this.CalculateExtrapolationTimes();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000673B File Offset: 0x0000493B
		protected internal virtual int CalculateItemsHash()
		{
			return HashUtility.CombineHash(this.GetClipsHash(), TrackAsset.GetAnimationClipHash(this.m_Curves), this.GetTimeRangeHash());
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000675C File Offset: 0x0000495C
		protected virtual Playable CreatePlayable(PlayableGraph graph, GameObject gameObject, TimelineClip clip)
		{
			if (!graph.IsValid())
			{
				throw new ArgumentException("graph must be a valid PlayableGraph");
			}
			if (clip == null)
			{
				throw new ArgumentNullException("clip");
			}
			IPlayableAsset asset = clip.asset as IPlayableAsset;
			if (asset != null)
			{
				Playable handle = asset.CreatePlayable(graph, gameObject);
				if (handle.IsValid<Playable>())
				{
					handle.SetAnimatedProperties(clip.curves);
					handle.SetSpeed(clip.timeScale);
					if (TrackAsset.OnClipPlayableCreate != null)
					{
						TrackAsset.OnClipPlayableCreate(clip, gameObject, handle);
					}
				}
				return handle;
			}
			return Playable.Null;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000067E0 File Offset: 0x000049E0
		internal void Invalidate()
		{
			this.m_ChildTrackCache = null;
			TimelineAsset timeline = this.timelineAsset;
			if (timeline != null)
			{
				timeline.Invalidate();
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000680C File Offset: 0x00004A0C
		internal double GetNotificationDuration()
		{
			if (!this.supportsNotifications)
			{
				return 0.0;
			}
			double maxTime = 0.0;
			int count = this.m_Markers.Count;
			for (int i = 0; i < count; i++)
			{
				IMarker marker = this.m_Markers[i];
				if (marker is INotification)
				{
					maxTime = Math.Max(maxTime, marker.time);
				}
			}
			return maxTime;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006870 File Offset: 0x00004A70
		internal virtual bool CanCompileClips()
		{
			return this.hasClips || this.hasCurves;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006882 File Offset: 0x00004A82
		public virtual bool CanCreateTrackMixer()
		{
			return this.CanCompileClips();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000688C File Offset: 0x00004A8C
		internal bool IsCompilable()
		{
			if (typeof(GroupTrack).IsAssignableFrom(base.GetType()))
			{
				return false;
			}
			bool ret = !this.mutedInHierarchy && (this.CanCreateTrackMixer() || this.CanCompileNotifications());
			if (!ret)
			{
				using (IEnumerator<TrackAsset> enumerator = this.GetChildTracks().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsCompilable())
						{
							return true;
						}
					}
				}
				return ret;
			}
			return ret;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006918 File Offset: 0x00004B18
		private void UpdateChildTrackCache()
		{
			if (this.m_ChildTrackCache == null)
			{
				if (this.m_Children == null || this.m_Children.Count == 0)
				{
					this.m_ChildTrackCache = TrackAsset.s_EmptyCache;
					return;
				}
				List<TrackAsset> childTracks = new List<TrackAsset>(this.m_Children.Count);
				for (int i = 0; i < this.m_Children.Count; i++)
				{
					TrackAsset subTrack = this.m_Children[i] as TrackAsset;
					if (subTrack != null)
					{
						childTracks.Add(subTrack);
					}
				}
				this.m_ChildTrackCache = childTracks;
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000699E File Offset: 0x00004B9E
		internal virtual int Hash()
		{
			return this.clips.Length + (this.m_Markers.Count << 16);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000069B8 File Offset: 0x00004BB8
		private int GetClipsHash()
		{
			int hash = 0;
			foreach (TimelineClip clip in this.m_Clips)
			{
				hash = hash.CombineHash(clip.Hash());
			}
			return hash;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006A14 File Offset: 0x00004C14
		protected static int GetAnimationClipHash(AnimationClip clip)
		{
			int hash = 0;
			if (clip != null && !clip.empty)
			{
				hash = hash.CombineHash(clip.frameRate.GetHashCode()).CombineHash(clip.length.GetHashCode());
			}
			return hash;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006A5D File Offset: 0x00004C5D
		private bool HasNotifications()
		{
			return this.m_Markers.HasNotifications();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006A6A File Offset: 0x00004C6A
		private bool CanCompileNotifications()
		{
			return this.supportsNotifications && this.m_Markers.HasNotifications();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006A84 File Offset: 0x00004C84
		private bool CanCreateMixerRecursive()
		{
			if (this.CanCreateTrackMixer())
			{
				return true;
			}
			using (IEnumerator<TrackAsset> enumerator = this.GetChildTracks().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.CanCreateMixerRecursive())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040000AD RID: 173
		private const int k_LatestVersion = 3;

		// Token: 0x040000AE RID: 174
		[SerializeField]
		[HideInInspector]
		private int m_Version;

		// Token: 0x040000AF RID: 175
		[Obsolete("Please use m_InfiniteClip (on AnimationTrack) instead.", false)]
		[SerializeField]
		[HideInInspector]
		[FormerlySerializedAs("m_animClip")]
		internal AnimationClip m_AnimClip;

		// Token: 0x040000B0 RID: 176
		private static TrackAsset.TransientBuildData s_BuildData = TrackAsset.TransientBuildData.Create();

		// Token: 0x040000B1 RID: 177
		internal const string kDefaultCurvesName = "Track Parameters";

		// Token: 0x040000B4 RID: 180
		[SerializeField]
		[HideInInspector]
		private bool m_Locked;

		// Token: 0x040000B5 RID: 181
		[SerializeField]
		[HideInInspector]
		private bool m_Muted;

		// Token: 0x040000B6 RID: 182
		[SerializeField]
		[HideInInspector]
		private string m_CustomPlayableFullTypename = string.Empty;

		// Token: 0x040000B7 RID: 183
		[SerializeField]
		[HideInInspector]
		private AnimationClip m_Curves;

		// Token: 0x040000B8 RID: 184
		[SerializeField]
		[HideInInspector]
		private PlayableAsset m_Parent;

		// Token: 0x040000B9 RID: 185
		[SerializeField]
		[HideInInspector]
		private List<ScriptableObject> m_Children;

		// Token: 0x040000BA RID: 186
		[NonSerialized]
		private int m_ItemsHash;

		// Token: 0x040000BB RID: 187
		[NonSerialized]
		private TimelineClip[] m_ClipsCache;

		// Token: 0x040000BC RID: 188
		private DiscreteTime m_Start;

		// Token: 0x040000BD RID: 189
		private DiscreteTime m_End;

		// Token: 0x040000BE RID: 190
		private bool m_CacheSorted;

		// Token: 0x040000BF RID: 191
		private bool? m_SupportsNotifications;

		// Token: 0x040000C0 RID: 192
		private static TrackAsset[] s_EmptyCache = new TrackAsset[0];

		// Token: 0x040000C1 RID: 193
		private IEnumerable<TrackAsset> m_ChildTrackCache;

		// Token: 0x040000C2 RID: 194
		private static Dictionary<Type, TrackBindingTypeAttribute> s_TrackBindingTypeAttributeCache = new Dictionary<Type, TrackBindingTypeAttribute>();

		// Token: 0x040000C3 RID: 195
		[SerializeField]
		[HideInInspector]
		protected internal List<TimelineClip> m_Clips = new List<TimelineClip>();

		// Token: 0x040000C4 RID: 196
		[SerializeField]
		[HideInInspector]
		private MarkerList m_Markers = new MarkerList(0);

		// Token: 0x02000025 RID: 37
		internal enum Versions
		{
			// Token: 0x040000C6 RID: 198
			Initial,
			// Token: 0x040000C7 RID: 199
			RotationAsEuler,
			// Token: 0x040000C8 RID: 200
			RootMotionUpgrade,
			// Token: 0x040000C9 RID: 201
			AnimatedTrackProperties
		}

		// Token: 0x02000026 RID: 38
		private static class TrackAssetUpgrade
		{
		}

		// Token: 0x02000027 RID: 39
		private struct TransientBuildData
		{
			// Token: 0x060001B2 RID: 434 RVA: 0x00006B30 File Offset: 0x00004D30
			public static TrackAsset.TransientBuildData Create()
			{
				return new TrackAsset.TransientBuildData
				{
					trackList = new List<TrackAsset>(20),
					clipList = new List<TimelineClip>(500),
					markerList = new List<IMarker>(100)
				};
			}

			// Token: 0x060001B3 RID: 435 RVA: 0x00006B73 File Offset: 0x00004D73
			public void Clear()
			{
				this.trackList.Clear();
				this.clipList.Clear();
				this.markerList.Clear();
			}

			// Token: 0x040000CA RID: 202
			public List<TrackAsset> trackList;

			// Token: 0x040000CB RID: 203
			public List<TimelineClip> clipList;

			// Token: 0x040000CC RID: 204
			public List<IMarker> markerList;
		}
	}
}
