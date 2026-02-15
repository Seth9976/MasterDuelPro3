using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a local variable.</summary>
	// Token: 0x02000226 RID: 550
	[Serializable]
	public class CodeVariableReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableReferenceExpression" /> class.</summary>
		// Token: 0x06000CDE RID: 3294 RVA: 0x0003A53C File Offset: 0x0003873C
		public CodeVariableReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableReferenceExpression" /> class using the specified local variable name.</summary>
		/// <param name="variableName">The name of the local variable to reference. </param>
		// Token: 0x06000CDF RID: 3295 RVA: 0x0003BBBB File Offset: 0x00039DBB
		public CodeVariableReferenceExpression(string variableName)
		{
			this._variableName = variableName;
		}

		/// <summary>Gets or sets the name of the local variable to reference.</summary>
		/// <returns>The name of the local variable to reference.</returns>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0003BBCA File Offset: 0x00039DCA
		public string VariableName
		{
			get
			{
				return this._variableName ?? string.Empty;
			}
		}

		// Token: 0x04000914 RID: 2324
		private string _variableName;
	}
}
