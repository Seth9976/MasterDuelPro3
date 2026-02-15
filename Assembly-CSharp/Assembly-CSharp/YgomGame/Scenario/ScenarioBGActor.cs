using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.Timeline;

namespace YgomGame.Scenario
{
	// Token: 0x020009A6 RID: 2470
	[DisallowMultipleComponent]
	public class ScenarioBGActor : MonoBehaviour
	{
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060047F8 RID: 18424 RVA: 0x0000216A File Offset: 0x0000036A
		public object renderTarget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060047F9 RID: 18425 RVA: 0x0000216A File Offset: 0x0000036A
		public RawImage rawImage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060047FA RID: 18426 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060047FB RID: 18427 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isError
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060047FC RID: 18428 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isExists
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060047FD RID: 18429 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060047FE RID: 18430 RVA: 0x0000216D File Offset: 0x0000036D
		public bool visible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060047FF RID: 18431 RVA: 0x000F4920 File Offset: 0x000F2B20
		// (set) Token: 0x06004800 RID: 18432 RVA: 0x0000216D File Offset: 0x0000036D
		public Color color
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06004801 RID: 18433 RVA: 0x000F4938 File Offset: 0x000F2B38
		// (set) Token: 0x06004802 RID: 18434 RVA: 0x0000216D File Offset: 0x0000036D
		public Vector3 offsetScale
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06004803 RID: 18435 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06004804 RID: 18436 RVA: 0x0000216D File Offset: 0x0000036D
		public float screenScale
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06004805 RID: 18437 RVA: 0x000F4950 File Offset: 0x000F2B50
		// (set) Token: 0x06004806 RID: 18438 RVA: 0x0000216D File Offset: 0x0000036D
		public Vector3 spritePos
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06004807 RID: 18439 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject shakeTarget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06004808 RID: 18440 RVA: 0x0000216A File Offset: 0x0000036A
		public LabeledPlayableController labeledPlayableController
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTitleText(string title)
		{
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ScenarioBGActor.Setting bgSetting, ScenarioBGLoader bgLoader)
		{
		}

		// Token: 0x0600480B RID: 18443 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadBg(string bgPath)
		{
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x0000216D File Offset: 0x0000036D
		public void CaptureBg(ScenarioBGActor source)
		{
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReleaseBg()
		{
		}

		// Token: 0x0600480F RID: 18447 RVA: 0x000F4968 File Offset: 0x000F2B68
		private Vector3 CalcScreenDiffMin()
		{
			return default(Vector3);
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x000F4980 File Offset: 0x000F2B80
		private Vector3 CalcScreenDiffMax()
		{
			return default(Vector3);
		}

		// Token: 0x06004811 RID: 18449 RVA: 0x0000216D File Offset: 0x0000036D
		private void AdjustPositionInScreen()
		{
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x000F4998 File Offset: 0x000F2B98
		public Vector3 CalcDirectionalPos(int direction)
		{
			return default(Vector3);
		}

		// Token: 0x04008643 RID: 34371
		private RawImage m_RawImageCache;

		// Token: 0x04008644 RID: 34372
		private string m_BgPath;

		// Token: 0x04008645 RID: 34373
		private bool m_LoadingBg;

		// Token: 0x04008646 RID: 34374
		private bool m_IsError;

		// Token: 0x04008647 RID: 34375
		private ScenarioBGLoader.BgGeneratedResource m_BgGeneratedResource;

		// Token: 0x04008648 RID: 34376
		private Vector3 m_ScreenMinPos;

		// Token: 0x04008649 RID: 34377
		private Vector3 m_ScreenMaxPos;

		// Token: 0x0400864A RID: 34378
		private float m_ScreenScale;

		// Token: 0x0400864B RID: 34379
		private Vector3 m_OffsetScale;

		// Token: 0x0400864C RID: 34380
		private bool m_Capture;

		// Token: 0x0400864D RID: 34381
		private ScenarioBGActor.Setting m_BgSetting;

		// Token: 0x0400864E RID: 34382
		private ScenarioBGLoader m_BGLoader;

		// Token: 0x0400864F RID: 34383
		private readonly Vector3 m_ShakerCenter;

		// Token: 0x020009A7 RID: 2471
		public class Setting
		{
			// Token: 0x04008650 RID: 34384
			public ElementObjectManager titlePref;

			// Token: 0x04008651 RID: 34385
			[NonSerialized]
			public Vector2Int bgRenderSize;
		}
	}
}
