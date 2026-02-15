using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000162 RID: 354
	internal class AttributeAccessor : Accessor
	{
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x00053F21 File Offset: 0x00052121
		internal bool IsSpecialXmlNamespace
		{
			get
			{
				return this.isSpecial;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x00053F29 File Offset: 0x00052129
		// (set) Token: 0x06001127 RID: 4391 RVA: 0x00053F31 File Offset: 0x00052131
		internal bool IsList
		{
			get
			{
				return this.isList;
			}
			set
			{
				this.isList = value;
			}
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00053F3C File Offset: 0x0005213C
		internal void CheckSpecial()
		{
			if (this.Name.LastIndexOf(':') >= 0)
			{
				if (!this.Name.StartsWith("xml:", StringComparison.Ordinal))
				{
					throw new InvalidOperationException(Res.GetString("Invalid name character in '{0}'.", new object[] { this.Name }));
				}
				this.Name = this.Name.Substring("xml:".Length);
				base.Namespace = "http://www.w3.org/XML/1998/namespace";
				this.isSpecial = true;
			}
			else if (base.Namespace == "http://www.w3.org/XML/1998/namespace")
			{
				this.isSpecial = true;
			}
			else
			{
				this.isSpecial = false;
			}
			if (this.isSpecial)
			{
				base.Form = XmlSchemaForm.Qualified;
			}
		}

		// Token: 0x0400083D RID: 2109
		private bool isSpecial;

		// Token: 0x0400083E RID: 2110
		private bool isList;
	}
}
