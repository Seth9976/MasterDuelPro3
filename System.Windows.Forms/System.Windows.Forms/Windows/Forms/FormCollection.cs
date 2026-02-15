using System;
using System.Collections;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.Form" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B8 RID: 184
	[DefaultMember("Item")]
	public class FormCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06000752 RID: 1874 RVA: 0x000206D6 File Offset: 0x0001E8D6
		internal void Add(Form form)
		{
			if (base.InnerList.Contains(form))
			{
				return;
			}
			base.InnerList.Add(form);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000206F4 File Offset: 0x0001E8F4
		internal void Remove(Form form)
		{
			base.InnerList.Remove(form);
		}
	}
}
