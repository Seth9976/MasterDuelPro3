using System;
using System.Reflection;

namespace System.CodeDom
{
	/// <summary>Represents a delegate declaration.</summary>
	// Token: 0x0200021E RID: 542
	[Serializable]
	public class CodeTypeDelegate : CodeTypeDeclaration
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDelegate" /> class.</summary>
		// Token: 0x06000CB5 RID: 3253 RVA: 0x0003B89C File Offset: 0x00039A9C
		public CodeTypeDelegate()
		{
			base.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
			base.TypeAttributes |= TypeAttributes.NotPublic;
			base.BaseTypes.Clear();
			base.BaseTypes.Add(new CodeTypeReference("System.Delegate"));
		}

		/// <summary>Gets or sets the return type of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the return type of the delegate.</returns>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0003B8F8 File Offset: 0x00039AF8
		public CodeTypeReference ReturnType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._returnType) == null)
				{
					codeTypeReference = (this._returnType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
		}

		/// <summary>Gets the parameters of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the parameters of the delegate.</returns>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0003B922 File Offset: 0x00039B22
		public CodeParameterDeclarationExpressionCollection Parameters { get; } = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x04000903 RID: 2307
		private CodeTypeReference _returnType;
	}
}
