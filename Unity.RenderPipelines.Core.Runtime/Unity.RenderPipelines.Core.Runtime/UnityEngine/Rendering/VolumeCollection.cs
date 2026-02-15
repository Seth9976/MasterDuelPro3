using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020001E7 RID: 487
	internal class VolumeCollection
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0003353A File Offset: 0x0003173A
		public int count
		{
			get
			{
				return this.m_Volumes.Count;
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00033548 File Offset: 0x00031748
		public bool Register(Volume volume, int layer)
		{
			if (volume == null)
			{
				throw new ArgumentNullException("volume", "The volume to register is null");
			}
			if (this.m_Volumes.Contains(volume))
			{
				return false;
			}
			this.m_Volumes.Add(volume);
			foreach (KeyValuePair<int, List<Volume>> kvp in this.m_SortedVolumes)
			{
				if ((kvp.Key & (1 << layer)) != 0 && !kvp.Value.Contains(volume))
				{
					kvp.Value.Add(volume);
				}
			}
			this.SetLayerIndexDirty(layer);
			return true;
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00033600 File Offset: 0x00031800
		public bool Unregister(Volume volume, int layer)
		{
			if (volume == null)
			{
				throw new ArgumentNullException("volume", "The volume to unregister is null");
			}
			this.m_Volumes.Remove(volume);
			foreach (KeyValuePair<int, List<Volume>> kvp in this.m_SortedVolumes)
			{
				if ((kvp.Key & (1 << layer)) != 0)
				{
					kvp.Value.Remove(volume);
				}
			}
			this.SetLayerIndexDirty(layer);
			return true;
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00033698 File Offset: 0x00031898
		public bool ChangeLayer(Volume volume, int previousLayerIndex, int currentLayerIndex)
		{
			if (volume == null)
			{
				throw new ArgumentNullException("volume", "The volume to change layer is null");
			}
			this.Unregister(volume, previousLayerIndex);
			return this.Register(volume, currentLayerIndex);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x000336C4 File Offset: 0x000318C4
		internal static void SortByPriority(List<Volume> volumes)
		{
			for (int i = 1; i < volumes.Count; i++)
			{
				Volume temp = volumes[i];
				int j = i - 1;
				while (j >= 0 && volumes[j].priority > temp.priority)
				{
					volumes[j + 1] = volumes[j];
					j--;
				}
				volumes[j + 1] = temp;
			}
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00033728 File Offset: 0x00031928
		public List<Volume> GrabVolumes(LayerMask mask)
		{
			List<Volume> list;
			if (!this.m_SortedVolumes.TryGetValue(mask, out list))
			{
				list = new List<Volume>();
				int numVolumes = this.m_Volumes.Count;
				for (int i = 0; i < numVolumes; i++)
				{
					Volume volume = this.m_Volumes[i];
					if ((mask & (1 << volume.gameObject.layer)) != 0)
					{
						list.Add(volume);
						this.m_SortNeeded[mask] = true;
					}
				}
				this.m_SortedVolumes.Add(mask, list);
			}
			bool sortNeeded;
			if (this.m_SortNeeded.TryGetValue(mask, out sortNeeded) && sortNeeded)
			{
				this.m_SortNeeded[mask] = false;
				VolumeCollection.SortByPriority(list);
			}
			return list;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000337EC File Offset: 0x000319EC
		public void SetLayerIndexDirty(int layerIndex)
		{
			foreach (KeyValuePair<int, List<Volume>> kvp in this.m_SortedVolumes)
			{
				int mask = kvp.Key;
				if ((mask & (1 << layerIndex)) != 0)
				{
					this.m_SortNeeded[mask] = true;
				}
			}
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00033858 File Offset: 0x00031A58
		public bool IsComponentActiveInMask<T>(LayerMask layerMask) where T : VolumeComponent
		{
			int mask = layerMask.value;
			foreach (KeyValuePair<int, List<Volume>> kvp in this.m_SortedVolumes)
			{
				if (kvp.Key == mask)
				{
					foreach (Volume volume in kvp.Value)
					{
						T component;
						if (volume.enabled && !(volume.profileRef == null) && volume.profileRef.TryGet<T>(out component) && component.active)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0400093B RID: 2363
		internal const int k_MaxLayerCount = 32;

		// Token: 0x0400093C RID: 2364
		private readonly Dictionary<int, List<Volume>> m_SortedVolumes = new Dictionary<int, List<Volume>>();

		// Token: 0x0400093D RID: 2365
		private readonly List<Volume> m_Volumes = new List<Volume>();

		// Token: 0x0400093E RID: 2366
		private readonly Dictionary<int, bool> m_SortNeeded = new Dictionary<int, bool>();
	}
}
