using System;
using MonoDevelop.Projects;

namespace IodineBinding
{
	public class IodineProjectBinding : IProjectBinding
	{
		public string Name {
			get {
				return "Iodine";
			}
		}

		public Project CreateProject (ProjectCreateInformation info, System.Xml.XmlElement projectOptions)
		{
			return new IodineProject (info, projectOptions);
		}

		public Project CreateSingleFileProject (string sourceFile)
		{
			return new IodineProject ();
		}

		public bool CanCreateSingleFileProject (string sourceFile)
		{
			return true;
		}

	}
}

