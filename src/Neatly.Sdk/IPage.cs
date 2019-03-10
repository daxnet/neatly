using System.Threading.Tasks;

namespace Neatly.Sdk
{
    public interface IPage
    {
        /// <summary>
        /// Shows the current page on the main window.
        /// </summary>
        /// <returns>The task which executes the show page operation.</returns>
        Task ShowAsync(INeatlyShell shell);
    }
}
