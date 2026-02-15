using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B4A RID: 2890
	public class MateTransformSetting : ScriptableObject
	{
		// Token: 0x060053CD RID: 21453 RVA: 0x0000216A File Offset: 0x0000036A
		public MateTransformSetting.Data FindByMateId(int mateId)
		{
			return null;
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TryApplyTransform(int mateId, Transform target)
		{
			return false;
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x0000216D File Offset: 0x0000036D
		public void SubmitData(MateTransformSetting.Data from)
		{
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSetting(MateTransformSetting.Data mateSetting)
		{
		}

		// Token: 0x0400914F RID: 37199
		[SerializeField]
		private List<MateTransformSetting.Data> m_Datas;

		// Token: 0x04009150 RID: 37200
		[SerializeField]
		private List<MateTransformSetting> m_Fallbacks;

		// Token: 0x02000B4B RID: 2891
		[Serializable]
		public class Data
		{
			// Token: 0x060053D2 RID: 21458 RVA: 0x00002739 File Offset: 0x00000939
			public Data()
			{
			}

			// Token: 0x060053D3 RID: 21459 RVA: 0x00002739 File Offset: 0x00000939
			public Data(int mateId, Vector3 position, Vector3 rotation, Vector3 scale)
			{
			}

			// Token: 0x060053D4 RID: 21460 RVA: 0x0000216D File Offset: 0x0000036D
			public void CopyTo(MateTransformSetting.Data target)
			{
			}

			// Token: 0x060053D5 RID: 21461 RVA: 0x0000216D File Offset: 0x0000036D
			public void ImportTransform(Transform transform)
			{
			}

			// Token: 0x060053D6 RID: 21462 RVA: 0x0000216D File Offset: 0x0000036D
			public void ExportLocate(Transform transform)
			{
			}

			// Token: 0x04009151 RID: 37201
			public int mateId;

			// Token: 0x04009152 RID: 37202
			public Vector3 position;

			// Token: 0x04009153 RID: 37203
			[SerializeField]
			public Vector3 rotation;

			// Token: 0x04009154 RID: 37204
			public Vector3 scale;
		}
	}
}
