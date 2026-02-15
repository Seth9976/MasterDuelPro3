using System;

namespace System.CodeDom
{
	/// <summary>Represents a catch exception block of a try/catch statement.</summary>
	// Token: 0x020001E6 RID: 486
	[Serializable]
	public class CodeCatchClause
	{
		/// <summary>Gets or sets the variable name of the exception that the catch clause handles.</summary>
		/// <returns>The name for the exception variable that the catch clause handles.</returns>
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0003A878 File Offset: 0x00038A78
		public string LocalName
		{
			get
			{
				return this._localName ?? string.Empty;
			}
		}

		/// <summary>Gets or sets the type of the exception to handle with the catch block.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the exception to handle.</returns>
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0003A88C File Offset: 0x00038A8C
		public CodeTypeReference CatchExceptionType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._catchExceptionType) == null)
				{
					codeTypeReference = (this._catchExceptionType = new CodeTypeReference(typeof(Exception)));
				}
				return codeTypeReference;
			}
		}

		/// <summary>Gets the statements within the catch block.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements within the catch block.</returns>
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0003A8BC File Offset: 0x00038ABC
		public CodeStatementCollection Statements
		{
			get
			{
				CodeStatementCollection codeStatementCollection;
				if ((codeStatementCollection = this._statements) == null)
				{
					codeStatementCollection = (this._statements = new CodeStatementCollection());
				}
				return codeStatementCollection;
			}
		}

		// Token: 0x04000898 RID: 2200
		private CodeStatementCollection _statements;

		// Token: 0x04000899 RID: 2201
		private CodeTypeReference _catchExceptionType;

		// Token: 0x0400089A RID: 2202
		private string _localName;
	}
}
