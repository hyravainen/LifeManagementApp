using System.Text.Json.Serialization;

namespace LifeManagementApp.Services.Dto;

public class OpenMeteoResponse
{
    [JsonPropertyName("current")]
    public OpenMeteoCurrent? Current { get; set; }
}
