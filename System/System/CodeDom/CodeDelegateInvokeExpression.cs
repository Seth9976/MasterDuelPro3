using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that raises an event.</summary>
	// Token: 0x020001F1 RID: 497
	[Serializable]
	public class CodeDelegateInvokeExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateInvokeExpression" /> class.</summary>
		// Token: 0x06000BF1 RID: 3057 RVA: 0x0003AB23 File Offset: 0x00038D23
		public CodeDelegateInvokeExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateInvokeExpression" /> class using the specified target object and parameters.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the target object. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters. </param>
		// Token: 0x06000BF2 RID: 3058 RVA: 0x0003AB36 File Offset: 0x00038D36
		public CodeDelegateInvokeExpression(CodeExpression targetObject, params CodeExpression[] parameters)
		{
			this.TargetObject = targetObject;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the event to invoke.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event to invoke.</returns>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0003AB5C File Offset: 0x00038D5C
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x0003AB64 File Offset: 0x00038D64
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the parameters to pass to the event handling methods attached to the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the parameters to pass to the event handling methods attached to the event.</returns>
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0003AB6D File Offset: 0x00038D6D
		public CodeExpressionCollection Parameters { get; } = new CodeExpressionCollection();
	}
}
