using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherWebSolution.Intefaces.Base.Entities;
using WeatherWebSolution.Intefaces.Base.Entities.Reposytories;

namespace WeatherWebSolution.WebAPIClients.Repositories
{
    public class WebRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly HttpClient _client;

        public WebRepository(HttpClient client)
        {
            _client = client;
        }

        #region Add : Метод добавление сущности item в БД

        public async Task<T> Add(T item, CancellationToken cancel = default)
        {
            var response = await _client.PostAsJsonAsync("", item, cancel).ConfigureAwait(false);
            var result = await response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>()
                .ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Delete : Метод удаления сущности item из БД

        public async Task<T> Delete(T item, CancellationToken cancel = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "")
            {
                Content = JsonContent.Create(item)
            };

            var response = await _client.SendAsync(request, cancel).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }

            var result = await response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancellationToken: cancel)
                .ConfigureAwait(false);
            return result;
        }

        #endregion

        #region DeleteById : Метод удаления сущности по id мз БД

        public async Task<T> DeleteById(int id, CancellationToken cancel = default)
        {
            var response = await _client.DeleteAsync($"{id}", cancel).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }

            var result = await response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancellationToken: cancel)
                .ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Exist : Метод определяющий наличие в БД элемента item

        public async Task<bool> Exist(T item, CancellationToken cancel = default)
        {
            var response = await _client.PostAsJsonAsync("exist", item, cancel).ConfigureAwait(false);
            return response.StatusCode != HttpStatusCode.NotFound && response.IsSuccessStatusCode;
        }

        #endregion

        #region ExistId : Метод определяющий наличие в БД элемента с id

        public async Task<bool> ExistId(int id, CancellationToken cancel = default)
        {
            var response = await _client.GetAsync($"/exist/id{id}", cancel).ConfigureAwait(false);
            return response.StatusCode != HttpStatusCode.NotFound && response.IsSuccessStatusCode;
        }

        #endregion

        #region Get : Метод получения перечисления count сущностей из БД, пропустив skip от начала

        public async Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancel = default) =>
            await _client
                .GetFromJsonAsync<IEnumerable<T>>($"items[{skip}-{count}]", cancel)
                .ConfigureAwait(false);

        #endregion

        #region  GetAll : Метод получения всех элементов в БД

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<IEnumerable<T>>("", cancel).ConfigureAwait(false);

        #endregion

        #region GetById : Метод получения сущносити из БД по id

        public async Task<T> GetById(int id, CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<T>($"{id}", cancel).ConfigureAwait(false);

        #endregion

        #region  GetCount : Метод получения количества count всех элементов в БД
        public async Task<int> GetCount(CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<int>("count").ConfigureAwait(false);

        #endregion

        #region GetPage : Метод получения страницы IPage с индесом pageIndex и размером pageSize

        public async Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            var response = await _client.GetAsync($"page/[{pageIndex}/{pageSize}]", cancel).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new PageItems
                {
                    Items = Enumerable.Empty<T>(),
                    TotalCount = 0,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };

            }
            return await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<PageItems>(cancellationToken: cancel)
                .ConfigureAwait(false);
        }

        #endregion

        #region Update : Метод обновления сущности item в БД по id

        public async Task<T> Update(T item, CancellationToken cancel = default)
        {
            var response = await _client.PutAsJsonAsync("", item, cancel).ConfigureAwait(false);
            var result = await response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>()
                .ConfigureAwait(false);
            return result;
        }

        #endregion

        private class PageItems : IPage<T>
        {
            public IEnumerable<T> Items { get; set; }
            public int TotalCount { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        }
    }
}
