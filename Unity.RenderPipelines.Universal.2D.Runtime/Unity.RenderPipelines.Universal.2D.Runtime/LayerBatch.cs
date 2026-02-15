using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200003E RID: 62
	internal struct LayerBatch
	{
		// Token: 0x06000180 RID: 384 RVA: 0x0000E084 File Offset: 0x0000C284
		public unsafe void InitRTIds(int index)
		{
			for (int i = 0; i < 4; i++)
			{
				*((ref this.renderTargetUsed.FixedElementField) + i) = false;
				*((ref this.renderTargetIds.FixedElementField) + (IntPtr)i * 4) = Shader.PropertyToID(string.Format("_LightTexture_{0}_{1}", index, i));
			}
			this.lights = new List<Light2D>();
			this.shadowLights = new List<Light2D>();
			this.shadowCasters = new List<ShadowCasterGroup2D>();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000E0FC File Offset: 0x0000C2FC
		public unsafe RenderTargetIdentifier GetRTId(CommandBuffer cmd, RenderTextureDescriptor desc, int index)
		{
			if (!(*((ref this.renderTargetUsed.FixedElementField) + index)))
			{
				cmd.GetTemporaryRT(*((ref this.renderTargetIds.FixedElementField) + (IntPtr)index * 4), desc, FilterMode.Bilinear);
				*((ref this.renderTargetUsed.FixedElementField) + index) = true;
			}
			return new RenderTargetIdentifier(*((ref this.renderTargetIds.FixedElementField) + (IntPtr)index * 4));
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000E158 File Offset: 0x0000C358
		public unsafe void ReleaseRT(CommandBuffer cmd)
		{
			for (int i = 0; i < 4; i++)
			{
				if (*((ref this.renderTargetUsed.FixedElementField) + i))
				{
					cmd.ReleaseTemporaryRT(*((ref this.renderTargetIds.FixedElementField) + (IntPtr)i * 4));
					*((ref this.renderTargetUsed.FixedElementField) + i) = false;
				}
			}
		}

		// Token: 0x0400013D RID: 317
		public int startLayerID;

		// Token: 0x0400013E RID: 318
		public int endLayerValue;

		// Token: 0x0400013F RID: 319
		public SortingLayerRange layerRange;

		// Token: 0x04000140 RID: 320
		public LightStats lightStats;

		// Token: 0x04000141 RID: 321
		public bool useNormals;

		// Token: 0x04000142 RID: 322
		[FixedBuffer(typeof(int), 4)]
		private LayerBatch.<renderTargetIds>e__FixedBuffer renderTargetIds;

		// Token: 0x04000143 RID: 323
		[FixedBuffer(typeof(bool), 4)]
		private LayerBatch.<renderTargetUsed>e__FixedBuffer renderTargetUsed;

		// Token: 0x04000144 RID: 324
		public List<Light2D> lights;

		// Token: 0x04000145 RID: 325
		public List<Light2D> shadowLights;

		// Token: 0x04000146 RID: 326
		public List<ShadowCasterGroup2D> shadowCasters;

		// Token: 0x04000147 RID: 327
		internal int[] activeBlendStylesIndices;

		// Token: 0x0200003F RID: 63
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <renderTargetIds>e__FixedBuffer
		{
			// Token: 0x04000148 RID: 328
			public int FixedElementField;
		}

		// Token: 0x02000040 RID: 64
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 4)]
		public struct <renderTargetUsed>e__FixedBuffer
		{
			// Token: 0x04000149 RID: 329
			public bool FixedElementField;
		}
	}
}
