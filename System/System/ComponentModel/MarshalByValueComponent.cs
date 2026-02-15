using System;
using System.ComponentModel.Design;

namespace System.ComponentModel
{
	/// <summary>Implements <see cref="T:System.ComponentModel.IComponent" /> and provides the base implementation for remotable components that are marshaled by value (a copy of the serialized object is passed).</summary>
	// Token: 0x0200028D RID: 653
	[DesignerCategory("Component")]
	[Designer("System.Windows.Forms.Design.ComponentDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[TypeConverter(typeof(ComponentConverter))]
	public class MarshalByValueComponent : IComponent, IDisposable, IServiceProvider
	{
		// Token: 0x06000F75 RID: 3957 RVA: 0x00042184 File Offset: 0x00040384
		~MarshalByValueComponent()
		{
			this.Dispose(false);
		}

		/// <summary>Gets or sets the site of the component.</summary>
		/// <returns>An object implementing the <see cref="T:System.ComponentModel.ISite" /> interface that represents the site of the component.</returns>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x000421B4 File Offset: 0x000403B4
		// (set) Token: 0x06000F77 RID: 3959 RVA: 0x000421BC File Offset: 0x000403BC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual ISite Site
		{
			get
			{
				return this._site;
			}
			set
			{
				this._site = value;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.MarshalByValueComponent" />.</summary>
		// Token: 0x06000F78 RID: 3960 RVA: 0x000421C5 File Offset: 0x000403C5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.MarshalByValueComponent" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000F79 RID: 3961 RVA: 0x000421D4 File Offset: 0x000403D4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					ISite site = this._site;
					if (site != null)
					{
						IContainer container = site.Container;
						if (container != null)
						{
							container.Remove(this);
						}
					}
					EventHandlerList events = this._events;
					EventHandler eventHandler = (EventHandler)((events != null) ? events[MarshalByValueComponent.s_eventDisposed] : null);
					if (eventHandler != null)
					{
						eventHandler(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets the implementer of the <see cref="T:System.IServiceProvider" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the implementer of the <see cref="T:System.IServiceProvider" />.</returns>
		/// <param name="service">A <see cref="T:System.Type" /> that represents the type of service you want. </param>
		// Token: 0x06000F7A RID: 3962 RVA: 0x00042258 File Offset: 0x00040458
		public virtual object GetService(Type service)
		{
			ISite site = this._site;
			if (site == null)
			{
				return null;
			}
			return site.GetService(service);
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any.null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x06000F7B RID: 3963 RVA: 0x0004226C File Offset: 0x0004046C
		public override string ToString()
		{
			ISite site = this._site;
			if (site != null)
			{
				return site.Name + " [" + base.GetType().FullName + "]";
			}
			return base.GetType().FullName;
		}

		// Token: 0x04000A1C RID: 2588
		private static readonly object s_eventDisposed = new object();

		// Token: 0x04000A1D RID: 2589
		private ISite _site;

		// Token: 0x04000A1E RID: 2590
		private EventHandlerList _events;
	}
}
