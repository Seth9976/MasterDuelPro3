using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;

// Token: 0x02000005 RID: 5
public class SupportClass
{
	// Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
	[CLSCompliant(false)]
	public static sbyte[] ToSByteArray(byte[] byteArray)
	{
		sbyte[] array = new sbyte[byteArray.Length];
		for (int i = 0; i < byteArray.Length; i++)
		{
			array[i] = (sbyte)byteArray[i];
		}
		return array;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000209C File Offset: 0x0000029C
	[CLSCompliant(false)]
	public static byte[] ToByteArray(sbyte[] sbyteArray)
	{
		byte[] array = new byte[sbyteArray.Length];
		for (int i = 0; i < sbyteArray.Length; i++)
		{
			array[i] = (byte)sbyteArray[i];
		}
		return array;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000020C8 File Offset: 0x000002C8
	public static byte[] ToByteArray(string sourceString)
	{
		byte[] array = new byte[sourceString.Length];
		for (int i = 0; i < sourceString.Length; i++)
		{
			array[i] = (byte)sourceString[i];
		}
		return array;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002100 File Offset: 0x00000300
	public static byte[] ToByteArray(object[] tempObjectArray)
	{
		byte[] array = new byte[tempObjectArray.Length];
		for (int i = 0; i < tempObjectArray.Length; i++)
		{
			array[i] = (byte)tempObjectArray[i];
		}
		return array;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002130 File Offset: 0x00000330
	[CLSCompliant(false)]
	public static int ReadInput(Stream sourceStream, ref sbyte[] target, int start, int count)
	{
		if (target.Length == 0)
		{
			return 0;
		}
		byte[] array = new byte[target.Length];
		int num = 0;
		int num2 = start;
		int num3;
		for (int i = count; i > 0; i -= num3)
		{
			num3 = sourceStream.Read(array, num2, i);
			if (num3 == 0)
			{
				break;
			}
			num += num3;
			num2 += num3;
		}
		if (num == 0)
		{
			return -1;
		}
		for (int j = start; j < start + num; j++)
		{
			target[j] = (sbyte)array[j];
		}
		return num;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x0000219C File Offset: 0x0000039C
	[CLSCompliant(false)]
	public static int ReadInput(TextReader sourceTextReader, ref sbyte[] target, int start, int count)
	{
		if (target.Length == 0)
		{
			return 0;
		}
		char[] array = new char[target.Length];
		int num = sourceTextReader.Read(array, start, count);
		if (num == 0)
		{
			return -1;
		}
		for (int i = start; i < start + num; i++)
		{
			target[i] = (sbyte)array[i];
		}
		return num;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000021E0 File Offset: 0x000003E0
	public static long Identity(long literal)
	{
		return literal;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000021E0 File Offset: 0x000003E0
	[CLSCompliant(false)]
	public static ulong Identity(ulong literal)
	{
		return literal;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000021E0 File Offset: 0x000003E0
	public static float Identity(float literal)
	{
		return literal;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000021E0 File Offset: 0x000003E0
	public static double Identity(double literal)
	{
		return literal;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000021E4 File Offset: 0x000003E4
	public static string FormatDateTime(DateTimeFormatInfo format, DateTime date)
	{
		string timeFormatPattern = SupportClass.DateTimeFormatManager.manager.GetTimeFormatPattern(format);
		string dateFormatPattern = SupportClass.DateTimeFormatManager.manager.GetDateFormatPattern(format);
		return date.ToString(dateFormatPattern + " " + timeFormatPattern, format);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000221D File Offset: 0x0000041D
	public static object PutElement(IDictionary collection, object key, object newValue)
	{
		object obj = collection[key];
		collection[key] = newValue;
		return obj;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x0000222E File Offset: 0x0000042E
	public static bool VectorRemoveElement(IList arrayList, object element)
	{
		bool flag = arrayList.Contains(element);
		arrayList.Remove(element);
		return flag;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x0000223E File Offset: 0x0000043E
	public static object HashtableRemove(Hashtable hashtable, object key)
	{
		object obj = hashtable[key];
		hashtable.Remove(key);
		return obj;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000224E File Offset: 0x0000044E
	public static void SetSize(ArrayList arrayList, int newSize)
	{
		if (newSize < 0)
		{
			throw new ArgumentException();
		}
		if (newSize < arrayList.Count)
		{
			arrayList.RemoveRange(newSize, arrayList.Count - newSize);
			return;
		}
		while (newSize > arrayList.Count)
		{
			arrayList.Add(null);
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002284 File Offset: 0x00000484
	public static object StackPush(Stack stack, object element)
	{
		stack.Push(element);
		return element;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002290 File Offset: 0x00000490
	public static void GetCharsFromString(string sourceString, int sourceStart, int sourceEnd, ref char[] destinationArray, int destinationStart)
	{
		int i = sourceStart;
		int num = destinationStart;
		while (i < sourceEnd)
		{
			destinationArray[num] = sourceString[i];
			i++;
			num++;
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000022BB File Offset: 0x000004BB
	public static FileStream GetFileStream(string FileName, bool Append)
	{
		if (Append)
		{
			return new FileStream(FileName, FileMode.Append);
		}
		return new FileStream(FileName, FileMode.Create);
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000022D0 File Offset: 0x000004D0
	[CLSCompliant(false)]
	public static char[] ToCharArray(sbyte[] sByteArray)
	{
		char[] array = new char[sByteArray.Length];
		sByteArray.CopyTo(array, 0);
		return array;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000022F0 File Offset: 0x000004F0
	public static char[] ToCharArray(byte[] byteArray)
	{
		char[] array = new char[byteArray.Length];
		byteArray.CopyTo(array, 0);
		return array;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002310 File Offset: 0x00000510
	public static object CreateNewInstance(Type classType)
	{
		object obj = null;
		Type[] array = new Type[0];
		ConstructorInfo[] constructors = classType.GetConstructors();
		if (constructors.Length == 0)
		{
			throw new UnauthorizedAccessException();
		}
		for (int i = 0; i < constructors.Length; i++)
		{
			if (constructors[i].GetParameters().Length == 0)
			{
				obj = classType.GetConstructor(array).Invoke(new object[0]);
				break;
			}
			if (i == constructors.Length - 1)
			{
				throw new MethodAccessException();
			}
		}
		return obj;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002376 File Offset: 0x00000576
	public static void WriteStackTrace(Exception throwable, TextWriter stream)
	{
		stream.Write(throwable.StackTrace);
		stream.Flush();
	}

	// Token: 0x0600001B RID: 27 RVA: 0x0000238C File Offset: 0x0000058C
	public static bool EqualsSupport(ICollection source, ICollection target)
	{
		IEnumerator enumerator = SupportClass.ReverseStack(source);
		IEnumerator enumerator2 = SupportClass.ReverseStack(target);
		if (source.Count != target.Count)
		{
			return false;
		}
		while (enumerator.MoveNext() && enumerator2.MoveNext())
		{
			if (!enumerator.Current.Equals(enumerator2.Current))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000023DD File Offset: 0x000005DD
	public static bool EqualsSupport(ICollection source, object target)
	{
		return !(target.GetType() != typeof(ICollection)) && SupportClass.EqualsSupport(source, (ICollection)target);
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002404 File Offset: 0x00000604
	public static bool EqualsSupport(IDictionaryEnumerator source, object target)
	{
		return !(target.GetType() != typeof(IDictionaryEnumerator)) && SupportClass.EqualsSupport(source, (IDictionaryEnumerator)target);
	}

	// Token: 0x0600001E RID: 30 RVA: 0x0000242B File Offset: 0x0000062B
	public static bool EqualsSupport(IDictionaryEnumerator source, IDictionaryEnumerator target)
	{
		while (source.MoveNext() && target.MoveNext())
		{
			if (source.Key.Equals(target.Key) && source.Value.Equals(target.Value))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002468 File Offset: 0x00000668
	public static IEnumerator ReverseStack(ICollection collection)
	{
		if (collection.GetType() == typeof(Stack))
		{
			ArrayList arrayList = new ArrayList(collection);
			arrayList.Reverse();
			return arrayList.GetEnumerator();
		}
		return collection.GetEnumerator();
	}

	// Token: 0x02000006 RID: 6
	public class Tokenizer
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000024A4 File Offset: 0x000006A4
		public Tokenizer(string source)
		{
			this.elements = new ArrayList();
			this.elements.AddRange(source.Split(this.delimiters.ToCharArray()));
			this.RemoveEmptyStrings();
			this.source = source;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024F8 File Offset: 0x000006F8
		public Tokenizer(string source, string delimiters)
		{
			this.elements = new ArrayList();
			this.delimiters = delimiters;
			this.elements.AddRange(source.Split(this.delimiters.ToCharArray()));
			this.RemoveEmptyStrings();
			this.source = source;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002554 File Offset: 0x00000754
		public Tokenizer(string source, string delimiters, bool retDel)
		{
			this.elements = new ArrayList();
			this.delimiters = delimiters;
			this.source = source;
			this.returnDelims = retDel;
			if (this.returnDelims)
			{
				this.Tokenize();
			}
			else
			{
				this.elements.AddRange(source.Split(this.delimiters.ToCharArray()));
			}
			this.RemoveEmptyStrings();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025C4 File Offset: 0x000007C4
		private void Tokenize()
		{
			string text = this.source;
			if (text.IndexOfAny(this.delimiters.ToCharArray()) < 0 && text.Length > 0)
			{
				this.elements.Add(text);
			}
			else if (text.IndexOfAny(this.delimiters.ToCharArray()) < 0 && text.Length <= 0)
			{
				return;
			}
			while (text.IndexOfAny(this.delimiters.ToCharArray()) >= 0)
			{
				if (text.IndexOfAny(this.delimiters.ToCharArray()) == 0)
				{
					if (text.Length > 1)
					{
						this.elements.Add(text.Substring(0, 1));
						text = text.Substring(1);
					}
					else
					{
						text = "";
					}
				}
				else
				{
					string text2 = text.Substring(0, text.IndexOfAny(this.delimiters.ToCharArray()));
					this.elements.Add(text2);
					this.elements.Add(text.Substring(text2.Length, 1));
					if (text.Length > text2.Length + 1)
					{
						text = text.Substring(text2.Length + 1);
					}
					else
					{
						text = "";
					}
				}
			}
			if (text.Length > 0)
			{
				this.elements.Add(text);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002706 File Offset: 0x00000906
		public int Count
		{
			get
			{
				return this.elements.Count;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002713 File Offset: 0x00000913
		public bool HasMoreTokens()
		{
			return this.elements.Count > 0;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002724 File Offset: 0x00000924
		public string NextToken()
		{
			if (this.source == "")
			{
				throw new Exception();
			}
			string text;
			if (this.returnDelims)
			{
				this.RemoveEmptyStrings();
				text = (string)this.elements[0];
				this.elements.RemoveAt(0);
				return text;
			}
			this.elements = new ArrayList();
			this.elements.AddRange(this.source.Split(this.delimiters.ToCharArray()));
			this.RemoveEmptyStrings();
			text = (string)this.elements[0];
			this.elements.RemoveAt(0);
			this.source = this.source.Remove(this.source.IndexOf(text), text.Length);
			this.source = this.source.TrimStart(this.delimiters.ToCharArray());
			return text;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002807 File Offset: 0x00000A07
		public string NextToken(string delimiters)
		{
			this.delimiters = delimiters;
			return this.NextToken();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002818 File Offset: 0x00000A18
		private void RemoveEmptyStrings()
		{
			for (int i = 0; i < this.elements.Count; i++)
			{
				if ((string)this.elements[i] == "")
				{
					this.elements.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x0400002B RID: 43
		private ArrayList elements;

		// Token: 0x0400002C RID: 44
		private string source;

		// Token: 0x0400002D RID: 45
		private string delimiters = " \t\n\r";

		// Token: 0x0400002E RID: 46
		private bool returnDelims;
	}

	// Token: 0x02000007 RID: 7
	public class DateTimeFormatManager
	{
		// Token: 0x0400002F RID: 47
		public static SupportClass.DateTimeFormatManager.DateTimeFormatHashTable manager = new SupportClass.DateTimeFormatManager.DateTimeFormatHashTable();

		// Token: 0x02000008 RID: 8
		public class DateTimeFormatHashTable : Hashtable
		{
			// Token: 0x0600002C RID: 44 RVA: 0x00002874 File Offset: 0x00000A74
			public void SetDateFormatPattern(DateTimeFormatInfo format, string newPattern)
			{
				if (this[format] != null)
				{
					((SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties)this[format]).DateFormatPattern = newPattern;
					return;
				}
				this.Add(format, new SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties
				{
					DateFormatPattern = newPattern
				});
			}

			// Token: 0x0600002D RID: 45 RVA: 0x000028B2 File Offset: 0x00000AB2
			public string GetDateFormatPattern(DateTimeFormatInfo format)
			{
				if (this[format] == null)
				{
					return "d-MMM-yy";
				}
				return ((SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties)this[format]).DateFormatPattern;
			}

			// Token: 0x0600002E RID: 46 RVA: 0x000028D4 File Offset: 0x00000AD4
			public void SetTimeFormatPattern(DateTimeFormatInfo format, string newPattern)
			{
				if (this[format] != null)
				{
					((SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties)this[format]).TimeFormatPattern = newPattern;
					return;
				}
				this.Add(format, new SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties
				{
					TimeFormatPattern = newPattern
				});
			}

			// Token: 0x0600002F RID: 47 RVA: 0x00002912 File Offset: 0x00000B12
			public string GetTimeFormatPattern(DateTimeFormatInfo format)
			{
				if (this[format] == null)
				{
					return "h:mm:ss tt";
				}
				return ((SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties)this[format]).TimeFormatPattern;
			}

			// Token: 0x02000009 RID: 9
			private class DateTimeFormatProperties
			{
				// Token: 0x04000030 RID: 48
				public string DateFormatPattern = "d-MMM-yy";

				// Token: 0x04000031 RID: 49
				public string TimeFormatPattern = "h:mm:ss tt";
			}
		}
	}

	// Token: 0x0200000A RID: 10
	public class ArrayListSupport
	{
		// Token: 0x06000032 RID: 50 RVA: 0x0000295C File Offset: 0x00000B5C
		public static object[] ToArray(ArrayList collection, object[] objects)
		{
			int num = 0;
			foreach (object obj in collection)
			{
				objects[num++] = obj;
			}
			return objects;
		}
	}

	// Token: 0x0200000B RID: 11
	public class ThreadClass : IThreadRunnable
	{
		// Token: 0x06000034 RID: 52 RVA: 0x0000298A File Offset: 0x00000B8A
		public ThreadClass()
		{
			this.threadField = new Thread(new ThreadStart(this.Run));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029AA File Offset: 0x00000BAA
		public ThreadClass(string Name)
		{
			this.threadField = new Thread(new ThreadStart(this.Run));
			this.Name = Name;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029D1 File Offset: 0x00000BD1
		public ThreadClass(ThreadStart Start)
		{
			this.threadField = new Thread(Start);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029E5 File Offset: 0x00000BE5
		public ThreadClass(ThreadStart Start, string Name)
		{
			this.threadField = new Thread(Start);
			this.Name = Name;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A00 File Offset: 0x00000C00
		public virtual void Run()
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A02 File Offset: 0x00000C02
		public virtual void Start()
		{
			this.threadField.Start();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A0F File Offset: 0x00000C0F
		public virtual void Interrupt()
		{
			this.threadField.Interrupt();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A1C File Offset: 0x00000C1C
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002A24 File Offset: 0x00000C24
		public Thread Instance
		{
			get
			{
				return this.threadField;
			}
			set
			{
				this.threadField = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A2D File Offset: 0x00000C2D
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002A3A File Offset: 0x00000C3A
		public string Name
		{
			get
			{
				return this.threadField.Name;
			}
			set
			{
				if (this.threadField.Name == null)
				{
					this.threadField.Name = value;
				}
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002A55 File Offset: 0x00000C55
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002A62 File Offset: 0x00000C62
		public ThreadPriority Priority
		{
			get
			{
				return this.threadField.Priority;
			}
			set
			{
				this.threadField.Priority = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002A70 File Offset: 0x00000C70
		public bool IsAlive
		{
			get
			{
				return this.threadField.IsAlive;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002A7D File Offset: 0x00000C7D
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002A8A File Offset: 0x00000C8A
		public bool IsBackground
		{
			get
			{
				return this.threadField.IsBackground;
			}
			set
			{
				this.threadField.IsBackground = value;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A98 File Offset: 0x00000C98
		public void Join()
		{
			this.threadField.Join();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public void Join(long MiliSeconds)
		{
			lock (this)
			{
				this.threadField.Join(new TimeSpan(MiliSeconds * 10000L));
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public void Join(long MiliSeconds, int NanoSeconds)
		{
			lock (this)
			{
				this.threadField.Join(new TimeSpan(MiliSeconds * 10000L + (long)(NanoSeconds * 100)));
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B4C File Offset: 0x00000D4C
		public void Resume()
		{
			this.threadField.Resume();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B59 File Offset: 0x00000D59
		public void Abort()
		{
			this.threadField.Abort();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B68 File Offset: 0x00000D68
		public void Abort(object stateInfo)
		{
			lock (this)
			{
				this.threadField.Abort(stateInfo);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BAC File Offset: 0x00000DAC
		public void Suspend()
		{
			this.threadField.Suspend();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BBC File Offset: 0x00000DBC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Thread[",
				this.Name,
				",",
				this.Priority.ToString(),
				",]"
			});
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C0C File Offset: 0x00000E0C
		public static SupportClass.ThreadClass Current()
		{
			return new SupportClass.ThreadClass
			{
				Instance = Thread.CurrentThread
			};
		}

		// Token: 0x04000032 RID: 50
		private Thread threadField;
	}

	// Token: 0x0200000C RID: 12
	public class CollectionSupport : CollectionBase
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002C26 File Offset: 0x00000E26
		public virtual bool Add(object element)
		{
			return base.List.Add(element) != -1;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C3C File Offset: 0x00000E3C
		public virtual bool AddAll(ICollection collection)
		{
			bool flag = false;
			if (collection != null)
			{
				IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current != null)
					{
						flag = this.Add(enumerator.Current);
					}
				}
			}
			return flag;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C7A File Offset: 0x00000E7A
		public virtual bool AddAll(SupportClass.CollectionSupport collection)
		{
			return this.AddAll(collection);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C83 File Offset: 0x00000E83
		public virtual bool Contains(object element)
		{
			return base.List.Contains(element);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C94 File Offset: 0x00000E94
		public virtual bool ContainsAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
			while (enumerator.MoveNext() && (flag = this.Contains(enumerator.Current)))
			{
			}
			return flag;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002CCA File Offset: 0x00000ECA
		public virtual bool ContainsAll(SupportClass.CollectionSupport collection)
		{
			return this.ContainsAll(collection);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002CD3 File Offset: 0x00000ED3
		public virtual bool IsEmpty()
		{
			return base.Count == 0;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public virtual bool Remove(object element)
		{
			bool flag = false;
			if (this.Contains(element))
			{
				base.List.Remove(element);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D08 File Offset: 0x00000F08
		public virtual bool RemoveAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (this.Contains(enumerator.Current))
				{
					flag = this.Remove(enumerator.Current);
				}
			}
			return flag;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002D49 File Offset: 0x00000F49
		public virtual bool RemoveAll(SupportClass.CollectionSupport collection)
		{
			return this.RemoveAll(collection);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002D54 File Offset: 0x00000F54
		public virtual bool RetainAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = base.GetEnumerator();
			SupportClass.CollectionSupport collectionSupport = new SupportClass.CollectionSupport();
			collectionSupport.AddAll(collection);
			while (enumerator.MoveNext())
			{
				if (!collectionSupport.Contains(enumerator.Current))
				{
					flag = this.Remove(enumerator.Current);
					if (flag)
					{
						enumerator = base.GetEnumerator();
					}
				}
			}
			return flag;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public virtual bool RetainAll(SupportClass.CollectionSupport collection)
		{
			return this.RetainAll(collection);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public virtual object[] ToArray()
		{
			int num = 0;
			object[] array = new object[base.Count];
			foreach (object obj in this)
			{
				array[num++] = obj;
			}
			return array;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public virtual object[] ToArray(object[] objects)
		{
			int num = 0;
			foreach (object obj in this)
			{
				objects[num++] = obj;
			}
			return objects;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E1E File Offset: 0x0000101E
		public static SupportClass.CollectionSupport ToCollectionSupport(object[] array)
		{
			SupportClass.CollectionSupport collectionSupport = new SupportClass.CollectionSupport();
			collectionSupport.AddAll(array);
			return collectionSupport;
		}
	}

	// Token: 0x0200000D RID: 13
	public class ListCollectionSupport : ArrayList
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002E2D File Offset: 0x0000102D
		public ListCollectionSupport()
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002E35 File Offset: 0x00001035
		public ListCollectionSupport(ICollection collection)
			: base(collection)
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E3E File Offset: 0x0000103E
		public ListCollectionSupport(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E47 File Offset: 0x00001047
		public new virtual bool Add(object valueToInsert)
		{
			base.Insert(this.Count, valueToInsert);
			return true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002E58 File Offset: 0x00001058
		public virtual bool AddAll(int index, IList list)
		{
			bool flag = false;
			if (list != null)
			{
				IEnumerator enumerator = new ArrayList(list).GetEnumerator();
				int num = index;
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					base.Insert(num++, obj);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E96 File Offset: 0x00001096
		public virtual bool AddAll(IList collection)
		{
			return this.AddAll(this.Count, collection);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002EA5 File Offset: 0x000010A5
		public virtual bool AddAll(SupportClass.CollectionSupport collection)
		{
			return this.AddAll(this.Count, collection);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002EB4 File Offset: 0x000010B4
		public virtual bool AddAll(int index, SupportClass.CollectionSupport collection)
		{
			return this.AddAll(index, collection);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002EBE File Offset: 0x000010BE
		public virtual object ListCollectionClone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002EC6 File Offset: 0x000010C6
		public virtual IEnumerator ListIterator()
		{
			return base.GetEnumerator();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002ED0 File Offset: 0x000010D0
		public virtual bool RemoveAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
			while (enumerator.MoveNext())
			{
				flag = true;
				if (base.Contains(enumerator.Current))
				{
					base.Remove(enumerator.Current);
				}
			}
			return flag;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002F12 File Offset: 0x00001112
		public virtual bool RemoveAll(SupportClass.CollectionSupport collection)
		{
			return this.RemoveAll(collection);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F1B File Offset: 0x0000111B
		public virtual object RemoveElement(int index)
		{
			object obj = this[index];
			this.RemoveAt(index);
			return obj;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F2C File Offset: 0x0000112C
		public virtual bool RemoveElement(object element)
		{
			bool flag = false;
			if (this.Contains(element))
			{
				base.Remove(element);
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F4E File Offset: 0x0000114E
		public virtual object RemoveFirst()
		{
			object obj = this[0];
			this.RemoveAt(0);
			return obj;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002F5E File Offset: 0x0000115E
		public virtual object RemoveLast()
		{
			object obj = this[this.Count - 1];
			base.RemoveAt(this.Count - 1);
			return obj;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002F7C File Offset: 0x0000117C
		public virtual bool RetainAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = this.GetEnumerator();
			SupportClass.ListCollectionSupport listCollectionSupport = new SupportClass.ListCollectionSupport(collection);
			while (enumerator.MoveNext())
			{
				if (!listCollectionSupport.Contains(enumerator.Current))
				{
					flag = this.RemoveElement(enumerator.Current);
					if (flag)
					{
						enumerator = this.GetEnumerator();
					}
				}
			}
			return flag;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002FC9 File Offset: 0x000011C9
		public virtual bool RetainAll(SupportClass.CollectionSupport collection)
		{
			return this.RetainAll(collection);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002FD4 File Offset: 0x000011D4
		public virtual bool ContainsAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
			while (enumerator.MoveNext() && (flag = this.Contains(enumerator.Current)))
			{
			}
			return flag;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000300A File Offset: 0x0000120A
		public virtual bool ContainsAll(SupportClass.CollectionSupport collection)
		{
			return this.ContainsAll(collection);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003014 File Offset: 0x00001214
		public virtual SupportClass.ListCollectionSupport SubList(int startIndex, int endIndex)
		{
			this.GetEnumerator();
			SupportClass.ListCollectionSupport listCollectionSupport = new SupportClass.ListCollectionSupport();
			for (int i = startIndex; i < endIndex; i++)
			{
				listCollectionSupport.Add(this[i]);
			}
			return listCollectionSupport;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000304C File Offset: 0x0000124C
		public virtual object[] ToArray(object[] objects)
		{
			if (objects.Length < this.Count)
			{
				objects = new object[this.Count];
			}
			int num = 0;
			foreach (object obj in this)
			{
				objects[num++] = obj;
			}
			return objects;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003094 File Offset: 0x00001294
		public virtual IEnumerator ListIterator(int index)
		{
			if (index < 0 || index > this.Count)
			{
				throw new IndexOutOfRangeException();
			}
			IEnumerator enumerator = this.GetEnumerator();
			if (index > 0)
			{
				int num = 0;
				while (enumerator.MoveNext() && num < index - 1)
				{
					num++;
				}
			}
			return enumerator;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000030D6 File Offset: 0x000012D6
		public virtual object GetLast()
		{
			if (this.Count == 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this[this.Count - 1];
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030F4 File Offset: 0x000012F4
		public virtual bool IsEmpty()
		{
			return this.Count == 0;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000030FF File Offset: 0x000012FF
		public virtual object Set(int index, object element)
		{
			object obj = this[index];
			this[index] = element;
			return obj;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003110 File Offset: 0x00001310
		public virtual object Get(int index)
		{
			return this[index];
		}
	}

	// Token: 0x0200000E RID: 14
	public class ArraysSupport
	{
		// Token: 0x06000078 RID: 120 RVA: 0x0000311C File Offset: 0x0000131C
		public static bool IsArrayEqual(Array array1, Array array2)
		{
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (!array1.GetValue(i).Equals(array2.GetValue(i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003164 File Offset: 0x00001364
		public static void FillArray(Array array, int fromindex, int toindex, object val)
		{
			object obj = val;
			Type elementType = array.GetType().GetElementType();
			if (elementType != val.GetType())
			{
				obj = Convert.ChangeType(val, elementType);
			}
			if (array.Length == 0)
			{
				throw new NullReferenceException();
			}
			if (fromindex > toindex)
			{
				throw new ArgumentException();
			}
			if (fromindex < 0 || array.Length < toindex)
			{
				throw new IndexOutOfRangeException();
			}
			int num;
			if (fromindex <= 0)
			{
				num = fromindex;
			}
			else
			{
				fromindex = (num = fromindex) - 1;
			}
			for (int i = num; i < toindex; i++)
			{
				array.SetValue(obj, i);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000031E0 File Offset: 0x000013E0
		public static void FillArray(Array array, object val)
		{
			SupportClass.ArraysSupport.FillArray(array, 0, array.Length, val);
		}
	}

	// Token: 0x0200000F RID: 15
	public class SetSupport : ArrayList
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00002E2D File Offset: 0x0000102D
		public SetSupport()
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002E35 File Offset: 0x00001035
		public SetSupport(ICollection collection)
			: base(collection)
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002E3E File Offset: 0x0000103E
		public SetSupport(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000031F0 File Offset: 0x000013F0
		public new virtual bool Add(object objectToAdd)
		{
			if (this.Contains(objectToAdd))
			{
				return false;
			}
			base.Add(objectToAdd);
			return true;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003208 File Offset: 0x00001408
		public virtual bool AddAll(ICollection collection)
		{
			bool flag = false;
			if (collection != null)
			{
				IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current != null)
					{
						flag = this.Add(enumerator.Current);
					}
				}
			}
			return flag;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003246 File Offset: 0x00001446
		public virtual bool AddAll(SupportClass.CollectionSupport collection)
		{
			return this.AddAll(collection);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003250 File Offset: 0x00001450
		public virtual bool ContainsAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = collection.GetEnumerator();
			while (enumerator.MoveNext() && (flag = this.Contains(enumerator.Current)))
			{
			}
			return flag;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003281 File Offset: 0x00001481
		public virtual bool ContainsAll(SupportClass.CollectionSupport collection)
		{
			return this.ContainsAll(collection);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000030F4 File Offset: 0x000012F4
		public virtual bool IsEmpty()
		{
			return this.Count == 0;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000328C File Offset: 0x0000148C
		public new virtual bool Remove(object elementToRemove)
		{
			bool flag = false;
			if (this.Contains(elementToRemove))
			{
				flag = true;
			}
			base.Remove(elementToRemove);
			return flag;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000032B0 File Offset: 0x000014B0
		public virtual bool RemoveAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = collection.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!flag && this.Contains(enumerator.Current))
				{
					flag = true;
				}
				this.Remove(enumerator.Current);
			}
			return flag;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000032F1 File Offset: 0x000014F1
		public virtual bool RemoveAll(SupportClass.CollectionSupport collection)
		{
			return this.RemoveAll(collection);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000032FC File Offset: 0x000014FC
		public virtual bool RetainAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = collection.GetEnumerator();
			SupportClass.SetSupport setSupport = (SupportClass.SetSupport)collection;
			while (enumerator.MoveNext())
			{
				if (!setSupport.Contains(enumerator.Current))
				{
					flag = this.Remove(enumerator.Current);
					enumerator = this.GetEnumerator();
				}
			}
			return flag;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003346 File Offset: 0x00001546
		public virtual bool RetainAll(SupportClass.CollectionSupport collection)
		{
			return this.RetainAll(collection);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003350 File Offset: 0x00001550
		public new virtual object[] ToArray()
		{
			int num = 0;
			object[] array = new object[this.Count];
			foreach (object obj in this)
			{
				array[num++] = obj;
			}
			return array;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000338C File Offset: 0x0000158C
		public virtual object[] ToArray(object[] objects)
		{
			int num = 0;
			foreach (object obj in this)
			{
				objects[num++] = obj;
			}
			return objects;
		}
	}

	// Token: 0x02000010 RID: 16
	public class AbstractSetSupport : SupportClass.SetSupport
	{
	}

	// Token: 0x02000011 RID: 17
	public class MessageDigestSupport
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000033C2 File Offset: 0x000015C2
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000033CA File Offset: 0x000015CA
		public HashAlgorithm Algorithm
		{
			get
			{
				return this.algorithm;
			}
			set
			{
				this.algorithm = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000033D3 File Offset: 0x000015D3
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000033DB File Offset: 0x000015DB
		public byte[] Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000033E4 File Offset: 0x000015E4
		public string AlgorithmName
		{
			get
			{
				return this.algorithmName;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000033EC File Offset: 0x000015EC
		public MessageDigestSupport(string algorithm)
		{
			if (algorithm.Equals("SHA-1"))
			{
				this.algorithmName = "SHA";
			}
			else
			{
				this.algorithmName = algorithm;
			}
			this.Algorithm = (HashAlgorithm)CryptoConfig.CreateFromName(this.algorithmName);
			this.position = 0;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000343D File Offset: 0x0000163D
		[CLSCompliant(false)]
		public sbyte[] DigestData()
		{
			sbyte[] array = SupportClass.ToSByteArray(this.Algorithm.ComputeHash(this.data));
			this.Reset();
			return array;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000345B File Offset: 0x0000165B
		[CLSCompliant(false)]
		public sbyte[] DigestData(byte[] newData)
		{
			this.Update(newData);
			return this.DigestData();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000346C File Offset: 0x0000166C
		public void Update(byte[] newData)
		{
			if (this.position == 0)
			{
				this.Data = newData;
				this.position = this.Data.Length - 1;
				return;
			}
			byte[] array = this.Data;
			this.Data = new byte[newData.Length + this.position + 1];
			array.CopyTo(this.Data, 0);
			newData.CopyTo(this.Data, array.Length);
			this.position = this.Data.Length - 1;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000034E4 File Offset: 0x000016E4
		public void Update(byte newData)
		{
			this.Update(new byte[] { newData });
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003504 File Offset: 0x00001704
		public void Update(byte[] newData, int offset, int count)
		{
			byte[] array = new byte[count];
			Array.Copy(newData, offset, array, 0, count);
			this.Update(array);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003529 File Offset: 0x00001729
		public void Reset()
		{
			this.data = null;
			this.position = 0;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003539 File Offset: 0x00001739
		public override string ToString()
		{
			return this.Algorithm.ToString();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003546 File Offset: 0x00001746
		public static SupportClass.MessageDigestSupport GetInstance(string algorithm)
		{
			return new SupportClass.MessageDigestSupport(algorithm);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003550 File Offset: 0x00001750
		[CLSCompliant(false)]
		public static bool EquivalentDigest(sbyte[] firstDigest, sbyte[] secondDigest)
		{
			bool flag = false;
			if (firstDigest.Length == secondDigest.Length)
			{
				int num = 0;
				flag = true;
				while (flag && num < firstDigest.Length)
				{
					flag = firstDigest[num] == secondDigest[num];
					num++;
				}
			}
			return flag;
		}

		// Token: 0x04000033 RID: 51
		private HashAlgorithm algorithm;

		// Token: 0x04000034 RID: 52
		private byte[] data;

		// Token: 0x04000035 RID: 53
		private int position;

		// Token: 0x04000036 RID: 54
		private string algorithmName;
	}

	// Token: 0x02000012 RID: 18
	public class SecureRandomSupport
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00003584 File Offset: 0x00001784
		public SecureRandomSupport()
		{
			this.generator = new RNGCryptoServiceProvider();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003597 File Offset: 0x00001797
		public SecureRandomSupport(byte[] seed)
		{
			this.generator = new RNGCryptoServiceProvider(seed);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000035AB File Offset: 0x000017AB
		[CLSCompliant(false)]
		public sbyte[] NextBytes(byte[] randomnumbersarray)
		{
			this.generator.GetBytes(randomnumbersarray);
			return SupportClass.ToSByteArray(randomnumbersarray);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000035C0 File Offset: 0x000017C0
		public static byte[] GetSeed(int numberOfBytes)
		{
			RandomNumberGenerator randomNumberGenerator = new RNGCryptoServiceProvider();
			byte[] array = new byte[numberOfBytes];
			randomNumberGenerator.GetBytes(array);
			return array;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000035E0 File Offset: 0x000017E0
		public void SetSeed(byte[] newSeed)
		{
			this.generator = new RNGCryptoServiceProvider(newSeed);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000035F0 File Offset: 0x000017F0
		public void SetSeed(long newSeed)
		{
			byte[] array = new byte[8];
			for (int i = 7; i > 0; i--)
			{
				array[i] = (byte)(newSeed - (newSeed >> 8 << 8));
				newSeed >>= 8;
			}
			this.SetSeed(array);
		}

		// Token: 0x04000037 RID: 55
		private RNGCryptoServiceProvider generator;
	}

	// Token: 0x02000013 RID: 19
	public interface SingleThreadModel
	{
	}
}
