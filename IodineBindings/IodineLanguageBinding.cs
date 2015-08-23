using System;
using MonoDevelop.Projects;

namespace IodineBinding
{
	public class IodineLanguageBinding : ILanguageBinding
	{
		public bool IsSourceCodeFile (MonoDevelop.Core.FilePath fileName)
		{
			return fileName.FileName.EndsWith (".id");
		}

		public MonoDevelop.Core.FilePath GetFileName (MonoDevelop.Core.FilePath fileNameWithoutExtension)
		{
			return new MonoDevelop.Core.FilePath (fileNameWithoutExtension.FileName + ".id");
		}

		public string Language {
			get {
				return "Iodine";
			}
		}

		public string SingleLineCommentTag {
			get {
				return "#";
			}
		}

		public string BlockCommentStartTag {
			get {
				return "/*";
			}
		}

		public string BlockCommentEndTag {
			get {
				return "*/";
			}
		}
	}
}

