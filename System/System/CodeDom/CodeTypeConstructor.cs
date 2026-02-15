using System;

namespace System.CodeDom
{
	/// <summary>Represents a static constructor for a class.</summary>
	// Token: 0x0200021B RID: 539
	[Serializable]
	public class CodeTypeConstructor : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeConstructor" /> class.</summary>
		// Token: 0x06000CA4 RID: 3236 RVA: 0x0003B6F4 File Offset: 0x000398F4
		public CodeTypeConstructor()
		{
			base.Name = ".cctor";
		}
	}
}
