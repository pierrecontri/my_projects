using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CarsCostSimulator.modules
{
    class XmlCarManage
    {
        //getName = lambda classObj : classObj.__name__
	    //clsmembers = map(getName, listClassSimulator)

        int indent = 0;
		object tmpObject = null;
		object tmpParentObject = null;
        string actual_element = string.Empty;

        public XmlCarManage(string databasefile, bool demomode)
        {
/*
		self.p = xml.parsers.expat.ParserCreate()
		if demomode:
			self.p.StartElementHandler = self.start_element_show
			self.p.EndElementHandler = self.end_element_show
			self.p.CharacterDataHandler = self.char_data_show
		else:
			self.p.StartElementHandler = self.start_element
			self.p.EndElementHandler = self.end_element
			self.p.CharacterDataHandler = self.char_data

		if databaseFile != "" and pth.exists(databaseFile):
			self.p.ParseFile(open(databaseFile))

 */
        }
    }
}
