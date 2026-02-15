using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000026 RID: 38
	public class TestStatus
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00004D90 File Offset: 0x00002F90
		public TestStatus(ZipFile file)
		{
			this.file_ = file;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004D9F File Offset: 0x00002F9F
		public TestOperation Operation
		{
			get
			{
				return this.operation_;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004DA7 File Offset: 0x00002FA7
		public ZipFile File
		{
			get
			{
				return this.file_;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004DAF File Offset: 0x00002FAF
		public ZipEntry Entry
		{
			get
			{
				return this.entry_;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004DB7 File Offset: 0x00002FB7
		public int ErrorCount
		{
			get
			{
				return this.errorCount_;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004DBF File Offset: 0x00002FBF
		public long BytesTested
		{
			get
			{
				return this.bytesTested_;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00004DC7 File Offset: 0x00002FC7
		public bool EntryValid
		{
			get
			{
				return this.entryValid_;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004DCF File Offset: 0x00002FCF
		internal void AddError()
		{
			this.errorCount_++;
			this.entryValid_ = false;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004DE6 File Offset: 0x00002FE6
		internal void SetOperation(TestOperation operation)
		{
			this.operation_ = operation;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004DEF File Offset: 0x00002FEF
		internal void SetEntry(ZipEntry entry)
		{
			this.entry_ = entry;
			this.entryValid_ = true;
			this.bytesTested_ = 0L;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004E07 File Offset: 0x00003007
		internal void SetBytesTested(long value)
		{
			this.bytesTested_ = value;
		}

		// Token: 0x040000DA RID: 218
		private readonly ZipFile file_;

		// Token: 0x040000DB RID: 219
		private ZipEntry entry_;

		// Token: 0x040000DC RID: 220
		private bool entryValid_;

		// Token: 0x040000DD RID: 221
		private int errorCount_;

		// Token: 0x040000DE RID: 222
		private long bytesTested_;

		// Token: 0x040000DF RID: 223
		private TestOperation operation_;
	}
}
