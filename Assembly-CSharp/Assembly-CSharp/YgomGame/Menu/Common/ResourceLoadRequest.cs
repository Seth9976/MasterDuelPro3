using System;
using System.Collections;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B5D RID: 2909
	public class ResourceLoadRequest : MonoBehaviour
	{
		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06005425 RID: 21541 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isExistsAssetContainerLabel
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06005426 RID: 21542 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isValidAsset
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool YgomGame_002EMenu_002ECommon_002EIAsyncProgressContent_002EIsDone()
		{
			return false;
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x0000216D File Offset: 0x0000036D
		private void YgomGame_002EMenu_002ECommon_002EIAsyncProgressContent_002EProgressUpdate()
		{
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x0000216A File Offset: 0x0000036A
		public Type CheckResourceType(params Type[] checkTypes)
		{
			return null;
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x000F4C6A File Offset: 0x000F2E6A
		public bool TryGetAssetSize(out Vector2 size)
		{
			size = default(Vector2);
			return false;
		}

		// Token: 0x0600542B RID: 21547 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Load(GameObject owner, string resourcePath, bool async = true, Action<ResourceLoadRequest> onFinishCallback = null, bool hold = false, bool disabledErrorNotify = false)
		{
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x0000216D File Offset: 0x0000036D
		private void Load()
		{
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yLoad()
		{
			return null;
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFinish()
		{
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x040091A9 RID: 37289
		private string m_Path;

		// Token: 0x040091AA RID: 37290
		private string m_AssetContainerLabel;

		// Token: 0x040091AB RID: 37291
		private bool m_Immediate;

		// Token: 0x040091AC RID: 37292
		private bool m_Hold;

		// Token: 0x040091AD RID: 37293
		private bool m_DisabledErrorNotify;

		// Token: 0x040091AE RID: 37294
		private uint m_Crc;

		// Token: 0x040091AF RID: 37295
		private IEnumerator m_Loadroutine;

		// Token: 0x040091B0 RID: 37296
		private Action<ResourceLoadRequest> m_OnFinishCallback;
	}
}
