using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace System.Data.Common
{
	/// <summary>A collection of <see cref="T:System.Data.Common.DataTableMapping" /> objects. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F0 RID: 240
	[ListBindable(false)]
	[DefaultMember("Item")]
	public sealed class DataTableMappingCollection : MarshalByRefObject
	{
		/// <summary>Gets the number of <see cref="T:System.Data.Common.DataTableMapping" /> objects in the collection.</summary>
		/// <returns>The number of DataTableMapping objects in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x000444C5 File Offset: 0x000426C5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Count
		{
			get
			{
				if (this._items == null)
				{
					return 0;
				}
				return this._items.Count;
			}
		}

		/// <summary>Gets the location of the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name.</summary>
		/// <returns>The zero-based location of the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name.</returns>
		/// <param name="sourceTable">The case-sensitive name of the source table. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000CC3 RID: 3267 RVA: 0x000444DC File Offset: 0x000426DC
		public int IndexOf(string sourceTable)
		{
			if (!string.IsNullOrEmpty(sourceTable))
			{
				for (int i = 0; i < this.Count; i++)
				{
					string sourceTable2 = this._items[i].SourceTable;
					if (sourceTable2 != null && ADP.SrcCompare(sourceTable, sourceTable2) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00044524 File Offset: 0x00042724
		internal void ValidateSourceTable(int index, string value)
		{
			int num = this.IndexOf(value);
			if (-1 != num && index != num)
			{
				throw ADP.TablesUniqueSourceTable(value);
			}
		}

		// Token: 0x04000535 RID: 1333
		private List<DataTableMapping> _items;
	}
}
