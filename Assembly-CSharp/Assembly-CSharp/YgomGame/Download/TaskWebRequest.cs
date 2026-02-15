using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace YgomGame.Download
{
	// Token: 0x02000F5E RID: 3934
	public class TaskWebRequest : TaskBase
	{
		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x060073E4 RID: 29668 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool m_updateProgressTimeOut
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x060073E5 RID: 29669 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060073E6 RID: 29670 RVA: 0x0000216D File Offset: 0x0000036D
		public byte[] Bytes
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x060073E7 RID: 29671 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060073E8 RID: 29672 RVA: 0x0000216D File Offset: 0x0000036D
		public string Text
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x060073E9 RID: 29673 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060073EA RID: 29674 RVA: 0x0000216D File Offset: 0x0000036D
		public DownloadErrorCode errorCode
		{
			[CompilerGenerated]
			get
			{
				return DownloadErrorCode.None;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x060073EB RID: 29675 RVA: 0x00002739 File Offset: 0x00000939
		public TaskWebRequest(string baseUrl, string path, TaskWebRequest.RequestType requestType)
		{
		}

		// Token: 0x060073EC RID: 29676 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x060073ED RID: 29677 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool IsSuccess()
		{
			return false;
		}

		// Token: 0x060073EE RID: 29678 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsError()
		{
			return false;
		}

		// Token: 0x060073EF RID: 29679 RVA: 0x0000216D File Offset: 0x0000036D
		public void Cancel()
		{
		}

		// Token: 0x060073F0 RID: 29680 RVA: 0x0000216D File Offset: 0x0000036D
		public void Exec()
		{
		}

		// Token: 0x060073F1 RID: 29681 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yExec()
		{
			return null;
		}

		// Token: 0x060073F2 RID: 29682 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual IEnumerator yProgress()
		{
			return null;
		}

		// Token: 0x060073F3 RID: 29683 RVA: 0x0000216A File Offset: 0x0000036A
		protected IEnumerator yRequest()
		{
			return null;
		}

		// Token: 0x0400ACDD RID: 44253
		private const float DownloadTimeOut = 60f;

		// Token: 0x0400ACDE RID: 44254
		private const ulong kThresholdDLBytes = 1024UL;

		// Token: 0x0400ACDF RID: 44255
		protected string m_baseUrl;

		// Token: 0x0400ACE0 RID: 44256
		protected string m_path;

		// Token: 0x0400ACE1 RID: 44257
		protected TaskWebRequest.RequestType m_requestType;

		// Token: 0x0400ACE2 RID: 44258
		private IEnumerator m_executor;

		// Token: 0x0400ACE3 RID: 44259
		protected bool m_success;

		// Token: 0x0400ACE4 RID: 44260
		protected bool m_cancel;

		// Token: 0x02000F5F RID: 3935
		public enum RequestType
		{
			// Token: 0x0400ACE6 RID: 44262
			Bytes,
			// Token: 0x0400ACE7 RID: 44263
			Text
		}
	}
}
