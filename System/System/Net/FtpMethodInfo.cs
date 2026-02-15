using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x02000395 RID: 917
	internal class FtpMethodInfo
	{
		// Token: 0x060016F1 RID: 5873 RVA: 0x000621EC File Offset: 0x000603EC
		internal FtpMethodInfo(string method, FtpOperation operation, FtpMethodFlags flags, string httpCommand)
		{
			this.Method = method;
			this.Operation = operation;
			this.Flags = flags;
			this.HttpCommand = httpCommand;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00062211 File Offset: 0x00060411
		internal bool HasFlag(FtpMethodFlags flags)
		{
			return (this.Flags & flags) > FtpMethodFlags.None;
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0006221E File Offset: 0x0006041E
		internal bool IsCommandOnly
		{
			get
			{
				return (this.Flags & (FtpMethodFlags.IsDownload | FtpMethodFlags.IsUpload)) == FtpMethodFlags.None;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x0006222B File Offset: 0x0006042B
		internal bool IsUpload
		{
			get
			{
				return (this.Flags & FtpMethodFlags.IsUpload) > FtpMethodFlags.None;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00062238 File Offset: 0x00060438
		internal bool IsDownload
		{
			get
			{
				return (this.Flags & FtpMethodFlags.IsDownload) > FtpMethodFlags.None;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x00062245 File Offset: 0x00060445
		internal bool ShouldParseForResponseUri
		{
			get
			{
				return (this.Flags & FtpMethodFlags.ShouldParseForResponseUri) > FtpMethodFlags.None;
			}
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00062254 File Offset: 0x00060454
		internal static FtpMethodInfo GetMethodInfo(string method)
		{
			method = method.ToUpper(CultureInfo.InvariantCulture);
			foreach (FtpMethodInfo ftpMethodInfo in FtpMethodInfo.s_knownMethodInfo)
			{
				if (method == ftpMethodInfo.Method)
				{
					return ftpMethodInfo;
				}
			}
			throw new ArgumentException("This method is not supported.", "method");
		}

		// Token: 0x04000E12 RID: 3602
		internal string Method;

		// Token: 0x04000E13 RID: 3603
		internal FtpOperation Operation;

		// Token: 0x04000E14 RID: 3604
		internal FtpMethodFlags Flags;

		// Token: 0x04000E15 RID: 3605
		internal string HttpCommand;

		// Token: 0x04000E16 RID: 3606
		private static readonly FtpMethodInfo[] s_knownMethodInfo = new FtpMethodInfo[]
		{
			new FtpMethodInfo("RETR", FtpOperation.DownloadFile, FtpMethodFlags.IsDownload | FtpMethodFlags.TakesParameter | FtpMethodFlags.HasHttpCommand, "GET"),
			new FtpMethodInfo("NLST", FtpOperation.ListDirectory, FtpMethodFlags.IsDownload | FtpMethodFlags.MayTakeParameter | FtpMethodFlags.HasHttpCommand | FtpMethodFlags.MustChangeWorkingDirectoryToPath, "GET"),
			new FtpMethodInfo("LIST", FtpOperation.ListDirectoryDetails, FtpMethodFlags.IsDownload | FtpMethodFlags.MayTakeParameter | FtpMethodFlags.HasHttpCommand | FtpMethodFlags.MustChangeWorkingDirectoryToPath, "GET"),
			new FtpMethodInfo("STOR", FtpOperation.UploadFile, FtpMethodFlags.IsUpload | FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("STOU", FtpOperation.UploadFileUnique, FtpMethodFlags.IsUpload | FtpMethodFlags.DoesNotTakeParameter | FtpMethodFlags.ShouldParseForResponseUri | FtpMethodFlags.MustChangeWorkingDirectoryToPath, null),
			new FtpMethodInfo("APPE", FtpOperation.AppendFile, FtpMethodFlags.IsUpload | FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("DELE", FtpOperation.DeleteFile, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("MDTM", FtpOperation.GetDateTimestamp, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("SIZE", FtpOperation.GetFileSize, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("RENAME", FtpOperation.Rename, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("MKD", FtpOperation.MakeDirectory, FtpMethodFlags.TakesParameter | FtpMethodFlags.ParameterIsDirectory, null),
			new FtpMethodInfo("RMD", FtpOperation.RemoveDirectory, FtpMethodFlags.TakesParameter | FtpMethodFlags.ParameterIsDirectory, null),
			new FtpMethodInfo("PWD", FtpOperation.PrintWorkingDirectory, FtpMethodFlags.DoesNotTakeParameter, null)
		};
	}
}
