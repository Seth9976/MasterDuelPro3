using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to marshal an object across a context boundary fails.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000198 RID: 408
	[ComVisible(true)]
	[Serializable]
	public class ContextMarshalException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ContextMarshalException" /> class with default properties.</summary>
		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003E759 File Offset: 0x0003C959
		public ContextMarshalException()
			: base(Environment.GetResourceString("Attempted to marshal an object across a context boundary."))
		{
			base.SetErrorCode(-2146233084);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ContextMarshalException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06000EFA RID: 3834 RVA: 0x00018324 File Offset: 0x00016524
		protected ContextMarshalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
