using System;
using System.Collections;
using System.IO;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a collection of temporary files.</summary>
	// Token: 0x02000229 RID: 553
	[Serializable]
	public class TempFileCollection : ICollection, IEnumerable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class with default values.</summary>
		// Token: 0x06000CE8 RID: 3304 RVA: 0x0003BC41 File Offset: 0x00039E41
		public TempFileCollection()
			: this(null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class using the specified temporary directory that is set to delete the temporary files after their generation and use, by default.</summary>
		/// <param name="tempDir">A path to the temporary directory to use for storing the temporary files. </param>
		// Token: 0x06000CE9 RID: 3305 RVA: 0x0003BC4B File Offset: 0x00039E4B
		public TempFileCollection(string tempDir)
			: this(tempDir, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class using the specified temporary directory and specified value indicating whether to keep or delete the temporary files after their generation and use, by default.</summary>
		/// <param name="tempDir">A path to the temporary directory to use for storing the temporary files. </param>
		/// <param name="keepFiles">true if the temporary files should be kept after use; false if the temporary files should be deleted. </param>
		// Token: 0x06000CEA RID: 3306 RVA: 0x0003BC55 File Offset: 0x00039E55
		public TempFileCollection(string tempDir, bool keepFiles)
		{
			this.KeepFiles = keepFiles;
			this._tempDir = tempDir;
			this._files = new Hashtable(StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. </summary>
		// Token: 0x06000CEB RID: 3307 RVA: 0x0003BC7B File Offset: 0x00039E7B
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000CEC RID: 3308 RVA: 0x0003BC8A File Offset: 0x00039E8A
		protected virtual void Dispose(bool disposing)
		{
			this.SafeDelete();
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0003BC94 File Offset: 0x00039E94
		~TempFileCollection()
		{
			this.Dispose(false);
		}

		/// <summary>Adds a file name with the specified file name extension to the collection.</summary>
		/// <returns>A file name with the specified extension that was just added to the collection.</returns>
		/// <param name="fileExtension">The file name extension for the auto-generated temporary file name to add to the collection. </param>
		// Token: 0x06000CEE RID: 3310 RVA: 0x0003BCC4 File Offset: 0x00039EC4
		public string AddExtension(string fileExtension)
		{
			return this.AddExtension(fileExtension, this.KeepFiles);
		}

		/// <summary>Adds a file name with the specified file name extension to the collection, using the specified value indicating whether the file should be deleted or retained.</summary>
		/// <returns>A file name with the specified extension that was just added to the collection.</returns>
		/// <param name="fileExtension">The file name extension for the auto-generated temporary file name to add to the collection. </param>
		/// <param name="keepFile">true if the file should be kept after use; false if the file should be deleted. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileExtension" /> is null or an empty string.</exception>
		// Token: 0x06000CEF RID: 3311 RVA: 0x0003BCD4 File Offset: 0x00039ED4
		public string AddExtension(string fileExtension, bool keepFile)
		{
			if (string.IsNullOrEmpty(fileExtension))
			{
				throw new ArgumentException(SR.Format("Argument {0} cannot be null or zero-length.", "fileExtension"), "fileExtension");
			}
			string text = this.BasePath + "." + fileExtension;
			this.AddFile(text, keepFile);
			return text;
		}

		/// <summary>Adds the specified file to the collection, using the specified value indicating whether to keep the file after the collection is disposed or when the <see cref="M:System.CodeDom.Compiler.TempFileCollection.Delete" /> method is called.</summary>
		/// <param name="fileName">The name of the file to add to the collection. </param>
		/// <param name="keepFile">true if the file should be kept after use; false if the file should be deleted. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileName" /> is null or an empty string.-or-<paramref name="fileName" /> is a duplicate.</exception>
		// Token: 0x06000CF0 RID: 3312 RVA: 0x0003BD20 File Offset: 0x00039F20
		public void AddFile(string fileName, bool keepFile)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentException(SR.Format("Argument {0} cannot be null or zero-length.", "fileName"), "fileName");
			}
			if (this._files[fileName] != null)
			{
				throw new ArgumentException(SR.Format("The file name '{0}' was already in the collection.", fileName), "fileName");
			}
			this._files.Add(fileName, keepFile);
		}

		/// <summary>Returns an enumerator that iterates through a collection. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06000CF1 RID: 3313 RVA: 0x0003BD85 File Offset: 0x00039F85
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._files.Keys.GetEnumerator();
		}

		/// <summary>Copies the elements of the collection to an array, starting at the specified index of the target array. </summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="start">The zero-based index in array at which copying begins.</param>
		// Token: 0x06000CF2 RID: 3314 RVA: 0x0003BD97 File Offset: 0x00039F97
		void ICollection.CopyTo(Array array, int start)
		{
			this._files.Keys.CopyTo(array, start);
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x0003BDAB File Offset: 0x00039FAB
		int ICollection.Count
		{
			get
			{
				return this._files.Count;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x000027B6 File Offset: 0x000009B6
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x000028AE File Offset: 0x00000AAE
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the temporary directory to store the temporary files in.</summary>
		/// <returns>The temporary directory to store the temporary files in.</returns>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0003BDB8 File Offset: 0x00039FB8
		public string TempDir
		{
			get
			{
				return this._tempDir ?? string.Empty;
			}
		}

		/// <summary>Gets the full path to the base file name, without a file name extension, on the temporary directory path, that is used to generate temporary file names for the collection.</summary>
		/// <returns>The full path to the base file name, without a file name extension, on the temporary directory path, that is used to generate temporary file names for the collection.</returns>
		/// <exception cref="T:System.Security.SecurityException">If the <see cref="P:System.CodeDom.Compiler.TempFileCollection.BasePath" /> property has not been set or is set to null, and <see cref="F:System.Security.Permissions.FileIOPermissionAccess.AllAccess" /> is not granted for the temporary directory indicated by the <see cref="P:System.CodeDom.Compiler.TempFileCollection.TempDir" /> property. </exception>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0003BDC9 File Offset: 0x00039FC9
		public string BasePath
		{
			get
			{
				this.EnsureTempNameCreated();
				return this._basePath;
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0003BDD8 File Offset: 0x00039FD8
		private void EnsureTempNameCreated()
		{
			if (this._basePath == null)
			{
				string text = null;
				bool flag = false;
				int num = 5000;
				do
				{
					this._basePath = Path.Combine(string.IsNullOrEmpty(this.TempDir) ? Path.GetTempPath() : this.TempDir, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
					text = this._basePath + ".tmp";
					try
					{
						new FileStream(text, FileMode.CreateNew, FileAccess.Write).Dispose();
						flag = true;
					}
					catch (IOException ex)
					{
						num--;
						if (num == 0 || ex is DirectoryNotFoundException)
						{
							throw;
						}
						flag = false;
					}
				}
				while (!flag);
				this._files.Add(text, this.KeepFiles);
			}
		}

		/// <summary>Gets or sets a value indicating whether to keep the files, by default, when the <see cref="M:System.CodeDom.Compiler.TempFileCollection.Delete" /> method is called or the collection is disposed.</summary>
		/// <returns>true if the files should be kept; otherwise, false.</returns>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0003BE8C File Offset: 0x0003A08C
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x0003BE94 File Offset: 0x0003A094
		public bool KeepFiles { get; set; }

		// Token: 0x06000CFB RID: 3323 RVA: 0x0003BEA0 File Offset: 0x0003A0A0
		private bool KeepFile(string fileName)
		{
			object obj = this._files[fileName];
			return obj != null && (bool)obj;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0003BEC8 File Offset: 0x0003A0C8
		internal void Delete(string fileName)
		{
			try
			{
				File.Delete(fileName);
			}
			catch
			{
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0003BEF0 File Offset: 0x0003A0F0
		internal void SafeDelete()
		{
			if (this._files != null && this._files.Count > 0)
			{
				string[] array = new string[this._files.Count];
				this._files.Keys.CopyTo(array, 0);
				foreach (string text in array)
				{
					if (!this.KeepFile(text))
					{
						this.Delete(text);
						this._files.Remove(text);
					}
				}
			}
		}

		// Token: 0x04000929 RID: 2345
		private string _basePath;

		// Token: 0x0400092A RID: 2346
		private readonly string _tempDir;

		// Token: 0x0400092B RID: 2347
		private readonly Hashtable _files;
	}
}
