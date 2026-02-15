using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000059 RID: 89
	internal sealed class DataViewManagerListItemTypeDescriptor : ICustomTypeDescriptor
	{
		// Token: 0x06000589 RID: 1417 RVA: 0x0001A73A File Offset: 0x0001893A
		internal DataViewManagerListItemTypeDescriptor(DataViewManager dataViewManager)
		{
			this._dataViewManager = dataViewManager;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001A749 File Offset: 0x00018949
		internal DataView GetDataView(DataTable table)
		{
			DataView dataView = new DataView(table);
			dataView.SetDataViewManager(this._dataViewManager);
			return dataView;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00018094 File Offset: 0x00016294
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00011F10 File Offset: 0x00010110
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00011F10 File Offset: 0x00010110
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00011F10 File Offset: 0x00010110
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00011F10 File Offset: 0x00010110
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00011F10 File Offset: 0x00010110
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00011F10 File Offset: 0x00010110
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001809C File Offset: 0x0001629C
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001809C File Offset: 0x0001629C
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000180A4 File Offset: 0x000162A4
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001A760 File Offset: 0x00018960
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this._propsCollection == null)
			{
				PropertyDescriptor[] array = null;
				DataSet dataSet = this._dataViewManager.DataSet;
				if (dataSet != null)
				{
					int count = dataSet.Tables.Count;
					array = new PropertyDescriptor[count];
					for (int i = 0; i < count; i++)
					{
						array[i] = new DataTablePropertyDescriptor(dataSet.Tables[i]);
					}
				}
				this._propsCollection = new PropertyDescriptorCollection(array);
			}
			return this._propsCollection;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000207F File Offset: 0x0000027F
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x040001B8 RID: 440
		private DataViewManager _dataViewManager;

		// Token: 0x040001B9 RID: 441
		private PropertyDescriptorCollection _propsCollection;
	}
}
