using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.Effect;
using YgomSystem.ElementSystem;
using YgomSystem.Timeline;

namespace YgomGame.Scenario
{
	// Token: 0x020009A9 RID: 2473
	[DisallowMultipleComponent]
	public class ScenarioBGLoader : MonoBehaviour
	{
		// Token: 0x0600481A RID: 18458 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ScenarioBGActor.Setting bgSetting)
		{
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadBG(GameObject owner, string path, Action<ScenarioBGLoader.BgGeneratedResource> onFinish)
		{
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x0000216A File Offset: 0x0000036A
		public Texture TryGetLoadedBgTexture(GameObject owner, string path)
		{
			return null;
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnLoadedBgAsset(GameObject owner, string loadedPath)
		{
		}

		// Token: 0x0600481E RID: 18462 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateBgRenderTexture(GameObject owner, string path, ScenarioBGLoader.BgGeneratedResource bgGeneratedResource)
		{
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedRenderTexture(string path, ScenarioBGLoader.BgGeneratedResource bgGeneratedResource, int rtId, RenderTexture renderTexture, Texture2D texture)
		{
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCompleteRequest(string path, ScenarioBGLoader.BgGeneratedResource bgGeneratedResource)
		{
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReleaseBg(GameObject owner, string path)
		{
		}

		// Token: 0x06004822 RID: 18466 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0400865A RID: 34394
		private ScenarioBGActor.Setting m_BGSetting;

		// Token: 0x0400865B RID: 34395
		private Dictionary<string, List<ScenarioBGLoader.RequestData>> m_Requests;

		// Token: 0x0400865C RID: 34396
		private Dictionary<string, ScenarioBGLoader.BgGeneratedResource> m_BgResourceMap;

		// Token: 0x0400865D RID: 34397
		private Dictionary<string, List<GameObject>> m_ReferencedOwnerMap;

		// Token: 0x020009AA RID: 2474
		private class RequestData
		{
			// Token: 0x06004824 RID: 18468 RVA: 0x00002739 File Offset: 0x00000939
			public RequestData(GameObject owner, Action<ScenarioBGLoader.BgGeneratedResource> callback)
			{
			}

			// Token: 0x0400865E RID: 34398
			public Action<ScenarioBGLoader.BgGeneratedResource> callback;

			// Token: 0x0400865F RID: 34399
			public GameObject owner;
		}

		// Token: 0x020009AB RID: 2475
		public class BgGeneratedResource
		{
			// Token: 0x17000674 RID: 1652
			// (get) Token: 0x06004825 RID: 18469 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06004826 RID: 18470 RVA: 0x0000216D File Offset: 0x0000036D
			public ElementObjectManager eom
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000675 RID: 1653
			// (get) Token: 0x06004827 RID: 18471 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004828 RID: 18472 RVA: 0x0000216D File Offset: 0x0000036D
			public int renderTextureId
			{
				[CompilerGenerated]
				get
				{
					return 0;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000676 RID: 1654
			// (get) Token: 0x06004829 RID: 18473 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600482A RID: 18474 RVA: 0x0000216D File Offset: 0x0000036D
			public Sprite createdSprite
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000677 RID: 1655
			// (get) Token: 0x0600482B RID: 18475 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600482C RID: 18476 RVA: 0x0000216D File Offset: 0x0000036D
			public Texture renderTexture
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000678 RID: 1656
			// (get) Token: 0x0600482D RID: 18477 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600482E RID: 18478 RVA: 0x0000216D File Offset: 0x0000036D
			public SpriteRenderer spriteRenderer
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000679 RID: 1657
			// (get) Token: 0x0600482F RID: 18479 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06004830 RID: 18480 RVA: 0x0000216D File Offset: 0x0000036D
			public SpriteScaler spriteScaler
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x1700067A RID: 1658
			// (get) Token: 0x06004831 RID: 18481 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06004832 RID: 18482 RVA: 0x0000216D File Offset: 0x0000036D
			public LabeledPlayableController labeledPlayableController
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x1700067B RID: 1659
			// (get) Token: 0x06004833 RID: 18483 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06004834 RID: 18484 RVA: 0x0000216D File Offset: 0x0000036D
			public Transform rootTran
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x1700067C RID: 1660
			// (get) Token: 0x06004835 RID: 18485 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06004836 RID: 18486 RVA: 0x0000216D File Offset: 0x0000036D
			public Transform scalerTran
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x1700067D RID: 1661
			// (get) Token: 0x06004837 RID: 18487 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06004838 RID: 18488 RVA: 0x0000216D File Offset: 0x0000036D
			public Transform shakePosTran
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x1700067E RID: 1662
			// (get) Token: 0x06004839 RID: 18489 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600483A RID: 18490 RVA: 0x0000216D File Offset: 0x0000036D
			public Transform movePosTran
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x0600483B RID: 18491 RVA: 0x0000216A File Offset: 0x0000036A
			public static ScenarioBGLoader.BgGeneratedResource Create(GameObject gameObject)
			{
				return null;
			}

			// Token: 0x0600483C RID: 18492 RVA: 0x0000216A File Offset: 0x0000036A
			public static ScenarioBGLoader.BgGeneratedResource Create(Sprite sprite)
			{
				return null;
			}

			// Token: 0x0600483D RID: 18493 RVA: 0x0000216A File Offset: 0x0000036A
			public static ScenarioBGLoader.BgGeneratedResource Create(Texture2D texture2D)
			{
				return null;
			}

			// Token: 0x0600483E RID: 18494 RVA: 0x00002739 File Offset: 0x00000939
			public BgGeneratedResource(GameObject originGob)
			{
			}

			// Token: 0x0600483F RID: 18495 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnCreatedRenderTexture(int rtId, RenderTexture renderTexture)
			{
			}

			// Token: 0x06004840 RID: 18496 RVA: 0x0000216D File Offset: 0x0000036D
			public void Release()
			{
			}
		}
	}
}
