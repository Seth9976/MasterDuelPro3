using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000039 RID: 57
	public class DiskArchiveStorage : BaseArchiveStorage
	{
		// Token: 0x060001DD RID: 477 RVA: 0x00008700 File Offset: 0x00006900
		public DiskArchiveStorage(ZipFile file, FileUpdateMode updateMode)
			: base(updateMode)
		{
			if (file.Name == null)
			{
				throw new ZipException("Cant handle non file archives");
			}
			this.fileName_ = file.Name;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008728 File Offset: 0x00006928
		public DiskArchiveStorage(ZipFile file)
			: this(file, FileUpdateMode.Safe)
		{
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008732 File Offset: 0x00006932
		public override Stream GetTemporaryOutput()
		{
			this.temporaryName_ = PathUtils.GetTempFileName(this.temporaryName_);
			this.temporaryStream_ = File.Open(this.temporaryName_, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
			return this.temporaryStream_;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008760 File Offset: 0x00006960
		public override Stream ConvertTemporaryToFinal()
		{
			if (this.temporaryStream_ == null)
			{
				throw new ZipException("No temporary stream has been created");
			}
			Stream stream = null;
			string tempFileName = PathUtils.GetTempFileName(this.fileName_);
			bool flag = false;
			try
			{
				this.temporaryStream_.Dispose();
				File.Move(this.fileName_, tempFileName);
				File.Move(this.temporaryName_, this.fileName_);
				flag = true;
				File.Delete(tempFileName);
				stream = File.Open(this.fileName_, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			catch (Exception)
			{
				stream = null;
				if (!flag)
				{
					File.Move(tempFileName, this.fileName_);
					File.Delete(this.temporaryName_);
				}
				throw;
			}
			return stream;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008804 File Offset: 0x00006A04
		public override Stream MakeTemporaryCopy(Stream stream)
		{
			stream.Dispose();
			this.temporaryName_ = PathUtils.GetTempFileName(this.fileName_);
			File.Copy(this.fileName_, this.temporaryName_, true);
			this.temporaryStream_ = new FileStream(this.temporaryName_, FileMode.Open, FileAccess.ReadWrite);
			return this.temporaryStream_;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008854 File Offset: 0x00006A54
		public override Stream OpenForDirectUpdate(Stream stream)
		{
			Stream stream2;
			if (stream == null || !stream.CanWrite)
			{
				if (stream != null)
				{
					stream.Dispose();
				}
				stream2 = new FileStream(this.fileName_, FileMode.Open, FileAccess.ReadWrite);
			}
			else
			{
				stream2 = stream;
			}
			return stream2;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008888 File Offset: 0x00006A88
		public override void Dispose()
		{
			if (this.temporaryStream_ != null)
			{
				this.temporaryStream_.Dispose();
			}
		}

		// Token: 0x0400011C RID: 284
		private Stream temporaryStream_;

		// Token: 0x0400011D RID: 285
		private readonly string fileName_;

		// Token: 0x0400011E RID: 286
		private string temporaryName_;
	}
}
