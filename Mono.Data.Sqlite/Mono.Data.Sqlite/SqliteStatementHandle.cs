using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000028 RID: 40
	internal class SqliteStatementHandle : CriticalHandle
	{
		// Token: 0x060001DB RID: 475 RVA: 0x0000D2AA File Offset: 0x0000B4AA
		private SqliteStatementHandle(IntPtr stmt)
			: this()
		{
			base.SetHandle(stmt);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000D24B File Offset: 0x0000B44B
		internal SqliteStatementHandle()
			: base(IntPtr.Zero)
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		protected override bool ReleaseHandle()
		{
			try
			{
				SQLiteBase.FinalizeStatement(this);
			}
			catch (SqliteException)
			{
			}
			return true;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000D288 File Offset: 0x0000B488
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000D29A File Offset: 0x0000B49A
		public static implicit operator IntPtr(SqliteStatementHandle stmt)
		{
			return stmt.handle;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000D2EC File Offset: 0x0000B4EC
		public static implicit operator SqliteStatementHandle(IntPtr stmt)
		{
			return new SqliteStatementHandle(stmt);
		}
	}
}
