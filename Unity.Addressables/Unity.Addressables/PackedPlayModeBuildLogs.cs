using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000002 RID: 2
[Serializable]
public class PackedPlayModeBuildLogs
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	public List<PackedPlayModeBuildLogs.RuntimeBuildLog> RuntimeBuildLogs
	{
		get
		{
			return this.m_RuntimeBuildLogs;
		}
		set
		{
			this.m_RuntimeBuildLogs = value;
		}
	}

	// Token: 0x04000001 RID: 1
	[SerializeField]
	private List<PackedPlayModeBuildLogs.RuntimeBuildLog> m_RuntimeBuildLogs = new List<PackedPlayModeBuildLogs.RuntimeBuildLog>();

	// Token: 0x02000003 RID: 3
	[Serializable]
	public struct RuntimeBuildLog
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002074 File Offset: 0x00000274
		public RuntimeBuildLog(LogType type, string message)
		{
			this.Type = type;
			this.Message = message;
		}

		// Token: 0x04000002 RID: 2
		public LogType Type;

		// Token: 0x04000003 RID: 3
		public string Message;
	}
}
