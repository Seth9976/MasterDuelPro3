using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Duel;
using YgomSystem.UI;
using YgomSystem.Utility;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B1C RID: 2844
	[DisallowMultipleComponent]
	public class BindingMate3D : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x060052A5 RID: 21157 RVA: 0x0000216A File Offset: 0x0000036A
		public string mateTransformSettingPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060052A6 RID: 21158 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060052A7 RID: 21159 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060052A8 RID: 21160 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool validMateId
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060052A9 RID: 21161 RVA: 0x0000216A File Offset: 0x0000036A
		public Character character
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060052AA RID: 21162 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060052AB RID: 21163 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x060052AC RID: 21164 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060052AD RID: 21165 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x060052AE RID: 21166 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x0000216D File Offset: 0x0000036D
		private void VisibleRefresh()
		{
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x0000216D File Offset: 0x0000036D
		public void SourceChanged()
		{
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnRebind()
		{
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yOnBindingRoutine()
		{
			return null;
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x040090CC RID: 37068
		[SerializeField]
		private int m_MateId;

		// Token: 0x040090CD RID: 37069
		[SerializeField]
		private Vector3 m_Position;

		// Token: 0x040090CE RID: 37070
		[SerializeField]
		private Vector3 m_Rotation;

		// Token: 0x040090CF RID: 37071
		[SerializeField]
		private Vector3 m_Scale;

		// Token: 0x040090D0 RID: 37072
		[AssetPath]
		[SerializeField]
		private string m_MateTransformSettingPath;

		// Token: 0x040090D1 RID: 37073
		[SerializeField]
		private bool m_UseTransformSetting;

		// Token: 0x040090D2 RID: 37074
		[SerializeField]
		private GameObject m_Locater;

		// Token: 0x040090D3 RID: 37075
		[SerializeField]
		private AvatarMotionSetting.MotionID m_BeginMotion;

		// Token: 0x040090D4 RID: 37076
		private uint m_CharacterPrefCrc;

		// Token: 0x040090D5 RID: 37077
		private Character m_Character;

		// Token: 0x040090D6 RID: 37078
		private uint m_SettingCrc;

		// Token: 0x040090D7 RID: 37079
		private bool m_Visible;

		// Token: 0x040090D8 RID: 37080
		private IEnumerator m_OnBindingRoutine;
	}
}
