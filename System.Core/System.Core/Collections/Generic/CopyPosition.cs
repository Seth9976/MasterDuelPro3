using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000153 RID: 339
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal readonly struct CopyPosition
	{
		// Token: 0x06000B39 RID: 2873 RVA: 0x0002C1AC File Offset: 0x0002A3AC
		internal CopyPosition(int row, int column)
		{
			this.Row = row;
			this.Column = column;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0002C1BC File Offset: 0x0002A3BC
		public static CopyPosition Start
		{
			get
			{
				return default(CopyPosition);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0002C1D2 File Offset: 0x0002A3D2
		internal int Row { get; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0002C1DA File Offset: 0x0002A3DA
		internal int Column { get; }

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002C1E2 File Offset: 0x0002A3E2
		public CopyPosition Normalize(int endColumn)
		{
			if (this.Column != endColumn)
			{
				return this;
			}
			return new CopyPosition(this.Row + 1, 0);
		}
	}
}
