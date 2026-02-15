using System;

namespace Unity.Profiling.Memory
{
	// Token: 0x02000036 RID: 54
	public class MemorySnapshotMetadata
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002FF2 File Offset: 0x000011F2
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002FFA File Offset: 0x000011FA
		public string Description { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003003 File Offset: 0x00001203
		internal byte[] Data { get; }
	}
}
