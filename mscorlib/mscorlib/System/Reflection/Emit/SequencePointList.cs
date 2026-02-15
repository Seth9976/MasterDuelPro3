using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x0200066F RID: 1647
	internal class SequencePointList
	{
		// Token: 0x06003279 RID: 12921 RVA: 0x000BD818 File Offset: 0x000BBA18
		public SequencePointList(ISymbolDocumentWriter doc)
		{
			this.doc = doc;
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x000BD827 File Offset: 0x000BBA27
		public ISymbolDocumentWriter Document
		{
			get
			{
				return this.doc;
			}
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x000BD830 File Offset: 0x000BBA30
		public int[] GetOffsets()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Offset;
			}
			return array;
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000BD870 File Offset: 0x000BBA70
		public int[] GetLines()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Line;
			}
			return array;
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x000BD8B0 File Offset: 0x000BBAB0
		public int[] GetColumns()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Col;
			}
			return array;
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x000BD8F0 File Offset: 0x000BBAF0
		public int[] GetEndLines()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].EndLine;
			}
			return array;
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x000BD930 File Offset: 0x000BBB30
		public int[] GetEndColumns()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].EndCol;
			}
			return array;
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06003280 RID: 12928 RVA: 0x000BD96F File Offset: 0x000BBB6F
		public int StartLine
		{
			get
			{
				return this.points[0].Line;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x000BD982 File Offset: 0x000BBB82
		public int EndLine
		{
			get
			{
				return this.points[this.count - 1].Line;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06003282 RID: 12930 RVA: 0x000BD99C File Offset: 0x000BBB9C
		public int StartColumn
		{
			get
			{
				return this.points[0].Col;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x000BD9AF File Offset: 0x000BBBAF
		public int EndColumn
		{
			get
			{
				return this.points[this.count - 1].Col;
			}
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000BD9CC File Offset: 0x000BBBCC
		public void AddSequencePoint(int offset, int line, int col, int endLine, int endCol)
		{
			SequencePoint sequencePoint = default(SequencePoint);
			sequencePoint.Offset = offset;
			sequencePoint.Line = line;
			sequencePoint.Col = col;
			sequencePoint.EndLine = endLine;
			sequencePoint.EndCol = endCol;
			if (this.points == null)
			{
				this.points = new SequencePoint[10];
			}
			else if (this.count >= this.points.Length)
			{
				SequencePoint[] array = new SequencePoint[this.count + 10];
				Array.Copy(this.points, array, this.points.Length);
				this.points = array;
			}
			this.points[this.count] = sequencePoint;
			this.count++;
		}

		// Token: 0x0400199B RID: 6555
		private ISymbolDocumentWriter doc;

		// Token: 0x0400199C RID: 6556
		private SequencePoint[] points;

		// Token: 0x0400199D RID: 6557
		private int count;
	}
}
