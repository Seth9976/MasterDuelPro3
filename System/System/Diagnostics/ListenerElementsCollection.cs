using System;
using System.Configuration;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x02000152 RID: 338
	[ConfigurationCollection(typeof(ListenerElement))]
	internal class ListenerElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x1700012C RID: 300
		public ListenerElement this[string name]
		{
			get
			{
				return (ListenerElement)base.BaseGet(name);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0002B621 File Offset: 0x00029821
		protected override ConfigurationElement CreateNewElement()
		{
			return new ListenerElement(true);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0002B629 File Offset: 0x00029829
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ListenerElement)element).Name;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0002B638 File Offset: 0x00029838
		public TraceListenerCollection GetRuntimeObject()
		{
			TraceListenerCollection traceListenerCollection = new TraceListenerCollection();
			bool flag = false;
			foreach (object obj in this)
			{
				ListenerElement listenerElement = (ListenerElement)obj;
				if (!flag && !listenerElement._isAddedByDefault)
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
					flag = true;
				}
				traceListenerCollection.Add(listenerElement.GetRuntimeObject());
			}
			return traceListenerCollection;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0002B6B8 File Offset: 0x000298B8
		protected override void InitializeDefault()
		{
			this.InitializeDefaultInternal();
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0002B6C0 File Offset: 0x000298C0
		internal void InitializeDefaultInternal()
		{
			this.BaseAdd(new ListenerElement(false)
			{
				Name = "Default",
				TypeName = typeof(DefaultTraceListener).FullName,
				_isAddedByDefault = true
			});
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0002B704 File Offset: 0x00029904
		protected override void BaseAdd(ConfigurationElement element)
		{
			ListenerElement listenerElement = element as ListenerElement;
			if (listenerElement.Name.Equals("Default") && listenerElement.TypeName.Equals(typeof(DefaultTraceListener).FullName))
			{
				base.BaseAdd(listenerElement, false);
				return;
			}
			base.BaseAdd(listenerElement, this.ThrowOnDuplicate);
		}
	}
}
