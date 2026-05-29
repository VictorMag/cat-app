using System.Net.Http.Json;
using CatApi.Models;

namespace CatApi.Services;

public class TheCatApiClient(HttpClient httpClient)
{
    public Task<List<BreedDto>?> GetBreedsAsync()
        => httpClient.GetFromJsonAsync<List<BreedDto>>("breeds");

    public Task<List<CatImageDto>?> GetImagesByBreedAsync(string breedId, int limit)
        => httpClient.GetFromJsonAsync<List<CatImageDto>>($"images/search?breed_ids={breedId}&limit={limit}");
}
