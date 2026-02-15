using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Selects the remoting surrogate that can be used to serialize an object that derives from a <see cref="T:System.MarshalByRefObject" />.</summary>
	// Token: 0x0200049B RID: 1179
	[ComVisible(true)]
	public class RemotingSurrogateSelector : ISurrogateSelector
	{
		/// <summary>Returns the appropriate surrogate for the given type in the given context.</summary>
		/// <returns>The appropriate surrogate for the given type in the given context.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> for which the surrogate is requested. </param>
		/// <param name="context">The source or destination of serialization. </param>
		/// <param name="ssout">When this method returns, contains an <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> that is appropriate for the specified object type. This parameter is passed uninitialized. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060025F7 RID: 9719 RVA: 0x0009A0B8 File Offset: 0x000982B8
		public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector ssout)
		{
			if (type.IsMarshalByRef)
			{
				ssout = this;
				return RemotingSurrogateSelector._objRemotingSurrogate;
			}
			if (RemotingSurrogateSelector.s_cachedTypeObjRef.IsAssignableFrom(type))
			{
				ssout = this;
				return RemotingSurrogateSelector._objRefSurrogate;
			}
			if (this._next != null)
			{
				return this._next.GetSurrogate(type, context, out ssout);
			}
			ssout = null;
			return null;
		}

		// Token: 0x0400122B RID: 4651
		private static Type s_cachedTypeObjRef = typeof(ObjRef);

		// Token: 0x0400122C RID: 4652
		private static ObjRefSurrogate _objRefSurrogate = new ObjRefSurrogate();

		// Token: 0x0400122D RID: 4653
		private static RemotingSurrogate _objRemotingSurrogate = new RemotingSurrogate();

		// Token: 0x0400122E RID: 4654
		private ISurrogateSelector _next;
	}
}
