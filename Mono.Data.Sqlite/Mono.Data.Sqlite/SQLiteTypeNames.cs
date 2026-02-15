using System;
using System.Data;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000012 RID: 18
	internal struct SQLiteTypeNames
	{
		// Token: 0x06000105 RID: 261 RVA: 0x0000935C File Offset: 0x0000755C
		internal SQLiteTypeNames(string newtypeName, DbType newdataType)
		{
			this.typeName = newtypeName;
			this.dataType = newdataType;
		}

		// Token: 0x0400004F RID: 79
		internal string typeName;

		// Token: 0x04000050 RID: 80
		internal DbType dataType;
	}
}
