using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x0200009D RID: 157
	internal struct Tessellator
	{
		// Token: 0x060003BE RID: 958 RVA: 0x0001ADD8 File Offset: 0x00018FD8
		private static float FindSplit(UHull hull, UEvent edge)
		{
			float d;
			if (hull.a.x < edge.a.x)
			{
				d = ModuleHandle.OrientFast(hull.a, hull.b, edge.a);
			}
			else
			{
				d = ModuleHandle.OrientFast(edge.b, edge.a, hull.a);
			}
			if (0f != d)
			{
				return d;
			}
			if (edge.b.x < hull.b.x)
			{
				d = ModuleHandle.OrientFast(hull.a, hull.b, edge.b);
			}
			else
			{
				d = ModuleHandle.OrientFast(edge.b, edge.a, hull.b);
			}
			if (0f != d)
			{
				return d;
			}
			return (float)(hull.idx - edge.idx);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001AEA1 File Offset: 0x000190A1
		private void SetAllocator(Allocator allocator)
		{
			this.m_Allocator = allocator;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001AEAC File Offset: 0x000190AC
		private bool AddPoint(NativeArray<UHull> hulls, int hullCount, NativeArray<float2> points, float2 p, int idx)
		{
			int i = ModuleHandle.GetLower<UHull, float2, Tessellator.TestHullPointL>(hulls, hullCount, p, default(Tessellator.TestHullPointL));
			int u = ModuleHandle.GetUpper<UHull, float2, Tessellator.TestHullPointU>(hulls, hullCount, p, default(Tessellator.TestHullPointU));
			if (i < 0 || u < 0)
			{
				return false;
			}
			for (int j = i; j < u; j++)
			{
				UHull hull = hulls[j];
				int k = hull.ilcount;
				while (k > 1 && ModuleHandle.OrientFast(points[hull.ilarray[k - 2]], points[hull.ilarray[k - 1]], p) > 0f)
				{
					int3 c = default(int3);
					c.x = hull.ilarray[k - 1];
					c.y = hull.ilarray[k - 2];
					c.z = idx;
					int num = this.m_CellCount;
					this.m_CellCount = num + 1;
					this.m_Cells[num] = c;
					k--;
				}
				hull.ilcount = k + 1;
				if (hull.ilcount > hull.ilarray.Length)
				{
					return false;
				}
				hull.ilarray[k] = idx;
				k = hull.iucount;
				while (k > 1 && ModuleHandle.OrientFast(points[hull.iuarray[k - 2]], points[hull.iuarray[k - 1]], p) < 0f)
				{
					int3 c2 = default(int3);
					c2.x = hull.iuarray[k - 2];
					c2.y = hull.iuarray[k - 1];
					c2.z = idx;
					int num = this.m_CellCount;
					this.m_CellCount = num + 1;
					this.m_Cells[num] = c2;
					k--;
				}
				hull.iucount = k + 1;
				if (hull.iucount > hull.iuarray.Length)
				{
					return false;
				}
				hull.iuarray[k] = idx;
				hulls[j] = hull;
			}
			return true;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001B0E4 File Offset: 0x000192E4
		private static void InsertHull(NativeArray<UHull> Hulls, int Pos, ref int Count, UHull Value)
		{
			if (Count < Hulls.Length - 1)
			{
				for (int i = Count; i > Pos; i--)
				{
					Hulls[i] = Hulls[i - 1];
				}
				Hulls[Pos] = Value;
				Count++;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001B12C File Offset: 0x0001932C
		private static void EraseHull(NativeArray<UHull> Hulls, int Pos, ref int Count)
		{
			if (Count < Hulls.Length)
			{
				for (int i = Pos; i < Count - 1; i++)
				{
					Hulls[i] = Hulls[i + 1];
				}
				Count--;
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001B16C File Offset: 0x0001936C
		private bool SplitHulls(NativeArray<UHull> hulls, ref int hullCount, NativeArray<float2> points, UEvent evt)
		{
			int index = ModuleHandle.GetLower<UHull, UEvent, Tessellator.TestHullEventLe>(hulls, hullCount, evt, default(Tessellator.TestHullEventLe));
			if (index < 0)
			{
				return false;
			}
			UHull hull = hulls[index];
			UHull newHull;
			newHull.a = evt.a;
			newHull.b = evt.b;
			newHull.idx = evt.idx;
			int y = hull.iuarray[hull.iucount - 1];
			newHull.iuarray = new ArraySlice<int>(this.m_IUArray, newHull.idx * this.m_NumHulls, this.m_NumHulls);
			newHull.iucount = hull.iucount;
			for (int i = 0; i < newHull.iucount; i++)
			{
				newHull.iuarray[i] = hull.iuarray[i];
			}
			hull.iuarray[0] = y;
			hull.iucount = 1;
			hulls[index] = hull;
			newHull.ilarray = new ArraySlice<int>(this.m_ILArray, newHull.idx * this.m_NumHulls, this.m_NumHulls);
			newHull.ilarray[0] = y;
			newHull.ilcount = 1;
			Tessellator.InsertHull(hulls, index + 1, ref hullCount, newHull);
			return true;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001B2A8 File Offset: 0x000194A8
		private bool MergeHulls(NativeArray<UHull> hulls, ref int hullCount, NativeArray<float2> points, UEvent evt)
		{
			float2 temp = evt.a;
			evt.a = evt.b;
			evt.b = temp;
			int index = ModuleHandle.GetEqual<UHull, UEvent, Tessellator.TestHullEventE>(hulls, hullCount, evt, default(Tessellator.TestHullEventE));
			if (index < 0)
			{
				return false;
			}
			UHull upper = hulls[index];
			UHull lower = hulls[index - 1];
			lower.iucount = upper.iucount;
			for (int i = 0; i < lower.iucount; i++)
			{
				lower.iuarray[i] = upper.iuarray[i];
			}
			hulls[index - 1] = lower;
			Tessellator.EraseHull(hulls, index, ref hullCount);
			return true;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001B354 File Offset: 0x00019554
		private static void InsertUniqueEdge(NativeArray<int2> edges, int2 e, ref int edgeCount)
		{
			TessEdgeCompare edgeComparer = default(TessEdgeCompare);
			bool validEdge = true;
			int i = 0;
			while (validEdge && i < edgeCount)
			{
				if (edgeComparer.Compare(e, edges[i]) == 0)
				{
					validEdge = false;
				}
				i++;
			}
			if (validEdge)
			{
				int num = edgeCount;
				edgeCount = num + 1;
				edges[num] = e;
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001B3A4 File Offset: 0x000195A4
		private void PrepareDelaunay(NativeArray<int2> edges, int edgeCount)
		{
			this.m_StarCount = this.m_CellCount * 3;
			this.m_Stars = new NativeArray<UStar>(this.m_StarCount, this.m_Allocator, NativeArrayOptions.ClearMemory);
			this.m_SPArray = new NativeArray<int>(this.m_StarCount * this.m_StarCount, this.m_Allocator, NativeArrayOptions.ClearMemory);
			int UEdgeCount = 0;
			NativeArray<int2> UEdges = new NativeArray<int2>(this.m_StarCount, this.m_Allocator, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < edgeCount; i++)
			{
				int2 e = edges[i];
				e.x = ((edges[i].x < edges[i].y) ? edges[i].x : edges[i].y);
				e.y = ((edges[i].x > edges[i].y) ? edges[i].x : edges[i].y);
				edges[i] = e;
				Tessellator.InsertUniqueEdge(UEdges, e, ref UEdgeCount);
			}
			this.m_Edges = new NativeArray<int2>(UEdgeCount, this.m_Allocator, NativeArrayOptions.ClearMemory);
			for (int j = 0; j < UEdgeCount; j++)
			{
				this.m_Edges[j] = UEdges[j];
			}
			UEdges.Dispose();
			ModuleHandle.InsertionSort<int2, TessEdgeCompare>(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<int2>(this.m_Edges), 0, this.m_Edges.Length - 1, default(TessEdgeCompare));
			for (int k = 0; k < this.m_StarCount; k++)
			{
				UStar s = this.m_Stars[k];
				s.points = new ArraySlice<int>(this.m_SPArray, k * this.m_StarCount, this.m_StarCount);
				s.pointCount = 0;
				this.m_Stars[k] = s;
			}
			for (int l = 0; l < this.m_CellCount; l++)
			{
				int a = this.m_Cells[l].x;
				int b = this.m_Cells[l].y;
				int c = this.m_Cells[l].z;
				UStar sa = this.m_Stars[a];
				UStar sb = this.m_Stars[b];
				UStar sc = this.m_Stars[c];
				int num = sa.pointCount;
				sa.pointCount = num + 1;
				sa.points[num] = b;
				num = sa.pointCount;
				sa.pointCount = num + 1;
				sa.points[num] = c;
				num = sb.pointCount;
				sb.pointCount = num + 1;
				sb.points[num] = c;
				num = sb.pointCount;
				sb.pointCount = num + 1;
				sb.points[num] = a;
				num = sc.pointCount;
				sc.pointCount = num + 1;
				sc.points[num] = a;
				num = sc.pointCount;
				sc.pointCount = num + 1;
				sc.points[num] = b;
				this.m_Stars[a] = sa;
				this.m_Stars[b] = sb;
				this.m_Stars[c] = sc;
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001B6EC File Offset: 0x000198EC
		private int OppositeOf(int a, int b)
		{
			ArraySlice<int> points = this.m_Stars[b].points;
			int i = 1;
			int j = this.m_Stars[b].pointCount;
			while (i < j)
			{
				if (points[i] == a)
				{
					return points[i - 1];
				}
				i += 2;
			}
			return -1;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001B740 File Offset: 0x00019940
		private int FindConstraint(int a, int b)
		{
			int2 e;
			e.x = ((a < b) ? a : b);
			e.y = ((a > b) ? a : b);
			return ModuleHandle.GetEqual<int2, int2, Tessellator.TestEdgePointE>(this.m_Edges, this.m_Edges.Length, e, default(Tessellator.TestEdgePointE));
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001B78C File Offset: 0x0001998C
		private void AddTriangle(int i, int j, int k)
		{
			UStar si = this.m_Stars[i];
			UStar sj = this.m_Stars[j];
			UStar sk = this.m_Stars[k];
			int num = si.pointCount;
			si.pointCount = num + 1;
			si.points[num] = j;
			num = si.pointCount;
			si.pointCount = num + 1;
			si.points[num] = k;
			num = sj.pointCount;
			sj.pointCount = num + 1;
			sj.points[num] = k;
			num = sj.pointCount;
			sj.pointCount = num + 1;
			sj.points[num] = i;
			num = sk.pointCount;
			sk.pointCount = num + 1;
			sk.points[num] = i;
			num = sk.pointCount;
			sk.pointCount = num + 1;
			sk.points[num] = j;
			this.m_Stars[i] = si;
			this.m_Stars[j] = sj;
			this.m_Stars[k] = sk;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001B890 File Offset: 0x00019A90
		private void RemovePair(int r, int j, int k)
		{
			UStar s = this.m_Stars[r];
			ArraySlice<int> points = s.points;
			int i = 1;
			int l = s.pointCount;
			while (i < l)
			{
				if (points[i - 1] == j && points[i] == k)
				{
					points[i - 1] = points[l - 2];
					points[i] = points[l - 1];
					s.points = points;
					s.pointCount -= 2;
					this.m_Stars[r] = s;
					return;
				}
				i += 2;
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001B927 File Offset: 0x00019B27
		private void RemoveTriangle(int i, int j, int k)
		{
			this.RemovePair(i, j, k);
			this.RemovePair(j, k, i);
			this.RemovePair(k, i, j);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001B944 File Offset: 0x00019B44
		private void EdgeFlip(int i, int j)
		{
			int a = this.OppositeOf(i, j);
			int b = this.OppositeOf(j, i);
			this.RemoveTriangle(i, j, a);
			this.RemoveTriangle(j, i, b);
			this.AddTriangle(i, b, a);
			this.AddTriangle(j, a, b);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001B988 File Offset: 0x00019B88
		private bool Flip(NativeArray<float2> points, ref NativeArray<int> stack, ref int stackCount, int a, int b, int x)
		{
			int y = this.OppositeOf(a, b);
			if (y < 0)
			{
				return true;
			}
			if (b < a)
			{
				int num = a;
				a = b;
				b = num;
				int num2 = x;
				x = y;
				y = num2;
			}
			if (this.FindConstraint(a, b) != -1)
			{
				return true;
			}
			if (ModuleHandle.IsInsideCircle(points[a], points[b], points[x], points[y]))
			{
				if (2 + stackCount >= stack.Length)
				{
					return false;
				}
				int num3 = stackCount;
				stackCount = num3 + 1;
				stack[num3] = a;
				num3 = stackCount;
				stackCount = num3 + 1;
				stack[num3] = b;
			}
			return true;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001BA24 File Offset: 0x00019C24
		private NativeArray<int3> GetCells(ref int count)
		{
			NativeArray<int3> cellsOut = new NativeArray<int3>(this.m_NumPoints * (this.m_NumPoints + 1), this.m_Allocator, NativeArrayOptions.ClearMemory);
			count = 0;
			int i = 0;
			int j = this.m_Stars.Length;
			while (i < j)
			{
				ArraySlice<int> points = this.m_Stars[i].points;
				int k = 0;
				int l = this.m_Stars[i].pointCount;
				while (k < l)
				{
					int s = points[k];
					int t = points[k + 1];
					if (i < math.min(s, t))
					{
						int3 c = default(int3);
						c.x = i;
						c.y = s;
						c.z = t;
						int num = count;
						count = num + 1;
						cellsOut[num] = c;
					}
					k += 2;
				}
				i++;
			}
			return cellsOut;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001BB04 File Offset: 0x00019D04
		internal bool ApplyDelaunay(NativeArray<float2> points, NativeArray<int2> edges)
		{
			if (this.m_CellCount == 0)
			{
				return false;
			}
			NativeArray<int> stack = new NativeArray<int>(this.m_NumPoints * (this.m_NumPoints + 1), this.m_Allocator, NativeArrayOptions.ClearMemory);
			int stackCount = 0;
			bool valid = true;
			this.PrepareDelaunay(edges, this.m_NumEdges);
			int a = 0;
			while (valid && a < this.m_NumPoints)
			{
				UStar star = this.m_Stars[a];
				for (int i = 1; i < star.pointCount; i += 2)
				{
					int b = star.points[i];
					if (b >= a && this.FindConstraint(a, b) < 0)
					{
						int x = star.points[i - 1];
						int y = -1;
						for (int j = 1; j < star.pointCount; j += 2)
						{
							if (star.points[j - 1] == b)
							{
								y = star.points[j];
								break;
							}
						}
						if (y >= 0 && ModuleHandle.IsInsideCircle(points[a], points[b], points[x], points[y]))
						{
							if (2 + stackCount >= stack.Length)
							{
								valid = false;
								break;
							}
							stack[stackCount++] = a;
							stack[stackCount++] = b;
						}
					}
				}
				a++;
			}
			int flipFlops = this.m_NumPoints * this.m_NumPoints;
			while (stackCount > 0 && valid)
			{
				int b2 = stack[stackCount - 1];
				stackCount--;
				int a2 = stack[stackCount - 1];
				stackCount--;
				int x2 = -1;
				int y2 = -1;
				UStar star2 = this.m_Stars[a2];
				for (int k = 1; k < star2.pointCount; k += 2)
				{
					int s = star2.points[k - 1];
					int t = star2.points[k];
					if (s == b2)
					{
						y2 = t;
					}
					else if (t == b2)
					{
						x2 = s;
					}
				}
				if (x2 >= 0 && y2 >= 0 && ModuleHandle.IsInsideCircle(points[a2], points[b2], points[x2], points[y2]))
				{
					this.EdgeFlip(a2, b2);
					valid = this.Flip(points, ref stack, ref stackCount, x2, a2, y2);
					valid = valid && this.Flip(points, ref stack, ref stackCount, a2, y2, x2);
					valid = valid && this.Flip(points, ref stack, ref stackCount, y2, b2, x2);
					valid = valid && this.Flip(points, ref stack, ref stackCount, b2, x2, y2);
					valid = valid && --flipFlops > 0;
				}
			}
			stack.Dispose();
			return valid;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001BDC0 File Offset: 0x00019FC0
		private int FindNeighbor(NativeArray<int3> cells, int count, int a, int b, int c)
		{
			int x = a;
			int y = b;
			int z = c;
			if (b < c)
			{
				if (b < a)
				{
					x = b;
					y = c;
					z = a;
				}
			}
			else if (c < a)
			{
				x = c;
				y = a;
				z = b;
			}
			if (x < 0)
			{
				return -1;
			}
			int3 key;
			key.x = x;
			key.y = y;
			key.z = z;
			return ModuleHandle.GetEqual<int3, int3, Tessellator.TestCellE>(cells, count, key, default(Tessellator.TestCellE));
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001BE28 File Offset: 0x0001A028
		private NativeArray<int3> Constrain(ref int count)
		{
			NativeArray<int3> cells = this.GetCells(ref count);
			int nc = count;
			for (int i = 0; i < nc; i++)
			{
				int3 c = cells[i];
				int x = c.x;
				int y = c.y;
				int z = c.z;
				if (y < z)
				{
					if (y < x)
					{
						c.x = y;
						c.y = z;
						c.z = x;
					}
				}
				else if (z < x)
				{
					c.x = z;
					c.y = x;
					c.z = y;
				}
				cells[i] = c;
			}
			ModuleHandle.InsertionSort<int3, TessCellCompare>(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<int3>(cells), 0, this.m_CellCount - 1, default(TessCellCompare));
			this.m_Flags = new NativeArray<int>(nc, this.m_Allocator, NativeArrayOptions.ClearMemory);
			this.m_Neighbors = new NativeArray<int>(nc * 3, this.m_Allocator, NativeArrayOptions.ClearMemory);
			this.m_Constraints = new NativeArray<int>(nc * 3, this.m_Allocator, NativeArrayOptions.ClearMemory);
			NativeArray<int> next = new NativeArray<int>(nc * 3, this.m_Allocator, NativeArrayOptions.ClearMemory);
			NativeArray<int> active = new NativeArray<int>(nc * 3, this.m_Allocator, NativeArrayOptions.ClearMemory);
			int side = 1;
			int nextCount = 0;
			int activeCount = 0;
			for (int j = 0; j < nc; j++)
			{
				int3 c2 = cells[j];
				for (int k = 0; k < 3; k++)
				{
					int x2 = k;
					int y2 = (k + 1) % 3;
					x2 = ((x2 == 0) ? c2.x : ((k == 1) ? c2.y : c2.z));
					y2 = ((y2 == 0) ? c2.x : ((y2 == 1) ? c2.y : c2.z));
					int o = this.OppositeOf(y2, x2);
					int num = (this.m_Neighbors[3 * j + k] = this.FindNeighbor(cells, count, y2, x2, o));
					int b = (this.m_Constraints[3 * j + k] = ((-1 != this.FindConstraint(x2, y2)) ? 1 : 0));
					if (num < 0)
					{
						if (b != 0)
						{
							next[nextCount++] = j;
						}
						else
						{
							active[activeCount++] = j;
							this.m_Flags[j] = 1;
						}
					}
				}
			}
			while (activeCount > 0 || nextCount > 0)
			{
				while (activeCount > 0)
				{
					int t = active[activeCount - 1];
					activeCount--;
					if (this.m_Flags[t] != -side)
					{
						this.m_Flags[t] = side;
						int3 @int = cells[t];
						for (int l = 0; l < 3; l++)
						{
							int f = this.m_Neighbors[3 * t + l];
							if (f >= 0 && this.m_Flags[f] == 0)
							{
								if (this.m_Constraints[3 * t + l] != 0)
								{
									next[nextCount++] = f;
								}
								else
								{
									active[activeCount++] = f;
									this.m_Flags[f] = side;
								}
							}
						}
					}
				}
				for (int e = 0; e < nextCount; e++)
				{
					active[e] = next[e];
				}
				activeCount = nextCount;
				nextCount = 0;
				side = -side;
			}
			active.Dispose();
			next.Dispose();
			return cells;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001C188 File Offset: 0x0001A388
		internal NativeArray<int3> RemoveExterior(ref int cellCount)
		{
			int constrainedCount = 0;
			NativeArray<int3> constrained = this.Constrain(ref constrainedCount);
			NativeArray<int3> cellsOut = new NativeArray<int3>(constrainedCount, this.m_Allocator, NativeArrayOptions.ClearMemory);
			cellCount = 0;
			for (int i = 0; i < constrainedCount; i++)
			{
				if (this.m_Flags[i] == -1)
				{
					int num = cellCount;
					cellCount = num + 1;
					cellsOut[num] = constrained[i];
				}
			}
			constrained.Dispose();
			return cellsOut;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001C1F0 File Offset: 0x0001A3F0
		internal NativeArray<int3> RemoveInterior(int cellCount)
		{
			int constrainedCount = 0;
			NativeArray<int3> constrained = this.Constrain(ref constrainedCount);
			NativeArray<int3> cellsOut = new NativeArray<int3>(constrainedCount, this.m_Allocator, NativeArrayOptions.ClearMemory);
			cellCount = 0;
			for (int i = 0; i < constrainedCount; i++)
			{
				if (this.m_Flags[i] == 1)
				{
					cellsOut[cellCount++] = constrained[i];
				}
			}
			constrained.Dispose();
			return cellsOut;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001C254 File Offset: 0x0001A454
		internal bool Triangulate(NativeArray<float2> points, int pointCount, NativeArray<int2> edges, int edgeCount)
		{
			this.m_NumEdges = edgeCount;
			this.m_NumHulls = edgeCount * 2;
			this.m_NumPoints = pointCount;
			this.m_CellCount = 0;
			this.m_Cells = new NativeArray<int3>(ModuleHandle.kMaxTriangleCount, this.m_Allocator, NativeArrayOptions.ClearMemory);
			this.m_ILArray = new NativeArray<int>(this.m_NumHulls * (this.m_NumHulls + 1), this.m_Allocator, NativeArrayOptions.ClearMemory);
			this.m_IUArray = new NativeArray<int>(this.m_NumHulls * (this.m_NumHulls + 1), this.m_Allocator, NativeArrayOptions.ClearMemory);
			NativeArray<UHull> hulls = new NativeArray<UHull>(this.m_NumPoints * 8, this.m_Allocator, NativeArrayOptions.ClearMemory);
			int hullCount = 0;
			NativeArray<UEvent> events = new NativeArray<UEvent>(this.m_NumPoints + this.m_NumEdges * 2, this.m_Allocator, NativeArrayOptions.ClearMemory);
			int eventCount = 0;
			for (int i = 0; i < this.m_NumPoints; i++)
			{
				UEvent evt = default(UEvent);
				evt.a = points[i];
				evt.b = default(float2);
				evt.idx = i;
				evt.type = 0;
				events[eventCount++] = evt;
			}
			for (int j = 0; j < this.m_NumEdges; j++)
			{
				int2 e = edges[j];
				float2 a = points[e.x];
				float2 b = points[e.y];
				if (a.x < b.x)
				{
					UEvent _s = default(UEvent);
					_s.a = a;
					_s.b = b;
					_s.idx = j;
					_s.type = 2;
					UEvent _e = default(UEvent);
					_e.a = b;
					_e.b = a;
					_e.idx = j;
					_e.type = 1;
					events[eventCount++] = _s;
					events[eventCount++] = _e;
				}
				else if (a.x > b.x)
				{
					UEvent _s2 = default(UEvent);
					_s2.a = b;
					_s2.b = a;
					_s2.idx = j;
					_s2.type = 2;
					UEvent _e2 = default(UEvent);
					_e2.a = a;
					_e2.b = b;
					_e2.idx = j;
					_e2.type = 1;
					events[eventCount++] = _s2;
					events[eventCount++] = _e2;
				}
			}
			ModuleHandle.InsertionSort<UEvent, TessEventCompare>(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<UEvent>(events), 0, eventCount - 1, default(TessEventCompare));
			bool hullOp = true;
			float minX = events[0].a.x - (1f + math.abs(events[0].a.x)) * math.pow(2f, -16f);
			UHull hull;
			hull.a.x = minX;
			hull.a.y = 1f;
			hull.b.x = minX;
			hull.b.y = 0f;
			hull.idx = -1;
			hull.ilarray = new ArraySlice<int>(this.m_ILArray, this.m_NumHulls * this.m_NumHulls, this.m_NumHulls);
			hull.iuarray = new ArraySlice<int>(this.m_IUArray, this.m_NumHulls * this.m_NumHulls, this.m_NumHulls);
			hull.ilcount = 0;
			hull.iucount = 0;
			hulls[hullCount++] = hull;
			int k = 0;
			int numEvents = eventCount;
			while (k < numEvents)
			{
				int type = events[k].type;
				if (type != 0)
				{
					if (type != 2)
					{
						hullOp = this.MergeHulls(hulls, ref hullCount, points, events[k]);
					}
					else
					{
						hullOp = this.SplitHulls(hulls, ref hullCount, points, events[k]);
					}
				}
				else
				{
					hullOp = this.AddPoint(hulls, hullCount, points, events[k].a, events[k].idx);
				}
				if (!hullOp)
				{
					break;
				}
				k++;
			}
			events.Dispose();
			hulls.Dispose();
			return hullOp;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001C660 File Offset: 0x0001A860
		internal static bool Tessellate(Allocator allocator, NativeArray<float2> pgPoints, int pgPointCount, NativeArray<int2> pgEdges, int pgEdgeCount, ref NativeArray<float2> outputVertices, ref int vertexCount, ref NativeArray<int> outputIndices, ref int indexCount)
		{
			Tessellator tess = default(Tessellator);
			tess.SetAllocator(allocator);
			int maxCount = 0;
			int triCount = 0;
			bool valid = tess.Triangulate(pgPoints, pgPointCount, pgEdges, pgEdgeCount);
			valid = valid && tess.ApplyDelaunay(pgPoints, pgEdges);
			if (valid)
			{
				NativeArray<int3> cells = tess.RemoveExterior(ref triCount);
				for (int i = 0; i < triCount; i++)
				{
					ushort a = (ushort)cells[i].x;
					ushort b = (ushort)cells[i].y;
					ushort c = (ushort)cells[i].z;
					if (a != b && b != c && a != c)
					{
						int num = indexCount;
						indexCount = num + 1;
						outputIndices[num] = (int)a;
						num = indexCount;
						indexCount = num + 1;
						outputIndices[num] = (int)c;
						num = indexCount;
						indexCount = num + 1;
						outputIndices[num] = (int)b;
					}
					maxCount = math.max(math.max(math.max(cells[i].x, cells[i].y), cells[i].z), maxCount);
				}
				maxCount = ((maxCount != 0) ? (maxCount + 1) : 0);
				for (int j = 0; j < maxCount; j++)
				{
					int num = vertexCount;
					vertexCount = num + 1;
					outputVertices[num] = pgPoints[j];
				}
				cells.Dispose();
			}
			tess.Cleanup();
			return valid;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001C7D4 File Offset: 0x0001A9D4
		internal void Cleanup()
		{
			if (this.m_Edges.IsCreated)
			{
				this.m_Edges.Dispose();
			}
			if (this.m_Stars.IsCreated)
			{
				this.m_Stars.Dispose();
			}
			if (this.m_SPArray.IsCreated)
			{
				this.m_SPArray.Dispose();
			}
			if (this.m_Cells.IsCreated)
			{
				this.m_Cells.Dispose();
			}
			if (this.m_ILArray.IsCreated)
			{
				this.m_ILArray.Dispose();
			}
			if (this.m_IUArray.IsCreated)
			{
				this.m_IUArray.Dispose();
			}
			if (this.m_Flags.IsCreated)
			{
				this.m_Flags.Dispose();
			}
			if (this.m_Neighbors.IsCreated)
			{
				this.m_Neighbors.Dispose();
			}
			if (this.m_Constraints.IsCreated)
			{
				this.m_Constraints.Dispose();
			}
		}

		// Token: 0x040002F7 RID: 759
		private NativeArray<int2> m_Edges;

		// Token: 0x040002F8 RID: 760
		private NativeArray<UStar> m_Stars;

		// Token: 0x040002F9 RID: 761
		private NativeArray<int3> m_Cells;

		// Token: 0x040002FA RID: 762
		private int m_CellCount;

		// Token: 0x040002FB RID: 763
		private NativeArray<int> m_ILArray;

		// Token: 0x040002FC RID: 764
		private NativeArray<int> m_IUArray;

		// Token: 0x040002FD RID: 765
		private NativeArray<int> m_SPArray;

		// Token: 0x040002FE RID: 766
		private int m_NumEdges;

		// Token: 0x040002FF RID: 767
		private int m_NumHulls;

		// Token: 0x04000300 RID: 768
		private int m_NumPoints;

		// Token: 0x04000301 RID: 769
		private int m_StarCount;

		// Token: 0x04000302 RID: 770
		private NativeArray<int> m_Flags;

		// Token: 0x04000303 RID: 771
		private NativeArray<int> m_Neighbors;

		// Token: 0x04000304 RID: 772
		private NativeArray<int> m_Constraints;

		// Token: 0x04000305 RID: 773
		private Allocator m_Allocator;

		// Token: 0x0200009E RID: 158
		private struct TestHullPointL : ICondition2<UHull, float2>
		{
			// Token: 0x060003D7 RID: 983 RVA: 0x0001C8B9 File Offset: 0x0001AAB9
			public bool Test(UHull h, float2 p, ref float t)
			{
				t = ModuleHandle.OrientFast(h.a, h.b, p);
				return t < 0f;
			}
		}

		// Token: 0x0200009F RID: 159
		private struct TestHullPointU : ICondition2<UHull, float2>
		{
			// Token: 0x060003D8 RID: 984 RVA: 0x0001C8D8 File Offset: 0x0001AAD8
			public bool Test(UHull h, float2 p, ref float t)
			{
				t = ModuleHandle.OrientFast(h.a, h.b, p);
				return t > 0f;
			}
		}

		// Token: 0x020000A0 RID: 160
		private struct TestHullEventLe : ICondition2<UHull, UEvent>
		{
			// Token: 0x060003D9 RID: 985 RVA: 0x0001C8F7 File Offset: 0x0001AAF7
			public bool Test(UHull h, UEvent p, ref float t)
			{
				t = Tessellator.FindSplit(h, p);
				return t <= 0f;
			}
		}

		// Token: 0x020000A1 RID: 161
		private struct TestHullEventE : ICondition2<UHull, UEvent>
		{
			// Token: 0x060003DA RID: 986 RVA: 0x0001C90E File Offset: 0x0001AB0E
			public bool Test(UHull h, UEvent p, ref float t)
			{
				t = Tessellator.FindSplit(h, p);
				return t == 0f;
			}
		}

		// Token: 0x020000A2 RID: 162
		private struct TestEdgePointE : ICondition2<int2, int2>
		{
			// Token: 0x060003DB RID: 987 RVA: 0x0001C924 File Offset: 0x0001AB24
			public bool Test(int2 h, int2 p, ref float t)
			{
				t = (float)default(TessEdgeCompare).Compare(h, p);
				return t == 0f;
			}
		}

		// Token: 0x020000A3 RID: 163
		private struct TestCellE : ICondition2<int3, int3>
		{
			// Token: 0x060003DC RID: 988 RVA: 0x0001C950 File Offset: 0x0001AB50
			public bool Test(int3 h, int3 p, ref float t)
			{
				t = (float)default(TessCellCompare).Compare(h, p);
				return t == 0f;
			}
		}
	}
}
