using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Duel;
using YgomSystem.UI;
using YgomSystem.Utility;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B1E RID: 2846
	[DisallowMultipleComponent]
	public class BindingMateRenderTexture : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x060052C9 RID: 21193 RVA: 0x0000216A File Offset: 0x0000036A
		public string mateTransformSettingPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x060052CA RID: 21194 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060052CB RID: 21195 RVA: 0x0000216D File Offset: 0x0000036D
		public int mateId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x060052CC RID: 21196 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool validMateId
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x060052CD RID: 21197 RVA: 0x0000216A File Offset: 0x0000036A
		public Character character
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x060052CE RID: 21198 RVA: 0x0000216A File Offset: 0x0000036A
		public RawImage rawImage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x060052CF RID: 21199 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060052D0 RID: 21200 RVA: 0x0000216D File Offset: 0x0000036D
		public bool visible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x060052D1 RID: 21201 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060052D2 RID: 21202 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onReloadEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x060052D3 RID: 21203 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x0000216D File Offset: 0x0000036D
		private void VisibleRefresh()
		{
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x0000216D File Offset: 0x0000036D
		public void SourceChanged()
		{
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnRebind()
		{
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yOnBindingRoutine()
		{
			return null;
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x040090E3 RID: 37091
		[SerializeField]
		private int m_MateId;

		// Token: 0x040090E4 RID: 37092
		[SerializeField]
		private Vector3 m_Position;

		// Token: 0x040090E5 RID: 37093
		[SerializeField]
		private Vector3 m_Rotation;

		// Token: 0x040090E6 RID: 37094
		[SerializeField]
		private Vector3 m_Scale;

		// Token: 0x040090E7 RID: 37095
		[SerializeField]
		[AssetPath]
		private string m_MateTransformSettingPath;

		// Token: 0x040090E8 RID: 37096
		[SerializeField]
		private bool m_UseTransformSetting;

		// Token: 0x040090E9 RID: 37097
		[SerializeField]
		private GameObject m_Locater;

		// Token: 0x040090EA RID: 37098
		[SerializeField]
		private AvatarMotionSetting.MotionID m_BeginMotion;

		// Token: 0x040090EB RID: 37099
		private GameObject m_RenderTextureTarget;

		// Token: 0x040090EC RID: 37100
		private int m_RenderTextureTargetId;

		// Token: 0x040090ED RID: 37101
		private uint m_CharacterPrefCrc;

		// Token: 0x040090EE RID: 37102
		private Character m_Character;

		// Token: 0x040090EF RID: 37103
		private uint m_SettingCrc;

		// Token: 0x040090F0 RID: 37104
		private RawImage m_RawImage;

		// Token: 0x040090F1 RID: 37105
		private bool m_Visible;

		// Token: 0x040090F2 RID: 37106
		private IEnumerator m_OnBindingRoutine;
	}
}
