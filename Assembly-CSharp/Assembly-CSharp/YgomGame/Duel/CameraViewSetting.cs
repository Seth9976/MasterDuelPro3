using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CB1 RID: 3249
	public class CameraViewSetting : ScriptableObject
	{
		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06005C84 RID: 23684 RVA: 0x0000216A File Offset: 0x0000036A
		private static CameraViewSetting instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005C85 RID: 23685 RVA: 0x0000216A File Offset: 0x0000036A
		public CameraViewSetting.ViewInfo GetInfo(string label)
		{
			return null;
		}

		// Token: 0x06005C86 RID: 23686 RVA: 0x0000216A File Offset: 0x0000036A
		public static CameraViewSetting.ViewInfo GetViewInfo(string label)
		{
			return null;
		}

		// Token: 0x04009825 RID: 38949
		public List<CameraViewSetting.ViewInfo> infoList;

		// Token: 0x04009826 RID: 38950
		public static CameraViewSetting _instance;

		// Token: 0x02000CB2 RID: 3250
		[Serializable]
		public class ViewInfo
		{
			// Token: 0x170009C9 RID: 2505
			// (get) Token: 0x06005C88 RID: 23688 RVA: 0x000F4FC4 File Offset: 0x000F31C4
			public Quaternion rotation
			{
				get
				{
					return default(Quaternion);
				}
			}

			// Token: 0x06005C89 RID: 23689 RVA: 0x0000216A File Offset: 0x0000036A
			public CameraViewSetting.ViewInfo Copy()
			{
				return null;
			}

			// Token: 0x04009827 RID: 38951
			public string label;

			// Token: 0x04009828 RID: 38952
			public Vector3 position;

			// Token: 0x04009829 RID: 38953
			public Vector3 angle;

			// Token: 0x0400982A RID: 38954
			public float fieldOfView;

			// Token: 0x0400982B RID: 38955
			public float nearClip;

			// Token: 0x0400982C RID: 38956
			public float farClip;
		}
	}
}
