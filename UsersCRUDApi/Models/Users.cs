using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UsersCRUDApi.Models;

public class UsersResponse
{
    [JsonPropertyName("data")]
    public List<User> Users { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("support")]
    public SupportDTO Support { get; set; }
}

public class SupportDTO
{
    [JsonPropertyName("url")]
    public string URL { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }
}

public class User
{
    [JsonPropertyName("id")]
    public int ID { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }
}

