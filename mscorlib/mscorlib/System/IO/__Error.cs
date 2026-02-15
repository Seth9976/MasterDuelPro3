using System;
using System.Security;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x020007D4 RID: 2004
	internal static class __Error
	{
		// Token: 0x06004021 RID: 16417 RVA: 0x000F6AFE File Offset: 0x000F4CFE
		internal static void EndOfFile()
		{
			throw new EndOfStreamException(Environment.GetResourceString("Unable to read beyond the end of the stream."));
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x000F6B0F File Offset: 0x000F4D0F
		internal static void FileNotOpen()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed file."));
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x000F6B21 File Offset: 0x000F4D21
		internal static void StreamIsClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed Stream."));
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x000F6B33 File Offset: 0x000F4D33
		internal static void ReaderClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot read from a closed TextReader."));
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x000F6B48 File Offset: 0x000F4D48
		internal static string GetDisplayablePath(string path, bool isInvalidPath)
		{
			if (string.IsNullOrEmpty(path))
			{
				return string.Empty;
			}
			if (path.Length < 2)
			{
				return path;
			}
			if (PathInternal.IsPartiallyQualified(path) && !isInvalidPath)
			{
				return path;
			}
			bool flag = false;
			try
			{
				if (!isInvalidPath)
				{
					flag = true;
				}
			}
			catch (SecurityException)
			{
			}
			catch (ArgumentException)
			{
			}
			catch (NotSupportedException)
			{
			}
			if (!flag)
			{
				if (Path.IsDirectorySeparator(path[path.Length - 1]))
				{
					path = Environment.GetResourceString("<Path discovery permission to the specified directory was denied.>");
				}
				else
				{
					path = Path.GetFileName(path);
				}
			}
			return path;
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x000F6BE4 File Offset: 0x000F4DE4
		internal static void WinIOError(int errorCode, string maybeFullPath)
		{
			bool flag = errorCode == 123 || errorCode == 161;
			string displayablePath = __Error.GetDisplayablePath(maybeFullPath, flag);
			if (errorCode <= 80)
			{
				if (errorCode <= 15)
				{
					switch (errorCode)
					{
					case 2:
						if (displayablePath.Length == 0)
						{
							throw new FileNotFoundException(Environment.GetResourceString("Unable to find the specified file."));
						}
						throw new FileNotFoundException(Environment.GetResourceString("Could not find file '{0}'.", new object[] { displayablePath }), displayablePath);
					case 3:
						if (displayablePath.Length == 0)
						{
							throw new DirectoryNotFoundException(Environment.GetResourceString("Could not find a part of the path."));
						}
						throw new DirectoryNotFoundException(Environment.GetResourceString("Could not find a part of the path '{0}'.", new object[] { displayablePath }));
					case 4:
						break;
					case 5:
						if (displayablePath.Length == 0)
						{
							throw new UnauthorizedAccessException(Environment.GetResourceString("Access to the path is denied."));
						}
						throw new UnauthorizedAccessException(Environment.GetResourceString("Access to the path '{0}' is denied.", new object[] { displayablePath }));
					default:
						if (errorCode == 15)
						{
							throw new DriveNotFoundException(Environment.GetResourceString("Could not find the drive '{0}'. The drive might not be ready or might not be mapped.", new object[] { displayablePath }));
						}
						break;
					}
				}
				else if (errorCode != 32)
				{
					if (errorCode == 80)
					{
						if (displayablePath.Length != 0)
						{
							throw new IOException(Environment.GetResourceString("The file '{0}' already exists.", new object[] { displayablePath }), Win32Native.MakeHRFromErrorCode(errorCode));
						}
					}
				}
				else
				{
					if (displayablePath.Length == 0)
					{
						throw new IOException(Environment.GetResourceString("The process cannot access the file because it is being used by another process."), Win32Native.MakeHRFromErrorCode(errorCode));
					}
					throw new IOException(Environment.GetResourceString("The process cannot access the file '{0}' because it is being used by another process.", new object[] { displayablePath }), Win32Native.MakeHRFromErrorCode(errorCode));
				}
			}
			else if (errorCode <= 183)
			{
				if (errorCode == 87)
				{
					throw new IOException(Win32Native.GetMessage(errorCode), Win32Native.MakeHRFromErrorCode(errorCode));
				}
				if (errorCode == 183)
				{
					if (displayablePath.Length != 0)
					{
						throw new IOException(Environment.GetResourceString("Cannot create \"{0}\" because a file or directory with the same name already exists.", new object[] { displayablePath }), Win32Native.MakeHRFromErrorCode(errorCode));
					}
				}
			}
			else
			{
				if (errorCode == 206)
				{
					throw new PathTooLongException(Environment.GetResourceString("The specified path, file name, or both are too long. The fully qualified file name must be less than 260 characters, and the directory name must be less than 248 characters."));
				}
				if (errorCode == 995)
				{
					throw new OperationCanceledException();
				}
			}
			throw new IOException(Win32Native.GetMessage(errorCode), Win32Native.MakeHRFromErrorCode(errorCode));
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x000F6E04 File Offset: 0x000F5004
		internal static void WriterClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot write to a closed TextWriter."));
		}
	}
}
