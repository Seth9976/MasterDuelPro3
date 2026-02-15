using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000669 RID: 1641
	internal struct ILExceptionInfo
	{
		// Token: 0x0600322C RID: 12844 RVA: 0x000BB47C File Offset: 0x000B967C
		internal void AddCatch(Type extype, int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 0;
			this.handlers[num].start = offset;
			this.handlers[num].extype = extype;
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x000BB4D8 File Offset: 0x000B96D8
		internal void AddFinally(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 2;
			this.handlers[num].start = offset;
			this.handlers[num].extype = null;
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000BB534 File Offset: 0x000B9734
		internal void AddFault(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 4;
			this.handlers[num].start = offset;
			this.handlers[num].extype = null;
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x000BB590 File Offset: 0x000B9790
		internal void AddFilter(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = -1;
			this.handlers[num].extype = null;
			this.handlers[num].filter_offset = offset;
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x000BB5EC File Offset: 0x000B97EC
		internal void End(int offset)
		{
			if (this.handlers == null)
			{
				return;
			}
			int num = this.handlers.Length - 1;
			if (num >= 0)
			{
				this.handlers[num].len = offset - this.handlers[num].start;
			}
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x000BB635 File Offset: 0x000B9835
		internal int LastClauseType()
		{
			if (this.handlers != null)
			{
				return this.handlers[this.handlers.Length - 1].type;
			}
			return 0;
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x000BB65C File Offset: 0x000B985C
		internal void PatchFilterClause(int start)
		{
			if (this.handlers != null && this.handlers.Length != 0)
			{
				this.handlers[this.handlers.Length - 1].start = start;
				this.handlers[this.handlers.Length - 1].type = 1;
			}
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x00002C89 File Offset: 0x00000E89
		internal void Debug(int b)
		{
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000BB6B0 File Offset: 0x000B98B0
		private void add_block(int offset)
		{
			if (this.handlers != null)
			{
				int num = this.handlers.Length;
				ILExceptionBlock[] array = new ILExceptionBlock[num + 1];
				Array.Copy(this.handlers, array, num);
				this.handlers = array;
				this.handlers[num].len = offset - this.handlers[num].start;
				return;
			}
			this.handlers = new ILExceptionBlock[1];
			this.len = offset - this.start;
		}

		// Token: 0x0400197B RID: 6523
		internal ILExceptionBlock[] handlers;

		// Token: 0x0400197C RID: 6524
		internal int start;

		// Token: 0x0400197D RID: 6525
		internal int len;

		// Token: 0x0400197E RID: 6526
		internal Label end;
	}
}
