using System;

namespace Spine
{
	// Token: 0x02000078 RID: 120
	public class SkeletonClipping
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00015F30 File Offset: 0x00014130
		public ExposedList<float> ClippedVertices
		{
			get
			{
				return this.clippedVertices;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00015F38 File Offset: 0x00014138
		public ExposedList<int> ClippedTriangles
		{
			get
			{
				return this.clippedTriangles;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00015F40 File Offset: 0x00014140
		public ExposedList<float> ClippedUVs
		{
			get
			{
				return this.clippedUVs;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00015F48 File Offset: 0x00014148
		public bool IsClipping
		{
			get
			{
				return this.clipAttachment != null;
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00015F54 File Offset: 0x00014154
		public int ClipStart(Slot slot, ClippingAttachment clip)
		{
			if (this.clipAttachment != null)
			{
				return 0;
			}
			this.clipAttachment = clip;
			int i = clip.worldVerticesLength;
			float[] vertices = this.clippingPolygon.Resize(i).Items;
			clip.ComputeWorldVertices(slot, 0, i, vertices, 0, 2);
			SkeletonClipping.MakeClockwise(this.clippingPolygon);
			this.clippingPolygons = this.triangulator.Decompose(this.clippingPolygon, this.triangulator.Triangulate(this.clippingPolygon));
			foreach (ExposedList<float> exposedList in this.clippingPolygons)
			{
				SkeletonClipping.MakeClockwise(exposedList);
				exposedList.Add(exposedList.Items[0]);
				exposedList.Add(exposedList.Items[1]);
			}
			return this.clippingPolygons.Count;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00016034 File Offset: 0x00014234
		public void ClipEnd(Slot slot)
		{
			if (this.clipAttachment != null && this.clipAttachment.endSlot == slot.data)
			{
				this.ClipEnd();
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00016057 File Offset: 0x00014257
		public void ClipEnd()
		{
			if (this.clipAttachment == null)
			{
				return;
			}
			this.clipAttachment = null;
			this.clippingPolygons = null;
			this.clippedVertices.Clear(true);
			this.clippedTriangles.Clear(true);
			this.clippingPolygon.Clear(true);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00016094 File Offset: 0x00014294
		public void ClipTriangles(float[] vertices, int[] triangles, int trianglesLength)
		{
			ExposedList<float> clipOutput = this.clipOutput;
			ExposedList<float> clippedVertices = this.clippedVertices;
			ExposedList<int> clippedTriangles = this.clippedTriangles;
			ExposedList<float>[] polygons = this.clippingPolygons.Items;
			int polygonsCount = this.clippingPolygons.Count;
			int index = 0;
			clippedVertices.Clear(true);
			clippedTriangles.Clear(true);
			for (int i = 0; i < trianglesLength; i += 3)
			{
				int vertexOffset = triangles[i] << 1;
				float x = vertices[vertexOffset];
				float y = vertices[vertexOffset + 1];
				vertexOffset = triangles[i + 1] << 1;
				float x2 = vertices[vertexOffset];
				float y2 = vertices[vertexOffset + 1];
				vertexOffset = triangles[i + 2] << 1;
				float x3 = vertices[vertexOffset];
				float y3 = vertices[vertexOffset + 1];
				for (int p = 0; p < polygonsCount; p++)
				{
					int s = clippedVertices.Count;
					if (!this.Clip(x, y, x2, y2, x3, y3, polygons[p], clipOutput))
					{
						float[] items = clippedVertices.Resize(s + 6).Items;
						items[s] = x;
						items[s + 1] = y;
						items[s + 2] = x2;
						items[s + 3] = y2;
						items[s + 4] = x3;
						items[s + 5] = y3;
						s = clippedTriangles.Count;
						int[] items2 = clippedTriangles.Resize(s + 3).Items;
						items2[s] = index;
						items2[s + 1] = index + 1;
						items2[s + 2] = index + 2;
						index += 3;
						break;
					}
					int clipOutputLength = clipOutput.Count;
					if (clipOutputLength != 0)
					{
						int clipOutputCount = clipOutputLength >> 1;
						float[] clipOutputItems = clipOutput.Items;
						float[] clippedVerticesItems = clippedVertices.Resize(s + clipOutputCount * 2).Items;
						int ii = 0;
						while (ii < clipOutputLength)
						{
							float x4 = clipOutputItems[ii];
							float y4 = clipOutputItems[ii + 1];
							clippedVerticesItems[s] = x4;
							clippedVerticesItems[s + 1] = y4;
							ii += 2;
							s += 2;
						}
						s = clippedTriangles.Count;
						int[] clippedTrianglesItems = clippedTriangles.Resize(s + 3 * (clipOutputCount - 2)).Items;
						clipOutputCount--;
						int ii2 = 1;
						while (ii2 < clipOutputCount)
						{
							clippedTrianglesItems[s] = index;
							clippedTrianglesItems[s + 1] = index + ii2;
							clippedTrianglesItems[s + 2] = index + ii2 + 1;
							ii2++;
							s += 3;
						}
						index += clipOutputCount + 1;
					}
				}
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000162BC File Offset: 0x000144BC
		public void ClipTriangles(float[] vertices, int[] triangles, int trianglesLength, float[] uvs)
		{
			ExposedList<float> clipOutput = this.clipOutput;
			ExposedList<float> clippedVertices = this.clippedVertices;
			ExposedList<int> clippedTriangles = this.clippedTriangles;
			ExposedList<float>[] polygons = this.clippingPolygons.Items;
			int polygonsCount = this.clippingPolygons.Count;
			int index = 0;
			clippedVertices.Clear(true);
			this.clippedUVs.Clear(true);
			clippedTriangles.Clear(true);
			for (int i = 0; i < trianglesLength; i += 3)
			{
				int vertexOffset = triangles[i] << 1;
				float x = vertices[vertexOffset];
				float y = vertices[vertexOffset + 1];
				float u = uvs[vertexOffset];
				float v = uvs[vertexOffset + 1];
				vertexOffset = triangles[i + 1] << 1;
				float x2 = vertices[vertexOffset];
				float y2 = vertices[vertexOffset + 1];
				float u2 = uvs[vertexOffset];
				float v2 = uvs[vertexOffset + 1];
				vertexOffset = triangles[i + 2] << 1;
				float x3 = vertices[vertexOffset];
				float y3 = vertices[vertexOffset + 1];
				float u3 = uvs[vertexOffset];
				float v3 = uvs[vertexOffset + 1];
				for (int p = 0; p < polygonsCount; p++)
				{
					int s = clippedVertices.Count;
					if (!this.Clip(x, y, x2, y2, x3, y3, polygons[p], clipOutput))
					{
						float[] items = clippedVertices.Resize(s + 6).Items;
						float[] clippedUVsItems = this.clippedUVs.Resize(s + 6).Items;
						items[s] = x;
						items[s + 1] = y;
						items[s + 2] = x2;
						items[s + 3] = y2;
						items[s + 4] = x3;
						items[s + 5] = y3;
						clippedUVsItems[s] = u;
						clippedUVsItems[s + 1] = v;
						clippedUVsItems[s + 2] = u2;
						clippedUVsItems[s + 3] = v2;
						clippedUVsItems[s + 4] = u3;
						clippedUVsItems[s + 5] = v3;
						s = clippedTriangles.Count;
						int[] items2 = clippedTriangles.Resize(s + 3).Items;
						items2[s] = index;
						items2[s + 1] = index + 1;
						items2[s + 2] = index + 2;
						index += 3;
						break;
					}
					int clipOutputLength = clipOutput.Count;
					if (clipOutputLength != 0)
					{
						float d0 = y2 - y3;
						float d = x3 - x2;
						float d2 = x - x3;
						float d3 = y3 - y;
						float d4 = 1f / (d0 * d2 + d * (y - y3));
						int clipOutputCount = clipOutputLength >> 1;
						float[] clipOutputItems = clipOutput.Items;
						float[] clippedVerticesItems = clippedVertices.Resize(s + clipOutputCount * 2).Items;
						float[] clippedUVsItems2 = this.clippedUVs.Resize(s + clipOutputCount * 2).Items;
						int ii = 0;
						while (ii < clipOutputLength)
						{
							float x4 = clipOutputItems[ii];
							float y4 = clipOutputItems[ii + 1];
							clippedVerticesItems[s] = x4;
							clippedVerticesItems[s + 1] = y4;
							float c0 = x4 - x3;
							float c = y4 - y3;
							float a = (d0 * c0 + d * c) * d4;
							float b = (d3 * c0 + d2 * c) * d4;
							float c2 = 1f - a - b;
							clippedUVsItems2[s] = u * a + u2 * b + u3 * c2;
							clippedUVsItems2[s + 1] = v * a + v2 * b + v3 * c2;
							ii += 2;
							s += 2;
						}
						s = clippedTriangles.Count;
						int[] clippedTrianglesItems = clippedTriangles.Resize(s + 3 * (clipOutputCount - 2)).Items;
						clipOutputCount--;
						int ii2 = 1;
						while (ii2 < clipOutputCount)
						{
							clippedTrianglesItems[s] = index;
							clippedTrianglesItems[s + 1] = index + ii2;
							clippedTrianglesItems[s + 2] = index + ii2 + 1;
							ii2++;
							s += 3;
						}
						index += clipOutputCount + 1;
					}
				}
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00016628 File Offset: 0x00014828
		internal bool Clip(float x1, float y1, float x2, float y2, float x3, float y3, ExposedList<float> clippingArea, ExposedList<float> output)
		{
			ExposedList<float> originalOutput = output;
			bool clipped = false;
			ExposedList<float> input;
			if (clippingArea.Count % 4 >= 2)
			{
				input = output;
				output = this.scratch;
			}
			else
			{
				input = this.scratch;
			}
			input.Clear(true);
			input.Add(x1);
			input.Add(y1);
			input.Add(x2);
			input.Add(y2);
			input.Add(x3);
			input.Add(y3);
			input.Add(x1);
			input.Add(y1);
			output.Clear(true);
			int clippingVerticesLast = clippingArea.Count - 4;
			float[] clippingVertices = clippingArea.Items;
			int i = 0;
			for (;;)
			{
				float edgeX = clippingVertices[i];
				float edgeY = clippingVertices[i + 1];
				float ex = edgeX - clippingVertices[i + 2];
				float ey = edgeY - clippingVertices[i + 3];
				int outputStart = output.Count;
				float[] inputVertices = input.Items;
				int ii = 0;
				int nn = input.Count - 2;
				while (ii < nn)
				{
					float inputX = inputVertices[ii];
					float inputY = inputVertices[ii + 1];
					ii += 2;
					float inputX2 = inputVertices[ii];
					float inputY2 = inputVertices[ii + 1];
					bool s2 = ey * (edgeX - inputX2) > ex * (edgeY - inputY2);
					float s3 = ey * (edgeX - inputX) - ex * (edgeY - inputY);
					if (s3 > 0f)
					{
						if (s2)
						{
							output.Add(inputX2);
							output.Add(inputY2);
							continue;
						}
						float ix = inputX2 - inputX;
						float iy = inputY2 - inputY;
						float t = s3 / (ix * ey - iy * ex);
						if (t < 0f || t > 1f)
						{
							output.Add(inputX2);
							output.Add(inputY2);
							continue;
						}
						output.Add(inputX + ix * t);
						output.Add(inputY + iy * t);
					}
					else if (s2)
					{
						float ix2 = inputX2 - inputX;
						float iy2 = inputY2 - inputY;
						float t2 = s3 / (ix2 * ey - iy2 * ex);
						if (t2 < 0f || t2 > 1f)
						{
							output.Add(inputX2);
							output.Add(inputY2);
							continue;
						}
						output.Add(inputX + ix2 * t2);
						output.Add(inputY + iy2 * t2);
						output.Add(inputX2);
						output.Add(inputY2);
					}
					clipped = true;
				}
				if (outputStart == output.Count)
				{
					break;
				}
				output.Add(output.Items[0]);
				output.Add(output.Items[1]);
				if (i == clippingVerticesLast)
				{
					goto IL_0284;
				}
				ExposedList<float> exposedList = output;
				output = input;
				output.Clear(true);
				input = exposedList;
				i += 2;
			}
			originalOutput.Clear(true);
			return true;
			IL_0284:
			if (originalOutput != output)
			{
				originalOutput.Clear(true);
				int j = 0;
				int k = output.Count - 2;
				while (j < k)
				{
					originalOutput.Add(output.Items[j]);
					j++;
				}
			}
			else
			{
				originalOutput.Resize(originalOutput.Count - 2);
			}
			return clipped;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00016904 File Offset: 0x00014B04
		public static void MakeClockwise(ExposedList<float> polygon)
		{
			float[] vertices = polygon.Items;
			int verticeslength = polygon.Count;
			float area = vertices[verticeslength - 2] * vertices[1] - vertices[0] * vertices[verticeslength - 1];
			int i = 0;
			int j = verticeslength - 3;
			while (i < j)
			{
				float p1x = vertices[i];
				float p1y = vertices[i + 1];
				float p2x = vertices[i + 2];
				float p2y = vertices[i + 3];
				area += p1x * p2y - p2x * p1y;
				i += 2;
			}
			if (area < 0f)
			{
				return;
			}
			int k = 0;
			int lastX = verticeslength - 2;
			int l = verticeslength >> 1;
			while (k < l)
			{
				float x = vertices[k];
				float y = vertices[k + 1];
				int other = lastX - k;
				vertices[k] = vertices[other];
				vertices[k + 1] = vertices[other + 1];
				vertices[other] = x;
				vertices[other + 1] = y;
				k += 2;
			}
		}

		// Token: 0x0400028E RID: 654
		internal readonly Triangulator triangulator = new Triangulator();

		// Token: 0x0400028F RID: 655
		internal readonly ExposedList<float> clippingPolygon = new ExposedList<float>();

		// Token: 0x04000290 RID: 656
		internal readonly ExposedList<float> clipOutput = new ExposedList<float>(128);

		// Token: 0x04000291 RID: 657
		internal readonly ExposedList<float> clippedVertices = new ExposedList<float>(128);

		// Token: 0x04000292 RID: 658
		internal readonly ExposedList<int> clippedTriangles = new ExposedList<int>(128);

		// Token: 0x04000293 RID: 659
		internal readonly ExposedList<float> clippedUVs = new ExposedList<float>(128);

		// Token: 0x04000294 RID: 660
		internal readonly ExposedList<float> scratch = new ExposedList<float>();

		// Token: 0x04000295 RID: 661
		internal ClippingAttachment clipAttachment;

		// Token: 0x04000296 RID: 662
		internal ExposedList<ExposedList<float>> clippingPolygons;
	}
}
