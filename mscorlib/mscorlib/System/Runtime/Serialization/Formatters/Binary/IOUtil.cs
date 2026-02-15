using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004E0 RID: 1248
	internal static class IOUtil
	{
		// Token: 0x0600273C RID: 10044 RVA: 0x0009E0F5 File Offset: 0x0009C2F5
		internal static bool FlagTest(MessageEnum flag, MessageEnum target)
		{
			return (flag & target) == target;
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x0009E100 File Offset: 0x0009C300
		internal static void WriteStringWithCode(string value, __BinaryWriter sout)
		{
			if (value == null)
			{
				sout.WriteByte(17);
				return;
			}
			sout.WriteByte(18);
			sout.WriteString(value);
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x0009E120 File Offset: 0x0009C320
		internal static void WriteWithCode(Type type, object value, __BinaryWriter sout)
		{
			if (type == null)
			{
				sout.WriteByte(17);
				return;
			}
			if (type == Converter.typeofString)
			{
				IOUtil.WriteStringWithCode((string)value, sout);
				return;
			}
			InternalPrimitiveTypeE internalPrimitiveTypeE = Converter.ToCode(type);
			sout.WriteByte((byte)internalPrimitiveTypeE);
			sout.WriteValue(internalPrimitiveTypeE, value);
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x0009E168 File Offset: 0x0009C368
		internal static object ReadWithCode(__BinaryParser input)
		{
			InternalPrimitiveTypeE internalPrimitiveTypeE = (InternalPrimitiveTypeE)input.ReadByte();
			if (internalPrimitiveTypeE == InternalPrimitiveTypeE.Null)
			{
				return null;
			}
			if (internalPrimitiveTypeE == InternalPrimitiveTypeE.String)
			{
				return input.ReadString();
			}
			return input.ReadValue(internalPrimitiveTypeE);
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x0009E198 File Offset: 0x0009C398
		internal static object[] ReadArgs(__BinaryParser input)
		{
			int num = input.ReadInt32();
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = IOUtil.ReadWithCode(input);
			}
			return array;
		}
	}
}
