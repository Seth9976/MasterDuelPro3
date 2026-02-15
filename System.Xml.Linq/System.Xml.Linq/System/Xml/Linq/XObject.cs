using System;

namespace System.Xml.Linq
{
	/// <summary>Represents a node or an attribute in an XML tree. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200001F RID: 31
	public abstract class XObject : IXmlLineInfo
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00004A55 File Offset: 0x00002C55
		internal XObject()
		{
		}

		/// <summary>Gets the base URI for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the base URI for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004EE4 File Offset: 0x000030E4
		public string BaseUri
		{
			get
			{
				XObject xobject = this;
				BaseUriAnnotation baseUriAnnotation;
				for (;;)
				{
					if (xobject == null || xobject.annotations != null)
					{
						if (xobject == null)
						{
							goto IL_0033;
						}
						baseUriAnnotation = xobject.Annotation<BaseUriAnnotation>();
						if (baseUriAnnotation != null)
						{
							break;
						}
						xobject = xobject.parent;
					}
					else
					{
						xobject = xobject.parent;
					}
				}
				return baseUriAnnotation.baseUri;
				IL_0033:
				return string.Empty;
			}
		}

		/// <summary>Gets the node type for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>The node type for this <see cref="T:System.Xml.Linq.XObject" />. </returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C7 RID: 199
		public abstract XmlNodeType NodeType { get; }

		/// <summary>Gets the parent <see cref="T:System.Xml.Linq.XElement" /> of this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>The parent <see cref="T:System.Xml.Linq.XElement" /> of this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004F29 File Offset: 0x00003129
		public XElement Parent
		{
			get
			{
				return this.parent as XElement;
			}
		}

		/// <summary>Adds an object to the annotation list of this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <param name="annotation">An <see cref="T:System.Object" /> that contains the annotation to add.</param>
		// Token: 0x060000C9 RID: 201 RVA: 0x00004F38 File Offset: 0x00003138
		public void AddAnnotation(object annotation)
		{
			if (annotation == null)
			{
				throw new ArgumentNullException("annotation");
			}
			if (this.annotations == null)
			{
				object obj;
				if (!(annotation is object[]))
				{
					obj = annotation;
				}
				else
				{
					(obj = new object[1])[0] = annotation;
				}
				this.annotations = obj;
				return;
			}
			object[] array = this.annotations as object[];
			if (array == null)
			{
				this.annotations = new object[] { this.annotations, annotation };
				return;
			}
			int num = 0;
			while (num < array.Length && array[num] != null)
			{
				num++;
			}
			if (num == array.Length)
			{
				Array.Resize<object>(ref array, num * 2);
				this.annotations = array;
			}
			array[num] = annotation;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004FD0 File Offset: 0x000031D0
		private object AnnotationForSealedType(Type type)
		{
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					if (this.annotations.GetType() == type)
					{
						return this.annotations;
					}
				}
				else
				{
					foreach (object obj in array)
					{
						if (obj == null)
						{
							break;
						}
						if (obj.GetType() == type)
						{
							return obj;
						}
					}
				}
			}
			return null;
		}

		/// <summary>Get the first annotation object of the specified type from this <see cref="T:System.Xml.Linq.XObject" />. </summary>
		/// <returns>The first annotation object that matches the specified type, or null if no annotation is of the specified type.</returns>
		/// <typeparam name="T">The type of the annotation to retrieve.</typeparam>
		// Token: 0x060000CB RID: 203 RVA: 0x00005034 File Offset: 0x00003234
		public T Annotation<T>() where T : class
		{
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					return this.annotations as T;
				}
				foreach (object obj in array)
				{
					if (obj == null)
					{
						break;
					}
					T t = obj as T;
					if (t != null)
					{
						return t;
					}
				}
			}
			return default(T);
		}

		/// <summary>Gets a value indicating whether or not this <see cref="T:System.Xml.Linq.XObject" /> has line information.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Linq.XObject" /> has line information, otherwise false.</returns>
		// Token: 0x060000CC RID: 204 RVA: 0x0000509E File Offset: 0x0000329E
		bool IXmlLineInfo.HasLineInfo()
		{
			return this.Annotation<LineInfoAnnotation>() != null;
		}

		/// <summary>Gets the line number that the underlying <see cref="T:System.Xml.XmlReader" /> reported for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the line number reported by the <see cref="T:System.Xml.XmlReader" /> for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000050AC File Offset: 0x000032AC
		int IXmlLineInfo.LineNumber
		{
			get
			{
				LineInfoAnnotation lineInfoAnnotation = this.Annotation<LineInfoAnnotation>();
				if (lineInfoAnnotation != null)
				{
					return lineInfoAnnotation.lineNumber;
				}
				return 0;
			}
		}

		/// <summary>Gets the line position that the underlying <see cref="T:System.Xml.XmlReader" /> reported for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the line position reported by the <see cref="T:System.Xml.XmlReader" /> for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000050CC File Offset: 0x000032CC
		int IXmlLineInfo.LinePosition
		{
			get
			{
				LineInfoAnnotation lineInfoAnnotation = this.Annotation<LineInfoAnnotation>();
				if (lineInfoAnnotation != null)
				{
					return lineInfoAnnotation.linePosition;
				}
				return 0;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000050EB File Offset: 0x000032EB
		internal bool HasBaseUri
		{
			get
			{
				return this.Annotation<BaseUriAnnotation>() != null;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000050F8 File Offset: 0x000032F8
		internal bool NotifyChanged(object sender, XObjectChangeEventArgs e)
		{
			bool flag = false;
			XObject xobject = this;
			for (;;)
			{
				if (xobject == null || xobject.annotations != null)
				{
					if (xobject == null)
					{
						break;
					}
					XObjectChangeAnnotation xobjectChangeAnnotation = xobject.Annotation<XObjectChangeAnnotation>();
					if (xobjectChangeAnnotation != null)
					{
						flag = true;
						if (xobjectChangeAnnotation.changed != null)
						{
							xobjectChangeAnnotation.changed(sender, e);
						}
					}
					xobject = xobject.parent;
				}
				else
				{
					xobject = xobject.parent;
				}
			}
			return flag;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000514C File Offset: 0x0000334C
		internal bool NotifyChanging(object sender, XObjectChangeEventArgs e)
		{
			bool flag = false;
			XObject xobject = this;
			for (;;)
			{
				if (xobject == null || xobject.annotations != null)
				{
					if (xobject == null)
					{
						break;
					}
					XObjectChangeAnnotation xobjectChangeAnnotation = xobject.Annotation<XObjectChangeAnnotation>();
					if (xobjectChangeAnnotation != null)
					{
						flag = true;
						if (xobjectChangeAnnotation.changing != null)
						{
							xobjectChangeAnnotation.changing(sender, e);
						}
					}
					xobject = xobject.parent;
				}
				else
				{
					xobject = xobject.parent;
				}
			}
			return flag;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000519F File Offset: 0x0000339F
		internal void SetBaseUri(string baseUri)
		{
			this.AddAnnotation(new BaseUriAnnotation(baseUri));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000051AD File Offset: 0x000033AD
		internal void SetLineInfo(int lineNumber, int linePosition)
		{
			this.AddAnnotation(new LineInfoAnnotation(lineNumber, linePosition));
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000051BC File Offset: 0x000033BC
		internal bool SkipNotify()
		{
			XObject xobject = this;
			for (;;)
			{
				if (xobject == null || xobject.annotations != null)
				{
					if (xobject == null)
					{
						break;
					}
					if (xobject.Annotation<XObjectChangeAnnotation>() != null)
					{
						return false;
					}
					xobject = xobject.parent;
				}
				else
				{
					xobject = xobject.parent;
				}
			}
			return true;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000051F8 File Offset: 0x000033F8
		internal SaveOptions GetSaveOptionsFromAnnotations()
		{
			XObject xobject = this;
			object obj;
			for (;;)
			{
				if (xobject == null || xobject.annotations != null)
				{
					if (xobject == null)
					{
						break;
					}
					obj = xobject.AnnotationForSealedType(typeof(SaveOptions));
					if (obj != null)
					{
						goto Block_3;
					}
					xobject = xobject.parent;
				}
				else
				{
					xobject = xobject.parent;
				}
			}
			return SaveOptions.None;
			Block_3:
			return (SaveOptions)obj;
		}

		// Token: 0x04000050 RID: 80
		internal XContainer parent;

		// Token: 0x04000051 RID: 81
		internal object annotations;
	}
}
