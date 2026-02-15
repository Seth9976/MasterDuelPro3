using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000542 RID: 1346
	internal struct ChainBuilderStats
	{
		// Token: 0x0400122C RID: 4652
		public uint elementsAdded;

		// Token: 0x0400122D RID: 4653
		public uint elementsRemoved;

		// Token: 0x0400122E RID: 4654
		public uint recursiveClipUpdates;

		// Token: 0x0400122F RID: 4655
		public uint recursiveClipUpdatesExpanded;

		// Token: 0x04001230 RID: 4656
		public uint nonRecursiveClipUpdates;

		// Token: 0x04001231 RID: 4657
		public uint recursiveTransformUpdates;

		// Token: 0x04001232 RID: 4658
		public uint recursiveTransformUpdatesExpanded;

		// Token: 0x04001233 RID: 4659
		public uint recursiveOpacityUpdates;

		// Token: 0x04001234 RID: 4660
		public uint recursiveOpacityUpdatesExpanded;

		// Token: 0x04001235 RID: 4661
		public uint opacityIdUpdates;

		// Token: 0x04001236 RID: 4662
		public uint colorUpdates;

		// Token: 0x04001237 RID: 4663
		public uint colorUpdatesExpanded;

		// Token: 0x04001238 RID: 4664
		public uint recursiveVisualUpdates;

		// Token: 0x04001239 RID: 4665
		public uint recursiveVisualUpdatesExpanded;

		// Token: 0x0400123A RID: 4666
		public uint nonRecursiveVisualUpdates;

		// Token: 0x0400123B RID: 4667
		public uint dirtyProcessed;

		// Token: 0x0400123C RID: 4668
		public uint nudgeTransformed;

		// Token: 0x0400123D RID: 4669
		public uint boneTransformed;

		// Token: 0x0400123E RID: 4670
		public uint skipTransformed;

		// Token: 0x0400123F RID: 4671
		public uint visualUpdateTransformed;

		// Token: 0x04001240 RID: 4672
		public uint updatedMeshAllocations;

		// Token: 0x04001241 RID: 4673
		public uint newMeshAllocations;

		// Token: 0x04001242 RID: 4674
		public uint groupTransformElementsChanged;
	}
}
