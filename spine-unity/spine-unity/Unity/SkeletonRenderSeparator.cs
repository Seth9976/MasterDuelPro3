using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Spine.Unity
{
	// Token: 0x0200003B RID: 59
	[ExecuteAlways]
	[HelpURL("http://esotericsoftware.com/spine-unity#SkeletonRenderSeparator")]
	public class SkeletonRenderSeparator : MonoBehaviour
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000BDA0 File Offset: 0x00009FA0
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000BDA8 File Offset: 0x00009FA8
		public SkeletonRenderer SkeletonRenderer
		{
			get
			{
				return this.skeletonRenderer;
			}
			set
			{
				if (this.skeletonRenderer != null)
				{
					this.skeletonRenderer.GenerateMeshOverride -= this.HandleRender;
				}
				this.skeletonRenderer = value;
				if (value == null)
				{
					base.enabled = false;
				}
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x0600022B RID: 555 RVA: 0x0000BDE8 File Offset: 0x00009FE8
		// (remove) Token: 0x0600022C RID: 556 RVA: 0x0000BE20 File Offset: 0x0000A020
		public event SkeletonRenderer.SkeletonRendererDelegate OnMeshAndMaterialsUpdated;

		// Token: 0x0600022D RID: 557 RVA: 0x0000BE58 File Offset: 0x0000A058
		public static SkeletonRenderSeparator AddToSkeletonRenderer(SkeletonRenderer skeletonRenderer, int sortingLayerID = 0, int extraPartsRenderers = 0, int sortingOrderIncrement = 5, int baseSortingOrder = 0, bool addMinimumPartsRenderers = true)
		{
			if (skeletonRenderer == null)
			{
				Debug.Log("Tried to add SkeletonRenderSeparator to a null SkeletonRenderer reference.");
				return null;
			}
			SkeletonRenderSeparator srs = skeletonRenderer.gameObject.AddComponent<SkeletonRenderSeparator>();
			srs.skeletonRenderer = skeletonRenderer;
			skeletonRenderer.Initialize(false, false);
			int count = extraPartsRenderers;
			if (addMinimumPartsRenderers)
			{
				count = extraPartsRenderers + skeletonRenderer.separatorSlots.Count + 1;
			}
			Transform skeletonRendererTransform = skeletonRenderer.transform;
			List<SkeletonPartsRenderer> componentRenderers = srs.partsRenderers;
			for (int i = 0; i < count; i++)
			{
				SkeletonPartsRenderer spr = SkeletonPartsRenderer.NewPartsRendererGameObject(skeletonRendererTransform, i.ToString(), 0);
				MeshRenderer meshRenderer = spr.MeshRenderer;
				meshRenderer.sortingLayerID = sortingLayerID;
				meshRenderer.sortingOrder = baseSortingOrder + i * sortingOrderIncrement;
				componentRenderers.Add(spr);
			}
			srs.OnEnable();
			return srs;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000BF04 File Offset: 0x0000A104
		public SkeletonPartsRenderer AddPartsRenderer(int sortingOrderIncrement = 5, string name = null)
		{
			int sortingLayerID = 0;
			int sortingOrder = 0;
			if (this.partsRenderers.Count > 0)
			{
				MeshRenderer meshRenderer = this.partsRenderers[this.partsRenderers.Count - 1].MeshRenderer;
				sortingLayerID = meshRenderer.sortingLayerID;
				sortingOrder = meshRenderer.sortingOrder + sortingOrderIncrement;
			}
			if (string.IsNullOrEmpty(name))
			{
				name = this.partsRenderers.Count.ToString();
			}
			SkeletonPartsRenderer spr = SkeletonPartsRenderer.NewPartsRendererGameObject(this.skeletonRenderer.transform, name, 0);
			this.partsRenderers.Add(spr);
			MeshRenderer meshRenderer2 = spr.MeshRenderer;
			meshRenderer2.sortingLayerID = sortingLayerID;
			meshRenderer2.sortingOrder = sortingOrder;
			return spr;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		public void OnEnable()
		{
			if (this.skeletonRenderer == null)
			{
				return;
			}
			if (this.copiedBlock == null)
			{
				this.copiedBlock = new MaterialPropertyBlock();
			}
			this.mainMeshRenderer = this.skeletonRenderer.GetComponent<MeshRenderer>();
			this.skeletonRenderer.GenerateMeshOverride -= this.HandleRender;
			this.skeletonRenderer.GenerateMeshOverride += this.HandleRender;
			if (this.copyMeshRendererFlags)
			{
				LightProbeUsage lightProbeUsage = this.mainMeshRenderer.lightProbeUsage;
				bool receiveShadows = this.mainMeshRenderer.receiveShadows;
				ReflectionProbeUsage reflectionProbeUsage = this.mainMeshRenderer.reflectionProbeUsage;
				ShadowCastingMode shadowCastingMode = this.mainMeshRenderer.shadowCastingMode;
				MotionVectorGenerationMode motionVectorGenerationMode = this.mainMeshRenderer.motionVectorGenerationMode;
				Transform probeAnchor = this.mainMeshRenderer.probeAnchor;
				for (int i = 0; i < this.partsRenderers.Count; i++)
				{
					SkeletonPartsRenderer currentRenderer = this.partsRenderers[i];
					if (!(currentRenderer == null))
					{
						MeshRenderer meshRenderer = currentRenderer.MeshRenderer;
						meshRenderer.lightProbeUsage = lightProbeUsage;
						meshRenderer.receiveShadows = receiveShadows;
						meshRenderer.reflectionProbeUsage = reflectionProbeUsage;
						meshRenderer.shadowCastingMode = shadowCastingMode;
						meshRenderer.motionVectorGenerationMode = motionVectorGenerationMode;
						meshRenderer.probeAnchor = probeAnchor;
					}
				}
			}
			if (this.skeletonRenderer.updateWhenInvisible != UpdateMode.FullUpdate)
			{
				this.skeletonRenderer.LateUpdateMesh();
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000C0E1 File Offset: 0x0000A2E1
		public void Update()
		{
			this.UpdateVisibility();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000C0E9 File Offset: 0x0000A2E9
		public void OnDisable()
		{
			if (this.skeletonRenderer == null)
			{
				return;
			}
			this.skeletonRenderer.GenerateMeshOverride -= this.HandleRender;
			this.skeletonRenderer.LateUpdateMesh();
			this.ClearPartsRendererMeshes();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000C124 File Offset: 0x0000A324
		public void UpdateVisibility()
		{
			foreach (SkeletonPartsRenderer partsRenderer in this.partsRenderers)
			{
				if (!(partsRenderer == null) && partsRenderer.MeshRenderer.isVisible)
				{
					if (!this.isVisible)
					{
						this.skeletonRenderer.OnBecameVisible();
						this.isVisible = true;
					}
					return;
				}
			}
			if (this.isVisible)
			{
				this.isVisible = false;
				this.skeletonRenderer.OnBecameInvisible();
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000C1BC File Offset: 0x0000A3BC
		private void HandleRender(SkeletonRendererInstruction instruction)
		{
			int rendererCount = this.partsRenderers.Count;
			if (rendererCount <= 0)
			{
				return;
			}
			bool assignPropertyBlock = this.copyPropertyBlock && this.mainMeshRenderer.HasPropertyBlock();
			if (assignPropertyBlock)
			{
				this.mainMeshRenderer.GetPropertyBlock(this.copiedBlock);
			}
			MeshGenerator.Settings settings = new MeshGenerator.Settings
			{
				addNormals = this.skeletonRenderer.addNormals,
				calculateTangents = this.skeletonRenderer.calculateTangents,
				immutableTriangles = false,
				pmaVertexColors = this.skeletonRenderer.pmaVertexColors,
				tintBlack = this.skeletonRenderer.tintBlack,
				useClipping = true,
				zSpacing = this.skeletonRenderer.zSpacing
			};
			ExposedList<SubmeshInstruction> submeshInstructions = instruction.submeshInstructions;
			SubmeshInstruction[] submeshInstructionsItems = submeshInstructions.Items;
			int lastSubmeshInstruction = submeshInstructions.Count - 1;
			int rendererIndex = 0;
			SkeletonPartsRenderer currentRenderer = this.partsRenderers[rendererIndex];
			int si = 0;
			int start = 0;
			while (si <= lastSubmeshInstruction)
			{
				if (!(currentRenderer == null) && (submeshInstructionsItems[si].forceSeparate || si == lastSubmeshInstruction))
				{
					currentRenderer.MeshGenerator.settings = settings;
					if (assignPropertyBlock)
					{
						currentRenderer.SetPropertyBlock(this.copiedBlock);
					}
					currentRenderer.RenderParts(instruction.submeshInstructions, start, si + 1);
					start = si + 1;
					rendererIndex++;
					if (rendererIndex >= rendererCount)
					{
						break;
					}
					currentRenderer = this.partsRenderers[rendererIndex];
				}
				si++;
			}
			if (this.OnMeshAndMaterialsUpdated != null)
			{
				this.OnMeshAndMaterialsUpdated(this.skeletonRenderer);
			}
			while (rendererIndex < rendererCount)
			{
				currentRenderer = this.partsRenderers[rendererIndex];
				if (currentRenderer != null)
				{
					this.partsRenderers[rendererIndex].ClearMesh();
				}
				rendererIndex++;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000C378 File Offset: 0x0000A578
		protected void ClearPartsRendererMeshes()
		{
			foreach (SkeletonPartsRenderer partsRenderer in this.partsRenderers)
			{
				if (partsRenderer != null)
				{
					partsRenderer.ClearMesh();
				}
			}
		}

		// Token: 0x04000145 RID: 325
		public const int DefaultSortingOrderIncrement = 5;

		// Token: 0x04000146 RID: 326
		[SerializeField]
		protected SkeletonRenderer skeletonRenderer;

		// Token: 0x04000147 RID: 327
		private MeshRenderer mainMeshRenderer;

		// Token: 0x04000148 RID: 328
		public bool copyPropertyBlock = true;

		// Token: 0x04000149 RID: 329
		[Tooltip("Copies MeshRenderer flags into each parts renderer")]
		public bool copyMeshRendererFlags = true;

		// Token: 0x0400014A RID: 330
		public List<SkeletonPartsRenderer> partsRenderers = new List<SkeletonPartsRenderer>();

		// Token: 0x0400014B RID: 331
		[NonSerialized]
		public bool isVisible = true;

		// Token: 0x0400014D RID: 333
		private MaterialPropertyBlock copiedBlock;
	}
}
