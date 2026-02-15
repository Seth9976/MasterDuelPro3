using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200054E RID: 1358
	internal struct RenderChainVEData
	{
		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x00093600 File Offset: 0x00091800
		public RenderChainCommand lastTailOrHeadCommand
		{
			get
			{
				return this.lastTailCommand ?? this.lastHeadCommand;
			}
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x00093624 File Offset: 0x00091824
		public static bool AllocatesID(BMPAlloc alloc)
		{
			return alloc.ownedState == OwnedState.Owned && alloc.IsValid();
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x0009364C File Offset: 0x0009184C
		public static bool InheritsID(BMPAlloc alloc)
		{
			return alloc.ownedState == OwnedState.Inherited && alloc.IsValid();
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x00093670 File Offset: 0x00091870
		public bool isInChain
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.flags & RenderDataFlags.IsInChain) == RenderDataFlags.IsInChain;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x0009367D File Offset: 0x0009187D
		public bool isGroupTransform
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.flags & RenderDataFlags.IsGroupTransform) == RenderDataFlags.IsGroupTransform;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x0009368A File Offset: 0x0009188A
		public bool isIgnoringDynamicColorHint
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.flags & RenderDataFlags.IsIgnoringDynamicColorHint) == RenderDataFlags.IsIgnoringDynamicColorHint;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x00093697 File Offset: 0x00091897
		public bool hasExtraData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.flags & RenderDataFlags.HasExtraData) == RenderDataFlags.HasExtraData;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x000936A4 File Offset: 0x000918A4
		public bool hasExtraMeshes
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.flags & RenderDataFlags.HasExtraMeshes) == RenderDataFlags.HasExtraMeshes;
			}
		}

		// Token: 0x0400129D RID: 4765
		public VisualElement prev;

		// Token: 0x0400129E RID: 4766
		public VisualElement next;

		// Token: 0x0400129F RID: 4767
		public VisualElement groupTransformAncestor;

		// Token: 0x040012A0 RID: 4768
		public VisualElement boneTransformAncestor;

		// Token: 0x040012A1 RID: 4769
		public VisualElement prevDirty;

		// Token: 0x040012A2 RID: 4770
		public VisualElement nextDirty;

		// Token: 0x040012A3 RID: 4771
		public RenderDataFlags flags;

		// Token: 0x040012A4 RID: 4772
		public int hierarchyDepth;

		// Token: 0x040012A5 RID: 4773
		public RenderDataDirtyTypes dirtiedValues;

		// Token: 0x040012A6 RID: 4774
		public uint dirtyID;

		// Token: 0x040012A7 RID: 4775
		public RenderChainCommand firstHeadCommand;

		// Token: 0x040012A8 RID: 4776
		public RenderChainCommand lastHeadCommand;

		// Token: 0x040012A9 RID: 4777
		public RenderChainCommand firstTailCommand;

		// Token: 0x040012AA RID: 4778
		public RenderChainCommand lastTailCommand;

		// Token: 0x040012AB RID: 4779
		public bool localFlipsWinding;

		// Token: 0x040012AC RID: 4780
		public bool localTransformScaleZero;

		// Token: 0x040012AD RID: 4781
		public bool worldFlipsWinding;

		// Token: 0x040012AE RID: 4782
		public bool worldTransformScaleZero;

		// Token: 0x040012AF RID: 4783
		public ClipMethod clipMethod;

		// Token: 0x040012B0 RID: 4784
		public int childrenStencilRef;

		// Token: 0x040012B1 RID: 4785
		public int childrenMaskDepth;

		// Token: 0x040012B2 RID: 4786
		public MeshHandle headMesh;

		// Token: 0x040012B3 RID: 4787
		public MeshHandle tailMesh;

		// Token: 0x040012B4 RID: 4788
		public Matrix4x4 verticesSpace;

		// Token: 0x040012B5 RID: 4789
		public BMPAlloc transformID;

		// Token: 0x040012B6 RID: 4790
		public BMPAlloc clipRectID;

		// Token: 0x040012B7 RID: 4791
		public BMPAlloc opacityID;

		// Token: 0x040012B8 RID: 4792
		public BMPAlloc textCoreSettingsID;

		// Token: 0x040012B9 RID: 4793
		public BMPAlloc colorID;

		// Token: 0x040012BA RID: 4794
		public BMPAlloc backgroundColorID;

		// Token: 0x040012BB RID: 4795
		public BMPAlloc borderLeftColorID;

		// Token: 0x040012BC RID: 4796
		public BMPAlloc borderTopColorID;

		// Token: 0x040012BD RID: 4797
		public BMPAlloc borderRightColorID;

		// Token: 0x040012BE RID: 4798
		public BMPAlloc borderBottomColorID;

		// Token: 0x040012BF RID: 4799
		public BMPAlloc tintColorID;

		// Token: 0x040012C0 RID: 4800
		public float compositeOpacity;

		// Token: 0x040012C1 RID: 4801
		public float backgroundAlpha;

		// Token: 0x040012C2 RID: 4802
		public BasicNode<TextureEntry> textures;

		// Token: 0x040012C3 RID: 4803
		public bool pendingRepaint;

		// Token: 0x040012C4 RID: 4804
		public bool pendingHierarchicalRepaint;
	}
}
