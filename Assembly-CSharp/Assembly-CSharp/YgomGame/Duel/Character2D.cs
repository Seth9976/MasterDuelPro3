using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000D18 RID: 3352
	public class Character2D : MonoBehaviour
	{
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x060060C4 RID: 24772 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsInitialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x060060C5 RID: 24773 RVA: 0x0000216A File Offset: 0x0000036A
		public Character character
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x060060C6 RID: 24774 RVA: 0x0000216A File Offset: 0x0000036A
		public Animator animator
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x060060C7 RID: 24775 RVA: 0x0000216A File Offset: 0x0000036A
		private static GameObject mateCreateLocator
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060060C8 RID: 24776 RVA: 0x0000216D File Offset: 0x0000036D
		private static void Create(Transform parent, Action<Character2D> onFinish = null)
		{
		}

		// Token: 0x060060C9 RID: 24777 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(int avatarId, Transform parent, Vector3 modelPos, Vector3 modelRot, Vector3 modelScale, Vector3 camPos, Vector3 camRot, int renderTexW = 256, int renderTexH = 256, Action<Character2D> onFinish = null, bool enblePostEffect = false, float imgW = -1f, float imgH = -1f)
		{
		}

		// Token: 0x060060CA RID: 24778 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(int avatarId, Transform parent, Vector3 modelPos, Vector3 modelRot, Vector3 modelScale, int renderTexW = 256, int renderTexH = 256, Action<Character2D> onFinish = null, bool enblePostEffect = false, float imgW = -1f, float imgH = -1f)
		{
		}

		// Token: 0x060060CB RID: 24779 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(int avatarId, Vector3 modelPos, Vector3 modelRot, Vector3 modelScale, Vector3 camPos, Vector3 camRot, int renderTexW = 256, int renderTexH = 256, Action onFinish = null, bool enblePostEffect = false, float imgW = -1f, float imgH = -1f)
		{
		}

		// Token: 0x060060CC RID: 24780 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator OnFinishCoroutine(int id, Action onFinish = null)
		{
			return null;
		}

		// Token: 0x060060CD RID: 24781 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject GetModelInstance()
		{
			return null;
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayMotion(AvatarMotionSetting.MotionID motion)
		{
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool HasMotion(AvatarMotionSetting.MotionID motion)
		{
			return false;
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopMotionSe(float fade = -1f)
		{
		}

		// Token: 0x060060D1 RID: 24785 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEnableSe(bool flg)
		{
		}

		// Token: 0x060060D2 RID: 24786 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDestroy()
		{
		}

		// Token: 0x060060D3 RID: 24787 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAutoCameraOnOff(bool flg)
		{
		}

		// Token: 0x04009C14 RID: 39956
		[SerializeField]
		public RawImage rawImg;

		// Token: 0x04009C15 RID: 39957
		private static readonly string Character2DPrefabPath;

		// Token: 0x04009C16 RID: 39958
		private bool isInitialized;

		// Token: 0x04009C17 RID: 39959
		private bool IsDestroyed;

		// Token: 0x04009C18 RID: 39960
		private Character chara;

		// Token: 0x04009C19 RID: 39961
		private int renderTextureId;

		// Token: 0x04009C1A RID: 39962
		private bool autoCameraOnOff;

		// Token: 0x04009C1B RID: 39963
		private Coroutine soundCheckCroutine;

		// Token: 0x04009C1C RID: 39964
		private static GameObject mateCreateLocatorCache;
	}
}
