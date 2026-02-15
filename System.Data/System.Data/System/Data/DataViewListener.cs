using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000057 RID: 87
	internal sealed class DataViewListener
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x0001A01C File Offset: 0x0001821C
		internal DataViewListener(DataView dv)
		{
			this._objectID = dv.ObjectID;
			this._dvWeak = new WeakReference(dv);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001A03C File Offset: 0x0001823C
		private void ChildRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ChildRelationCollectionChanged(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001A070 File Offset: 0x00018270
		private void ParentRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ParentRelationCollectionChanged(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001A0A4 File Offset: 0x000182A4
		private void ColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ColumnCollectionChangedInternal(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001A0D8 File Offset: 0x000182D8
		internal void MaintainDataView(ListChangedType changedType, DataRow row, bool trackAddRemove)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.MaintainDataView(changedType, row, trackAddRemove);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001A10C File Offset: 0x0001830C
		internal void IndexListChanged(ListChangedEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.IndexListChangedInternal(e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001A13C File Offset: 0x0001833C
		internal void RegisterMetaDataEvents(DataTable table)
		{
			this._table = table;
			if (table != null)
			{
				this.RegisterListener(table);
				CollectionChangeEventHandler collectionChangeEventHandler = new CollectionChangeEventHandler(this.ColumnCollectionChanged);
				table.Columns.ColumnPropertyChanged += collectionChangeEventHandler;
				table.Columns.CollectionChanged += collectionChangeEventHandler;
				CollectionChangeEventHandler collectionChangeEventHandler2 = new CollectionChangeEventHandler(this.ChildRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ChildRelations).RelationPropertyChanged += collectionChangeEventHandler2;
				table.ChildRelations.CollectionChanged += collectionChangeEventHandler2;
				CollectionChangeEventHandler collectionChangeEventHandler3 = new CollectionChangeEventHandler(this.ParentRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ParentRelations).RelationPropertyChanged += collectionChangeEventHandler3;
				table.ParentRelations.CollectionChanged += collectionChangeEventHandler3;
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001A1D6 File Offset: 0x000183D6
		internal void UnregisterMetaDataEvents()
		{
			this.UnregisterMetaDataEvents(true);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001A1E0 File Offset: 0x000183E0
		private void UnregisterMetaDataEvents(bool updateListeners)
		{
			DataTable table = this._table;
			this._table = null;
			if (table != null)
			{
				CollectionChangeEventHandler collectionChangeEventHandler = new CollectionChangeEventHandler(this.ColumnCollectionChanged);
				table.Columns.ColumnPropertyChanged -= collectionChangeEventHandler;
				table.Columns.CollectionChanged -= collectionChangeEventHandler;
				CollectionChangeEventHandler collectionChangeEventHandler2 = new CollectionChangeEventHandler(this.ChildRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ChildRelations).RelationPropertyChanged -= collectionChangeEventHandler2;
				table.ChildRelations.CollectionChanged -= collectionChangeEventHandler2;
				CollectionChangeEventHandler collectionChangeEventHandler3 = new CollectionChangeEventHandler(this.ParentRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ParentRelations).RelationPropertyChanged -= collectionChangeEventHandler3;
				table.ParentRelations.CollectionChanged -= collectionChangeEventHandler3;
				if (updateListeners)
				{
					List<DataViewListener> listeners = table.GetListeners();
					List<DataViewListener> list = listeners;
					lock (list)
					{
						listeners.Remove(this);
					}
				}
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001A2BC File Offset: 0x000184BC
		internal void RegisterListChangedEvent(Index index)
		{
			this._index = index;
			if (index != null)
			{
				lock (index)
				{
					index.AddRef();
					index.ListChangedAdd(this);
				}
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001A308 File Offset: 0x00018508
		internal void UnregisterListChangedEvent()
		{
			Index index = this._index;
			this._index = null;
			if (index != null)
			{
				Index index2 = index;
				lock (index2)
				{
					index.ListChangedRemove(this);
					if (index.RemoveRef() <= 1)
					{
						index.RemoveRef();
					}
				}
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001A368 File Offset: 0x00018568
		private void CleanUp(bool updateListeners)
		{
			this.UnregisterMetaDataEvents(updateListeners);
			this.UnregisterListChangedEvent();
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001A378 File Offset: 0x00018578
		private void RegisterListener(DataTable table)
		{
			List<DataViewListener> listeners = table.GetListeners();
			List<DataViewListener> list = listeners;
			lock (list)
			{
				int num = listeners.Count - 1;
				while (0 <= num)
				{
					DataViewListener dataViewListener = listeners[num];
					if (!dataViewListener._dvWeak.IsAlive)
					{
						listeners.RemoveAt(num);
						dataViewListener.CleanUp(false);
					}
					num--;
				}
				listeners.Add(this);
			}
		}

		// Token: 0x040001AD RID: 429
		private readonly WeakReference _dvWeak;

		// Token: 0x040001AE RID: 430
		private DataTable _table;

		// Token: 0x040001AF RID: 431
		private Index _index;

		// Token: 0x040001B0 RID: 432
		internal readonly int _objectID;
	}
}
