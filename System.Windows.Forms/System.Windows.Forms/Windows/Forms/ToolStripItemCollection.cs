using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.ToolStripItem" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D1 RID: 465
	[ListBindable(false)]
	[Editor("System.Windows.Forms.Design.ToolStripCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public class ToolStripItemCollection : ArrangedElementCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x06001431 RID: 5169 RVA: 0x000654CC File Offset: 0x000636CC
		internal ToolStripItemCollection(ToolStrip owner, ToolStripItem[] value, bool internalcreated)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.internal_created = internalcreated;
			this.owner = owner;
			if (value != null)
			{
				foreach (ToolStripItem toolStripItem in value)
				{
					this.AddNoOwnerOrLayout(toolStripItem);
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0006551A File Offset: 0x0006371A
		public override bool IsReadOnly
		{
			get
			{
				return base.IsReadOnly;
			}
		}

		/// <summary>Gets the item at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> located at the specified position in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />.</returns>
		/// <param name="index">The zero-based index of the item to get.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700055A RID: 1370
		public virtual ToolStripItem this[int index]
		{
			get
			{
				return (ToolStripItem)base[index];
			}
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the specified text to the collection.</summary>
		/// <returns>The new <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		// Token: 0x06001434 RID: 5172 RVA: 0x00065530 File Offset: 0x00063730
		public ToolStripItem Add(string text)
		{
			ToolStripItem toolStripItem = this.owner.CreateDefaultItem(text, null, null);
			this.Add(toolStripItem);
			return toolStripItem;
		}

		/// <summary>Adds the specified item to the end of the collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the zero-based index of the new item in the collection.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to add to the end of the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001435 RID: 5173 RVA: 0x00065558 File Offset: 0x00063758
		public int Add(ToolStripItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Contains(value))
			{
				return this.IndexOf(value);
			}
			value.InternalOwner = this.owner;
			if (value is ToolStripMenuItem && (value as ToolStripMenuItem).ShortcutKeys != Keys.None)
			{
				ToolStripManager.AddToolStripMenuItem((ToolStripMenuItem)value);
			}
			int num = base.Add(value);
			if (this.internal_created)
			{
				this.owner.OnItemAdded(new ToolStripItemEventArgs(value));
			}
			return num;
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the specified image and text to the collection and that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <returns>The new <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="onClick">Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
		// Token: 0x06001436 RID: 5174 RVA: 0x000655D0 File Offset: 0x000637D0
		public ToolStripItem Add(string text, Image image, EventHandler onClick)
		{
			ToolStripItem toolStripItem = this.owner.CreateDefaultItem(text, image, onClick);
			this.Add(toolStripItem);
			return toolStripItem;
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001437 RID: 5175 RVA: 0x000655F8 File Offset: 0x000637F8
		public new virtual void Clear()
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("This collection is read-only");
			}
			if (this.internal_created)
			{
				foreach (object obj in this)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					toolStripItem.InternalOwner = null;
					toolStripItem.Parent = null;
				}
			}
			base.Clear();
			this.owner.PerformLayout();
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00065680 File Offset: 0x00063880
		internal void ClearInternal()
		{
			base.Clear();
			this.owner.PerformLayout();
		}

		/// <summary>Determines whether the specified item is a member of the collection.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is a member of the current <see cref="T:System.Windows.Forms.ToolStripItemCollection" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to search for in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001439 RID: 5177 RVA: 0x00065693 File Offset: 0x00063893
		public bool Contains(ToolStripItem value)
		{
			return base.Contains(value);
		}

		/// <summary>Copies the collection into the specified position of the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> array.</summary>
		/// <param name="array">The array of type <see cref="T:System.Windows.Forms.ToolStripItem" /> to which to copy the collection. </param>
		/// <param name="index">The position in the <see cref="T:System.Windows.Forms.ToolStripItem" /> array at which to paste the collection. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600143A RID: 5178 RVA: 0x0006569C File Offset: 0x0006389C
		public void CopyTo(ToolStripItem[] array, int index)
		{
			base.CopyTo(array, index);
		}

		/// <summary>Retrieves the index of the specified item in the collection.</summary>
		/// <returns>A zero-based index value that represents the position of the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to locate in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600143B RID: 5179 RVA: 0x000656A6 File Offset: 0x000638A6
		public int IndexOf(ToolStripItem value)
		{
			return base.IndexOf(value);
		}

		/// <summary>Inserts the specified item into the collection at the specified index.</summary>
		/// <param name="index">The location in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> at which to insert the <see cref="T:System.Windows.Forms.ToolStripItem" />. </param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to insert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600143C RID: 5180 RVA: 0x000656B0 File Offset: 0x000638B0
		public void Insert(int index, ToolStripItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value is ToolStripMenuItem && (value as ToolStripMenuItem).ShortcutKeys != Keys.None)
			{
				ToolStripManager.AddToolStripMenuItem((ToolStripMenuItem)value);
			}
			if (value.Owner != null)
			{
				value.Owner.Items.Remove(value);
			}
			base.Insert(index, value);
			if (this.internal_created)
			{
				value.InternalOwner = this.owner;
				this.owner.OnItemAdded(new ToolStripItemEventArgs(value));
			}
			if (this.owner.Created)
			{
				this.owner.PerformLayout();
			}
		}

		/// <summary>Removes the specified item from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to remove from the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600143D RID: 5181 RVA: 0x0006574C File Offset: 0x0006394C
		public void Remove(ToolStripItem value)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("This collection is read-only");
			}
			base.Remove(value);
			if (value != null && this.internal_created)
			{
				value.InternalOwner = null;
				value.Parent = null;
			}
			if (this.internal_created)
			{
				this.owner.OnItemRemoved(new ToolStripItemEventArgs(value));
			}
			if (this.owner.Created)
			{
				this.owner.PerformLayout();
			}
		}

		/// <summary>Removes an item from the specified index in the collection.</summary>
		/// <param name="index">The index value of the <see cref="T:System.Windows.Forms.ToolStripItem" /> to remove. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600143E RID: 5182 RVA: 0x000657C0 File Offset: 0x000639C0
		public void RemoveAt(int index)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("This collection is read-only");
			}
			ToolStripItem toolStripItem = (ToolStripItem)base[index];
			this.Remove(toolStripItem);
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x000657F4 File Offset: 0x000639F4
		internal int AddNoOwnerOrLayout(ToolStripItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return base.Add(value);
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0006580B File Offset: 0x00063A0B
		internal void InsertNoOwnerOrLayout(int index, ToolStripItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (index > this.Count)
			{
				base.Add(value);
				return;
			}
			base.Insert(index, value);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00065835 File Offset: 0x00063A35
		internal void RemoveNoOwnerOrLayout(ToolStripItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.Remove(value);
		}

		/// <summary>Adds an item to the collection.</summary>
		/// <returns>The location at which <paramref name="value" /> was inserted.</returns>
		/// <param name="value">The item to add to the collection.</param>
		// Token: 0x06001442 RID: 5186 RVA: 0x0006584C File Offset: 0x00063A4C
		int IList.Add(object value)
		{
			return this.Add((ToolStripItem)value);
		}

		/// <summary>Removes all items from the collection.</summary>
		// Token: 0x06001443 RID: 5187 RVA: 0x0006585A File Offset: 0x00063A5A
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines if the collection contains a specified item.</summary>
		/// <returns>true if <paramref name="value" /> is contained in the collection; otherwise, false.</returns>
		/// <param name="value">The item to locate in the collection.</param>
		// Token: 0x06001444 RID: 5188 RVA: 0x00065862 File Offset: 0x00063A62
		bool IList.Contains(object value)
		{
			return this.Contains((ToolStripItem)value);
		}

		/// <summary>Determines the location of a specified item in the collection.</summary>
		/// <returns>The index of the item in the collection, if found; otherwise, -1.</returns>
		/// <param name="value">The item to locate in the collection.</param>
		// Token: 0x06001445 RID: 5189 RVA: 0x00065870 File Offset: 0x00063A70
		int IList.IndexOf(object value)
		{
			return this.IndexOf((ToolStripItem)value);
		}

		/// <summary>Inserts an item into the collection at a specified index.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The item to insert into the collection.</param>
		// Token: 0x06001446 RID: 5190 RVA: 0x0006587E File Offset: 0x00063A7E
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (ToolStripItem)value);
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0006588D File Offset: 0x00063A8D
		bool IList.IsFixedSize
		{
			get
			{
				return base.IsFixedSize;
			}
		}

		/// <summary>Removes the first occurrence of a specified item from the collection.</summary>
		/// <param name="value">The item to remove from the collection.</param>
		// Token: 0x06001448 RID: 5192 RVA: 0x00065895 File Offset: 0x00063A95
		void IList.Remove(object value)
		{
			this.Remove((ToolStripItem)value);
		}

		/// <summary>Removes an item from the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		// Token: 0x06001449 RID: 5193 RVA: 0x000658A3 File Offset: 0x00063AA3
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Retrieves the element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the item to get.</param>
		// Token: 0x1700055C RID: 1372
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04000C43 RID: 3139
		private ToolStrip owner;

		// Token: 0x04000C44 RID: 3140
		private bool internal_created;
	}
}
