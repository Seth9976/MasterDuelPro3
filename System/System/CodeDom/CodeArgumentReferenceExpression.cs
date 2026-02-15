using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to the value of an argument passed to a method.</summary>
	// Token: 0x020001D9 RID: 473
	[Serializable]
	public class CodeArgumentReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> class.</summary>
		// Token: 0x06000B94 RID: 2964 RVA: 0x0003A53C File Offset: 0x0003873C
		public CodeArgumentReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> class using the specified parameter name.</summary>
		/// <param name="parameterName">The name of the parameter to reference. </param>
		// Token: 0x06000B95 RID: 2965 RVA: 0x0003A544 File Offset: 0x00038744
		public CodeArgumentReferenceExpression(string parameterName)
		{
			this._parameterName = parameterName;
		}

		/// <summary>Gets or sets the name of the parameter this expression references.</summary>
		/// <returns>The name of the parameter to reference.</returns>
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0003A553 File Offset: 0x00038753
		public string ParameterName
		{
			get
			{
				return this._parameterName ?? string.Empty;
			}
		}

		// Token: 0x04000872 RID: 2162
		private string _parameterName;
	}
}
