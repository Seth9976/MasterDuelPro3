using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000027 RID: 39
	internal class SqliteConnectionHandle : CriticalHandle
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x0000D23C File Offset: 0x0000B43C
		private SqliteConnectionHandle(IntPtr db)
			: this()
		{
			base.SetHandle(db);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000D24B File Offset: 0x0000B44B
		internal SqliteConnectionHandle()
			: base(IntPtr.Zero)
		{
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000D258 File Offset: 0x0000B458
		protected override bool ReleaseHandle()
		{
			try
			{
				SQLiteBase.CloseConnection(this);
			}
			catch (SqliteException)
			{
			}
			return true;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000D288 File Offset: 0x0000B488
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000D29A File Offset: 0x0000B49A
		public static implicit operator IntPtr(SqliteConnectionHandle db)
		{
			return db.handle;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000D2A2 File Offset: 0x0000B4A2
		public static implicit operator SqliteConnectionHandle(IntPtr db)
		{
			return new SqliteConnectionHandle(db);
		}
	}
}
