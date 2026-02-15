using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement that throws an exception.</summary>
	// Token: 0x02000219 RID: 537
	[Serializable]
	public class CodeThrowExceptionStatement : CodeStatement
	{
		/// <summary>Gets or sets the exception to throw.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> representing an instance of the exception to throw.</returns>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0003B6AB File Offset: 0x000398AB
		public CodeExpression ToThrow { get; }
	}
}
