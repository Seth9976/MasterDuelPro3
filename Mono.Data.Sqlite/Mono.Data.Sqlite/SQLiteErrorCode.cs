using System;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000017 RID: 23
	public enum SQLiteErrorCode
	{
		// Token: 0x04000064 RID: 100
		Ok,
		// Token: 0x04000065 RID: 101
		Error,
		// Token: 0x04000066 RID: 102
		Internal,
		// Token: 0x04000067 RID: 103
		Perm,
		// Token: 0x04000068 RID: 104
		Abort,
		// Token: 0x04000069 RID: 105
		Busy,
		// Token: 0x0400006A RID: 106
		Locked,
		// Token: 0x0400006B RID: 107
		NoMem,
		// Token: 0x0400006C RID: 108
		ReadOnly,
		// Token: 0x0400006D RID: 109
		Interrupt,
		// Token: 0x0400006E RID: 110
		IOErr,
		// Token: 0x0400006F RID: 111
		Corrupt,
		// Token: 0x04000070 RID: 112
		NotFound,
		// Token: 0x04000071 RID: 113
		Full,
		// Token: 0x04000072 RID: 114
		CantOpen,
		// Token: 0x04000073 RID: 115
		Protocol,
		// Token: 0x04000074 RID: 116
		Empty,
		// Token: 0x04000075 RID: 117
		Schema,
		// Token: 0x04000076 RID: 118
		TooBig,
		// Token: 0x04000077 RID: 119
		Constraint,
		// Token: 0x04000078 RID: 120
		Mismatch,
		// Token: 0x04000079 RID: 121
		Misuse,
		// Token: 0x0400007A RID: 122
		NOLFS,
		// Token: 0x0400007B RID: 123
		Auth,
		// Token: 0x0400007C RID: 124
		Format,
		// Token: 0x0400007D RID: 125
		Range,
		// Token: 0x0400007E RID: 126
		NotADatabase,
		// Token: 0x0400007F RID: 127
		Row = 100,
		// Token: 0x04000080 RID: 128
		Done
	}
}
