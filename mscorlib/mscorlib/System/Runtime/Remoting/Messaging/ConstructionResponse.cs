using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" /> interface to create a message that responds to a call to instantiate a remote object.</summary>
	// Token: 0x02000484 RID: 1156
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class ConstructionResponse : MethodResponse, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
	{
		// Token: 0x06002548 RID: 9544 RVA: 0x00098233 File Offset: 0x00096433
		internal ConstructionResponse(object resultObject, LogicalCallContext callCtx, IMethodCallMessage msg)
			: base(resultObject, null, callCtx, msg)
		{
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x0009823F File Offset: 0x0009643F
		internal ConstructionResponse(Exception e, IMethodCallMessage msg)
			: base(e, msg)
		{
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x00098249 File Offset: 0x00096449
		internal ConstructionResponse(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x00098253 File Offset: 0x00096453
		public override IDictionary Properties
		{
			get
			{
				return base.Properties;
			}
		}
	}
}
