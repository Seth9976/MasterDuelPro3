using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.Network;

namespace YgomGame.Menu
{
	// Token: 0x02000A6D RID: 2669
	public abstract class DuelStartWaitingBase : MonoBehaviour
	{
		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06004DDB RID: 19931 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsComplete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06004DDC RID: 19932 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004DDD RID: 19933 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06004DDE RID: 19934 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<string, object> Result
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IEnumerator yWaitPrevious(float time)
		{
			return null;
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void StartWaiting()
		{
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool DispProgress()
		{
			return false;
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual int ProgressCount()
		{
			return 0;
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IEnumerator yWaitWaiting(Action callback)
		{
			return null;
		}

		// Token: 0x06004DE4 RID: 19940 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnCompleteWaiting(Handle e)
		{
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnErrorWaiting(Handle e)
		{
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x04008BB1 RID: 35761
		private bool m_bComplete;

		// Token: 0x04008BB2 RID: 35762
		private int m_ErrorCode;

		// Token: 0x04008BB3 RID: 35763
		protected Handle m_Handle;

		// Token: 0x04008BB4 RID: 35764
		protected Dictionary<string, object> m_Result;

		// Token: 0x04008BB5 RID: 35765
		protected Dictionary<string, object> m_Param;
	}
}
