using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000520 RID: 1312
	internal class EntryProcessor
	{
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x00089789 File Offset: 0x00087989
		// (set) Token: 0x0600245F RID: 9311 RVA: 0x00089791 File Offset: 0x00087991
		public RenderChainCommand firstHeadCommand { get; private set; }

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x0008979A File Offset: 0x0008799A
		// (set) Token: 0x06002461 RID: 9313 RVA: 0x000897A2 File Offset: 0x000879A2
		public RenderChainCommand lastHeadCommand { get; private set; }

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x000897AB File Offset: 0x000879AB
		// (set) Token: 0x06002463 RID: 9315 RVA: 0x000897B3 File Offset: 0x000879B3
		public RenderChainCommand firstTailCommand { get; private set; }

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x000897BC File Offset: 0x000879BC
		// (set) Token: 0x06002465 RID: 9317 RVA: 0x000897C4 File Offset: 0x000879C4
		public RenderChainCommand lastTailCommand { get; private set; }

		// Token: 0x06002466 RID: 9318 RVA: 0x000897D0 File Offset: 0x000879D0
		public void Init(Entry root, RenderChain renderChain, VisualElement ve)
		{
			UIRenderDevice device = renderChain.device;
			this.m_RenderChain = renderChain;
			this.m_CurrentElement = ve;
			this.m_PreProcessor.PreProcess(root);
			bool flag = this.m_PreProcessor.headAllocs.Count == 0 && ve.renderChainData.headMesh != null;
			if (flag)
			{
				device.Free(ve.renderChainData.headMesh);
				ve.renderChainData.headMesh = null;
			}
			bool flag2 = this.m_PreProcessor.tailAllocs.Count == 0 && ve.renderChainData.tailMesh != null;
			if (flag2)
			{
				device.Free(ve.renderChainData.tailMesh);
				ve.renderChainData.tailMesh = null;
			}
			bool hasExtraMeshes = ve.renderChainData.hasExtraMeshes;
			if (hasExtraMeshes)
			{
				renderChain.FreeExtraMeshes(ve);
			}
			renderChain.ResetTextures(ve);
			VisualElement parent = this.m_CurrentElement.hierarchy.parent;
			bool isGroupTransform = this.m_CurrentElement.renderChainData.isGroupTransform;
			bool flag3 = parent != null;
			if (flag3)
			{
				this.m_MaskDepthPopped = parent.renderChainData.childrenMaskDepth;
				this.m_StencilRefPopped = parent.renderChainData.childrenStencilRef;
				this.m_ClipRectIdPopped = (isGroupTransform ? UIRVEShaderInfoAllocator.infiniteClipRect : parent.renderChainData.clipRectID);
			}
			else
			{
				this.m_MaskDepthPopped = 0;
				this.m_StencilRefPopped = 0;
				this.m_ClipRectIdPopped = UIRVEShaderInfoAllocator.infiniteClipRect;
			}
			this.m_MaskDepthPushed = this.m_MaskDepthPopped + 1;
			this.m_StencilRefPushed = this.m_MaskDepthPopped;
			this.m_ClipRectIdPushed = this.m_CurrentElement.renderChainData.clipRectID;
			this.m_MaskDepth = this.m_MaskDepthPopped;
			this.m_StencilRef = this.m_StencilRefPopped;
			this.m_ClipRectId = this.m_ClipRectIdPopped;
			this.m_VertexDataComputed = false;
			this.m_Transform = Matrix4x4.identity;
			this.m_TextCoreSettingsPage = new Color32(0, 0, 0, 0);
			this.m_MaskMeshes.Clear();
			this.m_IsDrawingMask = false;
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000899C8 File Offset: 0x00087BC8
		public void ProcessHead()
		{
			this.m_IsTail = false;
			this.ProcessFirstAlloc(this.m_PreProcessor.headAllocs, ref this.m_CurrentElement.renderChainData.headMesh);
			this.m_FirstCommand = null;
			this.m_LastCommand = null;
			this.ProcessRange(0, this.m_PreProcessor.childrenIndex - 1);
			this.firstHeadCommand = this.m_FirstCommand;
			this.lastHeadCommand = this.m_LastCommand;
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x00089A3C File Offset: 0x00087C3C
		public void ProcessTail()
		{
			this.m_IsTail = true;
			this.ProcessFirstAlloc(this.m_PreProcessor.tailAllocs, ref this.m_CurrentElement.renderChainData.tailMesh);
			this.m_FirstCommand = null;
			this.m_LastCommand = null;
			this.ProcessRange(this.m_PreProcessor.childrenIndex + 1, this.m_PreProcessor.flattenedEntries.Count - 1);
			this.firstTailCommand = this.m_FirstCommand;
			this.lastTailCommand = this.m_LastCommand;
			Debug.Assert(this.m_MaskDepth == this.m_MaskDepthPopped);
			Debug.Assert(this.m_MaskMeshes.Count == 0);
			Debug.Assert(!this.m_IsDrawingMask);
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x00089AF8 File Offset: 0x00087CF8
		private void ProcessRange(int first, int last)
		{
			List<Entry> entries = this.m_PreProcessor.flattenedEntries;
			for (int i = first; i <= last; i++)
			{
				Entry entry = entries[i];
				switch (entry.type)
				{
				case EntryType.DrawSolidMesh:
					this.m_RenderType = VertexFlags.IsSolid;
					this.ProcessMeshEntry(entry, TextureId.invalid);
					break;
				case EntryType.DrawTexturedMesh:
				{
					Texture texture = entry.texture;
					TextureId textureId = TextureId.invalid;
					bool flag = texture != null;
					if (flag)
					{
						RectInt atlasRect;
						bool flag2 = this.m_RenderChain.atlas != null && this.m_RenderChain.atlas.TryGetAtlas(this.m_CurrentElement, texture as Texture2D, out textureId, out atlasRect);
						if (flag2)
						{
							this.m_RenderType = VertexFlags.IsDynamic;
							this.m_AtlasRect = new Rect((float)atlasRect.x, (float)atlasRect.y, (float)atlasRect.width, (float)atlasRect.height);
							this.m_RemapUVs = true;
							this.m_RenderChain.InsertTexture(this.m_CurrentElement, texture, textureId, true);
						}
						else
						{
							this.m_RenderType = VertexFlags.IsTextured;
							textureId = TextureRegistry.instance.Acquire(texture);
							this.m_RenderChain.InsertTexture(this.m_CurrentElement, texture, textureId, false);
						}
					}
					else
					{
						this.m_RenderType = VertexFlags.IsSolid;
					}
					this.ProcessMeshEntry(entry, textureId);
					this.m_RemapUVs = false;
					break;
				}
				case EntryType.DrawTexturedMeshSkipAtlas:
				{
					this.m_RenderType = VertexFlags.IsTextured;
					TextureId textureId2 = TextureRegistry.instance.Acquire(entry.texture);
					this.m_RenderChain.InsertTexture(this.m_CurrentElement, entry.texture, textureId2, false);
					this.ProcessMeshEntry(entry, textureId2);
					break;
				}
				case EntryType.DrawTextMesh:
				{
					this.m_RenderType = VertexFlags.IsText;
					TextureId textureId3 = TextureRegistry.instance.Acquire(entry.texture);
					this.m_RenderChain.InsertTexture(this.m_CurrentElement, entry.texture, textureId3, false);
					this.ProcessMeshEntry(entry, textureId3);
					break;
				}
				case EntryType.DrawGradients:
				{
					this.m_RenderType = VertexFlags.IsSvgGradients;
					GradientRemap gradientRemap = this.m_RenderChain.vectorImageManager.AddUser(entry.gradientsOwner, this.m_CurrentElement);
					this.m_GradientSettingIndexOffset = gradientRemap.destIndex;
					bool flag3 = gradientRemap.atlas != TextureId.invalid;
					TextureId textureId4;
					if (flag3)
					{
						textureId4 = gradientRemap.atlas;
					}
					else
					{
						textureId4 = TextureRegistry.instance.Acquire(entry.gradientsOwner.atlas);
						this.m_RenderChain.InsertTexture(this.m_CurrentElement, entry.gradientsOwner.atlas, textureId4, false);
					}
					this.ProcessMeshEntry(entry, textureId4);
					this.m_GradientSettingIndexOffset = -1;
					break;
				}
				case EntryType.DrawImmediate:
				{
					RenderChainCommand cmd = this.m_RenderChain.AllocCommand();
					cmd.type = CommandType.Immediate;
					cmd.owner = this.m_CurrentElement;
					cmd.isTail = this.m_IsTail;
					cmd.callback = entry.immediateCallback;
					this.AppendCommand(cmd);
					break;
				}
				case EntryType.DrawImmediateCull:
				{
					RenderChainCommand cmd2 = this.m_RenderChain.AllocCommand();
					cmd2.type = CommandType.ImmediateCull;
					cmd2.owner = this.m_CurrentElement;
					cmd2.isTail = this.m_IsTail;
					cmd2.callback = entry.immediateCallback;
					this.AppendCommand(cmd2);
					break;
				}
				case EntryType.DrawChildren:
				case EntryType.DedicatedPlaceholder:
					Debug.Assert(false);
					break;
				case EntryType.BeginStencilMask:
					Debug.Assert(this.m_MaskDepth == this.m_MaskDepthPopped);
					Debug.Assert(!this.m_IsDrawingMask);
					this.m_IsDrawingMask = true;
					this.m_StencilRef = this.m_StencilRefPushed;
					Debug.Assert(this.m_MaskDepth == this.m_StencilRef);
					break;
				case EntryType.EndStencilMask:
					Debug.Assert(this.m_IsDrawingMask);
					this.m_IsDrawingMask = false;
					this.m_MaskDepth = this.m_MaskDepthPushed;
					break;
				case EntryType.PopStencilMask:
					Debug.Assert(this.m_MaskDepth == this.m_StencilRef + 1);
					this.DrawReverseMask();
					this.m_MaskDepth = this.m_MaskDepthPopped;
					this.m_StencilRef = this.m_StencilRefPopped;
					break;
				case EntryType.PushClippingRect:
					this.m_ClipRectId = this.m_ClipRectIdPushed;
					break;
				case EntryType.PopClippingRect:
					this.m_ClipRectId = this.m_ClipRectIdPopped;
					break;
				case EntryType.PushScissors:
				{
					RenderChainCommand cmd3 = this.m_RenderChain.AllocCommand();
					cmd3.type = CommandType.PushScissor;
					cmd3.owner = this.m_CurrentElement;
					cmd3.isTail = this.m_IsTail;
					this.AppendCommand(cmd3);
					break;
				}
				case EntryType.PopScissors:
				{
					RenderChainCommand cmd4 = this.m_RenderChain.AllocCommand();
					cmd4.type = CommandType.PopScissor;
					cmd4.owner = this.m_CurrentElement;
					cmd4.isTail = this.m_IsTail;
					this.AppendCommand(cmd4);
					break;
				}
				case EntryType.PushGroupMatrix:
				{
					RenderChainCommand cmd5 = this.m_RenderChain.AllocCommand();
					cmd5.type = CommandType.PushView;
					cmd5.owner = this.m_CurrentElement;
					cmd5.isTail = this.m_IsTail;
					this.AppendCommand(cmd5);
					break;
				}
				case EntryType.PopGroupMatrix:
				{
					RenderChainCommand cmd6 = this.m_RenderChain.AllocCommand();
					cmd6.type = CommandType.PopView;
					cmd6.owner = this.m_CurrentElement;
					cmd6.isTail = this.m_IsTail;
					this.AppendCommand(cmd6);
					break;
				}
				case EntryType.PushRenderTexture:
				{
					Debug.Assert(this.m_MaskDepth == 0, "The RenderTargetMode feature must not be used within a stencil mask.");
					RenderChainCommand cmd7 = this.m_RenderChain.AllocCommand();
					cmd7.type = CommandType.PushRenderTexture;
					cmd7.owner = this.m_CurrentElement;
					cmd7.isTail = this.m_IsTail;
					this.AppendCommand(cmd7);
					break;
				}
				case EntryType.BlitAndPopRenderTexture:
				{
					RenderChainCommand cmd8 = this.m_RenderChain.AllocCommand();
					cmd8.type = CommandType.BlitToPreviousRT;
					cmd8.owner = this.m_CurrentElement;
					cmd8.isTail = this.m_IsTail;
					cmd8.state.material = EntryProcessor.GetBlitMaterial(this.m_CurrentElement.subRenderTargetMode);
					Debug.Assert(cmd8.state.material != null);
					this.AppendCommand(cmd8);
					RenderChainCommand cmd9 = this.m_RenderChain.AllocCommand();
					cmd9.type = CommandType.PopRenderTexture;
					cmd9.owner = this.m_CurrentElement;
					cmd9.isTail = this.m_IsTail;
					this.AppendCommand(cmd9);
					break;
				}
				case EntryType.PushDefaultMaterial:
				{
					RenderChainCommand cmd10 = this.m_RenderChain.AllocCommand();
					cmd10.type = CommandType.PushDefaultMaterial;
					cmd10.owner = this.m_CurrentElement;
					cmd10.isTail = this.m_IsTail;
					cmd10.state.material = entry.material;
					this.AppendCommand(cmd10);
					break;
				}
				case EntryType.PopDefaultMaterial:
				{
					RenderChainCommand cmd11 = this.m_RenderChain.AllocCommand();
					cmd11.type = CommandType.PopDefaultMaterial;
					cmd11.owner = this.m_CurrentElement;
					cmd11.isTail = this.m_IsTail;
					this.AppendCommand(cmd11);
					break;
				}
				case EntryType.CutRenderChain:
				{
					RenderChainCommand cmd12 = this.m_RenderChain.AllocCommand();
					cmd12.type = CommandType.CutRenderChain;
					cmd12.owner = this.m_CurrentElement;
					cmd12.isTail = this.m_IsTail;
					this.AppendCommand(cmd12);
					break;
				}
				default:
					throw new NotImplementedException();
				}
			}
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x0008A224 File Offset: 0x00088424
		private void ProcessMeshEntry(Entry entry, TextureId textureId)
		{
			int entryVertexCount = entry.vertices.Length;
			int entryIndexCount = entry.indices.Length;
			Debug.Assert(entryVertexCount > 0 == entryIndexCount > 0);
			bool flag = entryVertexCount > 0 && entryIndexCount > 0;
			if (flag)
			{
				bool flag2 = this.m_VertsFilled + entryVertexCount > this.m_AllocVertexCount;
				if (flag2)
				{
					this.ProcessNextAlloc();
					Debug.Assert(this.m_VertsFilled + entryVertexCount <= this.m_AllocVertexCount);
				}
				bool flag3 = !this.m_VertexDataComputed;
				if (flag3)
				{
					UIRUtility.GetVerticesTransformInfo(this.m_CurrentElement, out this.m_Transform);
					this.m_CurrentElement.renderChainData.verticesSpace = this.m_Transform;
					this.m_TransformData = this.m_RenderChain.shaderInfoAllocator.TransformAllocToVertexData(this.m_CurrentElement.renderChainData.transformID);
					this.m_OpacityData = this.m_RenderChain.shaderInfoAllocator.OpacityAllocToVertexData(this.m_CurrentElement.renderChainData.opacityID);
					this.m_VertexDataComputed = true;
				}
				Color32 opacityPage = new Color32(this.m_OpacityData.r, this.m_OpacityData.g, 0, 0);
				Color32 clipRectData = this.m_RenderChain.shaderInfoAllocator.ClipRectAllocToVertexData(this.m_ClipRectId);
				Color32 ids = new Color32(this.m_TransformData.b, clipRectData.b, this.m_OpacityData.b, 0);
				Color32 xformClipPages = new Color32(this.m_TransformData.r, this.m_TransformData.g, clipRectData.r, clipRectData.g);
				Color32 addFlags = new Color32((byte)this.m_RenderType, 0, 0, 0);
				bool flag4 = (entry.flags & EntryFlags.UsesTextCoreSettings) > (EntryFlags)0;
				if (flag4)
				{
					Color32 textCoreSettingsData = this.m_RenderChain.shaderInfoAllocator.TextCoreSettingsToVertexData(this.m_CurrentElement.renderChainData.textCoreSettingsID);
					this.m_TextCoreSettingsPage.r = textCoreSettingsData.r;
					this.m_TextCoreSettingsPage.g = textCoreSettingsData.g;
					ids.a = textCoreSettingsData.b;
				}
				NativeSlice<Vertex> targetVerticesSlice = this.m_Verts.Slice(this.m_VertsFilled, entryVertexCount);
				int entryIndexOffset = this.m_VertsFilled + (int)this.m_IndexOffset;
				NativeSlice<ushort> targetIndicesSlice = this.m_Indices.Slice(this.m_IndicesFilled, entryIndexCount);
				bool shapeWindingIsClockwise = UIRUtility.ShapeWindingIsClockwise(this.m_MaskDepth, this.m_StencilRef);
				bool transformFlipsWinding = this.m_CurrentElement.renderChainData.worldFlipsWinding;
				ConvertMeshJobData job = new ConvertMeshJobData
				{
					vertSrc = (IntPtr)entry.vertices.GetUnsafePtr<Vertex>(),
					vertDst = (IntPtr)targetVerticesSlice.GetUnsafePtr<Vertex>(),
					vertCount = entryVertexCount,
					transform = this.m_Transform,
					xformClipPages = xformClipPages,
					ids = ids,
					addFlags = addFlags,
					opacityPage = opacityPage,
					textCoreSettingsPage = this.m_TextCoreSettingsPage,
					usesTextCoreSettings = (((entry.flags & EntryFlags.UsesTextCoreSettings) != (EntryFlags)0) ? 1 : 0),
					textureId = textureId.ConvertToGpu(),
					gradientSettingsIndexOffset = this.m_GradientSettingIndexOffset,
					indexSrc = (IntPtr)entry.indices.GetUnsafePtr<ushort>(),
					indexDst = (IntPtr)targetIndicesSlice.GetUnsafePtr<ushort>(),
					indexCount = targetIndicesSlice.Length,
					indexOffset = entryIndexOffset,
					flipIndices = ((shapeWindingIsClockwise == transformFlipsWinding) ? 1 : 0),
					forceZ = (this.m_RenderChain.isFlat ? 1 : 0),
					positionZ = (this.m_IsDrawingMask ? 1f : 0f),
					remapUVs = (this.m_RemapUVs ? 1 : 0),
					atlasRect = this.m_AtlasRect
				};
				this.m_RenderChain.jobManager.Add(ref job);
				bool isDrawingMask = this.m_IsDrawingMask;
				if (isDrawingMask)
				{
					this.m_MaskMeshes.Push(new EntryProcessor.MaskMesh
					{
						vertices = targetVerticesSlice,
						indices = targetIndicesSlice,
						indexOffset = entryIndexOffset
					});
				}
				RenderChainCommand cmd = this.CreateMeshDrawCommand(this.m_Mesh, entryIndexCount, this.m_IndicesFilled, entry.material, textureId);
				this.AppendCommand(cmd);
				bool flag5 = entry.type == EntryType.DrawTextMesh;
				if (flag5)
				{
					cmd.state.sdfScale = entry.textScale;
					cmd.state.sharpness = entry.fontSharpness;
				}
				this.m_VertsFilled += entryVertexCount;
				this.m_IndicesFilled += entryIndexCount;
			}
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x0008A6A8 File Offset: 0x000888A8
		private void DrawReverseMask()
		{
			for (;;)
			{
				EntryProcessor.MaskMesh mesh;
				bool flag = this.m_MaskMeshes.TryPop(out mesh);
				if (!flag)
				{
					break;
				}
				Debug.Assert(mesh.indices.Length > 0 == mesh.vertices.Length > 0);
				bool flag2 = mesh.indices.Length > 0 && mesh.vertices.Length > 0;
				if (flag2)
				{
					RenderChainCommand cmd = this.CreateMeshDrawCommand(this.m_Mesh, mesh.indices.Length, this.m_IndicesFilled, null, TextureId.invalid);
					this.AppendCommand(cmd);
					NativeSlice<Vertex> dstVertices = this.m_Verts.Slice(this.m_VertsFilled, mesh.vertices.Length);
					NativeSlice<ushort> dstIndices = this.m_Indices.Slice(this.m_IndicesFilled, mesh.indices.Length);
					CopyMeshJobData job = new CopyMeshJobData
					{
						vertSrc = (IntPtr)mesh.vertices.GetUnsafePtr<Vertex>(),
						vertDst = (IntPtr)dstVertices.GetUnsafePtr<Vertex>(),
						vertCount = mesh.vertices.Length,
						indexSrc = (IntPtr)mesh.indices.GetUnsafePtr<ushort>(),
						indexDst = (IntPtr)dstIndices.GetUnsafePtr<ushort>(),
						indexCount = mesh.indices.Length,
						indexOffset = (int)this.m_IndexOffset + this.m_VertsFilled - mesh.indexOffset
					};
					this.m_RenderChain.jobManager.Add(ref job);
					this.m_IndicesFilled += mesh.indices.Length;
					this.m_VertsFilled += mesh.vertices.Length;
				}
			}
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x0008A874 File Offset: 0x00088A74
		private RenderChainCommand CreateMeshDrawCommand(MeshHandle mesh, int indexCount, int indexOffset, Material material, TextureId texture)
		{
			RenderChainCommand cmd = this.m_RenderChain.AllocCommand();
			cmd.type = CommandType.Draw;
			cmd.state = new State
			{
				material = material,
				texture = texture,
				stencilRef = this.m_StencilRef
			};
			cmd.mesh = mesh;
			cmd.indexOffset = indexOffset;
			cmd.indexCount = indexCount;
			cmd.owner = this.m_CurrentElement;
			cmd.isTail = this.m_IsTail;
			return cmd;
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x0008A8F8 File Offset: 0x00088AF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void AppendCommand(RenderChainCommand next)
		{
			bool flag = this.m_FirstCommand == null;
			if (flag)
			{
				this.m_FirstCommand = next;
				this.m_LastCommand = next;
			}
			else
			{
				next.prev = this.m_LastCommand;
				this.m_LastCommand.next = next;
				this.m_LastCommand = next;
			}
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x0008A948 File Offset: 0x00088B48
		private void ProcessFirstAlloc(List<EntryPreProcessor.AllocSize> allocList, ref MeshHandle mesh)
		{
			bool flag = allocList.Count > 0;
			if (flag)
			{
				EntryPreProcessor.AllocSize allocSize = allocList[0];
				EntryProcessor.UpdateOrAllocate(ref mesh, allocSize.vertexCount, allocSize.indexCount, this.m_RenderChain.device, out this.m_Verts, out this.m_Indices, out this.m_IndexOffset, this.m_RenderChain.statsByRef);
				this.m_AllocVertexCount = (int)mesh.allocVerts.size;
			}
			else
			{
				Debug.Assert(mesh == null);
				this.m_Verts = default(NativeSlice<Vertex>);
				this.m_Indices = default(NativeSlice<ushort>);
				this.m_IndexOffset = 0;
				this.m_AllocVertexCount = 0;
			}
			this.m_Mesh = mesh;
			this.m_VertsFilled = 0;
			this.m_IndicesFilled = 0;
			this.m_AllocIndex = 0;
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x0008AA0C File Offset: 0x00088C0C
		private void ProcessNextAlloc()
		{
			List<EntryPreProcessor.AllocSize> allocList = (this.m_IsTail ? this.m_PreProcessor.tailAllocs : this.m_PreProcessor.headAllocs);
			Debug.Assert(this.m_AllocIndex < allocList.Count - 1);
			List<EntryPreProcessor.AllocSize> list = allocList;
			int num = this.m_AllocIndex + 1;
			this.m_AllocIndex = num;
			EntryPreProcessor.AllocSize allocSize = list[num];
			this.m_Mesh = null;
			EntryProcessor.UpdateOrAllocate(ref this.m_Mesh, allocSize.vertexCount, allocSize.indexCount, this.m_RenderChain.device, out this.m_Verts, out this.m_Indices, out this.m_IndexOffset, this.m_RenderChain.statsByRef);
			this.m_AllocVertexCount = (int)this.m_Mesh.allocVerts.size;
			this.m_RenderChain.InsertExtraMesh(this.m_CurrentElement, this.m_Mesh);
			this.m_VertsFilled = 0;
			this.m_IndicesFilled = 0;
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x0008AAEC File Offset: 0x00088CEC
		private static void UpdateOrAllocate(ref MeshHandle data, int vertexCount, int indexCount, UIRenderDevice device, out NativeSlice<Vertex> verts, out NativeSlice<ushort> indices, out ushort indexOffset, ref ChainBuilderStats stats)
		{
			bool flag = data != null;
			if (flag)
			{
				bool flag2 = (ulong)data.allocVerts.size >= (ulong)((long)vertexCount) && (ulong)data.allocIndices.size >= (ulong)((long)indexCount);
				if (flag2)
				{
					device.Update(data, (uint)vertexCount, (uint)indexCount, out verts, out indices, out indexOffset);
					stats.updatedMeshAllocations += 1U;
				}
				else
				{
					device.Free(data);
					data = device.Allocate((uint)vertexCount, (uint)indexCount, out verts, out indices, out indexOffset);
					stats.newMeshAllocations += 1U;
				}
			}
			else
			{
				data = device.Allocate((uint)vertexCount, (uint)indexCount, out verts, out indices, out indexOffset);
				stats.newMeshAllocations += 1U;
			}
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x0008AB9C File Offset: 0x00088D9C
		private static Material CreateBlitShader(float colorConversion)
		{
			bool flag = EntryProcessor.s_blitShader == null;
			if (flag)
			{
				EntryProcessor.s_blitShader = Shader.Find(Shaders.k_ColorConversionBlit);
			}
			Debug.Assert(EntryProcessor.s_blitShader != null, "UI Tollkit Render Event: Shader Not found");
			Material blitMaterial = new Material(EntryProcessor.s_blitShader);
			blitMaterial.hideFlags |= HideFlags.DontSaveInEditor;
			blitMaterial.SetFloat("_ColorConversion", colorConversion);
			return blitMaterial;
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x0008AC0C File Offset: 0x00088E0C
		private static Material GetBlitMaterial(VisualElement.RenderTargetMode mode)
		{
			Material material;
			switch (mode)
			{
			case VisualElement.RenderTargetMode.NoColorConversion:
			{
				bool flag = EntryProcessor.s_blitMaterial_NoChange == null;
				if (flag)
				{
					EntryProcessor.s_blitMaterial_NoChange = EntryProcessor.CreateBlitShader(0f);
				}
				material = EntryProcessor.s_blitMaterial_NoChange;
				break;
			}
			case VisualElement.RenderTargetMode.LinearToGamma:
			{
				bool flag2 = EntryProcessor.s_blitMaterial_LinearToGamma == null;
				if (flag2)
				{
					EntryProcessor.s_blitMaterial_LinearToGamma = EntryProcessor.CreateBlitShader(1f);
				}
				material = EntryProcessor.s_blitMaterial_LinearToGamma;
				break;
			}
			case VisualElement.RenderTargetMode.GammaToLinear:
			{
				bool flag3 = EntryProcessor.s_blitMaterial_GammaToLinear == null;
				if (flag3)
				{
					EntryProcessor.s_blitMaterial_GammaToLinear = EntryProcessor.CreateBlitShader(-1f);
				}
				material = EntryProcessor.s_blitMaterial_GammaToLinear;
				break;
			}
			default:
				Debug.LogError(string.Format("No Shader for Unsupported RenderTargetMode: {0}", mode));
				material = null;
				break;
			}
			return material;
		}

		// Token: 0x0400111F RID: 4383
		private EntryPreProcessor m_PreProcessor = new EntryPreProcessor();

		// Token: 0x04001120 RID: 4384
		private RenderChain m_RenderChain;

		// Token: 0x04001121 RID: 4385
		private VisualElement m_CurrentElement;

		// Token: 0x04001122 RID: 4386
		private int m_MaskDepth;

		// Token: 0x04001123 RID: 4387
		private int m_MaskDepthPopped;

		// Token: 0x04001124 RID: 4388
		private int m_MaskDepthPushed;

		// Token: 0x04001125 RID: 4389
		private int m_StencilRef;

		// Token: 0x04001126 RID: 4390
		private int m_StencilRefPopped;

		// Token: 0x04001127 RID: 4391
		private int m_StencilRefPushed;

		// Token: 0x04001128 RID: 4392
		private BMPAlloc m_ClipRectId;

		// Token: 0x04001129 RID: 4393
		private BMPAlloc m_ClipRectIdPopped;

		// Token: 0x0400112A RID: 4394
		private BMPAlloc m_ClipRectIdPushed;

		// Token: 0x0400112B RID: 4395
		private bool m_IsDrawingMask;

		// Token: 0x0400112C RID: 4396
		private Stack<EntryProcessor.MaskMesh> m_MaskMeshes = new Stack<EntryProcessor.MaskMesh>(1);

		// Token: 0x0400112D RID: 4397
		private bool m_VertexDataComputed;

		// Token: 0x0400112E RID: 4398
		private Matrix4x4 m_Transform;

		// Token: 0x0400112F RID: 4399
		private Color32 m_TransformData;

		// Token: 0x04001130 RID: 4400
		private Color32 m_OpacityData;

		// Token: 0x04001131 RID: 4401
		private Color32 m_TextCoreSettingsPage;

		// Token: 0x04001132 RID: 4402
		private MeshHandle m_Mesh;

		// Token: 0x04001133 RID: 4403
		private NativeSlice<Vertex> m_Verts;

		// Token: 0x04001134 RID: 4404
		private NativeSlice<ushort> m_Indices;

		// Token: 0x04001135 RID: 4405
		private ushort m_IndexOffset;

		// Token: 0x04001136 RID: 4406
		private int m_AllocVertexCount;

		// Token: 0x04001137 RID: 4407
		private int m_AllocIndex;

		// Token: 0x04001138 RID: 4408
		private int m_VertsFilled;

		// Token: 0x04001139 RID: 4409
		private int m_IndicesFilled;

		// Token: 0x0400113A RID: 4410
		private VertexFlags m_RenderType;

		// Token: 0x0400113B RID: 4411
		private bool m_RemapUVs;

		// Token: 0x0400113C RID: 4412
		private Rect m_AtlasRect;

		// Token: 0x0400113D RID: 4413
		private int m_GradientSettingIndexOffset;

		// Token: 0x0400113E RID: 4414
		private bool m_IsTail;

		// Token: 0x0400113F RID: 4415
		private RenderChainCommand m_FirstCommand;

		// Token: 0x04001140 RID: 4416
		private RenderChainCommand m_LastCommand;

		// Token: 0x04001145 RID: 4421
		private static Material s_blitMaterial_LinearToGamma;

		// Token: 0x04001146 RID: 4422
		private static Material s_blitMaterial_GammaToLinear;

		// Token: 0x04001147 RID: 4423
		private static Material s_blitMaterial_NoChange;

		// Token: 0x04001148 RID: 4424
		private static Shader s_blitShader;

		// Token: 0x02000521 RID: 1313
		private struct MaskMesh
		{
			// Token: 0x04001149 RID: 4425
			public NativeSlice<Vertex> vertices;

			// Token: 0x0400114A RID: 4426
			public NativeSlice<ushort> indices;

			// Token: 0x0400114B RID: 4427
			public int indexOffset;
		}
	}
}
