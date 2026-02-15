using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000D5E RID: 3422
	public class DuelCursorJump : MonoBehaviour
	{
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06006384 RID: 25476 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006385 RID: 25477 RVA: 0x0000216D File Offset: 0x0000036D
		public bool opening
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06006386 RID: 25478 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(DuelGameObjectManager goManager, Func<bool> isOpenActiveFunc, Transform parent, Action<DuelCursorJump> finishCallback)
		{
		}

		// Token: 0x06006387 RID: 25479 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(DuelGameObjectManager goManager)
		{
		}

		// Token: 0x06006388 RID: 25480 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectItem(SharedDefinition.Location location, int position)
		{
		}

		// Token: 0x06006389 RID: 25481 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open()
		{
		}

		// Token: 0x0600638A RID: 25482 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close(bool force)
		{
		}

		// Token: 0x0600638B RID: 25483 RVA: 0x000F5B14 File Offset: 0x000F3D14
		private Vector2 GetPlaceScreenPoint(SharedDefinition.Location location, int position)
		{
			return default(Vector2);
		}

		// Token: 0x0600638C RID: 25484 RVA: 0x000F5B2C File Offset: 0x000F3D2C
		private Vector2 GetMateScreenPoint(SharedDefinition.Location location)
		{
			return default(Vector2);
		}

		// Token: 0x0600638D RID: 25485 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600638E RID: 25486 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0600638F RID: 25487 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActive(bool active)
		{
		}

		// Token: 0x04009D9E RID: 40350
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x04009D9F RID: 40351
		private ElementObjectManager ui;

		// Token: 0x04009DA0 RID: 40352
		private DuelGameObjectManager goManager;

		// Token: 0x04009DA1 RID: 40353
		private Selector selector;

		// Token: 0x04009DA2 RID: 40354
		private SelectionButton openButton;

		// Token: 0x04009DA3 RID: 40355
		private Image upIcon;

		// Token: 0x04009DA4 RID: 40356
		private Image downIcon;

		// Token: 0x04009DA5 RID: 40357
		private Image rightIcon;

		// Token: 0x04009DA6 RID: 40358
		private Image leftIcon;

		// Token: 0x04009DA7 RID: 40359
		private Image northIcon;

		// Token: 0x04009DA8 RID: 40360
		private Image eastIcon;

		// Token: 0x04009DA9 RID: 40361
		private Image southIcon;

		// Token: 0x04009DAA RID: 40362
		private Image westIcon;

		// Token: 0x04009DAB RID: 40363
		private Image profileNearIcon;

		// Token: 0x04009DAC RID: 40364
		private Image profileFarIcon;

		// Token: 0x04009DAD RID: 40365
		private Image mateNearIcon;

		// Token: 0x04009DAE RID: 40366
		private Image mateFarIcon;

		// Token: 0x04009DAF RID: 40367
		private bool isPlayableBgEffectNear;

		// Token: 0x04009DB0 RID: 40368
		private bool isPlayableBgEffectFar;

		// Token: 0x04009DB1 RID: 40369
		private bool reqOpen;

		// Token: 0x04009DB2 RID: 40370
		private Func<bool> isOpenActive;

		// Token: 0x04009DB3 RID: 40371
		private bool initialized;

		// Token: 0x04009DB4 RID: 40372
		private const string prefabPath = "Prefabs/Duel/DuelCursorJump";
	}
}
