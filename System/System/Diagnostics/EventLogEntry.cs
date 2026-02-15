using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Encapsulates a single record in the event log. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000185 RID: 389
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	[Serializable]
	public sealed class EventLogEntry : Component, ISerializable
	{
		// Token: 0x0600094C RID: 2380 RVA: 0x00031608 File Offset: 0x0002F808
		internal EventLogEntry(string category, short categoryNumber, int index, int eventID, string source, string message, string userName, string machineName, EventLogEntryType entryType, DateTime timeGenerated, DateTime timeWritten, byte[] data, string[] replacementStrings, long instanceId)
		{
			this.category = category;
			this.categoryNumber = categoryNumber;
			this.data = data;
			this.entryType = entryType;
			this.eventID = eventID;
			this.index = index;
			this.machineName = machineName;
			this.message = message;
			this.replacementStrings = replacementStrings;
			this.source = source;
			this.timeGenerated = timeGenerated;
			this.timeWritten = timeWritten;
			this.userName = userName;
			this.instanceId = instanceId;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00031688 File Offset: 0x0002F888
		[MonoTODO]
		private EventLogEntry(SerializationInfo info, StreamingContext context)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization. </param>
		// Token: 0x0600094E RID: 2382 RVA: 0x0000A4AB File Offset: 0x000086AB
		[MonoTODO("Needs serialization support")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001C8B6 File Offset: 0x0001AAB6
		internal EventLogEntry()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040006FF RID: 1791
		private string category;

		// Token: 0x04000700 RID: 1792
		private short categoryNumber;

		// Token: 0x04000701 RID: 1793
		private byte[] data;

		// Token: 0x04000702 RID: 1794
		private EventLogEntryType entryType;

		// Token: 0x04000703 RID: 1795
		private int eventID;

		// Token: 0x04000704 RID: 1796
		private int index;

		// Token: 0x04000705 RID: 1797
		private string machineName;

		// Token: 0x04000706 RID: 1798
		private string message;

		// Token: 0x04000707 RID: 1799
		private string[] replacementStrings;

		// Token: 0x04000708 RID: 1800
		private string source;

		// Token: 0x04000709 RID: 1801
		private DateTime timeGenerated;

		// Token: 0x0400070A RID: 1802
		private DateTime timeWritten;

		// Token: 0x0400070B RID: 1803
		private string userName;

		// Token: 0x0400070C RID: 1804
		private long instanceId;
	}
}
