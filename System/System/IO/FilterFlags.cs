using System;

namespace System.IO
{
	// Token: 0x0200035F RID: 863
	[Flags]
	internal enum FilterFlags : uint
	{
		// Token: 0x04000CAB RID: 3243
		ReadPoll = 4096U,
		// Token: 0x04000CAC RID: 3244
		ReadOutOfBand = 8192U,
		// Token: 0x04000CAD RID: 3245
		ReadLowWaterMark = 1U,
		// Token: 0x04000CAE RID: 3246
		WriteLowWaterMark = 1U,
		// Token: 0x04000CAF RID: 3247
		NoteTrigger = 16777216U,
		// Token: 0x04000CB0 RID: 3248
		NoteFFNop = 0U,
		// Token: 0x04000CB1 RID: 3249
		NoteFFAnd = 1073741824U,
		// Token: 0x04000CB2 RID: 3250
		NoteFFOr = 2147483648U,
		// Token: 0x04000CB3 RID: 3251
		NoteFFCopy = 3221225472U,
		// Token: 0x04000CB4 RID: 3252
		NoteFFCtrlMask = 3221225472U,
		// Token: 0x04000CB5 RID: 3253
		NoteFFlagsMask = 16777215U,
		// Token: 0x04000CB6 RID: 3254
		VNodeDelete = 1U,
		// Token: 0x04000CB7 RID: 3255
		VNodeWrite = 2U,
		// Token: 0x04000CB8 RID: 3256
		VNodeExtend = 4U,
		// Token: 0x04000CB9 RID: 3257
		VNodeAttrib = 8U,
		// Token: 0x04000CBA RID: 3258
		VNodeLink = 16U,
		// Token: 0x04000CBB RID: 3259
		VNodeRename = 32U,
		// Token: 0x04000CBC RID: 3260
		VNodeRevoke = 64U,
		// Token: 0x04000CBD RID: 3261
		VNodeNone = 128U,
		// Token: 0x04000CBE RID: 3262
		ProcExit = 2147483648U,
		// Token: 0x04000CBF RID: 3263
		ProcFork = 1073741824U,
		// Token: 0x04000CC0 RID: 3264
		ProcExec = 536870912U,
		// Token: 0x04000CC1 RID: 3265
		ProcReap = 268435456U,
		// Token: 0x04000CC2 RID: 3266
		ProcSignal = 134217728U,
		// Token: 0x04000CC3 RID: 3267
		ProcExitStatus = 67108864U,
		// Token: 0x04000CC4 RID: 3268
		ProcResourceEnd = 33554432U,
		// Token: 0x04000CC5 RID: 3269
		ProcAppactive = 8388608U,
		// Token: 0x04000CC6 RID: 3270
		ProcAppBackground = 4194304U,
		// Token: 0x04000CC7 RID: 3271
		ProcAppNonUI = 2097152U,
		// Token: 0x04000CC8 RID: 3272
		ProcAppInactive = 1048576U,
		// Token: 0x04000CC9 RID: 3273
		ProcAppAllStates = 15728640U,
		// Token: 0x04000CCA RID: 3274
		ProcPDataMask = 1048575U,
		// Token: 0x04000CCB RID: 3275
		ProcControlMask = 4293918720U,
		// Token: 0x04000CCC RID: 3276
		VMPressure = 2147483648U,
		// Token: 0x04000CCD RID: 3277
		VMPressureTerminate = 1073741824U,
		// Token: 0x04000CCE RID: 3278
		VMPressureSuddenTerminate = 536870912U,
		// Token: 0x04000CCF RID: 3279
		VMError = 268435456U,
		// Token: 0x04000CD0 RID: 3280
		TimerSeconds = 1U,
		// Token: 0x04000CD1 RID: 3281
		TimerMicroSeconds = 2U,
		// Token: 0x04000CD2 RID: 3282
		TimerNanoSeconds = 4U,
		// Token: 0x04000CD3 RID: 3283
		TimerAbsolute = 8U
	}
}
