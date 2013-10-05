using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TheHiddenTruth.Library.Model;

namespace TheHiddenTruth.Library.Services
{
    /// <summary>
    /// Defines the service manager interface
    /// </summary>
    [ServiceKnownType(typeof(ObservableCollection<SiteModel>))]
    public interface IServiceManager
    {
        /// <summary>
        /// Loads the new dvd movies async.
        /// </summary>
        /// <returns>
        /// Represents an asynchronous operation that return a IEnumerable.
        /// </returns>
        Task GetDataBlogZaSeriozniHora(string pageToken, Action<PageModel, Exception> callback);

        /// <summary>
        /// Loads the top dvd movies async.
        /// </summary>
        /// <returns>Represents an asynchronous operation that return a IEnumerable.</returns>
        Task GetDataAlterInformation(int? pageToken, Action<PageModel, Exception> callback);

        Task Search(string searchQuery, Action<PageModel, Exception> callback);
    }
}
