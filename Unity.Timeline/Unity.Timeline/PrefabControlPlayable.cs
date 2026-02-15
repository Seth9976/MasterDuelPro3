using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000058 RID: 88
	public class PrefabControlPlayable : PlayableBehaviour
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x00009D48 File Offset: 0x00007F48
		public static ScriptPlayable<PrefabControlPlayable> Create(PlayableGraph graph, GameObject prefabGameObject, Transform parentTransform)
		{
			if (prefabGameObject == null)
			{
				return ScriptPlayable<PrefabControlPlayable>.Null;
			}
			ScriptPlayable<PrefabControlPlayable> handle = ScriptPlayable<PrefabControlPlayable>.Create(graph, 0);
			handle.GetBehaviour().Initialize(prefabGameObject, parentTransform);
			return handle;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00009D7C File Offset: 0x00007F7C
		public GameObject prefabInstance
		{
			get
			{
				return this.m_Instance;
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009D84 File Offset: 0x00007F84
		public GameObject Initialize(GameObject prefabGameObject, Transform parentTransform)
		{
			if (prefabGameObject == null)
			{
				throw new ArgumentNullException("Prefab cannot be null");
			}
			if (this.m_Instance != null)
			{
				Debug.LogWarningFormat("Prefab Control Playable ({0}) has already been initialized with a Prefab ({1}).", new object[]
				{
					prefabGameObject.name,
					this.m_Instance.name
				});
			}
			else
			{
				this.m_Instance = Object.Instantiate<GameObject>(prefabGameObject, parentTransform, false);
				this.m_Instance.name = prefabGameObject.name + " [Timeline]";
				this.m_Instance.SetActive(false);
				PrefabControlPlayable.SetHideFlagsRecursive(this.m_Instance);
			}
			return this.m_Instance;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00009E22 File Offset: 0x00008022
		public override void OnPlayableDestroy(Playable playable)
		{
			if (this.m_Instance)
			{
				if (Application.isPlaying)
				{
					Object.Destroy(this.m_Instance);
					return;
				}
				Object.DestroyImmediate(this.m_Instance);
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00009E4F File Offset: 0x0000804F
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			if (this.m_Instance == null)
			{
				return;
			}
			this.m_Instance.SetActive(true);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00009E6C File Offset: 0x0000806C
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.m_Instance != null && info.effectivePlayState == PlayState.Paused)
			{
				this.m_Instance.SetActive(false);
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00009E94 File Offset: 0x00008094
		private static void SetHideFlagsRecursive(GameObject gameObject)
		{
			if (gameObject == null)
			{
				return;
			}
			gameObject.hideFlags = HideFlags.DontSaveInEditor | HideFlags.DontSaveInBuild;
			if (!Application.isPlaying)
			{
				gameObject.hideFlags |= HideFlags.HideInHierarchy;
			}
			foreach (object obj in gameObject.transform)
			{
				PrefabControlPlayable.SetHideFlagsRecursive(((Transform)obj).gameObject);
			}
		}

		// Token: 0x0400014F RID: 335
		private GameObject m_Instance;
	}
}
