using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Willow
{
	// Token: 0x02001554 RID: 5460
	[Serializable]
	public class TimelineReplacer
	{
		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x06009E88 RID: 40584 RVA: 0x0000216A File Offset: 0x0000036A
		private string[] monsterHipsNameList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06009E89 RID: 40585 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsValid()
		{
			return false;
		}

		// Token: 0x06009E8A RID: 40586 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetBind<T>(string trackName, T obj) where T : global::UnityEngine.Object
		{
		}

		// Token: 0x06009E8B RID: 40587 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetBind<T>(int id, T obj) where T : global::UnityEngine.Object
		{
		}

		// Token: 0x06009E8C RID: 40588 RVA: 0x0000216D File Offset: 0x0000036D
		public void Set(GameObject bindTarget = null, Transform moveTarget = null, Transform start = null, Transform end = null)
		{
		}

		// Token: 0x0400DDFF RID: 56831
		public PlayableDirector m_currentDirector;

		// Token: 0x0400DE00 RID: 56832
		public TimelineReplacer.BindTrackInfo[] m_bindTrackInfo;

		// Token: 0x0400DE01 RID: 56833
		public string[] m_bindTransformTweenTrackName;

		// Token: 0x0400DE02 RID: 56834
		public CustomTimelineController.DataReferenceTarget[] m_listReferenceTarget;

		// Token: 0x0400DE03 RID: 56835
		public CustomTimelineController.DataReferenceTarget[] m_listReferenceTransformTweenClipStart;

		// Token: 0x0400DE04 RID: 56836
		public CustomTimelineController.DataReferenceTarget[] m_listReferenceTransformTweenClipEnd;

		// Token: 0x0400DE05 RID: 56837
		private global::UnityEngine.Object[] m_asset;

		// Token: 0x0400DE06 RID: 56838
		private IEnumerable<PlayableBinding> m_bindingAll;

		// Token: 0x02001555 RID: 5461
		[Serializable]
		public class BindTrackInfo
		{
			// Token: 0x06009E8E RID: 40590 RVA: 0x00002739 File Offset: 0x00000939
			public BindTrackInfo(int idx, string type, string name)
			{
			}

			// Token: 0x0400DE07 RID: 56839
			public int m_index;

			// Token: 0x0400DE08 RID: 56840
			public string m_type;

			// Token: 0x0400DE09 RID: 56841
			public string m_name;
		}
	}
}
