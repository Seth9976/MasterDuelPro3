using System;
using System.Globalization;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a compiler error or warning.</summary>
	// Token: 0x0200022E RID: 558
	[Serializable]
	public class CompilerError
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerError" /> class.</summary>
		// Token: 0x06000D63 RID: 3427 RVA: 0x0003D7B4 File Offset: 0x0003B9B4
		public CompilerError()
			: this(string.Empty, 0, 0, string.Empty, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerError" /> class using the specified file name, line, column, error number, and error text.</summary>
		/// <param name="fileName">The file name of the file that the compiler was compiling when it encountered the error. </param>
		/// <param name="line">The line of the source of the error. </param>
		/// <param name="column">The column of the source of the error. </param>
		/// <param name="errorNumber">The error number of the error. </param>
		/// <param name="errorText">The error message text. </param>
		// Token: 0x06000D64 RID: 3428 RVA: 0x0003D7CD File Offset: 0x0003B9CD
		public CompilerError(string fileName, int line, int column, string errorNumber, string errorText)
		{
			this.Line = line;
			this.Column = column;
			this.ErrorNumber = errorNumber;
			this.ErrorText = errorText;
			this.FileName = fileName;
		}

		/// <summary>Gets or sets the line number where the source of the error occurs.</summary>
		/// <returns>The line number of the source file where the compiler encountered the error.</returns>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0003D7FA File Offset: 0x0003B9FA
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x0003D802 File Offset: 0x0003BA02
		public int Line { get; set; }

		/// <summary>Gets or sets the column number where the source of the error occurs.</summary>
		/// <returns>The column number of the source file where the compiler encountered the error.</returns>
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x0003D80B File Offset: 0x0003BA0B
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x0003D813 File Offset: 0x0003BA13
		public int Column { get; set; }

		/// <summary>Gets or sets the error number.</summary>
		/// <returns>The error number as a string.</returns>
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0003D81C File Offset: 0x0003BA1C
		// (set) Token: 0x06000D6A RID: 3434 RVA: 0x0003D824 File Offset: 0x0003BA24
		public string ErrorNumber { get; set; }

		/// <summary>Gets or sets the text of the error message.</summary>
		/// <returns>The text of the error message.</returns>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0003D82D File Offset: 0x0003BA2D
		// (set) Token: 0x06000D6C RID: 3436 RVA: 0x0003D835 File Offset: 0x0003BA35
		public string ErrorText { get; set; }

		/// <summary>Gets or sets a value that indicates whether the error is a warning.</summary>
		/// <returns>true if the error is a warning; otherwise, false.</returns>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0003D83E File Offset: 0x0003BA3E
		// (set) Token: 0x06000D6E RID: 3438 RVA: 0x0003D846 File Offset: 0x0003BA46
		public bool IsWarning { get; set; }

		/// <summary>Gets or sets the file name of the source file that contains the code which caused the error.</summary>
		/// <returns>The file name of the source file that contains the code which caused the error.</returns>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0003D84F File Offset: 0x0003BA4F
		// (set) Token: 0x06000D70 RID: 3440 RVA: 0x0003D857 File Offset: 0x0003BA57
		public string FileName { get; set; }

		/// <summary>Provides an implementation of Object's <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A string representation of the compiler error.</returns>
		// Token: 0x06000D71 RID: 3441 RVA: 0x0003D860 File Offset: 0x0003BA60
		public override string ToString()
		{
			if (this.FileName.Length <= 0)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} {1}: {2}", this.WarningString, this.ErrorNumber, this.ErrorText);
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}({1},{2}) : {3} {4}: {5}", new object[] { this.FileName, this.Line, this.Column, this.WarningString, this.ErrorNumber, this.ErrorText });
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0003D8F2 File Offset: 0x0003BAF2
		private string WarningString
		{
			get
			{
				if (!this.IsWarning)
				{
					return "error";
				}
				return "warning";
			}
		}
	}
}
