using System;
using System.Collections;
using System.Reflection;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects.</summary>
	// Token: 0x020002E0 RID: 736
	[DefaultMember("Item")]
	public class DesignerVerbCollection : CollectionBase
	{
		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to the collection.</summary>
		/// <returns>The index in the collection at which the verb was added.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to add to the collection. </param>
		// Token: 0x060011EA RID: 4586 RVA: 0x0003A510 File Offset: 0x00038710
		public int Add(DesignerVerb value)
		{
			return base.List.Add(value);
		}

		/// <summary>Raises the Set event.</summary>
		/// <param name="index">The index at which to set the item. </param>
		/// <param name="oldValue">The old object. </param>
		/// <param name="newValue">The new object. </param>
		// Token: 0x060011EB RID: 4587 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected override void OnSet(int index, object oldValue, object newValue)
		{
		}

		/// <summary>Raises the Insert event.</summary>
		/// <param name="index">The index at which to insert an item. </param>
		/// <param name="value">The object to insert. </param>
		// Token: 0x060011EC RID: 4588 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected override void OnInsert(int index, object value)
		{
		}

		/// <summary>Raises the Clear event.</summary>
		// Token: 0x060011ED RID: 4589 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected override void OnClear()
		{
		}

		/// <summary>Raises the Remove event.</summary>
		/// <param name="index">The index at which to remove the item. </param>
		/// <param name="value">The object to remove. </param>
		// Token: 0x060011EE RID: 4590 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected override void OnRemove(int index, object value)
		{
		}

		/// <summary>Raises the Validate event.</summary>
		/// <param name="value">The object to validate. </param>
		// Token: 0x060011EF RID: 4591 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected override void OnValidate(object value)
		{
		}
	}
}
