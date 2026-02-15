using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B19 RID: 2841
	public class BindingImageOrPrefab : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06005282 RID: 21122 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005283 RID: 21123 RVA: 0x0000216D File Offset: 0x0000036D
		public string path
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06005284 RID: 21124 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005285 RID: 21125 RVA: 0x0000216D File Offset: 0x0000036D
		public AspectRatioFitter.AspectMode imageAspectMode
		{
			get
			{
				return AspectRatioFitter.AspectMode.None;
			}
			set
			{
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06005286 RID: 21126 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005287 RID: 21127 RVA: 0x0000216D File Offset: 0x0000036D
		public BindingGameObjectEx.FitMode prefabFitMode
		{
			get
			{
				return BindingGameObjectEx.FitMode.NONE;
			}
			set
			{
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06005288 RID: 21128 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool visible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06005289 RID: 21129 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool handleOnFailed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (set) Token: 0x0600528A RID: 21130 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<BindingImageOrPrefab> handleOnFailedCallback
		{
			set
			{
			}
		}

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x0600528B RID: 21131 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600528C RID: 21132 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x0600528D RID: 21133 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600528F RID: 21135 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator ySequence()
		{
			return null;
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x0000216D File Offset: 0x0000036D
		private void SwitchTargetActivate(bool isPref)
		{
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x0000216D File Offset: 0x0000036D
		private void SourceChange()
		{
		}

		// Token: 0x040090B6 RID: 37046
		[SerializeField]
		private string m_Path;

		// Token: 0x040090B7 RID: 37047
		[SerializeField]
		private AspectRatioFitter.AspectMode m_ImageAspectMode;

		// Token: 0x040090B8 RID: 37048
		[SerializeField]
		private BindingGameObjectEx.FitMode m_PrefabFitMode;

		// Token: 0x040090B9 RID: 37049
		private uint m_Crc;

		// Token: 0x040090BA RID: 37050
		private IEnumerator m_SequenceRoutine;

		// Token: 0x040090BB RID: 37051
		private Action<BindingImageOrPrefab> m_HandleOnFailedCallback;
	}
}
