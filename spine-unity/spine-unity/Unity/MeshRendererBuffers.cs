using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000062 RID: 98
	public class MeshRendererBuffers : IDisposable
	{
		// Token: 0x06000318 RID: 792 RVA: 0x0001245C File Offset: 0x0001065C
		public void Initialize()
		{
			if (this.doubleBufferedMesh != null)
			{
				this.doubleBufferedMesh.GetNext().Clear();
				this.doubleBufferedMesh.GetNext().Clear();
				this.submeshMaterials.Clear(true);
				return;
			}
			this.doubleBufferedMesh = new DoubleBuffered<MeshRendererBuffers.SmartMesh>();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000124AC File Offset: 0x000106AC
		public Material[] GetUpdatedSharedMaterialsArray()
		{
			if (this.submeshMaterials.Count == this.sharedMaterials.Length)
			{
				this.submeshMaterials.CopyTo(this.sharedMaterials);
			}
			else
			{
				this.sharedMaterials = this.submeshMaterials.ToArray();
			}
			return this.sharedMaterials;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000124F8 File Offset: 0x000106F8
		public bool MaterialsChangedInLastUpdate()
		{
			int newSubmeshMaterials = this.submeshMaterials.Count;
			Material[] sharedMaterials = this.sharedMaterials;
			if (newSubmeshMaterials != sharedMaterials.Length)
			{
				return true;
			}
			Material[] submeshMaterialsItems = this.submeshMaterials.Items;
			for (int i = 0; i < newSubmeshMaterials; i++)
			{
				if (submeshMaterialsItems[i] != sharedMaterials[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00012544 File Offset: 0x00010744
		public void UpdateSharedMaterials(ExposedList<SubmeshInstruction> instructions)
		{
			int newSize = instructions.Count;
			if (newSize > this.submeshMaterials.Items.Length)
			{
				Array.Resize<Material>(ref this.submeshMaterials.Items, newSize);
			}
			this.submeshMaterials.Count = newSize;
			Material[] submeshMaterialsItems = this.submeshMaterials.Items;
			SubmeshInstruction[] instructionsItems = instructions.Items;
			for (int i = 0; i < newSize; i++)
			{
				submeshMaterialsItems[i] = instructionsItems[i].material;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000125B3 File Offset: 0x000107B3
		public MeshRendererBuffers.SmartMesh GetNextMesh()
		{
			return this.doubleBufferedMesh.GetNext();
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000125C0 File Offset: 0x000107C0
		public void Clear()
		{
			this.sharedMaterials = new Material[0];
			this.submeshMaterials.Clear(true);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000125DA File Offset: 0x000107DA
		public void Dispose()
		{
			if (this.doubleBufferedMesh == null)
			{
				return;
			}
			this.doubleBufferedMesh.GetNext().Dispose();
			this.doubleBufferedMesh.GetNext().Dispose();
			this.doubleBufferedMesh = null;
		}

		// Token: 0x040001FA RID: 506
		private DoubleBuffered<MeshRendererBuffers.SmartMesh> doubleBufferedMesh;

		// Token: 0x040001FB RID: 507
		internal readonly ExposedList<Material> submeshMaterials = new ExposedList<Material>();

		// Token: 0x040001FC RID: 508
		internal Material[] sharedMaterials = new Material[0];

		// Token: 0x02000063 RID: 99
		public class SmartMesh : IDisposable
		{
			// Token: 0x06000320 RID: 800 RVA: 0x0001262B File Offset: 0x0001082B
			public void Clear()
			{
				this.mesh.Clear();
				this.instructionUsed.Clear();
			}

			// Token: 0x06000321 RID: 801 RVA: 0x00012643 File Offset: 0x00010843
			public void Dispose()
			{
				if (this.mesh != null)
				{
					global::UnityEngine.Object.Destroy(this.mesh);
				}
				this.mesh = null;
			}

			// Token: 0x040001FD RID: 509
			public Mesh mesh = SpineMesh.NewSkeletonMesh();

			// Token: 0x040001FE RID: 510
			public SkeletonRendererInstruction instructionUsed = new SkeletonRendererInstruction();
		}
	}
}
