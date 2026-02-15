using System;

namespace System.ComponentModel
{
	/// <summary>Encapsulates zero or more components.</summary>
	// Token: 0x020002B8 RID: 696
	public class Container : IContainer, IDisposable
	{
		// Token: 0x06001079 RID: 4217 RVA: 0x00044A3C File Offset: 0x00042C3C
		~Container()
		{
			this.Dispose(false);
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Component" /> to the <see cref="T:System.ComponentModel.Container" />. The component is unnamed.</summary>
		/// <param name="component">The component to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600107A RID: 4218 RVA: 0x00044A6C File Offset: 0x00042C6C
		public virtual void Add(IComponent component)
		{
			this.Add(component, null);
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Component" /> to the <see cref="T:System.ComponentModel.Container" /> and assigns it a name.</summary>
		/// <param name="component">The component to add. </param>
		/// <param name="name">The unique, case-insensitive name to assign to the component.-or- null, which leaves the component unnamed. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not unique.</exception>
		// Token: 0x0600107B RID: 4219 RVA: 0x00044A78 File Offset: 0x00042C78
		public virtual void Add(IComponent component, string name)
		{
			object obj = this.syncObj;
			lock (obj)
			{
				if (component != null)
				{
					ISite site = component.Site;
					if (site == null || site.Container != this)
					{
						if (this.sites == null)
						{
							this.sites = new ISite[4];
						}
						else
						{
							this.ValidateName(component, name);
							if (this.sites.Length == this.siteCount)
							{
								ISite[] array = new ISite[this.siteCount * 2];
								Array.Copy(this.sites, 0, array, 0, this.siteCount);
								this.sites = array;
							}
						}
						if (site != null)
						{
							site.Container.Remove(component);
						}
						ISite site2 = this.CreateSite(component, name);
						ISite[] array2 = this.sites;
						int num = this.siteCount;
						this.siteCount = num + 1;
						array2[num] = site2;
						component.Site = site2;
						this.components = null;
					}
				}
			}
		}

		/// <summary>Creates a site <see cref="T:System.ComponentModel.ISite" /> for the given <see cref="T:System.ComponentModel.IComponent" /> and assigns the given name to the site.</summary>
		/// <returns>The newly created site.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to create a site for. </param>
		/// <param name="name">The name to assign to <paramref name="component" />, or null to skip the name assignment. </param>
		// Token: 0x0600107C RID: 4220 RVA: 0x00044B70 File Offset: 0x00042D70
		protected virtual ISite CreateSite(IComponent component, string name)
		{
			return new Container.Site(component, this, name);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Container" />.</summary>
		// Token: 0x0600107D RID: 4221 RVA: 0x00044B7A File Offset: 0x00042D7A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Container" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600107E RID: 4222 RVA: 0x00044B8C File Offset: 0x00042D8C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				object obj = this.syncObj;
				lock (obj)
				{
					while (this.siteCount > 0)
					{
						ISite[] array = this.sites;
						int num = this.siteCount - 1;
						this.siteCount = num;
						object obj2 = array[num];
						((ISite)obj2).Component.Site = null;
						((ISite)obj2).Component.Dispose();
					}
					this.sites = null;
					this.components = null;
				}
			}
		}

		/// <summary>Gets the service object of the specified type, if it is available.</summary>
		/// <returns>An <see cref="T:System.Object" /> implementing the requested service, or null if the service cannot be resolved.</returns>
		/// <param name="service">The <see cref="T:System.Type" /> of the service to retrieve. </param>
		// Token: 0x0600107F RID: 4223 RVA: 0x00044C10 File Offset: 0x00042E10
		protected virtual object GetService(Type service)
		{
			if (!(service == typeof(IContainer)))
			{
				return null;
			}
			return this;
		}

		/// <summary>Gets all the components in the <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <returns>A collection that contains the components in the <see cref="T:System.ComponentModel.Container" />.</returns>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00044C28 File Offset: 0x00042E28
		public virtual ComponentCollection Components
		{
			get
			{
				object obj = this.syncObj;
				ComponentCollection componentCollection2;
				lock (obj)
				{
					if (this.components == null)
					{
						IComponent[] array = new IComponent[this.siteCount];
						for (int i = 0; i < this.siteCount; i++)
						{
							array[i] = this.sites[i].Component;
						}
						this.components = new ComponentCollection(array);
						if (this.filter == null && this.checkedFilter)
						{
							this.checkedFilter = false;
						}
					}
					if (!this.checkedFilter)
					{
						this.filter = this.GetService(typeof(ContainerFilterService)) as ContainerFilterService;
						this.checkedFilter = true;
					}
					if (this.filter != null)
					{
						ComponentCollection componentCollection = this.filter.FilterComponents(this.components);
						if (componentCollection != null)
						{
							this.components = componentCollection;
						}
					}
					componentCollection2 = this.components;
				}
				return componentCollection2;
			}
		}

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <param name="component">The component to remove. </param>
		// Token: 0x06001081 RID: 4225 RVA: 0x00044D18 File Offset: 0x00042F18
		public virtual void Remove(IComponent component)
		{
			this.Remove(component, false);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00044D24 File Offset: 0x00042F24
		private void Remove(IComponent component, bool preserveSite)
		{
			object obj = this.syncObj;
			lock (obj)
			{
				if (component != null)
				{
					ISite site = component.Site;
					if (site != null && site.Container == this)
					{
						if (!preserveSite)
						{
							component.Site = null;
						}
						for (int i = 0; i < this.siteCount; i++)
						{
							if (this.sites[i] == site)
							{
								this.siteCount--;
								Array.Copy(this.sites, i + 1, this.sites, i, this.siteCount - i);
								this.sites[this.siteCount] = null;
								this.components = null;
								break;
							}
						}
					}
				}
			}
		}

		/// <summary>Determines whether the component name is unique for this container.</summary>
		/// <param name="component">The named component.</param>
		/// <param name="name">The component name to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not unique.</exception>
		// Token: 0x06001083 RID: 4227 RVA: 0x00044DE4 File Offset: 0x00042FE4
		protected virtual void ValidateName(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (name != null)
			{
				for (int i = 0; i < Math.Min(this.siteCount, this.sites.Length); i++)
				{
					ISite site = this.sites[i];
					if (site != null && site.Name != null && string.Equals(site.Name, name, StringComparison.OrdinalIgnoreCase) && site.Component != component && ((InheritanceAttribute)TypeDescriptor.GetAttributes(site.Component)[typeof(InheritanceAttribute)]).InheritanceLevel != InheritanceLevel.InheritedReadOnly)
					{
						throw new ArgumentException(SR.GetString("Duplicate component name '{0}'.  Component names must be unique and case-insensitive.", new object[] { name }));
					}
				}
			}
		}

		// Token: 0x04000A63 RID: 2659
		private ISite[] sites;

		// Token: 0x04000A64 RID: 2660
		private int siteCount;

		// Token: 0x04000A65 RID: 2661
		private ComponentCollection components;

		// Token: 0x04000A66 RID: 2662
		private ContainerFilterService filter;

		// Token: 0x04000A67 RID: 2663
		private bool checkedFilter;

		// Token: 0x04000A68 RID: 2664
		private object syncObj = new object();

		// Token: 0x020002B9 RID: 697
		private class Site : ISite, IServiceProvider
		{
			// Token: 0x06001085 RID: 4229 RVA: 0x00044EA6 File Offset: 0x000430A6
			internal Site(IComponent component, Container container, string name)
			{
				this.component = component;
				this.container = container;
				this.name = name;
			}

			// Token: 0x17000380 RID: 896
			// (get) Token: 0x06001086 RID: 4230 RVA: 0x00044EC3 File Offset: 0x000430C3
			public IComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x17000381 RID: 897
			// (get) Token: 0x06001087 RID: 4231 RVA: 0x00044ECB File Offset: 0x000430CB
			public IContainer Container
			{
				get
				{
					return this.container;
				}
			}

			// Token: 0x06001088 RID: 4232 RVA: 0x00044ED3 File Offset: 0x000430D3
			public object GetService(Type service)
			{
				if (!(service == typeof(ISite)))
				{
					return this.container.GetService(service);
				}
				return this;
			}

			// Token: 0x17000382 RID: 898
			// (get) Token: 0x06001089 RID: 4233 RVA: 0x000028AE File Offset: 0x00000AAE
			public bool DesignMode
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000383 RID: 899
			// (get) Token: 0x0600108A RID: 4234 RVA: 0x00044EF5 File Offset: 0x000430F5
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x04000A69 RID: 2665
			private IComponent component;

			// Token: 0x04000A6A RID: 2666
			private Container container;

			// Token: 0x04000A6B RID: 2667
			private string name;
		}
	}
}
