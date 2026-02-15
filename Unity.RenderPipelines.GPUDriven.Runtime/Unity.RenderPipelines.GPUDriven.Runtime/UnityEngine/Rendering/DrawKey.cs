using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000035 RID: 53
	internal struct DrawKey : IEquatable<DrawKey>
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00005B00 File Offset: 0x00003D00
		public bool Equals(DrawKey other)
		{
			return this.meshID == other.meshID && this.submeshIndex == other.submeshIndex && this.materialID == other.materialID && this.flags == other.flags && this.transparentInstanceId == other.transparentInstanceId && this.overridenComponents == other.overridenComponents && this.range.Equals(other.range) && this.lightmapIndex == other.lightmapIndex;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005B90 File Offset: 0x00003D90
		public override int GetHashCode()
		{
			return (int)(((((((((BatchDrawCommandFlags.FlipWinding | BatchDrawCommandFlags.IsLightMapped | BatchDrawCommandFlags.HasSortingPosition) * (BatchDrawCommandFlags.FlipWinding | BatchDrawCommandFlags.HasMotion | BatchDrawCommandFlags.IsLightMapped | BatchDrawCommandFlags.LODCrossFadeKeyword) + (int)this.meshID.value) * (BatchDrawCommandFlags.FlipWinding | BatchDrawCommandFlags.HasMotion | BatchDrawCommandFlags.IsLightMapped | BatchDrawCommandFlags.LODCrossFadeKeyword) + this.submeshIndex) * (BatchDrawCommandFlags.FlipWinding | BatchDrawCommandFlags.HasMotion | BatchDrawCommandFlags.IsLightMapped | BatchDrawCommandFlags.LODCrossFadeKeyword) + (int)this.materialID.value) * (BatchDrawCommandFlags.FlipWinding | BatchDrawCommandFlags.HasMotion | BatchDrawCommandFlags.IsLightMapped | BatchDrawCommandFlags.LODCrossFadeKeyword) + (int)this.flags) * (BatchDrawCommandFlags.FlipWinding | BatchDrawCommandFlags.HasMotion | BatchDrawCommandFlags.IsLightMapped | BatchDrawCommandFlags.LODCrossFadeKeyword) + this.transparentInstanceId) * (BatchDrawCommandFlags.FlipWinding | BatchDrawCommandFlags.HasMotion | BatchDrawCommandFlags.IsLightMapped | BatchDrawCommandFlags.LODCrossFadeKeyword) + this.range.GetHashCode()) * (BatchDrawCommandFlags.FlipWinding | BatchDrawCommandFlags.HasMotion | BatchDrawCommandFlags.IsLightMapped | BatchDrawCommandFlags.LODCrossFadeKeyword) + (int)this.overridenComponents) * (BatchDrawCommandFlags.FlipWinding | BatchDrawCommandFlags.HasMotion | BatchDrawCommandFlags.IsLightMapped | BatchDrawCommandFlags.LODCrossFadeKeyword) + this.lightmapIndex);
		}

		// Token: 0x040000B3 RID: 179
		public BatchMeshID meshID;

		// Token: 0x040000B4 RID: 180
		public int submeshIndex;

		// Token: 0x040000B5 RID: 181
		public BatchMaterialID materialID;

		// Token: 0x040000B6 RID: 182
		public BatchDrawCommandFlags flags;

		// Token: 0x040000B7 RID: 183
		public int transparentInstanceId;

		// Token: 0x040000B8 RID: 184
		public uint overridenComponents;

		// Token: 0x040000B9 RID: 185
		public RangeKey range;

		// Token: 0x040000BA RID: 186
		public int lightmapIndex;
	}
}
