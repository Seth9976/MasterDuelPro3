using System;

namespace System.IO.Enumeration
{
	// Token: 0x020007E8 RID: 2024
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	public ref struct FileSystemEntry
	{
		// Token: 0x0600412E RID: 16686 RVA: 0x000FB9F5 File Offset: 0x000F9BF5
		internal unsafe static void Initialize(ref FileSystemEntry entry, Interop.NtDll.FILE_FULL_DIR_INFORMATION* info, ReadOnlySpan<char> directory, ReadOnlySpan<char> rootDirectory, ReadOnlySpan<char> originalRootDirectory)
		{
			entry._info = info;
			entry.Directory = directory;
			entry.RootDirectory = rootDirectory;
			entry.OriginalRootDirectory = originalRootDirectory;
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x0600412F RID: 16687 RVA: 0x000FBA14 File Offset: 0x000F9C14
		// (set) Token: 0x06004130 RID: 16688 RVA: 0x000FBA1C File Offset: 0x000F9C1C
		public ReadOnlySpan<char> Directory { readonly get; private set; }

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06004131 RID: 16689 RVA: 0x000FBA25 File Offset: 0x000F9C25
		// (set) Token: 0x06004132 RID: 16690 RVA: 0x000FBA2D File Offset: 0x000F9C2D
		public ReadOnlySpan<char> RootDirectory { readonly get; private set; }

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06004133 RID: 16691 RVA: 0x000FBA36 File Offset: 0x000F9C36
		// (set) Token: 0x06004134 RID: 16692 RVA: 0x000FBA3E File Offset: 0x000F9C3E
		public ReadOnlySpan<char> OriginalRootDirectory { readonly get; private set; }

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06004135 RID: 16693 RVA: 0x000FBA47 File Offset: 0x000F9C47
		public unsafe ReadOnlySpan<char> FileName
		{
			get
			{
				return this._info->FileName;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06004136 RID: 16694 RVA: 0x000FBA54 File Offset: 0x000F9C54
		public unsafe FileAttributes Attributes
		{
			get
			{
				return this._info->FileAttributes;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06004137 RID: 16695 RVA: 0x000FBA61 File Offset: 0x000F9C61
		public bool IsDirectory
		{
			get
			{
				return (this.Attributes & FileAttributes.Directory) > (FileAttributes)0;
			}
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x000FBA6F File Offset: 0x000F9C6F
		public FileSystemInfo ToFileSystemInfo()
		{
			return FileSystemInfo.Create(Path.Join(this.Directory, this.FileName), ref this);
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x000FBA88 File Offset: 0x000F9C88
		public string ToSpecifiedFullPath()
		{
			ReadOnlySpan<char> readOnlySpan = this.Directory.Slice(this.RootDirectory.Length);
			if (PathInternal.EndsInDirectorySeparator(this.OriginalRootDirectory) && PathInternal.StartsWithDirectorySeparator(readOnlySpan))
			{
				readOnlySpan = readOnlySpan.Slice(1);
			}
			return Path.Join(this.OriginalRootDirectory, readOnlySpan, this.FileName);
		}

		// Token: 0x04002125 RID: 8485
		internal unsafe Interop.NtDll.FILE_FULL_DIR_INFORMATION* _info;
	}
}
