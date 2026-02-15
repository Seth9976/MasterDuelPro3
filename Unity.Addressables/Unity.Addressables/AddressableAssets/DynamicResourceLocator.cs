using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.U2D;

namespace UnityEngine.AddressableAssets
{
	// Token: 0x0200003F RID: 63
	internal class DynamicResourceLocator : IResourceLocator
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006EA1 File Offset: 0x000050A1
		public string LocatorId
		{
			get
			{
				return "DynamicResourceLocator";
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00006EA8 File Offset: 0x000050A8
		public virtual IEnumerable<object> Keys
		{
			get
			{
				return new object[0];
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00006EB0 File Offset: 0x000050B0
		private string AtlasSpriteProviderId
		{
			get
			{
				if (!string.IsNullOrEmpty(this.m_AtlasSpriteProviderId))
				{
					return this.m_AtlasSpriteProviderId;
				}
				foreach (IResourceProvider provider in this.m_Addressables.ResourceManager.ResourceProviders)
				{
					if (provider is AtlasSpriteProvider)
					{
						this.m_AtlasSpriteProviderId = provider.ProviderId;
						return this.m_AtlasSpriteProviderId;
					}
				}
				return typeof(AtlasSpriteProvider).FullName;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00006F44 File Offset: 0x00005144
		public IEnumerable<IResourceLocation> AllLocations
		{
			get
			{
				return new IResourceLocation[0];
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006F4C File Offset: 0x0000514C
		public DynamicResourceLocator(AddressablesImpl addr)
		{
			this.m_Addressables = addr;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006F5C File Offset: 0x0000515C
		public bool Locate(object key, Type type, out IList<IResourceLocation> locations)
		{
			locations = null;
			string mainKey;
			string subKey;
			if (ResourceManagerConfig.ExtractKeyAndSubKey(key, out mainKey, out subKey))
			{
				IList<IResourceLocation> locs;
				if (!this.m_Addressables.GetResourceLocations(mainKey, type, out locs) && type == typeof(Sprite))
				{
					this.m_Addressables.GetResourceLocations(mainKey, typeof(SpriteAtlas), out locs);
				}
				if (locs != null && locs.Count > 0)
				{
					locations = new List<IResourceLocation>(locs.Count);
					foreach (IResourceLocation i in locs)
					{
						this.CreateDynamicLocations(type, locations, key as string, subKey, i);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000701C File Offset: 0x0000521C
		internal void CreateDynamicLocations(Type type, IList<IResourceLocation> locations, string locName, string subKey, IResourceLocation mainLoc)
		{
			if (type == typeof(Sprite) && mainLoc.ResourceType == typeof(SpriteAtlas))
			{
				locations.Add(new ResourceLocationBase(locName, mainLoc.InternalId + "[" + subKey + "]", this.AtlasSpriteProviderId, type, new IResourceLocation[] { mainLoc }));
				return;
			}
			if (mainLoc.HasDependencies)
			{
				locations.Add(new ResourceLocationBase(locName, mainLoc.InternalId + "[" + subKey + "]", mainLoc.ProviderId, mainLoc.ResourceType, mainLoc.Dependencies.ToArray<IResourceLocation>()));
				return;
			}
			locations.Add(new ResourceLocationBase(locName, mainLoc.InternalId + "[" + subKey + "]", mainLoc.ProviderId, mainLoc.ResourceType, Array.Empty<IResourceLocation>()));
		}

		// Token: 0x040000CA RID: 202
		private AddressablesImpl m_Addressables;

		// Token: 0x040000CB RID: 203
		private string m_AtlasSpriteProviderId;
	}
}
