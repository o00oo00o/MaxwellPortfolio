using System.Threading.Tasks;
using System.Windows.Input;

namespace MaxExperiment.Util
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
