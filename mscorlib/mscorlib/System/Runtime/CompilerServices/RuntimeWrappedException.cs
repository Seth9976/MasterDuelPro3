using System;
using System.Runtime.Serialization;
using Unity;

namespace System.Runtime.CompilerServices
{
	/// <summary>Wraps an exception that does not derive from the <see cref="T:System.Exception" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000595 RID: 1429
	[Serializable]
	public sealed class RuntimeWrappedException : Exception
	{
		// Token: 0x06002B06 RID: 11014 RVA: 0x000AABF3 File Offset: 0x000A8DF3
		public RuntimeWrappedException(object thrownObject)
			: base("An object that does not derive from System.Exception has been wrapped in a RuntimeWrappedException.")
		{
			base.HResult = -2146233026;
			this._wrappedException = thrownObject;
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000AAC12 File Offset: 0x000A8E12
		private RuntimeWrappedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._wrappedException = info.GetValue("WrappedException", typeof(object));
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
		// Token: 0x06002B08 RID: 11016 RVA: 0x000AAC37 File Offset: 0x000A8E37
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("WrappedException", this._wrappedException, typeof(object));
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000176B9 File Offset: 0x000158B9
		internal RuntimeWrappedException()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040015D4 RID: 5588
		private object _wrappedException;
	}
}
