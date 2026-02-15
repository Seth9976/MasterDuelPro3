using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000457 RID: 1111
	internal class CADSerializer
	{
		// Token: 0x06002484 RID: 9348 RVA: 0x00095FC4 File Offset: 0x000941C4
		internal static IMessage DeserializeMessage(MemoryStream mem, IMethodCallMessage msg)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.SurrogateSelector = null;
			mem.Position = 0L;
			if (msg == null)
			{
				return (IMessage)binaryFormatter.Deserialize(mem, null);
			}
			return (IMessage)binaryFormatter.DeserializeMethodResponse(mem, null, msg);
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x00096008 File Offset: 0x00094208
		internal static MemoryStream SerializeMessage(IMessage msg)
		{
			MemoryStream memoryStream = new MemoryStream();
			new BinaryFormatter
			{
				SurrogateSelector = new RemotingSurrogateSelector()
			}.Serialize(memoryStream, msg);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x0009603C File Offset: 0x0009423C
		internal static object DeserializeObjectSafe(byte[] mem)
		{
			byte[] array = new byte[mem.Length];
			Array.Copy(mem, array, mem.Length);
			return CADSerializer.DeserializeObject(new MemoryStream(array));
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x00096068 File Offset: 0x00094268
		internal static MemoryStream SerializeObject(object obj)
		{
			MemoryStream memoryStream = new MemoryStream();
			new BinaryFormatter
			{
				SurrogateSelector = new RemotingSurrogateSelector()
			}.Serialize(memoryStream, obj);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x0009609B File Offset: 0x0009429B
		internal static object DeserializeObject(MemoryStream mem)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.SurrogateSelector = null;
			mem.Position = 0L;
			return binaryFormatter.Deserialize(mem);
		}
	}
}
