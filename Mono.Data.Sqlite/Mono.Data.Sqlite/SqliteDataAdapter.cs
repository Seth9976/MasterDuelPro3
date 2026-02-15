using System;
using System.ComponentModel;
using System.Data.Common;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000013 RID: 19
	[Designer("Microsoft.VSDesigner.Data.VS.SqlDataAdapterDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem("SQLite.Designer.SqliteDataAdapterToolboxItem, SQLite.Designer, Version=1.0.36.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139")]
	[DefaultEvent("RowUpdated")]
	public sealed class SqliteDataAdapter : DbDataAdapter
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000107 RID: 263 RVA: 0x00009384 File Offset: 0x00007584
		// (remove) Token: 0x06000108 RID: 264 RVA: 0x000093F1 File Offset: 0x000075F1
		public event EventHandler<RowUpdatingEventArgs> RowUpdating
		{
			add
			{
				EventHandler<RowUpdatingEventArgs> eventHandler = (EventHandler<RowUpdatingEventArgs>)base.Events[SqliteDataAdapter._updatingEventPH];
				if (eventHandler != null && value.Target is DbCommandBuilder)
				{
					EventHandler<RowUpdatingEventArgs> eventHandler2 = (EventHandler<RowUpdatingEventArgs>)SqliteDataAdapter.FindBuilder(eventHandler);
					if (eventHandler2 != null)
					{
						base.Events.RemoveHandler(SqliteDataAdapter._updatingEventPH, eventHandler2);
					}
				}
				base.Events.AddHandler(SqliteDataAdapter._updatingEventPH, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqliteDataAdapter._updatingEventPH, value);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00009404 File Offset: 0x00007604
		internal static Delegate FindBuilder(MulticastDelegate mcd)
		{
			if (mcd != null)
			{
				Delegate[] invocationList = mcd.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					if (invocationList[i].Target is DbCommandBuilder)
					{
						return invocationList[i];
					}
				}
			}
			return null;
		}

		// Token: 0x04000051 RID: 81
		private static object _updatingEventPH = new object();

		// Token: 0x04000052 RID: 82
		private static object _updatedEventPH = new object();
	}
}
