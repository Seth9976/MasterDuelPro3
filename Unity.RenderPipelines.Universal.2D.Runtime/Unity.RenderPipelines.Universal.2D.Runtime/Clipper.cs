using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000023 RID: 35
	internal class Clipper : ClipperBase
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00004288 File Offset: 0x00002488
		public Clipper(int InitOptions = 0)
		{
			this.m_Scanbeam = null;
			this.m_Maxima = null;
			this.m_ActiveEdges = null;
			this.m_SortedEdges = null;
			this.m_IntersectList = new List<IntersectNode>();
			this.m_IntersectNodeComparer = new MyIntersectNodeSort();
			this.m_ExecuteLocked = false;
			this.m_UsingPolyTree = false;
			this.m_PolyOuts = new List<OutRec>();
			this.m_Joins = new List<Join>();
			this.m_GhostJoins = new List<Join>();
			this.ReverseSolution = (1 & InitOptions) != 0;
			this.StrictlySimple = (2 & InitOptions) != 0;
			base.PreserveCollinear = (4 & InitOptions) != 0;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004320 File Offset: 0x00002520
		private void InsertMaxima(long X)
		{
			Maxima newMax = new Maxima();
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
			Maxima i = this.m_Maxima;
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000043E9 File Offset: 0x000025E9
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000043F1 File Offset: 0x000025F1
		public int LastIndex { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000043FA File Offset: 0x000025FA
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00004402 File Offset: 0x00002602
		public bool ReverseSolution { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000440B File Offset: 0x0000260B
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00004413 File Offset: 0x00002613
		public bool StrictlySimple { get; set; }

		// Token: 0x0600006B RID: 107 RVA: 0x0000441C File Offset: 0x0000261C
		public bool Execute(ClipTypes clipType, List<List<IntPoint>> solution, PolyFillTypes FillType = PolyFillTypes.pftEvenOdd)
		{
			return this.Execute(clipType, solution, FillType, FillType);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004428 File Offset: 0x00002628
		public bool Execute(ClipTypes clipType, PolyTree polytree, PolyFillTypes FillType = PolyFillTypes.pftEvenOdd)
		{
			return this.Execute(clipType, polytree, FillType, FillType);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004434 File Offset: 0x00002634
		public bool Execute(ClipTypes clipType, List<List<IntPoint>> solution, PolyFillTypes subjFillType, PolyFillTypes clipFillType)
		{
			if (this.m_ExecuteLocked)
			{
				return false;
			}
			if (this.m_HasOpenPaths)
			{
				throw new ClipperException("Error: PolyTree struct is needed for open path clipping.");
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

		// Token: 0x0600006E RID: 110 RVA: 0x000044BC File Offset: 0x000026BC
		public bool Execute(ClipTypes clipType, PolyTree polytree, PolyFillTypes subjFillType, PolyFillTypes clipFillType)
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

		// Token: 0x0600006F RID: 111 RVA: 0x0000452C File Offset: 0x0000272C
		internal void FixHoleLinkage(OutRec outRec)
		{
			if (outRec.FirstLeft == null || (outRec.IsHole != outRec.FirstLeft.IsHole && outRec.FirstLeft.Pts != null))
			{
				return;
			}
			OutRec orfl = outRec.FirstLeft;
			while (orfl != null && (orfl.IsHole == outRec.IsHole || orfl.Pts == null))
			{
				orfl = orfl.FirstLeft;
			}
			outRec.FirstLeft = orfl;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004594 File Offset: 0x00002794
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
					foreach (OutRec outRec in this.m_PolyOuts)
					{
						if (outRec.Pts != null && !outRec.IsOpen && (outRec.IsHole ^ this.ReverseSolution) == this.Area(outRec) > 0.0)
						{
							this.ReversePolyPtLinks(outRec.Pts);
						}
					}
					this.JoinCommonEdges();
					foreach (OutRec outRec2 in this.m_PolyOuts)
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

		// Token: 0x06000071 RID: 113 RVA: 0x00004754 File Offset: 0x00002954
		private void DisposeAllPolyPts()
		{
			for (int i = 0; i < this.m_PolyOuts.Count; i++)
			{
				base.DisposeOutRec(i);
			}
			this.m_PolyOuts.Clear();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000478C File Offset: 0x0000298C
		private void AddJoin(OutPt Op1, OutPt Op2, IntPoint OffPt)
		{
			Join i = new Join();
			i.OutPt1 = Op1;
			i.OutPt2 = Op2;
			i.OffPt = OffPt;
			this.m_Joins.Add(i);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000047C0 File Offset: 0x000029C0
		private void AddGhostJoin(OutPt Op, IntPoint OffPt)
		{
			Join i = new Join();
			i.OutPt1 = Op;
			i.OffPt = OffPt;
			this.m_GhostJoins.Add(i);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000047F0 File Offset: 0x000029F0
		private void InsertLocalMinimaIntoAEL(long botY)
		{
			LocalMinima lm;
			while (base.PopLocalMinima(botY, out lm))
			{
				TEdge lb = lm.LeftBound;
				TEdge rb = lm.RightBound;
				OutPt Op = null;
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
					if (ClipperBase.IsHorizontal(rb))
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
					if (Op != null && ClipperBase.IsHorizontal(rb) && this.m_GhostJoins.Count > 0 && rb.WindDelta != 0)
					{
						for (int i = 0; i < this.m_GhostJoins.Count; i++)
						{
							Join j = this.m_GhostJoins[i];
							if (this.HorzSegmentsOverlap(j.OutPt1.Pt.X, j.OffPt.X, rb.Bot.X, rb.Top.X))
							{
								this.AddJoin(j.OutPt1, Op, j.OffPt);
							}
						}
					}
					if (lb.OutIdx >= 0 && lb.PrevInAEL != null && lb.PrevInAEL.Curr.X == lb.Bot.X && lb.PrevInAEL.OutIdx >= 0 && ClipperBase.SlopesEqual(lb.PrevInAEL.Curr, lb.PrevInAEL.Top, lb.Curr, lb.Top, this.m_UseFullRange) && lb.WindDelta != 0 && lb.PrevInAEL.WindDelta != 0)
					{
						OutPt Op2 = this.AddOutPt(lb.PrevInAEL, lb.Bot);
						this.AddJoin(Op, Op2, lb.Top);
					}
					if (lb.NextInAEL != rb)
					{
						if (rb.OutIdx >= 0 && rb.PrevInAEL.OutIdx >= 0 && ClipperBase.SlopesEqual(rb.PrevInAEL.Curr, rb.PrevInAEL.Top, rb.Curr, rb.Top, this.m_UseFullRange) && rb.WindDelta != 0 && rb.PrevInAEL.WindDelta != 0)
						{
							OutPt Op3 = this.AddOutPt(rb.PrevInAEL, rb.Bot);
							this.AddJoin(Op, Op3, rb.Top);
						}
						TEdge e = lb.NextInAEL;
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

		// Token: 0x06000075 RID: 117 RVA: 0x00004B38 File Offset: 0x00002D38
		private void InsertEdgeIntoAEL(TEdge edge, TEdge startEdge)
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

		// Token: 0x06000076 RID: 118 RVA: 0x00004BF8 File Offset: 0x00002DF8
		private bool E2InsertsBeforeE1(TEdge e1, TEdge e2)
		{
			if (e2.Curr.X != e1.Curr.X)
			{
				return e2.Curr.X < e1.Curr.X;
			}
			if (e2.Top.Y > e1.Top.Y)
			{
				return e2.Top.X < Clipper.TopX(e1, e2.Top.Y);
			}
			return e1.Top.X > Clipper.TopX(e2, e1.Top.Y);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004C8B File Offset: 0x00002E8B
		private bool IsEvenOddFillType(TEdge edge)
		{
			if (edge.PolyTyp == PolyTypes.ptSubject)
			{
				return this.m_SubjFillType == PolyFillTypes.pftEvenOdd;
			}
			return this.m_ClipFillType == PolyFillTypes.pftEvenOdd;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004CA8 File Offset: 0x00002EA8
		private bool IsEvenOddAltFillType(TEdge edge)
		{
			if (edge.PolyTyp == PolyTypes.ptSubject)
			{
				return this.m_ClipFillType == PolyFillTypes.pftEvenOdd;
			}
			return this.m_SubjFillType == PolyFillTypes.pftEvenOdd;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004CC8 File Offset: 0x00002EC8
		private bool IsContributing(TEdge edge)
		{
			PolyFillTypes pft;
			PolyFillTypes pft2;
			if (edge.PolyTyp == PolyTypes.ptSubject)
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
			case PolyFillTypes.pftEvenOdd:
				if (edge.WindDelta == 0 && edge.WindCnt != 1)
				{
					return false;
				}
				break;
			case PolyFillTypes.pftNonZero:
				if (Math.Abs(edge.WindCnt) != 1)
				{
					return false;
				}
				break;
			case PolyFillTypes.pftPositive:
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
			case ClipTypes.ctIntersection:
				if (pft2 <= PolyFillTypes.pftNonZero)
				{
					return edge.WindCnt2 != 0;
				}
				if (pft2 != PolyFillTypes.pftPositive)
				{
					return edge.WindCnt2 < 0;
				}
				return edge.WindCnt2 > 0;
			case ClipTypes.ctUnion:
				if (pft2 <= PolyFillTypes.pftNonZero)
				{
					return edge.WindCnt2 == 0;
				}
				if (pft2 != PolyFillTypes.pftPositive)
				{
					return edge.WindCnt2 >= 0;
				}
				return edge.WindCnt2 <= 0;
			case ClipTypes.ctDifference:
				if (edge.PolyTyp == PolyTypes.ptSubject)
				{
					if (pft2 <= PolyFillTypes.pftNonZero)
					{
						return edge.WindCnt2 == 0;
					}
					if (pft2 != PolyFillTypes.pftPositive)
					{
						return edge.WindCnt2 >= 0;
					}
					return edge.WindCnt2 <= 0;
				}
				else
				{
					if (pft2 <= PolyFillTypes.pftNonZero)
					{
						return edge.WindCnt2 != 0;
					}
					if (pft2 != PolyFillTypes.pftPositive)
					{
						return edge.WindCnt2 < 0;
					}
					return edge.WindCnt2 > 0;
				}
				break;
			case ClipTypes.ctXor:
				if (edge.WindDelta != 0)
				{
					return true;
				}
				if (pft2 <= PolyFillTypes.pftNonZero)
				{
					return edge.WindCnt2 == 0;
				}
				if (pft2 != PolyFillTypes.pftPositive)
				{
					return edge.WindCnt2 >= 0;
				}
				return edge.WindCnt2 <= 0;
			default:
				return true;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004E58 File Offset: 0x00003058
		private void SetWindingCount(TEdge edge)
		{
			TEdge e = edge.PrevInAEL;
			while (e != null && (e.PolyTyp != edge.PolyTyp || e.WindDelta == 0))
			{
				e = e.PrevInAEL;
			}
			if (e == null)
			{
				PolyFillTypes pft = ((edge.PolyTyp == PolyTypes.ptSubject) ? this.m_SubjFillType : this.m_ClipFillType);
				if (edge.WindDelta == 0)
				{
					edge.WindCnt = ((pft == PolyFillTypes.pftNegative) ? (-1) : 1);
				}
				else
				{
					edge.WindCnt = edge.WindDelta;
				}
				edge.WindCnt2 = 0;
				e = this.m_ActiveEdges;
			}
			else if (edge.WindDelta == 0 && this.m_ClipType != ClipTypes.ctUnion)
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
					for (TEdge e2 = e.PrevInAEL; e2 != null; e2 = e2.PrevInAEL)
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

		// Token: 0x0600007B RID: 123 RVA: 0x000050B0 File Offset: 0x000032B0
		private void AddEdgeToSEL(TEdge edge)
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

		// Token: 0x0600007C RID: 124 RVA: 0x00005104 File Offset: 0x00003304
		internal bool PopEdgeFromSEL(out TEdge e)
		{
			e = this.m_SortedEdges;
			if (e == null)
			{
				return false;
			}
			TEdge tedge = e;
			this.m_SortedEdges = e.NextInSEL;
			if (this.m_SortedEdges != null)
			{
				this.m_SortedEdges.PrevInSEL = null;
			}
			tedge.NextInSEL = null;
			tedge.PrevInSEL = null;
			return true;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005150 File Offset: 0x00003350
		private void CopyAELToSEL()
		{
			TEdge e = this.m_ActiveEdges;
			this.m_SortedEdges = e;
			while (e != null)
			{
				e.PrevInSEL = e.PrevInAEL;
				e.NextInSEL = e.NextInAEL;
				e = e.NextInAEL;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005190 File Offset: 0x00003390
		private void SwapPositionsInSEL(TEdge edge1, TEdge edge2)
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
				TEdge next = edge2.NextInSEL;
				if (next != null)
				{
					next.PrevInSEL = edge1;
				}
				TEdge prev = edge1.PrevInSEL;
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
				TEdge next2 = edge1.NextInSEL;
				if (next2 != null)
				{
					next2.PrevInSEL = edge2;
				}
				TEdge prev2 = edge2.PrevInSEL;
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
				TEdge next3 = edge1.NextInSEL;
				TEdge prev3 = edge1.PrevInSEL;
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

		// Token: 0x0600007F RID: 127 RVA: 0x00005300 File Offset: 0x00003500
		private void AddLocalMaxPoly(TEdge e1, TEdge e2, IntPoint pt)
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

		// Token: 0x06000080 RID: 128 RVA: 0x00005364 File Offset: 0x00003564
		private OutPt AddLocalMinPoly(TEdge e1, TEdge e2, IntPoint pt)
		{
			OutPt result;
			TEdge e3;
			TEdge prevE;
			if (ClipperBase.IsHorizontal(e2) || e1.Dx > e2.Dx)
			{
				result = this.AddOutPt(e1, pt);
				e2.OutIdx = e1.OutIdx;
				e1.Side = EdgeSides.esLeft;
				e2.Side = EdgeSides.esRight;
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
				e1.Side = EdgeSides.esRight;
				e2.Side = EdgeSides.esLeft;
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
				long xPrev = Clipper.TopX(prevE, pt.Y);
				long xE = Clipper.TopX(e3, pt.Y);
				if (xPrev == xE && e3.WindDelta != 0 && prevE.WindDelta != 0 && ClipperBase.SlopesEqual(new IntPoint(xPrev, pt.Y), prevE.Top, new IntPoint(xE, pt.Y), e3.Top, this.m_UseFullRange))
				{
					OutPt outPt = this.AddOutPt(prevE, pt);
					this.AddJoin(result, outPt, e3.Top);
				}
			}
			return result;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000054BC File Offset: 0x000036BC
		private OutPt AddOutPt(TEdge e, IntPoint pt)
		{
			if (e.OutIdx < 0)
			{
				OutRec outRec = base.CreateOutRec();
				outRec.IsOpen = e.WindDelta == 0;
				OutPt newOp = new OutPt();
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
			OutRec outRec2 = this.m_PolyOuts[e.OutIdx];
			OutPt op = outRec2.Pts;
			bool ToFront = e.Side == EdgeSides.esLeft;
			if (ToFront && pt == op.Pt)
			{
				return op;
			}
			if (!ToFront && pt == op.Prev.Pt)
			{
				return op.Prev;
			}
			OutPt newOp2 = new OutPt();
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

		// Token: 0x06000082 RID: 130 RVA: 0x000055E0 File Offset: 0x000037E0
		private OutPt GetLastOutPt(TEdge e)
		{
			OutRec outRec = this.m_PolyOuts[e.OutIdx];
			if (e.Side == EdgeSides.esLeft)
			{
				return outRec.Pts;
			}
			return outRec.Pts.Prev;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000561C File Offset: 0x0000381C
		internal void SwapPoints(ref IntPoint pt1, ref IntPoint pt2)
		{
			IntPoint tmp = new IntPoint(pt1);
			pt1 = pt2;
			pt2 = tmp;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005649 File Offset: 0x00003849
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

		// Token: 0x06000085 RID: 133 RVA: 0x00005674 File Offset: 0x00003874
		private void SetHoleState(TEdge e, OutRec outRec)
		{
			TEdge e2 = e.PrevInAEL;
			TEdge eTmp = null;
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

		// Token: 0x06000086 RID: 134 RVA: 0x000056FB File Offset: 0x000038FB
		private double GetDx(IntPoint pt1, IntPoint pt2)
		{
			if (pt1.Y == pt2.Y)
			{
				return -3.4E+38;
			}
			return (double)(pt2.X - pt1.X) / (double)(pt2.Y - pt1.Y);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005734 File Offset: 0x00003934
		private bool FirstIsBottomPt(OutPt btmPt1, OutPt btmPt2)
		{
			OutPt p = btmPt1.Prev;
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

		// Token: 0x06000088 RID: 136 RVA: 0x0000588C File Offset: 0x00003A8C
		private OutPt GetBottomPt(OutPt pp)
		{
			OutPt dups = null;
			OutPt p;
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

		// Token: 0x06000089 RID: 137 RVA: 0x00005974 File Offset: 0x00003B74
		private OutRec GetLowermostRec(OutRec outRec1, OutRec outRec2)
		{
			if (outRec1.BottomPt == null)
			{
				outRec1.BottomPt = this.GetBottomPt(outRec1.Pts);
			}
			if (outRec2.BottomPt == null)
			{
				outRec2.BottomPt = this.GetBottomPt(outRec2.Pts);
			}
			OutPt bPt = outRec1.BottomPt;
			OutPt bPt2 = outRec2.BottomPt;
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

		// Token: 0x0600008A RID: 138 RVA: 0x00005A4E File Offset: 0x00003C4E
		private bool OutRec1RightOfOutRec2(OutRec outRec1, OutRec outRec2)
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

		// Token: 0x0600008B RID: 139 RVA: 0x00005A64 File Offset: 0x00003C64
		private OutRec GetOutRec(int idx)
		{
			OutRec outrec;
			for (outrec = this.m_PolyOuts[idx]; outrec != this.m_PolyOuts[outrec.Idx]; outrec = this.m_PolyOuts[outrec.Idx])
			{
			}
			return outrec;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005AA8 File Offset: 0x00003CA8
		private void AppendPolygon(TEdge e1, TEdge e2)
		{
			OutRec outRec = this.m_PolyOuts[e1.OutIdx];
			OutRec outRec2 = this.m_PolyOuts[e2.OutIdx];
			OutRec holeStateRec;
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
			OutPt p1_lft = outRec.Pts;
			OutPt p1_rt = p1_lft.Prev;
			OutPt p2_lft = outRec2.Pts;
			OutPt p2_rt = p2_lft.Prev;
			if (e1.Side == EdgeSides.esLeft)
			{
				if (e2.Side == EdgeSides.esLeft)
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
			else if (e2.Side == EdgeSides.esRight)
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
			for (TEdge e3 = this.m_ActiveEdges; e3 != null; e3 = e3.NextInAEL)
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

		// Token: 0x0600008D RID: 141 RVA: 0x00005C90 File Offset: 0x00003E90
		private void ReversePolyPtLinks(OutPt pp)
		{
			if (pp == null)
			{
				return;
			}
			OutPt pp2 = pp;
			do
			{
				OutPt pp3 = pp2.Next;
				pp2.Next = pp2.Prev;
				pp2.Prev = pp3;
				pp2 = pp3;
			}
			while (pp2 != pp);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005CC4 File Offset: 0x00003EC4
		private static void SwapSides(TEdge edge1, TEdge edge2)
		{
			EdgeSides side = edge1.Side;
			edge1.Side = edge2.Side;
			edge2.Side = side;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005CEC File Offset: 0x00003EEC
		private static void SwapPolyIndexes(TEdge edge1, TEdge edge2)
		{
			int outIdx = edge1.OutIdx;
			edge1.OutIdx = edge2.OutIdx;
			edge2.OutIdx = outIdx;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005D14 File Offset: 0x00003F14
		private void IntersectEdges(TEdge e1, TEdge e2, IntPoint pt)
		{
			bool e1Contributing = e1.OutIdx >= 0;
			bool e2Contributing = e2.OutIdx >= 0;
			if (e1.WindDelta == 0 || e2.WindDelta == 0)
			{
				if (e1.WindDelta == 0 && e2.WindDelta == 0)
				{
					return;
				}
				if (e1.PolyTyp == e2.PolyTyp && e1.WindDelta != e2.WindDelta && this.m_ClipType == ClipTypes.ctUnion)
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
					if (e1.WindDelta == 0 && Math.Abs(e2.WindCnt) == 1 && (this.m_ClipType != ClipTypes.ctUnion || e2.WindCnt2 == 0))
					{
						this.AddOutPt(e1, pt);
						if (e1Contributing)
						{
							e1.OutIdx = -1;
							return;
						}
					}
					else if (e2.WindDelta == 0 && Math.Abs(e1.WindCnt) == 1 && (this.m_ClipType != ClipTypes.ctUnion || e1.WindCnt2 == 0))
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
				PolyFillTypes e1FillType;
				PolyFillTypes e1FillType2;
				if (e1.PolyTyp == PolyTypes.ptSubject)
				{
					e1FillType = this.m_SubjFillType;
					e1FillType2 = this.m_ClipFillType;
				}
				else
				{
					e1FillType = this.m_ClipFillType;
					e1FillType2 = this.m_SubjFillType;
				}
				PolyFillTypes e2FillType;
				PolyFillTypes e2FillType2;
				if (e2.PolyTyp == PolyTypes.ptSubject)
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
				if (e1FillType != PolyFillTypes.pftPositive)
				{
					if (e1FillType != PolyFillTypes.pftNegative)
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
				if (e2FillType != PolyFillTypes.pftPositive)
				{
					if (e2FillType != PolyFillTypes.pftNegative)
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
							Clipper.SwapSides(e1, e2);
							Clipper.SwapPolyIndexes(e1, e2);
							return;
						}
					}
					else if (e2Contributing)
					{
						if (e1Wc == 0 || e1Wc == 1)
						{
							this.AddOutPt(e2, pt);
							Clipper.SwapSides(e1, e2);
							Clipper.SwapPolyIndexes(e1, e2);
							return;
						}
					}
					else if ((e1Wc == 0 || e1Wc == 1) && (e2Wc == 0 || e2Wc == 1))
					{
						long e1Wc2;
						if (e1FillType2 != PolyFillTypes.pftPositive)
						{
							if (e1FillType2 != PolyFillTypes.pftNegative)
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
						if (e2FillType2 != PolyFillTypes.pftPositive)
						{
							if (e2FillType2 != PolyFillTypes.pftNegative)
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
							case ClipTypes.ctIntersection:
								if (e1Wc2 > 0L && e2Wc2 > 0L)
								{
									this.AddLocalMinPoly(e1, e2, pt);
									return;
								}
								break;
							case ClipTypes.ctUnion:
								if (e1Wc2 <= 0L && e2Wc2 <= 0L)
								{
									this.AddLocalMinPoly(e1, e2, pt);
									return;
								}
								break;
							case ClipTypes.ctDifference:
								if ((e1.PolyTyp == PolyTypes.ptClip && e1Wc2 > 0L && e2Wc2 > 0L) || (e1.PolyTyp == PolyTypes.ptSubject && e1Wc2 <= 0L && e2Wc2 <= 0L))
								{
									this.AddLocalMinPoly(e1, e2, pt);
									return;
								}
								break;
							case ClipTypes.ctXor:
								this.AddLocalMinPoly(e1, e2, pt);
								return;
							default:
								return;
							}
						}
						else
						{
							Clipper.SwapSides(e1, e2);
						}
					}
					return;
				}
				if ((e1Wc != 0 && e1Wc != 1) || (e2Wc != 0 && e2Wc != 1) || (e1.PolyTyp != e2.PolyTyp && this.m_ClipType != ClipTypes.ctXor))
				{
					this.AddLocalMaxPoly(e1, e2, pt);
					return;
				}
				this.AddOutPt(e1, pt);
				this.AddOutPt(e2, pt);
				Clipper.SwapSides(e1, e2);
				Clipper.SwapPolyIndexes(e1, e2);
				return;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000061D0 File Offset: 0x000043D0
		private void DeleteFromSEL(TEdge e)
		{
			TEdge SelPrev = e.PrevInSEL;
			TEdge SelNext = e.NextInSEL;
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

		// Token: 0x06000092 RID: 146 RVA: 0x00006228 File Offset: 0x00004428
		private void ProcessHorizontals()
		{
			TEdge horzEdge;
			while (this.PopEdgeFromSEL(out horzEdge))
			{
				this.ProcessHorizontal(horzEdge);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006248 File Offset: 0x00004448
		private void GetHorzDirection(TEdge HorzEdge, out Directions Dir, out long Left, out long Right)
		{
			if (HorzEdge.Bot.X < HorzEdge.Top.X)
			{
				Left = HorzEdge.Bot.X;
				Right = HorzEdge.Top.X;
				Dir = Directions.dLeftToRight;
				return;
			}
			Left = HorzEdge.Top.X;
			Right = HorzEdge.Bot.X;
			Dir = Directions.dRightToLeft;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000062AC File Offset: 0x000044AC
		private void ProcessHorizontal(TEdge horzEdge)
		{
			bool IsOpen = horzEdge.WindDelta == 0;
			Directions dir;
			long horzLeft;
			long horzRight;
			this.GetHorzDirection(horzEdge, out dir, out horzLeft, out horzRight);
			TEdge eLastHorz = horzEdge;
			TEdge eMaxPair = null;
			while (eLastHorz.NextInLML != null && ClipperBase.IsHorizontal(eLastHorz.NextInLML))
			{
				eLastHorz = eLastHorz.NextInLML;
			}
			if (eLastHorz.NextInLML == null)
			{
				eMaxPair = this.GetMaximaPair(eLastHorz);
			}
			Maxima currMax = this.m_Maxima;
			if (currMax != null)
			{
				if (dir == Directions.dLeftToRight)
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
			OutPt op = null;
			for (;;)
			{
				bool IsLastHorz = horzEdge == eLastHorz;
				TEdge nextInAEL;
				for (TEdge e = this.GetNextInAEL(horzEdge, dir); e != null; e = nextInAEL)
				{
					if (currMax != null)
					{
						if (dir == Directions.dLeftToRight)
						{
							while (currMax != null)
							{
								if (currMax.X >= e.Curr.X)
								{
									break;
								}
								if (horzEdge.OutIdx >= 0 && !IsOpen)
								{
									this.AddOutPt(horzEdge, new IntPoint(currMax.X, horzEdge.Bot.Y));
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
									this.AddOutPt(horzEdge, new IntPoint(currMax.X, horzEdge.Bot.Y));
								}
								currMax = currMax.Prev;
							}
						}
					}
					if ((dir == Directions.dLeftToRight && e.Curr.X > horzRight) || (dir == Directions.dRightToLeft && e.Curr.X < horzLeft) || (e.Curr.X == horzEdge.Top.X && horzEdge.NextInLML != null && e.Dx < horzEdge.NextInLML.Dx))
					{
						break;
					}
					if (horzEdge.OutIdx >= 0 && !IsOpen)
					{
						op = this.AddOutPt(horzEdge, e.Curr);
						for (TEdge eNextHorz = this.m_SortedEdges; eNextHorz != null; eNextHorz = eNextHorz.NextInSEL)
						{
							if (eNextHorz.OutIdx >= 0 && this.HorzSegmentsOverlap(horzEdge.Bot.X, horzEdge.Top.X, eNextHorz.Bot.X, eNextHorz.Top.X))
							{
								OutPt op2 = this.GetLastOutPt(eNextHorz);
								this.AddJoin(op2, op, eNextHorz.Top);
							}
						}
						this.AddGhostJoin(op, horzEdge.Bot);
					}
					if (e == eMaxPair && IsLastHorz)
					{
						goto Block_28;
					}
					if (dir == Directions.dLeftToRight)
					{
						IntPoint Pt = new IntPoint(e.Curr.X, horzEdge.Curr.Y);
						this.IntersectEdges(horzEdge, e, Pt);
					}
					else
					{
						IntPoint Pt2 = new IntPoint(e.Curr.X, horzEdge.Curr.Y);
						this.IntersectEdges(e, horzEdge, Pt2);
					}
					nextInAEL = this.GetNextInAEL(e, dir);
					base.SwapPositionsInAEL(horzEdge, e);
				}
				if (horzEdge.NextInLML == null || !ClipperBase.IsHorizontal(horzEdge.NextInLML))
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
				for (TEdge eNextHorz2 = this.m_SortedEdges; eNextHorz2 != null; eNextHorz2 = eNextHorz2.NextInSEL)
				{
					if (eNextHorz2.OutIdx >= 0 && this.HorzSegmentsOverlap(horzEdge.Bot.X, horzEdge.Top.X, eNextHorz2.Bot.X, eNextHorz2.Top.X))
					{
						OutPt op3 = this.GetLastOutPt(eNextHorz2);
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
				TEdge ePrev = horzEdge.PrevInAEL;
				TEdge eNext = horzEdge.NextInAEL;
				if (ePrev != null && ePrev.Curr.X == horzEdge.Bot.X && ePrev.Curr.Y == horzEdge.Bot.Y && ePrev.WindDelta != 0 && ePrev.OutIdx >= 0 && ePrev.Curr.Y > ePrev.Top.Y && ClipperBase.SlopesEqual(horzEdge, ePrev, this.m_UseFullRange))
				{
					OutPt op4 = this.AddOutPt(ePrev, horzEdge.Bot);
					this.AddJoin(op, op4, horzEdge.Top);
					return;
				}
				if (eNext != null && eNext.Curr.X == horzEdge.Bot.X && eNext.Curr.Y == horzEdge.Bot.Y && eNext.WindDelta != 0 && eNext.OutIdx >= 0 && eNext.Curr.Y > eNext.Top.Y && ClipperBase.SlopesEqual(horzEdge, eNext, this.m_UseFullRange))
				{
					OutPt op5 = this.AddOutPt(eNext, horzEdge.Bot);
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

		// Token: 0x06000095 RID: 149 RVA: 0x00006896 File Offset: 0x00004A96
		private TEdge GetNextInAEL(TEdge e, Directions Directions)
		{
			if (Directions != Directions.dLeftToRight)
			{
				return e.PrevInAEL;
			}
			return e.NextInAEL;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000068A9 File Offset: 0x00004AA9
		private bool IsMinima(TEdge e)
		{
			return e != null && e.Prev.NextInLML != e && e.Next.NextInLML != e;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000068CF File Offset: 0x00004ACF
		private bool IsMaxima(TEdge e, double Y)
		{
			return e != null && (double)e.Top.Y == Y && e.NextInLML == null;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000068EE File Offset: 0x00004AEE
		private bool IsIntermediate(TEdge e, double Y)
		{
			return (double)e.Top.Y == Y && e.NextInLML != null;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000690C File Offset: 0x00004B0C
		internal TEdge GetMaximaPair(TEdge e)
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

		// Token: 0x0600009A RID: 154 RVA: 0x00006974 File Offset: 0x00004B74
		internal TEdge GetMaximaPairEx(TEdge e)
		{
			TEdge result = this.GetMaximaPair(e);
			if (result == null || result.OutIdx == -2 || (result.NextInAEL == result.PrevInAEL && !ClipperBase.IsHorizontal(result)))
			{
				return null;
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000069B0 File Offset: 0x00004BB0
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
				throw new ClipperException("ProcessIntersections error");
			}
			this.m_SortedEdges = null;
			return true;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006A3C File Offset: 0x00004C3C
		private void BuildIntersectList(long topY)
		{
			if (this.m_ActiveEdges == null)
			{
				return;
			}
			TEdge e = this.m_ActiveEdges;
			this.m_SortedEdges = e;
			while (e != null)
			{
				e.PrevInSEL = e.PrevInAEL;
				e.NextInSEL = e.NextInAEL;
				e.Curr.X = Clipper.TopX(e, topY);
				e = e.NextInAEL;
			}
			bool isModified = true;
			while (isModified && this.m_SortedEdges != null)
			{
				isModified = false;
				e = this.m_SortedEdges;
				while (e.NextInSEL != null)
				{
					TEdge eNext = e.NextInSEL;
					if (e.Curr.X > eNext.Curr.X)
					{
						IntPoint pt;
						this.IntersectPoint(e, eNext, out pt);
						if (pt.Y < topY)
						{
							pt = new IntPoint(Clipper.TopX(e, topY), topY);
						}
						IntersectNode newNode = new IntersectNode();
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

		// Token: 0x0600009D RID: 157 RVA: 0x00006B57 File Offset: 0x00004D57
		private bool EdgesAdjacent(IntersectNode inode)
		{
			return inode.Edge1.NextInSEL == inode.Edge2 || inode.Edge1.PrevInSEL == inode.Edge2;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006B81 File Offset: 0x00004D81
		private static int IntersectNodeSort(IntersectNode node1, IntersectNode node2)
		{
			return (int)(node2.Pt.Y - node1.Pt.Y);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006B9C File Offset: 0x00004D9C
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
					IntersectNode tmp = this.m_IntersectList[i];
					this.m_IntersectList[i] = this.m_IntersectList[j];
					this.m_IntersectList[j] = tmp;
				}
				this.SwapPositionsInSEL(this.m_IntersectList[i].Edge1, this.m_IntersectList[i].Edge2);
			}
			return true;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006C78 File Offset: 0x00004E78
		private void ProcessIntersectList()
		{
			for (int i = 0; i < this.m_IntersectList.Count; i++)
			{
				IntersectNode iNode = this.m_IntersectList[i];
				this.IntersectEdges(iNode.Edge1, iNode.Edge2, iNode.Pt);
				base.SwapPositionsInAEL(iNode.Edge1, iNode.Edge2);
			}
			this.m_IntersectList.Clear();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006CDD File Offset: 0x00004EDD
		internal static long Round(double value)
		{
			if (value >= 0.0)
			{
				return (long)(value + 0.5);
			}
			return (long)(value - 0.5);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006D04 File Offset: 0x00004F04
		private static long TopX(TEdge edge, long currentY)
		{
			if (currentY == edge.Top.Y)
			{
				return edge.Top.X;
			}
			return edge.Bot.X + Clipper.Round(edge.Dx * (double)(currentY - edge.Bot.Y));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006D54 File Offset: 0x00004F54
		private void IntersectPoint(TEdge edge1, TEdge edge2, out IntPoint ip)
		{
			ip = default(IntPoint);
			long pivotPoint = -1L;
			bool isClamp = edge2.Curr.N > 0L && edge2.Curr.N < (long)this.LastIndex && edge1.Curr.N > 0L && edge1.Curr.N < (long)this.LastIndex;
			if (edge1.Curr.N > edge2.Curr.N)
			{
				if (edge2.Curr.N != -1L)
				{
					if (isClamp)
					{
						pivotPoint = ((edge1.Curr.N > 0L) ? (edge1.Curr.N - 1L) : 0L);
					}
				}
				else
				{
					pivotPoint = edge1.Curr.N;
				}
			}
			else if (edge1.Curr.N != -1L)
			{
				if (isClamp)
				{
					pivotPoint = edge2.Curr.N;
				}
			}
			else
			{
				pivotPoint = ((edge2.Curr.N > 0L) ? (edge2.Curr.N - 1L) : 0L);
			}
			ip.D = 2L;
			ip.N = (isClamp ? pivotPoint : (-1L));
			if (edge1.Dx == edge2.Dx)
			{
				ip.Y = edge1.Curr.Y;
				ip.X = Clipper.TopX(edge1, ip.Y);
				return;
			}
			if (edge1.Delta.X == 0L)
			{
				ip.X = edge1.Bot.X;
				if (ClipperBase.IsHorizontal(edge2))
				{
					ip.Y = edge2.Bot.Y;
				}
				else
				{
					double b2 = (double)edge2.Bot.Y - (double)edge2.Bot.X / edge2.Dx;
					ip.Y = Clipper.Round((double)ip.X / edge2.Dx + b2);
				}
			}
			else if (edge2.Delta.X == 0L)
			{
				ip.X = edge2.Bot.X;
				if (ClipperBase.IsHorizontal(edge1))
				{
					ip.Y = edge1.Bot.Y;
				}
				else
				{
					double b3 = (double)edge1.Bot.Y - (double)edge1.Bot.X / edge1.Dx;
					ip.Y = Clipper.Round((double)ip.X / edge1.Dx + b3);
				}
			}
			else
			{
				double b3 = (double)edge1.Bot.X - (double)edge1.Bot.Y * edge1.Dx;
				double b2 = (double)edge2.Bot.X - (double)edge2.Bot.Y * edge2.Dx;
				double q = (b2 - b3) / (edge1.Dx - edge2.Dx);
				ip.Y = Clipper.Round(q);
				if (Math.Abs(edge1.Dx) < Math.Abs(edge2.Dx))
				{
					ip.X = Clipper.Round(edge1.Dx * q + b3);
				}
				else
				{
					ip.X = Clipper.Round(edge2.Dx * q + b2);
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
					ip.X = Clipper.TopX(edge1, ip.Y);
				}
				else
				{
					ip.X = Clipper.TopX(edge2, ip.Y);
				}
			}
			if (ip.Y > edge1.Curr.Y)
			{
				ip.Y = edge1.Curr.Y;
				if (Math.Abs(edge1.Dx) > Math.Abs(edge2.Dx))
				{
					ip.X = Clipper.TopX(edge2, ip.Y);
					return;
				}
				ip.X = Clipper.TopX(edge1, ip.Y);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007144 File Offset: 0x00005344
		private void ProcessEdgesAtTopOfScanbeam(long topY)
		{
			TEdge e = this.m_ActiveEdges;
			while (e != null)
			{
				bool IsMaximaEdge = this.IsMaxima(e, (double)topY);
				if (IsMaximaEdge)
				{
					TEdge eMaxPair = this.GetMaximaPairEx(e);
					IsMaximaEdge = eMaxPair == null || !ClipperBase.IsHorizontal(eMaxPair);
				}
				if (IsMaximaEdge)
				{
					if (this.StrictlySimple)
					{
						this.InsertMaxima(e.Top.X);
					}
					TEdge ePrev = e.PrevInAEL;
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
					if (this.IsIntermediate(e, (double)topY) && ClipperBase.IsHorizontal(e.NextInLML))
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
						e.Curr.X = Clipper.TopX(e, topY);
						e.Curr.Y = topY;
					}
					if (this.StrictlySimple)
					{
						TEdge ePrev2 = e.PrevInAEL;
						if (e.OutIdx >= 0 && e.WindDelta != 0 && ePrev2 != null && ePrev2.OutIdx >= 0 && ePrev2.Curr.X == e.Curr.X && ePrev2.WindDelta != 0)
						{
							IntPoint ip = new IntPoint(e.Curr);
							OutPt op = this.AddOutPt(ePrev2, ip);
							OutPt op2 = this.AddOutPt(e, ip);
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
					OutPt op3 = null;
					if (e.OutIdx >= 0)
					{
						op3 = this.AddOutPt(e, e.Top);
					}
					base.UpdateEdgeIntoAEL(ref e);
					TEdge ePrev3 = e.PrevInAEL;
					TEdge eNext = e.NextInAEL;
					if (ePrev3 != null && ePrev3.Curr.X == e.Bot.X && ePrev3.Curr.Y == e.Bot.Y && op3 != null && ePrev3.OutIdx >= 0 && ePrev3.Curr.Y > ePrev3.Top.Y && ClipperBase.SlopesEqual(e.Curr, e.Top, ePrev3.Curr, ePrev3.Top, this.m_UseFullRange) && e.WindDelta != 0 && ePrev3.WindDelta != 0)
					{
						OutPt op4 = this.AddOutPt(ePrev3, e.Bot);
						this.AddJoin(op3, op4, e.Top);
					}
					else if (eNext != null && eNext.Curr.X == e.Bot.X && eNext.Curr.Y == e.Bot.Y && op3 != null && eNext.OutIdx >= 0 && eNext.Curr.Y > eNext.Top.Y && ClipperBase.SlopesEqual(e.Curr, e.Top, eNext.Curr, eNext.Top, this.m_UseFullRange) && e.WindDelta != 0 && eNext.WindDelta != 0)
					{
						OutPt op5 = this.AddOutPt(eNext, e.Bot);
						this.AddJoin(op3, op5, e.Top);
					}
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000074A0 File Offset: 0x000056A0
		private void DoMaxima(TEdge e)
		{
			TEdge eMaxPair = this.GetMaximaPairEx(e);
			if (eMaxPair == null)
			{
				if (e.OutIdx >= 0)
				{
					this.AddOutPt(e, e.Top);
				}
				base.DeleteFromAEL(e);
				return;
			}
			TEdge eNext = e.NextInAEL;
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
			throw new ClipperException("DoMaxima error");
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000075BC File Offset: 0x000057BC
		public static void ReversePaths(List<List<IntPoint>> polys)
		{
			for (int i = 0; i < polys.Count; i++)
			{
				polys[i].Reverse();
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000075E6 File Offset: 0x000057E6
		public static bool Orientation(List<IntPoint> poly)
		{
			return Clipper.Area(poly) >= 0.0;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000075FC File Offset: 0x000057FC
		private int PointCount(OutPt pts)
		{
			if (pts == null)
			{
				return 0;
			}
			int result = 0;
			OutPt p = pts;
			do
			{
				result++;
				p = p.Next;
			}
			while (p != pts);
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007624 File Offset: 0x00005824
		private void BuildResult(List<List<IntPoint>> polyg)
		{
			polyg.Clear();
			polyg.Capacity = this.m_PolyOuts.Count;
			for (int i = 0; i < this.m_PolyOuts.Count; i++)
			{
				OutRec outRec = this.m_PolyOuts[i];
				if (outRec.Pts != null)
				{
					OutPt p = outRec.Pts.Prev;
					int cnt = this.PointCount(p);
					if (cnt >= 2)
					{
						List<IntPoint> pg = new List<IntPoint>(cnt);
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

		// Token: 0x060000AA RID: 170 RVA: 0x000076C0 File Offset: 0x000058C0
		private void BuildResult2(PolyTree polytree)
		{
			polytree.Clear();
			polytree.m_AllPolys.Capacity = this.m_PolyOuts.Count;
			for (int i = 0; i < this.m_PolyOuts.Count; i++)
			{
				OutRec outRec = this.m_PolyOuts[i];
				int cnt = this.PointCount(outRec.Pts);
				if ((!outRec.IsOpen || cnt >= 2) && (outRec.IsOpen || cnt >= 3))
				{
					this.FixHoleLinkage(outRec);
					PolyNode pn = new PolyNode();
					polytree.m_AllPolys.Add(pn);
					outRec.PolyNode = pn;
					pn.m_polygon.Capacity = cnt;
					OutPt op = outRec.Pts.Prev;
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
				OutRec outRec2 = this.m_PolyOuts[k];
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

		// Token: 0x060000AB RID: 171 RVA: 0x0000784C File Offset: 0x00005A4C
		private void FixupOutPolyline(OutRec outrec)
		{
			OutPt pp = outrec.Pts;
			OutPt lastPP = pp.Prev;
			while (pp != lastPP)
			{
				pp = pp.Next;
				if (pp.Pt == pp.Prev.Pt)
				{
					if (pp == lastPP)
					{
						lastPP = pp.Prev;
					}
					OutPt tmpPP = pp.Prev;
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

		// Token: 0x060000AC RID: 172 RVA: 0x000078C8 File Offset: 0x00005AC8
		private void FixupOutPolygon(OutRec outRec)
		{
			OutPt lastOK = null;
			outRec.BottomPt = null;
			OutPt pp = outRec.Pts;
			bool preserveCol = base.PreserveCollinear || this.StrictlySimple;
			while (pp.Prev != pp && pp.Prev != pp.Next)
			{
				if (pp.Pt == pp.Next.Pt || pp.Pt == pp.Prev.Pt || (ClipperBase.SlopesEqual(pp.Prev.Pt, pp.Pt, pp.Next.Pt, this.m_UseFullRange) && (!preserveCol || !base.Pt2IsBetweenPt1AndPt3(pp.Prev.Pt, pp.Pt, pp.Next.Pt))))
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

		// Token: 0x060000AD RID: 173 RVA: 0x000079E4 File Offset: 0x00005BE4
		private OutPt DupOutPt(OutPt outPt, bool InsertAfter)
		{
			OutPt result = new OutPt();
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

		// Token: 0x060000AE RID: 174 RVA: 0x00007A64 File Offset: 0x00005C64
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

		// Token: 0x060000AF RID: 175 RVA: 0x00007AE4 File Offset: 0x00005CE4
		private bool JoinHorz(OutPt op1, OutPt op1b, OutPt op2, OutPt op2b, IntPoint Pt, bool DiscardLeft)
		{
			Directions Dir = ((op1.Pt.X > op1b.Pt.X) ? Directions.dRightToLeft : Directions.dLeftToRight);
			Directions Dir2 = ((op2.Pt.X > op2b.Pt.X) ? Directions.dRightToLeft : Directions.dLeftToRight);
			if (Dir == Dir2)
			{
				return false;
			}
			if (Dir == Directions.dLeftToRight)
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
			if (Dir2 == Directions.dLeftToRight)
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
			if (Dir == Directions.dLeftToRight == DiscardLeft)
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

		// Token: 0x060000B0 RID: 176 RVA: 0x00007E48 File Offset: 0x00006048
		private bool JoinPoints(Join j, OutRec outRec1, OutRec outRec2)
		{
			OutPt op = j.OutPt1;
			OutPt op2 = j.OutPt2;
			bool isHorizontal = j.OutPt1.Pt.Y == j.OffPt.Y;
			if (isHorizontal && j.OffPt == j.OutPt1.Pt && j.OffPt == j.OutPt2.Pt)
			{
				if (outRec1 != outRec2)
				{
					return false;
				}
				OutPt op1b = j.OutPt1.Next;
				while (op1b != op && op1b.Pt == j.OffPt)
				{
					op1b = op1b.Next;
				}
				bool reverse = op1b.Pt.Y > j.OffPt.Y;
				OutPt op2b = j.OutPt2.Next;
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
				OutPt op1b = op;
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
				OutPt op2b = op2;
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
				IntPoint Pt;
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
				OutPt op1b = op.Next;
				while (op1b.Pt == op.Pt && op1b != op)
				{
					op1b = op1b.Next;
				}
				bool Reverse = op1b.Pt.Y > op.Pt.Y || !ClipperBase.SlopesEqual(op.Pt, op1b.Pt, j.OffPt, this.m_UseFullRange);
				if (Reverse)
				{
					op1b = op.Prev;
					while (op1b.Pt == op.Pt && op1b != op)
					{
						op1b = op1b.Prev;
					}
					if (op1b.Pt.Y > op.Pt.Y || !ClipperBase.SlopesEqual(op.Pt, op1b.Pt, j.OffPt, this.m_UseFullRange))
					{
						return false;
					}
				}
				OutPt op2b = op2.Next;
				while (op2b.Pt == op2.Pt && op2b != op2)
				{
					op2b = op2b.Next;
				}
				bool Reverse2 = op2b.Pt.Y > op2.Pt.Y || !ClipperBase.SlopesEqual(op2.Pt, op2b.Pt, j.OffPt, this.m_UseFullRange);
				if (Reverse2)
				{
					op2b = op2.Prev;
					while (op2b.Pt == op2.Pt && op2b != op2)
					{
						op2b = op2b.Prev;
					}
					if (op2b.Pt.Y > op2.Pt.Y || !ClipperBase.SlopesEqual(op2.Pt, op2b.Pt, j.OffPt, this.m_UseFullRange))
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

		// Token: 0x060000B1 RID: 177 RVA: 0x00008458 File Offset: 0x00006658
		public static int PointInPolygon(IntPoint pt, List<IntPoint> path)
		{
			int result = 0;
			int cnt = path.Count;
			if (cnt < 3)
			{
				return 0;
			}
			IntPoint ip = path[0];
			for (int i = 1; i <= cnt; i++)
			{
				IntPoint ipNext = ((i == cnt) ? path[0] : path[i]);
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

		// Token: 0x060000B2 RID: 178 RVA: 0x00008634 File Offset: 0x00006834
		private static int PointInPolygon(IntPoint pt, OutPt op)
		{
			int result = 0;
			OutPt startOp = op;
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

		// Token: 0x060000B3 RID: 179 RVA: 0x00008768 File Offset: 0x00006968
		private static bool Poly2ContainsPoly1(OutPt outPt1, OutPt outPt2)
		{
			OutPt op = outPt1;
			int res;
			for (;;)
			{
				res = Clipper.PointInPolygon(op.Pt, outPt2);
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

		// Token: 0x060000B4 RID: 180 RVA: 0x0000879C File Offset: 0x0000699C
		private void FixupFirstLefts1(OutRec OldOutRec, OutRec NewOutRec)
		{
			foreach (OutRec outRec in this.m_PolyOuts)
			{
				OutRec firstLeft = Clipper.ParseFirstLeft(outRec.FirstLeft);
				if (outRec.Pts != null && firstLeft == OldOutRec && Clipper.Poly2ContainsPoly1(outRec.Pts, NewOutRec.Pts))
				{
					outRec.FirstLeft = NewOutRec;
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000881C File Offset: 0x00006A1C
		private void FixupFirstLefts2(OutRec innerOutRec, OutRec outerOutRec)
		{
			OutRec orfl = outerOutRec.FirstLeft;
			foreach (OutRec outRec in this.m_PolyOuts)
			{
				if (outRec.Pts != null && outRec != outerOutRec && outRec != innerOutRec)
				{
					OutRec firstLeft = Clipper.ParseFirstLeft(outRec.FirstLeft);
					if (firstLeft == orfl || firstLeft == innerOutRec || firstLeft == outerOutRec)
					{
						if (Clipper.Poly2ContainsPoly1(outRec.Pts, innerOutRec.Pts))
						{
							outRec.FirstLeft = innerOutRec;
						}
						else if (Clipper.Poly2ContainsPoly1(outRec.Pts, outerOutRec.Pts))
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

		// Token: 0x060000B6 RID: 182 RVA: 0x000088F0 File Offset: 0x00006AF0
		private void FixupFirstLefts3(OutRec OldOutRec, OutRec NewOutRec)
		{
			foreach (OutRec outRec in this.m_PolyOuts)
			{
				OutRec firstLeft = Clipper.ParseFirstLeft(outRec.FirstLeft);
				if (outRec.Pts != null && firstLeft == OldOutRec)
				{
					outRec.FirstLeft = NewOutRec;
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000895C File Offset: 0x00006B5C
		private static OutRec ParseFirstLeft(OutRec FirstLeft)
		{
			while (FirstLeft != null && FirstLeft.Pts == null)
			{
				FirstLeft = FirstLeft.FirstLeft;
			}
			return FirstLeft;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00008974 File Offset: 0x00006B74
		private void JoinCommonEdges()
		{
			for (int i = 0; i < this.m_Joins.Count; i++)
			{
				Join join = this.m_Joins[i];
				OutRec outRec = this.GetOutRec(join.OutPt1.Idx);
				OutRec outRec2 = this.GetOutRec(join.OutPt2.Idx);
				if (outRec.Pts != null && outRec2.Pts != null && !outRec.IsOpen && !outRec2.IsOpen)
				{
					OutRec holeStateRec;
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
							if (Clipper.Poly2ContainsPoly1(outRec2.Pts, outRec.Pts))
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
							else if (Clipper.Poly2ContainsPoly1(outRec.Pts, outRec2.Pts))
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

		// Token: 0x060000B9 RID: 185 RVA: 0x00008BD8 File Offset: 0x00006DD8
		private void UpdateOutPtIdxs(OutRec outrec)
		{
			OutPt op = outrec.Pts;
			do
			{
				op.Idx = outrec.Idx;
				op = op.Prev;
			}
			while (op != outrec.Pts);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00008C08 File Offset: 0x00006E08
		private void DoSimplePolygons()
		{
			int i = 0;
			while (i < this.m_PolyOuts.Count)
			{
				OutRec outrec = this.m_PolyOuts[i++];
				OutPt op = outrec.Pts;
				if (op != null && !outrec.IsOpen)
				{
					do
					{
						for (OutPt op2 = op.Next; op2 != outrec.Pts; op2 = op2.Next)
						{
							if (op.Pt == op2.Pt && op2.Next != op && op2.Prev != op)
							{
								OutPt op3 = op.Prev;
								OutPt op4 = op2.Prev;
								op.Prev = op4;
								op4.Next = op;
								op2.Prev = op3;
								op3.Next = op2;
								outrec.Pts = op;
								OutRec outrec2 = base.CreateOutRec();
								outrec2.Pts = op2;
								this.UpdateOutPtIdxs(outrec2);
								if (Clipper.Poly2ContainsPoly1(outrec2.Pts, outrec.Pts))
								{
									outrec2.IsHole = !outrec.IsHole;
									outrec2.FirstLeft = outrec;
									if (this.m_UsingPolyTree)
									{
										this.FixupFirstLefts2(outrec2, outrec);
									}
								}
								else if (Clipper.Poly2ContainsPoly1(outrec.Pts, outrec2.Pts))
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

		// Token: 0x060000BB RID: 187 RVA: 0x00008DD0 File Offset: 0x00006FD0
		public static double Area(List<IntPoint> poly)
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

		// Token: 0x060000BC RID: 188 RVA: 0x00008E54 File Offset: 0x00007054
		internal double Area(OutRec outRec)
		{
			return this.Area(outRec.Pts);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00008E64 File Offset: 0x00007064
		internal double Area(OutPt op)
		{
			OutPt opFirst = op;
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

		// Token: 0x060000BE RID: 190 RVA: 0x00008EE0 File Offset: 0x000070E0
		public static List<List<IntPoint>> SimplifyPolygon(List<IntPoint> poly, PolyFillTypes fillType = PolyFillTypes.pftEvenOdd)
		{
			List<List<IntPoint>> result = new List<List<IntPoint>>();
			Clipper clipper = new Clipper(0);
			clipper.StrictlySimple = true;
			clipper.AddPath(poly, PolyTypes.ptSubject, true);
			clipper.Execute(ClipTypes.ctUnion, result, fillType, fillType);
			return result;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00008F18 File Offset: 0x00007118
		public static List<List<IntPoint>> SimplifyPolygons(List<List<IntPoint>> polys, PolyFillTypes fillType = PolyFillTypes.pftEvenOdd)
		{
			List<List<IntPoint>> result = new List<List<IntPoint>>();
			Clipper clipper = new Clipper(0);
			clipper.StrictlySimple = true;
			clipper.AddPaths(polys, PolyTypes.ptSubject, true);
			clipper.Execute(ClipTypes.ctUnion, result, fillType, fillType);
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00008F50 File Offset: 0x00007150
		private static double DistanceSqrd(IntPoint pt1, IntPoint pt2)
		{
			double num = (double)pt1.X - (double)pt2.X;
			double dy = (double)pt1.Y - (double)pt2.Y;
			return num * num + dy * dy;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00008F84 File Offset: 0x00007184
		private static double DistanceFromLineSqrd(IntPoint pt, IntPoint ln1, IntPoint ln2)
		{
			double A = (double)(ln1.Y - ln2.Y);
			double B = (double)(ln2.X - ln1.X);
			double C = A * (double)ln1.X + B * (double)ln1.Y;
			C = A * (double)pt.X + B * (double)pt.Y - C;
			return C * C / (A * A + B * B);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00008FE4 File Offset: 0x000071E4
		private static bool SlopesNearCollinear(IntPoint pt1, IntPoint pt2, IntPoint pt3, double distSqrd)
		{
			if (Math.Abs(pt1.X - pt2.X) > Math.Abs(pt1.Y - pt2.Y))
			{
				if (pt1.X > pt2.X == pt1.X < pt3.X)
				{
					return Clipper.DistanceFromLineSqrd(pt1, pt2, pt3) < distSqrd;
				}
				if (pt2.X > pt1.X == pt2.X < pt3.X)
				{
					return Clipper.DistanceFromLineSqrd(pt2, pt1, pt3) < distSqrd;
				}
				return Clipper.DistanceFromLineSqrd(pt3, pt1, pt2) < distSqrd;
			}
			else
			{
				if (pt1.Y > pt2.Y == pt1.Y < pt3.Y)
				{
					return Clipper.DistanceFromLineSqrd(pt1, pt2, pt3) < distSqrd;
				}
				if (pt2.Y > pt1.Y == pt2.Y < pt3.Y)
				{
					return Clipper.DistanceFromLineSqrd(pt2, pt1, pt3) < distSqrd;
				}
				return Clipper.DistanceFromLineSqrd(pt3, pt1, pt2) < distSqrd;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000090D8 File Offset: 0x000072D8
		private static bool PointsAreClose(IntPoint pt1, IntPoint pt2, double distSqrd)
		{
			double num = (double)pt1.X - (double)pt2.X;
			double dy = (double)pt1.Y - (double)pt2.Y;
			return num * num + dy * dy <= distSqrd;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00009110 File Offset: 0x00007310
		private static OutPt ExcludeOp(OutPt op)
		{
			OutPt result = op.Prev;
			result.Next = op.Next;
			op.Next.Prev = result;
			result.Idx = 0;
			return result;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00009144 File Offset: 0x00007344
		public static List<IntPoint> CleanPolygon(List<IntPoint> path, double distance = 1.415)
		{
			int cnt = path.Count;
			if (cnt == 0)
			{
				return new List<IntPoint>();
			}
			OutPt[] outPts = new OutPt[cnt];
			for (int i = 0; i < cnt; i++)
			{
				outPts[i] = new OutPt();
			}
			for (int j = 0; j < cnt; j++)
			{
				outPts[j].Pt = path[j];
				outPts[j].Next = outPts[(j + 1) % cnt];
				outPts[j].Next.Prev = outPts[j];
				outPts[j].Idx = 0;
			}
			double distSqrd = distance * distance;
			OutPt op = outPts[0];
			while (op.Idx == 0 && op.Next != op.Prev)
			{
				if (Clipper.PointsAreClose(op.Pt, op.Prev.Pt, distSqrd))
				{
					op = Clipper.ExcludeOp(op);
					cnt--;
				}
				else if (Clipper.PointsAreClose(op.Prev.Pt, op.Next.Pt, distSqrd))
				{
					Clipper.ExcludeOp(op.Next);
					op = Clipper.ExcludeOp(op);
					cnt -= 2;
				}
				else if (Clipper.SlopesNearCollinear(op.Prev.Pt, op.Pt, op.Next.Pt, distSqrd))
				{
					op = Clipper.ExcludeOp(op);
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
			List<IntPoint> result = new List<IntPoint>(cnt);
			for (int k = 0; k < cnt; k++)
			{
				result.Add(op.Pt);
				op = op.Next;
			}
			return result;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000092C8 File Offset: 0x000074C8
		public static List<List<IntPoint>> CleanPolygons(List<List<IntPoint>> polys, double distance = 1.415)
		{
			List<List<IntPoint>> result = new List<List<IntPoint>>(polys.Count);
			for (int i = 0; i < polys.Count; i++)
			{
				result.Add(Clipper.CleanPolygon(polys[i], distance));
			}
			return result;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00009308 File Offset: 0x00007508
		internal static List<List<IntPoint>> Minkowski(List<IntPoint> pattern, List<IntPoint> path, bool IsSum, bool IsClosed)
		{
			int delta = (IsClosed ? 1 : 0);
			int polyCnt = pattern.Count;
			int pathCnt = path.Count;
			List<List<IntPoint>> result = new List<List<IntPoint>>(pathCnt);
			if (IsSum)
			{
				for (int i = 0; i < pathCnt; i++)
				{
					List<IntPoint> p = new List<IntPoint>(polyCnt);
					for (int patternIndex = 0; patternIndex < pattern.Count; patternIndex++)
					{
						IntPoint ip = pattern[patternIndex];
						p.Add(new IntPoint(path[i].X + ip.X, path[i].Y + ip.Y));
					}
					result.Add(p);
				}
			}
			else
			{
				for (int j = 0; j < pathCnt; j++)
				{
					List<IntPoint> p2 = new List<IntPoint>(polyCnt);
					for (int patternIndex2 = 0; patternIndex2 < pattern.Count; patternIndex2++)
					{
						IntPoint ip2 = pattern[patternIndex2];
						p2.Add(new IntPoint(path[j].X - ip2.X, path[j].Y - ip2.Y));
					}
					result.Add(p2);
				}
			}
			List<List<IntPoint>> quads = new List<List<IntPoint>>((pathCnt + delta) * (polyCnt + 1));
			for (int k = 0; k < pathCnt - 1 + delta; k++)
			{
				for (int l = 0; l < polyCnt; l++)
				{
					List<IntPoint> quad = new List<IntPoint>(4);
					quad.Add(result[k % pathCnt][l % polyCnt]);
					quad.Add(result[(k + 1) % pathCnt][l % polyCnt]);
					quad.Add(result[(k + 1) % pathCnt][(l + 1) % polyCnt]);
					quad.Add(result[k % pathCnt][(l + 1) % polyCnt]);
					if (!Clipper.Orientation(quad))
					{
						quad.Reverse();
					}
					quads.Add(quad);
				}
			}
			return quads;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000094F4 File Offset: 0x000076F4
		public static List<List<IntPoint>> MinkowskiSum(List<IntPoint> pattern, List<IntPoint> path, bool pathIsClosed)
		{
			List<List<IntPoint>> paths = Clipper.Minkowski(pattern, path, true, pathIsClosed);
			Clipper clipper = new Clipper(0);
			clipper.AddPaths(paths, PolyTypes.ptSubject, true);
			clipper.Execute(ClipTypes.ctUnion, paths, PolyFillTypes.pftNonZero, PolyFillTypes.pftNonZero);
			return paths;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00009528 File Offset: 0x00007728
		private static List<IntPoint> TranslatePath(List<IntPoint> path, IntPoint delta)
		{
			List<IntPoint> outPath = new List<IntPoint>(path.Count);
			for (int i = 0; i < path.Count; i++)
			{
				outPath.Add(new IntPoint(path[i].X + delta.X, path[i].Y + delta.Y));
			}
			return outPath;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00009584 File Offset: 0x00007784
		public static List<List<IntPoint>> MinkowskiSum(List<IntPoint> pattern, List<List<IntPoint>> paths, bool pathIsClosed)
		{
			List<List<IntPoint>> solution = new List<List<IntPoint>>();
			Clipper c = new Clipper(0);
			for (int i = 0; i < paths.Count; i++)
			{
				List<List<IntPoint>> tmp = Clipper.Minkowski(pattern, paths[i], true, pathIsClosed);
				c.AddPaths(tmp, PolyTypes.ptSubject, true);
				if (pathIsClosed)
				{
					List<IntPoint> path = Clipper.TranslatePath(paths[i], pattern[0]);
					c.AddPath(path, PolyTypes.ptClip, true);
				}
			}
			c.Execute(ClipTypes.ctUnion, solution, PolyFillTypes.pftNonZero, PolyFillTypes.pftNonZero);
			return solution;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000095F8 File Offset: 0x000077F8
		public static List<List<IntPoint>> MinkowskiDiff(List<IntPoint> poly1, List<IntPoint> poly2)
		{
			List<List<IntPoint>> paths = Clipper.Minkowski(poly1, poly2, false, true);
			Clipper clipper = new Clipper(0);
			clipper.AddPaths(paths, PolyTypes.ptSubject, true);
			clipper.Execute(ClipTypes.ctUnion, paths, PolyFillTypes.pftNonZero, PolyFillTypes.pftNonZero);
			return paths;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000962C File Offset: 0x0000782C
		public static List<List<IntPoint>> PolyTreeToPaths(PolyTree polytree)
		{
			List<List<IntPoint>> result = new List<List<IntPoint>>();
			result.Capacity = polytree.Total;
			Clipper.AddPolyNodeToPaths(polytree, Clipper.NodeType.ntAny, result);
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00009654 File Offset: 0x00007854
		internal static void AddPolyNodeToPaths(PolyNode polynode, Clipper.NodeType nt, List<List<IntPoint>> paths)
		{
			bool match = true;
			if (nt != Clipper.NodeType.ntOpen)
			{
				if (nt == Clipper.NodeType.ntClosed)
				{
					match = !polynode.IsOpen;
				}
				if (polynode.m_polygon.Count > 0 && match)
				{
					paths.Add(polynode.m_polygon);
				}
				foreach (PolyNode polyNode in polynode.Childs)
				{
					Clipper.AddPolyNodeToPaths(polyNode, nt, paths);
				}
				return;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000096DC File Offset: 0x000078DC
		public static List<List<IntPoint>> OpenPathsFromPolyTree(PolyTree polytree)
		{
			List<List<IntPoint>> result = new List<List<IntPoint>>();
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

		// Token: 0x060000CF RID: 207 RVA: 0x00009738 File Offset: 0x00007938
		public static List<List<IntPoint>> ClosedPathsFromPolyTree(PolyTree polytree)
		{
			List<List<IntPoint>> result = new List<List<IntPoint>>();
			result.Capacity = polytree.Total;
			Clipper.AddPolyNodeToPaths(polytree, Clipper.NodeType.ntClosed, result);
			return result;
		}

		// Token: 0x04000082 RID: 130
		public const int ioReverseSolution = 1;

		// Token: 0x04000083 RID: 131
		public const int ioStrictlySimple = 2;

		// Token: 0x04000084 RID: 132
		public const int ioPreserveCollinear = 4;

		// Token: 0x04000085 RID: 133
		private ClipTypes m_ClipType;

		// Token: 0x04000086 RID: 134
		private Maxima m_Maxima;

		// Token: 0x04000087 RID: 135
		private TEdge m_SortedEdges;

		// Token: 0x04000088 RID: 136
		private List<IntersectNode> m_IntersectList;

		// Token: 0x04000089 RID: 137
		private IComparer<IntersectNode> m_IntersectNodeComparer;

		// Token: 0x0400008A RID: 138
		private bool m_ExecuteLocked;

		// Token: 0x0400008B RID: 139
		private PolyFillTypes m_ClipFillType;

		// Token: 0x0400008C RID: 140
		private PolyFillTypes m_SubjFillType;

		// Token: 0x0400008D RID: 141
		private List<Join> m_Joins;

		// Token: 0x0400008E RID: 142
		private List<Join> m_GhostJoins;

		// Token: 0x0400008F RID: 143
		private bool m_UsingPolyTree;

		// Token: 0x02000024 RID: 36
		internal enum NodeType
		{
			// Token: 0x04000094 RID: 148
			ntAny,
			// Token: 0x04000095 RID: 149
			ntOpen,
			// Token: 0x04000096 RID: 150
			ntClosed
		}
	}
}
