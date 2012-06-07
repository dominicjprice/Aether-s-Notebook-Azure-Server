using System.Data.SqlTypes;

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

    public partial class UserCatalog
    {
        public enum StatusCode 
        { 
            Default = 0,
            InstallationRequestSent,
            InstallationRejected,
            InstallationAccepted,
            AccessTokenRequestSent,
            AccessTokenRequestSucceeded,
            AccessTokenRequestFailed
        }
    }
}
