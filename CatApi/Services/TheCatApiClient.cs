namespace CatApi.Services;

public class TheCatApiClient(HttpClient httpClient)
{
    public Task<string> GetBreedsAsync()
        => httpClient.GetStringAsync("breeds");

    public Task<string> GetImagesByBreedAsync(string breedId, int limit)
        => httpClient.GetStringAsync($"images/search?breed_ids={breedId}&limit={limit}");
}
