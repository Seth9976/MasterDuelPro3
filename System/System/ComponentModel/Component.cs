using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides the base implementation for the <see cref="T:System.ComponentModel.IComponent" /> interface and enables object sharing between applications.</summary>
	// Token: 0x020002B6 RID: 694
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[DesignerCategory("Component")]
	public class Component : MarshalByRefObject, IComponent, IDisposable
	{
		// Token: 0x06001069 RID: 4201 RVA: 0x0004489C File Offset: 0x00042A9C
		~Component()
		{
			this.Dispose(false);
		}

		/// <summary>Gets a value indicating whether the component can raise an event.</summary>
		/// <returns>true if the component can raise events; otherwise, false. The default is true.</returns>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x00003BCC File Offset: 0x00001DCC
		protected virtual bool CanRaiseEvents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x000448CC File Offset: 0x00042ACC
		internal bool CanRaiseEventsInternal
		{
			get
			{
				return this.CanRaiseEvents;
			}
		}

		/// <summary>Gets the list of event handlers that are attached to this <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventHandlerList" /> that provides the delegates for this component.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x000448D4 File Offset: 0x00042AD4
		protected EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList(this);
				}
				return this.events;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.ISite" /> of the <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.ComponentModel.Component" />, or null if the <see cref="T:System.ComponentModel.Component" /> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer" />, the <see cref="T:System.ComponentModel.Component" /> does not have an <see cref="T:System.ComponentModel.ISite" /> associated with it, or the <see cref="T:System.ComponentModel.Component" /> is removed from its <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x000448F0 File Offset: 0x00042AF0
		// (set) Token: 0x0600106E RID: 4206 RVA: 0x000448F8 File Offset: 0x00042AF8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual ISite Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Component" />.</summary>
		// Token: 0x0600106F RID: 4207 RVA: 0x00044901 File Offset: 0x00042B01
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001070 RID: 4208 RVA: 0x00044910 File Offset: 0x00042B10
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					if (this.site != null && this.site.Container != null)
					{
						this.site.Container.Remove(this);
					}
					if (this.events != null)
					{
						EventHandler eventHandler = (EventHandler)this.events[Component.EventDisposed];
						if (eventHandler != null)
						{
							eventHandler(this, EventArgs.Empty);
						}
					}
				}
			}
		}

		/// <summary>Returns an object that represents a service provided by the <see cref="T:System.ComponentModel.Component" /> or by its <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents a service provided by the <see cref="T:System.ComponentModel.Component" />, or null if the <see cref="T:System.ComponentModel.Component" /> does not provide the specified service.</returns>
		/// <param name="service">A service provided by the <see cref="T:System.ComponentModel.Component" />. </param>
		// Token: 0x06001071 RID: 4209 RVA: 0x0004499C File Offset: 0x00042B9C
		protected virtual object GetService(Type service)
		{
			ISite site = this.site;
			if (site != null)
			{
				return site.GetService(service);
			}
			return null;
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.ComponentModel.Component" /> is currently in design mode.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.Component" /> is in design mode; otherwise, false.</returns>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x000449BC File Offset: 0x00042BBC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected bool DesignMode
		{
			get
			{
				ISite site = this.site;
				return site != null && site.DesignMode;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x06001073 RID: 4211 RVA: 0x000449DC File Offset: 0x00042BDC
		public override string ToString()
		{
			ISite site = this.site;
			if (site != null)
			{
				return site.Name + " [" + base.GetType().FullName + "]";
			}
			return base.GetType().FullName;
		}

		// Token: 0x04000A60 RID: 2656
		private static readonly object EventDisposed = new object();

		// Token: 0x04000A61 RID: 2657
		private ISite site;

		// Token: 0x04000A62 RID: 2658
		private EventHandlerList events;
	}
}
