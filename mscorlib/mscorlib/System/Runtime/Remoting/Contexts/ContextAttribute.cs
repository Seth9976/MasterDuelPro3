using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Provides the default implementations of the <see cref="T:System.Runtime.Remoting.Contexts.IContextAttribute" /> and <see cref="T:System.Runtime.Remoting.Contexts.IContextProperty" /> interfaces.</summary>
	// Token: 0x02000441 RID: 1089
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	[Serializable]
	public class ContextAttribute : Attribute, IContextAttribute, IContextProperty
	{
		/// <summary>Creates an instance of the <see cref="T:System.Runtime.Remoting.Contexts.ContextAttribute" /> class with the specified name.</summary>
		/// <param name="name">The name of the context attribute. </param>
		// Token: 0x06002431 RID: 9265 RVA: 0x00094D30 File Offset: 0x00092F30
		public ContextAttribute(string name)
		{
			this.AttributeName = name;
		}

		/// <summary>Gets the name of the context attribute.</summary>
		/// <returns>The name of the context attribute.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x00094D3F File Offset: 0x00092F3F
		public virtual string Name
		{
			get
			{
				return this.AttributeName;
			}
		}

		/// <summary>Returns a Boolean value indicating whether this instance is equal to the specified object.</summary>
		/// <returns>true if <paramref name="o" /> is not null and if the object names are equivalent; otherwise, false.</returns>
		/// <param name="o">The object to compare with this instance. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002433 RID: 9267 RVA: 0x00094D47 File Offset: 0x00092F47
		public override bool Equals(object o)
		{
			return o != null && o is ContextAttribute && !(((ContextAttribute)o).AttributeName != this.AttributeName);
		}

		/// <summary>Called when the context is frozen.</summary>
		/// <param name="newContext">The context to freeze. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002434 RID: 9268 RVA: 0x00002C89 File Offset: 0x00000E89
		public virtual void Freeze(Context newContext)
		{
		}

		/// <summary>Returns the hashcode for this instance of <see cref="T:System.Runtime.Remoting.Contexts.ContextAttribute" />.</summary>
		/// <returns>The hashcode for this instance of <see cref="T:System.Runtime.Remoting.Contexts.ContextAttribute" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002435 RID: 9269 RVA: 0x00094D73 File Offset: 0x00092F73
		public override int GetHashCode()
		{
			if (this.AttributeName == null)
			{
				return 0;
			}
			return this.AttributeName.GetHashCode();
		}

		/// <summary>Adds the current context property to the given message.</summary>
		/// <param name="ctorMsg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> to which to add the context property. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ctorMsg" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002436 RID: 9270 RVA: 0x00094D8A File Offset: 0x00092F8A
		public virtual void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			ctorMsg.ContextProperties.Add(this);
		}

		/// <summary>Returns a Boolean value indicating whether the context parameter meets the context attribute's requirements.</summary>
		/// <returns>true if the passed in context is okay; otherwise, false.</returns>
		/// <param name="ctx">The context in which to check. </param>
		/// <param name="ctorMsg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> to which to add the context property.</param>
		/// <exception cref="T:System.ArgumentNullException">Either <paramref name="ctx" /> or <paramref name="ctorMsg" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002437 RID: 9271 RVA: 0x00094DA8 File Offset: 0x00092FA8
		public virtual bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			if (ctx == null)
			{
				throw new ArgumentNullException("ctx");
			}
			if (!ctorMsg.ActivationType.IsContextful)
			{
				return true;
			}
			IContextProperty property = ctx.GetProperty(this.AttributeName);
			return property != null && this == property;
		}

		/// <summary>Returns a Boolean value indicating whether the context property is compatible with the new context.</summary>
		/// <returns>true if the context property is okay with the new context; otherwise, false.</returns>
		/// <param name="newCtx">The new context in which the property has been created. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002438 RID: 9272 RVA: 0x0000C091 File Offset: 0x0000A291
		public virtual bool IsNewContextOK(Context newCtx)
		{
			return true;
		}

		/// <summary>Indicates the name of the context attribute.</summary>
		// Token: 0x04001179 RID: 4473
		protected string AttributeName;
	}
}
