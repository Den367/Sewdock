using System.Threading;
using Mayando.Web.Extensions;

namespace Mayando.Web.Models
{
    public partial class MayandoContext
    {
        /// <summary>
        /// Returns all photos that are visible to the current user.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<Photo> UserVisiblePhotos
        {
            get
            {
                if (Thread.CurrentPrincipal.CanSeeAdministratorContent())
                {
                    return this.Photos;
                }
                else
                {
                    if ((this._UserVisiblePhotos == null))
                    {
                        this._UserVisiblePhotos = base.CreateQuery<Photo>("SELECT VALUE photo FROM [Photos] AS photo WHERE photo.Hidden=false");
                    }
                    return this._UserVisiblePhotos;
                }
            }
        }
        private global::System.Data.Objects.ObjectQuery<Photo> _UserVisiblePhotos;
    }
}