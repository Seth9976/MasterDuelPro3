using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Windows.Forms
{
	/// <summary>Implements a basic data transfer mechanism.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000067 RID: 103
	[ClassInterface(ClassInterfaceType.None)]
	public class DataObject : IDataObject, IDataObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataObject" /> class.</summary>
		// Token: 0x060004D2 RID: 1234 RVA: 0x00013165 File Offset: 0x00011365
		public DataObject()
		{
			this.entries = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataObject" /> class and adds the specified object in the specified format.</summary>
		/// <param name="format">The format of the specified data. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
		/// <param name="data">The data to store. </param>
		// Token: 0x060004D3 RID: 1235 RVA: 0x00013174 File Offset: 0x00011374
		public DataObject(string format, object data)
		{
			this.SetData(format, data);
		}

		/// <summary>Returns the data associated with the specified data format.</summary>
		/// <returns>The data associated with the specified format, or null.</returns>
		/// <param name="format">The format of the data to retrieve. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004D4 RID: 1236 RVA: 0x00013184 File Offset: 0x00011384
		public virtual object GetData(string format)
		{
			return this.GetData(format, true);
		}

		/// <summary>Returns the data associated with the specified data format, using an automated conversion parameter to determine whether to convert the data to the format.</summary>
		/// <returns>The data associated with the specified format, or null.</returns>
		/// <param name="format">The format of the data to retrieve. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <param name="autoConvert">true to the convert data to the specified format; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004D5 RID: 1237 RVA: 0x00013190 File Offset: 0x00011390
		public virtual object GetData(string format, bool autoConvert)
		{
			DataObject.Entry entry;
			if (autoConvert)
			{
				entry = DataObject.Entry.FindConvertible(this.entries, format);
			}
			else
			{
				entry = DataObject.Entry.Find(this.entries, format);
			}
			if (entry == null)
			{
				return null;
			}
			return entry.Data;
		}

		/// <summary>Determines whether data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to, the specified format.</summary>
		/// <returns>true if data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to, the specified format; otherwise, false.</returns>
		/// <param name="format">The format to check for. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004D6 RID: 1238 RVA: 0x000131C7 File Offset: 0x000113C7
		public virtual bool GetDataPresent(string format)
		{
			return this.GetDataPresent(format, true);
		}

		/// <summary>Determines whether this <see cref="T:System.Windows.Forms.DataObject" /> contains data in the specified format or, optionally, contains data that can be converted to the specified format.</summary>
		/// <returns>true if the data is in, or can be converted to, the specified format; otherwise, false.</returns>
		/// <param name="format">The format to check for. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <param name="autoConvert">true to determine whether data stored in this <see cref="T:System.Windows.Forms.DataObject" /> can be converted to the specified format; false to check whether the data is in the specified format. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004D7 RID: 1239 RVA: 0x000131D1 File Offset: 0x000113D1
		public virtual bool GetDataPresent(string format, bool autoConvert)
		{
			if (autoConvert)
			{
				return DataObject.Entry.FindConvertible(this.entries, format) != null;
			}
			return DataObject.Entry.Find(this.entries, format) != null;
		}

		/// <summary>Returns a list of all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with or can be converted to.</summary>
		/// <returns>An array of type <see cref="T:System.String" />, containing a list of all formats that are supported by the data stored in this object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004D8 RID: 1240 RVA: 0x000131F5 File Offset: 0x000113F5
		public virtual string[] GetFormats()
		{
			return this.GetFormats(true);
		}

		/// <summary>Returns a list of all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with or can be converted to, using an automatic conversion parameter to determine whether to retrieve only native data formats or all formats that the data can be converted to.</summary>
		/// <returns>An array of type <see cref="T:System.String" />, containing a list of all formats that are supported by the data stored in this object.</returns>
		/// <param name="autoConvert">true to retrieve all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to; false to retrieve only native data formats. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004D9 RID: 1241 RVA: 0x000131FE File Offset: 0x000113FE
		public virtual string[] GetFormats(bool autoConvert)
		{
			return DataObject.Entry.Entries(this.entries, autoConvert);
		}

		/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the object type as the data format.</summary>
		/// <param name="data">The data to store. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004DA RID: 1242 RVA: 0x0001320C File Offset: 0x0001140C
		public virtual void SetData(object data)
		{
			this.SetData(data.GetType(), data);
		}

		/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the specified format and indicating whether the data can be converted to another format.</summary>
		/// <param name="format">The format associated with the data. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <param name="autoConvert">true to allow the data to be converted to another format; otherwise, false. </param>
		/// <param name="data">The data to store. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004DB RID: 1243 RVA: 0x0001321C File Offset: 0x0001141C
		public virtual void SetData(string format, bool autoConvert, object data)
		{
			DataObject.Entry entry = DataObject.Entry.Find(this.entries, format);
			if (entry == null)
			{
				entry = new DataObject.Entry(format, data, autoConvert);
				lock (this)
				{
					if (this.entries == null)
					{
						this.entries = entry;
					}
					else
					{
						DataObject.Entry next = this.entries;
						while (next.next != null)
						{
							next = next.next;
						}
						next.next = entry;
					}
				}
				return;
			}
			entry.Data = data;
		}

		/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the specified format.</summary>
		/// <param name="format">The format associated with the data. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <param name="data">The data to store. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004DC RID: 1244 RVA: 0x000132A4 File Offset: 0x000114A4
		public virtual void SetData(string format, object data)
		{
			this.SetData(format, true, data);
		}

		/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the specified type as the format.</summary>
		/// <param name="format">A <see cref="T:System.Type" /> representing the format associated with the data. </param>
		/// <param name="data">The data to store. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004DD RID: 1245 RVA: 0x000132AF File Offset: 0x000114AF
		public virtual void SetData(Type format, object data)
		{
			this.SetData(this.EnsureFormat(format), true, data);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000132C0 File Offset: 0x000114C0
		internal string EnsureFormat(string name)
		{
			DataFormats.Format format = DataFormats.Format.Find(name);
			if (format == null)
			{
				format = DataFormats.Format.Add(name);
			}
			return format.Name;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000132E4 File Offset: 0x000114E4
		internal string EnsureFormat(Type type)
		{
			return this.EnsureFormat(type.FullName);
		}

		/// <summary>Creates a connection between a data object and an advisory sink. This method is called by an object that supports an advisory sink and enables the advisory sink to be notified of changes in the object's data.</summary>
		/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following: ValueDescriptionS_OKThe advisory connection was created.E_NOTIMPLThis method is not implemented on the data object.DV_E_LINDEXThere is an invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.DV_E_FORMATETCThere is an invalid value for the <paramref name="pFormatetc" /> parameter.OLE_E_ADVISENOTSUPPORTEDThe data object does not support change notification.</returns>
		/// <param name="pFormatetc"> A <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, target device, aspect, and medium that will be used for future notifications.</param>
		/// <param name="advf">One of the <see cref="T:System.Runtime.InteropServices.ComTypes.ADVF" /> values that specifies a group of flags for controlling the advisory connection.</param>
		/// <param name="pAdvSink">A pointer to the <see cref="T:System.Runtime.InteropServices.ComTypes.IAdviseSink" /> interface on the advisory sink that will receive the change notification.</param>
		/// <param name="pdwConnection">When this method returns, contains a pointer to a DWORD token that identifies this connection. You can use this token later to delete the advisory connection by passing it to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.DUnadvise(System.Int32)" />. If this value is zero, the connection was not established. This parameter is passed uninitialized.</param>
		// Token: 0x060004E0 RID: 1248 RVA: 0x00003D19 File Offset: 0x00001F19
		int IDataObject.DAdvise(ref FORMATETC pFormatetc, ADVF advf, IAdviseSink adviseSink, out int connection)
		{
			throw new NotImplementedException();
		}

		/// <summary>Destroys a notification connection that had been previously established.</summary>
		/// <param name="dwConnection">A DWORD token that specifies the connection to remove. Use the value returned by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.DAdvise(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.ADVF,System.Runtime.InteropServices.ComTypes.IAdviseSink,System.Int32@)" /> when the connection was originally established.</param>
		// Token: 0x060004E1 RID: 1249 RVA: 0x00003D19 File Offset: 0x00001F19
		void IDataObject.DUnadvise(int connection)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an object that can be used to enumerate the current advisory connections.</summary>
		/// <returns>This method supports the standard return value E_OUTOFMEMORY, as well as the following:ValueDescriptionS_OKThe enumerator object is successfully instantiated or there are no connections.OLE_E_ADVISENOTSUPPORTEDThis object does not support advisory notifications.</returns>
		/// <param name="enumAdvise">When this method returns, contains an <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumSTATDATA" /> that receives the interface pointer to the new enumerator object. If the implementation sets <paramref name="enumAdvise" /> to null, there are no connections to advisory sinks at this time. This parameter is passed uninitialized.</param>
		// Token: 0x060004E2 RID: 1250 RVA: 0x00003D19 File Offset: 0x00001F19
		int IDataObject.EnumDAdvise(out IEnumSTATDATA enumAdvise)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an object for enumerating the <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures for a data object. These structures are used in calls to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> or <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.SetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@,System.Boolean)" />. </summary>
		/// <returns>This method supports the standard return values E_INVALIDARG and E_OUTOFMEMORY, as well as the following:ValueDescriptionS_OKThe enumerator object was successfully created.E_NOTIMPLThe direction specified by the <paramref name="direction" /> parameter is not supported.OLE_S_USEREGRequests that OLE enumerate the formats from the registry.</returns>
		/// <param name="dwDirection">One of the <see cref="T:System.Runtime.InteropServices.ComTypes.DATADIR" /> values that specifies the direction of the data.</param>
		// Token: 0x060004E3 RID: 1251 RVA: 0x00003D19 File Offset: 0x00001F19
		IEnumFORMATETC IDataObject.EnumFormatEtc(DATADIR direction)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides a standard <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure that is logically equivalent to a more complex structure. Use this method to determine whether two different <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures would return the same data, removing the need for duplicate rendering.</summary>
		/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following: ValueDescriptionS_OKThe returned <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure is different from the one that was passed.DATA_S_SAMEFORMATETCThe <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures are the same and null is returned in the <paramref name="formatOut" /> parameter.DV_E_LINDEXThere is an invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.DV_E_FORMATETCThere is an invalid value for the <paramref name="pFormatetc" /> parameter.OLE_E_NOTRUNNINGThe application is not running.</returns>
		/// <param name="pformatetcIn">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device that the caller would like to use to retrieve data in a subsequent call such as <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. The <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> member is not significant in this case and should be ignored.</param>
		/// <param name="pformatetcOut">When this method returns, contains a pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure that contains the most general information possible for a specific rendering, making it canonically equivalent to <paramref name="formatetIn" />. The caller must allocate this structure and the <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetCanonicalFormatEtc(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.FORMATETC@)" /> method must fill in the data. To retrieve data in a subsequent call such as <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />, the caller uses the supplied value of <paramref name="formatOut" />, unless the value supplied is null. This value is null if the method returns DATA_S_SAMEFORMATETC. The <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> member is not significant in this case and should be ignored. This parameter is passed uninitialized.</param>
		// Token: 0x060004E4 RID: 1252 RVA: 0x00003D19 File Offset: 0x00001F19
		int IDataObject.GetCanonicalFormatEtc(ref FORMATETC formatIn, out FORMATETC formatOut)
		{
			throw new NotImplementedException();
		}

		/// <summary>Obtains data from a source data object. The <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> method, which is called by a data consumer, renders the data described in the specified <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure and transfers it through the specified <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure. The caller then assumes responsibility for releasing the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure.</summary>
		/// <param name="formatetc">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use when passing the data. It is possible to specify more than one medium by using the Boolean OR operator, allowing the method to choose the best medium among those specified.</param>
		/// <param name="medium">When this method returns, contains a pointer to the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure that indicates the storage medium containing the returned data through its <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.tymed" /> member, and the responsibility for releasing the medium through the value of its <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member. If <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> is null, the receiver of the medium is responsible for releasing it; otherwise, <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> points to the IUnknown interface on the appropriate object so its Release method can be called. The medium must be allocated and filled in by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory to perform this operation.</exception>
		// Token: 0x060004E5 RID: 1253 RVA: 0x00003D19 File Offset: 0x00001F19
		void IDataObject.GetData(ref FORMATETC format, out STGMEDIUM medium)
		{
			throw new NotImplementedException();
		}

		/// <summary>Obtains data from a source data object. This method, which is called by a data consumer, differs from the <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> method in that the caller must allocate and free the specified storage medium.</summary>
		/// <param name="formatetc">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use when passing the data. Only one medium can be specified in <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" />, and only the following <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> values are valid: <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ISTORAGE" />, <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ISTREAM" />, <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_HGLOBAL" />, or <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_FILE" />.</param>
		/// <param name="medium">A <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" />, passed by reference, that defines the storage medium containing the data being transferred. The medium must be allocated by the caller and filled in by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetDataHere(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. The caller must also free the medium. The implementation of this method must always supply a value of null for the <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member of the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure that this parameter points to.</param>
		// Token: 0x060004E6 RID: 1254 RVA: 0x00003D19 File Offset: 0x00001F19
		void IDataObject.GetDataHere(ref FORMATETC format, ref STGMEDIUM medium)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the data object is capable of rendering the data described in the <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure. Objects attempting a paste or drop operation can call this method before calling <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> to get an indication of whether the operation may be successful.</summary>
		/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following: ValueDescriptionS_OKA subsequent call to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> would probably be successful.DV_E_LINDEXAn invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.DV_E_FORMATETCAn invalid value for the <paramref name="pFormatetc" /> parameter.DV_E_TYMEDAn invalid <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.tymed" /> value.DV_E_DVASPECTAn invalid <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.dwAspect" /> value.OLE_E_NOTRUNNINGThe application is not running.</returns>
		/// <param name="formatetc">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use for the query.</param>
		// Token: 0x060004E7 RID: 1255 RVA: 0x00003D19 File Offset: 0x00001F19
		int IDataObject.QueryGetData(ref FORMATETC format)
		{
			throw new NotImplementedException();
		}

		/// <summary>Transfers data to the object that implements this method. This method is called by an object that contains a data source.</summary>
		/// <param name="pFormatetcIn">A <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format used by the data object when interpreting the data contained in the storage medium.</param>
		/// <param name="pmedium">A <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure, passed by reference, that defines the storage medium in which the data is being passed.</param>
		/// <param name="fRelease">true to specify that the data object called, which implements <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.SetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@,System.Boolean)" />, owns the storage medium after the call returns. This means that the data object must free the medium after it has been used by calling the ReleaseStgMedium function. false to specify that the caller retains ownership of the storage medium, and the data object called uses the storage medium for the duration of the call only.</param>
		/// <exception cref="T:System.NotImplementedException">This method does not support the type of the underlying data object.</exception>
		// Token: 0x060004E8 RID: 1256 RVA: 0x00003D19 File Offset: 0x00001F19
		void IDataObject.SetData(ref FORMATETC formatIn, ref STGMEDIUM medium, bool release)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040002BB RID: 699
		private DataObject.Entry entries;

		// Token: 0x02000068 RID: 104
		private class Entry
		{
			// Token: 0x060004E9 RID: 1257 RVA: 0x000132F2 File Offset: 0x000114F2
			internal Entry(string type, object data, bool autoconvert)
			{
				this.type = type;
				this.data = data;
				this.autoconvert = autoconvert;
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001330F File Offset: 0x0001150F
			// (set) Token: 0x060004EB RID: 1259 RVA: 0x00013317 File Offset: 0x00011517
			public object Data
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

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x060004EC RID: 1260 RVA: 0x00013320 File Offset: 0x00011520
			public bool AutoConvert
			{
				get
				{
					return this.autoconvert;
				}
			}

			// Token: 0x060004ED RID: 1261 RVA: 0x00013328 File Offset: 0x00011528
			public static int Count(DataObject.Entry entries)
			{
				int num = 0;
				while (entries != null)
				{
					num++;
					entries = entries.next;
				}
				return num;
			}

			// Token: 0x060004EE RID: 1262 RVA: 0x00013349 File Offset: 0x00011549
			public static DataObject.Entry Find(DataObject.Entry entries, string type)
			{
				return DataObject.Entry.Find(entries, type, false);
			}

			// Token: 0x060004EF RID: 1263 RVA: 0x00013354 File Offset: 0x00011554
			public static DataObject.Entry Find(DataObject.Entry entries, string type, bool only_convertible)
			{
				while (entries != null)
				{
					bool flag = true;
					if (only_convertible && !entries.autoconvert)
					{
						flag = false;
					}
					if (flag && string.Compare(entries.type, type, true) == 0)
					{
						return entries;
					}
					entries = entries.next;
				}
				return null;
			}

			// Token: 0x060004F0 RID: 1264 RVA: 0x00013394 File Offset: 0x00011594
			public static DataObject.Entry FindConvertible(DataObject.Entry entries, string type)
			{
				DataObject.Entry entry = DataObject.Entry.Find(entries, type);
				if (entry != null)
				{
					return entry;
				}
				if (type == DataFormats.StringFormat || type == DataFormats.Text || type == DataFormats.UnicodeText)
				{
					for (entry = entries; entry != null; entry = entry.next)
					{
						if (entry.type == DataFormats.StringFormat || entry.type == DataFormats.Text || entry.type == DataFormats.UnicodeText)
						{
							return entry;
						}
					}
				}
				return null;
			}

			// Token: 0x060004F1 RID: 1265 RVA: 0x0001341C File Offset: 0x0001161C
			public static string[] Entries(DataObject.Entry entries, bool convertible)
			{
				ArrayList arrayList = new ArrayList(DataObject.Entry.Count(entries));
				DataObject.Entry entry = entries;
				if (convertible)
				{
					DataObject.Entry entry2 = DataObject.Entry.Find(entries, DataFormats.Text);
					DataObject.Entry entry3 = DataObject.Entry.Find(entries, DataFormats.UnicodeText);
					DataObject.Entry entry4 = DataObject.Entry.Find(entries, DataFormats.StringFormat);
					bool flag = entry2 != null && entry2.AutoConvert;
					bool flag2 = entry3 != null && entry3.AutoConvert;
					bool flag3 = entry4 != null && entry4.AutoConvert;
					if (flag || flag2 || flag3)
					{
						arrayList.Add(DataFormats.StringFormat);
						arrayList.Add(DataFormats.UnicodeText);
						arrayList.Add(DataFormats.Text);
					}
				}
				while (entry != null)
				{
					if (!arrayList.Contains(entry.type))
					{
						arrayList.Add(entry.type);
					}
					entry = entry.next;
				}
				string[] array = new string[arrayList.Count];
				for (int i = 0; i < arrayList.Count; i++)
				{
					array[i] = (string)arrayList[i];
				}
				return array;
			}

			// Token: 0x040002BC RID: 700
			private string type;

			// Token: 0x040002BD RID: 701
			private object data;

			// Token: 0x040002BE RID: 702
			private bool autoconvert;

			// Token: 0x040002BF RID: 703
			internal DataObject.Entry next;
		}
	}
}
