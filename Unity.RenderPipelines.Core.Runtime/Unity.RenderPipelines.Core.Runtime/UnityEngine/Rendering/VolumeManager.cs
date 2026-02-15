using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.Profiling;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E6 RID: 230
	public sealed class VolumeManager
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x00011974 File Offset: 0x0000FB74
		[Obsolete("Please use the Register without a given layer index #from(6000.0)", false)]
		public void Register(Volume volume, int layer)
		{
			if (volume.gameObject.layer != layer)
			{
				Debug.LogWarning(string.Format("Trying to register Volume {0} on layer index {1}, when the GameObject {2} is on layer index {3}.", new object[]
				{
					volume.name,
					layer,
					volume.gameObject.name,
					volume.gameObject.layer
				}) + Environment.NewLine + "The Volume Manager will respect the GameObject's layer.");
			}
			this.Register(volume);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000119F0 File Offset: 0x0000FBF0
		[Obsolete("Please use the Register without a given layer index #from(6000.0)", false)]
		public void Unregister(Volume volume, int layer)
		{
			if (volume.gameObject.layer != layer)
			{
				Debug.LogWarning(string.Format("Trying to unregister Volume {0} on layer index {1}, when the GameObject {2} is on layer index {3}.", new object[]
				{
					volume.name,
					layer,
					volume.gameObject.name,
					volume.gameObject.layer
				}) + Environment.NewLine + "The Volume Manager will respect the GameObject's layer.");
			}
			this.Unregister(volume);
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00011A69 File Offset: 0x0000FC69
		public static VolumeManager instance
		{
			get
			{
				return VolumeManager.s_Instance.Value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x00011A75 File Offset: 0x0000FC75
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x00011A7D File Offset: 0x0000FC7D
		public VolumeStack stack { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00011A86 File Offset: 0x0000FC86
		[Obsolete("Please use baseComponentTypeArray instead.")]
		public IEnumerable<Type> baseComponentTypes
		{
			get
			{
				return this.baseComponentTypeArray;
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00011A90 File Offset: 0x0000FC90
		internal List<ValueTuple<string, Type>> GetVolumeComponentsForDisplay(Type currentPipelineAssetType)
		{
			if (currentPipelineAssetType == null)
			{
				return new List<ValueTuple<string, Type>>();
			}
			if (!currentPipelineAssetType.IsSubclassOf(typeof(RenderPipelineAsset)))
			{
				throw new ArgumentException("currentPipelineAssetType");
			}
			List<ValueTuple<string, Type>> supportedVolumeComponents;
			if (VolumeManager.s_SupportedVolumeComponentsForRenderPipeline.TryGetValue(currentPipelineAssetType, out supportedVolumeComponents))
			{
				return supportedVolumeComponents;
			}
			if (this.baseComponentTypeArray == null)
			{
				this.LoadBaseTypes(currentPipelineAssetType);
			}
			supportedVolumeComponents = this.BuildVolumeComponentDisplayList(this.baseComponentTypeArray);
			VolumeManager.s_SupportedVolumeComponentsForRenderPipeline[currentPipelineAssetType] = supportedVolumeComponents;
			return supportedVolumeComponents;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00011B04 File Offset: 0x0000FD04
		private List<ValueTuple<string, Type>> BuildVolumeComponentDisplayList(Type[] types)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			List<ValueTuple<string, Type>> volumes = new List<ValueTuple<string, Type>>();
			foreach (Type t in types)
			{
				string path = string.Empty;
				bool skipComponent = false;
				foreach (object attr in t.GetCustomAttributes(false))
				{
					VolumeComponentMenu attrMenu = attr as VolumeComponentMenu;
					if (attrMenu == null)
					{
						if (attr is HideInInspector || attr is ObsoleteAttribute)
						{
							skipComponent = true;
						}
					}
					else
					{
						path = attrMenu.menu;
					}
				}
				if (!skipComponent)
				{
					if (string.IsNullOrEmpty(path))
					{
						path = t.Name;
					}
					volumes.Add(new ValueTuple<string, Type>(path, t));
				}
			}
			return volumes.OrderBy((ValueTuple<string, Type> i) => i.Item1).ToList<ValueTuple<string, Type>>();
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		public Type[] baseComponentTypeArray { get; internal set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00011BF9 File Offset: 0x0000FDF9
		// (set) Token: 0x06000776 RID: 1910 RVA: 0x00011C01 File Offset: 0x0000FE01
		public VolumeProfile globalDefaultProfile { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00011C0A File Offset: 0x0000FE0A
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x00011C12 File Offset: 0x0000FE12
		public VolumeProfile qualityDefaultProfile { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00011C1B File Offset: 0x0000FE1B
		// (set) Token: 0x0600077A RID: 1914 RVA: 0x00011C23 File Offset: 0x0000FE23
		public ReadOnlyCollection<VolumeProfile> customDefaultProfiles { get; private set; }

		// Token: 0x0600077B RID: 1915 RVA: 0x00011C2C File Offset: 0x0000FE2C
		public VolumeComponent GetVolumeComponentDefaultState(Type volumeComponentType)
		{
			if (!typeof(VolumeComponent).IsAssignableFrom(volumeComponentType))
			{
				return null;
			}
			foreach (VolumeComponent component in this.m_ComponentsDefaultState)
			{
				if (component.GetType() == volumeComponentType)
				{
					return component;
				}
			}
			return null;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00011C77 File Offset: 0x0000FE77
		internal VolumeManager()
		{
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00011CA1 File Offset: 0x0000FEA1
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x00011CA9 File Offset: 0x0000FEA9
		public bool isInitialized { get; private set; }

		// Token: 0x0600077F RID: 1919 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		public void Initialize(VolumeProfile globalDefaultVolumeProfile = null, VolumeProfile qualityDefaultVolumeProfile = null)
		{
			this.LoadBaseTypes(GraphicsSettings.currentRenderPipelineAssetType);
			this.InitializeVolumeComponents();
			this.globalDefaultProfile = globalDefaultVolumeProfile;
			this.qualityDefaultProfile = qualityDefaultVolumeProfile;
			this.EvaluateVolumeDefaultState();
			this.m_DefaultStack = this.CreateStack();
			this.stack = this.m_DefaultStack;
			this.isInitialized = true;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00011D08 File Offset: 0x0000FF08
		public void Deinitialize()
		{
			this.DestroyStack(this.m_DefaultStack);
			this.m_DefaultStack = null;
			foreach (VolumeStack volumeStack in this.m_CreatedVolumeStacks)
			{
				volumeStack.Dispose();
			}
			this.m_CreatedVolumeStacks.Clear();
			this.baseComponentTypeArray = null;
			this.globalDefaultProfile = null;
			this.qualityDefaultProfile = null;
			this.customDefaultProfiles = null;
			this.isInitialized = false;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00011D9C File Offset: 0x0000FF9C
		public void SetGlobalDefaultProfile(VolumeProfile profile)
		{
			this.globalDefaultProfile = profile;
			this.EvaluateVolumeDefaultState();
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00011DAB File Offset: 0x0000FFAB
		public void SetQualityDefaultProfile(VolumeProfile profile)
		{
			this.qualityDefaultProfile = profile;
			this.EvaluateVolumeDefaultState();
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00011DBC File Offset: 0x0000FFBC
		public void SetCustomDefaultProfiles(List<VolumeProfile> profiles)
		{
			List<VolumeProfile> validProfiles = profiles ?? new List<VolumeProfile>();
			validProfiles.RemoveAll((VolumeProfile x) => x == null);
			this.customDefaultProfiles = new ReadOnlyCollection<VolumeProfile>(validProfiles);
			this.EvaluateVolumeDefaultState();
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00011E0C File Offset: 0x0001000C
		public void OnVolumeProfileChanged(VolumeProfile profile)
		{
			if (!this.isInitialized)
			{
				return;
			}
			if (this.globalDefaultProfile == profile || this.qualityDefaultProfile == profile || (this.customDefaultProfiles != null && this.customDefaultProfiles.Contains(profile)))
			{
				this.EvaluateVolumeDefaultState();
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00011E5C File Offset: 0x0001005C
		public void OnVolumeComponentChanged(VolumeComponent component)
		{
			List<VolumeProfile> defaultProfiles = new List<VolumeProfile> { this.globalDefaultProfile, this.globalDefaultProfile };
			if (this.customDefaultProfiles != null)
			{
				defaultProfiles.AddRange(this.customDefaultProfiles);
			}
			using (List<VolumeProfile>.Enumerator enumerator = defaultProfiles.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.components.Contains(component))
					{
						this.EvaluateVolumeDefaultState();
						break;
					}
				}
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00011EEC File Offset: 0x000100EC
		public VolumeStack CreateStack()
		{
			VolumeStack stack = new VolumeStack();
			stack.Reload(this.baseComponentTypeArray);
			this.m_CreatedVolumeStacks.Add(stack);
			return stack;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00011F18 File Offset: 0x00010118
		public void ResetMainStack()
		{
			this.stack = this.m_DefaultStack;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00011F26 File Offset: 0x00010126
		public void DestroyStack(VolumeStack stack)
		{
			this.m_CreatedVolumeStacks.Remove(stack);
			stack.Dispose();
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00011F3C File Offset: 0x0001013C
		private bool IsSupportedByObsoleteVolumeComponentMenuForRenderPipeline(Type t, Type pipelineAssetType)
		{
			bool legacySupported = false;
			if (t.GetCustomAttribute<VolumeComponentMenuForRenderPipeline>() != null)
			{
				Debug.LogWarning(string.Format("{0} is deprecated, use {1} and {2} with {3} instead. #from(2023.1)", new object[] { "VolumeComponentMenuForRenderPipeline", "SupportedOnRenderPipelineAttribute", "VolumeComponentMenu", t }));
			}
			return legacySupported;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00011F88 File Offset: 0x00010188
		internal void LoadBaseTypes(Type pipelineAssetType)
		{
			List<Type> list;
			using (ListPool<Type>.Get(out list))
			{
				foreach (Type t in CoreUtils.GetAllTypesDerivedFrom<VolumeComponent>())
				{
					if (!t.IsAbstract && (SupportedOnRenderPipelineAttribute.IsTypeSupportedOnRenderPipeline(t, pipelineAssetType) || this.IsSupportedByObsoleteVolumeComponentMenuForRenderPipeline(t, pipelineAssetType)))
					{
						list.Add(t);
					}
				}
				this.baseComponentTypeArray = list.ToArray();
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00012024 File Offset: 0x00010224
		internal void InitializeVolumeComponents()
		{
			BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			Type[] baseComponentTypeArray = this.baseComponentTypeArray;
			for (int i = 0; i < baseComponentTypeArray.Length; i++)
			{
				MethodInfo initMethod = baseComponentTypeArray[i].GetMethod("Init", flags);
				if (initMethod != null)
				{
					initMethod.Invoke(null, null);
				}
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001206C File Offset: 0x0001026C
		internal void EvaluateVolumeDefaultState()
		{
			if (this.baseComponentTypeArray == null || this.baseComponentTypeArray.Length == 0)
			{
				return;
			}
			using (VolumeManager.k_ProfilerMarkerEvaluateVolumeDefaultState.Auto())
			{
				VolumeManager.<>c__DisplayClass58_0 CS$<>8__locals1;
				CS$<>8__locals1.componentsDefaultStateList = new List<VolumeComponent>();
				foreach (Type type in this.baseComponentTypeArray)
				{
					CS$<>8__locals1.componentsDefaultStateList.Add((VolumeComponent)ScriptableObject.CreateInstance(type));
				}
				VolumeManager.<EvaluateVolumeDefaultState>g__ApplyDefaultProfile|58_0(this.globalDefaultProfile, ref CS$<>8__locals1);
				VolumeManager.<EvaluateVolumeDefaultState>g__ApplyDefaultProfile|58_0(this.qualityDefaultProfile, ref CS$<>8__locals1);
				if (this.customDefaultProfiles != null)
				{
					foreach (VolumeProfile volumeProfile in this.customDefaultProfiles)
					{
						VolumeManager.<EvaluateVolumeDefaultState>g__ApplyDefaultProfile|58_0(volumeProfile, ref CS$<>8__locals1);
					}
				}
				List<VolumeParameter> parametersDefaultStateList = new List<VolumeParameter>();
				foreach (VolumeComponent component in CS$<>8__locals1.componentsDefaultStateList)
				{
					parametersDefaultStateList.AddRange(component.parameters);
				}
				this.m_ComponentsDefaultState = CS$<>8__locals1.componentsDefaultStateList.ToArray();
				this.m_ParametersDefaultState = parametersDefaultStateList.ToArray();
				foreach (VolumeStack volumeStack in this.m_CreatedVolumeStacks)
				{
					volumeStack.requiresReset = true;
					volumeStack.requiresResetForAllProperties = true;
				}
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00012244 File Offset: 0x00010444
		public void Register(Volume volume)
		{
			this.m_VolumeCollection.Register(volume, volume.gameObject.layer);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001225E File Offset: 0x0001045E
		public void Unregister(Volume volume)
		{
			this.m_VolumeCollection.Unregister(volume, volume.gameObject.layer);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00012278 File Offset: 0x00010478
		public bool IsComponentActiveInMask<T>(LayerMask layerMask) where T : VolumeComponent
		{
			return this.m_VolumeCollection.IsComponentActiveInMask<T>(layerMask);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00012286 File Offset: 0x00010486
		internal void SetLayerDirty(int layer)
		{
			this.m_VolumeCollection.SetLayerIndexDirty(layer);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00012294 File Offset: 0x00010494
		internal void UpdateVolumeLayer(Volume volume, int prevLayer, int newLayer)
		{
			this.m_VolumeCollection.ChangeLayer(volume, prevLayer, newLayer);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000122A8 File Offset: 0x000104A8
		private void OverrideData(VolumeStack stack, List<VolumeComponent> components, float interpFactor)
		{
			int numComponents = components.Count;
			for (int i = 0; i < numComponents; i++)
			{
				VolumeComponent component = components[i];
				if (component.active)
				{
					VolumeComponent state = stack.GetComponent(component.GetType());
					if (state != null)
					{
						component.Override(state, interpFactor);
					}
				}
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000122F8 File Offset: 0x000104F8
		internal void ReplaceData(VolumeStack stack)
		{
			using (VolumeManager.k_ProfilerMarkerReplaceData.Auto())
			{
				VolumeParameter[] stackParams = stack.parameters;
				bool resetAllParameters = stack.requiresResetForAllProperties;
				int count = stackParams.Length;
				for (int i = 0; i < count; i++)
				{
					VolumeParameter stackParam = stackParams[i];
					if (stackParam.overrideState || resetAllParameters)
					{
						stackParam.overrideState = false;
						stackParam.SetValue(this.m_ParametersDefaultState[i]);
					}
				}
				stack.requiresResetForAllProperties = false;
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00012388 File Offset: 0x00010588
		[Conditional("UNITY_EDITOR")]
		public void CheckDefaultVolumeState()
		{
			if (this.m_ComponentsDefaultState == null || (this.m_ComponentsDefaultState.Length != 0 && this.m_ComponentsDefaultState[0] == null))
			{
				this.EvaluateVolumeDefaultState();
			}
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000123B4 File Offset: 0x000105B4
		[Conditional("UNITY_EDITOR")]
		public void CheckStack(VolumeStack stack)
		{
			if (stack.components == null)
			{
				stack.Reload(this.baseComponentTypeArray);
				return;
			}
			foreach (KeyValuePair<Type, VolumeComponent> kvp in stack.components)
			{
				if (kvp.Key == null || kvp.Value == null)
				{
					stack.Reload(this.baseComponentTypeArray);
					break;
				}
			}
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00012444 File Offset: 0x00010644
		private bool CheckUpdateRequired(VolumeStack stack)
		{
			if (this.m_VolumeCollection.count != 0)
			{
				stack.requiresReset = true;
				return true;
			}
			if (stack.requiresReset)
			{
				stack.requiresReset = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001246E File Offset: 0x0001066E
		public void Update(Transform trigger, LayerMask layerMask)
		{
			this.Update(this.stack, trigger, layerMask);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00012480 File Offset: 0x00010680
		public void Update(VolumeStack stack, Transform trigger, LayerMask layerMask)
		{
			using (VolumeManager.k_ProfilerMarkerUpdate.Auto())
			{
				if (this.isInitialized)
				{
					if (this.CheckUpdateRequired(stack))
					{
						this.ReplaceData(stack);
						bool onlyGlobal = trigger == null;
						Vector3 triggerPos = (onlyGlobal ? Vector3.zero : trigger.position);
						List<Volume> volumes = this.GrabVolumes(layerMask);
						Camera camera = null;
						if (!onlyGlobal)
						{
							trigger.TryGetComponent<Camera>(out camera);
						}
						int numVolumes = volumes.Count;
						for (int i = 0; i < numVolumes; i++)
						{
							Volume volume = volumes[i];
							if (!(volume == null) && volume.enabled && !(volume.profileRef == null) && volume.weight > 0f)
							{
								if (volume.isGlobal)
								{
									this.OverrideData(stack, volume.profileRef.components, Mathf.Clamp01(volume.weight));
								}
								else if (!onlyGlobal)
								{
									List<Collider> colliders = this.m_TempColliders;
									volume.GetComponents<Collider>(colliders);
									if (colliders.Count != 0)
									{
										float closestDistanceSqr = float.PositiveInfinity;
										int numColliders = colliders.Count;
										for (int c = 0; c < numColliders; c++)
										{
											Collider collider = colliders[c];
											if (collider.enabled)
											{
												float d = (collider.ClosestPoint(triggerPos) - triggerPos).sqrMagnitude;
												if (d < closestDistanceSqr)
												{
													closestDistanceSqr = d;
												}
											}
										}
										colliders.Clear();
										float blendDistSqr = volume.blendDistance * volume.blendDistance;
										if (closestDistanceSqr <= blendDistSqr)
										{
											float interpFactor = 1f;
											if (blendDistSqr > 0f)
											{
												interpFactor = 1f - closestDistanceSqr / blendDistSqr;
											}
											this.OverrideData(stack, volume.profileRef.components, interpFactor * Mathf.Clamp01(volume.weight));
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00012684 File Offset: 0x00010884
		public Volume[] GetVolumes(LayerMask layerMask)
		{
			List<Volume> list = this.GrabVolumes(layerMask);
			list.RemoveAll((Volume v) => v == null);
			return list.ToArray();
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000126B8 File Offset: 0x000108B8
		private List<Volume> GrabVolumes(LayerMask mask)
		{
			return this.m_VolumeCollection.GrabVolumes(mask);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x000104EC File Offset: 0x0000E6EC
		private static bool IsVolumeRenderedByCamera(Volume volume, Camera camera)
		{
			return true;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00012728 File Offset: 0x00010928
		[CompilerGenerated]
		internal static void <EvaluateVolumeDefaultState>g__ApplyDefaultProfile|58_0(VolumeProfile profile, ref VolumeManager.<>c__DisplayClass58_0 A_1)
		{
			if (profile == null)
			{
				return;
			}
			for (int i = 0; i < profile.components.Count; i++)
			{
				VolumeComponent profileComponent = profile.components[i];
				VolumeComponent defaultStateComponent = A_1.componentsDefaultStateList.FirstOrDefault((VolumeComponent x) => x.GetType() == profileComponent.GetType());
				if (defaultStateComponent != null && profileComponent.active)
				{
					profileComponent.Override(defaultStateComponent, 1f);
				}
			}
		}

		// Token: 0x040002CF RID: 719
		private static readonly ProfilerMarker k_ProfilerMarkerUpdate = new ProfilerMarker("VolumeManager.Update");

		// Token: 0x040002D0 RID: 720
		private static readonly ProfilerMarker k_ProfilerMarkerReplaceData = new ProfilerMarker("VolumeManager.ReplaceData");

		// Token: 0x040002D1 RID: 721
		private static readonly ProfilerMarker k_ProfilerMarkerEvaluateVolumeDefaultState = new ProfilerMarker("VolumeManager.EvaluateVolumeDefaultState");

		// Token: 0x040002D2 RID: 722
		private static readonly Lazy<VolumeManager> s_Instance = new Lazy<VolumeManager>(() => new VolumeManager());

		// Token: 0x040002D4 RID: 724
		private static readonly Dictionary<Type, List<ValueTuple<string, Type>>> s_SupportedVolumeComponentsForRenderPipeline = new Dictionary<Type, List<ValueTuple<string, Type>>>();

		// Token: 0x040002D9 RID: 729
		private readonly VolumeCollection m_VolumeCollection = new VolumeCollection();

		// Token: 0x040002DA RID: 730
		private VolumeComponent[] m_ComponentsDefaultState;

		// Token: 0x040002DB RID: 731
		internal VolumeParameter[] m_ParametersDefaultState;

		// Token: 0x040002DC RID: 732
		private readonly List<Collider> m_TempColliders = new List<Collider>(8);

		// Token: 0x040002DD RID: 733
		private VolumeStack m_DefaultStack;

		// Token: 0x040002DE RID: 734
		private readonly List<VolumeStack> m_CreatedVolumeStacks = new List<VolumeStack>();
	}
}
