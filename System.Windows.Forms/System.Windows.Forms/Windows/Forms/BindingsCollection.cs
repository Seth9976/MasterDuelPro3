using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.Binding" /> objects for a control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000022 RID: 34
	[DefaultEvent("CollectionChanged")]
	public class BindingsCollection : BaseCollection
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00003F64 File Offset: 0x00002164
		internal BindingsCollection()
		{
		}

		/// <summary>Gets the total number of bindings in the collection.</summary>
		/// <returns>The total number of bindings in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003F6C File Offset: 0x0000216C
		public override int Count
		{
			get
			{
				return base.Count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Binding" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Binding" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.Binding" /> to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The collection doesn't contain an item at the specified index. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000036 RID: 54
		public Binding this[int index]
		{
			get
			{
				return (Binding)base.List[index];
			}
		}

		/// <summary>Gets the bindings in the collection as an object.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing all of the collection members.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003F87 File Offset: 0x00002187
		protected override ArrayList List
		{
			get
			{
				return base.List;
			}
		}

		/// <summary>Adds the specified binding to the collection.</summary>
		/// <param name="binding">The <see cref="T:System.Windows.Forms.Binding" /> to add to the collection. </param>
		// Token: 0x060000B2 RID: 178 RVA: 0x00003F8F File Offset: 0x0000218F
		protected internal void Add(Binding binding)
		{
			this.AddCore(binding);
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.Binding" /> to the collection.</summary>
		/// <param name="dataBinding">The <see cref="T:System.Windows.Forms.Binding" /> to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataBinding" /> argument was null. </exception>
		// Token: 0x060000B3 RID: 179 RVA: 0x00003F98 File Offset: 0x00002198
		protected virtual void AddCore(Binding dataBinding)
		{
			CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Add, dataBinding);
			this.OnCollectionChanging(collectionChangeEventArgs);
			base.List.Add(dataBinding);
			this.OnCollectionChanged(collectionChangeEventArgs);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingsCollection.CollectionChanged" /> event.</summary>
		/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x060000B4 RID: 180 RVA: 0x00003FC8 File Offset: 0x000021C8
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, ccevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingsCollection.CollectionChanging" /> event. </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains event data.</param>
		// Token: 0x060000B5 RID: 181 RVA: 0x00003FDF File Offset: 0x000021DF
		protected virtual void OnCollectionChanging(CollectionChangeEventArgs e)
		{
			if (this.CollectionChanging != null)
			{
				this.CollectionChanging(this, e);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003FF6 File Offset: 0x000021F6
		internal bool Contains(Binding binding)
		{
			return this.List.Contains(binding);
		}

		// Token: 0x040000C7 RID: 199
		[CompilerGenerated]
		private CollectionChangeEventHandler CollectionChanged;

		// Token: 0x040000C8 RID: 200
		[CompilerGenerated]
		private CollectionChangeEventHandler CollectionChanging;
	}
}
