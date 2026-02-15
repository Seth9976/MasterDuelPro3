using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000025 RID: 37
	internal class ClipperOffset
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00009760 File Offset: 0x00007960
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00009768 File Offset: 0x00007968
		public double ArcTolerance { get; set; }

		// Token: 0x060000D2 RID: 210 RVA: 0x00009771 File Offset: 0x00007971
		public ClipperOffset(double arcTolerance = 0.25)
		{
			this.ArcTolerance = arcTolerance;
			this.m_lowest.X = -1L;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000097A3 File Offset: 0x000079A3
		public void Clear()
		{
			this.m_polyNodes.Childs.Clear();
			this.m_lowest.X = -1L;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006CDD File Offset: 0x00004EDD
		internal static long Round(double value)
		{
			if (value >= 0.0)
			{
				return (long)(value + 0.5);
			}
			return (long)(value - 0.5);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000097C4 File Offset: 0x000079C4
		public void AddPath(List<IntPoint> path, JoinTypes joinType, EndTypes endType)
		{
			int highI = path.Count - 1;
			if (highI < 0)
			{
				return;
			}
			PolyNode newNode = new PolyNode();
			newNode.m_jointype = joinType;
			newNode.m_endtype = endType;
			if (endType != EndTypes.etClosedLine)
			{
				if (endType != EndTypes.etClosedPolygon)
				{
					goto IL_0048;
				}
			}
			while (highI > 0 && path[0] == path[highI])
			{
				highI--;
			}
			IL_0048:
			newNode.m_polygon.Capacity = highI + 1;
			newNode.m_polygon.Add(path[0]);
			int i = 0;
			int j = 0;
			for (int k = 1; k <= highI; k++)
			{
				if (newNode.m_polygon[i] != path[k])
				{
					i++;
					newNode.m_polygon.Add(path[k]);
					if (path[k].Y > newNode.m_polygon[j].Y || (path[k].Y == newNode.m_polygon[j].Y && path[k].X < newNode.m_polygon[j].X))
					{
						j = i;
					}
				}
			}
			if (endType == EndTypes.etClosedPolygon && i < 2)
			{
				return;
			}
			this.m_polyNodes.AddChild(newNode);
			if (endType != EndTypes.etClosedPolygon)
			{
				return;
			}
			if (this.m_lowest.X < 0L)
			{
				this.m_lowest = new IntPoint((long)(this.m_polyNodes.ChildCount - 1), (long)j);
				return;
			}
			IntPoint ip = this.m_polyNodes.Childs[(int)this.m_lowest.X].m_polygon[(int)this.m_lowest.Y];
			if (newNode.m_polygon[j].Y > ip.Y || (newNode.m_polygon[j].Y == ip.Y && newNode.m_polygon[j].X < ip.X))
			{
				this.m_lowest = new IntPoint((long)(this.m_polyNodes.ChildCount - 1), (long)j);
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000099C8 File Offset: 0x00007BC8
		public void AddPaths(List<List<IntPoint>> paths, JoinTypes joinType, EndTypes endType)
		{
			for (int i = 0; i < paths.Count; i++)
			{
				List<IntPoint> p = paths[i];
				this.AddPath(p, joinType, endType);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000099F8 File Offset: 0x00007BF8
		private void FixOrientations()
		{
			if (this.m_lowest.X >= 0L && !Clipper.Orientation(this.m_polyNodes.Childs[(int)this.m_lowest.X].m_polygon))
			{
				for (int i = 0; i < this.m_polyNodes.ChildCount; i++)
				{
					PolyNode node = this.m_polyNodes.Childs[i];
					if (node.m_endtype == EndTypes.etClosedPolygon || (node.m_endtype == EndTypes.etClosedLine && Clipper.Orientation(node.m_polygon)))
					{
						node.m_polygon.Reverse();
					}
				}
				return;
			}
			for (int j = 0; j < this.m_polyNodes.ChildCount; j++)
			{
				PolyNode node2 = this.m_polyNodes.Childs[j];
				if (node2.m_endtype == EndTypes.etClosedLine && !Clipper.Orientation(node2.m_polygon))
				{
					node2.m_polygon.Reverse();
				}
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00009AD8 File Offset: 0x00007CD8
		internal static DoublePoint GetUnitNormal(IntPoint pt1, IntPoint pt2)
		{
			double dx = (double)(pt2.X - pt1.X);
			double dy = (double)(pt2.Y - pt1.Y);
			if (dx == 0.0 && dy == 0.0)
			{
				return default(DoublePoint);
			}
			double f = 1.0 / Math.Sqrt(dx * dx + dy * dy);
			dx *= f;
			dy *= f;
			return new DoublePoint(dy, -dx);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00009B4C File Offset: 0x00007D4C
		private void DoOffset(double delta)
		{
			this.m_destPolys = new List<List<IntPoint>>();
			this.m_delta = delta;
			if (ClipperBase.near_zero(delta))
			{
				this.m_destPolys.Capacity = this.m_polyNodes.ChildCount;
				for (int i = 0; i < this.m_polyNodes.ChildCount; i++)
				{
					PolyNode node = this.m_polyNodes.Childs[i];
					if (node.m_endtype == EndTypes.etClosedPolygon)
					{
						this.m_destPolys.Add(node.m_polygon);
					}
				}
				return;
			}
			double y;
			if (this.ArcTolerance <= 0.0)
			{
				y = 0.25;
			}
			else if (this.ArcTolerance > Math.Abs(delta) * 0.25)
			{
				y = Math.Abs(delta) * 0.25;
			}
			else
			{
				y = this.ArcTolerance;
			}
			double steps = 3.141592653589793 / Math.Acos(1.0 - y / Math.Abs(delta));
			this.m_sin = Math.Sin(6.283185307179586 / steps);
			this.m_cos = Math.Cos(6.283185307179586 / steps);
			this.m_StepsPerRad = steps / 6.283185307179586;
			if (delta < 0.0)
			{
				this.m_sin = -this.m_sin;
			}
			this.m_destPolys.Capacity = this.m_polyNodes.ChildCount * 2;
			for (int j = 0; j < this.m_polyNodes.ChildCount; j++)
			{
				PolyNode node2 = this.m_polyNodes.Childs[j];
				this.m_srcPoly = node2.m_polygon;
				int len = this.m_srcPoly.Count;
				if (len != 0 && (delta > 0.0 || (len >= 3 && node2.m_endtype == EndTypes.etClosedPolygon)))
				{
					this.m_destPoly = new List<IntPoint>();
					if (len == 1)
					{
						if (node2.m_jointype == JoinTypes.jtRound)
						{
							double X = 1.0;
							double Y = 0.0;
							int k = 1;
							while ((double)k <= steps)
							{
								this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[0].X + X * delta), ClipperOffset.Round((double)this.m_srcPoly[0].Y + Y * delta)));
								double num = X;
								X = X * this.m_cos - this.m_sin * Y;
								Y = num * this.m_sin + Y * this.m_cos;
								k++;
							}
						}
						else
						{
							double X2 = -1.0;
							double Y2 = -1.0;
							for (int l = 0; l < 4; l++)
							{
								this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[0].X + X2 * delta), ClipperOffset.Round((double)this.m_srcPoly[0].Y + Y2 * delta)));
								if (X2 < 0.0)
								{
									X2 = 1.0;
								}
								else if (Y2 < 0.0)
								{
									Y2 = 1.0;
								}
								else
								{
									X2 = -1.0;
								}
							}
						}
						this.m_destPolys.Add(this.m_destPoly);
					}
					else
					{
						this.m_normals.Clear();
						this.m_normals.Capacity = len;
						for (int m = 0; m < len - 1; m++)
						{
							this.m_normals.Add(ClipperOffset.GetUnitNormal(this.m_srcPoly[m], this.m_srcPoly[m + 1]));
						}
						if (node2.m_endtype == EndTypes.etClosedLine || node2.m_endtype == EndTypes.etClosedPolygon)
						{
							this.m_normals.Add(ClipperOffset.GetUnitNormal(this.m_srcPoly[len - 1], this.m_srcPoly[0]));
						}
						else
						{
							this.m_normals.Add(new DoublePoint(this.m_normals[len - 2]));
						}
						if (node2.m_endtype == EndTypes.etClosedPolygon)
						{
							int n = len - 1;
							for (int j2 = 0; j2 < len; j2++)
							{
								this.OffsetPoint(j2, ref n, node2.m_jointype);
							}
							this.m_destPolys.Add(this.m_destPoly);
						}
						else if (node2.m_endtype == EndTypes.etClosedLine)
						{
							int k2 = len - 1;
							for (int j3 = 0; j3 < len; j3++)
							{
								this.OffsetPoint(j3, ref k2, node2.m_jointype);
							}
							this.m_destPolys.Add(this.m_destPoly);
							this.m_destPoly = new List<IntPoint>();
							DoublePoint n2 = this.m_normals[len - 1];
							for (int j4 = len - 1; j4 > 0; j4--)
							{
								this.m_normals[j4] = new DoublePoint(-this.m_normals[j4 - 1].X, -this.m_normals[j4 - 1].Y);
							}
							this.m_normals[0] = new DoublePoint(-n2.X, -n2.Y);
							k2 = 0;
							for (int j5 = len - 1; j5 >= 0; j5--)
							{
								this.OffsetPoint(j5, ref k2, node2.m_jointype);
							}
							this.m_destPolys.Add(this.m_destPoly);
						}
						else
						{
							int k3 = 0;
							for (int j6 = 1; j6 < len - 1; j6++)
							{
								this.OffsetPoint(j6, ref k3, node2.m_jointype);
							}
							int j7 = len - 1;
							k3 = len - 2;
							this.m_sinA = 0.0;
							this.m_normals[j7] = new DoublePoint(-this.m_normals[j7].X, -this.m_normals[j7].Y);
							this.DoRound(j7, k3);
							for (int j8 = len - 1; j8 > 0; j8--)
							{
								this.m_normals[j8] = new DoublePoint(-this.m_normals[j8 - 1].X, -this.m_normals[j8 - 1].Y);
							}
							this.m_normals[0] = new DoublePoint(-this.m_normals[1].X, -this.m_normals[1].Y);
							k3 = len - 1;
							for (int j9 = k3 - 1; j9 > 0; j9--)
							{
								this.OffsetPoint(j9, ref k3, node2.m_jointype);
							}
							k3 = 1;
							this.m_sinA = 0.0;
							this.DoRound(0, 1);
							this.m_destPolys.Add(this.m_destPoly);
						}
					}
				}
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000A210 File Offset: 0x00008410
		public void Execute(ref List<List<IntPoint>> solution, double delta, int inputSize)
		{
			solution.Clear();
			this.FixOrientations();
			this.DoOffset(delta);
			Clipper clpr = new Clipper(0);
			clpr.AddPaths(this.m_destPolys, PolyTypes.ptSubject, true);
			clpr.LastIndex = inputSize - 1;
			if (delta > 0.0)
			{
				clpr.Execute(ClipTypes.ctUnion, solution, PolyFillTypes.pftPositive, PolyFillTypes.pftPositive);
				return;
			}
			IntRect r = ClipperBase.GetBounds(this.m_destPolys);
			clpr.AddPath(new List<IntPoint>(4)
			{
				new IntPoint(r.left - 10L, r.bottom + 10L),
				new IntPoint(r.right + 10L, r.bottom + 10L),
				new IntPoint(r.right + 10L, r.top - 10L),
				new IntPoint(r.left - 10L, r.top - 10L)
			}, PolyTypes.ptSubject, true);
			clpr.ReverseSolution = true;
			clpr.Execute(ClipTypes.ctUnion, solution, PolyFillTypes.pftNegative, PolyFillTypes.pftNegative);
			if (solution.Count > 0)
			{
				solution.RemoveAt(0);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000A328 File Offset: 0x00008528
		public void Execute(ref PolyTree solution, double delta)
		{
			solution.Clear();
			this.FixOrientations();
			this.DoOffset(delta);
			Clipper clpr = new Clipper(0);
			clpr.AddPaths(this.m_destPolys, PolyTypes.ptSubject, true);
			if (delta > 0.0)
			{
				clpr.Execute(ClipTypes.ctUnion, solution, PolyFillTypes.pftPositive, PolyFillTypes.pftPositive);
				return;
			}
			IntRect r = ClipperBase.GetBounds(this.m_destPolys);
			clpr.AddPath(new List<IntPoint>(4)
			{
				new IntPoint(r.left - 10L, r.bottom + 10L),
				new IntPoint(r.right + 10L, r.bottom + 10L),
				new IntPoint(r.right + 10L, r.top - 10L),
				new IntPoint(r.left - 10L, r.top - 10L)
			}, PolyTypes.ptSubject, true);
			clpr.ReverseSolution = true;
			clpr.Execute(ClipTypes.ctUnion, solution, PolyFillTypes.pftNegative, PolyFillTypes.pftNegative);
			if (solution.ChildCount == 1 && solution.Childs[0].ChildCount > 0)
			{
				PolyNode outerNode = solution.Childs[0];
				solution.Childs.Capacity = outerNode.ChildCount;
				solution.Childs[0] = outerNode.Childs[0];
				solution.Childs[0].m_Parent = solution;
				for (int i = 1; i < outerNode.ChildCount; i++)
				{
					solution.AddChild(outerNode.Childs[i]);
				}
				return;
			}
			solution.Clear();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000A4C4 File Offset: 0x000086C4
		private void OffsetPoint(int j, ref int k, JoinTypes jointype)
		{
			this.m_sinA = this.m_normals[k].X * this.m_normals[j].Y - this.m_normals[j].X * this.m_normals[k].Y;
			if (Math.Abs(this.m_sinA * this.m_delta) < 1.0)
			{
				if (this.m_normals[k].X * this.m_normals[j].X + this.m_normals[j].Y * this.m_normals[k].Y > 0.0)
				{
					IntPoint item = new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_normals[k].X * this.m_delta), ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_normals[k].Y * this.m_delta));
					item.NX = this.m_normals[k].X;
					item.NY = this.m_normals[k].Y;
					item.N = (long)j;
					item.D = 1L;
					this.m_destPoly.Add(item);
					return;
				}
			}
			else if (this.m_sinA > 1.0)
			{
				this.m_sinA = 1.0;
			}
			else if (this.m_sinA < -1.0)
			{
				this.m_sinA = -1.0;
			}
			if (this.m_sinA * this.m_delta < 0.0)
			{
				IntPoint pt = new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_normals[k].X * this.m_delta), ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_normals[k].Y * this.m_delta));
				pt.NX = this.m_normals[k].X;
				pt.NY = this.m_normals[k].Y;
				this.m_destPoly.Add(pt);
				pt = this.m_srcPoly[j];
				pt.NX = this.m_normals[k].X;
				pt.NY = this.m_normals[k].Y;
				pt.N = (long)j;
				pt.D = 1L;
				this.m_destPoly.Add(pt);
				pt = new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_normals[j].X * this.m_delta), ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_normals[j].Y * this.m_delta));
				pt.NX = this.m_normals[j].X;
				pt.NY = this.m_normals[j].Y;
				pt.N = (long)j;
				pt.D = 1L;
				this.m_destPoly.Add(pt);
			}
			else if (jointype == JoinTypes.jtRound)
			{
				this.DoRound(j, k);
			}
			k = j;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000A878 File Offset: 0x00008A78
		internal void DoSquare(int j, int k)
		{
			double dx = Math.Tan(Math.Atan2(this.m_sinA, this.m_normals[k].X * this.m_normals[j].X + this.m_normals[k].Y * this.m_normals[j].Y) / 4.0);
			IntPoint pt = new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_delta * (this.m_normals[k].X - this.m_normals[k].Y * dx)), ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_delta * (this.m_normals[k].Y + this.m_normals[k].X * dx)));
			pt.NX = this.m_normals[k].X - this.m_normals[k].Y * dx;
			pt.NY = this.m_normals[k].Y + this.m_normals[k].X * dx;
			this.m_destPoly.Add(pt);
			pt = new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_delta * (this.m_normals[j].X + this.m_normals[j].Y * dx)), ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_delta * (this.m_normals[j].Y - this.m_normals[j].X * dx)));
			pt.NX = this.m_normals[k].X + this.m_normals[k].Y * dx;
			pt.NY = this.m_normals[k].Y - this.m_normals[k].X * dx;
			this.m_destPoly.Add(pt);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		internal void DoMiter(int j, int k, double r)
		{
			double q = this.m_delta / r;
			IntPoint pt = new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + (this.m_normals[k].X + this.m_normals[j].X) * q), ClipperOffset.Round((double)this.m_srcPoly[j].Y + (this.m_normals[k].Y + this.m_normals[j].Y) * q));
			pt.NX = (this.m_normals[k].X + this.m_normals[j].X) * q;
			pt.NY = (this.m_normals[k].Y + this.m_normals[j].Y) * q;
			this.m_destPoly.Add(pt);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000ABCC File Offset: 0x00008DCC
		internal void DoRound(int j, int k)
		{
			double a = Math.Atan2(this.m_sinA, this.m_normals[k].X * this.m_normals[j].X + this.m_normals[k].Y * this.m_normals[j].Y);
			int steps = Math.Max((int)ClipperOffset.Round(this.m_StepsPerRad * Math.Abs(a)), 1);
			double X = this.m_normals[k].X;
			double Y = this.m_normals[k].Y;
			for (int i = 0; i < steps; i++)
			{
				IntPoint pt = new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + X * this.m_delta), ClipperOffset.Round((double)this.m_srcPoly[j].Y + Y * this.m_delta));
				pt.NX = X;
				pt.NY = Y;
				pt.N = (long)j;
				pt.D = 1L;
				this.m_destPoly.Add(pt);
				double num = X;
				X = X * this.m_cos - this.m_sin * Y;
				Y = num * this.m_sin + Y * this.m_cos;
			}
			IntPoint pt2 = new IntPoint(ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_normals[j].X * this.m_delta), ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_normals[j].Y * this.m_delta));
			pt2.NX = this.m_normals[j].X;
			pt2.NY = this.m_normals[j].Y;
			pt2.N = (long)j;
			pt2.D = 1L;
			this.m_destPoly.Add(pt2);
		}

		// Token: 0x04000097 RID: 151
		private List<List<IntPoint>> m_destPolys;

		// Token: 0x04000098 RID: 152
		private List<IntPoint> m_srcPoly;

		// Token: 0x04000099 RID: 153
		private List<IntPoint> m_destPoly;

		// Token: 0x0400009A RID: 154
		private List<DoublePoint> m_normals = new List<DoublePoint>();

		// Token: 0x0400009B RID: 155
		private double m_delta;

		// Token: 0x0400009C RID: 156
		private double m_sinA;

		// Token: 0x0400009D RID: 157
		private double m_sin;

		// Token: 0x0400009E RID: 158
		private double m_cos;

		// Token: 0x0400009F RID: 159
		private double m_StepsPerRad;

		// Token: 0x040000A0 RID: 160
		private IntPoint m_lowest;

		// Token: 0x040000A1 RID: 161
		private PolyNode m_polyNodes = new PolyNode();

		// Token: 0x040000A3 RID: 163
		private const double two_pi = 6.283185307179586;

		// Token: 0x040000A4 RID: 164
		private const double def_arc_tolerance = 0.25;
	}
}
