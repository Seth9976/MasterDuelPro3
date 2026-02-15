using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000039 RID: 57
	[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
	[HelpURL("http://esotericsoftware.com/spine-unity#SkeletonRenderSeparator")]
	public class SkeletonPartsRenderer : MonoBehaviour
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000BA8A File Offset: 0x00009C8A
		public MeshGenerator MeshGenerator
		{
			get
			{
				this.LazyIntialize();
				return this.meshGenerator;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000BA98 File Offset: 0x00009C98
		public MeshRenderer MeshRenderer
		{
			get
			{
				this.LazyIntialize();
				return this.meshRenderer;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000BAA6 File Offset: 0x00009CA6
		public MeshFilter MeshFilter
		{
			get
			{
				this.LazyIntialize();
				return this.meshFilter;
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600021C RID: 540 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		// (remove) Token: 0x0600021D RID: 541 RVA: 0x0000BAEC File Offset: 0x00009CEC
		public event SkeletonPartsRenderer.SkeletonPartsRendererDelegate OnMeshAndMaterialsUpdated;

		// Token: 0x0600021E RID: 542 RVA: 0x0000BB24 File Offset: 0x00009D24
		private void LazyIntialize()
		{
			if (this.buffers == null)
			{
				this.buffers = new MeshRendererBuffers();
				this.buffers.Initialize();
				if (this.meshGenerator != null)
				{
					return;
				}
				this.meshGenerator = new MeshGenerator();
				this.meshFilter = base.GetComponent<MeshFilter>();
				this.meshRenderer = base.GetComponent<MeshRenderer>();
				this.currentInstructions.Clear();
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000BB86 File Offset: 0x00009D86
		private void OnDestroy()
		{
			if (this.buffers != null)
			{
				this.buffers.Dispose();
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000BB9B File Offset: 0x00009D9B
		public void ClearMesh()
		{
			this.LazyIntialize();
			this.meshFilter.sharedMesh = null;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		public void RenderParts(ExposedList<SubmeshInstruction> instructions, int startSubmesh, int endSubmesh)
		{
			this.LazyIntialize();
			MeshRendererBuffers.SmartMesh smartMesh = this.buffers.GetNextMesh();
			this.currentInstructions.SetWithSubset(instructions, startSubmesh, endSubmesh);
			bool updateTriangles = SkeletonRendererInstruction.GeometryNotEqual(this.currentInstructions, smartMesh.instructionUsed);
			SubmeshInstruction[] currentInstructionsSubmeshesItems = this.currentInstructions.submeshInstructions.Items;
			this.meshGenerator.Begin();
			if (this.currentInstructions.hasActiveClipping)
			{
				for (int i = 0; i < this.currentInstructions.submeshInstructions.Count; i++)
				{
					this.meshGenerator.AddSubmesh(currentInstructionsSubmeshesItems[i], updateTriangles);
				}
			}
			else
			{
				this.meshGenerator.BuildMeshWithArrays(this.currentInstructions, updateTriangles);
			}
			this.buffers.UpdateSharedMaterials(this.currentInstructions.submeshInstructions);
			Mesh mesh = smartMesh.mesh;
			if (this.meshGenerator.VertexCount <= 0)
			{
				mesh.Clear();
			}
			else
			{
				this.meshGenerator.FillVertexData(mesh);
				if (updateTriangles)
				{
					this.meshGenerator.FillTriangles(mesh);
					this.meshRenderer.sharedMaterials = this.buffers.GetUpdatedSharedMaterialsArray();
				}
				else if (this.buffers.MaterialsChangedInLastUpdate())
				{
					this.meshRenderer.sharedMaterials = this.buffers.GetUpdatedSharedMaterialsArray();
				}
				this.meshGenerator.FillLateVertexData(mesh);
			}
			this.meshFilter.sharedMesh = mesh;
			smartMesh.instructionUsed.Set(this.currentInstructions);
			if (this.OnMeshAndMaterialsUpdated != null)
			{
				this.OnMeshAndMaterialsUpdated(this);
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000BD27 File Offset: 0x00009F27
		public void SetPropertyBlock(MaterialPropertyBlock block)
		{
			this.LazyIntialize();
			this.meshRenderer.SetPropertyBlock(block);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000BD3C File Offset: 0x00009F3C
		public static SkeletonPartsRenderer NewPartsRendererGameObject(Transform parent, string name, int sortingOrder = 0)
		{
			GameObject gameObject = new GameObject(name, new Type[]
			{
				typeof(MeshFilter),
				typeof(MeshRenderer)
			});
			gameObject.transform.SetParent(parent, false);
			SkeletonPartsRenderer skeletonPartsRenderer = gameObject.AddComponent<SkeletonPartsRenderer>();
			skeletonPartsRenderer.MeshRenderer.sortingOrder = sortingOrder;
			return skeletonPartsRenderer;
		}

		// Token: 0x0400013F RID: 319
		private MeshGenerator meshGenerator;

		// Token: 0x04000140 RID: 320
		private MeshRenderer meshRenderer;

		// Token: 0x04000141 RID: 321
		private MeshFilter meshFilter;

		// Token: 0x04000143 RID: 323
		private MeshRendererBuffers buffers;

		// Token: 0x04000144 RID: 324
		private SkeletonRendererInstruction currentInstructions = new SkeletonRendererInstruction();

		// Token: 0x0200003A RID: 58
		// (Invoke) Token: 0x06000226 RID: 550
		public delegate void SkeletonPartsRendererDelegate(SkeletonPartsRenderer skeletonPartsRenderer);
	}
}
