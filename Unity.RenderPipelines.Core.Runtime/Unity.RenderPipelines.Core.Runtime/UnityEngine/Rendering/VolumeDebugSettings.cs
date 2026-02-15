using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E1 RID: 225
	public abstract class VolumeDebugSettings<T> : IVolumeDebugSettings where T : MonoBehaviour, IAdditionalData
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00011160 File Offset: 0x0000F360
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x00011168 File Offset: 0x0000F368
		public int selectedComponent { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x00011171 File Offset: 0x0000F371
		public Camera selectedCamera
		{
			get
			{
				if (this.selectedCameraIndex >= 0)
				{
					return this.cameras.ElementAt(this.selectedCameraIndex);
				}
				return null;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00011190 File Offset: 0x0000F390
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x000111C0 File Offset: 0x0000F3C0
		public int selectedCameraIndex
		{
			get
			{
				int count = this.cameras.Count<Camera>();
				if (count <= 0)
				{
					return -1;
				}
				return Math.Clamp(this.m_SelectedCameraIndex, 0, count - 1);
			}
			set
			{
				int count = this.cameras.Count<Camera>();
				this.m_SelectedCameraIndex = Math.Clamp(value, 0, count - 1);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x000111EC File Offset: 0x0000F3EC
		public IEnumerable<Camera> cameras
		{
			get
			{
				this.m_Cameras.Clear();
				if (this.m_CamerasArray == null || this.m_CamerasArray.Length != Camera.allCamerasCount)
				{
					this.m_CamerasArray = new Camera[Camera.allCamerasCount];
				}
				Camera.GetAllCameras(this.m_CamerasArray);
				foreach (Camera camera in this.m_CamerasArray)
				{
					T additionalData;
					if (!(camera == null) && camera.cameraType != CameraType.Preview && camera.cameraType != CameraType.Reflection && camera.TryGetComponent<T>(out additionalData))
					{
						this.m_Cameras.Add(camera);
					}
				}
				return this.m_Cameras;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000742 RID: 1858
		public abstract VolumeStack selectedCameraVolumeStack { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000743 RID: 1859
		public abstract LayerMask selectedCameraLayerMask { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000744 RID: 1860
		public abstract Vector3 selectedCameraPosition { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00011288 File Offset: 0x0000F488
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x000112B0 File Offset: 0x0000F4B0
		public Type selectedComponentType
		{
			get
			{
				if (this.selectedComponent <= 0)
				{
					return null;
				}
				return this.volumeComponentsPathAndType[this.selectedComponent - 1].Item2;
			}
			set
			{
				int index = this.volumeComponentsPathAndType.FindIndex((ValueTuple<string, Type> t) => t.Item2 == value);
				if (index != -1)
				{
					this.selectedComponent = index + 1;
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x000112EF File Offset: 0x0000F4EF
		public List<ValueTuple<string, Type>> volumeComponentsPathAndType
		{
			get
			{
				return VolumeManager.instance.GetVolumeComponentsForDisplay(GraphicsSettings.currentRenderPipelineAssetType);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00011300 File Offset: 0x0000F500
		[Obsolete("This property is obsolete and kept only for not breaking user code. VolumeDebugSettings will use current pipeline when it needs to gather volume component types and paths. #from(23.2)", false)]
		public virtual Type targetRenderPipeline { get; }

		// Token: 0x06000749 RID: 1865 RVA: 0x00011308 File Offset: 0x0000F508
		internal VolumeParameter GetParameter(VolumeComponent component, FieldInfo field)
		{
			return (VolumeParameter)field.GetValue(component);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00011318 File Offset: 0x0000F518
		internal VolumeParameter GetParameter(FieldInfo field)
		{
			VolumeStack stack = this.selectedCameraVolumeStack;
			if (stack != null)
			{
				return this.GetParameter(stack.GetComponent(this.selectedComponentType), field);
			}
			return null;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00011344 File Offset: 0x0000F544
		internal VolumeParameter GetParameter(Volume volume, FieldInfo field)
		{
			VolumeComponent component;
			if (!(volume.HasInstantiatedProfile() ? volume.profile : volume.sharedProfile).TryGet<VolumeComponent>(this.selectedComponentType, out component))
			{
				return null;
			}
			VolumeParameter param = this.GetParameter(component, field);
			if (!param.overrideState)
			{
				return null;
			}
			return param;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001138C File Offset: 0x0000F58C
		private float ComputeWeight(Volume volume, Vector3 triggerPos)
		{
			if (volume == null)
			{
				return 0f;
			}
			VolumeProfile profile = (volume.HasInstantiatedProfile() ? volume.profile : volume.sharedProfile);
			if (!volume.gameObject.activeInHierarchy)
			{
				return 0f;
			}
			if (!volume.enabled || profile == null || volume.weight <= 0f)
			{
				return 0f;
			}
			VolumeComponent component;
			if (!profile.TryGet<VolumeComponent>(this.selectedComponentType, out component))
			{
				return 0f;
			}
			if (!component.active)
			{
				return 0f;
			}
			float weight = Mathf.Clamp01(volume.weight);
			if (!volume.isGlobal)
			{
				Collider[] components = volume.GetComponents<Collider>();
				float closestDistanceSqr = float.PositiveInfinity;
				foreach (Collider collider in components)
				{
					if (collider.enabled)
					{
						float d = (collider.ClosestPoint(triggerPos) - triggerPos).sqrMagnitude;
						if (d < closestDistanceSqr)
						{
							closestDistanceSqr = d;
						}
					}
				}
				float blendDistSqr = volume.blendDistance * volume.blendDistance;
				if (closestDistanceSqr > blendDistSqr)
				{
					weight = 0f;
				}
				else if (blendDistSqr > 0f)
				{
					weight *= 1f - closestDistanceSqr / blendDistSqr;
				}
			}
			return weight;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x000114B6 File Offset: 0x0000F6B6
		public Volume[] GetVolumes()
		{
			return (from v in VolumeManager.instance.GetVolumes(this.selectedCameraLayerMask)
				where v.sharedProfile != null
				select v).Reverse<Volume>().ToArray<Volume>();
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000114F8 File Offset: 0x0000F6F8
		private VolumeParameter[,] GetStates()
		{
			FieldInfo[] fields = (from t in this.selectedComponentType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				where t.FieldType.IsSubclassOf(typeof(VolumeParameter))
				select t).ToArray<FieldInfo>();
			VolumeParameter[,] states = new VolumeParameter[this.volumes.Length, fields.Length];
			for (int i = 0; i < this.volumes.Length; i++)
			{
				VolumeComponent component;
				if ((this.volumes[i].HasInstantiatedProfile() ? this.volumes[i].profile : this.volumes[i].sharedProfile).TryGet<VolumeComponent>(this.selectedComponentType, out component))
				{
					for (int j = 0; j < fields.Length; j++)
					{
						VolumeParameter param = this.GetParameter(component, fields[j]);
						states[i, j] = (param.overrideState ? param : null);
					}
				}
			}
			return states;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000115D4 File Offset: 0x0000F7D4
		private bool ChangedStates(VolumeParameter[,] newStates)
		{
			if (this.savedStates.GetLength(1) != newStates.GetLength(1))
			{
				return true;
			}
			for (int i = 0; i < this.savedStates.GetLength(0); i++)
			{
				for (int j = 0; j < this.savedStates.GetLength(1); j++)
				{
					if (this.savedStates[i, j] == null != (newStates[i, j] == null))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00011648 File Offset: 0x0000F848
		public bool RefreshVolumes(Volume[] newVolumes)
		{
			bool ret = false;
			if (this.volumes == null || !newVolumes.SequenceEqual(this.volumes))
			{
				this.volumes = (Volume[])newVolumes.Clone();
				this.savedStates = this.GetStates();
				ret = true;
			}
			else
			{
				VolumeParameter[,] newStates = this.GetStates();
				if (this.savedStates == null || this.ChangedStates(newStates))
				{
					this.savedStates = newStates;
					ret = true;
				}
			}
			Vector3 triggerPos = this.selectedCameraPosition;
			this.weights = new float[this.volumes.Length];
			for (int i = 0; i < this.volumes.Length; i++)
			{
				this.weights[i] = this.ComputeWeight(this.volumes[i], triggerPos);
			}
			return ret;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000116F4 File Offset: 0x0000F8F4
		public float GetVolumeWeight(Volume volume)
		{
			if (this.weights == null)
			{
				return 0f;
			}
			float total = 0f;
			for (int i = 0; i < this.volumes.Length; i++)
			{
				float weight = this.weights[i];
				weight *= 1f - total;
				total += weight;
				if (this.volumes[i] == volume)
				{
					return weight;
				}
			}
			return 0f;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001175C File Offset: 0x0000F95C
		public bool VolumeHasInfluence(Volume volume)
		{
			if (this.weights == null)
			{
				return false;
			}
			int index = Array.IndexOf<Volume>(this.volumes, volume);
			return index != -1 && this.weights[index] != 0f;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00011798 File Offset: 0x0000F998
		[Obsolete("Please use volumeComponentsPathAndType instead, and get the second element of the tuple", false)]
		public static List<Type> componentTypes
		{
			get
			{
				if (VolumeDebugSettings<T>.s_ComponentTypes == null)
				{
					VolumeDebugSettings<T>.s_ComponentTypes = (from t in VolumeManager.instance.baseComponentTypeArray
						where !t.IsDefined(typeof(HideInInspector), false)
						where !t.IsDefined(typeof(ObsoleteAttribute), false)
						orderby VolumeDebugSettings<T>.ComponentDisplayName(t)
						select t).ToList<Type>();
				}
				return VolumeDebugSettings<T>.s_ComponentTypes;
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00011834 File Offset: 0x0000FA34
		[Obsolete("Please use componentPathAndType instead, and get the first element of the tuple", false)]
		public static string ComponentDisplayName(Type component)
		{
			VolumeComponentMenuForRenderPipeline volumeComponentMenuForRenderPipeline = component.GetCustomAttribute(typeof(VolumeComponentMenuForRenderPipeline), false) as VolumeComponentMenuForRenderPipeline;
			if (volumeComponentMenuForRenderPipeline != null)
			{
				return volumeComponentMenuForRenderPipeline.menu;
			}
			VolumeComponentMenuForRenderPipeline volumeComponentMenu = component.GetCustomAttribute(typeof(VolumeComponentMenu), false) as VolumeComponentMenuForRenderPipeline;
			if (volumeComponentMenu != null)
			{
				return volumeComponentMenu.menu;
			}
			return component.Name;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00011889 File Offset: 0x0000FA89
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x00011890 File Offset: 0x0000FA90
		[Obsolete("Cameras are auto registered/unregistered, use property cameras", false)]
		private protected static List<T> additionalCameraDatas { protected get; private set; } = new List<T>();

		// Token: 0x06000757 RID: 1879 RVA: 0x00011898 File Offset: 0x0000FA98
		[Obsolete("Cameras are auto registered/unregistered", false)]
		public static void RegisterCamera(T additionalCamera)
		{
			if (!VolumeDebugSettings<T>.additionalCameraDatas.Contains(additionalCamera))
			{
				VolumeDebugSettings<T>.additionalCameraDatas.Add(additionalCamera);
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000118B2 File Offset: 0x0000FAB2
		[Obsolete("Cameras are auto registered/unregistered", false)]
		public static void UnRegisterCamera(T additionalCamera)
		{
			if (VolumeDebugSettings<T>.additionalCameraDatas.Contains(additionalCamera))
			{
				VolumeDebugSettings<T>.additionalCameraDatas.Remove(additionalCamera);
			}
		}

		// Token: 0x040002BF RID: 703
		protected int m_SelectedCameraIndex = -1;

		// Token: 0x040002C0 RID: 704
		private Camera[] m_CamerasArray;

		// Token: 0x040002C1 RID: 705
		private List<Camera> m_Cameras = new List<Camera>();

		// Token: 0x040002C3 RID: 707
		private float[] weights;

		// Token: 0x040002C4 RID: 708
		private Volume[] volumes;

		// Token: 0x040002C5 RID: 709
		private VolumeParameter[,] savedStates;

		// Token: 0x040002C6 RID: 710
		private static List<Type> s_ComponentTypes;
	}
}
