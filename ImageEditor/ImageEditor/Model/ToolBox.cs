using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditorSpace
{
    public class ToolBox
    {

        public static Tool GetTool(ToolType type)
        {
            switch(type)
            {
                case ToolType.Pen:
                    return new Pen();
                case ToolType.Line:
                    return new StraightLine();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
