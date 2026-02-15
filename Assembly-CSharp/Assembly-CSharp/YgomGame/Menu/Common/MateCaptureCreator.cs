using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B46 RID: 2886
	public class MateCaptureCreator : MonoBehaviour
	{
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060053BB RID: 21435 RVA: 0x0000216A File Offset: 0x0000036A
		private static MateCaptureCreator s_Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x0000216D File Offset: 0x0000036D
		public static void GetMateCapture(GameObject owner, MateCaptureContext context, Action<RenderTexture> callback)
		{
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RereaseMateCapture(GameObject owner)
		{
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0400913F RID: 37183
		private static MateCaptureCreator s_InstanceCache;

		// Token: 0x04009140 RID: 37184
		private List<MateCaptureCreator.CreateSequncer> m_Sequencers;

		// Token: 0x04009141 RID: 37185
		private List<MateCaptureCreator.CreateSequncer> m_CompletedSequencers;

		// Token: 0x04009142 RID: 37186
		private List<MateCaptureCreator.Cache> m_Caches;

		// Token: 0x02000B47 RID: 2887
		public class Request
		{
			// Token: 0x060053C0 RID: 21440 RVA: 0x00002739 File Offset: 0x00000939
			public Request(GameObject owner, Action<RenderTexture> callback)
			{
			}

			// Token: 0x04009143 RID: 37187
			public GameObject owner;

			// Token: 0x04009144 RID: 37188
			public Action<RenderTexture> callback;
		}

		// Token: 0x02000B48 RID: 2888
		public class Cache
		{
			// Token: 0x170007E8 RID: 2024
			// (get) Token: 0x060053C1 RID: 21441 RVA: 0x000029CC File Offset: 0x00000BCC
			public int refererCnt
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x060053C2 RID: 21442 RVA: 0x00002739 File Offset: 0x00000939
			public Cache(MateCaptureContext context, RenderTexture renderTexture, int renderTextureId)
			{
			}

			// Token: 0x060053C3 RID: 21443 RVA: 0x0000216A File Offset: 0x0000036A
			public RenderTexture AssignRef(GameObject owner)
			{
				return null;
			}

			// Token: 0x060053C4 RID: 21444 RVA: 0x0000216D File Offset: 0x0000036D
			public void Release()
			{
			}

			// Token: 0x04009145 RID: 37189
			public MateCaptureContext context;

			// Token: 0x04009146 RID: 37190
			public RenderTexture renderTexture;

			// Token: 0x04009147 RID: 37191
			public List<GameObject> referer;

			// Token: 0x04009148 RID: 37192
			public int renderTextureId;
		}

		// Token: 0x02000B49 RID: 2889
		public class CreateSequncer
		{
			// Token: 0x170007E9 RID: 2025
			// (get) Token: 0x060053C5 RID: 21445 RVA: 0x0000216A File Offset: 0x0000036A
			public RenderTexture renderTexture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170007EA RID: 2026
			// (get) Token: 0x060053C6 RID: 21446 RVA: 0x000029CC File Offset: 0x00000BCC
			public int renderTextureId
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x060053C7 RID: 21447 RVA: 0x0000216D File Offset: 0x0000036D
			public void Start()
			{
			}

			// Token: 0x060053C8 RID: 21448 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool Progress()
			{
				return false;
			}

			// Token: 0x060053C9 RID: 21449 RVA: 0x0000216A File Offset: 0x0000036A
			private IEnumerator yProgress()
			{
				return null;
			}

			// Token: 0x060053CA RID: 21450 RVA: 0x0000216D File Offset: 0x0000036D
			public void Abort()
			{
			}

			// Token: 0x060053CB RID: 21451 RVA: 0x0000216D File Offset: 0x0000036D
			public void Release(bool isRenderTexture = true)
			{
			}

			// Token: 0x04009149 RID: 37193
			public MateCaptureContext context;

			// Token: 0x0400914A RID: 37194
			public List<MateCaptureCreator.Request> requests;

			// Token: 0x0400914B RID: 37195
			private int m_RenderTextureId;

			// Token: 0x0400914C RID: 37196
			private RenderTexture m_RenderTexture;

			// Token: 0x0400914D RID: 37197
			private IEnumerator m_ProgressSeq;

			// Token: 0x0400914E RID: 37198
			private GameObject m_MateLocator;
		}
	}
}
