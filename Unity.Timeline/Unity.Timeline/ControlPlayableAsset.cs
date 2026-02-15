using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000034 RID: 52
	[NotKeyable]
	[Serializable]
	public class ControlPlayableAsset : PlayableAsset, IPropertyPreview, ITimelineClipAsset
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000735E File Offset: 0x0000555E
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00007366 File Offset: 0x00005566
		internal bool controllingDirectors { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000736F File Offset: 0x0000556F
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00007377 File Offset: 0x00005577
		internal bool controllingParticles { get; private set; }

		// Token: 0x060001F0 RID: 496 RVA: 0x00007380 File Offset: 0x00005580
		public void OnEnable()
		{
			if (this.particleRandomSeed == 0U)
			{
				this.particleRandomSeed = (uint)Random.Range(1, 10000);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000739B File Offset: 0x0000559B
		public override double duration
		{
			get
			{
				return this.m_Duration;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000073A3 File Offset: 0x000055A3
		public ClipCaps clipCaps
		{
			get
			{
				return ClipCaps.ClipIn | ClipCaps.SpeedMultiplier | (this.m_SupportLoop ? ClipCaps.Looping : ClipCaps.None);
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000073B4 File Offset: 0x000055B4
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			if (this.prefabGameObject != null)
			{
				if (ControlPlayableAsset.s_CreatedPrefabs.Contains(this.prefabGameObject))
				{
					Debug.LogWarningFormat("Control Track Clip ({0}) is causing a prefab to instantiate itself recursively. Aborting further instances.", new object[] { base.name });
					return Playable.Create(graph, 0);
				}
				ControlPlayableAsset.s_CreatedPrefabs.Add(this.prefabGameObject);
			}
			Playable root = Playable.Null;
			List<Playable> playables = new List<Playable>();
			GameObject sourceObject = this.sourceGameObject.Resolve(graph.GetResolver());
			if (this.prefabGameObject != null)
			{
				Transform parenTransform = ((sourceObject != null) ? sourceObject.transform : null);
				ScriptPlayable<PrefabControlPlayable> controlPlayable = PrefabControlPlayable.Create(graph, this.prefabGameObject, parenTransform);
				sourceObject = controlPlayable.GetBehaviour().prefabInstance;
				playables.Add(controlPlayable);
			}
			this.m_Duration = PlayableBinding.DefaultDuration;
			this.m_SupportLoop = false;
			this.controllingParticles = false;
			this.controllingDirectors = false;
			if (sourceObject != null)
			{
				IList<PlayableDirector> list2;
				if (!this.updateDirector)
				{
					IList<PlayableDirector> list = ControlPlayableAsset.k_EmptyDirectorsList;
					list2 = list;
				}
				else
				{
					list2 = this.GetComponent<PlayableDirector>(sourceObject);
				}
				IList<PlayableDirector> directors = list2;
				IList<ParticleSystem> list4;
				if (!this.updateParticle)
				{
					IList<ParticleSystem> list3 = ControlPlayableAsset.k_EmptyParticlesList;
					list4 = list3;
				}
				else
				{
					list4 = this.GetControllableParticleSystems(sourceObject);
				}
				IList<ParticleSystem> particleSystems = list4;
				this.UpdateDurationAndLoopFlag(directors, particleSystems);
				PlayableDirector director = go.GetComponent<PlayableDirector>();
				if (director != null)
				{
					this.m_ControlDirectorAsset = director.playableAsset;
				}
				if (go == sourceObject && this.prefabGameObject == null)
				{
					Debug.LogWarningFormat("Control Playable ({0}) is referencing the same PlayableDirector component than the one in which it is playing.", new object[] { base.name });
					this.active = false;
					if (!this.searchHierarchy)
					{
						this.updateDirector = false;
					}
				}
				if (this.active)
				{
					this.CreateActivationPlayable(sourceObject, graph, playables);
				}
				if (this.updateDirector)
				{
					this.SearchHierarchyAndConnectDirector(directors, graph, playables, this.prefabGameObject != null);
				}
				if (this.updateParticle)
				{
					this.SearchHierarchyAndConnectParticleSystem(particleSystems, graph, playables);
				}
				if (this.updateITimeControl)
				{
					ControlPlayableAsset.SearchHierarchyAndConnectControlableScripts(ControlPlayableAsset.GetControlableScripts(sourceObject), graph, playables);
				}
				root = ControlPlayableAsset.ConnectPlayablesToMixer(graph, playables);
			}
			if (this.prefabGameObject != null)
			{
				ControlPlayableAsset.s_CreatedPrefabs.Remove(this.prefabGameObject);
			}
			if (!root.IsValid<Playable>())
			{
				root = Playable.Create(graph, 0);
			}
			return root;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000075E0 File Offset: 0x000057E0
		private static Playable ConnectPlayablesToMixer(PlayableGraph graph, List<Playable> playables)
		{
			Playable mixer = Playable.Create(graph, playables.Count);
			for (int i = 0; i != playables.Count; i++)
			{
				ControlPlayableAsset.ConnectMixerAndPlayable(graph, mixer, playables[i], i);
			}
			mixer.SetPropagateSetTime(true);
			return mixer;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007624 File Offset: 0x00005824
		private void CreateActivationPlayable(GameObject root, PlayableGraph graph, List<Playable> outplayables)
		{
			ScriptPlayable<ActivationControlPlayable> activation = ActivationControlPlayable.Create(graph, root, this.postPlayback);
			if (activation.IsValid<ScriptPlayable<ActivationControlPlayable>>())
			{
				outplayables.Add(activation);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007654 File Offset: 0x00005854
		private void SearchHierarchyAndConnectParticleSystem(IEnumerable<ParticleSystem> particleSystems, PlayableGraph graph, List<Playable> outplayables)
		{
			foreach (ParticleSystem particleSystem in particleSystems)
			{
				if (particleSystem != null)
				{
					this.controllingParticles = true;
					outplayables.Add(ParticleControlPlayable.Create(graph, particleSystem, this.particleRandomSeed));
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000076C0 File Offset: 0x000058C0
		private void SearchHierarchyAndConnectDirector(IEnumerable<PlayableDirector> directors, PlayableGraph graph, List<Playable> outplayables, bool disableSelfReferences)
		{
			foreach (PlayableDirector director in directors)
			{
				if (director != null)
				{
					if (director.playableAsset != this.m_ControlDirectorAsset)
					{
						ScriptPlayable<DirectorControlPlayable> directorControlPlayable = DirectorControlPlayable.Create(graph, director);
						directorControlPlayable.GetBehaviour().pauseAction = this.directorOnClipEnd;
						outplayables.Add(directorControlPlayable);
						this.controllingDirectors = true;
					}
					else if (disableSelfReferences)
					{
						director.enabled = false;
					}
				}
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007758 File Offset: 0x00005958
		private static void SearchHierarchyAndConnectControlableScripts(IEnumerable<MonoBehaviour> controlableScripts, PlayableGraph graph, List<Playable> outplayables)
		{
			foreach (MonoBehaviour script in controlableScripts)
			{
				outplayables.Add(TimeControlPlayable.Create(graph, (ITimeControl)script));
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000077B0 File Offset: 0x000059B0
		private static void ConnectMixerAndPlayable(PlayableGraph graph, Playable mixer, Playable playable, int portIndex)
		{
			graph.Connect<Playable, Playable>(playable, 0, mixer, portIndex);
			mixer.SetInputWeight(playable, 1f);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000077CC File Offset: 0x000059CC
		internal IList<T> GetComponent<T>(GameObject gameObject)
		{
			List<T> components = new List<T>();
			if (gameObject != null)
			{
				if (this.searchHierarchy)
				{
					gameObject.GetComponentsInChildren<T>(true, components);
				}
				else
				{
					gameObject.GetComponents<T>(components);
				}
			}
			return components;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007802 File Offset: 0x00005A02
		internal static IEnumerable<MonoBehaviour> GetControlableScripts(GameObject root)
		{
			if (root == null)
			{
				yield break;
			}
			foreach (MonoBehaviour script in root.GetComponentsInChildren<MonoBehaviour>())
			{
				if (script is ITimeControl)
				{
					yield return script;
				}
			}
			MonoBehaviour[] array = null;
			yield break;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007814 File Offset: 0x00005A14
		internal void UpdateDurationAndLoopFlag(IList<PlayableDirector> directors, IList<ParticleSystem> particleSystems)
		{
			if (directors.Count == 0 && particleSystems.Count == 0)
			{
				return;
			}
			double maxDuration = double.NegativeInfinity;
			bool supportsLoop = false;
			foreach (PlayableDirector director in directors)
			{
				if (director.playableAsset != null)
				{
					double assetDuration = director.playableAsset.duration;
					if (director.playableAsset is TimelineAsset && assetDuration > 0.0)
					{
						assetDuration = (double)((DiscreteTime)assetDuration).OneTickAfter();
					}
					maxDuration = Math.Max(maxDuration, assetDuration);
					supportsLoop = supportsLoop || director.extrapolationMode == DirectorWrapMode.Loop;
				}
			}
			foreach (ParticleSystem particleSystem in particleSystems)
			{
				maxDuration = Math.Max(maxDuration, (double)particleSystem.main.duration);
				supportsLoop = supportsLoop || particleSystem.main.loop;
			}
			this.m_Duration = (double.IsNegativeInfinity(maxDuration) ? PlayableBinding.DefaultDuration : maxDuration);
			this.m_SupportLoop = supportsLoop;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000795C File Offset: 0x00005B5C
		private IList<ParticleSystem> GetControllableParticleSystems(GameObject go)
		{
			List<ParticleSystem> roots = new List<ParticleSystem>();
			if (this.searchHierarchy || go.GetComponent<ParticleSystem>() != null)
			{
				ControlPlayableAsset.GetControllableParticleSystems(go.transform, roots, ControlPlayableAsset.s_SubEmitterCollector);
				ControlPlayableAsset.s_SubEmitterCollector.Clear();
			}
			return roots;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000079A4 File Offset: 0x00005BA4
		private static void GetControllableParticleSystems(Transform t, ICollection<ParticleSystem> roots, HashSet<ParticleSystem> subEmitters)
		{
			ParticleSystem ps = t.GetComponent<ParticleSystem>();
			if (ps != null && !subEmitters.Contains(ps))
			{
				roots.Add(ps);
				ControlPlayableAsset.CacheSubEmitters(ps, subEmitters);
			}
			for (int i = 0; i < t.childCount; i++)
			{
				ControlPlayableAsset.GetControllableParticleSystems(t.GetChild(i), roots, subEmitters);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000079F8 File Offset: 0x00005BF8
		private static void CacheSubEmitters(ParticleSystem ps, HashSet<ParticleSystem> subEmitters)
		{
			if (ps == null)
			{
				return;
			}
			for (int i = 0; i < ps.subEmitters.subEmittersCount; i++)
			{
				subEmitters.Add(ps.subEmitters.GetSubEmitterSystem(i));
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007A40 File Offset: 0x00005C40
		public void GatherProperties(PlayableDirector director, IPropertyCollector driver)
		{
			if (director == null)
			{
				return;
			}
			if (ControlPlayableAsset.s_ProcessedDirectors.Contains(director))
			{
				return;
			}
			ControlPlayableAsset.s_ProcessedDirectors.Add(director);
			GameObject gameObject = this.sourceGameObject.Resolve(director);
			if (gameObject != null)
			{
				if (this.updateParticle)
				{
					ControlPlayableAsset.PreviewParticles(driver, gameObject.GetComponentsInChildren<ParticleSystem>(true));
				}
				if (this.active)
				{
					ControlPlayableAsset.PreviewActivation(driver, new GameObject[] { gameObject });
				}
				if (this.updateITimeControl)
				{
					ControlPlayableAsset.PreviewTimeControl(driver, director, ControlPlayableAsset.GetControlableScripts(gameObject));
				}
				if (this.updateDirector)
				{
					ControlPlayableAsset.PreviewDirectors(driver, this.GetComponent<PlayableDirector>(gameObject));
				}
			}
			ControlPlayableAsset.s_ProcessedDirectors.Remove(director);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007AEC File Offset: 0x00005CEC
		internal static void PreviewParticles(IPropertyCollector driver, IEnumerable<ParticleSystem> particles)
		{
			foreach (ParticleSystem ps in particles)
			{
				driver.AddFromName<ParticleSystem>(ps.gameObject, "randomSeed");
				driver.AddFromName<ParticleSystem>(ps.gameObject, "autoRandomSeed");
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00007B50 File Offset: 0x00005D50
		internal static void PreviewActivation(IPropertyCollector driver, IEnumerable<GameObject> objects)
		{
			foreach (GameObject gameObject in objects)
			{
				driver.AddFromName(gameObject, "m_IsActive");
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007BA0 File Offset: 0x00005DA0
		internal static void PreviewTimeControl(IPropertyCollector driver, PlayableDirector director, IEnumerable<MonoBehaviour> scripts)
		{
			foreach (MonoBehaviour script in scripts)
			{
				IPropertyPreview propertyPreview = script as IPropertyPreview;
				if (propertyPreview != null)
				{
					propertyPreview.GatherProperties(director, driver);
				}
				else
				{
					driver.AddFromComponent(script.gameObject, script);
				}
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007C04 File Offset: 0x00005E04
		internal static void PreviewDirectors(IPropertyCollector driver, IEnumerable<PlayableDirector> directors)
		{
			foreach (PlayableDirector childDirector in directors)
			{
				if (!(childDirector == null))
				{
					TimelineAsset timeline = childDirector.playableAsset as TimelineAsset;
					if (!(timeline == null))
					{
						timeline.GatherProperties(childDirector, driver);
					}
				}
			}
		}

		// Token: 0x040000EE RID: 238
		private const int k_MaxRandInt = 10000;

		// Token: 0x040000EF RID: 239
		private static readonly List<PlayableDirector> k_EmptyDirectorsList = new List<PlayableDirector>(0);

		// Token: 0x040000F0 RID: 240
		private static readonly List<ParticleSystem> k_EmptyParticlesList = new List<ParticleSystem>(0);

		// Token: 0x040000F1 RID: 241
		private static readonly HashSet<ParticleSystem> s_SubEmitterCollector = new HashSet<ParticleSystem>();

		// Token: 0x040000F2 RID: 242
		[SerializeField]
		public ExposedReference<GameObject> sourceGameObject;

		// Token: 0x040000F3 RID: 243
		[SerializeField]
		public GameObject prefabGameObject;

		// Token: 0x040000F4 RID: 244
		[SerializeField]
		public bool updateParticle = true;

		// Token: 0x040000F5 RID: 245
		[SerializeField]
		public uint particleRandomSeed;

		// Token: 0x040000F6 RID: 246
		[SerializeField]
		public bool updateDirector = true;

		// Token: 0x040000F7 RID: 247
		[SerializeField]
		public bool updateITimeControl = true;

		// Token: 0x040000F8 RID: 248
		[SerializeField]
		public bool searchHierarchy;

		// Token: 0x040000F9 RID: 249
		[SerializeField]
		public bool active = true;

		// Token: 0x040000FA RID: 250
		[SerializeField]
		public ActivationControlPlayable.PostPlaybackState postPlayback = ActivationControlPlayable.PostPlaybackState.Revert;

		// Token: 0x040000FB RID: 251
		[SerializeField]
		public DirectorControlPlayable.PauseAction directorOnClipEnd;

		// Token: 0x040000FC RID: 252
		private PlayableAsset m_ControlDirectorAsset;

		// Token: 0x040000FD RID: 253
		private double m_Duration = PlayableBinding.DefaultDuration;

		// Token: 0x040000FE RID: 254
		private bool m_SupportLoop;

		// Token: 0x040000FF RID: 255
		private static HashSet<PlayableDirector> s_ProcessedDirectors = new HashSet<PlayableDirector>();

		// Token: 0x04000100 RID: 256
		private static HashSet<GameObject> s_CreatedPrefabs = new HashSet<GameObject>();
	}
}
