using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000026 RID: 38
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06000195 RID: 405
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_close(IntPtr db);

		// Token: 0x06000196 RID: 406
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_create_function(IntPtr db, byte[] strName, int nArgs, int nType, IntPtr pvUser, SQLiteCallback func, SQLiteCallback fstep, SQLiteFinalCallback ffinal);

		// Token: 0x06000197 RID: 407
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_finalize(IntPtr stmt);

		// Token: 0x06000198 RID: 408
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_open_v2(byte[] utf8Filename, out IntPtr db, int flags, IntPtr vfs);

		// Token: 0x06000199 RID: 409
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_open(byte[] utf8Filename, out IntPtr db);

		// Token: 0x0600019A RID: 410
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern int sqlite3_open16(string fileName, out IntPtr db);

		// Token: 0x0600019B RID: 411
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_reset(IntPtr stmt);

		// Token: 0x0600019C RID: 412
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_bind_parameter_name(IntPtr stmt, int index);

		// Token: 0x0600019D RID: 413
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_database_name(IntPtr stmt, int index);

		// Token: 0x0600019E RID: 414
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_database_name16(IntPtr stmt, int index);

		// Token: 0x0600019F RID: 415
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_decltype(IntPtr stmt, int index);

		// Token: 0x060001A0 RID: 416
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_name(IntPtr stmt, int index);

		// Token: 0x060001A1 RID: 417
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_name16(IntPtr stmt, int index);

		// Token: 0x060001A2 RID: 418
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_origin_name(IntPtr stmt, int index);

		// Token: 0x060001A3 RID: 419
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_origin_name16(IntPtr stmt, int index);

		// Token: 0x060001A4 RID: 420
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_table_name(IntPtr stmt, int index);

		// Token: 0x060001A5 RID: 421
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_table_name16(IntPtr stmt, int index);

		// Token: 0x060001A6 RID: 422
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_text(IntPtr stmt, int index);

		// Token: 0x060001A7 RID: 423
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_text16(IntPtr stmt, int index);

		// Token: 0x060001A8 RID: 424
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_errmsg(IntPtr db);

		// Token: 0x060001A9 RID: 425
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_prepare(IntPtr db, IntPtr pSql, int nBytes, out IntPtr stmt, out IntPtr ptrRemain);

		// Token: 0x060001AA RID: 426
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_table_column_metadata(IntPtr db, byte[] dbName, byte[] tblName, byte[] colName, out IntPtr ptrDataType, out IntPtr ptrCollSeq, out int notNull, out int primaryKey, out int autoInc);

		// Token: 0x060001AB RID: 427
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_value_text(IntPtr p);

		// Token: 0x060001AC RID: 428
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_value_text16(IntPtr p);

		// Token: 0x060001AD RID: 429
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_libversion();

		// Token: 0x060001AE RID: 430
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_changes(IntPtr db);

		// Token: 0x060001AF RID: 431
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_busy_timeout(IntPtr db, int ms);

		// Token: 0x060001B0 RID: 432
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_blob(IntPtr stmt, int index, byte[] value, int nSize, IntPtr nTransient);

		// Token: 0x060001B1 RID: 433
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_double(IntPtr stmt, int index, double value);

		// Token: 0x060001B2 RID: 434
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_int(IntPtr stmt, int index, int value);

		// Token: 0x060001B3 RID: 435
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_int64(IntPtr stmt, int index, long value);

		// Token: 0x060001B4 RID: 436
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_null(IntPtr stmt, int index);

		// Token: 0x060001B5 RID: 437
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_text(IntPtr stmt, int index, byte[] value, int nlen, IntPtr pvReserved);

		// Token: 0x060001B6 RID: 438
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_parameter_count(IntPtr stmt);

		// Token: 0x060001B7 RID: 439
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_column_count(IntPtr stmt);

		// Token: 0x060001B8 RID: 440
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_step(IntPtr stmt);

		// Token: 0x060001B9 RID: 441
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern double sqlite3_column_double(IntPtr stmt, int index);

		// Token: 0x060001BA RID: 442
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_column_int(IntPtr stmt, int index);

		// Token: 0x060001BB RID: 443
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern long sqlite3_column_int64(IntPtr stmt, int index);

		// Token: 0x060001BC RID: 444
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_blob(IntPtr stmt, int index);

		// Token: 0x060001BD RID: 445
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_column_bytes(IntPtr stmt, int index);

		// Token: 0x060001BE RID: 446
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern TypeAffinity sqlite3_column_type(IntPtr stmt, int index);

		// Token: 0x060001BF RID: 447
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_create_collation(IntPtr db, byte[] strName, int nType, IntPtr pvUser, SQLiteCollation func);

		// Token: 0x060001C0 RID: 448
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_value_blob(IntPtr p);

		// Token: 0x060001C1 RID: 449
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_value_bytes(IntPtr p);

		// Token: 0x060001C2 RID: 450
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern double sqlite3_value_double(IntPtr p);

		// Token: 0x060001C3 RID: 451
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern long sqlite3_value_int64(IntPtr p);

		// Token: 0x060001C4 RID: 452
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern TypeAffinity sqlite3_value_type(IntPtr p);

		// Token: 0x060001C5 RID: 453
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_blob(IntPtr context, byte[] value, int nSize, IntPtr pvReserved);

		// Token: 0x060001C6 RID: 454
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_double(IntPtr context, double value);

		// Token: 0x060001C7 RID: 455
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_error(IntPtr context, byte[] strErr, int nLen);

		// Token: 0x060001C8 RID: 456
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_int64(IntPtr context, long value);

		// Token: 0x060001C9 RID: 457
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_null(IntPtr context);

		// Token: 0x060001CA RID: 458
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_text(IntPtr context, byte[] value, int nLen, IntPtr pvReserved);

		// Token: 0x060001CB RID: 459
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_aggregate_context(IntPtr context, int nBytes);

		// Token: 0x060001CC RID: 460
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern int sqlite3_bind_text16(IntPtr stmt, int index, string value, int nlen, IntPtr pvReserved);

		// Token: 0x060001CD RID: 461
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern void sqlite3_result_error16(IntPtr context, string strName, int nLen);

		// Token: 0x060001CE RID: 462
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern void sqlite3_result_text16(IntPtr context, string strName, int nLen, IntPtr pvReserved);

		// Token: 0x060001CF RID: 463
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_key(IntPtr db, byte[] key, int keylen);

		// Token: 0x060001D0 RID: 464
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_update_hook(IntPtr db, SQLiteUpdateCallback func, IntPtr pvUser);

		// Token: 0x060001D1 RID: 465
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_commit_hook(IntPtr db, SQLiteCommitCallback func, IntPtr pvUser);

		// Token: 0x060001D2 RID: 466
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_rollback_hook(IntPtr db, SQLiteRollbackCallback func, IntPtr pvUser);

		// Token: 0x060001D3 RID: 467
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_next_stmt(IntPtr db, IntPtr stmt);

		// Token: 0x060001D4 RID: 468
		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_exec(IntPtr db, byte[] strSql, IntPtr pvCallback, IntPtr pvParam, out IntPtr errMsg);
	}
}
