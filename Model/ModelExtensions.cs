using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

using Microsoft.SqlServer.Types;

namespace Aethers.Notebook.Model
{
    public partial class Location
    {
        public SqlGeography Position
        {
            get { return SqlGeography.STGeomFromWKB(new SqlBytes(positionBinary.ToArray()), 4326); }
        }
    }

    public partial class PositionLocationChanged
    {
        public SqlGeography Position
        {
            get { return SqlGeography.STGeomFromWKB(new SqlBytes(positionBinary.ToArray()), 4326); }
        }
    }
}
