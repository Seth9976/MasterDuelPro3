using System;

namespace System.Data
{
	// Token: 0x02000089 RID: 137
	internal enum RBTreeError
	{
		// Token: 0x040002AA RID: 682
		InvalidPageSize = 1,
		// Token: 0x040002AB RID: 683
		PagePositionInSlotInUse = 3,
		// Token: 0x040002AC RID: 684
		NoFreeSlots,
		// Token: 0x040002AD RID: 685
		InvalidStateinInsert,
		// Token: 0x040002AE RID: 686
		InvalidNextSizeInDelete = 7,
		// Token: 0x040002AF RID: 687
		InvalidStateinDelete,
		// Token: 0x040002B0 RID: 688
		InvalidNodeSizeinDelete,
		// Token: 0x040002B1 RID: 689
		InvalidStateinEndDelete,
		// Token: 0x040002B2 RID: 690
		CannotRotateInvalidsuccessorNodeinDelete,
		// Token: 0x040002B3 RID: 691
		IndexOutOFRangeinGetNodeByIndex = 13,
		// Token: 0x040002B4 RID: 692
		RBDeleteFixup,
		// Token: 0x040002B5 RID: 693
		UnsupportedAccessMethod1,
		// Token: 0x040002B6 RID: 694
		UnsupportedAccessMethod2,
		// Token: 0x040002B7 RID: 695
		UnsupportedAccessMethodInNonNillRootSubtree,
		// Token: 0x040002B8 RID: 696
		AttachedNodeWithZerorbTreeNodeId,
		// Token: 0x040002B9 RID: 697
		CompareNodeInDataRowTree,
		// Token: 0x040002BA RID: 698
		CompareSateliteTreeNodeInDataRowTree,
		// Token: 0x040002BB RID: 699
		NestedSatelliteTreeEnumerator
	}
}
