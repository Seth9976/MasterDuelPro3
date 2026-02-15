using System;

namespace System.ComponentModel.Design
{
	/// <summary>Extends the design mode behavior of a component.</summary>
	// Token: 0x02000007 RID: 7
	public class ComponentDesigner : IDesigner, IDisposable
	{
		/// <summary>Gets the component this designer is designing.</summary>
		/// <returns>The component managed by the designer.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002207 File Offset: 0x00000407
		public IComponent Component
		{
			get
			{
				return this._component;
			}
		}

		/// <summary>Gets the design-time verbs supported by the component that is associated with the designer.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects, or null if no designer verbs are available. This default implementation always returns null.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000220F File Offset: 0x0000040F
		public virtual DesignerVerbCollection Verbs
		{
			get
			{
				if (this._verbs == null)
				{
					this._verbs = new DesignerVerbCollection();
				}
				return this._verbs;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.ComponentDesigner" />.</summary>
		// Token: 0x0600001C RID: 28 RVA: 0x0000222A File Offset: 0x0000042A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.ComponentDesigner" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600001D RID: 29 RVA: 0x00002239 File Offset: 0x00000439
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._component = null;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002248 File Offset: 0x00000448
		~ComponentDesigner()
		{
			this.Dispose(false);
		}

		// Token: 0x04000002 RID: 2
		private IComponent _component;

		// Token: 0x04000003 RID: 3
		private DesignerVerbCollection _verbs;
	}
}
