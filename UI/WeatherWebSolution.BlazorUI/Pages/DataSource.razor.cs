using WeatherWebSolution.Domain.Base;

namespace WeatherWebSolution.BlazorUI.Pages
{
    public partial class DataSource
    {
        private DataSourceInfo _createdNewSouceModel = new ();

        private IList<DataSourceInfo> _sources;

        private async Task UpdateSource() => _sources = (await _repository.GetAll()).ToList();

        protected override async Task OnInitializedAsync() => await UpdateSource();

        private async Task RefreshButton_OnClick() => await UpdateSource();

        private async Task Remove(DataSourceInfo source)
        {
            if (_sources is not { Count: > 0 }) return;

            var removed = await _repository.Delete(source);
            if (removed is not null) _sources.Remove(removed);
        }

        private async Task OnSourceCreated()
        {
            var created_source = await _repository.Add(_createdNewSouceModel);
            if(created_source is null) return;
            _sources.Add(created_source);
            _createdNewSouceModel = new();
        }
    }
}
