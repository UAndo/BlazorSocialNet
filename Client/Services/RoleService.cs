using System.Net.Http.Json;
using BlazorSocialNet.Client.Services;
using BlazorSocialNet.Entities.Models;

public class RoleService : IRoleService
{
    /// <summary>
    /// The HTTP client
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="RoleService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public RoleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets the roles.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Role>?> GetRoles()
    {
        var result = await _httpClient.GetFromJsonAsync<IEnumerable<Role>>("api/role");
        Console.WriteLine(result);
        return result;
    }

    /// <summary>
    /// Gets the role.
    /// </summary>
    /// <param name="roleId">The role identifier.</param>
    /// <returns></returns>
    public async Task<Role?> GetRole(Guid roleId)
    {
        return await _httpClient.GetFromJsonAsync<Role>($"api/role/{roleId}");
    }

    public async Task<bool> CreateRole(Role role)
    {
        var response = await _httpClient.PostAsJsonAsync("api/role", role);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Updates the role.
    /// </summary>
    /// <param name="roleId">The role identifier.</param>
    /// <param name="role">The role.</param>
    /// <returns></returns>
    public async Task<bool> UpdateRole(Guid roleId, Role role)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/role/{roleId}", role);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Deletes the role.
    /// </summary>
    /// <param name="roleId">The role identifier.</param>
    /// <returns></returns>
    public async Task<bool> DeleteRole(Guid roleId)
    {
        var response = await _httpClient.DeleteAsync($"api/role/{roleId}");
        return response.IsSuccessStatusCode;
    }
}
