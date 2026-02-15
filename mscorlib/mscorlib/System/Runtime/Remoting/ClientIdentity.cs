using System;

namespace System.Runtime.Remoting
{
	// Token: 0x02000417 RID: 1047
	internal class ClientIdentity : Identity
	{
		// Token: 0x060022DC RID: 8924 RVA: 0x0008F690 File Offset: 0x0008D890
		public ClientIdentity(string objectUri, ObjRef objRef)
			: base(objectUri)
		{
			this._objRef = objRef;
			this._envoySink = ((this._objRef.EnvoyInfo != null) ? this._objRef.EnvoyInfo.EnvoySinks : null);
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x0008F6C6 File Offset: 0x0008D8C6
		// (set) Token: 0x060022DE RID: 8926 RVA: 0x0008F6DF File Offset: 0x0008D8DF
		public MarshalByRefObject ClientProxy
		{
			get
			{
				WeakReference proxyReference = this._proxyReference;
				return (MarshalByRefObject)((proxyReference != null) ? proxyReference.Target : null);
			}
			set
			{
				this._proxyReference = new WeakReference(value);
			}
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x0008F6ED File Offset: 0x0008D8ED
		public override ObjRef CreateObjRef(Type requestedType)
		{
			return this._objRef;
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x0008F6F5 File Offset: 0x0008D8F5
		public string TargetUri
		{
			get
			{
				return this._objRef.URI;
			}
		}

		// Token: 0x040010EC RID: 4332
		private WeakReference _proxyReference;
	}
}
