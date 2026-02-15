using System;

namespace System.CodeDom
{
	/// <summary>Represents the abstract base class from which all code statements derive.</summary>
	// Token: 0x02000216 RID: 534
	[Serializable]
	public class CodeStatement : CodeObject
	{
		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object that contains start directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0003B61C File Offset: 0x0003981C
		public CodeDirectiveCollection StartDirectives
		{
			get
			{
				CodeDirectiveCollection codeDirectiveCollection;
				if ((codeDirectiveCollection = this._startDirectives) == null)
				{
					codeDirectiveCollection = (this._startDirectives = new CodeDirectiveCollection());
				}
				return codeDirectiveCollection;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object that contains end directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0003B644 File Offset: 0x00039844
		public CodeDirectiveCollection EndDirectives
		{
			get
			{
				CodeDirectiveCollection codeDirectiveCollection;
				if ((codeDirectiveCollection = this._endDirectives) == null)
				{
					codeDirectiveCollection = (this._endDirectives = new CodeDirectiveCollection());
				}
				return codeDirectiveCollection;
			}
		}

		// Token: 0x040008F3 RID: 2291
		private CodeDirectiveCollection _startDirectives;

		// Token: 0x040008F4 RID: 2292
		private CodeDirectiveCollection _endDirectives;
	}
}
