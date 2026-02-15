using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace UnityEngine.AddressableAssets.ResourceLocators
{
	// Token: 0x0200005A RID: 90
	public class ResourceLocationMap : IResourceLocator
	{
		// Token: 0x06000237 RID: 567 RVA: 0x000092C9 File Offset: 0x000074C9
		public ResourceLocationMap(string id, int capacity = 0)
		{
			this.LocatorId = id;
			this.locations = new Dictionary<object, IList<IResourceLocation>>((capacity == 0) ? 100 : capacity);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000238 RID: 568 RVA: 0x000092EB File Offset: 0x000074EB
		// (set) Token: 0x06000239 RID: 569 RVA: 0x000092F3 File Offset: 0x000074F3
		public string LocatorId { get; private set; }

		// Token: 0x0600023A RID: 570 RVA: 0x000092FC File Offset: 0x000074FC
		public ResourceLocationMap(string id, IList<ResourceLocationData> locations)
		{
			this.LocatorId = id;
			if (locations == null)
			{
				return;
			}
			this.locations = new Dictionary<object, IList<IResourceLocation>>(locations.Count * 2);
			Dictionary<string, ResourceLocationBase> locMap = new Dictionary<string, ResourceLocationBase>();
			Dictionary<string, ResourceLocationData> dataMap = new Dictionary<string, ResourceLocationData>();
			for (int i = 0; i < locations.Count; i++)
			{
				ResourceLocationData rlData = locations[i];
				if (rlData.Keys == null || rlData.Keys.Length < 1)
				{
					Addressables.LogErrorFormat("Address with id '{0}' does not have any valid keys, skipping...", new object[] { rlData.InternalId });
				}
				else if (locMap.ContainsKey(rlData.Keys[0]))
				{
					Addressables.LogErrorFormat("Duplicate address '{0}' with id '{1}' found, skipping...", new object[]
					{
						rlData.Keys[0],
						rlData.InternalId
					});
				}
				else
				{
					ResourceLocationBase loc = new ResourceLocationBase(rlData.Keys[0], Addressables.ResolveInternalId(rlData.InternalId), rlData.Provider, rlData.ResourceType, Array.Empty<IResourceLocation>());
					loc.Data = rlData.Data;
					locMap.Add(rlData.Keys[0], loc);
					dataMap.Add(rlData.Keys[0], rlData);
				}
			}
			foreach (KeyValuePair<string, ResourceLocationBase> kvp in locMap)
			{
				ResourceLocationData data = dataMap[kvp.Key];
				if (data.Dependencies != null)
				{
					foreach (string d in data.Dependencies)
					{
						kvp.Value.Dependencies.Add(locMap[d]);
					}
					kvp.Value.ComputeDependencyHash();
				}
			}
			foreach (KeyValuePair<string, ResourceLocationBase> kvp2 in locMap)
			{
				foreach (string j in dataMap[kvp2.Key].Keys)
				{
					this.Add(j, kvp2.Value);
				}
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00009528 File Offset: 0x00007728
		public IEnumerable<IResourceLocation> AllLocations
		{
			get
			{
				return this.locations.SelectMany((KeyValuePair<object, IList<IResourceLocation>> k) => k.Value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00009554 File Offset: 0x00007754
		public Dictionary<object, IList<IResourceLocation>> Locations
		{
			get
			{
				return this.locations;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000955C File Offset: 0x0000775C
		public IEnumerable<object> Keys
		{
			get
			{
				return this.locations.Keys;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000956C File Offset: 0x0000776C
		public bool Locate(object key, Type type, out IList<IResourceLocation> locations)
		{
			IList<IResourceLocation> locs = null;
			if (!this.locations.TryGetValue(key, out locs))
			{
				locations = null;
				return false;
			}
			if (type == null)
			{
				locations = locs;
				return true;
			}
			int validTypeCount = 0;
			foreach (IResourceLocation i in locs)
			{
				if (type.IsAssignableFrom(i.ResourceType))
				{
					validTypeCount++;
				}
			}
			if (validTypeCount == 0)
			{
				locations = null;
				return false;
			}
			if (validTypeCount == locs.Count)
			{
				locations = locs;
				return true;
			}
			locations = new List<IResourceLocation>();
			foreach (IResourceLocation j in locs)
			{
				if (type.IsAssignableFrom(j.ResourceType))
				{
					locations.Add(j);
				}
			}
			return true;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009650 File Offset: 0x00007850
		public void Add(object key, IResourceLocation location)
		{
			IList<IResourceLocation> locations;
			if (!this.locations.TryGetValue(key, out locations))
			{
				this.locations.Add(key, locations = new List<IResourceLocation>());
			}
			locations.Add(location);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009687 File Offset: 0x00007887
		public void Add(object key, IList<IResourceLocation> locations)
		{
			this.locations.Add(key, locations);
		}

		// Token: 0x04000146 RID: 326
		private Dictionary<object, IList<IResourceLocation>> locations;
	}
}
