using System;
using System.Collections.Generic;

namespace Cinemachine
{
	// Token: 0x020000C5 RID: 197
	internal static class ClipperLib
	{
		// Token: 0x020000C6 RID: 198
		public struct DoublePoint
		{
			// Token: 0x06000448 RID: 1096 RVA: 0x00018CA3 File Offset: 0x00016EA3
			public DoublePoint(double x = 0.0, double y = 0.0)
			{
				this.X = x;
				this.Y = y;
			}

			// Token: 0x06000449 RID: 1097 RVA: 0x00018CB3 File Offset: 0x00016EB3
			public DoublePoint(ClipperLib.DoublePoint dp)
			{
				this.X = dp.X;
				this.Y = dp.Y;
			}

			// Token: 0x0600044A RID: 1098 RVA: 0x00018CCD File Offset: 0x00016ECD
			public DoublePoint(ClipperLib.IntPoint ip)
			{
				this.X = (double)ip.X;
				this.Y = (double)ip.Y;
			}

			// Token: 0x04000408 RID: 1032
			public double X;

			// Token: 0x04000409 RID: 1033
			public double Y;
		}

		// Token: 0x020000C7 RID: 199
		public class PolyTree : ClipperLib.PolyNode
		{
			// Token: 0x0600044B RID: 1099 RVA: 0x00018CEC File Offset: 0x00016EEC
			public void Clear()
			{
				for (int i = 0; i < this.m_AllPolys.Count; i++)
				{
					this.m_AllPolys[i] = null;
				}
				this.m_AllPolys.Clear();
				this.m_Childs.Clear();
			}

			// Token: 0x0600044C RID: 1100 RVA: 0x00018D32 File Offset: 0x00016F32
			public ClipperLib.PolyNode GetFirst()
			{
				if (this.m_Childs.Count > 0)
				{
					return this.m_Childs[0];
				}
				return null;
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x0600044D RID: 1101 RVA: 0x00018D50 File Offset: 0x00016F50
			public int Total
			{
				get
				{
					int result = this.m_AllPolys.Count;
					if (result > 0 && this.m_Childs[0] != this.m_AllPolys[0])
					{
						result--;
					}
					return result;
				}
			}

			// Token: 0x0400040A RID: 1034
			internal List<ClipperLib.PolyNode> m_AllPolys = new List<ClipperLib.PolyNode>();
		}

		// Token: 0x020000C8 RID: 200
		public class PolyNode
		{
			// Token: 0x0600044F RID: 1103 RVA: 0x00018DA0 File Offset: 0x00016FA0
			private bool IsHoleNode()
			{
				bool result = true;
				for (ClipperLib.PolyNode node = this.m_Parent; node != null; node = node.m_Parent)
				{
					result = !result;
				}
				return result;
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x06000450 RID: 1104 RVA: 0x00018DC8 File Offset: 0x00016FC8
			public int ChildCount
			{
				get
				{
					return this.m_Childs.Count;
				}
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x06000451 RID: 1105 RVA: 0x00018DD5 File Offset: 0x00016FD5
			public List<ClipperLib.IntPoint> Contour
			{
				get
				{
					return this.m_polygon;
				}
			}

			// Token: 0x06000452 RID: 1106 RVA: 0x00018DE0 File Offset: 0x00016FE0
			internal void AddChild(ClipperLib.PolyNode Child)
			{
				int cnt = this.m_Childs.Count;
				this.m_Childs.Add(Child);
				Child.m_Parent = this;
				Child.m_Index = cnt;
			}

			// Token: 0x06000453 RID: 1107 RVA: 0x00018E13 File Offset: 0x00017013
			public ClipperLib.PolyNode GetNext()
			{
				if (this.m_Childs.Count > 0)
				{
					return this.m_Childs[0];
				}
				return this.GetNextSiblingUp();
			}

			// Token: 0x06000454 RID: 1108 RVA: 0x00018E38 File Offset: 0x00017038
			internal ClipperLib.PolyNode GetNextSiblingUp()
			{
				if (this.m_Parent == null)
				{
					return null;
				}
				if (this.m_Index == this.m_Parent.m_Childs.Count - 1)
				{
					return this.m_Parent.GetNextSiblingUp();
				}
				return this.m_Parent.m_Childs[this.m_Index + 1];
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x06000455 RID: 1109 RVA: 0x00018E8D File Offset: 0x0001708D
			public List<ClipperLib.PolyNode> Childs
			{
				get
				{
					return this.m_Childs;
				}
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x06000456 RID: 1110 RVA: 0x00018E95 File Offset: 0x00017095
			public ClipperLib.PolyNode Parent
			{
				get
				{
					return this.m_Parent;
				}
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x06000457 RID: 1111 RVA: 0x00018E9D File Offset: 0x0001709D
			public bool IsHole
			{
				get
				{
					return this.IsHoleNode();
				}
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x06000458 RID: 1112 RVA: 0x00018EA5 File Offset: 0x000170A5
			// (set) Token: 0x06000459 RID: 1113 RVA: 0x00018EAD File Offset: 0x000170AD
			public bool IsOpen { get; set; }

			// Token: 0x0400040B RID: 1035
			internal ClipperLib.PolyNode m_Parent;

			// Token: 0x0400040C RID: 1036
			internal List<ClipperLib.IntPoint> m_polygon = new List<ClipperLib.IntPoint>();

			// Token: 0x0400040D RID: 1037
			internal int m_Index;

			// Token: 0x0400040E RID: 1038
			internal ClipperLib.JoinType m_jointype;

			// Token: 0x0400040F RID: 1039
			internal ClipperLib.EndType m_endtype;

			// Token: 0x04000410 RID: 1040
			internal List<ClipperLib.PolyNode> m_Childs = new List<ClipperLib.PolyNode>();
		}

		// Token: 0x020000C9 RID: 201
		internal struct Int128
		{
			// Token: 0x0600045B RID: 1115 RVA: 0x00018ED4 File Offset: 0x000170D4
			public Int128(long _lo)
			{
				this.lo = (ulong)_lo;
				if (_lo < 0L)
				{
					this.hi = -1L;
					return;
				}
				this.hi = 0L;
			}

			// Token: 0x0600045C RID: 1116 RVA: 0x00018EF3 File Offset: 0x000170F3
			public Int128(long _hi, ulong _lo)
			{
				this.lo = _lo;
				this.hi = _hi;
			}

			// Token: 0x0600045D RID: 1117 RVA: 0x00018F03 File Offset: 0x00017103
			public Int128(ClipperLib.Int128 val)
			{
				this.hi = val.hi;
				this.lo = val.lo;
			}

			// Token: 0x0600045E RID: 1118 RVA: 0x00018F1D File Offset: 0x0001711D
			public bool IsNegative()
			{
				return this.hi < 0L;
			}

			// Token: 0x0600045F RID: 1119 RVA: 0x00018F2C File Offset: 0x0001712C
			public static bool operator ==(ClipperLib.Int128 val1, ClipperLib.Int128 val2)
			{
				return val1 == val2 || (val1 != null && val2 != null && val1.hi == val2.hi && val1.lo == val2.lo);
			}

			// Token: 0x06000460 RID: 1120 RVA: 0x00018F79 File Offset: 0x00017179
			public static bool operator !=(ClipperLib.Int128 val1, ClipperLib.Int128 val2)
			{
				return !(val1 == val2);
			}

			// Token: 0x06000461 RID: 1121 RVA: 0x00018F88 File Offset: 0x00017188
			public override bool Equals(object obj)
			{
				if (obj == null || !(obj is ClipperLib.Int128))
				{
					return false;
				}
				ClipperLib.Int128 i128 = (ClipperLib.Int128)obj;
				return i128.hi == this.hi && i128.lo == this.lo;
			}

			// Token: 0x06000462 RID: 1122 RVA: 0x00018FC7 File Offset: 0x000171C7
			public override int GetHashCode()
			{
				return this.hi.GetHashCode() ^ this.lo.GetHashCode();
			}

			// Token: 0x06000463 RID: 1123 RVA: 0x00018FE0 File Offset: 0x000171E0
			public static bool operator >(ClipperLib.Int128 val1, ClipperLib.Int128 val2)
			{
				if (val1.hi != val2.hi)
				{
					return val1.hi > val2.hi;
				}
				return val1.lo > val2.lo;
			}

			// Token: 0x06000464 RID: 1124 RVA: 0x0001900D File Offset: 0x0001720D
			public static bool operator <(ClipperLib.Int128 val1, ClipperLib.Int128 val2)
			{
				if (val1.hi != val2.hi)
				{
					return val1.hi < val2.hi;
				}
				return val1.lo < val2.lo;
			}

			// Token: 0x06000465 RID: 1125 RVA: 0x0001903A File Offset: 0x0001723A
			public static ClipperLib.Int128 operator +(ClipperLib.Int128 lhs, ClipperLib.Int128 rhs)
			{
				lhs.hi += rhs.hi;
				lhs.lo += rhs.lo;
				if (lhs.lo < rhs.lo)
				{
					lhs.hi += 1L;
				}
				return lhs;
			}

			// Token: 0x06000466 RID: 1126 RVA: 0x0001907A File Offset: 0x0001727A
			public static ClipperLib.Int128 operator -(ClipperLib.Int128 lhs, ClipperLib.Int128 rhs)
			{
				return lhs + -rhs;
			}

			// Token: 0x06000467 RID: 1127 RVA: 0x00019088 File Offset: 0x00017288
			public static ClipperLib.Int128 operator -(ClipperLib.Int128 val)
			{
				if (val.lo == 0UL)
				{
					return new ClipperLib.Int128(-val.hi, 0UL);
				}
				return new ClipperLib.Int128(~val.hi, ~val.lo + 1UL);
			}

			// Token: 0x06000468 RID: 1128 RVA: 0x000190B8 File Offset: 0x000172B8
			public static explicit operator double(ClipperLib.Int128 val)
			{
				if (val.hi >= 0L)
				{
					return val.lo + (double)val.hi * 1.8446744073709552E+19;
				}
				if (val.lo == 0UL)
				{
					return (double)val.hi * 1.8446744073709552E+19;
				}
				return -(~val.lo + (double)(~(double)val.hi) * 1.8446744073709552E+19);
			}

			// Token: 0x06000469 RID: 1129 RVA: 0x00019124 File Offset: 0x00017324
			public static ClipperLib.Int128 Int128Mul(long lhs, long rhs)
			{
				bool flag = lhs < 0L != rhs < 0L;
				if (lhs < 0L)
				{
					lhs = -lhs;
				}
				if (rhs < 0L)
				{
					rhs = -rhs;
				}
				ulong num = (ulong)lhs >> 32;
				ulong int1Lo = (ulong)(lhs & (long)((ulong)(-1)));
				ulong int2Hi = (ulong)rhs >> 32;
				ulong int2Lo = (ulong)(rhs & (long)((ulong)(-1)));
				ulong a = num * int2Hi;
				ulong b = int1Lo * int2Lo;
				ulong c = num * int2Lo + int1Lo * int2Hi;
				long hi = (long)(a + (c >> 32));
				ulong lo = (c << 32) + b;
				if (lo < b)
				{
					hi += 1L;
				}
				ClipperLib.Int128 result = new ClipperLib.Int128(hi, lo);
				if (!flag)
				{
					return result;
				}
				return -result;
			}

			// Token: 0x04000412 RID: 1042
			private long hi;

			// Token: 0x04000413 RID: 1043
			private ulong lo;
		}

		// Token: 0x020000CA RID: 202
		public struct IntPoint
		{
			// Token: 0x0600046A RID: 1130 RVA: 0x000191AD File Offset: 0x000173AD
			public IntPoint(long X, long Y)
			{
				this.X = X;
				this.Y = Y;
			}

			// Token: 0x0600046B RID: 1131 RVA: 0x000191BD File Offset: 0x000173BD
			public IntPoint(double x, double y)
			{
				this.X = (long)x;
				this.Y = (long)y;
			}

			// Token: 0x0600046C RID: 1132 RVA: 0x000191CF File Offset: 0x000173CF
			public IntPoint(ClipperLib.IntPoint pt)
			{
				this.X = pt.X;
				this.Y = pt.Y;
			}

			// Token: 0x0600046D RID: 1133 RVA: 0x000191E9 File Offset: 0x000173E9
			public static bool operator ==(ClipperLib.IntPoint a, ClipperLib.IntPoint b)
			{
				return a.X == b.X && a.Y == b.Y;
			}

			// Token: 0x0600046E RID: 1134 RVA: 0x00019209 File Offset: 0x00017409
			public static bool operator !=(ClipperLib.IntPoint a, ClipperLib.IntPoint b)
			{
				return a.X != b.X || a.Y != b.Y;
			}

			// Token: 0x0600046F RID: 1135 RVA: 0x0001922C File Offset: 0x0001742C
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is ClipperLib.IntPoint)
				{
					ClipperLib.IntPoint a = (ClipperLib.IntPoint)obj;
					return this.X == a.X && this.Y == a.Y;
				}
				return false;
			}

			// Token: 0x06000470 RID: 1136 RVA: 0x0001926D File Offset: 0x0001746D
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x04000414 RID: 1044
			public long X;

			// Token: 0x04000415 RID: 1045
			public long Y;
		}

		// Token: 0x020000CB RID: 203
		public struct IntRect
		{
			// Token: 0x06000471 RID: 1137 RVA: 0x0001927F File Offset: 0x0001747F
			public IntRect(long l, long t, long r, long b)
			{
				this.left = l;
				this.top = t;
				this.right = r;
				this.bottom = b;
			}

			// Token: 0x06000472 RID: 1138 RVA: 0x0001929E File Offset: 0x0001749E
			public IntRect(ClipperLib.IntRect ir)
			{
				this.left = ir.left;
				this.top = ir.top;
				this.right = ir.right;
				this.bottom = ir.bottom;
			}

			// Token: 0x04000416 RID: 1046
			public long left;

			// Token: 0x04000417 RID: 1047
			public long top;

			// Token: 0x04000418 RID: 1048
			public long right;

			// Token: 0x04000419 RID: 1049
			public long bottom;
		}

		// Token: 0x020000CC RID: 204
		public enum ClipType
		{
			// Token: 0x0400041B RID: 1051
			ctIntersection,
			// Token: 0x0400041C RID: 1052
			ctUnion,
			// Token: 0x0400041D RID: 1053
			ctDifference,
			// Token: 0x0400041E RID: 1054
			ctXor
		}

		// Token: 0x020000CD RID: 205
		public enum PolyType
		{
			// Token: 0x04000420 RID: 1056
			ptSubject,
			// Token: 0x04000421 RID: 1057
			ptClip
		}

		// Token: 0x020000CE RID: 206
		public enum PolyFillType
		{
			// Token: 0x04000423 RID: 1059
			pftEvenOdd,
			// Token: 0x04000424 RID: 1060
			pftNonZero,
			// Token: 0x04000425 RID: 1061
			pftPositive,
			// Token: 0x04000426 RID: 1062
			pftNegative
		}

		// Token: 0x020000CF RID: 207
		public enum JoinType
		{
			// Token: 0x04000428 RID: 1064
			jtSquare,
			// Token: 0x04000429 RID: 1065
			jtRound,
			// Token: 0x0400042A RID: 1066
			jtMiter
		}

		// Token: 0x020000D0 RID: 208
		public enum EndType
		{
			// Token: 0x0400042C RID: 1068
			etClosedPolygon,
			// Token: 0x0400042D RID: 1069
			etClosedLine,
			// Token: 0x0400042E RID: 1070
			etOpenButt,
			// Token: 0x0400042F RID: 1071
			etOpenSquare,
			// Token: 0x04000430 RID: 1072
			etOpenRound
		}

		// Token: 0x020000D1 RID: 209
		internal enum EdgeSide
		{
			// Token: 0x04000432 RID: 1074
			esLeft,
			// Token: 0x04000433 RID: 1075
			esRight
		}

		// Token: 0x020000D2 RID: 210
		internal enum Direction
		{
			// Token: 0x04000435 RID: 1077
			dRightToLeft,
			// Token: 0x04000436 RID: 1078
			dLeftToRight
		}

		// Token: 0x020000D3 RID: 211
		internal class TEdge
		{
			// Token: 0x04000437 RID: 1079
			internal ClipperLib.IntPoint Bot;

			// Token: 0x04000438 RID: 1080
			internal ClipperLib.IntPoint Curr;

			// Token: 0x04000439 RID: 1081
			internal ClipperLib.IntPoint Top;

			// Token: 0x0400043A RID: 1082
			internal ClipperLib.IntPoint Delta;

			// Token: 0x0400043B RID: 1083
			internal double Dx;

			// Token: 0x0400043C RID: 1084
			internal ClipperLib.PolyType PolyTyp;

			// Token: 0x0400043D RID: 1085
			internal ClipperLib.EdgeSide Side;

			// Token: 0x0400043E RID: 1086
			internal int WindDelta;

			// Token: 0x0400043F RID: 1087
			internal int WindCnt;

			// Token: 0x04000440 RID: 1088
			internal int WindCnt2;

			// Token: 0x04000441 RID: 1089
			internal int OutIdx;

			// Token: 0x04000442 RID: 1090
			internal ClipperLib.TEdge Next;

			// Token: 0x04000443 RID: 1091
			internal ClipperLib.TEdge Prev;

			// Token: 0x04000444 RID: 1092
			internal ClipperLib.TEdge NextInLML;

			// Token: 0x04000445 RID: 1093
			internal ClipperLib.TEdge NextInAEL;

			// Token: 0x04000446 RID: 1094
			internal ClipperLib.TEdge PrevInAEL;

			// Token: 0x04000447 RID: 1095
			internal ClipperLib.TEdge NextInSEL;

			// Token: 0x04000448 RID: 1096
			internal ClipperLib.TEdge PrevInSEL;
		}

		// Token: 0x020000D4 RID: 212
		public class IntersectNode
		{
			// Token: 0x04000449 RID: 1097
			internal ClipperLib.TEdge Edge1;

			// Token: 0x0400044A RID: 1098
			internal ClipperLib.TEdge Edge2;

			// Token: 0x0400044B RID: 1099
			internal ClipperLib.IntPoint Pt;
		}

		// Token: 0x020000D5 RID: 213
		public class MyIntersectNodeSort : IComparer<ClipperLib.IntersectNode>
		{
			// Token: 0x06000475 RID: 1141 RVA: 0x000192D0 File Offset: 0x000174D0
			public int Compare(ClipperLib.IntersectNode node1, ClipperLib.IntersectNode node2)
			{
				long i = node2.Pt.Y - node1.Pt.Y;
				if (i > 0L)
				{
					return 1;
				}
				if (i < 0L)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x020000D6 RID: 214
		internal class LocalMinima
		{
			// Token: 0x0400044C RID: 1100
			internal long Y;

			// Token: 0x0400044D RID: 1101
			internal ClipperLib.TEdge LeftBound;

			// Token: 0x0400044E RID: 1102
			internal ClipperLib.TEdge RightBound;

			// Token: 0x0400044F RID: 1103
			internal ClipperLib.LocalMinima Next;
		}

		// Token: 0x020000D7 RID: 215
		internal class Scanbeam
		{
			// Token: 0x04000450 RID: 1104
			internal long Y;

			// Token: 0x04000451 RID: 1105
			internal ClipperLib.Scanbeam Next;
		}

		// Token: 0x020000D8 RID: 216
		internal class Maxima
		{
			// Token: 0x04000452 RID: 1106
			internal long X;

			// Token: 0x04000453 RID: 1107
			internal ClipperLib.Maxima Next;

			// Token: 0x04000454 RID: 1108
			internal ClipperLib.Maxima Prev;
		}

		// Token: 0x020000D9 RID: 217
		internal class OutRec
		{
			// Token: 0x04000455 RID: 1109
			internal int Idx;

			// Token: 0x04000456 RID: 1110
			internal bool IsHole;

			// Token: 0x04000457 RID: 1111
			internal bool IsOpen;

			// Token: 0x04000458 RID: 1112
			internal ClipperLib.OutRec FirstLeft;

			// Token: 0x04000459 RID: 1113
			internal ClipperLib.OutPt Pts;

			// Token: 0x0400045A RID: 1114
			internal ClipperLib.OutPt BottomPt;

			// Token: 0x0400045B RID: 1115
			internal ClipperLib.PolyNode PolyNode;
		}

		// Token: 0x020000DA RID: 218
		internal class OutPt
		{
			// Token: 0x0400045C RID: 1116
			internal int Idx;

			// Token: 0x0400045D RID: 1117
			internal ClipperLib.IntPoint Pt;

			// Token: 0x0400045E RID: 1118
			internal ClipperLib.OutPt Next;

			// Token: 0x0400045F RID: 1119
			internal ClipperLib.OutPt Prev;
		}

		// Token: 0x020000DB RID: 219
		internal class Join
		{
			// Token: 0x04000460 RID: 1120
			internal ClipperLib.OutPt OutPt1;

			// Token: 0x04000461 RID: 1121
			internal ClipperLib.OutPt OutPt2;

			// Token: 0x04000462 RID: 1122
			internal ClipperLib.IntPoint OffPt;
		}

		// Token: 0x020000DC RID: 220
		public class ClipperBase
		{
			// Token: 0x0600047D RID: 1149 RVA: 0x00019304 File Offset: 0x00017504
			internal static bool near_zero(double val)
			{
				return val > -1E-20 && val < 1E-20;
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x0600047E RID: 1150 RVA: 0x00019320 File Offset: 0x00017520
			// (set) Token: 0x0600047F RID: 1151 RVA: 0x00019328 File Offset: 0x00017528
			public bool PreserveCollinear { get; set; }

			// Token: 0x06000480 RID: 1152 RVA: 0x00019334 File Offset: 0x00017534
			public void Swap(ref long val1, ref long val2)
			{
				long tmp = val1;
				val1 = val2;
				val2 = tmp;
			}

			// Token: 0x06000481 RID: 1153 RVA: 0x0001934B File Offset: 0x0001754B
			internal static bool IsHorizontal(ClipperLib.TEdge e)
			{
				return e.Delta.Y == 0L;
			}

			// Token: 0x06000482 RID: 1154 RVA: 0x0001935C File Offset: 0x0001755C
			internal bool PointIsVertex(ClipperLib.IntPoint pt, ClipperLib.OutPt pp)
			{
				ClipperLib.OutPt pp2 = pp;
				while (!(pp2.Pt == pt))
				{
					pp2 = pp2.Next;
					if (pp2 == pp)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06000483 RID: 1155 RVA: 0x00019388 File Offset: 0x00017588
			internal bool PointOnLineSegment(ClipperLib.IntPoint pt, ClipperLib.IntPoint linePt1, ClipperLib.IntPoint linePt2, bool UseFullRange)
			{
				if (UseFullRange)
				{
					return (pt.X == linePt1.X && pt.Y == linePt1.Y) || (pt.X == linePt2.X && pt.Y == linePt2.Y) || (pt.X > linePt1.X == pt.X < linePt2.X && pt.Y > linePt1.Y == pt.Y < linePt2.Y && ClipperLib.Int128.Int128Mul(pt.X - linePt1.X, linePt2.Y - linePt1.Y) == ClipperLib.Int128.Int128Mul(linePt2.X - linePt1.X, pt.Y - linePt1.Y));
				}
				return (pt.X == linePt1.X && pt.Y == linePt1.Y) || (pt.X == linePt2.X && pt.Y == linePt2.Y) || (pt.X > linePt1.X == pt.X < linePt2.X && pt.Y > linePt1.Y == pt.Y < linePt2.Y && (pt.X - linePt1.X) * (linePt2.Y - linePt1.Y) == (linePt2.X - linePt1.X) * (pt.Y - linePt1.Y));
			}

			// Token: 0x06000484 RID: 1156 RVA: 0x00019514 File Offset: 0x00017714
			internal bool PointOnPolygon(ClipperLib.IntPoint pt, ClipperLib.OutPt pp, bool UseFullRange)
			{
				ClipperLib.OutPt pp2 = pp;
				while (!this.PointOnLineSegment(pt, pp2.Pt, pp2.Next.Pt, UseFullRange))
				{
					pp2 = pp2.Next;
					if (pp2 == pp)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06000485 RID: 1157 RVA: 0x0001954C File Offset: 0x0001774C
			internal static bool SlopesEqual(ClipperLib.TEdge e1, ClipperLib.TEdge e2, bool UseFullRange)
			{
				if (UseFullRange)
				{
					return ClipperLib.Int128.Int128Mul(e1.Delta.Y, e2.Delta.X) == ClipperLib.Int128.Int128Mul(e1.Delta.X, e2.Delta.Y);
				}
				return e1.Delta.Y * e2.Delta.X == e1.Delta.X * e2.Delta.Y;
			}

			// Token: 0x06000486 RID: 1158 RVA: 0x000195C8 File Offset: 0x000177C8
			internal static bool SlopesEqual(ClipperLib.IntPoint pt1, ClipperLib.IntPoint pt2, ClipperLib.IntPoint pt3, bool UseFullRange)
			{
				if (UseFullRange)
				{
					return ClipperLib.Int128.Int128Mul(pt1.Y - pt2.Y, pt2.X - pt3.X) == ClipperLib.Int128.Int128Mul(pt1.X - pt2.X, pt2.Y - pt3.Y);
				}
				return (pt1.Y - pt2.Y) * (pt2.X - pt3.X) - (pt1.X - pt2.X) * (pt2.Y - pt3.Y) == 0L;
			}

			// Token: 0x06000487 RID: 1159 RVA: 0x00019658 File Offset: 0x00017858
			internal static bool SlopesEqual(ClipperLib.IntPoint pt1, ClipperLib.IntPoint pt2, ClipperLib.IntPoint pt3, ClipperLib.IntPoint pt4, bool UseFullRange)
			{
				if (UseFullRange)
				{
					return ClipperLib.Int128.Int128Mul(pt1.Y - pt2.Y, pt3.X - pt4.X) == ClipperLib.Int128.Int128Mul(pt1.X - pt2.X, pt3.Y - pt4.Y);
				}
				return (pt1.Y - pt2.Y) * (pt3.X - pt4.X) - (pt1.X - pt2.X) * (pt3.Y - pt4.Y) == 0L;
			}

			// Token: 0x06000488 RID: 1160 RVA: 0x000196E8 File Offset: 0x000178E8
			internal ClipperBase()
			{
				this.m_MinimaList = null;
				this.m_CurrentLM = null;
				this.m_UseFullRange = false;
				this.m_HasOpenPaths = false;
			}

			// Token: 0x06000489 RID: 1161 RVA: 0x00019718 File Offset: 0x00017918
			public virtual void Clear()
			{
				this.DisposeLocalMinimaList();
				for (int i = 0; i < this.m_edges.Count; i++)
				{
					for (int j = 0; j < this.m_edges[i].Count; j++)
					{
						this.m_edges[i][j] = null;
					}
					this.m_edges[i].Clear();
				}
				this.m_edges.Clear();
				this.m_UseFullRange = false;
				this.m_HasOpenPaths = false;
			}

			// Token: 0x0600048A RID: 1162 RVA: 0x0001979C File Offset: 0x0001799C
			private void DisposeLocalMinimaList()
			{
				while (this.m_MinimaList != null)
				{
					ClipperLib.LocalMinima tmpLm = this.m_MinimaList.Next;
					this.m_MinimaList = null;
					this.m_MinimaList = tmpLm;
				}
				this.m_CurrentLM = null;
			}

			// Token: 0x0600048B RID: 1163 RVA: 0x000197D4 File Offset: 0x000179D4
			private void RangeTest(ClipperLib.IntPoint Pt, ref bool useFullRange)
			{
				if (useFullRange)
				{
					if (Pt.X > 4611686018427387903L || Pt.Y > 4611686018427387903L || -Pt.X > 4611686018427387903L || -Pt.Y > 4611686018427387903L)
					{
						throw new ClipperLib.ClipperException("Coordinate outside allowed range");
					}
				}
				else if (Pt.X > 1073741823L || Pt.Y > 1073741823L || -Pt.X > 1073741823L || -Pt.Y > 1073741823L)
				{
					useFullRange = true;
					this.RangeTest(Pt, ref useFullRange);
				}
			}

			// Token: 0x0600048C RID: 1164 RVA: 0x0001987B File Offset: 0x00017A7B
			private void InitEdge(ClipperLib.TEdge e, ClipperLib.TEdge eNext, ClipperLib.TEdge ePrev, ClipperLib.IntPoint pt)
			{
				e.Next = eNext;
				e.Prev = ePrev;
				e.Curr = pt;
				e.OutIdx = -1;
			}

			// Token: 0x0600048D RID: 1165 RVA: 0x0001989C File Offset: 0x00017A9C
			private void InitEdge2(ClipperLib.TEdge e, ClipperLib.PolyType polyType)
			{
				if (e.Curr.Y >= e.Next.Curr.Y)
				{
					e.Bot = e.Curr;
					e.Top = e.Next.Curr;
				}
				else
				{
					e.Top = e.Curr;
					e.Bot = e.Next.Curr;
				}
				this.SetDx(e);
				e.PolyTyp = polyType;
			}

			// Token: 0x0600048E RID: 1166 RVA: 0x00019910 File Offset: 0x00017B10
			private ClipperLib.TEdge FindNextLocMin(ClipperLib.TEdge E)
			{
				ClipperLib.TEdge E2;
				for (;;)
				{
					if (!(E.Bot != E.Prev.Bot) && !(E.Curr == E.Top))
					{
						if (E.Dx != -3.4E+38 && E.Prev.Dx != -3.4E+38)
						{
							break;
						}
						while (E.Prev.Dx == -3.4E+38)
						{
							E = E.Prev;
						}
						E2 = E;
						while (E.Dx == -3.4E+38)
						{
							E = E.Next;
						}
						if (E.Top.Y != E.Prev.Bot.Y)
						{
							goto Block_7;
						}
					}
					else
					{
						E = E.Next;
					}
				}
				return E;
				Block_7:
				if (E2.Prev.Bot.X < E.Bot.X)
				{
					E = E2;
				}
				return E;
			}

			// Token: 0x0600048F RID: 1167 RVA: 0x000199F8 File Offset: 0x00017BF8
			private ClipperLib.TEdge ProcessBound(ClipperLib.TEdge E, bool LeftBoundIsForward)
			{
				ClipperLib.TEdge Result = E;
				if (Result.OutIdx == -2)
				{
					E = Result;
					if (LeftBoundIsForward)
					{
						while (E.Top.Y == E.Next.Bot.Y)
						{
							E = E.Next;
						}
						while (E != Result)
						{
							if (E.Dx != -3.4E+38)
							{
								break;
							}
							E = E.Prev;
						}
					}
					else
					{
						while (E.Top.Y == E.Prev.Bot.Y)
						{
							E = E.Prev;
						}
						while (E != Result && E.Dx == -3.4E+38)
						{
							E = E.Next;
						}
					}
					if (E == Result)
					{
						if (LeftBoundIsForward)
						{
							Result = E.Next;
						}
						else
						{
							Result = E.Prev;
						}
					}
					else
					{
						if (LeftBoundIsForward)
						{
							E = Result.Next;
						}
						else
						{
							E = Result.Prev;
						}
						ClipperLib.LocalMinima locMin = new ClipperLib.LocalMinima();
						locMin.Next = null;
						locMin.Y = E.Bot.Y;
						locMin.LeftBound = null;
						locMin.RightBound = E;
						E.WindDelta = 0;
						Result = this.ProcessBound(E, LeftBoundIsForward);
						this.InsertLocalMinima(locMin);
					}
					return Result;
				}
				ClipperLib.TEdge EStart;
				if (E.Dx == -3.4E+38)
				{
					if (LeftBoundIsForward)
					{
						EStart = E.Prev;
					}
					else
					{
						EStart = E.Next;
					}
					if (EStart.Dx == -3.4E+38)
					{
						if (EStart.Bot.X != E.Bot.X && EStart.Top.X != E.Bot.X)
						{
							this.ReverseHorizontal(E);
						}
					}
					else if (EStart.Bot.X != E.Bot.X)
					{
						this.ReverseHorizontal(E);
					}
				}
				EStart = E;
				if (LeftBoundIsForward)
				{
					while (Result.Top.Y == Result.Next.Bot.Y && Result.Next.OutIdx != -2)
					{
						Result = Result.Next;
					}
					if (Result.Dx == -3.4E+38 && Result.Next.OutIdx != -2)
					{
						ClipperLib.TEdge Horz = Result;
						while (Horz.Prev.Dx == -3.4E+38)
						{
							Horz = Horz.Prev;
						}
						if (Horz.Prev.Top.X > Result.Next.Top.X)
						{
							Result = Horz.Prev;
						}
					}
					while (E != Result)
					{
						E.NextInLML = E.Next;
						if (E.Dx == -3.4E+38 && E != EStart && E.Bot.X != E.Prev.Top.X)
						{
							this.ReverseHorizontal(E);
						}
						E = E.Next;
					}
					if (E.Dx == -3.4E+38 && E != EStart && E.Bot.X != E.Prev.Top.X)
					{
						this.ReverseHorizontal(E);
					}
					Result = Result.Next;
				}
				else
				{
					while (Result.Top.Y == Result.Prev.Bot.Y && Result.Prev.OutIdx != -2)
					{
						Result = Result.Prev;
					}
					if (Result.Dx == -3.4E+38 && Result.Prev.OutIdx != -2)
					{
						ClipperLib.TEdge Horz = Result;
						while (Horz.Next.Dx == -3.4E+38)
						{
							Horz = Horz.Next;
						}
						if (Horz.Next.Top.X == Result.Prev.Top.X || Horz.Next.Top.X > Result.Prev.Top.X)
						{
							Result = Horz.Next;
						}
					}
					while (E != Result)
					{
						E.NextInLML = E.Prev;
						if (E.Dx == -3.4E+38 && E != EStart && E.Bot.X != E.Next.Top.X)
						{
							this.ReverseHorizontal(E);
						}
						E = E.Prev;
					}
					if (E.Dx == -3.4E+38 && E != EStart && E.Bot.X != E.Next.Top.X)
					{
						this.ReverseHorizontal(E);
					}
					Result = Result.Prev;
				}
				return Result;
			}

			// Token: 0x06000490 RID: 1168 RVA: 0x00019E44 File Offset: 0x00018044
			public bool AddPath(List<ClipperLib.IntPoint> pg, ClipperLib.PolyType polyType, bool Closed)
			{
				if (!Closed && polyType == ClipperLib.PolyType.ptClip)
				{
					throw new ClipperLib.ClipperException("AddPath: Open paths must be subject.");
				}
				int highI = pg.Count - 1;
				if (Closed)
				{
					while (highI > 0)
					{
						if (!(pg[highI] == pg[0]))
						{
							break;
						}
						highI--;
					}
				}
				while (highI > 0 && pg[highI] == pg[highI - 1])
				{
					highI--;
				}
				if ((Closed && highI < 2) || (!Closed && highI < 1))
				{
					return false;
				}
				List<ClipperLib.TEdge> edges = new List<ClipperLib.TEdge>(highI + 1);
				for (int i = 0; i <= highI; i++)
				{
					edges.Add(new ClipperLib.TEdge());
				}
				bool IsFlat = true;
				edges[1].Curr = pg[1];
				this.RangeTest(pg[0], ref this.m_UseFullRange);
				this.RangeTest(pg[highI], ref this.m_UseFullRange);
				this.InitEdge(edges[0], edges[1], edges[highI], pg[0]);
				this.InitEdge(edges[highI], edges[0], edges[highI - 1], pg[highI]);
				for (int j = highI - 1; j >= 1; j--)
				{
					this.RangeTest(pg[j], ref this.m_UseFullRange);
					this.InitEdge(edges[j], edges[j + 1], edges[j - 1], pg[j]);
				}
				ClipperLib.TEdge eStart = edges[0];
				ClipperLib.TEdge E = eStart;
				ClipperLib.TEdge eLoopStop = eStart;
				for (;;)
				{
					if (E.Curr == E.Next.Curr && (Closed || E.Next != eStart))
					{
						if (E == E.Next)
						{
							break;
						}
						if (E == eStart)
						{
							eStart = E.Next;
						}
						E = this.RemoveEdge(E);
						eLoopStop = E;
					}
					else
					{
						if (E.Prev == E.Next)
						{
							break;
						}
						if (Closed && ClipperLib.ClipperBase.SlopesEqual(E.Prev.Curr, E.Curr, E.Next.Curr, this.m_UseFullRange) && (!this.PreserveCollinear || !this.Pt2IsBetweenPt1AndPt3(E.Prev.Curr, E.Curr, E.Next.Curr)))
						{
							if (E == eStart)
							{
								eStart = E.Next;
							}
							E = this.RemoveEdge(E);
							E = E.Prev;
							eLoopStop = E;
						}
						else
						{
							E = E.Next;
							if (E == eLoopStop || (!Closed && E.Next == eStart))
							{
								break;
							}
						}
					}
				}
				if ((!Closed && E == E.Next) || (Closed && E.Prev == E.Next))
				{
					return false;
				}
				if (!Closed)
				{
					this.m_HasOpenPaths = true;
					eStart.Prev.OutIdx = -2;
				}
				E = eStart;
				do
				{
					this.InitEdge2(E, polyType);
					E = E.Next;
					if (IsFlat && E.Curr.Y != eStart.Curr.Y)
					{
						IsFlat = false;
					}
				}
				while (E != eStart);
				if (!IsFlat)
				{
					this.m_edges.Add(edges);
					ClipperLib.TEdge EMin = null;
					if (E.Prev.Bot == E.Prev.Top)
					{
						E = E.Next;
					}
					for (;;)
					{
						E = this.FindNextLocMin(E);
						if (E == EMin)
						{
							break;
						}
						if (EMin == null)
						{
							EMin = E;
						}
						ClipperLib.LocalMinima locMin = new ClipperLib.LocalMinima();
						locMin.Next = null;
						locMin.Y = E.Bot.Y;
						bool leftBoundIsForward;
						if (E.Dx < E.Prev.Dx)
						{
							locMin.LeftBound = E.Prev;
							locMin.RightBound = E;
							leftBoundIsForward = false;
						}
						else
						{
							locMin.LeftBound = E;
							locMin.RightBound = E.Prev;
							leftBoundIsForward = true;
						}
						locMin.LeftBound.Side = ClipperLib.EdgeSide.esLeft;
						locMin.RightBound.Side = ClipperLib.EdgeSide.esRight;
						if (!Closed)
						{
							locMin.LeftBound.WindDelta = 0;
						}
						else if (locMin.LeftBound.Next == locMin.RightBound)
						{
							locMin.LeftBound.WindDelta = -1;
						}
						else
						{
							locMin.LeftBound.WindDelta = 1;
						}
						locMin.RightBound.WindDelta = -locMin.LeftBound.WindDelta;
						E = this.ProcessBound(locMin.LeftBound, leftBoundIsForward);
						if (E.OutIdx == -2)
						{
							E = this.ProcessBound(E, leftBoundIsForward);
						}
						ClipperLib.TEdge E2 = this.ProcessBound(locMin.RightBound, !leftBoundIsForward);
						if (E2.OutIdx == -2)
						{
							E2 = this.ProcessBound(E2, !leftBoundIsForward);
						}
						if (locMin.LeftBound.OutIdx == -2)
						{
							locMin.LeftBound = null;
						}
						else if (locMin.RightBound.OutIdx == -2)
						{
							locMin.RightBound = null;
						}
						this.InsertLocalMinima(locMin);
						if (!leftBoundIsForward)
						{
							E = E2;
						}
					}
					return true;
				}
				if (Closed)
				{
					return false;
				}
				E.Prev.OutIdx = -2;
				ClipperLib.LocalMinima locMin2 = new ClipperLib.LocalMinima();
				locMin2.Next = null;
				locMin2.Y = E.Bot.Y;
				locMin2.LeftBound = null;
				locMin2.RightBound = E;
				locMin2.RightBound.Side = ClipperLib.EdgeSide.esRight;
				locMin2.RightBound.WindDelta = 0;
				for (;;)
				{
					if (E.Bot.X != E.Prev.Top.X)
					{
						this.ReverseHorizontal(E);
					}
					if (E.Next.OutIdx == -2)
					{
						break;
					}
					E.NextInLML = E.Next;
					E = E.Next;
				}
				this.InsertLocalMinima(locMin2);
				this.m_edges.Add(edges);
				return true;
			}

			// Token: 0x06000491 RID: 1169 RVA: 0x0001A3E8 File Offset: 0x000185E8
			public bool AddPaths(List<List<ClipperLib.IntPoint>> ppg, ClipperLib.PolyType polyType, bool closed)
			{
				bool result = false;
				for (int i = 0; i < ppg.Count; i++)
				{
					if (this.AddPath(ppg[i], polyType, closed))
					{
						result = true;
					}
				}
				return result;
			}

			// Token: 0x06000492 RID: 1170 RVA: 0x0001A41C File Offset: 0x0001861C
			internal bool Pt2IsBetweenPt1AndPt3(ClipperLib.IntPoint pt1, ClipperLib.IntPoint pt2, ClipperLib.IntPoint pt3)
			{
				if (pt1 == pt3 || pt1 == pt2 || pt3 == pt2)
				{
					return false;
				}
				if (pt1.X != pt3.X)
				{
					return pt2.X > pt1.X == pt2.X < pt3.X;
				}
				return pt2.Y > pt1.Y == pt2.Y < pt3.Y;
			}

			// Token: 0x06000493 RID: 1171 RVA: 0x0001A491 File Offset: 0x00018691
			private ClipperLib.TEdge RemoveEdge(ClipperLib.TEdge e)
			{
				e.Prev.Next = e.Next;
				e.Next.Prev = e.Prev;
				ClipperLib.TEdge next = e.Next;
				e.Prev = null;
				return next;
			}

			// Token: 0x06000494 RID: 1172 RVA: 0x0001A4C4 File Offset: 0x000186C4
			private void SetDx(ClipperLib.TEdge e)
			{
				e.Delta.X = e.Top.X - e.Bot.X;
				e.Delta.Y = e.Top.Y - e.Bot.Y;
				if (e.Delta.Y == 0L)
				{
					e.Dx = -3.4E+38;
					return;
				}
				e.Dx = (double)e.Delta.X / (double)e.Delta.Y;
			}

			// Token: 0x06000495 RID: 1173 RVA: 0x0001A554 File Offset: 0x00018754
			private void InsertLocalMinima(ClipperLib.LocalMinima newLm)
			{
				if (this.m_MinimaList == null)
				{
					this.m_MinimaList = newLm;
					return;
				}
				if (newLm.Y >= this.m_MinimaList.Y)
				{
					newLm.Next = this.m_MinimaList;
					this.m_MinimaList = newLm;
					return;
				}
				ClipperLib.LocalMinima tmpLm = this.m_MinimaList;
				while (tmpLm.Next != null && newLm.Y < tmpLm.Next.Y)
				{
					tmpLm = tmpLm.Next;
				}
				newLm.Next = tmpLm.Next;
				tmpLm.Next = newLm;
			}

			// Token: 0x06000496 RID: 1174 RVA: 0x0001A5D6 File Offset: 0x000187D6
			internal bool PopLocalMinima(long Y, out ClipperLib.LocalMinima current)
			{
				current = this.m_CurrentLM;
				if (this.m_CurrentLM != null && this.m_CurrentLM.Y == Y)
				{
					this.m_CurrentLM = this.m_CurrentLM.Next;
					return true;
				}
				return false;
			}

			// Token: 0x06000497 RID: 1175 RVA: 0x0001A60A File Offset: 0x0001880A
			private void ReverseHorizontal(ClipperLib.TEdge e)
			{
				this.Swap(ref e.Top.X, ref e.Bot.X);
			}

			// Token: 0x06000498 RID: 1176 RVA: 0x0001A628 File Offset: 0x00018828
			internal virtual void Reset()
			{
				this.m_CurrentLM = this.m_MinimaList;
				if (this.m_CurrentLM == null)
				{
					return;
				}
				this.m_Scanbeam = null;
				for (ClipperLib.LocalMinima lm = this.m_MinimaList; lm != null; lm = lm.Next)
				{
					this.InsertScanbeam(lm.Y);
					ClipperLib.TEdge e = lm.LeftBound;
					if (e != null)
					{
						e.Curr = e.Bot;
						e.OutIdx = -1;
					}
					e = lm.RightBound;
					if (e != null)
					{
						e.Curr = e.Bot;
						e.OutIdx = -1;
					}
				}
				this.m_ActiveEdges = null;
			}

			// Token: 0x06000499 RID: 1177 RVA: 0x0001A6B4 File Offset: 0x000188B4
			public static ClipperLib.IntRect GetBounds(List<List<ClipperLib.IntPoint>> paths)
			{
				int i = 0;
				int cnt = paths.Count;
				while (i < cnt && paths[i].Count == 0)
				{
					i++;
				}
				if (i == cnt)
				{
					return new ClipperLib.IntRect(0L, 0L, 0L, 0L);
				}
				ClipperLib.IntRect result = default(ClipperLib.IntRect);
				result.left = paths[i][0].X;
				result.right = result.left;
				result.top = paths[i][0].Y;
				result.bottom = result.top;
				while (i < cnt)
				{
					for (int j = 0; j < paths[i].Count; j++)
					{
						if (paths[i][j].X < result.left)
						{
							result.left = paths[i][j].X;
						}
						else if (paths[i][j].X > result.right)
						{
							result.right = paths[i][j].X;
						}
						if (paths[i][j].Y < result.top)
						{
							result.top = paths[i][j].Y;
						}
						else if (paths[i][j].Y > result.bottom)
						{
							result.bottom = paths[i][j].Y;
						}
					}
					i++;
				}
				return result;
			}

			// Token: 0x0600049A RID: 1178 RVA: 0x0001A848 File Offset: 0x00018A48
			internal void InsertScanbeam(long Y)
			{
				if (this.m_Scanbeam == null)
				{
					this.m_Scanbeam = new ClipperLib.Scanbeam();
					this.m_Scanbeam.Next = null;
					this.m_Scanbeam.Y = Y;
					return;
				}
				if (Y > this.m_Scanbeam.Y)
				{
					this.m_Scanbeam = new ClipperLib.Scanbeam
					{
						Y = Y,
						Next = this.m_Scanbeam
					};
					return;
				}
				ClipperLib.Scanbeam sb2 = this.m_Scanbeam;
				while (sb2.Next != null && Y <= sb2.Next.Y)
				{
					sb2 = sb2.Next;
				}
				if (Y == sb2.Y)
				{
					return;
				}
				sb2.Next = new ClipperLib.Scanbeam
				{
					Y = Y,
					Next = sb2.Next
				};
			}

			// Token: 0x0600049B RID: 1179 RVA: 0x0001A900 File Offset: 0x00018B00
			internal bool PopScanbeam(out long Y)
			{
				if (this.m_Scanbeam == null)
				{
					Y = 0L;
					return false;
				}
				Y = this.m_Scanbeam.Y;
				this.m_Scanbeam = this.m_Scanbeam.Next;
				return true;
			}

			// Token: 0x0600049C RID: 1180 RVA: 0x0001A92F File Offset: 0x00018B2F
			internal bool LocalMinimaPending()
			{
				return this.m_CurrentLM != null;
			}

			// Token: 0x0600049D RID: 1181 RVA: 0x0001A93C File Offset: 0x00018B3C
			internal ClipperLib.OutRec CreateOutRec()
			{
				ClipperLib.OutRec result = new ClipperLib.OutRec();
				result.Idx = -1;
				result.IsHole = false;
				result.IsOpen = false;
				result.FirstLeft = null;
				result.Pts = null;
				result.BottomPt = null;
				result.PolyNode = null;
				this.m_PolyOuts.Add(result);
				result.Idx = this.m_PolyOuts.Count - 1;
				return result;
			}

			// Token: 0x0600049E RID: 1182 RVA: 0x0001A9A0 File Offset: 0x00018BA0
			internal void DisposeOutRec(int index)
			{
				this.m_PolyOuts[index].Pts = null;
				this.m_PolyOuts[index] = null;
			}

			// Token: 0x0600049F RID: 1183 RVA: 0x0001A9C4 File Offset: 0x00018BC4
			internal void UpdateEdgeIntoAEL(ref ClipperLib.TEdge e)
			{
				if (e.NextInLML == null)
				{
					throw new ClipperLib.ClipperException("UpdateEdgeIntoAEL: invalid call");
				}
				ClipperLib.TEdge AelPrev = e.PrevInAEL;
				ClipperLib.TEdge AelNext = e.NextInAEL;
				e.NextInLML.OutIdx = e.OutIdx;
				if (AelPrev != null)
				{
					AelPrev.NextInAEL = e.NextInLML;
				}
				else
				{
					this.m_ActiveEdges = e.NextInLML;
				}
				if (AelNext != null)
				{
					AelNext.PrevInAEL = e.NextInLML;
				}
				e.NextInLML.Side = e.Side;
				e.NextInLML.WindDelta = e.WindDelta;
				e.NextInLML.WindCnt = e.WindCnt;
				e.NextInLML.WindCnt2 = e.WindCnt2;
				e = e.NextInLML;
				e.Curr = e.Bot;
				e.PrevInAEL = AelPrev;
				e.NextInAEL = AelNext;
				if (!ClipperLib.ClipperBase.IsHorizontal(e))
				{
					this.InsertScanbeam(e.Top.Y);
				}
			}

			// Token: 0x060004A0 RID: 1184 RVA: 0x0001AAC8 File Offset: 0x00018CC8
			internal void SwapPositionsInAEL(ClipperLib.TEdge edge1, ClipperLib.TEdge edge2)
			{
				if (edge1.NextInAEL == edge1.PrevInAEL || edge2.NextInAEL == edge2.PrevInAEL)
				{
					return;
				}
				if (edge1.NextInAEL == edge2)
				{
					ClipperLib.TEdge next = edge2.NextInAEL;
					if (next != null)
					{
						next.PrevInAEL = edge1;
					}
					ClipperLib.TEdge prev = edge1.PrevInAEL;
					if (prev != null)
					{
						prev.NextInAEL = edge2;
					}
					edge2.PrevInAEL = prev;
					edge2.NextInAEL = edge1;
					edge1.PrevInAEL = edge2;
					edge1.NextInAEL = next;
				}
				else if (edge2.NextInAEL == edge1)
				{
					ClipperLib.TEdge next2 = edge1.NextInAEL;
					if (next2 != null)
					{
						next2.PrevInAEL = edge2;
					}
					ClipperLib.TEdge prev2 = edge2.PrevInAEL;
					if (prev2 != null)
					{
						prev2.NextInAEL = edge1;
					}
					edge1.PrevInAEL = prev2;
					edge1.NextInAEL = edge2;
					edge2.PrevInAEL = edge1;
					edge2.NextInAEL = next2;
				}
				else
				{
					ClipperLib.TEdge next3 = edge1.NextInAEL;
					ClipperLib.TEdge prev3 = edge1.PrevInAEL;
					edge1.NextInAEL = edge2.NextInAEL;
					if (edge1.NextInAEL != null)
					{
						edge1.NextInAEL.PrevInAEL = edge1;
					}
					edge1.PrevInAEL = edge2.PrevInAEL;
					if (edge1.PrevInAEL != null)
					{
						edge1.PrevInAEL.NextInAEL = edge1;
					}
					edge2.NextInAEL = next3;
					if (edge2.NextInAEL != null)
					{
						edge2.NextInAEL.PrevInAEL = edge2;
					}
					edge2.PrevInAEL = prev3;
					if (edge2.PrevInAEL != null)
					{
						edge2.PrevInAEL.NextInAEL = edge2;
					}
				}
				if (edge1.PrevInAEL == null)
				{
					this.m_ActiveEdges = edge1;
					return;
				}
				if (edge2.PrevInAEL == null)
				{
					this.m_ActiveEdges = edge2;
				}
			}

			// Token: 0x060004A1 RID: 1185 RVA: 0x0001AC34 File Offset: 0x00018E34
			internal void DeleteFromAEL(ClipperLib.TEdge e)
			{
				ClipperLib.TEdge AelPrev = e.PrevInAEL;
				ClipperLib.TEdge AelNext = e.NextInAEL;
				if (AelPrev == null && AelNext == null && e != this.m_ActiveEdges)
				{
					return;
				}
				if (AelPrev != null)
				{
					AelPrev.NextInAEL = AelNext;
				}
				else
				{
					this.m_ActiveEdges = AelNext;
				}
				if (AelNext != null)
				{
					AelNext.PrevInAEL = AelPrev;
				}
				e.NextInAEL = null;
				e.PrevInAEL = null;
			}

			// Token: 0x04000463 RID: 1123
			internal const double horizontal = -3.4E+38;

			// Token: 0x04000464 RID: 1124
			internal const int Skip = -2;

			// Token: 0x04000465 RID: 1125
			internal const int Unassigned = -1;

			// Token: 0x04000466 RID: 1126
			internal const double tolerance = 1E-20;

			// Token: 0x04000467 RID: 1127
			public const long loRange = 1073741823L;

			// Token: 0x04000468 RID: 1128
			public const long hiRange = 4611686018427387903L;

			// Token: 0x04000469 RID: 1129
			internal ClipperLib.LocalMinima m_MinimaList;

			// Token: 0x0400046A RID: 1130
			internal ClipperLib.LocalMinima m_CurrentLM;

			// Token: 0x0400046B RID: 1131
			internal List<List<ClipperLib.TEdge>> m_edges = new List<List<ClipperLib.TEdge>>();

			// Token: 0x0400046C RID: 1132
			internal ClipperLib.Scanbeam m_Scanbeam;

			// Token: 0x0400046D RID: 1133
			internal List<ClipperLib.OutRec> m_PolyOuts;

			// Token: 0x0400046E RID: 1134
			internal ClipperLib.TEdge m_ActiveEdges;

			// Token: 0x0400046F RID: 1135
			internal bool m_UseFullRange;

			// Token: 0x04000470 RID: 1136
			internal bool m_HasOpenPaths;
		}

		// Token: 0x020000DD RID: 221
		public class Clipper : ClipperLib.ClipperBase
		{
			// Token: 0x060004A2 RID: 1186 RVA: 0x0001AC8C File Offset: 0x00018E8C
			public Clipper(int InitOptions = 0)
			{
				this.m_Scanbeam = null;
				this.m_Maxima = null;
				this.m_ActiveEdges = null;
				this.m_SortedEdges = null;
				this.m_IntersectList = new List<ClipperLib.IntersectNode>();
				this.m_IntersectNodeComparer = new ClipperLib.MyIntersectNodeSort();
				this.m_ExecuteLocked = false;
				this.m_UsingPolyTree = false;
				this.m_PolyOuts = new List<ClipperLib.OutRec>();
				this.m_Joins = new List<ClipperLib.Join>();
				this.m_GhostJoins = new List<ClipperLib.Join>();
				this.ReverseSolution = (1 & InitOptions) != 0;
				this.StrictlySimple = (2 & InitOptions) != 0;
				base.PreserveCollinear = (4 & InitOptions) != 0;
			}

			// Token: 0x060004A3 RID: 1187 RVA: 0x0001AD24 File Offset: 0x00018F24
			private void InsertMaxima(long X)
			{
				ClipperLib.Maxima newMax = new ClipperLib.Maxima();
				newMax.X = X;
				if (this.m_Maxima == null)
				{
					this.m_Maxima = newMax;
					this.m_Maxima.Next = null;
					this.m_Maxima.Prev = null;
					return;
				}
				if (X < this.m_Maxima.X)
				{
					newMax.Next = this.m_Maxima;
					newMax.Prev = null;
					this.m_Maxima = newMax;
					return;
				}
				ClipperLib.Maxima i = this.m_Maxima;
				while (i.Next != null && X >= i.Next.X)
				{
					i = i.Next;
				}
				if (X == i.X)
				{
					return;
				}
				newMax.Next = i.Next;
				newMax.Prev = i;
				if (i.Next != null)
				{
					i.Next.Prev = newMax;
				}
				i.Next = newMax;
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001ADED File Offset: 0x00018FED
			// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0001ADF5 File Offset: 0x00018FF5
			public bool ReverseSolution { get; set; }

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0001ADFE File Offset: 0x00018FFE
			// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0001AE06 File Offset: 0x00019006
			public bool StrictlySimple { get; set; }

			// Token: 0x060004A8 RID: 1192 RVA: 0x0001AE0F File Offset: 0x0001900F
			public bool Execute(ClipperLib.ClipType clipType, List<List<ClipperLib.IntPoint>> solution, ClipperLib.PolyFillType FillType = ClipperLib.PolyFillType.pftEvenOdd)
			{
				return this.Execute(clipType, solution, FillType, FillType);
			}

			// Token: 0x060004A9 RID: 1193 RVA: 0x0001AE1B File Offset: 0x0001901B
			public bool Execute(ClipperLib.ClipType clipType, ClipperLib.PolyTree polytree, ClipperLib.PolyFillType FillType = ClipperLib.PolyFillType.pftEvenOdd)
			{
				return this.Execute(clipType, polytree, FillType, FillType);
			}

			// Token: 0x060004AA RID: 1194 RVA: 0x0001AE28 File Offset: 0x00019028
			public bool Execute(ClipperLib.ClipType clipType, List<List<ClipperLib.IntPoint>> solution, ClipperLib.PolyFillType subjFillType, ClipperLib.PolyFillType clipFillType)
			{
				if (this.m_ExecuteLocked)
				{
					return false;
				}
				if (this.m_HasOpenPaths)
				{
					throw new ClipperLib.ClipperException("Error: PolyTree struct is needed for open path clipping.");
				}
				this.m_ExecuteLocked = true;
				solution.Clear();
				this.m_SubjFillType = subjFillType;
				this.m_ClipFillType = clipFillType;
				this.m_ClipType = clipType;
				this.m_UsingPolyTree = false;
				bool succeeded;
				try
				{
					succeeded = this.ExecuteInternal();
					if (succeeded)
					{
						this.BuildResult(solution);
					}
				}
				finally
				{
					this.DisposeAllPolyPts();
					this.m_ExecuteLocked = false;
				}
				return succeeded;
			}

			// Token: 0x060004AB RID: 1195 RVA: 0x0001AEB0 File Offset: 0x000190B0
			public bool Execute(ClipperLib.ClipType clipType, ClipperLib.PolyTree polytree, ClipperLib.PolyFillType subjFillType, ClipperLib.PolyFillType clipFillType)
			{
				if (this.m_ExecuteLocked)
				{
					return false;
				}
				this.m_ExecuteLocked = true;
				this.m_SubjFillType = subjFillType;
				this.m_ClipFillType = clipFillType;
				this.m_ClipType = clipType;
				this.m_UsingPolyTree = true;
				bool succeeded;
				try
				{
					succeeded = this.ExecuteInternal();
					if (succeeded)
					{
						this.BuildResult2(polytree);
					}
				}
				finally
				{
					this.DisposeAllPolyPts();
					this.m_ExecuteLocked = false;
				}
				return succeeded;
			}

			// Token: 0x060004AC RID: 1196 RVA: 0x0001AF20 File Offset: 0x00019120
			internal void FixHoleLinkage(ClipperLib.OutRec outRec)
			{
				if (outRec.FirstLeft == null || (outRec.IsHole != outRec.FirstLeft.IsHole && outRec.FirstLeft.Pts != null))
				{
					return;
				}
				ClipperLib.OutRec orfl = outRec.FirstLeft;
				while (orfl != null && (orfl.IsHole == outRec.IsHole || orfl.Pts == null))
				{
					orfl = orfl.FirstLeft;
				}
				outRec.FirstLeft = orfl;
			}

			// Token: 0x060004AD RID: 1197 RVA: 0x0001AF88 File Offset: 0x00019188
			private bool ExecuteInternal()
			{
				bool flag;
				try
				{
					this.Reset();
					this.m_SortedEdges = null;
					this.m_Maxima = null;
					long botY;
					if (!base.PopScanbeam(out botY))
					{
						flag = false;
					}
					else
					{
						this.InsertLocalMinimaIntoAEL(botY);
						long topY;
						while (base.PopScanbeam(out topY) || base.LocalMinimaPending())
						{
							this.ProcessHorizontals();
							this.m_GhostJoins.Clear();
							if (!this.ProcessIntersections(topY))
							{
								return false;
							}
							this.ProcessEdgesAtTopOfScanbeam(topY);
							botY = topY;
							this.InsertLocalMinimaIntoAEL(botY);
						}
						foreach (ClipperLib.OutRec outRec in this.m_PolyOuts)
						{
							if (outRec.Pts != null && !outRec.IsOpen && (outRec.IsHole ^ this.ReverseSolution) == this.Area(outRec) > 0.0)
							{
								this.ReversePolyPtLinks(outRec.Pts);
							}
						}
						this.JoinCommonEdges();
						foreach (ClipperLib.OutRec outRec2 in this.m_PolyOuts)
						{
							if (outRec2.Pts != null)
							{
								if (outRec2.IsOpen)
								{
									this.FixupOutPolyline(outRec2);
								}
								else
								{
									this.FixupOutPolygon(outRec2);
								}
							}
						}
						if (this.StrictlySimple)
						{
							this.DoSimplePolygons();
						}
						flag = true;
					}
				}
				finally
				{
					this.m_Joins.Clear();
					this.m_GhostJoins.Clear();
				}
				return flag;
			}

			// Token: 0x060004AE RID: 1198 RVA: 0x0001B148 File Offset: 0x00019348
			private void DisposeAllPolyPts()
			{
				for (int i = 0; i < this.m_PolyOuts.Count; i++)
				{
					base.DisposeOutRec(i);
				}
				this.m_PolyOuts.Clear();
			}

			// Token: 0x060004AF RID: 1199 RVA: 0x0001B180 File Offset: 0x00019380
			private void AddJoin(ClipperLib.OutPt Op1, ClipperLib.OutPt Op2, ClipperLib.IntPoint OffPt)
			{
				ClipperLib.Join i = new ClipperLib.Join();
				i.OutPt1 = Op1;
				i.OutPt2 = Op2;
				i.OffPt = OffPt;
				this.m_Joins.Add(i);
			}

			// Token: 0x060004B0 RID: 1200 RVA: 0x0001B1B4 File Offset: 0x000193B4
			private void AddGhostJoin(ClipperLib.OutPt Op, ClipperLib.IntPoint OffPt)
			{
				ClipperLib.Join i = new ClipperLib.Join();
				i.OutPt1 = Op;
				i.OffPt = OffPt;
				this.m_GhostJoins.Add(i);
			}

			// Token: 0x060004B1 RID: 1201 RVA: 0x0001B1E4 File Offset: 0x000193E4
			private void InsertLocalMinimaIntoAEL(long botY)
			{
				ClipperLib.LocalMinima lm;
				while (base.PopLocalMinima(botY, out lm))
				{
					ClipperLib.TEdge lb = lm.LeftBound;
					ClipperLib.TEdge rb = lm.RightBound;
					ClipperLib.OutPt Op = null;
					if (lb == null)
					{
						this.InsertEdgeIntoAEL(rb, null);
						this.SetWindingCount(rb);
						if (this.IsContributing(rb))
						{
							Op = this.AddOutPt(rb, rb.Bot);
						}
					}
					else if (rb == null)
					{
						this.InsertEdgeIntoAEL(lb, null);
						this.SetWindingCount(lb);
						if (this.IsContributing(lb))
						{
							Op = this.AddOutPt(lb, lb.Bot);
						}
						base.InsertScanbeam(lb.Top.Y);
					}
					else
					{
						this.InsertEdgeIntoAEL(lb, null);
						this.InsertEdgeIntoAEL(rb, lb);
						this.SetWindingCount(lb);
						rb.WindCnt = lb.WindCnt;
						rb.WindCnt2 = lb.WindCnt2;
						if (this.IsContributing(lb))
						{
							Op = this.AddLocalMinPoly(lb, rb, lb.Bot);
						}
						base.InsertScanbeam(lb.Top.Y);
					}
					if (rb != null)
					{
						if (ClipperLib.ClipperBase.IsHorizontal(rb))
						{
							if (rb.NextInLML != null)
							{
								base.InsertScanbeam(rb.NextInLML.Top.Y);
							}
							this.AddEdgeToSEL(rb);
						}
						else
						{
							base.InsertScanbeam(rb.Top.Y);
						}
					}
					if (lb != null && rb != null)
					{
						if (Op != null && ClipperLib.ClipperBase.IsHorizontal(rb) && this.m_GhostJoins.Count > 0 && rb.WindDelta != 0)
						{
							for (int i = 0; i < this.m_GhostJoins.Count; i++)
							{
								ClipperLib.Join j = this.m_GhostJoins[i];
								if (this.HorzSegmentsOverlap(j.OutPt1.Pt.X, j.OffPt.X, rb.Bot.X, rb.Top.X))
								{
									this.AddJoin(j.OutPt1, Op, j.OffPt);
								}
							}
						}
						if (lb.OutIdx >= 0 && lb.PrevInAEL != null && lb.PrevInAEL.Curr.X == lb.Bot.X && lb.PrevInAEL.OutIdx >= 0 && ClipperLib.ClipperBase.SlopesEqual(lb.PrevInAEL.Curr, lb.PrevInAEL.Top, lb.Curr, lb.Top, this.m_UseFullRange) && lb.WindDelta != 0 && lb.PrevInAEL.WindDelta != 0)
						{
							ClipperLib.OutPt Op2 = this.AddOutPt(lb.PrevInAEL, lb.Bot);
							this.AddJoin(Op, Op2, lb.Top);
						}
						if (lb.NextInAEL != rb)
						{
							if (rb.OutIdx >= 0 && rb.PrevInAEL.OutIdx >= 0 && ClipperLib.ClipperBase.SlopesEqual(rb.PrevInAEL.Curr, rb.PrevInAEL.Top, rb.Curr, rb.Top, this.m_UseFullRange) && rb.WindDelta != 0 && rb.PrevInAEL.WindDelta != 0)
							{
								ClipperLib.OutPt Op3 = this.AddOutPt(rb.PrevInAEL, rb.Bot);
								this.AddJoin(Op, Op3, rb.Top);
							}
							ClipperLib.TEdge e = lb.NextInAEL;
							if (e != null)
							{
								while (e != rb)
								{
									this.IntersectEdges(rb, e, lb.Curr);
									e = e.NextInAEL;
								}
							}
						}
					}
				}
			}

			// Token: 0x060004B2 RID: 1202 RVA: 0x0001B52C File Offset: 0x0001972C
			private void InsertEdgeIntoAEL(ClipperLib.TEdge edge, ClipperLib.TEdge startEdge)
			{
				if (this.m_ActiveEdges == null)
				{
					edge.PrevInAEL = null;
					edge.NextInAEL = null;
					this.m_ActiveEdges = edge;
					return;
				}
				if (startEdge == null && this.E2InsertsBeforeE1(this.m_ActiveEdges, edge))
				{
					edge.PrevInAEL = null;
					edge.NextInAEL = this.m_ActiveEdges;
					this.m_ActiveEdges.PrevInAEL = edge;
					this.m_ActiveEdges = edge;
					return;
				}
				if (startEdge == null)
				{
					startEdge = this.m_ActiveEdges;
				}
				while (startEdge.NextInAEL != null && !this.E2InsertsBeforeE1(startEdge.NextInAEL, edge))
				{
					startEdge = startEdge.NextInAEL;
				}
				edge.NextInAEL = startEdge.NextInAEL;
				if (startEdge.NextInAEL != null)
				{
					startEdge.NextInAEL.PrevInAEL = edge;
				}
				edge.PrevInAEL = startEdge;
				startEdge.NextInAEL = edge;
			}

			// Token: 0x060004B3 RID: 1203 RVA: 0x0001B5EC File Offset: 0x000197EC
			private bool E2InsertsBeforeE1(ClipperLib.TEdge e1, ClipperLib.TEdge e2)
			{
				if (e2.Curr.X != e1.Curr.X)
				{
					return e2.Curr.X < e1.Curr.X;
				}
				if (e2.Top.Y > e1.Top.Y)
				{
					return e2.Top.X < ClipperLib.Clipper.TopX(e1, e2.Top.Y);
				}
				return e1.Top.X > ClipperLib.Clipper.TopX(e2, e1.Top.Y);
			}

			// Token: 0x060004B4 RID: 1204 RVA: 0x0001B67F File Offset: 0x0001987F
			private bool IsEvenOddFillType(ClipperLib.TEdge edge)
			{
				if (edge.PolyTyp == ClipperLib.PolyType.ptSubject)
				{
					return this.m_SubjFillType == ClipperLib.PolyFillType.pftEvenOdd;
				}
				return this.m_ClipFillType == ClipperLib.PolyFillType.pftEvenOdd;
			}

			// Token: 0x060004B5 RID: 1205 RVA: 0x0001B69C File Offset: 0x0001989C
			private bool IsEvenOddAltFillType(ClipperLib.TEdge edge)
			{
				if (edge.PolyTyp == ClipperLib.PolyType.ptSubject)
				{
					return this.m_ClipFillType == ClipperLib.PolyFillType.pftEvenOdd;
				}
				return this.m_SubjFillType == ClipperLib.PolyFillType.pftEvenOdd;
			}

			// Token: 0x060004B6 RID: 1206 RVA: 0x0001B6BC File Offset: 0x000198BC
			private bool IsContributing(ClipperLib.TEdge edge)
			{
				ClipperLib.PolyFillType pft;
				ClipperLib.PolyFillType pft2;
				if (edge.PolyTyp == ClipperLib.PolyType.ptSubject)
				{
					pft = this.m_SubjFillType;
					pft2 = this.m_ClipFillType;
				}
				else
				{
					pft = this.m_ClipFillType;
					pft2 = this.m_SubjFillType;
				}
				switch (pft)
				{
				case ClipperLib.PolyFillType.pftEvenOdd:
					if (edge.WindDelta == 0 && edge.WindCnt != 1)
					{
						return false;
					}
					break;
				case ClipperLib.PolyFillType.pftNonZero:
					if (Math.Abs(edge.WindCnt) != 1)
					{
						return false;
					}
					break;
				case ClipperLib.PolyFillType.pftPositive:
					if (edge.WindCnt != 1)
					{
						return false;
					}
					break;
				default:
					if (edge.WindCnt != -1)
					{
						return false;
					}
					break;
				}
				switch (this.m_ClipType)
				{
				case ClipperLib.ClipType.ctIntersection:
					if (pft2 <= ClipperLib.PolyFillType.pftNonZero)
					{
						return edge.WindCnt2 != 0;
					}
					if (pft2 != ClipperLib.PolyFillType.pftPositive)
					{
						return edge.WindCnt2 < 0;
					}
					return edge.WindCnt2 > 0;
				case ClipperLib.ClipType.ctUnion:
					if (pft2 <= ClipperLib.PolyFillType.pftNonZero)
					{
						return edge.WindCnt2 == 0;
					}
					if (pft2 != ClipperLib.PolyFillType.pftPositive)
					{
						return edge.WindCnt2 >= 0;
					}
					return edge.WindCnt2 <= 0;
				case ClipperLib.ClipType.ctDifference:
					if (edge.PolyTyp == ClipperLib.PolyType.ptSubject)
					{
						if (pft2 <= ClipperLib.PolyFillType.pftNonZero)
						{
							return edge.WindCnt2 == 0;
						}
						if (pft2 != ClipperLib.PolyFillType.pftPositive)
						{
							return edge.WindCnt2 >= 0;
						}
						return edge.WindCnt2 <= 0;
					}
					else
					{
						if (pft2 <= ClipperLib.PolyFillType.pftNonZero)
						{
							return edge.WindCnt2 != 0;
						}
						if (pft2 != ClipperLib.PolyFillType.pftPositive)
						{
							return edge.WindCnt2 < 0;
						}
						return edge.WindCnt2 > 0;
					}
					break;
				case ClipperLib.ClipType.ctXor:
					if (edge.WindDelta != 0)
					{
						return true;
					}
					if (pft2 <= ClipperLib.PolyFillType.pftNonZero)
					{
						return edge.WindCnt2 == 0;
					}
					if (pft2 != ClipperLib.PolyFillType.pftPositive)
					{
						return edge.WindCnt2 >= 0;
					}
					return edge.WindCnt2 <= 0;
				default:
					return true;
				}
			}

			// Token: 0x060004B7 RID: 1207 RVA: 0x0001B84C File Offset: 0x00019A4C
			private void SetWindingCount(ClipperLib.TEdge edge)
			{
				ClipperLib.TEdge e = edge.PrevInAEL;
				while (e != null && (e.PolyTyp != edge.PolyTyp || e.WindDelta == 0))
				{
					e = e.PrevInAEL;
				}
				if (e == null)
				{
					ClipperLib.PolyFillType pft = ((edge.PolyTyp == ClipperLib.PolyType.ptSubject) ? this.m_SubjFillType : this.m_ClipFillType);
					if (edge.WindDelta == 0)
					{
						edge.WindCnt = ((pft == ClipperLib.PolyFillType.pftNegative) ? (-1) : 1);
					}
					else
					{
						edge.WindCnt = edge.WindDelta;
					}
					edge.WindCnt2 = 0;
					e = this.m_ActiveEdges;
				}
				else if (edge.WindDelta == 0 && this.m_ClipType != ClipperLib.ClipType.ctUnion)
				{
					edge.WindCnt = 1;
					edge.WindCnt2 = e.WindCnt2;
					e = e.NextInAEL;
				}
				else if (this.IsEvenOddFillType(edge))
				{
					if (edge.WindDelta == 0)
					{
						bool Inside = true;
						for (ClipperLib.TEdge e2 = e.PrevInAEL; e2 != null; e2 = e2.PrevInAEL)
						{
							if (e2.PolyTyp == e.PolyTyp && e2.WindDelta != 0)
							{
								Inside = !Inside;
							}
						}
						edge.WindCnt = (Inside ? 0 : 1);
					}
					else
					{
						edge.WindCnt = edge.WindDelta;
					}
					edge.WindCnt2 = e.WindCnt2;
					e = e.NextInAEL;
				}
				else
				{
					if (e.WindCnt * e.WindDelta < 0)
					{
						if (Math.Abs(e.WindCnt) > 1)
						{
							if (e.WindDelta * edge.WindDelta < 0)
							{
								edge.WindCnt = e.WindCnt;
							}
							else
							{
								edge.WindCnt = e.WindCnt + edge.WindDelta;
							}
						}
						else
						{
							edge.WindCnt = ((edge.WindDelta == 0) ? 1 : edge.WindDelta);
						}
					}
					else if (edge.WindDelta == 0)
					{
						edge.WindCnt = ((e.WindCnt < 0) ? (e.WindCnt - 1) : (e.WindCnt + 1));
					}
					else if (e.WindDelta * edge.WindDelta < 0)
					{
						edge.WindCnt = e.WindCnt;
					}
					else
					{
						edge.WindCnt = e.WindCnt + edge.WindDelta;
					}
					edge.WindCnt2 = e.WindCnt2;
					e = e.NextInAEL;
				}
				if (this.IsEvenOddAltFillType(edge))
				{
					while (e != edge)
					{
						if (e.WindDelta != 0)
						{
							edge.WindCnt2 = ((edge.WindCnt2 == 0) ? 1 : 0);
						}
						e = e.NextInAEL;
					}
					return;
				}
				while (e != edge)
				{
					edge.WindCnt2 += e.WindDelta;
					e = e.NextInAEL;
				}
			}

			// Token: 0x060004B8 RID: 1208 RVA: 0x0001BAA4 File Offset: 0x00019CA4
			private void AddEdgeToSEL(ClipperLib.TEdge edge)
			{
				if (this.m_SortedEdges == null)
				{
					this.m_SortedEdges = edge;
					edge.PrevInSEL = null;
					edge.NextInSEL = null;
					return;
				}
				edge.NextInSEL = this.m_SortedEdges;
				edge.PrevInSEL = null;
				this.m_SortedEdges.PrevInSEL = edge;
				this.m_SortedEdges = edge;
			}

			// Token: 0x060004B9 RID: 1209 RVA: 0x0001BAF8 File Offset: 0x00019CF8
			internal bool PopEdgeFromSEL(out ClipperLib.TEdge e)
			{
				e = this.m_SortedEdges;
				if (e == null)
				{
					return false;
				}
				ClipperLib.TEdge tedge = e;
				this.m_SortedEdges = e.NextInSEL;
				if (this.m_SortedEdges != null)
				{
					this.m_SortedEdges.PrevInSEL = null;
				}
				tedge.NextInSEL = null;
				tedge.PrevInSEL = null;
				return true;
			}

			// Token: 0x060004BA RID: 1210 RVA: 0x0001BB44 File Offset: 0x00019D44
			private void CopyAELToSEL()
			{
				ClipperLib.TEdge e = this.m_ActiveEdges;
				this.m_SortedEdges = e;
				while (e != null)
				{
					e.PrevInSEL = e.PrevInAEL;
					e.NextInSEL = e.NextInAEL;
					e = e.NextInAEL;
				}
			}

			// Token: 0x060004BB RID: 1211 RVA: 0x0001BB84 File Offset: 0x00019D84
			private void SwapPositionsInSEL(ClipperLib.TEdge edge1, ClipperLib.TEdge edge2)
			{
				if (edge1.NextInSEL == null && edge1.PrevInSEL == null)
				{
					return;
				}
				if (edge2.NextInSEL == null && edge2.PrevInSEL == null)
				{
					return;
				}
				if (edge1.NextInSEL == edge2)
				{
					ClipperLib.TEdge next = edge2.NextInSEL;
					if (next != null)
					{
						next.PrevInSEL = edge1;
					}
					ClipperLib.TEdge prev = edge1.PrevInSEL;
					if (prev != null)
					{
						prev.NextInSEL = edge2;
					}
					edge2.PrevInSEL = prev;
					edge2.NextInSEL = edge1;
					edge1.PrevInSEL = edge2;
					edge1.NextInSEL = next;
				}
				else if (edge2.NextInSEL == edge1)
				{
					ClipperLib.TEdge next2 = edge1.NextInSEL;
					if (next2 != null)
					{
						next2.PrevInSEL = edge2;
					}
					ClipperLib.TEdge prev2 = edge2.PrevInSEL;
					if (prev2 != null)
					{
						prev2.NextInSEL = edge1;
					}
					edge1.PrevInSEL = prev2;
					edge1.NextInSEL = edge2;
					edge2.PrevInSEL = edge1;
					edge2.NextInSEL = next2;
				}
				else
				{
					ClipperLib.TEdge next3 = edge1.NextInSEL;
					ClipperLib.TEdge prev3 = edge1.PrevInSEL;
					edge1.NextInSEL = edge2.NextInSEL;
					if (edge1.NextInSEL != null)
					{
						edge1.NextInSEL.PrevInSEL = edge1;
					}
					edge1.PrevInSEL = edge2.PrevInSEL;
					if (edge1.PrevInSEL != null)
					{
						edge1.PrevInSEL.NextInSEL = edge1;
					}
					edge2.NextInSEL = next3;
					if (edge2.NextInSEL != null)
					{
						edge2.NextInSEL.PrevInSEL = edge2;
					}
					edge2.PrevInSEL = prev3;
					if (edge2.PrevInSEL != null)
					{
						edge2.PrevInSEL.NextInSEL = edge2;
					}
				}
				if (edge1.PrevInSEL == null)
				{
					this.m_SortedEdges = edge1;
					return;
				}
				if (edge2.PrevInSEL == null)
				{
					this.m_SortedEdges = edge2;
				}
			}

			// Token: 0x060004BC RID: 1212 RVA: 0x0001BCF4 File Offset: 0x00019EF4
			private void AddLocalMaxPoly(ClipperLib.TEdge e1, ClipperLib.TEdge e2, ClipperLib.IntPoint pt)
			{
				this.AddOutPt(e1, pt);
				if (e2.WindDelta == 0)
				{
					this.AddOutPt(e2, pt);
				}
				if (e1.OutIdx == e2.OutIdx)
				{
					e1.OutIdx = -1;
					e2.OutIdx = -1;
					return;
				}
				if (e1.OutIdx < e2.OutIdx)
				{
					this.AppendPolygon(e1, e2);
					return;
				}
				this.AppendPolygon(e2, e1);
			}

			// Token: 0x060004BD RID: 1213 RVA: 0x0001BD58 File Offset: 0x00019F58
			private ClipperLib.OutPt AddLocalMinPoly(ClipperLib.TEdge e1, ClipperLib.TEdge e2, ClipperLib.IntPoint pt)
			{
				ClipperLib.OutPt result;
				ClipperLib.TEdge e3;
				ClipperLib.TEdge prevE;
				if (ClipperLib.ClipperBase.IsHorizontal(e2) || e1.Dx > e2.Dx)
				{
					result = this.AddOutPt(e1, pt);
					e2.OutIdx = e1.OutIdx;
					e1.Side = ClipperLib.EdgeSide.esLeft;
					e2.Side = ClipperLib.EdgeSide.esRight;
					e3 = e1;
					if (e3.PrevInAEL == e2)
					{
						prevE = e2.PrevInAEL;
					}
					else
					{
						prevE = e3.PrevInAEL;
					}
				}
				else
				{
					result = this.AddOutPt(e2, pt);
					e1.OutIdx = e2.OutIdx;
					e1.Side = ClipperLib.EdgeSide.esRight;
					e2.Side = ClipperLib.EdgeSide.esLeft;
					e3 = e2;
					if (e3.PrevInAEL == e1)
					{
						prevE = e1.PrevInAEL;
					}
					else
					{
						prevE = e3.PrevInAEL;
					}
				}
				if (prevE != null && prevE.OutIdx >= 0 && prevE.Top.Y < pt.Y && e3.Top.Y < pt.Y)
				{
					long xPrev = ClipperLib.Clipper.TopX(prevE, pt.Y);
					long xE = ClipperLib.Clipper.TopX(e3, pt.Y);
					if (xPrev == xE && e3.WindDelta != 0 && prevE.WindDelta != 0 && ClipperLib.ClipperBase.SlopesEqual(new ClipperLib.IntPoint(xPrev, pt.Y), prevE.Top, new ClipperLib.IntPoint(xE, pt.Y), e3.Top, this.m_UseFullRange))
					{
						ClipperLib.OutPt outPt = this.AddOutPt(prevE, pt);
						this.AddJoin(result, outPt, e3.Top);
					}
				}
				return result;
			}

			// Token: 0x060004BE RID: 1214 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
			private ClipperLib.OutPt AddOutPt(ClipperLib.TEdge e, ClipperLib.IntPoint pt)
			{
				if (e.OutIdx < 0)
				{
					ClipperLib.OutRec outRec = base.CreateOutRec();
					outRec.IsOpen = e.WindDelta == 0;
					ClipperLib.OutPt newOp = new ClipperLib.OutPt();
					outRec.Pts = newOp;
					newOp.Idx = outRec.Idx;
					newOp.Pt = pt;
					newOp.Next = newOp;
					newOp.Prev = newOp;
					if (!outRec.IsOpen)
					{
						this.SetHoleState(e, outRec);
					}
					e.OutIdx = outRec.Idx;
					return newOp;
				}
				ClipperLib.OutRec outRec2 = this.m_PolyOuts[e.OutIdx];
				ClipperLib.OutPt op = outRec2.Pts;
				bool ToFront = e.Side == ClipperLib.EdgeSide.esLeft;
				if (ToFront && pt == op.Pt)
				{
					return op;
				}
				if (!ToFront && pt == op.Prev.Pt)
				{
					return op.Prev;
				}
				ClipperLib.OutPt newOp2 = new ClipperLib.OutPt();
				newOp2.Idx = outRec2.Idx;
				newOp2.Pt = pt;
				newOp2.Next = op;
				newOp2.Prev = op.Prev;
				newOp2.Prev.Next = newOp2;
				op.Prev = newOp2;
				if (ToFront)
				{
					outRec2.Pts = newOp2;
				}
				return newOp2;
			}

			// Token: 0x060004BF RID: 1215 RVA: 0x0001BFD4 File Offset: 0x0001A1D4
			private ClipperLib.OutPt GetLastOutPt(ClipperLib.TEdge e)
			{
				ClipperLib.OutRec outRec = this.m_PolyOuts[e.OutIdx];
				if (e.Side == ClipperLib.EdgeSide.esLeft)
				{
					return outRec.Pts;
				}
				return outRec.Pts.Prev;
			}

			// Token: 0x060004C0 RID: 1216 RVA: 0x0001C010 File Offset: 0x0001A210
			internal void SwapPoints(ref ClipperLib.IntPoint pt1, ref ClipperLib.IntPoint pt2)
			{
				ClipperLib.IntPoint tmp = new ClipperLib.IntPoint(pt1);
				pt1 = pt2;
				pt2 = tmp;
			}

			// Token: 0x060004C1 RID: 1217 RVA: 0x0001C03D File Offset: 0x0001A23D
			private bool HorzSegmentsOverlap(long seg1a, long seg1b, long seg2a, long seg2b)
			{
				if (seg1a > seg1b)
				{
					base.Swap(ref seg1a, ref seg1b);
				}
				if (seg2a > seg2b)
				{
					base.Swap(ref seg2a, ref seg2b);
				}
				return seg1a < seg2b && seg2a < seg1b;
			}

			// Token: 0x060004C2 RID: 1218 RVA: 0x0001C068 File Offset: 0x0001A268
			private void SetHoleState(ClipperLib.TEdge e, ClipperLib.OutRec outRec)
			{
				ClipperLib.TEdge e2 = e.PrevInAEL;
				ClipperLib.TEdge eTmp = null;
				while (e2 != null)
				{
					if (e2.OutIdx >= 0 && e2.WindDelta != 0)
					{
						if (eTmp == null)
						{
							eTmp = e2;
						}
						else if (eTmp.OutIdx == e2.OutIdx)
						{
							eTmp = null;
						}
					}
					e2 = e2.PrevInAEL;
				}
				if (eTmp == null)
				{
					outRec.FirstLeft = null;
					outRec.IsHole = false;
					return;
				}
				outRec.FirstLeft = this.m_PolyOuts[eTmp.OutIdx];
				outRec.IsHole = !outRec.FirstLeft.IsHole;
			}

			// Token: 0x060004C3 RID: 1219 RVA: 0x0001C0EF File Offset: 0x0001A2EF
			private double GetDx(ClipperLib.IntPoint pt1, ClipperLib.IntPoint pt2)
			{
				if (pt1.Y == pt2.Y)
				{
					return -3.4E+38;
				}
				return (double)(pt2.X - pt1.X) / (double)(pt2.Y - pt1.Y);
			}

			// Token: 0x060004C4 RID: 1220 RVA: 0x0001C128 File Offset: 0x0001A328
			private bool FirstIsBottomPt(ClipperLib.OutPt btmPt1, ClipperLib.OutPt btmPt2)
			{
				ClipperLib.OutPt p = btmPt1.Prev;
				while (p.Pt == btmPt1.Pt && p != btmPt1)
				{
					p = p.Prev;
				}
				double dx1p = Math.Abs(this.GetDx(btmPt1.Pt, p.Pt));
				p = btmPt1.Next;
				while (p.Pt == btmPt1.Pt && p != btmPt1)
				{
					p = p.Next;
				}
				double dx1n = Math.Abs(this.GetDx(btmPt1.Pt, p.Pt));
				p = btmPt2.Prev;
				while (p.Pt == btmPt2.Pt && p != btmPt2)
				{
					p = p.Prev;
				}
				double dx2p = Math.Abs(this.GetDx(btmPt2.Pt, p.Pt));
				p = btmPt2.Next;
				while (p.Pt == btmPt2.Pt && p != btmPt2)
				{
					p = p.Next;
				}
				double dx2n = Math.Abs(this.GetDx(btmPt2.Pt, p.Pt));
				if (Math.Max(dx1p, dx1n) == Math.Max(dx2p, dx2n) && Math.Min(dx1p, dx1n) == Math.Min(dx2p, dx2n))
				{
					return this.Area(btmPt1) > 0.0;
				}
				return (dx1p >= dx2p && dx1p >= dx2n) || (dx1n >= dx2p && dx1n >= dx2n);
			}

			// Token: 0x060004C5 RID: 1221 RVA: 0x0001C280 File Offset: 0x0001A480
			private ClipperLib.OutPt GetBottomPt(ClipperLib.OutPt pp)
			{
				ClipperLib.OutPt dups = null;
				ClipperLib.OutPt p;
				for (p = pp.Next; p != pp; p = p.Next)
				{
					if (p.Pt.Y > pp.Pt.Y)
					{
						pp = p;
						dups = null;
					}
					else if (p.Pt.Y == pp.Pt.Y && p.Pt.X <= pp.Pt.X)
					{
						if (p.Pt.X < pp.Pt.X)
						{
							dups = null;
							pp = p;
						}
						else if (p.Next != pp && p.Prev != pp)
						{
							dups = p;
						}
					}
				}
				if (dups != null)
				{
					while (dups != p)
					{
						if (!this.FirstIsBottomPt(p, dups))
						{
							pp = dups;
						}
						dups = dups.Next;
						while (dups.Pt != pp.Pt)
						{
							dups = dups.Next;
						}
					}
				}
				return pp;
			}

			// Token: 0x060004C6 RID: 1222 RVA: 0x0001C368 File Offset: 0x0001A568
			private ClipperLib.OutRec GetLowermostRec(ClipperLib.OutRec outRec1, ClipperLib.OutRec outRec2)
			{
				if (outRec1.BottomPt == null)
				{
					outRec1.BottomPt = this.GetBottomPt(outRec1.Pts);
				}
				if (outRec2.BottomPt == null)
				{
					outRec2.BottomPt = this.GetBottomPt(outRec2.Pts);
				}
				ClipperLib.OutPt bPt = outRec1.BottomPt;
				ClipperLib.OutPt bPt2 = outRec2.BottomPt;
				if (bPt.Pt.Y > bPt2.Pt.Y)
				{
					return outRec1;
				}
				if (bPt.Pt.Y < bPt2.Pt.Y)
				{
					return outRec2;
				}
				if (bPt.Pt.X < bPt2.Pt.X)
				{
					return outRec1;
				}
				if (bPt.Pt.X > bPt2.Pt.X)
				{
					return outRec2;
				}
				if (bPt.Next == bPt)
				{
					return outRec2;
				}
				if (bPt2.Next == bPt2)
				{
					return outRec1;
				}
				if (this.FirstIsBottomPt(bPt, bPt2))
				{
					return outRec1;
				}
				return outRec2;
			}

			// Token: 0x060004C7 RID: 1223 RVA: 0x0001C442 File Offset: 0x0001A642
			private bool OutRec1RightOfOutRec2(ClipperLib.OutRec outRec1, ClipperLib.OutRec outRec2)
			{
				for (;;)
				{
					outRec1 = outRec1.FirstLeft;
					if (outRec1 == outRec2)
					{
						break;
					}
					if (outRec1 == null)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060004C8 RID: 1224 RVA: 0x0001C458 File Offset: 0x0001A658
			private ClipperLib.OutRec GetOutRec(int idx)
			{
				ClipperLib.OutRec outrec;
				for (outrec = this.m_PolyOuts[idx]; outrec != this.m_PolyOuts[outrec.Idx]; outrec = this.m_PolyOuts[outrec.Idx])
				{
				}
				return outrec;
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x0001C49C File Offset: 0x0001A69C
			private void AppendPolygon(ClipperLib.TEdge e1, ClipperLib.TEdge e2)
			{
				ClipperLib.OutRec outRec = this.m_PolyOuts[e1.OutIdx];
				ClipperLib.OutRec outRec2 = this.m_PolyOuts[e2.OutIdx];
				ClipperLib.OutRec holeStateRec;
				if (this.OutRec1RightOfOutRec2(outRec, outRec2))
				{
					holeStateRec = outRec2;
				}
				else if (this.OutRec1RightOfOutRec2(outRec2, outRec))
				{
					holeStateRec = outRec;
				}
				else
				{
					holeStateRec = this.GetLowermostRec(outRec, outRec2);
				}
				ClipperLib.OutPt p1_lft = outRec.Pts;
				ClipperLib.OutPt p1_rt = p1_lft.Prev;
				ClipperLib.OutPt p2_lft = outRec2.Pts;
				ClipperLib.OutPt p2_rt = p2_lft.Prev;
				if (e1.Side == ClipperLib.EdgeSide.esLeft)
				{
					if (e2.Side == ClipperLib.EdgeSide.esLeft)
					{
						this.ReversePolyPtLinks(p2_lft);
						p2_lft.Next = p1_lft;
						p1_lft.Prev = p2_lft;
						p1_rt.Next = p2_rt;
						p2_rt.Prev = p1_rt;
						outRec.Pts = p2_rt;
					}
					else
					{
						p2_rt.Next = p1_lft;
						p1_lft.Prev = p2_rt;
						p2_lft.Prev = p1_rt;
						p1_rt.Next = p2_lft;
						outRec.Pts = p2_lft;
					}
				}
				else if (e2.Side == ClipperLib.EdgeSide.esRight)
				{
					this.ReversePolyPtLinks(p2_lft);
					p1_rt.Next = p2_rt;
					p2_rt.Prev = p1_rt;
					p2_lft.Next = p1_lft;
					p1_lft.Prev = p2_lft;
				}
				else
				{
					p1_rt.Next = p2_lft;
					p2_lft.Prev = p1_rt;
					p1_lft.Prev = p2_rt;
					p2_rt.Next = p1_lft;
				}
				outRec.BottomPt = null;
				if (holeStateRec == outRec2)
				{
					if (outRec2.FirstLeft != outRec)
					{
						outRec.FirstLeft = outRec2.FirstLeft;
					}
					outRec.IsHole = outRec2.IsHole;
				}
				outRec2.Pts = null;
				outRec2.BottomPt = null;
				outRec2.FirstLeft = outRec;
				int OKIdx = e1.OutIdx;
				int ObsoleteIdx = e2.OutIdx;
				e1.OutIdx = -1;
				e2.OutIdx = -1;
				for (ClipperLib.TEdge e3 = this.m_ActiveEdges; e3 != null; e3 = e3.NextInAEL)
				{
					if (e3.OutIdx == ObsoleteIdx)
					{
						e3.OutIdx = OKIdx;
						e3.Side = e1.Side;
						break;
					}
				}
				outRec2.Idx = outRec.Idx;
			}

			// Token: 0x060004CA RID: 1226 RVA: 0x0001C684 File Offset: 0x0001A884
			private void ReversePolyPtLinks(ClipperLib.OutPt pp)
			{
				if (pp == null)
				{
					return;
				}
				ClipperLib.OutPt pp2 = pp;
				do
				{
					ClipperLib.OutPt pp3 = pp2.Next;
					pp2.Next = pp2.Prev;
					pp2.Prev = pp3;
					pp2 = pp3;
				}
				while (pp2 != pp);
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x0001C6B8 File Offset: 0x0001A8B8
			private static void SwapSides(ClipperLib.TEdge edge1, ClipperLib.TEdge edge2)
			{
				ClipperLib.EdgeSide side = edge1.Side;
				edge1.Side = edge2.Side;
				edge2.Side = side;
			}

			// Token: 0x060004CC RID: 1228 RVA: 0x0001C6E0 File Offset: 0x0001A8E0
			private static void SwapPolyIndexes(ClipperLib.TEdge edge1, ClipperLib.TEdge edge2)
			{
				int outIdx = edge1.OutIdx;
				edge1.OutIdx = edge2.OutIdx;
				edge2.OutIdx = outIdx;
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x0001C708 File Offset: 0x0001A908
			private void IntersectEdges(ClipperLib.TEdge e1, ClipperLib.TEdge e2, ClipperLib.IntPoint pt)
			{
				bool e1Contributing = e1.OutIdx >= 0;
				bool e2Contributing = e2.OutIdx >= 0;
				if (e1.WindDelta == 0 || e2.WindDelta == 0)
				{
					if (e1.WindDelta == 0 && e2.WindDelta == 0)
					{
						return;
					}
					if (e1.PolyTyp == e2.PolyTyp && e1.WindDelta != e2.WindDelta && this.m_ClipType == ClipperLib.ClipType.ctUnion)
					{
						if (e1.WindDelta == 0)
						{
							if (e2Contributing)
							{
								this.AddOutPt(e1, pt);
								if (e1Contributing)
								{
									e1.OutIdx = -1;
									return;
								}
							}
						}
						else if (e1Contributing)
						{
							this.AddOutPt(e2, pt);
							if (e2Contributing)
							{
								e2.OutIdx = -1;
								return;
							}
						}
					}
					else if (e1.PolyTyp != e2.PolyTyp)
					{
						if (e1.WindDelta == 0 && Math.Abs(e2.WindCnt) == 1 && (this.m_ClipType != ClipperLib.ClipType.ctUnion || e2.WindCnt2 == 0))
						{
							this.AddOutPt(e1, pt);
							if (e1Contributing)
							{
								e1.OutIdx = -1;
								return;
							}
						}
						else if (e2.WindDelta == 0 && Math.Abs(e1.WindCnt) == 1 && (this.m_ClipType != ClipperLib.ClipType.ctUnion || e1.WindCnt2 == 0))
						{
							this.AddOutPt(e2, pt);
							if (e2Contributing)
							{
								e2.OutIdx = -1;
							}
						}
					}
					return;
				}
				else
				{
					if (e1.PolyTyp == e2.PolyTyp)
					{
						if (this.IsEvenOddFillType(e1))
						{
							int oldE1WindCnt = e1.WindCnt;
							e1.WindCnt = e2.WindCnt;
							e2.WindCnt = oldE1WindCnt;
						}
						else
						{
							if (e1.WindCnt + e2.WindDelta == 0)
							{
								e1.WindCnt = -e1.WindCnt;
							}
							else
							{
								e1.WindCnt += e2.WindDelta;
							}
							if (e2.WindCnt - e1.WindDelta == 0)
							{
								e2.WindCnt = -e2.WindCnt;
							}
							else
							{
								e2.WindCnt -= e1.WindDelta;
							}
						}
					}
					else
					{
						if (!this.IsEvenOddFillType(e2))
						{
							e1.WindCnt2 += e2.WindDelta;
						}
						else
						{
							e1.WindCnt2 = ((e1.WindCnt2 == 0) ? 1 : 0);
						}
						if (!this.IsEvenOddFillType(e1))
						{
							e2.WindCnt2 -= e1.WindDelta;
						}
						else
						{
							e2.WindCnt2 = ((e2.WindCnt2 == 0) ? 1 : 0);
						}
					}
					ClipperLib.PolyFillType e1FillType;
					ClipperLib.PolyFillType e1FillType2;
					if (e1.PolyTyp == ClipperLib.PolyType.ptSubject)
					{
						e1FillType = this.m_SubjFillType;
						e1FillType2 = this.m_ClipFillType;
					}
					else
					{
						e1FillType = this.m_ClipFillType;
						e1FillType2 = this.m_SubjFillType;
					}
					ClipperLib.PolyFillType e2FillType;
					ClipperLib.PolyFillType e2FillType2;
					if (e2.PolyTyp == ClipperLib.PolyType.ptSubject)
					{
						e2FillType = this.m_SubjFillType;
						e2FillType2 = this.m_ClipFillType;
					}
					else
					{
						e2FillType = this.m_ClipFillType;
						e2FillType2 = this.m_SubjFillType;
					}
					int e1Wc;
					if (e1FillType != ClipperLib.PolyFillType.pftPositive)
					{
						if (e1FillType != ClipperLib.PolyFillType.pftNegative)
						{
							e1Wc = Math.Abs(e1.WindCnt);
						}
						else
						{
							e1Wc = -e1.WindCnt;
						}
					}
					else
					{
						e1Wc = e1.WindCnt;
					}
					int e2Wc;
					if (e2FillType != ClipperLib.PolyFillType.pftPositive)
					{
						if (e2FillType != ClipperLib.PolyFillType.pftNegative)
						{
							e2Wc = Math.Abs(e2.WindCnt);
						}
						else
						{
							e2Wc = -e2.WindCnt;
						}
					}
					else
					{
						e2Wc = e2.WindCnt;
					}
					if (!e1Contributing || !e2Contributing)
					{
						if (e1Contributing)
						{
							if (e2Wc == 0 || e2Wc == 1)
							{
								this.AddOutPt(e1, pt);
								ClipperLib.Clipper.SwapSides(e1, e2);
								ClipperLib.Clipper.SwapPolyIndexes(e1, e2);
								return;
							}
						}
						else if (e2Contributing)
						{
							if (e1Wc == 0 || e1Wc == 1)
							{
								this.AddOutPt(e2, pt);
								ClipperLib.Clipper.SwapSides(e1, e2);
								ClipperLib.Clipper.SwapPolyIndexes(e1, e2);
								return;
							}
						}
						else if ((e1Wc == 0 || e1Wc == 1) && (e2Wc == 0 || e2Wc == 1))
						{
							long e1Wc2;
							if (e1FillType2 != ClipperLib.PolyFillType.pftPositive)
							{
								if (e1FillType2 != ClipperLib.PolyFillType.pftNegative)
								{
									e1Wc2 = (long)Math.Abs(e1.WindCnt2);
								}
								else
								{
									e1Wc2 = (long)(-(long)e1.WindCnt2);
								}
							}
							else
							{
								e1Wc2 = (long)e1.WindCnt2;
							}
							long e2Wc2;
							if (e2FillType2 != ClipperLib.PolyFillType.pftPositive)
							{
								if (e2FillType2 != ClipperLib.PolyFillType.pftNegative)
								{
									e2Wc2 = (long)Math.Abs(e2.WindCnt2);
								}
								else
								{
									e2Wc2 = (long)(-(long)e2.WindCnt2);
								}
							}
							else
							{
								e2Wc2 = (long)e2.WindCnt2;
							}
							if (e1.PolyTyp != e2.PolyTyp)
							{
								this.AddLocalMinPoly(e1, e2, pt);
								return;
							}
							if (e1Wc == 1 && e2Wc == 1)
							{
								switch (this.m_ClipType)
								{
								case ClipperLib.ClipType.ctIntersection:
									if (e1Wc2 > 0L && e2Wc2 > 0L)
									{
										this.AddLocalMinPoly(e1, e2, pt);
										return;
									}
									break;
								case ClipperLib.ClipType.ctUnion:
									if (e1Wc2 <= 0L && e2Wc2 <= 0L)
									{
										this.AddLocalMinPoly(e1, e2, pt);
										return;
									}
									break;
								case ClipperLib.ClipType.ctDifference:
									if ((e1.PolyTyp == ClipperLib.PolyType.ptClip && e1Wc2 > 0L && e2Wc2 > 0L) || (e1.PolyTyp == ClipperLib.PolyType.ptSubject && e1Wc2 <= 0L && e2Wc2 <= 0L))
									{
										this.AddLocalMinPoly(e1, e2, pt);
										return;
									}
									break;
								case ClipperLib.ClipType.ctXor:
									this.AddLocalMinPoly(e1, e2, pt);
									return;
								default:
									return;
								}
							}
							else
							{
								ClipperLib.Clipper.SwapSides(e1, e2);
							}
						}
						return;
					}
					if ((e1Wc != 0 && e1Wc != 1) || (e2Wc != 0 && e2Wc != 1) || (e1.PolyTyp != e2.PolyTyp && this.m_ClipType != ClipperLib.ClipType.ctXor))
					{
						this.AddLocalMaxPoly(e1, e2, pt);
						return;
					}
					this.AddOutPt(e1, pt);
					this.AddOutPt(e2, pt);
					ClipperLib.Clipper.SwapSides(e1, e2);
					ClipperLib.Clipper.SwapPolyIndexes(e1, e2);
					return;
				}
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x0001CBC4 File Offset: 0x0001ADC4
			private void DeleteFromSEL(ClipperLib.TEdge e)
			{
				ClipperLib.TEdge SelPrev = e.PrevInSEL;
				ClipperLib.TEdge SelNext = e.NextInSEL;
				if (SelPrev == null && SelNext == null && e != this.m_SortedEdges)
				{
					return;
				}
				if (SelPrev != null)
				{
					SelPrev.NextInSEL = SelNext;
				}
				else
				{
					this.m_SortedEdges = SelNext;
				}
				if (SelNext != null)
				{
					SelNext.PrevInSEL = SelPrev;
				}
				e.NextInSEL = null;
				e.PrevInSEL = null;
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x0001CC1C File Offset: 0x0001AE1C
			private void ProcessHorizontals()
			{
				ClipperLib.TEdge horzEdge;
				while (this.PopEdgeFromSEL(out horzEdge))
				{
					this.ProcessHorizontal(horzEdge);
				}
			}

			// Token: 0x060004D0 RID: 1232 RVA: 0x0001CC3C File Offset: 0x0001AE3C
			private void GetHorzDirection(ClipperLib.TEdge HorzEdge, out ClipperLib.Direction Dir, out long Left, out long Right)
			{
				if (HorzEdge.Bot.X < HorzEdge.Top.X)
				{
					Left = HorzEdge.Bot.X;
					Right = HorzEdge.Top.X;
					Dir = ClipperLib.Direction.dLeftToRight;
					return;
				}
				Left = HorzEdge.Top.X;
				Right = HorzEdge.Bot.X;
				Dir = ClipperLib.Direction.dRightToLeft;
			}

			// Token: 0x060004D1 RID: 1233 RVA: 0x0001CCA0 File Offset: 0x0001AEA0
			private void ProcessHorizontal(ClipperLib.TEdge horzEdge)
			{
				bool IsOpen = horzEdge.WindDelta == 0;
				ClipperLib.Direction dir;
				long horzLeft;
				long horzRight;
				this.GetHorzDirection(horzEdge, out dir, out horzLeft, out horzRight);
				ClipperLib.TEdge eLastHorz = horzEdge;
				ClipperLib.TEdge eMaxPair = null;
				while (eLastHorz.NextInLML != null && ClipperLib.ClipperBase.IsHorizontal(eLastHorz.NextInLML))
				{
					eLastHorz = eLastHorz.NextInLML;
				}
				if (eLastHorz.NextInLML == null)
				{
					eMaxPair = this.GetMaximaPair(eLastHorz);
				}
				ClipperLib.Maxima currMax = this.m_Maxima;
				if (currMax != null)
				{
					if (dir == ClipperLib.Direction.dLeftToRight)
					{
						while (currMax != null && currMax.X <= horzEdge.Bot.X)
						{
							currMax = currMax.Next;
						}
						if (currMax != null && currMax.X >= eLastHorz.Top.X)
						{
							currMax = null;
						}
					}
					else
					{
						while (currMax.Next != null && currMax.Next.X < horzEdge.Bot.X)
						{
							currMax = currMax.Next;
						}
						if (currMax.X <= eLastHorz.Top.X)
						{
							currMax = null;
						}
					}
				}
				ClipperLib.OutPt op = null;
				for (;;)
				{
					bool IsLastHorz = horzEdge == eLastHorz;
					ClipperLib.TEdge nextInAEL;
					for (ClipperLib.TEdge e = this.GetNextInAEL(horzEdge, dir); e != null; e = nextInAEL)
					{
						if (currMax != null)
						{
							if (dir == ClipperLib.Direction.dLeftToRight)
							{
								while (currMax != null)
								{
									if (currMax.X >= e.Curr.X)
									{
										break;
									}
									if (horzEdge.OutIdx >= 0 && !IsOpen)
									{
										this.AddOutPt(horzEdge, new ClipperLib.IntPoint(currMax.X, horzEdge.Bot.Y));
									}
									currMax = currMax.Next;
								}
							}
							else
							{
								while (currMax != null && currMax.X > e.Curr.X)
								{
									if (horzEdge.OutIdx >= 0 && !IsOpen)
									{
										this.AddOutPt(horzEdge, new ClipperLib.IntPoint(currMax.X, horzEdge.Bot.Y));
									}
									currMax = currMax.Prev;
								}
							}
						}
						if ((dir == ClipperLib.Direction.dLeftToRight && e.Curr.X > horzRight) || (dir == ClipperLib.Direction.dRightToLeft && e.Curr.X < horzLeft) || (e.Curr.X == horzEdge.Top.X && horzEdge.NextInLML != null && e.Dx < horzEdge.NextInLML.Dx))
						{
							break;
						}
						if (horzEdge.OutIdx >= 0 && !IsOpen)
						{
							op = this.AddOutPt(horzEdge, e.Curr);
							for (ClipperLib.TEdge eNextHorz = this.m_SortedEdges; eNextHorz != null; eNextHorz = eNextHorz.NextInSEL)
							{
								if (eNextHorz.OutIdx >= 0 && this.HorzSegmentsOverlap(horzEdge.Bot.X, horzEdge.Top.X, eNextHorz.Bot.X, eNextHorz.Top.X))
								{
									ClipperLib.OutPt op2 = this.GetLastOutPt(eNextHorz);
									this.AddJoin(op2, op, eNextHorz.Top);
								}
							}
							this.AddGhostJoin(op, horzEdge.Bot);
						}
						if (e == eMaxPair && IsLastHorz)
						{
							goto Block_28;
						}
						if (dir == ClipperLib.Direction.dLeftToRight)
						{
							ClipperLib.IntPoint Pt = new ClipperLib.IntPoint(e.Curr.X, horzEdge.Curr.Y);
							this.IntersectEdges(horzEdge, e, Pt);
						}
						else
						{
							ClipperLib.IntPoint Pt2 = new ClipperLib.IntPoint(e.Curr.X, horzEdge.Curr.Y);
							this.IntersectEdges(e, horzEdge, Pt2);
						}
						nextInAEL = this.GetNextInAEL(e, dir);
						base.SwapPositionsInAEL(horzEdge, e);
					}
					if (horzEdge.NextInLML == null || !ClipperLib.ClipperBase.IsHorizontal(horzEdge.NextInLML))
					{
						goto IL_039F;
					}
					base.UpdateEdgeIntoAEL(ref horzEdge);
					if (horzEdge.OutIdx >= 0)
					{
						this.AddOutPt(horzEdge, horzEdge.Bot);
					}
					this.GetHorzDirection(horzEdge, out dir, out horzLeft, out horzRight);
				}
				Block_28:
				if (horzEdge.OutIdx >= 0)
				{
					this.AddLocalMaxPoly(horzEdge, eMaxPair, horzEdge.Top);
				}
				base.DeleteFromAEL(horzEdge);
				base.DeleteFromAEL(eMaxPair);
				return;
				IL_039F:
				if (horzEdge.OutIdx >= 0 && op == null)
				{
					op = this.GetLastOutPt(horzEdge);
					for (ClipperLib.TEdge eNextHorz2 = this.m_SortedEdges; eNextHorz2 != null; eNextHorz2 = eNextHorz2.NextInSEL)
					{
						if (eNextHorz2.OutIdx >= 0 && this.HorzSegmentsOverlap(horzEdge.Bot.X, horzEdge.Top.X, eNextHorz2.Bot.X, eNextHorz2.Top.X))
						{
							ClipperLib.OutPt op3 = this.GetLastOutPt(eNextHorz2);
							this.AddJoin(op3, op, eNextHorz2.Top);
						}
					}
					this.AddGhostJoin(op, horzEdge.Top);
				}
				if (horzEdge.NextInLML != null)
				{
					if (horzEdge.OutIdx < 0)
					{
						base.UpdateEdgeIntoAEL(ref horzEdge);
						return;
					}
					op = this.AddOutPt(horzEdge, horzEdge.Top);
					base.UpdateEdgeIntoAEL(ref horzEdge);
					if (horzEdge.WindDelta == 0)
					{
						return;
					}
					ClipperLib.TEdge ePrev = horzEdge.PrevInAEL;
					ClipperLib.TEdge eNext = horzEdge.NextInAEL;
					if (ePrev != null && ePrev.Curr.X == horzEdge.Bot.X && ePrev.Curr.Y == horzEdge.Bot.Y && ePrev.WindDelta != 0 && ePrev.OutIdx >= 0 && ePrev.Curr.Y > ePrev.Top.Y && ClipperLib.ClipperBase.SlopesEqual(horzEdge, ePrev, this.m_UseFullRange))
					{
						ClipperLib.OutPt op4 = this.AddOutPt(ePrev, horzEdge.Bot);
						this.AddJoin(op, op4, horzEdge.Top);
						return;
					}
					if (eNext != null && eNext.Curr.X == horzEdge.Bot.X && eNext.Curr.Y == horzEdge.Bot.Y && eNext.WindDelta != 0 && eNext.OutIdx >= 0 && eNext.Curr.Y > eNext.Top.Y && ClipperLib.ClipperBase.SlopesEqual(horzEdge, eNext, this.m_UseFullRange))
					{
						ClipperLib.OutPt op5 = this.AddOutPt(eNext, horzEdge.Bot);
						this.AddJoin(op, op5, horzEdge.Top);
						return;
					}
				}
				else
				{
					if (horzEdge.OutIdx >= 0)
					{
						this.AddOutPt(horzEdge, horzEdge.Top);
					}
					base.DeleteFromAEL(horzEdge);
				}
			}

			// Token: 0x060004D2 RID: 1234 RVA: 0x0001D28A File Offset: 0x0001B48A
			private ClipperLib.TEdge GetNextInAEL(ClipperLib.TEdge e, ClipperLib.Direction Direction)
			{
				if (Direction != ClipperLib.Direction.dLeftToRight)
				{
					return e.PrevInAEL;
				}
				return e.NextInAEL;
			}

			// Token: 0x060004D3 RID: 1235 RVA: 0x0001D29D File Offset: 0x0001B49D
			private bool IsMinima(ClipperLib.TEdge e)
			{
				return e != null && e.Prev.NextInLML != e && e.Next.NextInLML != e;
			}

			// Token: 0x060004D4 RID: 1236 RVA: 0x0001D2C3 File Offset: 0x0001B4C3
			private bool IsMaxima(ClipperLib.TEdge e, double Y)
			{
				return e != null && (double)e.Top.Y == Y && e.NextInLML == null;
			}

			// Token: 0x060004D5 RID: 1237 RVA: 0x0001D2E2 File Offset: 0x0001B4E2
			private bool IsIntermediate(ClipperLib.TEdge e, double Y)
			{
				return (double)e.Top.Y == Y && e.NextInLML != null;
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x0001D300 File Offset: 0x0001B500
			internal ClipperLib.TEdge GetMaximaPair(ClipperLib.TEdge e)
			{
				if (e.Next.Top == e.Top && e.Next.NextInLML == null)
				{
					return e.Next;
				}
				if (e.Prev.Top == e.Top && e.Prev.NextInLML == null)
				{
					return e.Prev;
				}
				return null;
			}

			// Token: 0x060004D7 RID: 1239 RVA: 0x0001D368 File Offset: 0x0001B568
			internal ClipperLib.TEdge GetMaximaPairEx(ClipperLib.TEdge e)
			{
				ClipperLib.TEdge result = this.GetMaximaPair(e);
				if (result == null || result.OutIdx == -2 || (result.NextInAEL == result.PrevInAEL && !ClipperLib.ClipperBase.IsHorizontal(result)))
				{
					return null;
				}
				return result;
			}

			// Token: 0x060004D8 RID: 1240 RVA: 0x0001D3A4 File Offset: 0x0001B5A4
			private bool ProcessIntersections(long topY)
			{
				if (this.m_ActiveEdges == null)
				{
					return true;
				}
				try
				{
					this.BuildIntersectList(topY);
					if (this.m_IntersectList.Count == 0)
					{
						return true;
					}
					if (this.m_IntersectList.Count != 1 && !this.FixupIntersectionOrder())
					{
						return false;
					}
					this.ProcessIntersectList();
				}
				catch
				{
					this.m_SortedEdges = null;
					this.m_IntersectList.Clear();
					throw new ClipperLib.ClipperException("ProcessIntersections error");
				}
				this.m_SortedEdges = null;
				return true;
			}

			// Token: 0x060004D9 RID: 1241 RVA: 0x0001D430 File Offset: 0x0001B630
			private void BuildIntersectList(long topY)
			{
				if (this.m_ActiveEdges == null)
				{
					return;
				}
				ClipperLib.TEdge e = this.m_ActiveEdges;
				this.m_SortedEdges = e;
				while (e != null)
				{
					e.PrevInSEL = e.PrevInAEL;
					e.NextInSEL = e.NextInAEL;
					e.Curr.X = ClipperLib.Clipper.TopX(e, topY);
					e = e.NextInAEL;
				}
				bool isModified = true;
				while (isModified && this.m_SortedEdges != null)
				{
					isModified = false;
					e = this.m_SortedEdges;
					while (e.NextInSEL != null)
					{
						ClipperLib.TEdge eNext = e.NextInSEL;
						if (e.Curr.X > eNext.Curr.X)
						{
							ClipperLib.IntPoint pt;
							this.IntersectPoint(e, eNext, out pt);
							if (pt.Y < topY)
							{
								pt = new ClipperLib.IntPoint(ClipperLib.Clipper.TopX(e, topY), topY);
							}
							ClipperLib.IntersectNode newNode = new ClipperLib.IntersectNode();
							newNode.Edge1 = e;
							newNode.Edge2 = eNext;
							newNode.Pt = pt;
							this.m_IntersectList.Add(newNode);
							this.SwapPositionsInSEL(e, eNext);
							isModified = true;
						}
						else
						{
							e = eNext;
						}
					}
					if (e.PrevInSEL == null)
					{
						break;
					}
					e.PrevInSEL.NextInSEL = null;
				}
				this.m_SortedEdges = null;
			}

			// Token: 0x060004DA RID: 1242 RVA: 0x0001D54B File Offset: 0x0001B74B
			private bool EdgesAdjacent(ClipperLib.IntersectNode inode)
			{
				return inode.Edge1.NextInSEL == inode.Edge2 || inode.Edge1.PrevInSEL == inode.Edge2;
			}

			// Token: 0x060004DB RID: 1243 RVA: 0x0001D575 File Offset: 0x0001B775
			private static int IntersectNodeSort(ClipperLib.IntersectNode node1, ClipperLib.IntersectNode node2)
			{
				return (int)(node2.Pt.Y - node1.Pt.Y);
			}

			// Token: 0x060004DC RID: 1244 RVA: 0x0001D590 File Offset: 0x0001B790
			private bool FixupIntersectionOrder()
			{
				this.m_IntersectList.Sort(this.m_IntersectNodeComparer);
				this.CopyAELToSEL();
				int cnt = this.m_IntersectList.Count;
				for (int i = 0; i < cnt; i++)
				{
					if (!this.EdgesAdjacent(this.m_IntersectList[i]))
					{
						int j = i + 1;
						while (j < cnt && !this.EdgesAdjacent(this.m_IntersectList[j]))
						{
							j++;
						}
						if (j == cnt)
						{
							return false;
						}
						ClipperLib.IntersectNode tmp = this.m_IntersectList[i];
						this.m_IntersectList[i] = this.m_IntersectList[j];
						this.m_IntersectList[j] = tmp;
					}
					this.SwapPositionsInSEL(this.m_IntersectList[i].Edge1, this.m_IntersectList[i].Edge2);
				}
				return true;
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x0001D66C File Offset: 0x0001B86C
			private void ProcessIntersectList()
			{
				for (int i = 0; i < this.m_IntersectList.Count; i++)
				{
					ClipperLib.IntersectNode iNode = this.m_IntersectList[i];
					this.IntersectEdges(iNode.Edge1, iNode.Edge2, iNode.Pt);
					base.SwapPositionsInAEL(iNode.Edge1, iNode.Edge2);
				}
				this.m_IntersectList.Clear();
			}

			// Token: 0x060004DE RID: 1246 RVA: 0x0001D6D1 File Offset: 0x0001B8D1
			internal static long Round(double value)
			{
				if (value >= 0.0)
				{
					return (long)(value + 0.5);
				}
				return (long)(value - 0.5);
			}

			// Token: 0x060004DF RID: 1247 RVA: 0x0001D6F8 File Offset: 0x0001B8F8
			private static long TopX(ClipperLib.TEdge edge, long currentY)
			{
				if (currentY == edge.Top.Y)
				{
					return edge.Top.X;
				}
				return edge.Bot.X + ClipperLib.Clipper.Round(edge.Dx * (double)(currentY - edge.Bot.Y));
			}

			// Token: 0x060004E0 RID: 1248 RVA: 0x0001D748 File Offset: 0x0001B948
			private void IntersectPoint(ClipperLib.TEdge edge1, ClipperLib.TEdge edge2, out ClipperLib.IntPoint ip)
			{
				ip = default(ClipperLib.IntPoint);
				if (edge1.Dx == edge2.Dx)
				{
					ip.Y = edge1.Curr.Y;
					ip.X = ClipperLib.Clipper.TopX(edge1, ip.Y);
					return;
				}
				if (edge1.Delta.X == 0L)
				{
					ip.X = edge1.Bot.X;
					if (ClipperLib.ClipperBase.IsHorizontal(edge2))
					{
						ip.Y = edge2.Bot.Y;
					}
					else
					{
						double b2 = (double)edge2.Bot.Y - (double)edge2.Bot.X / edge2.Dx;
						ip.Y = ClipperLib.Clipper.Round((double)ip.X / edge2.Dx + b2);
					}
				}
				else if (edge2.Delta.X == 0L)
				{
					ip.X = edge2.Bot.X;
					if (ClipperLib.ClipperBase.IsHorizontal(edge1))
					{
						ip.Y = edge1.Bot.Y;
					}
					else
					{
						double b3 = (double)edge1.Bot.Y - (double)edge1.Bot.X / edge1.Dx;
						ip.Y = ClipperLib.Clipper.Round((double)ip.X / edge1.Dx + b3);
					}
				}
				else
				{
					double b3 = (double)edge1.Bot.X - (double)edge1.Bot.Y * edge1.Dx;
					double b2 = (double)edge2.Bot.X - (double)edge2.Bot.Y * edge2.Dx;
					double q = (b2 - b3) / (edge1.Dx - edge2.Dx);
					ip.Y = ClipperLib.Clipper.Round(q);
					if (Math.Abs(edge1.Dx) < Math.Abs(edge2.Dx))
					{
						ip.X = ClipperLib.Clipper.Round(edge1.Dx * q + b3);
					}
					else
					{
						ip.X = ClipperLib.Clipper.Round(edge2.Dx * q + b2);
					}
				}
				if (ip.Y < edge1.Top.Y || ip.Y < edge2.Top.Y)
				{
					if (edge1.Top.Y > edge2.Top.Y)
					{
						ip.Y = edge1.Top.Y;
					}
					else
					{
						ip.Y = edge2.Top.Y;
					}
					if (Math.Abs(edge1.Dx) < Math.Abs(edge2.Dx))
					{
						ip.X = ClipperLib.Clipper.TopX(edge1, ip.Y);
					}
					else
					{
						ip.X = ClipperLib.Clipper.TopX(edge2, ip.Y);
					}
				}
				if (ip.Y > edge1.Curr.Y)
				{
					ip.Y = edge1.Curr.Y;
					if (Math.Abs(edge1.Dx) > Math.Abs(edge2.Dx))
					{
						ip.X = ClipperLib.Clipper.TopX(edge2, ip.Y);
						return;
					}
					ip.X = ClipperLib.Clipper.TopX(edge1, ip.Y);
				}
			}

			// Token: 0x060004E1 RID: 1249 RVA: 0x0001DA30 File Offset: 0x0001BC30
			private void ProcessEdgesAtTopOfScanbeam(long topY)
			{
				ClipperLib.TEdge e = this.m_ActiveEdges;
				while (e != null)
				{
					bool IsMaximaEdge = this.IsMaxima(e, (double)topY);
					if (IsMaximaEdge)
					{
						ClipperLib.TEdge eMaxPair = this.GetMaximaPairEx(e);
						IsMaximaEdge = eMaxPair == null || !ClipperLib.ClipperBase.IsHorizontal(eMaxPair);
					}
					if (IsMaximaEdge)
					{
						if (this.StrictlySimple)
						{
							this.InsertMaxima(e.Top.X);
						}
						ClipperLib.TEdge ePrev = e.PrevInAEL;
						this.DoMaxima(e);
						if (ePrev == null)
						{
							e = this.m_ActiveEdges;
						}
						else
						{
							e = ePrev.NextInAEL;
						}
					}
					else
					{
						if (this.IsIntermediate(e, (double)topY) && ClipperLib.ClipperBase.IsHorizontal(e.NextInLML))
						{
							base.UpdateEdgeIntoAEL(ref e);
							if (e.OutIdx >= 0)
							{
								this.AddOutPt(e, e.Bot);
							}
							this.AddEdgeToSEL(e);
						}
						else
						{
							e.Curr.X = ClipperLib.Clipper.TopX(e, topY);
							e.Curr.Y = topY;
						}
						if (this.StrictlySimple)
						{
							ClipperLib.TEdge ePrev2 = e.PrevInAEL;
							if (e.OutIdx >= 0 && e.WindDelta != 0 && ePrev2 != null && ePrev2.OutIdx >= 0 && ePrev2.Curr.X == e.Curr.X && ePrev2.WindDelta != 0)
							{
								ClipperLib.IntPoint ip = new ClipperLib.IntPoint(e.Curr);
								ClipperLib.OutPt op = this.AddOutPt(ePrev2, ip);
								ClipperLib.OutPt op2 = this.AddOutPt(e, ip);
								this.AddJoin(op, op2, ip);
							}
						}
						e = e.NextInAEL;
					}
				}
				this.ProcessHorizontals();
				this.m_Maxima = null;
				for (e = this.m_ActiveEdges; e != null; e = e.NextInAEL)
				{
					if (this.IsIntermediate(e, (double)topY))
					{
						ClipperLib.OutPt op3 = null;
						if (e.OutIdx >= 0)
						{
							op3 = this.AddOutPt(e, e.Top);
						}
						base.UpdateEdgeIntoAEL(ref e);
						ClipperLib.TEdge ePrev3 = e.PrevInAEL;
						ClipperLib.TEdge eNext = e.NextInAEL;
						if (ePrev3 != null && ePrev3.Curr.X == e.Bot.X && ePrev3.Curr.Y == e.Bot.Y && op3 != null && ePrev3.OutIdx >= 0 && ePrev3.Curr.Y > ePrev3.Top.Y && ClipperLib.ClipperBase.SlopesEqual(e.Curr, e.Top, ePrev3.Curr, ePrev3.Top, this.m_UseFullRange) && e.WindDelta != 0 && ePrev3.WindDelta != 0)
						{
							ClipperLib.OutPt op4 = this.AddOutPt(ePrev3, e.Bot);
							this.AddJoin(op3, op4, e.Top);
						}
						else if (eNext != null && eNext.Curr.X == e.Bot.X && eNext.Curr.Y == e.Bot.Y && op3 != null && eNext.OutIdx >= 0 && eNext.Curr.Y > eNext.Top.Y && ClipperLib.ClipperBase.SlopesEqual(e.Curr, e.Top, eNext.Curr, eNext.Top, this.m_UseFullRange) && e.WindDelta != 0 && eNext.WindDelta != 0)
						{
							ClipperLib.OutPt op5 = this.AddOutPt(eNext, e.Bot);
							this.AddJoin(op3, op5, e.Top);
						}
					}
				}
			}

			// Token: 0x060004E2 RID: 1250 RVA: 0x0001DD8C File Offset: 0x0001BF8C
			private void DoMaxima(ClipperLib.TEdge e)
			{
				ClipperLib.TEdge eMaxPair = this.GetMaximaPairEx(e);
				if (eMaxPair == null)
				{
					if (e.OutIdx >= 0)
					{
						this.AddOutPt(e, e.Top);
					}
					base.DeleteFromAEL(e);
					return;
				}
				ClipperLib.TEdge eNext = e.NextInAEL;
				while (eNext != null && eNext != eMaxPair)
				{
					this.IntersectEdges(e, eNext, e.Top);
					base.SwapPositionsInAEL(e, eNext);
					eNext = e.NextInAEL;
				}
				if (e.OutIdx == -1 && eMaxPair.OutIdx == -1)
				{
					base.DeleteFromAEL(e);
					base.DeleteFromAEL(eMaxPair);
					return;
				}
				if (e.OutIdx >= 0 && eMaxPair.OutIdx >= 0)
				{
					if (e.OutIdx >= 0)
					{
						this.AddLocalMaxPoly(e, eMaxPair, e.Top);
					}
					base.DeleteFromAEL(e);
					base.DeleteFromAEL(eMaxPair);
					return;
				}
				if (e.WindDelta == 0)
				{
					if (e.OutIdx >= 0)
					{
						this.AddOutPt(e, e.Top);
						e.OutIdx = -1;
					}
					base.DeleteFromAEL(e);
					if (eMaxPair.OutIdx >= 0)
					{
						this.AddOutPt(eMaxPair, e.Top);
						eMaxPair.OutIdx = -1;
					}
					base.DeleteFromAEL(eMaxPair);
					return;
				}
				throw new ClipperLib.ClipperException("DoMaxima error");
			}

			// Token: 0x060004E3 RID: 1251 RVA: 0x0001DEA8 File Offset: 0x0001C0A8
			public static void ReversePaths(List<List<ClipperLib.IntPoint>> polys)
			{
				foreach (List<ClipperLib.IntPoint> list in polys)
				{
					list.Reverse();
				}
			}

			// Token: 0x060004E4 RID: 1252 RVA: 0x0001DEF4 File Offset: 0x0001C0F4
			public static bool Orientation(List<ClipperLib.IntPoint> poly)
			{
				return ClipperLib.Clipper.Area(poly) >= 0.0;
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x0001DF0C File Offset: 0x0001C10C
			private int PointCount(ClipperLib.OutPt pts)
			{
				if (pts == null)
				{
					return 0;
				}
				int result = 0;
				ClipperLib.OutPt p = pts;
				do
				{
					result++;
					p = p.Next;
				}
				while (p != pts);
				return result;
			}

			// Token: 0x060004E6 RID: 1254 RVA: 0x0001DF34 File Offset: 0x0001C134
			private void BuildResult(List<List<ClipperLib.IntPoint>> polyg)
			{
				polyg.Clear();
				polyg.Capacity = this.m_PolyOuts.Count;
				for (int i = 0; i < this.m_PolyOuts.Count; i++)
				{
					ClipperLib.OutRec outRec = this.m_PolyOuts[i];
					if (outRec.Pts != null)
					{
						ClipperLib.OutPt p = outRec.Pts.Prev;
						int cnt = this.PointCount(p);
						if (cnt >= 2)
						{
							List<ClipperLib.IntPoint> pg = new List<ClipperLib.IntPoint>(cnt);
							for (int j = 0; j < cnt; j++)
							{
								pg.Add(p.Pt);
								p = p.Prev;
							}
							polyg.Add(pg);
						}
					}
				}
			}

			// Token: 0x060004E7 RID: 1255 RVA: 0x0001DFD0 File Offset: 0x0001C1D0
			private void BuildResult2(ClipperLib.PolyTree polytree)
			{
				polytree.Clear();
				polytree.m_AllPolys.Capacity = this.m_PolyOuts.Count;
				for (int i = 0; i < this.m_PolyOuts.Count; i++)
				{
					ClipperLib.OutRec outRec = this.m_PolyOuts[i];
					int cnt = this.PointCount(outRec.Pts);
					if ((!outRec.IsOpen || cnt >= 2) && (outRec.IsOpen || cnt >= 3))
					{
						this.FixHoleLinkage(outRec);
						ClipperLib.PolyNode pn = new ClipperLib.PolyNode();
						polytree.m_AllPolys.Add(pn);
						outRec.PolyNode = pn;
						pn.m_polygon.Capacity = cnt;
						ClipperLib.OutPt op = outRec.Pts.Prev;
						for (int j = 0; j < cnt; j++)
						{
							pn.m_polygon.Add(op.Pt);
							op = op.Prev;
						}
					}
				}
				polytree.m_Childs.Capacity = this.m_PolyOuts.Count;
				for (int k = 0; k < this.m_PolyOuts.Count; k++)
				{
					ClipperLib.OutRec outRec2 = this.m_PolyOuts[k];
					if (outRec2.PolyNode != null)
					{
						if (outRec2.IsOpen)
						{
							outRec2.PolyNode.IsOpen = true;
							polytree.AddChild(outRec2.PolyNode);
						}
						else if (outRec2.FirstLeft != null && outRec2.FirstLeft.PolyNode != null)
						{
							outRec2.FirstLeft.PolyNode.AddChild(outRec2.PolyNode);
						}
						else
						{
							polytree.AddChild(outRec2.PolyNode);
						}
					}
				}
			}

			// Token: 0x060004E8 RID: 1256 RVA: 0x0001E15C File Offset: 0x0001C35C
			private void FixupOutPolyline(ClipperLib.OutRec outrec)
			{
				ClipperLib.OutPt pp = outrec.Pts;
				ClipperLib.OutPt lastPP = pp.Prev;
				while (pp != lastPP)
				{
					pp = pp.Next;
					if (pp.Pt == pp.Prev.Pt)
					{
						if (pp == lastPP)
						{
							lastPP = pp.Prev;
						}
						ClipperLib.OutPt tmpPP = pp.Prev;
						tmpPP.Next = pp.Next;
						pp.Next.Prev = tmpPP;
						pp = tmpPP;
					}
				}
				if (pp == pp.Prev)
				{
					outrec.Pts = null;
				}
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x0001E1D8 File Offset: 0x0001C3D8
			private void FixupOutPolygon(ClipperLib.OutRec outRec)
			{
				ClipperLib.OutPt lastOK = null;
				outRec.BottomPt = null;
				ClipperLib.OutPt pp = outRec.Pts;
				bool preserveCol = base.PreserveCollinear || this.StrictlySimple;
				while (pp.Prev != pp && pp.Prev != pp.Next)
				{
					if (pp.Pt == pp.Next.Pt || pp.Pt == pp.Prev.Pt || (ClipperLib.ClipperBase.SlopesEqual(pp.Prev.Pt, pp.Pt, pp.Next.Pt, this.m_UseFullRange) && (!preserveCol || !base.Pt2IsBetweenPt1AndPt3(pp.Prev.Pt, pp.Pt, pp.Next.Pt))))
					{
						lastOK = null;
						pp.Prev.Next = pp.Next;
						pp.Next.Prev = pp.Prev;
						pp = pp.Prev;
					}
					else
					{
						if (pp == lastOK)
						{
							outRec.Pts = pp;
							return;
						}
						if (lastOK == null)
						{
							lastOK = pp;
						}
						pp = pp.Next;
					}
				}
				outRec.Pts = null;
			}

			// Token: 0x060004EA RID: 1258 RVA: 0x0001E2F4 File Offset: 0x0001C4F4
			private ClipperLib.OutPt DupOutPt(ClipperLib.OutPt outPt, bool InsertAfter)
			{
				ClipperLib.OutPt result = new ClipperLib.OutPt();
				result.Pt = outPt.Pt;
				result.Idx = outPt.Idx;
				if (InsertAfter)
				{
					result.Next = outPt.Next;
					result.Prev = outPt;
					outPt.Next.Prev = result;
					outPt.Next = result;
				}
				else
				{
					result.Prev = outPt.Prev;
					result.Next = outPt;
					outPt.Prev.Next = result;
					outPt.Prev = result;
				}
				return result;
			}

			// Token: 0x060004EB RID: 1259 RVA: 0x0001E374 File Offset: 0x0001C574
			private bool GetOverlap(long a1, long a2, long b1, long b2, out long Left, out long Right)
			{
				if (a1 < a2)
				{
					if (b1 < b2)
					{
						Left = Math.Max(a1, b1);
						Right = Math.Min(a2, b2);
					}
					else
					{
						Left = Math.Max(a1, b2);
						Right = Math.Min(a2, b1);
					}
				}
				else if (b1 < b2)
				{
					Left = Math.Max(a2, b1);
					Right = Math.Min(a1, b2);
				}
				else
				{
					Left = Math.Max(a2, b2);
					Right = Math.Min(a1, b1);
				}
				return Left < Right;
			}

			// Token: 0x060004EC RID: 1260 RVA: 0x0001E3F4 File Offset: 0x0001C5F4
			private bool JoinHorz(ClipperLib.OutPt op1, ClipperLib.OutPt op1b, ClipperLib.OutPt op2, ClipperLib.OutPt op2b, ClipperLib.IntPoint Pt, bool DiscardLeft)
			{
				ClipperLib.Direction Dir = ((op1.Pt.X > op1b.Pt.X) ? ClipperLib.Direction.dRightToLeft : ClipperLib.Direction.dLeftToRight);
				ClipperLib.Direction Dir2 = ((op2.Pt.X > op2b.Pt.X) ? ClipperLib.Direction.dRightToLeft : ClipperLib.Direction.dLeftToRight);
				if (Dir == Dir2)
				{
					return false;
				}
				if (Dir == ClipperLib.Direction.dLeftToRight)
				{
					while (op1.Next.Pt.X <= Pt.X && op1.Next.Pt.X >= op1.Pt.X && op1.Next.Pt.Y == Pt.Y)
					{
						op1 = op1.Next;
					}
					if (DiscardLeft && op1.Pt.X != Pt.X)
					{
						op1 = op1.Next;
					}
					op1b = this.DupOutPt(op1, !DiscardLeft);
					if (op1b.Pt != Pt)
					{
						op1 = op1b;
						op1.Pt = Pt;
						op1b = this.DupOutPt(op1, !DiscardLeft);
					}
				}
				else
				{
					while (op1.Next.Pt.X >= Pt.X && op1.Next.Pt.X <= op1.Pt.X && op1.Next.Pt.Y == Pt.Y)
					{
						op1 = op1.Next;
					}
					if (!DiscardLeft && op1.Pt.X != Pt.X)
					{
						op1 = op1.Next;
					}
					op1b = this.DupOutPt(op1, DiscardLeft);
					if (op1b.Pt != Pt)
					{
						op1 = op1b;
						op1.Pt = Pt;
						op1b = this.DupOutPt(op1, DiscardLeft);
					}
				}
				if (Dir2 == ClipperLib.Direction.dLeftToRight)
				{
					while (op2.Next.Pt.X <= Pt.X && op2.Next.Pt.X >= op2.Pt.X && op2.Next.Pt.Y == Pt.Y)
					{
						op2 = op2.Next;
					}
					if (DiscardLeft && op2.Pt.X != Pt.X)
					{
						op2 = op2.Next;
					}
					op2b = this.DupOutPt(op2, !DiscardLeft);
					if (op2b.Pt != Pt)
					{
						op2 = op2b;
						op2.Pt = Pt;
						op2b = this.DupOutPt(op2, !DiscardLeft);
					}
				}
				else
				{
					while (op2.Next.Pt.X >= Pt.X && op2.Next.Pt.X <= op2.Pt.X && op2.Next.Pt.Y == Pt.Y)
					{
						op2 = op2.Next;
					}
					if (!DiscardLeft && op2.Pt.X != Pt.X)
					{
						op2 = op2.Next;
					}
					op2b = this.DupOutPt(op2, DiscardLeft);
					if (op2b.Pt != Pt)
					{
						op2 = op2b;
						op2.Pt = Pt;
						op2b = this.DupOutPt(op2, DiscardLeft);
					}
				}
				if (Dir == ClipperLib.Direction.dLeftToRight == DiscardLeft)
				{
					op1.Prev = op2;
					op2.Next = op1;
					op1b.Next = op2b;
					op2b.Prev = op1b;
				}
				else
				{
					op1.Next = op2;
					op2.Prev = op1;
					op1b.Prev = op2b;
					op2b.Next = op1b;
				}
				return true;
			}

			// Token: 0x060004ED RID: 1261 RVA: 0x0001E758 File Offset: 0x0001C958
			private bool JoinPoints(ClipperLib.Join j, ClipperLib.OutRec outRec1, ClipperLib.OutRec outRec2)
			{
				ClipperLib.OutPt op = j.OutPt1;
				ClipperLib.OutPt op2 = j.OutPt2;
				bool isHorizontal = j.OutPt1.Pt.Y == j.OffPt.Y;
				if (isHorizontal && j.OffPt == j.OutPt1.Pt && j.OffPt == j.OutPt2.Pt)
				{
					if (outRec1 != outRec2)
					{
						return false;
					}
					ClipperLib.OutPt op1b = j.OutPt1.Next;
					while (op1b != op && op1b.Pt == j.OffPt)
					{
						op1b = op1b.Next;
					}
					bool reverse = op1b.Pt.Y > j.OffPt.Y;
					ClipperLib.OutPt op2b = j.OutPt2.Next;
					while (op2b != op2 && op2b.Pt == j.OffPt)
					{
						op2b = op2b.Next;
					}
					bool reverse2 = op2b.Pt.Y > j.OffPt.Y;
					if (reverse == reverse2)
					{
						return false;
					}
					if (reverse)
					{
						op1b = this.DupOutPt(op, false);
						op2b = this.DupOutPt(op2, true);
						op.Prev = op2;
						op2.Next = op;
						op1b.Next = op2b;
						op2b.Prev = op1b;
						j.OutPt1 = op;
						j.OutPt2 = op1b;
						return true;
					}
					op1b = this.DupOutPt(op, true);
					op2b = this.DupOutPt(op2, false);
					op.Next = op2;
					op2.Prev = op;
					op1b.Prev = op2b;
					op2b.Next = op1b;
					j.OutPt1 = op;
					j.OutPt2 = op1b;
					return true;
				}
				else if (isHorizontal)
				{
					ClipperLib.OutPt op1b = op;
					while (op.Prev.Pt.Y == op.Pt.Y && op.Prev != op1b)
					{
						if (op.Prev == op2)
						{
							break;
						}
						op = op.Prev;
					}
					while (op1b.Next.Pt.Y == op1b.Pt.Y && op1b.Next != op && op1b.Next != op2)
					{
						op1b = op1b.Next;
					}
					if (op1b.Next == op || op1b.Next == op2)
					{
						return false;
					}
					ClipperLib.OutPt op2b = op2;
					while (op2.Prev.Pt.Y == op2.Pt.Y && op2.Prev != op2b)
					{
						if (op2.Prev == op1b)
						{
							break;
						}
						op2 = op2.Prev;
					}
					while (op2b.Next.Pt.Y == op2b.Pt.Y && op2b.Next != op2 && op2b.Next != op)
					{
						op2b = op2b.Next;
					}
					if (op2b.Next == op2 || op2b.Next == op)
					{
						return false;
					}
					long Left;
					long Right;
					if (!this.GetOverlap(op.Pt.X, op1b.Pt.X, op2.Pt.X, op2b.Pt.X, out Left, out Right))
					{
						return false;
					}
					ClipperLib.IntPoint Pt;
					bool DiscardLeftSide;
					if (op.Pt.X >= Left && op.Pt.X <= Right)
					{
						Pt = op.Pt;
						DiscardLeftSide = op.Pt.X > op1b.Pt.X;
					}
					else if (op2.Pt.X >= Left && op2.Pt.X <= Right)
					{
						Pt = op2.Pt;
						DiscardLeftSide = op2.Pt.X > op2b.Pt.X;
					}
					else if (op1b.Pt.X >= Left && op1b.Pt.X <= Right)
					{
						Pt = op1b.Pt;
						DiscardLeftSide = op1b.Pt.X > op.Pt.X;
					}
					else
					{
						Pt = op2b.Pt;
						DiscardLeftSide = op2b.Pt.X > op2.Pt.X;
					}
					j.OutPt1 = op;
					j.OutPt2 = op2;
					return this.JoinHorz(op, op1b, op2, op2b, Pt, DiscardLeftSide);
				}
				else
				{
					ClipperLib.OutPt op1b = op.Next;
					while (op1b.Pt == op.Pt && op1b != op)
					{
						op1b = op1b.Next;
					}
					bool Reverse = op1b.Pt.Y > op.Pt.Y || !ClipperLib.ClipperBase.SlopesEqual(op.Pt, op1b.Pt, j.OffPt, this.m_UseFullRange);
					if (Reverse)
					{
						op1b = op.Prev;
						while (op1b.Pt == op.Pt && op1b != op)
						{
							op1b = op1b.Prev;
						}
						if (op1b.Pt.Y > op.Pt.Y || !ClipperLib.ClipperBase.SlopesEqual(op.Pt, op1b.Pt, j.OffPt, this.m_UseFullRange))
						{
							return false;
						}
					}
					ClipperLib.OutPt op2b = op2.Next;
					while (op2b.Pt == op2.Pt && op2b != op2)
					{
						op2b = op2b.Next;
					}
					bool Reverse2 = op2b.Pt.Y > op2.Pt.Y || !ClipperLib.ClipperBase.SlopesEqual(op2.Pt, op2b.Pt, j.OffPt, this.m_UseFullRange);
					if (Reverse2)
					{
						op2b = op2.Prev;
						while (op2b.Pt == op2.Pt && op2b != op2)
						{
							op2b = op2b.Prev;
						}
						if (op2b.Pt.Y > op2.Pt.Y || !ClipperLib.ClipperBase.SlopesEqual(op2.Pt, op2b.Pt, j.OffPt, this.m_UseFullRange))
						{
							return false;
						}
					}
					if (op1b == op || op2b == op2 || op1b == op2b || (outRec1 == outRec2 && Reverse == Reverse2))
					{
						return false;
					}
					if (Reverse)
					{
						op1b = this.DupOutPt(op, false);
						op2b = this.DupOutPt(op2, true);
						op.Prev = op2;
						op2.Next = op;
						op1b.Next = op2b;
						op2b.Prev = op1b;
						j.OutPt1 = op;
						j.OutPt2 = op1b;
						return true;
					}
					op1b = this.DupOutPt(op, true);
					op2b = this.DupOutPt(op2, false);
					op.Next = op2;
					op2.Prev = op;
					op1b.Prev = op2b;
					op2b.Next = op1b;
					j.OutPt1 = op;
					j.OutPt2 = op1b;
					return true;
				}
			}

			// Token: 0x060004EE RID: 1262 RVA: 0x0001ED68 File Offset: 0x0001CF68
			public static int PointInPolygon(ClipperLib.IntPoint pt, List<ClipperLib.IntPoint> path)
			{
				int result = 0;
				int cnt = path.Count;
				if (cnt < 3)
				{
					return 0;
				}
				ClipperLib.IntPoint ip = path[0];
				for (int i = 1; i <= cnt; i++)
				{
					ClipperLib.IntPoint ipNext = ((i == cnt) ? path[0] : path[i]);
					if (ipNext.Y == pt.Y && (ipNext.X == pt.X || (ip.Y == pt.Y && ipNext.X > pt.X == ip.X < pt.X)))
					{
						return -1;
					}
					if (ip.Y < pt.Y != ipNext.Y < pt.Y)
					{
						if (ip.X >= pt.X)
						{
							if (ipNext.X > pt.X)
							{
								result = 1 - result;
							}
							else
							{
								double d = (double)(ip.X - pt.X) * (double)(ipNext.Y - pt.Y) - (double)(ipNext.X - pt.X) * (double)(ip.Y - pt.Y);
								if (d == 0.0)
								{
									return -1;
								}
								if (d > 0.0 == ipNext.Y > ip.Y)
								{
									result = 1 - result;
								}
							}
						}
						else if (ipNext.X > pt.X)
						{
							double d2 = (double)(ip.X - pt.X) * (double)(ipNext.Y - pt.Y) - (double)(ipNext.X - pt.X) * (double)(ip.Y - pt.Y);
							if (d2 == 0.0)
							{
								return -1;
							}
							if (d2 > 0.0 == ipNext.Y > ip.Y)
							{
								result = 1 - result;
							}
						}
					}
					ip = ipNext;
				}
				return result;
			}

			// Token: 0x060004EF RID: 1263 RVA: 0x0001EF44 File Offset: 0x0001D144
			private static int PointInPolygon(ClipperLib.IntPoint pt, ClipperLib.OutPt op)
			{
				int result = 0;
				ClipperLib.OutPt startOp = op;
				long ptx = pt.X;
				long pty = pt.Y;
				long poly0x = op.Pt.X;
				long poly0y = op.Pt.Y;
				for (;;)
				{
					op = op.Next;
					long poly1x = op.Pt.X;
					long poly1y = op.Pt.Y;
					if (poly1y == pty && (poly1x == ptx || (poly0y == pty && poly1x > ptx == poly0x < ptx)))
					{
						break;
					}
					if (poly0y < pty != poly1y < pty)
					{
						if (poly0x >= ptx)
						{
							if (poly1x > ptx)
							{
								result = 1 - result;
							}
							else
							{
								double d = (double)(poly0x - ptx) * (double)(poly1y - pty) - (double)(poly1x - ptx) * (double)(poly0y - pty);
								if (d == 0.0)
								{
									return -1;
								}
								if (d > 0.0 == poly1y > poly0y)
								{
									result = 1 - result;
								}
							}
						}
						else if (poly1x > ptx)
						{
							double d2 = (double)(poly0x - ptx) * (double)(poly1y - pty) - (double)(poly1x - ptx) * (double)(poly0y - pty);
							if (d2 == 0.0)
							{
								return -1;
							}
							if (d2 > 0.0 == poly1y > poly0y)
							{
								result = 1 - result;
							}
						}
					}
					poly0x = poly1x;
					poly0y = poly1y;
					if (startOp == op)
					{
						return result;
					}
				}
				return -1;
			}

			// Token: 0x060004F0 RID: 1264 RVA: 0x0001F078 File Offset: 0x0001D278
			private static bool Poly2ContainsPoly1(ClipperLib.OutPt outPt1, ClipperLib.OutPt outPt2)
			{
				ClipperLib.OutPt op = outPt1;
				int res;
				for (;;)
				{
					res = ClipperLib.Clipper.PointInPolygon(op.Pt, outPt2);
					if (res >= 0)
					{
						break;
					}
					op = op.Next;
					if (op == outPt1)
					{
						return true;
					}
				}
				return res > 0;
			}

			// Token: 0x060004F1 RID: 1265 RVA: 0x0001F0AC File Offset: 0x0001D2AC
			private void FixupFirstLefts1(ClipperLib.OutRec OldOutRec, ClipperLib.OutRec NewOutRec)
			{
				foreach (ClipperLib.OutRec outRec in this.m_PolyOuts)
				{
					ClipperLib.OutRec firstLeft = ClipperLib.Clipper.ParseFirstLeft(outRec.FirstLeft);
					if (outRec.Pts != null && firstLeft == OldOutRec && ClipperLib.Clipper.Poly2ContainsPoly1(outRec.Pts, NewOutRec.Pts))
					{
						outRec.FirstLeft = NewOutRec;
					}
				}
			}

			// Token: 0x060004F2 RID: 1266 RVA: 0x0001F12C File Offset: 0x0001D32C
			private void FixupFirstLefts2(ClipperLib.OutRec innerOutRec, ClipperLib.OutRec outerOutRec)
			{
				ClipperLib.OutRec orfl = outerOutRec.FirstLeft;
				foreach (ClipperLib.OutRec outRec in this.m_PolyOuts)
				{
					if (outRec.Pts != null && outRec != outerOutRec && outRec != innerOutRec)
					{
						ClipperLib.OutRec firstLeft = ClipperLib.Clipper.ParseFirstLeft(outRec.FirstLeft);
						if (firstLeft == orfl || firstLeft == innerOutRec || firstLeft == outerOutRec)
						{
							if (ClipperLib.Clipper.Poly2ContainsPoly1(outRec.Pts, innerOutRec.Pts))
							{
								outRec.FirstLeft = innerOutRec;
							}
							else if (ClipperLib.Clipper.Poly2ContainsPoly1(outRec.Pts, outerOutRec.Pts))
							{
								outRec.FirstLeft = outerOutRec;
							}
							else if (outRec.FirstLeft == innerOutRec || outRec.FirstLeft == outerOutRec)
							{
								outRec.FirstLeft = orfl;
							}
						}
					}
				}
			}

			// Token: 0x060004F3 RID: 1267 RVA: 0x0001F200 File Offset: 0x0001D400
			private void FixupFirstLefts3(ClipperLib.OutRec OldOutRec, ClipperLib.OutRec NewOutRec)
			{
				foreach (ClipperLib.OutRec outRec in this.m_PolyOuts)
				{
					ClipperLib.OutRec firstLeft = ClipperLib.Clipper.ParseFirstLeft(outRec.FirstLeft);
					if (outRec.Pts != null && firstLeft == OldOutRec)
					{
						outRec.FirstLeft = NewOutRec;
					}
				}
			}

			// Token: 0x060004F4 RID: 1268 RVA: 0x0001F26C File Offset: 0x0001D46C
			private static ClipperLib.OutRec ParseFirstLeft(ClipperLib.OutRec FirstLeft)
			{
				while (FirstLeft != null && FirstLeft.Pts == null)
				{
					FirstLeft = FirstLeft.FirstLeft;
				}
				return FirstLeft;
			}

			// Token: 0x060004F5 RID: 1269 RVA: 0x0001F284 File Offset: 0x0001D484
			private void JoinCommonEdges()
			{
				for (int i = 0; i < this.m_Joins.Count; i++)
				{
					ClipperLib.Join join = this.m_Joins[i];
					ClipperLib.OutRec outRec = this.GetOutRec(join.OutPt1.Idx);
					ClipperLib.OutRec outRec2 = this.GetOutRec(join.OutPt2.Idx);
					if (outRec.Pts != null && outRec2.Pts != null && !outRec.IsOpen && !outRec2.IsOpen)
					{
						ClipperLib.OutRec holeStateRec;
						if (outRec == outRec2)
						{
							holeStateRec = outRec;
						}
						else if (this.OutRec1RightOfOutRec2(outRec, outRec2))
						{
							holeStateRec = outRec2;
						}
						else if (this.OutRec1RightOfOutRec2(outRec2, outRec))
						{
							holeStateRec = outRec;
						}
						else
						{
							holeStateRec = this.GetLowermostRec(outRec, outRec2);
						}
						if (this.JoinPoints(join, outRec, outRec2))
						{
							if (outRec == outRec2)
							{
								outRec.Pts = join.OutPt1;
								outRec.BottomPt = null;
								outRec2 = base.CreateOutRec();
								outRec2.Pts = join.OutPt2;
								this.UpdateOutPtIdxs(outRec2);
								if (ClipperLib.Clipper.Poly2ContainsPoly1(outRec2.Pts, outRec.Pts))
								{
									outRec2.IsHole = !outRec.IsHole;
									outRec2.FirstLeft = outRec;
									if (this.m_UsingPolyTree)
									{
										this.FixupFirstLefts2(outRec2, outRec);
									}
									if ((outRec2.IsHole ^ this.ReverseSolution) == this.Area(outRec2) > 0.0)
									{
										this.ReversePolyPtLinks(outRec2.Pts);
									}
								}
								else if (ClipperLib.Clipper.Poly2ContainsPoly1(outRec.Pts, outRec2.Pts))
								{
									outRec2.IsHole = outRec.IsHole;
									outRec.IsHole = !outRec2.IsHole;
									outRec2.FirstLeft = outRec.FirstLeft;
									outRec.FirstLeft = outRec2;
									if (this.m_UsingPolyTree)
									{
										this.FixupFirstLefts2(outRec, outRec2);
									}
									if ((outRec.IsHole ^ this.ReverseSolution) == this.Area(outRec) > 0.0)
									{
										this.ReversePolyPtLinks(outRec.Pts);
									}
								}
								else
								{
									outRec2.IsHole = outRec.IsHole;
									outRec2.FirstLeft = outRec.FirstLeft;
									if (this.m_UsingPolyTree)
									{
										this.FixupFirstLefts1(outRec, outRec2);
									}
								}
							}
							else
							{
								outRec2.Pts = null;
								outRec2.BottomPt = null;
								outRec2.Idx = outRec.Idx;
								outRec.IsHole = holeStateRec.IsHole;
								if (holeStateRec == outRec2)
								{
									outRec.FirstLeft = outRec2.FirstLeft;
								}
								outRec2.FirstLeft = outRec;
								if (this.m_UsingPolyTree)
								{
									this.FixupFirstLefts3(outRec2, outRec);
								}
							}
						}
					}
				}
			}

			// Token: 0x060004F6 RID: 1270 RVA: 0x0001F4E8 File Offset: 0x0001D6E8
			private void UpdateOutPtIdxs(ClipperLib.OutRec outrec)
			{
				ClipperLib.OutPt op = outrec.Pts;
				do
				{
					op.Idx = outrec.Idx;
					op = op.Prev;
				}
				while (op != outrec.Pts);
			}

			// Token: 0x060004F7 RID: 1271 RVA: 0x0001F518 File Offset: 0x0001D718
			private void DoSimplePolygons()
			{
				int i = 0;
				while (i < this.m_PolyOuts.Count)
				{
					ClipperLib.OutRec outrec = this.m_PolyOuts[i++];
					ClipperLib.OutPt op = outrec.Pts;
					if (op != null && !outrec.IsOpen)
					{
						do
						{
							for (ClipperLib.OutPt op2 = op.Next; op2 != outrec.Pts; op2 = op2.Next)
							{
								if (op.Pt == op2.Pt && op2.Next != op && op2.Prev != op)
								{
									ClipperLib.OutPt op3 = op.Prev;
									ClipperLib.OutPt op4 = op2.Prev;
									op.Prev = op4;
									op4.Next = op;
									op2.Prev = op3;
									op3.Next = op2;
									outrec.Pts = op;
									ClipperLib.OutRec outrec2 = base.CreateOutRec();
									outrec2.Pts = op2;
									this.UpdateOutPtIdxs(outrec2);
									if (ClipperLib.Clipper.Poly2ContainsPoly1(outrec2.Pts, outrec.Pts))
									{
										outrec2.IsHole = !outrec.IsHole;
										outrec2.FirstLeft = outrec;
										if (this.m_UsingPolyTree)
										{
											this.FixupFirstLefts2(outrec2, outrec);
										}
									}
									else if (ClipperLib.Clipper.Poly2ContainsPoly1(outrec.Pts, outrec2.Pts))
									{
										outrec2.IsHole = outrec.IsHole;
										outrec.IsHole = !outrec2.IsHole;
										outrec2.FirstLeft = outrec.FirstLeft;
										outrec.FirstLeft = outrec2;
										if (this.m_UsingPolyTree)
										{
											this.FixupFirstLefts2(outrec, outrec2);
										}
									}
									else
									{
										outrec2.IsHole = outrec.IsHole;
										outrec2.FirstLeft = outrec.FirstLeft;
										if (this.m_UsingPolyTree)
										{
											this.FixupFirstLefts1(outrec, outrec2);
										}
									}
									op2 = op;
								}
							}
							op = op.Next;
						}
						while (op != outrec.Pts);
					}
				}
			}

			// Token: 0x060004F8 RID: 1272 RVA: 0x0001F6E0 File Offset: 0x0001D8E0
			public static double Area(List<ClipperLib.IntPoint> poly)
			{
				int cnt = poly.Count;
				if (cnt < 3)
				{
					return 0.0;
				}
				double a = 0.0;
				int i = 0;
				int j = cnt - 1;
				while (i < cnt)
				{
					a += ((double)poly[j].X + (double)poly[i].X) * ((double)poly[j].Y - (double)poly[i].Y);
					j = i;
					i++;
				}
				return -a * 0.5;
			}

			// Token: 0x060004F9 RID: 1273 RVA: 0x0001F764 File Offset: 0x0001D964
			internal double Area(ClipperLib.OutRec outRec)
			{
				return this.Area(outRec.Pts);
			}

			// Token: 0x060004FA RID: 1274 RVA: 0x0001F774 File Offset: 0x0001D974
			internal double Area(ClipperLib.OutPt op)
			{
				ClipperLib.OutPt opFirst = op;
				if (op == null)
				{
					return 0.0;
				}
				double a = 0.0;
				do
				{
					a += (double)(op.Prev.Pt.X + op.Pt.X) * (double)(op.Prev.Pt.Y - op.Pt.Y);
					op = op.Next;
				}
				while (op != opFirst);
				return a * 0.5;
			}

			// Token: 0x060004FB RID: 1275 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
			public static List<List<ClipperLib.IntPoint>> SimplifyPolygon(List<ClipperLib.IntPoint> poly, ClipperLib.PolyFillType fillType = ClipperLib.PolyFillType.pftEvenOdd)
			{
				List<List<ClipperLib.IntPoint>> result = new List<List<ClipperLib.IntPoint>>();
				ClipperLib.Clipper clipper = new ClipperLib.Clipper(0);
				clipper.StrictlySimple = true;
				clipper.AddPath(poly, ClipperLib.PolyType.ptSubject, true);
				clipper.Execute(ClipperLib.ClipType.ctUnion, result, fillType, fillType);
				return result;
			}

			// Token: 0x060004FC RID: 1276 RVA: 0x0001F828 File Offset: 0x0001DA28
			public static List<List<ClipperLib.IntPoint>> SimplifyPolygons(List<List<ClipperLib.IntPoint>> polys, ClipperLib.PolyFillType fillType = ClipperLib.PolyFillType.pftEvenOdd)
			{
				List<List<ClipperLib.IntPoint>> result = new List<List<ClipperLib.IntPoint>>();
				ClipperLib.Clipper clipper = new ClipperLib.Clipper(0);
				clipper.StrictlySimple = true;
				clipper.AddPaths(polys, ClipperLib.PolyType.ptSubject, true);
				clipper.Execute(ClipperLib.ClipType.ctUnion, result, fillType, fillType);
				return result;
			}

			// Token: 0x060004FD RID: 1277 RVA: 0x0001F860 File Offset: 0x0001DA60
			private static double DistanceSqrd(ClipperLib.IntPoint pt1, ClipperLib.IntPoint pt2)
			{
				double num = (double)pt1.X - (double)pt2.X;
				double dy = (double)pt1.Y - (double)pt2.Y;
				return num * num + dy * dy;
			}

			// Token: 0x060004FE RID: 1278 RVA: 0x0001F894 File Offset: 0x0001DA94
			private static double DistanceFromLineSqrd(ClipperLib.IntPoint pt, ClipperLib.IntPoint ln1, ClipperLib.IntPoint ln2)
			{
				double A = (double)(ln1.Y - ln2.Y);
				double B = (double)(ln2.X - ln1.X);
				double C = A * (double)ln1.X + B * (double)ln1.Y;
				C = A * (double)pt.X + B * (double)pt.Y - C;
				return C * C / (A * A + B * B);
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x0001F8F4 File Offset: 0x0001DAF4
			private static bool SlopesNearCollinear(ClipperLib.IntPoint pt1, ClipperLib.IntPoint pt2, ClipperLib.IntPoint pt3, double distSqrd)
			{
				if (Math.Abs(pt1.X - pt2.X) > Math.Abs(pt1.Y - pt2.Y))
				{
					if (pt1.X > pt2.X == pt1.X < pt3.X)
					{
						return ClipperLib.Clipper.DistanceFromLineSqrd(pt1, pt2, pt3) < distSqrd;
					}
					if (pt2.X > pt1.X == pt2.X < pt3.X)
					{
						return ClipperLib.Clipper.DistanceFromLineSqrd(pt2, pt1, pt3) < distSqrd;
					}
					return ClipperLib.Clipper.DistanceFromLineSqrd(pt3, pt1, pt2) < distSqrd;
				}
				else
				{
					if (pt1.Y > pt2.Y == pt1.Y < pt3.Y)
					{
						return ClipperLib.Clipper.DistanceFromLineSqrd(pt1, pt2, pt3) < distSqrd;
					}
					if (pt2.Y > pt1.Y == pt2.Y < pt3.Y)
					{
						return ClipperLib.Clipper.DistanceFromLineSqrd(pt2, pt1, pt3) < distSqrd;
					}
					return ClipperLib.Clipper.DistanceFromLineSqrd(pt3, pt1, pt2) < distSqrd;
				}
			}

			// Token: 0x06000500 RID: 1280 RVA: 0x0001F9E8 File Offset: 0x0001DBE8
			private static bool PointsAreClose(ClipperLib.IntPoint pt1, ClipperLib.IntPoint pt2, double distSqrd)
			{
				double num = (double)pt1.X - (double)pt2.X;
				double dy = (double)pt1.Y - (double)pt2.Y;
				return num * num + dy * dy <= distSqrd;
			}

			// Token: 0x06000501 RID: 1281 RVA: 0x0001FA20 File Offset: 0x0001DC20
			private static ClipperLib.OutPt ExcludeOp(ClipperLib.OutPt op)
			{
				ClipperLib.OutPt result = op.Prev;
				result.Next = op.Next;
				op.Next.Prev = result;
				result.Idx = 0;
				return result;
			}

			// Token: 0x06000502 RID: 1282 RVA: 0x0001FA54 File Offset: 0x0001DC54
			public static List<ClipperLib.IntPoint> CleanPolygon(List<ClipperLib.IntPoint> path, double distance = 1.415)
			{
				int cnt = path.Count;
				if (cnt == 0)
				{
					return new List<ClipperLib.IntPoint>();
				}
				ClipperLib.OutPt[] outPts = new ClipperLib.OutPt[cnt];
				for (int i = 0; i < cnt; i++)
				{
					outPts[i] = new ClipperLib.OutPt();
				}
				for (int j = 0; j < cnt; j++)
				{
					outPts[j].Pt = path[j];
					outPts[j].Next = outPts[(j + 1) % cnt];
					outPts[j].Next.Prev = outPts[j];
					outPts[j].Idx = 0;
				}
				double distSqrd = distance * distance;
				ClipperLib.OutPt op = outPts[0];
				while (op.Idx == 0 && op.Next != op.Prev)
				{
					if (ClipperLib.Clipper.PointsAreClose(op.Pt, op.Prev.Pt, distSqrd))
					{
						op = ClipperLib.Clipper.ExcludeOp(op);
						cnt--;
					}
					else if (ClipperLib.Clipper.PointsAreClose(op.Prev.Pt, op.Next.Pt, distSqrd))
					{
						ClipperLib.Clipper.ExcludeOp(op.Next);
						op = ClipperLib.Clipper.ExcludeOp(op);
						cnt -= 2;
					}
					else if (ClipperLib.Clipper.SlopesNearCollinear(op.Prev.Pt, op.Pt, op.Next.Pt, distSqrd))
					{
						op = ClipperLib.Clipper.ExcludeOp(op);
						cnt--;
					}
					else
					{
						op.Idx = 1;
						op = op.Next;
					}
				}
				if (cnt < 3)
				{
					cnt = 0;
				}
				List<ClipperLib.IntPoint> result = new List<ClipperLib.IntPoint>(cnt);
				for (int k = 0; k < cnt; k++)
				{
					result.Add(op.Pt);
					op = op.Next;
				}
				return result;
			}

			// Token: 0x06000503 RID: 1283 RVA: 0x0001FBD8 File Offset: 0x0001DDD8
			public static List<List<ClipperLib.IntPoint>> CleanPolygons(List<List<ClipperLib.IntPoint>> polys, double distance = 1.415)
			{
				List<List<ClipperLib.IntPoint>> result = new List<List<ClipperLib.IntPoint>>(polys.Count);
				for (int i = 0; i < polys.Count; i++)
				{
					result.Add(ClipperLib.Clipper.CleanPolygon(polys[i], distance));
				}
				return result;
			}

			// Token: 0x06000504 RID: 1284 RVA: 0x0001FC18 File Offset: 0x0001DE18
			internal static List<List<ClipperLib.IntPoint>> Minkowski(List<ClipperLib.IntPoint> pattern, List<ClipperLib.IntPoint> path, bool IsSum, bool IsClosed)
			{
				int delta = (IsClosed ? 1 : 0);
				int polyCnt = pattern.Count;
				int pathCnt = path.Count;
				List<List<ClipperLib.IntPoint>> result = new List<List<ClipperLib.IntPoint>>(pathCnt);
				if (IsSum)
				{
					for (int i = 0; i < pathCnt; i++)
					{
						List<ClipperLib.IntPoint> p = new List<ClipperLib.IntPoint>(polyCnt);
						foreach (ClipperLib.IntPoint ip in pattern)
						{
							p.Add(new ClipperLib.IntPoint(path[i].X + ip.X, path[i].Y + ip.Y));
						}
						result.Add(p);
					}
				}
				else
				{
					for (int j = 0; j < pathCnt; j++)
					{
						List<ClipperLib.IntPoint> p2 = new List<ClipperLib.IntPoint>(polyCnt);
						foreach (ClipperLib.IntPoint ip2 in pattern)
						{
							p2.Add(new ClipperLib.IntPoint(path[j].X - ip2.X, path[j].Y - ip2.Y));
						}
						result.Add(p2);
					}
				}
				List<List<ClipperLib.IntPoint>> quads = new List<List<ClipperLib.IntPoint>>((pathCnt + delta) * (polyCnt + 1));
				for (int k = 0; k < pathCnt - 1 + delta; k++)
				{
					for (int l = 0; l < polyCnt; l++)
					{
						List<ClipperLib.IntPoint> quad = new List<ClipperLib.IntPoint>(4);
						quad.Add(result[k % pathCnt][l % polyCnt]);
						quad.Add(result[(k + 1) % pathCnt][l % polyCnt]);
						quad.Add(result[(k + 1) % pathCnt][(l + 1) % polyCnt]);
						quad.Add(result[k % pathCnt][(l + 1) % polyCnt]);
						if (!ClipperLib.Clipper.Orientation(quad))
						{
							quad.Reverse();
						}
						quads.Add(quad);
					}
				}
				return quads;
			}

			// Token: 0x06000505 RID: 1285 RVA: 0x0001FE40 File Offset: 0x0001E040
			public static List<List<ClipperLib.IntPoint>> MinkowskiSum(List<ClipperLib.IntPoint> pattern, List<ClipperLib.IntPoint> path, bool pathIsClosed)
			{
				List<List<ClipperLib.IntPoint>> paths = ClipperLib.Clipper.Minkowski(pattern, path, true, pathIsClosed);
				ClipperLib.Clipper clipper = new ClipperLib.Clipper(0);
				clipper.AddPaths(paths, ClipperLib.PolyType.ptSubject, true);
				clipper.Execute(ClipperLib.ClipType.ctUnion, paths, ClipperLib.PolyFillType.pftNonZero, ClipperLib.PolyFillType.pftNonZero);
				return paths;
			}

			// Token: 0x06000506 RID: 1286 RVA: 0x0001FE74 File Offset: 0x0001E074
			private static List<ClipperLib.IntPoint> TranslatePath(List<ClipperLib.IntPoint> path, ClipperLib.IntPoint delta)
			{
				List<ClipperLib.IntPoint> outPath = new List<ClipperLib.IntPoint>(path.Count);
				for (int i = 0; i < path.Count; i++)
				{
					outPath.Add(new ClipperLib.IntPoint(path[i].X + delta.X, path[i].Y + delta.Y));
				}
				return outPath;
			}

			// Token: 0x06000507 RID: 1287 RVA: 0x0001FED0 File Offset: 0x0001E0D0
			public static List<List<ClipperLib.IntPoint>> MinkowskiSum(List<ClipperLib.IntPoint> pattern, List<List<ClipperLib.IntPoint>> paths, bool pathIsClosed)
			{
				List<List<ClipperLib.IntPoint>> solution = new List<List<ClipperLib.IntPoint>>();
				ClipperLib.Clipper c = new ClipperLib.Clipper(0);
				for (int i = 0; i < paths.Count; i++)
				{
					List<List<ClipperLib.IntPoint>> tmp = ClipperLib.Clipper.Minkowski(pattern, paths[i], true, pathIsClosed);
					c.AddPaths(tmp, ClipperLib.PolyType.ptSubject, true);
					if (pathIsClosed)
					{
						List<ClipperLib.IntPoint> path = ClipperLib.Clipper.TranslatePath(paths[i], pattern[0]);
						c.AddPath(path, ClipperLib.PolyType.ptClip, true);
					}
				}
				c.Execute(ClipperLib.ClipType.ctUnion, solution, ClipperLib.PolyFillType.pftNonZero, ClipperLib.PolyFillType.pftNonZero);
				return solution;
			}

			// Token: 0x06000508 RID: 1288 RVA: 0x0001FF44 File Offset: 0x0001E144
			public static List<List<ClipperLib.IntPoint>> MinkowskiDiff(List<ClipperLib.IntPoint> poly1, List<ClipperLib.IntPoint> poly2)
			{
				List<List<ClipperLib.IntPoint>> paths = ClipperLib.Clipper.Minkowski(poly1, poly2, false, true);
				ClipperLib.Clipper clipper = new ClipperLib.Clipper(0);
				clipper.AddPaths(paths, ClipperLib.PolyType.ptSubject, true);
				clipper.Execute(ClipperLib.ClipType.ctUnion, paths, ClipperLib.PolyFillType.pftNonZero, ClipperLib.PolyFillType.pftNonZero);
				return paths;
			}

			// Token: 0x06000509 RID: 1289 RVA: 0x0001FF78 File Offset: 0x0001E178
			public static List<List<ClipperLib.IntPoint>> PolyTreeToPaths(ClipperLib.PolyTree polytree)
			{
				List<List<ClipperLib.IntPoint>> result = new List<List<ClipperLib.IntPoint>>();
				result.Capacity = polytree.Total;
				ClipperLib.Clipper.AddPolyNodeToPaths(polytree, ClipperLib.Clipper.NodeType.ntAny, result);
				return result;
			}

			// Token: 0x0600050A RID: 1290 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
			internal static void AddPolyNodeToPaths(ClipperLib.PolyNode polynode, ClipperLib.Clipper.NodeType nt, List<List<ClipperLib.IntPoint>> paths)
			{
				bool match = true;
				if (nt != ClipperLib.Clipper.NodeType.ntOpen)
				{
					if (nt == ClipperLib.Clipper.NodeType.ntClosed)
					{
						match = !polynode.IsOpen;
					}
					if (polynode.m_polygon.Count > 0 && match)
					{
						paths.Add(polynode.m_polygon);
					}
					foreach (ClipperLib.PolyNode polyNode in polynode.Childs)
					{
						ClipperLib.Clipper.AddPolyNodeToPaths(polyNode, nt, paths);
					}
					return;
				}
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x00020028 File Offset: 0x0001E228
			public static List<List<ClipperLib.IntPoint>> OpenPathsFromPolyTree(ClipperLib.PolyTree polytree)
			{
				List<List<ClipperLib.IntPoint>> result = new List<List<ClipperLib.IntPoint>>();
				result.Capacity = polytree.ChildCount;
				for (int i = 0; i < polytree.ChildCount; i++)
				{
					if (polytree.Childs[i].IsOpen)
					{
						result.Add(polytree.Childs[i].m_polygon);
					}
				}
				return result;
			}

			// Token: 0x0600050C RID: 1292 RVA: 0x00020084 File Offset: 0x0001E284
			public static List<List<ClipperLib.IntPoint>> ClosedPathsFromPolyTree(ClipperLib.PolyTree polytree)
			{
				List<List<ClipperLib.IntPoint>> result = new List<List<ClipperLib.IntPoint>>();
				result.Capacity = polytree.Total;
				ClipperLib.Clipper.AddPolyNodeToPaths(polytree, ClipperLib.Clipper.NodeType.ntClosed, result);
				return result;
			}

			// Token: 0x04000472 RID: 1138
			public const int ioReverseSolution = 1;

			// Token: 0x04000473 RID: 1139
			public const int ioStrictlySimple = 2;

			// Token: 0x04000474 RID: 1140
			public const int ioPreserveCollinear = 4;

			// Token: 0x04000475 RID: 1141
			private ClipperLib.ClipType m_ClipType;

			// Token: 0x04000476 RID: 1142
			private ClipperLib.Maxima m_Maxima;

			// Token: 0x04000477 RID: 1143
			private ClipperLib.TEdge m_SortedEdges;

			// Token: 0x04000478 RID: 1144
			private List<ClipperLib.IntersectNode> m_IntersectList;

			// Token: 0x04000479 RID: 1145
			private IComparer<ClipperLib.IntersectNode> m_IntersectNodeComparer;

			// Token: 0x0400047A RID: 1146
			private bool m_ExecuteLocked;

			// Token: 0x0400047B RID: 1147
			private ClipperLib.PolyFillType m_ClipFillType;

			// Token: 0x0400047C RID: 1148
			private ClipperLib.PolyFillType m_SubjFillType;

			// Token: 0x0400047D RID: 1149
			private List<ClipperLib.Join> m_Joins;

			// Token: 0x0400047E RID: 1150
			private List<ClipperLib.Join> m_GhostJoins;

			// Token: 0x0400047F RID: 1151
			private bool m_UsingPolyTree;

			// Token: 0x020000DE RID: 222
			internal enum NodeType
			{
				// Token: 0x04000483 RID: 1155
				ntAny,
				// Token: 0x04000484 RID: 1156
				ntOpen,
				// Token: 0x04000485 RID: 1157
				ntClosed
			}
		}

		// Token: 0x020000DF RID: 223
		public class ClipperOffset
		{
			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x0600050D RID: 1293 RVA: 0x000200AC File Offset: 0x0001E2AC
			// (set) Token: 0x0600050E RID: 1294 RVA: 0x000200B4 File Offset: 0x0001E2B4
			public double ArcTolerance { get; set; }

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x0600050F RID: 1295 RVA: 0x000200BD File Offset: 0x0001E2BD
			// (set) Token: 0x06000510 RID: 1296 RVA: 0x000200C5 File Offset: 0x0001E2C5
			public double MiterLimit { get; set; }

			// Token: 0x06000511 RID: 1297 RVA: 0x000200CE File Offset: 0x0001E2CE
			public ClipperOffset(double miterLimit = 2.0, double arcTolerance = 0.25)
			{
				this.MiterLimit = miterLimit;
				this.ArcTolerance = arcTolerance;
				this.m_lowest.X = -1L;
			}

			// Token: 0x06000512 RID: 1298 RVA: 0x00020107 File Offset: 0x0001E307
			public void Clear()
			{
				this.m_polyNodes.Childs.Clear();
				this.m_lowest.X = -1L;
			}

			// Token: 0x06000513 RID: 1299 RVA: 0x0001D6D1 File Offset: 0x0001B8D1
			internal static long Round(double value)
			{
				if (value >= 0.0)
				{
					return (long)(value + 0.5);
				}
				return (long)(value - 0.5);
			}

			// Token: 0x06000514 RID: 1300 RVA: 0x00020128 File Offset: 0x0001E328
			public void AddPath(List<ClipperLib.IntPoint> path, ClipperLib.JoinType joinType, ClipperLib.EndType endType)
			{
				int highI = path.Count - 1;
				if (highI < 0)
				{
					return;
				}
				ClipperLib.PolyNode newNode = new ClipperLib.PolyNode();
				newNode.m_jointype = joinType;
				newNode.m_endtype = endType;
				if (endType != ClipperLib.EndType.etClosedLine)
				{
					if (endType != ClipperLib.EndType.etClosedPolygon)
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
				if (endType == ClipperLib.EndType.etClosedPolygon && i < 2)
				{
					return;
				}
				this.m_polyNodes.AddChild(newNode);
				if (endType != ClipperLib.EndType.etClosedPolygon)
				{
					return;
				}
				if (this.m_lowest.X < 0L)
				{
					this.m_lowest = new ClipperLib.IntPoint((long)(this.m_polyNodes.ChildCount - 1), (long)j);
					return;
				}
				ClipperLib.IntPoint ip = this.m_polyNodes.Childs[(int)this.m_lowest.X].m_polygon[(int)this.m_lowest.Y];
				if (newNode.m_polygon[j].Y > ip.Y || (newNode.m_polygon[j].Y == ip.Y && newNode.m_polygon[j].X < ip.X))
				{
					this.m_lowest = new ClipperLib.IntPoint((long)(this.m_polyNodes.ChildCount - 1), (long)j);
				}
			}

			// Token: 0x06000515 RID: 1301 RVA: 0x0002032C File Offset: 0x0001E52C
			public void AddPaths(List<List<ClipperLib.IntPoint>> paths, ClipperLib.JoinType joinType, ClipperLib.EndType endType)
			{
				foreach (List<ClipperLib.IntPoint> p in paths)
				{
					this.AddPath(p, joinType, endType);
				}
			}

			// Token: 0x06000516 RID: 1302 RVA: 0x0002037C File Offset: 0x0001E57C
			private void FixOrientations()
			{
				if (this.m_lowest.X >= 0L && !ClipperLib.Clipper.Orientation(this.m_polyNodes.Childs[(int)this.m_lowest.X].m_polygon))
				{
					for (int i = 0; i < this.m_polyNodes.ChildCount; i++)
					{
						ClipperLib.PolyNode node = this.m_polyNodes.Childs[i];
						if (node.m_endtype == ClipperLib.EndType.etClosedPolygon || (node.m_endtype == ClipperLib.EndType.etClosedLine && ClipperLib.Clipper.Orientation(node.m_polygon)))
						{
							node.m_polygon.Reverse();
						}
					}
					return;
				}
				for (int j = 0; j < this.m_polyNodes.ChildCount; j++)
				{
					ClipperLib.PolyNode node2 = this.m_polyNodes.Childs[j];
					if (node2.m_endtype == ClipperLib.EndType.etClosedLine && !ClipperLib.Clipper.Orientation(node2.m_polygon))
					{
						node2.m_polygon.Reverse();
					}
				}
			}

			// Token: 0x06000517 RID: 1303 RVA: 0x0002045C File Offset: 0x0001E65C
			internal static ClipperLib.DoublePoint GetUnitNormal(ClipperLib.IntPoint pt1, ClipperLib.IntPoint pt2)
			{
				double dx = (double)(pt2.X - pt1.X);
				double dy = (double)(pt2.Y - pt1.Y);
				if (dx == 0.0 && dy == 0.0)
				{
					return default(ClipperLib.DoublePoint);
				}
				double f = 1.0 / Math.Sqrt(dx * dx + dy * dy);
				dx *= f;
				dy *= f;
				return new ClipperLib.DoublePoint(dy, -dx);
			}

			// Token: 0x06000518 RID: 1304 RVA: 0x000204D0 File Offset: 0x0001E6D0
			private void DoOffset(double delta)
			{
				this.m_destPolys = new List<List<ClipperLib.IntPoint>>();
				this.m_delta = delta;
				if (ClipperLib.ClipperBase.near_zero(delta))
				{
					this.m_destPolys.Capacity = this.m_polyNodes.ChildCount;
					for (int i = 0; i < this.m_polyNodes.ChildCount; i++)
					{
						ClipperLib.PolyNode node = this.m_polyNodes.Childs[i];
						if (node.m_endtype == ClipperLib.EndType.etClosedPolygon)
						{
							this.m_destPolys.Add(node.m_polygon);
						}
					}
					return;
				}
				if (this.MiterLimit > 2.0)
				{
					this.m_miterLim = 2.0 / (this.MiterLimit * this.MiterLimit);
				}
				else
				{
					this.m_miterLim = 0.5;
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
					ClipperLib.PolyNode node2 = this.m_polyNodes.Childs[j];
					this.m_srcPoly = node2.m_polygon;
					int len = this.m_srcPoly.Count;
					if (len != 0 && (delta > 0.0 || (len >= 3 && node2.m_endtype == ClipperLib.EndType.etClosedPolygon)))
					{
						this.m_destPoly = new List<ClipperLib.IntPoint>();
						if (len == 1)
						{
							if (node2.m_jointype == ClipperLib.JoinType.jtRound)
							{
								double X = 1.0;
								double Y = 0.0;
								int k = 1;
								while ((double)k <= steps)
								{
									this.m_destPoly.Add(new ClipperLib.IntPoint(ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[0].X + X * delta), ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[0].Y + Y * delta)));
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
									this.m_destPoly.Add(new ClipperLib.IntPoint(ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[0].X + X2 * delta), ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[0].Y + Y2 * delta)));
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
								this.m_normals.Add(ClipperLib.ClipperOffset.GetUnitNormal(this.m_srcPoly[m], this.m_srcPoly[m + 1]));
							}
							if (node2.m_endtype == ClipperLib.EndType.etClosedLine || node2.m_endtype == ClipperLib.EndType.etClosedPolygon)
							{
								this.m_normals.Add(ClipperLib.ClipperOffset.GetUnitNormal(this.m_srcPoly[len - 1], this.m_srcPoly[0]));
							}
							else
							{
								this.m_normals.Add(new ClipperLib.DoublePoint(this.m_normals[len - 2]));
							}
							if (node2.m_endtype == ClipperLib.EndType.etClosedPolygon)
							{
								int n = len - 1;
								for (int j2 = 0; j2 < len; j2++)
								{
									this.OffsetPoint(j2, ref n, node2.m_jointype);
								}
								this.m_destPolys.Add(this.m_destPoly);
							}
							else if (node2.m_endtype == ClipperLib.EndType.etClosedLine)
							{
								int k2 = len - 1;
								for (int j3 = 0; j3 < len; j3++)
								{
									this.OffsetPoint(j3, ref k2, node2.m_jointype);
								}
								this.m_destPolys.Add(this.m_destPoly);
								this.m_destPoly = new List<ClipperLib.IntPoint>();
								ClipperLib.DoublePoint n2 = this.m_normals[len - 1];
								for (int j4 = len - 1; j4 > 0; j4--)
								{
									this.m_normals[j4] = new ClipperLib.DoublePoint(-this.m_normals[j4 - 1].X, -this.m_normals[j4 - 1].Y);
								}
								this.m_normals[0] = new ClipperLib.DoublePoint(-n2.X, -n2.Y);
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
								if (node2.m_endtype == ClipperLib.EndType.etOpenButt)
								{
									int j7 = len - 1;
									ClipperLib.IntPoint pt = new ClipperLib.IntPoint(ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j7].X + this.m_normals[j7].X * delta), ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j7].Y + this.m_normals[j7].Y * delta));
									this.m_destPoly.Add(pt);
									pt = new ClipperLib.IntPoint(ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j7].X - this.m_normals[j7].X * delta), ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j7].Y - this.m_normals[j7].Y * delta));
									this.m_destPoly.Add(pt);
								}
								else
								{
									int j8 = len - 1;
									k3 = len - 2;
									this.m_sinA = 0.0;
									this.m_normals[j8] = new ClipperLib.DoublePoint(-this.m_normals[j8].X, -this.m_normals[j8].Y);
									if (node2.m_endtype == ClipperLib.EndType.etOpenSquare)
									{
										this.DoSquare(j8, k3);
									}
									else
									{
										this.DoRound(j8, k3);
									}
								}
								for (int j9 = len - 1; j9 > 0; j9--)
								{
									this.m_normals[j9] = new ClipperLib.DoublePoint(-this.m_normals[j9 - 1].X, -this.m_normals[j9 - 1].Y);
								}
								this.m_normals[0] = new ClipperLib.DoublePoint(-this.m_normals[1].X, -this.m_normals[1].Y);
								k3 = len - 1;
								for (int j10 = k3 - 1; j10 > 0; j10--)
								{
									this.OffsetPoint(j10, ref k3, node2.m_jointype);
								}
								if (node2.m_endtype == ClipperLib.EndType.etOpenButt)
								{
									ClipperLib.IntPoint pt = new ClipperLib.IntPoint(ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[0].X - this.m_normals[0].X * delta), ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[0].Y - this.m_normals[0].Y * delta));
									this.m_destPoly.Add(pt);
									pt = new ClipperLib.IntPoint(ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[0].X + this.m_normals[0].X * delta), ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[0].Y + this.m_normals[0].Y * delta));
									this.m_destPoly.Add(pt);
								}
								else
								{
									k3 = 1;
									this.m_sinA = 0.0;
									if (node2.m_endtype == ClipperLib.EndType.etOpenSquare)
									{
										this.DoSquare(0, 1);
									}
									else
									{
										this.DoRound(0, 1);
									}
								}
								this.m_destPolys.Add(this.m_destPoly);
							}
						}
					}
				}
			}

			// Token: 0x06000519 RID: 1305 RVA: 0x00020DD0 File Offset: 0x0001EFD0
			public void Execute(ref List<List<ClipperLib.IntPoint>> solution, double delta)
			{
				solution.Clear();
				this.FixOrientations();
				this.DoOffset(delta);
				ClipperLib.Clipper clpr = new ClipperLib.Clipper(0);
				clpr.AddPaths(this.m_destPolys, ClipperLib.PolyType.ptSubject, true);
				if (delta > 0.0)
				{
					clpr.Execute(ClipperLib.ClipType.ctUnion, solution, ClipperLib.PolyFillType.pftPositive, ClipperLib.PolyFillType.pftPositive);
					return;
				}
				ClipperLib.IntRect r = ClipperLib.ClipperBase.GetBounds(this.m_destPolys);
				clpr.AddPath(new List<ClipperLib.IntPoint>(4)
				{
					new ClipperLib.IntPoint(r.left - 10L, r.bottom + 10L),
					new ClipperLib.IntPoint(r.right + 10L, r.bottom + 10L),
					new ClipperLib.IntPoint(r.right + 10L, r.top - 10L),
					new ClipperLib.IntPoint(r.left - 10L, r.top - 10L)
				}, ClipperLib.PolyType.ptSubject, true);
				clpr.ReverseSolution = true;
				clpr.Execute(ClipperLib.ClipType.ctUnion, solution, ClipperLib.PolyFillType.pftNegative, ClipperLib.PolyFillType.pftNegative);
				if (solution.Count > 0)
				{
					solution.RemoveAt(0);
				}
			}

			// Token: 0x0600051A RID: 1306 RVA: 0x00020EE0 File Offset: 0x0001F0E0
			public void Execute(ref ClipperLib.PolyTree solution, double delta)
			{
				solution.Clear();
				this.FixOrientations();
				this.DoOffset(delta);
				ClipperLib.Clipper clpr = new ClipperLib.Clipper(0);
				clpr.AddPaths(this.m_destPolys, ClipperLib.PolyType.ptSubject, true);
				if (delta > 0.0)
				{
					clpr.Execute(ClipperLib.ClipType.ctUnion, solution, ClipperLib.PolyFillType.pftPositive, ClipperLib.PolyFillType.pftPositive);
					return;
				}
				ClipperLib.IntRect r = ClipperLib.ClipperBase.GetBounds(this.m_destPolys);
				clpr.AddPath(new List<ClipperLib.IntPoint>(4)
				{
					new ClipperLib.IntPoint(r.left - 10L, r.bottom + 10L),
					new ClipperLib.IntPoint(r.right + 10L, r.bottom + 10L),
					new ClipperLib.IntPoint(r.right + 10L, r.top - 10L),
					new ClipperLib.IntPoint(r.left - 10L, r.top - 10L)
				}, ClipperLib.PolyType.ptSubject, true);
				clpr.ReverseSolution = true;
				clpr.Execute(ClipperLib.ClipType.ctUnion, solution, ClipperLib.PolyFillType.pftNegative, ClipperLib.PolyFillType.pftNegative);
				if (solution.ChildCount == 1 && solution.Childs[0].ChildCount > 0)
				{
					ClipperLib.PolyNode outerNode = solution.Childs[0];
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

			// Token: 0x0600051B RID: 1307 RVA: 0x0002107C File Offset: 0x0001F27C
			private void OffsetPoint(int j, ref int k, ClipperLib.JoinType jointype)
			{
				this.m_sinA = this.m_normals[k].X * this.m_normals[j].Y - this.m_normals[j].X * this.m_normals[k].Y;
				if (Math.Abs(this.m_sinA * this.m_delta) < 1.0)
				{
					if (this.m_normals[k].X * this.m_normals[j].X + this.m_normals[j].Y * this.m_normals[k].Y > 0.0)
					{
						this.m_destPoly.Add(new ClipperLib.IntPoint(ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_normals[k].X * this.m_delta), ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_normals[k].Y * this.m_delta)));
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
					this.m_destPoly.Add(new ClipperLib.IntPoint(ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_normals[k].X * this.m_delta), ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_normals[k].Y * this.m_delta)));
					this.m_destPoly.Add(this.m_srcPoly[j]);
					this.m_destPoly.Add(new ClipperLib.IntPoint(ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_normals[j].X * this.m_delta), ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_normals[j].Y * this.m_delta)));
				}
				else
				{
					switch (jointype)
					{
					case ClipperLib.JoinType.jtSquare:
						this.DoSquare(j, k);
						break;
					case ClipperLib.JoinType.jtRound:
						this.DoRound(j, k);
						break;
					case ClipperLib.JoinType.jtMiter:
					{
						double r = 1.0 + (this.m_normals[j].X * this.m_normals[k].X + this.m_normals[j].Y * this.m_normals[k].Y);
						if (r >= this.m_miterLim)
						{
							this.DoMiter(j, k, r);
						}
						else
						{
							this.DoSquare(j, k);
						}
						break;
					}
					}
				}
				k = j;
			}

			// Token: 0x0600051C RID: 1308 RVA: 0x000213C0 File Offset: 0x0001F5C0
			internal void DoSquare(int j, int k)
			{
				double dx = Math.Tan(Math.Atan2(this.m_sinA, this.m_normals[k].X * this.m_normals[j].X + this.m_normals[k].Y * this.m_normals[j].Y) / 4.0);
				this.m_destPoly.Add(new ClipperLib.IntPoint(ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_delta * (this.m_normals[k].X - this.m_normals[k].Y * dx)), ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_delta * (this.m_normals[k].Y + this.m_normals[k].X * dx))));
				this.m_destPoly.Add(new ClipperLib.IntPoint(ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_delta * (this.m_normals[j].X + this.m_normals[j].Y * dx)), ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_delta * (this.m_normals[j].Y - this.m_normals[j].X * dx))));
			}

			// Token: 0x0600051D RID: 1309 RVA: 0x00021560 File Offset: 0x0001F760
			internal void DoMiter(int j, int k, double r)
			{
				double q = this.m_delta / r;
				this.m_destPoly.Add(new ClipperLib.IntPoint(ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].X + (this.m_normals[k].X + this.m_normals[j].X) * q), ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].Y + (this.m_normals[k].Y + this.m_normals[j].Y) * q)));
			}

			// Token: 0x0600051E RID: 1310 RVA: 0x00021600 File Offset: 0x0001F800
			internal void DoRound(int j, int k)
			{
				double a = Math.Atan2(this.m_sinA, this.m_normals[k].X * this.m_normals[j].X + this.m_normals[k].Y * this.m_normals[j].Y);
				int steps = Math.Max((int)ClipperLib.ClipperOffset.Round(this.m_StepsPerRad * Math.Abs(a)), 1);
				double X = this.m_normals[k].X;
				double Y = this.m_normals[k].Y;
				for (int i = 0; i < steps; i++)
				{
					this.m_destPoly.Add(new ClipperLib.IntPoint(ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].X + X * this.m_delta), ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].Y + Y * this.m_delta)));
					double num = X;
					X = X * this.m_cos - this.m_sin * Y;
					Y = num * this.m_sin + Y * this.m_cos;
				}
				this.m_destPoly.Add(new ClipperLib.IntPoint(ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].X + this.m_normals[j].X * this.m_delta), ClipperLib.ClipperOffset.Round((double)this.m_srcPoly[j].Y + this.m_normals[j].Y * this.m_delta)));
			}

			// Token: 0x04000486 RID: 1158
			private List<List<ClipperLib.IntPoint>> m_destPolys;

			// Token: 0x04000487 RID: 1159
			private List<ClipperLib.IntPoint> m_srcPoly;

			// Token: 0x04000488 RID: 1160
			private List<ClipperLib.IntPoint> m_destPoly;

			// Token: 0x04000489 RID: 1161
			private List<ClipperLib.DoublePoint> m_normals = new List<ClipperLib.DoublePoint>();

			// Token: 0x0400048A RID: 1162
			private double m_delta;

			// Token: 0x0400048B RID: 1163
			private double m_sinA;

			// Token: 0x0400048C RID: 1164
			private double m_sin;

			// Token: 0x0400048D RID: 1165
			private double m_cos;

			// Token: 0x0400048E RID: 1166
			private double m_miterLim;

			// Token: 0x0400048F RID: 1167
			private double m_StepsPerRad;

			// Token: 0x04000490 RID: 1168
			private ClipperLib.IntPoint m_lowest;

			// Token: 0x04000491 RID: 1169
			private ClipperLib.PolyNode m_polyNodes = new ClipperLib.PolyNode();

			// Token: 0x04000494 RID: 1172
			private const double two_pi = 6.283185307179586;

			// Token: 0x04000495 RID: 1173
			private const double def_arc_tolerance = 0.25;
		}

		// Token: 0x020000E0 RID: 224
		private class ClipperException : Exception
		{
			// Token: 0x0600051F RID: 1311 RVA: 0x00021792 File Offset: 0x0001F992
			public ClipperException(string description)
				: base(description)
			{
			}
		}
	}
}
