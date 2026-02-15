using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Profiling;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000536 RID: 1334
	internal class MeshGenerator : IMeshGenerator, IDisposable
	{
		// Token: 0x060024D2 RID: 9426 RVA: 0x0008C408 File Offset: 0x0008A608
		public MeshGenerator(MeshGenerationContext mgc)
		{
			this.m_MeshGenerationContext = mgc;
			this.m_OnMeshGenerationDelegate = new MeshGenerationCallback(this.OnMeshGeneration);
			this.textJobSystem = new UITKTextJobSystem();
		}

		// Token: 0x1700097B RID: 2427
		// (set) Token: 0x060024D3 RID: 9427 RVA: 0x0008C4CE File Offset: 0x0008A6CE
		public VisualElement currentElement
		{
			[CompilerGenerated]
			set
			{
				this.<currentElement>k__BackingField = value;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x0008C4D7 File Offset: 0x0008A6D7
		// (set) Token: 0x060024D5 RID: 9429 RVA: 0x0008C4DF File Offset: 0x0008A6DF
		public UITKTextJobSystem textJobSystem { get; set; }

		// Token: 0x060024D6 RID: 9430 RVA: 0x0008C4E8 File Offset: 0x0008A6E8
		private static Vector2 ConvertBorderRadiusPercentToPoints(Vector2 borderRectSize, Length length)
		{
			float x = length.value;
			float y = length.value;
			bool flag = length.unit == LengthUnit.Percent;
			if (flag)
			{
				x = borderRectSize.x * length.value / 100f;
				y = borderRectSize.y * length.value / 100f;
			}
			x = Mathf.Max(x, 0f);
			y = Mathf.Max(y, 0f);
			return new Vector2(x, y);
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x0008C564 File Offset: 0x0008A764
		public unsafe static void GetVisualElementRadii(VisualElement ve, out Vector2 topLeft, out Vector2 bottomLeft, out Vector2 topRight, out Vector2 bottomRight)
		{
			IResolvedStyle style = ve.resolvedStyle;
			Vector2 borderRectSize = new Vector2(style.width, style.height);
			ComputedStyle computedStyle = *ve.computedStyle;
			topLeft = MeshGenerator.ConvertBorderRadiusPercentToPoints(borderRectSize, computedStyle.borderTopLeftRadius);
			bottomLeft = MeshGenerator.ConvertBorderRadiusPercentToPoints(borderRectSize, computedStyle.borderBottomLeftRadius);
			topRight = MeshGenerator.ConvertBorderRadiusPercentToPoints(borderRectSize, computedStyle.borderTopRightRadius);
			bottomRight = MeshGenerator.ConvertBorderRadiusPercentToPoints(borderRectSize, computedStyle.borderBottomRightRadius);
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x0008C5E8 File Offset: 0x0008A7E8
		public static void AdjustBackgroundSizeForBorders(VisualElement visualElement, ref MeshGenerator.RectangleParams rectParams)
		{
			IResolvedStyle style = visualElement.resolvedStyle;
			Vector4 inset = Vector4.zero;
			bool flag = style.borderLeftWidth >= 1f && style.borderLeftColor.a >= 1f;
			if (flag)
			{
				inset.x = 0.5f;
			}
			bool flag2 = style.borderTopWidth >= 1f && style.borderTopColor.a >= 1f;
			if (flag2)
			{
				inset.y = 0.5f;
			}
			bool flag3 = style.borderRightWidth >= 1f && style.borderRightColor.a >= 1f;
			if (flag3)
			{
				inset.z = 0.5f;
			}
			bool flag4 = style.borderBottomWidth >= 1f && style.borderBottomColor.a >= 1f;
			if (flag4)
			{
				inset.w = 0.5f;
			}
			rectParams.rectInset = inset;
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x0008C6EA File Offset: 0x0008A8EA
		public void DrawText(List<NativeSlice<Vertex>> vertices, List<NativeSlice<ushort>> indices, List<Material> materials, List<GlyphRenderMode> renderModes)
		{
			this.DrawTextInfo(vertices, indices, materials, renderModes);
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x0008C6F9 File Offset: 0x0008A8F9
		public void DrawNativeText(NativeTextInfo textInfo, Vector2 pos)
		{
			this.DrawTextBase(null, textInfo, pos, true);
			FontAsset.UpdateFontAssetsInUpdateQueue();
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x0008C710 File Offset: 0x0008A910
		private void DrawTextBase(TextInfo textInfo, NativeTextInfo nativeTextInfo, Vector2 pos, bool isNative)
		{
			int i = 0;
			int meshInfoCount = (isNative ? nativeTextInfo.meshInfos.Length : textInfo.meshInfo.Length);
			while (i < meshInfoCount)
			{
				MeshInfo meshInfo = default(MeshInfo);
				FontAsset fa = null;
				bool flag = !isNative;
				int remainingVertexCount;
				if (flag)
				{
					meshInfo = textInfo.meshInfo[i];
					Debug.Assert((meshInfo.vertexCount & 3) == 0);
					remainingVertexCount = meshInfo.vertexCount;
				}
				else
				{
					int glyphAmount = nativeTextInfo.meshInfos[i].textElementInfos.Length;
					remainingVertexCount = glyphAmount * 4;
					fa = nativeTextInfo.meshInfos[i].fontAsset;
				}
				int verticesPerAlloc = (int)((ulong)UIRenderDevice.maxVerticesPerPage & 18446744073709551612UL);
				while (remainingVertexCount > 0)
				{
					int vertexCount = Mathf.Min(remainingVertexCount, verticesPerAlloc);
					int quadCount = vertexCount >> 2;
					int indexCount = quadCount * 6;
					this.m_Materials.Add(isNative ? fa.material : meshInfo.material);
					this.m_RenderModes.Add(isNative ? fa.atlasRenderMode : meshInfo.glyphRenderMode);
					NativeSlice<Vertex> vertices;
					NativeSlice<ushort> indices;
					this.m_MeshGenerationContext.AllocateTempMesh(vertexCount, indexCount, out vertices, out indices);
					int vDst = 0;
					int vSrc = 0;
					int j = 0;
					while (vDst < vertexCount)
					{
						if (isNative)
						{
							vertices[vDst] = MeshGenerator.ConvertTextVertexToUIRVertex(nativeTextInfo.meshInfos[i].textElementInfos[vSrc].bottomLeft, pos, false);
							vertices[vDst + 1] = MeshGenerator.ConvertTextVertexToUIRVertex(nativeTextInfo.meshInfos[i].textElementInfos[vSrc].topLeft, pos, false);
							vertices[vDst + 2] = MeshGenerator.ConvertTextVertexToUIRVertex(nativeTextInfo.meshInfos[i].textElementInfos[vSrc].topRight, pos, false);
							vertices[vDst + 3] = MeshGenerator.ConvertTextVertexToUIRVertex(nativeTextInfo.meshInfos[i].textElementInfos[vSrc].bottomRight, pos, false);
						}
						else
						{
							vertices[vDst] = MeshGenerator.ConvertTextVertexToUIRVertex(meshInfo.vertexData[vDst], pos, false);
							vertices[vDst + 1] = MeshGenerator.ConvertTextVertexToUIRVertex(meshInfo.vertexData[vDst + 1], pos, false);
							vertices[vDst + 2] = MeshGenerator.ConvertTextVertexToUIRVertex(meshInfo.vertexData[vDst + 2], pos, false);
							vertices[vDst + 3] = MeshGenerator.ConvertTextVertexToUIRVertex(meshInfo.vertexData[vDst + 3], pos, false);
						}
						indices[j] = (ushort)vDst;
						indices[j + 1] = (ushort)(vDst + 1);
						indices[j + 2] = (ushort)(vDst + 2);
						indices[j + 3] = (ushort)(vDst + 2);
						indices[j + 4] = (ushort)(vDst + 3);
						indices[j + 5] = (ushort)vDst;
						vDst += 4;
						vSrc++;
						j += 6;
					}
					this.m_VerticesArray.Add(vertices);
					this.m_IndicesArray.Add(indices);
					remainingVertexCount -= vertexCount;
				}
				Debug.Assert(remainingVertexCount == 0);
				i++;
			}
			this.DrawTextInfo(this.m_VerticesArray, this.m_IndicesArray, this.m_Materials, this.m_RenderModes);
			this.m_VerticesArray.Clear();
			this.m_IndicesArray.Clear();
			this.m_Materials.Clear();
			this.m_RenderModes.Clear();
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x0008CAAC File Offset: 0x0008ACAC
		private void DrawTextInfo(List<NativeSlice<Vertex>> vertices, List<NativeSlice<ushort>> indices, List<Material> materials, List<GlyphRenderMode> renderModes)
		{
			bool flag = vertices == null;
			if (!flag)
			{
				int i = 0;
				int drawCount = vertices.Count;
				while (i < drawCount)
				{
					bool flag2 = vertices[i].Length == 0;
					if (!flag2)
					{
						bool flag3 = ((Texture2D)materials[i].mainTexture).format != TextureFormat.Alpha8;
						if (flag3)
						{
							this.MakeText(materials[i].mainTexture, vertices[i], indices[i], false, 0f, 0f, true);
						}
						else
						{
							float sdfScale = 0f;
							bool flag4 = !TextGeneratorUtilities.IsBitmapRendering(renderModes[i]);
							if (flag4)
							{
								sdfScale = materials[i].GetFloat(TextShaderUtilities.ID_GradientScale);
							}
							int sharpnessId = TextShaderUtilities.ID_Sharpness;
							float sharpness = (materials[i].HasProperty(sharpnessId) ? materials[i].GetFloat(sharpnessId) : 0f);
							this.MakeText(materials[i].mainTexture, vertices[i], indices[i], true, sdfScale, sharpness, false);
						}
					}
					i++;
				}
			}
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x0008CBE4 File Offset: 0x0008ADE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Vertex ConvertTextVertexToUIRVertex(TextCoreVertex vertex, Vector2 posOffset, bool isDynamicColor = false)
		{
			float dilate = 0f;
			bool flag = vertex.uv2.y < 0f;
			if (flag)
			{
				dilate = 1f;
			}
			return new Vertex
			{
				position = new Vector3(vertex.position.x + posOffset.x, vertex.position.y + posOffset.y, 0f),
				uv = new Vector2(vertex.uv0.x, vertex.uv0.y),
				tint = vertex.color,
				flags = new Color32(0, (byte)(dilate * 255f), 0, isDynamicColor ? 2 : 0)
			};
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x0008CCA4 File Offset: 0x0008AEA4
		private void MakeText(Texture texture, NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, bool isSdf, float sdfScale, float sharpness, bool multiChannel)
		{
			if (isSdf)
			{
				this.m_MeshGenerationContext.entryRecorder.DrawSdfText(this.m_MeshGenerationContext.parentEntry, vertices, indices, texture, sdfScale, sharpness);
			}
			else
			{
				this.m_MeshGenerationContext.entryRecorder.DrawRasterText(this.m_MeshGenerationContext.parentEntry, vertices, indices, texture, multiChannel);
			}
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x0008CD00 File Offset: 0x0008AF00
		public void DrawRectangle(MeshGenerator.RectangleParams rectParams)
		{
			bool flag = rectParams.rect.width < 1E-30f || rectParams.rect.height < 1E-30f;
			if (!flag)
			{
				MeshGenerator.TessellationJobParameters rectangleJobParameters = new MeshGenerator.TessellationJobParameters
				{
					isBorderJob = false,
					rectParams = rectParams.ToNativeParams()
				};
				rectangleJobParameters.rectParams.texture = this.m_GCHandlePool.GetIntPtr(rectParams.texture);
				rectangleJobParameters.rectParams.sprite = this.m_GCHandlePool.GetIntPtr(rectParams.sprite);
				bool flag2 = rectParams.sprite != null && rectParams.sprite.texture != null;
				if (flag2)
				{
					rectangleJobParameters.rectParams.spriteTexture = this.m_GCHandlePool.GetIntPtr(rectParams.sprite.texture);
					rectangleJobParameters.rectParams.spriteVertices = this.m_GCHandlePool.GetIntPtr(rectParams.sprite.vertices);
					rectangleJobParameters.rectParams.spriteUVs = this.m_GCHandlePool.GetIntPtr(rectParams.sprite.uv);
					rectangleJobParameters.rectParams.spriteTriangles = this.m_GCHandlePool.GetIntPtr(rectParams.sprite.triangles);
				}
				bool flag3 = rectParams.backgroundRepeatInstanceList != null;
				if (flag3)
				{
					rectangleJobParameters.rectParams.backgroundRepeatInstanceListStartIndex = rectParams.backgroundRepeatInstanceListStartIndex;
					rectangleJobParameters.rectParams.backgroundRepeatInstanceListEndIndex = rectParams.backgroundRepeatInstanceListEndIndex;
					rectangleJobParameters.rectParams.backgroundRepeatInstanceList = this.m_GCHandlePool.GetIntPtr(rectParams.backgroundRepeatInstanceList);
				}
				rectangleJobParameters.rectParams.vectorImage = this.m_GCHandlePool.GetIntPtr(rectParams.vectorImage);
				VectorImage vectorImage = rectParams.vectorImage;
				bool isUsingGradients = ((vectorImage != null) ? vectorImage.atlas : null) != null;
				rectangleJobParameters.rectParams.meshFlags = rectangleJobParameters.rectParams.meshFlags | (isUsingGradients ? 4 : 0);
				UnsafeMeshGenerationNode unsafeNode;
				this.m_MeshGenerationContext.InsertUnsafeMeshGenerationNode(out unsafeNode);
				rectangleJobParameters.node = unsafeNode;
				this.m_TesselationJobParameters.Add(rectangleJobParameters);
			}
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x0008CF14 File Offset: 0x0008B114
		public void DrawBorder(MeshGenerator.BorderParams borderParams)
		{
			MeshGenerator.TessellationJobParameters borderJobParams = new MeshGenerator.TessellationJobParameters
			{
				isBorderJob = true,
				borderParams = borderParams
			};
			UnsafeMeshGenerationNode unsafeNode;
			this.m_MeshGenerationContext.InsertUnsafeMeshGenerationNode(out unsafeNode);
			borderJobParams.node = unsafeNode;
			this.m_TesselationJobParameters.Add(borderJobParams);
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x0008CF5F File Offset: 0x0008B15F
		public void DrawRectangleRepeat(MeshGenerator.RectangleParams rectParams, Rect totalRect, float scaledPixelsPerPoint)
		{
			this.DoDrawRectangleRepeat(ref rectParams, totalRect, scaledPixelsPerPoint);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x0008CF70 File Offset: 0x0008B170
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void DoDrawRectangleRepeat(ref MeshGenerator.RectangleParams rectParams, Rect totalRect, float scaledPixelsPerPoint)
		{
			Rect uv = new Rect(0f, 0f, 1f, 1f);
			bool flag = this.m_RepeatRectUVList == null;
			if (flag)
			{
				this.m_RepeatRectUVList = new List<MeshGenerator.RepeatRectUV>[2];
				this.m_RepeatRectUVList[0] = new List<MeshGenerator.RepeatRectUV>();
				this.m_RepeatRectUVList[1] = new List<MeshGenerator.RepeatRectUV>();
			}
			else
			{
				this.m_RepeatRectUVList[0].Clear();
				this.m_RepeatRectUVList[1].Clear();
			}
			Rect targetRect = rectParams.rect;
			bool flag2 = rectParams.backgroundSize.sizeType > BackgroundSizeType.Length;
			if (flag2)
			{
				bool flag3 = rectParams.backgroundSize.sizeType == BackgroundSizeType.Contain;
				if (flag3)
				{
					float ratioX = totalRect.width / targetRect.width;
					float ratioY = totalRect.height / targetRect.height;
					Rect rect = targetRect;
					bool flag4 = ratioX < ratioY;
					if (flag4)
					{
						rect.width = totalRect.width;
						rect.height = targetRect.height * totalRect.width / targetRect.width;
					}
					else
					{
						rect.width = targetRect.width * totalRect.height / targetRect.height;
						rect.height = totalRect.height;
					}
					targetRect = rect;
				}
				else
				{
					bool flag5 = rectParams.backgroundSize.sizeType == BackgroundSizeType.Cover;
					if (flag5)
					{
						float ratioX2 = totalRect.width / targetRect.width;
						float ratioY2 = totalRect.height / targetRect.height;
						Rect rect2 = targetRect;
						bool flag6 = ratioX2 > ratioY2;
						if (flag6)
						{
							rect2.width = totalRect.width;
							rect2.height = targetRect.height * totalRect.width / targetRect.width;
						}
						else
						{
							rect2.width = targetRect.width * totalRect.height / targetRect.height;
							rect2.height = totalRect.height;
						}
						targetRect = rect2;
					}
				}
			}
			else
			{
				bool flag7 = !rectParams.backgroundSize.x.IsNone() || !rectParams.backgroundSize.y.IsNone();
				if (flag7)
				{
					bool flag8 = !rectParams.backgroundSize.x.IsNone() && rectParams.backgroundSize.y.IsAuto();
					if (flag8)
					{
						Rect rect3 = targetRect;
						bool flag9 = rectParams.backgroundSize.x.unit == LengthUnit.Percent;
						if (flag9)
						{
							rect3.width = totalRect.width * rectParams.backgroundSize.x.value / 100f;
							rect3.height = rect3.width * targetRect.height / targetRect.width;
						}
						else
						{
							bool flag10 = rectParams.backgroundSize.x.unit == LengthUnit.Pixel;
							if (flag10)
							{
								rect3.width = rectParams.backgroundSize.x.value;
								rect3.height = rect3.width * targetRect.height / targetRect.width;
							}
						}
						targetRect = rect3;
					}
					else
					{
						bool flag11 = !rectParams.backgroundSize.x.IsNone() && !rectParams.backgroundSize.y.IsNone();
						if (flag11)
						{
							Rect rect4 = targetRect;
							bool flag12 = !rectParams.backgroundSize.x.IsAuto();
							if (flag12)
							{
								bool flag13 = rectParams.backgroundSize.x.unit == LengthUnit.Percent;
								if (flag13)
								{
									rect4.width = totalRect.width * rectParams.backgroundSize.x.value / 100f;
								}
								else
								{
									bool flag14 = rectParams.backgroundSize.x.unit == LengthUnit.Pixel;
									if (flag14)
									{
										rect4.width = rectParams.backgroundSize.x.value;
									}
								}
							}
							bool flag15 = !rectParams.backgroundSize.y.IsAuto();
							if (flag15)
							{
								bool flag16 = rectParams.backgroundSize.y.unit == LengthUnit.Percent;
								if (flag16)
								{
									rect4.height = totalRect.height * rectParams.backgroundSize.y.value / 100f;
								}
								else
								{
									bool flag17 = rectParams.backgroundSize.y.unit == LengthUnit.Pixel;
									if (flag17)
									{
										rect4.height = rectParams.backgroundSize.y.value;
									}
								}
								bool flag18 = rectParams.backgroundSize.x.IsAuto();
								if (flag18)
								{
									rect4.width = rect4.height * targetRect.width / targetRect.height;
								}
							}
							targetRect = rect4;
						}
					}
				}
			}
			bool flag19 = targetRect.size.x <= 1E-30f || targetRect.size.y <= 1E-30f;
			if (!flag19)
			{
				bool flag20 = totalRect.size.x <= 1E-30f || totalRect.size.y <= 1E-30f;
				if (!flag20)
				{
					bool flag21 = rectParams.backgroundSize.x.IsAuto() && rectParams.backgroundRepeat.y == Repeat.Round;
					if (flag21)
					{
						float invTargetHeight = 1f / targetRect.height;
						int count = (int)(totalRect.height * invTargetHeight + 0.5f);
						count = Math.Max(count, 1);
						Rect rect5 = default(Rect);
						rect5.height = totalRect.height / (float)count;
						rect5.width = rect5.height * targetRect.width * invTargetHeight;
						targetRect = rect5;
					}
					else
					{
						bool flag22 = rectParams.backgroundSize.y.IsAuto() && rectParams.backgroundRepeat.x == Repeat.Round;
						if (flag22)
						{
							float invTargetWidth = 1f / targetRect.width;
							int count2 = (int)(totalRect.width * invTargetWidth + 0.5f);
							count2 = Math.Max(count2, 1);
							Rect rect6 = default(Rect);
							rect6.width = totalRect.width / (float)count2;
							rect6.height = rect6.width * targetRect.height * invTargetWidth;
							targetRect = rect6;
						}
					}
					for (int axis = 0; axis < 2; axis++)
					{
						Repeat repeat = ((axis == 0) ? rectParams.backgroundRepeat.x : rectParams.backgroundRepeat.y);
						BackgroundPosition backgroundPosition = ((axis == 0) ? rectParams.backgroundPositionX : rectParams.backgroundPositionY);
						float linear_size = 0f;
						bool flag23 = repeat == Repeat.NoRepeat;
						if (flag23)
						{
							Rect rect7 = targetRect;
							MeshGenerator.RepeatRectUV repeatRectUV;
							repeatRectUV.uv = uv;
							repeatRectUV.rect = rect7;
							linear_size = rect7.size[axis];
							this.m_RepeatRectUVList[axis].Add(repeatRectUV);
						}
						else
						{
							bool flag24 = repeat == Repeat.Repeat;
							if (flag24)
							{
								Rect rect8 = targetRect;
								int count3 = (int)((totalRect.size[axis] + 1f / scaledPixelsPerPoint) / targetRect.size[axis]);
								bool flag25 = backgroundPosition.keyword == BackgroundPositionKeyword.Center;
								if (flag25)
								{
									bool flag26 = (count3 & 1) == 1;
									if (flag26)
									{
										count3 += 2;
									}
									else
									{
										count3++;
									}
								}
								else
								{
									count3 += 2;
								}
								for (int i = 0; i < count3; i++)
								{
									Vector2 r = rect8.position;
									r[axis] = (float)i * targetRect.size[axis];
									rect8.position = r;
									MeshGenerator.RepeatRectUV s;
									s.rect = rect8;
									s.uv = uv;
									linear_size += s.rect.size[axis];
									this.m_RepeatRectUVList[axis].Add(s);
								}
							}
							else
							{
								bool flag27 = repeat == Repeat.Space;
								if (flag27)
								{
									Rect rect9 = targetRect;
									int count4 = (int)(totalRect.size[axis] / targetRect.size[axis]);
									bool flag28 = count4 >= 0;
									if (flag28)
									{
										MeshGenerator.RepeatRectUV s2;
										s2.rect = rect9;
										s2.uv = uv;
										this.m_RepeatRectUVList[axis].Add(s2);
										linear_size = targetRect.size[axis];
									}
									bool flag29 = count4 >= 2;
									if (flag29)
									{
										Vector2 r2 = rect9.position;
										r2[axis] = totalRect.size[axis] - targetRect.size[axis];
										rect9.position = r2;
										MeshGenerator.RepeatRectUV s3;
										s3.rect = rect9;
										s3.uv = uv;
										this.m_RepeatRectUVList[axis].Add(s3);
										linear_size = totalRect.size[axis];
									}
									bool flag30 = count4 > 2;
									if (flag30)
									{
										float spaceOffset = (totalRect.size[axis] - targetRect.size[axis] * (float)count4) / (float)(count4 - 1);
										for (int j = 0; j < count4 - 2; j++)
										{
											Vector2 r3 = rect9.position;
											r3[axis] = (targetRect.size[axis] + spaceOffset) * (float)(1 + j);
											rect9.position = r3;
											MeshGenerator.RepeatRectUV s4;
											s4.rect = rect9;
											s4.uv = uv;
											this.m_RepeatRectUVList[axis].Add(s4);
										}
									}
								}
								else
								{
									bool flag31 = repeat == Repeat.Round;
									if (flag31)
									{
										int count5 = (int)((totalRect.size[axis] + targetRect.size[axis] * 0.5f) / targetRect.size[axis]);
										count5 = Math.Max(count5, 1);
										float new_size = totalRect.size[axis] / (float)count5;
										bool flag32 = backgroundPosition.keyword == BackgroundPositionKeyword.Center;
										if (flag32)
										{
											bool flag33 = (count5 & 1) == 1;
											if (flag33)
											{
												count5 += 2;
											}
											else
											{
												count5++;
											}
										}
										else
										{
											count5++;
										}
										Rect rect10 = targetRect;
										Vector2 d = rect10.size;
										d[axis] = new_size;
										rect10.size = d;
										targetRect = rect10;
										for (int k = 0; k < count5; k++)
										{
											Vector2 r4 = rect10.position;
											r4[axis] = new_size * (float)k;
											rect10.position = r4;
											MeshGenerator.RepeatRectUV s5;
											s5.rect = rect10;
											s5.uv = uv;
											this.m_RepeatRectUVList[axis].Add(s5);
											linear_size += s5.rect.size[axis];
										}
									}
								}
							}
						}
						float offset = 0f;
						bool alignToGrid = false;
						bool flag34 = backgroundPosition.keyword == BackgroundPositionKeyword.Center;
						if (flag34)
						{
							offset = (totalRect.size[axis] - linear_size) * 0.5f;
							alignToGrid = true;
						}
						else
						{
							bool flag35 = repeat != Repeat.Space;
							if (flag35)
							{
								bool flag36 = backgroundPosition.offset.unit == LengthUnit.Percent;
								if (flag36)
								{
									offset = (totalRect.size[axis] - targetRect.size[axis]) * backgroundPosition.offset.value / 100f;
									alignToGrid = true;
								}
								else
								{
									bool flag37 = backgroundPosition.offset.unit == LengthUnit.Pixel;
									if (flag37)
									{
										offset = backgroundPosition.offset.value;
									}
								}
								bool flag38 = backgroundPosition.keyword == BackgroundPositionKeyword.Right || backgroundPosition.keyword == BackgroundPositionKeyword.Bottom;
								if (flag38)
								{
									offset = totalRect.size[axis] - linear_size - offset;
								}
							}
						}
						bool flag39 = alignToGrid && rectParams.sprite == null && rectParams.vectorImage == null;
						if (flag39)
						{
							float sizeInPixels = targetRect.size[axis] * scaledPixelsPerPoint;
							bool flag40 = Mathf.Abs(Mathf.Round(sizeInPixels) - sizeInPixels) < 0.001f;
							if (flag40)
							{
								offset = AlignmentUtils.CeilToPixelGrid(offset, scaledPixelsPerPoint, -0.02f);
							}
						}
						bool flag41 = repeat == Repeat.Repeat || repeat == Repeat.Round;
						if (flag41)
						{
							float size = targetRect.size[axis];
							bool flag42 = size > 1E-30f;
							if (flag42)
							{
								bool flag43 = offset < -size;
								if (flag43)
								{
									int mod = (int)(-offset / size);
									offset += (float)mod * size;
								}
								bool flag44 = offset > 0f;
								if (flag44)
								{
									int mod2 = (int)(offset / size);
									offset -= (float)(1 + mod2) * size;
								}
							}
						}
						for (int l = 0; l < this.m_RepeatRectUVList[axis].Count; l++)
						{
							MeshGenerator.RepeatRectUV item = this.m_RepeatRectUVList[axis][l];
							Vector2 pos = item.rect.position;
							ref Vector2 ptr = ref pos;
							int num = axis;
							ptr[num] += offset;
							item.rect.position = pos;
							this.m_RepeatRectUVList[axis][l] = item;
						}
					}
					Rect originalUV = new Rect(uv);
					int totalRepeatCount = this.m_RepeatRectUVList[1].Count * this.m_RepeatRectUVList[0].Count;
					bool flag45 = totalRepeatCount > 1;
					if (flag45)
					{
						bool flag46 = rectParams.vectorImage == null;
						if (flag46)
						{
							bool flag47 = this.m_BackgroundRepeatInstanceList == null;
							if (flag47)
							{
								this.m_BackgroundRepeatInstanceList = new NativePagedList<MeshGenerator.BackgroundRepeatInstance>(8, Allocator.Persistent, Allocator.TempJob);
							}
							rectParams.backgroundRepeatInstanceList = this.m_BackgroundRepeatInstanceList;
							rectParams.backgroundRepeatInstanceListStartIndex = this.m_BackgroundRepeatInstanceList.GetCount();
						}
					}
					int currentTotalRepeatCount = 0;
					foreach (MeshGenerator.RepeatRectUV y in this.m_RepeatRectUVList[1])
					{
						Rect rect11 = y.rect;
						targetRect.y = rect11.y;
						rect11 = y.rect;
						targetRect.height = rect11.height;
						rect11 = y.uv;
						uv.y = rect11.y;
						rect11 = y.uv;
						uv.height = rect11.height;
						bool flag48 = targetRect.y < totalRect.y;
						if (flag48)
						{
							float left = totalRect.y - targetRect.y;
							float right = targetRect.height - left;
							float total = left + right;
							float new_height = originalUV.height * right / total;
							float new_y = originalUV.height * left / total;
							uv.y = new_y + originalUV.y;
							uv.height = new_height;
							targetRect.y = totalRect.y;
							targetRect.height = right;
						}
						bool flag49 = targetRect.yMax > totalRect.yMax;
						if (flag49)
						{
							float right2 = targetRect.yMax - totalRect.yMax;
							float left2 = targetRect.height - right2;
							float total2 = left2 + right2;
							float new_height2 = uv.height * left2 / total2;
							uv.height = new_height2;
							uv.y = uv.yMax - new_height2;
							targetRect.height = left2;
						}
						bool flag50 = rectParams.vectorImage == null;
						if (flag50)
						{
							float before = uv.y - originalUV.y;
							float after = originalUV.yMax - uv.yMax;
							uv.y += after - before;
						}
						foreach (MeshGenerator.RepeatRectUV x in this.m_RepeatRectUVList[0])
						{
							rect11 = x.rect;
							targetRect.x = rect11.x;
							rect11 = x.rect;
							targetRect.width = rect11.width;
							rect11 = x.uv;
							uv.x = rect11.x;
							rect11 = x.uv;
							uv.width = rect11.width;
							bool flag51 = targetRect.x < totalRect.x;
							if (flag51)
							{
								float left3 = totalRect.x - targetRect.x;
								float right3 = targetRect.width - left3;
								float total3 = left3 + right3;
								float new_width = uv.width * right3 / total3;
								float new_x = originalUV.x + originalUV.width * left3 / total3;
								uv.x = new_x;
								uv.width = new_width;
								targetRect.x = totalRect.x;
								targetRect.width = right3;
							}
							bool flag52 = targetRect.xMax > totalRect.xMax;
							if (flag52)
							{
								float right4 = targetRect.xMax - totalRect.xMax;
								float left4 = targetRect.width - right4;
								float total4 = left4 + right4;
								float new_width2 = uv.width * left4 / total4;
								uv.width = new_width2;
								targetRect.width = left4;
							}
							this.StampRectangleWithSubRect(rectParams, targetRect, totalRect, uv, ref rectParams.backgroundRepeatInstanceList);
							currentTotalRepeatCount++;
							bool flag53 = rectParams.backgroundRepeatInstanceList != null;
							if (flag53)
							{
								bool flag54 = currentTotalRepeatCount > 60;
								if (flag54)
								{
									currentTotalRepeatCount = 0;
									rectParams.backgroundRepeatInstanceListEndIndex = this.m_BackgroundRepeatInstanceList.GetCount();
									this.DrawRectangle(rectParams);
									rectParams.backgroundRepeatInstanceListStartIndex = rectParams.backgroundRepeatInstanceListEndIndex;
								}
							}
						}
					}
					bool flag55 = rectParams.backgroundRepeatInstanceList != null && currentTotalRepeatCount > 0;
					if (flag55)
					{
						rectParams.backgroundRepeatInstanceListEndIndex = this.m_BackgroundRepeatInstanceList.GetCount();
						this.DrawRectangle(rectParams);
					}
				}
			}
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x0008E228 File Offset: 0x0008C428
		private void StampRectangleWithSubRect(MeshGenerator.RectangleParams rectParams, Rect targetRect, Rect totalRect, Rect targetUV, ref NativePagedList<MeshGenerator.BackgroundRepeatInstance> backgroundRepeatInstanceList)
		{
			bool flag = targetRect.width < 0.001f || targetRect.height < 0.001f;
			if (!flag)
			{
				Rect fullRect = targetRect;
				fullRect.size /= targetUV.size;
				fullRect.position -= new Vector2(targetUV.position.x, 1f - targetUV.position.y - targetUV.size.y) * fullRect.size;
				Rect subRect = rectParams.subRect;
				subRect.position *= fullRect.size;
				subRect.position += fullRect.position;
				subRect.size *= fullRect.size;
				bool flag2 = rectParams.HasSlices(0.001f);
				if (flag2)
				{
					rectParams.backgroundRepeatRect = Rect.zero;
					rectParams.rect = targetRect;
				}
				else
				{
					Rect rect = MeshGenerator.RectangleParams.RectIntersection(subRect, targetRect);
					bool flag3 = rect.size.x < 0.001f || rect.size.y < 0.001f;
					if (flag3)
					{
						return;
					}
					bool flag4 = rect.size != subRect.size;
					if (flag4)
					{
						Vector2 sizeRatio = rect.size / subRect.size;
						Vector2 newUVSize = rectParams.uv.size * sizeRatio;
						Vector2 uvDiff = rectParams.uv.size - newUVSize;
						bool flag5 = rect.x > subRect.x;
						if (flag5)
						{
							float overflow = (subRect.xMax - rect.xMax) / subRect.width * rectParams.uv.size.x;
							rectParams.uv.x = rectParams.uv.x + (uvDiff.x - overflow);
						}
						bool flag6 = rect.yMax < subRect.yMax;
						if (flag6)
						{
							float overflow2 = (rect.y - subRect.y) / subRect.height * rectParams.uv.size.y;
							rectParams.uv.y = rectParams.uv.y + (uvDiff.y - overflow2);
						}
						rectParams.uv.size = newUVSize;
					}
					bool flag7 = rectParams.vectorImage != null;
					if (flag7)
					{
						rectParams.backgroundRepeatRect = Rect.zero;
						rectParams.rect = rect;
					}
					else
					{
						bool flag8 = totalRect == rect;
						if (flag8)
						{
							rectParams.backgroundRepeatRect = Rect.zero;
						}
						else
						{
							rectParams.backgroundRepeatRect = rect;
						}
						rectParams.rect = totalRect;
					}
				}
				bool flag9 = rectParams.vectorImage == null && backgroundRepeatInstanceList != null;
				if (flag9)
				{
					MeshGenerator.BackgroundRepeatInstance rect2;
					rect2.rect = rectParams.rect;
					rect2.backgroundRepeatRect = rectParams.backgroundRepeatRect;
					rect2.uv = rectParams.uv;
					backgroundRepeatInstanceList.Add(rect2);
				}
				else
				{
					this.DrawRectangle(rectParams);
				}
			}
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x0008E574 File Offset: 0x0008C774
		private static void AdjustSpriteWinding(Vector2[] vertices, ushort[] indices, NativeSlice<ushort> newIndices)
		{
			for (int i = 0; i < indices.Length; i += 3)
			{
				Vector3 v0 = vertices[(int)indices[i]];
				Vector3 v = vertices[(int)indices[i + 1]];
				Vector3 v2 = vertices[(int)indices[i + 2]];
				Vector3 v3 = (v - v0).normalized;
				Vector3 w = (v2 - v0).normalized;
				Vector3 c = Vector3.Cross(v3, w);
				bool flag = c.z >= 0f;
				if (flag)
				{
					newIndices[i] = indices[i + 1];
					newIndices[i + 1] = indices[i];
					newIndices[i + 2] = indices[i + 2];
				}
				else
				{
					newIndices[i] = indices[i];
					newIndices[i + 1] = indices[i + 1];
					newIndices[i + 2] = indices[i + 2];
				}
			}
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x0008E674 File Offset: 0x0008C874
		public void ScheduleJobs(MeshGenerationContext mgc)
		{
			int parameterCount = this.m_TesselationJobParameters.Count;
			bool flag = parameterCount == 0;
			if (!flag)
			{
				bool flag2 = this.m_JobParameters.Length < parameterCount;
				if (flag2)
				{
					this.m_JobParameters.Dispose();
					this.m_JobParameters = new NativeArray<MeshGenerator.TessellationJobParameters>(parameterCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				}
				for (int i = 0; i < parameterCount; i++)
				{
					this.m_JobParameters[i] = this.m_TesselationJobParameters[i];
				}
				this.m_TesselationJobParameters.Clear();
				MeshGenerator.TessellationJob job = new MeshGenerator.TessellationJob
				{
					jobParameters = this.m_JobParameters.Slice(0, parameterCount)
				};
				mgc.GetTempMeshAllocator(out job.allocator);
				JobHandle jobHandle = job.Schedule(parameterCount, 1, default(JobHandle));
				mgc.AddMeshGenerationJob(jobHandle);
				mgc.AddMeshGenerationCallback(this.m_OnMeshGenerationDelegate, null, MeshGenerationCallbackType.Work, true);
			}
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x0008E760 File Offset: 0x0008C960
		private void OnMeshGeneration(MeshGenerationContext ctx, object data)
		{
			bool flag = this.m_BackgroundRepeatInstanceList != null;
			if (flag)
			{
				this.m_BackgroundRepeatInstanceList.Reset();
			}
			this.m_GCHandlePool.ReturnAll();
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x0008E795 File Offset: 0x0008C995
		// (set) Token: 0x060024E8 RID: 9448 RVA: 0x0008E79D File Offset: 0x0008C99D
		internal bool disposed { get; private set; }

		// Token: 0x060024E9 RID: 9449 RVA: 0x0008E7A6 File Offset: 0x0008C9A6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0008E7B8 File Offset: 0x0008C9B8
		private void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = this.m_BackgroundRepeatInstanceList != null;
					if (flag)
					{
						this.m_BackgroundRepeatInstanceList.Dispose();
					}
					this.m_GCHandlePool.Dispose();
					this.m_JobParameters.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x040011C5 RID: 4549
		private static readonly ProfilerMarker k_MarkerDrawRectangle = new ProfilerMarker("MeshGenerator.DrawRectangle");

		// Token: 0x040011C6 RID: 4550
		private static readonly ProfilerMarker k_MarkerDrawBorder = new ProfilerMarker("MeshGenerator.DrawBorder");

		// Token: 0x040011C7 RID: 4551
		private static readonly ProfilerMarker k_MarkerDrawVectorImage = new ProfilerMarker("MeshGenerator.DrawVectorImage");

		// Token: 0x040011C8 RID: 4552
		private static readonly ProfilerMarker k_MarkerDrawRectangleRepeat = new ProfilerMarker("MeshGenerator.DrawRectangleRepeat");

		// Token: 0x040011C9 RID: 4553
		private MeshGenerationContext m_MeshGenerationContext;

		// Token: 0x040011CA RID: 4554
		private List<MeshGenerator.RepeatRectUV>[] m_RepeatRectUVList = null;

		// Token: 0x040011CB RID: 4555
		private NativePagedList<MeshGenerator.BackgroundRepeatInstance> m_BackgroundRepeatInstanceList = null;

		// Token: 0x040011CC RID: 4556
		private GCHandlePool m_GCHandlePool = new GCHandlePool(256, 64);

		// Token: 0x040011CD RID: 4557
		private NativeArray<MeshGenerator.TessellationJobParameters> m_JobParameters;

		// Token: 0x040011D0 RID: 4560
		private TextInfo m_TextInfo = new TextInfo(VertexDataLayout.VBO);

		// Token: 0x040011D1 RID: 4561
		private TextGenerationSettings m_Settings = new TextGenerationSettings
		{
			screenRect = Rect.zero,
			richText = true,
			inverseYAxis = true
		};

		// Token: 0x040011D2 RID: 4562
		private List<NativeSlice<Vertex>> m_VerticesArray = new List<NativeSlice<Vertex>>();

		// Token: 0x040011D3 RID: 4563
		private List<NativeSlice<ushort>> m_IndicesArray = new List<NativeSlice<ushort>>();

		// Token: 0x040011D4 RID: 4564
		private List<Material> m_Materials = new List<Material>();

		// Token: 0x040011D5 RID: 4565
		private List<GlyphRenderMode> m_RenderModes = new List<GlyphRenderMode>();

		// Token: 0x040011D6 RID: 4566
		private MeshGenerationCallback m_OnMeshGenerationDelegate;

		// Token: 0x040011D7 RID: 4567
		private List<MeshGenerator.TessellationJobParameters> m_TesselationJobParameters = new List<MeshGenerator.TessellationJobParameters>(256);

		// Token: 0x02000537 RID: 1335
		private struct RepeatRectUV
		{
			// Token: 0x040011D9 RID: 4569
			public Rect rect;

			// Token: 0x040011DA RID: 4570
			public Rect uv;
		}

		// Token: 0x02000538 RID: 1336
		public struct BackgroundRepeatInstance
		{
			// Token: 0x040011DB RID: 4571
			public Rect rect;

			// Token: 0x040011DC RID: 4572
			public Rect backgroundRepeatRect;

			// Token: 0x040011DD RID: 4573
			public Rect uv;
		}

		// Token: 0x02000539 RID: 1337
		public struct BorderParams
		{
			// Token: 0x060024EC RID: 9452 RVA: 0x0008E854 File Offset: 0x0008CA54
			internal MeshBuilderNative.NativeBorderParams ToNativeParams()
			{
				return new MeshBuilderNative.NativeBorderParams
				{
					rect = this.rect,
					leftColor = this.leftColor,
					topColor = this.topColor,
					rightColor = this.rightColor,
					bottomColor = this.bottomColor,
					leftWidth = this.leftWidth,
					topWidth = this.topWidth,
					rightWidth = this.rightWidth,
					bottomWidth = this.bottomWidth,
					topLeftRadius = this.topLeftRadius,
					topRightRadius = this.topRightRadius,
					bottomRightRadius = this.bottomRightRadius,
					bottomLeftRadius = this.bottomLeftRadius,
					leftColorPage = this.leftColorPage.ToNativeColorPage(),
					topColorPage = this.topColorPage.ToNativeColorPage(),
					rightColorPage = this.rightColorPage.ToNativeColorPage(),
					bottomColorPage = this.bottomColorPage.ToNativeColorPage()
				};
			}

			// Token: 0x040011DE RID: 4574
			public Rect rect;

			// Token: 0x040011DF RID: 4575
			public Color playmodeTintColor;

			// Token: 0x040011E0 RID: 4576
			public Color leftColor;

			// Token: 0x040011E1 RID: 4577
			public Color topColor;

			// Token: 0x040011E2 RID: 4578
			public Color rightColor;

			// Token: 0x040011E3 RID: 4579
			public Color bottomColor;

			// Token: 0x040011E4 RID: 4580
			public float leftWidth;

			// Token: 0x040011E5 RID: 4581
			public float topWidth;

			// Token: 0x040011E6 RID: 4582
			public float rightWidth;

			// Token: 0x040011E7 RID: 4583
			public float bottomWidth;

			// Token: 0x040011E8 RID: 4584
			public Vector2 topLeftRadius;

			// Token: 0x040011E9 RID: 4585
			public Vector2 topRightRadius;

			// Token: 0x040011EA RID: 4586
			public Vector2 bottomRightRadius;

			// Token: 0x040011EB RID: 4587
			public Vector2 bottomLeftRadius;

			// Token: 0x040011EC RID: 4588
			internal ColorPage leftColorPage;

			// Token: 0x040011ED RID: 4589
			internal ColorPage topColorPage;

			// Token: 0x040011EE RID: 4590
			internal ColorPage rightColorPage;

			// Token: 0x040011EF RID: 4591
			internal ColorPage bottomColorPage;
		}

		// Token: 0x0200053A RID: 1338
		public struct RectangleParams
		{
			// Token: 0x060024ED RID: 9453 RVA: 0x0008E960 File Offset: 0x0008CB60
			private static void AdjustUVsForScaleMode(Rect rect, Rect uv, Texture texture, ScaleMode scaleMode, out Rect rectOut, out Rect uvOut)
			{
				float srcAspect = Mathf.Abs((float)texture.width * uv.width / ((float)texture.height * uv.height));
				float destAspect = rect.width / rect.height;
				switch (scaleMode)
				{
				case ScaleMode.StretchToFill:
					break;
				case ScaleMode.ScaleAndCrop:
				{
					bool flag = destAspect > srcAspect;
					if (flag)
					{
						float stretch = uv.height * (srcAspect / destAspect);
						float crop = (uv.height - stretch) * 0.5f;
						uv = new Rect(uv.x, uv.y + crop, uv.width, stretch);
					}
					else
					{
						float stretch2 = uv.width * (destAspect / srcAspect);
						float crop2 = (uv.width - stretch2) * 0.5f;
						uv = new Rect(uv.x + crop2, uv.y, stretch2, uv.height);
					}
					break;
				}
				case ScaleMode.ScaleToFit:
				{
					bool flag2 = destAspect > srcAspect;
					if (flag2)
					{
						float stretch3 = srcAspect / destAspect;
						rect = new Rect(rect.xMin + rect.width * (1f - stretch3) * 0.5f, rect.yMin, stretch3 * rect.width, rect.height);
					}
					else
					{
						float stretch4 = destAspect / srcAspect;
						rect = new Rect(rect.xMin, rect.yMin + rect.height * (1f - stretch4) * 0.5f, rect.width, stretch4 * rect.height);
					}
					break;
				}
				default:
					throw new NotImplementedException();
				}
				rectOut = rect;
				uvOut = uv;
			}

			// Token: 0x060024EE RID: 9454 RVA: 0x0008EB04 File Offset: 0x0008CD04
			private static void AdjustSpriteUVsForScaleMode(Rect containerRect, Rect srcRect, Rect spriteGeomRect, Sprite sprite, ScaleMode scaleMode, out Rect rectOut, out Rect uvOut)
			{
				float srcAspect = sprite.rect.width / sprite.rect.height;
				float destAspect = containerRect.width / containerRect.height;
				Rect geomRectNorm = spriteGeomRect;
				geomRectNorm.position -= sprite.bounds.min;
				geomRectNorm.position /= sprite.bounds.size;
				geomRectNorm.size /= sprite.bounds.size;
				Vector2 p = geomRectNorm.position;
				p.y = 1f - geomRectNorm.size.y - p.y;
				geomRectNorm.position = p;
				switch (scaleMode)
				{
				case ScaleMode.StretchToFill:
				{
					Vector2 scale = containerRect.size;
					containerRect.position = geomRectNorm.position * scale;
					containerRect.size = geomRectNorm.size * scale;
					break;
				}
				case ScaleMode.ScaleAndCrop:
				{
					Rect stretchedRect = containerRect;
					bool flag = destAspect > srcAspect;
					if (flag)
					{
						stretchedRect.height = stretchedRect.width / srcAspect;
						stretchedRect.position = new Vector2(stretchedRect.position.x, -(stretchedRect.height - containerRect.height) / 2f);
					}
					else
					{
						stretchedRect.width = stretchedRect.height * srcAspect;
						stretchedRect.position = new Vector2(-(stretchedRect.width - containerRect.width) / 2f, stretchedRect.position.y);
					}
					Vector2 scale2 = stretchedRect.size;
					stretchedRect.position += geomRectNorm.position * scale2;
					stretchedRect.size = geomRectNorm.size * scale2;
					Rect newRect = MeshGenerator.RectangleParams.RectIntersection(containerRect, stretchedRect);
					bool flag2 = newRect.width < 1E-30f || newRect.height < 1E-30f;
					if (flag2)
					{
						newRect = Rect.zero;
					}
					else
					{
						Rect uvScale = newRect;
						uvScale.position -= stretchedRect.position;
						uvScale.position /= stretchedRect.size;
						uvScale.size /= stretchedRect.size;
						Vector2 scalePos = uvScale.position;
						scalePos.y = 1f - uvScale.size.y - scalePos.y;
						uvScale.position = scalePos;
						srcRect.position += uvScale.position * srcRect.size;
						srcRect.size *= uvScale.size;
					}
					containerRect = newRect;
					break;
				}
				case ScaleMode.ScaleToFit:
				{
					bool flag3 = destAspect > srcAspect;
					if (flag3)
					{
						float stretch = srcAspect / destAspect;
						containerRect = new Rect(containerRect.xMin + containerRect.width * (1f - stretch) * 0.5f, containerRect.yMin, stretch * containerRect.width, containerRect.height);
					}
					else
					{
						float stretch2 = destAspect / srcAspect;
						containerRect = new Rect(containerRect.xMin, containerRect.yMin + containerRect.height * (1f - stretch2) * 0.5f, containerRect.width, stretch2 * containerRect.height);
					}
					containerRect.position += geomRectNorm.position * containerRect.size;
					containerRect.size *= geomRectNorm.size;
					break;
				}
				default:
					throw new NotImplementedException();
				}
				rectOut = containerRect;
				uvOut = srcRect;
			}

			// Token: 0x060024EF RID: 9455 RVA: 0x0008EF18 File Offset: 0x0008D118
			internal static Rect RectIntersection(Rect a, Rect b)
			{
				Rect r = Rect.zero;
				r.min = Vector2.Max(a.min, b.min);
				r.max = Vector2.Min(a.max, b.max);
				r.size = Vector2.Max(r.size, Vector2.zero);
				return r;
			}

			// Token: 0x060024F0 RID: 9456 RVA: 0x0008EF80 File Offset: 0x0008D180
			private static Rect ComputeGeomRect(Sprite sprite)
			{
				Vector2 vMin = new Vector2(float.MaxValue, float.MaxValue);
				Vector2 vMax = new Vector2(float.MinValue, float.MinValue);
				foreach (Vector2 uv in sprite.vertices)
				{
					vMin = Vector2.Min(vMin, uv);
					vMax = Vector2.Max(vMax, uv);
				}
				return new Rect(vMin, vMax - vMin);
			}

			// Token: 0x060024F1 RID: 9457 RVA: 0x0008EFF8 File Offset: 0x0008D1F8
			private static Rect ComputeUVRect(Sprite sprite)
			{
				Vector2 uvMin = new Vector2(float.MaxValue, float.MaxValue);
				Vector2 uvMax = new Vector2(float.MinValue, float.MinValue);
				foreach (Vector2 uv in sprite.uv)
				{
					uvMin = Vector2.Min(uvMin, uv);
					uvMax = Vector2.Max(uvMax, uv);
				}
				return new Rect(uvMin, uvMax - uvMin);
			}

			// Token: 0x060024F2 RID: 9458 RVA: 0x0008F070 File Offset: 0x0008D270
			private static Rect ApplyPackingRotation(Rect uv, SpritePackingRotation rotation)
			{
				switch (rotation)
				{
				case SpritePackingRotation.FlipHorizontal:
				{
					uv.position += new Vector2(uv.size.x, 0f);
					Vector2 size = uv.size;
					size.x = -size.x;
					uv.size = size;
					break;
				}
				case SpritePackingRotation.FlipVertical:
				{
					uv.position += new Vector2(0f, uv.size.y);
					Vector2 size2 = uv.size;
					size2.y = -size2.y;
					uv.size = size2;
					break;
				}
				case SpritePackingRotation.Rotate180:
					uv.position += uv.size;
					uv.size = -uv.size;
					break;
				}
				return uv;
			}

			// Token: 0x060024F3 RID: 9459 RVA: 0x0008F174 File Offset: 0x0008D374
			public static MeshGenerator.RectangleParams MakeTextured(Rect rect, Rect uv, Texture texture, ScaleMode scaleMode, Color playModeTintColor)
			{
				MeshGenerator.RectangleParams.AdjustUVsForScaleMode(rect, uv, texture, scaleMode, out rect, out uv);
				Vector2 textureSize = new Vector2((float)texture.width, (float)texture.height);
				return new MeshGenerator.RectangleParams
				{
					rect = rect,
					subRect = new Rect(0f, 0f, 1f, 1f),
					uv = uv,
					color = Color.white,
					texture = texture,
					contentSize = textureSize,
					textureSize = textureSize,
					scaleMode = scaleMode,
					playmodeTintColor = playModeTintColor
				};
			}

			// Token: 0x060024F4 RID: 9460 RVA: 0x0008F21C File Offset: 0x0008D41C
			public static MeshGenerator.RectangleParams MakeSprite(Rect containerRect, Rect subRect, Sprite sprite, ScaleMode scaleMode, Color playModeTintColor, bool hasRadius, ref Vector4 slices, bool useForRepeat = false)
			{
				bool flag = sprite == null || sprite.bounds.size.x < 1E-30f || sprite.bounds.size.y < 1E-30f;
				MeshGenerator.RectangleParams rectangleParams2;
				if (flag)
				{
					MeshGenerator.RectangleParams rectangleParams = default(MeshGenerator.RectangleParams);
					rectangleParams2 = rectangleParams;
				}
				else
				{
					bool flag2 = sprite.texture == null;
					if (flag2)
					{
						Debug.LogWarning("Ignoring textureless sprite named \"" + sprite.name + "\", please import as a VectorImage instead");
						MeshGenerator.RectangleParams rectangleParams = default(MeshGenerator.RectangleParams);
						rectangleParams2 = rectangleParams;
					}
					else
					{
						Rect spriteGeomRect = MeshGenerator.RectangleParams.ComputeGeomRect(sprite);
						Rect spriteUVRect = MeshGenerator.RectangleParams.ComputeUVRect(sprite);
						Vector4 border = sprite.border;
						bool hasSlices = border != Vector4.zero || slices != Vector4.zero;
						bool hasSubRect = subRect != new Rect(0f, 0f, 1f, 1f);
						bool useTexturedQuad = scaleMode == ScaleMode.ScaleAndCrop || hasSlices || hasRadius || useForRepeat || hasSubRect;
						bool flag3 = useTexturedQuad && sprite.packed && sprite.packingRotation > SpritePackingRotation.None;
						if (flag3)
						{
							spriteUVRect = MeshGenerator.RectangleParams.ApplyPackingRotation(spriteUVRect, sprite.packingRotation);
						}
						bool flag4 = hasSubRect;
						Rect srcRect;
						if (flag4)
						{
							srcRect = subRect;
							srcRect.position *= spriteUVRect.size;
							srcRect.position += spriteUVRect.position;
							srcRect.size *= spriteUVRect.size;
						}
						else
						{
							srcRect = spriteUVRect;
						}
						Rect adjustedDstRect;
						Rect adjustedSrcRect;
						MeshGenerator.RectangleParams.AdjustSpriteUVsForScaleMode(containerRect, srcRect, spriteGeomRect, sprite, scaleMode, out adjustedDstRect, out adjustedSrcRect);
						Rect normalizedRect = spriteGeomRect;
						normalizedRect.size /= sprite.bounds.size;
						normalizedRect.position -= sprite.bounds.min;
						normalizedRect.position /= sprite.bounds.size;
						normalizedRect.position = new Vector2(normalizedRect.position.x, 1f - (normalizedRect.position.y + normalizedRect.height));
						MeshGenerator.RectangleParams rectangleParams = new MeshGenerator.RectangleParams
						{
							rect = adjustedDstRect,
							uv = adjustedSrcRect,
							subRect = normalizedRect,
							color = Color.white,
							texture = (useTexturedQuad ? sprite.texture : null),
							sprite = (useTexturedQuad ? null : sprite),
							contentSize = sprite.rect.size,
							textureSize = new Vector2((float)sprite.texture.width, (float)sprite.texture.height),
							spriteGeomRect = spriteGeomRect,
							scaleMode = scaleMode,
							playmodeTintColor = playModeTintColor,
							meshFlags = (sprite.packed ? MeshGenerationContext.MeshFlags.SkipDynamicAtlas : MeshGenerationContext.MeshFlags.None)
						};
						MeshGenerator.RectangleParams rp = rectangleParams;
						Vector4 spriteBorders = new Vector4(border.x, border.w, border.z, border.y);
						bool flag5 = slices != Vector4.zero && spriteBorders != Vector4.zero && spriteBorders != slices;
						if (flag5)
						{
							Debug.LogWarning(string.Format("Sprite \"{0}\" borders {1} are overridden by style slices {2}", sprite.name, spriteBorders, slices));
						}
						else
						{
							bool flag6 = slices == Vector4.zero;
							if (flag6)
							{
								slices = spriteBorders;
							}
						}
						rectangleParams2 = rp;
					}
				}
				return rectangleParams2;
			}

			// Token: 0x060024F5 RID: 9461 RVA: 0x0008F5D8 File Offset: 0x0008D7D8
			public static MeshGenerator.RectangleParams MakeVectorTextured(Rect rect, Rect uv, VectorImage vectorImage, ScaleMode scaleMode, Color playModeTintColor)
			{
				return new MeshGenerator.RectangleParams
				{
					rect = rect,
					subRect = new Rect(0f, 0f, 1f, 1f),
					uv = uv,
					color = Color.white,
					vectorImage = vectorImage,
					contentSize = new Vector2(vectorImage.width, vectorImage.height),
					scaleMode = scaleMode,
					playmodeTintColor = playModeTintColor
				};
			}

			// Token: 0x060024F6 RID: 9462 RVA: 0x0008F664 File Offset: 0x0008D864
			internal bool HasRadius(float epsilon)
			{
				return (this.topLeftRadius.x > epsilon && this.topLeftRadius.y > epsilon) || (this.topRightRadius.x > epsilon && this.topRightRadius.y > epsilon) || (this.bottomRightRadius.x > epsilon && this.bottomRightRadius.y > epsilon) || (this.bottomLeftRadius.x > epsilon && this.bottomLeftRadius.y > epsilon);
			}

			// Token: 0x060024F7 RID: 9463 RVA: 0x0008F6EC File Offset: 0x0008D8EC
			internal bool HasSlices(float epsilon)
			{
				return (float)this.leftSlice > epsilon || (float)this.topSlice > epsilon || (float)this.rightSlice > epsilon || (float)this.bottomSlice > epsilon;
			}

			// Token: 0x060024F8 RID: 9464 RVA: 0x0008F72C File Offset: 0x0008D92C
			internal MeshBuilderNative.NativeRectParams ToNativeParams()
			{
				return new MeshBuilderNative.NativeRectParams
				{
					rect = this.rect,
					subRect = this.subRect,
					backgroundRepeatRect = this.backgroundRepeatRect,
					uv = this.uv,
					color = this.color,
					scaleMode = this.scaleMode,
					topLeftRadius = this.topLeftRadius,
					topRightRadius = this.topRightRadius,
					bottomRightRadius = this.bottomRightRadius,
					bottomLeftRadius = this.bottomLeftRadius,
					spriteGeomRect = this.spriteGeomRect,
					contentSize = this.contentSize,
					textureSize = this.textureSize,
					texturePixelsPerPoint = 1f,
					leftSlice = this.leftSlice,
					topSlice = this.topSlice,
					rightSlice = this.rightSlice,
					bottomSlice = this.bottomSlice,
					sliceScale = this.sliceScale,
					rectInset = this.rectInset,
					colorPage = this.colorPage.ToNativeColorPage(),
					meshFlags = (int)this.meshFlags
				};
			}

			// Token: 0x040011F0 RID: 4592
			public Rect rect;

			// Token: 0x040011F1 RID: 4593
			public Rect uv;

			// Token: 0x040011F2 RID: 4594
			public Color color;

			// Token: 0x040011F3 RID: 4595
			public Rect subRect;

			// Token: 0x040011F4 RID: 4596
			public Rect backgroundRepeatRect;

			// Token: 0x040011F5 RID: 4597
			public NativePagedList<MeshGenerator.BackgroundRepeatInstance> backgroundRepeatInstanceList;

			// Token: 0x040011F6 RID: 4598
			public int backgroundRepeatInstanceListStartIndex;

			// Token: 0x040011F7 RID: 4599
			public int backgroundRepeatInstanceListEndIndex;

			// Token: 0x040011F8 RID: 4600
			public BackgroundPosition backgroundPositionX;

			// Token: 0x040011F9 RID: 4601
			public BackgroundPosition backgroundPositionY;

			// Token: 0x040011FA RID: 4602
			public BackgroundRepeat backgroundRepeat;

			// Token: 0x040011FB RID: 4603
			public BackgroundSize backgroundSize;

			// Token: 0x040011FC RID: 4604
			public Texture texture;

			// Token: 0x040011FD RID: 4605
			public Sprite sprite;

			// Token: 0x040011FE RID: 4606
			public VectorImage vectorImage;

			// Token: 0x040011FF RID: 4607
			public ScaleMode scaleMode;

			// Token: 0x04001200 RID: 4608
			public Color playmodeTintColor;

			// Token: 0x04001201 RID: 4609
			public Vector2 topLeftRadius;

			// Token: 0x04001202 RID: 4610
			public Vector2 topRightRadius;

			// Token: 0x04001203 RID: 4611
			public Vector2 bottomRightRadius;

			// Token: 0x04001204 RID: 4612
			public Vector2 bottomLeftRadius;

			// Token: 0x04001205 RID: 4613
			public Vector2 contentSize;

			// Token: 0x04001206 RID: 4614
			public Vector2 textureSize;

			// Token: 0x04001207 RID: 4615
			public int leftSlice;

			// Token: 0x04001208 RID: 4616
			public int topSlice;

			// Token: 0x04001209 RID: 4617
			public int rightSlice;

			// Token: 0x0400120A RID: 4618
			public int bottomSlice;

			// Token: 0x0400120B RID: 4619
			public float sliceScale;

			// Token: 0x0400120C RID: 4620
			internal Rect spriteGeomRect;

			// Token: 0x0400120D RID: 4621
			public Vector4 rectInset;

			// Token: 0x0400120E RID: 4622
			internal ColorPage colorPage;

			// Token: 0x0400120F RID: 4623
			internal MeshGenerationContext.MeshFlags meshFlags;
		}

		// Token: 0x0200053B RID: 1339
		private struct TessellationJobParameters
		{
			// Token: 0x04001210 RID: 4624
			public bool isBorderJob;

			// Token: 0x04001211 RID: 4625
			public MeshBuilderNative.NativeRectParams rectParams;

			// Token: 0x04001212 RID: 4626
			public MeshGenerator.BorderParams borderParams;

			// Token: 0x04001213 RID: 4627
			public UnsafeMeshGenerationNode node;
		}

		// Token: 0x0200053C RID: 1340
		private struct TessellationJob : IJobParallelFor
		{
			// Token: 0x060024F9 RID: 9465 RVA: 0x0008F86C File Offset: 0x0008DA6C
			public void Execute(int i)
			{
				MeshGenerator.TessellationJobParameters jobParams = this.jobParameters[i];
				bool isBorderJob = jobParams.isBorderJob;
				if (isBorderJob)
				{
					this.DrawBorder(jobParams.node, ref jobParams.borderParams);
				}
				else
				{
					ref MeshBuilderNative.NativeRectParams rectParams = ref jobParams.rectParams;
					bool flag = rectParams.vectorImage != IntPtr.Zero;
					if (flag)
					{
						this.DrawVectorImage(jobParams.node, ref rectParams, this.ExtractHandle<VectorImage>(rectParams.vectorImage));
					}
					else
					{
						bool flag2 = rectParams.sprite != IntPtr.Zero;
						if (flag2)
						{
							this.DrawSprite(jobParams.node, ref rectParams, this.ExtractHandle<Sprite>(rectParams.sprite));
						}
						else
						{
							this.DrawRectangle(jobParams.node, ref rectParams, this.ExtractHandle<Texture>(rectParams.texture));
						}
					}
				}
			}

			// Token: 0x060024FA RID: 9466 RVA: 0x0008F934 File Offset: 0x0008DB34
			private T ExtractHandle<T>(IntPtr handlePtr) where T : class
			{
				GCHandle handle = ((handlePtr != IntPtr.Zero) ? GCHandle.FromIntPtr(handlePtr) : default(GCHandle));
				return handle.IsAllocated ? (handle.Target as T) : default(T);
			}

			// Token: 0x060024FB RID: 9467 RVA: 0x0008F98C File Offset: 0x0008DB8C
			private unsafe void DrawBorder(UnsafeMeshGenerationNode node, ref MeshGenerator.BorderParams borderParams)
			{
				MeshWriteDataInterface meshData = MeshBuilderNative.MakeBorder(borderParams.ToNativeParams(), 0f);
				bool flag = meshData.vertexCount == 0 || meshData.indexCount == 0;
				if (!flag)
				{
					NativeSlice<Vertex> nativeVertices = UIRenderDevice.PtrToSlice<Vertex>((void*)meshData.vertices, meshData.vertexCount);
					NativeSlice<ushort> nativeIndices = UIRenderDevice.PtrToSlice<ushort>((void*)meshData.indices, meshData.indexCount);
					bool flag2 = nativeVertices.Length == 0 || nativeIndices.Length == 0;
					if (!flag2)
					{
						NativeSlice<Vertex> vertices;
						NativeSlice<ushort> indices;
						this.allocator.AllocateTempMesh(nativeVertices.Length, nativeIndices.Length, out vertices, out indices);
						Debug.Assert(vertices.Length == nativeVertices.Length);
						Debug.Assert(indices.Length == nativeIndices.Length);
						vertices.CopyFrom(nativeVertices);
						indices.CopyFrom(nativeIndices);
						node.DrawMesh(vertices, indices, null);
					}
				}
			}

			// Token: 0x060024FC RID: 9468 RVA: 0x0008FA80 File Offset: 0x0008DC80
			private void ApplyInset(ref MeshBuilderNative.NativeRectParams rectParams, Texture tex)
			{
				Rect rect = rectParams.rect;
				Vector4 inset = rectParams.rectInset;
				bool flag = Mathf.Approximately(rect.size.x, 0f) || Mathf.Approximately(rect.size.y, 0f) || inset == Vector4.zero;
				if (!flag)
				{
					Rect prevRect = rect;
					rect.x += inset.x;
					rect.y += inset.y;
					rect.width -= inset.x + inset.z;
					rect.height -= inset.y + inset.w;
					rectParams.rect = rect;
					Rect uv = rectParams.uv;
					bool flag2 = tex != null && uv.width > 1E-30f && uv.height > 1E-30f;
					if (flag2)
					{
						Vector2 uvScale = new Vector2(1f / prevRect.width, 1f / prevRect.height);
						uv.x += inset.x * uvScale.x;
						uv.y += inset.w * uvScale.y;
						uv.width -= (inset.x + inset.z) * uvScale.x;
						uv.height -= (inset.y + inset.w) * uvScale.y;
						rectParams.uv = uv;
					}
				}
			}

			// Token: 0x060024FD RID: 9469 RVA: 0x0008FC30 File Offset: 0x0008DE30
			private unsafe void DrawRectangle(UnsafeMeshGenerationNode node, ref MeshBuilderNative.NativeRectParams rectParams, Texture tex)
			{
				this.ApplyInset(ref rectParams, tex);
				bool flag = rectParams.backgroundRepeatInstanceList != IntPtr.Zero;
				if (flag)
				{
					NativePagedList<MeshGenerator.BackgroundRepeatInstance> list = ((GCHandle)rectParams.backgroundRepeatInstanceList).Target as NativePagedList<MeshGenerator.BackgroundRepeatInstance>;
					int rectCount = rectParams.backgroundRepeatInstanceListEndIndex - rectParams.backgroundRepeatInstanceListStartIndex;
					int freeVertices = Math.Min(4 * rectCount, (int)UIRenderDevice.maxVerticesPerPage);
					int freeIndices = Math.Min(6 * rectCount, (int)(UIRenderDevice.maxVerticesPerPage * 3U));
					int nextVerticesAllocSize = freeVertices;
					int nextIndicesAllocSize = freeIndices;
					NativeSlice<Vertex> vertices;
					NativeSlice<ushort> indices;
					this.allocator.AllocateTempMesh(nextVerticesAllocSize, nextIndicesAllocSize, out vertices, out indices);
					NativePagedList<MeshGenerator.BackgroundRepeatInstance>.Enumerator enumerator = new NativePagedList<MeshGenerator.BackgroundRepeatInstance>.Enumerator(list, rectParams.backgroundRepeatInstanceListStartIndex);
					for (int i = 0; i < rectCount; i++)
					{
						Debug.Assert(enumerator.HasNext());
						MeshGenerator.BackgroundRepeatInstance rect = enumerator.GetNext();
						rectParams.rect = rect.rect;
						rectParams.backgroundRepeatRect = rect.backgroundRepeatRect;
						rectParams.uv = rect.uv;
						bool flag2 = rectParams.texture != IntPtr.Zero;
						MeshWriteDataInterface meshData;
						if (flag2)
						{
							meshData = MeshBuilderNative.MakeTexturedRect(rectParams, 0f);
						}
						else
						{
							meshData = MeshBuilderNative.MakeSolidRect(rectParams, 0f);
						}
						bool flag3 = meshData.vertexCount == 0 || meshData.indexCount == 0;
						if (!flag3)
						{
							NativeSlice<Vertex> nativeVertices = UIRenderDevice.PtrToSlice<Vertex>((void*)meshData.vertices, meshData.vertexCount);
							NativeSlice<ushort> nativeIndices = UIRenderDevice.PtrToSlice<ushort>((void*)meshData.indices, meshData.indexCount);
							bool flag4 = freeVertices < meshData.vertexCount || freeIndices < meshData.indexCount;
							if (flag4)
							{
								bool flag5 = vertices.Length - freeVertices > 0 && indices.Length - freeIndices > 0;
								if (flag5)
								{
									node.DrawMesh(vertices.Slice(0, vertices.Length - freeVertices), indices.Slice(0, indices.Length - freeIndices), tex);
								}
								nextVerticesAllocSize = Math.Min(Math.Max(meshData.vertexCount, nextVerticesAllocSize) * 2, (int)UIRenderDevice.maxVerticesPerPage);
								nextIndicesAllocSize = Math.Min(Math.Max(meshData.indexCount, nextIndicesAllocSize) * 2, (int)(UIRenderDevice.maxVerticesPerPage * 3U));
								this.allocator.AllocateTempMesh(nextVerticesAllocSize, nextIndicesAllocSize, out vertices, out indices);
								freeVertices = nextVerticesAllocSize;
								freeIndices = nextIndicesAllocSize;
							}
							int start = vertices.Length - freeVertices;
							void* dstv = (void*)((byte*)vertices.GetUnsafePtr<Vertex>() + (IntPtr)start * (IntPtr)sizeof(Vertex));
							int sizev = meshData.vertexCount * sizeof(Vertex);
							UnsafeUtility.MemCpy(dstv, nativeVertices.GetUnsafePtr<Vertex>(), (long)sizev);
							ushort offset = (ushort)start;
							start = indices.Length - freeIndices;
							for (int j = 0; j < meshData.indexCount; j++)
							{
								indices[start + j] = nativeIndices[j] + offset;
							}
							freeVertices -= meshData.vertexCount;
							freeIndices -= meshData.indexCount;
						}
					}
					bool flag6 = vertices.Length - freeVertices > 0 && indices.Length - freeIndices > 0;
					if (flag6)
					{
						node.DrawMesh(vertices.Slice(0, vertices.Length - freeVertices), indices.Slice(0, indices.Length - freeIndices), tex);
					}
				}
				else
				{
					bool flag7 = rectParams.texture != IntPtr.Zero;
					MeshWriteDataInterface meshData2;
					if (flag7)
					{
						meshData2 = MeshBuilderNative.MakeTexturedRect(rectParams, 0f);
					}
					else
					{
						meshData2 = MeshBuilderNative.MakeSolidRect(rectParams, 0f);
					}
					bool flag8 = meshData2.vertexCount == 0 || meshData2.indexCount == 0;
					if (!flag8)
					{
						NativeSlice<Vertex> nativeVertices2 = UIRenderDevice.PtrToSlice<Vertex>((void*)meshData2.vertices, meshData2.vertexCount);
						NativeSlice<ushort> nativeIndices2 = UIRenderDevice.PtrToSlice<ushort>((void*)meshData2.indices, meshData2.indexCount);
						bool flag9 = nativeVertices2.Length == 0 || nativeIndices2.Length == 0;
						if (!flag9)
						{
							NativeSlice<Vertex> vertices2;
							NativeSlice<ushort> indices2;
							this.allocator.AllocateTempMesh(nativeVertices2.Length, nativeIndices2.Length, out vertices2, out indices2);
							Debug.Assert(vertices2.Length == nativeVertices2.Length);
							Debug.Assert(indices2.Length == nativeIndices2.Length);
							vertices2.CopyFrom(nativeVertices2);
							indices2.CopyFrom(nativeIndices2);
							node.DrawMesh(vertices2, indices2, tex);
						}
					}
				}
			}

			// Token: 0x060024FE RID: 9470 RVA: 0x000900A8 File Offset: 0x0008E2A8
			private void DrawSprite(UnsafeMeshGenerationNode node, ref MeshBuilderNative.NativeRectParams rectParams, Sprite sprite)
			{
				bool flag = rectParams.spriteTexture == IntPtr.Zero;
				if (!flag)
				{
					Texture2D spriteTexture = this.ExtractHandle<Texture2D>(rectParams.spriteTexture);
					Vector2[] spriteVertices = this.ExtractHandle<Vector2[]>(rectParams.spriteVertices);
					Vector2[] spriteUV = this.ExtractHandle<Vector2[]>(rectParams.spriteUVs);
					ushort[] spriteIndices = this.ExtractHandle<ushort[]>(rectParams.spriteTriangles);
					bool flag2 = spriteIndices != null && spriteIndices.Length == 0;
					if (!flag2)
					{
						int vertexCount = spriteVertices.Length;
						NativeSlice<Vertex> vertices;
						NativeSlice<ushort> indices;
						this.allocator.AllocateTempMesh(vertexCount, spriteIndices.Length, out vertices, out indices);
						MeshGenerator.AdjustSpriteWinding(spriteVertices, spriteIndices, indices);
						MeshBuilderNative.NativeColorPage colorPage = rectParams.colorPage;
						Color32 pageAndID = colorPage.pageAndID;
						Color32 flags = new Color32(0, 0, 0, (colorPage.isValid != 0) ? 1 : 0);
						Color32 page = new Color32(0, 0, colorPage.pageAndID.r, colorPage.pageAndID.g);
						Color32 ids = new Color32(0, 0, 0, colorPage.pageAndID.b);
						for (int i = 0; i < vertexCount; i++)
						{
							Vector2 v = spriteVertices[i];
							v -= rectParams.spriteGeomRect.position;
							v /= rectParams.spriteGeomRect.size;
							v.y = 1f - v.y;
							v *= rectParams.rect.size;
							v += rectParams.rect.position;
							vertices[i] = new Vertex
							{
								position = new Vector3(v.x, v.y, Vertex.nearZ),
								tint = rectParams.color,
								uv = spriteUV[i],
								flags = flags,
								opacityColorPages = page,
								ids = ids
							};
						}
						MeshGenerationContext.MeshFlags meshFlags = (MeshGenerationContext.MeshFlags)rectParams.meshFlags;
						bool skipAtlas = meshFlags == MeshGenerationContext.MeshFlags.SkipDynamicAtlas;
						node.DrawMeshInternal(vertices, indices, spriteTexture, skipAtlas);
					}
				}
			}

			// Token: 0x060024FF RID: 9471 RVA: 0x000902B8 File Offset: 0x0008E4B8
			private unsafe void DrawVectorImage(UnsafeMeshGenerationNode node, ref MeshBuilderNative.NativeRectParams rectParams, VectorImage vi)
			{
				bool isUsingGradients = (rectParams.meshFlags & 4) != 0;
				int vertexCount = vi.vertices.Length;
				Vertex[] svgVertices = new Vertex[vertexCount];
				for (int i = 0; i < vertexCount; i++)
				{
					VectorImageVertex v = vi.vertices[i];
					svgVertices[i] = new Vertex
					{
						position = v.position,
						tint = v.tint,
						uv = v.uv,
						settingIndex = new Color32((byte)(v.settingIndex >> 8), (byte)v.settingIndex, 0, 0),
						flags = v.flags,
						circle = v.circle
					};
				}
				bool flag = (float)rectParams.leftSlice <= 1E-30f && (float)rectParams.topSlice <= 1E-30f && (float)rectParams.rightSlice <= 1E-30f && (float)rectParams.bottomSlice <= 1E-30f;
				MeshWriteDataInterface meshData;
				if (flag)
				{
					meshData = MeshBuilderNative.MakeVectorGraphicsStretchBackground(svgVertices, vi.indices, vi.size.x, vi.size.y, rectParams.rect, rectParams.uv, rectParams.scaleMode, rectParams.color, rectParams.colorPage);
				}
				else
				{
					Vector4 sliceLTRB = new Vector4((float)rectParams.leftSlice, (float)rectParams.topSlice, (float)rectParams.rightSlice, (float)rectParams.bottomSlice);
					meshData = MeshBuilderNative.MakeVectorGraphics9SliceBackground(svgVertices, vi.indices, vi.size.x, vi.size.y, rectParams.rect, sliceLTRB, rectParams.color, rectParams.colorPage);
				}
				NativeSlice<Vertex> nativeVertices = UIRenderDevice.PtrToSlice<Vertex>((void*)meshData.vertices, meshData.vertexCount);
				NativeSlice<ushort> nativeIndices = UIRenderDevice.PtrToSlice<ushort>((void*)meshData.indices, meshData.indexCount);
				bool flag2 = nativeVertices.Length == 0 || nativeIndices.Length == 0;
				if (!flag2)
				{
					NativeSlice<Vertex> vertices;
					NativeSlice<ushort> indices;
					this.allocator.AllocateTempMesh(nativeVertices.Length, nativeIndices.Length, out vertices, out indices);
					Debug.Assert(vertices.Length == nativeVertices.Length);
					Debug.Assert(indices.Length == nativeIndices.Length);
					vertices.CopyFrom(nativeVertices);
					indices.CopyFrom(nativeIndices);
					bool flag3 = isUsingGradients;
					if (flag3)
					{
						node.DrawGradientsInternal(vertices, indices, vi);
					}
					else
					{
						node.DrawMesh(vertices, indices, null);
					}
				}
			}

			// Token: 0x04001214 RID: 4628
			[ReadOnly]
			public TempMeshAllocator allocator;

			// Token: 0x04001215 RID: 4629
			[ReadOnly]
			public NativeSlice<MeshGenerator.TessellationJobParameters> jobParameters;
		}
	}
}
