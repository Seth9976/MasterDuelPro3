using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200003A RID: 58
	public class MemoryArchiveStorage : BaseArchiveStorage
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x0000889D File Offset: 0x00006A9D
		public MemoryArchiveStorage()
			: base(FileUpdateMode.Direct)
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000088A6 File Offset: 0x00006AA6
		public MemoryArchiveStorage(FileUpdateMode updateMode)
			: base(updateMode)
		{
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000088AF File Offset: 0x00006AAF
		public MemoryStream FinalStream
		{
			get
			{
				return this.finalStream_;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000088B7 File Offset: 0x00006AB7
		public override Stream GetTemporaryOutput()
		{
			this.temporaryStream_ = new MemoryStream();
			return this.temporaryStream_;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000088CA File Offset: 0x00006ACA
		public override Stream ConvertTemporaryToFinal()
		{
			if (this.temporaryStream_ == null)
			{
				throw new ZipException("No temporary stream has been created");
			}
			this.finalStream_ = new MemoryStream(this.temporaryStream_.ToArray());
			return this.finalStream_;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000088FB File Offset: 0x00006AFB
		public override Stream MakeTemporaryCopy(Stream stream)
		{
			this.temporaryStream_ = new MemoryStream();
			stream.Position = 0L;
			StreamUtils.Copy(stream, this.temporaryStream_, new byte[4096]);
			return this.temporaryStream_;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000892C File Offset: 0x00006B2C
		public override Stream OpenForDirectUpdate(Stream stream)
		{
			Stream stream2;
			if (stream == null || !stream.CanWrite)
			{
				stream2 = new MemoryStream();
				if (stream != null)
				{
					stream.Position = 0L;
					StreamUtils.Copy(stream, stream2, new byte[4096]);
					stream.Dispose();
				}
			}
			else
			{
				stream2 = stream;
			}
			return stream2;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008971 File Offset: 0x00006B71
		public override void Dispose()
		{
			if (this.temporaryStream_ != null)
			{
				this.temporaryStream_.Dispose();
			}
		}

		// Token: 0x0400011F RID: 287
		private MemoryStream temporaryStream_;

		// Token: 0x04000120 RID: 288
		private MemoryStream finalStream_;
	}
}
