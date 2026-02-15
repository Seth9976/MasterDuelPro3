using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace Willow
{
	// Token: 0x02001549 RID: 5449
	public class CustomTimelineController : TimelineController
	{
		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x06009DE4 RID: 40420 RVA: 0x0000216A File Offset: 0x0000036A
		private Transform[] children
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x06009DE5 RID: 40421 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06009DE6 RID: 40422 RVA: 0x0000216D File Offset: 0x0000036D
		public CinemachineVirtualCamera mainVcam
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x06009DE7 RID: 40423 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009DE8 RID: 40424 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isCharaSamePotision
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x06009DE9 RID: 40425 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009DEA RID: 40426 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isFinishOff
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x06009DEB RID: 40427 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009DEC RID: 40428 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isSoundSeOff
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x06009DED RID: 40429 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06009DEE RID: 40430 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<GameObject> callbackInstantiatePrefab
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x06009DEF RID: 40431 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06009DF0 RID: 40432 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<GameObject, string> callbackInstantiatePrefabWithParentName
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x06009DF1 RID: 40433 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06009DF2 RID: 40434 RVA: 0x0000216D File Offset: 0x0000036D
		public float perticleLifespan
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x06009DF3 RID: 40435 RVA: 0x0000216A File Offset: 0x0000036A
		public List<GameObject> listCleanUp
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x06009DF4 RID: 40436 RVA: 0x0019B51B File Offset: 0x0019971B
		public TimelineReplacer checkReplacer
		{
			get
			{
				return this.m_replacer;
			}
		}

		// Token: 0x06009DF5 RID: 40437 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06009DF6 RID: 40438 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Start()
		{
		}

		// Token: 0x06009DF7 RID: 40439 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void LateUpdate()
		{
		}

		// Token: 0x06009DF8 RID: 40440 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06009DF9 RID: 40441 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnDestroy()
		{
		}

		// Token: 0x06009DFA RID: 40442 RVA: 0x0000216D File Offset: 0x0000036D
		private void CleanUp()
		{
		}

		// Token: 0x06009DFB RID: 40443 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Finish(bool isEndCall = true)
		{
		}

		// Token: 0x06009DFC RID: 40444 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetPosition()
		{
		}

		// Token: 0x06009DFD RID: 40445 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBindObject()
		{
		}

		// Token: 0x06009DFE RID: 40446 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBindObject(GameObject bindChara, CinemachineBrain camera = null, Transform moveTarget = null, Transform moveStart = null, Transform moveEnd = null)
		{
		}

		// Token: 0x06009DFF RID: 40447 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Play(Action endCall, float speed = 1f, double startTimePosition = 0.0)
		{
		}

		// Token: 0x06009E00 RID: 40448 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayBlendVcam(Action endCall, float speed = 1f, double startTimePosition = 0.0)
		{
		}

		// Token: 0x06009E01 RID: 40449 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Stop(bool isEndTime = true, bool isEndCall = true)
		{
		}

		// Token: 0x06009E02 RID: 40450 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetReplacePrefab(string parentObjectName, GameObject replacePrefab)
		{
		}

		// Token: 0x06009E03 RID: 40451 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject GetReplacePrefab(string parentObjectName)
		{
			return null;
		}

		// Token: 0x06009E04 RID: 40452 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearReplacePrefab()
		{
		}

		// Token: 0x06009E05 RID: 40453 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSoundSe()
		{
		}

		// Token: 0x06009E06 RID: 40454 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopSoundSe(float fadeTime = 1f)
		{
		}

		// Token: 0x0400DDA6 RID: 56742
		[SerializeField]
		private CinemachineVirtualCamera m_vcam;

		// Token: 0x0400DDA7 RID: 56743
		[SerializeField]
		private TimelineReplacer m_replacer;

		// Token: 0x0400DDA8 RID: 56744
		[SerializeField]
		private CinemachineBrain m_camera;

		// Token: 0x0400DDA9 RID: 56745
		[SerializeField]
		private GameObject m_chara;

		// Token: 0x0400DDAA RID: 56746
		[SerializeField]
		private Transform m_moveTarget;

		// Token: 0x0400DDAB RID: 56747
		[SerializeField]
		private Transform m_moveStart;

		// Token: 0x0400DDAC RID: 56748
		[SerializeField]
		private Transform m_moveEnd;

		// Token: 0x0400DDAD RID: 56749
		private bool m_init;

		// Token: 0x0400DDAE RID: 56750
		private Transform[] m_children;

		// Token: 0x0400DDAF RID: 56751
		private Tween m_tween;

		// Token: 0x0400DDB0 RID: 56752
		private Tween m_tweenFinish;

		// Token: 0x0400DDB1 RID: 56753
		private List<int> m_soundIds;

		// Token: 0x0400DDB2 RID: 56754
		private float m_perticleLifespan;

		// Token: 0x0400DDB3 RID: 56755
		private List<GameObject> m_listCleanUp;

		// Token: 0x0400DDB4 RID: 56756
		private Dictionary<string, GameObject> m_dicReplacePrefab;

		// Token: 0x0400DDB5 RID: 56757
		private List<CustomTimelineController.TaskSe> m_listTaskSe;

		// Token: 0x0200154A RID: 5450
		[Serializable]
		public class DataReferenceTarget
		{
			// Token: 0x06009E08 RID: 40456 RVA: 0x00002739 File Offset: 0x00000939
			public DataReferenceTarget(PropertyName prop, string targetName, GameObject targetObject = null)
			{
			}

			// Token: 0x0400DDB6 RID: 56758
			public int m_exposedNameHashCode;

			// Token: 0x0400DDB7 RID: 56759
			public string m_targetName;

			// Token: 0x0400DDB8 RID: 56760
			public GameObject m_targetObject;
		}

		// Token: 0x0200154B RID: 5451
		private class TaskSe
		{
			// Token: 0x170014A9 RID: 5289
			// (get) Token: 0x06009E09 RID: 40457 RVA: 0x000029C5 File Offset: 0x00000BC5
			// (set) Token: 0x06009E0A RID: 40458 RVA: 0x0000216D File Offset: 0x0000036D
			public float time
			{
				[CompilerGenerated]
				get
				{
					return 0f;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x170014AA RID: 5290
			// (get) Token: 0x06009E0B RID: 40459 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06009E0C RID: 40460 RVA: 0x0000216D File Offset: 0x0000036D
			public string key
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x06009E0D RID: 40461 RVA: 0x00002739 File Offset: 0x00000939
			public TaskSe()
			{
			}

			// Token: 0x06009E0E RID: 40462 RVA: 0x00002739 File Offset: 0x00000939
			public TaskSe(float time, string key)
			{
			}
		}
	}
}
