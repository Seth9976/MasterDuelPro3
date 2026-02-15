using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a primitive data type value.</summary>
	// Token: 0x0200020D RID: 525
	[Serializable]
	public class CodePrimitiveExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePrimitiveExpression" /> class.</summary>
		// Token: 0x06000C7E RID: 3198 RVA: 0x0003A53C File Offset: 0x0003873C
		public CodePrimitiveExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePrimitiveExpression" /> class using the specified object.</summary>
		/// <param name="value">The object to represent. </param>
		// Token: 0x06000C7F RID: 3199 RVA: 0x0003B541 File Offset: 0x00039741
		public CodePrimitiveExpression(object value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the primitive data type to represent.</summary>
		/// <returns>The primitive data type instance to represent the value of.</returns>
		// Token: 0x17000292 RID: 658
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x0003B550 File Offset: 0x00039750
		public object Value
		{
			[CompilerGenerated]
			set
			{
				this.<Value>k__BackingField = value;
			}
		}
	}
}
