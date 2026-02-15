using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a field of a type.</summary>
	// Token: 0x02000201 RID: 513
	[Serializable]
	public class CodeMemberField : CodeTypeMember
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class.</summary>
		// Token: 0x06000C25 RID: 3109 RVA: 0x0003AD00 File Offset: 0x00038F00
		public CodeMemberField()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">The type of the field. </param>
		/// <param name="name">The name of the field. </param>
		// Token: 0x06000C26 RID: 3110 RVA: 0x0003AD69 File Offset: 0x00038F69
		public CodeMemberField(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			base.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">The type of the field. </param>
		/// <param name="name">The name of the field. </param>
		// Token: 0x06000C27 RID: 3111 RVA: 0x0003AD84 File Offset: 0x00038F84
		public CodeMemberField(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			base.Name = name;
		}

		/// <summary>Gets or sets the type of the field.</summary>
		/// <returns>The type of the field.</returns>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0003ADA0 File Offset: 0x00038FA0
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x0003ADCA File Offset: 0x00038FCA
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
			set
			{
				this._type = value;
			}
		}

		/// <summary>Gets or sets the initialization expression for the field.</summary>
		/// <returns>The initialization expression for the field.</returns>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0003ADD3 File Offset: 0x00038FD3
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x0003ADDB File Offset: 0x00038FDB
		public CodeExpression InitExpression { get; set; }

		// Token: 0x040008C1 RID: 2241
		private CodeTypeReference _type;
	}
}
