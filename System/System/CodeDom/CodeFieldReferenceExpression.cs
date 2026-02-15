using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a field.</summary>
	// Token: 0x020001FA RID: 506
	[Serializable]
	public class CodeFieldReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> class.</summary>
		// Token: 0x06000C09 RID: 3081 RVA: 0x0003A53C File Offset: 0x0003873C
		public CodeFieldReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> class using the specified target object and field name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the field. </param>
		/// <param name="fieldName">The name of the field. </param>
		// Token: 0x06000C0A RID: 3082 RVA: 0x0003AC23 File Offset: 0x00038E23
		public CodeFieldReferenceExpression(CodeExpression targetObject, string fieldName)
		{
			this.TargetObject = targetObject;
			this.FieldName = fieldName;
		}

		/// <summary>Gets or sets the object that contains the field to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the field to reference.</returns>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0003AC39 File Offset: 0x00038E39
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x0003AC41 File Offset: 0x00038E41
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the field to reference.</summary>
		/// <returns>A string containing the field name.</returns>
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0003AC4A File Offset: 0x00038E4A
		// (set) Token: 0x06000C0E RID: 3086 RVA: 0x0003AC5B File Offset: 0x00038E5B
		public string FieldName
		{
			get
			{
				return this._fieldName ?? string.Empty;
			}
			set
			{
				this._fieldName = value;
			}
		}

		// Token: 0x040008B3 RID: 2227
		private string _fieldName;
	}
}
