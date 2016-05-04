using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditorSpace
{
    public class ToolBox
    {

        public static Tool GetTool(ToolCategory type)
        {
            switch(type)
            {
                case ToolCategory.Pen:
                    return new Pen();
                case ToolCategory.Line:
                    return new StraightLine();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
