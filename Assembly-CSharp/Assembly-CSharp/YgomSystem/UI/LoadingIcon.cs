using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005A5 RID: 1445
	[DisallowMultipleComponent]
	public class LoadingIcon : MonoBehaviour
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject loadingCoverLocator
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x0000216D File Offset: 0x0000036D
		public void AssignHandler(ILoadingIconHandler handler)
		{
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000F201A File Offset: 0x000F021A
		private void GetHandlersState(out bool completed, out bool visible)
		{
			completed = false;
			visible = false;
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator ySequence()
		{
			return null;
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSourceChanged()
		{
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x0000216D File Offset: 0x0000036D
		private void Terminate()
		{
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayRoutines(IEnumerator[] routines)
		{
			return null;
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayShowLoadingIcon()
		{
			return null;
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayHideLoadingIcon()
		{
			return null;
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayShowLoadingCover()
		{
			return null;
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayHideCoverIcon()
		{
			return null;
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayLoadStartTarget()
		{
			return null;
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayLoadEndTarget()
		{
			return null;
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x0000216D File Offset: 0x0000036D
		private void TerminateTweenTarget()
		{
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x0000216D File Offset: 0x0000036D
		private void TweenStopLabel(GameObject target, bool includeChildren = false, params string[] labels)
		{
		}

		// Token: 0x04002B84 RID: 11140
		[SerializeField]
		private GameObject[] m_HandleTargets;

		// Token: 0x04002B85 RID: 11141
		private List<ILoadingIconHandler> m_Handlers;

		// Token: 0x04002B86 RID: 11142
		[SerializeField]
		private GameObject m_LoadingIconPref;

		// Token: 0x04002B87 RID: 11143
		[SerializeField]
		private bool m_LoadingIconStretch;

		// Token: 0x04002B88 RID: 11144
		[SerializeField]
		private GameObject m_LoadingCoverPref;

		// Token: 0x04002B89 RID: 11145
		[SerializeField]
		private GameObject m_LoadingCoverLocator;

		// Token: 0x04002B8A RID: 11146
		[SerializeField]
		private List<GameObject> m_LoadingTweenTargets;

		// Token: 0x04002B8B RID: 11147
		private GameObject m_LoadingIcon;

		// Token: 0x04002B8C RID: 11148
		private GameObject m_LoadingCover;

		// Token: 0x04002B8D RID: 11149
		private IEnumerator m_Sequence;
	}
}
