using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Provides functionality to discover a bindable list and the properties of the items contained in the list when they differ from the public properties of the object to which they bind.</summary>
	// Token: 0x02000103 RID: 259
	public static class ListBindingHelper
	{
		/// <summary>Returns a list associated with the specified data source.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the underlying list if it exists; otherwise, the original data source specified by <paramref name="list" />.</returns>
		/// <param name="list">The data source to examine for its underlying list.</param>
		// Token: 0x06000930 RID: 2352 RVA: 0x00026E9F File Offset: 0x0002509F
		public static object GetList(object list)
		{
			if (list is IListSource)
			{
				return ((IListSource)list).GetList();
			}
			return list;
		}

		/// <summary>Returns the data type of the items in the specified list.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the items contained in the list.</returns>
		/// <param name="list">The list to be examined for type information. </param>
		// Token: 0x06000931 RID: 2353 RVA: 0x00026EB6 File Offset: 0x000250B6
		public static Type GetListItemType(object list)
		{
			return ListBindingHelper.GetListItemType(list, string.Empty);
		}

		/// <summary>Returns the data type of the items in the specified data source.</summary>
		/// <returns>For complex data binding, the <see cref="T:System.Type" /> of the items represented by the <paramref name="dataMember" /> in the data source; otherwise, the <see cref="T:System.Type" /> of the item in the list itself.</returns>
		/// <param name="dataSource">The data source to examine for items. </param>
		/// <param name="dataMember">The optional name of the property on the data source that is to be used as the data member. This can be null.</param>
		// Token: 0x06000932 RID: 2354 RVA: 0x00026EC4 File Offset: 0x000250C4
		public static Type GetListItemType(object dataSource, string dataMember)
		{
			if (dataSource == null)
			{
				return null;
			}
			if (dataMember != null && dataMember.Length > 0)
			{
				PropertyDescriptor property = ListBindingHelper.GetProperty(dataSource, dataMember);
				if (property == null)
				{
					return typeof(object);
				}
				return property.PropertyType;
			}
			else
			{
				if (dataSource is Array)
				{
					return dataSource.GetType().GetElementType();
				}
				if (!(dataSource is IEnumerable))
				{
					return dataSource.GetType();
				}
				IEnumerator enumerator = ((IEnumerable)dataSource).GetEnumerator();
				if (enumerator.MoveNext() && enumerator.Current != null)
				{
					return enumerator.Current.GetType();
				}
				if (dataSource is IList || dataSource.GetType() == typeof(IList<>))
				{
					PropertyInfo propertyByReflection = ListBindingHelper.GetPropertyByReflection(dataSource.GetType(), "Item");
					if (propertyByReflection != null)
					{
						return propertyByReflection.PropertyType;
					}
				}
				return typeof(object);
			}
		}

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that describes the properties of an item type contained in a specified data source, or properties of the specified data source.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the properties of the items contained in <paramref name="list" />, or properties of <paramref name="list." /></returns>
		/// <param name="list">The data source to examine for property information.</param>
		// Token: 0x06000933 RID: 2355 RVA: 0x00026F94 File Offset: 0x00025194
		public static PropertyDescriptorCollection GetListItemProperties(object list)
		{
			return ListBindingHelper.GetListItemProperties(list, null);
		}

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that describes the properties of an item type contained in a collection property of a data source. Uses the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> array to indicate which properties to examine.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> describing the properties of the item type contained in a collection property of the data source.</returns>
		/// <param name="list">The data source to be examined for property information.</param>
		/// <param name="listAccessors">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> array describing which properties of the data source to examine. This can be null.</param>
		// Token: 0x06000934 RID: 2356 RVA: 0x00026FA0 File Offset: 0x000251A0
		public static PropertyDescriptorCollection GetListItemProperties(object list, PropertyDescriptor[] listAccessors)
		{
			list = ListBindingHelper.GetList(list);
			if (list == null)
			{
				return new PropertyDescriptorCollection(null);
			}
			if (list is ITypedList)
			{
				return ((ITypedList)list).GetItemProperties(listAccessors);
			}
			if (listAccessors == null || listAccessors.Length == 0)
			{
				return TypeDescriptor.GetProperties(ListBindingHelper.GetListItemType(list), new Attribute[]
				{
					new BrowsableAttribute(true)
				});
			}
			Type propertyType = listAccessors[0].PropertyType;
			if (typeof(IList).IsAssignableFrom(propertyType) || typeof(IList<>).IsAssignableFrom(propertyType))
			{
				return TypeDescriptor.GetProperties(ListBindingHelper.GetPropertyByReflection(propertyType, "Item").PropertyType);
			}
			return new PropertyDescriptorCollection(new PropertyDescriptor[0]);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00027044 File Offset: 0x00025244
		private static PropertyDescriptor GetProperty(object obj, string property_name)
		{
			return TypeDescriptor.GetProperties(obj, new Attribute[]
			{
				new BrowsableAttribute(true)
			})[property_name];
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00027064 File Offset: 0x00025264
		private static PropertyInfo GetPropertyByReflection(Type type, string property_name)
		{
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				if (propertyInfo.Name == property_name)
				{
					return propertyInfo;
				}
			}
			return null;
		}
	}
}
