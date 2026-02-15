using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x0200007D RID: 125
	public class WaitForSpineTrackEntryEnd : IEnumerator
	{
		// Token: 0x06000385 RID: 901 RVA: 0x00013C6C File Offset: 0x00011E6C
		public WaitForSpineTrackEntryEnd(TrackEntry trackEntry)
		{
			this.SafeSubscribe(trackEntry);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00013C7B File Offset: 0x00011E7B
		private void HandleEnd(TrackEntry trackEntry)
		{
			this.m_WasFired = true;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00013C84 File Offset: 0x00011E84
		private void SafeSubscribe(TrackEntry trackEntry)
		{
			if (trackEntry == null)
			{
				Debug.LogWarning("TrackEntry was null. Coroutine will continue immediately.");
				this.m_WasFired = true;
				return;
			}
			trackEntry.End += this.HandleEnd;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00013CAD File Offset: 0x00011EAD
		public WaitForSpineTrackEntryEnd NowWaitFor(TrackEntry trackEntry)
		{
			this.SafeSubscribe(trackEntry);
			return this;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00013CB7 File Offset: 0x00011EB7
		bool IEnumerator.MoveNext()
		{
			if (this.m_WasFired)
			{
				((IEnumerator)this).Reset();
				return false;
			}
			return true;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00013CCA File Offset: 0x00011ECA
		void IEnumerator.Reset()
		{
			this.m_WasFired = false;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0001394C File Offset: 0x00011B4C
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000237 RID: 567
		private bool m_WasFired;
	}
}
