using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.Network;

namespace YgomGame.Menu
{
	// Token: 0x02000ADA RID: 2778
	public abstract class PvpMenuMatchingBase : MonoBehaviour
	{
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060050FE RID: 20734 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060050FF RID: 20735 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsComplete
		{
			get
			{
				return false;
			}
			protected set
			{
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06005100 RID: 20736 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsRequestCancel
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06005101 RID: 20737 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005102 RID: 20738 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsStartMatching
		{
			get
			{
				return false;
			}
			protected set
			{
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06005103 RID: 20739 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005104 RID: 20740 RVA: 0x0000216D File Offset: 0x0000036D
		public int ErrorCode
		{
			get
			{
				return 0;
			}
			protected set
			{
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06005105 RID: 20741 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005106 RID: 20742 RVA: 0x0000216D File Offset: 0x0000036D
		public int CancelErrorCode
		{
			get
			{
				return 0;
			}
			protected set
			{
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06005107 RID: 20743 RVA: 0x0000216A File Offset: 0x0000036A
		public Handle Handle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005108 RID: 20744 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IEnumerator yWaitPrevious(float time)
		{
			return null;
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void StartMatching(Dictionary<string, object> param)
		{
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool DispProgress()
		{
			return false;
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual int ProgressCount()
		{
			return 0;
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IEnumerator yWaitMatching(Action callback)
		{
			return null;
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IEnumerator yCancelMatching()
		{
			return null;
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void SetBootDuelParam(ref Dictionary<string, object> param)
		{
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void AbortMatching()
		{
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual Handle CallAPIMatching(Dictionary<string, object> matchParam)
		{
			return null;
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnCompleteMatching(Handle e)
		{
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnCompleteMatchingHandle(Handle e)
		{
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnErrorMatching(Handle e)
		{
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x04008F84 RID: 36740
		private bool m_bCompleteMatching;

		// Token: 0x04008F85 RID: 36741
		private bool m_bRequestCancel;

		// Token: 0x04008F86 RID: 36742
		private bool m_bStartMatching;

		// Token: 0x04008F87 RID: 36743
		private int m_ErrorCode;

		// Token: 0x04008F88 RID: 36744
		private int m_CancelErrorCode;

		// Token: 0x04008F89 RID: 36745
		protected Handle m_Handle;

		// Token: 0x04008F8A RID: 36746
		protected Dictionary<string, object> m_MatchParam;
	}
}
