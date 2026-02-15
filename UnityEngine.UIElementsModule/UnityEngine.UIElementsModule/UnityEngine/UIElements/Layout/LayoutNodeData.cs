using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200058B RID: 1419
	internal struct LayoutNodeData
	{
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060026E4 RID: 9956 RVA: 0x0009AEBA File Offset: 0x000990BA
		// (set) Token: 0x060026E5 RID: 9957 RVA: 0x0009AEC7 File Offset: 0x000990C7
		public bool HasNewLayout
		{
			get
			{
				return (this.Status & LayoutNodeData.FlexStatus.HasNewLayout) == LayoutNodeData.FlexStatus.HasNewLayout;
			}
			set
			{
				this.Status = (value ? (this.Status | LayoutNodeData.FlexStatus.HasNewLayout) : (this.Status & ~LayoutNodeData.FlexStatus.HasNewLayout));
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x0009AEE5 File Offset: 0x000990E5
		// (set) Token: 0x060026E7 RID: 9959 RVA: 0x0009AEF2 File Offset: 0x000990F2
		public bool IsDirty
		{
			get
			{
				return (this.Status & LayoutNodeData.FlexStatus.IsDirty) == LayoutNodeData.FlexStatus.IsDirty;
			}
			set
			{
				this.Status = (value ? (this.Status | LayoutNodeData.FlexStatus.IsDirty) : (this.Status & ~LayoutNodeData.FlexStatus.IsDirty));
			}
		}

		// Token: 0x040013B6 RID: 5046
		public FixedBuffer2<LayoutValue> ResolvedDimensions;

		// Token: 0x040013B7 RID: 5047
		private float TargetSize;

		// Token: 0x040013B8 RID: 5048
		public int ManagedMeasureFunctionIndex;

		// Token: 0x040013B9 RID: 5049
		public int ManagedBaselineFunctionIndex;

		// Token: 0x040013BA RID: 5050
		public int ManagedOwnerIndex;

		// Token: 0x040013BB RID: 5051
		public int LineIndex;

		// Token: 0x040013BC RID: 5052
		public LayoutHandle Config;

		// Token: 0x040013BD RID: 5053
		public LayoutHandle Parent;

		// Token: 0x040013BE RID: 5054
		public LayoutHandle NextChild;

		// Token: 0x040013BF RID: 5055
		public LayoutList<LayoutHandle> Children;

		// Token: 0x040013C0 RID: 5056
		private LayoutNodeData.FlexStatus Status;

		// Token: 0x0200058C RID: 1420
		[Flags]
		internal enum FlexStatus
		{
			// Token: 0x040013C2 RID: 5058
			IsDirty = 1,
			// Token: 0x040013C3 RID: 5059
			HasNewLayout = 4,
			// Token: 0x040013C4 RID: 5060
			DependsOnParentSize = 64,
			// Token: 0x040013C5 RID: 5061
			Fixed = 8,
			// Token: 0x040013C6 RID: 5062
			MinViolation = 16,
			// Token: 0x040013C7 RID: 5063
			MaxViolation = 32
		}
	}
}
