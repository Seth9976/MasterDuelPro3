using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a default value.</summary>
	// Token: 0x020001EF RID: 495
	[Serializable]
	public class CodeDefaultValueExpression : CodeExpression
	{
		/// <summary>Gets or sets the data type reference for a default value.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> object representing a data type that has a default value.</returns>
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0003AAB4 File Offset: 0x00038CB4
		public CodeTypeReference Type
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._type) == null)
				{
					codeTypeReference = (this._type = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
		}

		// Token: 0x040008A9 RID: 2217
		private CodeTypeReference _type;
	}
}
