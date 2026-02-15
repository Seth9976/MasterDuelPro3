using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000022 RID: 34
	internal class ClipperBase
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002900 File Offset: 0x00000B00
		internal static bool near_zero(double val)
		{
			return val > -1E-20 && val < 1E-20;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000291C File Offset: 0x00000B1C
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002924 File Offset: 0x00000B24
		public bool PreserveCollinear { get; set; }

		// Token: 0x06000041 RID: 65 RVA: 0x00002930 File Offset: 0x00000B30
		public void Swap(ref long val1, ref long val2)
		{
			long tmp = val1;
			val1 = val2;
			val2 = tmp;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002947 File Offset: 0x00000B47
		internal static bool IsHorizontal(TEdge e)
		{
			return e.Delta.Y == 0L;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002958 File Offset: 0x00000B58
		internal bool PointIsVertex(IntPoint pt, OutPt pp)
		{
			OutPt pp2 = pp;
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

		// Token: 0x06000044 RID: 68 RVA: 0x00002984 File Offset: 0x00000B84
		internal bool PointOnLineSegment(IntPoint pt, IntPoint linePt1, IntPoint linePt2, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return (pt.X == linePt1.X && pt.Y == linePt1.Y) || (pt.X == linePt2.X && pt.Y == linePt2.Y) || (pt.X > linePt1.X == pt.X < linePt2.X && pt.Y > linePt1.Y == pt.Y < linePt2.Y && Int128.Int128Mul(pt.X - linePt1.X, linePt2.Y - linePt1.Y) == Int128.Int128Mul(linePt2.X - linePt1.X, pt.Y - linePt1.Y));
			}
			return (pt.X == linePt1.X && pt.Y == linePt1.Y) || (pt.X == linePt2.X && pt.Y == linePt2.Y) || (pt.X > linePt1.X == pt.X < linePt2.X && pt.Y > linePt1.Y == pt.Y < linePt2.Y && (pt.X - linePt1.X) * (linePt2.Y - linePt1.Y) == (linePt2.X - linePt1.X) * (pt.Y - linePt1.Y));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B10 File Offset: 0x00000D10
		internal bool PointOnPolygon(IntPoint pt, OutPt pp, bool UseFullRange)
		{
			OutPt pp2 = pp;
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

		// Token: 0x06000046 RID: 70 RVA: 0x00002B48 File Offset: 0x00000D48
		internal static bool SlopesEqual(TEdge e1, TEdge e2, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return Int128.Int128Mul(e1.Delta.Y, e2.Delta.X) == Int128.Int128Mul(e1.Delta.X, e2.Delta.Y);
			}
			return e1.Delta.Y * e2.Delta.X == e1.Delta.X * e2.Delta.Y;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BC4 File Offset: 0x00000DC4
		internal static bool SlopesEqual(IntPoint pt1, IntPoint pt2, IntPoint pt3, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return Int128.Int128Mul(pt1.Y - pt2.Y, pt2.X - pt3.X) == Int128.Int128Mul(pt1.X - pt2.X, pt2.Y - pt3.Y);
			}
			return (pt1.Y - pt2.Y) * (pt2.X - pt3.X) - (pt1.X - pt2.X) * (pt2.Y - pt3.Y) == 0L;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002C54 File Offset: 0x00000E54
		internal static bool SlopesEqual(IntPoint pt1, IntPoint pt2, IntPoint pt3, IntPoint pt4, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return Int128.Int128Mul(pt1.Y - pt2.Y, pt3.X - pt4.X) == Int128.Int128Mul(pt1.X - pt2.X, pt3.Y - pt4.Y);
			}
			return (pt1.Y - pt2.Y) * (pt3.X - pt4.X) - (pt1.X - pt2.X) * (pt3.Y - pt4.Y) == 0L;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002CE4 File Offset: 0x00000EE4
		internal ClipperBase()
		{
			this.m_MinimaList = null;
			this.m_CurrentLM = null;
			this.m_UseFullRange = false;
			this.m_HasOpenPaths = false;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D14 File Offset: 0x00000F14
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

		// Token: 0x0600004B RID: 75 RVA: 0x00002D98 File Offset: 0x00000F98
		private void DisposeLocalMinimaList()
		{
			while (this.m_MinimaList != null)
			{
				LocalMinima tmpLm = this.m_MinimaList.Next;
				this.m_MinimaList = null;
				this.m_MinimaList = tmpLm;
			}
			this.m_CurrentLM = null;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002DD0 File Offset: 0x00000FD0
		private void RangeTest(IntPoint Pt, ref bool useFullRange)
		{
			if (useFullRange)
			{
				if (Pt.X > 4611686018427387903L || Pt.Y > 4611686018427387903L || -Pt.X > 4611686018427387903L || -Pt.Y > 4611686018427387903L)
				{
					throw new ClipperException("Coordinate outside allowed range");
				}
			}
			else if (Pt.X > 1073741823L || Pt.Y > 1073741823L || -Pt.X > 1073741823L || -Pt.Y > 1073741823L)
			{
				useFullRange = true;
				this.RangeTest(Pt, ref useFullRange);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E77 File Offset: 0x00001077
		private void InitEdge(TEdge e, TEdge eNext, TEdge ePrev, IntPoint pt)
		{
			e.Next = eNext;
			e.Prev = ePrev;
			e.Curr = pt;
			e.OutIdx = -1;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E98 File Offset: 0x00001098
		private void InitEdge2(TEdge e, PolyTypes polyType)
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

		// Token: 0x0600004F RID: 79 RVA: 0x00002F0C File Offset: 0x0000110C
		private TEdge FindNextLocMin(TEdge E)
		{
			TEdge E2;
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

		// Token: 0x06000050 RID: 80 RVA: 0x00002FF4 File Offset: 0x000011F4
		private TEdge ProcessBound(TEdge E, bool LeftBoundIsForward)
		{
			TEdge Result = E;
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
					LocalMinima locMin = new LocalMinima();
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
			TEdge EStart;
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
					TEdge Horz = Result;
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
					TEdge Horz = Result;
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

		// Token: 0x06000051 RID: 81 RVA: 0x00003440 File Offset: 0x00001640
		public bool AddPath(List<IntPoint> pg, PolyTypes polyType, bool Closed)
		{
			if (!Closed && polyType == PolyTypes.ptClip)
			{
				throw new ClipperException("AddPath: Open paths must be subject.");
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
			List<TEdge> edges = new List<TEdge>(highI + 1);
			for (int i = 0; i <= highI; i++)
			{
				edges.Add(new TEdge());
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
			TEdge eStart = edges[0];
			TEdge E = eStart;
			TEdge eLoopStop = eStart;
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
					if (Closed && ClipperBase.SlopesEqual(E.Prev.Curr, E.Curr, E.Next.Curr, this.m_UseFullRange) && (!this.PreserveCollinear || !this.Pt2IsBetweenPt1AndPt3(E.Prev.Curr, E.Curr, E.Next.Curr)))
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
				TEdge EMin = null;
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
					LocalMinima locMin = new LocalMinima();
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
					locMin.LeftBound.Side = EdgeSides.esLeft;
					locMin.RightBound.Side = EdgeSides.esRight;
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
					TEdge E2 = this.ProcessBound(locMin.RightBound, !leftBoundIsForward);
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
			LocalMinima locMin2 = new LocalMinima();
			locMin2.Next = null;
			locMin2.Y = E.Bot.Y;
			locMin2.LeftBound = null;
			locMin2.RightBound = E;
			locMin2.RightBound.Side = EdgeSides.esRight;
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

		// Token: 0x06000052 RID: 82 RVA: 0x000039E4 File Offset: 0x00001BE4
		public bool AddPaths(List<List<IntPoint>> ppg, PolyTypes polyType, bool closed)
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

		// Token: 0x06000053 RID: 83 RVA: 0x00003A18 File Offset: 0x00001C18
		internal bool Pt2IsBetweenPt1AndPt3(IntPoint pt1, IntPoint pt2, IntPoint pt3)
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

		// Token: 0x06000054 RID: 84 RVA: 0x00003A8D File Offset: 0x00001C8D
		private TEdge RemoveEdge(TEdge e)
		{
			e.Prev.Next = e.Next;
			e.Next.Prev = e.Prev;
			TEdge next = e.Next;
			e.Prev = null;
			return next;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003AC0 File Offset: 0x00001CC0
		private void SetDx(TEdge e)
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

		// Token: 0x06000056 RID: 86 RVA: 0x00003B50 File Offset: 0x00001D50
		private void InsertLocalMinima(LocalMinima newLm)
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
			LocalMinima tmpLm = this.m_MinimaList;
			while (tmpLm.Next != null && newLm.Y < tmpLm.Next.Y)
			{
				tmpLm = tmpLm.Next;
			}
			newLm.Next = tmpLm.Next;
			tmpLm.Next = newLm;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003BD2 File Offset: 0x00001DD2
		internal bool PopLocalMinima(long Y, out LocalMinima current)
		{
			current = this.m_CurrentLM;
			if (this.m_CurrentLM != null && this.m_CurrentLM.Y == Y)
			{
				this.m_CurrentLM = this.m_CurrentLM.Next;
				return true;
			}
			return false;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003C06 File Offset: 0x00001E06
		private void ReverseHorizontal(TEdge e)
		{
			this.Swap(ref e.Top.X, ref e.Bot.X);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003C24 File Offset: 0x00001E24
		internal virtual void Reset()
		{
			this.m_CurrentLM = this.m_MinimaList;
			if (this.m_CurrentLM == null)
			{
				return;
			}
			this.m_Scanbeam = null;
			for (LocalMinima lm = this.m_MinimaList; lm != null; lm = lm.Next)
			{
				this.InsertScanbeam(lm.Y);
				TEdge e = lm.LeftBound;
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

		// Token: 0x0600005A RID: 90 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public static IntRect GetBounds(List<List<IntPoint>> paths)
		{
			int i = 0;
			int cnt = paths.Count;
			while (i < cnt && paths[i].Count == 0)
			{
				i++;
			}
			if (i == cnt)
			{
				return new IntRect(0L, 0L, 0L, 0L);
			}
			IntRect result = default(IntRect);
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

		// Token: 0x0600005B RID: 91 RVA: 0x00003E44 File Offset: 0x00002044
		internal void InsertScanbeam(long Y)
		{
			if (this.m_Scanbeam == null)
			{
				this.m_Scanbeam = new Scanbeam();
				this.m_Scanbeam.Next = null;
				this.m_Scanbeam.Y = Y;
				return;
			}
			if (Y > this.m_Scanbeam.Y)
			{
				this.m_Scanbeam = new Scanbeam
				{
					Y = Y,
					Next = this.m_Scanbeam
				};
				return;
			}
			Scanbeam sb2 = this.m_Scanbeam;
			while (sb2.Next != null && Y <= sb2.Next.Y)
			{
				sb2 = sb2.Next;
			}
			if (Y == sb2.Y)
			{
				return;
			}
			sb2.Next = new Scanbeam
			{
				Y = Y,
				Next = sb2.Next
			};
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003EFC File Offset: 0x000020FC
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

		// Token: 0x0600005D RID: 93 RVA: 0x00003F2B File Offset: 0x0000212B
		internal bool LocalMinimaPending()
		{
			return this.m_CurrentLM != null;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003F38 File Offset: 0x00002138
		internal OutRec CreateOutRec()
		{
			OutRec result = new OutRec();
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

		// Token: 0x0600005F RID: 95 RVA: 0x00003F9C File Offset: 0x0000219C
		internal void DisposeOutRec(int index)
		{
			this.m_PolyOuts[index].Pts = null;
			this.m_PolyOuts[index] = null;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003FC0 File Offset: 0x000021C0
		internal void UpdateEdgeIntoAEL(ref TEdge e)
		{
			if (e.NextInLML == null)
			{
				throw new ClipperException("UpdateEdgeIntoAEL: invalid call");
			}
			TEdge AelPrev = e.PrevInAEL;
			TEdge AelNext = e.NextInAEL;
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
			if (!ClipperBase.IsHorizontal(e))
			{
				this.InsertScanbeam(e.Top.Y);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000040C4 File Offset: 0x000022C4
		internal void SwapPositionsInAEL(TEdge edge1, TEdge edge2)
		{
			if (edge1.NextInAEL == edge1.PrevInAEL || edge2.NextInAEL == edge2.PrevInAEL)
			{
				return;
			}
			if (edge1.NextInAEL == edge2)
			{
				TEdge next = edge2.NextInAEL;
				if (next != null)
				{
					next.PrevInAEL = edge1;
				}
				TEdge prev = edge1.PrevInAEL;
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
				TEdge next2 = edge1.NextInAEL;
				if (next2 != null)
				{
					next2.PrevInAEL = edge2;
				}
				TEdge prev2 = edge2.PrevInAEL;
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
				TEdge next3 = edge1.NextInAEL;
				TEdge prev3 = edge1.PrevInAEL;
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

		// Token: 0x06000062 RID: 98 RVA: 0x00004230 File Offset: 0x00002430
		internal void DeleteFromAEL(TEdge e)
		{
			TEdge AelPrev = e.PrevInAEL;
			TEdge AelNext = e.NextInAEL;
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

		// Token: 0x04000073 RID: 115
		internal const double horizontal = -3.4E+38;

		// Token: 0x04000074 RID: 116
		internal const int Skip = -2;

		// Token: 0x04000075 RID: 117
		internal const int Unassigned = -1;

		// Token: 0x04000076 RID: 118
		internal const double tolerance = 1E-20;

		// Token: 0x04000077 RID: 119
		public const long loRange = 1073741823L;

		// Token: 0x04000078 RID: 120
		public const long hiRange = 4611686018427387903L;

		// Token: 0x04000079 RID: 121
		internal LocalMinima m_MinimaList;

		// Token: 0x0400007A RID: 122
		internal LocalMinima m_CurrentLM;

		// Token: 0x0400007B RID: 123
		internal List<List<TEdge>> m_edges = new List<List<TEdge>>();

		// Token: 0x0400007C RID: 124
		internal Scanbeam m_Scanbeam;

		// Token: 0x0400007D RID: 125
		internal List<OutRec> m_PolyOuts;

		// Token: 0x0400007E RID: 126
		internal TEdge m_ActiveEdges;

		// Token: 0x0400007F RID: 127
		internal bool m_UseFullRange;

		// Token: 0x04000080 RID: 128
		internal bool m_HasOpenPaths;
	}
}
