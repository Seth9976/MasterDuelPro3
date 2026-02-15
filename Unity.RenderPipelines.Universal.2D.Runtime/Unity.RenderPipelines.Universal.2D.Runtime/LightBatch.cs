using System;
using Unity.Collections;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000044 RID: 68
	internal class LightBatch
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000EA0F File Offset: 0x0000CC0F
		private static int batchLightMod
		{
			get
			{
				return LightBuffer.kLightMod;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000EA16 File Offset: 0x0000CC16
		private static float batchRunningIndex
		{
			get
			{
				return (float)(LightBatch.sBatchIndexCounter++ % LightBuffer.kLightMod) / (float)LightBuffer.kLightMod;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000020A7 File Offset: 0x000002A7
		public static bool isBatchingSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000EA33 File Offset: 0x0000CC33
		internal NativeArray<PerLight2D> nativeBuffer
		{
			get
			{
				if (this.lightBuffer[this.activeCount] == null)
				{
					this.lightBuffer[this.activeCount] = new LightBuffer();
				}
				return this.lightBuffer[this.activeCount].nativeBuffer;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000EA68 File Offset: 0x0000CC68
		internal GraphicsBuffer graphicsBuffer
		{
			get
			{
				if (this.lightBuffer[this.activeCount] == null)
				{
					this.lightBuffer[this.activeCount] = new LightBuffer();
				}
				return this.lightBuffer[this.activeCount].graphicsBuffer;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000EA9D File Offset: 0x0000CC9D
		internal NativeArray<int> lightMarker
		{
			get
			{
				if (this.lightBuffer[this.activeCount] == null)
				{
					this.lightBuffer[this.activeCount] = new LightBuffer();
				}
				return this.lightBuffer[this.activeCount].lightMarkers;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000EAD4 File Offset: 0x0000CCD4
		internal PerLight2D GetLight(int index)
		{
			return this.nativeBuffer[index];
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		internal static int batchSlotIndex
		{
			get
			{
				return (int)(LightBatch.batchRunningIndex * (float)LightBuffer.kLightMod);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000EB00 File Offset: 0x0000CD00
		internal void SetLight(int index, PerLight2D light)
		{
			this.nativeBuffer[index] = light;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000EB1D File Offset: 0x0000CD1D
		internal static float GetBatchColor()
		{
			return (float)LightBatch.batchSlotIndex / (float)LightBatch.batchLightMod;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		internal static int GetBatchSlotIndex(float channelColor)
		{
			return (int)(channelColor * (float)LightBuffer.kLightMod);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000EB37 File Offset: 0x0000CD37
		private static int Hash(Light2D light, Material material)
		{
			return (((-2128831035 * 16777619) ^ material.GetHashCode()) * 16777619) ^ ((light.lightCookieSprite == null) ? 0 : light.lightCookieSprite.GetHashCode());
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000B4B1 File Offset: 0x000096B1
		private void Validate()
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000EB70 File Offset: 0x0000CD70
		private void OnAssemblyReload()
		{
			for (int i = 0; i < LightBuffer.kCount; i++)
			{
				this.lightBuffer[this.activeCount].Release();
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000EBA0 File Offset: 0x0000CDA0
		private void ResetInternals()
		{
			for (int i = 0; i < LightBuffer.kCount; i++)
			{
				if (this.lightBuffer[i] != null)
				{
					this.lightBuffer[i].Reset();
				}
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000EBD4 File Offset: 0x0000CDD4
		private void SetBuffer()
		{
			this.Validate();
			this.graphicsBuffer.SetData<PerLight2D>(this.nativeBuffer, this.lightCount, this.lightCount, math.min(LightBuffer.kBatchMax, LightBuffer.kMax - this.lightCount));
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000EC0F File Offset: 0x0000CE0F
		internal int SlotIndex(int x)
		{
			return this.lightCount + x;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000EC19 File Offset: 0x0000CE19
		internal void Reset()
		{
			if (LightBatch.isBatchingSupported)
			{
				this.maxIndex = 0;
				this.hashCode = 0;
				this.batchCount = 0;
				this.lightCount = 0;
				this.activeCount = 0;
				Shader.SetGlobalBuffer("_Light2DBuffer", this.graphicsBuffer);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000EC58 File Offset: 0x0000CE58
		internal bool CanBatch(Light2D light, Material material, int index, out int lightHash)
		{
			lightHash = LightBatch.Hash(light, material);
			this.hashCode = ((this.hashCode == 0) ? lightHash : this.hashCode);
			if (this.batchCount == 0)
			{
				this.hashCode = lightHash;
			}
			else if (this.hashCode != lightHash || this.SlotIndex(index) >= LightBuffer.kMax || this.lightMarker[index] == 1)
			{
				this.hashCode = lightHash;
				return false;
			}
			return true;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		internal bool AddBatch(Light2D light, Material material, Matrix4x4 mat, Mesh mesh, int subset, int lightHash, int index)
		{
			this.cachedLight = light;
			this.cachedMaterial = material;
			this.matrices[this.batchCount] = mat;
			this.lightMeshes[this.batchCount] = mesh;
			this.subsets[this.batchCount] = subset;
			this.batchCount++;
			this.maxIndex = math.max(this.maxIndex, index);
			this.lightMarker[index] = 1;
			return true;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000ED54 File Offset: 0x0000CF54
		internal void Flush(RasterCommandBuffer cmd)
		{
			if (this.batchCount > 0)
			{
				using (new ProfilingScope(cmd, LightBatch.profilingDrawBatched))
				{
					this.SetBuffer();
					cmd.SetGlobalInt(LightBatch.k_BufferOffset, this.lightCount);
					cmd.DrawMultipleMeshes(this.matrices, this.lightMeshes, this.subsets, this.batchCount, this.cachedMaterial, -1, null);
				}
				this.lightCount = this.lightCount + this.maxIndex + 1;
			}
			for (int i = 0; i < this.batchCount; i++)
			{
				this.lightMeshes[i] = null;
			}
			this.ResetInternals();
			this.batchCount = 0;
			this.maxIndex = 0;
		}

		// Token: 0x0400015B RID: 347
		private static readonly ProfilingSampler profilingDrawBatched = new ProfilingSampler("Light2D Batcher");

		// Token: 0x0400015C RID: 348
		private static readonly int k_BufferOffset = Shader.PropertyToID("_BatchBufferOffset");

		// Token: 0x0400015D RID: 349
		private static int sBatchIndexCounter = 0;

		// Token: 0x0400015E RID: 350
		private int[] subsets = new int[LightBuffer.kMax];

		// Token: 0x0400015F RID: 351
		private Mesh[] lightMeshes = new Mesh[LightBuffer.kMax];

		// Token: 0x04000160 RID: 352
		private Matrix4x4[] matrices = new Matrix4x4[LightBuffer.kMax];

		// Token: 0x04000161 RID: 353
		private LightBuffer[] lightBuffer = new LightBuffer[LightBuffer.kCount];

		// Token: 0x04000162 RID: 354
		private Light2D cachedLight;

		// Token: 0x04000163 RID: 355
		private Material cachedMaterial;

		// Token: 0x04000164 RID: 356
		private int hashCode;

		// Token: 0x04000165 RID: 357
		private int lightCount;

		// Token: 0x04000166 RID: 358
		private int maxIndex;

		// Token: 0x04000167 RID: 359
		private int batchCount;

		// Token: 0x04000168 RID: 360
		private int activeCount;
	}
}
