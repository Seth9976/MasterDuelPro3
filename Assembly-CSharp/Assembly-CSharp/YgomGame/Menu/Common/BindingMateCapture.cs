using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;
using YgomSystem.Utility;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B1D RID: 2845
	public class BindingMateCapture : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060052B8 RID: 21176 RVA: 0x0000216A File Offset: 0x0000036A
		public string mateTransformSettingPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060052B9 RID: 21177 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool validSource
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x060052BA RID: 21178 RVA: 0x0000216A File Offset: 0x0000036A
		public RawImage rawImage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x060052BB RID: 21179 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060052BC RID: 21180 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x060052BD RID: 21181 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060052BE RID: 21182 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x060052BF RID: 21183 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060052C1 RID: 21185 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x0000216D File Offset: 0x0000036D
		private void VisibleRefresh()
		{
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x0000216D File Offset: 0x0000036D
		public void SourceChanged()
		{
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnRebind()
		{
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yOnBindingRoutine()
		{
			return null;
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x040090D9 RID: 37081
		[SerializeField]
		private int m_MateId;

		// Token: 0x040090DA RID: 37082
		[SerializeField]
		private Vector3 m_Position;

		// Token: 0x040090DB RID: 37083
		[SerializeField]
		private Vector3 m_Rotation;

		// Token: 0x040090DC RID: 37084
		[SerializeField]
		private Vector3 m_Scale;

		// Token: 0x040090DD RID: 37085
		[SerializeField]
		[AssetPath]
		private string m_MateTransformSettingPath;

		// Token: 0x040090DE RID: 37086
		[SerializeField]
		private bool m_UseTransformSetting;

		// Token: 0x040090DF RID: 37087
		[SerializeField]
		private GameObject m_Locater;

		// Token: 0x040090E0 RID: 37088
		private RawImage m_RawImageCache;

		// Token: 0x040090E1 RID: 37089
		private bool m_Visible;

		// Token: 0x040090E2 RID: 37090
		private IEnumerator m_OnBindingRoutine;
	}
}
