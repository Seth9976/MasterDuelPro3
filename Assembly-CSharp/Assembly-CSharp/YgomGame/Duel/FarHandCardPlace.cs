using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000E8D RID: 3725
	public class FarHandCardPlace : HandCardPlace
	{
		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06006C2A RID: 27690 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectedIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06006C2B RID: 27691 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int decidedIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06006C2C RID: 27692 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int handCardNum
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06006C2D RID: 27693 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override HandCardManager.ViewSortMode sortMode
		{
			get
			{
				return HandCardManager.ViewSortMode.EngineIndex;
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06006C2E RID: 27694 RVA: 0x000F5F90 File Offset: 0x000F4190
		protected override Vector3 locatorOffsetTargetDeciding
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06006C2F RID: 27695 RVA: 0x000F5FA8 File Offset: 0x000F41A8
		protected override Vector3 locatorOffsetTargetSelecting
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06006C30 RID: 27696 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isAllOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006C31 RID: 27697 RVA: 0x000F5FBE File Offset: 0x000F41BE
		public FarHandCardPlace(DuelFieldBase duelField, int team, int position)
			: base(null, 0, 0)
		{
		}

		// Token: 0x06006C32 RID: 27698 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SelectNextItem(int index)
		{
		}

		// Token: 0x06006C33 RID: 27699 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SelectPrevItem(int index)
		{
		}

		// Token: 0x06006C34 RID: 27700 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnRegister(CardRoot cardRoot, int index, bool withEffect)
		{
		}

		// Token: 0x06006C35 RID: 27701 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void HighlightImpl(int index, SharedDefinition.ActivateAura auraType)
		{
		}

		// Token: 0x06006C36 RID: 27702 RVA: 0x0000216D File Offset: 0x0000036D
		public override void UpdateCenterPosition()
		{
		}

		// Token: 0x06006C37 RID: 27703 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SetupCardLocator(CardLocator cardLocator)
		{
		}

		// Token: 0x06006C38 RID: 27704 RVA: 0x000F5FC9 File Offset: 0x000F41C9
		public override void GetPosture(int index, out Vector3 position, out Quaternion rotation, out float depth, int card_num = -1, bool originPosition = false)
		{
			position = default(Vector3);
			rotation = default(Quaternion);
			depth = 0f;
		}

		// Token: 0x06006C39 RID: 27705 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int GetViewIndex(int index)
		{
			return 0;
		}

		// Token: 0x06006C3A RID: 27706 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int GetIndexByViewIndex(int viewIndex)
		{
			return 0;
		}

		// Token: 0x06006C3B RID: 27707 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int UniqueIdToIndex(int uniqueId)
		{
			return 0;
		}

		// Token: 0x06006C3C RID: 27708 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SetSortImpl(HandCardManager.ViewSortMode sort)
		{
		}

		// Token: 0x06006C3D RID: 27709 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InsertViewIndexImpl(int targetIndex, int InsertViewIndex)
		{
		}

		// Token: 0x06006C3E RID: 27710 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void RefreshCardLocatorsImpl()
		{
		}

		// Token: 0x06006C3F RID: 27711 RVA: 0x0000216D File Offset: 0x0000036D
		public override void UpdateAllCardPosition()
		{
		}

		// Token: 0x0400A786 RID: 42886
		private Vector3 _locatorOffsetTargetDeciding;

		// Token: 0x0400A787 RID: 42887
		private Vector3 _locatorOffsetTargetSelecting;
	}
}
