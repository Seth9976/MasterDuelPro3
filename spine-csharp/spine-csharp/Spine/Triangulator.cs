using System;

namespace Spine
{
	// Token: 0x02000086 RID: 134
	public class Triangulator
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x0001CFB4 File Offset: 0x0001B1B4
		public ExposedList<int> Triangulate(ExposedList<float> verticesArray)
		{
			float[] vertices = verticesArray.Items;
			int vertexCount = verticesArray.Count >> 1;
			ExposedList<int> indicesArray = this.indicesArray;
			indicesArray.Clear(true);
			int[] indices = indicesArray.Resize(vertexCount).Items;
			for (int i = 0; i < vertexCount; i++)
			{
				indices[i] = i;
			}
			ExposedList<bool> isConcaveArray = this.isConcaveArray;
			bool[] isConcave = isConcaveArray.Resize(vertexCount).Items;
			int j = 0;
			int k = vertexCount;
			while (j < k)
			{
				isConcave[j] = Triangulator.IsConcave(j, vertexCount, vertices, indices);
				j++;
			}
			ExposedList<int> triangles = this.triangles;
			triangles.Clear(true);
			triangles.EnsureCapacity(Math.Max(0, vertexCount - 2) << 2);
			while (vertexCount > 3)
			{
				int previous = vertexCount - 1;
				int l = 0;
				int next = 1;
				for (;;)
				{
					if (!isConcave[l])
					{
						int p = indices[previous] << 1;
						int p2 = indices[l] << 1;
						int p3 = indices[next] << 1;
						float p1x = vertices[p];
						float p1y = vertices[p + 1];
						float p2x = vertices[p2];
						float p2y = vertices[p2 + 1];
						float p3x = vertices[p3];
						float p3y = vertices[p3 + 1];
						for (int ii = (next + 1) % vertexCount; ii != previous; ii = (ii + 1) % vertexCount)
						{
							if (isConcave[ii])
							{
								int v = indices[ii] << 1;
								float vx = vertices[v];
								float vy = vertices[v + 1];
								if (Triangulator.PositiveArea(p3x, p3y, p1x, p1y, vx, vy) && Triangulator.PositiveArea(p1x, p1y, p2x, p2y, vx, vy) && Triangulator.PositiveArea(p2x, p2y, p3x, p3y, vx, vy))
								{
									goto IL_0169;
								}
							}
						}
						goto Block_7;
					}
					IL_0169:
					if (next == 0)
					{
						break;
					}
					previous = l;
					l = next;
					next = (next + 1) % vertexCount;
				}
				while (isConcave[l])
				{
					l--;
					if (l <= 0)
					{
						break;
					}
				}
				IL_0196:
				triangles.Add(indices[(vertexCount + l - 1) % vertexCount]);
				triangles.Add(indices[l]);
				triangles.Add(indices[(l + 1) % vertexCount]);
				indicesArray.RemoveAt(l);
				isConcaveArray.RemoveAt(l);
				vertexCount--;
				int previousIndex = (vertexCount + l - 1) % vertexCount;
				int nextIndex = ((l == vertexCount) ? 0 : l);
				isConcave[previousIndex] = Triangulator.IsConcave(previousIndex, vertexCount, vertices, indices);
				isConcave[nextIndex] = Triangulator.IsConcave(nextIndex, vertexCount, vertices, indices);
				continue;
				Block_7:
				goto IL_0196;
			}
			if (vertexCount == 3)
			{
				triangles.Add(indices[2]);
				triangles.Add(indices[0]);
				triangles.Add(indices[1]);
			}
			return triangles;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
		public ExposedList<ExposedList<float>> Decompose(ExposedList<float> verticesArray, ExposedList<int> triangles)
		{
			float[] vertices = verticesArray.Items;
			ExposedList<ExposedList<float>> convexPolygons = this.convexPolygons;
			int i = 0;
			int j = convexPolygons.Count;
			while (i < j)
			{
				this.polygonPool.Free(convexPolygons.Items[i]);
				i++;
			}
			convexPolygons.Clear(true);
			ExposedList<ExposedList<int>> convexPolygonsIndices = this.convexPolygonsIndices;
			int k = 0;
			int l = convexPolygonsIndices.Count;
			while (k < l)
			{
				this.polygonIndicesPool.Free(convexPolygonsIndices.Items[k]);
				k++;
			}
			convexPolygonsIndices.Clear(true);
			ExposedList<int> polygonIndices = this.polygonIndicesPool.Obtain();
			polygonIndices.Clear(true);
			ExposedList<float> polygon = this.polygonPool.Obtain();
			polygon.Clear(true);
			int fanBaseIndex = -1;
			int lastWinding = 0;
			int[] trianglesItems = triangles.Items;
			int m = 0;
			int n = triangles.Count;
			while (m < n)
			{
				int t = trianglesItems[m] << 1;
				int t2 = trianglesItems[m + 1] << 1;
				int t3 = trianglesItems[m + 2] << 1;
				float x = vertices[t];
				float y = vertices[t + 1];
				float x2 = vertices[t2];
				float y2 = vertices[t2 + 1];
				float x3 = vertices[t3];
				float y3 = vertices[t3 + 1];
				bool merged = false;
				if (fanBaseIndex == t)
				{
					int o = polygon.Count - 4;
					float[] p = polygon.Items;
					int num = Triangulator.Winding(p[o], p[o + 1], p[o + 2], p[o + 3], x3, y3);
					int winding2 = Triangulator.Winding(x3, y3, p[0], p[1], p[2], p[3]);
					if (num == lastWinding && winding2 == lastWinding)
					{
						polygon.Add(x3);
						polygon.Add(y3);
						polygonIndices.Add(t3);
						merged = true;
					}
				}
				if (!merged)
				{
					if (polygon.Count > 0)
					{
						convexPolygons.Add(polygon);
						convexPolygonsIndices.Add(polygonIndices);
					}
					else
					{
						this.polygonPool.Free(polygon);
						this.polygonIndicesPool.Free(polygonIndices);
					}
					polygon = this.polygonPool.Obtain();
					polygon.Clear(true);
					polygon.Add(x);
					polygon.Add(y);
					polygon.Add(x2);
					polygon.Add(y2);
					polygon.Add(x3);
					polygon.Add(y3);
					polygonIndices = this.polygonIndicesPool.Obtain();
					polygonIndices.Clear(true);
					polygonIndices.Add(t);
					polygonIndices.Add(t2);
					polygonIndices.Add(t3);
					lastWinding = Triangulator.Winding(x, y, x2, y2, x3, y3);
					fanBaseIndex = t;
				}
				m += 3;
			}
			if (polygon.Count > 0)
			{
				convexPolygons.Add(polygon);
				convexPolygonsIndices.Add(polygonIndices);
			}
			int i2 = 0;
			int n2 = convexPolygons.Count;
			while (i2 < n2)
			{
				polygonIndices = convexPolygonsIndices.Items[i2];
				if (polygonIndices.Count != 0)
				{
					int firstIndex = polygonIndices.Items[0];
					int lastIndex = polygonIndices.Items[polygonIndices.Count - 1];
					polygon = convexPolygons.Items[i2];
					int o2 = polygon.Count - 4;
					float[] items = polygon.Items;
					float prevPrevX = items[o2];
					float prevPrevY = items[o2 + 1];
					float prevX = items[o2 + 2];
					float prevY = items[o2 + 3];
					float firstX = items[0];
					float firstY = items[1];
					float secondX = items[2];
					float secondY = items[3];
					int winding3 = Triangulator.Winding(prevPrevX, prevPrevY, prevX, prevY, firstX, firstY);
					for (int ii = 0; ii < n2; ii++)
					{
						if (ii != i2)
						{
							ExposedList<int> otherIndices = convexPolygonsIndices.Items[ii];
							if (otherIndices.Count == 3)
							{
								int otherFirstIndex = otherIndices.Items[0];
								int otherSecondIndex = otherIndices.Items[1];
								int otherLastIndex = otherIndices.Items[2];
								ExposedList<float> otherPoly = convexPolygons.Items[ii];
								float x4 = otherPoly.Items[otherPoly.Count - 2];
								float y4 = otherPoly.Items[otherPoly.Count - 1];
								if (otherFirstIndex == firstIndex && otherSecondIndex == lastIndex)
								{
									int num2 = Triangulator.Winding(prevPrevX, prevPrevY, prevX, prevY, x4, y4);
									int winding4 = Triangulator.Winding(x4, y4, firstX, firstY, secondX, secondY);
									if (num2 == winding3 && winding4 == winding3)
									{
										otherPoly.Clear(true);
										otherIndices.Clear(true);
										polygon.Add(x4);
										polygon.Add(y4);
										polygonIndices.Add(otherLastIndex);
										prevPrevX = prevX;
										prevPrevY = prevY;
										prevX = x4;
										prevY = y4;
										ii = 0;
									}
								}
							}
						}
					}
				}
				i2++;
			}
			for (int i3 = convexPolygons.Count - 1; i3 >= 0; i3--)
			{
				polygon = convexPolygons.Items[i3];
				if (polygon.Count == 0)
				{
					convexPolygons.RemoveAt(i3);
					this.polygonPool.Free(polygon);
					polygonIndices = convexPolygonsIndices.Items[i3];
					convexPolygonsIndices.RemoveAt(i3);
					this.polygonIndicesPool.Free(polygonIndices);
				}
			}
			return convexPolygons;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001D694 File Offset: 0x0001B894
		private static bool IsConcave(int index, int vertexCount, float[] vertices, int[] indices)
		{
			int previous = indices[(vertexCount + index - 1) % vertexCount] << 1;
			int current = indices[index] << 1;
			int next = indices[(index + 1) % vertexCount] << 1;
			return !Triangulator.PositiveArea(vertices[previous], vertices[previous + 1], vertices[current], vertices[current + 1], vertices[next], vertices[next + 1]);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001D6DD File Offset: 0x0001B8DD
		private static bool PositiveArea(float p1x, float p1y, float p2x, float p2y, float p3x, float p3y)
		{
			return p1x * (p3y - p2y) + p2x * (p1y - p3y) + p3x * (p2y - p1y) >= 0f;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001D700 File Offset: 0x0001B900
		private static int Winding(float p1x, float p1y, float p2x, float p2y, float p3x, float p3y)
		{
			float px = p2x - p1x;
			float py = p2y - p1y;
			if (p3x * py - p3y * px + px * p1y - p1x * py < 0f)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x040002FA RID: 762
		private readonly ExposedList<ExposedList<float>> convexPolygons = new ExposedList<ExposedList<float>>();

		// Token: 0x040002FB RID: 763
		private readonly ExposedList<ExposedList<int>> convexPolygonsIndices = new ExposedList<ExposedList<int>>();

		// Token: 0x040002FC RID: 764
		private readonly ExposedList<int> indicesArray = new ExposedList<int>();

		// Token: 0x040002FD RID: 765
		private readonly ExposedList<bool> isConcaveArray = new ExposedList<bool>();

		// Token: 0x040002FE RID: 766
		private readonly ExposedList<int> triangles = new ExposedList<int>();

		// Token: 0x040002FF RID: 767
		private readonly Pool<ExposedList<float>> polygonPool = new Pool<ExposedList<float>>(16, int.MaxValue);

		// Token: 0x04000300 RID: 768
		private readonly Pool<ExposedList<int>> polygonIndicesPool = new Pool<ExposedList<int>>(16, int.MaxValue);
	}
}
