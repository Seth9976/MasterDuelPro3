using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	internal struct MarkerList : ISerializationCallbackReceiver
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00008CEC File Offset: 0x00006EEC
		public List<IMarker> markers
		{
			get
			{
				this.BuildCache();
				return this.m_Cache;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00008CFA File Offset: 0x00006EFA
		public MarkerList(int capacity)
		{
			this.m_Objects = new List<ScriptableObject>(capacity);
			this.m_Cache = new List<IMarker>(capacity);
			this.m_CacheDirty = true;
			this.m_HasNotifications = false;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00008D22 File Offset: 0x00006F22
		public void Add(ScriptableObject item)
		{
			if (item == null)
			{
				return;
			}
			this.m_Objects.Add(item);
			this.m_CacheDirty = true;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00008D41 File Offset: 0x00006F41
		public bool Remove(IMarker item)
		{
			if (!(item is ScriptableObject))
			{
				throw new InvalidOperationException("Supplied type must be a ScriptableObject");
			}
			return this.Remove((ScriptableObject)item, item.parent.timelineAsset, item.parent);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00008D73 File Offset: 0x00006F73
		public bool Remove(ScriptableObject item, TimelineAsset timelineAsset, PlayableAsset thingToDirty)
		{
			if (!this.m_Objects.Contains(item))
			{
				return false;
			}
			this.m_Objects.Remove(item);
			this.m_CacheDirty = true;
			TimelineUndo.PushDestroyUndo(timelineAsset, thingToDirty, item);
			return true;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00008DA2 File Offset: 0x00006FA2
		public void Clear()
		{
			this.m_Objects.Clear();
			this.m_CacheDirty = true;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00008DB6 File Offset: 0x00006FB6
		public bool Contains(ScriptableObject item)
		{
			return this.m_Objects.Contains(item);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00008DC4 File Offset: 0x00006FC4
		public IEnumerable<IMarker> GetMarkers()
		{
			return this.markers;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00008DCC File Offset: 0x00006FCC
		public int Count
		{
			get
			{
				return this.markers.Count;
			}
		}

		// Token: 0x170000BD RID: 189
		public IMarker this[int idx]
		{
			get
			{
				return this.markers[idx];
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00008DE7 File Offset: 0x00006FE7
		public List<ScriptableObject> GetRawMarkerList()
		{
			return this.m_Objects;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00008DF0 File Offset: 0x00006FF0
		public IMarker CreateMarker(Type type, double time, TrackAsset owner)
		{
			if (!typeof(ScriptableObject).IsAssignableFrom(type) || !typeof(IMarker).IsAssignableFrom(type))
			{
				throw new InvalidOperationException("The requested type needs to inherit from ScriptableObject and implement IMarker");
			}
			if (!owner.supportsNotifications && typeof(INotification).IsAssignableFrom(type))
			{
				throw new InvalidOperationException("Markers implementing the INotification interface cannot be added on tracks that do not support notifications");
			}
			ScriptableObject markerSO = ScriptableObject.CreateInstance(type);
			IMarker marker = (IMarker)markerSO;
			marker.time = time;
			TimelineCreateUtilities.SaveAssetIntoObject(markerSO, owner);
			this.Add(markerSO);
			marker.Initialize(owner);
			return marker;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00008E7A File Offset: 0x0000707A
		public bool HasNotifications()
		{
			this.BuildCache();
			return this.m_HasNotifications;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00002811 File Offset: 0x00000A11
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00008E88 File Offset: 0x00007088
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_CacheDirty = true;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00008E94 File Offset: 0x00007094
		private void BuildCache()
		{
			if (this.m_CacheDirty)
			{
				this.m_Cache = new List<IMarker>(this.m_Objects.Count);
				this.m_HasNotifications = false;
				foreach (ScriptableObject o in this.m_Objects)
				{
					if (o != null)
					{
						this.m_Cache.Add(o as IMarker);
						if (o is INotification)
						{
							this.m_HasNotifications = true;
						}
					}
				}
				this.m_CacheDirty = false;
			}
		}

		// Token: 0x04000129 RID: 297
		[SerializeField]
		[HideInInspector]
		private List<ScriptableObject> m_Objects;

		// Token: 0x0400012A RID: 298
		[HideInInspector]
		[NonSerialized]
		private List<IMarker> m_Cache;

		// Token: 0x0400012B RID: 299
		private bool m_CacheDirty;

		// Token: 0x0400012C RID: 300
		private bool m_HasNotifications;
	}
}
