using System;
using System.Runtime.Serialization;

namespace System.Resources
{
	/// <summary>The exception that is thrown when the satellite assembly for the resources of the default culture is missing.</summary>
	// Token: 0x020005C3 RID: 1475
	[Serializable]
	public class MissingSatelliteAssemblyException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with default properties.</summary>
		// Token: 0x06002BB7 RID: 11191 RVA: 0x000AC7C2 File Offset: 0x000AA9C2
		public MissingSatelliteAssemblyException()
			: base("Resource lookup fell back to the ultimate fallback resources in a satellite assembly, but that satellite either was not found or could not be loaded. Please consider reinstalling or repairing the application.")
		{
			base.HResult = -2146233034;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with a specified error message and the name of a neutral culture. </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="cultureName">The name of the neutral culture.</param>
		// Token: 0x06002BB8 RID: 11192 RVA: 0x000AC7DA File Offset: 0x000AA9DA
		public MissingSatelliteAssemblyException(string message, string cultureName)
			: base(message)
		{
			base.HResult = -2146233034;
			this._cultureName = cultureName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class from serialized data. </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination of the exception.</param>
		// Token: 0x06002BB9 RID: 11193 RVA: 0x00018324 File Offset: 0x00016524
		protected MissingSatelliteAssemblyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04001620 RID: 5664
		private string _cultureName;
	}
}
