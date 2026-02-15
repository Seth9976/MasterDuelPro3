using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spine.Unity
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	public class MeshGenerator
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000F5D4 File Offset: 0x0000D7D4
		public Vector2[] UV2
		{
			get
			{
				this.PrepareOptionalUVBuffer(ref this.uv2, this.vertexBuffer.Count);
				return this.uv2.Items;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000F5F8 File Offset: 0x0000D7F8
		public Vector2[] UV3
		{
			get
			{
				this.PrepareOptionalUVBuffer(ref this.uv3, this.vertexBuffer.Count);
				return this.uv3.Items;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000F61C File Offset: 0x0000D81C
		public int VertexCount
		{
			get
			{
				return this.vertexBuffer.Count;
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000F629 File Offset: 0x0000D829
		public int SubmeshIndexCount(int submeshIndex)
		{
			return this.submeshes.Items[submeshIndex].Count;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000F640 File Offset: 0x0000D840
		public MeshGeneratorBuffers Buffers
		{
			get
			{
				return new MeshGeneratorBuffers
				{
					vertexCount = this.VertexCount,
					vertexBuffer = this.vertexBuffer.Items,
					uvBuffer = this.uvBuffer.Items,
					colorBuffer = this.colorBuffer.Items,
					meshGenerator = this
				};
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public SkeletonClipping SkeletonClipping
		{
			get
			{
				return this.clipper;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000F6AC File Offset: 0x0000D8AC
		public MeshGenerator()
		{
			this.submeshes.TrimExcess();
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000F740 File Offset: 0x0000D940
		public static void GenerateSingleSubmeshInstruction(SkeletonRendererInstruction instructionOutput, Skeleton skeleton, Material material)
		{
			ExposedList<Slot> drawOrder = skeleton.DrawOrder;
			int drawOrderCount = drawOrder.Count;
			instructionOutput.Clear();
			ExposedList<SubmeshInstruction> workingSubmeshInstructions = instructionOutput.submeshInstructions;
			instructionOutput.attachments.Resize(drawOrderCount);
			Attachment[] workingAttachmentsItems = instructionOutput.attachments.Items;
			int totalRawVertexCount = 0;
			SubmeshInstruction current = new SubmeshInstruction
			{
				skeleton = skeleton,
				preActiveClippingSlotSource = -1,
				startSlot = 0,
				rawFirstVertexIndex = 0,
				material = material,
				forceSeparate = false,
				endSlot = drawOrderCount
			};
			object rendererObject = null;
			bool skeletonHasClipping = false;
			Slot[] drawOrderItems = drawOrder.Items;
			for (int i = 0; i < drawOrderCount; i++)
			{
				Slot slot = drawOrderItems[i];
				if (!slot.Bone.Active || slot.A == 0f)
				{
					workingAttachmentsItems[i] = null;
				}
				else
				{
					if (slot.Data.BlendMode == BlendMode.Additive)
					{
						current.hasPMAAdditiveSlot = true;
					}
					Attachment attachment = slot.Attachment;
					workingAttachmentsItems[i] = attachment;
					RegionAttachment regionAttachment = attachment as RegionAttachment;
					int attachmentVertexCount;
					int attachmentTriangleCount;
					if (regionAttachment != null)
					{
						if (regionAttachment.Sequence != null)
						{
							regionAttachment.Sequence.Apply(slot, regionAttachment);
						}
						rendererObject = regionAttachment.Region;
						attachmentVertexCount = 4;
						attachmentTriangleCount = 6;
					}
					else
					{
						MeshAttachment meshAttachment = attachment as MeshAttachment;
						if (meshAttachment != null)
						{
							if (meshAttachment.Sequence != null)
							{
								meshAttachment.Sequence.Apply(slot, meshAttachment);
							}
							rendererObject = meshAttachment.Region;
							attachmentVertexCount = meshAttachment.WorldVerticesLength >> 1;
							attachmentTriangleCount = meshAttachment.Triangles.Length;
						}
						else
						{
							if (attachment is ClippingAttachment)
							{
								current.hasClipping = true;
								skeletonHasClipping = true;
							}
							attachmentVertexCount = 0;
							attachmentTriangleCount = 0;
						}
					}
					current.rawTriangleCount += attachmentTriangleCount;
					current.rawVertexCount += attachmentVertexCount;
					totalRawVertexCount += attachmentVertexCount;
				}
			}
			if (material == null && rendererObject != null)
			{
				current.material = (Material)((AtlasRegion)rendererObject).page.rendererObject;
			}
			instructionOutput.hasActiveClipping = skeletonHasClipping;
			instructionOutput.rawVertexCount = totalRawVertexCount;
			if (totalRawVertexCount > 0)
			{
				workingSubmeshInstructions.Resize(1);
				workingSubmeshInstructions.Items[0] = current;
				return;
			}
			workingSubmeshInstructions.Resize(0);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000F954 File Offset: 0x0000DB54
		public static bool RequiresMultipleSubmeshesByDrawOrder(Skeleton skeleton)
		{
			ExposedList<Slot> drawOrder = skeleton.DrawOrder;
			int drawOrderCount = drawOrder.Count;
			Slot[] drawOrderItems = drawOrder.Items;
			Material lastRendererMaterial = null;
			for (int i = 0; i < drawOrderCount; i++)
			{
				Slot slot = drawOrderItems[i];
				if (slot.Bone.Active && slot.A != 0f)
				{
					IHasTextureRegion rendererAttachment = slot.Attachment as IHasTextureRegion;
					if (rendererAttachment != null)
					{
						if (rendererAttachment.Sequence != null)
						{
							rendererAttachment.Sequence.Apply(slot, rendererAttachment);
						}
						Material material = (Material)((AtlasRegion)rendererAttachment.Region).page.rendererObject;
						if (lastRendererMaterial != material)
						{
							if (lastRendererMaterial != null)
							{
								return true;
							}
							lastRendererMaterial = material;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000FA0C File Offset: 0x0000DC0C
		public static void GenerateSkeletonRendererInstruction(SkeletonRendererInstruction instructionOutput, Skeleton skeleton, Dictionary<Slot, Material> customSlotMaterials, List<Slot> separatorSlots, bool generateMeshOverride, bool immutableTriangles = false)
		{
			ExposedList<Slot> drawOrder = skeleton.DrawOrder;
			int drawOrderCount = drawOrder.Count;
			instructionOutput.Clear();
			ExposedList<SubmeshInstruction> workingSubmeshInstructions = instructionOutput.submeshInstructions;
			instructionOutput.attachments.Resize(drawOrderCount);
			Attachment[] workingAttachmentsItems = instructionOutput.attachments.Items;
			int totalRawVertexCount = 0;
			bool skeletonHasClipping = false;
			SubmeshInstruction current = new SubmeshInstruction
			{
				skeleton = skeleton,
				preActiveClippingSlotSource = -1
			};
			bool isCustomSlotMaterialsPopulated = customSlotMaterials != null && customSlotMaterials.Count > 0;
			int separatorCount = ((separatorSlots == null) ? 0 : separatorSlots.Count);
			bool hasSeparators = separatorCount > 0;
			int clippingAttachmentSource = -1;
			int lastPreActiveClipping = -1;
			SlotData clippingEndSlot = null;
			int submeshIndex = 0;
			Slot[] drawOrderItems = drawOrder.Items;
			for (int i = 0; i < drawOrderCount; i++)
			{
				Slot slot = drawOrderItems[i];
				if (!slot.Bone.Active || (slot.A == 0f && slot.Data != clippingEndSlot))
				{
					workingAttachmentsItems[i] = null;
				}
				else
				{
					if (slot.Data.BlendMode == BlendMode.Additive)
					{
						current.hasPMAAdditiveSlot = true;
					}
					Attachment attachment = slot.Attachment;
					workingAttachmentsItems[i] = attachment;
					int attachmentVertexCount = 0;
					int attachmentTriangleCount = 0;
					object region = null;
					bool noRender = false;
					RegionAttachment regionAttachment = attachment as RegionAttachment;
					if (regionAttachment != null)
					{
						if (regionAttachment.Sequence != null)
						{
							regionAttachment.Sequence.Apply(slot, regionAttachment);
						}
						region = regionAttachment.Region;
						attachmentVertexCount = 4;
						attachmentTriangleCount = 6;
					}
					else
					{
						MeshAttachment meshAttachment = attachment as MeshAttachment;
						if (meshAttachment != null)
						{
							if (meshAttachment.Sequence != null)
							{
								meshAttachment.Sequence.Apply(slot, meshAttachment);
							}
							region = meshAttachment.Region;
							attachmentVertexCount = meshAttachment.WorldVerticesLength >> 1;
							attachmentTriangleCount = meshAttachment.Triangles.Length;
						}
						else
						{
							ClippingAttachment clippingAttachment = attachment as ClippingAttachment;
							if (clippingAttachment != null)
							{
								clippingEndSlot = clippingAttachment.EndSlot;
								clippingAttachmentSource = i;
								current.hasClipping = true;
								skeletonHasClipping = true;
							}
							noRender = true;
						}
					}
					if (hasSeparators)
					{
						current.forceSeparate = false;
						for (int s = 0; s < separatorCount; s++)
						{
							if (slot == separatorSlots[s])
							{
								current.forceSeparate = true;
								break;
							}
						}
					}
					if (noRender)
					{
						if (current.forceSeparate && generateMeshOverride)
						{
							current.endSlot = i;
							current.preActiveClippingSlotSource = lastPreActiveClipping;
							workingSubmeshInstructions.Resize(submeshIndex + 1);
							workingSubmeshInstructions.Items[submeshIndex] = current;
							submeshIndex++;
							current.startSlot = i;
							lastPreActiveClipping = clippingAttachmentSource;
							current.rawTriangleCount = 0;
							current.rawVertexCount = 0;
							current.rawFirstVertexIndex = totalRawVertexCount;
							current.hasClipping = clippingAttachmentSource >= 0;
						}
					}
					else
					{
						Material material;
						if (isCustomSlotMaterialsPopulated)
						{
							if (!customSlotMaterials.TryGetValue(slot, out material))
							{
								material = (Material)((AtlasRegion)region).page.rendererObject;
							}
						}
						else
						{
							material = (Material)((AtlasRegion)region).page.rendererObject;
						}
						if (current.forceSeparate || (current.rawVertexCount > 0 && current.material != material))
						{
							current.endSlot = i;
							current.preActiveClippingSlotSource = lastPreActiveClipping;
							workingSubmeshInstructions.Resize(submeshIndex + 1);
							workingSubmeshInstructions.Items[submeshIndex] = current;
							submeshIndex++;
							current.startSlot = i;
							lastPreActiveClipping = clippingAttachmentSource;
							current.rawTriangleCount = 0;
							current.rawVertexCount = 0;
							current.rawFirstVertexIndex = totalRawVertexCount;
							current.hasClipping = clippingAttachmentSource >= 0;
						}
						current.material = material;
						current.rawTriangleCount += attachmentTriangleCount;
						current.rawVertexCount += attachmentVertexCount;
						current.rawFirstVertexIndex = totalRawVertexCount;
						totalRawVertexCount += attachmentVertexCount;
					}
					if (clippingEndSlot != null && slot.Data == clippingEndSlot && i != clippingAttachmentSource)
					{
						clippingEndSlot = null;
						clippingAttachmentSource = -1;
					}
				}
			}
			if (current.rawVertexCount > 0)
			{
				current.endSlot = drawOrderCount;
				current.preActiveClippingSlotSource = lastPreActiveClipping;
				current.forceSeparate = false;
				workingSubmeshInstructions.Resize(submeshIndex + 1);
				workingSubmeshInstructions.Items[submeshIndex] = current;
			}
			instructionOutput.hasActiveClipping = skeletonHasClipping;
			instructionOutput.rawVertexCount = totalRawVertexCount;
			instructionOutput.immutableTriangles = immutableTriangles;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		public static void TryReplaceMaterials(ExposedList<SubmeshInstruction> workingSubmeshInstructions, Dictionary<Material, Material> customMaterialOverride)
		{
			SubmeshInstruction[] wsii = workingSubmeshInstructions.Items;
			for (int i = 0; i < workingSubmeshInstructions.Count; i++)
			{
				Material material = wsii[i].material;
				Material overrideMaterial;
				if (!(material == null) && customMaterialOverride.TryGetValue(material, out overrideMaterial))
				{
					wsii[i].material = overrideMaterial;
				}
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000FE38 File Offset: 0x0000E038
		public void Begin()
		{
			this.vertexBuffer.Clear(false);
			this.colorBuffer.Clear(false);
			this.uvBuffer.Clear(false);
			this.clipper.ClipEnd();
			this.meshBoundsMin.x = float.PositiveInfinity;
			this.meshBoundsMin.y = float.PositiveInfinity;
			this.meshBoundsMax.x = float.NegativeInfinity;
			this.meshBoundsMax.y = float.NegativeInfinity;
			this.meshBoundsThickness = 0f;
			this.submeshIndex = 0;
			this.submeshes.Count = 1;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000FED4 File Offset: 0x0000E0D4
		public void AddSubmesh(SubmeshInstruction instruction, bool updateTriangles = true)
		{
			MeshGenerator.Settings settings = this.settings;
			int newSubmeshCount = this.submeshIndex + 1;
			if (this.submeshes.Items.Length < newSubmeshCount)
			{
				this.submeshes.Resize(newSubmeshCount);
			}
			this.submeshes.Count = newSubmeshCount;
			ExposedList<int> submesh = this.submeshes.Items[this.submeshIndex];
			if (submesh == null)
			{
				submesh = (this.submeshes.Items[this.submeshIndex] = new ExposedList<int>());
			}
			submesh.Clear(false);
			Skeleton skeleton = instruction.skeleton;
			Slot[] drawOrderItems = skeleton.DrawOrder.Items;
			Color32 color = default(Color32);
			float skeletonA = skeleton.A;
			float skeletonR = skeleton.R;
			float skeletonG = skeleton.G;
			float skeletonB = skeleton.B;
			Vector2 meshBoundsMin = this.meshBoundsMin;
			Vector2 meshBoundsMax = this.meshBoundsMax;
			float zSpacing = settings.zSpacing;
			bool pmaVertexColors = settings.pmaVertexColors;
			bool tintBlack = settings.tintBlack;
			bool linearColorSpace = QualitySettings.activeColorSpace == ColorSpace.Linear;
			bool useClipping = settings.useClipping && instruction.hasClipping;
			bool canvasGroupTintBlack = settings.tintBlack && settings.canvasGroupCompatible;
			if (useClipping && instruction.preActiveClippingSlotSource >= 0)
			{
				Slot slot = drawOrderItems[instruction.preActiveClippingSlotSource];
				this.clipper.ClipStart(slot, slot.Attachment as ClippingAttachment);
			}
			for (int slotIndex = instruction.startSlot; slotIndex < instruction.endSlot; slotIndex++)
			{
				Slot slot2 = drawOrderItems[slotIndex];
				if (!slot2.Bone.Active)
				{
					this.clipper.ClipEnd(slot2);
				}
				else
				{
					Attachment attachment = slot2.Attachment;
					float z = zSpacing * (float)slotIndex;
					float[] workingVerts = this.tempVerts;
					Color c = default(Color);
					RegionAttachment region = attachment as RegionAttachment;
					float[] uvs;
					int[] attachmentTriangleIndices;
					int attachmentVertexCount;
					int attachmentIndexCount;
					if (region != null)
					{
						region.ComputeWorldVertices(slot2, workingVerts, 0, 2);
						uvs = region.UVs;
						attachmentTriangleIndices = this.regionTriangles;
						c.r = region.R;
						c.g = region.G;
						c.b = region.B;
						c.a = region.A;
						attachmentVertexCount = 4;
						attachmentIndexCount = 6;
					}
					else
					{
						MeshAttachment mesh = attachment as MeshAttachment;
						if (mesh == null)
						{
							if (useClipping)
							{
								ClippingAttachment clippingAttachment = attachment as ClippingAttachment;
								if (clippingAttachment != null)
								{
									this.clipper.ClipStart(slot2, clippingAttachment);
									goto IL_0859;
								}
							}
							this.clipper.ClipEnd(slot2);
							goto IL_0859;
						}
						int meshVerticesLength = mesh.WorldVerticesLength;
						if (workingVerts.Length < meshVerticesLength)
						{
							workingVerts = new float[meshVerticesLength];
							this.tempVerts = workingVerts;
						}
						mesh.ComputeWorldVertices(slot2, 0, meshVerticesLength, workingVerts, 0, 2);
						uvs = mesh.UVs;
						attachmentTriangleIndices = mesh.Triangles;
						c.r = mesh.R;
						c.g = mesh.G;
						c.b = mesh.B;
						c.a = mesh.A;
						attachmentVertexCount = meshVerticesLength >> 1;
						attachmentIndexCount = mesh.Triangles.Length;
					}
					float tintBlackAlpha = 1f;
					if (pmaVertexColors)
					{
						float alpha = skeletonA * slot2.A * c.a;
						bool isAdditiveSlot = slot2.Data.BlendMode == BlendMode.Additive;
						if (linearColorSpace && isAdditiveSlot)
						{
							alpha = Mathf.LinearToGammaSpace(alpha);
						}
						color.a = (byte)(alpha * 255f);
						color.r = (byte)(skeletonR * slot2.R * c.r * (float)color.a);
						color.g = (byte)(skeletonG * slot2.G * c.g * (float)color.a);
						color.b = (byte)(skeletonB * slot2.B * c.b * (float)color.a);
						if (canvasGroupTintBlack)
						{
							tintBlackAlpha = (isAdditiveSlot ? 0f : alpha);
							color.a = byte.MaxValue;
						}
						else if (isAdditiveSlot)
						{
							color.a = 0;
						}
					}
					else
					{
						color.a = (byte)(skeletonA * slot2.A * c.a * 255f);
						color.r = (byte)(skeletonR * slot2.R * c.r * 255f);
						color.g = (byte)(skeletonG * slot2.G * c.g * 255f);
						color.b = (byte)(skeletonB * slot2.B * c.b * 255f);
					}
					if (useClipping && this.clipper.IsClipping)
					{
						this.clipper.ClipTriangles(workingVerts, attachmentTriangleIndices, attachmentIndexCount, uvs);
						workingVerts = this.clipper.ClippedVertices.Items;
						attachmentVertexCount = this.clipper.ClippedVertices.Count >> 1;
						attachmentTriangleIndices = this.clipper.ClippedTriangles.Items;
						attachmentIndexCount = this.clipper.ClippedTriangles.Count;
						uvs = this.clipper.ClippedUVs.Items;
					}
					if (attachmentVertexCount != 0 && attachmentIndexCount != 0)
					{
						if (tintBlack)
						{
							float r2 = slot2.R2;
							float g2 = slot2.G2;
							float b2 = slot2.B2;
							if (pmaVertexColors)
							{
								float alpha2 = skeletonA * slot2.A * c.a;
								bool isAdditiveSlot2 = slot2.Data.BlendMode == BlendMode.Additive;
								if (linearColorSpace && isAdditiveSlot2)
								{
									alpha2 = Mathf.LinearToGammaSpace(alpha2);
								}
								r2 *= alpha2;
								g2 *= alpha2;
								b2 *= alpha2;
							}
							this.AddAttachmentTintBlack(r2, g2, b2, tintBlackAlpha, attachmentVertexCount);
						}
						int ovc = this.vertexBuffer.Count;
						int newVertexCount = ovc + attachmentVertexCount;
						int oldArraySize = this.vertexBuffer.Items.Length;
						if (newVertexCount > oldArraySize)
						{
							int newArraySize = (int)((float)oldArraySize * 1.3f);
							if (newArraySize < newVertexCount)
							{
								newArraySize = newVertexCount;
							}
							Array.Resize<Vector3>(ref this.vertexBuffer.Items, newArraySize);
							Array.Resize<Vector2>(ref this.uvBuffer.Items, newArraySize);
							Array.Resize<Color32>(ref this.colorBuffer.Items, newArraySize);
						}
						this.vertexBuffer.Count = (this.uvBuffer.Count = (this.colorBuffer.Count = newVertexCount));
						Vector3[] vbi = this.vertexBuffer.Items;
						Vector2[] ubi = this.uvBuffer.Items;
						Color32[] cbi = this.colorBuffer.Items;
						if (ovc == 0)
						{
							for (int i = 0; i < attachmentVertexCount; i++)
							{
								int vi = ovc + i;
								int i2 = i << 1;
								float x = workingVerts[i2];
								float y = workingVerts[i2 + 1];
								vbi[vi].x = x;
								vbi[vi].y = y;
								vbi[vi].z = z;
								ubi[vi].x = uvs[i2];
								ubi[vi].y = uvs[i2 + 1];
								cbi[vi] = color;
								if (x < meshBoundsMin.x)
								{
									meshBoundsMin.x = x;
								}
								if (x > meshBoundsMax.x)
								{
									meshBoundsMax.x = x;
								}
								if (y < meshBoundsMin.y)
								{
									meshBoundsMin.y = y;
								}
								if (y > meshBoundsMax.y)
								{
									meshBoundsMax.y = y;
								}
							}
						}
						else
						{
							for (int j = 0; j < attachmentVertexCount; j++)
							{
								int vi2 = ovc + j;
								int i3 = j << 1;
								float x2 = workingVerts[i3];
								float y2 = workingVerts[i3 + 1];
								vbi[vi2].x = x2;
								vbi[vi2].y = y2;
								vbi[vi2].z = z;
								ubi[vi2].x = uvs[i3];
								ubi[vi2].y = uvs[i3 + 1];
								cbi[vi2] = color;
								if (x2 < meshBoundsMin.x)
								{
									meshBoundsMin.x = x2;
								}
								else if (x2 > meshBoundsMax.x)
								{
									meshBoundsMax.x = x2;
								}
								if (y2 < meshBoundsMin.y)
								{
									meshBoundsMin.y = y2;
								}
								else if (y2 > meshBoundsMax.y)
								{
									meshBoundsMax.y = y2;
								}
							}
						}
						if (updateTriangles)
						{
							int oldTriangleCount = submesh.Count;
							int newTriangleCount = oldTriangleCount + attachmentIndexCount;
							if (newTriangleCount > submesh.Items.Length)
							{
								Array.Resize<int>(ref submesh.Items, newTriangleCount);
							}
							submesh.Count = newTriangleCount;
							int[] submeshItems = submesh.Items;
							for (int k = 0; k < attachmentIndexCount; k++)
							{
								submeshItems[oldTriangleCount + k] = attachmentTriangleIndices[k] + ovc;
							}
						}
					}
					this.clipper.ClipEnd(slot2);
				}
				IL_0859:;
			}
			this.clipper.ClipEnd();
			this.meshBoundsMin = meshBoundsMin;
			this.meshBoundsMax = meshBoundsMax;
			this.meshBoundsThickness = (float)instruction.endSlot * zSpacing;
			int[] currentSubmeshItems = submesh.Items;
			int l = submesh.Count;
			int m = currentSubmeshItems.Length;
			while (l < m)
			{
				currentSubmeshItems[l] = 0;
				l++;
			}
			this.submeshIndex++;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000107B0 File Offset: 0x0000E9B0
		public void BuildMesh(SkeletonRendererInstruction instruction, bool updateTriangles)
		{
			SubmeshInstruction[] wsii = instruction.submeshInstructions.Items;
			int i = 0;
			int j = instruction.submeshInstructions.Count;
			while (i < j)
			{
				this.AddSubmesh(wsii[i], updateTriangles);
				i++;
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000107F0 File Offset: 0x0000E9F0
		public void BuildMeshWithArrays(SkeletonRendererInstruction instruction, bool updateTriangles)
		{
			MeshGenerator.Settings settings = this.settings;
			bool canvasGroupTintBlack = settings.tintBlack && settings.canvasGroupCompatible;
			int totalVertexCount = instruction.rawVertexCount;
			bool linearColorSpace = QualitySettings.activeColorSpace == ColorSpace.Linear;
			if (totalVertexCount > this.vertexBuffer.Items.Length)
			{
				Array.Resize<Vector3>(ref this.vertexBuffer.Items, totalVertexCount);
				Array.Resize<Vector2>(ref this.uvBuffer.Items, totalVertexCount);
				Array.Resize<Color32>(ref this.colorBuffer.Items, totalVertexCount);
			}
			this.vertexBuffer.Count = (this.uvBuffer.Count = (this.colorBuffer.Count = totalVertexCount));
			Color32 color = default(Color32);
			int vertexIndex = 0;
			float[] tempVerts = this.tempVerts;
			Vector2 bmin = this.meshBoundsMin;
			Vector2 bmax = this.meshBoundsMax;
			Vector3[] vbi = this.vertexBuffer.Items;
			Vector2[] ubi = this.uvBuffer.Items;
			Color32[] cbi = this.colorBuffer.Items;
			int lastSlotIndex = 0;
			int si = 0;
			int i = instruction.submeshInstructions.Count;
			while (si < i)
			{
				SubmeshInstruction submeshInstruction2 = instruction.submeshInstructions.Items[si];
				Skeleton skeleton = submeshInstruction2.skeleton;
				Slot[] drawOrderItems = skeleton.DrawOrder.Items;
				float a = skeleton.A;
				float r = skeleton.R;
				float g = skeleton.G;
				float b = skeleton.B;
				int endSlot = submeshInstruction2.endSlot;
				int startSlot = submeshInstruction2.startSlot;
				lastSlotIndex = endSlot;
				if (settings.tintBlack)
				{
					int vi = vertexIndex;
					Vector2 b2;
					b2.y = 1f;
					this.PrepareOptionalUVBuffer(ref this.uv2, totalVertexCount);
					this.PrepareOptionalUVBuffer(ref this.uv3, totalVertexCount);
					Vector2[] uv2i = this.uv2.Items;
					Vector2[] uv3i = this.uv3.Items;
					for (int slotIndex = startSlot; slotIndex < endSlot; slotIndex++)
					{
						Slot slot = drawOrderItems[slotIndex];
						if (slot.Bone.Active && slot.A != 0f)
						{
							Attachment attachment = slot.Attachment;
							Vector2 rg;
							rg.x = slot.R2;
							rg.y = slot.G2;
							b2.x = slot.B2;
							b2.y = 1f;
							RegionAttachment regionAttachment = attachment as RegionAttachment;
							if (regionAttachment != null)
							{
								if (settings.pmaVertexColors)
								{
									float alpha = a * slot.A * regionAttachment.A;
									bool isAdditiveSlot = slot.Data.BlendMode == BlendMode.Additive;
									if (linearColorSpace && isAdditiveSlot)
									{
										alpha = Mathf.LinearToGammaSpace(alpha);
									}
									rg.x *= alpha;
									rg.y *= alpha;
									b2.x *= alpha;
									b2.y = (isAdditiveSlot ? 0f : alpha);
								}
								uv2i[vi] = rg;
								uv2i[vi + 1] = rg;
								uv2i[vi + 2] = rg;
								uv2i[vi + 3] = rg;
								uv3i[vi] = b2;
								uv3i[vi + 1] = b2;
								uv3i[vi + 2] = b2;
								uv3i[vi + 3] = b2;
								vi += 4;
							}
							else
							{
								MeshAttachment meshAttachment = attachment as MeshAttachment;
								if (meshAttachment != null)
								{
									if (settings.pmaVertexColors)
									{
										float alpha2 = a * slot.A * meshAttachment.A;
										bool isAdditiveSlot2 = slot.Data.BlendMode == BlendMode.Additive;
										if (linearColorSpace && isAdditiveSlot2)
										{
											alpha2 = Mathf.LinearToGammaSpace(alpha2);
										}
										rg.x *= alpha2;
										rg.y *= alpha2;
										b2.x *= alpha2;
										b2.y = (isAdditiveSlot2 ? 0f : alpha2);
									}
									int verticesArrayLength = meshAttachment.WorldVerticesLength;
									for (int iii = 0; iii < verticesArrayLength; iii += 2)
									{
										uv2i[vi] = rg;
										uv3i[vi] = b2;
										vi++;
									}
								}
							}
						}
					}
				}
				for (int slotIndex2 = startSlot; slotIndex2 < endSlot; slotIndex2++)
				{
					Slot slot2 = drawOrderItems[slotIndex2];
					if (slot2.Bone.Active && slot2.A != 0f)
					{
						Attachment attachment2 = slot2.Attachment;
						float z = (float)slotIndex2 * settings.zSpacing;
						RegionAttachment regionAttachment2 = attachment2 as RegionAttachment;
						if (regionAttachment2 != null)
						{
							regionAttachment2.ComputeWorldVertices(slot2, tempVerts, 0, 2);
							float x = tempVerts[0];
							float y = tempVerts[1];
							float x2 = tempVerts[2];
							float y2 = tempVerts[3];
							float x3 = tempVerts[4];
							float y3 = tempVerts[5];
							float x4 = tempVerts[6];
							float y4 = tempVerts[7];
							vbi[vertexIndex].x = x;
							vbi[vertexIndex].y = y;
							vbi[vertexIndex].z = z;
							vbi[vertexIndex + 1].x = x4;
							vbi[vertexIndex + 1].y = y4;
							vbi[vertexIndex + 1].z = z;
							vbi[vertexIndex + 2].x = x2;
							vbi[vertexIndex + 2].y = y2;
							vbi[vertexIndex + 2].z = z;
							vbi[vertexIndex + 3].x = x3;
							vbi[vertexIndex + 3].y = y3;
							vbi[vertexIndex + 3].z = z;
							if (settings.pmaVertexColors)
							{
								float alpha3 = a * slot2.A * regionAttachment2.A;
								bool isAdditiveSlot3 = slot2.Data.BlendMode == BlendMode.Additive;
								if (linearColorSpace && isAdditiveSlot3)
								{
									alpha3 = Mathf.LinearToGammaSpace(alpha3);
								}
								color.a = (byte)(alpha3 * 255f);
								color.r = (byte)(r * slot2.R * regionAttachment2.R * (float)color.a);
								color.g = (byte)(g * slot2.G * regionAttachment2.G * (float)color.a);
								color.b = (byte)(b * slot2.B * regionAttachment2.B * (float)color.a);
								if (canvasGroupTintBlack)
								{
									color.a = byte.MaxValue;
								}
								else if (isAdditiveSlot3)
								{
									color.a = 0;
								}
							}
							else
							{
								color.a = (byte)(a * slot2.A * regionAttachment2.A * 255f);
								color.r = (byte)(r * slot2.R * regionAttachment2.R * 255f);
								color.g = (byte)(g * slot2.G * regionAttachment2.G * 255f);
								color.b = (byte)(b * slot2.B * regionAttachment2.B * 255f);
							}
							cbi[vertexIndex] = color;
							cbi[vertexIndex + 1] = color;
							cbi[vertexIndex + 2] = color;
							cbi[vertexIndex + 3] = color;
							float[] regionUVs = regionAttachment2.UVs;
							ubi[vertexIndex].x = regionUVs[0];
							ubi[vertexIndex].y = regionUVs[1];
							ubi[vertexIndex + 1].x = regionUVs[6];
							ubi[vertexIndex + 1].y = regionUVs[7];
							ubi[vertexIndex + 2].x = regionUVs[2];
							ubi[vertexIndex + 2].y = regionUVs[3];
							ubi[vertexIndex + 3].x = regionUVs[4];
							ubi[vertexIndex + 3].y = regionUVs[5];
							if (x < bmin.x)
							{
								bmin.x = x;
							}
							if (x > bmax.x)
							{
								bmax.x = x;
							}
							if (x2 < bmin.x)
							{
								bmin.x = x2;
							}
							else if (x2 > bmax.x)
							{
								bmax.x = x2;
							}
							if (x3 < bmin.x)
							{
								bmin.x = x3;
							}
							else if (x3 > bmax.x)
							{
								bmax.x = x3;
							}
							if (x4 < bmin.x)
							{
								bmin.x = x4;
							}
							else if (x4 > bmax.x)
							{
								bmax.x = x4;
							}
							if (y < bmin.y)
							{
								bmin.y = y;
							}
							if (y > bmax.y)
							{
								bmax.y = y;
							}
							if (y2 < bmin.y)
							{
								bmin.y = y2;
							}
							else if (y2 > bmax.y)
							{
								bmax.y = y2;
							}
							if (y3 < bmin.y)
							{
								bmin.y = y3;
							}
							else if (y3 > bmax.y)
							{
								bmax.y = y3;
							}
							if (y4 < bmin.y)
							{
								bmin.y = y4;
							}
							else if (y4 > bmax.y)
							{
								bmax.y = y4;
							}
							vertexIndex += 4;
						}
						else
						{
							MeshAttachment meshAttachment2 = attachment2 as MeshAttachment;
							if (meshAttachment2 != null)
							{
								int verticesArrayLength2 = meshAttachment2.WorldVerticesLength;
								if (tempVerts.Length < verticesArrayLength2)
								{
									tempVerts = (this.tempVerts = new float[verticesArrayLength2]);
								}
								meshAttachment2.ComputeWorldVertices(slot2, tempVerts);
								if (settings.pmaVertexColors)
								{
									float alpha4 = a * slot2.A * meshAttachment2.A;
									bool isAdditiveSlot4 = slot2.Data.BlendMode == BlendMode.Additive;
									if (linearColorSpace && isAdditiveSlot4)
									{
										alpha4 = Mathf.LinearToGammaSpace(alpha4);
									}
									color.a = (byte)(alpha4 * 255f);
									color.r = (byte)(r * slot2.R * meshAttachment2.R * (float)color.a);
									color.g = (byte)(g * slot2.G * meshAttachment2.G * (float)color.a);
									color.b = (byte)(b * slot2.B * meshAttachment2.B * (float)color.a);
									if (canvasGroupTintBlack)
									{
										color.a = byte.MaxValue;
									}
									else if (isAdditiveSlot4)
									{
										color.a = 0;
									}
								}
								else
								{
									color.a = (byte)(a * slot2.A * meshAttachment2.A * 255f);
									color.r = (byte)(r * slot2.R * meshAttachment2.R * 255f);
									color.g = (byte)(g * slot2.G * meshAttachment2.G * 255f);
									color.b = (byte)(b * slot2.B * meshAttachment2.B * 255f);
								}
								float[] attachmentUVs = meshAttachment2.UVs;
								if (vertexIndex == 0)
								{
									float fx = tempVerts[0];
									float fy = tempVerts[1];
									if (fx < bmin.x)
									{
										bmin.x = fx;
									}
									if (fx > bmax.x)
									{
										bmax.x = fx;
									}
									if (fy < bmin.y)
									{
										bmin.y = fy;
									}
									if (fy > bmax.y)
									{
										bmax.y = fy;
									}
								}
								for (int iii2 = 0; iii2 < verticesArrayLength2; iii2 += 2)
								{
									float x5 = tempVerts[iii2];
									float y5 = tempVerts[iii2 + 1];
									vbi[vertexIndex].x = x5;
									vbi[vertexIndex].y = y5;
									vbi[vertexIndex].z = z;
									cbi[vertexIndex] = color;
									ubi[vertexIndex].x = attachmentUVs[iii2];
									ubi[vertexIndex].y = attachmentUVs[iii2 + 1];
									if (x5 < bmin.x)
									{
										bmin.x = x5;
									}
									else if (x5 > bmax.x)
									{
										bmax.x = x5;
									}
									if (y5 < bmin.y)
									{
										bmin.y = y5;
									}
									else if (y5 > bmax.y)
									{
										bmax.y = y5;
									}
									vertexIndex++;
								}
							}
						}
					}
				}
				si++;
			}
			this.meshBoundsMin = bmin;
			this.meshBoundsMax = bmax;
			this.meshBoundsThickness = (float)lastSlotIndex * settings.zSpacing;
			int submeshInstructionCount = instruction.submeshInstructions.Count;
			this.submeshes.Count = submeshInstructionCount;
			if (updateTriangles)
			{
				if (this.submeshes.Items.Length < submeshInstructionCount)
				{
					this.submeshes.Resize(submeshInstructionCount);
					int j = 0;
					int k = submeshInstructionCount;
					while (j < k)
					{
						ExposedList<int> submeshBuffer = this.submeshes.Items[j];
						if (submeshBuffer == null)
						{
							this.submeshes.Items[j] = new ExposedList<int>();
						}
						else
						{
							submeshBuffer.Clear(false);
						}
						j++;
					}
				}
				SubmeshInstruction[] submeshInstructionsItems = instruction.submeshInstructions.Items;
				int attachmentFirstVertex = 0;
				for (int smbi = 0; smbi < submeshInstructionCount; smbi++)
				{
					SubmeshInstruction submeshInstruction = submeshInstructionsItems[smbi];
					ExposedList<int> currentSubmeshBuffer = this.submeshes.Items[smbi];
					int newTriangleCount = submeshInstruction.rawTriangleCount;
					if (newTriangleCount > currentSubmeshBuffer.Items.Length)
					{
						Array.Resize<int>(ref currentSubmeshBuffer.Items, newTriangleCount);
					}
					else if (newTriangleCount < currentSubmeshBuffer.Items.Length)
					{
						int[] sbi = currentSubmeshBuffer.Items;
						int ei = newTriangleCount;
						int nn = sbi.Length;
						while (ei < nn)
						{
							sbi[ei] = 0;
							ei++;
						}
					}
					currentSubmeshBuffer.Count = newTriangleCount;
					int[] tris = currentSubmeshBuffer.Items;
					int triangleIndex = 0;
					Slot[] drawOrderItems2 = submeshInstruction.skeleton.DrawOrder.Items;
					int slotIndex3 = submeshInstruction.startSlot;
					int endSlot2 = submeshInstruction.endSlot;
					while (slotIndex3 < endSlot2)
					{
						Slot slot3 = drawOrderItems2[slotIndex3];
						if (slot3.Bone.Active && slot3.A != 0f)
						{
							Attachment attachment3 = drawOrderItems2[slotIndex3].Attachment;
							if (attachment3 is RegionAttachment)
							{
								tris[triangleIndex] = attachmentFirstVertex;
								tris[triangleIndex + 1] = attachmentFirstVertex + 2;
								tris[triangleIndex + 2] = attachmentFirstVertex + 1;
								tris[triangleIndex + 3] = attachmentFirstVertex + 2;
								tris[triangleIndex + 4] = attachmentFirstVertex + 3;
								tris[triangleIndex + 5] = attachmentFirstVertex + 1;
								triangleIndex += 6;
								attachmentFirstVertex += 4;
							}
							else
							{
								MeshAttachment meshAttachment3 = attachment3 as MeshAttachment;
								if (meshAttachment3 != null)
								{
									int[] attachmentTriangles = meshAttachment3.Triangles;
									int ii = 0;
									int nn2 = attachmentTriangles.Length;
									while (ii < nn2)
									{
										tris[triangleIndex] = attachmentFirstVertex + attachmentTriangles[ii];
										ii++;
										triangleIndex++;
									}
									attachmentFirstVertex += meshAttachment3.WorldVerticesLength >> 1;
								}
							}
						}
						slotIndex3++;
					}
				}
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00011634 File Offset: 0x0000F834
		public void ScaleVertexData(float scale)
		{
			Vector3[] vbi = this.vertexBuffer.Items;
			int i = 0;
			int j = this.vertexBuffer.Count;
			while (i < j)
			{
				vbi[i] *= scale;
				i++;
			}
			this.meshBoundsMin *= scale;
			this.meshBoundsMax *= scale;
			this.meshBoundsThickness *= scale;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000116B0 File Offset: 0x0000F8B0
		public void ScaleAndOffsetVertexData(float scale, Vector2 offset2D)
		{
			Vector3 offset = new Vector3(offset2D.x, offset2D.y);
			Vector3[] vbi = this.vertexBuffer.Items;
			int i = 0;
			int j = this.vertexBuffer.Count;
			while (i < j)
			{
				vbi[i] = vbi[i] * scale + offset;
				i++;
			}
			this.meshBoundsMin *= scale;
			this.meshBoundsMax *= scale;
			this.meshBoundsMin += offset2D;
			this.meshBoundsMax += offset2D;
			this.meshBoundsThickness *= scale;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00011764 File Offset: 0x0000F964
		public Bounds GetMeshBounds()
		{
			if (float.IsInfinity(this.meshBoundsMin.x))
			{
				return default(Bounds);
			}
			float halfWidth = (this.meshBoundsMax.x - this.meshBoundsMin.x) * 0.5f;
			float halfHeight = (this.meshBoundsMax.y - this.meshBoundsMin.y) * 0.5f;
			return new Bounds
			{
				center = new Vector3(this.meshBoundsMin.x + halfWidth, this.meshBoundsMin.y + halfHeight),
				extents = new Vector3(halfWidth, halfHeight, this.meshBoundsThickness * 0.5f)
			};
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00011814 File Offset: 0x0000FA14
		private void AddAttachmentTintBlack(float r2, float g2, float b2, float a, int vertexCount)
		{
			Vector2 rg = new Vector2(r2, g2);
			Vector2 bo = new Vector2(b2, a);
			int ovc = this.vertexBuffer.Count;
			int newVertexCount = ovc + vertexCount;
			this.PrepareOptionalUVBuffer(ref this.uv2, newVertexCount);
			this.PrepareOptionalUVBuffer(ref this.uv3, newVertexCount);
			Vector2[] uv2i = this.uv2.Items;
			Vector2[] uv3i = this.uv3.Items;
			for (int i = 0; i < vertexCount; i++)
			{
				uv2i[ovc + i] = rg;
				uv3i[ovc + i] = bo;
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000118A2 File Offset: 0x0000FAA2
		private void PrepareOptionalUVBuffer(ref ExposedList<Vector2> uvBuffer, int vertexCount)
		{
			if (uvBuffer == null)
			{
				uvBuffer = new ExposedList<Vector2>();
			}
			if (vertexCount > uvBuffer.Items.Length)
			{
				Array.Resize<Vector2>(ref uvBuffer.Items, vertexCount);
			}
			uvBuffer.Count = vertexCount;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000118D0 File Offset: 0x0000FAD0
		private void ResizeOptionalUVBuffer(ref ExposedList<Vector2> uvBuffer, int vertexCount)
		{
			if (uvBuffer != null && vertexCount != uvBuffer.Items.Length)
			{
				Array.Resize<Vector2>(ref uvBuffer.Items, vertexCount);
				uvBuffer.Count = vertexCount;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000118F8 File Offset: 0x0000FAF8
		public void FillVertexData(Mesh mesh)
		{
			Vector3[] vbi = this.vertexBuffer.Items;
			Vector2[] ubi = this.uvBuffer.Items;
			Color32[] cbi = this.colorBuffer.Items;
			int vbiLength = vbi.Length;
			int count = this.vertexBuffer.Count;
			Vector3 vector3zero = Vector3.zero;
			for (int i = count; i < vbiLength; i++)
			{
				vbi[i] = vector3zero;
			}
			mesh.vertices = vbi;
			mesh.uv = ubi;
			mesh.colors32 = cbi;
			mesh.bounds = this.GetMeshBounds();
			if (this.settings.addNormals)
			{
				int oldLength = 0;
				if (this.normals == null)
				{
					this.normals = new Vector3[vbiLength];
				}
				else
				{
					oldLength = this.normals.Length;
				}
				if (oldLength != vbiLength)
				{
					Array.Resize<Vector3>(ref this.normals, vbiLength);
					Vector3[] localNormals = this.normals;
					for (int j = oldLength; j < vbiLength; j++)
					{
						localNormals[j] = Vector3.back;
					}
				}
				mesh.normals = this.normals;
			}
			this.ResizeOptionalUVBuffer(ref this.uv2, vbiLength);
			this.ResizeOptionalUVBuffer(ref this.uv3, vbiLength);
			mesh.uv2 = ((this.uv2 == null) ? null : this.uv2.Items);
			mesh.uv3 = ((this.uv3 == null) ? null : this.uv3.Items);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00011A40 File Offset: 0x0000FC40
		public void FillLateVertexData(Mesh mesh)
		{
			if (this.settings.calculateTangents)
			{
				int vertexCount = this.vertexBuffer.Count;
				ExposedList<int>[] sbi = this.submeshes.Items;
				int submeshCount = this.submeshes.Count;
				Vector3[] vbi = this.vertexBuffer.Items;
				Vector2[] ubi = this.uvBuffer.Items;
				MeshGenerator.SolveTangents2DEnsureSize(ref this.tangents, ref this.tempTanBuffer, vertexCount, vbi.Length);
				for (int i = 0; i < submeshCount; i++)
				{
					int[] submesh = sbi[i].Items;
					int triangleCount = sbi[i].Count;
					MeshGenerator.SolveTangents2DTriangles(this.tempTanBuffer, submesh, triangleCount, vbi, ubi, vertexCount);
				}
				MeshGenerator.SolveTangents2DBuffer(this.tangents, this.tempTanBuffer, vertexCount);
				mesh.tangents = this.tangents;
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00011B08 File Offset: 0x0000FD08
		public void FillTriangles(Mesh mesh)
		{
			int submeshCount = this.submeshes.Count;
			ExposedList<int>[] submeshesItems = this.submeshes.Items;
			mesh.subMeshCount = submeshCount;
			for (int i = 0; i < submeshCount; i++)
			{
				mesh.SetTriangles(submeshesItems[i].Items, 0, submeshesItems[i].Count, i, false, 0);
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00011B5C File Offset: 0x0000FD5C
		public void EnsureVertexCapacity(int minimumVertexCount, bool inlcudeTintBlack = false, bool includeTangents = false, bool includeNormals = false)
		{
			if (minimumVertexCount > this.vertexBuffer.Items.Length)
			{
				Array.Resize<Vector3>(ref this.vertexBuffer.Items, minimumVertexCount);
				Array.Resize<Vector2>(ref this.uvBuffer.Items, minimumVertexCount);
				Array.Resize<Color32>(ref this.colorBuffer.Items, minimumVertexCount);
				if (inlcudeTintBlack)
				{
					if (this.uv2 == null)
					{
						this.uv2 = new ExposedList<Vector2>(minimumVertexCount);
						this.uv3 = new ExposedList<Vector2>(minimumVertexCount);
					}
					this.uv2.Resize(minimumVertexCount);
					this.uv3.Resize(minimumVertexCount);
				}
				if (includeNormals)
				{
					if (this.normals == null)
					{
						this.normals = new Vector3[minimumVertexCount];
					}
					else
					{
						Array.Resize<Vector3>(ref this.normals, minimumVertexCount);
					}
				}
				if (includeTangents)
				{
					if (this.tangents == null)
					{
						this.tangents = new Vector4[minimumVertexCount];
						return;
					}
					Array.Resize<Vector4>(ref this.tangents, minimumVertexCount);
				}
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00011C38 File Offset: 0x0000FE38
		public void TrimExcess()
		{
			this.vertexBuffer.TrimExcess();
			this.uvBuffer.TrimExcess();
			this.colorBuffer.TrimExcess();
			if (this.uv2 != null)
			{
				this.uv2.TrimExcess();
			}
			if (this.uv3 != null)
			{
				this.uv3.TrimExcess();
			}
			int vbiLength = this.vertexBuffer.Items.Length;
			if (this.normals != null)
			{
				Array.Resize<Vector3>(ref this.normals, vbiLength);
			}
			if (this.tangents != null)
			{
				Array.Resize<Vector4>(ref this.tangents, vbiLength);
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00011CC2 File Offset: 0x0000FEC2
		internal static void SolveTangents2DEnsureSize(ref Vector4[] tangentBuffer, ref Vector2[] tempTanBuffer, int vertexCount, int vertexBufferLength)
		{
			if (tangentBuffer == null || tangentBuffer.Length != vertexBufferLength)
			{
				tangentBuffer = new Vector4[vertexBufferLength];
			}
			if (tempTanBuffer == null || tempTanBuffer.Length < vertexCount * 2)
			{
				tempTanBuffer = new Vector2[vertexCount * 2];
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00011CF0 File Offset: 0x0000FEF0
		internal static void SolveTangents2DTriangles(Vector2[] tempTanBuffer, int[] triangles, int triangleCount, Vector3[] vertices, Vector2[] uvs, int vertexCount)
		{
			for (int t = 0; t < triangleCount; t += 3)
			{
				int i = triangles[t];
				int i2 = triangles[t + 1];
				int i3 = triangles[t + 2];
				Vector3 v = vertices[i];
				Vector3 vector = vertices[i2];
				Vector3 v2 = vertices[i3];
				Vector2 w = uvs[i];
				Vector2 w2 = uvs[i2];
				Vector2 w3 = uvs[i3];
				float x = vector.x - v.x;
				float x2 = v2.x - v.x;
				float y = vector.y - v.y;
				float y2 = v2.y - v.y;
				float s = w2.x - w.x;
				float s2 = w3.x - w.x;
				float t2 = w2.y - w.y;
				float t3 = w3.y - w.y;
				float div = s * t3 - s2 * t2;
				float r = ((div == 0f) ? 0f : (1f / div));
				Vector2 sdir;
				sdir.x = (t3 * x - t2 * x2) * r;
				sdir.y = (t3 * y - t2 * y2) * r;
				tempTanBuffer[i] = (tempTanBuffer[i2] = (tempTanBuffer[i3] = sdir));
				Vector2 tdir;
				tdir.x = (s * x2 - s2 * x) * r;
				tdir.y = (s * y2 - s2 * y) * r;
				tempTanBuffer[vertexCount + i] = (tempTanBuffer[vertexCount + i2] = (tempTanBuffer[vertexCount + i3] = tdir));
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00011EAC File Offset: 0x000100AC
		internal static void SolveTangents2DBuffer(Vector4[] tangents, Vector2[] tempTanBuffer, int vertexCount)
		{
			Vector4 tangent;
			tangent.z = 0f;
			for (int i = 0; i < vertexCount; i++)
			{
				Vector2 t = tempTanBuffer[i];
				float magnitude = Mathf.Sqrt(t.x * t.x + t.y * t.y);
				if ((double)magnitude > 1E-05)
				{
					float reciprocalMagnitude = 1f / magnitude;
					t.x *= reciprocalMagnitude;
					t.y *= reciprocalMagnitude;
				}
				Vector2 t2 = tempTanBuffer[vertexCount + i];
				tangent.x = t.x;
				tangent.y = t.y;
				tangent.w = (float)((t.y * t2.x > t.x * t2.y) ? 1 : (-1));
				tangents[i] = tangent;
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00011F88 File Offset: 0x00010188
		public static void FillMeshLocal(Mesh mesh, RegionAttachment regionAttachment)
		{
			if (mesh == null)
			{
				return;
			}
			if (regionAttachment == null)
			{
				return;
			}
			MeshGenerator.AttachmentVerts.Clear();
			float[] offsets = regionAttachment.Offset;
			MeshGenerator.AttachmentVerts.Add(new Vector3(offsets[0], offsets[1]));
			MeshGenerator.AttachmentVerts.Add(new Vector3(offsets[2], offsets[3]));
			MeshGenerator.AttachmentVerts.Add(new Vector3(offsets[4], offsets[5]));
			MeshGenerator.AttachmentVerts.Add(new Vector3(offsets[6], offsets[7]));
			MeshGenerator.AttachmentUVs.Clear();
			float[] uvs = regionAttachment.UVs;
			MeshGenerator.AttachmentUVs.Add(new Vector2(uvs[2], uvs[3]));
			MeshGenerator.AttachmentUVs.Add(new Vector2(uvs[4], uvs[5]));
			MeshGenerator.AttachmentUVs.Add(new Vector2(uvs[6], uvs[7]));
			MeshGenerator.AttachmentUVs.Add(new Vector2(uvs[0], uvs[1]));
			MeshGenerator.AttachmentColors32.Clear();
			Color32 c = new Color(regionAttachment.R, regionAttachment.G, regionAttachment.B, regionAttachment.A);
			for (int i = 0; i < 4; i++)
			{
				MeshGenerator.AttachmentColors32.Add(c);
			}
			MeshGenerator.AttachmentIndices.Clear();
			MeshGenerator.AttachmentIndices.AddRange(new int[] { 0, 2, 1, 0, 3, 2 });
			mesh.Clear();
			mesh.name = regionAttachment.Name;
			mesh.SetVertices(MeshGenerator.AttachmentVerts);
			mesh.SetUVs(0, MeshGenerator.AttachmentUVs);
			mesh.SetColors(MeshGenerator.AttachmentColors32);
			mesh.SetTriangles(MeshGenerator.AttachmentIndices, 0);
			mesh.RecalculateBounds();
			MeshGenerator.AttachmentVerts.Clear();
			MeshGenerator.AttachmentUVs.Clear();
			MeshGenerator.AttachmentColors32.Clear();
			MeshGenerator.AttachmentIndices.Clear();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00012144 File Offset: 0x00010344
		public static void FillMeshLocal(Mesh mesh, MeshAttachment meshAttachment, SkeletonData skeletonData)
		{
			if (mesh == null)
			{
				return;
			}
			if (meshAttachment == null)
			{
				return;
			}
			int vertexCount = meshAttachment.WorldVerticesLength / 2;
			MeshGenerator.AttachmentVerts.Clear();
			if (meshAttachment.IsWeighted())
			{
				int count = meshAttachment.WorldVerticesLength;
				int[] meshAttachmentBones = meshAttachment.Bones;
				int v = 0;
				float[] vertices = meshAttachment.Vertices;
				int w = 0;
				int b = 0;
				while (w < count)
				{
					float wx = 0f;
					float wy = 0f;
					int i = meshAttachmentBones[v++];
					i += v;
					while (v < i)
					{
						BoneMatrix bm = BoneMatrix.CalculateSetupWorld(skeletonData.Bones.Items[meshAttachmentBones[v]]);
						float vx = vertices[b];
						float vy = vertices[b + 1];
						float weight = vertices[b + 2];
						wx += (vx * bm.a + vy * bm.b + bm.x) * weight;
						wy += (vx * bm.c + vy * bm.d + bm.y) * weight;
						v++;
						b += 3;
					}
					MeshGenerator.AttachmentVerts.Add(new Vector3(wx, wy));
					w += 2;
				}
			}
			else
			{
				float[] localVerts = meshAttachment.Vertices;
				Vector3 pos = default(Vector3);
				for (int j = 0; j < vertexCount; j++)
				{
					int ii = j * 2;
					pos.x = localVerts[ii];
					pos.y = localVerts[ii + 1];
					MeshGenerator.AttachmentVerts.Add(pos);
				}
			}
			float[] uvs = meshAttachment.UVs;
			Vector2 uv = default(Vector2);
			Color32 c = new Color(meshAttachment.R, meshAttachment.G, meshAttachment.B, meshAttachment.A);
			MeshGenerator.AttachmentUVs.Clear();
			MeshGenerator.AttachmentColors32.Clear();
			for (int k = 0; k < vertexCount; k++)
			{
				int ii2 = k * 2;
				uv.x = uvs[ii2];
				uv.y = uvs[ii2 + 1];
				MeshGenerator.AttachmentUVs.Add(uv);
				MeshGenerator.AttachmentColors32.Add(c);
			}
			MeshGenerator.AttachmentIndices.Clear();
			MeshGenerator.AttachmentIndices.AddRange(meshAttachment.Triangles);
			mesh.Clear();
			mesh.name = meshAttachment.Name;
			mesh.SetVertices(MeshGenerator.AttachmentVerts);
			mesh.SetUVs(0, MeshGenerator.AttachmentUVs);
			mesh.SetColors(MeshGenerator.AttachmentColors32);
			mesh.SetTriangles(MeshGenerator.AttachmentIndices, 0);
			mesh.RecalculateBounds();
			MeshGenerator.AttachmentVerts.Clear();
			MeshGenerator.AttachmentUVs.Clear();
			MeshGenerator.AttachmentColors32.Clear();
			MeshGenerator.AttachmentIndices.Clear();
		}

		// Token: 0x040001DB RID: 475
		public MeshGenerator.Settings settings = MeshGenerator.Settings.Default;

		// Token: 0x040001DC RID: 476
		private const float BoundsMinDefault = float.PositiveInfinity;

		// Token: 0x040001DD RID: 477
		private const float BoundsMaxDefault = float.NegativeInfinity;

		// Token: 0x040001DE RID: 478
		[NonSerialized]
		protected readonly ExposedList<Vector3> vertexBuffer = new ExposedList<Vector3>(4);

		// Token: 0x040001DF RID: 479
		[NonSerialized]
		protected readonly ExposedList<Vector2> uvBuffer = new ExposedList<Vector2>(4);

		// Token: 0x040001E0 RID: 480
		[NonSerialized]
		protected readonly ExposedList<Color32> colorBuffer = new ExposedList<Color32>(4);

		// Token: 0x040001E1 RID: 481
		[NonSerialized]
		protected readonly ExposedList<ExposedList<int>> submeshes = new ExposedList<ExposedList<int>>
		{
			new ExposedList<int>(6)
		};

		// Token: 0x040001E2 RID: 482
		[NonSerialized]
		private Vector2 meshBoundsMin;

		// Token: 0x040001E3 RID: 483
		[NonSerialized]
		private Vector2 meshBoundsMax;

		// Token: 0x040001E4 RID: 484
		[NonSerialized]
		private float meshBoundsThickness;

		// Token: 0x040001E5 RID: 485
		[NonSerialized]
		private int submeshIndex;

		// Token: 0x040001E6 RID: 486
		[NonSerialized]
		private SkeletonClipping clipper = new SkeletonClipping();

		// Token: 0x040001E7 RID: 487
		[NonSerialized]
		private float[] tempVerts = new float[8];

		// Token: 0x040001E8 RID: 488
		[NonSerialized]
		private int[] regionTriangles = new int[] { 0, 1, 2, 2, 3, 0 };

		// Token: 0x040001E9 RID: 489
		[NonSerialized]
		private Vector3[] normals;

		// Token: 0x040001EA RID: 490
		[NonSerialized]
		private Vector4[] tangents;

		// Token: 0x040001EB RID: 491
		[NonSerialized]
		private Vector2[] tempTanBuffer;

		// Token: 0x040001EC RID: 492
		[NonSerialized]
		private ExposedList<Vector2> uv2;

		// Token: 0x040001ED RID: 493
		[NonSerialized]
		private ExposedList<Vector2> uv3;

		// Token: 0x040001EE RID: 494
		private static List<Vector3> AttachmentVerts = new List<Vector3>();

		// Token: 0x040001EF RID: 495
		private static List<Vector2> AttachmentUVs = new List<Vector2>();

		// Token: 0x040001F0 RID: 496
		private static List<Color32> AttachmentColors32 = new List<Color32>();

		// Token: 0x040001F1 RID: 497
		private static List<int> AttachmentIndices = new List<int>();

		// Token: 0x02000061 RID: 97
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000074 RID: 116
			// (get) Token: 0x06000317 RID: 791 RVA: 0x00012408 File Offset: 0x00010608
			public static MeshGenerator.Settings Default
			{
				get
				{
					return new MeshGenerator.Settings
					{
						pmaVertexColors = true,
						zSpacing = 0f,
						useClipping = true,
						tintBlack = false,
						calculateTangents = false,
						addNormals = false,
						immutableTriangles = false
					};
				}
			}

			// Token: 0x040001F2 RID: 498
			public bool useClipping;

			// Token: 0x040001F3 RID: 499
			[Range(-0.1f, 0f)]
			public float zSpacing;

			// Token: 0x040001F4 RID: 500
			public bool tintBlack;

			// Token: 0x040001F5 RID: 501
			[FormerlySerializedAs("canvasGroupTintBlack")]
			[Tooltip("Enable when using SkeletonGraphic under a CanvasGroup. When enabled, PMA Vertex Color alpha value is stored at uv2.g instead of color.a to capture CanvasGroup modifying color.a. Also helps to detect correct parameter setting combinations.")]
			public bool canvasGroupCompatible;

			// Token: 0x040001F6 RID: 502
			public bool pmaVertexColors;

			// Token: 0x040001F7 RID: 503
			public bool addNormals;

			// Token: 0x040001F8 RID: 504
			public bool calculateTangents;

			// Token: 0x040001F9 RID: 505
			public bool immutableTriangles;
		}
	}
}
