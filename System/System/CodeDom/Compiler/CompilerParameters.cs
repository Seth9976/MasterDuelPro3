using System;
using System.Collections.Specialized;
using System.Security.Policy;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the parameters used to invoke a compiler.</summary>
	// Token: 0x02000231 RID: 561
	[Serializable]
	public class CompilerParameters
	{
		/// <summary>Specifies an evidence object that represents the security policy permissions to grant the compiled assembly.</summary>
		/// <returns>An  object that represents the security policy permissions to grant the compiled assembly.</returns>
		// Token: 0x170002D3 RID: 723
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x0003DA29 File Offset: 0x0003BC29
		[Obsolete("CAS policy is obsolete and will be removed in a future release of the .NET Framework. Please see http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		public Evidence Evidence
		{
			set
			{
				this._evidence = ((value != null) ? value.Clone() : null);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class.</summary>
		// Token: 0x06000D7B RID: 3451 RVA: 0x0003DA3D File Offset: 0x0003BC3D
		public CompilerParameters()
			: this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names and output file name.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference. </param>
		/// <param name="outputName">The output file name. </param>
		// Token: 0x06000D7C RID: 3452 RVA: 0x0003DA47 File Offset: 0x0003BC47
		public CompilerParameters(string[] assemblyNames, string outputName)
			: this(assemblyNames, outputName, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names, output name, and a value indicating whether to include debug information.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference. </param>
		/// <param name="outputName">The output file name. </param>
		/// <param name="includeDebugInformation">true to include debug information; false to exclude debug information. </param>
		// Token: 0x06000D7D RID: 3453 RVA: 0x0003DA54 File Offset: 0x0003BC54
		public CompilerParameters(string[] assemblyNames, string outputName, bool includeDebugInformation)
		{
			this.<CoreAssemblyFileName>k__BackingField = string.Empty;
			this.WarningLevel = -1;
			base..ctor();
			if (assemblyNames != null)
			{
				this.ReferencedAssemblies.AddRange(assemblyNames);
			}
			this.OutputAssembly = outputName;
			this.IncludeDebugInformation = includeDebugInformation;
		}

		/// <summary>Gets or sets a value indicating whether to generate an executable.</summary>
		/// <returns>true if an executable should be generated; otherwise, false.</returns>
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0003DAB7 File Offset: 0x0003BCB7
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x0003DABF File Offset: 0x0003BCBF
		public bool GenerateExecutable { get; set; }

		/// <summary>Gets or sets a value indicating whether to generate the output in memory.</summary>
		/// <returns>true if the compiler should generate the output in memory; otherwise, false.</returns>
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x0003DAC8 File Offset: 0x0003BCC8
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x0003DAD0 File Offset: 0x0003BCD0
		public bool GenerateInMemory { get; set; }

		/// <summary>Gets the assemblies referenced by the current project.</summary>
		/// <returns>A collection that contains the assembly names that are referenced by the source to compile.</returns>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x0003DAD9 File Offset: 0x0003BCD9
		public StringCollection ReferencedAssemblies
		{
			get
			{
				return this._assemblyNames;
			}
		}

		/// <summary>Gets or sets the name of the output assembly.</summary>
		/// <returns>The name of the output assembly.</returns>
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x0003DAE1 File Offset: 0x0003BCE1
		// (set) Token: 0x06000D84 RID: 3460 RVA: 0x0003DAE9 File Offset: 0x0003BCE9
		public string OutputAssembly { get; set; }

		/// <summary>Gets or sets the collection that contains the temporary files.</summary>
		/// <returns>A collection that contains the temporary files.</returns>
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0003DAF4 File Offset: 0x0003BCF4
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x0003DB19 File Offset: 0x0003BD19
		public TempFileCollection TempFiles
		{
			get
			{
				TempFileCollection tempFileCollection;
				if ((tempFileCollection = this._tempFiles) == null)
				{
					tempFileCollection = (this._tempFiles = new TempFileCollection());
				}
				return tempFileCollection;
			}
			set
			{
				this._tempFiles = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to include debug information in the compiled executable.</summary>
		/// <returns>true if debug information should be generated; otherwise, false.</returns>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0003DB22 File Offset: 0x0003BD22
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x0003DB2A File Offset: 0x0003BD2A
		public bool IncludeDebugInformation { get; set; }

		/// <summary>Gets or sets a value indicating whether to treat warnings as errors.</summary>
		/// <returns>true if warnings should be treated as errors; otherwise, false.</returns>
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0003DB33 File Offset: 0x0003BD33
		public bool TreatWarningsAsErrors { get; }

		/// <summary>Gets or sets the warning level at which the compiler aborts compilation.</summary>
		/// <returns>The warning level at which the compiler aborts compilation.</returns>
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0003DB3B File Offset: 0x0003BD3B
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x0003DB43 File Offset: 0x0003BD43
		public int WarningLevel { get; set; }

		/// <summary>Gets or sets optional command-line arguments to use when invoking the compiler.</summary>
		/// <returns>Any additional command-line arguments for the compiler.</returns>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0003DB4C File Offset: 0x0003BD4C
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x0003DB54 File Offset: 0x0003BD54
		public string CompilerOptions { get; set; }

		/// <summary>Gets or sets the file name of a Win32 resource file to link into the compiled assembly.</summary>
		/// <returns>A Win32 resource file that will be linked into the compiled assembly.</returns>
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0003DB5D File Offset: 0x0003BD5D
		public string Win32Resource { get; }

		/// <summary>Gets the .NET Framework resource files to include when compiling the assembly output.</summary>
		/// <returns>A collection that contains the file paths of .NET Framework resources to include in the generated assembly.</returns>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0003DB65 File Offset: 0x0003BD65
		public StringCollection EmbeddedResources
		{
			get
			{
				return this._embeddedResources;
			}
		}

		/// <summary>Gets the .NET Framework resource files that are referenced in the current source.</summary>
		/// <returns>A collection that contains the file paths of .NET Framework resources that are referenced by the source.</returns>
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0003DB6D File Offset: 0x0003BD6D
		public StringCollection LinkedResources
		{
			get
			{
				return this._linkedResources;
			}
		}

		// Token: 0x0400093E RID: 2366
		private Evidence _evidence;

		// Token: 0x0400093F RID: 2367
		private readonly StringCollection _assemblyNames = new StringCollection();

		// Token: 0x04000940 RID: 2368
		private readonly StringCollection _embeddedResources = new StringCollection();

		// Token: 0x04000941 RID: 2369
		private readonly StringCollection _linkedResources = new StringCollection();

		// Token: 0x04000942 RID: 2370
		private TempFileCollection _tempFiles;
	}
}
