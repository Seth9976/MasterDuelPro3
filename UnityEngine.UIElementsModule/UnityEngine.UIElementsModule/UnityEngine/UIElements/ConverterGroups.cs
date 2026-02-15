using System;
using System.Collections.Generic;
using System.Globalization;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000029 RID: 41
	public static class ConverterGroups
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x0000429A File Offset: 0x0000249A
		static ConverterGroups()
		{
			ConverterGroups.RegisterPrimitivesConverter();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000042D0 File Offset: 0x000024D0
		private static void RegisterPrimitivesConverter()
		{
			ConverterGroups.RegisterInt8Converters();
			ConverterGroups.RegisterInt16Converters();
			ConverterGroups.RegisterInt32Converters();
			ConverterGroups.RegisterInt64Converters();
			ConverterGroups.RegisterUInt8Converters();
			ConverterGroups.RegisterUInt16Converters();
			ConverterGroups.RegisterUInt32Converters();
			ConverterGroups.RegisterUInt64Converters();
			ConverterGroups.RegisterFloatConverters();
			ConverterGroups.RegisterDoubleConverters();
			ConverterGroups.RegisterBooleanConverters();
			ConverterGroups.RegisterCharConverters();
			ConverterGroups.RegisterColorConverters();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000432C File Offset: 0x0000252C
		internal static void RegisterGlobal<TSource, TDestination>(TypeConverter<TSource, TDestination> converter)
		{
			ConverterGroups.s_GlobalConverters.registry.Register(typeof(TSource), typeof(TDestination), converter);
			TypeConversion.Register<TSource, TDestination>(converter);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000436C File Offset: 0x0000256C
		public unsafe static bool TryConvert<TSource, TDestination>(ref TSource source, out TDestination destination)
		{
			Type sourceType = typeof(TSource);
			Type destinationType = typeof(TDestination);
			Delegate converter;
			bool flag = ConverterGroups.s_GlobalConverters.registry.TryGetConverter(typeof(TSource), destinationType, out converter);
			bool flag2;
			if (flag)
			{
				destination = ((TypeConverter<TSource, TDestination>)converter)(ref source);
				flag2 = true;
			}
			else
			{
				bool flag3 = sourceType.IsValueType && destinationType.IsValueType;
				if (flag3)
				{
					bool flag4 = sourceType == destinationType;
					if (flag4)
					{
						destination = *UnsafeUtility.As<TSource, TDestination>(ref source);
						flag2 = true;
					}
					else
					{
						bool flag5 = ConverterGroups.s_PrimitivesConverters.registry.TryGetConverter(sourceType, destinationType, out converter);
						if (flag5)
						{
							destination = ((TypeConverter<TSource, TDestination>)converter)(ref source);
							flag2 = true;
						}
						else
						{
							destination = default(TDestination);
							flag2 = false;
						}
					}
				}
				else
				{
					TSource tsource = source;
					TDestination d;
					bool flag6;
					if (tsource is TDestination)
					{
						d = tsource as TDestination;
						flag6 = true;
					}
					else
					{
						flag6 = false;
					}
					bool flag7 = flag6;
					if (flag7)
					{
						destination = d;
						flag2 = true;
					}
					else
					{
						bool flag8 = destinationType.IsAssignableFrom(sourceType) && source == null;
						if (flag8)
						{
							destination = default(TDestination);
							flag2 = true;
						}
						else
						{
							bool flag9 = destinationType == typeof(string);
							if (flag9)
							{
								ref TSource ptr = ref source;
								tsource = default(TSource);
								object obj;
								if (tsource == null)
								{
									tsource = source;
									ptr = ref tsource;
									if (tsource == null)
									{
										obj = null;
										goto IL_019B;
									}
								}
								obj = ptr.ToString();
								IL_019B:
								destination = (TDestination)((object)obj);
								flag2 = true;
							}
							else
							{
								bool flag10 = destinationType == typeof(object);
								if (flag10)
								{
									destination = (TDestination)((object)source);
									flag2 = true;
								}
								else
								{
									destination = default(TDestination);
									flag2 = false;
								}
							}
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004564 File Offset: 0x00002764
		private static void RegisterInt8Converters()
		{
			TypeConverterRegistry typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(sbyte), typeof(bool), new TypeConverter<sbyte, bool>(delegate(ref sbyte v)
			{
				return (long)v > 0L;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(sbyte), typeof(char), new TypeConverter<sbyte, char>(delegate(ref sbyte v)
			{
				return (char)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(sbyte), typeof(short), new TypeConverter<sbyte, short>(delegate(ref sbyte v)
			{
				return (short)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(sbyte), typeof(int), new TypeConverter<sbyte, int>(delegate(ref sbyte v)
			{
				return (int)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(sbyte), typeof(long), new TypeConverter<sbyte, long>(delegate(ref sbyte v)
			{
				return (long)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(sbyte), typeof(byte), new TypeConverter<sbyte, byte>(delegate(ref sbyte v)
			{
				return (byte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(sbyte), typeof(ushort), new TypeConverter<sbyte, ushort>(delegate(ref sbyte v)
			{
				return (ushort)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(sbyte), typeof(uint), new TypeConverter<sbyte, uint>(delegate(ref sbyte v)
			{
				return (uint)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(sbyte), typeof(ulong), new TypeConverter<sbyte, ulong>(delegate(ref sbyte v)
			{
				return (ulong)((long)v);
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(sbyte), typeof(float), new TypeConverter<sbyte, float>(delegate(ref sbyte v)
			{
				return (float)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(sbyte), typeof(double), new TypeConverter<sbyte, double>(delegate(ref sbyte v)
			{
				return (double)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(string), typeof(sbyte), new TypeConverter<string, sbyte>(delegate(ref string v)
			{
				sbyte r;
				bool flag = sbyte.TryParse(v, out r);
				sbyte b;
				if (flag)
				{
					b = r;
				}
				else
				{
					double fromDouble;
					sbyte result;
					b = ((double.TryParse(v, out fromDouble) && ConverterGroups.TryConvert<double, sbyte>(ref fromDouble, out result)) ? result : 0);
				}
				return b;
			}));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000048BC File Offset: 0x00002ABC
		private static void RegisterInt16Converters()
		{
			TypeConverterRegistry typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(short), typeof(bool), new TypeConverter<short, bool>(delegate(ref short v)
			{
				return (long)v > 0L;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(short), typeof(sbyte), new TypeConverter<short, sbyte>(delegate(ref short v)
			{
				return (sbyte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(short), typeof(char), new TypeConverter<short, char>(delegate(ref short v)
			{
				return (char)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(short), typeof(int), new TypeConverter<short, int>(delegate(ref short v)
			{
				return (int)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(short), typeof(long), new TypeConverter<short, long>(delegate(ref short v)
			{
				return (long)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(short), typeof(byte), new TypeConverter<short, byte>(delegate(ref short v)
			{
				return (byte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(short), typeof(ushort), new TypeConverter<short, ushort>(delegate(ref short v)
			{
				return (ushort)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(short), typeof(uint), new TypeConverter<short, uint>(delegate(ref short v)
			{
				return (uint)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(short), typeof(ulong), new TypeConverter<short, ulong>(delegate(ref short v)
			{
				return (ulong)((long)v);
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(short), typeof(float), new TypeConverter<short, float>(delegate(ref short v)
			{
				return (float)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(short), typeof(double), new TypeConverter<short, double>(delegate(ref short v)
			{
				return (double)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(string), typeof(short), new TypeConverter<string, short>(delegate(ref string v)
			{
				short r;
				bool flag = short.TryParse(v, out r);
				short num;
				if (flag)
				{
					num = r;
				}
				else
				{
					double fromDouble;
					short result;
					num = ((double.TryParse(v, out fromDouble) && ConverterGroups.TryConvert<double, short>(ref fromDouble, out result)) ? result : 0);
				}
				return num;
			}));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004C14 File Offset: 0x00002E14
		private static void RegisterInt32Converters()
		{
			TypeConverterRegistry typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(int), typeof(bool), new TypeConverter<int, bool>(delegate(ref int v)
			{
				return (long)v > 0L;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(int), typeof(sbyte), new TypeConverter<int, sbyte>(delegate(ref int v)
			{
				return (sbyte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(int), typeof(char), new TypeConverter<int, char>(delegate(ref int v)
			{
				return (char)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(int), typeof(short), new TypeConverter<int, short>(delegate(ref int v)
			{
				return (short)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(int), typeof(long), new TypeConverter<int, long>(delegate(ref int v)
			{
				return (long)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(int), typeof(byte), new TypeConverter<int, byte>(delegate(ref int v)
			{
				return (byte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(int), typeof(ushort), new TypeConverter<int, ushort>(delegate(ref int v)
			{
				return (ushort)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(int), typeof(uint), new TypeConverter<int, uint>(delegate(ref int v)
			{
				return (uint)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(int), typeof(ulong), new TypeConverter<int, ulong>(delegate(ref int v)
			{
				return (ulong)((long)v);
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(int), typeof(float), new TypeConverter<int, float>(delegate(ref int v)
			{
				return (float)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(int), typeof(double), new TypeConverter<int, double>(delegate(ref int v)
			{
				return (double)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(string), typeof(int), new TypeConverter<string, int>(delegate(ref string v)
			{
				int r;
				bool flag = int.TryParse(v, out r);
				int num;
				if (flag)
				{
					num = r;
				}
				else
				{
					double fromDouble;
					int result;
					num = ((double.TryParse(v, out fromDouble) && ConverterGroups.TryConvert<double, int>(ref fromDouble, out result)) ? result : 0);
				}
				return num;
			}));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004F6C File Offset: 0x0000316C
		private static void RegisterInt64Converters()
		{
			TypeConverterRegistry typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(long), typeof(bool), new TypeConverter<long, bool>(delegate(ref long v)
			{
				return v > 0L;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(long), typeof(sbyte), new TypeConverter<long, sbyte>(delegate(ref long v)
			{
				return (sbyte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(long), typeof(char), new TypeConverter<long, char>(delegate(ref long v)
			{
				return (char)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(long), typeof(short), new TypeConverter<long, short>(delegate(ref long v)
			{
				return (short)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(long), typeof(int), new TypeConverter<long, int>(delegate(ref long v)
			{
				return (int)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(long), typeof(byte), new TypeConverter<long, byte>(delegate(ref long v)
			{
				return (byte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(long), typeof(ushort), new TypeConverter<long, ushort>(delegate(ref long v)
			{
				return (ushort)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(long), typeof(uint), new TypeConverter<long, uint>(delegate(ref long v)
			{
				return (uint)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(long), typeof(ulong), new TypeConverter<long, ulong>(delegate(ref long v)
			{
				return (ulong)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(long), typeof(float), new TypeConverter<long, float>(delegate(ref long v)
			{
				return (float)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(long), typeof(double), new TypeConverter<long, double>(delegate(ref long v)
			{
				return (double)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(string), typeof(long), new TypeConverter<string, long>(delegate(ref string v)
			{
				long r;
				bool flag = long.TryParse(v, out r);
				long num;
				if (flag)
				{
					num = r;
				}
				else
				{
					double fromDouble;
					long result;
					num = ((double.TryParse(v, out fromDouble) && ConverterGroups.TryConvert<double, long>(ref fromDouble, out result)) ? result : 0L);
				}
				return num;
			}));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000052C4 File Offset: 0x000034C4
		private static void RegisterUInt8Converters()
		{
			TypeConverterRegistry typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(byte), typeof(bool), new TypeConverter<byte, bool>(delegate(ref byte v)
			{
				return v > 0;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(byte), typeof(sbyte), new TypeConverter<byte, sbyte>(delegate(ref byte v)
			{
				return (sbyte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(byte), typeof(char), new TypeConverter<byte, char>(delegate(ref byte v)
			{
				return (char)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(byte), typeof(short), new TypeConverter<byte, short>(delegate(ref byte v)
			{
				return (short)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(byte), typeof(int), new TypeConverter<byte, int>(delegate(ref byte v)
			{
				return (int)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(byte), typeof(long), new TypeConverter<byte, long>(delegate(ref byte v)
			{
				return (long)((ulong)v);
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(byte), typeof(ushort), new TypeConverter<byte, ushort>(delegate(ref byte v)
			{
				return (ushort)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(byte), typeof(uint), new TypeConverter<byte, uint>(delegate(ref byte v)
			{
				return (uint)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(byte), typeof(ulong), new TypeConverter<byte, ulong>(delegate(ref byte v)
			{
				return (ulong)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(byte), typeof(float), new TypeConverter<byte, float>(delegate(ref byte v)
			{
				return (float)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(byte), typeof(double), new TypeConverter<byte, double>(delegate(ref byte v)
			{
				return (double)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(byte), typeof(object), new TypeConverter<byte, object>(delegate(ref byte v)
			{
				return v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(string), typeof(byte), new TypeConverter<string, byte>(delegate(ref string v)
			{
				byte r;
				bool flag = byte.TryParse(v, out r);
				byte b;
				if (flag)
				{
					b = r;
				}
				else
				{
					double fromDouble;
					byte result;
					b = ((double.TryParse(v, out fromDouble) && ConverterGroups.TryConvert<double, byte>(ref fromDouble, out result)) ? result : 0);
				}
				return b;
			}));
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005660 File Offset: 0x00003860
		private static void RegisterUInt16Converters()
		{
			TypeConverterRegistry typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ushort), typeof(bool), new TypeConverter<ushort, bool>(delegate(ref ushort v)
			{
				return v > 0;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ushort), typeof(sbyte), new TypeConverter<ushort, sbyte>(delegate(ref ushort v)
			{
				return (sbyte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ushort), typeof(char), new TypeConverter<ushort, char>(delegate(ref ushort v)
			{
				return (char)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ushort), typeof(short), new TypeConverter<ushort, short>(delegate(ref ushort v)
			{
				return (short)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ushort), typeof(int), new TypeConverter<ushort, int>(delegate(ref ushort v)
			{
				return (int)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ushort), typeof(long), new TypeConverter<ushort, long>(delegate(ref ushort v)
			{
				return (long)((ulong)v);
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ushort), typeof(byte), new TypeConverter<ushort, byte>(delegate(ref ushort v)
			{
				return (byte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ushort), typeof(uint), new TypeConverter<ushort, uint>(delegate(ref ushort v)
			{
				return (uint)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ushort), typeof(ulong), new TypeConverter<ushort, ulong>(delegate(ref ushort v)
			{
				return (ulong)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ushort), typeof(float), new TypeConverter<ushort, float>(delegate(ref ushort v)
			{
				return (float)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ushort), typeof(double), new TypeConverter<ushort, double>(delegate(ref ushort v)
			{
				return (double)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(string), typeof(ushort), new TypeConverter<string, ushort>(delegate(ref string v)
			{
				ushort r;
				bool flag = ushort.TryParse(v, out r);
				ushort num;
				if (flag)
				{
					num = r;
				}
				else
				{
					double fromDouble;
					ushort result;
					num = ((double.TryParse(v, out fromDouble) && ConverterGroups.TryConvert<double, ushort>(ref fromDouble, out result)) ? result : 0);
				}
				return num;
			}));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000059B8 File Offset: 0x00003BB8
		private static void RegisterUInt32Converters()
		{
			TypeConverterRegistry typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(uint), typeof(bool), new TypeConverter<uint, bool>(delegate(ref uint v)
			{
				return v > 0U;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(uint), typeof(sbyte), new TypeConverter<uint, sbyte>(delegate(ref uint v)
			{
				return (sbyte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(uint), typeof(char), new TypeConverter<uint, char>(delegate(ref uint v)
			{
				return (char)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(uint), typeof(short), new TypeConverter<uint, short>(delegate(ref uint v)
			{
				return (short)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(uint), typeof(int), new TypeConverter<uint, int>(delegate(ref uint v)
			{
				return (int)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(uint), typeof(long), new TypeConverter<uint, long>(delegate(ref uint v)
			{
				return (long)((ulong)v);
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(uint), typeof(byte), new TypeConverter<uint, byte>(delegate(ref uint v)
			{
				return (byte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(uint), typeof(ushort), new TypeConverter<uint, ushort>(delegate(ref uint v)
			{
				return (ushort)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(uint), typeof(ulong), new TypeConverter<uint, ulong>(delegate(ref uint v)
			{
				return (ulong)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(uint), typeof(float), new TypeConverter<uint, float>(delegate(ref uint v)
			{
				return v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(uint), typeof(double), new TypeConverter<uint, double>(delegate(ref uint v)
			{
				return v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(string), typeof(uint), new TypeConverter<string, uint>(delegate(ref string v)
			{
				uint r;
				bool flag = uint.TryParse(v, out r);
				uint num;
				if (flag)
				{
					num = r;
				}
				else
				{
					double fromDouble;
					uint result;
					num = ((double.TryParse(v, out fromDouble) && ConverterGroups.TryConvert<double, uint>(ref fromDouble, out result)) ? result : 0U);
				}
				return num;
			}));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005D10 File Offset: 0x00003F10
		private static void RegisterUInt64Converters()
		{
			TypeConverterRegistry typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ulong), typeof(bool), new TypeConverter<ulong, bool>(delegate(ref ulong v)
			{
				return v > 0UL;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ulong), typeof(sbyte), new TypeConverter<ulong, sbyte>(delegate(ref ulong v)
			{
				return (sbyte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ulong), typeof(char), new TypeConverter<ulong, char>(delegate(ref ulong v)
			{
				return (char)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ulong), typeof(short), new TypeConverter<ulong, short>(delegate(ref ulong v)
			{
				return (short)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ulong), typeof(int), new TypeConverter<ulong, int>(delegate(ref ulong v)
			{
				return (int)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ulong), typeof(long), new TypeConverter<ulong, long>(delegate(ref ulong v)
			{
				return (long)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ulong), typeof(byte), new TypeConverter<ulong, byte>(delegate(ref ulong v)
			{
				return (byte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ulong), typeof(ushort), new TypeConverter<ulong, ushort>(delegate(ref ulong v)
			{
				return (ushort)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ulong), typeof(uint), new TypeConverter<ulong, uint>(delegate(ref ulong v)
			{
				return (uint)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ulong), typeof(float), new TypeConverter<ulong, float>(delegate(ref ulong v)
			{
				return v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(ulong), typeof(double), new TypeConverter<ulong, double>(delegate(ref ulong v)
			{
				return v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(string), typeof(ulong), new TypeConverter<string, ulong>(delegate(ref string v)
			{
				ulong r;
				bool flag = ulong.TryParse(v, out r);
				ulong num;
				if (flag)
				{
					num = r;
				}
				else
				{
					double fromDouble;
					ulong result;
					num = ((double.TryParse(v, out fromDouble) && ConverterGroups.TryConvert<double, ulong>(ref fromDouble, out result)) ? result : 0UL);
				}
				return num;
			}));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006068 File Offset: 0x00004268
		private static void RegisterFloatConverters()
		{
			TypeConverterRegistry typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(float), typeof(bool), new TypeConverter<float, bool>(delegate(ref float v)
			{
				return (double)v != 0.0;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(float), typeof(sbyte), new TypeConverter<float, sbyte>(delegate(ref float v)
			{
				return (sbyte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(float), typeof(char), new TypeConverter<float, char>(delegate(ref float v)
			{
				return (char)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(float), typeof(short), new TypeConverter<float, short>(delegate(ref float v)
			{
				return (short)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(float), typeof(int), new TypeConverter<float, int>(delegate(ref float v)
			{
				return (int)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(float), typeof(long), new TypeConverter<float, long>(delegate(ref float v)
			{
				return (long)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(float), typeof(byte), new TypeConverter<float, byte>(delegate(ref float v)
			{
				return (byte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(float), typeof(ushort), new TypeConverter<float, ushort>(delegate(ref float v)
			{
				return (ushort)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(float), typeof(uint), new TypeConverter<float, uint>(delegate(ref float v)
			{
				return (uint)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(float), typeof(ulong), new TypeConverter<float, ulong>(delegate(ref float v)
			{
				return (ulong)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(float), typeof(double), new TypeConverter<float, double>(delegate(ref float v)
			{
				return (double)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(float), typeof(string), new TypeConverter<float, string>(delegate(ref float v)
			{
				return v.ToString(CultureInfo.InvariantCulture);
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(string), typeof(float), new TypeConverter<string, float>(delegate(ref string v)
			{
				float r;
				bool flag = float.TryParse(v, out r);
				float num;
				if (flag)
				{
					num = r;
				}
				else
				{
					double fromDouble;
					float result;
					num = ((double.TryParse(v, out fromDouble) && ConverterGroups.TryConvert<double, float>(ref fromDouble, out result)) ? result : 0f);
				}
				return num;
			}));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006404 File Offset: 0x00004604
		private static void RegisterDoubleConverters()
		{
			TypeConverterRegistry typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(double), typeof(bool), new TypeConverter<double, bool>(delegate(ref double v)
			{
				return v != 0.0;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(double), typeof(sbyte), new TypeConverter<double, sbyte>(delegate(ref double v)
			{
				return (sbyte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(double), typeof(char), new TypeConverter<double, char>(delegate(ref double v)
			{
				return (char)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(double), typeof(short), new TypeConverter<double, short>(delegate(ref double v)
			{
				return (short)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(double), typeof(int), new TypeConverter<double, int>(delegate(ref double v)
			{
				return (int)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(double), typeof(long), new TypeConverter<double, long>(delegate(ref double v)
			{
				return (long)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(double), typeof(byte), new TypeConverter<double, byte>(delegate(ref double v)
			{
				return (byte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(double), typeof(ushort), new TypeConverter<double, ushort>(delegate(ref double v)
			{
				return (ushort)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(double), typeof(uint), new TypeConverter<double, uint>(delegate(ref double v)
			{
				return (uint)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(double), typeof(ulong), new TypeConverter<double, ulong>(delegate(ref double v)
			{
				return (ulong)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(double), typeof(float), new TypeConverter<double, float>(delegate(ref double v)
			{
				return (float)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(double), typeof(string), new TypeConverter<double, string>(delegate(ref double v)
			{
				return v.ToString(CultureInfo.InvariantCulture);
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(string), typeof(double), new TypeConverter<string, double>(delegate(ref string v)
			{
				double r;
				double.TryParse(v, out r);
				return r;
			}));
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000067A0 File Offset: 0x000049A0
		private static void RegisterBooleanConverters()
		{
			TypeConverterRegistry typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(bool), typeof(char), new TypeConverter<bool, char>(delegate(ref bool v)
			{
				return v ? '\u0001' : '\0';
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(bool), typeof(sbyte), new TypeConverter<bool, sbyte>(delegate(ref bool v)
			{
				return v ? 1 : 0;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(bool), typeof(short), new TypeConverter<bool, short>(delegate(ref bool v)
			{
				return v ? 1 : 0;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(bool), typeof(int), new TypeConverter<bool, int>(delegate(ref bool v)
			{
				return v ? 1 : 0;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(bool), typeof(long), new TypeConverter<bool, long>(delegate(ref bool v)
			{
				return v ? 1L : 0L;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(bool), typeof(byte), new TypeConverter<bool, byte>(delegate(ref bool v)
			{
				return v ? 1 : 0;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(bool), typeof(ushort), new TypeConverter<bool, ushort>(delegate(ref bool v)
			{
				return v ? 1 : 0;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(bool), typeof(uint), new TypeConverter<bool, uint>(delegate(ref bool v)
			{
				return v ? 1U : 0U;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(bool), typeof(ulong), new TypeConverter<bool, ulong>(delegate(ref bool v)
			{
				return v ? 1UL : 0UL;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(bool), typeof(float), new TypeConverter<bool, float>(delegate(ref bool v)
			{
				return v ? 1f : 0f;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(bool), typeof(double), new TypeConverter<bool, double>(delegate(ref bool v)
			{
				return v ? 1.0 : 0.0;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(string), typeof(bool), new TypeConverter<string, bool>(delegate(ref string v)
			{
				bool r;
				bool flag = bool.TryParse(v, out r);
				bool flag2;
				if (flag)
				{
					flag2 = r;
				}
				else
				{
					double fromDouble;
					bool result;
					flag2 = double.TryParse(v, out fromDouble) && ConverterGroups.TryConvert<double, bool>(ref fromDouble, out result) && result;
				}
				return flag2;
			}));
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006AF8 File Offset: 0x00004CF8
		private static void RegisterCharConverters()
		{
			TypeConverterRegistry typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(char), typeof(bool), new TypeConverter<char, bool>(delegate(ref char v)
			{
				return v > '\0';
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(char), typeof(sbyte), new TypeConverter<char, sbyte>(delegate(ref char v)
			{
				return (sbyte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(char), typeof(short), new TypeConverter<char, short>(delegate(ref char v)
			{
				return (short)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(char), typeof(int), new TypeConverter<char, int>(delegate(ref char v)
			{
				return (int)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(char), typeof(long), new TypeConverter<char, long>(delegate(ref char v)
			{
				return (long)((ulong)v);
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(char), typeof(byte), new TypeConverter<char, byte>(delegate(ref char v)
			{
				return (byte)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(char), typeof(ushort), new TypeConverter<char, ushort>(delegate(ref char v)
			{
				return (ushort)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(char), typeof(uint), new TypeConverter<char, uint>(delegate(ref char v)
			{
				return (uint)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(char), typeof(ulong), new TypeConverter<char, ulong>(delegate(ref char v)
			{
				return (ulong)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(char), typeof(float), new TypeConverter<char, float>(delegate(ref char v)
			{
				return (float)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(char), typeof(double), new TypeConverter<char, double>(delegate(ref char v)
			{
				return (double)v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(string), typeof(char), new TypeConverter<string, char>(delegate(ref string v)
			{
				return (!string.IsNullOrEmpty(v)) ? v[0] : '\0';
			}));
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006E50 File Offset: 0x00005050
		private static void RegisterColorConverters()
		{
			TypeConverterRegistry typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(Color), typeof(Color32), new TypeConverter<Color, Color32>(delegate(ref Color v)
			{
				return v;
			}));
			typeConverterRegistry = ConverterGroups.s_PrimitivesConverters.registry;
			typeConverterRegistry.Register(typeof(Color32), typeof(Color), new TypeConverter<Color32, Color>(delegate(ref Color32 v)
			{
				return v;
			}));
		}

		// Token: 0x04000058 RID: 88
		private static readonly ConverterGroup s_GlobalConverters = new ConverterGroup("__global_converters", null, null);

		// Token: 0x04000059 RID: 89
		private static readonly ConverterGroup s_PrimitivesConverters = new ConverterGroup("__primitives_converters", null, null);

		// Token: 0x0400005A RID: 90
		private static readonly Dictionary<string, ConverterGroup> s_BindingConverterGroups = new Dictionary<string, ConverterGroup>();
	}
}
